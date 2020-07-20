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
using Infragistics.Win.DataVisualization;
using Showcase.CashflowDashboard.Data;
using Infragistics.Win.UltraWinScrollBar;

namespace Showcase.CashflowDashboard
{
    public partial class MainForm : Form
    {
        #region Constants

        // This is the extent of the Y Axis Labels in the three charts that make up the Total Cashflow section.
        //
        private const int TotalCashflowChartYAxisLabelExtent = 60;

        // This is the number of months that wil be visible in the chart in the TotalCashflow 
        // chart (which is actually 3 charts). 
        //
        private const int MonthsToDisplay = 13;

        // This is the scale of the horizontal area of the three charts that make up the Total Cashflow section.
        // The charts will be scaled down so that only a certain number of months of data (MonthsToDisplay) is 
        // visible and then a scrollbar can be used to change the viewport. 
        //
        internal const double TotalCashflowChartHorizontalScale = 1.0D / (((double)DataManager.NumberOfYears * 12.0D) / (double)MainForm.MonthsToDisplay);

        #endregion //Constants

        #region Private Members

        // These two colors are used to draw a 3D border between the Inflow and outflow panels
        private Color inflowPanelRightBorderColor = Color.FromArgb(215, 213, 214);
        private Color outflowPanelLeftBorderColor = Color.FromArgb(245, 243, 244);

        // This indicates the selected item on the top chart. Clicking an item on the top chart
        // updates the rest of the form UI with data based on the selected month. 
        private int selectedIndex;       
        #endregion // Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();  
         
            // InitializeUI sets up all of the UI controls on the form. 
            this.InitializeUI();
        }

        #endregion //Constructor        

        #region Methods

        #region ForceTotalCashChartRefresh
        /// <summary>
        /// This method forces a refresh and repaint of the three charts that make up the 
        /// Total Cashflow chart. This is neccessary to make their scrolling appear smooth
        /// and synchronized. 
        /// </summary>
        private void ForceTotalCashChartRefresh()
        {
            this.crtEndingCash.Flush();
            this.crtLabels.Flush();
            this.crtMonthlyTotals.Flush();
            this.crtEndingCash.Update();
            this.crtLabels.Update();
            this.crtMonthlyTotals.Update();
        }  
        #endregion // ForceTotalCashChartRefresh
       
        #region InitializeEndingCashChart
        /// <summary>
        /// Initalizes the crtEndingCash chart.
        /// </summary>
        /// <remarks>
        /// The top part of the form is made up a 3 charts that look like one big chart. This is the top
        /// chart which displays a LineSeries that shows the total ending cash value for each month.
        /// </remarks>
        private void InitializeEndingCashChart()
        {
            // crtEndingCash is the first of three charts that appear to be one big chart at the top 
            // of the form in the Total Cashflow section. This chart shows the total ending cash
            // for each month. 

            // Turn off zooming / panning.
            this.crtEndingCash.HorizontalZoomable = false;
            this.crtEndingCash.VerticalZoomable = false;           

            // The chart is a different color at design-time to make it easy to see and interact with.
            // But at run-time, make it the same color as the panel it's in. 
            this.crtEndingCash.BackColor = this.pnlTotalCashFlowChart.BackColor;

            // Make sure this chart lines up with the other two. 
            this.crtEndingCash.ViewerMargin = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.crtEndingCash.Margin = new Padding(0);

            // Set the scale so that only one year of data is visible. 
            this.crtEndingCash.WindowScaleHorizontal = MainForm.TotalCashflowChartHorizontalScale;
                        

            // The line series requires an X and a Y Axis.
            
            // The X axis is for categories (months). But  don't need to display the X axis labels 
            // for this chart (The bottom chart - crtMonthlyTotals - will take care of that).
            //
            #region X Axis
            var endingCashChartCategoryX = new CategoryXAxis();
            endingCashChartCategoryX.DataSource = DataManager.MonthlyData;
            endingCashChartCategoryX.Interval = 1;
            endingCashChartCategoryX.Label = "Month";

            // But drawing the Strip in the background color we cover up the vertical lines in every
            // alternate interval. 
            endingCashChartCategoryX.Strip = new SolidColorBrush(Color.FromArgb(235, 233, 234));            
            endingCashChartCategoryX.MajorStroke = new SolidColorBrush(Color.FromArgb(195, 195, 195));

            // Don't draw the horizontal lines to create the illusion that the three charts are one
            // big chart. 
            endingCashChartCategoryX.Stroke = new SolidColorBrush(Color.Transparent);
            endingCashChartCategoryX.UseClusteringMode = true;

            // Set the LabelTextColor to the same as the background color so no labels are displayed. 
            endingCashChartCategoryX.LabelTextColor = new SolidColorBrush(Color.FromArgb(235, 233, 234));            //

            // Even though we are not displaying the labels, we want the tickmarks to extend so that
            // it looks like one solid line joining with the middle chart. 
            endingCashChartCategoryX.LabelExtent = 10;
            endingCashChartCategoryX.TickLength = 10; 
            #endregion // X Axis

            // The X axis is for the ending cash values (in millions).
            //
            #region Y Axis
            var endingCashChartNumericY = new NumericYAxis();
            endingCashChartNumericY.LabelFontFamily = "Verdana";            
            endingCashChartNumericY.StrokeThickness = 1;
            endingCashChartNumericY.LabelTextColor = new SolidColorBrush(Color.FromArgb(146, 146, 146));
            endingCashChartNumericY.MajorStroke = new SolidColorBrush(Color.FromArgb(195, 195, 195));

            // We need to set the label extent so that the labels line up with the other
            // two charts. 
            endingCashChartNumericY.LabelExtent = TotalCashflowChartYAxisLabelExtent;
            
            // We could let the chart determine a reasonable minimum and maximum value, but explicitly 
            // setting them ensures that we end up with good round numbers (100M to 200M).
            endingCashChartNumericY.MinimumValue = 100000000;
            endingCashChartNumericY.MaximumValue = 200000000;

            // The chart will show the entire value as the label, so handle the FormatLabel
            // event to format the value into more user-friendly text. 
            endingCashChartNumericY.FormatLabel += new AxisFormatLabelHandler(endingCashChartNumericY_FormatLabel);
            #endregion // Y Axis

            // Added the Axes to the chart. 
            this.crtEndingCash.Axes.Add(endingCashChartNumericY);
            this.crtEndingCash.Axes.Add(endingCashChartCategoryX);

            // This is the line series which displays the EndingCash value for each month. 
            //
            #region Line Series
            var endingCashChartSeries = new LineSeries();
            endingCashChartSeries.Title = "ending cash value";
            endingCashChartSeries.XAxis = endingCashChartCategoryX;
            endingCashChartSeries.YAxis = endingCashChartNumericY;
            endingCashChartSeries.ValueMemberPath = "EndingCash";
            endingCashChartSeries.DataSource = DataManager.MonthlyData;
            endingCashChartSeries.IsTransitionInEnabled = true;
            endingCashChartSeries.Thickness = 3;
            endingCashChartSeries.Brush = new SolidColorBrush(Color.FromArgb(131, 201, 252));
            endingCashChartSeries.Outline = new SolidColorBrush(Color.FromArgb(131, 201, 252));
            endingCashChartSeries.MarkerBrush = new SolidColorBrush(Color.FromArgb(235, 233, 234));
            endingCashChartSeries.MarkerOutline = new SolidColorBrush(Color.FromArgb(131, 201, 252));
            endingCashChartSeries.MarkerType = MarkerType.Automatic; 
            #endregion // Line Series

            // Add the series to the chart. 
            this.crtEndingCash.Series.Add(endingCashChartSeries);            
        }
        #endregion // InitializeEndingCashChart

