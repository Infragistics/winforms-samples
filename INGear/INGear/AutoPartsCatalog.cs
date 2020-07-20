using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Infragistics.Win.UltraWinSchedule;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace Showcase.INGear
{
    internal class AutoPartsCatalog
    {
        #region Members

        private static AutoPartsCatalog instance;
        private DataSet data;
        private Dictionary<string, Appointment> appointmentTable;
        private string[] manufacturers = new string[] 
            { "ACME", "MotoParts", "DC AutoParts", "AutoMatrix", "Car Parts International", 
                "Standard Automotive", "Karmatic", "Grecko", "Marshall Motors", "Prestige Worldwide",
                "OSC Dynamics" };

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Private constructor used to enforce singleton design.
        /// </summary>
        private AutoPartsCatalog()
        {
            this.CreateData();
        }

        #endregion //Constructor

        #region Properties

        #region DeliveriesTable

        /// <summary>
        /// Gets the deliveries (Appointments) table.
        /// </summary>
        internal DataTable DeliveriesTable
        {
            get
            {
                if (this.data == null)
                    this.CreateData();

                return this.data.Tables["Deliveries"];
            }
        }

        #endregion //DeliveriesTable

        #region Instance

        /// <summary>
        /// Gets the singleton instance of the AutoPartsCatalog class
        /// </summary>
        internal static AutoPartsCatalog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AutoPartsCatalog();
                }
                return instance;
            }
        }


        #endregion //Instance

        #region InventoryTable

        /// <summary>
        /// Gets the inventory table.
        /// </summary>
        internal DataTable InventoryTable
        {
            get
            {
                if (this.data == null)
                    this.CreateData();

                return this.data.Tables["Inventory"];
            }
        }

        #endregion //InventoryTable

        #endregion //Properties

        #region Methods

        #region BindDeliveries

        /// <summary>
        /// Binds the deliveries table to the UltraCalendarInfo, and keep a table to map appointments to the part number.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="bindingContext">The binding context.</param>
        internal void BindDeliveries(UltraCalendarInfo info, Control bindingContext)
        {

            // setup the Appointment bindings
            info.DataBindingsForAppointments.BindingContextControl = bindingContext;
            info.DataBindingsForAppointments.SubjectMember = "Subject";
            info.DataBindingsForAppointments.DescriptionMember = "Description";
            info.DataBindingsForAppointments.StartDateTimeMember = "StartDateTime";
            info.DataBindingsForAppointments.EndDateTimeMember = "EndDateTime";
            info.DataBindingsForAppointments.AllDayEventMember = "AllDayEvent";
            info.DataBindingsForAppointments.DataKeyMember = "DataKey";
            info.DataBindingsForAppointments.SetDataBinding(this.data.Tables["Deliveries"], string.Empty);

            if (this.appointmentTable == null)
                this.appointmentTable = new Dictionary<string, Appointment>();
            else
                this.appointmentTable.Clear();

            // create table
            foreach (Appointment app in info.Appointments)
            {
                app.BarColor = Color.Transparent;
                this.appointmentTable[(string)app.DataKey] = app;
            }
        }

        #endregion //BindDeliveries

        #region CreateData

        /// <summary>
        /// Creates the underlying data.
        /// </summary>
        private void CreateData()
        {
            this.data = new DataSet();

            // create the Delivery table
            DataTable deliveryTable = this.data.Tables.Add("Deliveries");
            deliveryTable.Columns.Add("StartDateTime", typeof(DateTime)); // used in Schedule binding
            deliveryTable.Columns.Add("Category", typeof(CategoryType));
            deliveryTable.Columns.Add("EndDateTime", typeof(DateTime)); // used in Schedule binding
            deliveryTable.Columns.Add("Subject", typeof(string)); // used in Schedule binding
            deliveryTable.Columns.Add("Description", typeof(string)); // used in Schedule binding
            deliveryTable.Columns.Add("AllDayEvent", typeof(bool)); // used in Schedule binding
            deliveryTable.Columns.Add("DataKey", typeof(string)); // used in Schedule binding
            deliveryTable.Columns.Add("Weight");
            deliveryTable.Columns.Add("Cost");
            deliveryTable.Columns.Add("Count", typeof(int));
            
            // create the Inventory table
            DataTable table = data.Tables.Add("Inventory");
            table.Columns.Add("PartNumber");
            table.Columns.Add("Manufacturer");
            table.Columns.Add("Category", typeof(CategoryType));
            table.Columns.Add("Component");
            table.Columns.Add("PricePerItem", typeof(float));
            table.Columns.Add("WeightPerItem", typeof(float));
            table.Columns.Add("InStock", typeof(int));

            // loop through each main Category
            foreach (CategoryType partType in Enum.GetValues(typeof(CategoryType)))
            {
                // for each subcategory, create a random number (2 - 5) of parts.
                foreach (string component in AutoPartsCatalog.GetSubCategories(partType))
                {
                    for (int i = 0; i < Utilities.RandomInt(2, 5); i++)
                    {

                        int numberInStock = Utilities.RandomInt(0, 12);
                         DataRow row = table.Rows.Add(new object[] { 
                            Utilities.GenerateSerialNumber(Utilities.RandomInt(2, 4), Utilities.RandomInt(8, 12)),
                            this.GetManufacturer(),
                            partType,
                            component,
                            Utilities.RandomDouble(2, 30),
                            Utilities.RandomDouble(10, 100),
                            numberInStock,
                        });

                        // if the current stock is less than 2, make a random delivery appointment.
                        if (numberInStock <= 1)
                            this.CreateDelivery(row, Utilities.RandomInt(1, 10));

                    }
                }
            }

            table.AcceptChanges();
        }

        #endregion //CreateData

        #region CreateDelivery

        /// <summary>
        /// Creates a delivery (Appointment) for the provided Inventory DataRow using the provided count.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="count">The count.</param>
        internal void CreateDelivery(DataRow row, int count)
        {
            CategoryType category = (CategoryType)row["Category"];
            DataTable deliveryTable = this.DeliveriesTable;

            string key = (string)row["PartNumber"];
            DateTime deliveryDate = DateTime.Today.AddDays(Utilities.RandomInt(0, 30)).AddHours(8).AddHours(Utilities.RandomInt(0, 2) * 4);
            DataRow newRow = deliveryTable.NewRow();

            // schedule bound values
            newRow["Subject"] = string.Format(Properties.Resources.TrackingNumber + " : {0}", Utilities.GenerateSerialNumber(0, 18));
            newRow["StartDateTime"] = deliveryDate;
            newRow["EndDateTime"] = deliveryDate.AddHours(4);
            newRow["Description"] = string.Format(Properties.Resources.Delivery + " : {0}", Utilities.CategoryTypeToString(category));
            newRow["AllDayEvent"] = false;
            newRow["DataKey"] = key;

            // extra data
            newRow["Count"] = count;
            newRow["Weight"] = (((float)row["WeightPerItem"]) * count).ToString("F2");
            newRow["Cost"] = (((float)row["PricePerItem"]) * count).ToString("C");
            newRow["Category"] = category;

            deliveryTable.Rows.Add(newRow);
        }

        #endregion //CreateDelivery

        #region GetDelivery

        /// <summary>
        /// Gets the delivery (Appointment) for the provided part number
        /// </summary>
        internal Appointment GetDelivery(string partnumber)
        {
            if (this.appointmentTable.ContainsKey(partnumber))
                return this.appointmentTable[partnumber];

            return null;
        }

        #endregion //GetDelivery

        #region GetDeliveryDataRowFromPartNumber

        /// <summary>
        /// Gets the DataRow from the Delivery table for the provided part number
        /// </summary>
        internal DataRow GetDataRowFromDelivery(string partNumber)
        {
            DataRow[] rows = this.DeliveriesTable.Select(string.Format("DataKey = '{0}'", partNumber));

            if (rows == null ||
                rows.Length == 0)
                return null;

            return rows[0];
        }

        #endregion //GetDataRowFromDelivery

        #region GetPartDataRowFromPartNumber

        /// <summary>
        /// Gets the DateRow from the Inventory table for the part number.
        /// </summary>
        internal DataRow GetPartDataRowFromPartNumber(string partNumber)
        {
            DataRow[] rows = this.InventoryTable.Select(string.Format("PartNumber = '{0}'", partNumber));

            if (rows == null ||
                rows.Length == 0)
                return null;

            return rows[0];
        }

        #endregion //GetPartDataRowFromPartNumber

        #region GetManufacturer

        private string GetManufacturer()
        {
            return this.GetManufacturer(Utilities.RandomInt(0, this.manufacturers.Length));
        }

        private string GetManufacturer(int index)
        {
            return this.manufacturers[index];
        }

        #endregion //GetManufacturer

        #region GetSubCategories

        /// <summary>
        /// Gets the subcategories for the provided CategoryType
        /// </summary>
        internal static List<string> GetSubCategories(CategoryType category)
        {
            List<string> list = new List<string>();

            switch (category)
            {
                case CategoryType.Brakes:
                    list.AddRange(new string[] { Properties.Resources.Brakes_Calipers, Properties.Resources.Brakes_Discs, Properties.Resources.Brakes_Drums, Properties.Resources.Brakes_Cables });
                    break;
                case CategoryType.EngineBlocks:
                    list.AddRange(new string[] { Properties.Resources.Engine_Bolts, Properties.Resources.Engine_Gaskets, Properties.Resources.Engine_Pulleys, Properties.Resources.Engine_WaterPumps, Properties.Resources.Engine_Alternators, Properties.Resources.Engine_ValveCovers });
                    break;
                case CategoryType.Pistons:
                    list.AddRange(new string[] { Properties.Resources.Pistons_Rings, Properties.Resources.Pistons_OilPump, Properties.Resources.Pistons_Bearings });
                    break;
                case CategoryType.Sparkplugs:
                    list.AddRange(new string[] { Properties.Resources.Sparkplugs_Plugs, Properties.Resources.Sparkplugs_Wires, Properties.Resources.Sparkplugs_Boots, Properties.Resources.Sparkplugs_Terminals });
                    break;
                case CategoryType.Batteries:
                    list.AddRange(new string[] { Properties.Resources.Batteries_Battery, Properties.Resources.Batteries_Cables, Properties.Resources.Batteries_Clamps, Properties.Resources.Batteries_Bolts });
                    break;
                case CategoryType.Suspension:
                    list.AddRange(new string[] { Properties.Resources.Suspension_Shocks, Properties.Resources.Suspension_Struts });
                    break;
                case CategoryType.Tires:
                    list.AddRange(new string[] { Properties.Resources.Tires_Standard, Properties.Resources.Tires_AllWeather, Properties.Resources.Tires_Snow });
                    break;
                case CategoryType.Transmissions:
                    list.AddRange(new string[] { Properties.Resources.Transmissions_Automatic, Properties.Resources.Transmissions_Manual });
                    break;

            }
            return list;
        }

        #endregion //GetSubCategories

        #endregion //Methods
    }
}
