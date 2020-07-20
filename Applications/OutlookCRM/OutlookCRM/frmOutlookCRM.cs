using System;
using System.Windows.Forms;
using Infragistics.Win.UltraWinExplorerBar;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using Infragistics.Win.UltraWinTabbedMdi;
using Infragistics.Win;

namespace OutlookCRM
{
    public partial class frmOutlookCRM : Form
    {
        #region Constants

        const string CUSTOMERS = "Customers";
        const string ORDERS = "Orders";
        const string ORDERDETAILS = "OrderDetails";
        const string PRODUCTS = "ProductSales";
        const string CATEGORIES = "SalesByCategory";
        const string SHIPPERS = "Shippers";
        const string CONTENTEMPTY = "Content Empty";

        #endregion // Constants

        #region Members

        private GridExportFileFormat gridExportFormat = GridExportFileFormat.PDF;
        private Timer progressTimer = null;
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel progressPanel = null;
        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel progressPanelLabel = null;
        private Random random = new Random((int)(DateTime.Now.Ticks));

        #endregion // Members

        #region Constructor
        public frmOutlookCRM()
        {
            InitializeComponent();
        }
        #endregion // Constructor

        #region Properties

        #region ProgressPanel

        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ProgressPanel
        {
            get
            {
                if (this.progressPanel == null)
                {
                    this.progressPanel = this.ultraStatusBar1.Panels["uspProgress"];
                }
                return this.progressPanel;
            }
        }

        private Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ProgressPanelLabel
        {
            get
            {
                if (this.progressPanelLabel == null)
                {
                    this.progressPanelLabel = this.ultraStatusBar1.Panels["lblProgress"];
                }
                return this.progressPanelLabel;
            }
        }

        #endregion // ProgressPanel

        #endregion // Properties

        #region Base Class Overrides

