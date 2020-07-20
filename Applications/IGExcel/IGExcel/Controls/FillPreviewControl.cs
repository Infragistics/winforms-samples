using System.Drawing;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Infragistics.Win;
using Infragistics.Win.UltraWinSpreadsheet;

namespace IGExcel.Controls
{
    public class FillPreviewControl : UltraSpreadsheet, IUIElementCursorFilter
    {
        #region Constructor

        public FillPreviewControl()
        {
            //Hide the UI elements 
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormulaBar.Visible = false;
            this.AreGridlinesVisible = false;
            this.AreHeadersVisible = false;
            Workbook.WindowOptions.TabBarVisible = false;
            Workbook.WindowOptions.ScrollBars = ScrollBars.None;
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

        #region PreviewFill

        internal CellFill PreviewFill
        {
            get
            {
                return this.Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.Fill;
            }
            set
            {
                this.Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.Fill = value;
            }
        }

        #endregion // PreviewFill

        #region IUIElementCursorFilter

        public void BeforeSetCursor(UIElement element, bool adjustableCursor, ref System.Windows.Forms.Cursor cursor)
        {
            cursor = System.Windows.Forms.Cursors.Arrow;
        }
        #endregion //IUIElementCursorFilter
    }
}