        #region InitializeLabelsChart
        /// <summary>
        /// Initalizes the crtLabels chart.
        /// </summary>
        /// <remarks>
        /// The top part of the form is made up a 3 charts that look like one big chart. This is the middle
        /// chart which displays the net inflow/outflow for the month.
        /// </remarks>
        private void InitializeLabelsChart()
        {
            // Turn off zooming / panning.
            this.crtLabels.HorizontalZoomable = false;
            this.crtLabels.VerticalZoomable = false;
            
            // The chart is a different color at design-time to make it easy to see and interact with.
            // But at run-time, make it the same color as the panel it's in. 
            this.crtLabels.BackColor = this.pnlTotalCashFlowChart.BackColor;

            // Make sure this chart lines up with the other two. 
            this.crtLabels.ViewerMargin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.crtLabels.Margin = new Padding(0);

            // Set the scale so that only one year of data is visible. 
            this.crtLabels.WindowScaleHorizontal = MainForm.TotalCashflowChartHorizontalScale;

            // X Axis
            // This chart is a little unusual. We don't actually want to display the values as 
            // graphics, we just want to display a value. So we will fake it by making the 
            // chart with an X Axis and arranging it so that the Labels are centered within
            // the chart vertically and there is no room for the graphical data. 
            //
            #region X Axis
            var labelsChartCategoryX = new CategoryXAxis();

            // Bind to the same data as the other two charts. 
            labelsChartCategoryX.DataSource = DataManager.MonthlyData;            
            labelsChartCategoryX.LabelTextStyle = FontStyle.Bold;
            labelsChartCategoryX.LabelTextColor = new SolidColorBrush(Color.FromArgb(146, 146, 146));
            labelsChartCategoryX.UseClusteringMode = true;

            // Explicitly set the Interval to 1 so we always show every month.
            labelsChartCategoryX.Interval = 1;
            
            // This is actually irrelevant. We need to bind to a real field just so that the chart
            // will have the right number of items in the right places. But since we are going to be 
            // showing a calculated value as the label, we could use any valid field name here. 
            labelsChartCategoryX.Label = "EndingCash";
            
            // This eliminates the alternative background color for each interval.
            labelsChartCategoryX.Strip = new Infragistics.Win.DataVisualization.SolidColorBrush(Color.FromArgb(235, 233, 234));
            
            // This is to create a solid vertical line between each month. 
            labelsChartCategoryX.MajorStroke = new SolidColorBrush(Color.FromArgb(195, 195, 195));
            labelsChartCategoryX.LabelLocation = AxisLabelsLocation.InsideBottom;

            // Remove the line at the bottom of each item so this chart blends into the one
            // below it (this.crtMonthlyTotals) and they look like one chart.             
            labelsChartCategoryX.Stroke = new SolidColorBrush(Color.Transparent);
            
            // Use the entire height of the control as the LabelExtent. This chart does not needs to 
            // display any data, we just want labels. 
            labelsChartCategoryX.LabelExtent = this.crtLabels.Height;
            
            // Hook the FormatLabel event. This event allows us to change the text of the label. We do
            // this for two reasons. First, because the value of the label doesn't exist in the
            // data source - it needs to be calculated. Second, so we can format the value in 
            // a more user-friendly way. 
            labelsChartCategoryX.FormatLabel += new AxisFormatLabelHandler(labelsChartCategoryX_FormatLabel); 
            #endregion // X Axis

            // Y Axis
            // The Y Axis here is very simple. It doesn't do much, but it's required to be there
            // in order for the chart to dislpay anything. 
            //
            #region Y Axis
            var labelsChartNumericY = new NumericYAxis();

            // We need to set the label extent so that the labels line up with the other
            // two charts. 
            labelsChartNumericY.LabelExtent = TotalCashflowChartYAxisLabelExtent;
            #endregion // Y Axis          

            // Added the Axes to the chart. 
            this.crtLabels.Axes.Add(labelsChartNumericY);
            this.crtLabels.Axes.Add(labelsChartCategoryX);            
        }
        #endregion // InitializeLabelsChart

