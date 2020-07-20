using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.Misc;
using System.Diagnostics;
using Showcase.CashflowDashboard.Data;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.DataVisualization;

namespace Showcase.CashflowDashboard
{
    /// <summary>
    /// Base control for the Inflow and Outflow details
    /// </summary>
    abstract partial class CashflowDetails : UserControl
    {
        #region Private Members

        #region ChartType enum

        /// <summary>
        /// Enumeration of the two different chart types (tabs).
        /// </summary>
        private enum ChartType
        {
            Category,
            Overview
        }
        #endregion // ChartType enum 
        
        // Store the current month, last month, and last year data. 
        internal MonthlyData currentMonthData;
        internal MonthlyData lastMonthData;
        internal MonthlyData lastYearData;

        // Store references to some of the chart Axes / Series
        private CategoryYAxis categoryChartChartCategoryY;
        private StackedBarSeries categoryChartStackedBarSeries;        

        // Keep track of the currently selected item.
        private int selectedIndex;

        public static readonly string MillionsSuffix = Utilities.LocalizeString("MillionsSuffix");
        
        #endregion // Private Members

        #region Constructor
        /// <summary>
        /// Creates a new instance of the CashflowDetails control
        /// </summary>
        public CashflowDetails()
        {
            InitializeComponent();

            // Initialize the UI controls. 
            this.InitializeUI();
        }        
        #endregion // Constructor

        #region Private Methods

        #region ForceOverviewChartRefresh
        /// <summary>
        /// This method forces a refresh and repaint of the Overview chart.
        /// </summary>
        private void ForceOverviewChartRefresh()
        {
            this.crtOverview.Flush();
            this.crtOverview.Update();
        }
        #endregion // ForceOverviewChartRefresh        

        #region GetLegend
        /// <summary>
        /// Gets the legend image for the specified source. 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal Image GetLegend(Source source)
        {
            string legendResouceName = string.Format("{0}{1}Legend", source.ToString(), this.FlowDirection.ToString());
            CachedImages cachedImage = (CachedImages)Enum.Parse(typeof(CachedImages), legendResouceName);
            return Utilities.GetCachedImage(cachedImage);
        }  
        #endregion // GetLegend

        #region GetLocalizedGridHeaderCaption
        private string GetLocalizedGridHeaderCaption(string columnKey)
        {
            string resourceName = string.Format("CashflowDetails_Grid_Caption_{0}", columnKey);
            return Utilities.LocalizeString(resourceName);
        }
        #endregion // GetLocalizedGridHeaderCaption

        #region GetOrCreateColumn
        /// <summary>
        /// This is a convenient helper method to retrieve an unbound column from the grid. If the 
        /// column already exists, it's returned. If not, it is created and then returned. 
        /// </summary>
        /// <param name="band"></param>
        /// <param name="key"></param>
        /// <param name="dataType"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
		private static UltraGridColumn GetOrCreateUnboundColumn(UltraGridBand band, string key, Type dataType, string caption)
        {
            UltraGridColumn column;
            if (false == band.Columns.Exists(key))
            {
                column = band.Columns.Add(key);
                column.DataType = dataType;
                column.Header.Caption = caption;
            }
            else
                column = band.Columns[key];

            return column;
        } 
	    #endregion // GetOrCreateColumn

        #region GetValueColumnKey
        /// <summary>
        /// Returns the key of the Value column. The key will vary depending on whether this
        /// control is displaying Inflow or Outflow. 
        /// </summary>
        /// <returns></returns>
        private string GetValueColumnKey()
        {
            return string.Format("Actual{0}", this.FlowDirection.ToString());
        }  
        #endregion // GetValueColumnKey

        #region InitializeUI
        /// <summary>
        /// Initializes the control UI. 
        /// </summary>
        private void InitializeUI()
        {
            // Don't show a focus rect on the Category and Overview buttons. Or on the 
            // grid. 
            this.btnCategory.DrawFilter = new UIElementFilters.NoFocusRectDrawFilter();
            this.btnOverview.DrawFilter = new UIElementFilters.NoFocusRectDrawFilter();
            this.grdCashflowSources.DrawFilter = new UIElementFilters.NoFocusRectDrawFilter();

            // There are two charts (one on each tab). Initialize each one. 
            this.InitializeCategoryChart();
            this.InitializeOverviewChart();

            // Select the Category tab initially. 
            this.SelectTab(ChartType.Category);
        }
        #endregion // InitializeUI

