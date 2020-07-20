using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookCRM
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Infragistics.Win.AppStyling.StyleManager.Load(Utilities.GetEmbeddedResourceStream("OutlookCRM.Styling.Theme 01.isl"));

            Application.Run(new frmOutlookCRM());
        }
    }
}
