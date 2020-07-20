using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Resources;

namespace IGPaint
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

        #region Properties

        #region ApplicationName

        /// <summary>
        /// Gets the name of the application
        /// </summary>
        internal static string ApplicationName
        {
            get { return GetAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title); }
        }

        #endregion // ApplicationName

        #region Description

        /// <summary>
        /// Gets the description of the application
        /// </summary>
        internal static string Description
        {
            get { return GetAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description); }
        }

        #endregion // Description

        #region Company

        /// <summary>
        /// Gets the company
        /// </summary>
        internal static string Company
        {
            get { return GetAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company); }
        }

        #endregion // Company

        #region Copyright

        /// <summary>
        /// Gets the copyright information.
        /// </summary>
        internal static string Copyright
        {
            get { return GetAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright); }
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

        #region Methods

        #region GetLocalizedString
        /// <summary>
        /// Localizes a string using the ResourceManager.
        /// </summary>
        /// <param name="currentString"></param>
        /// <returns></returns>
        internal static string GetLocalizedString(string currentString)
        {
            ResourceManager rm = IGPaint.Properties.Resources.ResourceManager;
            string localizedString = rm.GetString(currentString);
            return (string.IsNullOrEmpty(localizedString) ? currentString : localizedString).Replace('_', ' ');
        }
        #endregion // GetLocalizedString

        #region InitializeUI

        private void InitializeUI()
        {
            this.lblAppName.Text = GetLocalizedString(AboutControl.ApplicationName);
            this.lblDescription.Text = GetLocalizedString(AboutControl.Description);
            this.lblCompany.Text = string.Format("{0}: {1}", GetLocalizedString("Publisher"), AboutControl.Company);
            this.lblCopyright.Text = GetLocalizedString(AboutControl.Copyright);
            this.lblVersion.Text = string.Format("{0}: {1}", GetLocalizedString("Version"), AboutControl.Version);

            System.IO.Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("IGPaint.Images.Logo.PNG");
            Image logo = Image.FromStream(stream);
            this.pbLogo.Size = logo.Size;
            this.pbLogo.Image = logo;
        }

        #endregion // InitializeUI

        #region GetAssemblyAttribute

        /// <summary>
        /// Retrieves the string representation of the request AssemblyAttribute from the project's AssemblyInfo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string GetAssemblyAttribute<T>(Func<T, string> value)
            where T : Attribute
        {
            T attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }

        #endregion // GetAssemblyAttribute

        #endregion //Methods
    }
}