        #region InitializeCategoryChart

        /// <summary>
        /// Initializes the Category chart. This chart shows a stack series that breaks down the 
        /// Inflow or outflow by source. 
        /// </summary>
        private void InitializeCategoryChart()
        {
            // Create a BindingList that contains last year, the current month, and last months data. 
            // This will be used as the DataSource for the chart so we can show one stack for each. 
            BindingList<MonthlyData> monthlyDatas = new BindingList<MonthlyData>() { this.lastYearData, this.currentMonthData, this.lastMonthData };

            // X Axis
            // The X Axis is pretty simple. It's the monetary value of the Inflow / Outflow. 
            //
            #region X Axis
            var categoryChartChartNumericX = new NumericXAxis();

            // Hook FormatLabel so that we can show the values in a more user-friendly format. 
            categoryChartChartNumericX.FormatLabel += new AxisFormatLabelHandler(categoryChartChartNumericX_FormatLabel);
            #endregion // X Axis

            // Y Axis
            // The Y Axis will display the "name" of the item: "last month", "current", or "last year".
            //
            #region Y Axis
            this.categoryChartChartCategoryY = new CategoryYAxis();
            this.categoryChartChartCategoryY.DataSource = monthlyDatas;
            this.categoryChartChartCategoryY.LabelFontFamily = "Verdana";
            this.categoryChartChartCategoryY.LabelHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Left;
            this.categoryChartChartCategoryY.LabelTextColor = new SolidColorBrush(Color.FromArgb(146, 146, 146));

            // This is arbitrary. We will format the label, anyway, so we could use any field here. 
            this.categoryChartChartCategoryY.Label = "Month";
            this.categoryChartChartCategoryY.Interval = 1;
            
            // Since the text we want to display doesn't exist as a property on the MonthlyData, we need
            // to hook LabelFormat and return the appropriate text. 
            this.categoryChartChartCategoryY.FormatLabel += new AxisFormatLabelHandler(categoryChartChartCategoryY_FormatLabel);
            #endregion // Y Axis

            // Add the axes to the chart. 
            this.crtCategory.Axes.Add(this.categoryChartChartCategoryY);
            this.crtCategory.Axes.Add(categoryChartChartNumericX);

            // StackedBarSeries
            // The way a StackedBarSeries works is that we create the series and then add
            // a StackedFragmentSeries for each source. 
            //
            #region StackedBarSeries
            
            // Create the StackedBarSeries itself. 
            this.categoryChartStackedBarSeries = new StackedBarSeries();

            // Set it's DataSource. 
            this.categoryChartStackedBarSeries.DataSource = monthlyDatas;

            // Set the X and Y axes. 
            this.categoryChartStackedBarSeries.XAxis = categoryChartChartNumericX;
            this.categoryChartStackedBarSeries.YAxis = categoryChartChartCategoryY;

            // Get a string representing whether we are showing inflow or outflow. 
            string flowDirectionString = this.FlowDirection.ToString();

            // Loop through each source.             
            Source[] sources = (Source[])Enum.GetValues(typeof(Source));
            foreach (Source source in sources)
            {
                // Create a new StackedFragmentSeries for this source. 
                var stackedFragmentSeriesForSource = new StackedFragmentSeries();
                stackedFragmentSeriesForSource.Title = source.ToString();
                stackedFragmentSeriesForSource.Outline = new SolidColorBrush(Color.Transparent);

                // Call into a method to get the appropriate color for the fragment. 
                stackedFragmentSeriesForSource.Brush = this.GetSourceChartColor(source);

                // The name of the property on the MonthlyData that we need to use is a combination
                // of the source and the flow direction. 
                stackedFragmentSeriesForSource.ValueMemberPath = string.Format("{0}{1}", source.ToString(), flowDirectionString);
                
                // Add the fragment to the series. 
                this.categoryChartStackedBarSeries.Series.Add(stackedFragmentSeriesForSource);
            } 
            #endregion // StackedBarSeries

            // Add the series to the chart. 
            this.crtCategory.Series.Add(this.categoryChartStackedBarSeries);
        } 
        #endregion // InitializeCategoryChart

