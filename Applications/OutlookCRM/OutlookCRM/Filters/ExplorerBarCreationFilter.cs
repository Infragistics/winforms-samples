using Infragistics.Win;
using Infragistics.Win.UltraWinExplorerBar;
using System.Drawing;

namespace OutlookCRM.Filters
{
    public class ExplorerBarCreationFilter : IUIElementCreationFilter
    {
        #region IUIElementCreationFilter

        #region AfterCreateChildElements
        void IUIElementCreationFilter.AfterCreateChildElements(UIElement parent)
        {
            // Remove the splitter ui element
            if (parent is GroupAreaUIElement)
            {
                GroupUIElement expandedGroupUI = parent.GetDescendant(typeof(GroupUIElement)) as GroupUIElement;
                NavigationSplitterBarUIElement splitterUI = parent.GetDescendant(typeof(NavigationSplitterBarUIElement)) as NavigationSplitterBarUIElement;

                if (expandedGroupUI != null && splitterUI != null)
                {
                    parent.ChildElements.Remove(splitterUI);
                    expandedGroupUI.Rect = new Rectangle(expandedGroupUI.Rect.X, expandedGroupUI.Rect.Y, expandedGroupUI.Rect.Width, expandedGroupUI.Rect.Height + splitterUI.Rect.Height);

                    splitterUI.Dispose();
                }
            }
        }
        #endregion // AfterCreateChildElements

        #region BeforeCreateChildElements
        bool IUIElementCreationFilter.BeforeCreateChildElements(UIElement parent)
        {
            return false;
        }
        #endregion // BeforeCreateChildElements

        #endregion // IUIElementCreationFilter
    }
}
