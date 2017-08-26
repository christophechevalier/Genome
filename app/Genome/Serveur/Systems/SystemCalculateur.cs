using Serveur.Tools;
using Serveur.View_Ctrl;
using System;
using System.Windows;

namespace Serveur.Systems
{
    public class SystemCalculateur
    {
        private InterfaceCalculateur interf;
        private ObjectSerializer serializer;
        private GetLocalAddress getAdress;
        private Connexion conn;
        private SocketListenerCalculateur socketListener;
        private string ipOrch;
        public ThreadManager threadM;

        private string receiveMessage = "Elements envoyés et reçu";
        private string notReceiveMessage = "Elements envoyés";
        private string ipNotValid = "Adresse non valide";
        private string error = "Erreur, veuillez réessayer";
        private string traitement = "Appareillage en cours";

        public SystemCalculateur(InterfaceCalculateur interf)
        {
            this.serializer = new ObjectSerializer();
            this.getAdress = new GetLocalAddress();
            this.conn = new Connexion();
            this.threadM = new ThreadManager();
            this.interf = interf;
            this.interf.BtnAppareillage.Click += new RoutedEventHandler(onClickAppareillage);
            this.socketListener = new SocketListenerCalculateur(this);

            this.socketListener.startListening();
        }

        private void onClickAppareillage(object sender, EventArgs e)
        {
            interf.BoxInformations.Text = traitement;
            ipOrch = interf.IpAdress.Text;
            Message message = new Message(getAdress.GetAddress(), "", 0, getAdress.GetAddress());
            byte[] tabMessage = serializer.Serialize(message);
            int retour = conn.SendMessage(ipOrch, tabMessage);

            switch (retour)
            {
                case 0:
                    interf.BoxInformations.Text = receiveMessage;
                    break;
                case 1:
                    interf.BoxInformations.Text = notReceiveMessage;
                    break;
                case 2:
                    interf.BoxInformations.Text = ipNotValid;
                    break;
                default:
                    interf.BoxInformations.Text = error;
                    break;
            }
        }

        public void returnResults(string result)
        {
            Message messResult = new Message(result, "", 4, getAdress.GetAddress());
            byte[] tabMessage = serializer.Serialize(messResult);
            int retour = conn.SendMessage(ipOrch, tabMessage);
        }
    }
}