        #region Dispose
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                if (this.progressTimer != null)
                {
                    this.progressTimer.Stop();
                    this.progressTimer.Tick -= this.progressTimer_Tick;
                    this.progressTimer.Dispose();
                    this.progressTimer = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion // Dispose

        #endregion // Base Class Overrides

        #region Methods

        #region InitUI
        private void InitUI()
        {
            #region ExplorerBar
            // activate the first group and then activate the first item in that group.
            UltraExplorerBarGroup group = this.ultraExplorerBar1.Groups["Sales"];
            if (group != null)
            {
                group.Active = true;
                group.Selected = true;

                UltraExplorerBarItem item = group.Items["Customers"];
                if (item != null)
                {
                    item.Active = true;
                    this.ultraExplorerBar1.PerformAction(UltraExplorerBarAction.ClickActiveItem);
                }

            }
            #endregion // ExplorerBar

            #region AboutControl
            // setup the about control.
            Control control = new AboutControl();
            control.Visible = false;
            control.Parent = this;
            ((Infragistics.Win.UltraWinToolbars.PopupControlContainerTool)this.ultraToolbarsManager1.Tools["pccAbout"]).Control = control;
            #endregion // AboutControl

            #region StatusBar

            #region ProgressBar
            // setup a timer to show progress during certain operations that could be time consuming.
            progressTimer = Infragistics.Win.Utilities.CreateTimer();
            progressTimer.Interval = 250;
            progressTimer.Tick += progressTimer_Tick;
            progressTimer.Start();
            #endregion // ProgressBar

            #region TrackBar
            // if there is a Midpoint defined set the value of the trackbar to the midpoint value.
            if (this.ultraTrackBar1.MidpointSettings.Value.HasValue)
            {
                this.ultraTrackBar1.Value = this.ultraTrackBar1.MidpointSettings.Value.Value;
            }
            #endregion // TrackBar

            #endregion // StatusBar            
        }
        
        #endregion // InitUI

        #endregion // Methods

        #region Event Handlers

        #region frmOutlookCRM_Load
        private void frmOutlookCRM_Load(object sender, EventArgs e)
        {
            // Assign custom filters.
            this.ultraExplorerBar1.CreationFilter = new Filters.ExplorerBarCreationFilter();
            this.ultraToolbarsManager1.DrawFilter = new Filters.ToolbarDrawFilter();

            // Initialize the UI.
            InitUI();
        }

        #endregion // frmOutlookCRM_Load
        
        #region ultraToolbarsManager1_ToolClick
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            frmContent frmContent = this.ActiveMdiChild as frmContent;

            switch (e.Tool.Key)
            {
                #region NOT IMPLEMENTED

                case "btnNewCustomer":    // ButtonTool
                    // Place code here
                    break;

                case "btnEdit":    // ButtonTool
                    // Place code here
                    break;

                case "btnDuplicate":    // ButtonTool
                    // Place code here
                    break;

                case "btnDelete":    // ButtonTool
                    // Place code here
                    break;

                case "btnMerge":    // ButtonTool
                    // Place code here
                    break;

                case "btnDetectDuplicates":    // ButtonTool
                    // Place code here
                    break;

                case "btnSendEmail":    // ButtonTool
                    // Place code here
                    break;

                case "btnNewMeeting":    // ButtonTool
                    // Place code here
                    break;

                case "btnRunWorkflow":    // ButtonTool
                    // Place code here
                    break;

                case "btnStartDialog":    // ButtonTool
                    // Place code here
                    break;

                case "btnImportData":    // ButtonTool
                    // Place code here
                    break;

                case "btnAdvancedFind":    // ButtonTool
                    // Place code here
                    break;

                case "btnRunReport":    // PopupMenuTool
                    // Place code here
                    break;

                case "btnCall":    // ButtonTool
                    // Place code here
                    break;

                case "btnPrint":    // ButtonTool
                    // Place code here
                    break;

                case "pmtOptions":    // PopupMenuTool
                    // Place code here
                    break;

                case "pccAbout":    // PopupControlContainerTool
                    // Place code here
                    break;

                #endregion // NOT IMPLEMENTED

                #region Export buttons

                case "btnExcel":    // ButtonTool
                    if (frmContent == null)
                    {
                        return;
                    }
                    if (frmContent.Grid.IsExportAsyncInProgress)
                    {
                        MessageBox.Show(Properties.Resources.ExportInProgressMessage);
                        return;
                    }

                    // export the grid to excel.
                    this.ultraGridExcelExporter1.ExportAsync(frmContent.Grid, Application.StartupPath + @"\ExcelExport.xls");
                    break;

                case "btnPDF":    // ButtonTool
                    if (frmContent == null)
                    {
                        return;
                    }
                    if (frmContent.Grid.IsExportAsyncInProgress)
                    {
                        MessageBox.Show(Properties.Resources.ExportInProgressMessage);
                        return;
                    }

                    this.gridExportFormat = GridExportFileFormat.PDF;
                    // export the grid to pdf.
                    this.ultraGridDocumentExporter1.ExportAsync(frmContent.Grid, Application.StartupPath + @"\pdfExport.pdf", Infragistics.Win.UltraWinGrid.DocumentExport.GridExportFileFormat.PDF);
                    break;

                case "btnXPS":    // ButtonTool
                    if (frmContent == null)
                    {
                        return;
                    }
                    if (frmContent.Grid.IsExportAsyncInProgress)
                    {
                        MessageBox.Show(Properties.Resources.ExportInProgressMessage);
                        return;
                    }
                    this.gridExportFormat = GridExportFileFormat.XPS;
                    // export the grid to xps.
                    this.ultraGridDocumentExporter1.ExportAsync(frmContent.Grid, Application.StartupPath + @"\xpsExport.xps", Infragistics.Win.UltraWinGrid.DocumentExport.GridExportFileFormat.XPS);
                    break;

                #endregion // Export buttons

                #region View -> Layout ListItems

                case "listPaneLayout":    // ListTool
                    if (frmContent == null)
                    {
                        return;
                    }
                    // toggle the pane layout.
                    switch (e.ListToolItem.Key)
                    {
                        case "Right":
                            frmContent.SetPaneLayout(Enums.PaneLayout.Right);
                            break;
                        case "Bottom":
                            frmContent.SetPaneLayout(Enums.PaneLayout.Bottom);
                            break;
                        case "Off":
                            frmContent.SetPaneLayout(Enums.PaneLayout.Off);
                            break;
                        default:
                            break;
                    }
                    break;

                #endregion // View -> Layout ListItems

                #region Exit
                case "btnExit":    // ButtonTool
                    Application.Exit();
                    break;
                #endregion // Exit

                default:
                    break;
            }
        }
        #endregion // ultraToolbarsManager1_ToolClick

