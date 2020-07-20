using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinTree;
using Infragistics.Win.UltraWinCarousel;
using Infragistics.Win.Misc;
using Infragistics.Win.DataVisualization;
using Infragistics.Olap;
using Infragistics.Olap.FlatData;

namespace Showcase.IGExcel
{
    #region frmMain class
    /// <summary>
    /// Main form for the Infragistics Elemental showcase sample.
    /// </summary>
    public partial class ofrmMain : Form
    {
        #region Fields

        //  Static/consts
        /// <summary>17 x 13</summary>
        static private readonly Size LabelImageSize = new Size(17, 13);

        /// <summary>23 x 23</summary>
        static private readonly Size ScrollButtonSize = new Size(23, 23);
        
        /// <summary>23 x 17</summary>
        static private readonly Size SelectedLabelImageSize = new Size(23, 17);
        
        /// <summary>75 x 25</summary>
        static private readonly Size DropDownCommandButtonSize = new Size(75, 25);
        
        /// <summary>Size of the dropdown control</summary>
        static private readonly Size DropDownSize = new Size((DropDownCommandButtonSize.Width * 2) + (DropDownPadding * 3), DropDownCommandButtonSize.Width * 3);

        /// <summary>20 x 20</summary>
        static private readonly Size DropDownButtonSize = new Size(20, 20);

        /// <summary>16 pixels</summary>
        private const double ColumnSeriesBarWidth = 16D;
        
        /// <summary>Calibri</summary>
        private const string FontFamily = "Calibri";
        
        /// <summary>BigWavesSurfing</summary>
        private const HoodieStyle InitialStyle = HoodieStyle.BigWavesSurfing;
        
        /// <summary>24 frames</summary>
        private const int CarouselAnimationFrames = 24;
        
        /// <summary>3 item slots</summary>
        public const int CarouselItemSlots = 3;
        
        /// <summary>120 pixels</summary>
        private const int RowHeaderWidth = 120;
        
        /// <summary>5 pixels</summary>
        private const int DropDownPadding = 5;

        /// <summary>GDI</summary>
        private const TextRenderingMode TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;

        //  Instance variables
        private Assets.ImageServer images = null;
        private Assets.Colors colors = null;
        private Data.SalesData salesData = null;
        private Data.ProductData productData = null;
        private Panel underline = null;
        private Data.OlapData olapData = null;
        private AppearanceBase formAppearance = null;
        private UltraTree dateFilterTree = null;
        private Control dateFilterDropDownControl = null;
        private HoodieStyle currentStyle = ofrmMain.InitialStyle;

        #endregion Fields

        #region Constructor
        /// <summary>Constructor</summary>
        public ofrmMain()
        {
            this.InitializeComponent();

            this.InitializeUI();
        }
        #endregion Constructor

        #region ImageServer
        /// <summary>
        /// Returns an instance of the ImageServer class.
        /// </summary>
        private Assets.ImageServer Images
        {
            get
            {
                if ( this.images == null )
                    this.images = new Assets.ImageServer();

                return this.images;
            }
        }
        #endregion ImageServer

        #region Colors
        /// <summary>
        /// Returns an instance of the Colors class.
        /// </summary>
        private Assets.Colors Colors
        {
            get
            {
                if ( this.colors == null )
                    this.colors = new Assets.Colors();

                return this.colors;
            }
        }
        #endregion Colors

        #region SalesData
        /// <summary>
        /// Returns an instance of the SalesData class.
        /// This class contains all the sales data for the application.
        /// </summary>
        private Data.SalesData SalesData
        {
            get
            {
                if ( this.salesData == null )
                    this.salesData = new Data.SalesData();

                return this.salesData;
            }
        }
        #endregion SalesData

        #region ProductData
        /// <summary>
        /// Returns an instance of the ProductData class.
        /// </summary>
        private Data.ProductData ProductData
        {
            get
            {
                if ( this.productData == null )
                    this.productData = new Data.ProductData();

                return this.productData;
            }
        }
        #endregion ProductData

        #region OlapData
        /// <summary>
        /// Returns an instance of the OlapData class.
        /// This is used primarily by the PivotGrid control.
        /// </summary>
        private Data.OlapData OlapData
        {
            get
            {
                if ( this.olapData == null )
                    this.olapData = new Data.OlapData(this.SalesData, this.grid);

                return this.olapData;
            }
        }
        #endregion ProductData

        #region StyleLabelControls
        /// <summary>
        /// Returns an array containing each of the style labels
        /// displayed on the form.
        /// </summary>
        private IEnumerable<UltraLabel> StyleLabelControls
        {
            get
            {
                return new UltraLabel[]
                {
                    this.lbl3650A, this.lbl4589A, this.lbl7188A, this.lbl6309A,
                    this.lbl8711A, this.lbl9002A, this.lbl6917A, this.lbl3167A,
                };

            }
        }
        #endregion StyleLabelControls

