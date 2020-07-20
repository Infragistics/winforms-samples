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
    /// This class will draw a border on an UltraPanel on each side in the specified colors.
    /// </summary>
    /// <remarks>
    /// This is mainly used to provide a sort've 3D separator between two adjacents panels. One draws a 
    /// dark border on one side and the other draws a light border on the adjacent side. 
    /// </remarks>
    internal class UltraPanelBorderDrawFilter 
        : IUIElementDrawFilter
    {
        Color? borderColorLeft;
        Color? borderColorRight;
        Color? borderColorTop;
        Color? borderColorBottom;

        internal UltraPanelBorderDrawFilter(
            Color? borderColorLeft = null,
            Color? borderColorRight = null,
            Color? borderColorTop = null,
            Color? borderColorBottom = null)
        {
            this.borderColorLeft = borderColorLeft;
            this.borderColorRight = borderColorRight;
            this.borderColorTop = borderColorTop;
            this.borderColorBottom = borderColorBottom;
        }

        private void DrawBorder(ref UIElementDrawParams drawParams, Border3DSide borderSide, Color? color)
        {
            if (null == color)
                return;

            drawParams.DrawBorders(UIElementBorderStyle.Solid, borderSide, color.Value, drawParams.Element.Rect, drawParams.ElementDrawingClipRect);
        }

        bool IUIElementDrawFilter.DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            this.DrawBorder(ref drawParams, Border3DSide.Left, this.borderColorLeft);
            this.DrawBorder(ref drawParams, Border3DSide.Right, this.borderColorRight);
            this.DrawBorder(ref drawParams, Border3DSide.Top, this.borderColorTop);
            this.DrawBorder(ref drawParams, Border3DSide.Bottom, this.borderColorBottom);
            return false;
        }

        DrawPhase IUIElementDrawFilter.GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (drawParams.Element is UltraPanelClientAreaUIElement)
                return DrawPhase.BeforeDrawBorders;

            return DrawPhase.None;
        }
    }
}
