using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    class ConnexionOrchestrateur
    {
        #region Propriétés
        IPEndPoint end;
        Socket sock;

        public static string path;
        public static string message = "Stopped";
        #endregion

        #region Constructeur
        public ConnexionOrchestrateur()
        {
            end = new IPEndPoint(IPAddress.Any, 2014);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(end);
        }
        #endregion

        public void Connexion()
        {
            while (true)
            {
                try
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 2017);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    socket.Bind(endPoint);

                }
                catch
                {

                }
            }
        }

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

                Console.WriteLine("Passage 1");
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
