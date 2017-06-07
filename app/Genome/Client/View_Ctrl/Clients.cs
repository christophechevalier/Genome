using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.View_Ctrl
{
    class Clients
    {
        public InterfaceClient interfaceClient;
        public string adresseOrchestrateur;

        public Clients()
        {
            adresseOrchestrateur = "";
        }
        public void SendFile()
        {
            if(adresseOrchestrateur == "")
            {
                MessageError("Aucune connecxion à un orchestrateur");
            }
        }

        public void SetButtons()
        {
            interfaceClient.ButtonFile.Click += (sender, args) => { SendFile();};
        }

        public void MessageError(string message)
        {
            interfaceClient.messagePanel.Visibility = System.Windows.Visibility.Visible;
            interfaceClient.messagePanel.Content = message;
        }
    }
}
