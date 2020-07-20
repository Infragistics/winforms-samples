using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;

namespace IGExcel.Dialogs
{
    internal partial class ZoomDialog : Form
    {
        #region Members

        private int zoomLevel;
        private bool suppressEvent;

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomDialog"/> class.
        /// </summary>
        /// <param name="zoomLevel">The zoom level.</param>
        internal ZoomDialog(int zoomLevel)
        {
            InitializeComponent();

            this.InitializeUI(zoomLevel);
        }

        #endregion //Constructor

        #region Base Class Overrides

        #region OnFormClosing

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (this.DialogResult == System.Windows.Forms.DialogResult.OK &&
                (this.ZoomLevel < 10 ||
                this.ZoomLevel > 400))
            {
                Infragistics.Win.UltraMessageBox.UltraMessageBoxManager.Show(ResourceStrings.Text_ZoomError_Message, ResourceStrings.Text_ZoomError_Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        #endregion //OnFormClosing

        #endregion //Base Class Overrides

        #region Properties

        #region ZoomLevel

        internal int ZoomLevel
        {
            get
            {
                return this.zoomLevel;
            }
            set
            {
                if (this.zoomLevel != value)
                {
                    this.zoomLevel = value;

                    if (this.suppressEvent == false)
                    {
                        this.suppressEvent = true;

                        this.numZoomLevel.Value = this.zoomLevel;
                        this.optPresetLevels.Value = this.zoomLevel;
                        if (this.optPresetLevels.Value == null)
                            this.optPresetLevels.Value = DBNull.Value;

                        this.suppressEvent = false;
                    }
                }
            }
        }

        #endregion //ZoomLevel

        #endregion //Properties

        #region Methods

        #region InitializeUI

        private void InitializeUI(int zoomLevel)
        {
            this.ZoomLevel = zoomLevel;
            this.pnlBackground.Appearance.BackColor = Infragistics.Win.Office2013ColorTable.Colors.DockAreaGradientDark;
            this.numZoomLevel.Value = zoomLevel;

            ValueListItem selectedItem = this.optPresetLevels.ValueList.FindByDataValue(zoomLevel);
            if (selectedItem == null)
                selectedItem = this.optPresetLevels.ValueList.FindByDataValue(DBNull.Value);

            this.optPresetLevels.Value = selectedItem.DataValue;

            this.LocalizeStrings();
        }

        #endregion //InitializeUI

        #region LocalizeStrings

        private void LocalizeStrings()
        {
            this.btnOK.Text = ResourceStrings.Text_Ok;
            this.btnCancel.Text = ResourceStrings.Text_Cancel;
            this.ultraFormManager1.FormStyleSettings.Caption = ResourceStrings.Text_Zoom;
            this.lblMagnification.Text = ResourceStrings.Text_Magnification;

            ValueListItem customItem = this.optPresetLevels.ValueList.FindByDataValue(DBNull.Value);
            customItem.DisplayText = ResourceStrings.Lbl_Custom;
        }

        #endregion //LocalizeStrings

        #region numZoomLevel_ValidationError

        private void numZoomLevel_ValidationError(object sender, Infragistics.Win.UltraWinEditors.ValidationErrorEventArgs e)
        {
            e.Beep = false;
            e.RetainFocus = false;

            int result;
            int.TryParse(e.InvalidText, out result);
            this.ZoomLevel = result;
        }

        #endregion //numZoomLevel_ValidationError

        #region numZoomLevel_ValueChanged

        private void numZoomLevel_ValueChanged(object sender, EventArgs e)
        {
            if (this.suppressEvent)
                return;

            this.ZoomLevel = (int)this.numZoomLevel.Value;
        }


        #endregion //numZoomLevel_ValueChanged

        #region optPresetLevels_ValueChanged

        private void optPresetLevels_ValueChanged(object sender, EventArgs e)
        {
            if (this.suppressEvent)
                return;

            if (this.optPresetLevels.Value is int)
                this.ZoomLevel = (int)this.optPresetLevels.Value;
        }

        #endregion //optPresetLevels_ValueChanged

        #endregion //Methods

    }
}
