using Serveur.Systems;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Serveur.Tools
{
    class SocketListenerOrchestrateur
    {
        ObjectSerializer serializer;
        Message message;
        IPEndPoint end;
        Socket sock;
        SystemOrchestrateur system;
        InterpretMessageOrchestrateur interpretMessage;

        public SocketListenerOrchestrateur(SystemOrchestrateur system)
        {
            this.serializer = new ObjectSerializer();
            this.system = system;
            this.interpretMessage = new InterpretMessageOrchestrateur(system);
        }

        public void startListening()
        {
            try
            {
                end = new IPEndPoint(IPAddress.Any, 2014);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                sock.Bind(end);
                Thread ecoute = new Thread(new ThreadStart(ReceiveMessage));
                ecoute.IsBackground = true;
                ecoute.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    sock.Listen(100);
                    Socket client = sock.Accept();
                    byte[] clientData = new byte[1024 * 25000];
                    int receiveByteLength = client.Receive(clientData);
                    message = serializer.Deserialize<Message>(clientData) as Message;
                    Console.WriteLine("Message received");
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
