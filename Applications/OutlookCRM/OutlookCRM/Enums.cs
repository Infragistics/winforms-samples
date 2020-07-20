
namespace OutlookCRM
{
    public class Enums
    {
        public enum ContentType
        {
            [System.ComponentModel.Description("None")]
            None = 0,
            [System.ComponentModel.Description("Customers")]
            Customers = 1,
            [System.ComponentModel.Description("Orders")]
            Orders = 2,
            [System.ComponentModel.Description("Order Details")]
            OrderDetails = 3,
            [System.ComponentModel.Description("Quarterly Orders")]
            QuarterlyOrders = 4,
            [System.ComponentModel.Description("Products")]
            ProductSales = 5,
            [System.ComponentModel.Description("Categories")]
            SalesByCategory = 6,
            [System.ComponentModel.Description("Sales By Quarter")]
            SalesByQuarter = 7,
            [System.ComponentModel.Description("Sales By Year")]
            SalesByYear = 8,
            [System.ComponentModel.Description("Shippers")]
            Shippers = 9,
            [System.ComponentModel.Description("Suppliers")]
            Suppliers = 10,
        }

        public enum PaneLayout
        {
            Right = 0,
            Bottom = 1,
            Off = 2,
        }
    }
}
