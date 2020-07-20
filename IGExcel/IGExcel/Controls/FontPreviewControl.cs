using System.Drawing;
using System.Linq;
using System.Windows;
using Infragistics.Documents.Excel;
using Infragistics.Win;
using Infragistics.Win.UltraWinSpreadsheet;

namespace IGExcel.Controls
{
    public class FontPreviewControl : UltraSpreadsheet, IUIElementCursorFilter
    {
        #region Members

        private FontStylesCustom previewFontStyle = FontStylesCustom.Regular;
        private string previewText;
        private string previewFontFamily;
        private int previewFontSize;
        private Color previewFontColor;
        private bool previewStrikethrough;
        private bool previewSubScript;
        private bool previewSuperscript;
        private FontUnderlineStyle previewUnderline;

        #endregion //Members

        #region Constructor

        public FontPreviewControl()
        {
            //Hide the UI elements 
            this.PreviewText = "AaBbCcYyZz";
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormulaBar.Visible = false;
            this.AreGridlinesVisible = false;
            this.AreHeadersVisible = false;
            this.CursorFilter = this;
            Workbook.WindowOptions.TabBarVisible = false;
            Workbook.WindowOptions.ScrollBars = ScrollBars.None;
            var format = Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat;
            format.VerticalAlignment = VerticalCellAlignment.Distributed;
            format.Alignment = HorizontalCellAlignment.Distributed;
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

        #region PreviewFontStyle

        internal const string PreviewFontStyleCustomPropertyName = "PreviewFontStyle";

        public FontStylesCustom PreviewFontStyle
        {
            get { return this.previewFontStyle; }
            set 
            {
                if (this.previewFontStyle != value)
                {
                    this.previewFontStyle = value;
                    this.OnPropertyChanged(PreviewFontStyleCustomPropertyName);
                }
            }
        }

        #endregion // PreviewFontStyle

        #region PreviewText

        internal const string PreviewTextPropertyName = "PreviewText";

        public string PreviewText
        {
            get { return this.previewText; }
            set
            {
                if (this.previewText != value)
                {
                    this.previewText = value;
                    this.OnPropertyChanged(PreviewTextPropertyName);
                }
            }
        }

        #endregion // FontStyle

        #region PreviewFontFamily

        internal const string PreviewFontFamilyPropertyName = "PreviewFontFamily";

        public string PreviewFontFamily
        {
            get { return this.previewFontFamily; }
            set
            {
                if (this.previewFontFamily != value)
                {
                    this.previewFontFamily = value;
                    this.OnPropertyChanged(PreviewFontFamilyPropertyName);
                }
            }
        }

        #endregion // PreviewFontFamily

        #region PreviewFontSize

        internal const string PreviewFontSizePropertyName = "PreviewFontSize";

        public int PreviewFontSize
        {
            get { return this.previewFontSize; }
            set
            {
                if (this.previewFontSize != value)
                {
                    this.previewFontSize = value;
                    this.OnPropertyChanged(PreviewFontSizePropertyName);
                }
            }
        }

        #endregion // PreviewFontSize

        #region PreviewFontColor

        internal const string PreviewFontColorPropertyName = "PreviewFontColor";

        public Color PreviewFontColor
        {
            get { return this.previewFontColor; }
            set
            {
                if (this.previewFontColor != value)
                {
                    this.previewFontColor = value;
                    this.OnPropertyChanged(PreviewFontColorPropertyName);
                }
            }
        }

        #endregion // PreviewFontColor

        #region PreviewStrikethrough

        internal const string PreviewStrikethroughPropertyName = "PreviewStrikethrough";

        public bool PreviewStrikethrough
        {
            get { return this.previewStrikethrough; }
            set
            {
                if (this.previewStrikethrough != value)
                {
                    this.previewStrikethrough = value;
                    this.OnPropertyChanged(PreviewStrikethroughPropertyName);
                }
            }
        }

        #endregion // PreviewFontColor

        #region PreviewSubscript

        internal const string PreviewSubscriptPropertyName = "PreviewSubscript";

        public bool PreviewSubscript
        {
            get { return this.previewSubScript; }
            set 
            {
                if (this.previewSubScript != value)
                {
                    this.previewSubScript = value;
                    this.OnPropertyChanged(PreviewSubscriptPropertyName);
                }
            }
        }

        #endregion // PreviewSubscript

        #region PreviewSuperscript

        internal const string PreviewSuperscriptPropertyName = "PreviewSuperscript";

        public bool PreviewSuperscript
        {
            get { return this.previewSuperscript; }
            set
            {
                if (this.previewSuperscript != value)
                {
                    this.previewSuperscript = value;
                    this.OnPropertyChanged(PreviewSuperscriptPropertyName);
                }
            }
        }

        #endregion // PreviewSuperscript

        #region PreviewUnderline

        internal const string PreviewUnderlinePropertyName = "PreviewUnderline";

        public FontUnderlineStyle PreviewUnderline
        {
            get { return this.previewUnderline; }
            set
            {
                if (this.previewUnderline != value)
                {
                    this.previewUnderline = value;
                    this.OnPropertyChanged(PreviewUnderlinePropertyName);
                }
            }
        }

        #endregion // PreviewUnderline

        #endregion //Properties

        #region OnPropertyChanged

        private void OnPropertyChanged(string propertyName)
        {
            var font = Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.Font;

            switch (propertyName)
            {
                case PreviewFontStyleCustomPropertyName:
                    switch (this.previewFontStyle)
                    {
                        case FontStylesCustom.Bold:
                            font.Bold = ExcelDefaultableBoolean.True;
                            font.Italic = ExcelDefaultableBoolean.False;
                            break;
                        case FontStylesCustom.Regular:
                            font.Bold = ExcelDefaultableBoolean.Default;
                            font.Italic = ExcelDefaultableBoolean.Default;
                            break;
                        case FontStylesCustom.Italic:
                            font.Bold = ExcelDefaultableBoolean.False;
                            font.Italic = ExcelDefaultableBoolean.True;
                            break;
                        case FontStylesCustom.BoldItalic:
                            font.Bold = ExcelDefaultableBoolean.True;
                            font.Italic = ExcelDefaultableBoolean.True;
                            break;
                    }
                    break;

                case PreviewTextPropertyName:
                    Workbook.Worksheets[0].Rows[0].Cells[0].Value = this.previewText;
                    break;

                case PreviewFontFamilyPropertyName:
                    font.Name = this.previewFontFamily;
                    break;

                case PreviewFontSizePropertyName:
                    font.Height = this.previewFontSize;
                    break;

                case PreviewFontColorPropertyName:
                    if (this.previewFontColor.IsEmpty)
                        font.ColorInfo = null;
                    else
                        font.ColorInfo = new WorkbookColorInfo(this.previewFontColor);
                    break;

                case PreviewStrikethroughPropertyName:
                    font.Strikeout = (this.previewStrikethrough) ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
                    break;

                case PreviewSuperscriptPropertyName:
                case PreviewSubscriptPropertyName:
                    if (this.previewSubScript)
                    {
                        font.SuperscriptSubscriptStyle = FontSuperscriptSubscriptStyle.Subscript;
                    }
                    else if (this.previewSuperscript)
                    {
                        font.SuperscriptSubscriptStyle = FontSuperscriptSubscriptStyle.Superscript;
                    }
                    else
                        font.SuperscriptSubscriptStyle = FontSuperscriptSubscriptStyle.Default;
                    break;

                case PreviewUnderlinePropertyName:
                    font.UnderlineStyle = this.previewUnderline;
                    break;

            }
        }

        #endregion //OnPropertyChanged

        #region IUIElementCursorFilter

        public void BeforeSetCursor(UIElement element, bool adjustableCursor, ref System.Windows.Forms.Cursor cursor)
        {
            cursor = System.Windows.Forms.Cursors.Arrow;
        }
        #endregion //IUIElementCursorFilter
    }
}
