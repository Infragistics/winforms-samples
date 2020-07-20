using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Showcase.CashflowDashboard.Data
{
    public class Activity
    {
        #region Private Members
        private Source source;
        private decimal actualInflow;
        private decimal actualOutflow;
        private decimal projectedInflow;
        private decimal projectedOutflow;
        #endregion // Private Members

        #region Constructor

        public Activity(
            Source source,
            decimal actualInflow,
            decimal actualOutflow,
            decimal projectedInflow,
            decimal projectedOutflow)
        {
            this.source = source;
            this.actualInflow = actualInflow;
            this.actualOutflow = actualOutflow;
            this.projectedInflow = projectedInflow;
            this.projectedOutflow = projectedOutflow;
        }

        #endregion // Constructor        

        #region Public Properties
        
        #region Source
        public Source Source
        {
            get { return this.source; }
        }
        #endregion // Source

        #region ActualInflow
        public decimal ActualInflow
        {
            get { return this.actualInflow; }
        }
        #endregion // ActualInflow

        #region ActualOutflow
        public decimal ActualOutflow
        {
            get { return this.actualOutflow; }
        }
        #endregion // ActualOutflow

        #region ProjectedInflow
        public decimal ProjectedInflow
        {
            get { return this.projectedInflow; }
        }
        #endregion // ProjectedInflow

        #region ProjectedOutflow
        public decimal ProjectedOutflow
        {
            get { return this.projectedOutflow; }
        }
        #endregion // ProjectedOutflow

        #endregion //Public Properties
    }
}
