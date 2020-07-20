using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace Showcase.InventoryManagement
{
    internal class EditorButtonUIFilter : IUIElementDrawFilter
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
        /// <exception cref="System.NotImplementedException"></exception>
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            switch (drawPhase)
            {
                case DrawPhase.BeforeDrawBackColor:
                    EditorButtonUIElement editorButtonUIElement = drawParams.Element as EditorButtonUIElement;
                    if (editorButtonUIElement != null &&
                        editorButtonUIElement.IsMouseOver &&
                        !editorButtonUIElement.IsMouseDown)
                    {
                        drawParams.AppearanceData.BackColor = System.Drawing.Color.FromArgb(211, 211, 211);
                    }

                    break;
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
        /// <exception cref="System.NotImplementedException"></exception>
        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (drawParams.Element is EditorButtonUIElement)
                return DrawPhase.BeforeDrawBackColor;
            return DrawPhase.None;
        }

        #endregion //GetPhasesToFilter

        #endregion //IUIElementDrawFilter
    }
}
