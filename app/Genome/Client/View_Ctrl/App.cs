using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.View_Ctrl
{
    /// <summary>
    /// App
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class App : System.Windows.Application
    {
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            #line 4 "..\..\..\App.xaml"
                        this.StartupUri = new System.Uri("View_Ctrl/MainWindow.xaml", System.UriKind.RelativeOrAbsolute);

            #line default
            #line hidden
        }

        /// <summary>
        /// Point d'entrée de l'application
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Main()
        {
            Client.View_Ctrl.App app = new Client.View_Ctrl.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
