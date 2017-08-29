using Serveur.Entity;
using Serveur.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    public class Connexion
    {
        private String response = String.Empty;
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);
        private SystemOrchestrateur connexionOrchestrateur;
        public Connexion(SystemOrchestrateur connexionOrchestrateur)
        {
            this.connexionOrchestrateur = connexionOrchestrateur;
        }

        public Connexion()
        {

        }

        public void SendMessage(Calculateur calculateur, byte[] content)
        {
            /*
            byte[] bytes = new byte[1024 * 25000];
            byte[] endEnvoi = Encoding.ASCII.GetBytes("<EOFILE>");
            byte[] contenu = new byte[content.Length + endEnvoi.Length];
            for (int i = 0; i < content.Length; i++)
            {
                contenu[i] = content[i];
            }
            for (int i = 0; i < endEnvoi.Length; i++)
            {
                contenu[content.Length + i] = endEnvoi[i];
            }
            IPAddress ip;
            if (IPAddress.TryParse(calculateur.IP, out ip)) { }
            else { return; }
            IPEndPoint ipEnd = new IPEndPoint(ip, 2014);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            clientSocket.Connect(ipEnd);
            clientSocket.Send(contenu);
            */

            //byte[] bytes = new byte[1024];
            //IPAddress ip;
            //if (IPAddress.TryParse(calculateur.IP, out ip)) { }
            //else { return; }
            //IPEndPoint ipEnd = new IPEndPoint(ip, 2014);
            //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            //clientSocket.Connect(ipEnd);
            //clientSocket.Send(content);
            //if(clientSocket.Poll(10000, SelectMode.SelectRead))
            //{
            //    clientSocket.Receive(bytes);
            //}
            //else
            //{
            //    // mise en carrafe du calculateur
            //}
            //clientSocket.Close();

            try
            {
                IPAddress ip;
                if (IPAddress.TryParse(calculateur.IP, out ip)) { }
                else
                {
                    // Quitte la méthode sinon
                    return;
                }
                IPEndPoint remoteEP = new IPEndPoint(ip, 2014);

                // Create a TCP/IP socket.
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                //connectDone.WaitOne();
                string chaine = "<EOFILE>";
                byte[] chaineByte = Encoding.UTF8.GetBytes(chaine);
                byte[] finalObjectSerialized = new byte[content.Length + chaineByte.Length];
                for (int i = 0; i < content.Length; i++)
                {
                    finalObjectSerialized[i] = content[i];
                }
                for (int i = 0; i < chaineByte.Length; i++)
                {
                    finalObjectSerialized[i + content.Length] = chaineByte[i];
                }
                // Send test data to the remote device.
                Send(client, content);
                //sendDone.WaitOne();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;
                // Complete the connection.
                client.EndConnect(ar);
                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());
                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Send(Socket client, byte[] data)
        {
            client.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public int SendMessage(string Ip, byte[] content)
        {
            byte[] bytes = new byte[1024 * 25000];
            IPAddress ip;
            if (IPAddress.TryParse(Ip, out ip)) { }
            else { return 1; }
            IPEndPoint ipEnd = new IPEndPoint(ip, 2014);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                clientSocket.Connect(ipEnd);
                clientSocket.Send(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 2;
            }
            if (clientSocket.Poll(10000, SelectMode.SelectRead))
            {
                clientSocket.Receive(bytes);
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
