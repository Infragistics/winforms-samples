using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Showcase.CashflowDashboard
{
    /// <summary>
    /// A control that shows some information about the current month, previous month, and previous year. 
    /// </summary>
    public partial class SummaryBlock : UserControl
    {
        #region Private Members

        // Color and pen that will be user to draw the bottom border of each SummaryBlock control. 
        // Note that the SummaryBlock also draws a top border, but we don't need a constant or
        // Pen for that, because it's a named color (Black).
        private Color bottomBorderColor = Color.FromArgb(35, 62, 91);
        private Pen bottomBorderPen;        
        #endregion // Private Members

        #region Constructor
        public SummaryBlock()
        {
            InitializeComponent();
                        
            // This is neccessary so that we can do custom border drawing in OnPaintBackground. 
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        } 
        #endregion // Constructor

        #region Methods

        #region DisposeResources
        /// <summary>
        /// Disposes any resources created by the control. 
        /// </summary>
        private void DisposeResources()
        {
            // Dispose the bottom border pen. 
            // Note that we do not need to worry about the top border pen
            // because it's using a name color and therefore a static framework pen.
            if (null != this.bottomBorderPen)
            {
                this.bottomBorderPen.Dispose();
                this.bottomBorderPen = null;
            }
        }
        #endregion // DisposeResources 

        #endregion // Methods

        #region Properties

        #region Private Properties

        #region BottomBorderPen
        /// <summary>
        /// The pen is used to draw the bottom border. 
        /// Note that the SummaryBlock also draws a top border, but we don't need a constant or
        /// Pen for that, because it's a named color (Black).
        /// </summary>
        private Pen BottomBorderPen
        {
            get
            {
                if (null == this.bottomBorderPen)
                    this.bottomBorderPen = new Pen(this.bottomBorderColor);

                return this.bottomBorderPen;
            }
        }
        #endregion // BottomBorderPen

        #endregion // Private Properties

        #region Public Properties

        #region TitleText
        /// <summary>
        /// Gets or sets the text of the Title. 
        /// </summary>
        [Localizable(true)]
        public string TitleText
        {
            get { return this.lblTitle.Text; }
            set { this.lblTitle.Text = value; }
        }
        #endregion // TitleText

        #region CurrentValue
        /// <summary>
        /// Gets or sets the text of the current value. 
        /// </summary>
        public string CurrentValue
        {
            get { return this.lblCurrentlValue.Text; }
            set { this.lblCurrentlValue.Text = value; }
        }
        #endregion // CurrentValue

        #region LastMonthValue
        /// <summary>
        /// Gets or sets the text of the previous month's value. 
        /// </summary>
        public string LastMonthValue
        {
            get { return this.lblLastMonthValue.Text; }
            set { this.lblLastMonthValue.Text = value; }
        }
        #endregion // LastMonthValue

        #region LastYearValue
        /// <summary>
        /// Gets or sets the text of the previous year's value. 
        /// </summary>
        public string LastYearValue
        {
            get { return this.lblLastYearValue.Text; }
            set { this.lblLastYearValue.Text = value; }
        }
        #endregion // LastYearValue

        #endregion // Public Properties 

        #endregion // Properties

        #region OnPaintBackground
        /// <summary>
        /// Overriden to draw a top and bottom border on the control.
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            // Draw a cool-looking top and bottom border.
            Point topLeft = Utilities.GetPoint(this, ContentAlignment.TopLeft);
            Point topRight = Utilities.GetPoint(this, ContentAlignment.TopRight);
            Point bottomLeft = Utilities.GetPoint(this, ContentAlignment.BottomLeft);
            Point bottomRight = Utilities.GetPoint(this, ContentAlignment.BottomRight);

            e.Graphics.DrawLine(this.BottomBorderPen, topLeft, topRight);
            e.Graphics.DrawLine(Pens.Black, bottomLeft, bottomRight);
        } 
        #endregion // OnPaintBackground       
    }
}
