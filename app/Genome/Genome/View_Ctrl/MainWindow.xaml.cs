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
using Client.View_Ctrl;

namespace Client.View_Ctrl
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Propriétés
        private View_Ctrl.InterfaceClient ui_client { get; set; }
        #endregion

        #region Constructeur
        public MainWindow()
        {
            InitializeComponent();

            this.ui_client = new InterfaceClient();

            MainframeClient.Content = ui_client;
        }
        #endregion
    }
}
