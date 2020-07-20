using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;

namespace IGExcel.Controls
{
    public partial class UserAccountControl : UserControl, IUIElementDrawFilter
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccountControl"/> class.
        /// </summary>
        public UserAccountControl()
        {
            InitializeComponent();

            this.InitializeUI();
        }

        #endregion

        #region InitializeUI

        private void InitializeUI()
        {
            this.LocalizeStrings();
            
            this.lblAccountSettings.DrawFilter = this.lblSwitchAccounts.DrawFilter = this;

            this.ultraPanel1.Appearance.BackColor = Infragistics.Win.Office2013ColorTable.Colors.MenuDroppedBackColor;
            this.lblAccountSettings.Appearance.BorderColor =
                this.lblSwitchAccounts.Appearance.BorderColor =
                Infragistics.Win.Office2013ColorTable.Colors.PopupMenuBorder;
        }

        #endregion //InitializeUI

        #region LocalizeStrings

        private void LocalizeStrings()
        {
            this.lblName.Text = ResourceStrings.Ig_UserName_String;
            this.lblEmail.Text = ResourceStrings.Ig_UserEmail_String;
            this.lblAccountSettings.Text = ResourceStrings.Text_AccountSettings;
            this.lblSwitchAccounts.Text = ResourceStrings.Text_SwitchAccount;
        }

        #endregion //LocalizeStrings

        #region IUIElementDrawFilter

        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            drawParams.DrawBorders(UIElementBorderStyle.Solid, Border3DSide.Top, drawParams.Element.Rect);
            return true;
        }

        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (drawParams.Element is Infragistics.Win.Misc.UltraLabelControlUIElement)
                return DrawPhase.BeforeDrawBorders;
            return DrawPhase.None;
        }

        #endregion //IUIElementDrawFilter
    }
}