        #region InitializeOverviewChart
        /// <summary>
        /// The overview chart shows the breakdown of inflow/outflow by source across the same
        /// date range as the TotalCashflow chart. 
        /// </summary>
        private void InitializeOverviewChart()
        {
            // Set the scale so that only one year of data is visible. 
            this.crtOverview.WindowScaleHorizontal = MainForm.TotalCashflowChartHorizontalScale;

            // X Axis
            // The X Axis is the month. 
            //
            #region X Axis
            var overviewChartCategoryX = new CategoryXAxis();
            overviewChartCategoryX.DataSource = DataManager.MonthlyData;
            overviewChartCategoryX.LabelFontFamily = "Verdana";
            overviewChartCategoryX.LabelFontSize = 10.0D;
            overviewChartCategoryX.LabelHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Left;
            overviewChartCategoryX.LabelTextColor = new SolidColorBrush(Color.FromArgb(146, 146, 146));
            overviewChartCategoryX.UseClusteringMode = true;

            // We are using the Month field for the label. But this is really irrelevant. We
            // are going to use the FormatLabel event to show a three-letter abbreviation for the month
            // instead of the value of the Month field (which is an integer from 1 to 12). 
            overviewChartCategoryX.Label = "Month";            

            // Explicitly set the Interval to 1 so we always show every month.
            overviewChartCategoryX.Interval = 1;

            // Hook FormatLabel so we can take the integer (Month) and display a more user-friendly
            // 3-letter abbreviation for the month.
            overviewChartCategoryX.FormatLabel += new AxisFormatLabelHandler(overviewChartCategoryX_FormatLabel);
            #endregion // X Axis

            // Y Axis
            // The Y Axis is the value of inflow/outflow for the source. 
            //
            #region Y Axis
            var overviewChartNumericY = new NumericYAxis();
            overviewChartNumericY.LabelFontFamily = "Verdana";
            overviewChartNumericY.LabelTextColor = new SolidColorBrush(Color.FromArgb(146, 146, 146));

            // Hook LabelFormat so we can format the numeric values into more user-friendly text. 
            overviewChartNumericY.FormatLabel += new AxisFormatLabelHandler(overviewChartNumericY_FormatLabel);
            #endregion // Y Axis           

            // Add the axes to the chart. 
            this.crtOverview.Axes.Add(overviewChartNumericY);
            this.crtOverview.Axes.Add(overviewChartCategoryX);

            // Area Series
            // We will add an area series to the chart for each source. 
            //
            #region Area Series

            // Get a string representing whether we are showing inflow or outflow. 
            string flowDirectionString = this.FlowDirection.ToString();

            // Loop through each source.
            Source[] sources = (Source[])Enum.GetValues(typeof(Source));
            foreach (Source source in sources)
            {
                // Create a new area series for this source.
                var areaSeries = new AreaSeries();

                areaSeries.DataSource = DataManager.MonthlyData;
                areaSeries.Title = source.ToString();
                areaSeries.Outline = new SolidColorBrush(Color.Transparent);
                areaSeries.Brush = this.GetSourceChartColor(source);

                // The name of the property on the MonthlyData that we need to use is a combination
                // of the source and the flow direction. 
                areaSeries.ValueMemberPath = string.Format("{0}{1}", source.ToString(), flowDirectionString);                

                // Set the axes on the series. 
                areaSeries.XAxis = overviewChartCategoryX;
                areaSeries.YAxis = overviewChartNumericY;

                // Add the series to the chart. 
                this.crtOverview.Series.Add(areaSeries);
            }             
            #endregion // Area Series
        }        
        #endregion // InitializeOverviewChart

