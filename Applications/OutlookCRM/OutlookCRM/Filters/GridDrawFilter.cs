using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace OutlookCRM.Filters
{
    public class GridDrawFilter : IUIElementDrawFilter
    {
        #region DrawElement
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            // Custom draw borders around the column headers.
            HeaderUIElement headerUiElement = drawParams.Element as HeaderUIElement;
            if (headerUiElement != null && drawPhase == DrawPhase.BeforeDrawBorders)
            {
                drawParams.DrawBorders(headerUiElement.BorderStyle, Border3DSide.Bottom);
                return true;
            }

            // Remove the focus rectangle.
            if (drawPhase == DrawPhase.BeforeDrawFocus)
            {
                return true;
            }

            return false;
        }
        #endregion // DrawElement

        #region GetPhasesToFilter
        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (drawParams.Element is HeaderUIElement)
            {
                return DrawPhase.BeforeDrawBorders;
            }
            return DrawPhase.BeforeDrawFocus;
        }
        #endregion // GetPhasesToFilter

    }
}
