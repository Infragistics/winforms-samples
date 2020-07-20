using Infragistics.Documents.Excel;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Drawing;
using Infragistics.Shared;
using Infragistics.Win.UltraWinSpreadsheet;
using System;

namespace IGExcel
{
    public class SpreadsheetSelectionAdapter : 
        DisposableObject,
        INotifyPropertyChanged
    {

        #region Members

        internal const string ActiveSelectionCellRangeFormatPropertyName = "ActiveSelectionCellRangeFormat";

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetSelectionAdapter"/> class.
        /// </summary>
        public SpreadsheetSelectionAdapter()
        {
            this.borderColor = new WorkbookColorInfo(Color.Black);
            this.cellBorderLineStyle = CellBorderLineStyle.Thin;
            this.sheetCellRangeBorders = SpreadsheetCellRangeBorders.BottomBorder;
            this.lastAppliedBorderSettings = ExcelBorders.BottomBorder;
            this.FillColor = Color.Yellow;
            this.ForegroundColor = Color.Red;
        }

        #endregion //Constructor

        #region Properties

        #region SpreadSheet

        private UltraSpreadsheet spreadSheet;
        public UltraSpreadsheet SpreadSheet
        {
            get { return spreadSheet; }
            set
            {
                if (spreadSheet != value)
                {
                    if (spreadSheet != null)
                        this.HookSpreadSheet(spreadSheet, false);

                    spreadSheet = value;

                    if (spreadSheet != null)
                    {
                        this.HookSpreadSheet(spreadSheet, true);
                        this.UpdateSelectionInfo();
                    }

                    NotifyPropertyChanged(SpreadSheetPropertyName);
                }
            }
        }
        internal const string SpreadSheetPropertyName = "SpreadSheet";

        #endregion //SpreadSheet

        #region BorderColor

        private WorkbookColorInfo borderColor;
        public WorkbookColorInfo BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    NotifyPropertyChanged(BorderColorPropertyName);
                }
            }
        }
        internal const string BorderColorPropertyName = "BorderColor";

        #endregion //BorderColor

        #region CanExecuteFormatAsTable

        private bool canExecuteFormatAsTable;
        public bool CanExecuteFormatAsTable
        {
            get { return canExecuteFormatAsTable; }
            set
            {
                if (canExecuteFormatAsTable != value)
                {
                    canExecuteFormatAsTable = value;
                    NotifyPropertyChanged(CanExecuteFormatAsTablePropertyName);
                }
            }
        }
        internal const string CanExecuteFormatAsTablePropertyName = "CanExecuteFormatAsTable";

        #endregion //CanExecuteFormatAsTable

        #region CellBorderLineStyle

        private CellBorderLineStyle cellBorderLineStyle;
        public CellBorderLineStyle CellBorderLineStyle
        {
            get { return cellBorderLineStyle; }
            set
            {
                if (cellBorderLineStyle != value)
                {
                    cellBorderLineStyle = value;
                    NotifyPropertyChanged(CellBorderLineStylePropertyName);
                }
            }
        }
        internal const string CellBorderLineStylePropertyName = "CellBorderLineStyle";

        #endregion //CellBorderLineStyle

        #region FillColor

        public Color FillColor
        {
            get
            {
                if (this.spreadSheet == null)
                    return Color.Empty;

                CellFillPattern fill = this.spreadSheet.ActiveSelectionCellRangeFormat.Fill as CellFillPattern;
                if (fill == null ||
                    fill == CellFill.NoColor ||
                    fill.BackgroundColorInfo == null ||
                    fill.BackgroundColorInfo.Color.HasValue == false)
                    return Color.Empty;

                return fill.BackgroundColorInfo.Color.Value;
            }
            set
            {
                if (this.FillColor != value)
                {
                    if (this.spreadSheet != null)
                    {
                        if (value.IsEmpty)
                            this.spreadSheet.ActiveSelectionCellRangeFormat.Fill = CellFill.NoColor;
                        else
                            this.spreadSheet.ActiveSelectionCellRangeFormat.Fill = CellFill.CreateSolidFill(value);
                    }

                    NotifyPropertyChanged(FillColorPropertyName);
                }
            }
        }
        internal const string FillColorPropertyName = "FillColor";

        #endregion //FillColor

        #region ForegroundColor

        public Color ForegroundColor
        {
            get
            {
                if (this.spreadSheet == null)
                    return Color.Empty;

                WorkbookColorInfo fontColor = this.spreadSheet.ActiveSelectionCellRangeFormat.Font.ColorInfo as WorkbookColorInfo;
                if (fontColor == null ||
                    fontColor == WorkbookColorInfo.Automatic ||
                    fontColor.Color.HasValue == false)
                    return Color.Empty;

                return fontColor.Color.Value;
            }
            set
            {
                if (this.ForegroundColor != value)
                {
                    if (this.spreadSheet != null)
                    {
                        if (value.IsEmpty)
                            this.spreadSheet.ActiveSelectionCellRangeFormat.Font.ColorInfo = WorkbookColorInfo.Automatic;
                        else
                            this.spreadSheet.ActiveSelectionCellRangeFormat.Font.ColorInfo = new WorkbookColorInfo(value);
                    }

                    NotifyPropertyChanged(ForegroundColorPropertyName);
                }
            }
        }
        internal const string ForegroundColorPropertyName = "ForegroundColor";

        #endregion // ForegroundColor

        #region Format

        internal SpreadsheetCellRangeFormat Format
        {
            get
            {
                if (this.spreadSheet == null)
                    return null;

                return this.spreadSheet.ActiveSelectionCellRangeFormat;
            }
        }

        #endregion

        #region IsInEditMode

        private bool isInEditMode;
        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                if (isInEditMode != value)
                {
                    isInEditMode = value;
                    NotifyPropertyChanged(IsInEditModePropertyName);
                }
            }
        }
        internal const string IsInEditModePropertyName = "IsInEditMode";

        #endregion //CanExecuteFormatAsTable

        #region LastAppliedBorderSettings

        private ExcelBorders lastAppliedBorderSettings;
        public ExcelBorders LastAppliedBorderSettings
        {
            get { return lastAppliedBorderSettings; }
            set
            {
                if (lastAppliedBorderSettings != value)
                {
                    lastAppliedBorderSettings = value;
                    NotifyPropertyChanged(LastAppliedBorderSettingsPropertyName);
                }
            }
        }
        internal const string LastAppliedBorderSettingsPropertyName = "LastAppliedBorderSettings";

        #endregion //LastAppliedBorderSettings

        #region SheetCellRangeBorders

        private SpreadsheetCellRangeBorders sheetCellRangeBorders;
        public SpreadsheetCellRangeBorders SheetCellRangeBorders
        {
            get { return sheetCellRangeBorders; }
            set
            {
                if (sheetCellRangeBorders != value)
                {
                    sheetCellRangeBorders = value;
                    NotifyPropertyChanged(SheetCellRangeBordersPropertyName);
                }
            }
        }
        internal const string SheetCellRangeBordersPropertyName = "SheetCellRangeBorders";

        #endregion //SheetCellRangeBorders

        #region SelectedTable

        private WorksheetTable selectedTable;
        public WorksheetTable SelectedTable
        {
            get { return selectedTable; }
            set
            {
                if (selectedTable != value)
                {
                    selectedTable = value;
                    NotifyPropertyChanged(SelectedTablePropertyName);
                }
            }
        }
        internal const string SelectedTablePropertyName = "SelectedTable";

        #endregion //SelectedTable

        #region Selection

        private SpreadsheetSelection selection;
        public SpreadsheetSelection Selection
        {
            get { return selection; }
            set
            {
                selection = value;
                NotifyPropertyChanged(SelectionPropertyName);
            }
        }
        internal const string SelectionPropertyName = "Selection";

        #endregion //Selection

        #region TableStyle

        private string tableStyle;
        public string TableStyle
        {
            get { return tableStyle; }
            set
            {
                if (tableStyle != value)
                {
                    tableStyle = value;
                    NotifyPropertyChanged(TableStylePropertyName);
                }
            }
        }
        internal const string TableStylePropertyName = "TableStyle";

        #endregion //TableStyle

        #endregion //Properties

        #region Methods

        #region HookSpreadSheet

        /// <summary>
        /// Subscribe/Unsubscript to the UltraSpreadsheet events
        /// </summary>
        /// <param name="spreadsheet">The spreadsheet.</param>
        /// <param name="hook">if set to <c>true</c> [hook].</param>
        private void HookSpreadSheet(UltraSpreadsheet spreadsheet, bool hook)
        {
            if (spreadsheet != null)
            {
                if (hook)
                {
                    spreadsheet.SelectionChanged += OnSelectionChanged;
                    spreadsheet.VisibleChanged += OnVisibleChanged;
                    spreadsheet.WorkbookDirtied += OnWorkbookDirtied;
                    spreadsheet.EditModeEntered += OnEditModeEntered;
                    spreadsheet.EditModeExited += OnEditModeExited;
                    spreadsheet.PropertyChanged += OnPropertyChanged;
                }
                else
                {
                    spreadsheet.SelectionChanged -= OnSelectionChanged;
                    spreadsheet.WorkbookDirtied -= OnWorkbookDirtied;
                    spreadsheet.VisibleChanged -= OnVisibleChanged;
                    spreadsheet.EditModeEntered -= OnEditModeEntered;
                    spreadsheet.EditModeExited -= OnEditModeExited;
                    spreadsheet.PropertyChanged -= OnPropertyChanged;
                }
            }
        }

        #endregion //HookSpreadSheet

        #region OnEditModeExited

        private void OnEditModeExited(object sender, SpreadsheetEditModeExitedEventArgs e)
        {
            this.CanExecuteFormatAsTable = true;
            this.IsInEditMode = false;
        }

        #endregion //OnEditModeExited

        #region OnEditModeEntered

        private void OnEditModeEntered(object sender, SpreadsheetEditModeEnteredEventArgs e)
        {
            this.CanExecuteFormatAsTable = false;
            this.IsInEditMode = true;
        }

        #endregion //OnEditModeEntered

        #region OnPropertyChanged

        private void OnPropertyChanged(object sender, Infragistics.Win.PropertyChangedEventArgs e)
        {
            if (e.ChangeInfo.PropId.ToString() == ActiveSelectionCellRangeFormatPropertyName)
                this.NotifyPropertyChanged(ActiveSelectionCellRangeFormatPropertyName);
        }

        #endregion //OnPropertyChanged

        #region OnSelectionChanged

        void OnSelectionChanged(object sender, SpreadsheetSelectionChangedEventArgs e)
        {
            this.UpdateSelectionInfo();
        }

        #endregion //OnSelectionChanged

        #region OnVisibleChanged

        private void OnVisibleChanged(object sender, System.EventArgs e)
        {
            if (this.SpreadSheet.Visible && this.Selection == null)
                this.Selection = this.SpreadSheet.ActiveSelection;
        }

        #endregion //OnIsVisibleChanged

        #region OnWorkbookDirtied

        public event EventHandler WorkbookDirtied;
        private void OnWorkbookDirtied(object sender, SpreadsheetWorkbookDirtiedEventArgs e)
        {
            var handler = WorkbookDirtied;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion //OnWorkbookDirtied

        #region UpdateSelectionInfo

        /// <summary>
        /// Update the properties based on the current selection
        /// </summary>
        internal void UpdateSelectionInfo()
        {
            if (this.SpreadSheet == null || this.SpreadSheet.ActiveSelection == null)
                return;

            try
            {
                if (this.SpreadSheet.ActiveSelection.CellRanges.Count == 1 && this.SpreadSheet.ActiveWorksheet.Tables.Count > 0)
                {
                    foreach (var table in this.SpreadSheet.ActiveWorksheet.Tables)
                    {
                        TableAndSelectionLocation location = TableIntersectsWithSelectionRange(table.WholeTableRegion, this.SpreadSheet.ActiveSelection.CellRanges[0]);

                        if (location == TableAndSelectionLocation.SelectionIsInsideTable)
                        {
                            this.TableStyle = table.Style.Name;
                            this.CanExecuteFormatAsTable = true;
                            this.SelectedTable = table;
                            break;
                        }
                        else
                        {
                            this.TableStyle = string.Empty;
                            this.CanExecuteFormatAsTable = location == TableAndSelectionLocation.NoIntersection;
                            this.SelectedTable = null;

                            if (location == TableAndSelectionLocation.PartialIntersection)
                                break;
                        }
                    }
                }
                else
                {

                    this.TableStyle = string.Empty;
                    this.CanExecuteFormatAsTable = this.SpreadSheet.ActiveSelection.CellRanges.Count == 1;
                    this.SelectedTable = null;
                }

            }
            catch { }

            this.Selection = SpreadSheet.ActiveSelection;
        }

        #endregion //UpdateSelectionInfo

        #region Private Methods

        private enum TableAndSelectionLocation { NoIntersection, SelectionIsInsideTable, PartialIntersection }

        private TableAndSelectionLocation TableIntersectsWithSelectionRange(WorksheetRegion tableRegion, SpreadsheetCellRange selectionRegion)
        {
            Rectangle tableRectangle = GetRectangleFromCoordinates(tableRegion.FirstRow, tableRegion.FirstColumn, tableRegion.LastRow, tableRegion.LastColumn);
            Rectangle selectionRectangle = GetRectangleFromCoordinates(selectionRegion.FirstRow, selectionRegion.FirstColumn, selectionRegion.LastRow, selectionRegion.LastColumn);

            if (selectionRectangle.Width == 0 && selectionRectangle.Height == 0)
            {
                if (tableRectangle.Contains(selectionRectangle.X, selectionRectangle.Y))
                {
                    return TableAndSelectionLocation.SelectionIsInsideTable;
                }
                else
                {
                    return TableAndSelectionLocation.NoIntersection;
                }
            }

            if (!tableRectangle.IntersectsWith(selectionRectangle))
            {
                return TableAndSelectionLocation.NoIntersection;
            }
            else if (tableRectangle.Contains(selectionRectangle))
            {
                return TableAndSelectionLocation.SelectionIsInsideTable;
            }
            else
            {
                return TableAndSelectionLocation.PartialIntersection;
            }
        }
        
        private static Rectangle GetRectangleFromCoordinates(int xFirst, int yFirst, int xLast, int yLast)
        {
            int x = Math.Min(xFirst, xLast);
            int y = Math.Min(yFirst, yLast);
            int width = Math.Max(Math.Max(xFirst, xLast) - x, 0);
            int height= Math.Max(Math.Max(yFirst, yLast) - y, 0);
            Rectangle rectX = new Rectangle(x, y, width, height);

            return rectX;
        }

        // This seems to not be used.
        #region Commented out
        //private bool IntersectsWith(int rowXFirst, int columnXFirst, int rowXLast, int columnXLast, int rowYFirst, int columnYFirst, int rowYLast, int columnYLast)
        //{
        //    Rectangle rectX = GetRectangleFromCoordinates(rowXFirst, columnXFirst, rowXLast, columnXLast);
        //    Rectangle rectY = GetRectangleFromCoordinates(rowYFirst, columnYFirst, rowYLast, columnYLast);

        //    return rectX.IntersectsWith(rectY);
        //}
        #endregion // Commented out

        #endregion //Private Methods

        #region OnDispose

        protected override void OnDispose()
        {
            this.selectedTable = null;
            this.selection = null;
            this.SpreadSheet = null;
        }

        #endregion

        #endregion //Methods

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion //INotifyPropertyChanged

    }
}