        #region SelectTab
        /// <summary>
        /// Select either the Overview or Category tabs. 
        /// </summary>
        private void SelectTab(ChartType chartType)
        {
            // The user toggles between the tabs using these two buttons. 
            UltraButton activeButton;
            UltraButton otherButton;

            // Get the button they clicked and, by extension, the button they did not click. 
            switch (chartType)
            {
                case ChartType.Category:
                    activeButton = this.btnCategory;
                    otherButton = this.btnOverview;
                    break;
                case ChartType.Overview:
                    activeButton = this.btnOverview;
                    otherButton = this.btnCategory;
                    break;
                default:
                    Debug.Fail("Unknown ChartType");
                    return;
            }

            // Give the active button a white background to highlight it. 
            activeButton.Appearance.ImageBackground = Utilities.GetCachedImage(CachedImages.WhiteButtonTransparent);
            activeButton.Appearance.ForeColor = Color.FromArgb(102, 102, 102);

            // Give the other button no background, so it's de-emphasized. 
            otherButton.Appearance.ResetImageBackground();
            otherButton.Appearance.ForeColor = Color.FromArgb(154, 154, 154);

            // Select the appropriate tab. This tab is in Wizard mode, so it has no UI. That's
            // why we are using the buttons. 
            this.tabChartType.SelectedTab = this.tabChartType.Tabs[chartType.ToString()];
        } 
        #endregion // SelectTab        

        #endregion // Private Methods

        #region Public Methods

        #region UpdateData
        /// <summary>
        /// This method is called when a new month is selected in the TotalCashflow chart on the
        /// main form. 
        /// </summary>
        /// <param name="selectedIndex">The index of the "selected" month. This is so we can select the same month in the Overview chart.</param>
        /// <param name="scrollPositionOfOverviewChart">The scroll position of the TotalCashflowChart so we can synchronize the scrolling of the Overview chart.</param>
        public void UpdateData(int selectedIndex, double scrollPositionOfOverviewChart)
        {
            // Set the SelectedIndex. We need to store this here so that we can draw an overlay
            // on the selected item when the Overview chart paints. 
            this.SelectedIndex = selectedIndex;

            // Get the month data for the current month, previous month, and previous year and
            // cache these values. 
            this.currentMonthData = DataManager.MonthlyData[this.SelectedIndex];
            DateTime current = new DateTime(currentMonthData.Year, currentMonthData.Month, 1);

            DateTime previousMonth = current.AddMonths(-1);
            this.lastMonthData = DataManager.GetMonthlyData(previousMonth.Month, previousMonth.Year);

            DateTime previousYear = current.AddYears(-1);
            this.lastYearData = DataManager.GetMonthlyData(previousYear.Month, previousYear.Year);

            // Check to see if the MonthlyData.Activities have changed. 
            if (this.grdCashflowSources.DataSource != currentMonthData.Activities)
            {
                // Bind the grid to the Activities of the current month data. 
                this.grdCashflowSources.SetDataBinding(currentMonthData.Activities, null);

                // Autosize the grid columns so that all of the data fits. 
                this.grdCashflowSources.DisplayLayout.PerformAutoResizeColumns(false, PerformAutoSizeType.AllRowsInBand, AutoResizeColumnWidthOptions.All);

                // Create a BindingList that contains last year, the current month, and last months data. 
                // This will be used as the DataSource for the chart so we can show one stack for each. 
                BindingList<MonthlyData> monthlyDatas = new BindingList<MonthlyData>() { this.lastYearData, this.currentMonthData, this.lastMonthData };

                // Update the data source of the Category Chart Y Axis and StackedBarSeries. 
                this.categoryChartChartCategoryY.DataSource = monthlyDatas;
                this.categoryChartStackedBarSeries.DataSource = monthlyDatas;

                // Set the scroll position of the Overview chart. 
                this.crtOverview.WindowPositionHorizontal = scrollPositionOfOverviewChart;
            }

            // Force the overview chart to refresh.
            this.ForceOverviewChartRefresh();            
        }
        #endregion // UpdateData 

        #endregion // Public Methods

        #region Abstract Members

        #region FlowDirection
        /// <summary>
        /// Returns the FlowDirection of this control (Inflow or Outflow)
        /// </summary>
        internal abstract FlowDirection FlowDirection
        {
            get;
        }       
        #endregion // FlowDirection 

        #region GetGridRowValues
        /// <summary>
        /// Returns the specified values for use in the grid.
        /// </summary>
        /// <param name="activity">The activity from which to retrieve the value</param>
        /// <param name="lastMonth">The value (inflow or outflow) of the previous month.</param>
        /// <param name="lastYear">The value (inflow or outflow) of the previous year.</param>
        /// <param name="value">The value (inflow or outflow) of the current month</param>
        /// <param name="projected">The projected value (inflow or outflow) of the current month.</param>
        /// <param name="legend">The legend image for the source of the activity.</param>
        internal abstract void GetGridRowValues(Showcase.CashflowDashboard.Data.Activity activity, ref decimal? lastMonth, ref decimal? lastYear, out decimal value, out decimal projected, out Image legend);
        #endregion // GetGridRowValues

