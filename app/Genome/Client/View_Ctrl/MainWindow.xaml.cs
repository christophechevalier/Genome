using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.View_Ctrl
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Propriétés
        private InterfaceClient interClient { get; set; }
        #endregion

        #region Constructeur
        public MainWindow()
        {
            InitializeComponent();

            this.interClient = new InterfaceClient();
            MainframeClient.Content = interClient;
            init_Listeners_Client();
        }
        #endregion

        #region Initinialisation Listeners
        #region Initialisation Client
        /// <summary>
        /// Méthode qui contient l'ensemble des listeners de la vue CLIENT
        /// </summary>
        private void init_Listeners_Client()
        {
            // Vue Client
        }
        #endregion
        #endregion

        #region Settings
        /// <summary>
        ///  Méthode définissant le comportement de l'application à la fermeture de la fenêtre
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
        #endregion
    }
}
