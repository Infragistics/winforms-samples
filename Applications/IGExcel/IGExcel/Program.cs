using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGExcel
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

            Infragistics.Win.Office2013ColorTable.Theme = Infragistics.Win.Office2013Theme.Excel;
            Application.Run(new frmMain());
        }
    }
}