        #region GetSourceChartColor
        /// <summary>
        /// Returns the color which should be used in the chart series for the specified source.
        /// </summary>
        internal abstract Color GetSourceChartColor(Source source); 
        #endregion // GetSourceChartColor

        #endregion // Abstract Members

        #region Internal Properties

        #region SelectedIndex
        /// <summary>
        /// This is the currently-selected month. This index is used to show the highlights 
        /// on the top chart(s) and also in the Overview charts for Inflow and Outflow.
        /// </summary>
        internal int SelectedIndex
        {
            get { return this.selectedIndex; }
            set
            {
                if (this.selectedIndex == value)
                    return;

                this.selectedIndex = value;               
            }
        }
        #endregion // SelectedIndex

        #endregion // Internal Properties

        #region Event Handlers

        #region btnChartType_Click
        /// <summary>
        /// Handles the click event of both this.btnOverview and this.btnCategory. This allows the 
        /// user to choose which chart is displayed. 
        /// </summary>
        private void btnChartType_Click(object sender, EventArgs e)
        {            
            // Select the appropriate tab based on which button was clicked. 
            // The tab control is in Wizard mode, so there is no UI. So these two buttons
            // serve as the only way for the user to change tabs. 
            //
            UltraButton button = (UltraButton)sender;
            if (button == this.btnOverview)
                this.SelectTab(ChartType.Overview);
            else
                this.SelectTab(ChartType.Category);
        }
        #endregion // btnChartType_Click

        #region categoryChartChartCategoryY_FormatLabel
        /// <summary>
        /// Handles FormatLabel of the CategoryYAxis of this.crtCategory.
        /// </summary>
        string categoryChartChartCategoryY_FormatLabel(AxisLabelInfo info)
        {
            // This chart is bound to a BindingList of MonthlyData objects. These don't have
            // names, and even if they did, the names wouldn't be relative to the current month.
            // So here we will determine which Monthlydata is being displayed and give it
            // a user-friendly label. 
            var monthlyData = (MonthlyData)info.Item;

            // If the monthlyData is null, then this is either the previous month or previous
            // year. We cannot tell which. So just display a generic message of 'no previous data'.
            if (null == monthlyData)
                return Utilities.LocalizeString("CashflowDetails_Category_Label_noPreviousData");

            if (monthlyData == this.currentMonthData)
                return Utilities.LocalizeString("CashflowDetails_Category_Label_current");
            else if (monthlyData == this.lastMonthData)
                return Utilities.LocalizeString("CashflowDetails_Category_Label_lastmonth");
            else if (monthlyData == this.lastYearData)
                return Utilities.LocalizeString("CashflowDetails_Category_Label_lastyear");
                        
            Debug.Fail("Unknown data item");
            return info.Value.ToString();
        }         
        #endregion // categoryChartChartCategoryY_FormatLabel

        #region categoryChartChartNumericX_FormatLabel
        /// <summary>
        /// Handles FormatLabel of the NumericXAxis of this.crtCategory.
        /// </summary>
        string categoryChartChartNumericX_FormatLabel(AxisLabelInfo info)
        {
            // Format the value in millions. 
            return Utilities.GetDisplayValueInMillions((decimal)info.Value, 0, CashflowDetails.MillionsSuffix);
        }
        #endregion // categoryChartChartNumericX_FormatLabel

        #region grdCashflowSources_InitializeLayout
        /// <summary>
        /// Handles the InitializeLayot of the grid. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdCashflowSources_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // Store some useful variable refernences. 
            UltraGridLayout layout = e.Layout;
            UltraGridBand band = layout.Bands[0];
            UltraGridOverride ov = layout.Override;

            // This grid needs no selection.
            ov.HeaderClickAction = HeaderClickAction.Default;
            ov.SelectTypeCell = SelectType.None;
            ov.SelectTypeRow = SelectType.None;
            ov.SelectTypeCol = SelectType.None;
            ov.CellClickAction = CellClickAction.CellSelect;            

