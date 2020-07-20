namespace Showcase.CashflowDashboard
{
    partial class SummaryBlock
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
                
                this.DisposeResources();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryBlock));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            this.pnlCentered = new Infragistics.Win.Misc.UltraPanel();
            this.lblTitle = new Infragistics.Win.Misc.UltraLabel();
            this.lblLastYearValue = new Infragistics.Win.Misc.UltraLabel();
            this.lblLastMonthValue = new Infragistics.Win.Misc.UltraLabel();
            this.lblLastYear = new Infragistics.Win.Misc.UltraLabel();
            this.lblLastMonth = new Infragistics.Win.Misc.UltraLabel();
            this.lblCurrentlValue = new Infragistics.Win.Misc.UltraLabel();
            this.pnlCentered.ClientArea.SuspendLayout();
            this.pnlCentered.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCentered
            // 
            resources.ApplyResources(this.pnlCentered, "pnlCentered");
            // 
            // pnlCentered.ClientArea
            // 
            this.pnlCentered.ClientArea.Controls.Add(this.lblTitle);
            this.pnlCentered.ClientArea.Controls.Add(this.lblLastYearValue);
            this.pnlCentered.ClientArea.Controls.Add(this.lblLastMonthValue);
            this.pnlCentered.ClientArea.Controls.Add(this.lblLastYear);
            this.pnlCentered.ClientArea.Controls.Add(this.lblLastMonth);
            this.pnlCentered.ClientArea.Controls.Add(this.lblCurrentlValue);
            this.pnlCentered.Name = "pnlCentered";
            // 
            // lblTitle
            // 
            appearance1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(174)))), ((int)(((byte)(187)))));
            this.lblTitle.Appearance = appearance1;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // lblLastYearValue
            // 
            appearance2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(148)))), ((int)(((byte)(189)))));
            this.lblLastYearValue.Appearance = appearance2;
            resources.ApplyResources(this.lblLastYearValue, "lblLastYearValue");
            this.lblLastYearValue.Name = "lblLastYearValue";
            // 
            // lblLastMonthValue
            // 
            appearance3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(148)))), ((int)(((byte)(189)))));
            this.lblLastMonthValue.Appearance = appearance3;
            resources.ApplyResources(this.lblLastMonthValue, "lblLastMonthValue");
            this.lblLastMonthValue.Name = "lblLastMonthValue";
            // 
            // lblLastYear
            // 
            appearance4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(148)))), ((int)(((byte)(189)))));
            this.lblLastYear.Appearance = appearance4;
            resources.ApplyResources(this.lblLastYear, "lblLastYear");
            this.lblLastYear.Name = "lblLastYear";
            // 
            // lblLastMonth
            // 
            appearance5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(148)))), ((int)(((byte)(189)))));
            this.lblLastMonth.Appearance = appearance5;
            resources.ApplyResources(this.lblLastMonth, "lblLastMonth");
            this.lblLastMonth.Name = "lblLastMonth";
            // 
            // lblCurrentlValue
            // 
            appearance6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblCurrentlValue.Appearance = appearance6;
            resources.ApplyResources(this.lblCurrentlValue, "lblCurrentlValue");
            this.lblCurrentlValue.Name = "lblCurrentlValue";
            // 
            // SummaryBlock
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(46)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.pnlCentered);
            resources.ApplyResources(this, "$this");
            this.Name = "SummaryBlock";
            this.pnlCentered.ClientArea.ResumeLayout(false);
            this.pnlCentered.ClientArea.PerformLayout();
            this.pnlCentered.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel pnlCentered;
        private Infragistics.Win.Misc.UltraLabel lblLastYearValue;
        private Infragistics.Win.Misc.UltraLabel lblLastMonthValue;
        private Infragistics.Win.Misc.UltraLabel lblLastYear;
        private Infragistics.Win.Misc.UltraLabel lblLastMonth;
        private Infragistics.Win.Misc.UltraLabel lblTitle;
        private Infragistics.Win.Misc.UltraLabel lblCurrentlValue;

    }
}
