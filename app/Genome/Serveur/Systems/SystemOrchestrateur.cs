using Serveur.View_Ctrl;

namespace Serveur.Systems
{
    class SystemOrchestrateur
    {
        private InterfaceOrchestrateur interf;

        public SystemOrchestrateur(InterfaceOrchestrateur interf)
        {
            this.interf = interf;
        }
    }
}
