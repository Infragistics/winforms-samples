namespace Showcase.IGExcel
{
    partial class ofrmMain
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

                //  Dispose of any images we created
                if ( this.images != null )
                    this.images.Dispose();
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ofrmMain));
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
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
            Infragistics.Win.UltraWinCarousel.CarouselItem carouselItem1 = new Infragistics.Win.UltraWinCarousel.CarouselItem();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinCarousel.CarouselItem carouselItem2 = new Infragistics.Win.UltraWinCarousel.CarouselItem();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinCarousel.CarouselItem carouselItem3 = new Infragistics.Win.UltraWinCarousel.CarouselItem();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinCarousel.CarouselPath carouselPath1 = new Infragistics.Win.UltraWinCarousel.CarouselPath("EllipseBottom", new System.Drawing.PointF[] {
            ((System.Drawing.PointF)(resources.GetObject("carousel.Path"))),
            ((System.Drawing.PointF)(resources.GetObject("carousel.Path1")))}, new byte[] {
            ((byte)(0)),
            ((byte)(1))});
            this.pnlMain = new Infragistics.Win.Misc.UltraPanel();
            this.cmdDateFilter = new Infragistics.Win.Misc.UltraButton();
            this.lblSelected = new Infragistics.Win.Misc.UltraLabel();
            this.grid = new Infragistics.Win.UltraWinPivotGrid.UltraPivotGrid();
            this.chart = new Infragistics.Win.DataVisualization.UltraDataChart();
            this.lbl3167A = new Infragistics.Win.Misc.UltraLabel();
            this.lbl6917A = new Infragistics.Win.Misc.UltraLabel();
            this.lbl9002A = new Infragistics.Win.Misc.UltraLabel();
            this.lbl8711A = new Infragistics.Win.Misc.UltraLabel();
            this.lbl6309A = new Infragistics.Win.Misc.UltraLabel();
            this.lbl7188A = new Infragistics.Win.Misc.UltraLabel();
            this.lbl4589A = new Infragistics.Win.Misc.UltraLabel();
            this.lbl3650A = new Infragistics.Win.Misc.UltraLabel();
            this.lblTitle = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.carousel = new Infragistics.Win.UltraWinCarousel.UltraCarousel();
            this.formManager = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this._frmMain_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmMain_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmMain_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmMain_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.pnlMain.ClientArea.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carousel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formManager)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.pnlMain.Appearance = appearance1;
            // 
            // pnlMain.ClientArea
            // 
            this.pnlMain.ClientArea.Controls.Add(this.cmdDateFilter);
            this.pnlMain.ClientArea.Controls.Add(this.lblSelected);
            this.pnlMain.ClientArea.Controls.Add(this.grid);
            this.pnlMain.ClientArea.Controls.Add(this.chart);
            this.pnlMain.ClientArea.Controls.Add(this.lbl3167A);
            this.pnlMain.ClientArea.Controls.Add(this.lbl6917A);
            this.pnlMain.ClientArea.Controls.Add(this.lbl9002A);
            this.pnlMain.ClientArea.Controls.Add(this.lbl8711A);
            this.pnlMain.ClientArea.Controls.Add(this.lbl6309A);
            this.pnlMain.ClientArea.Controls.Add(this.lbl7188A);
            this.pnlMain.ClientArea.Controls.Add(this.lbl4589A);
            this.pnlMain.ClientArea.Controls.Add(this.lbl3650A);
            this.pnlMain.ClientArea.Controls.Add(this.lblTitle);
            this.pnlMain.ClientArea.Controls.Add(this.carousel);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // cmdDateFilter
            // 
            resources.ApplyResources(this.cmdDateFilter, "cmdDateFilter");
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(114)))), ((int)(((byte)(130)))));
            appearance2.Image = ((object)(resources.GetObject("appearance2.Image")));
            appearance2.ImageHAlign = Infragistics.Win.HAlign.Right;
            resources.ApplyResources(appearance2, "appearance2");
            this.cmdDateFilter.Appearance = appearance2;
            this.cmdDateFilter.BackColorInternal = System.Drawing.Color.Gray;
            this.cmdDateFilter.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            this.cmdDateFilter.ForeColor = System.Drawing.Color.White;
            this.cmdDateFilter.ImageSize = new System.Drawing.Size(23, 23);
            this.cmdDateFilter.Name = "cmdDateFilter";
            this.cmdDateFilter.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.cmdDateFilter.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // lblSelected
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            appearance3.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints")));
            appearance3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance3, "appearance3");
            this.lblSelected.Appearance = appearance3;
            resources.ApplyResources(this.lblSelected, "lblSelected");
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.lblSelected.UseMnemonic = false;
            // 
            // grid
            // 
            this.grid.AllowDrop = true;
            resources.ApplyResources(this.grid, "grid");
            appearance4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.grid.Appearance = appearance4;
            this.grid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            appearance5.BackColor = System.Drawing.Color.Transparent;
            appearance5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(114)))), ((int)(((byte)(130)))));
            this.grid.DropAreaAppearance.Normal = appearance5;
            this.grid.Name = "grid";
            this.grid.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.grid.UseServerFormat = true;
            // 
            // chart
            // 
            resources.ApplyResources(this.chart, "chart");
            this.chart.BackColor = System.Drawing.SystemColors.ControlDark;
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.Brushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.CrosshairPoint = new Infragistics.Win.DataVisualization.Point(double.NaN, double.NaN);
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(181)))), ((int)(((byte)(197)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(174)))), ((int)(((byte)(122)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(169)))), ((int)(((byte)(88)))))));
            this.chart.MarkerBrushes.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(125)))), ((int)(((byte)(191)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.MarkerOutlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Name = "chart";
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(125)))), ((int)(((byte)(141)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(143)))), ((int)(((byte)(143)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(143)))), ((int)(((byte)(88)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(126)))), ((int)(((byte)(17)))))));
            this.chart.Outlines.Add(new Infragistics.Win.DataVisualization.SolidColorBrush(System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(88)))), ((int)(((byte)(162)))))));
            this.chart.PreviewRect = new Infragistics.Win.DataVisualization.Rectangle(double.PositiveInfinity, double.PositiveInfinity, double.NegativeInfinity, double.NegativeInfinity);
            this.chart.TitleFontSize = 12D;
            // 
            // lbl3167A
            // 
            resources.ApplyResources(this.lbl3167A, "lbl3167A");
            appearance6.BackColor = System.Drawing.Color.Transparent;
            appearance6.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints1")));
            appearance6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance6, "appearance6");
            this.lbl3167A.Appearance = appearance6;
            this.lbl3167A.Name = "lbl3167A";
            this.lbl3167A.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.lbl3167A.UseMnemonic = false;
            // 
            // lbl6917A
            // 
            resources.ApplyResources(this.lbl6917A, "lbl6917A");
            appearance7.BackColor = System.Drawing.Color.Transparent;
            appearance7.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints2")));
            appearance7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance7, "appearance7");
            this.lbl6917A.Appearance = appearance7;
            this.lbl6917A.Name = "lbl6917A";
            this.lbl6917A.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.lbl6917A.UseMnemonic = false;
            // 
            // lbl9002A
            // 
            resources.ApplyResources(this.lbl9002A, "lbl9002A");
            appearance8.BackColor = System.Drawing.Color.Transparent;
            appearance8.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints3")));
            appearance8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance8, "appearance8");
            this.lbl9002A.Appearance = appearance8;
            this.lbl9002A.Name = "lbl9002A";
            this.lbl9002A.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.lbl9002A.UseMnemonic = false;
            // 
            // lbl8711A
            // 
            resources.ApplyResources(this.lbl8711A, "lbl8711A");
            appearance9.BackColor = System.Drawing.Color.Transparent;
            appearance9.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints4")));
            appearance9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance9, "appearance9");
            this.lbl8711A.Appearance = appearance9;
            this.lbl8711A.Name = "lbl8711A";
            this.lbl8711A.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.lbl8711A.UseMnemonic = false;
            // 
            // lbl6309A
            // 
            resources.ApplyResources(this.lbl6309A, "lbl6309A");
            appearance10.BackColor = System.Drawing.Color.Transparent;
            appearance10.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints5")));
            appearance10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance10, "appearance10");
            this.lbl6309A.Appearance = appearance10;
            this.lbl6309A.Name = "lbl6309A";
            this.lbl6309A.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            // 
            // lbl7188A
            // 
            resources.ApplyResources(this.lbl7188A, "lbl7188A");
            appearance11.BackColor = System.Drawing.Color.Transparent;
            appearance11.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints6")));
            appearance11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance11, "appearance11");
            this.lbl7188A.Appearance = appearance11;
            this.lbl7188A.Name = "lbl7188A";
            this.lbl7188A.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            // 
            // lbl4589A
            // 
            resources.ApplyResources(this.lbl4589A, "lbl4589A");
            appearance12.BackColor = System.Drawing.Color.Transparent;
            appearance12.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints7")));
            appearance12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance12, "appearance12");
            this.lbl4589A.Appearance = appearance12;
            this.lbl4589A.Name = "lbl4589A";
            this.lbl4589A.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            // 
            // lbl3650A
            // 
            resources.ApplyResources(this.lbl3650A, "lbl3650A");
            appearance13.BackColor = System.Drawing.Color.Transparent;
            appearance13.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints8")));
            appearance13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(78)))), ((int)(((byte)(80)))));
            resources.ApplyResources(appearance13, "appearance13");
            this.lbl3650A.Appearance = appearance13;
            this.lbl3650A.Name = "lbl3650A";
            this.lbl3650A.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            // 
            // lblTitle
            // 
            appearance14.BackColor = System.Drawing.Color.Transparent;
            appearance14.FontData.Name = resources.GetString("resource.Name");
            this.lblTitle.Appearance = appearance14;
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabStop = true;
            this.lblTitle.TextSmoothingMode = Infragistics.Win.FormattedLinkLabel.TextSmoothingMode.AntiAlias;
            // 
            // carousel
            // 
            appearance15.BackColor = System.Drawing.Color.Transparent;
            appearance15.BorderColor = System.Drawing.Color.Transparent;
            this.carousel.Appearance = appearance15;
            resources.ApplyResources(this.carousel, "carousel");
            appearance16.Image = ((object)(resources.GetObject("appearance16.Image")));
            carouselItem1.Settings.Appearance = appearance16;
            appearance17.Image = ((object)(resources.GetObject("appearance17.Image")));
            carouselItem2.Settings.Appearance = appearance17;
            appearance18.Image = ((object)(resources.GetObject("appearance18.Image")));
            carouselItem3.Settings.Appearance = appearance18;
            this.carousel.Items.AddRange(new Infragistics.Win.UltraWinCarousel.CarouselItem[] {
            carouselItem1,
            carouselItem2,
            carouselItem3});
            appearance19.BackColor = System.Drawing.Color.Transparent;
            appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance19.BorderColor = System.Drawing.Color.Transparent;
            this.carousel.ItemSettings.Appearance = appearance19;
            this.carousel.ItemSize = new System.Drawing.Size(239, 365);
            this.carousel.Name = "carousel";
            carouselPath1.ItemSlots = 3;
            carouselPath1.Name = "EllipseBottom";
            carouselPath1.PathData.Points = new System.Drawing.PointF[] {
        ((System.Drawing.PointF)(resources.GetObject("resource.Points"))),
        ((System.Drawing.PointF)(resources.GetObject("resource.Points1")))};
            carouselPath1.PathData.Types = new byte[] {
        ((byte)(0)),
        ((byte)(1))};
            carouselPath1.PathMargin = new System.Windows.Forms.Padding(-1, -40, -1, -40);
            carouselPath1.ScalingStops.Add(new Infragistics.Win.UltraWinCarousel.PathScalingStop(0.25F, 0.4F));
            carouselPath1.ScalingStops.Add(new Infragistics.Win.UltraWinCarousel.PathScalingStop(0.5F, 1F));
            carouselPath1.ScalingStops.Add(new Infragistics.Win.UltraWinCarousel.PathScalingStop(0.75F, 0.4F));
            this.carousel.Path = carouselPath1;
            this.carousel.PathColor = System.Drawing.Color.Transparent;
            this.carousel.ScrollButtons = Infragistics.Win.UltraWinCarousel.CarouselScrollButtonTypes.None;
            this.carousel.ScrollingOrientation = System.Windows.Forms.Orientation.Vertical;
            this.carousel.ShowPath = false;
            // 
            // formManager
            // 
            this.formManager.Form = this;
            this.formManager.FormStyleSettings.Style = Infragistics.Win.UltraWinForm.UltraFormStyle.Office2013;
            // 
            // _frmMain_UltraFormManager_Dock_Area_Left
            // 
            this._frmMain_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmMain_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._frmMain_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frmMain_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmMain_UltraFormManager_Dock_Area_Left.FormManager = this.formManager;
            this._frmMain_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 1;
            resources.ApplyResources(this._frmMain_UltraFormManager_Dock_Area_Left, "_frmMain_UltraFormManager_Dock_Area_Left");
            this._frmMain_UltraFormManager_Dock_Area_Left.Name = "_frmMain_UltraFormManager_Dock_Area_Left";
            // 
            // _frmMain_UltraFormManager_Dock_Area_Right
            // 
            this._frmMain_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmMain_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._frmMain_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frmMain_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmMain_UltraFormManager_Dock_Area_Right.FormManager = this.formManager;
            this._frmMain_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 1;
            resources.ApplyResources(this._frmMain_UltraFormManager_Dock_Area_Right, "_frmMain_UltraFormManager_Dock_Area_Right");
            this._frmMain_UltraFormManager_Dock_Area_Right.Name = "_frmMain_UltraFormManager_Dock_Area_Right";
            // 
            // _frmMain_UltraFormManager_Dock_Area_Top
            // 
            this._frmMain_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmMain_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._frmMain_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frmMain_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmMain_UltraFormManager_Dock_Area_Top.FormManager = this.formManager;
            resources.ApplyResources(this._frmMain_UltraFormManager_Dock_Area_Top, "_frmMain_UltraFormManager_Dock_Area_Top");
            this._frmMain_UltraFormManager_Dock_Area_Top.Name = "_frmMain_UltraFormManager_Dock_Area_Top";
            // 
            // _frmMain_UltraFormManager_Dock_Area_Bottom
            // 
            this._frmMain_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmMain_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._frmMain_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frmMain_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmMain_UltraFormManager_Dock_Area_Bottom.FormManager = this.formManager;
            this._frmMain_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 1;
            resources.ApplyResources(this._frmMain_UltraFormManager_Dock_Area_Bottom, "_frmMain_UltraFormManager_Dock_Area_Bottom");
            this._frmMain_UltraFormManager_Dock_Area_Bottom.Name = "_frmMain_UltraFormManager_Dock_Area_Bottom";
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this._frmMain_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frmMain_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frmMain_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frmMain_UltraFormManager_Dock_Area_Bottom);
            this.Name = "frmMain";
            this.pnlMain.ClientArea.ResumeLayout(false);
            this.pnlMain.ClientArea.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carousel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel pnlMain;
        private Infragistics.Win.UltraWinPivotGrid.UltraPivotGrid grid;
        private Infragistics.Win.DataVisualization.UltraDataChart chart;
        private Infragistics.Win.Misc.UltraLabel lbl3167A;
        private Infragistics.Win.Misc.UltraLabel lbl6917A;
        private Infragistics.Win.Misc.UltraLabel lbl9002A;
        private Infragistics.Win.Misc.UltraLabel lbl8711A;
        private Infragistics.Win.Misc.UltraLabel lbl6309A;
        private Infragistics.Win.Misc.UltraLabel lbl7188A;
        private Infragistics.Win.Misc.UltraLabel lbl4589A;
        private Infragistics.Win.Misc.UltraLabel lbl3650A;
        private Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel lblTitle;
        private Infragistics.Win.UltraWinCarousel.UltraCarousel carousel;
        private Infragistics.Win.Misc.UltraLabel lblSelected;
        private Infragistics.Win.UltraWinForm.UltraFormManager formManager;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmMain_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmMain_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmMain_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmMain_UltraFormManager_Dock_Area_Bottom;
        private Infragistics.Win.Misc.UltraButton cmdDateFilter;

    }
}

