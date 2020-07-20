using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Showcase.CashflowDashboard.Data
{
    public class MonthlyData
    {
        #region Private Members

        private int month;
        private int year;
        private decimal previousTotalCash;
        private decimal endingCash;
        private decimal inflow;
        private decimal outflow;
        private decimal projectedInflow;
        private decimal projectedOutflow;

        private Dictionary<Source, decimal> inflows;        
        private Dictionary<Source, decimal> outflows;

        private BindingList<Activity> activities;

        #endregion // Private Members

        #region Constructor

        public MonthlyData(
            int month,
            int year,
            decimal previousTotalCash)
        {
            this.month = month;
            this.year = year;
            this.previousTotalCash = previousTotalCash;

            this.activities = this.GenerateActivites();
            this.CalculateTotals();
        }

        #endregion // Constructor

        #region Private Methods

        #region CalculateTotals
        private void CalculateTotals()
        {
            this.endingCash = this.previousTotalCash;
            this.inflow = 0;
            this.outflow = 0;
            this.projectedInflow = 0;
            this.projectedOutflow = 0;
            
            foreach (Activity activity in this.Activities)
            {
                this.inflow += activity.ActualInflow;
                this.outflow += activity.ActualOutflow;
                this.projectedInflow += activity.ProjectedInflow;
                this.projectedOutflow += activity.ProjectedOutflow;

                this.Inflows[activity.Source] = activity.ActualInflow;
                this.Outflows[activity.Source] = activity.ActualOutflow;
            }

            this.endingCash += this.inflow;
            this.endingCash -= this.outflow;
        }
        #endregion // CalculateTotals

        #region GenerateActivites
        private BindingList<Activity> GenerateActivites()
        {
            BindingList<Activity> activities = new BindingList<Activity>();
            Source[] sources = (Source[])Enum.GetValues(typeof(Source));
            foreach (Source source in sources)
            {
                decimal actualInflow = RandomDataGenerator.RandomDecimal(1500000, 6000000);
                decimal actualOutflow = RandomDataGenerator.RandomDecimal(1500000, 5500000);

                decimal projectedInflow = RandomDataGenerator.RandomProjection(actualInflow);
                decimal projectedOutflow = RandomDataGenerator.RandomProjection(actualOutflow);

                Activity activity = new Activity(
                    source,
                    actualInflow,                    
                    actualOutflow,
                    projectedInflow,
                    projectedOutflow
                    );

                activities.Add(activity);
            }            

            return activities;
        }
        #endregion // GenerateActivites        

        #region GetLastMonthData
        private MonthlyData GetLastMonthData()
        {
            DateTime dt = new DateTime(this.Year, this.Month, 1);
            dt.AddMonths(-1);
            return DataManager.GetMonthlyData(dt.Month, dt.Year);
        } 
        #endregion // GetLastMonthData

        #region GetLastYearData
        private MonthlyData GetLastYearData()
        {
            DateTime dt = new DateTime(this.Year, this.Month, 1);
            dt.AddYears(-1);
            return DataManager.GetMonthlyData(dt.Month, dt.Year);
        }
        #endregion // GetLastYearData

        #endregion // Private Methods

        #region Public Methods

        #region GetActivity
        public Activity GetActivity(Source source)
        {
            var results =
                from act in this.Activities
                where act.Source == source
                select act;

            return results.First();
        }
        #endregion // GetActivity

        #region GetInflow
        public decimal GetInflow(Source source)
        {
            return this.Inflows[source];
        } 
        #endregion // GetInflow

        #region GetOutflow
        public decimal GetOutflow(Source source)
        {
            return this.Outflows[source];
        }
        #endregion // GetOutflow

        #endregion // Public Methods

        #region Private Properties

        #region Inflows
        private Dictionary<Source, decimal> Inflows
        {
            get
            {
                if (null == this.inflows)
                    this.inflows = new Dictionary<Source, decimal>(4);

                return this.inflows;
            }
        }
        #endregion // Inflows 

        #region Outflows
        private Dictionary<Source, decimal> Outflows
        {
            get
            {
                if (null == this.outflows)
                    this.outflows = new Dictionary<Source, decimal>(4);

                return this.outflows;
            }
        }
        #endregion // Outflows 

        #endregion // Private Properties

        #region Public Properties

        #region Activities
        public BindingList<Activity> Activities
        {
            get
            {
                return this.activities;
            }
        }
        #endregion // Activities

        #region AssetLiabilityRatio
        public decimal AssetLiabilityRatio
        {
            get
            {
                return this.Inflow / this.Outflow;
            }
        } 
        #endregion // AssetLiabilityRatio

        #region EndingCash
		public decimal EndingCash
        {
            get 
            {
                return this.endingCash;                    
            }
        } 
	    #endregion // EndingCash        

        #region Month
        public int Month
        {
            get { return this.month; }
        }
        #endregion // Month        

        #region PreviousTotalCash
        public decimal PreviousTotalCash
        {
            get { return this.previousTotalCash; }
        } 
        #endregion // PreviousTotalCash

        #region Inflow
        public decimal Inflow
        {
            get { return this.inflow; }
        }
        #endregion // Inflow     

        #region Outflow
        public decimal Outflow
        {
            get { return this.outflow; }
        }
        #endregion // Outflow

        #region OperationsInflow
        public decimal OperationsInflow
        {
            get { return this.GetInflow(Source.Operations); }
        } 
        #endregion // OperationsInflow

        #region OperationsOutflow
        public decimal OperationsOutflow
        {
            get { return this.GetOutflow(Source.Operations); }
        } 
        #endregion // OperationsOutflow

        #region FinancingInflow
        public decimal FinancingInflow
        {
            get { return this.GetInflow(Source.Financing); }
        }
        #endregion // FinancingInflow

        #region FinancingOutflow
        public decimal FinancingOutflow
        {
            get { return this.GetOutflow(Source.Financing); }
        }
        #endregion // FinancingOutflow

        #region InvestingInflow
        public decimal InvestingInflow
        {
            get { return this.GetInflow(Source.Investing); }
        }
        #endregion // InvestingInflow

        #region InvestingOutflow
        public decimal InvestingOutflow
        {
            get { return this.GetOutflow(Source.Investing); }
        }
        #endregion // InvestingOutflow

        #region OtherInflow
        public decimal OtherInflow
        {
            get { return this.GetInflow(Source.Other); }
        }
        #endregion // OtherInflow

        #region OtherOutflow
        public decimal OtherOutflow
        {
            get { return this.GetOutflow(Source.Other); }
        }
        #endregion // OtherOutflow

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

        #region Year
        public int Year
        {
            get { return this.year; }
        }
        #endregion // Year

        #endregion // Public Properties        
    }
}