        #region InitializeMonthlyTotalsChart
        /// <summary>
        /// Initalizes the crtMonthlyTotals chart.
        /// </summary>
        /// <remarks>
        /// The top part of the form is made up a 3 charts that look like one big chart. This is the bottom
        /// chart which displays the inflow and outflow and projected values for each month.
        /// </remarks>
        private void InitializeMonthlyTotalsChart()
        {
            // Turn off zooming / panning.
            this.crtMonthlyTotals.HorizontalZoomable = false;
            this.crtMonthlyTotals.VerticalZoomable = false;

            // The chart is a different color at design-time to make it easy to see and interact with.
            // But at run-time, make it the same color as the panel it's in. 
            this.crtMonthlyTotals.BackColor = this.pnlTotalCashFlowChart.BackColor;

            // Make sure this chart lines up with the other two. 
            this.crtMonthlyTotals.ViewerMargin = new System.Windows.Forms.Padding(0, 0, 0, 0);            
            this.crtMonthlyTotals.Margin = new Padding(0);

            // Set the scale so that only one year of data is visible. 
            this.crtMonthlyTotals.WindowScaleHorizontal = MainForm.TotalCashflowChartHorizontalScale;

            // X Axis 1
            // This chart has 2 X Axes. This one is for the Column Series which displays the 
            // inflow and outflow for each month. 
            //
            #region X Axis 1
            var monthlyTotalsChartCategoryX1 = new CategoryXAxis();            
            monthlyTotalsChartCategoryX1.DataSource = DataManager.MonthlyData;

            // Explicitly set the Interval to 1 so we always show every month.
            monthlyTotalsChartCategoryX1.Interval = 1;

            // We are using the Month field for the label. But this is really irrelevant. We
            // are going to use the FormatLabel event to show a three-letter abbreviation for the month
            // instead of the value of the Month field (which is an integer from 1 to 12). 
            monthlyTotalsChartCategoryX1.Label = "Month";
            monthlyTotalsChartCategoryX1.LabelFontFamily = "Verdana";            
            monthlyTotalsChartCategoryX1.LabelTextColor = new SolidColorBrush(Color.FromArgb(146, 146, 146));

            // This eliminates the alternative background color for each interval.
            monthlyTotalsChartCategoryX1.Strip = new Infragistics.Win.DataVisualization.SolidColorBrush(Color.FromArgb(235, 233, 234));

            // This is to create a solid vertical line between each month. 
            monthlyTotalsChartCategoryX1.MajorStroke = new SolidColorBrush(Color.FromArgb(195, 195, 195));

            // Hook FormatLabel so we can take the integer (Month) and display a more user-friendly
            // 3-letter abbreviation for the month.
            monthlyTotalsChartCategoryX1.FormatLabel += new AxisFormatLabelHandler(monthlyTotalsChartCategoryX1_FormatLabel);
            #endregion // X Axis 1

            // X Axis 2
            // This chart has 2 X Axes. This one is for the Column Series which displays the 
            // PROJECTED inflow and outflow for each month. 
            //
            #region X Axis 2
            var monthlyTotalsChartCategoryX2 = new CategoryXAxis();            

            monthlyTotalsChartCategoryX2.DataSource = DataManager.MonthlyData;
            monthlyTotalsChartCategoryX2.Interval = 1;

            // Even though this axis will not be displaying any labels, we still need to set the
            // Label field so it has the right number of data items.
            monthlyTotalsChartCategoryX2.Label = "Month";

            // This is a sneaky trick to draw some lines going up that join with the chart
            // in the middle (this.crtLabels) so they look like one big chart.             
            monthlyTotalsChartCategoryX2.LabelLocation = AxisLabelsLocation.OutsideTop;
            monthlyTotalsChartCategoryX2.LabelExtent = 10;
            monthlyTotalsChartCategoryX2.TickLength = 10;

            // This is essentially to hide the labels, which are unneccessary since X Axis 1
            // is displaying the labels we need.
            monthlyTotalsChartCategoryX2.LabelTextColor = new SolidColorBrush(Color.FromArgb(235, 233, 234));

            // We don't needs any of these to draw since X Axis 1 already gives us the lines we need. 
            monthlyTotalsChartCategoryX2.Strip = new SolidColorBrush(Color.Transparent);
            monthlyTotalsChartCategoryX2.Stroke = new SolidColorBrush(Color.Transparent);
            monthlyTotalsChartCategoryX2.MajorStroke = new SolidColorBrush(Color.Transparent); 
            #endregion // X Axis 2

            // Y Axis
            // The Y Axis is the monetary value of each inflow and outflow. We will use the 
            // same Y Axis for all 4 Columns (and both X Axes). 
            //
            #region Y Axis
            var monthlyTotalsChartNumericY = new NumericYAxis();
            monthlyTotalsChartNumericY.LabelTextColor = new SolidColorBrush(Color.FromArgb(146, 146, 146));
            monthlyTotalsChartNumericY.MajorStroke = new SolidColorBrush(Color.FromArgb(195, 195, 195));
            monthlyTotalsChartNumericY.StrokeThickness = 1;

            // We need to set the label extent so that the labels line up with the other
            // two charts. 
            monthlyTotalsChartNumericY.LabelExtent = TotalCashflowChartYAxisLabelExtent;
            
            // Always start at 0 so the relative sizes of the columns are meaningful. 
            monthlyTotalsChartNumericY.MinimumValue = 0;

            // Hook format label so we can apply custom formatting to the labels. Otherwise,
            // these labels will show the raw values in the millions and there will be a whole
            // lot of unneccessary zeroes. 
            monthlyTotalsChartNumericY.FormatLabel += new AxisFormatLabelHandler(monthlyTotalsChartNumericY_FormatLabel); 
            #endregion // Y Axis

            // Add the axes to the chart.
            this.crtMonthlyTotals.Axes.Add(monthlyTotalsChartNumericY);
            this.crtMonthlyTotals.Axes.Add(monthlyTotalsChartCategoryX1);
            this.crtMonthlyTotals.Axes.Add(monthlyTotalsChartCategoryX2);

            // Inflow column series
            //
            #region inflow column series
            var monthlyTotalsChartInflowSeries = new ColumnSeries();
            monthlyTotalsChartInflowSeries.Title = "inflow";
            monthlyTotalsChartInflowSeries.XAxis = monthlyTotalsChartCategoryX1;
            monthlyTotalsChartInflowSeries.YAxis = monthlyTotalsChartNumericY;
            monthlyTotalsChartInflowSeries.ValueMemberPath = "Inflow";
            monthlyTotalsChartInflowSeries.DataSource = DataManager.MonthlyData;
            monthlyTotalsChartInflowSeries.IsTransitionInEnabled = true;
            monthlyTotalsChartInflowSeries.Brush = new SolidColorBrush(Color.FromArgb(56, 106, 155));
            monthlyTotalsChartInflowSeries.Outline = new SolidColorBrush(Color.FromArgb(56, 106, 155));
            #endregion // inflow column  series

            // Outflow column series
            //
            #region Outflow column series
            var monthlyTotalsChartOutflowSeries = new ColumnSeries();
            monthlyTotalsChartOutflowSeries.Title = "outflow";
            monthlyTotalsChartOutflowSeries.XAxis = monthlyTotalsChartCategoryX1;
            monthlyTotalsChartOutflowSeries.YAxis = monthlyTotalsChartNumericY;
            monthlyTotalsChartOutflowSeries.ValueMemberPath = "Outflow";
            monthlyTotalsChartOutflowSeries.DataSource = DataManager.MonthlyData;
            monthlyTotalsChartOutflowSeries.IsTransitionInEnabled = true;
            monthlyTotalsChartOutflowSeries.Brush = new SolidColorBrush(Color.FromArgb(243, 122, 43));
            monthlyTotalsChartOutflowSeries.Outline = new SolidColorBrush(Color.FromArgb(243, 122, 43)); 
            #endregion // Outflow column series

            // Projected inflow column series
            //
            #region Projected inflow column series
            var monthlyTotalsChartProjectedInflow = new ColumnSeries();
            monthlyTotalsChartProjectedInflow.Title = "projected inflow";
            monthlyTotalsChartProjectedInflow.XAxis = monthlyTotalsChartCategoryX2;
            monthlyTotalsChartProjectedInflow.YAxis = monthlyTotalsChartNumericY;
            monthlyTotalsChartProjectedInflow.ValueMemberPath = "ProjectedInflow";
            monthlyTotalsChartProjectedInflow.DataSource = DataManager.MonthlyData;
            monthlyTotalsChartProjectedInflow.IsTransitionInEnabled = true;
            monthlyTotalsChartProjectedInflow.MarkerType = MarkerType.Circle;
            monthlyTotalsChartProjectedInflow.Brush = new SolidColorBrush(Color.Transparent);
            monthlyTotalsChartProjectedInflow.Outline = new SolidColorBrush(Color.Transparent);
            monthlyTotalsChartProjectedInflow.MarkerBrush = new SolidColorBrush(Color.FromArgb(56, 106, 155));
            monthlyTotalsChartProjectedInflow.MarkerOutline = new SolidColorBrush(Color.FromArgb(240, 240, 240)); 
            #endregion // Projected inflow column series

            // Projected outflow column series
            //
            #region Projected outflow column series
            var monthlyTotalsChartProjectedOutflow = new ColumnSeries();
            monthlyTotalsChartProjectedOutflow.Title = "projected outflow";
            monthlyTotalsChartProjectedOutflow.XAxis = monthlyTotalsChartCategoryX2;
            monthlyTotalsChartProjectedOutflow.YAxis = monthlyTotalsChartNumericY;
            monthlyTotalsChartProjectedOutflow.ValueMemberPath = "ProjectedOutflow";
            monthlyTotalsChartProjectedOutflow.DataSource = DataManager.MonthlyData;
            monthlyTotalsChartProjectedOutflow.IsTransitionInEnabled = true;
            monthlyTotalsChartProjectedOutflow.MarkerType = MarkerType.Circle;
            monthlyTotalsChartProjectedOutflow.Brush = new SolidColorBrush(Color.Transparent);
            monthlyTotalsChartProjectedOutflow.Outline = new SolidColorBrush(Color.Transparent);
            monthlyTotalsChartProjectedOutflow.MarkerBrush = new SolidColorBrush(Color.FromArgb(243, 122, 43));
            monthlyTotalsChartProjectedOutflow.MarkerOutline = new SolidColorBrush(Color.FromArgb(240, 240, 240)); 
            #endregion // Projected outflow column series
            
            // Add the series to the chart. 
            this.crtMonthlyTotals.Series.Add(monthlyTotalsChartInflowSeries);
            this.crtMonthlyTotals.Series.Add(monthlyTotalsChartOutflowSeries);
            this.crtMonthlyTotals.Series.Add(monthlyTotalsChartProjectedInflow);
            this.crtMonthlyTotals.Series.Add(monthlyTotalsChartProjectedOutflow);
        }
        #endregion // InitializeMonthlyTotalsChart       

