using Serveur.CAD;
using Serveur.Entity;
using Serveur.Tools;
using Serveur.View_Ctrl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Serveur.Systems
{
    public class SystemOrchestrateur
    {
        #region Propriétés
        IPEndPoint end;
        Socket sock;
        public static string path;
        public static string message = "Stopped";
        public ObservableCollection<Calculateur> listeCalculateurs;
        public OrchestrateurCAD orchCad;
        public InterfaceOrchestrateur interfOrch;
        public Job jobAsked;
        private GetLocalAddress getAdress;
        private SocketListenerOrchestrateur listener;
        #endregion

        #region Constructeur
        public SystemOrchestrateur(InterfaceOrchestrateur interfaceOrchestrateur)
        {
            this.interfOrch = interfaceOrchestrateur;
            this.orchCad = new OrchestrateurCAD();
            this.listeCalculateurs = new ObservableCollection<Calculateur>();
            this.orchCad.Calculateurs = this.listeCalculateurs;
            this.getAdress = new GetLocalAddress();
            this.interfOrch.adresseIp.Content = getAdress.GetAddress();
            this.interfOrch.DataContext = this;
            this.interfOrch.DataGridCalculators.ItemsSource = orchCad.Calculateurs;
            this.listener = new SocketListenerOrchestrateur(this);
            this.end = new IPEndPoint(IPAddress.Any, 2015);
            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            startEcouteFile();
            startEcouteMessage();
        }
        #endregion
        private void startEcouteMessage()
        {
            Thread ecoute = new Thread(new ThreadStart(listener.startListening));
            ecoute.IsBackground = true;
            ecoute.Start();
        }

        private void startEcouteFile()
        {
            sock.Bind(end);
            Task taskReceiveFile = Task.Run(() => ReceiveFile());
        }

        public void AddCalculateurList(string ip)
        {
            bool trouv = false;
            foreach (Calculateur calcul in listeCalculateurs)
            {
                if (calcul.IP == ip)
                {
                    trouv = true;
                }
            }
            if (!trouv)
            {
                Calculateur calc = new Calculateur();
                calc.IP = ip;
                calc.Status = Status.Connecte;

                System.Windows.Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Normal,
                    (Action)delegate()
                    {
                        listeCalculateurs.Add(calc);
                    }
                );
            }
        }

        public void ChangeStatus(string ip, Status status)
        {
            foreach (Calculateur calc in listeCalculateurs)
            {
                if (calc.IP == ip)
                {
                    calc.Status = status;
                }
            }
        }

        public void DeleteCalculateur(string ip)
        {
            foreach (Calculateur calc in listeCalculateurs)
            {
                if (calc.IP == ip)
                {
                    listeCalculateurs.Remove(calc);
                }
            }
        }

        // Méthode de démarrage du serveur
        public void StartServer()
        {
            try
            {
                // Création de l'endpoint et du port d'écoute
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 2017);
                // Création du socket internetwork
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                // Binding de l'endpoint
                socket.Bind(endPoint);
                Console.WriteLine("start server");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR : " + e);
            }
        }

        // Méthode retournant le nombre de serveur disponible
        public int NombreServeurDisponible()
        {
            int number = 0;
            foreach (Calculateur calc in listeCalculateurs)
            {
                if (calc.Status.Equals("OK"))
                {
                    number++;
                }
            }
            return number;
        }

        // méthode pour recevoir un fichier envoyé par un client
        public void ReceiveFile()
        {
            // Démarrage du serveur
            StartServer();
            //while (true)
            //{
                try
                {
                    // Création de l'objet serialiseur
                    ObjectSerializer serializer = new ObjectSerializer();
                    // Création de l'objet de récupération du fichier
                    FileData file = new FileData();
                    // Ecoute du socket avec une limite de 100
                    sock.Listen(100);
                    // Acceptation des client par le socket
                    Socket client = sock.Accept();
                    // Création d'un nouveau tableau de stockage des données en bytes avec comme limite 25Mo
                    byte[] clientData = new byte[1024 * 25000];
                    // Réception des données
                    int receiveByteLength = client.Receive(clientData);
                    // Déserialisation des données et convertion en type FileData
                    file = serializer.Deserialize<FileData>(clientData) as FileData;
                    // Récupération de la taille du fichier
                    int fileNameLength = BitConverter.ToInt32(file.Content, 0);
                    // Récupération du nom du fichier
                    string fileName = Encoding.ASCII.GetString(file.Content, 4, fileNameLength);
                    // Ecriture du fichier à la racine de l'executable du serveur
                    BinaryWriter writer = new BinaryWriter(File.Open(Directory.GetCurrentDirectory() + "/" + file.FileName, FileMode.Append));
                    // Démarrage de l'écriture et de la sauvegarde du fichier
                    writer.Write(clientData, 4 + fileNameLength, receiveByteLength - 4 - fileNameLength);
                    // Fermeture de l'écriture
                    writer.Close();
                    Console.WriteLine("File Received");
                    // Création de l'objet MapReducer avec comme paramètre le fichier reçu et l'Id du chunk demandé
                    MapReducer reducer = new MapReducer(fileName, this);
                    // Récupération du chunk demandé sous forme de liste
                    //List<string> chunk = reducer.CreateChunk();
                    //break;
                }
                catch(Exception e)
                {
                    Console.WriteLine("ERROR : " + e);
                }
            //}
        }
    }
}
