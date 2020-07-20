using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win.UltraWinSchedule.DayView;
using Infragistics.Win;
using System.Windows.Forms;

namespace Showcase.InventoryManagement
{
    internal class DayViewUIFilter : Infragistics.Win.IUIElementCreationFilter, Infragistics.Win.IUIElementDrawFilter
    {

        #region IUIElementCreationFilter

        #region AfterCreateChildElements
        /// <summary>
        /// Called after an element's ChildElements have been
        /// created. The child element's can be repositioned here
        /// and/or new element's can be added.
        /// </summary>
        /// <param name="parent">The <see cref="T:Infragistics.Win.UIElement" /> whose child elements have been created/positioned.</param>
        public void AfterCreateChildElements(Infragistics.Win.UIElement parent)
        {
            if (parent is DayUIElement)
            {
                List<UIElement> elementsToRemove = new List<UIElement>();
                foreach (UIElement childElement in parent.ChildElements)
                {
                    if (childElement is AppointmentSelectedUIElement)
                        elementsToRemove.Add(childElement);
                }

                foreach (UIElement element in elementsToRemove)
                {
                    parent.ChildElements.Remove(element);
                    element.Dispose();
                }

                return;
            }

            MoreItemsIndicatorUIElement moreItemsElement = parent as MoreItemsIndicatorUIElement;
            if (moreItemsElement != null)
            {
                moreItemsElement.Image = (moreItemsElement.Up)
                    ? Properties.Resources.MoreActivityIndicator_Up
                    : Properties.Resources.MoreActivityIndicator_Down;
                return;
            }
        }

        #endregion //AfterCreateChildElements

        #region BeforeCreateChildElements

        /// <summary>
        /// Called before child elements are to be created/positioned.
        /// This is called during a draw operation for an element
        /// whose ChildElementsDirty is set to true. Returning true from
        /// this method indicates that the default creation logic
        /// should be bypassed.
        /// </summary>
        /// <param name="parent">The <see cref="T:Infragistics.Win.UIElement" /> whose child elements are going to be created/positioned.</param>
        /// <returns>
        /// True if the default creation logic should be bypassed.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool BeforeCreateChildElements(Infragistics.Win.UIElement parent)
        {
            return false;
        }

        #endregion //BeforeCreateChildElements

        #endregion //IUIElementCreationFilter

        #region IUIElementDrawFilter

        #region DrawElement

        /// <summary>
        /// Draws the element.
        /// </summary>
        /// <param name="drawPhase">The draw phase.</param>
        /// <param name="drawParams">The draw parameters.</param>
        /// <returns></returns>
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {

            UIElement element = drawParams.Element;
            if (drawPhase == DrawPhase.BeforeDrawBorders)
            {
                Border3DSide sides = (Border3DSide)0;
                if (element is DayHeaderAreaUIElement)
                    sides = Border3DSide.Top | Border3DSide.Bottom;
                else if (element is DayHeaderUIElement)
                    sides = Border3DSide.Bottom;

                if (sides != 0)
                {
                    if ((sides & Border3DSide.Top) != 0)
                        drawParams.DrawBorders(UIElementBorderStyle.Solid, Border3DSide.Top);

                    if ((sides & Border3DSide.Bottom) != 0)
                        drawParams.DrawBorders(UIElementBorderStyle.TwoColor, Border3DSide.Bottom);
                    return true;
                }
            }
            return false;
        }

        #endregion // DrawElement

        #region GetPhasesToFilter

        /// <summary>
        /// Called before each element is about to be drawn.
        /// </summary>
        /// <param name="drawParams">The <see cref="T:Infragistics.Win.UIElementDrawParams" /> used to provide rendering information.</param>
        /// <returns>
        /// Bit flags indicating which phases of the drawing operation to filter. The DrawElement method will be called only for those phases.
        /// </returns>
        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            UIElement element = drawParams.Element;
            if (element is DayHeaderAreaUIElement ||
                element is DayHeaderUIElement)
                return DrawPhase.BeforeDrawBorders;
            return DrawPhase.None;
        }

        #endregion //GetPhasesToFilter

        #endregion //IUIElementDrawFilter
    }
}
