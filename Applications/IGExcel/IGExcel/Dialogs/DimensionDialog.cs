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
    internal partial class DimensionDialog : Form
    {
        #region Members

        private DimensionMode dimension;
        private SpreadsheetSelection selection;
        private Worksheet activeWorksheet;

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DimensionDialog"/> class.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        /// <param name="activeWorksheet">The active worksheet.</param>
        /// <param name="selection">The selection.</param>
        internal DimensionDialog(DimensionMode dimension, Worksheet activeWorksheet, SpreadsheetSelection selection)
        {
            this.InitializeComponent();

            this.dimension = dimension;
            this.activeWorksheet = activeWorksheet;
            this.selection = selection;
            this.InitializeUI();
        }

        #endregion //Constructor

        #region Properties

        #region DimensionValue

        internal int DimensionValue
        {
            get
            {
                return (int)this.numExtent.Value;
            }
            set
            {
                this.numExtent.Value = value;
            }
        }

        #endregion //DimensionValue

        #endregion //Properties

        #region Methods

        #region InitializeUI

        private void InitializeUI()
        {
            this.pnlBackground.Appearance.BackColor = Infragistics.Win.Office2013ColorTable.Colors.DockAreaGradientDark;
            this.Icon = (this.dimension == DimensionMode.Height) ?  Properties.Resources.RowHeight: Properties.Resources.ColumnWidth;
            this.ReadDimension();
            this.LocalizeStrings();
        }

        #endregion //InitializeUI

        #region LocalizeStrings

        private void LocalizeStrings()
        {
            this.btnOK.Text = ResourceStrings.Text_Ok;
            this.btnCancel.Text = ResourceStrings.Text_Cancel;

            switch(this.dimension)
            {
                case DimensionMode.Height:

                    this.ultraFormManager1.FormStyleSettings.Caption = ResourceStrings.Text_RowHeightTitle;
                    this.lblDimension.Text = ResourceStrings.Lbl_RowHeight;
                    break;
                case DimensionMode.Width:
                    this.ultraFormManager1.FormStyleSettings.Caption = ResourceStrings.Text_ColumnWidthTitle;
                    this.lblDimension.Text = ResourceStrings.Lbl_ColumnWidth;
                    break;
                case DimensionMode.Standard:
                    this.ultraFormManager1.FormStyleSettings.Caption = ResourceStrings.Text_StandardWidth;
                    this.lblDimension.Text = ResourceStrings.Lbl_StandardColumnWidth;
                    break;
                default:
                    Debug.Fail("Unknown dimension");
                    break;
            }
        }

        #endregion //LocalizeStrings

        #region ReadDimension

        private void ReadDimension()
        {
            switch (this.dimension)
            {
                case DimensionMode.Height:
                    var rowHeight = this.activeWorksheet.Rows[this.selection.CellRanges[0].FirstRow].GetCellBoundsInTwips(this.selection.CellRanges[0].FirstColumn).Height;

                    foreach (var range in this.selection.CellRanges)
                    {
                        for (int i = range.FirstRow; i <= range.LastRow; i++)
                        {
                            var row = this.activeWorksheet.Rows[i];
                            if (rowHeight != row.GetCellBoundsInTwips(range.FirstColumn).Height)
                            {
                                rowHeight = -1;
                                break;
                            }
                        }

                        if (rowHeight == -1)
                            break;
                    }

                    if (rowHeight != -1)
                    {
                        this.DimensionValue = rowHeight / 20;
                    }
                    break;
                case DimensionMode.Width: var columnWidth = this.activeWorksheet.Columns[this.selection.CellRanges[0].FirstColumn].Width;

                    foreach (var range in this.selection.CellRanges)
                    {

                        for (int i = range.FirstColumn; i <= range.LastColumn; i++)
                        {
                            if (columnWidth != this.activeWorksheet.Columns[i].Width)
                            {
                                columnWidth = -2;
                            }
                        }

                        if (columnWidth == -2)
                            break;
                    }

                    if (columnWidth == -1)
                    {
                        this.DimensionValue = (int)this.activeWorksheet.GetDefaultColumnWidth(WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                    }
                    else if (columnWidth != -2)
                    {
                        this.DimensionValue = (int)this.activeWorksheet.Columns[this.selection.CellRanges[0].FirstColumn].GetWidth(WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                    }

                    break;
                case DimensionMode.Standard:
                    this.DimensionValue = (int)this.activeWorksheet.GetDefaultColumnWidth(WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                    break;
            }
        }

        #endregion //ReadDimension

        #endregion //Methods

        #region Event Handlers

        #region DimensionDialog_FormClosing

        private void DimensionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                switch (this.dimension)
                {
                    case DimensionMode.Height:
                        foreach (var range in this.selection.CellRanges)
                        {
                            for (int i = range.FirstRow; i <= range.LastRow; i++)
                            {
                                this.activeWorksheet.Rows[i].Height = this.DimensionValue * 20;
                            }
                        }
                        break;
                    case DimensionMode.Width:
                        foreach (var range in this.selection.CellRanges)
                        {
                            for (int i = range.FirstColumn; i <= range.LastColumn; i++)
                            {
                                this.activeWorksheet.Columns[i].SetWidth(this.DimensionValue, WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                            }
                        }
                        break;
                    case DimensionMode.Standard:
                        this.activeWorksheet.SetDefaultColumnWidth(this.DimensionValue, WorksheetColumnWidthUnit.CharacterPaddingExcluded);
                        break;
                }
            }
        }

        #endregion //DimensionDialog_FormClosing

        #region numExtent_ValueChanged

        private void numExtent_ValueChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = !(this.numExtent.Value == null ||
                this.numExtent.Value == DBNull.Value);
        }

        #endregion //numExtent_ValueChanged

        #endregion //Event Handlers

        internal enum DimensionMode
        {
            Height,
            Width,
            Standard,
        }

    }
}
