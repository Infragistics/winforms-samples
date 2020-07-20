using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Documents.Excel;
using Infragistics.Win;
using Infragistics.Win.UltraMessageBox;
using Infragistics.Win.UltraWinSpreadsheet;
using Infragistics.Win.UltraWinToolbars;

namespace IGExcel
{
    public partial class frmMain
        : Form
    {
        #region Fields

        #region Constants
        
        private const string FontFamily = "Calibri";

        #endregion //Constants

        #region Instance Variables

        private RecentFilesManager recentManager;
        private Workbook workbook;
        private string workbookPath;
        private bool workbookIsDirty = false;
        private SpreadsheetSelectionAdapter selectionAdapter;
        private bool suspendEventFiring;
        private bool toolEnabledStateDirty = false;

        #endregion //Instance Variables

        #endregion //Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain"/> class.
        /// </summary>
        public frmMain()
        {
            this.ShowSplashScreen();

            this.InitializeComponent();

            // Update the splash screen
            this.OnInitializationStatusChanged(ResourceStrings.Text_InitializingApplication);

            this.InitializeUI();
        }

        #endregion //Constructor

        #region Base Class Overrides

        #region Dispose

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
               
                if (this.selectionAdapter != null)
                {
                    this.selectionAdapter.PropertyChanged -= selectionAdapter_PropertyChanged;
                    this.selectionAdapter.WorkbookDirtied -= selectionAdapter_WorkbookDirtied;
                    this.selectionAdapter.Dispose();
                    this.selectionAdapter = null;
                }

                this.recentManager = null;
                this.Workbook = null;
            }
            base.Dispose(disposing);
        }

        #endregion //Dispose

        #region OnFormClosing

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // If the Workbook is not null after setting the property to null, then the current Workbook is dirty and the user pressed Cancel.
            // As such, we should not close the application.
            this.Workbook = null;
            e.Cancel = (this.Workbook != null);
        }

        #endregion //OnFormClosing

        #region OnShown

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Show the Backstage with 'New' menu visible.
            this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.DropDown((PopupToolBase)this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.NavigationMenu.Tools["New"]);
            this.OnInitializationComplete();         
        }

        #endregion //OnShown

        #endregion //Base Class Overrides

        #region Properties

        #region Workbook

        private Workbook Workbook
        {
            get
            {
                return this.workbook;
            }
            set
            {
                if (this.workbook != value)
                {
                    if (this.workbook != null)
                    {
                        this.ultraSpreadsheet1.PerformAction(UltraSpreadsheetAction.ExitEditModeAndUpdateSelectedCells);
                        if (this.ultraSpreadsheet1.IsInEditMode == true)
                            return;

                        while (this.workbookIsDirty)
                        {
                            // Prompt for save.
                            string workbookName = this.ShortWorkbookName;
                            var result = UltraMessageBoxManager.Show(
                                string.Format(ResourceStrings.Msg_WantToSaveChangesLong, workbookName), string.Format(ResourceStrings.Msg_WantToSaveChanges, workbookName), MessageBoxButtons.YesNoCancel);

                            switch (result)
                            {
                                case DialogResult.Yes:
                                    this.Save(false);
                                    break;
                                case DialogResult.No:
                                    this.WorkbookIsDirty = false;
                                    break;
                                case System.Windows.Forms.DialogResult.Cancel:
                                    return;
                            }
                        }
                        this.workbook.PropertyChanged -= workbook_PropertyChanged;
                        this.workbook.DocumentProperties.PropertyChanged -= workbook_PropertyChanged;
                    }
                    this.workbook = value;

                    bool hasWorkbook = (this.workbook != null);
                    this.ultraSpreadsheet1.Workbook = this.workbook;
                    this.ultraSpreadsheet1.Visible = hasWorkbook;

                    if (hasWorkbook)
                    {
                        this.workbook.PropertyChanged += workbook_PropertyChanged;
                        this.workbook.DocumentProperties.PropertyChanged += workbook_PropertyChanged;

                        this.ultraToolbarsManager1.Tools["DeleteWorksheets"].SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(UltraSpreadsheetAction.DeleteWorksheets);
                    }
                    else
                    {
                        this.WorkbookPath = null;
                    }

                    this.UpdateCaption();

                    // Make sure the UltraSpreadsheet has focus so the user can immediately begin typing.
                    this.ultraSpreadsheet1.Focus();
                }
            }
        }

        #endregion //Workbook

        #region WorkbookIsDirty

        private bool WorkbookIsDirty
        {
            get
            {
                return this.workbookIsDirty;
            }
            set
            {
                if (this.workbookIsDirty != value)
                {
                    this.workbookIsDirty = value;
                    this.UpdateCaption();
                }
            }
        }

        #endregion WorkbookIsDirty

        #region WorkbookPath

        private string WorkbookPath
        {
            get
            {
                return this.workbookPath;
            }
            set
            {
                if (this.workbookPath != value)
                {
                    this.workbookPath = value;
                    if (this.workbookPath != null)
                        this.recentManager.AddFile(value, true);
                this.UpdateCaption();
                }
            }
        }

        #endregion //WorkbookPath

        #region ShortWorkbookName

        private string ShortWorkbookName
        {
            get
            {
                if (string.IsNullOrEmpty(this.workbookPath))
                {
                    if (this.workbook != null)
                        return ResourceStrings.ResourceManager.GetString("Text_Book1");
                    return null;
                }

                return Path.GetFileNameWithoutExtension(this.workbookPath);
            }
        }

        #endregion //ShortWorkbookName

        #endregion //Properties

        #region Methods

        #region Browse

        /// <summary>
        /// Opens the OpenFileDialog/SaveFileDialog
        /// </summary>
        private void Browse(string initialPath, bool open)
        {
            var filters = "Excel Workbook (*.xlsx)|*.xlsx|Excel Macro-Enabled Workbook (*.xlsm)|*.xlsm|Excel 97-2003 Workbook (*.xls)|*.xls|Excel Template (*.xltx)|*.xltx|Excel Macro-Enabled Template (*.xltm)|*.xltm";


            FileDialog dialog = (open) ? (FileDialog)new OpenFileDialog() { Multiselect = false } : (FileDialog)new SaveFileDialog();
            dialog.Filter = filters;
            dialog.InitialDirectory = initialPath;
            dialog.AutoUpgradeEnabled = false;
            dialog.AddExtension = true;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                try
                {
                    if (open)
                        this.OpenFile(dialog.FileName);
                    else
                        this.SaveWorkbook(dialog.FileName);
                }
                catch (PathTooLongException ex)
                {
                    if (UltraMessageBoxManager.Show(
                        string.Format(ResourceStrings.Text_FilenameError_Content, ex.Message),
                        ResourceStrings.Text_FilenameError_Header,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)    
                        this.Browse(initialPath, open);
                }
            }       
        }

        #endregion //Browse

        #region InitializeBackstageControls

        private void InitializeBackstageControls()
        {
            PopupControlContainerTool infoTool = this.ultraToolbarsManager1.Tools["Info"] as PopupControlContainerTool;
            IGExcel.Controls.InfoControl infoControl = infoTool.Control as IGExcel.Controls.InfoControl;
            if (infoControl == null)
            {
                infoControl = new IGExcel.Controls.InfoControl();
                infoTool.Control = infoControl;
            }

            PopupControlContainerTool newTool = this.ultraToolbarsManager1.Tools["New"] as PopupControlContainerTool;
            IGExcel.Controls.NewControl newControl = newTool.Control as IGExcel.Controls.NewControl;
            if (newControl == null)
            {
                newControl = new IGExcel.Controls.NewControl();
                newTool.Control = newControl;
                newControl.NewWorkbookSelected += (sender, args) =>
                {
                    this.Workbook = args.Workbook;
                    this.WorkbookPath = null;

                    if (this.Workbook == args.Workbook)
                        this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.CloseUp();
                };
            }            
        }

        #endregion //InitializeBackstageControls

        #region InitializeGalleryTools

        private void InitializeGalleryTools()
        {
            PopupGalleryTool tableGallery = this.ultraToolbarsManager1.Tools["FormatAsTable"] as PopupGalleryTool;

            foreach (TableStyles groupKey in Enum.GetValues(typeof(TableStyles)))
            {

                GalleryToolItemGroup group = tableGallery.Groups.Add(groupKey.ToString());
                List<string> styles = StyleUtils.GetTableStyles(groupKey);
                foreach (string styleString in styles)
                {
                    GalleryToolItem item = new GalleryToolItem(styleString);
                    item.Settings.Appearance.Image = Utils.GetEmbeddedImage(string.Format("Images.Tables.{0}.png", styleString));
                    tableGallery.Items.Add(item);
                    group.Items.Add(styleString);
                }
            }

            PopupGalleryTool cellStylesGallery = this.ultraToolbarsManager1.Tools["CellStyles"] as PopupGalleryTool;

            foreach (CellStyles groupKey in Enum.GetValues(typeof(CellStyles)))
            {

                GalleryToolItemGroup group = cellStylesGallery.Groups.Add(groupKey.ToString());
                List<string> styles = StyleUtils.GetCellStyles(groupKey);
                foreach (string styleString in styles)
                {
                    GalleryToolItem item = new GalleryToolItem(styleString);
                    item.Settings.Appearance.Image = Utils.GetEmbeddedImage(string.Format("Images.Cells.{0}.png", styleString));
                    cellStylesGallery.Items.Add(item);
                    group.Items.Add(styleString);
                }
            }
        }

        #endregion //InitializeGalleryTools

        #region InitializeUI
        /// <summary>
        /// Entry point for user interface initialization.
        /// </summary>
        private void InitializeUI()
        {
            this.InitializeGalleryTools();
            this.InitializeBackstageControls();

            // Create a default workbook.
            var defaultWorkbook = new Workbook(WorkbookFormat.Excel2007);
            defaultWorkbook.Worksheets.Add("Sheet1");
            this.Workbook = defaultWorkbook;

            // Create the SelectionAdapter            
            this.selectionAdapter = new SpreadsheetSelectionAdapter();
            this.selectionAdapter.PropertyChanged += selectionAdapter_PropertyChanged;
            this.selectionAdapter.WorkbookDirtied += selectionAdapter_WorkbookDirtied;
            this.selectionAdapter.SpreadSheet = this.ultraSpreadsheet1;

            this.Font = new Font(frmMain.FontFamily, 9f);

            // Populate the recent files and folders
            this.recentManager = new RecentFilesManager();
            this.recentManager.SetRepositories(Properties.Settings.Default, p => Properties.Settings.Default.RecentFilesRepo, p => Properties.Settings.Default.RecentFoldersRepo);
            this.recentManager.PropertyChanged += (sender, args) =>
                {
                    this.PopulateRecentListTool(args.PropertyName);
                };
            this.PopulateRecentListTool("RecentFiles");
            this.PopulateRecentListTool("RecentFolders");
            
            this.UpdateCaption();
            ((ComboBoxTool)this.ultraToolbarsManager1.Tools["FontSize"]).ValueList = StyleUtils.GetFontSizeValueList();

            // Hook up to the UndoManager
            var undoManager = this.ultraSpreadsheet1.UndoManager;
            this.ultraToolbarsManager1.Tools["Undo"].SharedProps.Enabled = undoManager.CanUndo;
            this.ultraToolbarsManager1.Tools["Redo"].SharedProps.Enabled = undoManager.CanRedo;
            undoManager.PropertyChanged += undoManager_PropertyChanged;

            this.LocalizeStrings();

            ((PopupControlContainerTool)this.ultraToolbarsManager1.Tools["CurrentUser"]).Control = new Controls.UserAccountControl();
        }

        #endregion InitializeUI

        #region LocalizeStrings

        private void LocalizeStrings()
        {
            var rm = ResourceStrings.ResourceManager;

            this.ultraToolbarsManager1.Ribbon.FileMenuButtonCaption = rm.GetString("Text_FILE");

            // Localize each RibbonTab
            foreach (RibbonTab tab in this.ultraToolbarsManager1.Ribbon.Tabs)
            {
                var localizedString = rm.GetString(string.Format("Text_{0}", tab.Key));

                if (string.IsNullOrEmpty(localizedString) == false)
                    tab.Caption = localizedString;

                // Localize each RibbonGroup
                foreach (RibbonGroup group in tab.Groups)
                {
                    localizedString = rm.GetString(string.Format("Text_{0}", group.Key));
                    if (string.IsNullOrEmpty(localizedString) == false)
                        group.Caption = localizedString;
                }
            }

            // Localize each tool
            foreach (ToolBase tool in this.ultraToolbarsManager1.Tools)
            {
                SharedProps sharedProps = tool.SharedProps;

                var localizedString = rm.GetString(string.Format("Text_{0}", tool.Key));
                if (string.IsNullOrEmpty(localizedString) == false)
                    sharedProps.Caption = localizedString;

                localizedString = rm.GetString(string.Format("Text_{0}_Header", tool.Key));
                if (string.IsNullOrEmpty(localizedString) == false)
                    sharedProps.ToolTipTitle = localizedString;

                localizedString = rm.GetString(string.Format("Text_{0}_Content", tool.Key));
                if (string.IsNullOrEmpty(localizedString) == false)
                    sharedProps.ToolTipText = localizedString;

                localizedString = rm.GetString(string.Format("Text_{0}_Description", tool.Key));
                if (string.IsNullOrEmpty(localizedString) == false)
                    sharedProps.DescriptionOnMenu = localizedString;

                PopupGalleryTool galleryTool = tool as PopupGalleryTool;
                if (galleryTool != null)
                {
                    foreach (GalleryToolItemGroup group in galleryTool.Groups)
                    {
                        localizedString = rm.GetString(string.Format("Text_{0}", group.Key));
                        if (string.IsNullOrEmpty(localizedString) == false)
                            group.Text = localizedString;
                    }
                }

            }

            string name = rm.GetString("Ig_UserName_String");
            this.ultraToolbarsManager1.Tools["CurrentUser"].SharedProps.Caption = name;
            this.ultraToolbarsManager1.Tools["CurrentUser"].SharedProps.ToolTipText = string.Format(rm.GetString("Text_IsSignedIn"), name);

            foreach (var key in new string[] { "FontSettings", "AlignmentSettings" })
            {
                var image = Utils.GetEmbeddedImage(string.Format("Images.{0}_Preview.png", key));
                var tool = this.ultraToolbarsManager1.Tools[key];

                tool.SharedProps.ToolTipTitle = rm.GetString(string.Format("Text_{0}_Description_Header", key));
                var encodedImage = Infragistics.Win.FormattedLinkLabel.FormattedLinkEditor.EncodeImage(image);
                tool.SharedProps.ToolTipTextFormatted = string.Format("<p> <img data='{0}' align='left' VSpace='10' HSpace='10'/>{1}</p>", encodedImage, rm.GetString(string.Format("Text_{0}_Description_Content", key)));
            }
        }

        #endregion //LocalizeStrings

        #region OpenFile

        private void OpenFile(string path)
        {
            if (string.IsNullOrEmpty(path) ||
                File.Exists(path) == false)
                return;

            try
            {
                var loadedWorkbook = Workbook.Load(path);

                if (loadedWorkbook != null)
                {
                    this.Workbook = loadedWorkbook;
                    if (this.Workbook == loadedWorkbook)
                        this.WorkbookPath = path;
                }
            }
            catch (Exception ex)
            {
                UltraMessageBoxManager.Show(ex.Message);
            }
        }

        #endregion //OpenFile

        #region PopulateRecentListTool

        private void PopulateRecentListTool(string key)
        {
            ListTool listTool = (ListTool)this.ultraToolbarsManager1.Tools[key];
            listTool.ListToolItems.Clear();
            foreach (RecentFilesManager.RecentFileInfo fileInfo in this.recentManager.GetCollection(key))
            {
                ListToolItem item = new ListToolItem()
                {
                    Text = fileInfo.Name,
                    DescriptionOnMenu = Infragistics.Win.FormattedLinkLabel.ParsedFormattedTextValue.EscapeXML(fileInfo.FullName.Replace("\\", " » ")),
                    Tag = fileInfo.FullName,
                };
                listTool.ListToolItems.Add(item);
            }
        }

        #endregion //PopulateRecentListTool

        #region Save

        private void Save(bool useBackstage)
        {
            if (string.IsNullOrEmpty(this.workbookPath))
            {
                if (useBackstage)
                {
                    ApplicationMenu2010 backStage = this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010;
                    PopupToolBase saveAsTool = (PopupToolBase)backStage.NavigationMenu.Tools["SaveAs"];
                    if (backStage.IsOpen)
                        backStage.NavigationMenu.ActiveContentTool = saveAsTool;
                    else
                        backStage.DropDown(saveAsTool);
                }
                else
                {
                    this.Browse(null, false);
                }
                return;
            }
            if (useBackstage &&
                this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.IsOpen)
                this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.CloseUp();

            this.SaveWorkbook(this.workbookPath);
        }

        #endregion //Save

        #region SaveWorkbook

        private void SaveWorkbook(string path)
        {
            if (string.IsNullOrEmpty(path) || 
                this.workbook == null)
                return;

            try
            {
                var extention = Path.GetExtension(path);
                var selctedWorkbookFormat = Utils.GetWorkbookFormatFromFileExtension(extention);

                if (this.workbook.CurrentFormat != selctedWorkbookFormat)
                {
                    this.workbook.SetCurrentFormat(selctedWorkbookFormat);
                }

                this.workbook.Save(path);
                this.WorkbookIsDirty = false;
                this.WorkbookPath = path;
            }
            catch (Exception ex)
            {
                UltraMessageBoxManager.Show(ex.Message, ResourceStrings.Text_Exception, MessageBoxButtons.OK);
            }
        }

        #endregion //SaveWorkbook

        #region SetBorder

        private void SetBorder(string parameter = null)
        {
            SpreadsheetCellRangeBorders borders = this.selectionAdapter.SheetCellRangeBorders;
            var borderColor = this.selectionAdapter.BorderColor;
            CellBorderLineStyle style = this.selectionAdapter.CellBorderLineStyle;

            ExcelBorders borderCommnd = this.selectionAdapter.LastAppliedBorderSettings;

            if (parameter != null)
            {
                borderCommnd = (ExcelBorders)Enum.Parse(typeof(ExcelBorders), parameter.ToString());
            }

            this.selectionAdapter.LastAppliedBorderSettings = borderCommnd;

            switch (borderCommnd)
            {
                case ExcelBorders.BottomBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.TopBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.TopBorder;
                    break;
                case ExcelBorders.LeftBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.LeftBorder;
                    break;
                case ExcelBorders.RightBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.RightBorder;
                    break;
                case ExcelBorders.NoBorder:
                    borders = SpreadsheetCellRangeBorders.AllBorder;
                    style = CellBorderLineStyle.None;
                    break;
                case ExcelBorders.AllBorders:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.AllBorder;
                    break;
                case ExcelBorders.OutsideBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.OutsideBorder;
                    break;
                case ExcelBorders.ThickBoxBorder:
                    style = CellBorderLineStyle.Thick;
                    borders = SpreadsheetCellRangeBorders.OutsideBorder;
                    break;
                case ExcelBorders.BottomDoubleBorder:
                    style = CellBorderLineStyle.Double;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.ThickBottomBorder:
                    style = CellBorderLineStyle.Thick;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.TopAndBottomBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.TopBorder | SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.TopAndThickBottomBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.TopBorder;
                    this.selectionAdapter.SpreadSheet.ActiveSelectionCellRangeFormat.SetBorders(borders, borderColor, style);
                    style = CellBorderLineStyle.Thick;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                case ExcelBorders.TopAndDoubleBottomBorder:
                    style = CellBorderLineStyle.Thin;
                    borders = SpreadsheetCellRangeBorders.TopBorder;
                    this.selectionAdapter.SpreadSheet.ActiveSelectionCellRangeFormat.SetBorders(borders, borderColor, style);
                    style = CellBorderLineStyle.Double;
                    borders = SpreadsheetCellRangeBorders.BottomBorder;
                    break;
                default:
                    break;
            }

            this.selectionAdapter.SheetCellRangeBorders = borders;
            this.selectionAdapter.BorderColor = borderColor;
            this.selectionAdapter.CellBorderLineStyle = style;


            this.selectionAdapter.SpreadSheet.ActiveSelectionCellRangeFormat.SetBorders(borders, borderColor, style);
        }

        #endregion //SetBorder

        #region SetZoomLevel

        private void SetZoomLevel(int zoomLevel)
        {
            this.ultraSpreadsheet1.ZoomLevel = zoomLevel;
            this.tbZoom.Value = zoomLevel;
            this.tbZoom.Refresh();
            this.ultraStatusBar1.Panels["Zoom"].Text = string.Format("{0}%", zoomLevel);
            this.ultraStatusBar1.Refresh();
        }

        #endregion //SetZoomLevel

        #region ShowDimensionDialog

        private void ShowDimensionDialog(Dialogs.DimensionDialog.DimensionMode dimension)
        {
            using (Dialogs.DimensionDialog dialog = new Dialogs.DimensionDialog(dimension, this.ultraSpreadsheet1.ActiveWorksheet, this.ultraSpreadsheet1.ActiveSelection))
            {
                dialog.ShowDialog(this);
            }
        }

        #endregion //ShowDimensionDialog

        #region ShowFormatAsTableDialog

        private void ShowFormatAsTableDialog(string tableStyle)
        {
            using (Dialogs.FormatAsTableDialog dialog = new Dialogs.FormatAsTableDialog(this.workbook, this.ultraSpreadsheet1.ActiveWorksheet, this.ultraSpreadsheet1.ActiveSelection, tableStyle))
            {
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this.selectionAdapter.UpdateSelectionInfo();
                }
            }
        }

        #endregion //ShowFormatAsTableDialog

        #region ShowFormatCellsDialog

        private void ShowFormatCellsDialog(Dialogs.FormatCellsDialog.InitialSelectedTab initialTab)
        {
            using (Dialogs.FormatCellsDialog dialog = new Dialogs.FormatCellsDialog(initialTab, this.selectionAdapter))
            {
                dialog.ShowDialog(this);
            }
        }

        #endregion //ShowFormatCellsDialog

        #region ShowZoomDialog

        private void ShowZoomDialog()
        {
            using (Dialogs.ZoomDialog dialog = new Dialogs.ZoomDialog(this.ultraSpreadsheet1.ZoomLevel))
            {
                if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    this.SetZoomLevel(dialog.ZoomLevel);
            }
        }

        #endregion //ShowZoomDialog

        #region UpdateCaption

        private void UpdateCaption()
        {
            string shortName = this.ShortWorkbookName;
            this.Text =
                this.ultraToolbarsManager1.Ribbon.Caption = string.IsNullOrEmpty(shortName)
                    ? "IG Excel"
                    : string.Format(
                        "{0}{1} - IG Excel",
                        this.ShortWorkbookName,
                        (this.workbookIsDirty) ? "*" : "");
        }

        #endregion //UpdateCaption

        #region UpdateFontTools

        private void UpdateFontTools()
        {
            bool original = this.suspendEventFiring;
            try
            {
                this.suspendEventFiring = true;
                ((ComboBoxTool)this.ultraToolbarsManager1.Tools["FontSize"]).Value = this.selectionAdapter.Format.Font.Height;
                ((FontListTool)this.ultraToolbarsManager1.Tools["Font"]).Value = this.selectionAdapter.Format.Font.Name;
            }
            finally
            {
                this.suspendEventFiring = original;
            }
        }

        #endregion //UpdateFontTools

        #region UpdateStatus

        private void UpdateStatus()
        {
            this.ultraStatusBar1.Panels["Status"].Text = this.selectionAdapter.IsInEditMode ? ResourceStrings.Text_Enter : ResourceStrings.Text_Ready;
        }

        #endregion //UpdateStatus

        #region UpdateToolEnabledState

        private void UpdateToolEnabledState()
        {
            if (this.toolEnabledStateDirty == false)
                return;

            this.toolEnabledStateDirty = false;
            this.ultraToolbarsManager1.BeginUpdate();

            bool hasWorkbook = this.workbook != null;
            bool isInEditMode = this.selectionAdapter != null && this.selectionAdapter.IsInEditMode;
            UltraSpreadsheetState state = this.ultraSpreadsheet1.CurrentState;

            foreach (ToolBase tool in this.ultraToolbarsManager1.Tools)
            {
                UltraSpreadsheetAction action;
                if (Enum.TryParse<UltraSpreadsheetAction>(tool.Key, out action))
                {
                    tool.SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(action, state);
                }
                else
                {
                    switch (tool.Key)
                    {
                        case "MergeAndCenterMenu":
                        case "MergeAndCenter":
                            tool.SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(UltraSpreadsheetAction.MergeCellsAndCenter, state);
                            break;

                        case "InsertEntireRow":
                        case "InsertSheetRows":
                            tool.SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(UltraSpreadsheetAction.InsertRows, state);
                            break;

                        case "InsertEntireColumn":
                        case "InsertSheetColumns":
                            tool.SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(UltraSpreadsheetAction.InsertColumns, state);
                            break;

                        case "DeleteEntireRow":
                        case "DeleteSheetRows":
                            tool.SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(UltraSpreadsheetAction.DeleteRows, state);
                            break;

                        case "DeleteEntireColumn":
                        case "DeleteSheetColumns":
                            tool.SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(UltraSpreadsheetAction.DeleteColumns, state);
                            break;

                        case "FormatAsTable":
                            tool.SharedProps.Enabled = (hasWorkbook && this.selectionAdapter != null && this.selectionAdapter.CanExecuteFormatAsTable && !isInEditMode);
                            break;

                        case "BordersMenu":
                        case "Gridlines":
                        case "FormulaBar":
                        case "Headings":
                        case "Zoom":
                        case "FreezePanesMenu":
                        case "FillColor":
                        case "Insert":
                        case "Delete":
                        case "Format":
                        case "CellStyles":
                        case "AlignmentSettings":
                            tool.SharedProps.Enabled = hasWorkbook && !isInEditMode;
                            break;

                        case "Browse":
                        case "Computer":
                        case "ComputerLabel":
                        case "CurrentUser":
                        case "New":
                        case "Open":
                        case "OpenLabel":
                        case "RecentDocuments":
                        case "RecentFiles":
                        case "RecentFolders":
                            // do nothing
                            break;

                        default:
                            tool.SharedProps.Enabled = hasWorkbook;
                            break;
                    }
                }
            }

            this.ultraToolbarsManager1.EndUpdate();
        }
    
        #endregion //UpdateToolEnabledState

        #region UpdateUI

        private void UpdateUI()
        {
            bool originalSuspendEventFiring = this.suspendEventFiring;

            try
            {
                this.suspendEventFiring = true;

                var format = this.selectionAdapter.Format;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["ToggleBold"]).Checked = format.Font.Bold == ExcelDefaultableBoolean.True;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["ToggleItalic"]).Checked = format.Font.Italic == ExcelDefaultableBoolean.True;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["ToggleStrikeThrough"]).Checked = format.Font.Strikeout == ExcelDefaultableBoolean.True;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["ToggleWrapText"]).Checked = format.WrapText == ExcelDefaultableBoolean.True;

                switch (format.Alignment)
                {
                    case HorizontalCellAlignment.Left:
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignHorizontalLeft"]).Checked = true;
                        break;
                    case HorizontalCellAlignment.Center:
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignHorizontalCenter"]).Checked = true;
                        break;
                    case HorizontalCellAlignment.Right:
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignHorizontalRight"]).Checked = true;
                        break;
                    default:
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignHorizontalLeft"]).Checked = false;
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignHorizontalCenter"]).Checked = false;
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignHorizontalRight"]).Checked = false;
                        break;
                }

                switch (format.VerticalAlignment)
                {
                    case VerticalCellAlignment.Top:
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignVerticalTop"]).Checked = true;
                        break;
                    case VerticalCellAlignment.Center:
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignVerticalMiddle"]).Checked = true;
                        break;
                    case VerticalCellAlignment.Bottom:
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignVerticalBottom"]).Checked = true;
                        break;
                    default:
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignVerticalTop"]).Checked = false;
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignVerticalMiddle"]).Checked = false;
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["AlignVerticalBottom"]).Checked = false;
                        break;
                }

                PopupGalleryTool stylesTool = (PopupGalleryTool)this.ultraToolbarsManager1.Tools["CellStyles"];
                if (format.Style.IsBuiltIn && 
                    stylesTool.Items.Contains(format.Style.Name))
                    stylesTool.SelectedItemKey = format.Style.Name;
                else
                    stylesTool.SelectedItem = null;

                this.UpdateFontTools();
            }
            finally
            {
                this.suspendEventFiring = originalSuspendEventFiring;
            }
        }

        #endregion //UpdateUI

        #region UpdateUndoRedoHistoryTools

        private void UpdateUndoRedoHistoryTools(bool undo, bool redo)
        {
            Infragistics.Undo.UndoManager undoManager = this.ultraSpreadsheet1.UndoManager;
            bool isInEditMode = this.selectionAdapter.IsInEditMode;

            if (undo)
            {
                int index = 0;
                PopupMenuTool undoTool = this.ultraToolbarsManager1.Tools["Undo"] as PopupMenuTool;
                int historyToolsNeeded = (isInEditMode) ? 1 : Math.Min(undoManager.UndoHistory.Count, 10);
                for (index = 0; index < historyToolsNeeded; index++)
                {
                    string key = string.Format("HistoryUndo{0}", index);
                    if (this.ultraToolbarsManager1.Tools.Exists(key) == false)
                    {
                        this.ultraToolbarsManager1.Tools.Add(new ButtonTool(key));
                        undoTool.Tools.AddTool(key);
                    }
                    ToolBase undoHistoryTool = this.ultraToolbarsManager1.Tools[key];

                    if (isInEditMode)
                    {
                        undoHistoryTool.SharedProps.Caption =
                            undoHistoryTool.SharedProps.ToolTipText = ResourceStrings.Text_Typing;
                        undoHistoryTool.SharedProps.Visible = true;
                        undoHistoryTool.Tag = null;
                    }
                    else
                    {
                        Infragistics.Undo.UndoHistoryItem undoItem = undoManager.UndoHistory[index];
                        undoHistoryTool.SharedProps.Caption = undoItem.ShortDescription;
                        undoHistoryTool.SharedProps.ToolTipText = undoItem.LongDescription;
                        undoHistoryTool.SharedProps.Visible = true;
                        undoHistoryTool.Tag = undoItem;
                    }
                }
                while (index < 10)
                {
                    string key = string.Format("HistoryUndo{0}", index);
                    if (this.ultraToolbarsManager1.Tools.Exists(key))
                    {
                        ToolBase unusedTool = this.ultraToolbarsManager1.Tools[key];
                        unusedTool.SharedProps.Visible = false;
                        unusedTool.Tag = null;
                        index++;
                        continue;
                    }
                    break;
                }

                if (isInEditMode)
                {
                    // When the cell is in edit-mode, the Undo/Redo is handled by the editor.
                    Infragistics.Win.FormattedLinkLabel.FormattedLinkEmbeddableUIElement el = this.ultraSpreadsheet1.UIElement.GetDescendant(typeof(Infragistics.Win.FormattedLinkLabel.FormattedLinkEmbeddableUIElement)) as Infragistics.Win.FormattedLinkLabel.FormattedLinkEmbeddableUIElement;
                    var editor = el.Editor as Infragistics.Win.FormattedLinkLabel.FormattedLinkEditor;
                    undoTool.SharedProps.Enabled = (editor.EditInfo != null && editor.EditInfo.CanPerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.Undo));
                }
                else
                    undoTool.SharedProps.Enabled = undoManager.CanUndo;

            }

            if (redo)
            {
                int index = 0;
                PopupMenuTool redoTool = this.ultraToolbarsManager1.Tools["Redo"] as PopupMenuTool;
                int historyToolsNeeded = (isInEditMode) ? 1 : Math.Min(undoManager.RedoHistory.Count, 10);
                for (index = 0; index < historyToolsNeeded; index++)
                {
                    string key = string.Format("HistoryRedo{0}", index);
                    if (this.ultraToolbarsManager1.Tools.Exists(key) == false)
                    {
                        this.ultraToolbarsManager1.Tools.Add(new ButtonTool(key));
                        redoTool.Tools.AddTool(key);
                    }
                    ToolBase redoHistoryTool = this.ultraToolbarsManager1.Tools[key];

                    if (isInEditMode)
                    {
                        redoHistoryTool.SharedProps.Caption = 
                            redoHistoryTool.SharedProps.ToolTipText = ResourceStrings.Text_Typing;
                        redoHistoryTool.SharedProps.Visible = true;
                        redoHistoryTool.Tag = null;
                    }
                    else
                    {
                        Infragistics.Undo.UndoHistoryItem redoItem = undoManager.RedoHistory[index];
                        redoHistoryTool.SharedProps.Caption = redoItem.ShortDescription;
                        redoHistoryTool.SharedProps.ToolTipText = redoItem.LongDescription;
                        redoHistoryTool.SharedProps.Visible = true;
                        redoHistoryTool.Tag = redoItem;
                    }
                }
                while (index < 10)
                {
                    string key = string.Format("HistoryRedo{0}", index);
                    if (this.ultraToolbarsManager1.Tools.Exists(key))
                    {
                        ToolBase unusedTool = this.ultraToolbarsManager1.Tools[key];
                        unusedTool.SharedProps.Visible = false;
                        unusedTool.Tag = null;
                        index++;
                        continue;
                    }
                    break;
                }
                if (isInEditMode)
                {
                    // When the cell is in edit-mode, the Undo/Redo is handled by the editor.
                    Infragistics.Win.FormattedLinkLabel.FormattedLinkEmbeddableUIElement el = this.ultraSpreadsheet1.UIElement.GetDescendant(typeof(Infragistics.Win.FormattedLinkLabel.FormattedLinkEmbeddableUIElement)) as Infragistics.Win.FormattedLinkLabel.FormattedLinkEmbeddableUIElement;
                    var editor = el.Editor as Infragistics.Win.FormattedLinkLabel.FormattedLinkEditor;
                    redoTool.SharedProps.Enabled = (editor.EditInfo != null && editor.EditInfo.CanPerformAction(Infragistics.Win.FormattedLinkLabel.FormattedLinkEditorAction.Redo));
                }
                else
                    redoTool.SharedProps.Enabled = undoManager.CanRedo;

            }
        }
        #endregion //UpdateUndoRedoHistoryTools

        #endregion //Methods

        #region Event Handlers

        #region selectionAdapter_PropertyChanged

        private void selectionAdapter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case SpreadsheetSelectionAdapter.SelectionPropertyName:
                    this.UpdateUI();
                    break;

                case SpreadsheetSelectionAdapter.LastAppliedBorderSettingsPropertyName:
                    this.ultraToolbarsManager1.Tools["BordersMenu"].SharedProps.AppearancesSmall.Appearance.Image = string.Format("{0}_16x16.png", this.selectionAdapter.LastAppliedBorderSettings.ToString());
                    break;

                case SpreadsheetSelectionAdapter.CanExecuteFormatAsTablePropertyName:
                    this.ultraToolbarsManager1.Tools["FormatAsTable"].SharedProps.Enabled = this.selectionAdapter.CanExecuteFormatAsTable;
                    break;
                    
                case SpreadsheetSelectionAdapter.ActiveSelectionCellRangeFormatPropertyName:
                case SpreadsheetSelectionAdapter.IsInEditModePropertyName:
                    this.UpdateUndoRedoHistoryTools(true, true);
                    this.UpdateStatus();
                    this.toolEnabledStateDirty = true;
                    this.BeginInvoke((Action)(()=>             
                        {
                            this.UpdateToolEnabledState();
                        }));
                    break;
            }
        }

        #endregion //selectionAdapter_PropertyChanged

        #region selectionAdapter_WorkbookDirtied

        void selectionAdapter_WorkbookDirtied(object sender, EventArgs e)
        {
            this.WorkbookIsDirty = true;
        }

        #endregion //selectionAdapter_WorkbookDirtied

        #region tbZoom_ValueChanged

        private void tbZoom_ValueChanged(object sender, EventArgs e)
        {
            this.SetZoomLevel(tbZoom.Value);
        }

        #endregion //tbZoom_ValueChanged

        #region ultraStatusBar1_ButtonClick

        private void ultraStatusBar1_ButtonClick(object sender, Infragistics.Win.UltraWinStatusBar.PanelEventArgs e)
        {
            switch (e.Panel.Key)
            {
                case "Zoom":
                    this.ShowZoomDialog();
                    break;

            }
        }

        #endregion //ultraStatusBar1_ButtonClick

        #region ultraToolbarsManager1_BeforeApplicationMenu2010Displayed

        private void ultraToolbarsManager1_BeforeApplicationMenu2010Displayed(object sender, BeforeApplicationMenu2010DisplayedEventArgs e)
        {
            // Make sure the information in the Info tab on the Backstage is up to date.
            PopupControlContainerTool infoTool = this.ultraToolbarsManager1.Tools["Info"] as PopupControlContainerTool;
            ((IGExcel.Controls.InfoControl)infoTool.Control).Initialize(this.ShortWorkbookName, this.workbook);
        }

        #endregion //ultraToolbarsManager1_BeforeApplicationMenu2010Displayed

        #region ultraToolbarsManager1_GalleryToolItemClick

        private void ultraToolbarsManager1_GalleryToolItemClick(object sender, GalleryToolItemEventArgs e)
        {
            switch (e.GalleryTool.Key)
            {
                case "FormatAsTable":
                    if (this.selectionAdapter.SelectedTable != null)
                    {
                        this.selectionAdapter.SelectedTable.Style = this.workbook.StandardTableStyles[e.Item.Key];
                    }
                    else
                    {
                        this.ShowFormatAsTableDialog(e.Item.Key);
                    }
                    break;
                case "CellStyles":
                    this.ultraSpreadsheet1.ActiveSelectionCellRangeFormat.Style = this.workbook.Styles[e.Item.Key];
                    break;
            }
            this.UpdateUI();
        }

        #endregion //ultraToolbarsManager1_GalleryToolItemClick

        #region ultraToolbarsManager1_ToolClick

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            if (this.suspendEventFiring)
                return;

            switch (e.Tool.Key)
            {

                case "ZoomToSelection":             // ButtonTool
                case "ToggleSplitPanes":            // StateButtonTool
                case "Paste":                       // ButtonTool
                case "Cut":                         // ButtonTool
                case "Copy":                        // ButtonTool
                case "ToggleBold":                  // StateButtonTool
                case "ToggleItalic":                // StateButtonTool
                case "ToggleStrikeThrough":         // StateButtonTool
                case "AlignVerticalTop":            // StateButtonTool
                case "AlignVerticalMiddle":         // StateButtonTool
                case "AlignVerticalBottom":         // StateButtonTool
                case "AlignHorizontalLeft":         // StateButtonTool
                case "AlignHorizontalCenter":       // StateButtonTool
                case "AlignHorizontalRight":        // StateButtonTool
                case "DecreaseIndentation":         // ButtonTool
                case "IncreaseIndentation":         // ButtonTool
                case "ToggleWrapText":              // StateButtonTool
                case "MergeCellsAcross":            // ButtonTool
                case "MergeCells":                  // ButtonTool
                case "UnmergeCells":                // ButtonTool
                case "DeleteCellsShiftLeft":        // ButtonTool
                case "DeleteCellsShiftUp":          // ButtonTool
                case "AutoFitRowHeight":            // ButtonTool
                case "AutoFitColumnWidth":          // ButtonTool
                case "RenameWorksheet":             // ButtonTool
                case "HideRows":                    // ButtonTool
                case "HideColumns":                 // ButtonTool
                case "UnhideRows":                  // ButtonTool
                case "UnhideColumns":               // ButtonTool
                case "Undo":                        // PopupMenuTool
                case "Redo":                        // PopupMenuTool
                    {
                        UltraSpreadsheetAction action;
                        if (Enum.TryParse<UltraSpreadsheetAction>(e.Tool.Key, out action))
                        {
                            e.Tool.SharedProps.Enabled = this.ultraSpreadsheet1.PerformAction(action);
                        }
                    }
                    break;

                case "Gridlines":    // StateButtonTool
                    #region Gridlines
                    {
                        this.ultraSpreadsheet1.AreGridlinesVisible = ((StateButtonTool)this.ultraToolbarsManager1.Tools["Gridlines"]).Checked;
                    }
                    break;
                    #endregion

                case "FormulaBar":    // StateButtonTool
                    #region FormulaBar
                    {
                        this.ultraSpreadsheet1.FormulaBar.Visible = ((StateButtonTool)this.ultraToolbarsManager1.Tools["FormulaBar"]).Checked;
                    }
                    break;
                    #endregion

                case "Headings":    // StateButtonTool
                    #region Headings
                    {
                        this.ultraSpreadsheet1.AreHeadersVisible = ((StateButtonTool)this.ultraToolbarsManager1.Tools["Headings"]).Checked;
                    }
                    break;
                    #endregion

                case "Zoom":    // ButtonTool
                    #region Zoom
                    {
                        this.ShowZoomDialog();
                    }
                    break;
                    #endregion

                case "ZoomTo100":    // ButtonTool
                    #region ZoomTo100
                    {
                        this.SetZoomLevel(100);
                    }
                    break;
                    #endregion //ZoomTo100

                case "FreezePanes":    // ButtonTool
                    #region FreezePanes
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.ToggleFreezePanes);
                        this.ultraToolbarsManager1.Tools["FreezePanes"].SharedProps.Caption = (this.ultraSpreadsheet1.Panes.Count == 1)
                            ? ResourceStrings.ResourceManager.GetString("Text_FreezePanes")
                            : ResourceStrings.ResourceManager.GetString("Text_UnfreezePanes");
                    }
                    break;
                    #endregion //FreezePanes

                case "FreezeTopRow":    // ButtonTool
                    #region FreezeTopRow
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.FreezeFirstRow);
                        this.ultraToolbarsManager1.Tools["FreezePanes"].SharedProps.Caption = (this.ultraSpreadsheet1.Panes.Count == 1)
                            ? ResourceStrings.ResourceManager.GetString("Text_FreezePanes")
                            : ResourceStrings.ResourceManager.GetString("Text_UnfreezePanes");
                    }
                    break;
                    #endregion //FreezeTopRow

                case "FreezeFirstColumn":    // ButtonTool
                    #region FreezeFirstColumn
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.FreezeFirstColumn);
                        this.ultraToolbarsManager1.Tools["FreezePanes"].SharedProps.Caption = (this.ultraSpreadsheet1.Panes.Count == 1)
                            ? ResourceStrings.ResourceManager.GetString("Text_FreezePanes")
                            : ResourceStrings.ResourceManager.GetString("Text_UnfreezePanes");
                    }
                    break;
                    #endregion //FreezeFirstColumn

                case "IncreaseFontSize":    // ButtonTool
                    #region IncreaseFontSize
                    this.ultraSpreadsheet1.PerformAction(UltraSpreadsheetAction.IncreaseFontSize);
                    this.UpdateFontTools();
                    break;
                    #endregion //IncreaseFontSize

                case "DecreaseFontSize":    // ButtonTool
                    #region DecreaseFontSize
                    this.ultraSpreadsheet1.PerformAction(UltraSpreadsheetAction.DecreaseFontSize);
                    this.UpdateFontTools();
                    break;
                    #endregion //DecreaseFontSize

                case "BordersMenu":    // PopupMenuTool
                    #region BordersMenu
                    this.SetBorder();
                    break;
                    #endregion //BordersMenu

                case "FillColor":    // PopupColorPickerTool
                    #region FillColor
                    this.selectionAdapter.FillColor = ((PopupColorPickerTool)e.Tool).SelectedColor;
                    break;
                    #endregion //FillColor

                case "FontColor":    // PopupColorPickerTool
                    #region FontColor
                    this.selectionAdapter.ForegroundColor = ((PopupColorPickerTool)e.Tool).SelectedColor;
                    break;
                    #endregion //FontColor

                case "MergeAndCenterMenu":    // PopupMenuTool
                    #region MergeAndCenterMenu
                    this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.MergeCellsAndCenter);
                    break;
                    #endregion //MergeAndCenterMenu

                case "MergeAndCenter":    // ButtonTool
                    #region MergeAndCenter
                    this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.MergeCellsAndCenter);
                    break;
                    #endregion //MergeAndCenter

                case "InsertEntireRow":    // ButtonTool
                case "InsertSheetRows":    // ButtonTool
                    #region InsertEntireRow/InsertSheetRows
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.InsertRows);
                    }
                    break;
                    #endregion //InsertEntireRow/InsertSheetRows

                case "InsertEntireColumn":    // ButtonTool
                case "InsertSheetColumns":    // ButtonTool
                    #region InsertEntireColumn/InsertSheetColumns
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.InsertColumns);
                    }
                    break;
                    #endregion //InsertEntireColumn/InsertSheetColumns

                case "InsertNewWorksheets":    // ButtonTool
                    #region InsertNewWorksheets
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.InsertNewWorksheets);
                        this.ultraToolbarsManager1.Tools["DeleteWorksheets"].SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(UltraSpreadsheetAction.DeleteWorksheets);
                    }
                    break;
                    #endregion //InsertNewWorksheets

                case "InsertCellsShiftRight":    // ButtonTool
                    #region InsertCellsShiftRight
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.InsertCellsShiftRight);
                    }
                    break;
                    #endregion //InsertCellsShiftRight

                case "InsertCellsShiftDown":    // ButtonTool
                    #region InsertCellsShiftDown
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.InsertCellsShiftDown);
                    }
                    break;
                    #endregion //InsertCellsShiftDown

                case "DeleteEntireRow":    // ButtonTool
                case "DeleteSheetRows":    // ButtonTool
                    #region DeleteEntireRow/DeleteSheetRows
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.DeleteRows);
                    }
                    break;
                    #endregion //DeleteEntireRow/DeleteSheetRows

                case "DeleteEntireColumn":    // ButtonTool
                case "DeleteSheetColumns":    // ButtonTool
                    #region DeleteEntireColumn/DeleteSheetColumns
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.DeleteColumns);
                    }
                    break;
                    #endregion //DeleteEntireColumn/DeleteSheetColumns

                case "DeleteWorksheets":    // ButtonTool
                    #region DeleteWorksheets
                    {
                        this.ultraSpreadsheet1.PerformAction(Infragistics.Win.UltraWinSpreadsheet.UltraSpreadsheetAction.DeleteWorksheets);
                        e.Tool.SharedProps.Enabled = this.ultraSpreadsheet1.CanPerformAction(UltraSpreadsheetAction.DeleteWorksheets);
                    }
                    break;
                    #endregion //DeleteWorksheets

                case "RowHeight":    // ButtonTool
                    #region RowHeight
                    this.ShowDimensionDialog(Dialogs.DimensionDialog.DimensionMode.Height);
                    break;
                    #endregion //RowHeight

                case "ColumnWidth":    // ButtonTool
                    #region ColumnWidth
                    this.ShowDimensionDialog(Dialogs.DimensionDialog.DimensionMode.Width);
                    break;
                    #endregion //ColumnWidth

                case "DefaultWidth":    // ButtonTool
                    #region DefaultWidth

                    this.ShowDimensionDialog(Dialogs.DimensionDialog.DimensionMode.Standard);
                    break;
                    #endregion //DefaultWidth

                case "Save":    // ButtonTool
                case "Save2":    // StateButtonTool
                    #region Save
                    {
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["Save2"]).Checked = false;
                        this.Save(true);
                    }
                    break;
                    #endregion

                case "BottomBorder":                // ButtonTool
                case "TopBorder":                   // ButtonTool
                case "LeftBorder":                  // ButtonTool
                case "RightBorder":                 // ButtonTool
                case "NoBorder":                    // ButtonTool
                case "AllBorders":                  // ButtonTool
                case "OutsideBorder":               // ButtonTool
                case "ThickBoxBorder":              // ButtonTool
                case "BottomDoubleBorder":          // ButtonTool
                case "ThickBottomBorder":           // ButtonTool
                case "TopAndBottomBorder":          // ButtonTool
                case "TopAndThickBottomBorder":     // ButtonTool
                case "TopAndDoubleBottomBorder":    // ButtonTool
                    #region Borders
                    this.SetBorder(e.Tool.Key);
                    break;
                    #endregion //Borders

                case "Close":    // ButtonTool
                    #region Close
                    {
                        this.Workbook = null;
                    }
                    break;
                    #endregion //Close

                case "RecentFiles":    // ListTool
                    #region RecentFiles
                    {
                        this.OpenFile(e.ListToolItem.Tag as string);
                    }
                    break;
                    #endregion

                case "RecentFolders":    // ListTool
                    #region RecentFolders
                    {
                        this.Browse(e.ListToolItem.Tag as string, this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.NavigationMenu.ActiveContentTool.Key == "Open");
                    }
                    break;
                    #endregion

                case "Browse":    // ButtonTool
                    #region Browse
                    {
                        this.Browse(null, this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.NavigationMenu.ActiveContentTool.Key == "Open");
                    }
                    break;
                    #endregion

                case "FontSettings":    // ButtonTool
                    #region FontSettings

                    this.ShowFormatCellsDialog(Dialogs.FormatCellsDialog.InitialSelectedTab.Font);
                    break;
                    #endregion //FontSettings

                case "AlignmentSettings":    // ButtonTool
                    #region AlignmentSettings

                    this.ShowFormatCellsDialog(Dialogs.FormatCellsDialog.InitialSelectedTab.Alignment);
                    break;
                    #endregion //AlignmentSettings

                default:
                    #region default

                    if (e.Tool.Key.StartsWith("History"))
                    {
                        var historyItem = this.ultraToolbarsManager1.Tools[e.Tool.Key].Tag as Infragistics.Undo.UndoHistoryItem;
                        if (historyItem != null)
                            historyItem.Execute();
                        else
                            this.ultraSpreadsheet1.PerformAction(e.Tool.Key.Contains("Undo") ? UltraSpreadsheetAction.Undo : UltraSpreadsheetAction.Redo);
                    }

                    break;
                    #endregion //default

            }
        }

        #endregion //ultraToolbarsManager1_ToolClick

        #region ultraToolbarsManager1_ToolValueChanged

        private void ultraToolbarsManager1_ToolValueChanged(object sender, ToolEventArgs e)
        {
            if (this.suspendEventFiring)
                return;

            switch (e.Tool.Key)
            {

                case "FillColor":
                    #region FillColor
                    this.selectionAdapter.FillColor = ((PopupColorPickerTool)e.Tool).SelectedColor;
                    break;
                    #endregion //FillColor

                case "Font":
                    #region Font
                    this.selectionAdapter.Format.Font.Name = (string)((FontListTool)e.Tool).Value;
                    break;
                    #endregion //Font

                case "FontColor":
                    #region FontColor
                    this.selectionAdapter.ForegroundColor = ((PopupColorPickerTool)e.Tool).SelectedColor;
                    break;
                    #endregion //Fontcolor

                case "FontSize":
                    #region FontSize
                    this.selectionAdapter.Format.Font.Height = (int)((ComboBoxTool)e.Tool).Value;
                    break;
                    #endregion //FontSize
            }
        }

        #endregion //ultraToolbarsManager1_ToolValueChanged

        #region undoManager_PropertyChanged

        private void undoManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // rebuild the tools based on the UndoManager's history.
            switch (e.PropertyName)
            {
                case "TopUndoHistoryItem":
                case "TopRedoHistoryItem":
                    this.UpdateUndoRedoHistoryTools(e.PropertyName == "TopUndoHistoryItem", e.PropertyName == "TopRedoHistoryItem");
                    break;
            }
        }

        #endregion //undoManager_PropertyChanged

        #region workbook_PropertyChanged

        void workbook_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.WorkbookIsDirty = true;
        }

        #endregion //workbook_PropertyChanged

        #endregion //Event Handlers

    }
}
