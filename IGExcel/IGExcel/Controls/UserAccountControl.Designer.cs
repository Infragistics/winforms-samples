namespace IGExcel.Controls
{
    partial class UserAccountControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserAccountControl));
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this.lblName = new Infragistics.Win.Misc.UltraLabel();
            this.lblEmail = new Infragistics.Win.Misc.UltraLabel();
            this.lblAccountSettings = new Infragistics.Win.Misc.UltraLabel();
            this.lblSwitchAccounts = new Infragistics.Win.Misc.UltraLabel();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraPanel1
            // 
            appearance1.BackColor = System.Drawing.Color.White;
            this.ultraPanel1.Appearance = appearance1;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.tableLayoutPanel1);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 0);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(341, 180);
            this.ultraPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Controls.Add(this.ultraPictureBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblEmail, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAccountSettings, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblSwitchAccounts, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 180);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ultraPictureBox1
            // 
            this.ultraPictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
            this.ultraPictureBox1.Image = ((object)(resources.GetObject("ultraPictureBox1.Image")));
            this.ultraPictureBox1.Location = new System.Drawing.Point(18, 10);
            this.ultraPictureBox1.Name = "ultraPictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.ultraPictureBox1, 2);
            this.ultraPictureBox1.Size = new System.Drawing.Size(64, 70);
            this.ultraPictureBox1.TabIndex = 0;
            // 
            // lblName
            // 
            appearance2.FontData.Name = "Segoe UI";
            appearance2.FontData.SizeInPoints = 16F;
            appearance2.TextVAlignAsString = "Bottom";
            this.lblName.Appearance = appearance2;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(103, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(220, 39);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "John Smith";
            // 
            // lblEmail
            // 
            appearance3.FontData.Name = "Segoe UI";
            appearance3.FontData.SizeInPoints = 9F;
            this.lblEmail.Appearance = appearance3;
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmail.Location = new System.Drawing.Point(103, 48);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(220, 39);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "JSmith@demo.infragistics.com";
            // 
            // lblAccountSettings
            // 
            appearance4.FontData.Name = "Segoe UI";
            appearance4.FontData.SizeInPoints = 10F;
            appearance4.FontData.UnderlineAsString = "True";
            appearance4.TextVAlignAsString = "Middle";
            this.lblAccountSettings.Appearance = appearance4;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAccountSettings, 5);
            this.lblAccountSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAccountSettings.Location = new System.Drawing.Point(0, 90);
            this.lblAccountSettings.Margin = new System.Windows.Forms.Padding(0);
            this.lblAccountSettings.Name = "lblAccountSettings";
            this.lblAccountSettings.Padding = new System.Drawing.Size(15, 0);
            this.lblAccountSettings.Size = new System.Drawing.Size(341, 45);
            this.lblAccountSettings.TabIndex = 3;
            this.lblAccountSettings.Text = "Account settings";
            // 
            // lblSwitchAccounts
            // 
            appearance5.FontData.Name = "Segoe UI";
            appearance5.FontData.SizeInPoints = 10F;
            appearance5.FontData.UnderlineAsString = "True";
            appearance5.TextVAlignAsString = "Middle";
            this.lblSwitchAccounts.Appearance = appearance5;
            this.tableLayoutPanel1.SetColumnSpan(this.lblSwitchAccounts, 5);
            this.lblSwitchAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSwitchAccounts.Location = new System.Drawing.Point(0, 135);
            this.lblSwitchAccounts.Margin = new System.Windows.Forms.Padding(0);
            this.lblSwitchAccounts.Name = "lblSwitchAccounts";
            this.lblSwitchAccounts.Padding = new System.Drawing.Size(15, 0);
            this.lblSwitchAccounts.Size = new System.Drawing.Size(341, 45);
            this.lblSwitchAccounts.TabIndex = 4;
            this.lblSwitchAccounts.Text = "Switch account";
            // 
            // UserAccountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ultraPanel1);
            this.Name = "UserAccountControl";
            this.Size = new System.Drawing.Size(341, 180);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Infragistics.Win.UltraWinEditors.UltraPictureBox ultraPictureBox1;
        private Infragistics.Win.Misc.UltraLabel lblName;
        private Infragistics.Win.Misc.UltraLabel lblEmail;
        private Infragistics.Win.Misc.UltraLabel lblAccountSettings;
        private Infragistics.Win.Misc.UltraLabel lblSwitchAccounts;
    }
}
