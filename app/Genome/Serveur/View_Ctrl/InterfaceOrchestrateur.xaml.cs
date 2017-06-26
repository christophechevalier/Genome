using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
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
        public InterfaceOrchestrateur()
        {
            InitializeComponent();
            getNumberOfCores();
        }

        /// <summary>
        /// Méthode pour récupérer le nombre de coeurs d'un calculateur
        /// </summary>
        private void getNumberOfCores()
        {
            int coreCount = 0;
            SelectQuery query = new SelectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            try
            {
                foreach (var item in searcher.Get())
                {
                    coreCount += int.Parse(item["NumberOfCores"].ToString());

                    Console.WriteLine("Nombre de coeur(s) : {0}", coreCount);
                    NbCores.Content = coreCount;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
