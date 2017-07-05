using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Management;
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
        private SocketListenerAsynchrone listener;

        public InterfaceOrchestrateur()
        {
            InitializeComponent();

            listener = new SocketListenerAsynchrone();
            Thread ecoute = new Thread(new ThreadStart(listener.StartListening));
            ecoute.Start();
        }
    }
}
