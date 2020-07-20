using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;

namespace IGExcel
{
    #region NoFocusDrawFilter class
    /// <summary>
    /// Suppresses the focus rectangle drawing for an
    /// UltraControlBase-derived control.
    /// 
    /// Implements the singleton pattern since the class does
    /// not maintain state, and the implementation is exactly
    /// the same regardless of which control is using it.
    /// </summary>
    public class NoFocusDrawFilter : IUIElementDrawFilter
    {
        #region Fields
        static private NoFocusDrawFilter instance = null;
        #endregion Fields

        #region Creation

        /// <summary>Private constructor to prevent external instance creation</summary>
        private NoFocusDrawFilter(){}

        /// <summary>
        /// Returns the sole instance of this class.
        /// </summary>
        static public NoFocusDrawFilter Instance
        {
            get
            {
                if ( NoFocusDrawFilter.instance == null )
                    NoFocusDrawFilter.instance = new NoFocusDrawFilter();

                return NoFocusDrawFilter.instance;
            }
        }

        #endregion Creation

        #region IUIElementDrawFilter

        bool IUIElementDrawFilter.DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            return drawPhase == DrawPhase.BeforeDrawFocus;
        }

        DrawPhase IUIElementDrawFilter.GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            return DrawPhase.BeforeDrawFocus;
        }

        #endregion IUIElementDrawFilter
    }
    #endregion NoFocusDrawFilter class
}