        #region InitializeUI

        /// <summary>
        /// Initializes the UI controls.
        /// </summary>
        private void InitializeUI()
        {            
            // These DrawFilters will draw a border on the right and left edges of the Inflow and Outflow 
            // panels, respectively. This creates a nice 3D border effect between the two. 
            this.pnlInflow.DrawFilter = new UIElementFilters.UltraPanelBorderDrawFilter(borderColorRight: this.inflowPanelRightBorderColor);
            this.pnlOutflow.DrawFilter = new UIElementFilters.UltraPanelBorderDrawFilter(borderColorLeft: this.outflowPanelLeftBorderColor);
            
            // There are three charts in the TotalCashflow section at the top. They are set up to look
            // like one chart. Initialize them now. 
            //
            this.InitializeEndingCashChart();
            this.InitializeLabelsChart();
            this.InitializeMonthlyTotalsChart();
        }        
        #endregion //InitializeUI                  

        #region UpdateDateRangeLabel
        /// <summary>
        /// Updates the date range label (on top) to show the current range of months that are
        /// displayed in the Total Cashflow chart(s).
        /// </summary>
        private void UpdateDateRangeLabel()
        {
            // Get the first visible month in the chart. 
            var categoryAxis = this.crtMonthlyTotals.Axes.OfType<CategoryAxisBase>().First();
            var firstVisibleIndexD = categoryAxis.UnscaleValue(this.crtMonthlyTotals.ViewportRect.Left);
            int firstVisibleIndex = (int)Math.Round(firstVisibleIndexD, 0);
            var firstVisibleMonth = DataManager.MonthlyData[firstVisibleIndex];

            // Get the last visible month in the chart. 
            var lastVisibleIndexD = categoryAxis.UnscaleValue(this.crtMonthlyTotals.ViewportRect.Right);
            int lastVisibleIndex = (int)Math.Round(lastVisibleIndexD, 0) - 1;
            var lastVisibleMonth = DataManager.MonthlyData[lastVisibleIndex];

            // Update the label with the proper range.
            this.lblDateRangeValue.Text = Utilities.LocalizeString("DateRangeFormat", firstVisibleMonth.Month, firstVisibleMonth.Year, lastVisibleMonth.Month, lastVisibleMonth.Year);
        }
        #endregion // UpdateDateRangeLabel

