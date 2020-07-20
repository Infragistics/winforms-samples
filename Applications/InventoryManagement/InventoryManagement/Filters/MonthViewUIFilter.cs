using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win;
using System.Windows.Forms;
using Infragistics.Win.UltraWinSchedule.MonthViewSingle;

namespace Showcase.InventoryManagement
{
    internal class MonthViewUIFilter : IUIElementDrawFilter, IUIElementCreationFilter
    {

        #region IUIElementCreationFilter

        #region AfterCreateChildElements

        /// <summary>
        /// Called after an element's ChildElements have been
        /// created. The child element's can be repositioned here
        /// and/or new element's can be added.
        /// </summary>
        /// <param name="parent">The <see cref="T:Infragistics.Win.UIElement" /> whose child elements have been created/positioned.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void AfterCreateChildElements(UIElement parent)
        {
            if (parent is MoreActivityIndicatorUIElement)
            {       
                ImageUIElement imageElement = parent.GetDescendant(typeof(ImageUIElement)) as ImageUIElement;
                if (imageElement != null)
                    imageElement.Image = Properties.Resources.MoreActivityIndicator_Down;
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
        public bool BeforeCreateChildElements(UIElement parent)
        {
            return false;
        }

        #endregion //BeforeCreateChildElements

        #endregion //IUIElementCreationFilter

        #region IUIElementDrawFilter

        #region DrawElement

        /// <summary>
        /// Called during the drawing operation of a UIElement for a specific phase
        /// of the operation. This will only be called for the phases returned
        /// from the GetPhasesToFilter method.
        /// </summary>
        /// <param name="drawPhase">Contains a single bit which identifies the current draw phase.</param>
        /// <param name="drawParams">The <see cref="T:Infragistics.Win.UIElementDrawParams" /> used to provide rendering information.</param>
        /// <returns>
        /// Returning true from this method indicates that this phase has been handled and the default processing should be skipped.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            UIElement element = drawParams.Element;
            switch (drawPhase)
            {
                case DrawPhase.BeforeDrawBorders:
                    {
                        Border3DSide sides = (Border3DSide)0;

                        if (element is DayOfWeekHeaderUIElement)
                            sides = Border3DSide.Top | Border3DSide.Bottom;

                        if (sides != 0)
                        {
                            if ((sides & Border3DSide.Top) != 0)
                                drawParams.DrawBorders(UIElementBorderStyle.Solid, Border3DSide.Top);

                            if ((sides & Border3DSide.Bottom) != 0)
                                drawParams.DrawBorders(UIElementBorderStyle.TwoColor, Border3DSide.Bottom);
                            return true;
                        }
                        break;
                    }
                case DrawPhase.BeforeDrawFocus:
                    return true;
            }
            return false;
        }

        #endregion //DrawElement

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
            if (element is DayOfWeekHeaderUIElement)
                return DrawPhase.BeforeDrawBorders;
                
            if (element is DayNumberUIElement)
                return DrawPhase.BeforeDrawFocus;
            return DrawPhase.None;
        }

        #endregion //GetPhasesToFilter

        #endregion //IUIElementDrawFilter

    }
}