        #region InitializeUI
        /// <summary>
        /// Entry point for user interface initialization.
        /// </summary>
        private void InitializeUI()
        {
            this.formAppearance = new Infragistics.Win.Appearance();
            this.formAppearance.BackColor = this.Colors.FormBackground;
            this.formAppearance.BackColor2 = this.Colors.FormBackground;
            this.formAppearance.BackGradientStyle = GradientStyle.None;

            Infragistics.Win.Appearance formButtonAppearance = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance formButtonHotAppearance = new Infragistics.Win.Appearance();
            Color dark = this.Colors.GridHeaderBackground;
            Color light = Color.FromArgb(192, dark);

            formButtonAppearance.BackColor = dark;
            formButtonAppearance.ForeColor = Color.FromArgb(192, Color.White);

            formButtonHotAppearance.BackColor = light;
            formButtonHotAppearance.ForeColor = Color.White;

            this.formManager.FormStyleSettings.CaptionButtonsAppearances.DefaultButtonAppearances.Appearance = formButtonAppearance;
            this.formManager.FormStyleSettings.CaptionButtonsAppearances.DefaultButtonAppearances.HotTrackAppearance = formButtonHotAppearance;
            this.formManager.FormStyleSettings.CaptionAreaAppearance.BackColor = this.Colors.FormBackground;

            this.grid.Appearance.BackColor = Color.Transparent;
            this.BackColor = this.Colors.FormBackground;
            this.Font = new Font(ofrmMain.FontFamily, 9f);
            this.SetTitle();
            this.InitializeLabelControls();
            this.InitializeDateFilterDropDown();
            this.InitializeCarousel(ofrmMain.InitialStyle);
            this.InitializePivotGrid();
            this.OnItemSelected(ofrmMain.InitialStyle);
            this.SetLabelImages();

            this.pnlMain.Appearance = this.formAppearance;
        }
        #endregion InitializeUI

        #region InitializeDateFilterDropDown
        /// <summary>
        /// Initializes the dropdown which provides a UI for date filtering.
        /// </summary>
        private void InitializeDateFilterDropDown()
        {
            //  No focus rectangle
            this.cmdDateFilter.DrawFilter = NoFocusDrawFilter.Instance;

            //  Use GDI text rendering for anti-aliasing
            this.cmdDateFilter.TextRenderingMode = ofrmMain.TextRenderingMode;

            //  Suppress theming
            this.cmdDateFilter.UseOsThemes = DefaultableBoolean.False;

            //  Set up appearance
            this.cmdDateFilter.Text = Assets.strings.ResourceManager.GetString("DateFilter");
            this.cmdDateFilter.Appearance.BackColor = this.Colors.TitleDark;
            this.cmdDateFilter.Appearance.ForeColor = this.Colors.DateFilterDropDownButtonForeColor;
            this.cmdDateFilter.Appearance.BorderColor = Color.Transparent;
            this.cmdDateFilter.Appearance.Image = this.Images[Assets.ImageServer.ButtonImages.DropDownButton];
            this.cmdDateFilter.Appearance.ImageHAlign = HAlign.Right;
            this.cmdDateFilter.ImageSize = ofrmMain.DropDownButtonSize;
            this.cmdDateFilter.Appearance.TextHAlign = HAlign.Left;
            this.cmdDateFilter.HotTrackAppearance.Image = this.Images[Assets.ImageServer.ButtonImages.DropDownButtonHot];
            this.cmdDateFilter.HotTrackAppearance.ForeColor = this.Colors.DateFilterDropDownButtonForeColorHot;
            this.cmdDateFilter.HotTrackAppearance.BorderColor = Color.Transparent;
            this.cmdDateFilter.Font = new Font(ofrmMain.FontFamily, 12f);

            //  Handle the Click event, and show the dropdown when the button is clicked
            EventHandler handler =
            delegate(object sender, EventArgs e)
            {
                if ( this.OlapData.DateMembers == null )
                    return;

                if ( DropDownManager.IsDroppedDown(this.cmdDateFilter) )
                {
                    DropDownManager.CloseDropDown(this.cmdDateFilter);
                }
                else
                {
                    //  Create the dropdown control
                    Control dropDownControl = this.CreateDateFilterDropDownControl(ofrmMain.DropDownSize, ofrmMain.DropDownCommandButtonSize, ofrmMain.DropDownPadding);

                    //  Get the screen location, size, etc.
                    System.Drawing.Rectangle screenRect = this.cmdDateFilter.Parent.RectangleToScreen(this.cmdDateFilter.Bounds);
                    System.Drawing.Point location = screenRect.Location;
                    location.Offset(screenRect.Width, screenRect.Height);

                    //  Drop down
                    DropDownManager.DropDown(
                        this.cmdDateFilter,
                        dropDownControl,
                        this.cmdDateFilter,
                        null,
                        screenRect,
                        ofrmMain.DropDownSize,
                        location,
                        true);

                }

            };
           
            this.cmdDateFilter.Click += handler;

        }
        #endregion InitializeDateFilterDropDown

