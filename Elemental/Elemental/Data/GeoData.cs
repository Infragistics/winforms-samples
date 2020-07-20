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
    #region GeoData class
    /// <summary>
    /// Provides geographical data.
    /// </summary>
    public class GeoData
    {
        #region Fields
        private DataTable table = null;
        #endregion Fields

        #region Table
        /// <summary>
        /// Returns a DataTable containing geographic data
        /// </summary>
        public DataTable Table
        {
            get
            {
                if ( this.table == null )
                {
                    Type type = typeof(frmMain);
                    string xmlName = string.Format("Data.{0}.xml", "ZipCodes");
                    string xsdName = string.Format("Data.{0}.xsd", "ZipCodes");
                    using ( Stream xmlStream = type.Module.Assembly.GetManifestResourceStream(type, xmlName) )
                    using ( Stream xsdStream = type.Module.Assembly.GetManifestResourceStream(type, xsdName) )
                    {
                        this.table = new DataTable("ZipCodes");
                        this.table.ReadXmlSchema(xsdStream);
                        this.table.ReadXml(xmlStream);

                    }


                }

                return this.table;
            }
        }
        #endregion Table

        #region Indexer (ZipCode => DataRow)
        /// <summary>
        /// Returns the DataRow corresponding to the specified postal code.
        /// </summary>
        public DataRow this[string zipCode]
        {
            get
            {
                DataTable table = this.Table;
                DataRow[] rows = table.Select(string.Format("ZipCode = '{0}'", zipCode));

                return rows.Length == 0 ? null : rows[0];
            }
        }
        #endregion Indexer (ZipCode => DataRow)

        #region RegionFromZipCode
        /// <summary>
        /// Returns the geographic region (i.e., northeast, southwest, etc.)
        /// corresponding to the specified postal code.
        /// </summary>
        public GeographicRegion RegionFromZipCode(string zipCode)
        {
            DataRow row = this[zipCode];
            if ( row == null )
                return GeographicRegion.Undefined;

            string state = row["state"] as string;
            return GeoData.RegionFromState(state);
        }
        #endregion RegionFromZipCode

        #region RegionFromState
        /// <summary>
        /// Returns the geographic region (i.e., northeast, southwest, etc.)
        /// corresponding to the specified state.
        /// </summary>
        static public GeographicRegion RegionFromState( string state )
        {
            GeographicRegion region = GeographicRegion.Undefined;

            switch ( state )
            {
                case "Connecticut":
                case "Delaware":
                case "Maine":
                case "Maryland":
                case "Massachusetts":
                case "New Hampshire":
                case "New Jersey":
                case "New York":
                case "Pennsylvania":
                case "Rhode Island":
                case "Vermont":
                case "District of Columbia":
                    region = GeographicRegion.Northeast;
                    break;

                case "Alabama":
                case "Arkansas":
                case "Florida":
                case "Georgia":
                case "Kentucky":
                case "Louisiana":
                case "Mississippi":
                case "North Carolina":
                case "South Carolina":
                case "Tennessee":
                case "Virginia":
                case "West Virginia":
                    region = GeographicRegion.Southeast;
                    break;

                case "Illinois":
                case "Indiana":
                case "Iowa":
                case "Kansas":
                case "Michigan":
                case "Minnesota":
                case "Missouri":
                case "Nebraska":
                case "North Dakota":
                case "Ohio":
                case "South Dakota":
                case "Wisconsin":
                    region = GeographicRegion.Midwest;
                    break;

                case "Puerto Rico":
                case "Hawaii":
                    region = GeographicRegion.Other;
                    break;

                case "Alaska":
                case "California":
                case "Colorado":
                case "Idaho":
                case "Montana":
                case "Nevada":
                case "Oregon":
                case "Utah":
                case "Washington":
                case "Wyoming":
                    region = GeographicRegion.Northwest;
                    break;

                case "Arizona":
                case "New Mexico":
                case "Oklahoma":
                case "Texas":
                    region = GeographicRegion.Southwest;
                    break;

                default:
                    Debug.Fail("Unrecognized state: '{0}'", state);
                    break;
            }

            return region;

        }
        #endregion RegionFromState

    }
    #endregion GeoData class

    #region GeographicRegion
    /// <summary>
    /// Defines constants which identify a geographic region of the United States.
    /// </summary>
    public enum GeographicRegion
    {
        Undefined,
        Northeast,
        Southeast,
        Midwest,
        Northwest,
        Southwest,
        Other,
    }
    #endregion GeographicRegion


}