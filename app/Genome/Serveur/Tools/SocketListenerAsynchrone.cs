using Serveur.Systems;
using Serveur.Tools;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Serveur.View_Ctrl
{
    class SocketListenerAsynchrone
    {
        // Thread signal.
        public ManualResetEvent allDone = new ManualResetEvent(false);
        ObjectSerializer serializer;
        ReadAndInterpretMessage interpret;

        public SocketListenerAsynchrone(SystemOrchestrateur conn)
        {
            interpret = new ReadAndInterpretMessage(conn);
            serializer = new ObjectSerializer();
        }

        public void StartListening()
        {
            // Tableau de Byte pour la réception de données
            byte[] bytes = new Byte[1024];

            // Créé l'IPEndPoint pour le socket avec l'adresse de ce poste sur le port 80
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostAddresses(Dns.GetHostName())[0]);
            GetLocalAddress address = new GetLocalAddress();
            IPAddress ipAddress;
            if (IPAddress.TryParse(address.GetAddress(), out ipAddress)) { }
            else
            {
                // Quitte la méthode sinon
                return;
            }
            
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 80);

            // Créé le socket TCP/IP
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Charge le socket avec l'IPEndPoint et écoute les connexions
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Remet à zéro l'écoute d'évenement
                    allDone.Reset();

                    // Démarre une écoute asynchrone par le socket
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Attends une connexion pour continuer
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            Console.Read();

        }


        public void AcceptCallback(IAsyncResult ar)
        {
            // Signale au thread de continuer
            this.allDone.Set();

            // Récupère le socket qui gère la requête du client
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Créé le StateObject
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            byte[] content;

            // Récupère le StateObject et le socket asynchrone
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Lis les données du socket client
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // Si il y a des données, alors on les stock
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                // Vérifie la présence du tag de fin, si il n'y est pas, on lit plus de données
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // Toute les données ont étées reçues, on les affiche en console
                    Console.WriteLine("Données reçues : \n {0}", content);
                    // Renvoie les données au client
                    //Send(handler, content);
                    serializer.Deserialize<Message>(content);
                }
                else
                {
                    // Si toutes les données n'ont pas étées reçues, on continue de lire
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void Send(Socket handler, String data)
        {
            // Converti les données du format string au format byte par l'ASCII
            //byte[] byteData = Encoding.ASCII.GetBytes(data);
            byte[] byteData = Encoding.ASCII.GetBytes("Message reçu !");

            // Envoie des données au poste distant
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Récupère le socket du StateObject
                Socket handler = (Socket)ar.AsyncState;

                // Termine l'envois des données au poste distant
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Réponse de {0} bytes envoyée au calculateur.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
