namespace Showcase.INGear
{
    partial class MainForm
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

            this.ultraCalendarInfo1.CalendarInfoChanged += new Infragistics.Win.UltraWinSchedule.CalendarInfoChangedEventHandler(ultraCalendarInfo1_CalendarInfoChanged);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab(true);
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            this.ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.dvSchedule = new Infragistics.Win.UltraWinSchedule.UltraDayView();
            this.ultraCalendarInfo1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarInfo(this.components);
            this.ultraCalendarLook1 = new Infragistics.Win.UltraWinSchedule.UltraCalendarLook(this.components);
            this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.wvSchedule = new Infragistics.Win.UltraWinSchedule.UltraWeekView();
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.mvSchedule = new Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.gridInventory = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.tcSchedule = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPrevious = new Infragistics.Win.Misc.UltraLabel();
            this.lblCurrent = new Infragistics.Win.Misc.UltraLabel();
            this.MainForm_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.lblStart = new Infragistics.Win.Misc.UltraLabel();
            this.btnSchedule = new Infragistics.Win.Misc.UltraButton();
            this.btnInventory = new Infragistics.Win.Misc.UltraButton();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.lblLogo = new Infragistics.Win.Misc.UltraLabel();
            this._MainForm_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._MainForm_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._MainForm_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._MainForm_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraRadialMenu1 = new Infragistics.Win.UltraWinRadialMenu.UltraRadialMenu(this.components);
            this.ultraTouchProvider1 = new Infragistics.Win.Touch.UltraTouchProvider(this.components);
            this.modalPanelManager1 = new Showcase.CustomControl.ModalPanelManager(this.components);
            this.ultraTabPageControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvSchedule)).BeginInit();
            this.ultraTabPageControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wvSchedule)).BeginInit();
            this.ultraTabPageControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvSchedule)).BeginInit();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInventory)).BeginInit();
            this.ultraPanel1.SuspendLayout();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcSchedule)).BeginInit();
            this.tcSchedule.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.MainForm_Fill_Panel.ClientArea.SuspendLayout();
            this.MainForm_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraRadialMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTouchProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraTabPageControl5
            // 
            resources.ApplyResources(this.ultraTabPageControl5, "ultraTabPageControl5");
            this.ultraTabPageControl5.Controls.Add(this.dvSchedule);
            this.ultraTabPageControl5.Name = "ultraTabPageControl5";
            // 
            // dvSchedule
            // 
            resources.ApplyResources(this.dvSchedule, "dvSchedule");
            this.dvSchedule.CalendarInfo = this.ultraCalendarInfo1;
            this.dvSchedule.CalendarLook = this.ultraCalendarLook1;
            this.dvSchedule.Name = "dvSchedule";
            this.dvSchedule.TimeSlotDescriptorLabelStyle = Infragistics.Win.UltraWinSchedule.TimeSlotDescriptorLabelStyle.Alternating;
            this.dvSchedule.TimeSlotInterval = Infragistics.Win.UltraWinSchedule.TimeSlotInterval.SixtyMinutes;
            this.dvSchedule.BeforeAppointmentEdited += new Infragistics.Win.UltraWinSchedule.BeforeAppointmentEditedEventHandler(this.dvSchedule_BeforeAppointmentEdited);
            this.dvSchedule.AppointmentsDragging += new Infragistics.Win.UltraWinSchedule.AppointmentsDraggingHandler(this.schedule_AppointmentsDragging);
            this.dvSchedule.AppointmentResizing += new Infragistics.Win.UltraWinSchedule.AppointmentResizingHandler(this.schedule_AppointmentResizing);
            // 
            // ultraCalendarInfo1
            // 
            this.ultraCalendarInfo1.AllowAllDayEvents = false;
            this.ultraCalendarInfo1.AlternateSelectTypeDay = Infragistics.Win.UltraWinSchedule.SelectType.None;
            this.ultraCalendarInfo1.DataBindingsForAppointments.BindingContextControl = this;
            this.ultraCalendarInfo1.DataBindingsForOwners.BindingContextControl = this;
            this.ultraCalendarInfo1.LogicalDayDuration = System.TimeSpan.Parse("12:00:00");
            this.ultraCalendarInfo1.LogicalDayOffset = System.TimeSpan.Parse("08:00:00");
            this.ultraCalendarInfo1.MaxAlternateSelectedDays = 1;
            this.ultraCalendarInfo1.MaxSelectedDays = 1;
            this.ultraCalendarInfo1.SelectTypeActivity = Infragistics.Win.UltraWinSchedule.SelectType.Single;
            this.ultraCalendarInfo1.SelectTypeDay = Infragistics.Win.UltraWinSchedule.SelectType.Single;
            this.ultraCalendarInfo1.BeforeDisplayAppointmentDialog += new Infragistics.Win.UltraWinSchedule.DisplayAppointmentDialogEventHandler(this.ultraCalendarInfo1_BeforeDisplayAppointmentDialog);
            this.ultraCalendarInfo1.AfterSelectedAppointmentsChange += new System.EventHandler(this.ultraCalendarInfo1_AfterSelectedAppointmentsChange);
            // 
            // ultraTabPageControl4
            // 
            resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
            this.ultraTabPageControl4.Controls.Add(this.wvSchedule);
            this.ultraTabPageControl4.Name = "ultraTabPageControl4";
            // 
            // wvSchedule
            // 
            resources.ApplyResources(this.wvSchedule, "wvSchedule");
            this.wvSchedule.CalendarInfo = this.ultraCalendarInfo1;
            this.wvSchedule.CalendarLook = this.ultraCalendarLook1;
            this.wvSchedule.Name = "wvSchedule";
            this.wvSchedule.BeforeAppointmentEdit += new Infragistics.Win.UltraWinSchedule.BeforeAppointmentEditEventHandler(this.schedule_BeforeAppointmentEdit);
            this.wvSchedule.MoreActivityIndicatorClicked += new Infragistics.Win.UltraWinSchedule.MoreActivityIndicatorClickedEventHandler(this.schedule_MoreActivityIndicatorClicked);
            this.wvSchedule.AppointmentsDragging += new Infragistics.Win.UltraWinSchedule.AppointmentsDraggingHandler(this.schedule_AppointmentsDragging);
            this.wvSchedule.AppointmentResizing += new Infragistics.Win.UltraWinSchedule.AppointmentResizingHandler(this.schedule_AppointmentResizing);
            // 
            // ultraTabPageControl3
            // 
            resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
            this.ultraTabPageControl3.Controls.Add(this.mvSchedule);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            // 
            // mvSchedule
            // 
            resources.ApplyResources(this.mvSchedule, "mvSchedule");
            this.mvSchedule.CalendarInfo = this.ultraCalendarInfo1;
            this.mvSchedule.CalendarLook = this.ultraCalendarLook1;
            this.mvSchedule.Name = "mvSchedule";
            this.mvSchedule.WeekendDisplayStyle = Infragistics.Win.UltraWinSchedule.WeekendDisplayStyleEnum.Full;
            this.mvSchedule.BeforeAppointmentEdit += new Infragistics.Win.UltraWinSchedule.BeforeAppointmentEditEventHandler(this.schedule_BeforeAppointmentEdit);
            this.mvSchedule.MoreActivityIndicatorClicked += new Infragistics.Win.UltraWinSchedule.MoreActivityIndicatorClickedEventHandler(this.schedule_MoreActivityIndicatorClicked);
            this.mvSchedule.AppointmentsDragging += new Infragistics.Win.UltraWinSchedule.AppointmentsDraggingHandler(this.schedule_AppointmentsDragging);
            this.mvSchedule.AppointmentResizing += new Infragistics.Win.UltraWinSchedule.AppointmentResizingHandler(this.schedule_AppointmentResizing);
            // 
            // ultraTabPageControl1
            // 
            resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
            this.ultraTabPageControl1.Controls.Add(this.gridInventory);
            this.ultraTabPageControl1.Controls.Add(this.ultraPanel1);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            // 
            // gridInventory
            // 
            resources.ApplyResources(this.gridInventory, "gridInventory");
            appearance5.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Form;
            resources.ApplyResources(appearance5, "appearance5");
            this.gridInventory.DisplayLayout.Appearance = appearance5;
            this.gridInventory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.gridInventory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.gridInventory.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.gridInventory.Name = "gridInventory";
            this.gridInventory.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridInventory_InitializeLayout);
            this.gridInventory.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.gridInventory_InitializeRow);
            this.gridInventory.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.gridInventory_ClickCellButton);
            this.gridInventory.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.gridInventory_ClickCell);
            // 
            // ultraPanel1
            // 
            resources.ApplyResources(this.ultraPanel1, "ultraPanel1");
            appearance6.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Form;
            resources.ApplyResources(appearance6, "appearance6");
            this.ultraPanel1.Appearance = appearance6;
            // 
            // ultraPanel1.ClientArea
            // 
            resources.ApplyResources(this.ultraPanel1.ClientArea, "ultraPanel1.ClientArea");
            this.ultraPanel1.Name = "ultraPanel1";
            // 
            // ultraTabPageControl2
            // 
            resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
            this.ultraTabPageControl2.Controls.Add(this.tcSchedule);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            // 
            // tcSchedule
            // 
            resources.ApplyResources(this.tcSchedule, "tcSchedule");
            this.tcSchedule.Controls.Add(this.ultraTabSharedControlsPage2);
            this.tcSchedule.Controls.Add(this.ultraTabPageControl3);
            this.tcSchedule.Controls.Add(this.ultraTabPageControl4);
            this.tcSchedule.Controls.Add(this.ultraTabPageControl5);
            this.tcSchedule.ImageSize = new System.Drawing.Size(32, 32);
            this.tcSchedule.MinTabWidth = 75;
            this.tcSchedule.Name = "tcSchedule";
            this.tcSchedule.SharedControlsPage = this.ultraTabSharedControlsPage2;
            appearance7.AlphaLevel = ((short)(75));
            appearance7.BackColor = System.Drawing.Color.Black;
            appearance7.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            appearance7.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Form;
            appearance7.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            resources.ApplyResources(appearance7, "appearance7");
            this.tcSchedule.TabHeaderAreaAppearance = appearance7;
            this.tcSchedule.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.TopRight;
            appearance8.AlphaLevel = ((short)(255));
            resources.ApplyResources(appearance8, "appearance8");
            ultraTab5.Appearance = appearance8;
            ultraTab5.Key = "Daily";
            ultraTab5.TabPage = this.ultraTabPageControl5;
            resources.ApplyResources(ultraTab5, "ultraTab5");
            ultraTab5.ForceApplyResources = "";
            appearance9.AlphaLevel = ((short)(255));
            resources.ApplyResources(appearance9, "appearance9");
            ultraTab4.Appearance = appearance9;
            ultraTab4.Key = "Weekly";
            ultraTab4.TabPage = this.ultraTabPageControl4;
            resources.ApplyResources(ultraTab4, "ultraTab4");
            ultraTab4.ForceApplyResources = "";
            appearance10.AlphaLevel = ((short)(255));
            resources.ApplyResources(appearance10, "appearance10");
            ultraTab3.Appearance = appearance10;
            ultraTab3.Key = "Monthly";
            ultraTab3.TabPage = this.ultraTabPageControl3;
            resources.ApplyResources(ultraTab3, "ultraTab3");
            ultraTab3.ForceApplyResources = "";
            this.tcSchedule.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab5,
            ultraTab4,
            ultraTab3});
            this.tcSchedule.TabSize = new System.Drawing.Size(0, 40);
            // 
            // ultraTabSharedControlsPage2
            // 
            resources.ApplyResources(this.ultraTabSharedControlsPage2, "ultraTabSharedControlsPage2");
            this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.lblPrevious);
            this.flowLayoutPanel1.Controls.Add(this.lblCurrent);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lblPrevious
            // 
            resources.ApplyResources(this.lblPrevious, "lblPrevious");
            appearance3.FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
            appearance3.FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
            appearance3.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints1")));
            appearance3.FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
            appearance3.FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
            appearance3.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(appearance3, "appearance3");
            this.lblPrevious.Appearance = appearance3;
            this.lblPrevious.Name = "lblPrevious";
            this.lblPrevious.TabStop = true;
            // 
            // lblCurrent
            // 
            resources.ApplyResources(this.lblCurrent, "lblCurrent");
            appearance4.FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
            appearance4.FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
            appearance4.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints2")));
            appearance4.FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
            appearance4.FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
            appearance4.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(appearance4, "appearance4");
            this.lblCurrent.Appearance = appearance4;
            this.lblCurrent.Name = "lblCurrent";
            // 
            // MainForm_Fill_Panel
            // 
            resources.ApplyResources(this.MainForm_Fill_Panel, "MainForm_Fill_Panel");
            appearance1.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Form;
            resources.ApplyResources(appearance1, "appearance1");
            this.MainForm_Fill_Panel.Appearance = appearance1;
            // 
            // MainForm_Fill_Panel.ClientArea
            // 
            resources.ApplyResources(this.MainForm_Fill_Panel.ClientArea, "MainForm_Fill_Panel.ClientArea");
            this.MainForm_Fill_Panel.ClientArea.Controls.Add(this.lblStart);
            this.MainForm_Fill_Panel.ClientArea.Controls.Add(this.flowLayoutPanel1);
            this.MainForm_Fill_Panel.ClientArea.Controls.Add(this.btnSchedule);
            this.MainForm_Fill_Panel.ClientArea.Controls.Add(this.btnInventory);
            this.MainForm_Fill_Panel.ClientArea.Controls.Add(this.ultraTabControl1);
            this.MainForm_Fill_Panel.ClientArea.Controls.Add(this.lblLogo);
            this.MainForm_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainForm_Fill_Panel.Name = "MainForm_Fill_Panel";
            // 
            // lblStart
            // 
            resources.ApplyResources(this.lblStart, "lblStart");
            appearance2.FontData.BoldAsString = resources.GetString("resource.BoldAsString");
            appearance2.FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
            appearance2.FontData.SizeInPoints = ((float)(resources.GetObject("resource.SizeInPoints")));
            appearance2.FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
            appearance2.FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
            appearance2.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(appearance2, "appearance2");
            this.lblStart.Appearance = appearance2;
            this.lblStart.Name = "lblStart";
            // 
            // btnSchedule
            // 
            resources.ApplyResources(this.btnSchedule, "btnSchedule");
            this.btnSchedule.ImageSize = new System.Drawing.Size(39, 34);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.btnSchedule.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // btnInventory
            // 
            resources.ApplyResources(this.btnInventory, "btnInventory");
            this.btnInventory.ImageSize = new System.Drawing.Size(39, 34);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.btnInventory.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // ultraTabControl1
            // 
            resources.ApplyResources(this.ultraTabControl1, "ultraTabControl1");
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
            ultraTab1.Key = "SearchGrid";
            ultraTab1.TabPage = this.ultraTabPageControl1;
            resources.ApplyResources(ultraTab1, "ultraTab1");
            ultraTab1.ForceApplyResources = "";
            ultraTab2.Key = "DeliverySchedule";
            ultraTab2.TabPage = this.ultraTabPageControl2;
            resources.ApplyResources(ultraTab2, "ultraTab2");
            ultraTab2.ForceApplyResources = "";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2});
            // 
            // ultraTabSharedControlsPage1
            // 
            resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            // 
            // lblLogo
            // 
            resources.ApplyResources(this.lblLogo, "lblLogo");
            appearance11.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(appearance11, "appearance11");
            this.lblLogo.Appearance = appearance11;
            this.lblLogo.Name = "lblLogo";
            // 
            // _MainForm_Toolbars_Dock_Area_Left
            // 
            resources.ApplyResources(this._MainForm_Toolbars_Dock_Area_Left, "_MainForm_Toolbars_Dock_Area_Left");
            this._MainForm_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._MainForm_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._MainForm_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._MainForm_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._MainForm_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 1;
            this._MainForm_Toolbars_Dock_Area_Left.Name = "_MainForm_Toolbars_Dock_Area_Left";
            this._MainForm_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this;
            this.ultraToolbarsManager1.DockWithinContainerBaseType = typeof(System.Windows.Forms.Form);
            this.ultraToolbarsManager1.FormDisplayStyle = Infragistics.Win.UltraWinToolbars.FormDisplayStyle.RoundedSizable;
            this.ultraToolbarsManager1.Office2007UICompatibility = false;
            this.ultraToolbarsManager1.Ribbon.AllowAutoHide = Infragistics.Win.DefaultableBoolean.True;
            appearance12.ImageBackgroundOrigin = Infragistics.Win.ImageBackgroundOrigin.Form;
            resources.ApplyResources(appearance12, "appearance12");
            this.ultraToolbarsManager1.Ribbon.CaptionAreaAppearance = appearance12;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013;
            // 
            // _MainForm_Toolbars_Dock_Area_Right
            // 
            resources.ApplyResources(this._MainForm_Toolbars_Dock_Area_Right, "_MainForm_Toolbars_Dock_Area_Right");
            this._MainForm_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._MainForm_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._MainForm_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._MainForm_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._MainForm_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 1;
            this._MainForm_Toolbars_Dock_Area_Right.Name = "_MainForm_Toolbars_Dock_Area_Right";
            this._MainForm_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _MainForm_Toolbars_Dock_Area_Top
            // 
            resources.ApplyResources(this._MainForm_Toolbars_Dock_Area_Top, "_MainForm_Toolbars_Dock_Area_Top");
            this._MainForm_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._MainForm_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._MainForm_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._MainForm_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._MainForm_Toolbars_Dock_Area_Top.Name = "_MainForm_Toolbars_Dock_Area_Top";
            this._MainForm_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _MainForm_Toolbars_Dock_Area_Bottom
            // 
            resources.ApplyResources(this._MainForm_Toolbars_Dock_Area_Bottom, "_MainForm_Toolbars_Dock_Area_Bottom");
            this._MainForm_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._MainForm_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._MainForm_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._MainForm_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._MainForm_Toolbars_Dock_Area_Bottom.InitialResizeAreaExtent = 1;
            this._MainForm_Toolbars_Dock_Area_Bottom.Name = "_MainForm_Toolbars_Dock_Area_Bottom";
            this._MainForm_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // ultraRadialMenu1
            // 
            this.ultraRadialMenu1.MenuSettings.CenterButtonImageSize = new System.Drawing.Size(48, 48);
            this.ultraRadialMenu1.MenuSettings.Size = new System.Drawing.Size(266, 266);
            this.ultraRadialMenu1.OwningControl = this;
            this.ultraRadialMenu1.CenterButtonClick += new System.EventHandler<Infragistics.Win.UltraWinRadialMenu.CenterButtonClickEventArgs>(this.ultraRadialMenu1_CenterButtonClick);
            this.ultraRadialMenu1.DrillDownButtonClick += new System.EventHandler<Infragistics.Win.UltraWinRadialMenu.RadialMenuToolDrillDownButtonClickEventArgs>(this.ultraRadialMenu1_DrillDownButtonClick);
            this.ultraRadialMenu1.ToolClick += new System.EventHandler<Infragistics.Win.UltraWinRadialMenu.RadialMenuToolClickEventArgs>(this.ultraRadialMenu1_ToolClick);
            this.ultraRadialMenu1.PropertyChanged += new Infragistics.Win.PropertyChangedEventHandler(this.ultraRadialMenu1_PropertyChanged);
            // 
            // ultraTouchProvider1
            // 
            this.ultraTouchProvider1.ContainingControl = this;
            // 
            // modalPanelManager1
            // 
            this.modalPanelManager1.BeforeShown += new Showcase.CustomControl.ModalPanelManager.BeforeModalShownEventHandler(this.modalPanelManager1_BeforeShown);
            this.modalPanelManager1.AfterShown += new Showcase.CustomControl.ModalPanelManager.AfterModalShownEventHandler(this.modalPanelManager1_AfterShown);
            this.modalPanelManager1.AfterClosed += new Showcase.CustomControl.ModalPanelManager.AfterModalClosedEventHandler(this.modalPanelManager1_AfterClosed);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainForm_Fill_Panel);
            this.Controls.Add(this._MainForm_Toolbars_Dock_Area_Left);
            this.Controls.Add(this._MainForm_Toolbars_Dock_Area_Right);
            this.Controls.Add(this._MainForm_Toolbars_Dock_Area_Bottom);
            this.Controls.Add(this._MainForm_Toolbars_Dock_Area_Top);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ultraTabPageControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvSchedule)).EndInit();
            this.ultraTabPageControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wvSchedule)).EndInit();
            this.ultraTabPageControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mvSchedule)).EndInit();
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInventory)).EndInit();
            this.ultraPanel1.ResumeLayout(false);
            this.ultraTabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcSchedule)).EndInit();
            this.tcSchedule.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.MainForm_Fill_Panel.ClientArea.ResumeLayout(false);
            this.MainForm_Fill_Panel.ClientArea.PerformLayout();
            this.MainForm_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraRadialMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTouchProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private Infragistics.Win.Misc.UltraPanel MainForm_Fill_Panel;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _MainForm_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _MainForm_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _MainForm_Toolbars_Dock_Area_Bottom;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _MainForm_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridInventory;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Infragistics.Win.Misc.UltraLabel lblPrevious;
        private Infragistics.Win.Misc.UltraLabel lblCurrent;
        private Infragistics.Win.Misc.UltraButton btnSchedule;
        private Infragistics.Win.Misc.UltraButton btnInventory;
        private Infragistics.Win.Misc.UltraLabel lblLogo;
        private Infragistics.Win.UltraWinRadialMenu.UltraRadialMenu ultraRadialMenu1;
        private Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle mvSchedule;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarInfo ultraCalendarInfo1;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarLook ultraCalendarLook1;
        private Infragistics.Win.Touch.UltraTouchProvider ultraTouchProvider1;
        private Showcase.CustomControl.ModalPanelManager modalPanelManager1;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tcSchedule;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage2;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl4;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl5;
        private Infragistics.Win.UltraWinSchedule.UltraWeekView wvSchedule;
        private Infragistics.Win.UltraWinSchedule.UltraDayView dvSchedule;
        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraLabel lblStart;
    }
}