            // No row selectors or borders. 
            ov.RowSelectors = DefaultableBoolean.False;
            ov.BorderStyleCell = UIElementBorderStyle.None;
            ov.BorderStyleRow = UIElementBorderStyle.None;
            ov.BorderStyleHeader = UIElementBorderStyle.None;

            // Set the BackColor and ForeColor of the row so that the rows look like the same
            // color as the background. 
            ov.RowAppearance.BackColor = Color.FromArgb(235, 233, 234);
            ov.RowAppearance.ForeColor = Color.FromArgb(154, 154, 154);
            
            // Set up the header colors so that like the rows, the headers also blend
            // into the background. 
            ov.HeaderAppearance.BackColor = Color.FromArgb(235, 233, 234);
            ov.HeaderAppearance.ForeColor = Color.FromArgb(102, 102, 102);
            ov.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

            // Align the text in all cells and headers to the left. 
            ov.CellAppearance.TextHAlign = HAlign.Left;
            ov.HeaderAppearance.TextHAlign = HAlign.Left;

            // Put a little space in between rows. 
            ov.RowSpacingBefore = 4;

            // Don't let the user change anything. 
            ov.AllowUpdate = DefaultableBoolean.False;
            ov.AllowDelete = DefaultableBoolean.False;
            ov.AllowColMoving = AllowColMoving.NotAllowed;

            // The data source has a whole bunch of fields in it and we only want to display 
            // a small few. So start off hiding them all, and then we will make the ones we want visible. 
            foreach (UltraGridColumn column in band.Columns)
                column.Hidden = true;

            // Label Column
            // This column shows the legend. 
            //
            #region Label Column
            UltraGridColumn labelColumn = CashflowDetails.GetOrCreateUnboundColumn(band, "label", typeof(Image), null);
            labelColumn.Header.VisiblePosition = 0;
            labelColumn.Header.Caption = this.GetLocalizedGridHeaderCaption(labelColumn.Key);
            labelColumn.Hidden = false; 
            #endregion // Label Column

            // Source Column
            // This column shows the source of the inflow / outflow. 
            //
            #region Source Column
            UltraGridColumn sourceColumn = band.Columns["Source"];
            sourceColumn.Header.VisiblePosition = 1;
            sourceColumn.Header.Caption = this.GetLocalizedGridHeaderCaption(sourceColumn.Key);
            sourceColumn.Hidden = false;

            // Loop through each source.             
            Source[] sources = (Source[])Enum.GetValues(typeof(Source));
            ValueList sourcesValueList = new ValueList();
            foreach (Source source in sources)
            {
                string resourceName = string.Format("Source_Text_{0}", source.ToString());
                sourcesValueList.ValueListItems.Add(source, Utilities.LocalizeString(resourceName));
            }
            sourceColumn.ValueList = sourcesValueList;
            #endregion // Source Column

            // Value Column
            // Show the value (inflow or outflow) for the current selected month. 
            //
            #region Value Column
            // The key of the value column will vary depending on whether this is Inflow or Outflow
            // so get the key and store it here.
            string valueColumnKey = this.GetValueColumnKey();

            UltraGridColumn valueColumn = band.Columns[valueColumnKey];
            valueColumn.Header.VisiblePosition = 2;
            valueColumn.Header.Caption = this.GetLocalizedGridHeaderCaption("value");            
            valueColumn.Hidden = false;

            // Give this column a nice currently format. 
            valueColumn.Format = "c0"; 
            #endregion // Value Column

            // LastMonth column
            // This column shows the value from last month and also a little indicator and a percentage
            // of how much we went up or down from the previous month. 
            //
            #region LastMonth Column
            UltraGridColumn lastMonthColumn = CashflowDetails.GetOrCreateUnboundColumn(band, "last month", typeof(string), null);
            lastMonthColumn.Header.VisiblePosition = 3;
            lastMonthColumn.Header.Caption = this.GetLocalizedGridHeaderCaption(lastMonthColumn.Key);
            lastMonthColumn.Hidden = false;
            #endregion // LastMonth Column

            // LastYear column
            // This column shows the value from last year and also a little indicator and a percentage
            // of how much we went up or down from the previous year. 
            //
            #region LastYear Column
            UltraGridColumn lastYearColumn = CashflowDetails.GetOrCreateUnboundColumn(band, "last year", typeof(string), null);
            lastYearColumn.Header.VisiblePosition = 4;
            lastYearColumn.Header.Caption = this.GetLocalizedGridHeaderCaption(lastYearColumn.Key);
            lastYearColumn.Hidden = false; 
            #endregion // LastYear Column

