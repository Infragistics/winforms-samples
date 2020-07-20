using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraMessageBox;
using Infragistics.Win.UltraWinRadialMenu;
using System.Diagnostics;
using Infragistics.Win.Misc;

namespace Showcase.INGear
{
    public partial class MainForm : Form
    {
        #region Constants

        private const UI DefaultGridData = UI.Delivery;

        #endregion //Constants

        #region Members

        private UI currentGridData = MainForm.DefaultGridData;
        private bool menuInitialized;

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this.InitializeUI();
        }

        #endregion //Constructor

        #region Base Class Overrides

        #region OnLoad

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // set the ColorScheme to DarkGray so the single pixel form border is Grey.
            Infragistics.Win.Office2013ColorTable.ColorScheme = Infragistics.Win.Office2013ColorScheme.DarkGray;

            // align the contents in the FlowLayoutPanel
            int alignmentOffset = (this.flowLayoutPanel1.Height - this.lblCurrent.Height) / 2;
            this.lblPrevious.Margin = 
                this.lblCurrent.Margin = new Padding(0, alignmentOffset, 0, 0);

            // create the data and delivery appointments
            AutoPartsCatalog.Instance.BindDeliveries(this.ultraCalendarInfo1, this);
            this.ultraCalendarInfo1.CalendarInfoChanged += new CalendarInfoChangedEventHandler(ultraCalendarInfo1_CalendarInfoChanged);

