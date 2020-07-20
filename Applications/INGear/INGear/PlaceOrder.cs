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
    public partial class PlaceOrder : UserControl
    {
        #region Members

        private float pricePerItem;
        private int quantity;
        private DataRow row;
        private Showcase.CustomControl.ModalPanelManager modalPanelManager;

        #endregion //Members

        #region Constructor

        public PlaceOrder()
        {
            InitializeComponent();
        }

        public PlaceOrder(DataRow partData, Showcase.CustomControl.ModalPanelManager modalPanelManager)
            : this()
        {
            this.row = partData;
            this.modalPanelManager = modalPanelManager;
            this.modalPanelManager.AfterClosed += new Showcase.CustomControl.ModalPanelManager.AfterModalClosedEventHandler(modalPanelManager_AfterClosed);
            this.InitializeUI(partData);
        }

        #endregion //Constructor

        #region Event Handlers

        #region btnOK_Click

        private void btnOK_Click(object sender, EventArgs e)
        {
            AutoPartsCatalog.Instance.CreateDelivery(this.row, this.quantity);
            // set the row as modified to reinitialize the grid-row.
            this.row.SetModified();
            this.modalPanelManager.Hide();
        }

        #endregion //btnOK_Click

        #region btnCancel_Click

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.modalPanelManager.Hide();
        }

        #endregion //btnCancel_Click

        #region modalPanelManager_AfterClosed

        void modalPanelManager_AfterClosed(object sender, EventArgs e)
        {
            this.modalPanelManager.AfterClosed -= new Showcase.CustomControl.ModalPanelManager.AfterModalClosedEventHandler(modalPanelManager_AfterClosed);
            this.modalPanelManager = null;
            this.row = null;
        }

        #endregion //modalPanelManager_AfterClosed

        #region nmQuantity_ValueChanged

        private void nmQuantity_ValueChanged(object sender, EventArgs e)
        {
            this.quantity = Convert.ToInt16(nmQuantity.Value);
            this.lblCost.Text = (this.quantity * this.pricePerItem).ToString("C");
        }

        #endregion //nmQuantity_ValueChanged

        #endregion // Event Handlers

        #region Methods

        #region InitializeUI

        private void InitializeUI(DataRow partRow)
        {
            // populate the control values based on the passed in data.
            CategoryType category = (CategoryType)partRow["Category"];
            this.lblCategory.Text = Utilities.CategoryTypeToString(category);
            this.lblPartNumber.Text = Utilities.LocalizeString(partRow["PartNumber"].ToString());
            this.lblManufacturer.Text = Utilities.LocalizeString(partRow["Manufacturer"].ToString());
            this.lblItem.Text = Utilities.LocalizeString(partRow["Component"].ToString());
            this.pbImage.Image = Utilities.GetCachedImage(Utilities.CachedImagesFromCategory((CategoryType)partRow["Category"], true));
            this.pricePerItem = (float)partRow["PricePerItem"];
            this.nmQuantity.Value = 1;
        }

        #endregion //InitializeUI

        #endregion //Methods

    }
}
