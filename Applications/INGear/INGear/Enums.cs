using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Showcase.INGear
{

    #region CachedImages 

    /// <summary>
    /// Enum used for accessing the cached images resources.
    /// </summary>
    internal enum CachedImages
    {
        Logo,
        Background,
        Brakes,
        Tires,
        Suspension,
        Batteries,
        Sparkplugs,
        Pistons,
        EngineBlocks,
        Transmissions,
        Daily,
        Weekly,
        Monthly,
        Inventory,
        Schedule,
        Invoice_Brakes,
        Invoice_Sparkplugs,
        Invoice_Suspension,
        Invoice_Tires,
        Invoice_EngineBlocks,
        Invoice_Pistons,
        Invoice_Transmissions,
        Invoice_Batteries,
        RadialCenter,
    } 

    #endregion CachedImages

    #region CategoryType

    /// <summary>
    /// Enum to represent the main categories
    /// </summary>
    internal enum CategoryType
    {
        Brakes,
        Tires,
        Suspension,
        Batteries,
        Sparkplugs,
        Pistons,
        EngineBlocks,
        Transmissions,
    }

    #endregion //CategoryType

    #region UI

    /// <summary>
    /// Enum to indicate what view is being displayed.
    /// </summary>
    internal enum UI
    {
        Inventory,
        Delivery,
    }

    #endregion //UI
}