            // assign the data to the grid.
            this.gridInventory.SyncWithCurrencyManager = false;
            this.gridInventory.DataSource = AutoPartsCatalog.Instance.DeliveriesTable;
            this.gridInventory.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand);
        }

        #endregion // OnLoad

        #region OnMove

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Move"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            // show/reposition the radial menu
            if (this.menuInitialized)
                this.ShowRadialMenu();
        }

        #endregion //OnMove

        #region OnShown

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Shown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);


            // show and position the radial menu
            this.menuInitialized = true;
            this.ShowRadialMenu();
        }

        #endregion //OnShown

        #region OnSizeChanged

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);


            // show/reposition the radial menu
            if (this.menuInitialized)
                this.ShowRadialMenu();
        }

        #endregion //OnSizeChanged

        #endregion //Base Class Overrides

        #region Event Handlers

        #region btnInventory_Click

        /// <summary>
        /// Handles the Click event of the btnInventory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnInventory_Click(object sender, EventArgs e)
        {
            this.SelectView(UI.Inventory);
        }

        #endregion //btnInventory_Click

        #region btnSchedule_Click

        /// <summary>
        /// Handles the Click event of the btnSchedule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSchedule_Click(object sender, EventArgs e)
        {
            this.SelectView(UI.Delivery);
        }

        #endregion // btnSchedule_Click

        #region dvSchedule_BeforeAppointmentEdited

        /// <summary>
        /// Handles the BeforeAppointmentEdited event of the dvSchedule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.CancelableAppointmentEventArgs"/> instance containing the event data.</param>
        private void dvSchedule_BeforeAppointmentEdited(object sender, CancelableAppointmentEventArgs e)
        {
            // cancel the event
            e.Cancel = true;
        }

        #endregion //dvSchedule_BeforeAppointmentEdited

        #region gridInventory_ClickCellButton

        /// <summary>
        /// Handles the ClickCellButton event of the gridInventory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void gridInventory_ClickCellButton(object sender, CellEventArgs e)
        {
            Appointment appointment = AutoPartsCatalog.Instance.GetDelivery((string)e.Cell.Row.Cells["PartNumber"].Value);
            if (appointment != null)
            {
                // activate the appointment and switch the view
                this.ultraCalendarInfo1.SelectedAppointments.Clear();
                appointment.Selected = true;
                this.ultraCalendarInfo1.ActivateDay(appointment.StartDateTime);
                this.SelectView(UI.Delivery);
            }
            else
            {
                // show the PlaceOrder control
                PlaceOrder order = new PlaceOrder(((DataRowView)e.Cell.Row.ListObject).Row, this.modalPanelManager1);
                this.modalPanelManager1.Show(this, order);
            }
        }

        #endregion // gridInventory_ClickCellButton

        #region gridInventory_ClickCell

        /// <summary>
        /// Handles the ClickCell event of the gridInventory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.ClickCellEventArgs"/> instance containing the event data.</param>
        private void gridInventory_ClickCell(object sender, ClickCellEventArgs e)
        {

            // if the initial delivery grid view currently being shown, show the shipping invoice control when any cell is clicked.
            if (this.currentGridData == UI.Delivery)
            {
                this.DisplayShippingInvoice((string)e.Cell.Row.Cells["DataKey"].Value);
                return;
            }
        }

        #endregion //gridInventory_ClickCell

        #region gridInventory_InitializeLayout

        /// <summary>
        /// Handles the InitializeLayout event of the gridInventory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void gridInventory_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            layout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            layout.Override.CellClickAction = CellClickAction.RowSelect;
            layout.LoadStyle = LoadStyle.LoadOnDemand;
            layout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            layout.Override.RowSizing = RowSizing.Fixed;
            layout.Override.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            layout.Override.CellAppearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            layout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
            layout.Override.CellSpacing = 3;
            UltraGridBand band = layout.Bands[0];
            if (band.Key == "Deliveries")
            {
                band.Columns["EndDateTime"].Hidden = true;
                band.Columns["AllDayEvent"].Hidden = true;
                band.Columns["Category"].Hidden = true;
                band.Columns["DataKey"].Header.Caption = Properties.Resources.PartNumber;
                band.Columns["Weight"].Header.Caption = Properties.Resources.TotalWeightLbs;
                band.Columns["Cost"].Header.Caption = Properties.Resources.TotalDue;
                band.Columns["Subject"].Header.Caption = Properties.Resources.Subject;
                band.Columns["Description"].Header.Caption = Properties.Resources.Description;
                band.Columns["Count"].Header.Caption = Properties.Resources.Count;

                UltraGridColumn column = band.Columns["StartDateTime"];
                column.Header.Caption = Properties.Resources.DeliveryDate;
                column.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateWithoutDropDown;
                band.SortedColumns.Add(column, false);
            }
            else
            {
                UltraGridColumn column = band.Columns["WeightPerItem"];
                column.Header.Caption = Properties.Resources.WeightLbs;
                column.Format = "F1";

                column = band.Columns["PricePerItem"];
                column.Header.Caption = Properties.Resources.PricePerItem;
                column.Format = "C";

                band.Columns["PartNumber"].Header.Caption = Properties.Resources.PartNumber;
                band.Columns["InStock"].Header.Caption = Properties.Resources.InStock;
                band.Columns["Manufacturer"].Header.Caption = Properties.Resources.Manufacturer;
                band.Columns["Component"].Header.Caption = Properties.Resources.Component;
                
                // Add a column for the Orders ("Order Part", "View Shipment") button
                if (band.Columns.IndexOf("Orders") == -1)
                {
                    UltraGridColumn deliveries = band.Columns.Add("Orders");
                    deliveries.Header.Caption = Properties.Resources.Availability;
                    deliveries.DataType = typeof(string);
                    deliveries.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
                    deliveries.ButtonDisplayStyle = ButtonDisplayStyle.Always;
                }
            }
        }

        #endregion //gridInventory_InitializeLayout

        #region gridInventory_InitializeRow

        /// <summary>
        /// Handles the InitializeRow event of the gridInventory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void gridInventory_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.IsDataRow)
            {
                if (e.Row.Band.Key != "Deliveries")
                {
                    // update the cell for the Orders column based on the existance of a pending shipment

                    UltraGridCell cell = e.Row.Cells["Orders"];
                    if (AutoPartsCatalog.Instance.GetDelivery((string)e.Row.Cells["PartNumber"].Value) != null)
                        cell.Value = Properties.Resources.ViewShipment;
                    else
                        cell.Value = Properties.Resources.OrderPart;
                }
            }
        }

        #endregion // gridInventory_InitializeRow

        #region modalPanelManager1_AfterClosed

        /// <summary>
        /// Handles the AfterClosed event of the modalPanelManager1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void modalPanelManager1_AfterClosed(object sender, EventArgs e)
        {
            // Change the FormDisplayStyle base to sizable, and show the radial menu.
            this.ultraToolbarsManager1.FormDisplayStyle = Infragistics.Win.UltraWinToolbars.FormDisplayStyle.RoundedSizable;
            this.ShowRadialMenu();
        }

        #endregion //modalPanelManager1_AfterClosed

        #region modalPanelManager1_AfterShown

        /// <summary>
        /// Handles the AfterShown event of the modalPanelManager1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void modalPanelManager1_AfterShown(object sender, EventArgs e)
        {
        }

        #endregion //modalPanelManager1_AfterShown

        #region modalPanelManager1_BeforeShown

        /// <summary>
        /// Handles the BeforeShown event of the modalPanelManager1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void modalPanelManager1_BeforeShown(object sender, EventArgs e)
        {
            // change the FormDisplayStyle to Fixed, to prevent the user from resizing the form while the modal panel is open.
            this.ultraToolbarsManager1.FormDisplayStyle = Infragistics.Win.UltraWinToolbars.FormDisplayStyle.RoundedFixed;

            // hide the radial menu
            this.ultraRadialMenu1.Hide();
        }

        #endregion // modalPanelManager1_BeforeShown

        #region schedule_AppointmentsDragging

        /// <summary>
        /// Handles the AppointmentsDragging event of the schedule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.AppointmentsDraggingEventArgs"/> instance containing the event data.</param>
        private void schedule_AppointmentsDragging(object sender, AppointmentsDraggingEventArgs e)
        {
            // Cancel the event
            e.Cancel = true;
        }

        #endregion // schedule_AppointmentsDragging

        #region schedule_AppointmentResizing

        /// <summary>
        /// Handles the AppointmentResizing event of the schedule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.AppointmentResizingEventArgs"/> instance containing the event data.</param>
        private void schedule_AppointmentResizing(object sender, AppointmentResizingEventArgs e)
        {
            // Cancel the event
            e.Cancel = true;
        }

        #endregion // schedule_AppointmentResizing

        #region schedule_BeforeAppointmentEdit

        /// <summary>
        /// Handles the BeforeAppointmentEdit event of the schedule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.BeforeAppointmentEditEventArgs"/> instance containing the event data.</param>
        private void schedule_BeforeAppointmentEdit(object sender, BeforeAppointmentEditEventArgs e)
        {
            // Cancel the event
            e.Cancel = true;
        }

        #endregion // schedule_BeforeAppointmentEdit

        #region schedule_MoreActivityIndicatorClicked

        /// <summary>
        /// Handles the MoreActivityIndicatorClicked event of the schedule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.MoreActivityIndicatorClickedEventArgs"/> instance containing the event data.</param>
        private void schedule_MoreActivityIndicatorClicked(object sender, MoreActivityIndicatorClickedEventArgs e)
        {
            this.ActivateDay(e.Day.Date);
            this.tcSchedule.SelectedTab = this.tcSchedule.Tabs["Daily"];
        }

        #endregion //schedule_MoreActivityIndicatorClicked

        #region ultraCalendarInfo1_AfterSelectedAppointmentsChange

        /// <summary>
        /// Handles the AfterSelectedAppointmentsChange event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ultraCalendarInfo1_AfterSelectedAppointmentsChange(object sender, EventArgs e)
        {
            // Whenever an appointment is selected, activate the day of the StartDate.
            if (this.ultraCalendarInfo1.SelectedAppointments.Count > 0)
            {
                this.ActivateDay(this.ultraCalendarInfo1.SelectedAppointments[0].StartDateTime);
            }
        }

        #endregion //ultraCalendarInfo1_AfterSelectedAppointmentsChange

        #region ultraCalendarInfo1_BeforeDisplayAppointmentDialog

        /// <summary>
        /// Handles the BeforeDisplayAppointmentDialog event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.DisplayAppointmentDialogEventArgs"/> instance containing the event data.</param>
        private void ultraCalendarInfo1_BeforeDisplayAppointmentDialog(object sender, DisplayAppointmentDialogEventArgs e)
        {
            // cancel the displaying of the default AppointmentDialog
            e.Cancel = true;


            // display the Shipping Invoice control
            this.DisplayShippingInvoice((string)e.Appointment.DataKey);
        }

        #endregion //ultraCalendarInfo1_BeforeDisplayAppointmentDialog

        #region ultraCalendarInfo1_CalendarInfoChanged

        /// <summary>
        /// Handles the CalendarInfoChanged event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.CalendarInfoChangedEventArgs"/> instance containing the event data.</param>
        void ultraCalendarInfo1_CalendarInfoChanged(object sender, CalendarInfoChangedEventArgs e)
        {

            // If the Appointments collection has changed, rebind the UltraCalendarInfo to the Deliveries table (so we rebuild the PartNumber/Appointment table)
            if (e.PropChangeInfo.PropId is Infragistics.Win.UltraWinSchedule.PropertyIds &&
                ((Infragistics.Win.UltraWinSchedule.PropertyIds)e.PropChangeInfo.PropId) == Infragistics.Win.UltraWinSchedule.PropertyIds.Appointments)
            {
                AutoPartsCatalog.Instance.BindDeliveries(this.ultraCalendarInfo1, this);
            }
        }

        #endregion //ultraCalendarInfo1_CalendarInfoChanged

        #region ultraRadialMenu1_CenterButtonClick

        /// <summary>
        /// Handles the CenterButtonClick event of the ultraRadialMenu1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinRadialMenu.CenterButtonClickEventArgs"/> instance containing the event data.</param>
        private void ultraRadialMenu1_CenterButtonClick(object sender, Infragistics.Win.UltraWinRadialMenu.CenterButtonClickEventArgs e)
        {    
            // when the CenterButtonTool is clicked, make sure the Inventory grid is being shown.
            if (this.ultraTabControl1.SelectedTab != this.ultraTabControl1.Tabs["SearchGrid"])
            {
                this.ultraRadialMenu1.CenterTool = this.ultraRadialMenu1.FindRootTool();
                this.SelectView(UI.Inventory,  true);
            }
        }

        #endregion // ultraRadialMenu1_CenterButtonClick

        #region ultraRadialMenu1_DrillDownButtonClick

        /// <summary>
        /// Handles the DrillDownButtonClick event of the ultraRadialMenu1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinRadialMenu.RadialMenuToolDrillDownButtonClickEventArgs"/> instance containing the event data.</param>
        private void ultraRadialMenu1_DrillDownButtonClick(object sender, RadialMenuToolDrillDownButtonClickEventArgs e)
        {
            // filter the grid based on the tool being "expanded".
            this.FilterGridBasedOnTool(e.Tool);
        }

        #endregion // ultraRadialMenu1_DrillDownButtonClick

        #region ultraRadialMenu1_PropertyChanged

        /// <summary>
        /// Handles the PropertyChanged event of the ultraRadialMenu1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ultraRadialMenu1_PropertyChanged(object sender, Infragistics.Win.PropertyChangedEventArgs e)
        {
            if (e.ChangeInfo.PropId is Infragistics.Win.UltraWinRadialMenu.PropertyIds)
            {
                switch ((Infragistics.Win.UltraWinRadialMenu.PropertyIds)e.ChangeInfo.PropId)
                {
                    // listen for the Expanded property changed notification, and reposition the radial menu accordingly.
                    case Infragistics.Win.UltraWinRadialMenu.PropertyIds.Expanded:
                        this.ShowRadialMenu();
                        break;
                }
            }
        }

        #endregion // ultraRadialMenu1_PropertyChanged

        #region ultraRadialMenu1_ToolClick

        /// <summary>
        /// Handles the ToolClick event of the ultraRadialMenu1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinRadialMenu.RadialMenuToolClickEventArgs"/> instance containing the event data.</param>
        private void ultraRadialMenu1_ToolClick(object sender, RadialMenuToolClickEventArgs e)
        {
            // filter the grid based on the clicked tool
            this.FilterGridBasedOnTool(e.Tool);
        }

        #endregion // ultraRadialMenu1_ToolClick

        #endregion //Event Handler

        #region Methods

        #region ActivateDay

        /// <summary>
        /// Activates the specified date in the scheduling controls.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        private void ActivateDay(DateTime dateTime)
        {
            this.ultraCalendarInfo1.SelectedDateRanges.Clear();
            this.ultraCalendarInfo1.ActivateDay(dateTime);
        }

        #endregion //ActivateDay

        #region ChangeHeader

        /// <summary>
        /// Changes the header label based on the provided Category and SubCategory.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="subCategory">The sub category.</param>
        private void ChangeHeader(CategoryType category, string subCategory)
        {
            if (string.IsNullOrEmpty(subCategory) == false)
            {
                this.lblCurrent.Text = Utilities.LocalizeString(subCategory);
                this.lblPrevious.Text = string.Format("{0} /", Utilities.CategoryTypeToString(category));
            }
            else
            {
                this.lblCurrent.Text = string.Format("{0}", Utilities.CategoryTypeToString(category));
                this.lblPrevious.Text = string.Empty;
            }
        }

        #endregion //ChangeHeader

        #region DisplayShippingInvoice

        /// <summary>
        /// Displays the shipping invoice.
        /// </summary>
        /// <param name="partNumber">The part number.</param>
        private void DisplayShippingInvoice(string partNumber)
        {
            DataRow deliveryData = AutoPartsCatalog.Instance.GetDataRowFromDelivery(partNumber);
            if (deliveryData == null)
                return;

            DataRow partData = AutoPartsCatalog.Instance.GetPartDataRowFromPartNumber(partNumber);

            if (partData == null)
                return;

            ShipmentInformation info = new ShipmentInformation(deliveryData, partData);
            info.Text = ((DateTime)deliveryData["StartDateTime"]).ToShortDateString();
            this.modalPanelManager1.Show(this, info, true, true);
        }

        #endregion //DisplayShippingInvoice

        #region FilterGridBasedOnTool

        /// <summary>
        /// Filters the grid based on tool.
        /// </summary>
        /// <param name="tool">The tool.</param>
        private void FilterGridBasedOnTool(RadialMenuTool tool)
        {
            string key = tool.Key;
            CategoryType category;
            try
            {
                // get the CategoryType from the Tag
                if (tool.Tag is CategoryType)
                {
                    category = (CategoryType)tool.Tag;

                    // if the currentGridData is still the defualt data, bind the Inventory data to the grid
                    if (this.currentGridData == MainForm.DefaultGridData)
                    {
                        this.gridInventory.DataSource = AutoPartsCatalog.Instance.InventoryTable;
                        this.currentGridData = UI.Inventory;
                    }

                    // apply a filter to the Category column based on the CategoryType
                    UltraGridBand band = this.gridInventory.DisplayLayout.Bands["Inventory"];
                    ColumnFilter filter = band.ColumnFilters["Category"];
                    if (filter.FilterConditions.Count > 0)
                        filter.FilterConditions[0].CompareValue = (int)category;
                    else
                        filter.FilterConditions.Add(FilterComparisionOperator.Equals, (int)category);

                    // add secondary filter to the Components column
                    filter = band.ColumnFilters["Component"];
                    if (string.IsNullOrEmpty(key) == false)
                    {
                        if (filter.FilterConditions.Count > 0)
                            filter.FilterConditions[0].CompareValue = key;
                        else
                            filter.FilterConditions.Add(FilterComparisionOperator.Equals, key);
                    }
                    else
                        filter.ClearFilterConditions();

                    this.ChangeHeader(category, key);
                }
            }
            finally
            {
                this.gridInventory.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.VisibleRows);
            }
        }

        #endregion FilterGridBasedOnTool

        #region InitializeUI

        /// <summary>
        /// Initializes the UI controls.
        /// </summary>
        private void InitializeUI()
        {
            // Assign the images 
            Image background = Utilities.GetCachedImage(CachedImages.Background);
            this.lblLogo.Appearance.ImageBackground = background;
            this.ultraToolbarsManager1.Ribbon.CaptionAreaAppearance.ImageBackground = background;
            this.MainForm_Fill_Panel.Appearance.ImageBackground = background;
            this.tcSchedule.TabHeaderAreaAppearance.ImageBackground = background;
            this.gridInventory.DisplayLayout.Appearance.ImageBackground = background;
            this.dvSchedule.Appearance.ImageBackground = background;
            this.wvSchedule.Appearance.ImageBackground = background;
            this.mvSchedule.Appearance.ImageBackground = background;
            this.ultraPanel1.Appearance.ImageBackground = background;
            this.lblLogo.Appearance.ImageBackground = Utilities.GetCachedImage(CachedImages.Logo);
            this.btnInventory.Appearance.Image = Utilities.GetCachedImage(CachedImages.Inventory);
            this.btnSchedule.Appearance.Image = Utilities.GetCachedImage(CachedImages.Schedule);
            this.tcSchedule.Tabs["Daily"].Appearance.Image = Utilities.GetCachedImage(CachedImages.Daily);
            this.tcSchedule.Tabs["Weekly"].Appearance.Image = Utilities.GetCachedImage(CachedImages.Weekly);
            this.tcSchedule.Tabs["Monthly"].Appearance.Image = Utilities.GetCachedImage(CachedImages.Monthly);
            this.ultraRadialMenu1.ToolSettings.CenterButtonAppearance.Image = Utilities.GetCachedImage(CachedImages.RadialCenter);

            // assign UIElement filters
            this.ultraToolbarsManager1.CreationFilter = new Showcase.INGear.ToolbarsManagerUIElementFilter();
            this.dvSchedule.CreationFilter = new Showcase.INGear.DayViewUIElementFilter();

            // create the radial menu tools
            foreach (CategoryType category in Enum.GetValues(typeof(CategoryType)))
            {
                RadialMenuTool tool = new RadialMenuTool();
                tool.Tag = category;
                tool.ToolTipText = Utilities.LocalizeString(Utilities.CategoryTypeToString(category));
                tool.ToolSettings.Appearance.Image = Utilities.GetCachedImage(Utilities.CachedImagesFromCategory(category));
                foreach (string sub in AutoPartsCatalog.GetSubCategories(category))
                {
                    RadialMenuTool childTool = new RadialMenuTool(sub);
                    childTool.Tag = category;
                    childTool.Text = Utilities.LocalizeString(sub);
                    tool.Tools.Add(childTool);
                }
                this.ultraRadialMenu1.CenterTool.Tools.Add(tool);
            }

            this.LocalizeStrings();
        }

        #endregion //InitializeUI

        #region LocalizeStrings

        /// <summary>
        /// Localizes the strings.
        /// </summary>
        private void LocalizeStrings()
        {
            this.lblPrevious.Text = string.Empty;
            this.lblCurrent.Text = Properties.Resources.PendingDeliveries;
            this.lblStart.Text = Properties.Resources.ExpandInventoryFilter;
        }

        #endregion //LocalizeStrings

        #region SelectView

        /// <summary>
        /// Changed the view between Grid and Schedule view
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="skipExpansion">if set to <c>true</c> [skip expansion].</param>
        private void SelectView(UI view, bool skipExpansion = false)
        {
            switch (view)
            {
                case UI.Inventory:
                    this.ultraTabControl1.SelectedTab = this.ultraTabControl1.Tabs["SearchGrid"];
                    if (skipExpansion == false)
                        this.ultraRadialMenu1.Expanded = true;
                    break;
                case UI.Delivery:
                    this.ultraTabControl1.SelectedTab = this.ultraTabControl1.Tabs["DeliverySchedule"];
                    this.ultraRadialMenu1.Expanded = false;
                    break;
            }
            this.UpdateHeader();
            this.ShowRadialMenu();
        }

        #endregion //SelectView

        #region ShowRadialMenu

        /// <summary>
        /// Shows the radial menu.
        /// </summary>
        private void ShowRadialMenu()
        {
            // hide the radial menu if the form is minimized
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ultraRadialMenu1.Hide();
                return;
            }

            Rectangle formBounds = this.Bounds;

            Point location;

            // if the menu is expanded, shift it in so the whole menu appears within the client area of the form.
            // if it is collapsed, push the center button closer to the form edge
            if (this.ultraRadialMenu1.Expanded)
            {
                Size menuSize = this.ultraRadialMenu1.MenuSettings.Size;
                int offset = 20;
                location = new Point(formBounds.Right - menuSize.Width - offset, formBounds.Bottom - menuSize.Height - offset);
                this.lblStart.Visible = false;
            }
            else
            {
                Size menuSize = this.ultraRadialMenu1.MenuSettings.Size;
                Size centerButtonSize = this.ultraRadialMenu1.MenuSettings.CenterButtonImageSize;
                int offset = (int)(this.ultraRadialMenu1.MenuSettings.OuterRingThickness * 1.5);
                location = new Point(formBounds.Right - (menuSize.Width - centerButtonSize.Width - offset), formBounds.Bottom - (menuSize.Height - centerButtonSize.Height - offset));

                this.lblStart.Visible = (this.ultraTabControl1.SelectedTab.Key == "SearchGrid");
            }
            this.ultraRadialMenu1.Show(this, location);
        }

        #endregion //ShowRadialMenu

        #region UpdateHeader

        /// <summary>
        /// Updates the header.
        /// </summary>
        private void UpdateHeader()
        {
            switch (this.ultraTabControl1.SelectedTab.Key)
            {
                case "SearchGrid":

                    if (this.currentGridData == UI.Delivery)
                    {
                        this.lblCurrent.Text = Properties.Resources.PendingDeliveries;
                        this.lblPrevious.Text = string.Empty;
                        return;
                    }

                    CategoryType category;
                    UltraGridBand band = this.gridInventory.DisplayLayout.Bands["Inventory"];
                    ColumnFilter filter = band.ColumnFilters["Category"];
                    if (filter.FilterConditions.Count > 0)
                    {
                        category = (CategoryType)filter.FilterConditions[0].CompareValue;
                        string subCategory = string.Empty;
                        filter = band.ColumnFilters["Component"];
                        if (filter.FilterConditions.Count > 0)
                            subCategory = (string)filter.FilterConditions[0].CompareValue;

                        this.ChangeHeader(category, subCategory);
                    }
                    else
                    {
                        // shouldn't get here
                        Debug.Fail("");
                        this.lblCurrent.Text = string.Empty;
                        this.lblPrevious.Text = string.Empty;
                    }

                    break;
                case "DeliverySchedule":
                    this.lblCurrent.Text = Properties.Resources.Deliveries;
                    this.lblPrevious.Text = string.Empty;
                    break;
            }
        }

        #endregion //UpdateHeader

        #endregion //Methods

    }
}
