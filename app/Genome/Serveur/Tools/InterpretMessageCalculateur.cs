using Serveur.Entity;
using Serveur.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Tools
{
    class InterpretMessageCalculateur
    {
        SystemCalculateur systemCalc;

        #region Constructeur
        public InterpretMessageCalculateur(SystemCalculateur system)
        {
            this.systemCalc = system;
        }
        #endregion

        public void RetrieveMessage(Message message)
        {
            switch (message.GetKey())
            {
                // Création
                case 0:
                    break;
                // Changement Status (libre, occupé, erreur)
                case 1:
                    break;
                default:
                    break;
            }
        }
    }
}
