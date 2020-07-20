namespace IGExcel.Dialogs
{
    partial class ZoomDialog
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
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoomDialog));
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.pnlBackground = new Infragistics.Win.Misc.UltraPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.numZoomLevel = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.lblPercent = new Infragistics.Win.Misc.UltraLabel();
            this.btnOK = new Infragistics.Win.Misc.UltraButton();
            this.btnCancel = new Infragistics.Win.Misc.UltraButton();
            this.lblMagnification = new Infragistics.Win.Misc.UltraLabel();
            this.optPresetLevels = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this._ZoomDialog_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._ZoomDialog_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._ZoomDialog_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.pnlBackground.ClientArea.SuspendLayout();
            this.pnlBackground.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZoomLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optPresetLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            this.ultraFormManager1.FormStyleSettings.Style = Infragistics.Win.UltraWinForm.UltraFormStyle.Office2013;
            // 
            // pnlBackground
            // 
            appearance7.BackColor = System.Drawing.Color.White;
            this.pnlBackground.Appearance = appearance7;
            // 
            // pnlBackground.ClientArea
            // 
            this.pnlBackground.ClientArea.Controls.Add(this.tableLayoutPanel1);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(1, 31);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(263, 253);
            this.pnlBackground.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblMagnification, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.optPresetLevels, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(263, 253);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.numZoomLevel);
            this.flowLayoutPanel1.Controls.Add(this.lblPercent);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(139, 151);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(100, 30);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // numZoomLevel
            // 
            this.numZoomLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numZoomLevel.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2013;
            this.numZoomLevel.Location = new System.Drawing.Point(3, 3);
            this.numZoomLevel.MaskInput = "nnn";
            this.numZoomLevel.MaxValue = 400;
            this.numZoomLevel.MinValue = 10;
            this.numZoomLevel.Name = "numZoomLevel";
            this.numZoomLevel.PromptChar = ' ';
            this.numZoomLevel.Size = new System.Drawing.Size(54, 24);
            this.numZoomLevel.SpinButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.Always;
            this.numZoomLevel.TabIndex = 2;
            this.numZoomLevel.ValueChanged += new System.EventHandler(this.numZoomLevel_ValueChanged);
            this.numZoomLevel.ValidationError += new Infragistics.Win.UltraWinEditors.UltraNumericEditorBase.ValidationErrorEventHandler(this.numZoomLevel_ValidationError);
            // 
            // lblPercent
            // 
            this.lblPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(63, 10);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(13, 17);
            this.lblPercent.TabIndex = 0;
            this.lblPercent.Text = "%";
            // 
            // btnOK
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(116)))), ((int)(((byte)(71)))));
            appearance1.BackColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            appearance1.ForeColor = System.Drawing.Color.White;
            appearance1.ForeColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnOK.Appearance = appearance1;
            this.btnOK.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(149)))), ((int)(((byte)(111)))));
            this.btnOK.HotTrackAppearance = appearance2;
            this.btnOK.Location = new System.Drawing.Point(23, 207);
            this.btnOK.Name = "btnOK";
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(149)))), ((int)(((byte)(111)))));
            this.btnOK.PressedAppearance = appearance3;
            this.btnOK.Size = new System.Drawing.Size(100, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // btnCancel
            // 
            appearance4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(229)))), ((int)(((byte)(213)))));
            appearance4.BackColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            appearance4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(116)))), ((int)(((byte)(71)))));
            appearance4.ForeColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnCancel.Appearance = appearance4;
            this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(217)))), ((int)(((byte)(195)))));
            this.btnCancel.HotTrackAppearance = appearance5;
            this.btnCancel.Location = new System.Drawing.Point(139, 207);
            this.btnCancel.Name = "btnCancel";
            appearance6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(205)))), ((int)(((byte)(178)))));
            this.btnCancel.PressedAppearance = appearance6;
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // lblMagnification
            // 
            appearance8.FontData.SizeInPoints = 12F;
            appearance8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(149)))), ((int)(((byte)(111)))));
            this.lblMagnification.Appearance = appearance8;
            this.lblMagnification.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblMagnification, 3);
            this.lblMagnification.Location = new System.Drawing.Point(23, 23);
            this.lblMagnification.Name = "lblMagnification";
            this.lblMagnification.Size = new System.Drawing.Size(106, 24);
            this.lblMagnification.TabIndex = 3;
            this.lblMagnification.Text = "Magnification";
            // 
            // optPresetLevels
            // 
            this.optPresetLevels.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.optPresetLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            appearance9.FontData.SizeInPoints = 9F;
            this.optPresetLevels.ItemAppearance = appearance9;
            valueListItem2.DataValue = 200;
            valueListItem2.DisplayText = "200%";
            valueListItem3.DataValue = 100;
            valueListItem3.DisplayText = "100%";
            valueListItem4.DataValue = 75;
            valueListItem4.DisplayText = "75%";
            valueListItem5.DataValue = 50;
            valueListItem5.DisplayText = "50%";
            valueListItem6.DataValue = 25;
            valueListItem6.DisplayText = "25%";
            valueListItem7.DataValue = ((object)(resources.GetObject("valueListItem7.DataValue")));
            valueListItem7.DisplayText = "Custom";
            this.optPresetLevels.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem2,
            valueListItem3,
            valueListItem4,
            valueListItem5,
            valueListItem6,
            valueListItem7});
            this.optPresetLevels.ItemSpacingVertical = 3;
            this.optPresetLevels.Location = new System.Drawing.Point(23, 53);
            this.optPresetLevels.Name = "optPresetLevels";
            this.optPresetLevels.Size = new System.Drawing.Size(100, 128);
            this.optPresetLevels.TabIndex = 0;
            this.optPresetLevels.ValueChanged += new System.EventHandler(this.optPresetLevels_ValueChanged);
            // 
            // _ZoomDialog_UltraFormManager_Dock_Area_Left
            // 
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 1;
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 31);
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.Name = "_ZoomDialog_UltraFormManager_Dock_Area_Left";
            this._ZoomDialog_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(1, 253);
            // 
            // _ZoomDialog_UltraFormManager_Dock_Area_Right
            // 
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 1;
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(264, 31);
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.Name = "_ZoomDialog_UltraFormManager_Dock_Area_Right";
            this._ZoomDialog_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(1, 253);
            // 
            // _ZoomDialog_UltraFormManager_Dock_Area_Top
            // 
            this._ZoomDialog_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._ZoomDialog_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ZoomDialog_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._ZoomDialog_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ZoomDialog_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._ZoomDialog_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._ZoomDialog_UltraFormManager_Dock_Area_Top.Name = "_ZoomDialog_UltraFormManager_Dock_Area_Top";
            this._ZoomDialog_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(265, 31);
            // 
            // _ZoomDialog_UltraFormManager_Dock_Area_Bottom
            // 
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 1;
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 284);
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.Name = "_ZoomDialog_UltraFormManager_Dock_Area_Bottom";
            this._ZoomDialog_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(265, 1);
            // 
            // ZoomDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(265, 285);
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this._ZoomDialog_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._ZoomDialog_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._ZoomDialog_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._ZoomDialog_UltraFormManager_Dock_Area_Bottom);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoomDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zoom";
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.pnlBackground.ClientArea.ResumeLayout(false);
            this.pnlBackground.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZoomLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optPresetLevels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel pnlBackground;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _ZoomDialog_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _ZoomDialog_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _ZoomDialog_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _ZoomDialog_UltraFormManager_Dock_Area_Bottom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor numZoomLevel;
        private Infragistics.Win.Misc.UltraLabel lblPercent;
        private Infragistics.Win.Misc.UltraLabel lblMagnification;
        private Infragistics.Win.UltraWinEditors.UltraOptionSet optPresetLevels;
        private Infragistics.Win.Misc.UltraButton btnOK;
        private Infragistics.Win.Misc.UltraButton btnCancel;
    }
}