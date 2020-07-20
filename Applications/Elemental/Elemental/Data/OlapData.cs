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
using Infragistics.Olap;
using Infragistics.Olap.Core;
using Infragistics.Olap.Core.Data;
using Infragistics.Olap.FlatData;
using Elemental;

namespace Elemental.Data
{
    #region OlapData class
    /// <summary>
    /// Manages OLAP data.
    /// </summary>
    public class OlapData
    {
        #region Fields
        
        private FlatDataSource ds = null;
        private SalesData salesData = null;
        private Control sync = null;
        private List<Member> dateMembers = null;

        #endregion Fields

        #region Constructor
        /// <summary>Creates a new instance.</summary>
        public OlapData(SalesData salesData, Control sync)
        {
            this.salesData = salesData;
            this.sync = sync;
        }
        #endregion Constructor

        #region OlapDataSource
        /// <summary>
        /// Returns an Infragistics.Olap.FlatData.FlatDataSource instance
        /// built from the SalesData instance from which this instance was
        /// initialized.
        /// </summary>
        public FlatDataSource DataSource
        {
            get
            {
                if ( this.ds == null )
                {
                    //  Set up the initial settings so that the attached pivot grid
                    //  shows the price on the measure axis, the geographic region on
                    //  the row axis, the product style on the column axis, and the
                    //  Date hierarchy on the filter axis.
                    FlatDataSourceInitialSettings settings = new FlatDataSourceInitialSettings();
                    settings.Measures = "[Measures].[Price]";
                    settings.Rows = "[Geography].[Region]";
                    settings.Filters = "[Date].[Date]";
                    settings.Columns = "[Product].[Style]";
                    this.ds = new FlatDataSource(this.salesData, this.salesData.ItemType, settings);

                    //  Handle the InitializeOlapAxisElementDescriptor event so we can
                    //  tell the data source which properties of the Sale class are
                    //  measures and which are hierarchies, and also so we can hide
                    //  the ones we don't need to appear in the pivot grid.
                    InitializeOlapAxisElementDescriptorHandler axisElementHandler =
                    delegate(object sender, InitializeOlapAxisElementDescriptorEventArgs args)
                    {
                        PropertyDescriptor descriptor = args.PropertyDescriptor;
                        string propertyName = descriptor.Name;

                        switch ( propertyName )
                        {
                            //  The 'Price' property is the sole member of the Measure axis
                            case "Price":
                                args.OlapElementDescriptor = new MeasureDescriptor(descriptor);
                                break;

                            //  Hide the 'Year' and 'ZipCode' properties
                            case "Year":
                            case "ZipCode":
                                args.Visible = false;
                                break;

                            //  All other properties are hierarchies
                            default:
                                args.OlapElementDescriptor = new HierarchyDescriptor(descriptor);
                                break;
                        }
                    };

                    this.ds.InitializeOlapAxisElementDescriptor += axisElementHandler;

                    //  Handle the InitializeHierarchyDescriptor event
                    InitializeHierarchyDescriptorHandler hierarchyDescriptorHandler =
                    delegate(object sender, InitializeHierarchyDescriptorEventArgs args)
                    {
                        string name = args.HierarchyDescriptor.Name;
                        switch ( name )
                        {
                            //  We want the State and City property to appear as if they
                            //  were levels within the Region hierarchy, sice these three
                            //  entities each represent different granularities of the geographic
                            //  location at which the sale was made.
                            //
                            //  This will cause the axis on which this hierarchy appears to
                            //  show a header for each geographic region. Each region in turn
                            //  displays an expansion indicator which provides access to the next
                            //  level of the hierarchy (state). Each of those headers is also
                            //  expandable, showing a header for each city in that state when expanded.
                            //
                            case "Region":

                                //  Remove any existing levels
                                args.HierarchyDescriptor.LevelDescriptors.Clear();

                                //  Add levels so that the hierarchy appears as a list of regions,
                                //  each of which shows the list of the states in that region when expanded,
                                //  with each of those states showing a list of the cities in that state
                                //  when expanded.
                                LevelDescriptor regionLevel = args.HierarchyDescriptor.LevelDescriptors.Add("region");
                                regionLevel.MemberProvider = (item) =>
                                {
                                    Sale sale = item as Sale;
                                    return sale.Region.ToString();
                                };

                                //  Make the state a level under the 'Region' hiearachy
                                LevelDescriptor stateLevel = args.HierarchyDescriptor.LevelDescriptors.Add("state");
                                stateLevel.MemberProvider = (item) =>
                                {
                                    Sale sale = item as Sale;
                                    return sale.State;
                                };

                                //  Make the city a level under the 'Region' hiearachy, appearing
                                //  as a child of the 'State' level.
                                LevelDescriptor cityLevel = args.HierarchyDescriptor.LevelDescriptors.Add("city");
                                cityLevel.MemberProvider = (item) =>
                                {
                                    Sale sale = item as Sale;
                                    return sale.City;
                                };

                                break;
                            
                            //  
                            case "Style":

                                //  Remove the 'All' level, but keep the 'default' one
                                args.HierarchyDescriptor.LevelDescriptors.Remove("all");

                                LevelDescriptor level = null;
                                if ( args.HierarchyDescriptor.LevelDescriptors.TryGetItem("default", out level) )
                                {
                                    //  Assign a func to the MemberProvider property so that
                                    //  each header for the level displays the localized name
                                    //  of the style.
                                    level.MemberProvider = (item) =>
                                    {
                                        Sale sale = item as Sale;
                                        return Assets.strings.ResourceManager.GetString(sale.Style.ToString());
                                    }; 
                                }
                                break;
                        }
                    };
                    
                    this.ds.InitializeHierarchyDescriptor += hierarchyDescriptorHandler;

                    //  Generate the data cube, grouping hierarchies by category.
                    //  For DateTime hierarchies, arrange the levels so that the
                    //  year, quarter, and month are displayed, with the quarter
                    //  being a child of the year, and the month being a child of
                    //  the quarter.
                    CubeGenerationParameters parameters = new CubeGenerationParameters();
                    parameters.HierarchyDimensionGrouping = HierarchyDimensionGrouping.ByCategory;
                    parameters.LevelCreationParameters.DateTimeLevelCreation = OlapData.DateTimeLevelCreationFlags;
                    this.ds.GenerateCube(parameters);

                    //  After initialization is complete, get the list of date members
                    //  which is used for filtering.
                    InitializeAsyncCompletedHandler handler =
                    delegate(object sender, InitializeAsyncCompletedEventArgs e)
                    {
                        this.GetDateHierarchyMembers();
                    };

                    this.ds.InitializeAsyncCompleted += handler;

                    //  Trigger initialization of the data source.
                    this.ds.InitializeAsync(this.sync);
                }

                return this.ds;
            }
        }