        #region UpdateMonthlyDetailsUI
        /// <summary>
        /// Whenever we change the "selected" month (by clicking on one of the Total Cashflow chart
        /// items), we need to update some variables and some UI stuff. 
        /// </summary>
        /// <param name="monthlyData"></param>
        private void UpdateMonthlyDetailsUI(MonthlyData monthlyData)
        {
            // Set the SelectedIndex. This is the currently-selected month. This index is used
            // to show the highlights on the top chart(s) and also in the Overview charts for 
            // Inflow and Outflow.
            //
            this.SelectedIndex = DataManager.MonthlyData.IndexOf(monthlyData);

            // Update the Inflow and Outflow sections on the bottom. This tells them to update
            // the scroll position and highlight drawing on the Overview charts.
            //
            this.cfdInflow.UpdateData(this.selectedIndex, this.crtMonthlyTotals.WindowPositionHorizontal);
            this.cfdOutflow.UpdateData(this.selectedIndex, this.crtMonthlyTotals.WindowPositionHorizontal);
        }
        #endregion // UpdateMonthlyDetailsUI

        #region UpdateSummaryBlocks
        /// <summary>
        /// Update the summary blocks (on the left) with the data from the currently "selected" month.
        /// </summary>
        private void UpdateSummaryBlocks()
        {
            string localizedNotApplicableText = Utilities.LocalizeString("NotApplicable");

            // Get the "selected" month data. 
            var currentMonthData = DataManager.MonthlyData[this.SelectedIndex];
            DateTime current = new DateTime(currentMonthData.Year, currentMonthData.Month, 1);            

            // Update the all of the blocks with the values from the selected month. 
            this.sbEndingCashValue.CurrentValue = Utilities.GetDisplayValueInMillions(currentMonthData.EndingCash, 1, CashflowDetails.MillionsSuffix);
            this.sbAssetLiablityRatio.CurrentValue = currentMonthData.AssetLiabilityRatio.ToString("n1");
            this.sbCashFlowFromOE.CurrentValue = Utilities.GetDisplayValueInMillions(currentMonthData.GetInflow(Source.Operations), 1, CashflowDetails.MillionsSuffix);
            this.sbCashFlowFromInvesting.CurrentValue = Utilities.GetDisplayValueInMillions(currentMonthData.GetInflow(Source.Investing), 1, CashflowDetails.MillionsSuffix);
            this.sbCashFlowFromFinancing.CurrentValue = Utilities.GetDisplayValueInMillions(currentMonthData.GetInflow(Source.Financing), 1, CashflowDetails.MillionsSuffix);           

            // Now get the previous month
            DateTime previousMonth = current.AddMonths(-1);
            var lastMonthData = DataManager.GetMonthlyData(previousMonth.Month, previousMonth.Year);
            if (lastMonthData != null)
            {
                // If there is a previous month, update each summary block with last month's data.
                this.sbEndingCashValue.LastMonthValue = Utilities.GetDisplayValueInMillions(lastMonthData.EndingCash, 1, CashflowDetails.MillionsSuffix);
                this.sbAssetLiablityRatio.LastMonthValue = lastMonthData.AssetLiabilityRatio.ToString("n1");
                this.sbCashFlowFromOE.LastMonthValue = Utilities.GetDisplayValueInMillions(lastMonthData.GetInflow(Source.Operations), 1, CashflowDetails.MillionsSuffix);
                this.sbCashFlowFromInvesting.LastMonthValue = Utilities.GetDisplayValueInMillions(lastMonthData.GetInflow(Source.Investing), 1, CashflowDetails.MillionsSuffix);
                this.sbCashFlowFromFinancing.LastMonthValue = Utilities.GetDisplayValueInMillions(lastMonthData.GetInflow(Source.Financing), 1, CashflowDetails.MillionsSuffix);
            }
            else
            {
                // If there is no previous month, just use a hard-coded string. 
                this.sbEndingCashValue.LastMonthValue = localizedNotApplicableText;
                this.sbAssetLiablityRatio.LastMonthValue = localizedNotApplicableText;
                this.sbCashFlowFromOE.LastMonthValue = localizedNotApplicableText;
                this.sbCashFlowFromInvesting.LastMonthValue = localizedNotApplicableText;
                this.sbCashFlowFromFinancing.LastMonthValue = localizedNotApplicableText;
            }

            // Now get the previous year
            DateTime previousYear = current.AddYears(-1);
            var lastYearData = DataManager.GetMonthlyData(previousYear.Month, previousYear.Year);
            if (lastYearData != null)
            {
                // If there is a previous year, update each summary block with last year's data.
                this.sbEndingCashValue.LastYearValue = Utilities.GetDisplayValueInMillions(lastYearData.EndingCash, 1, CashflowDetails.MillionsSuffix);
                this.sbAssetLiablityRatio.LastYearValue = lastYearData.AssetLiabilityRatio.ToString("n1");
                this.sbCashFlowFromOE.LastYearValue = Utilities.GetDisplayValueInMillions(lastYearData.GetInflow(Source.Operations), 1, CashflowDetails.MillionsSuffix);
                this.sbCashFlowFromInvesting.LastYearValue = Utilities.GetDisplayValueInMillions(lastYearData.GetInflow(Source.Investing), 1, CashflowDetails.MillionsSuffix);
                this.sbCashFlowFromFinancing.LastYearValue = Utilities.GetDisplayValueInMillions(lastYearData.GetInflow(Source.Financing), 1, CashflowDetails.MillionsSuffix);
            }
            else
            {
                // If there is no previous month, just use a hard-coded string. 
                this.sbEndingCashValue.LastYearValue = localizedNotApplicableText;
                this.sbAssetLiablityRatio.LastYearValue = localizedNotApplicableText;
                this.sbCashFlowFromOE.LastYearValue = localizedNotApplicableText;
                this.sbCashFlowFromInvesting.LastYearValue = localizedNotApplicableText;
                this.sbCashFlowFromFinancing.LastYearValue = localizedNotApplicableText;
            }
        }
        #endregion // UpdateSummaryBlocks

