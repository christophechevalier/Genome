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
using System.Windows.Media.Animation;
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
        /// <summary>
        /// Constructeur de l'interface cliente
        /// </summary>
        #region Constructeur
        public InterfaceClient()
        {
            InitializeComponent();
            ConnexionClient client = new ConnexionClient();
            client.SetInputInterfaceClient(this);
            init_Selection_Traitement();
            createProgressBar(100);
        }
        #endregion

        #region Initialisation Listeners
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
            DataSelect.Items.Add(" Prédire la couleur des yeux ");
        }
        #endregion

        /// <summary>
        /// Méthode pour afficher le bandeau déroulant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 50;
            da.To = RenderSize.Width + RenderSize.Height;
            da.RepeatBehavior = RepeatBehavior.Forever;
            da.AutoReverse = false;
            da.Duration = new Duration(TimeSpan.FromSeconds(12));

            LabelMessage.Content = "INFORMATIONS : ''Rien a signalé ! Aucun problème n'a été détecté..'' ";
            // LabelMessage.Content = something.message;
            LabelMessage.BeginAnimation(Label.WidthProperty, da);
        }

        private void createProgressBar(int i)
        {
            ProgressBar ProgStatusBar = new ProgressBar();
            ProgStatusBar.IsIndeterminate = false;
            ProgStatusBar.Orientation = Orientation.Horizontal;
            ProgStatusBar.Width = 400;
            ProgStatusBar.Height = 25;

            Duration dur = new Duration(TimeSpan.FromSeconds(30));
            DoubleAnimation dblAnim = new DoubleAnimation(200.0, dur);
            ProgStatusBar.BeginAnimation(ProgressBar.ValueProperty, dblAnim);
            ProgStatusBar.Value = i;
            StatusBar.Items.Add(ProgStatusBar);
        }
    }
}
