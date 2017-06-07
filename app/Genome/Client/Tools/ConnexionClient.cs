using Client.View_Ctrl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Tools
{
    class ConnexionClient
    {
        // adresse IP de l'orchestrateur
        public static string ip;
        public static string message = "Idle";

        // méthode pour définir le clic du bouton envoi Fichier
        public void SetInputInterfaceClient(InterfaceClient client)
        {
            client.BtnEnvoiFichier.Click += new RoutedEventHandler(delegate 
            {
                // Appel de la méthode OpenFile au clic 
                OpenFile();
            });
        }

        // méthode pour ouvrir le file dialog
        void OpenFile()
        {
            FileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                // retourne si un fichier a bien été selectionné
                SendFile(fileDialog.FileName, ip);
            }
        }

        void SendFile(string fileName, string address)
        {
            IPAddress ip;

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

        }
    }
}
