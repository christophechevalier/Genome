using Serveur.Entity;
using Serveur.Systems;

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
                    systemCalc.threadM.traitement(message.GetMessage(), Job.AnalyseQuantitative);
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
