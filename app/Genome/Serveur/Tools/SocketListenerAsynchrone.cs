using Serveur.Entity;
using Serveur.Systems;
using Serveur.Tools;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Serveur.View_Ctrl
{
    public class SocketListenerAsynchrone
    {
        public ManualResetEvent allDone = new ManualResetEvent(false);
        public SocketListenerCalculateur socketListenerCalc;

        public SocketListenerAsynchrone(SocketListenerCalculateur sock)
        {
            this.socketListenerCalc = sock;
        }

        public void StartListening()
        {
            Thread.Sleep(10000);
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 2014);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(ReadCallback), state);
        }
        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);
            // There  might be more data, so store the data received so far.
            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

            // Check for end-of-file tag. If it is not there, read 
            // more data.
            content = state.sb.ToString();
            //Console.WriteLine(content);
            if (state.buffer.Length != 4096)
            {
                // All the data has been read from the 
                // client. Display it on the console.                    
                Console.WriteLine("Message received");
                byte[] contenu = new byte[state.buffer.Length - 5];
                for (int i = 0; i < contenu.Length; i++)
                {
                    contenu[i] = state.buffer[i];
                }
                ChunkData chunk = (ChunkData)socketListenerCalc.serializer.Deserialize<ChunkData>(contenu);
                socketListenerCalc.interpretMessage.RetrieveMessage(chunk);
                AcceptCallback(ar);
            }
            else
            {
                // Not all data received. Get more.
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
        }
    }
}
