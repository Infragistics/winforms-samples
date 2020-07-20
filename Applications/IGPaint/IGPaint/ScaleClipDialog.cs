using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Infragistics.Win;

namespace IGPaint
{
    #region ScaleClipDialog

	/// <summary>
	/// Summary description for ScaleClipDialog.
	/// </summary>
	public class ScaleClipDialog : System.Windows.Forms.Form
	{
        #region Members Variables

        private Infragistics.Win.Misc.UltraButton btnDialogOK;
        private Infragistics.Win.Misc.UltraButton btnDialogCancel;
        private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSet1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Infragistics.Win.UltraWinEditors.UltraPictureBox ultraPictureBox1;
        private Image originalImage;
        private Size imageEditorSize;

        private System.ComponentModel.IContainer components;

        #endregion // Members Variables

        #region Constructor

		public ScaleClipDialog(Image originalImage, Size editorSize)
		{
            this.originalImage = originalImage;
            this.imageEditorSize = editorSize;

			InitializeComponent();
		}

        #endregion // Constructor

        #region Dispose

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        #endregion // Dispose

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScaleClipDialog));
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.btnDialogOK = new Infragistics.Win.Misc.UltraButton();
            this.btnDialogCancel = new Infragistics.Win.Misc.UltraButton();
            this.ultraOptionSet1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDialogOK
            // 
            resources.ApplyResources(this.btnDialogOK, "btnDialogOK");
            this.btnDialogOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDialogOK.Name = "btnDialogOK";
            // 
            // btnDialogCancel
            // 
            resources.ApplyResources(this.btnDialogCancel, "btnDialogCancel");
            this.btnDialogCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDialogCancel.Name = "btnDialogCancel";
            // 
            // ultraOptionSet1
            // 
            resources.ApplyResources(this.ultraOptionSet1, "ultraOptionSet1");
            this.ultraOptionSet1.BackColor = System.Drawing.Color.Transparent;
            this.ultraOptionSet1.BackColorInternal = System.Drawing.Color.Transparent;
            this.ultraOptionSet1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraOptionSet1.CheckedIndex = 0;
            valueListItem1.DataValue = "Scale";
            resources.ApplyResources(valueListItem1, "valueListItem1");
            valueListItem1.ForceApplyResources = "";
            valueListItem2.DataValue = "Clip";
            resources.ApplyResources(valueListItem2, "valueListItem2");
            valueListItem2.ForceApplyResources = "";
            this.ultraOptionSet1.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.ultraOptionSet1.ItemSpacingVertical = 10;
            this.ultraOptionSet1.Name = "ultraOptionSet1";
            this.ultraOptionSet1.ValueChanged += new System.EventHandler(this.ultraOptionSet1_ValueChanged);
            // 
            // ultraLabel1
            // 
            resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
            appearance3.FontData.BoldAsString = resources.GetString("resource.BoldAsString");
            appearance3.FontData.Name = resources.GetString("resource.Name");
            this.ultraLabel1.Appearance = appearance3;
            this.ultraLabel1.Name = "ultraLabel1";
            // 
            // ultraPictureBox1
            // 
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            this.ultraPictureBox1.Appearance = appearance4;
            this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
            this.ultraPictureBox1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            resources.ApplyResources(this.ultraPictureBox1, "ultraPictureBox1");
            this.ultraPictureBox1.Name = "ultraPictureBox1";
            // 
            // ScaleClipDialog
            // 
            this.AcceptButton = this.btnDialogOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnDialogCancel;
            this.Controls.Add(this.ultraPictureBox1);
            this.Controls.Add(this.ultraLabel1);
            this.Controls.Add(this.ultraOptionSet1);
            this.Controls.Add(this.btnDialogCancel);
            this.Controls.Add(this.btnDialogOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScaleClipDialog";
            this.Load += new System.EventHandler(this.ScaleClipDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet1)).EndInit();
            this.ResumeLayout(false);

        }

		#endregion // Windows Form Designer generated code

        #region ScaleClipDialog_Load

        private void ScaleClipDialog_Load(object sender, System.EventArgs e)
        {
            string toolTip = Properties.Resources.ScaleClipTooltip;
            this.toolTip1.SetToolTip( this.ultraOptionSet1, toolTip );
        }

        #endregion // ScaleClipDialog_Load

        #region ultraOptionSet1_ValueChanged

        /// <summary>
        /// Handles the ValueChanged event of the ultraOptionSet1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ultraOptionSet1_ValueChanged(object sender, EventArgs e)
        {
            Image newImage = null;
            Size editorSize = this.imageEditorSize;
            if (this.ultraOptionSet1.CheckedIndex == 0) // scale image
            {
                newImage = DrawUtility.CreateScaledImage(this.originalImage, editorSize, new Rectangle(Point.Empty, this.originalImage.Size));
            }
            else // clip image
            {
                Image clippedImage = new Bitmap(editorSize.Width, editorSize.Height);
                using (Graphics grfx = Graphics.FromImage(clippedImage))
                {
                    grfx.DrawImage(this.originalImage, new Rectangle(0, 0, editorSize.Width, editorSize.Height),
                        new Rectangle(0, 0, editorSize.Width, editorSize.Height), GraphicsUnit.Pixel);
                    newImage = clippedImage;
                }
            }
            this.ultraPictureBox1.Image = newImage;
        }

        #endregion //ultraOptionSet1_ValueChanged

        #region NewImage

        internal Image NewImage
        {
            get { return this.ultraPictureBox1.Image as Image; }
        }

        #endregion // NewImage

    };

    #endregion // ScaleClipDialog
}
