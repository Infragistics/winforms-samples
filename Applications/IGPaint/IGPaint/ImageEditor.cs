using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Win32;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraImageEditor;
using Infragistics.Win.UltraWinToolbars;

namespace IGPaint
{
	#region frmImageEditor class

	internal class frmImageEditor : System.Windows.Forms.Form
	{
		#region Constants

		private static readonly int SELECTED_COLOR_INDICATOR_HEIGHT  = 40;
		private static readonly int SELECTED_COLOR_INDICATOR_PADDING = 16;
		
		private const int BRUSH_SIZE_SMALL  = 1;
		private const int BRUSH_SIZE_MEDIUM = 3;
		private const int BRUSH_SIZE_LARGE  = 5;

        private const int MAX_IMAGE_WIDTH   = 100;
        private const int MAX_IMAGE_HEIGHT  = 100;

        private const int MAX_MRU_IMAGEFILES = 10;

        private const string DEFAULT_IMAGE_NAME     = "image1.bmp";

        internal static readonly string	REG_KEY	= "Software\\Infragistics\\Win\\AppearanceImageEditor\\ImageEditor";
        internal static readonly string RECENT_IMAGE_FILENAMES = "FilePath.xml";

		#endregion // Constants

		#region Member variables

        private System.ComponentModel.IContainer components;

		private System.Windows.Forms.ImageList ilSmall;
		private Infragistics.Win.Misc.UltraPanel pnlMain;
		private Infragistics.Win.IGPanel pnlColor;
		private Infragistics.Win.UltraImageEditor.ColorSelector colorSelector1;
        private Infragistics.Win.UltraImageEditor.SelectedColorIndicator selectedColorIndicator1;
		private Infragistics.Win.UltraImageEditor.UltraImageEditor ultraImageEditor1;

		private Infragistics.Win.IGPanel pnlImageAttributes;
		private Infragistics.Win.IGPanel pnlCaptureImage;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
		private System.Windows.Forms.Label label2;

        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmImageEditor_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmImageEditor_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmImageEditor_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _frmImageEditor_Toolbars_Dock_Area_Bottom;
        private Infragistics.Win.UltraWinStatusBar.UltraStatusBar ultraStatusBar1;
        private System.Windows.Forms.LinkLabel linkLabelSize;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelDPI;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.Label labelPixelFormat;
        private System.Windows.Forms.Label labelFormat;
        private Infragistics.Win.UltraWinEditors.UltraPictureBox ultraPictureBox1;

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBoxPreview;

        private System.Windows.Forms.ToolTip toolTip1;

        private ArrayList mruImageFiles;
        private string fileName = String.Empty;
        private bool isSynchronizingUI = false;
        private bool inSynchronizeColors = false;
        private bool canceled = false;
        private Infragistics.Win.UltraWinEditors.UltraTrackBar trackBarZoom;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblPixels;
        private TableLayoutPanel tableLayoutPanel1;
        private ImageList ilLarge;

		#endregion // Member variables

		#region Constructor
		
		// Creates a new instance of the "frmImageEditor" class.		
		public frmImageEditor()
		{
			InitializeComponent();

			this.ultraImageEditor1.Dock = DockStyle.Fill;

            // Initialize the open and save file dialogs - title, filter, etc.
            this.saveFileDlg.Filter				= Properties.Resources.BitmapFilter;
            this.saveFileDlg.Title				= Properties.Resources.SaveAs;
            this.saveFileDlg.OverwritePrompt	= true;
            this.saveFileDlg.AddExtension		= true;
            this.saveFileDlg.DefaultExt			= ".bmp";

            this.openFileDlg.Filter				= Properties.Resources.AllImagesFilter;
            this.openFileDlg.Title				= Properties.Resources.Open;
            this.openFileDlg.AddExtension		= false;
            this.openFileDlg.CheckFileExists	= true;
            this.openFileDlg.RestoreDirectory	= false;

            this.mruImageFiles = new ArrayList();

			this.pnlMain.Appearance.BackColor = Office2013ColorTable.Colors.RibbonTabAreaBackColorGradientLight;
			Office2013ColorTable.ColorSchemeChanged += new EventHandler(this.OnOffice2013ColorSchemeChanged);
		}

		#endregion // Constructor

        #region Dispose

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                this.colorSelector1.ColorSelected -= new ColorSelectedEventHandler(this.colorSelector1_ColorSelected);
                this.ultraImageEditor1.MouseVirtualPositionChanged -= new MouseVirtualPositionChangedEventHandler( this.ultraImageEditor1_MouseVirtualPositionChanged );

				Office2013ColorTable.ColorSchemeChanged -= new EventHandler(this.OnOffice2013ColorSchemeChanged);

                if (components != null) 
                {
                    components.Dispose();
                }
            }

