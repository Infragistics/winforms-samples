using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Showcase.InventoryManagement
{
    internal class NoFocusRectDrawFilter : Infragistics.Win.IUIElementDrawFilter
    {
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
        public bool DrawElement(Infragistics.Win.DrawPhase drawPhase, ref Infragistics.Win.UIElementDrawParams drawParams)
        {
            return true;
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
        public Infragistics.Win.DrawPhase GetPhasesToFilter(ref Infragistics.Win.UIElementDrawParams drawParams)
        {
            return Infragistics.Win.DrawPhase.BeforeDrawFocus;
        }

        #endregion //GetPhasesToFilter

        #endregion //IUIElementDrawFilter
    }
}