        #region CreateDateFilterDropDownControl
        /// <summary>
        /// Creates a Panel control containing an UltraTree and two UltraButtons.
        /// This control appears when the 'Date Filters' dropdown button is clicked.
        /// </summary>
        private Control CreateDateFilterDropDownControl(Size size, Size buttonSize, int padding)
        {
            if ( this.dateFilterDropDownControl == null )
            {
                this.dateFilterDropDownControl = new Panel();
                this.dateFilterDropDownControl.Size = size;
                this.dateFilterDropDownControl.BackColor = this.colors.FilterDropDownBackground;
                this.dateFilterDropDownControl.Font = this.Font;

                //  OK button
                UltraButton cmdOk = new UltraButton();
                cmdOk.Name = "cmdOk";
                cmdOk.Text = Assets.strings.ResourceManager.GetString("Ok");
                cmdOk.TextRenderingMode = ofrmMain.TextRenderingMode;
                cmdOk.DrawFilter = NoFocusDrawFilter.Instance;

                //  Cancel button
                UltraButton cmdCancel = new UltraButton();
                cmdCancel.Name = "cmdCancel";
                cmdCancel.Text = Assets.strings.ResourceManager.GetString("Cancel");
                cmdCancel.TextRenderingMode = ofrmMain.TextRenderingMode;
                cmdCancel.DrawFilter = NoFocusDrawFilter.Instance;

                UltraButton[] buttons = new UltraButton[]{cmdOk, cmdCancel};

                //  Handle the Click event
                EventHandler clickHandler =
                delegate(object sender, EventArgs e)
                {
                    UltraButton b = sender as UltraButton;
                    if (b.Name == "cmdOk")
                    {
                        this.dateFilterTree.AcceptChanges();
                        this.UpdateFilters();
                    }
                    else
                        this.dateFilterTree.RejectChanges();

                    DropDownManager.CloseDropDown(this.cmdDateFilter);
                };

                //  Buttons: appearance
                AppearanceBase appearance = new Infragistics.Win.Appearance();
                appearance.BackColor = this.colors.TitleLight;
                appearance.BorderColor = Color.FromArgb(96, Color.White);
                appearance.ForeColor = Color.White;

                //  Buttons: size, location
                int top = size.Height - buttonSize.Height - padding;
                cmdOk.Location = new System.Drawing.Point(size.Width - (buttonSize.Width * 2) - (padding * 2), top);
                cmdCancel.Location = new System.Drawing.Point(size.Width - buttonSize.Width - padding, top);

                //  Apply the appearance, suppress theming, hook the Click event
                foreach ( UltraButton button in buttons )
                {
                    button.Appearance = appearance;
                    button.UseOsThemes = DefaultableBoolean.False;
                    button.ButtonStyle = UIElementButtonStyle.Flat;
                    button.Click += clickHandler;
                    this.dateFilterDropDownControl.Controls.Add(button);
                }

                //  Create the UltraTree
                UltraTree tree = this.CreateDateFilterTree();
                Size treeSize = size;
                treeSize.Width -= padding;
                treeSize.Height -= (buttonSize.Height + (padding * 3));
                tree.Size = treeSize;
                tree.Top = padding;
                tree.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                this.dateFilterDropDownControl.Controls.Add(tree);

                this.dateFilterDropDownControl.Leave += delegate(object sender, EventArgs arg)
                {
                    this.dateFilterTree.RejectChanges();
                };

            }

            return this.dateFilterDropDownControl;
        }
        #endregion CreateDateFilterDropDownControl

        #region CreateDateFilterTree
        /// <summary>
        /// Creates the UltraTree control used to present the date hierarchy.
        /// This control provides a UI through which the user can use checkboxes
        /// to show or hide sales data for specific years or quarters.
        /// </summary>
        /// <returns></returns>
        private UltraTree CreateDateFilterTree()
        {
            if ( this.dateFilterTree == null )
            {
                this.dateFilterTree = new UltraTree();

                // generate column set which will track the changes on filters and allows you to revert the changes
                this.dateFilterTree.ColumnSettings.RootColumnSet = new UltraTreeColumnSet();
                this.dateFilterTree.ColumnSettings.RootColumnSet.Columns.Add(new UltraTreeNodeColumn() { Key = UltraTreeExtender.OriginalStateColumnKey, DataType = typeof(CheckState) });

                //  Suppress hot tracking
                this.dateFilterTree.Override.HotTracking = DefaultableBoolean.False;

                //  Use synchronized checkboxes, without node connectors
                this.dateFilterTree.NodeConnectorStyle = NodeConnectorStyle.None;
                this.dateFilterTree.Override.NodeStyle = NodeStyle.SynchronizedCheckBox;
                this.dateFilterTree.ScrollBounds = ScrollBounds.ScrollToFill;

                //  Use GDI text rendering for anti-aliasing
                this.dateFilterTree.TextRenderingMode = ofrmMain.TextRenderingMode;

                //  Set up the appearance
                this.dateFilterTree.BorderStyle = UIElementBorderStyle.None;
                this.dateFilterTree.Appearance.BackColor = this.Colors.FilterDropDownBackground;
                this.dateFilterTree.Appearance.ForeColor = Color.White;
                this.dateFilterTree.Appearance.BorderColor = Color.Transparent;
                this.dateFilterTree.Override.ShowExpansionIndicator = ShowExpansionIndicator.Never;

                //  No focus rectangle
                this.dateFilterTree.DrawFilter = NoFocusDrawFilter.Instance;

                UltraTreeNode node = null;

                string q1 = Assets.strings.ResourceManager.GetString("Q1");
                string q2 = Assets.strings.ResourceManager.GetString("Q2");
                string q3 = Assets.strings.ResourceManager.GetString("Q3");
                string q4 = Assets.strings.ResourceManager.GetString("Q4");

                for ( int year = 2009; year <= 2014; year ++ )
                {
                    node = this.dateFilterTree.Nodes.Add(string.Format("{0}", year));

                    //  Set the node's Tag to the fully qualified name of the
                    //  corresponding member in the OLAP data source
                    string name = string.Format("[Date].[Date].[year].&[{0}]", year);
                    node.Tag = name;

                    UltraTreeNode child = null;

                    for ( int quarter = 1; quarter <= 4; quarter ++ )
                    {
                        string text = Assets.strings.ResourceManager.GetString(string.Format("Q{0}", quarter));
                        child = node.Nodes.Add(string.Format("{0}.{1}", year, quarter), text);
                        child.Tag = string.Format("[Date].[Date].[quarter].&[{0}]&[Q{1} {0}]", year, quarter);
                        child.CheckedState = CheckState.Checked;
                    }
                }

                AfterNodeChangedEventHandler afterChecked = delegate(object sender, NodeEventArgs arg)
                {
                     Control cmdOk = this.dateFilterDropDownControl.Controls["cmdOk"];
                     cmdOk.Enabled = dateFilterTreeHasFilter(this.dateFilterTree.Nodes);
                };
                this.dateFilterTree.AfterCheck += afterChecked;
                this.dateFilterTree.AcceptChanges();
                this.dateFilterTree.ExpandAll();
            }

            return this.dateFilterTree;
        }
        #region dateFilterTreeHasFilter
        
