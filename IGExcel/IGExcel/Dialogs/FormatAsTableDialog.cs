using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Documents.Excel;
using Infragistics.Win.UltraWinSpreadsheet;

namespace IGExcel.Dialogs
{
    internal partial class FormatAsTableDialog : Form
    {
        #region Members

        private SpreadsheetSelection selection;        
        private Worksheet activeWorksheet;
        private Workbook workbook;
        private string tableStyle;

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatAsTableDialog"/> class.
        /// </summary>
        /// <param name="workbook">The workbook.</param>
        /// <param name="activeWorksheet">The active worksheet.</param>
        /// <param name="selection">The selection.</param>
        /// <param name="tableStyle">The table style.</param>
        internal FormatAsTableDialog(Workbook workbook, Worksheet activeWorksheet, SpreadsheetSelection selection, string tableStyle)
        {
            this.InitializeComponent();

            this.workbook = workbook;
            this.activeWorksheet = activeWorksheet;
            this.selection = selection;
            this.tableStyle = tableStyle;

            this.InitializeUI();
        }

        #endregion //Constructor

        #region Properties

        #region Range

        private string Range
        {
            get
            {
                return this.lblRange.Text;
            }
            set
            {
                this.lblRange.Text = value;
            }
        }

        #endregion //Range

        #endregion //Properties

        #region Methods

        #region InitializeUI

        private void InitializeUI()
        {
            this.pnlBackground.Appearance.BackColor = Infragistics.Win.Office2013ColorTable.Colors.DockAreaGradientDark;
            this.Range = selection.CellRanges[0].ToString(this.workbook.CellReferenceMode);

            this.LocalizeStrings();
        }

        #endregion //InitializeUI

        #region LocalizeStrings

        private void LocalizeStrings()
        {
            this.ultraFormManager1.FormStyleSettings.Caption = ResourceStrings.Text_FormatAsTable;
            this.btnOK.Text = ResourceStrings.Text_Ok;
            this.btnCancel.Text = ResourceStrings.Text_Cancel;

            this.lblTable.Text = ResourceStrings.Lbl_WhereIsTheDataForYourTable;
            this.cbTableHeaders.Text = ResourceStrings.Lbl_MyTableHasHeaders;
        }

        #endregion //LocalizeStrings

        #endregion //Methods

        #region Event Handlers

        #region DimensionDialog_FormClosing

        private void DimensionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.activeWorksheet.Tables.Add(this.Range, this.cbTableHeaders.Checked, this.workbook.StandardTableStyles[this.tableStyle]);
            }
        }

        #endregion //DimensionDialog_FormClosing

        #endregion //Event Handlers

    }
}
