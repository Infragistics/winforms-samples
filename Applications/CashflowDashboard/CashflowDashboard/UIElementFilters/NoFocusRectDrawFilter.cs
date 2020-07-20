using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win;
using Infragistics.Win.Misc;
using System.Windows.Forms;
using System.Drawing;

namespace Showcase.CashflowDashboard.UIElementFilters
{
    /// <summary>
    /// This class prevent the control from drawing a focus rectangle. 
    /// </summary>
    internal class NoFocusRectDrawFilter 
        : IUIElementDrawFilter
    {
        bool IUIElementDrawFilter.DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            // Return tru to tell the control that all of the drawing for this phase is complete
            // and the control should not draw anything. 
            return true;
        }

        DrawPhase IUIElementDrawFilter.GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            return DrawPhase.BeforeDrawFocus;
        }
    }
}
