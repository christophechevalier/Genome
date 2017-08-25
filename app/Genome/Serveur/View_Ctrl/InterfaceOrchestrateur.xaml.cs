using Serveur.CAD;
using Serveur.Entity;
using Serveur.Systems;
using Serveur.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
