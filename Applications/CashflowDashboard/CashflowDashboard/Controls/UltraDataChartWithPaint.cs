using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win.DataVisualization;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace Showcase.CashflowDashboard.Controls
{
    [Designer("Infragistics.Win.DataVisualization.DataChart.Design.UltraDataChartDesigner, Infragistics4.Win.v14.2.Design, Version=14.2.0.9000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb", typeof(IDesigner))]
    [DefaultProperty("Series")]
    public class UltraDataChartWithPaint : UltraDataChart
    {
        public UltraDataChartWithPaint() : base() {}

        /// <summary>
        /// At this time of writing this sample, the Paint event of the UltraDataChart control does 
        /// not fire. This will, of course, be fixed. But as a workaround, we derived from 
        /// UltraDataChart and added a new event (AfterPaint) that fires after the Paint 
        /// event would have fired.  
        /// </summary>
        public event PaintEventHandler AfterPaint;

        /// <summary>
        /// Raises the System.Windows.Forms.Control.Paint event.
        /// </summary>
        /// <param name="pe">A System.Windows.Forms.PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (null != this.AfterPaint)
                this.AfterPaint(this, pe);
        }
    }
}
