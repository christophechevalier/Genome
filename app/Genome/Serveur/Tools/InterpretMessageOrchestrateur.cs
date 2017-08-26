using Serveur.Entity;
using Serveur.Systems;
using System;

namespace Serveur.Tools
{
    class InterpretMessageOrchestrateur
    {
        SystemOrchestrateur systemOrch;

        #region Constructeur
        public InterpretMessageOrchestrateur(SystemOrchestrateur system)
        {
            this.systemOrch = system;
        }
        #endregion

        public void RetrieveMessage(Message message)
        {
            switch (message.GetKey())
            {
                // Création
                case 0:
                    systemOrch.AddCalculateurList(message.GetMessage());
                    break;
                // Changement Status (libre, occupé, erreur)
                case 1:
                    systemOrch.ChangeStatus(message.GetIpCalculateur(), (Status)Enum.Parse(typeof(Status), message.GetMessage()));
                    break;
                default:
                    break;
            }
        }
    }
}