        #region ultraExplorerBar1_ItemClick
        private void ultraExplorerBar1_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {
            #region progress
            // show a progress bar in the status bar
            if (this.progressTimer != null && this.ProgressPanel != null && this.ProgressPanelLabel != null)
            {
                this.progressTimer.Stop();
                this.progressPanel.ProgressBarInfo.Value = this.progressPanel.ProgressBarInfo.Minimum;
                this.progressPanelLabel.Text = string.Format("{0}: {1}%", Utilities.GetLocalizedString("Updating"), this.progressPanel.ProgressBarInfo.Value);
                this.progressTimer.Start();
            }
            #endregion // progress

            frmContent frmContent = null;
            MdiTab mdiTab = null;

            // get the appropriate content form instance.  If there is no instance yet, create one of the appropriate content type.
            switch (e.Item.Key)
            {
                case CUSTOMERS:
                    mdiTab = this.ultraTabbedMdiManager1.TabFromKey(CUSTOMERS);
                    frmContent = mdiTab == null
                                     ? new frmContent(OutlookCRM.Enums.ContentType.Customers) { MdiParent = this }
                                     : mdiTab.Form as frmContent;
                    break;
                case ORDERS:
                    mdiTab = this.ultraTabbedMdiManager1.TabFromKey(ORDERS);
                    frmContent = mdiTab == null
                                     ? new frmContent(OutlookCRM.Enums.ContentType.Orders) { MdiParent = this }
                                     : mdiTab.Form as frmContent;
                    break;
                case ORDERDETAILS:
                    mdiTab = this.ultraTabbedMdiManager1.TabFromKey(ORDERDETAILS);
                    frmContent = mdiTab == null
                                     ? new frmContent(OutlookCRM.Enums.ContentType.OrderDetails) { MdiParent = this }
                                     : mdiTab.Form as frmContent;
                    break;
                case PRODUCTS:
                    mdiTab = this.ultraTabbedMdiManager1.TabFromKey(PRODUCTS);
                    frmContent = mdiTab == null
                                     ? new frmContent(OutlookCRM.Enums.ContentType.ProductSales) { MdiParent = this }
                                     : mdiTab.Form as frmContent;
                    break;
                case CATEGORIES:
                    mdiTab = this.ultraTabbedMdiManager1.TabFromKey(CATEGORIES);
                    frmContent = mdiTab == null
                                     ? new frmContent(OutlookCRM.Enums.ContentType.SalesByCategory) { MdiParent = this }
                                     : mdiTab.Form as frmContent;
                    break;
                case SHIPPERS:
                    mdiTab = this.ultraTabbedMdiManager1.TabFromKey(SHIPPERS);
                    frmContent = mdiTab == null
                                     ? new frmContent(OutlookCRM.Enums.ContentType.Shippers) { MdiParent = this }
                                     : mdiTab.Form as frmContent;
                    break;
                default:
                    mdiTab = this.ultraTabbedMdiManager1.TabFromKey(CONTENTEMPTY);
                    frmContentEmpty frmContentEmpty = mdiTab == null
                                     ? new frmContentEmpty() { MdiParent = this }
                                     : mdiTab.Form as frmContentEmpty;
                    if (frmContentEmpty != null)
                    {
                        frmContentEmpty.Show();
                        frmContentEmpty.Activate();
                    }
                    break;
            }
            if (frmContent != null)
            {
                // show and activate the appropriate instance of the content form.
                frmContent.Show();
                frmContent.Activate();
            }
        }
        #endregion // ultraExplorerBar1_ItemClick
        
        #region ultraTabbedMdiManager1_InitializeTab
        private void ultraTabbedMdiManager1_InitializeTab(object sender, Infragistics.Win.UltraWinTabbedMdi.MdiTabEventArgs e)
        {
            // initialize the mdi tabs key as appropriate.
            frmContent frmContent = e.Tab.Form as frmContent;
            if (frmContent == null)
            {
                if (e.Tab.Form is frmContentEmpty)
                {
                    e.Tab.Key = CONTENTEMPTY;
                }
                return;
            }

            switch (frmContent.ContentType)
            {
                case OutlookCRM.Enums.ContentType.Customers:
                    e.Tab.Key = CUSTOMERS;
                    this.ultraStatusBar1.SetStatusBarText(frmContent.Grid, Utilities.GetLocalizedString("Customers"));
                    break;
                case OutlookCRM.Enums.ContentType.Orders:
                    e.Tab.Key = ORDERS;
                    this.ultraStatusBar1.SetStatusBarText(frmContent.Grid, Utilities.GetLocalizedString("Orders"));
                    break;
                case OutlookCRM.Enums.ContentType.OrderDetails:
                    e.Tab.Key = ORDERDETAILS;
                    this.ultraStatusBar1.SetStatusBarText(frmContent.Grid, Properties.Resources.OrderDetails);
                    break;
                case OutlookCRM.Enums.ContentType.ProductSales:
                    e.Tab.Key = PRODUCTS;
                    this.ultraStatusBar1.SetStatusBarText(frmContent.Grid, Utilities.GetLocalizedString("Products"));
                    break;
                case OutlookCRM.Enums.ContentType.SalesByCategory:
                    e.Tab.Key = CATEGORIES;
                    this.ultraStatusBar1.SetStatusBarText(frmContent.Grid, Utilities.GetLocalizedString("Categories"));
                    break;
                case OutlookCRM.Enums.ContentType.Shippers:
                    e.Tab.Key = SHIPPERS;
                    this.ultraStatusBar1.SetStatusBarText(frmContent.Grid, Utilities.GetLocalizedString("Shippers"));
                    break;
                default:
                    break;
            }

            // set the AutoStatus text for the status bar when over the parts of the content form.
            this.ultraStatusBar1.SetStatusBarText(frmContent.Grid, frmContent.GetStatusBarTextGrid());
            this.ultraStatusBar1.SetStatusBarText(frmContent.Chart1, frmContent.GetStatusBarTextChart1());
            this.ultraStatusBar1.SetStatusBarText(frmContent.Chart2, frmContent.GetStatusBarTextChart2());
            this.ultraStatusBar1.SetStatusBarText(frmContent.Chart3, frmContent.GetStatusBarTextChart3());
        }
        #endregion // ultraTabbedMdiManager1_InitializeTab

