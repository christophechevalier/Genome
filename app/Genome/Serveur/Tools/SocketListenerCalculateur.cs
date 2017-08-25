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
    class SocketListenerCalculateur
    {
        ObjectSerializer serializer;
        Message message;
        IPEndPoint end;
        Socket sock;
        SystemCalculateur system;
        ReadAndInterpretMessage interpretMessage;

        public SocketListenerCalculateur(SystemCalculateur system)
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
            Thread ecoute = new Thread(new ThreadStart(ReceiveMessage));
            ecoute.Start();
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