        /// <summary>
        /// check if customer has selected value to filter
        /// </summary>
        private bool dateFilterTreeHasFilter(TreeNodesCollection treeNodesCollection)
        {
            if (treeNodesCollection == null || treeNodesCollection.Count == 0)
                return false;
            foreach(var node in treeNodesCollection)
            {
                if (node.CheckedState == CheckState.Checked)
                    return true;
                else if (dateFilterTreeHasFilter(node.Nodes))
                    return true;
            }
            return false;
        }
        #endregion dateFilterTreeHasFilter

        #endregion CreateDateFilterTree

        #region UpdateFilters
        /// <summary>
        /// Reads the check state for each node in the date filter tree
        /// to determine which years/quarters of sales data should be displayed.
        /// </summary>
        private bool UpdateFilters()
        {
            if ( this.dateFilterTree == null )
                return false;

            string hierarchyName = "[Date].[Date]";

            //  Remove all currently enabled filters
            this.OlapData.DataSource.RemoveAllFilterMembersAsync(hierarchyName, false);

            foreach ( UltraTreeNode node in this.dateFilterTree.Nodes )
            {
                CheckState checkState = node.CheckedState;
                string memberName = node.Tag as string;

                switch ( checkState )
                {
                    case CheckState.Checked:
                        this.OlapData.DataSource.AddFilterMemberAsync(hierarchyName, memberName, false);
                        break;

                    case CheckState.Indeterminate:
                        foreach ( UltraTreeNode child in node.Nodes )
                        {
                            checkState = child.CheckedState;
                            memberName = child.Tag as string;

                            switch ( checkState )
                            {
                                case CheckState.Checked:
                                    this.OlapData.DataSource.AddFilterMemberAsync(hierarchyName, memberName, false);
                                    break;
                            }

                        }

                        break;
                }
            }

            this.OlapData.DataSource.UpdateAsync(this.grid);

            this.InitializeChart(this.currentStyle);

            return true;
        }
        #endregion UpdateFilters

        #region InitializeLabelControls
        /// <summary>
        /// Initializes the UltraLabel controls.
        /// </summary>
        private void InitializeLabelControls()
        {
            //  Get the localized SKU prefix
            string skuPrefix = Assets.strings.ResourceManager.GetString("SkuPrefix");

            //  Use the same foreground color for all labels
            Color labelForeColor = this.Colors.LabelForeColor;
            IEnumerable<UltraLabel> labels = this.StyleLabelControls;

            //  Iterate the style labels and set the relevant properties on each one.
            foreach ( UltraLabel label in labels )
            {
                //  Use the name of the control to get the SKU number
                string s = label.Name.Replace("lbl", "SKU");
                HoodieSKU sku = (HoodieSKU)Enum.Parse(typeof(HoodieSKU), s);

                //  Get the style and assign it to the Tag property for future reference.
                HoodieStyle style = (HoodieStyle)sku;
                label.Tag = style;

                //  Set the font and foreground color
                label.Appearance.FontData.Name = ofrmMain.FontFamily;
                label.Appearance.ForeColor = labelForeColor;

                //  Get the localized string for the corresponding SKU,
                //  and assign it to the Text property.
                s = Assets.strings.ResourceManager.GetString(sku.ToString());
                label.Text = string.Format("{0}{1}", skuPrefix, s);

                //  Use GDI text rendering for anti-aliasing.
                label.TextRenderingMode = ofrmMain.TextRenderingMode;
            }

            //  Set up the appearance of the 'selected' label, i.e., the
            //  one which appears below the application title and changes
            //  when a new style is selected.
            this.lblSelected.Appearance.FontData.Name = ofrmMain.FontFamily;
            this.lblSelected.Appearance.FontData.SizeInPoints = 11f;
            this.lblSelected.Appearance.ForeColor = labelForeColor;
            this.lblSelected.TextRenderingMode = ofrmMain.TextRenderingMode;

        }
        #endregion InitializeLabelControls

        #region SetLabelImages
        /// <summary>
        /// Assigns an image to each label's appearance.
        /// The image is a simple rectangle filled with the color
        /// associated with the style represented by that label.
        /// </summary>
        private void SetLabelImages()
        {
            IEnumerable<UltraLabel> labels = this.StyleLabelControls;

            foreach ( UltraLabel label in labels )
            {
                Image oldImage = label.Appearance.Image as Image;
                if ( oldImage != null )
                    oldImage.Dispose();

                string name = label.Name;
                name = name.Replace("lbl", string.Empty);
                HoodieStyle style = (HoodieStyle)label.Tag;
                label.Tag = style;
                Image image = Assets.ImageServer.GenerateColorImage(this.Colors[style], ofrmMain.LabelImageSize);

                label.Appearance.ForeColor = this.Colors.LabelForeColor;
                label.Appearance.Image = image;
                label.Appearance.TextVAlign = VAlign.Middle;
                label.ImageSize = ofrmMain.LabelImageSize;
            }
        }
        #endregion SetLabelImages

