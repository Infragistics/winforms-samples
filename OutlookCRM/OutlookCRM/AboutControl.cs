using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace OutlookCRM
{
    public partial class AboutControl : UserControl
    {

        #region Constructor

        public AboutControl()
        {
            InitializeComponent();

            this.InitializeUI();
        }

        #endregion //Constructor

        #region Methods

        #region InitializeUI

        private void InitializeUI()
        {
            this.lblAppName.Text = Properties.Resources.Title;
            this.lblDescription.Text = Properties.Resources.Description;
            this.lblCompany.Text = string.Format("{0}: {1}", Properties.Resources.Publisher, AboutControl.Company);
            this.lblCopyright.Text = AboutControl.Copyright;
            this.lblVersion.Text = string.Format("{0}: {1}", Properties.Resources.Version, AboutControl.Version);

            Stream stream = Utilities.GetEmbeddedResourceStream("OutlookCRM.Images.Logo.PNG");
            Image logo = Image.FromStream(stream);
            this.pbLogo.Size = logo.Size;
            this.pbLogo.Image = logo;
        }

        #endregion // InitializeUI

        #endregion Methods

        #region Properties

        #region ApplicationName

        /// <summary>
        /// Gets the name of the application
        /// </summary>
        internal static string ApplicationName
        {
            get { return Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title); }
        }

        #endregion // ApplicationName

        #region Description

        /// <summary>
        /// Gets the description of the application
        /// </summary>
        internal static string Description
        {
            get { return Utilities.GetAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description); }
        }

        #endregion // Description

        #region Company

        /// <summary>
        /// Gets the company
        /// </summary>
        internal static string Company
        {
            get { return Utilities.GetAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company ); }
        }

        #endregion // Company

        #region Copyright

        /// <summary>
        /// Gets the copyright information.
        /// </summary>
        internal static string Copyright
        {
            get { return Utilities.GetAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright); }
        }

        #endregion // Copyright

        #region Version

        /// <summary>
        /// Gets the build version information.
        /// </summary>
        internal static string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        #endregion // Version

        #endregion //Properties
    }
}
