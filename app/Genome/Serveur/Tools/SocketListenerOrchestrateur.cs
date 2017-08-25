using Serveur.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    class SocketListenerOrchestrateur
    {
        ObjectSerializer serializer;
        Message message;
        IPEndPoint end;
        Socket sock;
        SystemOrchestrateur system;
        ReadAndInterpretMessage interpretMessage;

        public SocketListenerOrchestrateur(SystemOrchestrateur system)
        {
            this.serializer = new ObjectSerializer();
            this.system = system;
            this.interpretMessage = new ReadAndInterpretMessage(system);
        }

        public void startListening()
        {
            end = new IPEndPoint(IPAddress.Any, 2014);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(end);
            //Task taskReceiveFile = Task.Run(() => ReceiveFile());
            Thread ecoute = new Thread(new ThreadStart(ReceiveMessage));
            ecoute.Start();
        }

        public void StartEcoute()
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

        public void ReceiveMessage()
        {
            // Démarrage du serveur
            //StartEcoute();
            while (true)
            {
                try
                {
                    // Ecoute du socket avec une limite de 100
                    sock.Listen(100);
                    // Acceptation des client par le socket
                    Socket client = sock.Accept();
                    // Création d'un nouveau tableau de stockage des données en bytes avec comme limite 25Mo
                    byte[] clientData = new byte[1024 * 25000];
                    // Réception des données
                    int receiveByteLength = client.Receive(clientData);
                    // Déserialisation des données et convertion en type FileData
                    message = serializer.Deserialize<Message>(clientData) as Message;
                    // Récupération de la taille du fichier
                    //int fileNameLength = BitConverter.ToInt32(file.Content, 0);
                    // Récupération du nom du fichier
                    //string fileName = Encoding.ASCII.GetString(file.Content, 4, fileNameLength);
                    // Ecriture du fichier à la racine de l'executable du serveur
                    //BinaryWriter writer = new BinaryWriter(File.Open(Directory.GetCurrentDirectory() + "/" + file.FileName, FileMode.Append));
                    // Démarrage de l'écriture et de la sauvegarde du fichier
                    //writer.Write(clientData, 4 + fileNameLength, receiveByteLength - 4 - fileNameLength);
                    // Fermeture de l'écriture
                    //writer.Close();
                    Console.WriteLine("Message received");
                    // Création de l'objet MapReducer avec comme paramètre le fichier reçu et l'Id du chunk demandé
                    //MapReducer reducer = new MapReducer(fileName, 1, this);
                    // Récupération du chunk demandé sous forme de liste
                    //List<string> chunk = reducer.CreateChunk();
                    interpretMessage.RetrieveMessage(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR : " + e);
                }
            }
        }

    }
}
