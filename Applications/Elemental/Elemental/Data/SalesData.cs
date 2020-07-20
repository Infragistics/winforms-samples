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
    #region SalesData class
    /// <summary>
    /// Generates sales data.
    /// </summary>
    public class SalesData : IEnumerable<Sale>
    {
        #region Fields

        /// <summary>
        /// 1/1/2009
        /// </summary>
        static public readonly DateTime MinDate = new DateTime(2009, 1, 1);

        /// <summary>
        /// 12/31/2014
        /// </summary>
        static public readonly DateTime MaxDate = new DateTime(2014, 12, 31);

        static private readonly decimal[] priceRange = new decimal[]
        {
            68.49m,
            64.99m,
            59.39m,
            53.69m,
            47.99m,
            44.49m,
            37.19m,
        };


        private GeoData geoData = null;
        private List<Sale> list = null;

        #endregion Fields

        #region Constructor
        public SalesData()
        {
        }
        #endregion Constructor

        #region ItemType
        /// <summary>
        /// Returns the type of the items in this collection.
        /// </summary>
        public Type ItemType { get { return typeof(Sale); } }
        #endregion ItemType

        #region Count
        /// <summary>
        /// Returns the number of items in this collection.
        /// </summary>
        public int Count
        {
            get
            {
                return this.List.Count;
            }
        }
        #endregion Count

        #region GeoData
        /// <summary>
        /// Returns an object which provides geographical data.
        /// </summary>
        private GeoData GeoData
        {
            get
            {
                if ( this.geoData == null )
                    this.geoData = new GeoData();

                return this.geoData;
            }
        }
        #endregion GeoData

        #region List
        /// <summary>
        /// Returns a reference to the inner list of Sale items
        /// </summary>
        private List<Sale> List
        {
            get
            {
                if ( this.list == null )
                {
                    this.list = new List<Sale>(5000);
                    this.GenerateData(this.list);
                }

                return this.list;
            }
        }
        #endregion List

        #region GenerateData
        /// <summary>
        /// Generates random data and populates the specified list.
        /// </summary>
        private void GenerateData(List<Sale> list)
        {
            DataTable table = this.GeoData.Table;
            int rowCount = table.Rows.Count;

            int[] years = new int[]{2009, 2010, 2011, 2012, 2013, 2014};
            int[] sizes = new int[]{800, 1150, 1250, 1800, 1200, 1500};

            for ( int i = 0; i < years.Length; i ++ )
            {
                int year = years[i];
                int size = sizes[i];

                DateTime minDate = new DateTime(year, 1, 1);
                DateTime maxDate =
                    year == DateTime.Today.Year ?
                    new DateTime(year, DateTime.Today.Month, 1) :
                    new DateTime(year, 12, 31);
                
                int processed = 0;
                while ( processed < size )
                {
                    processed += 1;

                    int rowIndex = Randomizer.Random(0, rowCount - 1);
                    DataRow row = table.Rows[rowIndex];
                    string zipCode = row["ZipCode"] as string;

                    if ( SalesData.IsZipValid(zipCode) == false )
                        continue;

                    string city = row["City"] as string;

                    if ( string.IsNullOrEmpty(city) )
                        continue;

                    string state = row["State"] as string;

                    HoodieStyle style = Randomizer.RandomStyle();

                    DateTime date = Randomizer.Random(minDate, maxDate);

                    int priceIndex = Randomizer.Random(0, SalesData.priceRange.Length);
                    decimal price = priceRange[priceIndex];

                    Sale sale = new Sale(date, style, price, zipCode, city, state);
                    list.Add( sale );
                }
            }

        }
        #endregion GenerateData

        #region IsZipValid
        /// <summary>
        /// Returns a boolean value indicating whether the specified zip code is valid.
        /// </summary>
        static private bool IsZipValid(string zipCode)
        {
            if ( zipCode.Length != 5 )
                return false;

            if ( char.IsNumber(zipCode[4]) == false ||
                 char.IsNumber(zipCode[3]) == false ||
                 char.IsNumber(zipCode[2]) == false ||
                 char.IsNumber(zipCode[1]) == false ||
                 char.IsNumber(zipCode[0]) == false )
                return false;

            return true;
        }
        #endregion IsZipValid

        #region GetPriceRange
        /// <summary>
        /// Returns the price range for the specified year.
        /// </summary>
        private decimal[] GetPriceRange(int year)
        {
            switch ( year )
            {
                case 2009:
                    return new decimal[]
                    {
                        11.99m,
                        13.49m,
                        15.99m,
                        16.19m,
                        18.49m,
                    };

                case 2010:
                    return new decimal[]
                    {
                        9.99m,
                        11.49m,
                        13.99m,
                    };

                case 2011:
                    return new decimal[]
                    {
                        6.99m,
                        8.49m,
                        9.99m,
                        11.19m,
                    };

                case 2012:
                    return new decimal[]
                    {
                        6.99m,
                        8.49m,
                        9.99m,
                        11.19m,
                    };

                case 2013:
                case 2014:
                    return new decimal[]
                    {
                        6.99m,
                        8.49m,
                        9.99m,
                        11.99m,
                        13.49m,
                        15.99m,
                        16.19m,
                        18.49m,
                    };

                default:
                    return this.GetPriceRange(2012);

            }
        }
        #endregion GetPriceRange

        #region Indexer (int)
        /// <summary>
        /// Returns a decimal array containing the price range
        /// for the specified year.
        /// </summary>
        public decimal[] this[int year]
        {
            get
            {
                decimal[] range = null;

                switch ( year )
                {
                    case 2009:
                        range = new decimal[]{ 23.99m, 19.99m, 15.49m, 14.99m };
                        break;

                    case 2010:
                        range = new decimal[]{ 19.99m, 15.49m, 14.99m, 13.69m };
                        break;

                    case 2011:
                        range = new decimal[]{ 15.49m, 14.99m, 13.69m, 12.49m };
                        break;

                    case 2012:
                        range = new decimal[]{ 14.99m, 13.69m, 12.49m, 11.99m };
                        break;

                    case 2013:
                        range = new decimal[]{ 13.69m, 12.49m, 11.99m, 9.99m };
                        break;

                    default:
                        range = new decimal[]{ 3.99m, 4.49m, 6.19m, 8.99m };
                        break;

                }

                return range;
            }
        }
        #endregion Indexer (int)

        #region Indexer (HoodieStyle)
        /// <summary>
        /// Returns a list of each sale order which applies
        /// to the specified style.
        /// </summary>
        public IEnumerable<Sale> this[HoodieStyle style]
        {
            get
            {
                List<Sale> data = this.List;

                var query = from sale in data
                            where sale.Style == style
                            select sale;

                foreach ( Sale sale in query )
                {
                    yield return sale;
                }
            }
        }
        #endregion Indexer (HoodieStyle)

        #region IEnumerable<Sale>

        IEnumerator<Sale> IEnumerable<Sale>.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            IEnumerable<Sale> i = this as IEnumerable<Sale>;
            return i.GetEnumerator();
        }

        #endregion IEnumerable<Sale>

    }
    #endregion SalesData class

    #region Sale class
    /// <summary>
    /// Encapsulates a sales record.
    /// </summary>
    public class Sale
    {
        #region Fields

        private const string CategoryDate = "Date";
        private const string CategoryProduct = "Product";
        private const string CategoryGeography = "Geography";

        #endregion Fields

        #region Constructor
        /// <summary>
        /// Creates a new instance with the specified parameters.
        /// </summary>
        public Sale(
            DateTime date,
            HoodieStyle style, decimal price,
            string zipCode, string city, string state)
        {
            GeographicRegion region = GeoData.RegionFromState(state);
            if ( region == GeographicRegion.Undefined )
                throw new ArgumentException(string.Format("Unrecognized state '{0}'.", state));

            this.Date = date;
            this.Style = style;
            this.Price = price;
            this.ZipCode = zipCode;
            this.State = state;
            this.City = city == null ? string.Empty : city;
            this.Region = region;

        }
        #endregion Constructor

        #region Date
        /// <summary>
        /// The date/time of the sale.
        /// </summary>
        [Category(Sale.CategoryDate)]
        public DateTime Date
        {
            get;
            private set;
        }
        #endregion Date

        #region Year
        /// <summary>
        /// Returns the year component of the date of the sale.
        /// </summary>
        [Category(Sale.CategoryDate)]
        public int Year
        {
            get { return this.Date.Year; }
        }
        #endregion Year

        #region Style
        /// <summary>
        /// The style of the item that was purchased.
        /// </summary>
        [Category(Sale.CategoryProduct)]
        public HoodieStyle Style
        {
            get;
            private set;
        }
        #endregion Style

        #region Price
        /// <summary>
        /// The price paid for the item.
        /// </summary>
        [Category(Sale.CategoryProduct)]
        public decimal Price
        {
            get;
            private set;
        }
        #endregion Price

        #region ZipCode
        /// <summary>
        /// The zip code of the customer who made the purchase.
        /// </summary>
        [Category(Sale.CategoryGeography)]
        public string ZipCode
        {
            get;
            private set;
        }
        #endregion ZipCode

        #region City
        /// <summary>
        /// The city where the customer who made the purchase lives.
        /// </summary>
        [Category(Sale.CategoryGeography)]
        public string City
        {
            get;
            private set;
        }
        #endregion City

        #region State
        /// <summary>
        /// The state where the customer who made the purchase lives.
        /// </summary>
        [Category(Sale.CategoryGeography)]
        public string State
        {
            get;
            private set;
        }
        #endregion State

        #region Region
        /// <summary>
        /// The geographic region where the customer who made the purchase lives.
        /// </summary>
        [Category(Sale.CategoryGeography)]
        public GeographicRegion Region
        {
            get;
            private set;
        }
        #endregion Region

        #region ToString
        /// <summary>
        /// Returns the string representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}; {1}; {2}", this.Style, this.Date.ToShortDateString(), this.Price.ToString("c"));
        }
        #endregion ToString
    }
    #endregion Sale class

    #region SalesSummaryData class
    /// <summary>
    /// A collection of SalesSummary instances.
    /// </summary>
    public class SalesSummaryData : IEnumerable<SalesSummary>
    {
        #region Constructor
        /// <summary>Creates a new instance.</summary>
        public SalesSummaryData(SalesData salesData, DateFilter[] filters)
        {
            Dictionary<int, Dictionary<HoodieStyle, decimal>> data =
                new Dictionary<int, Dictionary<HoodieStyle, decimal>>();

            //  First pass: create the outer table
            foreach ( Sale sale in salesData )
            {
                int year = sale.Year;

                int index = year - SalesData.MinDate.Year;
                DateFilter filter = index < filters.Length ? filters[index] : null;
                if ( filter != null && filter.IsFilteredIn() == false )
                    continue;

                Dictionary<HoodieStyle, decimal> summary = null;
                if ( data.TryGetValue(year, out summary) == false )
                {
                    summary = new Dictionary<HoodieStyle, decimal>();
                    data.Add(year, summary);
                }
            }

            //  Second pass: aggregate summaries
            foreach ( Sale sale in salesData )
            {
                int index = sale.Year - SalesData.MinDate.Year;
                int quarter = DateFilter.GetQuarter(sale.Date);
                DateFilter filter = index < filters.Length ? filters[index] : null;
                if ( filter != null && filter.IsFilteredIn(quarter) == false )
                    continue;

                Dictionary<HoodieStyle, decimal> summary = null;
                if ( data.TryGetValue(sale.Year, out summary) == false )
                    continue;

                decimal total = 0m;
                if ( summary.TryGetValue(sale.Style, out total) == false )
                    summary.Add(sale.Style, 0m);

                summary[sale.Style] += sale.Price;
            }

            //  Create SalesSummary instances for each year
            this.List = new List<SalesSummary>(data.Count);
            decimal min = decimal.MaxValue;
            decimal max = 0m;
            foreach ( KeyValuePair<int, Dictionary<HoodieStyle, decimal>> pair in data )
            {
                SalesSummary summary = new SalesSummary(pair.Key, pair.Value);
                this.List.Add(summary);

                min = Math.Min(min, summary.Minimum);
                max = Math.Max(max, summary.Maximum);
            }

            //  Set the min/max
            this.Minimum = min;
            this.Maximum = max;

            //  Sort the list by year
            this.List.Sort(new Sorter());
        }
        #endregion Constructor

        #region Minimum
        /// <summary>
        /// Returns the minimum sales total.
        /// </summary>
        public decimal Minimum
        {
            get;
            private set;
        }
        #endregion Minimum

        #region Maximum
        /// <summary>
        /// Returns the maximum sales total.
        /// </summary>
        public decimal Maximum
        {
            get;
            private set;
        }
        #endregion Maximum

        #region Interval
        /// <summary>
        /// Returns the interval between max and min.
        /// </summary>
        public decimal Interval
        {
            get
            {
                return (this.Maximum - this.Minimum) / frmMain.CarouselItemSlots;
            }
        }
        #endregion Interval

        #region List
        private List<SalesSummary> List
        {
            get;
            set;
        }
        #endregion List

        #region IEnumerable<SalesSummary>

        IEnumerator<SalesSummary> IEnumerable<SalesSummary>.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        #endregion IEnumerable<SalesSummary>

        #region Sorter class
        private class Sorter : IComparer<SalesSummary>
        {
            #region IComparer<SalesSummary>

            int IComparer<SalesSummary>.Compare(SalesSummary x, SalesSummary y)
            {
                if ( x == null || y == null )
                    return 0;

                return x.Year.CompareTo(y.Year);
            }

            #endregion IComparer<SalesSummary>
        }
        #endregion Sorter class
    }
    #endregion SalesSummaryData class

    #region SalesSummary class
    /// <summary>
    /// Encapsulates a summary of sales data.
    /// </summary>
    public class SalesSummary
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public SalesSummary(int year, IDictionary<HoodieStyle, decimal> data)
        {
            this.Year = year;
            Type type = this.GetType();
            foreach ( KeyValuePair<HoodieStyle, decimal> pair in data )
            {
                string propName = pair.Key.ToString();
                PropertyInfo prop = type.GetProperty(propName);
                if ( prop == null )
                {
                    Debug.Fail(string.Format("The {0} class does not expose a property named {1}.", type.Name, propName ));
                    continue;
                }

                prop.SetValue(this, pair.Value, null);
            }
        }

        /// <summary>
        /// The year for which this summary was generated.
        /// </summary>
        public int Year
        {
            get;
            private set;
        }

        /// <summary>
        /// The total sales for AbstractFoliage.
        /// </summary>
        public decimal AbstractFoliage
        {
            get;
            private set;
        }

        /// <summary>
        /// The total sales for BigWavesSurfing.
        /// </summary>
        public decimal BigWavesSurfing
        {
            get;
            private set;
        }

        /// <summary>
        /// The total sales for BlackDragon.
        /// </summary>
        public decimal BlackDragon
        {
            get;
            private set;
        }

        /// <summary>
        /// The total sales for FastRider.
        /// </summary>
        public decimal FastRider
        {
            get;
            private set;
        }

        /// <summary>
        /// The total sales for SkullsAndFlourishes.
        /// </summary>
        public decimal SkullsAndFlourishes
        {
            get;
            private set;
        }

        /// <summary>
        /// The total sales for RockAndRoll.
        /// </summary>
        public decimal RockAndRoll
        {
            get;
            private set;
        }

        /// <summary>
        /// The total sales for RockStar.
        /// </summary>
        public decimal RockStar
        {
            get;
            private set;
        }

        /// <summary>
        /// The total sales for TattooLove.
        /// </summary>
        public decimal TattooLove
        {
            get;
            private set;
        }

        #region Minimum
        /// <summary>
        /// The minimum value for this summary.
        /// </summary>
        [Browsable(false)]        
        public decimal Minimum
        {
            get
            {
                decimal min = decimal.MaxValue;

                min = Math.Min(min, this.AbstractFoliage);
                min = Math.Min(min, this.BigWavesSurfing);
                min = Math.Min(min, this.BlackDragon);
                min = Math.Min(min, this.FastRider);
                min = Math.Min(min, this.RockAndRoll);
                min = Math.Min(min, this.RockStar);
                min = Math.Min(min, this.SkullsAndFlourishes);
                min = Math.Min(min, this.TattooLove);

                return min;
            }
        }
        #endregion Minimum

        #region Maximum
        /// <summary>
        /// The maximum value for this summary.
        /// </summary>
        [Browsable(false)]
        public decimal Maximum
        {
            get
            {
                decimal max = 0m;

                max = Math.Max(max, this.AbstractFoliage);
                max = Math.Max(max, this.BigWavesSurfing);
                max = Math.Max(max, this.BlackDragon);
                max = Math.Max(max, this.FastRider);
                max = Math.Max(max, this.RockAndRoll);
                max = Math.Max(max, this.RockStar);
                max = Math.Max(max, this.SkullsAndFlourishes);
                max = Math.Max(max, this.TattooLove);

                return max;
            }
        }
        #endregion Maximum

        #region ToString
        /// <summary>
        /// Returns the string representation of this instance.
        /// </summary>
        public override string ToString()
        {
            return this.Year.ToString();
        }
        #endregion ToString
    }
    #endregion SalesSummary class

    #region DateFilter class
    /// <summary>
    /// Encapsulates the filter criteria for a given year.
    /// </summary>
    public class DateFilter
    {
        private bool[] quarters = null;

        /// <summary>Creates a new instance.</summary>
        public DateFilter(int year, bool q1, bool q2, bool q3, bool q4)
        {
            this.Year = year;
            this.quarters = new bool[]{ q1, q2, q3, q4 };
        }

        /// <summary>
        /// Returns the year associated with this filter.
        /// </summary>
        public int Year
        { 
            get; 
            private set;
        }

        /// <summary>
        /// Returns a boolean value indicating whether the year associated
        /// with this filter has any data to display.
        /// </summary>
        public bool IsFilteredIn()
        {
            return this.IsFilteredIn(0);
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified quarter
        /// has any data to display.
        /// </summary>
        public bool IsFilteredIn(int quarter)
        {
            if ( quarter == 0 || quarter > 4 )
                return
                    this.IsFilteredIn(1) ||
                    this.IsFilteredIn(2) ||
                    this.IsFilteredIn(3) ||
                    this.IsFilteredIn(4);
            else
            {
                quarter -= 1;
                return this.quarters[quarter];
            }
        }

        /// <summary>
        /// Returns the quarter in which the specified date falls.
        /// </summary>
        static public int GetQuarter(DateTime date)
        {
            int month = date.Month;
            return month > 9 ? 4 : month > 6 ? 3 : month > 3 ? 2 : 1;
        }

    }
    #endregion DateFilter class
}