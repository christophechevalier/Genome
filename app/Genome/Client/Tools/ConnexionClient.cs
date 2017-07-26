using Client.View_Ctrl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Serveur.Entity;
using Serveur.Tools;

namespace Client.Tools
{
    class ConnexionClient
    {
        // adresse IP de l'orchestrateur
        private static InterfaceClient interfaceClient;
        private static readonly object padlock = new object();
        public static InterfaceClient InterfaceClient
        {
            get
            {
                lock (padlock)
                {
                    if(interfaceClient == null)
                    {
                        interfaceClient = new InterfaceClient();
                    }
                    return interfaceClient;
                }
            }
        }
        public static string ip = "127.0.0.1";
        public static string message = "Idle";

        // méthode pour définir le clic du bouton envoi Fichier
        public void SetInputInterfaceClient(InterfaceClient client)
        {
            interfaceClient = client;
            client.BtnEnvoiFichier.Click += new RoutedEventHandler(delegate 
            {
                
                OpenFile();
            });
        }

        // méthode pour ouvrir le file dialog
        void OpenFile()
        {
            FileDialog fileDialog = new OpenFileDialog();
            // Limite la selection aux fichiers texte
            fileDialog.Filter = "txt files (*.txt)|*.txt";
            if (fileDialog.ShowDialog() == true)
            {
                // retourne si un fichier a bien été selectionné
                CheckFileIntegrity(fileDialog.FileName);
                Console.WriteLine("finis");
            }
        }

        void CheckFileIntegrity(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            for (int i = 1; i != lines.Length - 1; i++)
            {
                string[] value = lines[i].Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                char[] letters = value[3].ToCharArray();
                foreach (char letter in letters)
                {
                    if (letter != 'A' && letter != 'C' && letter != 'T' && letter != 'G' && letter != '-' && letter != 'I' && letter != 'D')
                    {
                        Console.WriteLine(value[2] + " : " + value[3]);
                        return;
                    }

                }
            }
            Console.WriteLine("Fichier intègre");
            SendFile(fileName, ip);
        }

        void SendFile(string fileName, string address)
        {
            IPAddress ip;
            FileData file = new FileData();
            file.FileSize = GetFileSize(fileName);
            file.Id = int.Parse(DateTime.Now.Ticks.ToString());
            file.FileName = GetFileName(fileName);
            file.Job = DetermineJob();
            // check si le string adresse est bien au format IPV4
            if (IPAddress.TryParse(address, out ip)) { }
            else
            {
                // Quitte la méthode sinon
                return;
            }

            // informations de port local
            IPEndPoint ipEnd = new IPEndPoint(ip, 2014);

            // Création du socket réseau utilisant le protocole IP
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            // Définition du chemin réel du fichier
            string filePath = "";
            fileName = fileName.Replace("\\", "/");
            while (fileName.IndexOf("/") > -1)
            {
                filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                fileName = fileName.Substring(fileName.IndexOf("/") + 1);
            }

            // Traduit le fichier en tableau de bytes
            byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);

            // Encode toutes les données en bytes avant l'envois
            message = "Buffering ...";
            byte[] fileData = File.ReadAllBytes(filePath + fileName);
            byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
            byte[] fileNameLength = BitConverter.GetBytes(fileNameByte.Length);

            // copie les données du client dans les tableaux de bytes avec un index de 4 pour éviter les caractères de racine (c://)
            fileNameLength.CopyTo(clientData, 0);
            fileNameByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNameByte.Length);

            file.Content = clientData;
            ObjectSerializer serializer = new ObjectSerializer();
            byte[] objectSerialized = serializer.Serialize(file);

            // Connexion au client
            message = "Connection to server ...";
            clientSocket.Connect(ipEnd);

            // Envois du fichier au client
            message = "File Sending ...";
            clientSocket.Send(objectSerialized);

            // Fermeture de la connexion
            message = "Disconnecting ...";
            clientSocket.Close();
            message = "File transfered";
        }

        float GetFileSize(string fileName)
        {
            float value = new float();
            FileInfo fileInfo = new FileInfo(fileName);
            value = fileInfo.Length;
            Console.WriteLine(value);
            return value;
        }

        Job DetermineJob()
        {
            Job job = new Job();
            switch (interfaceClient.DataSelect.SelectedIndex)
            {
                case 0:
                    job = Job.AnalyseQuantitative;
                    break;
                case 1:
                    job = Job.RechercherSequence;
                    break;
                case 2:
                    job = Job.TransformerSequence;
                    break;
                case 3:
                    job = Job.TrouverGene;
                    break;
                case 4:
                    job = Job.PredireYeux;
                    break;
            }
            return job;
        }

        string GetFileName(string fileName)
        {
            string name = "";
            FileInfo fileInfo = new FileInfo(fileName);
            name = fileInfo.Name;
            Console.WriteLine(name);
            return name;
        }
    }
}