        #region ultraGridExcelExporter1_ExportEnded
        private void ultraGridExcelExporter1_ExportEnded(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.ExportEndedEventArgs e)
        {
            // show the excel export in excel.
            if (!e.Canceled)
                System.Diagnostics.Process.Start(Application.StartupPath + @"\ExcelExport.xls");
        }
        #endregion // ultraGridExcelExporter1_ExportEnded

        #region ultraGridDocumentExporter1_ExportEnded
        private void ultraGridDocumentExporter1_ExportEnded(object sender, Infragistics.Win.UltraWinGrid.DocumentExport.ExportEndedEventArgs e)
        {
            // show the document export in the appropriate format.
            if (!e.Canceled)
            {
                switch (this.gridExportFormat)
                {
                    case GridExportFileFormat.PDF:
                        System.Diagnostics.Process.Start(Application.StartupPath + @"\pdfExport.pdf");
                        break;
                    case GridExportFileFormat.XPS:
                        System.Diagnostics.Process.Start(Application.StartupPath + @"\xpsExport.xps");
                        break;
                }
            }
        }
        #endregion // ultraGridDocumentExporter1_ExportEnded

        #region progressTimer_Tick
        void progressTimer_Tick(object sender, EventArgs e)
        {
            // show a progress bar when certain operations take place.
            if (this.ProgressPanel != null && this.ProgressPanelLabel != null)
            {
                int step = random.Next(0, 10);
                if (this.progressPanel.ProgressBarInfo.Value + step > this.progressPanel.ProgressBarInfo.Maximum)
                {
                    this.progressPanel.ProgressBarInfo.Value = this.progressPanel.ProgressBarInfo.Minimum;
                    this.progressPanelLabel.Text = Properties.Resources.UpToDate;
                    this.progressTimer.Stop();
                }
                else
                {
                    this.progressPanel.ProgressBarInfo.Value += step;
                    this.progressPanelLabel.Text = string.Format("{0}: {1}%", Utilities.GetLocalizedString("Updating"), this.progressPanel.ProgressBarInfo.Value);
                }
            }
            else
            {
                this.progressTimer.Stop();
            }
        }
        #endregion // progressTimer_Tick

        #region ultraTrackBar1_ValueChanged
        private void ultraTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            // update the label that shows the value of the trackbar.
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel usp = this.ultraStatusBar1.Panels["lblZoom"];
            if (usp != null)
            {
                usp.Text = string.Format("{0}%", this.ultraTrackBar1.Value);
            }
        }
        #endregion // ultraTrackBar1_ValueChanged

        #region ultraTrackBar1_DoubleClick
        private void ultraTrackBar1_DoubleClick(object sender, EventArgs e)
        {
            // snap to the midpoint value if a double click on the trackbar occurs.
            if (!this.ultraTrackBar1.MidpointSettings.Value.HasValue)
                return;

            MouseEventArgs mouseEventArgs = e as MouseEventArgs;
            if (mouseEventArgs != null)
            {
                UIElement uie = this.ultraTrackBar1.UIElement.ElementFromPoint(mouseEventArgs.Location);
                if (uie != null && uie.GetAncestor(typeof(Infragistics.Win.UltraWinEditors.TrackBarThumbUIElement)) != null)
                {
                    this.ultraTrackBar1.Value = this.ultraTrackBar1.MidpointSettings.Value.Value;
                }
            }
        }
        #endregion // ultraTrackBar1_DoubleClick

        #endregion // Event Handlers
    }
}
