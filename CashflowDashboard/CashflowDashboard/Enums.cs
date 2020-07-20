using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Showcase.CashflowDashboard
{
    #region FlowDirection

    /// <summary>
    /// Enumeration of the direction in which money can flow. 
    /// </summary>
    public enum FlowDirection
    {
        Inflow,
        Outflow
    } 
    #endregion // FlowDirection

    #region CachedImages 

    /// <summary>
    /// Enum used for accessing the cached images resources.
    /// </summary>
    internal enum CachedImages
    {
        WhiteButtonTransparent,  
        OperationsInflowLegend,
        OperationsOutflowLegend,
        InvestingInflowLegend,
        InvestingOutflowLegend,
        FinancingInflowLegend,
        FinancingOutflowLegend,
        OtherInflowLegend,
        OtherOutflowLegend,
    } 

    #endregion CachedImages
}
