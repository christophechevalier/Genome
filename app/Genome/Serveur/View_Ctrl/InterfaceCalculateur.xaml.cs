using Serveur.Tools;
using System;
using System.Management;
using System.Windows;
using System.Windows.Controls;

namespace Serveur.View_Ctrl
{
    /// <summary>
    /// Logique d'interaction pour InterfaceCalculateur.xaml
    /// </summary>
    public partial class InterfaceCalculateur : Page
    {
        #region Propriétés
        private ConnexionOrchestrateur connexion;
        private ThreadManager threadM;
        #endregion

        #region Constructeur
        public InterfaceCalculateur()
        {
            InitializeComponent();
            connexion = new ConnexionOrchestrateur();
            threadM = new ThreadManager();
            //connexion.envoiMessage("Salut à vous étranger !");
            //threadM.traitement(1, "AACCGGTTFHRHAC", 1); // Réponse A:3 ; C:3 ; T:2 ; G:2 ; Incconu:4/

        }
        #endregion

        /// <summary>
        /// Méthode pour récupérer le nombre de coeurs d'un calculateur
        /// </summary>
        public void getNumberOfCores()
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "Une erreur s'est produite");
            }
        }

    }
}
