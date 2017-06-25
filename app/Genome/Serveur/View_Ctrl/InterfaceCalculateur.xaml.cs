﻿using Serveur.Tools;
using System;
using System.Management;
using System.Windows.Controls;

namespace Serveur.View_Ctrl
{
    /// <summary>
    /// Logique d'interaction pour InterfaceCalculateur.xaml
    /// </summary>
    public partial class InterfaceCalculateur : Page
    {
        private ConnexionOrchestrateur connexion;

        public InterfaceCalculateur()
        {
            InitializeComponent();
            connexion = new ConnexionOrchestrateur();
            connexion.envoiMessage("Salut à vous étranger !");
        }

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

            }
        }
    }
}
