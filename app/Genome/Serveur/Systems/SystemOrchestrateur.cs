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
        public InterfaceOrchestrateur orchestrateur;
        public Job jobAsked;
        private SocketListenerAsynchrone listener;
        #endregion

        #region Constructeur
        public SystemOrchestrateur(InterfaceOrchestrateur interfaceOrchestrateur)
        {
            this.orchestrateur = interfaceOrchestrateur;
            this.orchCad = new OrchestrateurCAD();
            end = new IPEndPoint(IPAddress.Any, 2014);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(end);
            Task taskReceiveFile = Task.Run(() => ReceiveFile());

            listener = new SocketListenerAsynchrone(this);
            Thread ecoute = new Thread(new ThreadStart(listener.StartListening));
            ecoute.Start();
            
            SetOrchestrateurCad(orchCad);

            orchestrateur.DataContext = this;
            orchestrateur.DataGridCalculators.ItemsSource = orchCad.Calculateurs;
        }

        public void SetInputInterfaceOrchestrateur(InterfaceOrchestrateur orchestrateur)
        {
            this.orchestrateur = orchestrateur;
        }

        public void SetOrchestrateurCad(OrchestrateurCAD orchCAD)
        {
            this.orchCad = orchCAD;
            this.orchCad.Calculateurs = this.listeCalculateurs;
        }
        #endregion




        public void AddCalculateurList(string ip)
        {
            Calculateur calc = new Calculateur();
            calc.IP = ip;
            calc.Status = Status.Libre;
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
            while (true)
            {
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
                }
                catch(Exception e)
                {
                    Console.WriteLine("ERROR : " + e);
                }
            }
        }

        // Méthode pour envoyer un message 
        public void envoiMessage(string message)
        {
            // Tableau de Byte pour la réception de données
            byte[] bytes = new byte[1024];

            // Connection à un appareil distant
            try
            {
                // Met en place un IPEndPoint pour la connexion par socket, ici port 80 et adresse de ce poste
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostAddresses(Dns.GetHostName())[0]);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 80);

                // Création du socket TCP/IP
                Socket sock = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                // Connection du socket à l'IPEndPoint avec récupération des exceptions
                try
                {
                    sock.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}", sock.RemoteEndPoint.ToString());

                    // Encode la chaine de caractère en un tableau de byte
                    byte[] msg = Encoding.ASCII.GetBytes(message + "<EOF>");

                    // Envoie les données par le socket
                    int bytesSent = sock.Send(msg);

                    // Reçois la réponse du poste distant
                    int bytesRec = sock.Receive(bytes);
                    Console.WriteLine("Réponse de l'orchestrateur = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Libère le socket
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
