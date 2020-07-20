using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Showcase.InventoryManagement
{
    /// <summary>
    /// Temporary datasource for the InventoryManagement application.
    /// </summary>
    public class InventoryDataSource
    {
        #region Members

        private DataSet dataSet;
        private static InventoryDataSource instance;
        private Random random;

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="InventoryDataSource"/> class from being created.
        /// </summary>
        private InventoryDataSource()
        {
        }        

        #endregion //Constructor

        #region Properties

        #region Instance

        private static InventoryDataSource Instance
        {
            get
            {
                if (instance == null)
                    instance = new InventoryDataSource();
                return instance;
            }
        }

        #endregion //Instance

        #region DataSet

        private DataSet DataSet
        {
            get
            {
                if (this.dataSet == null)
                    this.GenerateData();

                return this.dataSet;
            }
        }

        #endregion //DataSet

        #region Random

        /// <summary>
        /// Gets a Random instance.
        /// </summary>
        /// <value>
        /// The random.
        /// </value>
        internal static Random Random
        {
            get
            {
                if (Instance.random == null)
                    Instance.random = new Random();
                return Instance.random;
            }
        }

        #endregion //Random

        #endregion //Properties

        #region Methods

        #region GetTable

        /// <summary>
        /// Gets the requested table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        internal static DataTable GetTable(Table table)
        {
            return Instance.DataSet.Tables[table.ToString()];
        }

        #endregion //GetTable

        #region GenerateData

        /// <summary>
        /// Generates the underlying data
        /// </summary>
        private void GenerateData()
        {
            Random random = InventoryDataSource.Random;
            this.dataSet = new DataSet();

            foreach (Table tableType in Enum.GetValues(typeof(Table)))
            {
                DataTable dataTable = this.dataSet.Tables.Add(tableType.ToString());
                switch (tableType)
                {
                    case Table.Inventory:
                        {
                            dataTable.Columns.Add("ID", typeof(int));
                            dataTable.Columns.Add("Category", typeof(string));
                            dataTable.Columns.Add("Name", typeof(string));
                            dataTable.Columns.Add("Description", typeof(string));
                            dataTable.Columns.Add("Quantity", typeof(int));
                            dataTable.Columns.Add("Cost", typeof(double));
                            dataTable.Columns.Add("Price", typeof(double));
                            dataTable.Columns.Add("Sales", typeof(double));
                            dataTable.Columns.Add("Profit", typeof(double));
                            dataTable.Columns.Add("IsNew", typeof(bool));

                            dataTable.Columns["Name"].Caption = Properties.Resources.Name;
                            dataTable.Columns["Quantity"].Caption = Properties.Resources.Quantity;
                            dataTable.Columns["Cost"].Caption = Properties.Resources.Cost;
                            dataTable.Columns["Price"].Caption = Properties.Resources.Price;

                            for (int i = 1; i <= 30; i++)
                            {
                                int amountSold = random.Next(200);
                                int cost = random.Next(1, 20);
                                int price = random.Next(cost + 1, 100);

                                double sales = amountSold * price;
                                double profit = amountSold * (price - cost);

                                dataTable.Rows.Add(new object[] {
                                    i,
                                    string.Format(Properties.Resources.Category + " {0}", i % 5),
                                    string.Format(Properties.Resources.ProductName + " {0}", i),
                                    Properties.Resources.Description,
                                    random.Next(200),
                                    cost,
                                    price,
                                    sales,
                                    profit,
                                    ((i % 5) == 0)
                                });
                            }
                        }
                        break;
                    case Table.Orders:
                        {
                            dataTable.Columns.Add("Subject", typeof(string));
                            dataTable.Columns.Add("StartDateTime", typeof(DateTime));
                            dataTable.Columns.Add("EndDateTime", typeof(DateTime));
                            dataTable.Columns.Add("Description", typeof(string));



                            DateTime todayAt8 = DateTime.Today.AddHours(8);
                            for (int i = 0; i < 30; i++)
                            {
                                int dayOffset = random.Next(0, 14);
                                int halfHourOffset = random.Next(0, 16);
                                bool fullHour = (random.Next(2) % 2) == 0;

                                DateTime start = todayAt8.AddDays(dayOffset).AddHours(halfHourOffset / 2).AddMinutes(30 * (halfHourOffset % 2));
                                DateTime end = start.AddMinutes((fullHour? 60 : 30));

                                dataTable.Rows.Add(new object[] {
                                    Properties.Resources.AppointmentText,
                                    start,
                                    end,
                                    Properties.Resources.ProductOrder});                                   
                            }
                        }
                        break;
                    case Table.Contacts:
                        {
                            int contactTypeCount = Enum.GetValues(typeof(ContactType)).Length;

                            dataTable.Columns.Add("Company_Name", typeof(string));
                            dataTable.Columns.Add("Contact_Name", typeof(string));
                            dataTable.Columns.Add("Type", typeof(string));
                            dataTable.Columns.Add("E-mail_Address", typeof(string));
                            dataTable.Columns.Add("Phone_Number", typeof(string));

                            dataTable.Columns["Company_Name"].Caption = Properties.Resources.CompanyName;
                            dataTable.Columns["Contact_Name"].Caption = Properties.Resources.ContactName;
                            dataTable.Columns["Type"].Caption = Properties.Resources.Type;
                            dataTable.Columns["E-mail_Address"].Caption = Properties.Resources.EmailAddress;
                            dataTable.Columns["Phone_Number"].Caption = Properties.Resources.PhoneNumber;

                            for (int i = 1; i < 20; i++)
                            {
                                dataTable.Rows.Add(new object[] {
                                    Properties.Resources.CompanyName,
                                    Properties.Resources.CompanyContactName,
                                    InventoryManagementForm.GetLocalizedString(((ContactType)random.Next(0, contactTypeCount)).ToString()),
                                    "contactname@companyname.com",
                                    "123-456-7890"
                                    });
                            }
                        }
                        break;
                    case Table.ChartData1:
                        {
                            dataTable.Columns.Add("Month");
                            dataTable.Columns.Add(Properties.Resources.Sales);
                            dataTable.Columns.Add(Properties.Resources.Profit);
                            dataTable.Columns.Add(Properties.Resources.Cost);

                            int min = 0;
                            int max = 300;
                            for (int i = 0; i < 12; i++)
                            {
                                DataRow row = dataTable.NewRow();
                                row[0] = DateTime.Today.AddMonths(i);
                                row[1] = random.Next(min, max);
                                row[2] = random.Next(min, max);
                                row[3] = random.Next(min, max);

                                min = min + 100;
                                max = max + 200;

                                dataTable.Rows.Add(row);
                            }                            
                        }
                        break;

                    case Table.ChartData2:
                        {
                            dataTable.Columns.Add("Month");
                            dataTable.Columns.Add("Week 1", typeof(int));
                            dataTable.Columns.Add("Week 2", typeof(int));
                            dataTable.Columns.Add("Week 3", typeof(int));
                            dataTable.Columns.Add("Week 4", typeof(int));

                            for (int i = 5; i >= 0; i--)
                            {
                                dataTable.Rows.Add(
                                    new object[] { 
                                        DateTime.Today.AddMonths(0 - i).ToString("y"),
                                        random.Next(100, 1000),
                                        random.Next(100, 1000),
                                        random.Next(100, 1000),
                                        random.Next(100, 1000)
                                    });
                            }
                        }
                        break;
                }
            }

        }

        #endregion //GenerateData

        #endregion //Methods

    }
}
