using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Showcase.CashflowDashboard.Data
{
    public class RandomDataGenerator
    {
        #region Private Members
        private static Random rnd = new Random(1);
        #endregion Pricate Members

        #region RandomDecimal
        public static decimal RandomDecimal(int minValue, int maxValue)
        {
            decimal dollars = RandomDataGenerator.RandomInt(minValue, maxValue);
            decimal cents = RandomDataGenerator.RandomInt(0, 99);
            cents = cents / 100;

            return dollars + cents;
        } 
        #endregion // RandomDecimal

        #region RandomInt
        public static int RandomInt(int Min, int Max)
        {
            return rnd.Next(Min, Max + 1);
        }
        #endregion RandomInt

        #region RandomProjection
        public static decimal RandomProjection(decimal startingValue)
        {
            decimal percentage = RandomProjectionPercentage();
            return startingValue * percentage;
        } 
        #endregion // RandomProjection

        #region RandomProjectionPercentage
        private static decimal RandomProjectionPercentage()
        {
            decimal percentage = RandomDataGenerator.RandomInt(75, 125);
            percentage /= 100;
            return percentage;
        } 
        #endregion // RandomProjectionPercentage
    }
}
