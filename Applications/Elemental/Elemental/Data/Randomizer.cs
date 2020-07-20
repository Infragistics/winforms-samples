using System;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Elemental;

namespace Elemental.Data
{
    #region Randomizer class
    /// <summary>
    /// Provides randomization support.
    /// </summary>
    static public class Randomizer
    {
        static private System.Random randomDecimal = null;
        static private System.Random randomDate = null;
        static private System.Random randomInteger = null;

        #region Random (HoodieStyle)
        static public HoodieStyle RandomStyle()
        {
            if ( Randomizer.randomInteger == null )
                Randomizer.randomInteger = new Random();

            int min = 0;
            int max = Enum.GetValues(typeof(HoodieStyle)).Length;
            int value = Randomizer.randomInteger.Next(min, max);
            return (HoodieStyle)value;
        }
        #endregion Random (HoodieStyle)

        #region Random (integer)
        static public int Random(int min, int max)
        {
            if ( Randomizer.randomInteger == null )
                Randomizer.randomInteger = new Random();

            return Randomizer.randomInteger.Next(min, max);
        }
        #endregion Random (integer)

        #region Random (decimal)
        static public decimal Random(decimal min, decimal max)
        {
            if ( Randomizer.randomDecimal == null )
                Randomizer.randomDecimal = new Random();

            int iMin = (int)(min * 100);
            int iMax = (int)(max * 100);
            int value = Randomizer.randomDecimal.Next(iMin, iMax);
            return (decimal)value / (decimal)100;
        }
        #endregion Random (decimal)

        #region Random (date)
        static public DateTime Random(DateTime min, DateTime max)
        {
            if ( Randomizer.randomDate == null )
                Randomizer.randomDate = new Random();

            int range = (int)(max.Subtract(min).TotalDays);
            int value = Randomizer.randomDate.Next(range);
            return min.AddDays(value);
        }
        #endregion Random (date)

    }
    #endregion Randomizer class
}