        #region InitializePivotGrid
        /// <summary>
        /// Initializes the UltraPivotGrid control.
        /// </summary>
        private void InitializePivotGrid()
        {
            //  Set the data source
            this.grid.DataSource = this.OlapData.DataSource;

            //  Use GDI text rendering.
            this.grid.TextRenderingMode = ofrmMain.TextRenderingMode;

            //  Use the compact row header layout
            this.grid.RowHeaderLayout = Infragistics.Win.UltraWinPivotGrid.RowHeaderLayout.Compact;

            //  Set the appearance for row and column headers.
            Infragistics.Win.Appearance columnHeaderAppearance = new Infragistics.Win.Appearance();

            columnHeaderAppearance.BackColor = this.Colors.GridHeaderBackground;
            columnHeaderAppearance.BorderColor = this.Colors.GridHeaderBorder;
            columnHeaderAppearance.ForeColor = Color.White;
            columnHeaderAppearance.BackGradientStyle = GradientStyle.None;

            this.grid.ColumnHeaderAppearance.Normal = columnHeaderAppearance;
            this.grid.ColumnHeaderAppearance.HotTracking = columnHeaderAppearance;


            Infragistics.Win.Appearance rowHeaderAppearance = new Infragistics.Win.Appearance();
            rowHeaderAppearance.BackColor = this.Colors.FormBackground;
            rowHeaderAppearance.BackGradientStyle = GradientStyle.None;
            rowHeaderAppearance.BorderColor = Color.LightGray;
            rowHeaderAppearance.ForeColor = this.colors.GridRowHeaderForeColor;

            this.grid.RowHeaderAppearance.Normal = rowHeaderAppearance;
            this.grid.RowHeaderAppearance.HotTracking = rowHeaderAppearance;

            Infragistics.Win.Appearance columnHeaderSelectedAppearance = new Infragistics.Win.Appearance();
            columnHeaderSelectedAppearance.BackColor = this.Colors.TitleDark;
            columnHeaderSelectedAppearance.ForeColor = Color.White;
            this.grid.ColumnHeaderAppearance.Selected = columnHeaderSelectedAppearance;

            Infragistics.Win.Appearance rowHeaderSelectedAppearance = new Infragistics.Win.Appearance();
            rowHeaderSelectedAppearance.BackColor = rowHeaderAppearance.BackColor;
            rowHeaderSelectedAppearance.ForeColor = rowHeaderAppearance.ForeColor;
            this.grid.RowHeaderAppearance.Selected = rowHeaderSelectedAppearance;

            this.grid.ColumnHeaderAppearance.HasSelectedCell.ForeColor = this.Colors.GridHeaderBackground;
            this.grid.RowHeaderAppearance.HasSelectedCell.ForeColor = rowHeaderAppearance.ForeColor;

            //  Set the appearance for the drop areas.
            Infragistics.Win.Appearance dropAreaAppearance = new Infragistics.Win.Appearance();
            dropAreaAppearance.BackColor = Color.Transparent;
            //dropAreaAppearance.BorderColor = Color.Transparent;//this.Colors.GridHeaderBackground;
            dropAreaAppearance.BorderColor = this.Colors.GridHeaderBackground;
            this.grid.DropAreaAppearance.Normal = dropAreaAppearance;

            Infragistics.Win.Appearance dropAreaDragOverAppearance = new Infragistics.Win.Appearance();
            dropAreaDragOverAppearance.BackColor = this.Colors.TitleLight;
            dropAreaDragOverAppearance.ForeColor = Color.White;
            this.grid.DropAreaAppearance.DragOver = dropAreaDragOverAppearance;

            //  Make the background of cells transparent, center align the text
            this.grid.CellAppearance.Normal.BackColor = Color.Transparent;
            this.grid.CellAppearance.Normal.TextHAlign = HAlign.Center;

            //  Hide the default image for the filter drop area, set its ForeColor and Text
            this.grid.FilterDropArea.ShowDefaultImage = false;
            this.grid.FilterDropArea.Appearance.Normal.ForeColor = this.Colors.TitleLight;
            this.grid.FilterDropArea.Text = Assets.strings.ResourceManager.GetString("FilterDropAreaText");

            //  Hide the default image for the measure drop area, set its ForeColor and Text
            this.grid.MeasureDropArea.ShowDefaultImage = false;

            //  Hide the default image for the row drop area, set its ForeColor and Text
            this.grid.RowDropArea.ShowDefaultImage = false;
            this.grid.RowDropArea.Appearance.Normal.ForeColor = this.Colors.TitleLight;
            this.grid.RowDropArea.Text = Assets.strings.ResourceManager.GetString("RowDropAreaText");

            //  Hide the default image for the column drop area, set its ForeColor and Text
            this.grid.ColumnDropArea.ShowDefaultImage = false;
            this.grid.ColumnDropArea.Appearance.Normal.ForeColor = this.Colors.TitleLight;
            this.grid.ColumnDropArea.Text = Assets.strings.ResourceManager.GetString("ColumnDropAreaText");

            //  Do not display filter or remove buttons for any of the
            //  drop area items, since that functionality is handled by
            //  other parts of the UI.
            this.grid.FilterDropArea.AllowFiltering = false;
            this.grid.FilterDropArea.AllowRemoveItems = false;

            this.grid.RowDropArea.AllowFiltering = false;
            this.grid.RowDropArea.AllowRemoveItems = false;

            this.grid.ColumnDropArea.AllowFiltering = false;
            this.grid.ColumnDropArea.AllowRemoveItems = false;

            this.grid.MeasureDropArea.AllowRemoveItems = false;

            //  Set the appearance for the drop area items
            Infragistics.Win.Appearance dropItemAppearance = new Infragistics.Win.Appearance();
            dropItemAppearance.BackColor = Color.Transparent;
            dropItemAppearance.BorderColor = Color.Transparent;
            dropItemAppearance.ForeColor = this.Colors.TitleLight;
            dropItemAppearance.FontData.Bold = DefaultableBoolean.True;
            this.grid.DropAreaItemAppearance.Normal = dropItemAppearance;
            this.grid.DropAreaItemAppearance.HotTracking = dropItemAppearance;

            ////  Make the ForeColor for the buttons which remove drop area items white
            //Win.Appearance whiteForeColorAppearance = new Infragistics.Win.Appearance();
            //whiteForeColorAppearance.ForeColor = Color.White;
            //this.grid.FilterDropArea.RemoveItemButtonAppearance.Normal = whiteForeColorAppearance;
            //this.grid.RowDropArea.RemoveItemButtonAppearance.Normal = whiteForeColorAppearance;
            //this.grid.ColumnDropArea.RemoveItemButtonAppearance.Normal = whiteForeColorAppearance;
            //this.grid.MeasureDropArea.RemoveItemButtonAppearance.Normal = whiteForeColorAppearance;

            ////  Make the filter buttons white
            //this.grid.FilterDropArea.FilterButtonAppearance.Normal.BackColor = Color.White;
            //this.grid.RowDropArea.FilterButtonAppearance.Normal.BackColor = Color.White;
            //this.grid.ColumnDropArea.FilterButtonAppearance.Normal.BackColor = Color.White;

            //  Handle the InitializeAsyncCompleted event so we can
            //  set the row header width after initialization
            InitializeAsyncCompletedHandler initializeAsyncCompletedHandler =
            delegate(object sender, InitializeAsyncCompletedEventArgs e)
            {
                this.grid.SetRowHeaderColumnWidth(0, ofrmMain.RowHeaderWidth);
            };

            this.OlapData.DataSource.InitializeAsyncCompleted += initializeAsyncCompletedHandler;
        }

