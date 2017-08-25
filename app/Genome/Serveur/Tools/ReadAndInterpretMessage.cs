using Serveur.Entity;
using Serveur.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    class ReadAndInterpretMessage
    {
        SystemOrchestrateur connexionOrch;

        #region Constructeur
        public ReadAndInterpretMessage(SystemOrchestrateur conn)
        {
            this.connexionOrch = conn;
        }
        #endregion

        public void RetrieveMessage(Message message)
        {
            switch (message.GetKey())
            {
                // Création
                case 0:
                    connexionOrch.AddCalculateurList(message.GetMessage());
                    break;
                // Changement Status (libre, occupé, erreur)
                case 1:
                    connexionOrch.ChangeStatus(message.GetIpCalculateur(), (Status)Enum.Parse(typeof(Status), message.GetMessage()));
                    break;
                default:
                    break;
            }
        }
    }
}
