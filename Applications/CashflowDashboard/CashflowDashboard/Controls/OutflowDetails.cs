using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Showcase.CashflowDashboard.Data;
using System.Drawing;
using System.Diagnostics;

namespace Showcase.CashflowDashboard
{
    /// <summary>
    /// CashFlowDetails for Outflow.
    /// </summary>
    internal class OutflowDetails : CashflowDetails
    {
        #region FlowDirection
        /// <summary>
        /// Returns the FlowDirection of this control (Inflow or Outflow)
        /// </summary>
        internal override FlowDirection FlowDirection
        {
            get { return CashflowDashboard.FlowDirection.Outflow; }
        } 
        #endregion // FlowDirection

        #region GetSourceChartColor
        /// <summary>
        /// Returns the color which should be used in the chart series for the specified source.
        /// </summary>
        internal override System.Drawing.Color GetSourceChartColor(Data.Source source)
        {
            switch (source)
            {
                case Source.Operations:
                    return Color.FromArgb(228, 93, 71);

                case Source.Investing:
                    return Color.FromArgb(239, 122, 52);

                case Source.Financing:
                    return Color.FromArgb(238, 168, 36);

                case Source.Other:
                    return Color.FromArgb(245, 209, 87);

                default:
                    Debug.Fail("Unknown Source");
                    return Color.FromArgb(228, 93, 71);
            }
        } 
        #endregion // GetSourceChartColor

        #region GetGridRowValues
        /// <summary>
        /// Returns the specified values for use in the grid.
        /// </summary>
        /// <param name="activity">The activity from which to retrieve the value</param>
        /// <param name="lastMonth">The outflow value of the previous month.</param>
        /// <param name="lastYear">The outflow value of the previous year.</param>
        /// <param name="value">The outflow value of the current month</param>
        /// <param name="projected">The projected outflow value of the current month.</param>
        /// <param name="legend">The legend image for the source of the activity.</param>
        internal override void GetGridRowValues(Showcase.CashflowDashboard.Data.Activity activity, ref decimal? lastMonth, ref decimal? lastYear, out decimal value, out decimal projected, out Image legend)
        {
            value = activity.ActualOutflow;
            projected = activity.ProjectedOutflow;

            if (null != this.lastMonthData)
                lastMonth = this.lastMonthData.GetOutflow(activity.Source);

            if (null != this.lastYearData)
                lastYear = this.lastYearData.GetOutflow(activity.Source);

            legend = this.GetLegend(activity.Source);
        } 
        #endregion // GetGridRowValues
    }
}

