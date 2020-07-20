using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Showcase.CashflowDashboard.Data
{
    public static class DataManager
    {
        #region Private Members
        private static BindingList<MonthlyData> monthlyData;
        public const int NumberOfYears = 5;        
        #endregion // Private Members

        #region Public Properties

        #region MonthlyData
        public static BindingList<MonthlyData> MonthlyData
        {
            get 
            {
                if (null == DataManager.monthlyData)
                    DataManager.monthlyData = DataManager.GenerateMonthlyData();

                return DataManager.monthlyData; 
            }
        }
        #endregion // MonthlyData       

        #endregion // Public Properties

        #region Public Methods

        #region GetMonthlyData
        public static MonthlyData GetMonthlyData(int month, int year)
        {
            var results =
                from md in DataManager.MonthlyData
                where md.Month == month && md.Year == year
                select md;

            if (results.Count() == 0)
                return null;

            return results.First();
        }
        #endregion // GetMonthlyData 

        #endregion // Public Methods

        #region Private Methods

        #region GenerateMonthlyData
        private static BindingList<MonthlyData> GenerateMonthlyData()
        {            
            BindingList<MonthlyData> months = new BindingList<MonthlyData>();
            DateTime endingDate = DateTime.Now.AddMonths(-1);
            DateTime startingDate = endingDate.AddYears(-DataManager.NumberOfYears).AddMonths(1);

            // Start off with a nice cool $125 million. 
            decimal previousTotalCash = 110000000;

            for (DateTime date = startingDate; date <= endingDate; date = date.AddMonths(1))
            {
                int month = date.Month;
                int year = date.Year;

                MonthlyData monthlyData = new MonthlyData(month, year, previousTotalCash);
                previousTotalCash = monthlyData.EndingCash;
                months.Add(monthlyData);
            }

            return months;
        }
        #endregion // GenerateMonthlyData

        #endregion // Private Methods
    }
}