        #endregion InitializePivotGrid

        #region InitializeCarousel
        /// <summary>
        /// Initializes the UltraCarousel control.
        /// </summary>
        /// <param name="initialStyle">The initial HoodieStyle; the corresponding item will appear in the forefront.</param>
        private void InitializeCarousel(HoodieStyle initialStyle)
        {
            int itemSlots = ofrmMain.CarouselItemSlots;
            int animationFrames = ofrmMain.CarouselAnimationFrames;

            //  Show scroll buttons
            this.carousel.ScrollButtonSize = ofrmMain.ScrollButtonSize;
            this.carousel.ScrollButtons = CarouselScrollButtonTypes.NextOrPreviousItem;
            this.carousel.ScrollButtonsAlignment = ContentAlignment.BottomLeft;

            //  Use a transparent background
            this.carousel.Appearance.BackColor = Color.Transparent;
            this.carousel.BackColor = Color.Transparent;

            //  Set up the scroll button appearance
            Infragistics.Win.Appearance scrollButtonAppearance = new Infragistics.Win.Appearance();
            scrollButtonAppearance.BackColor = Color.Transparent;
            scrollButtonAppearance.BorderColor = Color.Transparent;
            scrollButtonAppearance.ForeColor = this.Colors.TitleLight;
            scrollButtonAppearance.ImageBackground = this.Images[Assets.ImageServer.ButtonImages.ScrollButton];

            Infragistics.Win.Appearance scrollButtonHotAppearance = new Infragistics.Win.Appearance();
            scrollButtonHotAppearance.BackColor = Color.Transparent;
            scrollButtonHotAppearance.BorderColor = Color.Transparent;
            scrollButtonHotAppearance.ForeColor = this.Colors.ScrollButtonForeColorHot;
            scrollButtonHotAppearance.ImageBackground = this.Images[Assets.ImageServer.ButtonImages.ScrollButtonHot];

            Infragistics.Win.Appearance scrollButtonPressedAppearance = new Infragistics.Win.Appearance();
            scrollButtonPressedAppearance.BackColor = Color.Transparent;
            scrollButtonPressedAppearance.BorderColor = Color.Transparent;
            scrollButtonPressedAppearance.ForeColor = this.Colors.TitleDark;
            scrollButtonPressedAppearance.ImageBackground = this.Images[Assets.ImageServer.ButtonImages.ScrollButtonPressed];

            this.carousel.ScrollButtonAppearance = scrollButtonAppearance;
            this.carousel.ScrollButtonHotTrackedAppearance = scrollButtonHotAppearance;
            this.carousel.ScrollButtonPressedAppearance = scrollButtonPressedAppearance;

            //  Automatically scroll an item to the forefront when clicked.
            this.carousel.AutoScrollItem = true;

            //  Suppress hot tracking transitions
            this.carousel.UseHotTrackingTransitions = DefaultableBoolean.False;

            //  Suppress focus rect
            this.carousel.DrawFilter = NoFocusDrawFilter.Instance;

            //  Clear any previously added items
            this.carousel.Items.Clear();

            //  Since all images are the same size we can get the size
            //  from any one of them
            Image image = this.Images[HoodieStyle.AbstractFoliage];
            Size itemSize = image.Size;

            //  Set the image size, item slots and wrapping
            this.carousel.ItemSettings.ImageSize = itemSize;
            this.carousel.ItemSize = itemSize;
            this.carousel.Path.ItemSlots = itemSlots;
            this.carousel.Path.ItemWrapping = ItemWrappingMode.Standard;

            //  Pull the margins outside the top and bottom of the control,
            //  which has the effect of spacing the items further apart.
            this.carousel.Path.PathMargin = new Padding(0, -48, 0, -48);

            //  Set up the path stops
            Tuple<float, float>[] stops = null;            
            switch ( itemSlots )
            {
                case 5:
                    stops = new Tuple<float,float>[]
                    {
                        new Tuple<float, float>(.25f, .33f),
                        new Tuple<float, float>(.33f, .67f),
                        new Tuple<float, float>(.5f, 1f),
                        new Tuple<float, float>(.67f, .67f),
                        new Tuple<float, float>(.75f, .33f),
                    };
                    break;

                default:
                    stops = new Tuple<float,float>[]
                    {
                        new Tuple<float, float>(.25f, .4f),
                        new Tuple<float, float>(.5f, 1f),
                        new Tuple<float, float>(.75f, .4f),
                    };
                    break;

            }

            if ( stops != null )
            {
                foreach ( Tuple<float, float> t in stops )
                {
                    this.carousel.Path.ScalingStops.Add(t.Item1, t.Item2);
                }
            }
            
            //  Set the number of frames per transition
            this.carousel.Path.AnimationFrames = animationFrames;

            //  Set the first item to whichever one will make the initial
            //  style appear in the forefront.
            HoodieStyle first = Utils.Subtract(initialStyle, itemSlots / 2);
            List<Tuple<Image, HoodieStyle>> images = this.Images.GetHoodieImages(first);

            //  Add an item for each image, since we have one item per product
            foreach ( Tuple<Image, HoodieStyle> tuple in images )
            {
                string key = tuple.Item2.ToString();
                CarouselItem item = this.carousel.Items.Add(key);
                item.Settings.Appearance.Image = tuple.Item1;
                item.Settings.Appearance.BackColor = Color.Transparent;
                item.Settings.Appearance.BorderColor = Color.Transparent;
                item.Settings.Appearance.BackGradientStyle = GradientStyle.None;
                item.Settings.HotTrackingAppearance.BackColor = Color.Transparent;
                item.Settings.HotTrackingAppearance.BorderColor = Color.Transparent;
                item.Settings.HotTrackingAppearance.BackGradientStyle = GradientStyle.None;
                item.Settings.PressedAppearance.BackColor = Color.Transparent;
                item.Settings.PressedAppearance.BorderColor = Color.Transparent;
                item.Settings.PressedAppearance.BackGradientStyle = GradientStyle.None;
            }

            //  Hook the AnimationCompleted event.
            EventHandler<AnimationCompletedEventArgs> animationComplete =
            delegate(object sender, AnimationCompletedEventArgs args)
            {
                UltraCarousel carousel = sender as UltraCarousel;
                int slotCount = this.carousel.Path.ItemSlots;
                CarouselItem item = carousel.GetItemFromItemSlot(slotCount / 2);

                if ( item == null )
                {
                    Debug.Fail("No item here.");
                    return;
                }

                string key = item.Key;
                HoodieStyle style = (HoodieStyle)Enum.Parse(typeof(HoodieStyle), key);

                this.OnItemSelected(style);            
            };

            this.carousel.AnimationCompleted += animationComplete;

        }
        #endregion InitializeCarousel

