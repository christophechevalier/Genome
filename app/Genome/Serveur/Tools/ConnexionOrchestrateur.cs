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
    }
}
