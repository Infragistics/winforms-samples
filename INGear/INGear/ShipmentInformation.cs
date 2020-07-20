using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Showcase.INGear
{
    public partial class ShipmentInformation : UserControl
    {

        #region Constructors

        public ShipmentInformation()
        {
            InitializeComponent();
        }

        public ShipmentInformation(DataRow deliveryData, DataRow partData) : this()
        {
            this.InitializeUI(deliveryData, partData);
        }

        #endregion // Constructors

        #region Methods

        #region InitializeUI

        private void InitializeUI(DataRow deliveryRow, DataRow partRow)
        {
            // localize the labels
            foreach (Control c in new Control[] { 
                this.lblCategoryLabel,
                this.lblCostLabel,
                this.lblCountLabel,
                this.lblInvoice,
                this.lblItemLabel,
                this.lblManufacturerLabel,
                this.lblPartNumberLabel,
                this.lblTrackingNumberLabel,
                this.lblWeightLabel})
                c.Text = Utilities.LocalizeString(c.Text);
                
            // populate the control values based on the passed in data.
            CategoryType category = (CategoryType)deliveryRow["Category"];
            string trackingNumber = deliveryRow["Subject"].ToString();
            
            this.lblTrackingNumber.Text = trackingNumber.Substring(trackingNumber.LastIndexOf(' '));
            this.lblCategory.Text = Utilities.CategoryTypeToString(category);
            this.lblPartNumber.Text = deliveryRow["DataKey"].ToString();
            this.lblCount.Text = deliveryRow["Count"].ToString();
            this.lblWeight.Text = deliveryRow["Weight"].ToString();
            this.lblCost.Text = deliveryRow["Cost"].ToString();
            this.lblManufacturer.Text = partRow["Manufacturer"].ToString();
            this.lblItem.Text = partRow["Component"].ToString();
            this.pbImage.Image = Utilities.GetCachedImage(Utilities.CachedImagesFromCategory(category, true));
        }

        #endregion //InitializeUI

        #endregion //Methods
    }
}