        #region InitializeChart
        /// <summary>
        /// Initializes the UltraCarousel control.
        /// </summary>
        /// <param name="style">The current HoodieStyle; the corresponding series will appear thicker and bolder.</param>
        private void InitializeChart(HoodieStyle style)
        {
            //  Set the background
            this.chart.BackColor = this.Colors.FormBackground;

            //  Clear any previously added axes and series
            this.chart.Axes.Clear();
            this.chart.Series.Clear();

            //  Create a new SalesSummaryData instance, based on the sales data and
            //  current filter criteria
            Data.DateFilter[] filters = this.DateFilters;
            Data.SalesSummaryData summaryData = new Data.SalesSummaryData(this.SalesData, filters);

            //  Create a CategoryXAxis, assign all sales data as
            //  its data source, and the 'Year' property as its label.
            CategoryXAxis seriesXAxis = new CategoryXAxis();
            seriesXAxis.DataSource = summaryData;
            seriesXAxis.Label = "Year";

            //  Create a NumericYAxis
            NumericYAxis seriesYAxis = new NumericYAxis();

            //  Hook the FormatLabel event so we can show the sales data
            //  as currency values. Also, don't show anything unless it is
            //  evenly divisible by 1000.
            AxisFormatLabelHandler formatLabelHandler =
            delegate(AxisLabelInfo info)
            {
                decimal value = (decimal)info.Value;
                return (value % 1000) == 0 ? Utils.FormatCurrencyValue(value) : string.Empty;
            };

            seriesYAxis.FormatLabel += formatLabelHandler;

            //  Assign colors
            Color foreColor = this.Colors.ChartForeColor;

            seriesXAxis.LabelTextColor = foreColor;
            seriesXAxis.TickStroke = foreColor;
            seriesXAxis.MajorStrokeThickness = .001f;
            seriesXAxis.MinorStrokeThickness = .001f;

            seriesYAxis.LabelTextColor = foreColor;
            seriesYAxis.MajorStroke = foreColor;
            seriesYAxis.MajorStrokeThickness = .001f;
            seriesYAxis.MinorStrokeThickness = .001f;

            //  Add the axes to the control's Axes collection
            this.chart.Axes.Add(seriesXAxis);
            this.chart.Axes.Add(seriesYAxis);

            //  Set the range and interval for the y-axis
            seriesYAxis.Interval = (double)5000;
            seriesYAxis.MinimumValue = 15000;
            seriesYAxis.MaximumValue = 0;

            Array a = Enum.GetValues(typeof(HoodieStyle));
            Series selectedSeries = null;
            foreach ( object o in a)
            {
                HoodieStyle hs = (HoodieStyle)o;
                bool isSelected = (hs == style);

                //  Create a BarSeries for this style, assign the
                //  summary data as its DataSource, and the name of
                //  the style as its ValueMemberPath;
                LineSeries series = new LineSeries();
                series.DataSource = summaryData;
                series.ValueMemberPath = hs.ToString();

                //  Assign the Axes
                series.XAxis = seriesXAxis;
                series.YAxis = seriesYAxis;

                //  Get the color for this style and assign it as the brush.
                //  Note that there is an implicit conversion defined for
                //  the System.Drawing.Color type, so we can directly assign
                //  that value.
                Color barColor = this.Colors[hs];
                series.Brush = barColor;
                series.Outline = Color.Transparent;

                //  If this is the currently selected style, make the series
                //  stand out a bit.                
                series.Thickness = isSelected ? 4D : 2D;
                series.MarkerType = isSelected ? MarkerType.Circle : MarkerType.None;
                
                //  Set the marker colors and outline
                series.MarkerBrush = barColor;
                series.MarkerOutline = barColor;
                

                //  Add the series to the control's Series collection, unless
                //  this is the selected style, in which case we'll add it last
                //  so that it appears superimposed over the other ones.
                if ( isSelected )
                    selectedSeries = series;
                else
                    this.chart.Series.Add(series);
            }
            
            //  Add the selected series now since we didn't do that
            //  in the loop above.
            if ( selectedSeries != null )
                this.chart.Series.Add(selectedSeries);
        }
        #endregion InitializeChart

