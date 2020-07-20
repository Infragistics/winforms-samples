using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinListView;
using Infragistics.Win.UltraWinSchedule;
using System.Resources;
using Infragistics.Win.DataVisualization;

namespace Showcase.InventoryManagement
{
    public partial class InventoryManagementForm : Form
    {
        #region Members

        private bool dateSelectionRecursionFlag = false;
        private bool timeSlotSelectionRecursionFlag = false;

        #endregion //Members

        #region Constructor

        public InventoryManagementForm()
        {
            InitializeComponent();
        }

        #endregion // Constructor

        #region Base Class Overrides

        #region OnLoad

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                Infragistics.Win.Office2013ColorTable.ColorScheme = Infragistics.Win.Office2013ColorScheme.DarkGray;

                // Hook up UIElement filters
                this.tcMain.DrawFilter =
                    this.tcCustomers.DrawFilter =
                    this.tcInventory.DrawFilter =
                    this.tcOrders.DrawFilter =
                    this.gridContacts.DrawFilter =
                    this.gridInventory.DrawFilter =
                    this.gridReports.DrawFilter =
                    this.lvNewProducts.DrawFilter = new NoFocusRectDrawFilter();

                DayViewUIFilter dvFilter = new DayViewUIFilter();
                this.dvDaily.CreationFilter =
                    this.dvWeekly.CreationFilter = dvFilter;
                this.dvDaily.DrawFilter =
                    this.dvWeekly.DrawFilter = dvFilter;

                MonthViewUIFilter mvFilter = new MonthViewUIFilter();
                this.mvMonthly.CreationFilter = mvFilter;
                this.mvMonthly.DrawFilter = mvFilter;

                this.gridContacts.DrawFilter =
                    this.gridInventory.DrawFilter =
                    this.gridReports.DrawFilter = new GridUIFilter();

                // Bind to the data
                this.gridContacts.DataSource = InventoryDataSource.GetTable(Table.Contacts);

                DataTable inventoryTable = InventoryDataSource.GetTable(Table.Inventory);
                this.gridInventory.DataSource = inventoryTable;
                this.gridReports.DataSource = inventoryTable;

                var newProducts = from p in inventoryTable.AsEnumerable()
                                  where p.Field<bool>("IsNew")
                                  select new
                                  {
                                      Name = p.Field<string>("Name"),
                                      Category = p.Field<string>("Category")
                                  };
                foreach (var newProduct in newProducts)
                {
                    this.lvNewProducts.Items.Add(new UltraListViewItem(newProduct.Name,
                                                    new UltraListViewSubItem[] { 
                                                    new UltraListViewSubItem(newProduct.Category, null)
                                                },
                                                    null));
                }

                // Set up the schedule controls on the Orders tab
                this.SetAppointmentBindings(InventoryDataSource.GetTable(Table.Orders));
                int yearNum;
                int weekNum = this.ultraCalendarInfo1.GetWeekNumberForDate(DateTime.Today, out yearNum);
                this.SelectWorkWeek(weekNum, yearNum);
                this.ultraCalendarInfo1.BeforeAppointmentAdded += new CancelableAppointmentEventHandler(ultraCalendarInfo1_BeforeAppointmentAdded);

                this.llDateNavigation.DrawFilter = new EditorButtonUIFilter();

                // Assign the sample chart data
                DataTable chartData = InventoryDataSource.GetTable(Table.ChartData1);
                               
                //chart1
                var yAxis = new NumericYAxis();
                var xAxis = new CategoryXAxis()
                {
                    DataSource = chartData,
                    Label = "Month"                   
                };
                xAxis.FormatLabel += XAxis_FormatLabel;

                foreach (string category in new string[] { Properties.Resources.Sales, Properties.Resources.Profit, Properties.Resources.Cost })
                {
                    var series = new LineSeries()
                    {
                        DataSource = chartData,
                        ValueMemberPath = category,                        
                        XAxis = xAxis,
                        YAxis = yAxis,
                        Thickness = 3
                    };
                    Datacharthome.Axes.Add(xAxis);
                    Datacharthome.Axes.Add(yAxis);
                    Datacharthome.Series.Add(series);
                }               