        #endregion OlapDataSource

        #region DateMembers
        /// <summary>
        /// Returns a list of the date-related members.
        /// </summary>
        public List<Member> DateMembers
        {
            get { return this.dateMembers; }
        }
        #endregion DateMembers

        #region GetDateHierarchyMembers
        /// <summary>
        /// Handles the GetHierarchyMembersAsyncCompleted event so as to
        /// store a list of the date-related members.
        /// </summary>
        private void GetDateHierarchyMembers()
        {
            GetHierarchyMembersAsyncCompletedHandler handler =
            delegate(object sender, GetHierarchyMembersAsyncCompletedEventArgs e)
            {
                this.dateMembers = new List<Member>(e.Members);
            };

            this.DataSource.GetHierarchyMembersAsyncCompleted += handler;
            this.DataSource.GetHierarchyMembersAsync("[Date].[Date]", this.sync);
        }
        #endregion GetDateHierarchyMembers

        #region DateTimeLevelCreationFlags
        /// <summary>
        /// Year | Quarter | Month
        /// </summary>
        static private DateTimeLevelCreationFlags DateTimeLevelCreationFlags
        {
            get
            {
                return
                    Infragistics.Olap.FlatData.DateTimeLevelCreationFlags.Year |
                    Infragistics.Olap.FlatData.DateTimeLevelCreationFlags.Quarter |
                    Infragistics.Olap.FlatData.DateTimeLevelCreationFlags.Month;
            }
        }
        #endregion DateTimeLevelCreationFlags
    }
    #endregion OlapData class
}