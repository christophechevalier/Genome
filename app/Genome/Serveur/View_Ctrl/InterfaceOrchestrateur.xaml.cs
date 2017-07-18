using Serveur.Entity;
using Serveur.Tools;
using System;
using System.Collections.Generic;
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
        private SocketListenerAsynchrone listener;

        public InterfaceOrchestrateur()
        {
            InitializeComponent();
            ConnexionOrchestrateur connexionOrchestrateur = new ConnexionOrchestrateur();
            MyDataGrid.ItemsSource = loadCollectionData();
            listener = new SocketListenerAsynchrone();
            Thread ecoute = new Thread(new ThreadStart(listener.StartListening));
            ecoute.Start();
        }

        private List<Calculateur> loadCollectionData()
        {
            List<Calculateur> calc = new List<Calculateur>();
            calc.Add(new Calculateur()
            {
                Id = 1,
                Name = "Container 1",
                Ip = "192.168.1.15",
                NbCore = 4,
                Cpu = 50,
                Status = "Ok",
                Task = 75
            });
            return calc;
        }
    }
}
