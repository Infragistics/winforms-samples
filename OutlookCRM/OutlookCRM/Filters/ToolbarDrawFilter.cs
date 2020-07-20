using Infragistics.Win;
using Infragistics.Win.UltraWinToolbars;
using System.Windows.Forms;

namespace OutlookCRM.Filters
{
    public class ToolbarDrawFilter : IUIElementDrawFilter
    {
        #region DrawElement
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            // Draw a custom border around ButtonToolUIElements
            ButtonToolUIElement buttonToolUiElement = drawParams.Element as ButtonToolUIElement;
            if (buttonToolUiElement != null && drawPhase == DrawPhase.BeforeDrawBorders)
            {
                drawParams.DrawBorders(UIElementBorderStyle.Solid, Border3DSide.All);
                return true;
            }
            return false;
        }
        #endregion // DrawElement

        #region GetPhasesToFilter
        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (drawParams.Element is ButtonToolUIElement)
            {
                return DrawPhase.BeforeDrawBorders;
            }
            return DrawPhase.None;
        }
        #endregion // GetPhasesToFilter
    }
}
