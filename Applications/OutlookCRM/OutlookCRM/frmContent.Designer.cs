namespace OutlookCRM
{
    partial class frmContent
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContent));
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Categories", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CategoryID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CategoryName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Picture");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            this.lblPaneDescription = new Infragistics.Win.Misc.UltraLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ultraLegend1 = new Infragistics.Win.DataVisualization.UltraLegend();
            this.ultraPieChart1 = new Infragistics.Win.DataVisualization.UltraPieChart();
            this.ultraDataChart3 = new Infragistics.Win.DataVisualization.UltraDataChart();
            this.ultraDataChart1 = new Infragistics.Win.DataVisualization.UltraDataChart();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
           // this.nWind1 = new OutlookCRM.Data.NWind();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPaneHeader = new Infragistics.Win.Misc.UltraLabel();
            this.inboxControlStyler1 = new Infragistics.Win.AppStyling.Runtime.InboxControlStyler(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraPieChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
          //  ((System.ComponentModel.ISupportInitialize)(this.nWind1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inboxControlStyler1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPaneDescription
            // 
            appearance18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblPaneDescription.Appearance = appearance18;
            resources.ApplyResources(this.lblPaneDescription, "lblPaneDescription");
            this.tableLayoutPanel1.SetColumnSpan(this.lblPaneDescription, 3);
            this.lblPaneDescription.Name = "lblPaneDescription";
            this.lblPaneDescription.Padding = new System.Drawing.Size(3, 0);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ultraLegend1);
            this.splitContainer1.Panel1.Controls.Add(this.ultraGrid1);
            this.inboxControlStyler1.SetStyleSettings(this.splitContainer1.Panel1, new Infragistics.Win.AppStyling.Runtime.InboxControlStyleSettings(Infragistics.Win.DefaultableBoolean.Default));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.inboxControlStyler1.SetStyleSettings(this.splitContainer1, new Infragistics.Win.AppStyling.Runtime.InboxControlStyleSettings(Infragistics.Win.DefaultableBoolean.Default));
            // 
            // ultraLegend1
            // 
            resources.ApplyResources(this.ultraLegend1, "ultraLegend1");
            this.ultraLegend1.Name = "ultraLegend1";
            this.inboxControlStyler1.SetStyleSettings(this.ultraLegend1, new Infragistics.Win.AppStyling.Runtime.InboxControlStyleSettings(Infragistics.Win.DefaultableBoolean.Default));
            // 
            // ultraPieChart1
            // 
            this.ultraPieChart1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.ultraPieChart1, "ultraPieChart1");
            this.ultraPieChart1.FontSize = 14D;
            this.ultraPieChart1.Name = "ultraPieChart1";
            this.tableLayoutPanel1.SetRowSpan(this.ultraPieChart1, 3);
            this.ultraPieChart1.SelectedStyle.Opacity = 1D;
            this.ultraPieChart1.SelectedStyle.Stroke = new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))));
            this.ultraPieChart1.SelectedStyle.StrokeThickness = 6D;
            this.inboxControlStyler1.SetStyleSettings(this.ultraPieChart1, new Infragistics.Win.AppStyling.Runtime.InboxControlStyleSettings(Infragistics.Win.DefaultableBoolean.Default));
            // 
            // ultraDataChart3
            // 
            this.ultraDataChart3.BackColor = System.Drawing.Color.White;
            this.ultraDataChart3.CrosshairPoint = new Infragistics.Win.DataVisualization.Point(double.NaN, double.NaN);
            resources.ApplyResources(this.ultraDataChart3, "ultraDataChart3");
            this.ultraDataChart3.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))));
            this.ultraDataChart3.Name = "ultraDataChart3";
            this.tableLayoutPanel1.SetRowSpan(this.ultraDataChart3, 3);
            this.inboxControlStyler1.SetStyleSettings(this.ultraDataChart3, new Infragistics.Win.AppStyling.Runtime.InboxControlStyleSettings(Infragistics.Win.DefaultableBoolean.Default));
            // 
            // ultraDataChart1
            // 
            this.ultraDataChart1.BackColor = System.Drawing.Color.White;
            this.ultraDataChart1.CrosshairPoint = new Infragistics.Win.DataVisualization.Point(double.NaN, double.NaN);
            resources.ApplyResources(this.ultraDataChart1, "ultraDataChart1");
            this.ultraDataChart1.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))));
            this.ultraDataChart1.Name = "ultraDataChart1";
            this.tableLayoutPanel1.SetRowSpan(this.ultraDataChart1, 3);
            this.inboxControlStyler1.SetStyleSettings(this.ultraDataChart1, new Infragistics.Win.AppStyling.Runtime.InboxControlStyleSettings(Infragistics.Win.DefaultableBoolean.Default));
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.DataSource = this.nWind1;
            appearance3.BackColor = System.Drawing.SystemColors.Window;
            appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(appearance3, "appearance3");
            this.ultraGrid1.DisplayLayout.Appearance = appearance3;
            this.ultraGrid1.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridColumn5.Header.VisiblePosition = 0;
            ultraGridColumn6.Header.VisiblePosition = 1;
            ultraGridColumn7.Header.VisiblePosition = 2;
            ultraGridColumn8.Header.VisiblePosition = 3;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.GroupByBox.Appearance = appearance4;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
            this.ultraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance6.BackColor2 = System.Drawing.SystemColors.Control;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
            this.ultraGrid1.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(168)))), ((int)(((byte)(152)))));
            this.ultraGrid1.DisplayLayout.Override.ActiveCellColumnHeaderAppearance = appearance8;
            appearance9.BackColor = System.Drawing.SystemColors.Highlight;
            appearance9.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance9;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance10;
            appearance11.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGrid1.DisplayLayout.Override.CellAppearance = appearance11;
            this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            this.ultraGrid1.DisplayLayout.Override.CellPadding = 0;
            this.ultraGrid1.DisplayLayout.Override.DefaultRowHeight = 50;
            appearance12.BackColor = System.Drawing.SystemColors.Control;
            appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance12.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance12.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance12;
            appearance13.BackColor = System.Drawing.Color.Transparent;
            appearance13.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            resources.ApplyResources(appearance13, "appearance13");
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance13;
            this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.Standard;
            appearance14.BorderAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance14;
            this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.RowSpacingBefore = 1;
            appearance15.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance15;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            resources.ApplyResources(this.ultraGrid1, "ultraGrid1");
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGrid1_InitializeLayout);
            this.ultraGrid1.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ultraGrid1_InitializeRow);
            this.ultraGrid1.AfterRowActivate += new System.EventHandler(this.ultraGrid1_AfterRowActivate);
            this.ultraGrid1.BeforeSortChange += new Infragistics.Win.UltraWinGrid.BeforeSortChangeEventHandler(this.ultraGrid1_BeforeSortChange);
            this.ultraGrid1.BeforeColPosChanged += new Infragistics.Win.UltraWinGrid.BeforeColPosChangedEventHandler(this.ultraGrid1_BeforeColPosChanged);
            // 
            // nWind1
            // 
            //this.nWind1.DataSetName = "NWind";
            //this.nWind1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            //// 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblPaneDescription, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ultraDataChart3, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblPaneHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ultraPieChart1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ultraDataChart1, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.inboxControlStyler1.SetStyleSettings(this.tableLayoutPanel1, new Infragistics.Win.AppStyling.Runtime.InboxControlStyleSettings(Infragistics.Win.DefaultableBoolean.Default));
            // 
            // lblPaneHeader
            // 
            appearance19.FontData.Name = resources.GetString("resource.Name");
            appearance19.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints")));
            appearance19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lblPaneHeader.Appearance = appearance19;
            resources.ApplyResources(this.lblPaneHeader, "lblPaneHeader");
            this.tableLayoutPanel1.SetColumnSpan(this.lblPaneHeader, 3);
            this.lblPaneHeader.Name = "lblPaneHeader";
            // 
            // frmContent
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmContent";
            this.inboxControlStyler1.SetStyleSettings(this, new Infragistics.Win.AppStyling.Runtime.InboxControlStyleSettings(Infragistics.Win.DefaultableBoolean.Default));
            this.Load += new System.EventHandler(this.frmContent_Load);
            this.Shown += new System.EventHandler(this.frmContent_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraPieChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nWind1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inboxControlStyler1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private Infragistics.Win.Misc.UltraLabel lblPaneDescription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Infragistics.Win.Misc.UltraLabel lblPaneHeader;
      //  private Data.NWind nWind1;
        private Infragistics.Win.AppStyling.Runtime.InboxControlStyler inboxControlStyler1;
        private Infragistics.Win.DataVisualization.UltraDataChart ultraDataChart3;
        private Infragistics.Win.DataVisualization.UltraDataChart ultraDataChart1;
        private Infragistics.Win.DataVisualization.UltraPieChart ultraPieChart1;
        private Infragistics.Win.DataVisualization.UltraLegend ultraLegend1;
    }
}