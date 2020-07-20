namespace OutlookCRM
{
    partial class AboutControl
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
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            this.lblDescription = new Infragistics.Win.Misc.UltraLabel();
            this.pbLogo = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this.lblCompany = new Infragistics.Win.Misc.UltraLabel();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAppName = new Infragistics.Win.Misc.UltraLabel();
            this.lblVersion = new Infragistics.Win.Misc.UltraLabel();
            this.lblCopyright = new Infragistics.Win.Misc.UltraLabel();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            appearance1.FontData.SizeInPoints = 12F;
            this.lblDescription.Appearance = appearance1;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(57, 113);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblDescription.MaximumSize = new System.Drawing.Size(800, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(190, 26);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description";
            // 
            // pbLogo
            // 
            this.pbLogo.BorderShadowColor = System.Drawing.Color.Empty;
            this.pbLogo.Location = new System.Drawing.Point(282, 66);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbLogo.Name = "pbLogo";
            this.tableLayoutPanel1.SetRowSpan(this.pbLogo, 11);
            this.pbLogo.ScaleImage = Infragistics.Win.ScaleImage.Always;
            this.pbLogo.Size = new System.Drawing.Size(200, 185);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.UseAppStyling = false;
            // 
            // lblCompany
            // 
            appearance3.FontData.SizeInPoints = 12F;
            this.lblCompany.Appearance = appearance3;
            this.lblCompany.AutoSize = true;
            this.lblCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCompany.Location = new System.Drawing.Point(57, 199);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(190, 26);
            this.lblCompany.TabIndex = 5;
            this.lblCompany.Text = "lblCompany";
            // 
            // ultraPanel1
            // 
            this.ultraPanel1.AutoScroll = true;
            this.ultraPanel1.AutoSize = true;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.tableLayoutPanel1);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 0);
            this.ultraPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(917, 369);
            this.ultraPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 368F));
            this.tableLayoutPanel1.Controls.Add(this.lblDescription, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pbLogo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAppName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCompany, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblVersion, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblCopyright, 1, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(917, 369);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblAppName
            // 
            appearance2.FontData.SizeInPoints = 14F;
            this.lblAppName.Appearance = appearance2;
            this.lblAppName.AutoSize = true;
            this.lblAppName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppName.Location = new System.Drawing.Point(57, 66);
            this.lblAppName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(190, 30);
            this.lblAppName.TabIndex = 2;
            this.lblAppName.Text = "ApplicationName";
            // 
            // lblVersion
            // 
            appearance4.FontData.SizeInPoints = 12F;
            this.lblVersion.Appearance = appearance4;
            this.lblVersion.AutoSize = true;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Location = new System.Drawing.Point(57, 156);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(190, 26);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "Version";
            // 
            // lblCopyright
            // 
            appearance5.FontData.SizeInPoints = 12F;
            this.lblCopyright.Appearance = appearance5;
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCopyright.Location = new System.Drawing.Point(57, 242);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(190, 26);
            this.lblCopyright.TabIndex = 6;
            this.lblCopyright.Text = "lblCopyRight";
            // 
            // AboutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ultraPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AboutControl";
            this.Size = new System.Drawing.Size(917, 369);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ClientArea.PerformLayout();
            this.ultraPanel1.ResumeLayout(false);
            this.ultraPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.Misc.UltraLabel lblDescription;
        private Infragistics.Win.UltraWinEditors.UltraPictureBox pbLogo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Infragistics.Win.Misc.UltraLabel lblAppName;
        private Infragistics.Win.Misc.UltraLabel lblCompany;
        private Infragistics.Win.Misc.UltraLabel lblVersion;
        private Infragistics.Win.Misc.UltraLabel lblCopyright;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;

    }
}
