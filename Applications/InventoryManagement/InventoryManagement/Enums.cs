using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Showcase.InventoryManagement
{
    #region ContactType

    /// <summary>
    /// Used to specify different types of contacts
    /// </summary>
    public enum ContactType
    {
        /// <summary>
        /// Represents a business customer
        /// </summary>
        Business_Customer,

        /// <summary>
        /// Represents a consumer
        /// </summary>
        Consumer,

        /// <summary>
        /// Represents a supplier
        /// </summary>
        Supplier,
    }

    #endregion //ContactType

    #region Table

    /// <summary>
    /// Enumeration used to when requesting a table of data
    /// </summary>
    public enum Table
    {
        /// <summary>
        /// Represents the table of contacts.
        /// </summary>
        Contacts,

        /// <summary>
        /// Represents the table of inventory. 
        /// </summary>
        Inventory,

        /// <summary>
        /// Represets the table containing order data.
        /// </summary>
        Orders,

        /// <summary>
        /// Represents the table of sample Line chart data
        /// </summary>
        ChartData1,

        /// <summary>
        /// Represents the table of sample Column chart data
        /// </summary>
        ChartData2,
    }

    #endregion //Table
}
