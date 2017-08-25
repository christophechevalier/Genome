using Serveur.Tools;
using Serveur.View_Ctrl;
using System;
using System.Windows;

namespace Serveur.Systems
{
    class SystemCalculateur
    {
        private InterfaceCalculateur interf;
        private ObjectSerializer serializer;
        private GetLocalAddress getAdress;

        public SystemCalculateur(InterfaceCalculateur interf)
        {
            this.serializer = new ObjectSerializer();
            this.getAdress = new GetLocalAddress();
            this.interf = interf;
            this.interf.BtnAppareillage.Click += new RoutedEventHandler(onClickAppareillage);
        }

        private void onClickAppareillage(object sender, EventArgs e)
        {
            string ip = interf.IpAdress.Text;
            Message message = new Message(getAdress.GetAddress(), "", 0, getAdress.GetAddress());
            Byte[] tabMessage = serializer.Serialize(message);
            //conn.SendMessage(ip, tabMessage);
        }
    }
}
