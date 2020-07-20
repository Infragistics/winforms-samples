using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Showcase.CashflowDashboard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           // load the style library from the embedded resource
            System.Reflection.Assembly assm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream stream = assm.GetManifestResourceStream("StyleLibraries.CashflowDashboard.isl");
            if (stream != null)
                Infragistics.Win.AppStyling.StyleManager.Load(stream);

            Application.Run(new MainForm());
        }
    }
}
