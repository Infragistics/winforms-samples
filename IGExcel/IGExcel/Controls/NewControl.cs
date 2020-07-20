using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.Misc;
using System.Reflection;
using Infragistics.Documents.Excel;
using System.IO;
using Infragistics.Win;
using Infragistics.Documents.Excel.ConditionalFormatting;
using Infragistics.Documents.Excel.Sorting;

namespace IGExcel.Controls
{
    public partial class NewControl : UserControl, IUIElementDrawFilter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NewControl"/> class.
        /// </summary>
        public NewControl()
        {
            InitializeComponent();
            this.InitializeUI();
        }

        #endregion //Constructor

        #region Methods

        #region CreateNewButton

        private void CreateNewButton(string key)
        {
            // Create a new button with the appropriate appearance.
            UltraButton button = new UltraButton()
            {
                ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2013Button,
                DrawFilter = this,
                ImageSize = new System.Drawing.Size(200, 150),
                Margin = new System.Windows.Forms.Padding(20),
                ShowFocusRect = false,
                ShowOutline = false,
                Size = new Size(250, 225),
                Tag = key,
                UseOsThemes = Infragistics.Win.DefaultableBoolean.False,
            };


            key = (string.IsNullOrEmpty(key)) ? "BlankDocument" : key.Replace(".xlsx", string.Empty).Split('.').Last();
            button.Text = key;
            Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance()
            {
                ImageHAlign = HAlign.Center,
                ImageVAlign = VAlign.Middle,
                TextHAlign = HAlign.Left,
                TextVAlign = VAlign.Bottom,
            };
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("IGExcel.Images.{0}.png", key)))
            {
                if (stream != null)
                {
                    Image image = Image.FromStream(stream);
                    appearance.Image = image;
                }
            }
            button.Appearance = appearance;
            
            // Add the button to the FlowLayoutPanel
            this.flowLayoutPanel1.Controls.Add(button);

            // Hook up the event handler
            button.Click += (sender, args) =>
            {
                this.CreateWorkbook(((UltraButton)sender).Tag as string);
            };
        }

        #endregion //CreateNewButton

        #region CreateWorkbook

        private void CreateWorkbook(string resource)
        {
            Workbook workbook;
            if (string.IsNullOrEmpty(resource))
            {
                workbook = new Workbook();
                workbook.Worksheets.Add("Sheet1");             
            }
            else
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    workbook = Workbook.Load(stream);
                }                

                Worksheet sheet2 = workbook.Worksheets[2];
                OperatorConditionalFormat condition1 = sheet2.ConditionalFormats.AddOperatorCondition("C5:C28", Infragistics.Documents.Excel.ConditionalFormatting.FormatConditionOperator.GreaterEqual);
                condition1.SetOperand1(2500);
                condition1.CellFormat.Font.ColorInfo = new WorkbookColorInfo(Color.Red);

                OperatorConditionalFormat condition2 = sheet2.ConditionalFormats.AddOperatorCondition("C5:C28", FormatConditionOperator.Less);
                condition2.SetOperand1(2500);
                condition2.CellFormat.Font.ColorInfo = new WorkbookColorInfo(Color.Green);     
                
                
                                                
            }
            this.OnNewWorkbookSelected(workbook);
        }

        #endregion //CreateWorkbook

        #region InitializeUI

        private void InitializeUI()
        {
            this.CreateNewButton(null);

            var spreadsheetResources = Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(resource => resource.EndsWith(".xlsx"));
            foreach (var resourceName in spreadsheetResources)
                this.CreateNewButton(resourceName);

            this.LocalizeStrings();
        }

        #endregion //InitializeUI

        #region LocalizeStrings

        private void LocalizeStrings()
        {
            System.Resources.ResourceManager rm = ResourceStrings.ResourceManager;
            this.lblNew.Text = rm.GetString("Text_New");

            foreach (Control control in this.flowLayoutPanel1.Controls)
                control.Text = rm.GetString(string.Format("Text_{0}", control.Text));

        }

        #endregion //LocalizeStrings

        #endregion //Methods

        #region IUIElementDrawFilter

        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            // Only draw the top and bottom border
            drawParams.DrawBorders(UIElementBorderStyle.Solid, Border3DSide.Top | Border3DSide.Bottom);
            return true;
        }

        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (drawParams.Element is UltraButtonUIElement)
                return DrawPhase.BeforeDrawBorders;
            return DrawPhase.None;
        }

        #endregion //IUIElementDrawFilter

        #region NewWorkbookSelected Event

        internal EventHandler<NewWorkbookEventArgs> NewWorkbookSelected;

        #region OnNewWorkbookSelected

        private void OnNewWorkbookSelected(Workbook workbook)
        {
            if (workbook != null &&
                this.NewWorkbookSelected != null)
                this.NewWorkbookSelected(this, new NewWorkbookEventArgs(workbook));
        }

        #endregion //OnNewWorkbookSelected

        #region NewWorkbookEventArgs

        internal class NewWorkbookEventArgs : EventArgs
        {
            internal NewWorkbookEventArgs(Workbook workbook)
                : base()
            {
                this.Workbook = workbook;
            }

            internal Workbook Workbook { get; private set; }

        }

        #endregion //NewWorkbookEventArgs

        #endregion //NewWorkbookSelected Event
    }
}
