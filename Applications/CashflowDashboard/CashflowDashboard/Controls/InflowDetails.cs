using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Showcase.CashflowDashboard.Data;
using System.Diagnostics;
using System.Drawing;

namespace Showcase.CashflowDashboard
{
    /// <summary>
    /// CashFlowDetails for Inflow. 
    /// </summary>
    internal class InflowDetails : CashflowDetails
    {
        #region FlowDirection
        /// <summary>
        /// Returns the FlowDirection of this control (Inflow or Outflow)
        /// </summary>
        internal override FlowDirection FlowDirection
        {
            get { return CashflowDashboard.FlowDirection.Inflow; }
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
                    return Color.FromArgb(31, 57, 82);

                case Source.Investing:
                    return Color.FromArgb(56, 106, 155);

                case Source.Financing:
                    return Color.FromArgb(80, 143, 196);

                case Source.Other:
                    return Color.FromArgb(131, 193, 250);

                default:
                    Debug.Fail("Unknown Source");
                    return Color.FromArgb(31, 57, 82);
            }
        } 
        #endregion // GetSourceChartColor

        #region GetGridRowValues
        /// <summary>
        /// Returns the specified values for use in the grid.
        /// </summary>
        /// <param name="activity">The activity from which to retrieve the value</param>
        /// <param name="lastMonth">The inflow value of the previous month.</param>
        /// <param name="lastYear">The inflow value of the previous year.</param>
        /// <param name="value">The inflow value of the current month</param>
        /// <param name="projected">The projected inflow value of the current month.</param>
        /// <param name="legend">The legend image for the source of the activity.</param>
        internal override void GetGridRowValues(Showcase.CashflowDashboard.Data.Activity activity, ref decimal? lastMonth, ref decimal? lastYear, out decimal value, out decimal projected, out Image legend)
        {
            value = activity.ActualInflow;
            projected = activity.ProjectedInflow;

            if (null != this.lastMonthData)
                lastMonth = this.lastMonthData.GetInflow(activity.Source);

            if (null != this.lastYearData)
                lastYear = this.lastYearData.GetInflow(activity.Source);

            legend = this.GetLegend(activity.Source);            
        }        
        #endregion // GetGridRowValues
    }
}