            base.Dispose( disposing );
        }

            #endregion // Dispose

		#region Windows Form Designer generated code

            #region InitializeComponent

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImageEditor));
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("Palette");
            Infragistics.Win.UltraWinToolbars.OptionSet optionSet2 = new Infragistics.Win.UltraWinToolbars.OptionSet("GradientStyle");
            Infragistics.Win.UltraWinToolbars.OptionSet optionSet3 = new Infragistics.Win.UltraWinToolbars.OptionSet("BrushSize");
            Infragistics.Win.UltraWinToolbars.OptionSet optionSet4 = new Infragistics.Win.UltraWinToolbars.OptionSet("DrawingTools");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool74 = new Infragistics.Win.UltraWinToolbars.ButtonTool("New");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool75 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Open");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool76 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Save");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool77 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SaveAs");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool17 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Recent ");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool79 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Properties");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool78 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Close");
            Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool1 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("About");
            Infragistics.Win.UltraWinToolbars.ContextualTabGroup contextualTabGroup1 = new Infragistics.Win.UltraWinToolbars.ContextualTabGroup("Text Tools");
            Infragistics.Win.UltraWinToolbars.RibbonTab ribbonTab1 = new Infragistics.Win.UltraWinToolbars.RibbonTab("Home");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup1 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Clipboard");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PasteTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CutTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CopyTool");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup2 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Tools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("SelectRectangle", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool16 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawText", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ColorSelect", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool7 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Fill", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawBrush", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Erase", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("EraseColor", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawPoint", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool17 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawAirbrush", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool8 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawLine", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool9 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawArc", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool10 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawRectangleOutline", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool11 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledRectangle", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool12 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledRectangleWithOutline", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool13 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawEllipseOutline", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool14 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledEllipse", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool15 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledEllipseWithOutline", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup3 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Color");
            Infragistics.Win.UltraWinToolbars.PopupColorPickerTool popupColorPickerTool1 = new Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("ForeColor");
            Infragistics.Win.UltraWinToolbars.PopupColorPickerTool popupColorPickerTool2 = new Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("BackColor");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Palette");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InvertColorsTool");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup4 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Brush");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool18 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("SmallBrush", "BrushSize");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool19 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("MediumBrush", "BrushSize");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool20 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("LargeBrush", "BrushSize");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup5 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Actions");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CaptureImage");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearImage");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImageSize");
            Infragistics.Win.UltraWinToolbars.RibbonTab ribbonTab2 = new Infragistics.Win.UltraWinToolbars.RibbonTab("View");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup6 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Flip/Rotate");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FlipVertical");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FlipHorizontal");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RotateLeftTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RotateRightTool");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup7 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Move");
            Infragistics.Win.UltraWinToolbars.LabelTool labelTool1 = new Infragistics.Win.UltraWinToolbars.LabelTool("Spacer");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveLeft");
            Infragistics.Win.UltraWinToolbars.LabelTool labelTool5 = new Infragistics.Win.UltraWinToolbars.LabelTool("Spacer");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveUp");
            Infragistics.Win.UltraWinToolbars.LabelTool labelTool7 = new Infragistics.Win.UltraWinToolbars.LabelTool("Spacer");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveDown");
            Infragistics.Win.UltraWinToolbars.LabelTool labelTool6 = new Infragistics.Win.UltraWinToolbars.LabelTool("Spacer");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveRight");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup8 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Zoom");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomInTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomOutTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomFitTool");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup9 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Show/Hide");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool21 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DisplayImageGrid", "");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool22 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("StatusBar", "");
            Infragistics.Win.UltraWinToolbars.RibbonTab ribbonTab3 = new Infragistics.Win.UltraWinToolbars.RibbonTab("Text");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup10 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Font");
            Infragistics.Win.UltraWinToolbars.FontListTool fontListTool1 = new Infragistics.Win.UltraWinToolbars.FontListTool("FontName");
            Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool1 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("FontSize");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool23 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Bold", "");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool24 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Italic", "");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool25 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Underlined", "");
            Infragistics.Win.UltraWinToolbars.RibbonGroup ribbonGroup11 = new Infragistics.Win.UltraWinToolbars.RibbonGroup("Text Color");
            Infragistics.Win.UltraWinToolbars.PopupColorPickerTool popupColorPickerTool3 = new Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("ForeColor");
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Save");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UndoTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RedoTool");
            Infragistics.Win.UltraWinToolbars.UltraTaskPaneToolbar ultraTaskPaneToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraTaskPaneToolbar("TaskPaneToolbar");
            Infragistics.Win.UltraWinToolbars.TaskPaneTool taskPaneTool1 = new Infragistics.Win.UltraWinToolbars.TaskPaneTool("Image Attributes");
            Infragistics.Win.UltraWinToolbars.UltraTaskPaneToolbar ultraTaskPaneToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraTaskPaneToolbar("Color");
            Infragistics.Win.UltraWinToolbars.TaskPaneTool taskPaneTool3 = new Infragistics.Win.UltraWinToolbars.TaskPaneTool("Color Chooser");
            Infragistics.Win.UltraWinToolbars.TaskPaneTool taskPaneTool4 = new Infragistics.Win.UltraWinToolbars.TaskPaneTool("Color Chooser");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CutTool");
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CopyTool");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool31 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PasteTool");
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool32 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UndoTool");
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool33 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RedoTool");
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.TaskPaneTool taskPaneTool5 = new Infragistics.Win.UltraWinToolbars.TaskPaneTool("Image Attributes");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool34 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RotateLeftTool");
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool35 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RotateRightTool");
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool36 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomInTool");
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool37 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomOutTool");
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool38 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomFitTool");
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool26 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("SmallBrush", "BrushSize");
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool27 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("MediumBrush", "BrushSize");
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool28 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("LargeBrush", "BrushSize");
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool29 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("SelectRectangle", "DrawingTools");
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool30 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Erase", "DrawingTools");
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool31 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("EraseColor", "DrawingTools");
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool32 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ColorSelect", "DrawingTools");
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool33 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawPoint", "DrawingTools");
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool34 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawAirbrush", "DrawingTools");
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool35 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Fill", "DrawingTools");
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool36 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawLine", "DrawingTools");
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool37 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawArc", "DrawingTools");
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool38 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawBrush", "DrawingTools");
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool39 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawEllipseOutline", "DrawingTools");
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool40 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledEllipse", "DrawingTools");
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool41 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledEllipseWithOutline", "DrawingTools");
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool42 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawRectangleOutline", "DrawingTools");
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool43 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledRectangle", "DrawingTools");
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool44 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledRectangleWithOutline", "DrawingTools");
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool39 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InvertColorsTool");
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("GradientStyle");
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool45 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_None", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool46 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Vertical", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool47 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Horizontal", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool48 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_BackwardDiagonal", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool49 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_ForwardDiagonal", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool50 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_HorizontalBump", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool51 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_VerticalBump", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool52 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Circular", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool53 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Rectangular", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool54 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Elliptical", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool55 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_None", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool56 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Vertical", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool57 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Horizontal", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool58 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_BackwardDiagonal", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool59 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_ForwardDiagonal", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool60 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_HorizontalBump", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool61 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_VerticalBump", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool62 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Circular", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool63 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Rectangular", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool64 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GradientStyle_Elliptical", "GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool65 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DisplayImageGrid", "");
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("File");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool40 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Open");
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool41 = new Infragistics.Win.UltraWinToolbars.ButtonTool("New");
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool42 = new Infragistics.Win.UltraWinToolbars.ButtonTool("SaveAs");
            Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool43 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Close");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Help");
            Infragistics.Win.UltraWinToolbars.TextBoxTool textBoxTool1 = new Infragistics.Win.UltraWinToolbars.TextBoxTool("TextBoxTool1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool45 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImageSize");
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool46 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CaptureImage");
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Edit");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool47 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CutTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool48 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CopyTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool49 = new Infragistics.Win.UltraWinToolbars.ButtonTool("PasteTool");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("View");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool66 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DisplayImageGrid", "");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool50 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomInTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool51 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomOutTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool52 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ZoomFitTool");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool67 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("StatusBar", "");
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool53 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FlipVertical");
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Image");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool54 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FlipVertical");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool55 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FlipHorizontal");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool56 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RotateLeftTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool57 = new Infragistics.Win.UltraWinToolbars.ButtonTool("RotateRightTool");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool9 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MoveImage");
            Infragistics.Win.UltraWinToolbars.PopupColorPickerTool popupColorPickerTool4 = new Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("ForeColor");
            Infragistics.Win.UltraWinToolbars.PopupColorPickerTool popupColorPickerTool5 = new Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("BackColor");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool10 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Palette");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool58 = new Infragistics.Win.UltraWinToolbars.ButtonTool("InvertColorsTool");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool59 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CaptureImage");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool60 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearImage");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool61 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ImageSize");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool62 = new Infragistics.Win.UltraWinToolbars.ButtonTool("FlipHorizontal");
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool11 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("MoveImage");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool63 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveUp");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool64 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveDown");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool65 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveLeft");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool66 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveRight");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool67 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveUp");
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool68 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveDown");
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool69 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveLeft");
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool70 = new Infragistics.Win.UltraWinToolbars.ButtonTool("MoveRight");
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupColorPickerTool popupColorPickerTool6 = new Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("ForeColor");
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupColorPickerTool popupColorPickerTool7 = new Infragistics.Win.UltraWinToolbars.PopupColorPickerTool("BackColor");
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool12 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Palette");
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool68 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Standard", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool69 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("16Colors", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool70 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("WebSafeColors", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool71 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("HSLColors", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool72 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GrayScale", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool73 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Standard", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool74 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("16Colors", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool75 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("WebSafeColors", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool76 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("HSLColors", "Palette");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool77 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("GrayScale", "Palette");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool71 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ClearImage");
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool13 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Tools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool78 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("SelectRectangle", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool79 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("ColorSelect", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool80 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Erase", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool81 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("EraseColor", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool82 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Fill", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool83 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawPoint", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool84 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawBrush", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool14 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Brush Size");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool15 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("GradientStyle");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool85 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawAirbrush", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool86 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawLine", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool87 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawArc", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool88 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawText", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool89 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawRectangleOutline", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool90 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledRectangle", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool91 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledRectangleWithOutline", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool92 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawEllipseOutline", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool93 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledEllipse", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool94 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawFilledEllipseWithOutline", "DrawingTools");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool16 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Brush Size");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool95 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("SmallBrush", "BrushSize");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool96 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("MediumBrush", "BrushSize");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool97 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("LargeBrush", "BrushSize");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool72 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Save");
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.FontListTool fontListTool2 = new Infragistics.Win.UltraWinToolbars.FontListTool("FontName");
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool98 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Bold", "");
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool99 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Italic", "");
            Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool100 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Underlined", "");
            Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ComboBoxTool comboBoxTool2 = new Infragistics.Win.UltraWinToolbars.ComboBoxTool("FontSize");
            Infragistics.Win.ValueList valueList1 = new Infragistics.Win.ValueList(0);
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool101 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DrawText", "DrawingTools");
            Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ListTool listTool2 = new Infragistics.Win.UltraWinToolbars.ListTool("MRUFiles");
            Infragistics.Win.UltraWinToolbars.ListToolItem listToolItem1 = new Infragistics.Win.UltraWinToolbars.ListToolItem();
            Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.LabelTool labelTool2 = new Infragistics.Win.UltraWinToolbars.LabelTool("Recent Documents");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool73 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Color");
            Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool18 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Recent ");
            Infragistics.Win.UltraWinToolbars.LabelTool labelTool3 = new Infragistics.Win.UltraWinToolbars.LabelTool("Recent Documents");
            Infragistics.Win.UltraWinToolbars.ListTool listTool3 = new Infragistics.Win.UltraWinToolbars.ListTool("MRUFiles");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool80 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Properties");
            Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool2 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("About");
            Infragistics.Win.UltraWinToolbars.LabelTool labelTool4 = new Infragistics.Win.UltraWinToolbars.LabelTool("Spacer");
            this.trackBarZoom = new Infragistics.Win.UltraWinEditors.UltraTrackBar();
            this.pnlColor = new Infragistics.Win.IGPanel();
            this.selectedColorIndicator1 = new Infragistics.Win.UltraImageEditor.SelectedColorIndicator();
            this.colorSelector1 = new Infragistics.Win.UltraImageEditor.ColorSelector();
            this.pnlImageAttributes = new Infragistics.Win.IGPanel();
            this.ultraGroupBoxPreview = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabelSize = new System.Windows.Forms.LinkLabel();
            this.lblPixels = new System.Windows.Forms.Label();
            this.labelPixelFormat = new System.Windows.Forms.Label();
            this.labelFormat = new System.Windows.Forms.Label();
            this.labelResolution = new System.Windows.Forms.Label();
            this.labelDPI = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.pnlCaptureImage = new Infragistics.Win.IGPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.ilSmall = new System.Windows.Forms.ImageList(this.components);
            this.pnlMain = new Infragistics.Win.Misc.UltraPanel();
            this.ultraImageEditor1 = new Infragistics.Win.UltraImageEditor.UltraImageEditor();
            this._frmImageEditor_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ilLarge = new System.Windows.Forms.ImageList(this.components);
            this._frmImageEditor_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._frmImageEditor_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._frmImageEditor_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraStatusBar1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
            this.pnlColor.SuspendLayout();
            this.pnlImageAttributes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPreview)).BeginInit();
            this.ultraGroupBoxPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlCaptureImage.SuspendLayout();
            this.pnlMain.ClientArea.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraStatusBar1)).BeginInit();
            this.ultraStatusBar1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarZoom
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            this.trackBarZoom.Appearance = appearance1;
            this.trackBarZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            this.trackBarZoom.BackColorInternal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            //this.trackBarZoom.ButtonSettings.ShowIncrementButtons = Infragistics.Win.DefaultableBoolean.False;
            resources.ApplyResources(this.trackBarZoom, "trackBarZoom");
            //this.trackBarZoom.MaxValue = 100;
            appearance2.BackColor = System.Drawing.Color.White;
            this.trackBarZoom.MidpointSettings.Appearance = appearance2;
            this.trackBarZoom.MidpointSettings.Location = Infragistics.Win.UltraWinEditors.TickmarkLocation.Center;
            this.trackBarZoom.MidpointSettings.Visible = Infragistics.Win.DefaultableBoolean.True;
            this.trackBarZoom.MinValue = 1;
            this.trackBarZoom.Name = "trackBarZoom";
            this.trackBarZoom.TickmarkSettingsMinor.Visible = Infragistics.Win.DefaultableBoolean.False;
            this.trackBarZoom.Value = 1;
            this.trackBarZoom.ValueObject = 1;
            this.trackBarZoom.ViewStyle = Infragistics.Win.UltraWinEditors.TrackBarViewStyle.Office2007;
            this.trackBarZoom.ValueChanged += new System.EventHandler(this.trackBarZoom_ValueChanged);
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.Color.Transparent;
            this.pnlColor.Controls.Add(this.selectedColorIndicator1);
            this.pnlColor.Controls.Add(this.colorSelector1);
            resources.ApplyResources(this.pnlColor, "pnlColor");
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Resize += new System.EventHandler(this.pnlColor_Resize);
            // 
            // selectedColorIndicator1
            // 
            resources.ApplyResources(this.selectedColorIndicator1, "selectedColorIndicator1");
            this.selectedColorIndicator1.MaximumBoxSize = 16;
            this.selectedColorIndicator1.Name = "selectedColorIndicator1";
            this.selectedColorIndicator1.SelectedBackColorChanged += new System.EventHandler(this.selectedColorIndicator1_SelectedBackColorChanged);
            this.selectedColorIndicator1.SelectedForeColorChanged += new System.EventHandler(this.selectedColorIndicator1_SelectedForeColorChanged);
            // 
            // colorSelector1
            // 
            resources.ApplyResources(this.colorSelector1, "colorSelector1");
            this.colorSelector1.IncludeKnownColors = false;
            this.colorSelector1.Name = "colorSelector1";
            // 
            // pnlImageAttributes
            // 
            this.pnlImageAttributes.BackColor = System.Drawing.Color.Transparent;
            this.pnlImageAttributes.Controls.Add(this.ultraGroupBoxPreview);
            this.pnlImageAttributes.Controls.Add(this.ultraGroupBox1);
            resources.ApplyResources(this.pnlImageAttributes, "pnlImageAttributes");
            this.pnlImageAttributes.Name = "pnlImageAttributes";
            // 
            // ultraGroupBoxPreview
            // 
            resources.ApplyResources(this.ultraGroupBoxPreview, "ultraGroupBoxPreview");
            this.ultraGroupBoxPreview.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            appearance3.BorderColor = System.Drawing.Color.Blue;
            this.ultraGroupBoxPreview.ContentAreaAppearance = appearance3;
            this.ultraGroupBoxPreview.Controls.Add(this.ultraPictureBox1);
            appearance4.FontData.BoldAsString = resources.GetString("resource.BoldAsString");
            appearance4.FontData.Name = resources.GetString("resource.Name");
            appearance4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ultraGroupBoxPreview.HeaderAppearance = appearance4;
            this.ultraGroupBoxPreview.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBoxPreview.Name = "ultraGroupBoxPreview";
            this.ultraGroupBoxPreview.Resize += new System.EventHandler(this.ultraGroupBoxPreview_Resize);
            // 
            // ultraPictureBox1
            // 
            resources.ApplyResources(this.ultraPictureBox1, "ultraPictureBox1");
            this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
            this.ultraPictureBox1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraPictureBox1.Name = "ultraPictureBox1";
            this.ultraPictureBox1.ScaleImage = Infragistics.Win.ScaleImage.Always;
            // 
            // ultraGroupBox1
            // 
            resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            appearance5.BorderColor = System.Drawing.Color.Blue;
            this.ultraGroupBox1.ContentAreaAppearance = appearance5;
            this.ultraGroupBox1.Controls.Add(this.tableLayoutPanel1);
            appearance6.FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
            appearance6.FontData.Name = resources.GetString("resource.Name1");
            appearance6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ultraGroupBox1.HeaderAppearance = appearance6;
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.linkLabelSize);
            this.flowLayoutPanel1.Controls.Add(this.lblPixels);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // linkLabelSize
            // 
            resources.ApplyResources(this.linkLabelSize, "linkLabelSize");
            this.linkLabelSize.Name = "linkLabelSize";
            this.linkLabelSize.TabStop = true;
            this.toolTip1.SetToolTip(this.linkLabelSize, resources.GetString("linkLabelSize.ToolTip"));
            this.linkLabelSize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSize_LinkClicked);
            // 
            // lblPixels
            // 
            resources.ApplyResources(this.lblPixels, "lblPixels");
            this.lblPixels.Name = "lblPixels";
            // 
            // labelPixelFormat
            // 
            resources.ApplyResources(this.labelPixelFormat, "labelPixelFormat");
            this.labelPixelFormat.Name = "labelPixelFormat";
            // 
            // labelFormat
            // 
            resources.ApplyResources(this.labelFormat, "labelFormat");
            this.labelFormat.Name = "labelFormat";
            // 
            // labelResolution
            // 
            resources.ApplyResources(this.labelResolution, "labelResolution");
            this.labelResolution.Name = "labelResolution";
            // 
            // labelDPI
            // 
            resources.ApplyResources(this.labelDPI, "labelDPI");
            this.labelDPI.Name = "labelDPI";
            // 
            // labelSize
            // 
            resources.ApplyResources(this.labelSize, "labelSize");
            this.labelSize.Name = "labelSize";
            // 
            // pnlCaptureImage
            // 
            this.pnlCaptureImage.BackColor = System.Drawing.Color.Transparent;
            this.pnlCaptureImage.Controls.Add(this.label2);
            resources.ApplyResources(this.pnlCaptureImage, "pnlCaptureImage");
            this.pnlCaptureImage.Name = "pnlCaptureImage";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // ilSmall
            // 
            this.ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmall.ImageStream")));
            this.ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSmall.Images.SetKeyName(0, "IGPaint - 16x16 - App Icon.png");
            this.ilSmall.Images.SetKeyName(1, "IGPaint - 16x16 - Circle Border and Fill.png");
            this.ilSmall.Images.SetKeyName(2, "IGPaint - 16x16 - Circle Border.png");
            this.ilSmall.Images.SetKeyName(3, "IGPaint - 16x16 - Circle fill.png");
            this.ilSmall.Images.SetKeyName(4, "IGPaint - 16x16 - Color Back.png");
            this.ilSmall.Images.SetKeyName(5, "IGPaint - 16x16 - Color Front.png");
            this.ilSmall.Images.SetKeyName(6, "IGPaint - 16x16 - Copy format.png");
            this.ilSmall.Images.SetKeyName(7, "IGPaint - 16x16 - Copy.png");
            this.ilSmall.Images.SetKeyName(8, "IGPaint - 16x16 - Curves.png");
            this.ilSmall.Images.SetKeyName(9, "IGPaint - 16x16 - Cut.png");
            this.ilSmall.Images.SetKeyName(10, "IGPaint - 16x16 - Eraser All.png");
            this.ilSmall.Images.SetKeyName(11, "IGPaint - 16x16 - Eraser Matching.png");
            this.ilSmall.Images.SetKeyName(12, "IGPaint - 16x16 - Eye Drop.png");
            this.ilSmall.Images.SetKeyName(13, "IGPaint - 16x16 - Fill.png");
            this.ilSmall.Images.SetKeyName(14, "IGPaint - 16x16 - Flip Horizontal.png");
            this.ilSmall.Images.SetKeyName(15, "IGPaint - 16x16 - Flip Vertical.png");
            this.ilSmall.Images.SetKeyName(16, "IGPaint - 16x16 - Gradient.png");
            this.ilSmall.Images.SetKeyName(17, "IGPaint - 16x16 - Image Grid.png");
            this.ilSmall.Images.SetKeyName(18, "IGPaint - 16x16 - Invert Colors.png");
            this.ilSmall.Images.SetKeyName(19, "IGPaint - 16x16 - Line.png");
            this.ilSmall.Images.SetKeyName(20, "IGPaint - 16x16 - Move down.png");
            this.ilSmall.Images.SetKeyName(21, "IGPaint - 16x16 - Move left.png");
            this.ilSmall.Images.SetKeyName(22, "IGPaint - 16x16 - Move right.png");
            this.ilSmall.Images.SetKeyName(23, "IGPaint - 16x16 - Move Up.png");
            this.ilSmall.Images.SetKeyName(24, "IGPaint - 16x16 - New.png");
            this.ilSmall.Images.SetKeyName(25, "IGPaint - 16x16 - Open.png");
            this.ilSmall.Images.SetKeyName(26, "IGPaint - 16x16 - Palette.png");
            this.ilSmall.Images.SetKeyName(27, "IGPaint - 16x16 - Paste.png");
            this.ilSmall.Images.SetKeyName(28, "IGPaint - 16x16 - Pen.png");
            this.ilSmall.Images.SetKeyName(29, "IGPaint - 16x16 - Redo.png");
            this.ilSmall.Images.SetKeyName(30, "IGPaint - 16x16 - Rotate left.png");
            this.ilSmall.Images.SetKeyName(31, "IGPaint - 16x16 - Rotate Right.png");
            this.ilSmall.Images.SetKeyName(32, "IGPaint - 16x16 - Save 02.png");
            this.ilSmall.Images.SetKeyName(33, "IGPaint - 16x16 - Save as.png");
            this.ilSmall.Images.SetKeyName(34, "IGPaint - 16x16 - Save.png");
            this.ilSmall.Images.SetKeyName(35, "IGPaint - 16x16 - Select.png");
            this.ilSmall.Images.SetKeyName(36, "IGPaint - 16x16 - Spryer.png");
            this.ilSmall.Images.SetKeyName(37, "IGPaint - 16x16 - Square Fill and Line.png");
            this.ilSmall.Images.SetKeyName(38, "IGPaint - 16x16 - Square Fill.png");
            this.ilSmall.Images.SetKeyName(39, "IGPaint - 16x16 - Square line.png");
            this.ilSmall.Images.SetKeyName(40, "IGPaint - 16x16 - Text.png");
            this.ilSmall.Images.SetKeyName(41, "IGPaint - 16x16 - Undo.png");
            this.ilSmall.Images.SetKeyName(42, "IGPaint - 16x16 - Zoom In.png");
            this.ilSmall.Images.SetKeyName(43, "IGPaint - 16x16 - Zoom out.png");
            this.ilSmall.Images.SetKeyName(44, "IGPaint - 16x16 - Zoom to fit.png");
            this.ilSmall.Images.SetKeyName(45, "IGPaint - 16x16 - ForeColor.png");
            this.ilSmall.Images.SetKeyName(46, "IGPaint - 16x16 - Bold.png");
            this.ilSmall.Images.SetKeyName(47, "IGPaint - 16x16 - Italic.png");
            this.ilSmall.Images.SetKeyName(48, "IGPaint - 16x16 - Large Brush.png");
            this.ilSmall.Images.SetKeyName(49, "IGPaint - 16x16 - Medium Brush.png");
            this.ilSmall.Images.SetKeyName(50, "IGPaint - 16x16 - Small Brush.png");
            this.ilSmall.Images.SetKeyName(51, "IGPaint - 16x16 - StatusBar.png");
            this.ilSmall.Images.SetKeyName(52, "IGPaint - 16x16 - Underline.png");
            this.ilSmall.Images.SetKeyName(53, "IGPaint - 16x16 - Image Size.png");
            this.ilSmall.Images.SetKeyName(54, "IGPaint - 16x16 - Capture.png");
            this.ilSmall.Images.SetKeyName(55, "IGPaint - 16x16 - Clear Image.png");
            // 
            // pnlMain
            // 
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            appearance7.BorderColor = System.Drawing.Color.Gainsboro;
            this.pnlMain.Appearance = appearance7;
            this.pnlMain.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            // 
            // pnlMain.ClientArea
            // 
            this.pnlMain.ClientArea.Controls.Add(this.pnlImageAttributes);
            this.pnlMain.ClientArea.Controls.Add(this.pnlColor);
            this.pnlMain.ClientArea.Controls.Add(this.ultraImageEditor1);
            this.pnlMain.ClientArea.Controls.Add(this.pnlCaptureImage);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // ultraImageEditor1
            // 
            this.ultraImageEditor1.DarkGridLineSpacing = 0;
            resources.ApplyResources(this.ultraImageEditor1, "ultraImageEditor1");
            this.ultraImageEditor1.MagnificationLevel = 11.44444F;
            this.ultraImageEditor1.Name = "ultraImageEditor1";
            this.ultraImageEditor1.TransparentHatchColor = System.Drawing.Color.Transparent;
            this.ultraImageEditor1.AfterImageEdited += new Infragistics.Win.UltraImageEditor.AfterImageEditedEventHandler(this.ultraImageEditor1_AfterImageEdited);
            this.ultraImageEditor1.ColorSelectColorChanged += new Infragistics.Win.UltraImageEditor.ColorSelectedEventHandler(this.ultraImageEditor1_ColorSelectColorChanged);
            // 
            // _frmImageEditor_Toolbars_Dock_Area_Left
            // 
            this._frmImageEditor_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmImageEditor_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._frmImageEditor_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._frmImageEditor_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmImageEditor_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 1;
            resources.ApplyResources(this._frmImageEditor_Toolbars_Dock_Area_Left, "_frmImageEditor_Toolbars_Dock_Area_Left");
            this._frmImageEditor_Toolbars_Dock_Area_Left.Name = "_frmImageEditor_Toolbars_Dock_Area_Left";
            this._frmImageEditor_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // ilLarge
            // 
            this.ilLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLarge.ImageStream")));
            this.ilLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLarge.Images.SetKeyName(0, "IGPaint - 32x32 - Gradient.png");
            this.ilLarge.Images.SetKeyName(1, "IGPaint - 32x32 - Paste.png");
            this.ilLarge.Images.SetKeyName(2, "IGPaint - 32x32 - ForeColor.png");
            // 
            // _frmImageEditor_Toolbars_Dock_Area_Right
            // 
            this._frmImageEditor_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmImageEditor_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._frmImageEditor_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._frmImageEditor_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmImageEditor_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 1;
            resources.ApplyResources(this._frmImageEditor_Toolbars_Dock_Area_Right, "_frmImageEditor_Toolbars_Dock_Area_Right");
            this._frmImageEditor_Toolbars_Dock_Area_Right.Name = "_frmImageEditor_Toolbars_Dock_Area_Right";
            this._frmImageEditor_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _frmImageEditor_Toolbars_Dock_Area_Top
            // 
            this._frmImageEditor_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmImageEditor_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._frmImageEditor_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._frmImageEditor_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this._frmImageEditor_Toolbars_Dock_Area_Top, "_frmImageEditor_Toolbars_Dock_Area_Top");
            this._frmImageEditor_Toolbars_Dock_Area_Top.Name = "_frmImageEditor_Toolbars_Dock_Area_Top";
            this._frmImageEditor_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _frmImageEditor_Toolbars_Dock_Area_Bottom
            // 
            this._frmImageEditor_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmImageEditor_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._frmImageEditor_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._frmImageEditor_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this._frmImageEditor_Toolbars_Dock_Area_Bottom, "_frmImageEditor_Toolbars_Dock_Area_Bottom");
            this._frmImageEditor_Toolbars_Dock_Area_Bottom.Name = "_frmImageEditor_Toolbars_Dock_Area_Bottom";
            this._frmImageEditor_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // ultraStatusBar1
            // 
            appearance8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(88)))), ((int)(((byte)(155)))));
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance8.ForeColor = System.Drawing.Color.White;
            this.ultraStatusBar1.Appearance = appearance8;
            this.ultraStatusBar1.BorderStylePanel = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraStatusBar1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Borderless;
            this.ultraStatusBar1.Controls.Add(this.trackBarZoom);
            resources.ApplyResources(this.ultraStatusBar1, "ultraStatusBar1");
            this.ultraStatusBar1.Name = "ultraStatusBar1";
            ultraStatusPanel1.Key = "currentToolPanel";
            ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Spring;
            resources.ApplyResources(ultraStatusPanel1, "ultraStatusPanel1");
            ultraStatusPanel2.Key = "positionPanel";
            resources.ApplyResources(ultraStatusPanel2, "ultraStatusPanel2");
            appearance9.ForeColor = System.Drawing.Color.White;
            ultraStatusPanel3.Appearance = appearance9;
            ultraStatusPanel3.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            ultraStatusPanel3.Key = "dimensionsPanel";
            ultraStatusPanel3.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Button;
            resources.ApplyResources(ultraStatusPanel3, "ultraStatusPanel3");
            ultraStatusPanel4.Control = this.trackBarZoom;
            ultraStatusPanel4.Key = "zoom";
            ultraStatusPanel4.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.ControlContainer;
            ultraStatusPanel4.Width = 150;
            ultraStatusPanel5.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            ultraStatusPanel5.Key = "zoomMultiplier";
            ultraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            resources.ApplyResources(ultraStatusPanel5, "ultraStatusPanel5");
            this.ultraStatusBar1.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[] {
            ultraStatusPanel1,
            ultraStatusPanel2,
            ultraStatusPanel3,
            ultraStatusPanel4,
            ultraStatusPanel5});
            this.ultraStatusBar1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraStatusBar1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.ultraStatusBar1.ViewStyle = Infragistics.Win.UltraWinStatusBar.ViewStyle.Office2003;
            this.ultraStatusBar1.ButtonClick += new Infragistics.Win.UltraWinStatusBar.PanelEventHandler(this.ultraStatusBar1_ButtonClick);
            // 
            // saveFileDlg
            // 
            this.saveFileDlg.FileName = "image1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.labelPixelFormat, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelFormat, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelSize, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelDPI, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelResolution, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this;
            this.ultraToolbarsManager1.DockWithinContainerBaseType = typeof(System.Windows.Forms.Form);
            this.ultraToolbarsManager1.ImageListLarge = this.ilLarge;
            this.ultraToolbarsManager1.ImageListSmall = this.ilSmall;
            this.ultraToolbarsManager1.Office2007UICompatibility = false;
            optionSet1.AllowAllUp = false;
            optionSet2.AllowAllUp = false;
            optionSet3.AllowAllUp = false;
            optionSet4.AllowAllUp = false;
            this.ultraToolbarsManager1.OptionSets.Add(optionSet1);
            this.ultraToolbarsManager1.OptionSets.Add(optionSet2);
            this.ultraToolbarsManager1.OptionSets.Add(optionSet3);
            this.ultraToolbarsManager1.OptionSets.Add(optionSet4);
            this.ultraToolbarsManager1.Ribbon.ApplicationMenu.ToolAreaRight.Settings.LabelDisplayStyle = Infragistics.Win.UltraWinToolbars.LabelMenuDisplayStyle.Header;
            appearance10.Image = "IGPaint - 16x16 - Save.png";
            buttonTool76.InstanceProps.AppearancesSmall.Appearance = appearance10;
            buttonTool78.InstanceProps.IsFirstInGroup = true;
            popupControlContainerTool1.InstanceProps.IsFirstInGroup = true;
            this.ultraToolbarsManager1.Ribbon.ApplicationMenu2010.NavigationMenu.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool74,
            buttonTool75,
            buttonTool76,
            buttonTool77,
            popupMenuTool17,
            buttonTool79,
            buttonTool78,
            popupControlContainerTool1});
            this.ultraToolbarsManager1.Ribbon.FileMenuStyle = Infragistics.Win.UltraWinToolbars.FileMenuStyle.ApplicationMenu2010;
            resources.ApplyResources(contextualTabGroup1, "contextualTabGroup1");
            contextualTabGroup1.Key = "Text Tools";
            contextualTabGroup1.Visible = false;
            this.ultraToolbarsManager1.Ribbon.NonInheritedContextualTabGroups.AddRange(new Infragistics.Win.UltraWinToolbars.ContextualTabGroup[] {
            contextualTabGroup1});
            resources.ApplyResources(ribbonTab1, "ribbonTab1");
            resources.ApplyResources(ribbonGroup1, "ribbonGroup1");
            buttonTool11.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
            ribbonGroup1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool11,
            buttonTool12,
            buttonTool13});
            resources.ApplyResources(ribbonGroup2, "ribbonGroup2");
            ribbonGroup2.LayoutDirection = Infragistics.Win.UltraWinToolbars.RibbonGroupToolLayoutDirection.Horizontal;
            ribbonGroup2.PreferredToolSize = Infragistics.Win.UltraWinToolbars.RibbonToolSize.ImageOnly;
            ribbonGroup2.Settings.CanCollapse = Infragistics.Win.DefaultableBoolean.False;
            stateButtonTool1.InstanceProps.ButtonGroup = "Select";
            stateButtonTool16.InstanceProps.ButtonGroup = "Select";
            stateButtonTool2.InstanceProps.ButtonGroup = "Select";
            stateButtonTool7.InstanceProps.ButtonGroup = "Select";
            stateButtonTool6.InstanceProps.ButtonGroup = "Erase";
            stateButtonTool3.InstanceProps.ButtonGroup = "Erase";
            stateButtonTool4.InstanceProps.ButtonGroup = "Erase";
            stateButtonTool5.Checked = true;
            stateButtonTool5.InstanceProps.ButtonGroup = "Draw";
            stateButtonTool17.InstanceProps.ButtonGroup = "Draw";
            stateButtonTool8.InstanceProps.ButtonGroup = "Rectangle";
            stateButtonTool9.InstanceProps.ButtonGroup = "Rectangle";
            stateButtonTool10.InstanceProps.ButtonGroup = "Rectangle";
            stateButtonTool11.InstanceProps.ButtonGroup = "Rectangle";
            stateButtonTool12.InstanceProps.ButtonGroup = "Rectangle";
            stateButtonTool13.InstanceProps.ButtonGroup = "Ellipse";
            stateButtonTool14.InstanceProps.ButtonGroup = "Ellipse";
            stateButtonTool15.InstanceProps.ButtonGroup = "Ellipse";
            ribbonGroup2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            stateButtonTool1,
            stateButtonTool16,
            stateButtonTool2,
            stateButtonTool7,
            stateButtonTool6,
            stateButtonTool3,
            stateButtonTool4,
            stateButtonTool5,
            stateButtonTool17,
            stateButtonTool8,
            stateButtonTool9,
            stateButtonTool10,
            stateButtonTool11,
            stateButtonTool12,
            stateButtonTool13,
            stateButtonTool14,
            stateButtonTool15});
            resources.ApplyResources(ribbonGroup3, "ribbonGroup3");
            ribbonGroup3.DialogBoxLauncherKey = "Color";
            popupMenuTool1.InstanceProps.IsFirstInGroup = true;
            buttonTool14.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Normal;
            ribbonGroup3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            popupColorPickerTool1,
            popupColorPickerTool2,
            popupMenuTool1,
            buttonTool14});
            resources.ApplyResources(ribbonGroup4, "ribbonGroup4");
            popupMenuTool2.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
            stateButtonTool19.Checked = true;
            ribbonGroup4.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            popupMenuTool2,
            stateButtonTool18,
            stateButtonTool19,
            stateButtonTool20});
            resources.ApplyResources(ribbonGroup5, "ribbonGroup5");
            buttonTool15.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Normal;
            buttonTool16.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Normal;
            buttonTool17.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Normal;
            ribbonGroup5.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool15,
            buttonTool16,
            buttonTool17});
            ribbonTab1.Groups.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonGroup[] {
            ribbonGroup1,
            ribbonGroup2,
            ribbonGroup3,
            ribbonGroup4,
            ribbonGroup5});
            resources.ApplyResources(ribbonTab2, "ribbonTab2");
            resources.ApplyResources(ribbonGroup6, "ribbonGroup6");
            buttonTool20.InstanceProps.IsFirstInGroup = true;
            ribbonGroup6.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool18,
            buttonTool19,
            buttonTool20,
            buttonTool21});
            resources.ApplyResources(ribbonGroup7, "ribbonGroup7");
            labelTool1.InstanceProps.Width = 16;
            buttonTool1.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.ImageOnly;
            buttonTool4.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.ImageOnly;
            labelTool7.InstanceProps.Width = 17;
            buttonTool3.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.ImageOnly;
            buttonTool2.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.ImageOnly;
            ribbonGroup7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            labelTool1,
            buttonTool1,
            labelTool5,
            buttonTool4,
            labelTool7,
            buttonTool3,
            labelTool6,
            buttonTool2});
            resources.ApplyResources(ribbonGroup8, "ribbonGroup8");
            ribbonGroup8.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool26,
            buttonTool27,
            buttonTool28});
            resources.ApplyResources(ribbonGroup9, "ribbonGroup9");
            stateButtonTool21.Checked = true;
            stateButtonTool22.Checked = true;
            stateButtonTool22.InstanceProps.MinimumSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Normal;
            stateButtonTool22.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            ribbonGroup9.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            stateButtonTool21,
            stateButtonTool22});
            ribbonTab2.Groups.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonGroup[] {
            ribbonGroup6,
            ribbonGroup7,
            ribbonGroup8,
            ribbonGroup9});
            resources.ApplyResources(ribbonTab3, "ribbonTab3");
            ribbonTab3.ContextualTabGroupKey = "Text Tools";
            resources.ApplyResources(ribbonGroup10, "ribbonGroup10");
            fontListTool1.InstanceProps.Width = 199;
            comboBoxTool1.InstanceProps.Width = 109;
            stateButtonTool23.InstanceProps.IsFirstInGroup = true;
            ribbonGroup10.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            fontListTool1,
            comboBoxTool1,
            stateButtonTool23,
            stateButtonTool24,
            stateButtonTool25});
            resources.ApplyResources(ribbonGroup11, "ribbonGroup11");
            appearance11.Image = "IGPaint - 32x32 - ForeColor.png";
            popupColorPickerTool3.InstanceProps.AppearancesLarge.Appearance = appearance11;
            appearance12.Image = "IGPaint - 16x16 - ForeColor.png";
            popupColorPickerTool3.InstanceProps.AppearancesSmall.Appearance = appearance12;
            resources.ApplyResources(popupColorPickerTool3.InstanceProps, "popupColorPickerTool3.InstanceProps");
            popupColorPickerTool3.InstanceProps.PreferredSizeOnRibbon = Infragistics.Win.UltraWinToolbars.RibbonToolSize.Large;
            popupColorPickerTool3.ForceApplyResources = "InstanceProps";
            ribbonGroup11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            popupColorPickerTool3});
            ribbonTab3.Groups.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonGroup[] {
            ribbonGroup10,
            ribbonGroup11});
            this.ultraToolbarsManager1.Ribbon.NonInheritedRibbonTabs.AddRange(new Infragistics.Win.UltraWinToolbars.RibbonTab[] {
            ribbonTab1,
            ribbonTab2,
            ribbonTab3});
            this.ultraToolbarsManager1.Ribbon.QuickAccessToolbar.ContextMenuToolKeys = new string[] {
        "New",
        "Open",
        "Save",
        "UndoTool",
        "RedoTool"};
            this.ultraToolbarsManager1.Ribbon.QuickAccessToolbar.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool8,
            buttonTool9,
            buttonTool10});
            this.ultraToolbarsManager1.Ribbon.Visible = true;
            this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2013;
            ultraTaskPaneToolbar1.DockedColumn = 0;
            ultraTaskPaneToolbar1.DockedContentExtent = 214;
            ultraTaskPaneToolbar1.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            ultraTaskPaneToolbar1.DockedRow = 0;
            ultraTaskPaneToolbar1.FloatingContentSize = new System.Drawing.Size(200, 370);
            ultraTaskPaneToolbar1.FloatingLocation = new System.Drawing.Point(621, 325);
            ultraTaskPaneToolbar1.FloatingSize = new System.Drawing.Size(200, 395);
            ultraTaskPaneToolbar1.HomeTaskPaneToolKey = "Image Attributes";
            ultraTaskPaneToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            taskPaneTool1});
            ultraTaskPaneToolbar1.SelectedTaskPaneToolKey = "Image Attributes";
            resources.ApplyResources(ultraTaskPaneToolbar1, "ultraTaskPaneToolbar1");
            ultraTaskPaneToolbar2.DockedColumn = 0;
            ultraTaskPaneToolbar2.DockedContentExtent = 145;
            ultraTaskPaneToolbar2.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            ultraTaskPaneToolbar2.DockedRow = 0;
            ultraTaskPaneToolbar2.HomeTaskPaneToolKey = "Color Chooser";
            ultraTaskPaneToolbar2.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            taskPaneTool3});
            ultraTaskPaneToolbar2.SelectedTaskPaneToolKey = "Color Chooser";
            resources.ApplyResources(ultraTaskPaneToolbar2, "ultraTaskPaneToolbar2");
            ultraTaskPaneToolbar2.Visible = false;
            this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraTaskPaneToolbar1,
            ultraTaskPaneToolbar2});
            taskPaneTool4.ControlName = "pnlColor";
            resources.ApplyResources(taskPaneTool4.SharedPropsInternal, "taskPaneTool4.SharedPropsInternal");
            taskPaneTool4.ForceApplyResources = "SharedPropsInternal";
            appearance13.Image = "IGPaint - 16x16 - Cut.png";
            buttonTool29.SharedPropsInternal.AppearancesSmall.Appearance = appearance13;
            resources.ApplyResources(buttonTool29.SharedPropsInternal, "buttonTool29.SharedPropsInternal");
            buttonTool29.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            buttonTool29.ForceApplyResources = "SharedPropsInternal";
            appearance14.Image = "IGPaint - 16x16 - Copy.png";
            buttonTool30.SharedPropsInternal.AppearancesSmall.Appearance = appearance14;
            resources.ApplyResources(buttonTool30.SharedPropsInternal, "buttonTool30.SharedPropsInternal");
            buttonTool30.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            buttonTool30.ForceApplyResources = "SharedPropsInternal";
            appearance15.Image = "IGPaint - 32x32 - Paste.png";
            buttonTool31.SharedPropsInternal.AppearancesLarge.Appearance = appearance15;
            appearance16.Image = 24;
            buttonTool31.SharedPropsInternal.AppearancesSmall.Appearance = appearance16;
            resources.ApplyResources(buttonTool31.SharedPropsInternal, "buttonTool31.SharedPropsInternal");
            buttonTool31.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            buttonTool31.ForceApplyResources = "SharedPropsInternal";
            appearance17.Image = "IGPaint - 16x16 - Undo.png";
            buttonTool32.SharedPropsInternal.AppearancesSmall.Appearance = appearance17;
            resources.ApplyResources(buttonTool32.SharedPropsInternal, "buttonTool32.SharedPropsInternal");
            buttonTool32.SharedPropsInternal.Enabled = false;
            buttonTool32.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            buttonTool32.ForceApplyResources = "SharedPropsInternal";
            appearance18.Image = "IGPaint - 16x16 - Redo.png";
            buttonTool33.SharedPropsInternal.AppearancesSmall.Appearance = appearance18;
            resources.ApplyResources(buttonTool33.SharedPropsInternal, "buttonTool33.SharedPropsInternal");
            buttonTool33.SharedPropsInternal.Enabled = false;
            buttonTool33.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            buttonTool33.ForceApplyResources = "SharedPropsInternal";
            taskPaneTool5.ControlName = "pnlImageAttributes";
            resources.ApplyResources(taskPaneTool5, "taskPaneTool5");
            resources.ApplyResources(taskPaneTool5.SharedPropsInternal, "taskPaneTool5.SharedPropsInternal");
            taskPaneTool5.ForceApplyResources = "SharedPropsInternal|";
            appearance19.Image = "IGPaint - 16x16 - Rotate left.png";
            buttonTool34.SharedPropsInternal.AppearancesSmall.Appearance = appearance19;
            resources.ApplyResources(buttonTool34.SharedPropsInternal, "buttonTool34.SharedPropsInternal");
            buttonTool34.ForceApplyResources = "SharedPropsInternal";
            appearance20.Image = "IGPaint - 16x16 - Rotate Right.png";
            buttonTool35.SharedPropsInternal.AppearancesSmall.Appearance = appearance20;
            resources.ApplyResources(buttonTool35.SharedPropsInternal, "buttonTool35.SharedPropsInternal");
            buttonTool35.ForceApplyResources = "SharedPropsInternal";
            appearance21.Image = "IGPaint - 16x16 - Zoom In.png";
            buttonTool36.SharedPropsInternal.AppearancesSmall.Appearance = appearance21;
            resources.ApplyResources(buttonTool36.SharedPropsInternal, "buttonTool36.SharedPropsInternal");
            buttonTool36.ForceApplyResources = "SharedPropsInternal";
            appearance22.Image = "IGPaint - 16x16 - Zoom out.png";
            buttonTool37.SharedPropsInternal.AppearancesSmall.Appearance = appearance22;
            resources.ApplyResources(buttonTool37.SharedPropsInternal, "buttonTool37.SharedPropsInternal");
            buttonTool37.ForceApplyResources = "SharedPropsInternal";
            appearance23.Image = "IGPaint - 16x16 - Zoom to fit.png";
            buttonTool38.SharedPropsInternal.AppearancesSmall.Appearance = appearance23;
            resources.ApplyResources(buttonTool38.SharedPropsInternal, "buttonTool38.SharedPropsInternal");
            buttonTool38.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool26.OptionSetKey = "BrushSize";
            appearance24.Image = "IGPaint - 16x16 - Small Brush.png";
            stateButtonTool26.SharedPropsInternal.AppearancesSmall.Appearance = appearance24;
            resources.ApplyResources(stateButtonTool26.SharedPropsInternal, "stateButtonTool26.SharedPropsInternal");
            stateButtonTool26.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool27.Checked = true;
            stateButtonTool27.OptionSetKey = "BrushSize";
            appearance25.Image = "IGPaint - 16x16 - Medium Brush.png";
            stateButtonTool27.SharedPropsInternal.AppearancesSmall.Appearance = appearance25;
            resources.ApplyResources(stateButtonTool27.SharedPropsInternal, "stateButtonTool27.SharedPropsInternal");
            stateButtonTool27.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool28.OptionSetKey = "BrushSize";
            appearance26.Image = "IGPaint - 16x16 - Large Brush.png";
            stateButtonTool28.SharedPropsInternal.AppearancesSmall.Appearance = appearance26;
            resources.ApplyResources(stateButtonTool28.SharedPropsInternal, "stateButtonTool28.SharedPropsInternal");
            stateButtonTool28.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool29.OptionSetKey = "DrawingTools";
            appearance27.Image = "IGPaint - 16x16 - Select.png";
            stateButtonTool29.SharedPropsInternal.AppearancesSmall.Appearance = appearance27;
            resources.ApplyResources(stateButtonTool29.SharedPropsInternal, "stateButtonTool29.SharedPropsInternal");
            stateButtonTool29.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool30.OptionSetKey = "DrawingTools";
            appearance28.Image = "IGPaint - 16x16 - Eraser All.png";
            stateButtonTool30.SharedPropsInternal.AppearancesSmall.Appearance = appearance28;
            resources.ApplyResources(stateButtonTool30.SharedPropsInternal, "stateButtonTool30.SharedPropsInternal");
            stateButtonTool30.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool31.OptionSetKey = "DrawingTools";
            appearance29.Image = "IGPaint - 16x16 - Eraser Matching.png";
            stateButtonTool31.SharedPropsInternal.AppearancesSmall.Appearance = appearance29;
            resources.ApplyResources(stateButtonTool31.SharedPropsInternal, "stateButtonTool31.SharedPropsInternal");
            stateButtonTool31.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool32.OptionSetKey = "DrawingTools";
            appearance30.Image = "IGPaint - 16x16 - Eye Drop.png";
            stateButtonTool32.SharedPropsInternal.AppearancesSmall.Appearance = appearance30;
            resources.ApplyResources(stateButtonTool32.SharedPropsInternal, "stateButtonTool32.SharedPropsInternal");
            stateButtonTool32.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool33.Checked = true;
            stateButtonTool33.OptionSetKey = "DrawingTools";
            appearance31.Image = "IGPaint - 16x16 - Pen.png";
            stateButtonTool33.SharedPropsInternal.AppearancesSmall.Appearance = appearance31;
            resources.ApplyResources(stateButtonTool33.SharedPropsInternal, "stateButtonTool33.SharedPropsInternal");
            stateButtonTool33.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool34.OptionSetKey = "DrawingTools";
            appearance32.Image = "IGPaint - 16x16 - Spryer.png";
            stateButtonTool34.SharedPropsInternal.AppearancesSmall.Appearance = appearance32;
            resources.ApplyResources(stateButtonTool34.SharedPropsInternal, "stateButtonTool34.SharedPropsInternal");
            stateButtonTool34.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool35.OptionSetKey = "DrawingTools";
            appearance33.Image = "IGPaint - 16x16 - Fill.png";
            stateButtonTool35.SharedPropsInternal.AppearancesSmall.Appearance = appearance33;
            resources.ApplyResources(stateButtonTool35.SharedPropsInternal, "stateButtonTool35.SharedPropsInternal");
            stateButtonTool35.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool36.OptionSetKey = "DrawingTools";
            appearance34.Image = "IGPaint - 16x16 - Line.png";
            stateButtonTool36.SharedPropsInternal.AppearancesSmall.Appearance = appearance34;
            resources.ApplyResources(stateButtonTool36.SharedPropsInternal, "stateButtonTool36.SharedPropsInternal");
            stateButtonTool36.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool37.OptionSetKey = "DrawingTools";
            appearance35.Image = "IGPaint - 16x16 - Curves.png";
            stateButtonTool37.SharedPropsInternal.AppearancesSmall.Appearance = appearance35;
            resources.ApplyResources(stateButtonTool37.SharedPropsInternal, "stateButtonTool37.SharedPropsInternal");
            stateButtonTool37.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool38.OptionSetKey = "DrawingTools";
            appearance36.Image = "IGPaint - 16x16 - Copy format.png";
            stateButtonTool38.SharedPropsInternal.AppearancesSmall.Appearance = appearance36;
            resources.ApplyResources(stateButtonTool38.SharedPropsInternal, "stateButtonTool38.SharedPropsInternal");
            stateButtonTool38.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool39.OptionSetKey = "DrawingTools";
            appearance37.Image = "IGPaint - 16x16 - Circle Border.png";
            stateButtonTool39.SharedPropsInternal.AppearancesSmall.Appearance = appearance37;
            resources.ApplyResources(stateButtonTool39.SharedPropsInternal, "stateButtonTool39.SharedPropsInternal");
            stateButtonTool39.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool40.OptionSetKey = "DrawingTools";
            appearance38.Image = "IGPaint - 16x16 - Circle fill.png";
            stateButtonTool40.SharedPropsInternal.AppearancesSmall.Appearance = appearance38;
            resources.ApplyResources(stateButtonTool40.SharedPropsInternal, "stateButtonTool40.SharedPropsInternal");
            stateButtonTool40.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool41.OptionSetKey = "DrawingTools";
            appearance39.Image = "IGPaint - 16x16 - Circle Border and Fill.png";
            stateButtonTool41.SharedPropsInternal.AppearancesSmall.Appearance = appearance39;
            resources.ApplyResources(stateButtonTool41.SharedPropsInternal, "stateButtonTool41.SharedPropsInternal");
            stateButtonTool41.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool42.OptionSetKey = "DrawingTools";
            appearance40.Image = "IGPaint - 16x16 - Square line.png";
            stateButtonTool42.SharedPropsInternal.AppearancesSmall.Appearance = appearance40;
            resources.ApplyResources(stateButtonTool42.SharedPropsInternal, "stateButtonTool42.SharedPropsInternal");
            stateButtonTool42.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool43.OptionSetKey = "DrawingTools";
            appearance41.Image = "IGPaint - 16x16 - Square Fill.png";
            stateButtonTool43.SharedPropsInternal.AppearancesSmall.Appearance = appearance41;
            resources.ApplyResources(stateButtonTool43.SharedPropsInternal, "stateButtonTool43.SharedPropsInternal");
            stateButtonTool43.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool44.OptionSetKey = "DrawingTools";
            appearance42.Image = "IGPaint - 16x16 - Square Fill and Line.png";
            stateButtonTool44.SharedPropsInternal.AppearancesSmall.Appearance = appearance42;
            resources.ApplyResources(stateButtonTool44.SharedPropsInternal, "stateButtonTool44.SharedPropsInternal");
            stateButtonTool44.ForceApplyResources = "SharedPropsInternal";
            appearance43.Image = "IGPaint - 16x16 - Invert Colors.png";
            buttonTool39.SharedPropsInternal.AppearancesSmall.Appearance = appearance43;
            resources.ApplyResources(buttonTool39.SharedPropsInternal, "buttonTool39.SharedPropsInternal");
            buttonTool39.ForceApplyResources = "SharedPropsInternal";
            popupMenuTool3.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Segmented;
            appearance44.Image = "IGPaint - 32x32 - Gradient.png";
            popupMenuTool3.SharedPropsInternal.AppearancesLarge.Appearance = appearance44;
            appearance45.Image = "IGPaint - 16x16 - Gradient.png";
            popupMenuTool3.SharedPropsInternal.AppearancesSmall.Appearance = appearance45;
            resources.ApplyResources(popupMenuTool3.SharedPropsInternal, "popupMenuTool3.SharedPropsInternal");
            popupMenuTool3.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            stateButtonTool45,
            stateButtonTool46,
            stateButtonTool47,
            stateButtonTool48,
            stateButtonTool49,
            stateButtonTool50,
            stateButtonTool51,
            stateButtonTool52,
            stateButtonTool53,
            stateButtonTool54});
            popupMenuTool3.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool55.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool55.SharedPropsInternal, "stateButtonTool55.SharedPropsInternal");
            stateButtonTool55.SharedPropsInternal.Tag = 1;
            stateButtonTool55.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool56.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool56.SharedPropsInternal, "stateButtonTool56.SharedPropsInternal");
            stateButtonTool56.SharedPropsInternal.Tag = 2;
            stateButtonTool56.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool57.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool57.SharedPropsInternal, "stateButtonTool57.SharedPropsInternal");
            stateButtonTool57.SharedPropsInternal.Tag = 3;
            stateButtonTool57.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool58.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool58.SharedPropsInternal, "stateButtonTool58.SharedPropsInternal");
            stateButtonTool58.SharedPropsInternal.Tag = 4;
            stateButtonTool58.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool59.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool59.SharedPropsInternal, "stateButtonTool59.SharedPropsInternal");
            stateButtonTool59.SharedPropsInternal.Tag = 5;
            stateButtonTool59.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool60.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool60.SharedPropsInternal, "stateButtonTool60.SharedPropsInternal");
            stateButtonTool60.SharedPropsInternal.Tag = 6;
            stateButtonTool60.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool61.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool61.SharedPropsInternal, "stateButtonTool61.SharedPropsInternal");
            stateButtonTool61.SharedPropsInternal.Tag = 7;
            stateButtonTool61.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool62.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool62.SharedPropsInternal, "stateButtonTool62.SharedPropsInternal");
            stateButtonTool62.SharedPropsInternal.Tag = 8;
            stateButtonTool62.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool63.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool63.SharedPropsInternal, "stateButtonTool63.SharedPropsInternal");
            stateButtonTool63.SharedPropsInternal.Tag = 9;
            stateButtonTool63.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool64.OptionSetKey = "GradientStyle";
            resources.ApplyResources(stateButtonTool64.SharedPropsInternal, "stateButtonTool64.SharedPropsInternal");
            stateButtonTool64.SharedPropsInternal.Tag = 10;
            stateButtonTool64.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool65.Checked = true;
            appearance46.Image = "IGPaint - 16x16 - Image Grid.png";
            stateButtonTool65.SharedPropsInternal.AppearancesSmall.Appearance = appearance46;
            resources.ApplyResources(stateButtonTool65.SharedPropsInternal, "stateButtonTool65.SharedPropsInternal");
            stateButtonTool65.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            stateButtonTool65.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(popupMenuTool4.SharedPropsInternal, "popupMenuTool4.SharedPropsInternal");
            popupMenuTool4.ForceApplyResources = "SharedPropsInternal";
            appearance47.Image = "IGPaint - 16x16 - Open.png";
            buttonTool40.SharedPropsInternal.AppearancesSmall.Appearance = appearance47;
            resources.ApplyResources(buttonTool40.SharedPropsInternal, "buttonTool40.SharedPropsInternal");
            buttonTool40.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            buttonTool40.ForceApplyResources = "SharedPropsInternal";
            appearance48.Image = "IGPaint - 16x16 - New.png";
            buttonTool41.SharedPropsInternal.AppearancesSmall.Appearance = appearance48;
            resources.ApplyResources(buttonTool41.SharedPropsInternal, "buttonTool41.SharedPropsInternal");
            buttonTool41.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            buttonTool41.ForceApplyResources = "SharedPropsInternal";
            appearance49.Image = "IGPaint - 16x16 - Save as.png";
            buttonTool42.SharedPropsInternal.AppearancesSmall.Appearance = appearance49;
            resources.ApplyResources(buttonTool42.SharedPropsInternal, "buttonTool42.SharedPropsInternal");
            buttonTool42.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(buttonTool43.SharedPropsInternal, "buttonTool43.SharedPropsInternal");
            buttonTool43.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool43.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(popupMenuTool5.SharedPropsInternal, "popupMenuTool5.SharedPropsInternal");
            popupMenuTool5.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(textBoxTool1.SharedPropsInternal, "textBoxTool1.SharedPropsInternal");
            textBoxTool1.ForceApplyResources = "SharedPropsInternal";
            appearance50.Image = "IGPaint - 16x16 - Image Size.png";
            buttonTool45.SharedPropsInternal.AppearancesSmall.Appearance = appearance50;
            resources.ApplyResources(buttonTool45.SharedPropsInternal, "buttonTool45.SharedPropsInternal");
            buttonTool45.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.DefaultForToolType;
            buttonTool45.ForceApplyResources = "SharedPropsInternal";
            appearance51.Image = "IGPaint - 16x16 - Capture.png";
            buttonTool46.SharedPropsInternal.AppearancesSmall.Appearance = appearance51;
            resources.ApplyResources(buttonTool46.SharedPropsInternal, "buttonTool46.SharedPropsInternal");
            buttonTool46.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(popupMenuTool6.SharedPropsInternal, "popupMenuTool6.SharedPropsInternal");
            buttonTool47.InstanceProps.IsFirstInGroup = true;
            popupMenuTool6.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool47,
            buttonTool48,
            buttonTool49});
            popupMenuTool6.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(popupMenuTool7.SharedPropsInternal, "popupMenuTool7.SharedPropsInternal");
            stateButtonTool66.Checked = true;
            buttonTool50.InstanceProps.IsFirstInGroup = true;
            popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            stateButtonTool66,
            buttonTool50,
            buttonTool51,
            buttonTool52});
            popupMenuTool7.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool67.Checked = true;
            stateButtonTool67.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            appearance52.Image = "IGPaint - 16x16 - StatusBar.png";
            stateButtonTool67.SharedPropsInternal.AppearancesSmall.Appearance = appearance52;
            resources.ApplyResources(stateButtonTool67.SharedPropsInternal, "stateButtonTool67.SharedPropsInternal");
            stateButtonTool67.ForceApplyResources = "SharedPropsInternal";
            appearance53.Image = "IGPaint - 16x16 - Flip Vertical.png";
            buttonTool53.SharedPropsInternal.AppearancesSmall.Appearance = appearance53;
            resources.ApplyResources(buttonTool53.SharedPropsInternal, "buttonTool53.SharedPropsInternal");
            buttonTool53.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.DefaultForToolType;
            buttonTool53.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(popupMenuTool8.SharedPropsInternal, "popupMenuTool8.SharedPropsInternal");
            popupColorPickerTool4.InstanceProps.IsFirstInGroup = true;
            buttonTool59.InstanceProps.IsFirstInGroup = true;
            popupMenuTool8.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool54,
            buttonTool55,
            buttonTool56,
            buttonTool57,
            popupMenuTool9,
            popupColorPickerTool4,
            popupColorPickerTool5,
            popupMenuTool10,
            buttonTool58,
            buttonTool59,
            buttonTool60,
            buttonTool61});
            popupMenuTool8.ForceApplyResources = "SharedPropsInternal";
            appearance54.Image = "IGPaint - 16x16 - Flip Horizontal.png";
            buttonTool62.SharedPropsInternal.AppearancesSmall.Appearance = appearance54;
            resources.ApplyResources(buttonTool62.SharedPropsInternal, "buttonTool62.SharedPropsInternal");
            buttonTool62.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.DefaultForToolType;
            buttonTool62.ForceApplyResources = "SharedPropsInternal";
            popupMenuTool11.AllowTearaway = true;
            popupMenuTool11.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Segmented;
            resources.ApplyResources(popupMenuTool11.SharedPropsInternal, "popupMenuTool11.SharedPropsInternal");
            popupMenuTool11.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool63,
            buttonTool64,
            buttonTool65,
            buttonTool66});
            popupMenuTool11.ForceApplyResources = "SharedPropsInternal";
            appearance55.Image = "IGPaint - 16x16 - Move Up.png";
            buttonTool67.SharedPropsInternal.AppearancesSmall.Appearance = appearance55;
            resources.ApplyResources(buttonTool67.SharedPropsInternal, "buttonTool67.SharedPropsInternal");
            buttonTool67.ForceApplyResources = "SharedPropsInternal";
            appearance56.Image = "IGPaint - 16x16 - Move down.png";
            buttonTool68.SharedPropsInternal.AppearancesSmall.Appearance = appearance56;
            resources.ApplyResources(buttonTool68.SharedPropsInternal, "buttonTool68.SharedPropsInternal");
            buttonTool68.ForceApplyResources = "SharedPropsInternal";
            appearance57.Image = "IGPaint - 16x16 - Move left.png";
            buttonTool69.SharedPropsInternal.AppearancesSmall.Appearance = appearance57;
            resources.ApplyResources(buttonTool69.SharedPropsInternal, "buttonTool69.SharedPropsInternal");
            buttonTool69.ForceApplyResources = "SharedPropsInternal";
            appearance58.Image = "IGPaint - 16x16 - Move right.png";
            buttonTool70.SharedPropsInternal.AppearancesSmall.Appearance = appearance58;
            resources.ApplyResources(buttonTool70.SharedPropsInternal, "buttonTool70.SharedPropsInternal");
            buttonTool70.ForceApplyResources = "SharedPropsInternal";
            popupColorPickerTool6.AllowTearaway = true;
            popupColorPickerTool6.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Standard;
            popupColorPickerTool6.SelectedColor = System.Drawing.Color.Black;
            appearance59.Image = "IGPaint - 16x16 - Color Front.png";
            popupColorPickerTool6.SharedPropsInternal.AppearancesSmall.Appearance = appearance59;
            resources.ApplyResources(popupColorPickerTool6.SharedPropsInternal, "popupColorPickerTool6.SharedPropsInternal");
            popupColorPickerTool6.ShowAutomaticColor = false;
            popupColorPickerTool6.ShowTransparentColor = true;
            popupColorPickerTool6.ForceApplyResources = "SharedPropsInternal";
            popupColorPickerTool7.AllowTearaway = true;
            popupColorPickerTool7.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Standard;
            appearance60.Image = "IGPaint - 16x16 - Color Back.png";
            popupColorPickerTool7.SharedPropsInternal.AppearancesSmall.Appearance = appearance60;
            resources.ApplyResources(popupColorPickerTool7.SharedPropsInternal, "popupColorPickerTool7.SharedPropsInternal");
            popupColorPickerTool7.ShowAutomaticColor = false;
            popupColorPickerTool7.ShowTransparentColor = true;
            popupColorPickerTool7.ForceApplyResources = "SharedPropsInternal";
            appearance61.Image = "IGPaint - 16x16 - Palette.png";
            popupMenuTool12.SharedPropsInternal.AppearancesSmall.Appearance = appearance61;
            resources.ApplyResources(popupMenuTool12.SharedPropsInternal, "popupMenuTool12.SharedPropsInternal");
            popupMenuTool12.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageOnlyOnToolbars;
            stateButtonTool68.Checked = true;
            stateButtonTool68.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool69.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool70.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool71.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool72.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            popupMenuTool12.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            stateButtonTool68,
            stateButtonTool69,
            stateButtonTool70,
            stateButtonTool71,
            stateButtonTool72});
            popupMenuTool12.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool73.Checked = true;
            stateButtonTool73.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool73.OptionSetKey = "Palette";
            resources.ApplyResources(stateButtonTool73.SharedPropsInternal, "stateButtonTool73.SharedPropsInternal");
            stateButtonTool73.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool74.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool74.OptionSetKey = "Palette";
            resources.ApplyResources(stateButtonTool74.SharedPropsInternal, "stateButtonTool74.SharedPropsInternal");
            stateButtonTool74.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool75.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool75.OptionSetKey = "Palette";
            resources.ApplyResources(stateButtonTool75.SharedPropsInternal, "stateButtonTool75.SharedPropsInternal");
            stateButtonTool75.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool76.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool76.OptionSetKey = "Palette";
            resources.ApplyResources(stateButtonTool76.SharedPropsInternal, "stateButtonTool76.SharedPropsInternal");
            stateButtonTool76.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool77.MenuDisplayStyle = Infragistics.Win.UltraWinToolbars.StateButtonMenuDisplayStyle.DisplayCheckmark;
            stateButtonTool77.OptionSetKey = "Palette";
            resources.ApplyResources(stateButtonTool77.SharedPropsInternal, "stateButtonTool77.SharedPropsInternal");
            stateButtonTool77.ForceApplyResources = "SharedPropsInternal";
            appearance62.Image = "IGPaint - 16x16 - Clear Image.png";
            buttonTool71.SharedPropsInternal.AppearancesSmall.Appearance = appearance62;
            resources.ApplyResources(buttonTool71.SharedPropsInternal, "buttonTool71.SharedPropsInternal");
            buttonTool71.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.DefaultForToolType;
            buttonTool71.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(popupMenuTool13.SharedPropsInternal, "popupMenuTool13.SharedPropsInternal");
            stateButtonTool80.InstanceProps.IsFirstInGroup = true;
            stateButtonTool83.Checked = true;
            stateButtonTool83.InstanceProps.IsFirstInGroup = true;
            stateButtonTool86.InstanceProps.IsFirstInGroup = true;
            stateButtonTool89.InstanceProps.IsFirstInGroup = true;
            stateButtonTool92.InstanceProps.IsFirstInGroup = true;
            popupMenuTool13.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            stateButtonTool78,
            stateButtonTool79,
            stateButtonTool80,
            stateButtonTool81,
            stateButtonTool82,
            stateButtonTool83,
            stateButtonTool84,
            popupMenuTool14,
            popupMenuTool15,
            stateButtonTool85,
            stateButtonTool86,
            stateButtonTool87,
            stateButtonTool88,
            stateButtonTool89,
            stateButtonTool90,
            stateButtonTool91,
            stateButtonTool92,
            stateButtonTool93,
            stateButtonTool94});
            popupMenuTool13.ForceApplyResources = "SharedPropsInternal";
            popupMenuTool16.AllowTearaway = true;
            resources.ApplyResources(popupMenuTool16.SharedPropsInternal, "popupMenuTool16.SharedPropsInternal");
            stateButtonTool96.Checked = true;
            popupMenuTool16.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            stateButtonTool95,
            stateButtonTool96,
            stateButtonTool97});
            popupMenuTool16.ForceApplyResources = "SharedPropsInternal";
            appearance63.Image = "IGPaint - 16x16 - Save 02.png";
            buttonTool72.SharedPropsInternal.AppearancesSmall.Appearance = appearance63;
            resources.ApplyResources(buttonTool72.SharedPropsInternal, "buttonTool72.SharedPropsInternal");
            buttonTool72.SharedPropsInternal.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            buttonTool72.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(fontListTool2.SharedPropsInternal, "fontListTool2.SharedPropsInternal");
            fontListTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            fontListTool2.SharedPropsInternal.Spring = true;
            fontListTool2.ForceApplyResources = "SharedPropsInternal";
            appearance64.Image = "IGPaint - 16x16 - Bold.png";
            stateButtonTool98.SharedPropsInternal.AppearancesSmall.Appearance = appearance64;
            resources.ApplyResources(stateButtonTool98.SharedPropsInternal, "stateButtonTool98.SharedPropsInternal");
            stateButtonTool98.ForceApplyResources = "SharedPropsInternal";
            appearance65.Image = "IGPaint - 16x16 - Italic.png";
            stateButtonTool99.SharedPropsInternal.AppearancesSmall.Appearance = appearance65;
            resources.ApplyResources(stateButtonTool99.SharedPropsInternal, "stateButtonTool99.SharedPropsInternal");
            stateButtonTool99.ForceApplyResources = "SharedPropsInternal";
            appearance66.Image = "IGPaint - 16x16 - Underline.png";
            stateButtonTool100.SharedPropsInternal.AppearancesSmall.Appearance = appearance66;
            resources.ApplyResources(stateButtonTool100.SharedPropsInternal, "stateButtonTool100.SharedPropsInternal");
            stateButtonTool100.ForceApplyResources = "SharedPropsInternal";
            comboBoxTool2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDown;
            resources.ApplyResources(comboBoxTool2.SharedPropsInternal, "comboBoxTool2.SharedPropsInternal");
            comboBoxTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            comboBoxTool2.SharedPropsInternal.Spring = true;
            valueListItem1.DataValue = 6F;
            valueListItem2.DataValue = 8F;
            valueListItem3.DataValue = 10F;
            valueListItem4.DataValue = 10.5F;
            valueListItem5.DataValue = 11F;
            valueListItem6.DataValue = 12F;
            valueListItem7.DataValue = 14F;
            valueListItem8.DataValue = 16F;
            valueList1.ValueListItems.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2,
            valueListItem3,
            valueListItem4,
            valueListItem5,
            valueListItem6,
            valueListItem7,
            valueListItem8});
            comboBoxTool2.ValueList = valueList1;
            comboBoxTool2.VerticalDisplayStyle = Infragistics.Win.UltraWinToolbars.VerticalDisplayStyle.Hide;
            comboBoxTool2.ForceApplyResources = "SharedPropsInternal";
            stateButtonTool101.OptionSetKey = "DrawingTools";
            appearance67.Image = "IGPaint - 16x16 - Text.png";
            stateButtonTool101.SharedPropsInternal.AppearancesSmall.Appearance = appearance67;
            resources.ApplyResources(stateButtonTool101.SharedPropsInternal, "stateButtonTool101.SharedPropsInternal");
            stateButtonTool101.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(listToolItem1, "listToolItem1");
            listTool2.ListToolItemsInternal.Add(listToolItem1);
            listTool2.MaxItemsToDisplay = 3;
            listTool2.MoreItemsText = "More Files...";
            appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisPath;
            listTool2.SharedPropsInternal.AppearancesSmall.Appearance = appearance68;
            resources.ApplyResources(listTool2.SharedPropsInternal, "listTool2.SharedPropsInternal");
            listTool2.SharedPropsInternal.MaxWidth = 20;
            listTool2.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(labelTool2.SharedPropsInternal, "labelTool2.SharedPropsInternal");
            labelTool2.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(buttonTool73.SharedPropsInternal, "buttonTool73.SharedPropsInternal");
            buttonTool73.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(popupMenuTool18.SharedPropsInternal, "popupMenuTool18.SharedPropsInternal");
            labelTool3.InstanceProps.IsFirstInGroup = true;
            listTool3.InstanceProps.IsFirstInGroup = false;
            popupMenuTool18.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            labelTool3,
            listTool3});
            popupMenuTool18.ForceApplyResources = "SharedPropsInternal";
            resources.ApplyResources(buttonTool80.SharedPropsInternal, "buttonTool80.SharedPropsInternal");
            buttonTool80.ForceApplyResources = "SharedPropsInternal";
            popupControlContainerTool2.DropDownArrowStyle = Infragistics.Win.UltraWinToolbars.DropDownArrowStyle.Standard;
            resources.ApplyResources(popupControlContainerTool2.SharedPropsInternal, "popupControlContainerTool2.SharedPropsInternal");
            popupControlContainerTool2.ForceApplyResources = "SharedPropsInternal";
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            taskPaneTool4,
            buttonTool29,
            buttonTool30,
            buttonTool31,
            buttonTool32,
            buttonTool33,
            taskPaneTool5,
            buttonTool34,
            buttonTool35,
            buttonTool36,
            buttonTool37,
            buttonTool38,
            stateButtonTool26,
            stateButtonTool27,
            stateButtonTool28,
            stateButtonTool29,
            stateButtonTool30,
            stateButtonTool31,
            stateButtonTool32,
            stateButtonTool33,
            stateButtonTool34,
            stateButtonTool35,
            stateButtonTool36,
            stateButtonTool37,
            stateButtonTool38,
            stateButtonTool39,
            stateButtonTool40,
            stateButtonTool41,
            stateButtonTool42,
            stateButtonTool43,
            stateButtonTool44,
            buttonTool39,
            popupMenuTool3,
            stateButtonTool55,
            stateButtonTool56,
            stateButtonTool57,
            stateButtonTool58,
            stateButtonTool59,
            stateButtonTool60,
            stateButtonTool61,
            stateButtonTool62,
            stateButtonTool63,
            stateButtonTool64,
            stateButtonTool65,
            popupMenuTool4,
            buttonTool40,
            buttonTool41,
            buttonTool42,
            buttonTool43,
            popupMenuTool5,
            textBoxTool1,
            buttonTool45,
            buttonTool46,
            popupMenuTool6,
            popupMenuTool7,
            stateButtonTool67,
            buttonTool53,
            popupMenuTool8,
            buttonTool62,
            popupMenuTool11,
            buttonTool67,
            buttonTool68,
            buttonTool69,
            buttonTool70,
            popupColorPickerTool6,
            popupColorPickerTool7,
            popupMenuTool12,
            stateButtonTool73,
            stateButtonTool74,
            stateButtonTool75,
            stateButtonTool76,
            stateButtonTool77,
            buttonTool71,
            popupMenuTool13,
            popupMenuTool16,
            buttonTool72,
            fontListTool2,
            stateButtonTool98,
            stateButtonTool99,
            stateButtonTool100,
            comboBoxTool2,
            stateButtonTool101,
            listTool2,
            labelTool2,
            buttonTool73,
            popupMenuTool18,
            buttonTool80,
            popupControlContainerTool2,
            labelTool4});
            this.ultraToolbarsManager1.BeforeToolDropdown += new Infragistics.Win.UltraWinToolbars.BeforeToolDropdownEventHandler(this.ultraToolbarsManager1_BeforeToolDropdown);
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
            this.ultraToolbarsManager1.ToolValueChanged += new Infragistics.Win.UltraWinToolbars.ToolEventHandler(this.ultraToolbarsManager1_ToolValueChanged);
            // 
            // frmImageEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this._frmImageEditor_Toolbars_Dock_Area_Left);
            this.Controls.Add(this._frmImageEditor_Toolbars_Dock_Area_Right);
            this.Controls.Add(this._frmImageEditor_Toolbars_Dock_Area_Bottom);
            this.Controls.Add(this.ultraStatusBar1);
            this.Controls.Add(this._frmImageEditor_Toolbars_Dock_Area_Top);
            this.KeyPreview = true;
            this.Name = "frmImageEditor";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmImageEditor_Closing);
            this.Load += new System.EventHandler(this.frmImageEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmImageEditor_KeyDown);
            this.SystemColorsChanged += new System.EventHandler(this.frmImageEditor_SystemColorsChanged);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
            this.pnlColor.ResumeLayout(false);
            this.pnlColor.PerformLayout();
            this.pnlImageAttributes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPreview)).EndInit();
            this.ultraGroupBoxPreview.ResumeLayout(false);
            this.ultraGroupBoxPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.pnlCaptureImage.ResumeLayout(false);
            this.pnlMain.ClientArea.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraStatusBar1)).EndInit();
            this.ultraStatusBar1.ResumeLayout(false);
            this.ultraStatusBar1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion // InitializeComponent

		#endregion // Windows Form Designer generated code

		#region Event handlers

            #region frmImageEditor Event Handlers

                #region frmImageEditor_Load

		private void frmImageEditor_Load(object sender, System.EventArgs e)
		{
		}

		protected override void OnLoad(EventArgs e)
		{
            //	Hook into events
            this.colorSelector1.ColorSelected += new ColorSelectedEventHandler(this.colorSelector1_ColorSelected);
            this.ultraImageEditor1.MouseVirtualPositionChanged += new MouseVirtualPositionChangedEventHandler( this.ultraImageEditor1_MouseVirtualPositionChanged );
			
            //	Initialize the current drawing tool
            this.ultraImageEditor1.EditMode = ImageEditMode.DrawPoint;

            // initialize the image editor font based on the control font
            this.GetImageEditorFont( this.ultraImageEditor1.DrawingFont );

            //	Initialize the current color panels
            this.colorSelector1_ColorSelected( this, new ColorSelectedEventArgs( Color.Black, ImageEditColorType.ForeColor ) );
            this.colorSelector1_ColorSelected( this, new ColorSelectedEventArgs( Color.White, ImageEditColorType.BackColor ) );

            // Initialize System color sensitive color value for TaskPane GroupBox border
            this.ultraGroupBox1.ContentAreaAppearance.BorderColor =  this.ultraGroupBoxPreview.ContentAreaAppearance.BorderColor =
                Infragistics.Win.Office2003Colors.FloatingToolbarCaption;

            //	Show the pixel grid by default
            StateButtonTool displayImageGridTool = this.ultraToolbarsManager1.Tools["DisplayImageGrid"] as StateButtonTool;
            displayImageGridTool.Checked = this.ultraImageEditor1.DrawPixelGrid = true;

            //	Make the brush size large by default
            ((Infragistics.Win.UltraWinToolbars.StateButtonTool)this.ultraToolbarsManager1.Tools["LargeBrush"]).Checked = true;

            // Load the default image from the image theme
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("IGPaint.Images.IGPaint - DefaultImage.png"))
            {
                this.ultraImageEditor1.SetImage(Image.FromStream(stream));
                stream.Close();
            }
            this.ClearUndoStack();
           
            // Initialize Undo/Redo enabled state
            this.ultraToolbarsManager1.Tools["RedoTool"].SharedProps.Enabled = this.ultraImageEditor1.CanRedo;
            this.ultraToolbarsManager1.Tools["UndoTool"].SharedProps.Enabled = this.ultraImageEditor1.CanUndo;
			
            //	Update the image dimensions panel display
            Size imageSize = this.ultraImageEditor1.ImageSize;
            string imageSizeText = String.Format( "{0} x {1} " + Properties.Resources.Pixels, imageSize.Width, imageSize.Height );
            this.ultraStatusBar1.Panels["dimensionsPanel"].Text = imageSizeText;

            // Update TaskPane Image Attributes
            this.UpdateImageAttributes();

            // Display default file name in form
            this.DisplayFileName( Properties.Resources.Untitled );

            // Update the trackbar to display the proper magnification level
            this.trackBarZoom.Value = (int)Math.Floor(this.ultraImageEditor1.MagnificationLevel);
            this.trackBarZoom.MaxValue = 15;
            // Show the StatusBar by default
            ((Infragistics.Win.UltraWinToolbars.StateButtonTool)this.ultraToolbarsManager1.Tools["StatusBar"]).Checked = true;

            //	Get previously saved values from the registry, if any
            this.LoadSettingsFromRegistry();

            // Get previously saved file names from xml, if any
            this.LoadFileNamesFromXml();

            // Update Image File MRU list
            this.UpdateRecentImageFiles();

            // Populate the About tool.
            Control control = new AboutControl();
            control.Parent = this;
            ((PopupControlContainerTool)this.ultraToolbarsManager1.Tools["About"]).Control = control;

			base.OnLoad(e);
		}

			    #endregion // frmImageEditor_Load

                #region frmImageEditor_Closing

        private void frmImageEditor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //	Only do this if the image has actually been modified
            if ( this.ultraImageEditor1.Modified )
            {
                string message = null;
                string caption = Properties.Resources.ImageEditor;

                if ( this.fileName != String.Empty )
                {
                    string fileName = this.GetFileName(  this.fileName );
                    message = String.Format(Properties.Resources.SaveChanges, fileName);
                }
                else
                    message = String.Format(Properties.Resources.SaveChanges, Properties.Resources.Untitled);

                DialogResult result = MessageBox.Show( this,
                    message,
                    caption,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question );


                this.canceled = false;

                switch ( result )
                {
                    case DialogResult.Yes:{ this.FileSaveAction(); break; }
                    case DialogResult.No:{ this.canceled = true; break; }
                    case DialogResult.Cancel:{ e.Cancel = true; break; }
                }
            }
            else
                this.canceled = true;

            // save settings to registry
            this.SaveSettingsToRegistry();

            // save MRU list file names to xml
            this.SaveFileNamesToXml();
        }

			    #endregion // frmImageEditor_Closing

                #region frmImageEditor_KeyDown

        private void frmImageEditor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = this.ProcessShortcutKey( e.KeyData );
        }

			    #endregion // frmImageEditor_KeyDown

                #region frmImageEditor_SystemColorsChanged

        private void frmImageEditor_SystemColorsChanged(object sender, System.EventArgs e)
        {
            // Synchronize System color sensitive color value for TaskPane GroupBox borders
            Color color = Infragistics.Win.Office2013ColorTable.Colors.FloatingToolbarCaption;

            this.ultraGroupBox1.ContentAreaAppearance.BorderColor           = color;
            this.ultraGroupBoxPreview.ContentAreaAppearance.BorderColor   = color;              
        }

                #endregion // frmImageEditor_SystemColorsChanged

            #endregion // frmImageEditor Event Handlers

            #region Panel/GroupBox Event Handlers

                #region pnlColor_Resize

        private void pnlColor_Resize(object sender, System.EventArgs e)
        {
            int height = this.pnlColor.Height - (SELECTED_COLOR_INDICATOR_HEIGHT + (SELECTED_COLOR_INDICATOR_PADDING * 2));
            this.colorSelector1.Height = height;

            Rectangle rect = new Rectangle(	(this.pnlColor.Bounds.Width / 2) - (SELECTED_COLOR_INDICATOR_HEIGHT / 2),
                this.colorSelector1.Bottom + SELECTED_COLOR_INDICATOR_PADDING,
                SELECTED_COLOR_INDICATOR_HEIGHT,
                SELECTED_COLOR_INDICATOR_HEIGHT );

            this.selectedColorIndicator1.Bounds = rect;
        }

			    #endregion // pnlColor_Resize

                #region ultraGroupBoxPreview_Resize

        private void ultraGroupBoxPreview_Resize(object sender, System.EventArgs e)
        {
            this.CenterPreview();
        }

            #endregion // ultraGroupBoxPreview_Resize

            #endregion // Panel/GroupBox Event Handlers

            #region LinkLabel Event Handlers

                #region linkLabelSize_LinkClicked

        private void linkLabelSize_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            this.SetImageSize();
        }

                #endregion // linkLabelSize_LinkClicked

            #endregion // LinkLabel Event Handlers

            #region ColorSelector/SelectedColorIndicator Event Handlers

                #region colorSelector1_ColorSelected

        private void colorSelector1_ColorSelected(object sender, ColorSelectedEventArgs e)
        {
            this.SynchronizeColors( e.Color, e.Type );
        }

			    #endregion // colorSelector1_ColorSelected

                #region selectedColorIndicator1_SelectedForeColorChanged

        private void selectedColorIndicator1_SelectedForeColorChanged(object sender, System.EventArgs e)
        {
            this.colorSelector1.SelectedForeColor = this.selectedColorIndicator1.SelectedForeColor;
        }

                #endregion selectedColorIndicator1_SelectedForeColorChanged

                #region selectedColorIndicator1_SelectedBackColorChanged

        private void selectedColorIndicator1_SelectedBackColorChanged(object sender, System.EventArgs e)
        {
            this.colorSelector1.SelectedBackColor = this.selectedColorIndicator1.SelectedBackColor;
        }

                #endregion selectedColorIndicator1_SelectedBackColorChanged

            #endregion // ColorSelector/SelectedColorIndicator Event Handlers

            #region ultraImageEditor1 Event Handlers

                #region ultraImageEditor1_MouseVirtualPositionChanged

        private void ultraImageEditor1_MouseVirtualPositionChanged(object sender, MouseVirtualPositionChangedEventArgs e)
        {
            this.ultraStatusBar1.Panels["positionPanel"].Text = String.Format( "{0}, {1}", e.X, e.Y );
        }

			    #endregion // ultraImageEditor1_MouseVirtualPositionChanged

                #region ultraImageEditor1_ColorSelectColorChanged

        private void ultraImageEditor1_ColorSelectColorChanged(object sender, ColorSelectedEventArgs e)
        {
            this.SynchronizeUI();
        }

			    #endregion // ultraImageEditor1_ColorSelectColorChanged

                #region ultraImageEditor1_AfterImageEdited

        private void ultraImageEditor1_AfterImageEdited(object sender, AfterImageEditedEventArgs e)
        {
            //	Update the dialog controls that reflect property values of the
            //	UltraImageEditor whenever anything about the image changes
            this.SynchronizeUI();
        }

			    #endregion // ultraImageEditor1_AfterImageEdited

            #endregion // ultraImageEditor1 Event Handlers

            #region ultraToolbarsManager1 EventHandlers

                #region ultraToolbarsManager1_ToolClick

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch ( e.Tool.Key )
            {

                #region File

                // file actions
                case "New":{        this.FileNewAction(); break; }
                case "Open":{       this.FileOpenAction(); break; }
                case "Save":{       this.FileSaveAction(); break; }
                case "SaveAs":{     this.FileSaveAsAction(); break; }
                case "Close":{      this.Close();  return; }
                case "MRUFiles":
                {
                    this.openFileDlg.FileName = e.ListToolItem.Text;
                    this.FileOpenAction( true ); break;
                }

                #endregion // File

                #region Edit

                // undo/redo
                case "UndoTool":{ this.ultraImageEditor1.Undo(); break; }
                case "RedoTool":{ this.ultraImageEditor1.Redo(); break; }

                // cut/copy/paste
                case "CutTool":{ this.ultraImageEditor1.CutToClipboard(); break; }
                case "CopyTool":{ this.ultraImageEditor1.CopyToClipboard(); break; }
                case "PasteTool":{ this.ultraImageEditor1.PasteFromClipboard(); break; }

                #endregion // Edit

                #region View

                // grid
                case "DisplayImageGrid":{ this.ultraImageEditor1.DrawPixelGrid = ((StateButtonTool)e.Tool).Checked; break; }
			
                // magnification
                case "ZoomInTool": { this.Zoom(false, this.ultraImageEditor1.MagnificationLevel + 1); break; }
                case "ZoomOutTool": { this.Zoom(false, this.ultraImageEditor1.MagnificationLevel - 1); break; }
                case "ZoomFitTool":{ this.Zoom( true, 0 ); break; }

                // statusbar
                case "StatusBar":{ this.ultraStatusBar1.Visible = ((StateButtonTool)e.Tool).Checked; break; }

                #endregion // View

                #region Image

                // spatial manipulation
                case "FlipVertical":{       this.ultraImageEditor1.FlipVertically(); break; }
                case "FlipHorizontal":{     this.ultraImageEditor1.FlipHorizontally(); break; }
                case "RotateLeftTool":{     this.ultraImageEditor1.RotateLeft(); break; }
                case "RotateRightTool":{    this.ultraImageEditor1.RotateRight(); break; }
                case "MoveUp":{             this.ultraImageEditor1.OffsetImage(0, -1, false); break; }
                case "MoveDown":{           this.ultraImageEditor1.OffsetImage(0, 1, false); break; }
                case "MoveLeft":{           this.ultraImageEditor1.OffsetImage(-1, 0, false); break; }
                case "MoveRight":{          this.ultraImageEditor1.OffsetImage(1, 0, false); break; }

                // palette selection
                case "Standard":{           this.UseDefaultPalette(); break; }
                case "GrayScale":{          this.UseGrayScalePalette(); break; }
                case "WebSafeColors":{      this.UseWebSafePalette(); break; }
                case "HSLColors":{          this.UseHSLPalette(); break; }
                case "16Colors":{           this.UseBasicPalette(); break; }
                case "InvertColorsTool":{   this.ultraImageEditor1.InvertColors(); break; }
                    
                // image 
                case "CaptureImage":{       this.CaptureImage(); break; }                  
                case "ClearImage":{         this.ultraImageEditor1.ClearImage( Color.Transparent ); break; }
                case "ImageSize":{          this.SetImageSize(); break; }                    

                #endregion // Image

                #region Tools

                // select tools
                case "SelectRectangle":	
                case "ColorSelect":	

                // erase/fill tools
                case "Erase":	
                case "EraseColor":	
                case "Fill":	

                // draw tools
                case "DrawBrush":
                case "DrawAirbrush":
                case "DrawPoint":	
                case "DrawLine":
                case "DrawArc":
                case "DrawText":
                case "DrawRectangleOutline":	
                case "DrawFilledRectangle":	
                case "DrawFilledRectangleWithOutline":
                case "DrawEllipseOutline":	
                case "DrawFilledEllipse":	
                case "DrawFilledEllipseWithOutline":
                {
                    ImageEditMode editMode = this.GetImageEditMode( e.Tool.Key );
                    if ( editMode != this.ultraImageEditor1.EditMode )
                        this.ultraImageEditor1.EditMode = editMode;

                    this.OnEditModeChanged();
                    break;
                }

                // gradient styles
                case "GradientStyle_None":
                case "GradientStyle_Vertical":
                case "GradientStyle_Horizontal":
                case "GradientStyle_BackwardDiagonal":
                case "GradientStyle_ForwardDiagonal":
                case "GradientStyle_HorizontalBump":
                case "GradientStyle_VerticalBump":
                case "GradientStyle_Circular":
                case "GradientStyle_Rectangular":
                case "GradientStyle_Elliptical":{ this.SetDrawingGradientStyle( e.Tool ); return; }

                // brush size
                case "SmallBrush":
                case "MediumBrush":
                case "LargeBrush":{ this.SetBrushSize( e.Tool.Key ); break; }

                #endregion // Tools

                #region Font

                case "Bold":
                case "Italic":
                case "Underlined":{ this.SetImageEditorFont(); break; }

				#endregion //Font

				#region Color

				case "Color":
				{
					this.ultraToolbarsManager1.Toolbars["Color"].Visible = !this.ultraToolbarsManager1.Toolbars["Color"].Visible;
					break;
				}

                #endregion // Color

                #region Image Attributes

                case "Properties":
                {
                    this.ultraToolbarsManager1.Toolbars["TaskPaneToolbar"].Visible = true;
                    break;
                }

                #endregion // Image Attributes

            }

            this.UpdateStatusBar( e.Tool.SharedProps.Caption );
        }

		        #endregion // ultraToolbarsManager1_ToolClick

                #region ultraToolbarsManager1_ToolValueChanged

        private void ultraToolbarsManager1_ToolValueChanged(object sender, Infragistics.Win.UltraWinToolbars.ToolEventArgs e)
        {
            switch ( e.Tool.Key )
            {
                case "FontName":
                case "Font Size":{ this.SetImageEditorFont(); break; }

                case "ForeColor":
                {
                    PopupColorPickerTool colorTool = e.Tool as PopupColorPickerTool;
                    this.SynchronizeColors( colorTool.SelectedColor, ImageEditColorType.ForeColor );
                    break;
                }
                case "BackColor":
                {
                    PopupColorPickerTool colorTool = e.Tool as PopupColorPickerTool;
                    this.SynchronizeColors( colorTool.SelectedColor, ImageEditColorType.BackColor );
                    break;
                }
            }
        }

                #endregion // ultraToolbarsManager1_ToolValueChanged

                #region ultraToolbarsManager1_BeforeToolDropdown

        private void ultraToolbarsManager1_BeforeToolDropdown(object sender, Infragistics.Win.UltraWinToolbars.BeforeToolDropdownEventArgs e)
        {
            switch ( e.Tool.Key )
            {
                case "File":
                {
                    this.ultraToolbarsManager1.Tools["SaveAs"].SharedProps.Enabled = this.fileName == String.Empty ? false : true;
                    break;
                }

                case "ForeColor":                
                case "BackColor":
                case "GradientStyle":
                {
                    this.ultraStatusBar1.Panels["currentToolPanel"].Text = e.Tool.SharedProps.Caption;
                    break;
                }
            }
        }

                #endregion // ultraToolbarsManager1_BeforeToolDropdown

            #endregion // ultraToolbarsManager1 EventHandlers

            #region OnOffice2013ColorSchemeChanged
        private void OnOffice2013ColorSchemeChanged(object sender, EventArgs e)
		{
			this.pnlMain.Appearance.BackColor = Office2013ColorTable.Colors.RibbonTabAreaBackColorGradientLight;
		}
            #endregion //OnOffice2013ColorSchemeChanged

            #region trackBarZoom_ValueChanged

        /// <summary>
        /// Handles the ValueChanged event of the trackBarZoom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            this.Zoom(false, this.trackBarZoom.Value);
        }

            #endregion //trackBarZoom_ValueChanged

            #region ultraStatusBar1_ButtonClick

        private void ultraStatusBar1_ButtonClick(object sender, Infragistics.Win.UltraWinStatusBar.PanelEventArgs e)
        {
            this.SetImageSize();
            this.ultraStatusBar1.Refresh();
        }

            #endregion //ultraStatusBar1_ButtonClick

        #endregion // Event handlers

        #region Methods

            #region Load/Save Settings Functions

                #region LoadSettingsFromRegistry

        // LoadSettingsFromRegistry.  Loads values from the registry (as saved at the end of the previous dialog session)
        // for the dialog location and size, current editing tool, drawing ForeColor/BackColor, and brush size.
        private void LoadSettingsFromRegistry()
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.CreateSubKey( REG_KEY );
                if ( regKey != null )
                {
                    this.Location	= new Point( Convert.ToInt32(regKey.GetValue("DialogX", this.Location.X)),
                        Convert.ToInt32(regKey.GetValue("DialogY", this.Location.Y)) );

                    this.Size		= new Size( Convert.ToInt32(regKey.GetValue("DialogWidth", 576)),
                        Convert.ToInt32(regKey.GetValue("DialogHeight", 376)) );

                    int defaultTool = (int)ImageEditMode.DrawPoint;
                    int currentTool = Convert.ToInt32( regKey.GetValue("CurrentTool", defaultTool) );

                    this.ultraImageEditor1.EditMode = (ImageEditMode)currentTool;
                    this.SelectToolBasedOnEditMode( this.ultraImageEditor1.EditMode );

                    int foreColor = Convert.ToInt32( regKey.GetValue("ForeColor", Color.Black.ToArgb()) );
                    int backColor = Convert.ToInt32( regKey.GetValue("BackColor", Color.White.ToArgb()) );
                    this.ultraImageEditor1.DrawingForeColor = Color.FromArgb( foreColor );
                    this.ultraImageEditor1.DrawingBackColor = Color.FromArgb( backColor );

                    ((Infragistics.Win.UltraWinToolbars.PopupColorPickerTool)this.ultraToolbarsManager1.Tools["ForeColor"]).SelectedColor = this.ultraImageEditor1.DrawingForeColor;
                    ((Infragistics.Win.UltraWinToolbars.PopupColorPickerTool)this.ultraToolbarsManager1.Tools["BackColor"]).SelectedColor = this.ultraImageEditor1.DrawingBackColor;

                    //	Push the appropriate 'brush size' button
                    int brushSize = Convert.ToInt32( regKey.GetValue("BrushSize", BRUSH_SIZE_LARGE) );
                    switch( brushSize )
                    {
                        case BRUSH_SIZE_SMALL:
                            ((Infragistics.Win.UltraWinToolbars.StateButtonTool)this.ultraToolbarsManager1.Tools["SmallBrush"]).Checked = true;
                            break;

                        case BRUSH_SIZE_MEDIUM:
                            ((Infragistics.Win.UltraWinToolbars.StateButtonTool)this.ultraToolbarsManager1.Tools["MediumBrush"]).Checked = true;
                            break;

                        case BRUSH_SIZE_LARGE:
                            ((Infragistics.Win.UltraWinToolbars.StateButtonTool)this.ultraToolbarsManager1.Tools["LargeBrush"]).Checked = true;
                            break;

                        default:
                        {
                            Debug.Assert( false, "Unrecognized brush size retrieved from registry settings - possible registry corruption" );
                            ((Infragistics.Win.UltraWinToolbars.StateButtonTool)this.ultraToolbarsManager1.Tools["LargeBrush"]).Checked = true;
                        }
                            break;
                    }

                    string fontName     = Convert.ToString( regKey.GetValue("FontName", this.ultraImageEditor1.DrawingFont.Name) );
                    float fontSize      = Convert.ToSingle( regKey.GetValue("FontSize", this.ultraImageEditor1.DrawingFont.Size) );
                    FontStyle fontStyle = (FontStyle)( Convert.ToInt32(regKey.GetValue("FontStyle", this.ultraImageEditor1.DrawingFont.Style)) );

                    try
                    {
                        this.ultraImageEditor1.DrawingFont = new Font( fontName, fontSize, fontStyle );
                    }
                    catch{}

                    this.SynchronizeUI();
                }
            }
            catch {}
        }

			    #endregion // LoadSettingsFromRegistry

			    #region SaveSettingsToRegistry

        // Saves values to the registry for the dialog location and size,
        // current editing tool, drawing ForeColor/BackColor, and brush size.
        private void SaveSettingsToRegistry()
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey( REG_KEY, true );
                if (regKey != null)
                {
                    regKey.SetValue( "DialogX", this.Location.X );
                    regKey.SetValue( "DialogY", this.Location.Y );

                    regKey.SetValue( "DialogWidth", this.Size.Width );
                    regKey.SetValue( "DialogHeight", this.Size.Height );

                    regKey.SetValue( "CurrentTool", (int)(this.ultraImageEditor1.EditMode) );

                    regKey.SetValue( "ForeColor", this.ultraImageEditor1.DrawingForeColor.ToArgb() );
                    regKey.SetValue( "BackColor", this.ultraImageEditor1.DrawingBackColor.ToArgb() );

                    regKey.SetValue( "BrushSize", this.ultraImageEditor1.BrushSize );

                    regKey.SetValue( "FontName", this.ultraImageEditor1.DrawingFont.Name );
                    regKey.SetValue( "FontSize", this.ultraImageEditor1.DrawingFont.Size );

                    int fontStyle = 0;
                    if ( (this.ultraImageEditor1.DrawingFont.Style & FontStyle.Bold) == FontStyle.Bold )
                        fontStyle |= (int)FontStyle.Bold;

                    if ( (this.ultraImageEditor1.DrawingFont.Style & FontStyle.Italic) == FontStyle.Italic )
                        fontStyle |= (int)FontStyle.Italic;

                    if ( (this.ultraImageEditor1.DrawingFont.Style & FontStyle.Underline) == FontStyle.Underline )
                        fontStyle |= (int)FontStyle.Underline;

                    if ( (this.ultraImageEditor1.DrawingFont.Style & FontStyle.Strikeout) == FontStyle.Strikeout )
                        fontStyle |= (int)FontStyle.Strikeout;

                    regKey.SetValue( "FontStyle", fontStyle );
                }
            }
            catch {}
        }

			    #endregion // SaveSettingsToRegistry

                #region LoadFileNamesFromXml

        private void LoadFileNamesFromXml()
        {
            this.mruImageFiles.Clear();
            
            DataSet ds = new DataSet();
            
            try
            {
                if ( File.Exists( Application.StartupPath + "\\" + RECENT_IMAGE_FILENAMES ) )
                    ds.ReadXml( Application.StartupPath + "\\" + RECENT_IMAGE_FILENAMES, XmlReadMode.InferSchema );
                else
                    return;
            }
            catch{}

            if ( ds.Tables.Count > 0 && ds.Tables[0].TableName == "FilePaths" )
            {
                DataTable dt = ds.Tables["FilePaths"];

                foreach( DataRow row in dt.Rows )
                    this.AddUniqueFileName( (string)row["FilePath"] );
            }
        }

                #endregion // LoadFileNamesFromXml

                #region SaveFileNamesToXml

        private void SaveFileNamesToXml()
        {
            if ( this.mruImageFiles.Count == 0 )
                return;

            DataSet ds = new DataSet();
            DataTable dt = ds.Tables.Add("FilePaths");
            dt.Columns.Add( "FilePath", typeof(string) );

            if ( this.fileName != String.Empty )
                this.AddUniqueFileName( this.fileName );
                
            object[] fileStrings = this.mruImageFiles.ToArray();
            foreach ( object obj in fileStrings )
            {
                string filePath = obj as string;
                dt.Rows.Add( new object[]{ filePath } );
            }

            try
            {                
                ds.WriteXml( Application.StartupPath + "\\" + RECENT_IMAGE_FILENAMES ); 
            }
            catch{}
        }

                #endregion // SaveFileNamesToXml

            #endregion // Load/Save Settings Functions

            #region File Functions

                #region FileNewAction

        private void FileNewAction( )
        {
            using ( frmImageDimensions dlg = new frmImageDimensions(this.ultraImageEditor1.ImageSize) )
            {
                dlg.MaximumDimensions = new Size( MAX_IMAGE_WIDTH, MAX_IMAGE_HEIGHT );
                dlg.StartPosition = FormStartPosition.CenterParent;
                if ( dlg.ShowDialog(this) == DialogResult.OK )
                {
                    // clear the image and change the dimensions
                    this.ultraImageEditor1.ClearImage( Color.Transparent );
                    this.ultraImageEditor1.SetImageDimensions( dlg.Dimensions.Width, dlg.Dimensions.Height, false );

                    // reset the state since we're starting with a clean image
                    this.ClearUndoStack();

                    // update mru image files
                    this.UpdateRecentImageFiles();

                    // reset file name
                    this.fileName = String.Empty;

                    // display default file name in form
                    this.DisplayFileName( Properties.Resources.Untitled );
                }
            }
        }

			    #endregion // FileNewAction

                #region FileOpenAction

        private void FileOpenAction(bool suppressOpenFileDialog = false )
        {
            if ( suppressOpenFileDialog || 
                this.openFileDlg.ShowDialog(this) == DialogResult.OK )
            {
                try
                {
                    Image newImage = null;
                    Image original = Image.FromFile( this.openFileDlg.FileName );

                    // if image is less than max dimensions (100 x 100 px) use it
                    if ( (original.Size.Width <= MAX_IMAGE_WIDTH) && (original.Size.Height <= MAX_IMAGE_HEIGHT) )
                    {
                        newImage = original;
                    }
                    else
                    {
                        this.ultraImageEditor1.SetImageDimensions( MAX_IMAGE_WIDTH, MAX_IMAGE_HEIGHT );
                        // scale or clip original image to current image editor dimensions
                        ScaleClipDialog scaleClipDialog = new ScaleClipDialog( original, this.ultraImageEditor1.ImageSize );
                        newImage = scaleClipDialog.NewImage;
                    }

                    if ( newImage != null )
                    {
                        // set image
                        this.ultraImageEditor1.SetImage( newImage );

                        // update MRU list of image files
                        this.UpdateRecentImageFiles();

                        // save the file string
                        this.fileName = this.openFileDlg.FileName;

                        // display file name in form
                        this.DisplayFileName( this.fileName );

                        // clean up resources
                        if ( newImage != original )
                            newImage.Dispose();
                     }

                    original.Dispose();

                    // reset state since we're starting with a clean image
                    this.ClearUndoStack();
                }
                catch
                {
                    MessageBox.Show(
                        this,
                        Properties.Resources.InvalidImageError, 
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation );
                }
            }
        }

			    #endregion // FileOpenAction

                #region FileSaveAction

        private void FileSaveAction()
        {
            try
            {
                // save the image
                using ( Image img = this.ultraImageEditor1.GetImage() )
                {
                    if ( this.fileName != String.Empty )
                        img.Save( this.fileName );
                    else
                        this.FileSaveAsAction();
                }
            }
            catch
            {
                MessageBox.Show(
                    this,
                    String.Format( "Unable to save the file to the specified location - '{0}'.", this.saveFileDlg.FileName), 
                    "Error encountered",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation );
            }
        }

                #endregion // FileSaveAction

                #region FileSaveAsAction

        private void FileSaveAsAction( )
        {
            if ( this.saveFileDlg.ShowDialog(this) == DialogResult.OK )
            {
                try
                {
                    // save the image
                    using ( Image img = this.ultraImageEditor1.GetImage() )
                        img.Save( this.saveFileDlg.FileName );

                    this.UpdateRecentImageFiles();

                    // save the file string
                    this.fileName = this.saveFileDlg.FileName;

                    // display file name in form
                    this.DisplayFileName( fileName );
                }
                catch
                {
                    MessageBox.Show(
                        this,
                        String.Format("Unable to save the file to the specified location - '{0}'.", this.saveFileDlg.FileName), 
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation );
                }
            }
        }

			    #endregion // FileSaveAsAction

                #region DisplayFileName

        private void DisplayFileName( string fileName )
        {
            string file = this.GetFileName( fileName );
            if ( file != String.Empty )
                this.Text = String.Format( Properties.Resources.IGPaint + " - [{0}]", file );
            else
                this.Text = Properties.Resources.IGPaint;
        }

                #endregion // DisplayFileName

                #region GetFileName

        private string GetFileName( string fileName )
        {
            try
            {
                return Path.GetFileName( fileName );
            }
            catch{ return String.Empty; }
        }

                #endregion // GetFileName

            #endregion // File Functions

			#region ProcessShortcutKey

		/// <summary>
		/// Simulates shortcut key functionality for the toolbar.
		/// </summary>
		/// <param name="keyData">The key data to process.</param>
		/// <returns>A boolean indicating whether the keystroke was handled.</returns>
		private bool ProcessShortcutKey( Keys keyData )
		{
			#region SelectAll

			//	Ctrl+A
			if ( keyData == (Keys.A | Keys.Control) )
			{
				this.ultraImageEditor1.OffsetImage( 0, 0, false );
				return true;
			}

			#endregion // SelectAll

			#region ZoomIn

			//	Ctrl++
			if ( keyData == (Keys.Oemplus | Keys.Control) )
			{
                this.Zoom(false, this.ultraImageEditor1.MagnificationLevel + 1);
				return true;
			}

			#endregion // ZoomIn

			#region ZoomOut

			//	Ctrl+-
			if ( keyData == (Keys.OemMinus | Keys.Control) )
            {
                this.Zoom(false, this.ultraImageEditor1.MagnificationLevel - 1);
				return true;
			}

			#endregion // ZoomOut

			return false;
        }

			#endregion // ProcessShortcutKey

            #region SelectToolBasedOnEditMode

        private void SelectToolBasedOnEditMode( ImageEditMode imageEditMode )
        {
            ((Infragistics.Win.UltraWinToolbars.StateButtonTool)this.ultraToolbarsManager1.Tools[this.GetToolKeyFromEditMode(imageEditMode)]).Checked = true;
        }

			#endregion // SelectToolBasedOnEditMode

            #region GetToolFromImageEditMode

        // Returns the Tool that corresponds to the specified ImageEditMode
        // constant, or null if no correlation exists.
        private ToolBase GetToolFromImageEditMode( ImageEditMode imageEditMode )
        {
            switch( imageEditMode )
            {
                case ImageEditMode.ColorSelect:{ return this.ultraToolbarsManager1.Tools["ColorSelect"]; }
                case ImageEditMode.DrawAirbrush:{ return this.ultraToolbarsManager1.Tools["DrawAirbrush"];  }
                case ImageEditMode.DrawArc:{ return this.ultraToolbarsManager1.Tools["DrawArc"]; }
                case ImageEditMode.DrawBrush:{ return this.ultraToolbarsManager1.Tools["DrawBrush"]; }
                case ImageEditMode.DrawEllipseOutline:{ return this.ultraToolbarsManager1.Tools["DrawEllipseOutline"]; }
                case ImageEditMode.DrawFilledEllipse:{ return this.ultraToolbarsManager1.Tools["DrawFilledEllipse"]; }
                case ImageEditMode.DrawFilledEllipseWithOutline:{ return this.ultraToolbarsManager1.Tools["DrawFilledEllipseWithOutline"]; }
                case ImageEditMode.DrawFilledRectangle:{ return this.ultraToolbarsManager1.Tools["DrawFilledRectangle"];  }
                case ImageEditMode.DrawFilledRectangleWithOutline:{ return this.ultraToolbarsManager1.Tools["DrawFilledRectangleWithOutline"]; }
                case ImageEditMode.DrawLine:{ return this.ultraToolbarsManager1.Tools["DrawLine"]; }
                case ImageEditMode.DrawPoint:{ return this.ultraToolbarsManager1.Tools["DrawPoint"]; }
                case ImageEditMode.DrawRectangleOutline:{  return this.ultraToolbarsManager1.Tools["DrawRectangleOutline"];  }
                case ImageEditMode.DrawText:{ return this.ultraToolbarsManager1.Tools["DrawText"]; }
                case ImageEditMode.Erase:{ return this.ultraToolbarsManager1.Tools["Erase"]; }
                case ImageEditMode.EraseColor:{ return this.ultraToolbarsManager1.Tools["EraseColor"]; }
                case ImageEditMode.Fill:{ return this.ultraToolbarsManager1.Tools["Fill"]; }
                case ImageEditMode.SelectRectangle:{ return this.ultraToolbarsManager1.Tools["SelectRectangle"]; }
                default:{ return null;  }
            }
        }

			#endregion // GetToolFromImageEditMode

            #region GetToolKeyFromEditMode

        private string GetToolKeyFromEditMode( ImageEditMode imageEditMode )
        {
            switch ( imageEditMode )
            {
                case ImageEditMode.ColorSelect:
                    return "ColorSelect";
                case ImageEditMode.DrawAirbrush:
                    return "DrawAirbrush";
                case ImageEditMode.DrawArc:
                    return "DrawArc";
                case ImageEditMode.DrawBrush:
                    return "DrawBrush";
                case ImageEditMode.DrawEllipseOutline:
                    return "DrawEllipseOutline";
                case ImageEditMode.DrawFilledEllipse:
                    return "DrawFilledEllipse";
                case ImageEditMode.DrawFilledEllipseWithOutline:
                    return "DrawFilledEllipseWithOutline";
                case ImageEditMode.DrawFilledRectangle:
                    return "DrawFilledRectangle";
                case ImageEditMode.DrawFilledRectangleWithOutline:
                    return "DrawFilledRectangleWithOutline";
                case ImageEditMode.DrawLine:
                    return "DrawLine";
                case ImageEditMode.DrawRectangleOutline:
                    return "DrawRectangleOutline";
                case ImageEditMode.DrawText:
                    return "DrawText";
                case ImageEditMode.Erase:
                    return "Erase";
                case ImageEditMode.EraseColor:
                    return "EraseColor";
                case ImageEditMode.Fill:
                    return "Fill";
                case ImageEditMode.SelectRectangle:
                    return "SelectRectangle";
                default:
                    return "DrawPoint";
            }
        }

			#endregion // GetToolKeyFromEditMode

            #region GetImageEditMode

        private ImageEditMode GetImageEditMode( string editModeKey )
        {
            switch( editModeKey )
            {
                case "SelectRectangle":
                    return ImageEditMode.SelectRectangle;
                case "ColorSelect":
                    return ImageEditMode.ColorSelect;
	
                case "Erase":
                    return ImageEditMode.Erase;
                case "EraseColor":
                    return ImageEditMode.EraseColor;
                case "Fill":
                    return ImageEditMode.Fill;

                case "DrawBrush":
                    return ImageEditMode.DrawBrush;
                case "DrawAirbrush":
                    return ImageEditMode.DrawAirbrush;
                case "DrawPoint":
                    return ImageEditMode.DrawPoint;
                case "DrawLine":
                    return ImageEditMode.DrawLine;
                case "DrawArc":
                    return ImageEditMode.DrawArc;
                case "DrawText":
                    return ImageEditMode.DrawText;

                case "DrawRectangleOutline":
                    return ImageEditMode.DrawRectangleOutline;
                case "DrawFilledRectangle":
                    return ImageEditMode.DrawFilledRectangle;
                case "DrawFilledRectangleWithOutline":
                    return ImageEditMode.DrawFilledRectangleWithOutline;

                case "DrawEllipseOutline":
                    return ImageEditMode.DrawEllipseOutline;
                case "DrawFilledEllipse":
                    return ImageEditMode.DrawFilledEllipse;
                case "DrawFilledEllipseWithOutline":
                    return ImageEditMode.DrawFilledEllipseWithOutline;

                default:
                    Debug.Fail(string.Format("Unexpected ImageEditMode Tool Key '{0}'", editModeKey));
                    return ImageEditMode.DrawPoint;
            }
        }

		#endregion //GetImageEditMode

            #region OnEditModeChanged

        private void OnEditModeChanged()
        {
            // show the text contextual tab when in text mode
            bool isDrawText = this.ultraImageEditor1.EditMode == ImageEditMode.DrawText;

            ContextualTabGroup tabGroup = this.ultraToolbarsManager1.Ribbon.ContextualTabGroups["Text Tools"];
            tabGroup.Visible = isDrawText;

            if (isDrawText)
                this.ultraToolbarsManager1.Ribbon.SelectedTab = tabGroup.Tabs[0];
        }

		    #endregion // OnEditModeChanged

            #region SynchronizeUI

        // Updates the UI controls that reflect properties of the UltraImageEditor
        private void SynchronizeUI()
        {
            if ( this.isSynchronizingUI )
                return;

            try
            {
                this.isSynchronizingUI = true;

                // update fore and backcolor
                this.SynchronizeColors( this.ultraImageEditor1.DrawingForeColor, ImageEditColorType.ForeColor );
                this.SynchronizeColors( this.ultraImageEditor1.DrawingBackColor, ImageEditColorType.BackColor );

                // update tooltips
                ToolBase redoTool = this.ultraToolbarsManager1.Tools["RedoTool"];
                bool redoEnabled = redoTool.SharedProps.Enabled = this.ultraImageEditor1.CanRedo;
                redoTool.SharedProps.ToolTipText = redoEnabled ? Properties.Resources.RedoLastAction : Properties.Resources.CantRedo;

                ToolBase undoTool = this.ultraToolbarsManager1.Tools["UndoTool"];
                bool undoEnabled = undoTool.SharedProps.Enabled = this.ultraImageEditor1.CanUndo;
                undoTool.SharedProps.ToolTipText = undoEnabled ? Properties.Resources.UndoLastAction : Properties.Resources.CantUndo;

                // update status bar
                ToolBase tool = this.GetToolFromImageEditMode( this.ultraImageEditor1.EditMode );
                if ( tool != null )
                {
                    this.ultraStatusBar1.Panels["currentToolPanel"].Text = tool.SharedProps.Caption;
                }

                Size imageSize = this.ultraImageEditor1.ImageSize;
                this.ultraStatusBar1.Panels["dimensionsPanel"].Text = String.Format( "{0} x {1}", imageSize.Width, imageSize.Height );

                // update image attributes on task pane
                this.UpdateImageAttributes();
            }
            finally
            {
                this.isSynchronizingUI = false;
            }
        }

			#endregion // SynchronizeUI

            #region SynchronizeColors

        private void SynchronizeColors( Color color, ImageEditColorType colorType )
        {
            if ( this.inSynchronizeColors )
                return;

            this.inSynchronizeColors = true;

            try
            {
                // BackColor
                if( colorType == ImageEditColorType.BackColor )
                {
                    if ( this.ultraImageEditor1.DrawingBackColor != color )
                        this.ultraImageEditor1.DrawingBackColor = color;
                    if ( this.selectedColorIndicator1.SelectedBackColor != color )
                        this.selectedColorIndicator1.SelectedBackColor = color;
                    if ( this.colorSelector1.SelectedBackColor != color )
                        this.colorSelector1.SelectedBackColor = color;
                    ((Infragistics.Win.UltraWinToolbars.PopupColorPickerTool)this.ultraToolbarsManager1.Tools["BackColor"]).SelectedColor = this.ultraImageEditor1.DrawingBackColor;
                }
                else // ForeColor
                {
                    if ( this.ultraImageEditor1.DrawingForeColor != color )
                        this.ultraImageEditor1.DrawingForeColor = color;
                    if ( this.selectedColorIndicator1.SelectedForeColor != color )
                        this.selectedColorIndicator1.SelectedForeColor = color;
                    if ( this.colorSelector1.SelectedForeColor != color )
                        this.colorSelector1.SelectedForeColor = color;
                    ((Infragistics.Win.UltraWinToolbars.PopupColorPickerTool)this.ultraToolbarsManager1.Tools["ForeColor"]).SelectedColor = this.ultraImageEditor1.DrawingForeColor;
                }
            }
            finally
            {
                this.inSynchronizeColors = false;
            }
        }

		    #endregion // SynchronizeColors

            #region UpdateImageAttributes

        private void UpdateImageAttributes( )
        {
            // image size
            Size imageSize = this.ultraImageEditor1.ImageSize;
            string imageSizeText = String.Format( "{0} x {1}", imageSize.Width, imageSize.Height );
            this.linkLabelSize.Text = imageSizeText;

            // get a copy of the image
            Image currentImage = this.ultraImageEditor1.GetImage();

            // image resolution
            string imageResolution = String.Format( "{0} x {1}  dpi", currentImage.HorizontalResolution, currentImage.VerticalResolution );
            this.labelDPI.Text = imageResolution;

            // image pixel format
            this.labelPixelFormat.Text = currentImage.PixelFormat.ToString();

            // dispose old image
            if ( this.ultraPictureBox1.Image != null )
            {
                Image image = this.ultraPictureBox1.Image as Image;
                image.Dispose();           
            }

            // set thumbnail
            this.ultraPictureBox1.Image = currentImage;

            // center thumbnail
            this.CenterPreview();
        }

            #endregion // UpdateImageAttributes

            #region UpdateRecentImageFiles

        private void UpdateRecentImageFiles()
        {
            // Clear UI
            this.ClearImageFileUI();

            // discard any old image file names over max limit
            this.TrimImageFileNamesToMax();

            // put the file names in MRU (reverse) order
            ArrayList mruFiles = this.mruImageFiles.Clone() as ArrayList;
            mruFiles.Reverse();
            object[] fileStrings = mruFiles.ToArray();

            ListTool list = this.ultraToolbarsManager1.Tools["MRUFiles"] as ListTool;

            // create link labels and File menu MRU list
            for ( int index = 0; index != fileStrings.Length; index++ )
            { 
                // get path and file name
                string fileString = (string)fileStrings[index];

                // assign file name to File menu MRU list
                list.ListToolItems.Add( index.ToString() + "MRUFile", fileString );               
            }
        }

            #endregion // UpdateRecentImageFiles

            #region CenterPreview

        private void CenterPreview()
        {
            int pictureWidth = this.ultraPictureBox1.Width;
            int groupBoxWidth = this.ultraGroupBoxPreview.Width;
            this.ultraPictureBox1.Left = (groupBoxWidth - pictureWidth)/2;
        }

            #endregion // CenterPreview

            #region ClearUndoStack

        private void ClearUndoStack()
        {
            this.ultraImageEditor1.ResetModified();
            this.ultraImageEditor1.ClearUndoStack();

            this.ultraToolbarsManager1.Tools["RedoTool"].SharedProps.Enabled = this.ultraImageEditor1.CanRedo;
            this.ultraToolbarsManager1.Tools["UndoTool"].SharedProps.Enabled = this.ultraImageEditor1.CanUndo;
        }

		    #endregion // ClearUndoStack

            #region ClearImageFileUI

        private void ClearImageFileUI()
        {
            // clear File menu MRU list
            ListTool list = this.ultraToolbarsManager1.Tools["MRUFiles"] as ListTool;
            list.ListToolItems.Clear();
        }

		    #endregion // ClearImageFileUI

            #region TrimImageFileNamesToMax

        private void TrimImageFileNamesToMax()
        {
            if ( this.fileName != String.Empty )
            {
                this.AddUniqueFileName( this.fileName );

                // trim to max capacity
                if ( this.mruImageFiles.Count > MAX_MRU_IMAGEFILES )  
                    this.mruImageFiles.RemoveRange( 0, this.mruImageFiles.Count - MAX_MRU_IMAGEFILES - 1 );
                else if ( this.mruImageFiles.Count == MAX_MRU_IMAGEFILES )
                    this.mruImageFiles.RemoveAt( 0 );
            }
        }

		    #endregion // TrimImageFileNamesToMax

            #region AddUniqueFileName

        private void AddUniqueFileName( string fileName )
        {
            // remove any duplicate
            int indexDuplicateFile = this.mruImageFiles.IndexOf( fileName );
            if ( indexDuplicateFile != -1 )
                this.mruImageFiles.RemoveAt( indexDuplicateFile );

            // add new filename to top of list
            this.mruImageFiles.Add( fileName );
        }

            #endregion // AddUniqueFileName

            #region Palette Functions

                #region UseGrayScalePalette

        private void UseGrayScalePalette()
        {
            CustomColorCollection colors = new CustomColorCollection();            

            for( int index = 0; index < 256; index += 2 )
                colors.Add( Color.FromArgb(index, index, index) );

            this.colorSelector1.ColorPalette = colors;
            this.ultraToolbarsManager1.Toolbars["Color"].Visible = true;
        }

		        #endregion // UseGrayScalePalette

		        #region UseWebSafePalette

        private void UseWebSafePalette()
        {
            CustomColorCollection colors = new CustomColorCollection();

            int value = 0xffffff;

            do
            {
                colors.Add( Color.FromArgb(255, Color.FromArgb(value)) );

                if ( value == 0 )
                    break;

                if ( (value & 0xff) == 0 )
                {
                    if ( (value & 0x00ff00) > 0 )
                    {
                        value -= 0x003300;
                        value += 0x0000ff;
                    }
                    else
                    {
                        value -= 0x330000;
                        value += 0x00ffff;
                    }
                }
                else
                    value -= 0x000033;

            } while ( value >= 0 );

            this.colorSelector1.ColorPalette = colors;
            this.ultraToolbarsManager1.Toolbars["Color"].Visible = true;
        }

		       #endregion // UseWebSafePalette

		        #region UseHSLPalette

        private void UseHSLPalette()
        {
            CustomColorCollection colors = new CustomColorCollection();

            // 30 degree increments
            for( float hue = 0f; hue < 360f; hue += 30f )
            {
                // 10% saturation increase across
                for ( float sat = 0f; sat <= 1f; sat += .1f )
                {
                    // 10 % luminance increase per row
                    for ( float lum = 0f; lum <= 1f; lum += .1f )
                        colors.Add( XPItemColor.ColorFromHLS(hue, lum, sat) );
                }
            }

            this.colorSelector1.ColorPalette = colors;
            this.ultraToolbarsManager1.Toolbars["Color"].Visible = true;
        }

		        #endregion // UseHSLPalette

		        #region UseBasicPalette

        private void UseBasicPalette()
        {
            CustomColorCollection colors = new CustomColorCollection();

            colors.Add( Color.White );
            colors.Add( Color.Yellow );
            colors.Add( Color.Lime );
            colors.Add( Color.Silver );
            colors.Add( Color.Olive );
            colors.Add( Color.Green );
            colors.Add( Color.Teal );
            colors.Add( Color.Blue );
            colors.Add( Color.Magenta );
            colors.Add( Color.Red );
            colors.Add( Color.Gray );
            colors.Add( Color.Navy );
            colors.Add( Color.Purple );
            colors.Add( Color.Maroon );
            colors.Add( Color.Black );

            this.colorSelector1.ColorPalette = colors;
            this.ultraToolbarsManager1.Toolbars["Color"].Visible = true;
        }

		        #endregion // UseBasicPalette

		        #region UseDefaultPalette

        private void UseDefaultPalette()
        {
            this.colorSelector1.ColorPalette = null;
            this.ultraToolbarsManager1.Toolbars["Color"].Visible = true;
        }

		        #endregion // UseDefaultPalette

		    #endregion // Palette Functions

            #region Text Functions

                #region DoesFontFamilyExist

        private static bool DoesFontFamilyExist( string fontFamily )
        {
            foreach( FontFamily family in FontFamily.Families )
            {
                if ( Object.Equals(family.Name, fontFamily) )
                    return true;
            }

            return false;
        } 

		        #endregion // DoesFontFamilyExist

		        #region SetImageEditorFont

        private void SetImageEditorFont()
        {
            string fontName = (string)((FontListTool)this.ultraToolbarsManager1.Tools["FontName"]).Value;
			
            if ( !DoesFontFamilyExist(fontName) )
                return;

            FontFamily ff;

            // If the font family can't be created then just exit.
            try
            {
                ff = new FontFamily( fontName );
            }
            catch { return; }

            FontStyle style = FontStyle.Regular;

            style |= ((StateButtonTool)this.ultraToolbarsManager1.Tools["Bold"]).Checked ? FontStyle.Bold : 0;
            style |= ((StateButtonTool)this.ultraToolbarsManager1.Tools["Italic"]).Checked ? FontStyle.Italic : 0;
            style |= ((StateButtonTool)this.ultraToolbarsManager1.Tools["Underlined"]).Checked ? FontStyle.Underline : 0;

            FontStyle oldStyle = style;

            if ( !ff.IsStyleAvailable ( style ) )
            {
                FontStyle mainStyle = style & ( FontStyle.Regular | FontStyle.Bold | FontStyle.Italic );
                FontStyle otherStyle = style & ~mainStyle;

                while ( true )
                {
                    if ( (mainStyle & FontStyle.Italic) != 0 )
                        mainStyle ^= FontStyle.Italic;

                    if ( ff.IsStyleAvailable( mainStyle ) )
                        break;

                    if ( (style & FontStyle.Italic) != 0)
                        mainStyle |= FontStyle.Italic;

                    if ( (mainStyle & FontStyle.Bold) != 0 )
                        mainStyle ^= FontStyle.Bold;

                    if ( ff.IsStyleAvailable( mainStyle ) )
                        break;

                    if ( (style & (FontStyle.Italic | FontStyle.Bold )) != 0)
                        mainStyle = FontStyle.Italic;

                    if ( ff.IsStyleAvailable( mainStyle ) )
                        break;

                    mainStyle = 0;

                    if ( ff.IsStyleAvailable( FontStyle.Bold ) )
                        mainStyle = FontStyle.Bold;
                    else
                        if ( ff.IsStyleAvailable( FontStyle.Italic ) )
                        mainStyle = FontStyle.Italic;

                    break;
                }

                style = mainStyle | otherStyle;
            }

            ff.Dispose();

            if( style != oldStyle )
            {
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["Bold"]).Checked = (style & FontStyle.Bold) != 0;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["Italic"]).Checked = (style & FontStyle.Italic) != 0;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["Underlined"]).Checked = (style & FontStyle.Underline) != 0;
            }

            this.ultraImageEditor1.DrawingFont = new Font( fontName, this.GetFontSize(), style );

            this.GetImageEditorFont( this.ultraImageEditor1.DrawingFont );
        }

		        #endregion // SetImageEditorFont
		
		        #region GetImageEditorFont

        private void GetImageEditorFont( Font font )
        {
            bool wasEnabled = this.ultraToolbarsManager1.EventManager.AllEventsEnabled;
            this.ultraToolbarsManager1.EventManager.AllEventsEnabled = false;

            try
            {
                if( font != null )
                {
                    ((FontListTool)this.ultraToolbarsManager1.Tools["FontName"]).Value = font.Name;
                    ((ComboBoxTool)this.ultraToolbarsManager1.Tools["FontSize"]).Value = font.SizeInPoints.ToString();
                    ((StateButtonTool)this.ultraToolbarsManager1.Tools["Bold"]).Checked = (font.Style & FontStyle.Bold) != 0;
                    ((StateButtonTool)this.ultraToolbarsManager1.Tools["Italic"]).Checked = (font.Style & FontStyle.Italic) != 0;
                    ((StateButtonTool)this.ultraToolbarsManager1.Tools["Underlined"]).Checked = (font.Style & FontStyle.Underline) != 0;
                }
            }
            finally
            {
                this.ultraToolbarsManager1.EventManager.AllEventsEnabled = wasEnabled;
            }
        }

		        #endregion // GetImageEditorFont

		        #region GetFontSize

        private float GetFontSize()
        {
            ComboBoxTool cboTool = this.ultraToolbarsManager1.Tools["FontSize"] as ComboBoxTool;

            double size;

            if ( !double.TryParse(cboTool.Text, NumberStyles.Float, null, out size) )
                return this.ultraImageEditor1.DrawingFont.Size;

            return (float)size;
        }

		        #endregion / /GetFontSize

            #endregion // Text Functions

            #region Image/View/Draw Functions

                #region CaptureImage

        private void CaptureImage( )
        {
            ImageCaptureDialog imageCaptureDialog = new ImageCaptureDialog( this.ultraImageEditor1.ImageSize );
            DialogResult result = imageCaptureDialog.ShowDialog( this );
            if ( result == DialogResult.OK )
                this.ultraImageEditor1.SetImage( imageCaptureDialog.Image );
        }

			    #endregion // CaptureImage

                #region SetImageSize

        private void SetImageSize( )
        {
            frmImageDimensions form = new frmImageDimensions();
            form.MaximumDimensions = new Size( MAX_IMAGE_WIDTH, MAX_IMAGE_HEIGHT );
            form.ShowMe( this.ultraImageEditor1.ImageSize );
            Size dimensions = form.Dimensions;
            if ( dimensions != Size.Empty )
            {
                this.ultraImageEditor1.SetImageDimensions( dimensions.Width, dimensions.Height );
                string imageSizeText = String.Format( "{0} x {1}", dimensions.Width, dimensions.Height );
                this.ultraStatusBar1.Panels["dimensionsPanel"].Text = imageSizeText;
                this.linkLabelSize.Text = imageSizeText;
            }
        }

			    #endregion // SetImageSize

                #region Zoom

        // Sets the magnification of the image editor.
        internal void Zoom(bool autoFit, float magnificationLevel)
        {
            if (autoFit)
            {
                this.ultraImageEditor1.AutoFitMagnifiedView = true;
            }
            else
            {
                if (magnificationLevel >= this.trackBarZoom.MinValue &&
                    magnificationLevel <= this.trackBarZoom.MaxValue)
                {
                    int magnification = (int)Math.Floor(magnificationLevel);
                    this.ultraImageEditor1.AutoFitMagnifiedView = false;
                    this.ultraImageEditor1.MagnificationLevel = magnification;
                    this.trackBarZoom.Value = magnification;
                    this.ultraStatusBar1.Panels["zoomMultiplier"].Text = String.Format("{0}x", magnification);
                }
            }
        }

                #endregion // Zoom

                #region SetBrushSize

        private void SetBrushSize( string toolKey )
        {
            //	Set the brush size, and also uncheck the other buttons
            int brushSize = BRUSH_SIZE_SMALL;

            if ( toolKey.Equals("SmallBrush") )
            {
                brushSize = BRUSH_SIZE_SMALL;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["MediumBrush"]).Checked =
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["LargeBrush"]).Checked  = false;                        
            }
            else
            if ( toolKey.Equals("MediumBrush") )
            {
                brushSize = BRUSH_SIZE_MEDIUM;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["SmallBrush"]).Checked =
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["LargeBrush"]).Checked = false;
            }
            else
            if ( toolKey.Equals("LargeBrush") )
            {
                brushSize = BRUSH_SIZE_LARGE;
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["SmallBrush"]).Checked  =
                ((StateButtonTool)this.ultraToolbarsManager1.Tools["MediumBrush"]).Checked = false;
            }

            this.ultraImageEditor1.BrushSize = brushSize;
					
            //	Apply this value to the UltraImageEditor's 'EraseSize' property as well
            this.ultraImageEditor1.EraseSize = brushSize;
        }

			    #endregion // SetBrushSize

                #region SetDrawingGradientStyle

        private void SetDrawingGradientStyle( ToolBase tool )
        {
            Type enumType = typeof( Infragistics.Win.GradientStyle );
            this.ultraImageEditor1.DrawingGradientStyle = (Infragistics.Win.GradientStyle)Enum.ToObject( enumType, (int)tool.SharedProps.Tag );
        }

			    #endregion // SetDrawingGradientStyle
        
            #endregion // Image/View/Draw Functions

            #region UpdateStatusBar

        private void UpdateStatusBar( string caption )
        {
            // strip ampersand
            int index = caption.IndexOf('&');
            if (  index != -1 )
                caption = caption.Remove( index, 1 );
            this.ultraStatusBar1.Panels["currentToolPanel"].Text = caption;
        }

            #endregion // UpdateStatusBar

        #endregion // Methods

		#region Properties

			#region Image

		/// <summary>
		/// Returns the Image that resulted from the dialog session, or null if the session was canceled.
		/// </summary>
		public Image Image
		{
			get
			{
				if ( this.canceled )
					return null;
				else
					return this.ultraImageEditor1.GetImage();
			}
		}

			#endregion // Image

        #endregion // Properties

    };

	#endregion // frmImageEditor class
}
