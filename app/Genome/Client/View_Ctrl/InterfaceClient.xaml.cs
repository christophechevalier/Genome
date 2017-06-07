﻿using Client.Tools;
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
        }
    }
}
