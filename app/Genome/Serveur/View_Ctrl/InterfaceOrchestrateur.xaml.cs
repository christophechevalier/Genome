using System.Windows.Controls;

namespace Serveur.View_Ctrl
{
    /// <summary>
    /// Logique d'interaction pour InterfaceOrchestrateur.xaml
    /// </summary>
    public partial class InterfaceOrchestrateur : Page
    {
        #region Propriétés
        private SocketListenerAsynchrone listener;
        #endregion

        #region Constructeur
        public InterfaceOrchestrateur()
        {
            InitializeComponent();

            //SystemOrchestrateur connexionOrchestrateur = new SystemOrchestrateur(this);

            //listener = new SocketListenerAsynchrone(connexionOrchestrateur);

            //Thread ecoute = new Thread(new ThreadStart(listener.StartListening));
            //ecoute.Start();

            //OrchestrateurCAD orchCad = new OrchestrateurCAD();
            //connexionOrchestrateur.SetOrchestrateurCad(orchCad);

            //this.DataContext = this;
            //DataGridCalculators.ItemsSource = orchCad.Calculateurs;        
        }
        #endregion
    }
}
