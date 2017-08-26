using Serveur.Entity;
using Serveur.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    public class Connexion
    {
        SystemOrchestrateur connexionOrchestrateur;
        public Connexion(SystemOrchestrateur connexionOrchestrateur)
        {
            this.connexionOrchestrateur = connexionOrchestrateur;
        }

        public Connexion()
        {

        }

        public void SendMessage(Calculateur calculateur, byte[] content)
        {
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
