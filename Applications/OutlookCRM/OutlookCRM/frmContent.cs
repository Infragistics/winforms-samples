using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.DataVisualization;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinMaskedEdit;
using Infragistics.Win.UltraWinSchedule;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;
using Orientation = System.Windows.Forms.Orientation;

namespace OutlookCRM
{
    public partial class frmContent : Form
    {
        #region Members

        private readonly OutlookCRM.Enums.ContentType contentType = Enums.ContentType.None;
        private Random random = new Random((int)(DateTime.Now.Ticks));
        private DataSet nWind1 = new DataSet();
        #endregion // Members

        #region Constructors
        public frmContent()
        {

            InitializeComponent();
               //this.nWind1.Load (~/Data\NWind.xml
        }

        public frmContent(OutlookCRM.Enums.ContentType contentType ) : this()
        {
            this.contentType = contentType;

            this.nWind1.DataSetName = "NWind";
            this.nWind1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;

            // Get the data loaded up.
            this.nWind1.ReadXml(Utilities.GetEmbeddedResourceStream("OutlookCRM.Data.NWind.xml"));
            this.nWind1.AcceptChanges();

            // Initialize the UI.
            InitUI();
        }

        #endregion // Constructors

        #region Properties

        #region ContentType
        /// <summary>
        /// Returns the type of content this form displays.
        /// </summary>
        public OutlookCRM.Enums.ContentType ContentType
        {
            get { return this.contentType; }
        }
        #endregion // ContentType

        #region Grid
        public Infragistics.Win.UltraWinGrid.UltraGrid Grid
        {
            get { return this.ultraGrid1; }
        }
        #endregion // Grid

        #region Chart1
        //public Infragistics.Win.UltraWinChart.UltraChart Chart1
        //{
        //    get { return this.ultraChart1; }
        //}

       public Infragistics.Win.DataVisualization.UltraDataChart Chart1
        {
           get { return this.ultraDataChart1; }
        }
        #endregion // Chart1

        #region Chart2
        //public Infragistics.Win.UltraWinChart.UltraChart Chart2
        //{
        //    get { return this.ultraChart2; }
        //}
        public Infragistics.Win.DataVisualization.UltraPieChart Chart2
        {
            get { return this.ultraPieChart1; }
        }
        #endregion // Chart2

        #region Chart3
        //public Infragistics.Win.UltraWinChart.UltraChart Chart3
        //{
        //    get { return this.ultraChart3; }
        //}
        public Infragistics.Win.DataVisualization.UltraDataChart Chart3
        {
            get { return this.ultraDataChart3; }
        }
        #endregion // Chart3

        #endregion // Properties

        #region Event Handlers

        #region frmContent_Load
        private void frmContent_Load(object sender, EventArgs e)
        {
            // Assign custom filters.
            this.ultraGrid1.DrawFilter = new Filters.GridDrawFilter();
        }

        #endregion // frmContent_Load

        #region frmContent_Shown
        private void frmContent_Shown(object sender, EventArgs e)
        {
            // Autosize grid columns.
            this.ultraGrid1.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.VisibleRows, AutoResizeColumnWidthOptions.All);

            if (this.ultraGrid1.Rows.Count > 0)
            {
                // Activate the first row.
                this.ultraGrid1.Rows[0].Activate();

                if (this.ultraGrid1.ActiveRow != null)
                {
                    if (!this.ultraGrid1.ActiveRow.IsGroupByRow)
                    {
                        // If not a group by row then activate the first cell.
                        UltraGridCell cell = this.ultraGrid1.ActiveRow.Cells[this.ultraGrid1.DisplayLayout.Bands[0].GetFirstVisibleCol(this.ultraGrid1.DisplayLayout.ColScrollRegions[0], false)];
                        if (cell != null)
                        {
                            cell.Activate();
                            if (this.contentType == Enums.ContentType.Customers)
                            {
                                // If this is the Customers ContentType then we actually want the next cell active.
                                this.ultraGrid1.PerformAction(UltraGridAction.NextCell);
                            }
                        }
                    }
                    else
                    {
                        // If this is a group by row then expand all the rows.
                        this.ultraGrid1.Rows.ExpandAll(false);
                        if (this.contentType == Enums.ContentType.OrderDetails)
                        {
                            // Activate the first row under the first expanded group by row.
                            this.ultraGrid1.Rows[0].ChildBands[0].Rows[0].Activate();
                            UltraGridCell cell = this.ultraGrid1.ActiveRow.Cells[this.ultraGrid1.DisplayLayout.Bands[0].GetFirstVisibleCol(this.ultraGrid1.DisplayLayout.ColScrollRegions[0], false)];
                            if (cell != null)
                            {
                                // Activate the first cell.
                                cell.Activate();
                            }
                        }
                    }
                }
            }