                //2nd chart
                
                var yAxis2 = new NumericYAxis();
                var xAxis2 = new CategoryXAxis()
                {
                    DataSource = chartData,
                    Label = "Month"
                };

                xAxis2.FormatLabel += XAxis_FormatLabel;

                foreach (string category in new string[] { Properties.Resources.Sales, Properties.Resources.Profit, Properties.Resources.Cost })
                {
                    var series2 = new LineSeries()
                    {
                        DataSource = chartData,
                        ValueMemberPath = category,
                        XAxis = xAxis2,
                        YAxis = yAxis2,
                        Thickness = 3
                    };
                    datachartReportsStock.Axes.Add(xAxis2);
                    datachartReportsStock.Axes.Add(yAxis2);
                    datachartReportsStock.Series.Add(series2);
                }
                //3rd chart

                DataTable chartData2 = InventoryDataSource.GetTable(Table.ChartData2);

                var yAxis3 = new NumericYAxis();
                var xAxis3 = new CategoryXAxis()
                {
                    DataSource = chartData2,
                    Label = "Month"
                };

                for (int i = 1; i <= 4; i++)
                {
                    var series3 = new ColumnSeries()
                    {
                        DataSource = chartData2,
                        ValueMemberPath = chartData2.Columns[i].ToString(),
                        XAxis = xAxis3,
                        YAxis = yAxis3,
                        Thickness = 3
                    };
                    datachartReportsSales.Axes.Add(xAxis3);
                    datachartReportsSales.Axes.Add(yAxis3);
                    datachartReportsSales.Series.Add(series3);
                }
                //end

            }
        }

        private string XAxis_FormatLabel(AxisLabelInfo info)
        {
            // throw new NotImplementedException();
            return string.Format("{0:dd/MM}", info.DateValue);

        }

        #endregion // OnLoad

        #endregion // Base Class Overrides

        #region Event Handlers

        #region CategoryButton_Click

        /// <summary>
        /// Handles the Click event of the Category buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            UltraButton button = sender as UltraButton;
            if (button != null)
            {
                // Activate the tab that corresponds to the clicked button.
                tcMain.SelectedTab = tcMain.Tabs[button.Tag as string];                
            }
        }

        #endregion // CategoryButton_Click

        #region dvWeekly_AfterTimeSlotSelectionChanged

        /// <summary>
        /// Handles the AfterTimeSlotSelectionChanged event of the dvWeekly control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dvWeekly_AfterTimeSlotSelectionChanged(object sender, EventArgs e)
        {
            if (this.ultraCalendarInfo1.AlternateSelectedDateRanges.SelectedDaysCount > 0)
            {
                this.timeSlotSelectionRecursionFlag = true;
                try
                {
                    // Make sure the selected timeslot is visible within the visible days.
                    while (this.ultraCalendarInfo1.AlternateSelectedDateRanges[0].StartDate > this.dvWeekly.SelectedTimeSlotRange.StartDateTime.Date)
                    {
                        this.dvWeekly.PerformAction(UltraDayViewAction.SameTimeSlotNextDay);
                    }

                    while (this.ultraCalendarInfo1.AlternateSelectedDateRanges[0].EndDate < this.dvWeekly.SelectedTimeSlotRange.EndDateTime.Date)
                    {
                        this.dvWeekly.PerformAction(UltraDayViewAction.SameTimeSlotPreviousDay);
                    }
                }
                finally
                {
                    this.timeSlotSelectionRecursionFlag = false;
                }
            }

            // Keep the selected date in synch with the selected timeslot
            if (!this.dateSelectionRecursionFlag)
                this.ultraCalendarInfo1.SelectedDateRanges.Add(this.dvWeekly.SelectedTimeSlotRange.StartDateTime, true);
        }

        #endregion //dvWeekly_AfterTimeSlotSelectionChanged

        #region Grid_AfterRowActivate

        /// <summary>
        /// Handles the AfterRowActivate event of the UltraGrid controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Grid_AfterRowActivate(object sender, EventArgs e)
        {
            // Keep the SelectedRow and ActiveRow synchronized
            UltraGrid grid = (UltraGrid)sender;
            grid.Selected.Rows.Clear();
            grid.ActiveRow.Selected = true;
        }

        #endregion //Grid_AfterRowActivate

        #region Grid_InitializeLayout

        /// <summary>
        /// Handles the InitializeLayout event of the UltraGrid controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void Grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Override.DefaultRowHeight = 30;

            ColumnsCollection columns = e.Layout.Bands[0].Columns;
            foreach (UltraGridColumn column in columns)
            {

                column.Header.Caption = GetLocalizedString(column.Header.Caption);

                switch (column.Key)
                {
                    case "Cost":
                    case "Price":
                    case "Sales":
                    case "Profit":
                        {
                            column.Format = "c";
                            column.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CurrencyNonNegative;
                        }
                        break;
                }
            }

            // Hide the unnecessary columns
            if (sender == this.gridInventory)
            {
                columns["Sales"].Hidden = true;
                columns["Profit"].Hidden = true;
                columns["IsNew"].Hidden = true;
            }
            else if (sender == this.gridReports)
            {
                columns["Quantity"].Hidden = true;
                columns["IsNew"].Hidden = true;
            }
        }

        #endregion //Grid_InitializeLayout

        #region llDateNavigation_EditorButtonClick

        /// <summary>
        /// Handles the EditorButtonClick event of the llDateNavigation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinEditors.EditorButtonEventArgs"/> instance containing the event data.</param>
        private void llDateNavigation_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {            
            this.NavigateSchedule(this.tcOrders.SelectedTab.Key, (e.Button.Key == "Next"));
        }

        #endregion //llDateNavigation_EditorButtonClick

        #region mvMonthly_MoreActivityIndicatorClicked
        /// <summary>
        /// Handles the MoreActivityIndicatorClicked event of the mvMonthly control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MoreActivityIndicatorClickedEventArgs"/> instance containing the event data.</param>
        private void mvMonthly_MoreActivityIndicatorClicked(object sender, MoreActivityIndicatorClickedEventArgs e)
        {
            // Activate the day and switch to the Daily view.
            this.ultraCalendarInfo1.ActiveDay = e.Day;
            this.ultraCalendarInfo1.SelectedDateRanges.Add(this.ultraCalendarInfo1.ActiveDay.Date, true);
            this.tcOrders.SelectedTab = this.tcOrders.Tabs["Daily"];
        }

        #endregion //mvMonthly_MoreActivityIndicatorClicked

        #region scHome1_Panel1_SizeChanged

        /// <summary>
        /// Handles the SizeChanged event of the scHome1_Panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void scHome1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            // when the panel's size changes we should resize the child TableLayoutPanel and maintain it's size ratio
            Size panelSize = this.scHome1.Panel1.Size;
            int extent = Math.Min(panelSize.Width - this.tableLayoutPanel1.Left, panelSize.Height - this.tableLayoutPanel1.Top);
            this.tableLayoutPanel1.Size = new Size(extent, extent);

            this.scHome2.SplitterDistance = extent / 2;
        }

        #endregion //scHome1_Panel1_SizeChanged

        #region tcOrders_SelectedTabChanged

        /// <summary>
        /// Handles the SelectedTabChanged event of the tcOrders control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs"/> instance containing the event data.</param>
        private void tcOrders_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            switch (tcOrders.SelectedTab.Key)
            {
                case "Daily":
                    this.lblSchedule.Text = Properties.Resources.DailyOrdersSchedule;
                    break;
                case "Weekly":
                    this.lblSchedule.Text = Properties.Resources.WeeklyOrdersSchedule;
                    break;
                case "Monthly":
                    this.lblSchedule.Text = Properties.Resources.MonthlyOrdersSchedule;
                    break;
            }

            this.SetScheduleHeader();
        }

        #endregion //tcOrders_SelectedTabChanged

        #region tableLayoutPanel3_SizeChanged

        /// <summary>
        /// Handles the SizeChanged event of the tableLayoutPanel3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tableLayoutPanel3_SizeChanged(object sender, EventArgs e)
        {

            float fontSize1 = 11;
            float fontSize2 = 17;

            if (this.tableLayoutPanel3.Width < 380)
            {
                fontSize1 = 8;
                fontSize2 = 12;
            }
            else if (this.tableLayoutPanel3.Width < 450)
            {
                fontSize1 = 10;
                fontSize2 = 14;
            }

            if (this.lblSold.Appearance.FontData.SizeInPoints != fontSize1)
            {
                    this.lblSold.Appearance.FontData.SizeInPoints = fontSize1;
                    this.lblQuantity.Appearance.FontData.SizeInPoints = fontSize1;
                    this.lblRevenue.Appearance.FontData.SizeInPoints = fontSize1;
                    this.lblProfit.Appearance.FontData.SizeInPoints = fontSize1;
                    this.lblSoldValue.Appearance.FontData.SizeInPoints = fontSize2;
                    this.lblQuantityValue.Appearance.FontData.SizeInPoints = fontSize2;
                    this.lblRevenueValue.Appearance.FontData.SizeInPoints = fontSize2;
                    this.lblProfitValue.Appearance.FontData.SizeInPoints = fontSize2;
            }
        }

        #endregion //tableLayoutPanel3_SizeChanged

        #region ultraCalendarInfo1_AfterActiveDayChanged

        /// <summary>
        /// Handles the AfterActiveDayChanged event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AfterActiveDayChangedEventArgs"/> instance containing the event data.</param>
        private void ultraCalendarInfo1_AfterActiveDayChanged(object sender, AfterActiveDayChangedEventArgs e)
        {
            this.SetScheduleHeader();
        }

        #endregion // ultraCalendarInfo1_AfterActiveDayChanged

        #region ultraCalendarInfo1_AfterSelectedDateRangeChange

        /// <summary>
        /// Handles the AfterSelectedDateRangeChange event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ultraCalendarInfo1_AfterSelectedDateRangeChange(object sender, EventArgs e)
        {
            if (this.dateSelectionRecursionFlag)
                return;

            // If the selected days change, make sure the weekly DayView is showing the correct days.
            if (this.ultraCalendarInfo1.SelectedDateRanges.SelectedDaysCount > 0)
            {
                Week week = this.ultraCalendarInfo1.SelectedDateRanges[0].FirstDay.Week;
                this.SelectWorkWeek(week.WeekNumber, week.Year.YearNumber);
            }
        }

        #endregion //ultraCalendarInfo1_AfterSelectedDateRangeChange

        #region ultraCalendarInfo1_AppointmentDataInitialized

        /// <summary>
        /// Handles the AppointmentDataInitialized event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.AppointmentDataInitializedEventArgs"/> instance containing the event data.</param>
        private void ultraCalendarInfo1_AppointmentDataInitialized(object sender, Infragistics.Win.UltraWinSchedule.AppointmentDataInitializedEventArgs e)
        {
            // Assign the bar and appointment color
            Color[] colors = new Color[] 
                {
                    Color.FromArgb(255, 158, 179, 6),
                    Color.FromArgb(255, 2, 107, 193),
                    Color.FromArgb(255, 248, 90, 50),
                    Color.FromArgb(255, 206, 26, 55),
                    Color.FromArgb(255, 243, 156, 21),
                    Color.FromArgb(255, 102, 24, 136)
                };

            Color color = colors[e.Appointment.BindingListIndex % colors.Length];

            e.Appointment.BarColor = color;
            e.Appointment.Appearance.BackColor = ControlPaint.Light(ControlPaint.LightLight(color));
        }

        #endregion //ultraCalendarInfo1_AppointmentDataInitialized

        #region ultraCalendarInfo1_BeforeAlternateSelectedDateRangeChange

        /// <summary>
        /// Handles the BeforeAlternateSelectedDateRangeChange event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BeforeSelectedDateRangeChangeEventArgs"/> instance containing the event data.</param>
        private void ultraCalendarInfo1_BeforeAlternateSelectedDateRangeChange(object sender, BeforeSelectedDateRangeChangeEventArgs e)
        {

            if (this.timeSlotSelectionRecursionFlag)
            {
                e.Cancel = true;
                return;
            }

            if (this.dateSelectionRecursionFlag)
                return;

            if (this.ultraCalendarInfo1.AlternateSelectedDateRanges.SelectedDaysCount > 0 &&
                e.NewSelectedDateRanges.SelectedDaysCount > 0)
            {
                Infragistics.Win.UltraWinSchedule.Day currentFirstDay = this.ultraCalendarInfo1.AlternateSelectedDateRanges[0].FirstDay;
                Infragistics.Win.UltraWinSchedule.Day newFirstDay = e.NewSelectedDateRanges[0].FirstDay;

                int compareValue = DateTime.Compare(currentFirstDay.Date, newFirstDay.Date);
                if (compareValue > 0)
                {
                    this.SelectWorkWeek(currentFirstDay.Week.WeekNumber - 1, currentFirstDay.Week.Year.YearNumber);
                    e.Cancel = true;
                }
                else if (compareValue < 0)
                {
                    this.SelectWorkWeek(currentFirstDay.Week.WeekNumber + 1, currentFirstDay.Week.Year.YearNumber);
                    e.Cancel = true;
                }
            }
        }

        #endregion //ultraCalendarInfo1_BeforeAlternateSelectedDateRangeChange

        #region Cancelled Schedule events

        #region ultraCalendarInfo1_BeforeAppointmentAdded

        /// <summary>
        /// Handles the BeforeAppointmentAdded event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelableAppointmentEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void ultraCalendarInfo1_BeforeAppointmentAdded(object sender, CancelableAppointmentEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion //ultraCalendarInfo1_BeforeAppointmentAdded

        #region dayView_BeforeAppointmentsDeleted

        /// <summary>
        /// Handles the BeforeAppointmentsDeleted event of the dayView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BeforeAppointmentsDeletedEventArgs"/> instance containing the event data.</param>
        private void dayView_BeforeAppointmentsDeleted(object sender, BeforeAppointmentsDeletedEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion //dayView_BeforeAppointmentsDeleted

        #region dayView_BeforeAppointmentEdited

        /// <summary>
        /// Handles the BeforeAppointmentEdited event of the UltraDayView controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelableAppointmentEventArgs"/> instance containing the event data.</param>
        private void dayView_BeforeAppointmentEdited(object sender, CancelableAppointmentEventArgs e)
        {
            // We don't want to show the default appointment dialog, so cancel the event
            e.Cancel = true;
        }

        #endregion //dayView_BeforeAppointmentEdited

        #region dayView_BeforeAppointmentsMoved

        /// <summary>
        /// Handles the BeforeAppointmentsMoved event of the dayView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelableAppointmentsEventArgs"/> instance containing the event data.</param>
        private void dayView_BeforeAppointmentsMoved(object sender, CancelableAppointmentsEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion //dayView_BeforeAppointmentsMoved

        #region dayView_BeforeAppointmentResized

        /// <summary>
        /// Handles the BeforeAppointmentResized event of the dayView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BeforeAppointmentResizedEventArgs"/> instance containing the event data.</param>
        private void dayView_BeforeAppointmentResized(object sender, BeforeAppointmentResizedEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion //dayView_BeforeAppointmentResized

        #region ultraCalendarInfo1_BeforeDisplayAppointmentDialog

        /// <summary>
        /// Handles the BeforeDisplayAppointmentDialog event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinSchedule.DisplayAppointmentDialogEventArgs"/> instance containing the event data.</param>
        private void ultraCalendarInfo1_BeforeDisplayAppointmentDialog(object sender, Infragistics.Win.UltraWinSchedule.DisplayAppointmentDialogEventArgs e)
        {
            // don't show the appointment dialog
            e.Cancel = true;
        }

        #endregion //ultraCalendarInfo1_BeforeDisplayAppointmentDialog

        #region mvMonthly_AppointmentsDragging

        /// <summary>
        /// Handles the AppointmentsDragging event of the mvMonthly control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AppointmentsDraggingEventArgs"/> instance containing the event data.</param>
        private void mvMonthly_AppointmentsDragging(object sender, AppointmentsDraggingEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion //mvMonthly_AppointmentsDragging

        #region mvMonthly_AppointmentResizing

        /// <summary>
        /// Handles the AppointmentResizing event of the mvMonthly control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AppointmentResizingEventArgs"/> instance containing the event data.</param>
        private void mvMonthly_AppointmentResizing(object sender, AppointmentResizingEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion //mvMonthly_AppointmentResizing

        #endregion //Cancelled Schedule events

        #endregion //Event Handlers

        #region Methods

        #region GetLocalizedString
        /// <summary>
        /// Localizes a string using the ResourceManager.
        /// </summary>
        /// <param name="currentString"></param>
        /// <returns></returns>
        internal static string GetLocalizedString(string currentString)
        {
            ResourceManager rm = Showcase.InventoryManagement.Properties.Resources.ResourceManager;
            string localizedString = rm.GetString(currentString);
            return (string.IsNullOrEmpty(localizedString) ? currentString : localizedString).Replace('_', ' ');
        }
        #endregion // GetLocalizedString

        #region NavigateSchedule

        /// <summary>
        /// Navigates the schedule schedule controls.
        /// </summary>
        private void NavigateSchedule(string selectedTabKey, bool forward)
        {
            int directionOffset = forward ? 1 : -1;

            switch (selectedTabKey)
            {
                case "Daily":
                    this.ultraCalendarInfo1.ActivateDay(this.ultraCalendarInfo1.ActiveDay.Date.AddDays(directionOffset));
                    this.ultraCalendarInfo1.SelectedDateRanges.Add(this.ultraCalendarInfo1.ActiveDay.Date, true);
                    break;
                case "Weekly":
                    Week week = this.ultraCalendarInfo1.AlternateSelectedDateRanges[0].FirstDay.Week;
                    this.SelectWorkWeek(week.WeekNumber + directionOffset, week.Year.YearNumber);
                    break;
                case "Monthly":
                    this.ultraCalendarInfo1.ActivateDay(this.ultraCalendarInfo1.ActiveDay.Date.AddMonths(directionOffset));
                    break;
            }
        }

        #endregion //NavigateSchedule

        #region SelectWorkWeek

        private void SelectWorkWeek(int weekNum, int year)
        {
            if (this.dateSelectionRecursionFlag)
                return;

            this.dateSelectionRecursionFlag = true;
            try
            {
                DateTime startOfWeek = this.ultraCalendarInfo1.GetDateForFirstDayOfWeek(weekNum, year);
                while (this.ultraCalendarInfo1.DaysOfWeek[startOfWeek.DayOfWeek].IsWorkDay == false)
                    startOfWeek = startOfWeek.AddDays(1);

                this.ultraCalendarInfo1.AlternateSelectedDateRanges.Add(startOfWeek, this.ultraCalendarInfo1.WorkDaysPerWeek - 1, true);
            }
            finally
            {
                this.dateSelectionRecursionFlag = false;
            }
        }

        #endregion // SelectWorkWeek

        #region SetAppointmentBindings

        /// <summary>
        /// Set the Data Bindings for Appointments
        /// </summary>
        /// <param name="table"></param>
        private void SetAppointmentBindings(DataTable table)
        {
            this.ultraCalendarInfo1.DataBindingsForAppointments.BindingContextControl = this;

            // Basic Appointment properties
            this.ultraCalendarInfo1.DataBindingsForAppointments.SubjectMember = "Subject";
            this.ultraCalendarInfo1.DataBindingsForAppointments.DescriptionMember = "Description";

            this.ultraCalendarInfo1.DataBindingsForAppointments.StartDateTimeMember = "StartDateTime";
            this.ultraCalendarInfo1.DataBindingsForAppointments.EndDateTimeMember = "EndDateTime";

            // DataSource & DataMember
            this.ultraCalendarInfo1.DataBindingsForAppointments.SetDataBinding(table, null);
        }

        #endregion //SetAppointmentBindings

        #region SetScheduleHeader

        private void SetScheduleHeader()
        {
            this.llDateNavigation.Text = (this.tcOrders.SelectedTab.Key == "Monthly")
                ? this.ultraCalendarInfo1.ActiveDay.Date.ToString("Y")
                : this.llDateNavigation.Text = string.Empty;
        }

        #endregion //SetScheduleHeader

        #endregion //Methods

        
        private void datachartReportsStock_Click(object sender, EventArgs e)
        {

        }
    }
}