        #region UpdateTotalCashflowChartScrollPosition
        /// <summary>
        /// The Total Cashflow chart at the top is actually three charts. Whenever the scroll
        /// position changes, we need to update the WindowPositionHorizontal on each of the 3. 
        /// </summary>
        private void UpdateTotalCashflowChartScrollPosition()
        {
            // Calculate the new WindowPositionHorizontal based on the horizontal scale and the 
            // current value of the scrollbar. The position is a number from 0 to 1.
            double scale = 1.0 - MainForm.TotalCashflowChartHorizontalScale;
            double chartPosition = ((double)this.scrTotalCashflow.Value / (double)this.scrTotalCashflow.MaximumDragValue) * scale;

            // If the chart is too small (becuase the window has been resized), then 
            // there will be no valid position. So just bail out. 
            if (double.IsNaN(chartPosition))
                return;

            // Apply the posiiton to all three charts. 
            this.crtEndingCash.WindowPositionHorizontal = chartPosition;
            this.crtLabels.WindowPositionHorizontal = chartPosition;
            this.crtMonthlyTotals.WindowPositionHorizontal = chartPosition;

            // Force all three charts to refresh. This makes them scroll more smoothly and in synch. 
            this.ForceTotalCashChartRefresh();

            // Update the Date range label at the top of the form. 
            UpdateDateRangeLabel();
        }        
        #endregion // UpdateTotalCashflowChartScrollPosition