            // Projected column
            // This column shows the project value (inflow or outflow) for the selected month and 
            // also a little indicator and a percentage of how the actual value differs from the
            // projection
            //
            #region Projected column
            UltraGridColumn projectedColumn = CashflowDetails.GetOrCreateUnboundColumn(band, "projected", typeof(string), null);
            projectedColumn.Header.VisiblePosition = 5;
            projectedColumn.Header.Caption = this.GetLocalizedGridHeaderCaption(projectedColumn.Key);
            projectedColumn.Hidden = false; 
            #endregion // Projected column

            // Make the visible columns fill the available width of the grid. 
            layout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
        }        
        #endregion // grdCashflowSources_InitializeLayout

        #region grdCashflowSources_InitializeRow
        /// <summary>
        ///  Handles the InitializeRow of the grid. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdCashflowSources_InitializeRow(object sender, InitializeRowEventArgs e)
        {            
            // If there's no current month data, yet, just bail out. 
            if (null == this.currentMonthData)
                return;

            // Each row in the grid is an Activity. Get the Activity for the row. 
            Showcase.CashflowDashboard.Data.Activity activity = (Showcase.CashflowDashboard.Data.Activity)e.Row.ListObject;
            
            // Get the values we need to fill up the unbound columns in the grid.
            // This is done with a call to an absract method so that the InflowDetails and 
            // Outflow Details can return the appropriate values. 
            //
            decimal value;
            decimal projected;
            decimal? lastMonth = null;
            decimal? lastYear = null;
            Image legend;
            this.GetGridRowValues(activity, ref lastMonth, ref lastYear, out value, out projected, out legend);

            // Populate the grid cells with the calculated values. 
            //
            e.Row.Cells["projected"].Value= Utilities.GetComparisonString(value, projected);

            if (null != lastMonth)
                e.Row.Cells["last month"].Value = Utilities.GetComparisonString(value, lastMonth.Value);
            else
                e.Row.Cells["last month"].Value = Utilities.LocalizeString("CashflowDetails_Category_Label_noPreviousData");

            if (null != lastYear)
                e.Row.Cells["last year"].Value = Utilities.GetComparisonString(value, lastYear.Value);
            else
                e.Row.Cells["last year"].Value = Utilities.LocalizeString("CashflowDetails_Category_Label_noPreviousData");

            e.Row.Cells["label"].Value = legend;
        }        
        #endregion // grdCashflowSources_InitializeRow        

        #region overviewChartCategoryX_FormatLabel
        /// <summary>
        /// Handles FormatLabel of the CategoryXAxis of this.crtOverview.
        /// </summary>
        string overviewChartCategoryX_FormatLabel(AxisLabelInfo info)
        {
            // Instead of an integer for the month, display a user-friendly 3-letter 
            // abbreviation. 
            var monthlyData = (MonthlyData)info.Item;
            DateTime dateTime = new DateTime(monthlyData.Year, monthlyData.Month, 1);
            string monthName = dateTime.ToString(Utilities.LocalizeString("Chart_XAxis_Month_Format")).ToLower();
            return monthName;
        }
        #endregion // overviewChartCategoryX_FormatLabel

        #region overviewChartNumericY_FormatLabel
        /// <summary>
        /// Handles FormatLabel of the NumericXAxis of this.crtOverview.
        /// </summary>
        string overviewChartNumericY_FormatLabel(AxisLabelInfo info)
        {
            // Format the value in millions. 
            return Utilities.GetDisplayValueInMillions((decimal)info.Value, 1, CashflowDetails.MillionsSuffix);
        }
        #endregion // overviewChartNumericY_FormatLabel

        #region crtOverview_AfterPaint
        private void crtOverview_AfterPaint(object sender, PaintEventArgs e)
        {
            // Get the chart.
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

            xPosition = Math.Max(chart.ViewportRect.Left, xPosition);

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

            // Draw a solid border around the item. 
            Utilities.DrawBorder(e.Graphics, itemRect, Border3DSide.All, Pens.Black);
        } 
        #endregion // crtOverview_AfterPaint

        #endregion // Event Handlers
    }
}
