using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using System.Windows;
using Infragistics.Win.UltraWinSpreadsheet;
using System.Drawing;
using Infragistics.Win;

namespace IGExcel.Controls
{
    public class FormatPreviewControl : UltraSpreadsheet, IUIElementCursorFilter
    {
        #region Constructor

        public FormatPreviewControl()
        {
            //Hide the UI elements 
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormulaBar.Visible = false;
            this.AreGridlinesVisible = false;
            this.AreHeadersVisible = false;
            Workbook.WindowOptions.TabBarVisible = false;
            Workbook.WindowOptions.ScrollBars = ScrollBars.None;
            Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
            this.SpreadsheetLook.CellSelectionDragBorderColor = Color.Transparent;
            this.KeyActionMappings.Clear();
            this.TabStop = false;

            this.EditModeEntering += (p1, p2) =>
            {
                p2.Cancel = true;
            };

            this.SizeChanged += (s, a) =>
            {
                Workbook.Worksheets[0].Rows[0].Height = (int)Infragistics.Win.DrawUtility.PixelsToPoints(this.Height, false) * 20;
                Workbook.Worksheets[0].SetDefaultColumnWidth(this.Width, WorksheetColumnWidthUnit.Pixel);
            };

        }

        #endregion //Constructor

        #region Properties

        #region PreviewFormatMask

        internal string PreviewFormatMask
        {
            get
            {
                return this.Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.FormatString;
            }
            set
            {
                this.Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.FormatString = value;
            }

        }

        #endregion // PreviewFormatMask

        #region PreviewValue

        internal object PreviewValue
        {
            get
            {
                return this.Workbook.Worksheets[0].Rows[0].Cells[0].Value;
            }
            set
            {
                this.Workbook.Worksheets[0].Rows[0].Cells[0].Value = value;
            }

        }

        #endregion // PreviewValue

        #endregion // Properties

        #region IUIElementCursorFilter

        public void BeforeSetCursor(UIElement element, bool adjustableCursor, ref System.Windows.Forms.Cursor cursor)
        {
            cursor = System.Windows.Forms.Cursors.Arrow;
        }
        #endregion //IUIElementCursorFilter
    }
}
