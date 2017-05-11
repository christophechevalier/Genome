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
using Serveur.View_Ctrl;

namespace Serveur.View_Ctrl
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Propriétés
        private View_Ctrl.InterfaceServeur ui_serveur { get; set; }
        #endregion

        #region Constructeur
        public MainWindow()
        {
            InitializeComponent();

            this.ui_serveur = new InterfaceServeur();

            MainframeServeur.Content = ui_serveur;
        }
        #endregion
    }
}
