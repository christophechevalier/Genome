using Serveur.Entity;
using Serveur.Systems;
<<<<<<< 7a8394ff800d502661b6197d672d128eb2b67d44
using Serveur.View_Ctrl;
=======
>>>>>>> Correction du binding et ajustement d'envois de messages
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Serveur.Tools
{
    public class SocketListenerCalculateur
    {
        public ObjectSerializer serializer;
        public IPEndPoint end;
        public Socket sock;
        public SystemCalculateur system;
        public InterpretMessageCalculateur interpretMessage;
        SocketListenerAsynchrone listener;

        public SocketListenerCalculateur(SystemCalculateur system)
        {
            this.serializer = new ObjectSerializer();
            this.system = system;
            this.interpretMessage = new InterpretMessageCalculateur(system);
            this.listener = new SocketListenerAsynchrone(this);

        }

        public void startListening()
        {
            /*
            end = new IPEndPoint(IPAddress.Any, 2014);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(end);
            Thread ecoute = new Thread(new ThreadStart(ReceiveMessage));
            ecoute.IsBackground = true;
            ecoute.Start();
            /*/
            Thread ecoute = new Thread(new ThreadStart(listener.StartListening));
            ecoute.IsBackground = true;
            ecoute.Start();
        }

        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    /*
                    sock.Listen(100);
                    Socket client = sock.Accept();
                    byte[] clientData = new byte[10240];

                    while (Convert.ToString(clientData[clientData.Length - 1]) != ">" )
                    {
                        client.Receive(clientData);
                    }
                    //int receiveByteLength = 
                    Console.WriteLine("Message received");
                    chunk = (ChunkData) serializer.Deserialize<ChunkData>(clientData);
                    interpretMessage.RetrieveMessage(chunk);
                    */
                    }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR : " + e);
                }

            }
        }
    }
}
