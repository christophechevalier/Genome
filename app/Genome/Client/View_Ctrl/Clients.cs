using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.View_Ctrl
{
    class Clients
    {
        #region Propriétés
        public InterfaceClient interClient { get; set; }
        public string adresseOrchestrateur;
        #endregion

        #region Constructeur
        public Clients()
        {
            adresseOrchestrateur = "";
        }
        #endregion

        public void SendFile()
        {
            if(adresseOrchestrateur == "")
            {
                MessageError("Aucune connexion à un orchestrateur");
            }
        }

        public void SetButtons()
        {
            interClient.BtnEnvoiFichier.Click += (sender, args) => { SendFile(); };
        }

        public void MessageError(string message)
        {
            interClient.LabelMessage.Visibility = System.Windows.Visibility.Visible;
            interClient.LabelMessage.Content = message;
        }
    }
}
