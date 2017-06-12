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

namespace Serveur.View_Ctrl
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Propriétés
        private InterfaceServeur interServ { get; set; }
        private InterfaceCalculateur interCalc { get; set; }
        private InterfaceOrchestrateur interOrch { get; set; }
        #endregion

        #region Constructeur
        public MainWindow()
        {
            InitializeComponent();

            this.interServ = new InterfaceServeur();
            MainframeServeur.Content = interServ;
            init_Listeners_Serveur();
        }
        #endregion

        #region Initinialisation Listeners
        #region Initialisation Serveur
        /// <summary>
        /// Méthode qui contient l'ensemble des listeners de la vue SERVEUR
        /// </summary>
        private void init_Listeners_Serveur()
        {
            // Click sur le bouton ouvrir la vue calculateur
            interServ.BtnOuvrirCalculateur.Click += delegate (object sender, RoutedEventArgs e)
            {
                this.interCalc = new InterfaceCalculateur();
                init_Listeners_Calculateur();
                MainframeServeur.Content = interCalc;
            };

            // Click sur le bouton ouvrir la vue orchestrateur
            interServ.BtnOuvrirOrchestrateur.Click += delegate (object sender, RoutedEventArgs e)
            {
                this.interOrch = new InterfaceOrchestrateur();
                init_Listeners_Orchestrateur();
                MainframeServeur.Content = interOrch;
            };
        }
        #endregion
        #region Initialisation Calculateur
        /// <summary>
        /// Méthode qui contient l'ensemble des listeners de la vue CALCULATEUR
        /// </summary>
        private void init_Listeners_Calculateur()
        {
            // Click sur le bouton retour vers le MENU SERVEUR
            interCalc.BtnRetourMenuServeur.Click += delegate (object sender, RoutedEventArgs e)
            {
                MainframeServeur.Content = interServ;
            };
        }
        #endregion
        #region Initialisation Orchestrateur
        /// <summary>
        /// Méthode qui contient l'ensemble des listeners de la vue ORCHESTRATEUR
        /// </summary>
        private void init_Listeners_Orchestrateur()
        {
            // Click sur le bouton retour vers le MENU SERVEUR
            interOrch.BtnRetourMenuServeur.Click += delegate (object sender, RoutedEventArgs e)
            {
                MainframeServeur.Content = interServ;
            };
        }
        #endregion
        #endregion
    }
}
