using Showcase.INGear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INGear
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // load the style library from the embedded resource
            System.Reflection.Assembly assm = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream stream = assm.GetManifestResourceStream("Showcase.INGear.StyleLibraries.INGear.isl");
            Infragistics.Win.AppStyling.StyleManager.Load(@"..\..\..\StyleLibraries\INGear.isl");


            Application.Run(new MainForm());
        }
    }
}