        #endregion //Methods

        #region Properties

        #region SelectedIndex
        /// <summary>
        /// This is the currently-selected month. This index is used to show the highlights 
        /// on the top chart(s) and also in the Overview charts for Inflow and Outflow.
        /// </summary>
        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            {
                if (this.selectedIndex == value)
                    return;

                this.selectedIndex = value;                
                
                // When the selectedIndex changes, force a repaint on the chart(s).
                this.ForceTotalCashChartRefresh();

                // Update the summary block (on the left). 
                this.UpdateSummaryBlocks();

                // Update the current Inflow and Outflow labels. 
                MonthlyData currentMonth = DataManager.MonthlyData[this.selectedIndex];
                this.lblTotalInflow.Text = Utilities.LocalizeString("InflowLabel_Text", Utilities.GetDisplayValueInMillions(currentMonth.Inflow, 1, CashflowDetails.MillionsSuffix));
                this.lblTotalOutflow.Text = Utilities.LocalizeString("OutflowLabel_Text", Utilities.GetDisplayValueInMillions(currentMonth.Outflow, 1, CashflowDetails.MillionsSuffix));
            }
        } 
        #endregion // SelectedIndex

        #endregion // Properties        

        #region Event Handlers

        #region endingCashChartNumericY_FormatLabel
        /// <summary>
        /// Handles FormatLabel of the NumericYAxis of this.crtEndingCash.
        /// </summary>
        string endingCashChartNumericY_FormatLabel(AxisLabelInfo info)
        {
            // The decimal value has a whole lot of zeroes, so format it to the nearest
            // million. 
            decimal value = (decimal)info.Value;
            return Utilities.GetDisplayValueInMillions(value, 0, CashflowDetails.MillionsSuffix);
        }  
        #endregion // endingCashChartNumericY_FormatLabel

        #region labelsChartCategoryX_FormatLabel
        /// <summary>
        /// Handles FormatLabel of the CategoryXAxis of this.crtLabels.
        /// </summary>
        string labelsChartCategoryX_FormatLabel(AxisLabelInfo info)
        {            
            // Calculate the net (inflow - outflow) value for the month.
            var monthlyData = (MonthlyData)info.Item;
            var net = monthlyData.Inflow - monthlyData.Outflow;

            // Format it to the nearest tenth of a million. 
            var text = Utilities.GetDisplayValueInMillions(net, 1);

            return text;
        }  
        #endregion // labelsChartCategoryX_FormatLabel

        #region monthlyTotalsChartCategoryX1_FormatLabel
        /// <summary>
        /// Handles FormatLabel of the CategoryXAxis1 of this.crtMonthlyTotals.
        /// </summary>
        string monthlyTotalsChartCategoryX1_FormatLabel(AxisLabelInfo info)
        {
            // By default, these labels would show the Month field, which is an integer. 
            // But we can do better. 

            // Get the Month and Year. 
            var monthlyData = (MonthlyData)info.Item;
            DateTime dateTime = new DateTime(monthlyData.Year, monthlyData.Month, 1);

            // For January, append the year. Since the chart is showing 13 months, at least one instance of
            // January will always be displayed, so the user will always have the year as context. 
            if (dateTime.Month == 1)
                return dateTime.ToString(Utilities.LocalizeString("Chart_XAxis_Month_Format_January")).ToLower();
            else
                return dateTime.ToString(Utilities.LocalizeString("Chart_XAxis_Month_Format")).ToLower();
        }
        #endregion // monthlyTotalsChartCategoryX1_FormatLabel

        #region monthlyTotalsChartNumericY_FormatLabel
        /// <summary>
        /// Handles FormatLabel of the NumericYAxis of this.crtMonthlyTotals.
        /// </summary>
        string monthlyTotalsChartNumericY_FormatLabel(AxisLabelInfo info)
        {
            // The decimal value has a whole lot of zeroes, so format it to the nearest
            // million. 
            decimal value = (decimal)info.Value;
            return Utilities.GetDisplayValueInMillions(value, 0, CashflowDetails.MillionsSuffix);
        }        
        #endregion // monthlyTotalsChartNumericY_FormatLabel       

        #region scrTotalCashflow_ValueChanged
        /// <summary>
        /// The Total Cashflow chart at the top is actually three charts. This scrollbar is used
        /// // to control the scrolling of all three. 
        /// </summary>
        private void scrTotalCashflow_ValueChanged(object sender, EventArgs e)
        {            
            this.UpdateTotalCashflowChartScrollPosition();
        } 
        #endregion // scrTotalCashflow_ValueChanged       

        #region crtMonthlyTotals_Resize
        private void crtMonthlyTotals_Resize(object sender, EventArgs e)
        {
            // We need to re-initialize the scrollbar any time the width of the chart changes
            // so that the thumb will be correctly sized.

            // Don't adjust the scrollbar when the form is minimized, since that will result in a
            // range of 0 and end up setting the Scrollbar's value to 0.
            if (this.WindowState == FormWindowState.Minimized)
                return;

            // Store the original position of the scrollbar.
            double originalScrollBarPosition = (double)this.scrTotalCashflow.Value / (double)this.scrTotalCashflow.MaximumDragValue;

            // largeChange is essentially 1 page (13 months) of data.
            int largeChange = this.crtMonthlyTotals.Width - MainForm.TotalCashflowChartYAxisLabelExtent;

            // The small change will be one month. 
            int smallChange = largeChange / MainForm.MonthsToDisplay;

            // Total area is the entire range the data, so it's smallChange times the total
            // number of months of data we have. 
            int totalArea = smallChange * (DataManager.NumberOfYears * 12);

            // maximumDragValue is totalArea minus one page, since there's no point in scrolling 
            // beyond the last data item. 
            int maximumDragValue = totalArea - largeChange;

            // Make sure none of the values are invalid
            maximumDragValue = Math.Max(maximumDragValue, 0);
            smallChange = Math.Max(smallChange, 0);
            largeChange = Math.Max(largeChange, 0);

            // Initialize the scrollbar.            
            this.scrTotalCashflow.Initialize(0, maximumDragValue, smallChange, largeChange);

            // Restore the position of the scrollbar to the same relative position
            this.scrTotalCashflow.Value = (int)Math.Ceiling(originalScrollBarPosition * maximumDragValue);
        } 
        #endregion // crtMonthlyTotals_Resize

        #region MainForm_Load
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Scroll to the end so we see the most recent data in the chart. 
            this.scrTotalCashflow.Value = this.scrTotalCashflow.MaximumDragValue;

            // Select the last month of data to start off. 
            this.UpdateMonthlyDetailsUI(DataManager.MonthlyData.Last());
        } 
        #endregion // MainForm_Load      

        #region crtTtalCashflow_MouseUp
        /// <summary>
        /// Handles the MouseUp event for all three of the chart that make up the TotalCashflow chart.
        /// Clicking on any one of these chart will "select" that month. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crtTtalCashflow_MouseUp(object sender, MouseEventArgs e)
        {
            // Determine which item was clicked
            var series = crtEndingCash.Series.OfType<CategorySeries>().First();
            var index = series.GetExactItemIndexFromSeriesPixel( new Infragistics.Win.DataVisualization.Point(e.X, 0) );
            index = Math.Round(index);

            // Set the selected month. 
            this.SelectedIndex = (int)index;

            // Update the monthly details UI. 
            MonthlyData monthlyData = DataManager.MonthlyData[this.SelectedIndex];
            if (null != monthlyData)
                this.UpdateMonthlyDetailsUI(monthlyData);
        } 
        #endregion // crtTtalCashflow_MouseUp
        
        #region crtTotalCashFlow_AfterPaint
        /// <summary>
        /// Draws an overlay around the selected month. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void crtTotalCashFlow_AfterPaint(object sender, PaintEventArgs e)
        {
            // Get the chart. Remember that there are three charts that make up the TotalCashflow
            // chart, so this could be any of the three. 
            var chart = (UltraDataChart)sender;

            // Get the current SelectedIndex. 
            int selectedIndex = this.SelectedIndex;

            // If there is no selected month, just bail out. Nothing to do. 
            if (selectedIndex == -1)
                return;

            // Get the Rect of the selected item. 
            var categoryAxis = chart.Axes.OfType<CategoryAxisBase>().First();
            var xPosition = categoryAxis.ScaleValue(selectedIndex);

            // if xPosition is left of the viewport, correct it to the left side of the viewport.
            // but only do this if the selected item is partially visible .. if it is < 1 item away from the viewport left, then it is partially visible.
            var axisVisibleMinimum = categoryAxis.GetUnscaledValue(chart.ViewportRect.Left, chart.WindowRect, chart.ViewportRect);
            if (xPosition < chart.ViewportRect.Left && axisVisibleMinimum - selectedIndex < 1)
            {
                xPosition = chart.ViewportRect.Left;
            }

            // intersect the category bounding box with the ViewportRect so the highlight rectangle does not paint outside the viewport.
            var itemRect = categoryAxis.GetCategoryBoundingBox(new Infragistics.Win.DataVisualization.Point(xPosition, 0), false, 0.0D);

            itemRect = itemRect.Intersect(chart.ViewportRect);

            // Extend the height of the rect to fill the entire height of the chart. 
            itemRect.Y = 0;
            itemRect.Height = chart.Height;
            
            // If the item rect is empty, there's nothing to draw. 
            if (itemRect.Width <= 0 || itemRect.Height <= 0)
                return;
            
            // Draw a semi-transparent overlay over the item. 
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(64, Color.White)))
                e.Graphics.FillRectangle(brush, itemRect);

            // Reduce the rect width and height by 1 so that when we draw the border, it's inside the chart. 
            itemRect.Width -= 1;
            itemRect.Height -= 1;

            Border3DSide borderSides;
            if (chart == this.crtEndingCash)
            {                
                // this.crtEndingCash is on top, so it needs left, top, and right borders.
                borderSides = Border3DSide.Left | Border3DSide.Top | Border3DSide.Right;
            }
            else if  (chart == this.crtLabels)
            {
                // this.crtLabels is in the middle, so it needs left and right borders.
                borderSides = Border3DSide.Left | Border3DSide.Right;                
            }
            else if (chart == this.crtMonthlyTotals)
            {
                // this.crtMonthlyTotals is on the bottom, so it needs a left, bottom, and right borders.
                borderSides = Border3DSide.Left | Border3DSide.Bottom | Border3DSide.Right;
            }
            else
            {
                Debug.Fail("Unknown chart control");
                return;
            }

            // Draw a solid border around the item. 
            Utilities.DrawBorder(e.Graphics, itemRect, borderSides, Pens.Black);
        } 
        #endregion // crtTotalCashFlow_AfterPaint               
       
        #endregion // Event Handlers              
    }
}