            // Give the charts sample data
            this.AssignChartData();
            // The pie chart has the ability to break apart specific slices to draw attention to that slice.
            //            this.ultraChart2.PieChart.BreakSlice(1, true);
           // this.piecha.PieChart.BreakSlice(1, true);

        }
        #endregion // frmContent_Shown

        #region ultraGrid1_AfterRowActivate
        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            this.AssignChartData();
        }
        #endregion // ultraGrid1_AfterRowActivate

        #region ultraGrid1_InitializeLayout
        private void ultraGrid1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout gridLayout = e.Layout;

            switch (this.contentType)
            {
                case Enums.ContentType.Customers:
                    #region Customers
                    // use fixed headers.
                    gridLayout.UseFixedHeaders = true;
                    // turn off the fixed header indicator so the user cannot change them.
                    gridLayout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;
                    // turn off the border that draws between fixed cells and non-fixed cells.
                    gridLayout.Override.FixedCellSeparatorColor = Color.Transparent;
                    // hide the customer id column.
                    gridLayout.Bands[0].Columns["CustomerID"].Hidden = true;

                    // add an unbound column for an image.
                    if (!gridLayout.Bands[0].Columns.Contains("CustomerImage"))
                    {
                        UltraGridColumn col = gridLayout.Bands[0].Columns.Insert(0, "CustomerImage");
                        col.DataType = typeof(Image);
                        col.Style = ColumnStyle.Image;
                        col.Header.Caption = string.Empty;
                        col.Header.Fixed = true;
                    }
                    #endregion // Customers
                    break;
                case Enums.ContentType.Orders:
                    #region Orders
                    // hide the order id column.
                    gridLayout.Bands[0].Columns["OrderID"].Hidden = true;
                    // set the caption of the other id fields to a ui friendly string.
                    gridLayout.Bands[0].Columns["CustomerID"].Header.Caption = Utilities.GetLocalizedString("Customer");
                    gridLayout.Bands[0].Columns["EmployeeID"].Header.Caption = Utilities.GetLocalizedString("Employee");

                    // An example to show a combo editor in a cell instead of showing the id.
                    Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboEditor = new UltraComboEditor();
                    this.Controls.Add(ultraComboEditor);
                    ultraComboEditor.DataSource = this.nWind1.Tables["Employees"];

                    ultraComboEditor.DisplayMember = "LastName";
                    ultraComboEditor.ValueMember = "EmployeeID";
                    ultraComboEditor.ValueList.ItemHeight = 35;
                    gridLayout.Bands[0].Columns["EmployeeID"].Style = ColumnStyle.DropDownList;
                    gridLayout.Bands[0].Columns["EmployeeID"].EditorComponent = ultraComboEditor;

                    // An example to show a combo editor in a cell instead of showing the id.
                    Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboEditor2 = new UltraComboEditor();
                    this.Controls.Add(ultraComboEditor2);
                    ultraComboEditor2.DataSource = this.nWind1.Tables["Customers"];
                    ultraComboEditor2.DisplayMember = "CompanyName";
                    ultraComboEditor2.ValueMember = "CustomerID";
                    ultraComboEditor2.ValueList.ItemHeight = 35;
                    gridLayout.Bands[0].Columns["CustomerID"].Style = ColumnStyle.DropDownList;
                    gridLayout.Bands[0].Columns["CustomerID"].EditorComponent = ultraComboEditor2;

                    // An example to show a combo editor in a cell instead of showing the id.
                    Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboEditor3 = new UltraComboEditor();
                    this.Controls.Add(ultraComboEditor3);
                    ultraComboEditor3.DataSource = this.nWind1.Tables["Shippers"];
                    ultraComboEditor3.DisplayMember = "CompanyName";
                    ultraComboEditor3.ValueMember = "ShipperID";
                    ultraComboEditor3.ValueList.ItemHeight = 35;
                    gridLayout.Bands[0].Columns["ShipVia"].Style = ColumnStyle.DropDownList;
                    gridLayout.Bands[0].Columns["ShipVia"].EditorComponent = ultraComboEditor3;

                    // An example to show a calendar combo in a cell instead of showing the DateTimeEditor and the .net month calendar.
                    Infragistics.Win.UltraWinSchedule.UltraCalendarCombo ultraCalendarCombo = new UltraCalendarCombo();
                    this.Controls.Add(ultraCalendarCombo);
                    gridLayout.Bands[0].Columns["OrderDate"].EditorComponent = ultraCalendarCombo;

                    Infragistics.Win.UltraWinSchedule.UltraCalendarCombo ultraCalendarCombo2 = new UltraCalendarCombo();
                    this.Controls.Add(ultraCalendarCombo2);
                    gridLayout.Bands[0].Columns["RequiredDate"].EditorComponent = ultraCalendarCombo2;

                    Infragistics.Win.UltraWinSchedule.UltraCalendarCombo ultraCalendarCombo3 = new UltraCalendarCombo();
                    this.Controls.Add(ultraCalendarCombo3);
                    gridLayout.Bands[0].Columns["ShippedDate"].EditorComponent = ultraCalendarCombo3;
                    #endregion // Orders
                    break;
                case Enums.ContentType.OrderDetails:
                    #region OrderDetails
                    // set the caption of the other id fields to a ui friendly string.
                    gridLayout.Bands[0].Columns["OrderID"].Header.Caption = Utilities.GetLocalizedString("Order");
                    gridLayout.Bands[0].Columns["ProductID"].Header.Caption = Utilities.GetLocalizedString("Product");
                    // give ProductID a MinWidth so it looks nice.
                    gridLayout.Bands[0].Columns["ProductID"].MinWidth = 200;

                    // An example to show a combo editor in a cell instead of showing the id.
                    Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboEditor4 = new UltraComboEditor();
                    this.Controls.Add(ultraComboEditor4);
                    ultraComboEditor4.DataSource = this.nWind1.Tables["Products"];
                    ultraComboEditor4.DisplayMember = "ProductName";
                    ultraComboEditor4.ValueMember = "ProductID";
                    gridLayout.Bands[0].Columns["ProductID"].Style = ColumnStyle.DropDownList;
                    gridLayout.Bands[0].Columns["ProductID"].EditorComponent = ultraComboEditor4;

                    // UnitPrice can pick up local currency symbols and display styles.
                    gridLayout.Bands[0].Columns["UnitPrice"].Style = ColumnStyle.Currency;
                    gridLayout.Bands[0].Columns["UnitPrice"].MaskDisplayMode = MaskMode.IncludeLiterals;

                    // make this grid show in OutlookGroupBy mode.
                    gridLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
                    // make the OrderID column the GroupBy column.
                    gridLayout.Bands[0].SortedColumns.Add("OrderID", false, true);
                    #endregion // OrderDetails
                    break;
                case Enums.ContentType.QuarterlyOrders:
                    break;
                case Enums.ContentType.ProductSales:
                    #region Products
                    // hide the product id and supplier id columns.
                    gridLayout.Bands[0].Columns["ProductID"].Hidden = true;
                    gridLayout.Bands[0].Columns["SupplierID"].Hidden = true;
                    // set the caption of the other id fields to a ui friendly string.
                    gridLayout.Bands[0].Columns["CategoryID"].Header.Caption = Utilities.GetLocalizedString("Category");

                    // An example to show a combo editor in a cell instead of showing the id.
                    Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboEditor5 = new UltraComboEditor();
                    this.Controls.Add(ultraComboEditor5);
                    ultraComboEditor5.DataSource = this.nWind1.Tables["Categories"];
                    ultraComboEditor5.DisplayMember = "CategoryName";
                    ultraComboEditor5.ValueMember = "CategoryID";
                    gridLayout.Bands[0].Columns["CategoryID"].Style = ColumnStyle.DropDownList;
                    gridLayout.Bands[0].Columns["CategoryID"].EditorComponent = ultraComboEditor5;

                    // UnitPrice can pick up local currency symbols and display styles.
                    gridLayout.Bands[0].Columns["UnitPrice"].Style = ColumnStyle.Currency;
                    gridLayout.Bands[0].Columns["UnitPrice"].MaskDisplayMode = MaskMode.IncludeLiterals;
                    #endregion // Products
                    break;
                case Enums.ContentType.SalesByCategory:
                    #region Categories
                    // hide the category id and the hideous NorthWind picture columns.
                    gridLayout.Bands[0].Columns["CategoryID"].Hidden = true;
                    gridLayout.Bands[0].Columns["Picture"].Hidden = true;
                    #endregion // Categories
                    break;
                case Enums.ContentType.SalesByQuarter:
                    break;
                case Enums.ContentType.SalesByYear:
                    break;
                case Enums.ContentType.Shippers:
                    #region Shippers
                    // hide the shipper id column
                    gridLayout.Bands[0].Columns["ShipperID"].Hidden = true;
                    #endregion // Shippers
                    break;
                case Enums.ContentType.Suppliers:
                    break;
                default:
                    break;
            }

            #region UI friendly column headers
            // give the column headers more readable strings instead of mushed camel casing text.
            foreach (UltraGridBand ultraGridBand in gridLayout.Bands)
            {
                foreach (UltraGridColumn column in ultraGridBand.Columns)
                {
                    Utilities.SetUIFriendlyString(column.Header);
                }
            }
            #endregion //UI friendly column headers
        }

        #endregion // ultraGrid1_InitializeLayout

        #region ultraGrid1_InitializeRow
        private void ultraGrid1_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            switch (this.contentType)
            {
                case Enums.ContentType.Customers:
                    // create a random people image for the unbound CustomerImage column
                    e.Row.Cells["CustomerImage"].Appearance.ImageBackground = (Utilities.GetImageFromResource("People0" + random.Next(1, 7) + ".png"));
                    break;
                case Enums.ContentType.Orders:

                    break;
                case Enums.ContentType.OrderDetails:

                    break;
                case Enums.ContentType.QuarterlyOrders:
                    break;
                case Enums.ContentType.ProductSales:

                    break;
                case Enums.ContentType.SalesByCategory:

                    break;
                case Enums.ContentType.SalesByQuarter:
                    break;
                case Enums.ContentType.SalesByYear:
                    break;
                case Enums.ContentType.Shippers:

                    break;
                case Enums.ContentType.Suppliers:
                    break;
                default:
                    break;
            }
        }
        #endregion // ultraGrid1_InitializeRow

        #region ultraGrid1_BeforeSortChange
        private void ultraGrid1_BeforeSortChange(object sender, BeforeSortChangeEventArgs e)
        {
            // Do not sort on the CustomerImage column.
            if (this.contentType == Enums.ContentType.Customers)
            {
                if (e.SortedColumns.Exists("CustomerImage"))
                {
                    e.SortedColumns.Remove("CustomerImage");
                }
            }
        }
        #endregion // ultraGrid1_BeforeSortChange

        #region ultraGrid1_BeforeColPosChanged
        private void ultraGrid1_BeforeColPosChanged(object sender, BeforeColPosChangedEventArgs e)
        {
            // Do not resize the CustomerImage column.
            if (this.contentType == Enums.ContentType.Customers)
            {
                if (e.ColumnHeaders[0].Column.Key == "CustomerImage")
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion // ultraGrid1_BeforeColPosChanged

        #endregion // Event Handlers

        #region Methods

        #region InitUI
        private void InitUI()
        {
            // set the appropriate grid DataMember, Pane Header and Description text, and the form text.
            switch (this.contentType)
            {
                case Enums.ContentType.Customers:
                    this.ultraGrid1.DataMember = this.nWind1.Tables["Customers"].TableName;
                    this.lblPaneHeader.Text = Properties.Resources.CustomerDashboard;
                    this.Text = Utilities.GetLocalizedString("Customers");
                    this.lblPaneDescription.Text = Utilities.GetLocalizedString("CustomerDashboardDescription");
                    break;
                case Enums.ContentType.Orders:
                    this.ultraGrid1.DataMember = this.nWind1.Tables["Orders"].TableName;
                    this.lblPaneHeader.Text = Properties.Resources.CustomerOrders;
                    this.Text = Utilities.GetLocalizedString("Orders");
                    this.lblPaneDescription.Text = Utilities.GetLocalizedString("OrdersDashboardDescription");
                    break;
                case Enums.ContentType.OrderDetails:
                    this.ultraGrid1.DataMember = this.nWind1.Tables["Order_Details"].TableName;
                    this.lblPaneHeader.Text = Properties.Resources.OrderDetails;
                    this.Text = Properties.Resources.OrderDetails;
                    this.lblPaneDescription.Text = Utilities.GetLocalizedString("OrderDetailsDashboardDescription");
                    break;
                case Enums.ContentType.QuarterlyOrders:
                    break;
                case Enums.ContentType.ProductSales:
                    this.ultraGrid1.DataMember = this.nWind1.Tables["Products"].TableName;
                    this.lblPaneHeader.Text = Properties.Resources.ProductInformation;
                    this.Text = Utilities.GetLocalizedString("Products");
                    this.lblPaneDescription.Text = Utilities.GetLocalizedString("ProductsDashboardDescription");
                    break;
                case Enums.ContentType.SalesByCategory:
                    this.ultraGrid1.DataMember = this.nWind1.Tables["Categories"].TableName;
                    this.lblPaneHeader.Text = Utilities.GetLocalizedString("Categories");
                    this.Text = Utilities.GetLocalizedString("Categories");
                    this.lblPaneDescription.Text = Utilities.GetLocalizedString("CategoriesDashboardDescription");
                    break;
                case Enums.ContentType.SalesByQuarter:
                    break;
                case Enums.ContentType.SalesByYear:
                    break;
                case Enums.ContentType.Shippers:
                    this.ultraGrid1.DataMember = this.nWind1.Tables["Shippers"].TableName;
                    this.lblPaneHeader.Text = Properties.Resources.ShipperInformation;
                    this.Text = Utilities.GetLocalizedString("Shippers");
                    this.lblPaneDescription.Text = Utilities.GetLocalizedString("ShippersDashboardDescription");
                    break;
                case Enums.ContentType.Suppliers:
                    break;
                default:
                    break;
            }
        }
        #endregion // InitUI

        #region SetPaneLayout
        public void SetPaneLayout(OutlookCRM.Enums.PaneLayout paneLayout)
        {
            switch (paneLayout)
            {
                case OutlookCRM.Enums.PaneLayout.Right:
                    // Show the Chart portion.
                    this.splitContainer1.Panel2Collapsed = false;
                    // Change the orientation to simulate the layout change.
                    this.splitContainer1.Orientation = Orientation.Vertical;

                    // some manipulate of the table layout panel to swap the charts around for a better view.
                    //this.tableLayoutPanel1.SetRowSpan(this.ultraChart1, 1);
                    //this.tableLayoutPanel1.SetColumnSpan(this.ultraChart1, 3);

                    //this.tableLayoutPanel1.SetRow(this.ultraChart2, 3);
                    //this.tableLayoutPanel1.SetRowSpan(this.ultraChart2, 1);
                    //this.tableLayoutPanel1.SetColumn(this.ultraChart2, 0);
                    //this.tableLayoutPanel1.SetColumnSpan(this.ultraChart2, 3);

                    //this.tableLayoutPanel1.SetRow(this.ultraChart3, 4);
                    //this.tableLayoutPanel1.SetRowSpan(this.ultraChart3, 1);
                    //this.tableLayoutPanel1.SetColumn(this.ultraChart3, 0);
                    //this.tableLayoutPanel1.SetColumnSpan(this.ultraChart3, 3);

                    this.tableLayoutPanel1.SetRowSpan(this.ultraDataChart1, 1);
                    this.tableLayoutPanel1.SetColumnSpan(this.ultraDataChart1, 3);

                    this.tableLayoutPanel1.SetRow(this.ultraPieChart1, 3);
                    this.tableLayoutPanel1.SetRowSpan(this.ultraPieChart1, 1);
                    this.tableLayoutPanel1.SetColumn(this.ultraPieChart1, 0);
                    this.tableLayoutPanel1.SetColumnSpan(this.ultraPieChart1, 3);

                    this.tableLayoutPanel1.SetRow(this.ultraDataChart3, 4);
                    this.tableLayoutPanel1.SetRowSpan(this.ultraDataChart3, 1);
                    this.tableLayoutPanel1.SetColumn(this.ultraDataChart3, 0);
                    this.tableLayoutPanel1.SetColumnSpan(this.ultraDataChart3, 3);

                    break;

                case OutlookCRM.Enums.PaneLayout.Bottom:
                    // Show the Chart portion.
                    this.splitContainer1.Panel2Collapsed = false;
                    // Change the orientation to simulate the layout change.
                    this.splitContainer1.Orientation = Orientation.Horizontal;

                    // some manipulate of the table layout panel to swap the charts around for a better view.
                    //this.tableLayoutPanel1.SetRowSpan(this.ultraChart1, 3);
                    //this.tableLayoutPanel1.SetColumnSpan(this.ultraChart1, 1);

                    //this.tableLayoutPanel1.SetRow(this.ultraChart2, 2);
                    //this.tableLayoutPanel1.SetRowSpan(this.ultraChart2, 3);
                    //this.tableLayoutPanel1.SetColumn(this.ultraChart2, 1);
                    //this.tableLayoutPanel1.SetColumnSpan(this.ultraChart2, 1);

                    //this.tableLayoutPanel1.SetRow(this.ultraChart3, 2);
                    //this.tableLayoutPanel1.SetRowSpan(this.ultraChart3, 3);
                    //this.tableLayoutPanel1.SetColumn(this.ultraChart3, 2);
                    //this.tableLayoutPanel1.SetColumnSpan(this.ultraChart3, 1);

                    this.tableLayoutPanel1.SetRowSpan(this.ultraDataChart1, 3);
                    this.tableLayoutPanel1.SetColumnSpan(this.ultraDataChart1, 1);

                    this.tableLayoutPanel1.SetRow(this.ultraPieChart1 , 2);
                    this.tableLayoutPanel1.SetRowSpan(this.ultraPieChart1, 3);
                    this.tableLayoutPanel1.SetColumn(this.ultraPieChart1, 1);
                    this.tableLayoutPanel1.SetColumnSpan(this.ultraPieChart1, 1);

                    this.tableLayoutPanel1.SetRow(this.ultraDataChart3, 2);
                    this.tableLayoutPanel1.SetRowSpan(this.ultraDataChart3, 3);
                    this.tableLayoutPanel1.SetColumn(this.ultraDataChart3, 2);
                    this.tableLayoutPanel1.SetColumnSpan(this.ultraDataChart3, 1);

                    break;
                case OutlookCRM.Enums.PaneLayout.Off:
                    // Hide the Chart portion.
                    this.splitContainer1.Panel2Collapsed = true;
                    break;
                default:
                    break;
            }
        }
        #endregion // SetPaneLayout

        #region GetStatusBarTextGrid
        public string GetStatusBarTextGrid()
        {
            // return the AutoStatus text for the status bar.
            switch (this.contentType)
            {
                case Enums.ContentType.Customers:
                    return Utilities.GetLocalizedString("Customers");
                    break;
                case Enums.ContentType.Orders:
                    return Utilities.GetLocalizedString("Orders");
                    break;
                case Enums.ContentType.OrderDetails:
                    return Properties.Resources.OrderDetails;
                    break;
                case Enums.ContentType.QuarterlyOrders:
                    return string.Empty;
                    break;
                case Enums.ContentType.ProductSales:
                    return Utilities.GetLocalizedString("Products");
                    break;
                case Enums.ContentType.SalesByCategory:
                    return Utilities.GetLocalizedString("Categories");
                    break;
                case Enums.ContentType.SalesByQuarter:
                    return string.Empty;
                    break;
                case Enums.ContentType.SalesByYear:
                    return string.Empty;
                    break;
                case Enums.ContentType.Shippers:
                    return Utilities.GetLocalizedString("Shippers");
                    break;
                case Enums.ContentType.Suppliers:
                    return string.Empty;
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
        #endregion // GetStatusBarTextGrid

        #region GetStatusBarTextChart1
        public string GetStatusBarTextChart1()
        {
            // return the AutoStatus text for the status bar.
            switch (this.contentType)
            {
                case Enums.ContentType.Customers:
                    return Utilities.GetLocalizedString("Customers");
                    break;
                case Enums.ContentType.Orders:
                    return Utilities.GetLocalizedString("Orders");
                    break;
                case Enums.ContentType.OrderDetails:
                    return Properties.Resources.OrderDetails;
                    break;
                case Enums.ContentType.QuarterlyOrders:
                    return string.Empty;
                    break;
                case Enums.ContentType.ProductSales:
                    return Utilities.GetLocalizedString("Products");
                    break;
                case Enums.ContentType.SalesByCategory:
                    return Utilities.GetLocalizedString("Categories");
                    break;
                case Enums.ContentType.SalesByQuarter:
                    return string.Empty;
                    break;
                case Enums.ContentType.SalesByYear:
                    return string.Empty;
                    break;
                case Enums.ContentType.Shippers:
                    return Utilities.GetLocalizedString("Shippers");
                    break;
                case Enums.ContentType.Suppliers:
                    return string.Empty;
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
        #endregion // GetStatusBarTextChart1

        #region GetStatusBarTextChart2
        public string GetStatusBarTextChart2()
        {
            // return the AutoStatus text for the status bar.
            switch (this.contentType)
            {
                case Enums.ContentType.Customers:
                    return Utilities.GetLocalizedString("Customers");
                    break;
                case Enums.ContentType.Orders:
                    return Utilities.GetLocalizedString("Orders");
                    break;
                case Enums.ContentType.OrderDetails:
                    return Properties.Resources.OrderDetails;
                    break;
                case Enums.ContentType.QuarterlyOrders:
                    return string.Empty;
                    break;
                case Enums.ContentType.ProductSales:
                    return Utilities.GetLocalizedString("Products");
                    break;
                case Enums.ContentType.SalesByCategory:
                    return Utilities.GetLocalizedString("Categories");
                    break;
                case Enums.ContentType.SalesByQuarter:
                    return string.Empty;
                    break;
                case Enums.ContentType.SalesByYear:
                    return string.Empty;
                    break;
                case Enums.ContentType.Shippers:
                    return Utilities.GetLocalizedString("Shippers");
                    break;
                case Enums.ContentType.Suppliers:
                    return string.Empty;
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
        #endregion // GetStatusBarTextChart2

        #region GetStatusBarTextChart3
        public string GetStatusBarTextChart3()
        {
            // return the AutoStatus text for the status bar.
            switch (this.contentType)
            {
                case Enums.ContentType.Customers:
                    return Utilities.GetLocalizedString("Customers");
                    break;
                case Enums.ContentType.Orders:
                    return Utilities.GetLocalizedString("Orders");
                    break;
                case Enums.ContentType.OrderDetails:
                    return Properties.Resources.OrderDetails;
                    break;
                case Enums.ContentType.QuarterlyOrders:
                    return string.Empty;
                    break;
                case Enums.ContentType.ProductSales:
                    return Utilities.GetLocalizedString("Products");
                    break;
                case Enums.ContentType.SalesByCategory:
                    return Utilities.GetLocalizedString("Categories");
                    break;
                case Enums.ContentType.SalesByQuarter:
                    return string.Empty;
                    break;
                case Enums.ContentType.SalesByYear:
                    return string.Empty;
                    break;
                case Enums.ContentType.Shippers:
                    return Utilities.GetLocalizedString("Shippers");
                    break;
                case Enums.ContentType.Suppliers:
                    return string.Empty;
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
        #endregion // GetStatusBarTextChart3

        #region AssignChartData
        private void AssignChartData()
        {
            DataTable dt= this.GetChartData();

            //UltraDataChart1
            ultraDataChart1.Series.Clear();

            if (ultraDataChart1.Axes.Count == 0)
            {
                var yAxis = new NumericYAxis() { MinimumValue = 0 };
                var xAxis = new CategoryXAxis()
                {
                    DataSource = dt,
                    Label = "Month"
                };

                this.ultraDataChart1.Axes.Add(xAxis);
                this.ultraDataChart1.Axes.Add(yAxis);

                CreateChart1Series(ultraDataChart1,xAxis, yAxis, dt);
            }
            else
            {
                var xAxis = this.ultraDataChart1.Axes.Where(x => x is CategoryXAxis).FirstOrDefault() as CategoryXAxis;
                xAxis.DataSource = dt;

                var yAxis = this.ultraDataChart1.Axes.Where(y => y is NumericYAxis).FirstOrDefault() as NumericYAxis;
                CreateChart1Series(ultraDataChart1,xAxis, yAxis, dt);
            }         

            this.ultraLegend1.Visible = false;
            this.ultraLegend1.BackColor = Color.Transparent;
            this.ultraLegend1.ForeColor = Color.White;
            this.ultraLegend1.Location = new Infragistics.Win.DataVisualization.Point(600, 0);
            this.ultraLegend1.Size = new System.Drawing.Size(150, 150);
            this.ultraLegend1.BorderStyle = BorderStyle.FixedSingle;
            this.ultraDataChart1.Legend = ultraLegend1;

            //Piechart

            ultraPieChart1.LabelMemberPath = "Col0";
            ultraPieChart1.ValueMemberPath = "Col0";
            ultraPieChart1.DataSource = dt;
           
            UltraItemLegend legend = new UltraItemLegend();
             //this.Controls.Add(legend);
            legend.Dock = DockStyle.Right;
            ultraPieChart1.Height = 500;
            ultraPieChart1.Legend = legend;
            ultraPieChart1.AllowSliceSelection = true;
            ultraPieChart1.AllowSliceExplosion = true;
            ultraPieChart1.SliceClick += pieChart_SliceClick;

            //ultraDataChart3

            ultraDataChart3.Series.Clear();

            if (ultraDataChart3.Axes.Count == 0)
            {
                var yAxis3 = new NumericYAxis();
                var xAxis3 = new CategoryXAxis()
                {
                    DataSource = dt,
                    Label = "Month"
                };
                this.ultraDataChart3.Axes.Add(xAxis3);
                this.ultraDataChart3.Axes.Add(yAxis3);

                CreateChart2Series(ultraDataChart3,xAxis3, yAxis3, dt);
            }
            else
            {
                var xAxis3 = this.ultraDataChart1.Axes.Where(x => x is CategoryXAxis).FirstOrDefault() as CategoryXAxis;
                xAxis3.DataSource = dt;

                var yAxis3 = this.ultraDataChart1.Axes.Where(y => y is NumericYAxis).FirstOrDefault() as NumericYAxis;
                CreateChart2Series(ultraDataChart3,xAxis3, yAxis3, dt);               
            }
        }

        void pieChart_SliceClick(object sender, SliceClickEventArgs e)
        {
            UltraPieChart pieChart = sender as UltraPieChart;
            e.IsExploded = !e.IsExploded;
        }
        #endregion // AssignChartData
        public void CreateChart1Series(UltraDataChart chart,CategoryXAxis xAxis, NumericYAxis yAxis, DataTable table)
        {
            for (int x = 1; x < 4; x++)
            {
                var series = new ColumnSeries()
                {
                    DataSource = table,//ds.Tables[0],
                    ValueMemberPath = table.Columns[x].ToString(),
                    XAxis = xAxis,
                    YAxis = yAxis,
                    Thickness = 3,
                };

                chart.Series.Add(series);
            }
        }
        public void CreateChart2Series(UltraDataChart chart, CategoryXAxis xAxis, NumericYAxis yAxis, DataTable table)
        {
            for (int x = 1; x < 4; x++)
            {
                var series = new AreaSeries()
                {
                    DataSource = table,
                    ValueMemberPath = table.Columns[x].ToString(),                    
                    XAxis = xAxis,
                    YAxis = yAxis,
                    Thickness = 3,
                };

                chart.Series.Add(series);
            }
        }
        
        public DataTable GetChartData()
         {
                DataTable dt = new DataTable();

                dt.Columns.Add("Month", typeof(string));
                dt.Columns.Add("Col0", typeof(int));
                dt.Columns.Add("Col1", typeof(int));
                dt.Columns.Add("Col2", typeof(int));

                int min = 0;
                int max = 300;
                for (int j = 0; j < 5; j++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = DateTime.Today.AddMonths(j);
                    row[1] = random.Next(min, max);
                    row[2] = random.Next(min, max);
                    row[3] = random.Next(min, max);

                    min = min + 100;
                    max = max + 200;

                    dt.Rows.Add(row);
                }
            return dt;
         }

        #endregion // Methods

    }
}