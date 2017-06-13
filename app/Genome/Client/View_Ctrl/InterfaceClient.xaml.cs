using Client.Tools;
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
    /// Logique d'interaction pour InterfaceClient.xaml
    /// </summary>
    public partial class InterfaceClient : Page
    {
        public InterfaceClient()
        {
            InitializeComponent();
            ConnexionClient client = new ConnexionClient();
            client.SetInputInterfaceClient(this);
            init_Selection_Traitement();
        }

        /// <summary>
        /// Initialisation de la liste du menu
        /// </summary>
        private void init_Selection_Traitement()
        {
            TitleChoixTraitement.Content = "Traitement - Prédiction - Analyse & Recherche";
            DataSelect.Text = "-- Choisir un traitement --  ";
            DataSelect.Items.Add(" Analyse Quantitative sur les génomes ");
            DataSelect.Items.Add(" Rechercher une séquence donnée dans un génome ");
            DataSelect.Items.Add(" Transformer les séquences d'ADN vers les acides animés ");
            DataSelect.Items.Add(" Trouver des gènes ");
            DataSelect.Items.Add(" Prédir la couleur des yeux ");
        }
    }
}
