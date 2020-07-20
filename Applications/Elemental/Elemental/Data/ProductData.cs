using System;
using System.Reflection;
using System.ComponentModel;
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
    #region ProductData class
    /// <summary>
    /// Contains product-related data.
    /// </summary>
    public class ProductData
    {
        #region Indexer (HoodieSKU => HoodieStyle)
        /// <summary>
        /// Returns the HoodieStyle constant associated with the
        /// specified HoodieSKU constant.
        /// </summary>
        public HoodieStyle this[HoodieSKU sku]
        {
            get
            {
                int value = (int)sku;
                return (HoodieStyle)value;
            }
        }
        #endregion Indexer (HoodieSKU => HoodieStyle)

        #region Indexer (HoodieStyle => HoodieSKU)
        /// <summary>
        /// Returns the HoodieSKU constant associated with the
        /// specified HoodieStyle constant.
        /// </summary>
        public HoodieSKU this[HoodieStyle style]
        {
            get
            {
                int value = (int)style;
                return (HoodieSKU)value;
            }
        }
        #endregion Indexer (HoodieStyle => HoodieSKU)

        #region GetDisplayString
        /// <summary>
        /// Returns the localized string associated with the
        /// specified HoodieStyle
        /// </summary>
        static public string GetDisplayString(HoodieStyle style)
        {
            return Assets.strings.ResourceManager.GetString(style.ToString());
        }

        /// <summary>
        /// Returns the localized string associated with the
        /// specified HoodieSKU.
        /// </summary>
        static public string GetDisplayString(HoodieSKU sku)
        {
            
            return Assets.strings.ResourceManager.GetString(sku.ToString());
        }
        #endregion GetDisplayString
    }
    #endregion ProductData class
}