        #region DateFilters
        /// <summary>
        /// Returns an array of objects which reflect the state of the
        /// checked nodes in the date filter (UltraTree) control.
        /// Used to efficiently apply filtering to the SalesData class.
        /// </summary>
        private Data.DateFilter[] DateFilters
        {
            get
            {
                if ( this.dateFilterTree == null ||
                     this.dateFilterTree.Nodes.Count == 0 )
                    return new Data.DateFilter[0];

                Data.DateFilter[] filters = new Data.DateFilter[this.dateFilterTree.Nodes.Count];

                for ( int i = 0; i < this.dateFilterTree.Nodes.Count; i ++ )
                {
                    UltraTreeNode node = this.dateFilterTree.Nodes[i];
                    int year = int.Parse(node.Key);
                    bool q1 = node.Nodes[0].CheckedState == CheckState.Checked;
                    bool q2 = node.Nodes[1].CheckedState == CheckState.Checked;
                    bool q3 = node.Nodes[2].CheckedState == CheckState.Checked;
                    bool q4 = node.Nodes[3].CheckedState == CheckState.Checked;

                    filters[i] = new Data.DateFilter(year, q1, q2, q3, q4);
                }

                return filters;
            }
        }
        #endregion DateFilters

        #region SetTitle
        /// <summary>
        /// Sets the application title using localized strings.
        /// </summary>
        private void SetTitle()
        {
            this.Text = Assets.strings.ResourceManager.GetString("Title");
            string title1 = Assets.strings.ResourceManager.GetString("VintageRock");
            string title2 = Assets.strings.ResourceManager.GetString("HoodedCollection");
            string color1 = Assets.Colors.ToHexString(this.Colors.TitleDark);
            string color2 = Assets.Colors.ToHexString(this.Colors.TitleLight);
            string fontFamily = ofrmMain.FontFamily;

            string title = string.Format("<span style=\"font-family:{4}; font-size:24pt; font-weight:normal\"><span style=\"color:{1};\">{0}</span> <span style=\"color:{3}; font-weight:normal;\">{2}</span></span>", title1, color1, title2, color2, fontFamily);
            this.lblTitle.Value = title;
        }
        #endregion SetTitle

        #region OnItemSelected
        /// <summary>
        /// Called when the forefront item in the carousel changes.
        /// Changes the label which displays the currently selected style,
        /// highlights the corresponding series in the chart, and selects
        /// the corresponding column in the pivot grid.
        /// </summary>
        /// <param name="style">The style that was 'selected', i.e., the one corresponding to the forefront carousel item.</param>
        private void OnItemSelected(HoodieStyle style)
        {
            HoodieSKU sku = this.ProductData[style];

            //  Get the localized display strings for the SKU and the name
            //  of the style, and assign them to the selected style's label text
            string strStyle = Data.ProductData.GetDisplayString(style);
            string strSKU = Data.ProductData.GetDisplayString(sku);
            string prefix = Assets.strings.ResourceManager.GetString("SkuPrefix");
            this.lblSelected.Text = string.Format("{0}{1} - {2}", prefix, strSKU, strStyle);

            //  Dispose of the last image since we will be reassigning it
            Image oldImage = this.lblSelected.Appearance.Image  as Image;
            if ( oldImage != null )
                oldImage.Dispose();

            //  Generate an image for the color
            Color color = this.Colors[style];
            Image image = Assets.ImageServer.GenerateColorImage(color, ofrmMain.SelectedLabelImageSize);
            this.lblSelected.Appearance.Image = image;
            this.lblSelected.ImageSize = ofrmMain.SelectedLabelImageSize;

            //  Populate the chart with data for this style
            this.InitializeChart(style);

            //  Find the corresponding label control and underline it
            //  by positioning a one-pixel high Panel control under it.
            string s = sku.ToString().Replace("SKU", string.Empty);
            string name = string.Format("lbl{0}", s);
            Control[] controls = this.Controls.Find(name, true);
            if ( controls.Length == 1 )
            {
                System.Drawing.Rectangle labelRect = controls[0].Bounds;
                
                if ( this.underline == null )
                {
                    this.underline = new Panel();
                    this.underline.Anchor = controls[0].Anchor;
                    this.underline.BackColor = this.Colors.LabelForeColor;
                    controls[0].Parent.Controls.Add(this.underline);
                }

                int offset = 0;
                this.underline.SetBounds(labelRect.Left - offset, labelRect.Bottom + 1, labelRect.Width + offset, 1);
                this.underline.BringToFront();
            }

            //  Select the corresponding column in the pivot grid, and
            //  bring it into the viewable area of the control.
            Infragistics.Win.UltraWinPivotGrid.Data.PivotGridColumn column =
                Utils.ColumnFromStyle(this.grid, style);

            if ( column != null )
            {
                this.grid.Selection.Columns.Add(column, true);
                this.grid.BringColumnIntoView(column.Index);
            }

            this.currentStyle = style;

        }
        #endregion OnItemSelected
    }
    #endregion frmMain class
}
