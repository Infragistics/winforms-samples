using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Elemental
{
    #region HoodieStyle
    /// <summary>
    /// Constants which identify a hoodie style.
    /// </summary>
    public enum HoodieStyle
    {
        BigWavesSurfing = 0,
        RockAndRoll = 1,
        TattooLove = 2,
        RockStar = 3,
        SkullsAndFlourishes = 4,
        BlackDragon = 5,
        FastRider = 6,
        AbstractFoliage = 7,
    }
    #endregion HoodieStyle

    #region HoodieSKU
    /// <summary>
    /// Constants which identify a hoodie SKU.
    /// Note that the value assigned is the same as that
    /// of the corresponding HoodieStyle, so that a HoodieStyle
    /// can be easily converted to a HoodieSKU, and vice versa.
    /// </summary>
    public enum HoodieSKU
    {
        SKU4589A = (int)HoodieStyle.BigWavesSurfing,
        SKU9002A = (int)HoodieStyle.RockAndRoll,
        SKU3167A = (int)HoodieStyle.TattooLove,
        SKU6917A = (int)HoodieStyle.RockStar,
        SKU8711A = (int)HoodieStyle.SkullsAndFlourishes,
        SKU7188A = (int)HoodieStyle.BlackDragon,
        SKU6309A = (int)HoodieStyle.FastRider,
        SKU3650A = (int)HoodieStyle.AbstractFoliage,
    }
    #endregion HoodieSKU

}