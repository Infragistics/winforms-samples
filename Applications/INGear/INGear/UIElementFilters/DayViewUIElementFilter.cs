using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win;
using Infragistics.Win.UltraWinSchedule.DayView;

namespace Showcase.INGear
{
    internal class DayViewUIElementFilter : Infragistics.Win.IUIElementCreationFilter
    {
        #region IUIElementCreationFilter

        #region AfterCreateChildElements

        /// <summary>
        /// Called after an element's ChildElements have been
        /// created. The child element's can be repositioned here
        /// and/or new element's can be added.
        /// </summary>
        /// <param name="parent">The <see cref="T:Infragistics.Win.UIElement"/> whose child elements have been created/positioned.</param>
        public void AfterCreateChildElements(Infragistics.Win.UIElement parent)
        {
            if (parent is TimeSlotDescriptorAreaUIElement)
            {
                List<UIElement> elementsToRemove = new List<UIElement>();
                foreach (UIElement element in parent.ChildElements)
                {
                    // find all the UIElements for the TileSlotDescriptors that are not 8, 12, or 4.
                    TimeSlotDescriptorUIElement timeSlotElement = element as TimeSlotDescriptorUIElement;
                    if (timeSlotElement != null &&
                        timeSlotElement.FirstTimeSlot.StartTime.Hour % 4 == 0)
                        continue;
                    

                    // add to the list of element to remove
                    elementsToRemove.Add(timeSlotElement);
                }

                if (elementsToRemove.Count > 0)
                {
                    // remove each element in the list
                    foreach (UIElement element in elementsToRemove)
                        parent.ChildElements.Remove(element);
                }
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
        /// <param name="parent">The <see cref="T:Infragistics.Win.UIElement"/> whose child elements are going to be created/positioned.</param>
        /// <returns>
        /// True if the default creation logic should be bypassed.
        /// </returns>
        public bool BeforeCreateChildElements(Infragistics.Win.UIElement parent)
        {
            return false;
        }

        #endregion //BeforeCreateChildElements

        #endregion //IUIElementCreationFilter
    }
}
