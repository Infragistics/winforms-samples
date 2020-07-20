using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinLiveTileView;
using System.Globalization;
using Infragistics.Win.IGControls;
using Infragistics.Win.DataVisualization;

namespace INTune
{
    public partial class frmLiveTileView : Form
    {
        #region Constants

        public const string TopImagePath = "LiveTile.Top";
        public const string PopImagePath = "LiveTile.Pop";
        public const string RockImagePath = "LiveTile.Rock";
        public const string RAndBImagePath = "LiveTile.RAndB";
        public const string CountryImagePath = "LiveTile.Country";
        public const string RandomArtistImagePath = "LiveTile.Random";

        public const string Small100PercentImageSuffix = "_75x75.png";
        public const string Small140PercentImageSuffix = "_105x105.png";
        public const string Small180PercentImageSuffix = "_135x135.png";
        public const string Small80PercentImageSuffix = "_60x60.png";

        public const string SmallImageCollectionEdge100PercentImageSuffix = "_77x77.png";
        public const string SmallImageCollectionEdge140PercentImageSuffix = "_108x108.png";
        public const string SmallImageCollectionEdge180PercentImageSuffix = "_139x139.png";
        public const string SmallImageCollectionEdge80PercentImageSuffix = "_62x62.png";

        public const string SmallImageCollectionInside100PercentImageSuffix = "_78x78.png";
        public const string SmallImageCollectionInside140PercentImageSuffix = "_110x110.png";
        public const string SmallImageCollectionInside180PercentImageSuffix = "_141x141.png";
        public const string SmallImageCollectionInside80PercentImageSuffix = "_63x63.png";

        public const string CollapsedImageSuffix = "_30x30.png";

        public const string Medium100PercentImageSuffix = "_150x150.png";
        public const string Medium140PercentImageSuffix = "_210x210.png";
        public const string Medium180PercentImageSuffix = "_270x270.png";
        public const string Medium80PercentImageSuffix = "_120x120.png";

        public const string Wide100PercentImageSuffix = "_310x150.png";
        public const string Wide140PercentImageSuffix = "_434x210.png";
        public const string Wide180PercentImageSuffix = "_558x270.png";
        public const string Wide80PercentImageSuffix = "_248x120.png";

        public const string WideImageMain100PercentImageSuffix = "_160x150.png";
        public const string WideImageMain140PercentImageSuffix = "_224x210.png";
        public const string WideImageMain180PercentImageSuffix = "_288x270.png";
        public const string WideImageMain80PercentImageSuffix = "_128x120.png";

        public const string Large100PercentImageSuffix = "_310x310.png";
        public const string Large140PercentImageSuffix = "_434x434.png";
        public const string Large180PercentImageSuffix = "_558x558.png";
        public const string Large80PercentImageSuffix = "_248x248.png";

        public Color GridAlternateRowBackColor = Color.FromArgb(128, 54, 54, 54);
        public Color GridRowBackColor = Color.FromArgb(128, 41, 39, 39);
        public Color TracksSoldBackColor = Color.FromArgb(61, 167, 172);
        public Color NewsBackColor = Color.FromArgb(112, 29, 91);

        #endregion // Constants

        #region Members

        private Dictionary<int, string> TrackTitles = null;
        private Dictionary<int, string> TrackDurations = null;
        private Dictionary<int, string> AlbumNames = null;
        private Dictionary<int, int> MonthlyDownloads = null;
        private Dictionary<int, int> TotalDownloads = null;
        private Dictionary<string, string> NewsDictionary = null;

        #endregion // Members

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public frmLiveTileView()
        {
            InitializeComponent();
            Infragistics.Win.Office2013ColorTable.ColorScheme = Infragistics.Win.Office2013ColorScheme.DarkGray;
            this.WindowState = FormWindowState.Maximized;
            this.ultraGrid1.DrawFilter = new GridDrawFilter();
            this.ultraToolbarsManager1.CreationFilter = new ToolbarsManagerUIElementFilter();
            InitLiveTileView();
            InitDictionaries();
            CreateTiles();
            AddRandomArtistAnimationFrames();


        }
        #endregion // Constructor

        #region frmLiveTileView_Load
        /// <summary>
        /// Main Form's load event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLiveTileView_Load(object sender, EventArgs e)
        {
            InitGrid();
            this.ultraGrid1.DataSource = this.GetGridData().Tables["Tracks"];
                       
            ContextMenuStrip gridContextMenu = new ContextMenuStrip();
            ToolStripMenuItem resetGridDataMenuItem = new ToolStripMenuItem(Utilities.GetLocalizedString("Randomize"), null, RandomizeGridData);
            gridContextMenu.Items.Add(resetGridDataMenuItem);
            this.ultraGrid1.ContextMenuStrip = gridContextMenu;

            InitChart();
            ContextMenuStrip chartContextMenu = new ContextMenuStrip();
            ToolStripMenuItem resetChartDataMenuItem = new ToolStripMenuItem(Utilities.GetLocalizedString("Randomize"), null, RandomizeChartData);
            chartContextMenu.Items.Add(resetChartDataMenuItem);
            this.ultraDataChart1.ContextMenuStrip = chartContextMenu;
                       
        }
        private static void OutputControls(Control control)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1} - {2}", control.Name, control.Bounds, control.GetType().Name));
            System.Diagnostics.Debug.Indent();
            foreach (Control childControl in control.Controls)
            {
                OutputControls(childControl);
            }
            System.Diagnostics.Debug.Unindent();
        }

        #endregion // frmLiveTileView_Load

        #region Tab Navigation

        #region btnBackToLiveTileView_Click
        private void btnBackToLiveTileView_Click(object sender, EventArgs e)
        {
            this.ultraTabControl1.SelectedTab = this.ultraTabControl1.Tabs["LiveTileView"];
        }
        #endregion // btnBackToLiveTileView_Click

        #region ultraLiveTileView1_TileClick
        private void ultraLiveTileView1_TileClick(object sender, TileClickEventArgs e)
        {
            if (e.Tile == null)
            {
                return;
            }
            string tileKey = e.Tile.Key;
            if (tileKey.Contains("Sold"))
            {
                 this.ultraTabControl1.SelectedTab = this.ultraTabControl1.Tabs["TrendsChart"];
            }
            else if (tileKey.Contains("News"))
            {
                // do nothing
            }
            else
            {
                this.ultraTabControl1.SelectedTab = this.ultraTabControl1.Tabs["TrendsGrid"];
            }

        }
        #endregion // ultraLiveTileView1_TileClick

        #endregion // Tab Navigation

        #region CreateTiles
        private void CreateTiles()
        {
            #region Top Performing Artists
            TileGroup topGroup = this.ultraLiveTileView1.Groups["TopPerformingArtists"];
            
            #region TopTracksSold
            LiveTile topTracksSoldLiveTile = new LiveTile();
            topTracksSoldLiveTile.Key = "TopTracksSoldTile";
            topTracksSoldLiveTile.Appearance.Normal.BackColor = TracksSoldBackColor;

            #region Animation Frames

            #region Wide Animation Frames

            #region Wide Frame 1
            LiveTileFrameWide topTracksSoldWideFrame = new LiveTileFrameWide();
            topTracksSoldWideFrame.Animation = TileFrameAnimation.Fade;
            LiveTileWideCustomContent liveTileWideCustomContent = new LiveTileWideCustomContent();
            liveTileWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement blockText = new LiveTileContentTextElement();
            blockText.Text = "225";
            blockText.TextSize = TileTemplateTextSize.Block;
            blockText.Location = new System.Drawing.Point(10, 10);
            blockText.Size = new System.Drawing.Size(147, 58);
            liveTileWideCustomContent.Elements.Add(blockText);

            LiveTileContentTextElement normalText = new LiveTileContentTextElement();
            normalText.Text = Properties.Resources.TracksSoldThisMonth;
            normalText.TextSize = TileTemplateTextSize.Heading;
            normalText.Location = new System.Drawing.Point(10, 85);
            normalText.Size = new System.Drawing.Size(235, 25);
            normalText.Appearance.TextTrimming = TextTrimming.None;
            liveTileWideCustomContent.Elements.Add(normalText);

            topTracksSoldWideFrame.Content = liveTileWideCustomContent;
            topTracksSoldLiveTile.DefaultView.WideFrames.Add(topTracksSoldWideFrame);
            #endregion // Wide Frame 1

            #region Wide Frame 2
            topTracksSoldWideFrame = new LiveTileFrameWide();
            topTracksSoldWideFrame.Interval = new TimeSpan(0, 0, 5);
            liveTileWideCustomContent = new LiveTileWideCustomContent();
            liveTileWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            blockText = new LiveTileContentTextElement();
            blockText.Text = "247";
            blockText.TextSize = TileTemplateTextSize.Block;
            blockText.Location = new System.Drawing.Point(10, 10);
            blockText.Size = new System.Drawing.Size(125, 65);
            liveTileWideCustomContent.Elements.Add(blockText);

            normalText = new LiveTileContentTextElement();
            normalText.Text = Properties.Resources.TracksSoldThisMonth;
            normalText.TextSize = TileTemplateTextSize.Heading;
            normalText.Location = new System.Drawing.Point(10, 85);
            normalText.Size = new System.Drawing.Size(125, 18);
            normalText.Appearance.TextTrimming = TextTrimming.None;
            liveTileWideCustomContent.Elements.Add(normalText);

            topTracksSoldWideFrame.Content = liveTileWideCustomContent;
            topTracksSoldLiveTile.DefaultView.WideFrames.Add(topTracksSoldWideFrame);
            #endregion //Wide Frame 2

            #region Wide Frame 3
            topTracksSoldWideFrame = new LiveTileFrameWide();
            topTracksSoldWideFrame.Interval = new TimeSpan(0, 0, 4);
            liveTileWideCustomContent = new LiveTileWideCustomContent();
            liveTileWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            blockText = new LiveTileContentTextElement();
            blockText.Text = "268";
            blockText.TextSize = TileTemplateTextSize.Block;
            blockText.Location = new System.Drawing.Point(10, 10);
            blockText.Size = new System.Drawing.Size(125, 65);
            liveTileWideCustomContent.Elements.Add(blockText);

            normalText = new LiveTileContentTextElement();
            normalText.Text = Properties.Resources.TracksSoldThisMonth;
            normalText.TextSize = TileTemplateTextSize.Heading;
            normalText.Location = new System.Drawing.Point(10, 85);
            normalText.Size = new System.Drawing.Size(125, 18);
            normalText.Appearance.TextTrimming = TextTrimming.None;
            liveTileWideCustomContent.Elements.Add(normalText);

            topTracksSoldWideFrame.Content = liveTileWideCustomContent;
            topTracksSoldLiveTile.DefaultView.WideFrames.Add(topTracksSoldWideFrame);
            #endregion // Wide Frame 3

            #region Wide Frame 4
            topTracksSoldWideFrame = new LiveTileFrameWide();
            topTracksSoldWideFrame.Interval = new TimeSpan(0, 0, 7);
            liveTileWideCustomContent = new LiveTileWideCustomContent();
            liveTileWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            blockText = new LiveTileContentTextElement();
            blockText.Text = "311";
            blockText.TextSize = TileTemplateTextSize.Block;
            blockText.Location = new System.Drawing.Point(10, 10);
            blockText.Size = new System.Drawing.Size(125, 65);
            liveTileWideCustomContent.Elements.Add(blockText);

            normalText = new LiveTileContentTextElement();
            normalText.Text = Properties.Resources.TracksSoldThisMonth;
            normalText.TextSize = TileTemplateTextSize.Heading;
            normalText.Location = new System.Drawing.Point(10, 85);
            normalText.Size = new System.Drawing.Size(125, 18);
            normalText.Appearance.TextTrimming = TextTrimming.None;
            liveTileWideCustomContent.Elements.Add(normalText);

            topTracksSoldWideFrame.Content = liveTileWideCustomContent;
            topTracksSoldLiveTile.DefaultView.WideFrames.Add(topTracksSoldWideFrame);
            #endregion // Wide Frame 4

            #endregion // Wide Animation Frames

            #endregion Animation Frames

            topTracksSoldLiveTile.Sizing = TileSizing.Wide;
            topTracksSoldLiveTile.CurrentSize = TileSize.Wide;
            topGroup.Tiles.Add(topTracksSoldLiveTile);
            #endregion // TopTracksSold

            #region TopArtistImageCollection
            LiveTile topArtistImageCollectionLiveTile = new LiveTile();
            topArtistImageCollectionLiveTile.Key = "topArtistImageCollection";

            #region CollapsedImage
            SetTileCollapsedImage(topArtistImageCollectionLiveTile, TopImagePath, 1, "ImageMainArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Wide Animation Frames

            #region Wide Frame 1
            LiveTileFrameWide topArtistImageCollectionWideFrame = new LiveTileFrameWide();
            TileWideImageCollection topArtistImageCollectionContentWide = new TileWideImageCollection();
            SetTileImageCollection(topArtistImageCollectionContentWide, TopImagePath, 1, "Artist");

            topArtistImageCollectionWideFrame.Content = topArtistImageCollectionContentWide;
            topArtistImageCollectionLiveTile.DefaultView.WideFrames.Add(topArtistImageCollectionWideFrame);
            #endregion // Wide Frame 1

            #endregion // Wide Animation Frames

            #region Large Animation Frames

            #region Large Frame 1
            LiveTileFrameLarge topArtistImageCollectionLargeFrame = new LiveTileFrameLarge();
            TileLargeImageCollection topArtistImageCollectionContentLarge = new TileLargeImageCollection();
            SetTileImageCollection(topArtistImageCollectionContentLarge, TopImagePath, 1, "Artist");

            topArtistImageCollectionLargeFrame.Content = topArtistImageCollectionContentLarge;
            topArtistImageCollectionLiveTile.DefaultView.LargeFrames.Add(topArtistImageCollectionLargeFrame);
            #endregion // Large Frame 1

            #endregion // Large Animation Frames

            #endregion // Animation Frames

            topArtistImageCollectionLiveTile.CurrentSize = TileSize.Wide;
            topArtistImageCollectionLiveTile.Sizing = TileSizing.Large | TileSizing.Wide;
            topGroup.Tiles.Add(topArtistImageCollectionLiveTile);

            #endregion // TopArtistImageCollection

            #region TopGrossingArtists
            LiveTile topGrossingArtistsLiveTile = new LiveTile();
            topGrossingArtistsLiveTile.Key = "topArtistsGrossSold";

            #region CollapsedImage
            SetTileCollapsedImage(topGrossingArtistsLiveTile, TopImagePath, 1, "TopGrossing");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Wide Animation Frames

            #region Wide Frame 1
            LiveTileFrameWide topGrossingArtistsWideFrame = new LiveTileFrameWide();
            TileWideImageAndText01 topGrossingArtistsContentWide = new TileWideImageAndText01();
            topGrossingArtistsContentWide.TextCaptionWrap.Text = Properties.Resources.TopGrossingArtists;
            topGrossingArtistsContentWide.Image.AllResolutions.Image = Utilities.GetImageFromResource(TopImagePath + ".Wide", "TopGrossing1" + Wide100PercentImageSuffix);
            
            topGrossingArtistsWideFrame.Content = topGrossingArtistsContentWide;
            topGrossingArtistsLiveTile.DefaultView.WideFrames.Add(topGrossingArtistsWideFrame);
            #endregion Wide Frame 1

            #endregion // Wide Animation Frames

            #endregion // Animation Frames

            topGrossingArtistsLiveTile.Sizing = TileSizing.Wide;
            topGrossingArtistsLiveTile.CurrentSize = TileSize.Wide;
            topGroup.Tiles.Add(topGrossingArtistsLiveTile);
            
            #endregion

            #region TopArtist1
            LiveTile topArtist1LiveTile = new LiveTile();
            topArtist1LiveTile.Key = "TopArtist1";

            #region SmallImage
            SetTileSmallImage(topArtist1LiveTile, TopImagePath, 1, "Artist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(topArtist1LiveTile, TopImagePath, 1, "Artist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium topArtist1MediumFrame = new LiveTileFrameMedium();
            TileMediumImage topArtist1MediumContent = new TileMediumImage();
            SetTileImage(topArtist1MediumContent, TopImagePath, 1, "Artist");

            topArtist1MediumFrame.Content = topArtist1MediumContent;
            topArtist1LiveTile.DefaultView.MediumFrames.Add(topArtist1MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide topArtist1WideFrame = new LiveTileFrameWide();
            TileWideImage topArtist1WideContent = new TileWideImage();
            SetTileImage(topArtist1WideContent, TopImagePath, 1, "Artist");

            topArtist1WideFrame.Content = topArtist1WideContent;
            topArtist1LiveTile.DefaultView.WideFrames.Add(topArtist1WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge topArtist1LargeFrame = new LiveTileFrameLarge();
            TileLargeImage topArtist1LargeContent = new TileLargeImage();
            SetTileImage(topArtist1LargeContent, TopImagePath, 1, "Artist");

            topArtist1LargeFrame.Content = topArtist1LargeContent;
            topArtist1LiveTile.DefaultView.LargeFrames.Add(topArtist1LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            topArtist1LiveTile.CurrentSize = TileSize.Wide;
            topGroup.Tiles.Add(topArtist1LiveTile);
            #endregion // TopArtist1

            #region TopArtist2
            LiveTile topArtist2LiveTile = new LiveTile();
            topArtist2LiveTile.Key = "TopArtist2";

            #region SmallImage
            SetTileSmallImage(topArtist2LiveTile, TopImagePath, 2, "Artist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(topArtist2LiveTile, TopImagePath, 2, "Artist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium topArtist2MediumFrame = new LiveTileFrameMedium();
            TileMediumImage topArtist2MediumContent = new TileMediumImage();
            SetTileImage(topArtist2MediumContent, TopImagePath, 2, "Artist");

            topArtist2MediumFrame.Content = topArtist2MediumContent;
            topArtist2LiveTile.DefaultView.MediumFrames.Add(topArtist2MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide topArtist2WideFrame = new LiveTileFrameWide();
            TileWideImage topArtist2WideContent = new TileWideImage();
            SetTileImage(topArtist2WideContent, TopImagePath, 2, "Artist");

            topArtist2WideFrame.Content = topArtist2WideContent;
            topArtist2LiveTile.DefaultView.WideFrames.Add(topArtist2WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge topArtist2LargeFrame = new LiveTileFrameLarge();
            TileLargeImage topArtist2LargeContent = new TileLargeImage();
            SetTileImage(topArtist2LargeContent, TopImagePath, 2, "Artist");

            topArtist2LargeFrame.Content = topArtist2LargeContent;
            topArtist2LiveTile.DefaultView.LargeFrames.Add(topArtist2LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            topArtist2LiveTile.CurrentSize = TileSize.Large;
            topGroup.Tiles.Add(topArtist2LiveTile);
            #endregion // TopArtist2

            #region TopArtist3
            LiveTile topArtist3LiveTile = new LiveTile();
            topArtist3LiveTile.Key = "TopArtist3";

            #region SmallImage
            SetTileSmallImage(topArtist3LiveTile, TopImagePath, 3, "Artist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(topArtist3LiveTile, TopImagePath, 3, "Artist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium topArtist3MediumFrame = new LiveTileFrameMedium();
            TileMediumImage topArtist3MediumContent = new TileMediumImage();
            SetTileImage(topArtist3MediumContent, TopImagePath, 3, "Artist");

            topArtist3MediumFrame.Content = topArtist3MediumContent;
            topArtist3LiveTile.DefaultView.MediumFrames.Add(topArtist3MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide topArtist3WideFrame = new LiveTileFrameWide();
            TileWideImage topArtist3WideContent = new TileWideImage();
            SetTileImage(topArtist3WideContent, TopImagePath, 3, "Artist");

            topArtist3WideFrame.Content = topArtist3WideContent;
            topArtist3LiveTile.DefaultView.WideFrames.Add(topArtist3WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge topArtist3LargeFrame = new LiveTileFrameLarge();
            TileLargeImage topArtist3LargeContent = new TileLargeImage();
            SetTileImage(topArtist3LargeContent, TopImagePath, 3, "Artist");

            topArtist3LargeFrame.Content = topArtist3LargeContent;
            topArtist3LiveTile.DefaultView.LargeFrames.Add(topArtist3LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            topArtist3LiveTile.CurrentSize = TileSize.Wide;
            topGroup.Tiles.Add(topArtist3LiveTile);
            #endregion // TopArtist3

            #region TopNews
            LiveTile topNewsLiveTile = new LiveTile();
            topNewsLiveTile.Key = "TopNewsTile";
            topNewsLiveTile.Appearance.Normal.BackColor = NewsBackColor;

            #region Animation Frames

            #region Wide Animation Frames

            #region Wide Frame 1
            LiveTileFrameWide topNewsWideFrame = new LiveTileFrameWide();
            LiveTileWideCustomContent liveTileNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;
            topNewsWideFrame.Interval = new TimeSpan(0, 0, 4);
            topNewsWideFrame.Animation = TileFrameAnimation.Fade;

            LiveTileContentTextElement headerText = new LiveTileContentTextElement();
            headerText.Text = Properties.Resources.News;
            headerText.TextSize = TileTemplateTextSize.Heading;
            headerText.Location = new System.Drawing.Point(10, 10);
            liveTileNewsWideCustomContent.Elements.Add(headerText);

            LiveTileContentTextElement newsText = new LiveTileContentTextElement();
            newsText.Text = this.GetNews("TopNews1");
            newsText.TextSize = TileTemplateTextSize.Normal;
            newsText.Location = new System.Drawing.Point(10, 49);
            newsText.MaxLines = 2;
            newsText.Size = new System.Drawing.Size(200, 18);
            newsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileNewsWideCustomContent.Elements.Add(newsText);

            topNewsWideFrame.Content = liveTileNewsWideCustomContent;
            topNewsLiveTile.DefaultView.WideFrames.Add(topNewsWideFrame);
            #endregion // Wide Frame 1

            #region Wide Frame 2
            topNewsWideFrame = new LiveTileFrameWide();
            topNewsWideFrame.Interval = new TimeSpan(0, 0, 7);
            liveTileNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;
            
            headerText = new LiveTileContentTextElement();
            headerText.Text = Properties.Resources.News;
            headerText.TextSize = TileTemplateTextSize.Heading;
            headerText.Location = new System.Drawing.Point(10, 10);
            liveTileNewsWideCustomContent.Elements.Add(headerText);

            newsText = new LiveTileContentTextElement();
            newsText.Text = this.GetNews("TopNews2");
            newsText.TextSize = TileTemplateTextSize.Normal;
            newsText.Location = new System.Drawing.Point(10, 49);
            newsText.MaxLines = 2;
            newsText.Size = new System.Drawing.Size(200, 18);
            newsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileNewsWideCustomContent.Elements.Add(newsText);

            topNewsWideFrame.Content = liveTileNewsWideCustomContent;
            topNewsLiveTile.DefaultView.WideFrames.Add(topNewsWideFrame);
            #endregion // Wide Frame 2

            #region Wide Frame 3
            topNewsWideFrame = new LiveTileFrameWide();
            topNewsWideFrame.Interval = new TimeSpan(0, 0, 10);
            liveTileNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            headerText = new LiveTileContentTextElement();
            headerText.Text = Properties.Resources.News;
            headerText.TextSize = TileTemplateTextSize.Heading;
            headerText.Location = new System.Drawing.Point(10, 10);
            liveTileNewsWideCustomContent.Elements.Add(headerText);

            newsText = new LiveTileContentTextElement();
            newsText.Text = this.GetNews("TopNews3");
            newsText.TextSize = TileTemplateTextSize.Normal;
            newsText.Location = new System.Drawing.Point(10, 49);
            newsText.MaxLines = 2;
            newsText.Size = new System.Drawing.Size(200, 18);
            newsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileNewsWideCustomContent.Elements.Add(newsText);

            topNewsWideFrame.Content = liveTileNewsWideCustomContent;
            topNewsLiveTile.DefaultView.WideFrames.Add(topNewsWideFrame);
            #endregion // Wide Frame 3

            #endregion // Wide Animation Frames

            #endregion Animation Frames

            topNewsLiveTile.Sizing = TileSizing.Wide;
            topNewsLiveTile.CurrentSize = TileSize.Wide;
            topGroup.Tiles.Add(topNewsLiveTile);
            #endregion // Top News

            #region TopArtist4
            LiveTile topArtist4LiveTile = new LiveTile();
            topArtist4LiveTile.Key = "TopArtist4";

            #region SmallImage
            SetTileSmallImage(topArtist4LiveTile, TopImagePath, 4, "Artist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(topArtist4LiveTile, TopImagePath, 4, "Artist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium topArtist4MediumFrame = new LiveTileFrameMedium();
            TileMediumImage topArtist4MediumContent = new TileMediumImage();
            SetTileImage(topArtist4MediumContent, TopImagePath, 4, "Artist");

            topArtist4MediumFrame.Content = topArtist4MediumContent;
            topArtist4LiveTile.DefaultView.MediumFrames.Add(topArtist4MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide topArtist4WideFrame = new LiveTileFrameWide();
            TileWideImage topArtist4WideContent = new TileWideImage();
            SetTileImage(topArtist4WideContent, TopImagePath, 4, "Artist");

            topArtist4WideFrame.Content = topArtist4WideContent;
            topArtist4LiveTile.DefaultView.WideFrames.Add(topArtist4WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge topArtist4LargeFrame = new LiveTileFrameLarge();
            TileLargeImage topArtist4LargeContent = new TileLargeImage();
            SetTileImage(topArtist4LargeContent, TopImagePath, 4, "Artist");

            topArtist4LargeFrame.Content = topArtist4LargeContent;
            topArtist4LiveTile.DefaultView.LargeFrames.Add(topArtist4LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            topArtist4LiveTile.CurrentSize = TileSize.Wide;
            topGroup.Tiles.Add(topArtist4LiveTile);
            #endregion // TopArtist4

            #endregion // Top Performing Artists
            
            #region Pop
            TileGroup popGroup = this.ultraLiveTileView1.Groups["Pop"];

            #region PopArtist1
            LiveTile popArtist1LiveTile = new LiveTile();
            popArtist1LiveTile.Key = "PopArtist1";

            #region SmallImage
            SetTileSmallImage(popArtist1LiveTile, PopImagePath, 1, "PopArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(popArtist1LiveTile, PopImagePath, 1, "PopArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium popArtist1MediumFrame = new LiveTileFrameMedium();
            TileMediumImage popArtist1MediumContent = new TileMediumImage();
            SetTileImage(popArtist1MediumContent, PopImagePath, 1, "PopArtist");

            popArtist1MediumFrame.Content = popArtist1MediumContent;
            popArtist1LiveTile.DefaultView.MediumFrames.Add(popArtist1MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide popArtist1WideFrame = new LiveTileFrameWide();
            TileWideImage popArtist1WideContent = new TileWideImage();
            SetTileImage(popArtist1WideContent, PopImagePath, 1, "PopArtist");

            popArtist1WideFrame.Content = popArtist1WideContent;
            popArtist1LiveTile.DefaultView.WideFrames.Add(popArtist1WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge popArtist1LargeFrame = new LiveTileFrameLarge();
            TileLargeImage popArtist1LargeContent = new TileLargeImage();
            SetTileImage(popArtist1LargeContent, PopImagePath, 1, "PopArtist");

            popArtist1LargeFrame.Content = popArtist1LargeContent;
            popArtist1LiveTile.DefaultView.LargeFrames.Add(popArtist1LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            popArtist1LiveTile.CurrentSize = TileSize.Wide;
            popGroup.Tiles.Add(popArtist1LiveTile);
            #endregion // PopArtist1

            #region PopArtist2
            LiveTile popArtist2LiveTile = new LiveTile();
            popArtist2LiveTile.Key = "PopArtist2";

            #region SmallImage
            SetTileSmallImage(popArtist2LiveTile, PopImagePath, 2, "PopArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(popArtist2LiveTile, PopImagePath, 2, "PopArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium popArtist2MediumFrame = new LiveTileFrameMedium();
            TileMediumImage popArtist2MediumContent = new TileMediumImage();
            SetTileImage(popArtist2MediumContent, PopImagePath, 2, "PopArtist");

            popArtist2MediumFrame.Content = popArtist2MediumContent;
            popArtist2LiveTile.DefaultView.MediumFrames.Add(popArtist2MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide popArtist2WideFrame = new LiveTileFrameWide();
            TileWideImage popArtist2WideContent = new TileWideImage();
            SetTileImage(popArtist2WideContent, PopImagePath, 2, "PopArtist");

            popArtist2WideFrame.Content = popArtist2WideContent;
            popArtist2LiveTile.DefaultView.WideFrames.Add(popArtist2WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge popArtist2LargeFrame = new LiveTileFrameLarge();
            TileLargeImage popArtist2LargeContent = new TileLargeImage();
            SetTileImage(popArtist2LargeContent, PopImagePath, 2, "PopArtist");

            popArtist2LargeFrame.Content = popArtist2LargeContent;
            popArtist2LiveTile.DefaultView.LargeFrames.Add(popArtist2LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            popArtist2LiveTile.CurrentSize = TileSize.Medium;
            popGroup.Tiles.Add(popArtist2LiveTile);
            #endregion // PopArtist2

            #region PopTracksSold
            LiveTile popTracksSoldLiveTile = new LiveTile();
            popTracksSoldLiveTile.Key = "PopTracksSoldTile";
            popTracksSoldLiveTile.Appearance.Normal.BackColor = TracksSoldBackColor;
            popTracksSoldLiveTile.Appearance.Normal.FontData.Name = "Arial";

            #region Animation Frames

            #region Medium Animation Frames

            #region Medium Frame 1
            LiveTileFrameMedium popTracksSoldMediumFrame = new LiveTileFrameMedium();
            popTracksSoldMediumFrame.Animation = TileFrameAnimation.Fade;
            LiveTileMediumCustomContent liveTilePopMediumCustomContent = new LiveTileMediumCustomContent();
            liveTilePopMediumCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement popBlockText = new LiveTileContentTextElement();
            popBlockText.Text = "43";
            popBlockText.TextSize = TileTemplateTextSize.Block;
            popBlockText.Location = new System.Drawing.Point(10, 10);
            liveTilePopMediumCustomContent.Elements.Add(popBlockText);

            LiveTileContentTextElement popNormalText = new LiveTileContentTextElement();
            popNormalText.Text = Properties.Resources.TracksSold;
            popNormalText.TextSize = TileTemplateTextSize.Normal;
            popNormalText.Location = new System.Drawing.Point(10, 85);
            liveTilePopMediumCustomContent.Elements.Add(popNormalText);

            popTracksSoldMediumFrame.Content = liveTilePopMediumCustomContent;
            popTracksSoldLiveTile.DefaultView.MediumFrames.Add(popTracksSoldMediumFrame);
            #endregion // Medium Frame 1

            #region Medium Frame 2
            popTracksSoldMediumFrame = new LiveTileFrameMedium();
            popTracksSoldMediumFrame.Interval = new TimeSpan(0, 0, 7);
            liveTilePopMediumCustomContent = new LiveTileMediumCustomContent();
            liveTilePopMediumCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            popBlockText = new LiveTileContentTextElement();
            popBlockText.Text = "52";
            popBlockText.TextSize = TileTemplateTextSize.Block;
            popBlockText.Location = new System.Drawing.Point(10, 10);
            liveTilePopMediumCustomContent.Elements.Add(popBlockText);

            popNormalText = new LiveTileContentTextElement();
            popNormalText.Text = Properties.Resources.TracksSold;
            popNormalText.TextSize = TileTemplateTextSize.Normal;
            popNormalText.Location = new System.Drawing.Point(10, 85);
            liveTilePopMediumCustomContent.Elements.Add(popNormalText);

            popTracksSoldMediumFrame.Content = liveTilePopMediumCustomContent;
            popTracksSoldLiveTile.DefaultView.MediumFrames.Add(popTracksSoldMediumFrame);
            #endregion //Medium Frame 2

            #endregion // Medium Animation Frames

            #endregion Animation Frames

            popTracksSoldLiveTile.Sizing = TileSizing.Medium;
            popTracksSoldLiveTile.CurrentSize = TileSize.Medium;
            popGroup.Tiles.Add(popTracksSoldLiveTile);
            #endregion // PopTracksSold

            #region PopArtist3
            LiveTile popArtist3LiveTile = new LiveTile();
            popArtist3LiveTile.Key = "PopArtist3";

            #region SmallImage
            SetTileSmallImage(popArtist3LiveTile, PopImagePath, 3, "PopArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(popArtist3LiveTile, PopImagePath, 3, "PopArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium popArtist3MediumFrame = new LiveTileFrameMedium();
            TileMediumImage popArtist3MediumContent = new TileMediumImage();
            SetTileImage(popArtist3MediumContent, PopImagePath, 3, "PopArtist");

            popArtist3MediumFrame.Content = popArtist3MediumContent;
            popArtist3LiveTile.DefaultView.MediumFrames.Add(popArtist3MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide popArtist3WideFrame = new LiveTileFrameWide();
            TileWideImage popArtist3WideContent = new TileWideImage();
            SetTileImage(popArtist3WideContent, PopImagePath, 3, "PopArtist");

            popArtist3WideFrame.Content = popArtist3WideContent;
            popArtist3LiveTile.DefaultView.WideFrames.Add(popArtist3WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge popArtist3LargeFrame = new LiveTileFrameLarge();
            TileLargeImage popArtist3LargeContent = new TileLargeImage();
            SetTileImage(popArtist3LargeContent, PopImagePath, 3, "PopArtist");

            popArtist3LargeFrame.Content = popArtist3LargeContent;
            popArtist3LiveTile.DefaultView.LargeFrames.Add(popArtist3LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            popArtist3LiveTile.CurrentSize = TileSize.Wide;
            popGroup.Tiles.Add(popArtist3LiveTile);
            #endregion // PopArtist3

            #region PopArtist4
            LiveTile popArtist4LiveTile = new LiveTile();
            popArtist4LiveTile.Key = "PopArtist4";

            #region SmallImage
            SetTileSmallImage(popArtist4LiveTile, PopImagePath, 4, "PopArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(popArtist4LiveTile, PopImagePath, 4, "PopArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium popArtist4MediumFrame = new LiveTileFrameMedium();
            TileMediumImage popArtist4MediumContent = new TileMediumImage();
            SetTileImage(popArtist4MediumContent, PopImagePath, 4, "PopArtist");

            popArtist4MediumFrame.Content = popArtist4MediumContent;
            popArtist4LiveTile.DefaultView.MediumFrames.Add(popArtist4MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide popArtist4WideFrame = new LiveTileFrameWide();
            TileWideImage popArtist4WideContent = new TileWideImage();
            SetTileImage(popArtist4WideContent, PopImagePath, 4, "PopArtist");

            popArtist4WideFrame.Content = popArtist4WideContent;
            popArtist4LiveTile.DefaultView.WideFrames.Add(popArtist4WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge popArtist4LargeFrame = new LiveTileFrameLarge();
            TileLargeImage popArtist4LargeContent = new TileLargeImage();
            SetTileImage(popArtist4LargeContent, PopImagePath, 4, "PopArtist");

            popArtist4LargeFrame.Content = popArtist4LargeContent;
            popArtist4LiveTile.DefaultView.LargeFrames.Add(popArtist4LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            popArtist4LiveTile.CurrentSize = TileSize.Wide;
            popGroup.Tiles.Add(popArtist4LiveTile);
            #endregion // PopArtist4

            #region PopNews
            LiveTile popNewsLiveTile = new LiveTile();
            popNewsLiveTile.Key = "PopNewsTile";
            popNewsLiveTile.Appearance.Normal.BackColor = NewsBackColor;

            #region Animation Frames

            #region Wide Animation Frames

            #region Wide Frame 1
            LiveTileFrameWide popNewsWideFrame = new LiveTileFrameWide();
            popNewsWideFrame.Animation = TileFrameAnimation.Fade;
            popNewsWideFrame.Interval = new TimeSpan(0, 0, 9);
            LiveTileWideCustomContent liveTilePopNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTilePopNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement popHeaderText = new LiveTileContentTextElement();
            popHeaderText.Text = Properties.Resources.PopNews;
            popHeaderText.TextSize = TileTemplateTextSize.Heading;
            popHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTilePopNewsWideCustomContent.Elements.Add(popHeaderText);

            LiveTileContentTextElement popNewsText = new LiveTileContentTextElement();
            popNewsText.Text = this.GetNews("PopNews1");
            popNewsText.TextSize = TileTemplateTextSize.Normal;
            popNewsText.Location = new System.Drawing.Point(10, 49);
            popNewsText.MaxLines = 2;
            popNewsText.Size = new System.Drawing.Size(200, 18);
            popNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTilePopNewsWideCustomContent.Elements.Add(popNewsText);

            popNewsWideFrame.Content = liveTilePopNewsWideCustomContent;
            popNewsLiveTile.DefaultView.WideFrames.Add(popNewsWideFrame);
            #endregion // Wide Frame 1

            #region Wide Frame 2
            popNewsWideFrame = new LiveTileFrameWide();
            popNewsWideFrame.Interval = new TimeSpan(0, 0, 7);
            liveTilePopNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTilePopNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            popHeaderText = new LiveTileContentTextElement();
            popHeaderText.Text = Properties.Resources.PopNews;
            popHeaderText.TextSize = TileTemplateTextSize.Heading;
            popHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTilePopNewsWideCustomContent.Elements.Add(popHeaderText);

            popNewsText = new LiveTileContentTextElement();
            popNewsText.Text = this.GetNews("PopNews2");
            popNewsText.TextSize = TileTemplateTextSize.Normal;
            popNewsText.Location = new System.Drawing.Point(10, 49);
            popNewsText.MaxLines = 2;
            popNewsText.Size = new System.Drawing.Size(200, 18);
            popNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTilePopNewsWideCustomContent.Elements.Add(popNewsText);

            popNewsWideFrame.Content = liveTilePopNewsWideCustomContent;
            popNewsLiveTile.DefaultView.WideFrames.Add(popNewsWideFrame);
            #endregion // Wide Frame 2

            #region Wide Frame 3
            popNewsWideFrame = new LiveTileFrameWide();
            popNewsWideFrame.Interval = new TimeSpan(0, 0, 12);
            liveTilePopNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTilePopNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            popHeaderText = new LiveTileContentTextElement();
            popHeaderText.Text = Properties.Resources.PopNews;
            popHeaderText.TextSize = TileTemplateTextSize.Heading;
            popHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTilePopNewsWideCustomContent.Elements.Add(popHeaderText);

            popNewsText = new LiveTileContentTextElement();
            popNewsText.Text = this.GetNews("PopNews3");
            popNewsText.TextSize = TileTemplateTextSize.Normal;
            popNewsText.Location = new System.Drawing.Point(10, 49);
            popNewsText.MaxLines = 2;
            popNewsText.Size = new System.Drawing.Size(200, 18);
            popNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTilePopNewsWideCustomContent.Elements.Add(popNewsText);

            popNewsWideFrame.Content = liveTilePopNewsWideCustomContent;
            popNewsLiveTile.DefaultView.WideFrames.Add(popNewsWideFrame);
            #endregion // Wide Frame 3

            #endregion // Wide Animation Frames

            #endregion Animation Frames

            popNewsLiveTile.Sizing = TileSizing.Wide;
            popNewsLiveTile.CurrentSize = TileSize.Wide;
            popGroup.Tiles.Add(popNewsLiveTile);
            #endregion // PopNews

            #region PopArtist5
            LiveTile popArtist5LiveTile = new LiveTile();
            popArtist5LiveTile.Key = "PopArtist5";

            #region SmallImage
            SetTileSmallImage(popArtist5LiveTile, PopImagePath, 5, "PopArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(popArtist5LiveTile, PopImagePath, 5, "PopArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium popArtist5MediumFrame = new LiveTileFrameMedium();
            TileMediumImage popArtist5MediumContent = new TileMediumImage();
            SetTileImage(popArtist5MediumContent, PopImagePath, 5, "PopArtist");

            popArtist5MediumFrame.Content = popArtist5MediumContent;
            popArtist5LiveTile.DefaultView.MediumFrames.Add(popArtist5MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide popArtist5WideFrame = new LiveTileFrameWide();
            TileWideImage popArtist5WideContent = new TileWideImage();
            SetTileImage(popArtist5WideContent, PopImagePath, 5, "PopArtist");

            popArtist5WideFrame.Content = popArtist5WideContent;
            popArtist5LiveTile.DefaultView.WideFrames.Add(popArtist5WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge popArtist5LargeFrame = new LiveTileFrameLarge();
            TileLargeImage popArtist5LargeContent = new TileLargeImage();
            SetTileImage(popArtist5LargeContent, PopImagePath, 5, "PopArtist");

            popArtist5LargeFrame.Content = popArtist5LargeContent;
            popArtist5LiveTile.DefaultView.LargeFrames.Add(popArtist5LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            popArtist5LiveTile.CurrentSize = TileSize.Wide;
            popGroup.Tiles.Add(popArtist5LiveTile);
            #endregion // PopArtist5

            #endregion // Pop

            #region Rock
            TileGroup rockGroup = this.ultraLiveTileView1.Groups["Rock"];

            #region RockArtist1
            LiveTile rockArtist1LiveTile = new LiveTile();
            rockArtist1LiveTile.Key = "RockArtist1";

            #region SmallImage
            SetTileSmallImage(rockArtist1LiveTile, RockImagePath, 1, "RockArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(rockArtist1LiveTile, RockImagePath, 1, "RockArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium rockArtist1MediumFrame = new LiveTileFrameMedium();
            TileMediumImage rockArtist1MediumContent = new TileMediumImage();
            SetTileImage(rockArtist1MediumContent, RockImagePath, 1, "RockArtist");

            rockArtist1MediumFrame.Content = rockArtist1MediumContent;
            rockArtist1LiveTile.DefaultView.MediumFrames.Add(rockArtist1MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide rockArtist1WideFrame = new LiveTileFrameWide();
            TileWideImage rockArtist1WideContent = new TileWideImage();
            SetTileImage(rockArtist1WideContent, RockImagePath, 1, "RockArtist");

            rockArtist1WideFrame.Content = rockArtist1WideContent;
            rockArtist1LiveTile.DefaultView.WideFrames.Add(rockArtist1WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge rockArtist1LargeFrame = new LiveTileFrameLarge();
            TileLargeImage rockArtist1LargeContent = new TileLargeImage();
            SetTileImage(rockArtist1LargeContent, RockImagePath, 1, "RockArtist");

            rockArtist1LargeFrame.Content = rockArtist1LargeContent;
            rockArtist1LiveTile.DefaultView.LargeFrames.Add(rockArtist1LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            rockArtist1LiveTile.CurrentSize = TileSize.Wide;
            rockGroup.Tiles.Add(rockArtist1LiveTile);
            #endregion // RockArtist1

            #region RockArtist2
            LiveTile rockArtist2LiveTile = new LiveTile();
            rockArtist2LiveTile.Key = "RockArtist2";

            #region SmallImage
            SetTileSmallImage(rockArtist2LiveTile, RockImagePath, 2, "RockArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(rockArtist2LiveTile, RockImagePath, 2, "RockArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium rockArtist2MediumFrame = new LiveTileFrameMedium();
            TileMediumImage rockArtist2MediumContent = new TileMediumImage();
            SetTileImage(rockArtist2MediumContent, RockImagePath, 2, "RockArtist");

            rockArtist2MediumFrame.Content = rockArtist2MediumContent;
            rockArtist2LiveTile.DefaultView.MediumFrames.Add(rockArtist2MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide rockArtist2WideFrame = new LiveTileFrameWide();
            TileWideImage rockArtist2WideContent = new TileWideImage();
            SetTileImage(rockArtist2WideContent, RockImagePath, 2, "RockArtist");

            rockArtist2WideFrame.Content = rockArtist2WideContent;
            rockArtist2LiveTile.DefaultView.WideFrames.Add(rockArtist2WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge rockArtist2LargeFrame = new LiveTileFrameLarge();
            TileLargeImage rockArtist2LargeContent = new TileLargeImage();
            SetTileImage(rockArtist2LargeContent, RockImagePath, 2, "RockArtist");

            rockArtist2LargeFrame.Content = rockArtist2LargeContent;
            rockArtist2LiveTile.DefaultView.LargeFrames.Add(rockArtist2LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            rockArtist2LiveTile.CurrentSize = TileSize.Medium;
            rockGroup.Tiles.Add(rockArtist2LiveTile);
            #endregion // RockArtist2

            #region RockTracksSold
            LiveTile rockTracksSoldLiveTile = new LiveTile();
            rockTracksSoldLiveTile.Key = "RockTracksSoldTile";
            rockTracksSoldLiveTile.Appearance.Normal.BackColor = TracksSoldBackColor;
            rockTracksSoldLiveTile.Appearance.Normal.FontData.Name = "Arial";

            #region Animation Frames

            #region Medium Animation Frames

            #region Medium Frame 1
            LiveTileFrameMedium rockTracksSoldMediumFrame = new LiveTileFrameMedium();
            rockTracksSoldMediumFrame.Animation = TileFrameAnimation.Fade;
            rockTracksSoldMediumFrame.Interval = new TimeSpan(0, 0, 4);
            LiveTileMediumCustomContent liveTileRockMediumCustomContent = new LiveTileMediumCustomContent();
            liveTileRockMediumCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement rockBlockText = new LiveTileContentTextElement();
            rockBlockText.Text = "25";
            rockBlockText.TextSize = TileTemplateTextSize.Block;
            rockBlockText.Location = new System.Drawing.Point(10, 10);
            liveTileRockMediumCustomContent.Elements.Add(rockBlockText);

            LiveTileContentTextElement rockNormalText = new LiveTileContentTextElement();
            rockNormalText.Text = Properties.Resources.TracksSold;
            rockNormalText.TextSize = TileTemplateTextSize.Normal;
            rockNormalText.Location = new System.Drawing.Point(10, 85);
            liveTileRockMediumCustomContent.Elements.Add(rockNormalText);

            rockTracksSoldMediumFrame.Content = liveTileRockMediumCustomContent;
            rockTracksSoldLiveTile.DefaultView.MediumFrames.Add(rockTracksSoldMediumFrame);
            #endregion // Medium Frame 1

            #region Medium Frame 2
            rockTracksSoldMediumFrame = new LiveTileFrameMedium();
            rockTracksSoldMediumFrame.Interval = new TimeSpan(0, 0, 7);
            liveTileRockMediumCustomContent = new LiveTileMediumCustomContent();
            liveTileRockMediumCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            rockBlockText = new LiveTileContentTextElement();
            rockBlockText.Text = "37";
            rockBlockText.TextSize = TileTemplateTextSize.Block;
            rockBlockText.Location = new System.Drawing.Point(10, 10);
            liveTileRockMediumCustomContent.Elements.Add(rockBlockText);

            rockNormalText = new LiveTileContentTextElement();
            rockNormalText.Text = Properties.Resources.TracksSold;
            rockNormalText.TextSize = TileTemplateTextSize.Normal;
            rockNormalText.Location = new System.Drawing.Point(10, 85);
            liveTileRockMediumCustomContent.Elements.Add(rockNormalText);

            rockTracksSoldMediumFrame.Content = liveTileRockMediumCustomContent;
            rockTracksSoldLiveTile.DefaultView.MediumFrames.Add(rockTracksSoldMediumFrame);
            #endregion //Medium Frame 2

            #endregion // Medium Animation Frames

            #endregion Animation Frames

            rockTracksSoldLiveTile.Sizing = TileSizing.Medium;
            rockTracksSoldLiveTile.CurrentSize = TileSize.Medium;
            rockGroup.Tiles.Add(rockTracksSoldLiveTile);
            #endregion // RockTracksSold

            #region RockArtist3
            LiveTile rockArtist3LiveTile = new LiveTile();
            rockArtist3LiveTile.Key = "RockArtist3";

            #region SmallImage
            SetTileSmallImage(rockArtist3LiveTile, RockImagePath, 3, "RockArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(rockArtist3LiveTile, RockImagePath, 3, "RockArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium rockArtist3MediumFrame = new LiveTileFrameMedium();
            TileMediumImage rockArtist3MediumContent = new TileMediumImage();
            SetTileImage(rockArtist3MediumContent, RockImagePath, 3, "RockArtist");

            rockArtist3MediumFrame.Content = rockArtist3MediumContent;
            rockArtist3LiveTile.DefaultView.MediumFrames.Add(rockArtist3MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide rockArtist3WideFrame = new LiveTileFrameWide();
            TileWideImage rockArtist3WideContent = new TileWideImage();
            SetTileImage(rockArtist3WideContent, RockImagePath, 3, "RockArtist");

            rockArtist3WideFrame.Content = rockArtist3WideContent;
            rockArtist3LiveTile.DefaultView.WideFrames.Add(rockArtist3WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge rockArtist3LargeFrame = new LiveTileFrameLarge();
            TileLargeImage rockArtist3LargeContent = new TileLargeImage();
            SetTileImage(rockArtist3LargeContent, RockImagePath, 3, "RockArtist");

            rockArtist3LargeFrame.Content = rockArtist3LargeContent;
            rockArtist3LiveTile.DefaultView.LargeFrames.Add(rockArtist3LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            rockArtist3LiveTile.CurrentSize = TileSize.Large;
            rockGroup.Tiles.Add(rockArtist3LiveTile);
            #endregion // RockArtist3

            #region RockNews
            LiveTile rockNewsLiveTile = new LiveTile();
            rockNewsLiveTile.Key = "RockNewsTile";
            rockNewsLiveTile.Appearance.Normal.BackColor = NewsBackColor;

            #region Animation Frames

            #region Wide Animation Frames

            #region Wide Frame 1
            LiveTileFrameWide rockNewsWideFrame = new LiveTileFrameWide();
            rockNewsWideFrame.Animation = TileFrameAnimation.Fade;
            rockNewsWideFrame.Interval = new TimeSpan(0, 0, 10);
            LiveTileWideCustomContent liveTileRockNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileRockNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement rockHeaderText = new LiveTileContentTextElement();
            rockHeaderText.Text = Properties.Resources.RockNews;
            rockHeaderText.TextSize = TileTemplateTextSize.Heading;
            rockHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileRockNewsWideCustomContent.Elements.Add(rockHeaderText);

            LiveTileContentTextElement rockNewsText = new LiveTileContentTextElement();
            rockNewsText.Text = this.GetNews("RockNews1");
            rockNewsText.TextSize = TileTemplateTextSize.Normal;
            rockNewsText.Location = new System.Drawing.Point(10, 49);
            rockNewsText.MaxLines = 2;
            rockNewsText.Size = new System.Drawing.Size(200, 18);
            rockNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileRockNewsWideCustomContent.Elements.Add(rockNewsText);

            rockNewsWideFrame.Content = liveTileRockNewsWideCustomContent;
            rockNewsLiveTile.DefaultView.WideFrames.Add(rockNewsWideFrame);
            #endregion // Wide Frame 1

            #region Wide Frame 2
            rockNewsWideFrame = new LiveTileFrameWide();
            rockNewsWideFrame.Interval = new TimeSpan(0, 0, 7);
            liveTileRockNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileRockNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            rockHeaderText = new LiveTileContentTextElement();
            rockHeaderText.Text = Properties.Resources.RockNews;
            rockHeaderText.TextSize = TileTemplateTextSize.Heading;
            rockHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileRockNewsWideCustomContent.Elements.Add(rockHeaderText);

            rockNewsText = new LiveTileContentTextElement();
            rockNewsText.Text = this.GetNews("RockNews2");
            rockNewsText.TextSize = TileTemplateTextSize.Normal;
            rockNewsText.Location = new System.Drawing.Point(10, 49);
            rockNewsText.MaxLines = 2;
            rockNewsText.Size = new System.Drawing.Size(200, 18);
            rockNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileRockNewsWideCustomContent.Elements.Add(rockNewsText);

            rockNewsWideFrame.Content = liveTileRockNewsWideCustomContent;
            rockNewsLiveTile.DefaultView.WideFrames.Add(rockNewsWideFrame);
            #endregion // Wide Frame 2

            #region Wide Frame 3
            rockNewsWideFrame = new LiveTileFrameWide();
            rockNewsWideFrame.Interval = new TimeSpan(0, 0, 6);
            liveTileRockNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileRockNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            rockHeaderText = new LiveTileContentTextElement();
            rockHeaderText.Text = Properties.Resources.RockNews;
            rockHeaderText.TextSize = TileTemplateTextSize.Heading;
            rockHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileRockNewsWideCustomContent.Elements.Add(rockHeaderText);

            rockNewsText = new LiveTileContentTextElement();
            rockNewsText.Text = this.GetNews("RockNews3");
            rockNewsText.TextSize = TileTemplateTextSize.Normal;
            rockNewsText.Location = new System.Drawing.Point(10, 49);
            rockNewsText.MaxLines = 2;
            rockNewsText.Size = new System.Drawing.Size(200, 18);
            rockNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileRockNewsWideCustomContent.Elements.Add(rockNewsText);

            rockNewsWideFrame.Content = liveTileRockNewsWideCustomContent;
            rockNewsLiveTile.DefaultView.WideFrames.Add(rockNewsWideFrame);
            #endregion // Wide Frame 3

            #endregion // Wide Animation Frames

            #endregion Animation Frames

            rockNewsLiveTile.Sizing = TileSizing.Wide;
            rockNewsLiveTile.CurrentSize = TileSize.Wide;
            rockGroup.Tiles.Add(rockNewsLiveTile);
            #endregion // RockNews

            #region RockArtist4
            LiveTile rockArtist4LiveTile = new LiveTile();
            rockArtist4LiveTile.Key = "RockArtist4";

            #region SmallImage
            SetTileSmallImage(rockArtist4LiveTile, RockImagePath, 4, "RockArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(rockArtist4LiveTile, RockImagePath, 4, "RockArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium rockArtist4MediumFrame = new LiveTileFrameMedium();
            TileMediumImage rockArtist4MediumContent = new TileMediumImage();
            SetTileImage(rockArtist4MediumContent, RockImagePath, 4, "RockArtist");

            rockArtist4MediumFrame.Content = rockArtist4MediumContent;
            rockArtist4LiveTile.DefaultView.MediumFrames.Add(rockArtist4MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide rockArtist4WideFrame = new LiveTileFrameWide();
            TileWideImage rockArtist4WideContent = new TileWideImage();
            SetTileImage(rockArtist4WideContent, RockImagePath, 4, "RockArtist");

            rockArtist4WideFrame.Content = rockArtist4WideContent;
            rockArtist4LiveTile.DefaultView.WideFrames.Add(rockArtist4WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge rockArtist4LargeFrame = new LiveTileFrameLarge();
            TileLargeImage rockArtist4LargeContent = new TileLargeImage();
            SetTileImage(rockArtist4LargeContent, RockImagePath, 4, "RockArtist");

            rockArtist4LargeFrame.Content = rockArtist4LargeContent;
            rockArtist4LiveTile.DefaultView.LargeFrames.Add(rockArtist4LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            rockArtist4LiveTile.CurrentSize = TileSize.Wide;
            rockGroup.Tiles.Add(rockArtist4LiveTile);
            #endregion // RockArtist4

            #endregion // Rock
            
            #region R And B
            TileGroup rAndBGroup = this.ultraLiveTileView1.Groups["RAndB"];

            #region RAndBArtist1
            LiveTile rAndBArtist1LiveTile = new LiveTile();
            rAndBArtist1LiveTile.Key = "RAndBArtist1";

            #region SmallImage
            SetTileSmallImage(rAndBArtist1LiveTile, RAndBImagePath, 1, "RAndBArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(rAndBArtist1LiveTile, RAndBImagePath, 1, "RAndBArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium rAndBArtist1MediumFrame = new LiveTileFrameMedium();
            TileMediumImage rAndBArtist1MediumContent = new TileMediumImage();
            SetTileImage(rAndBArtist1MediumContent, RAndBImagePath, 1, "RAndBArtist");

            rAndBArtist1MediumFrame.Content = rAndBArtist1MediumContent;
            rAndBArtist1LiveTile.DefaultView.MediumFrames.Add(rAndBArtist1MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide rAndBArtist1WideFrame = new LiveTileFrameWide();
            TileWideImage rAndBArtist1WideContent = new TileWideImage();
            SetTileImage(rAndBArtist1WideContent, RAndBImagePath, 1, "RAndBArtist");

            rAndBArtist1WideFrame.Content = rAndBArtist1WideContent;
            rAndBArtist1LiveTile.DefaultView.WideFrames.Add(rAndBArtist1WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge rAndBArtist1LargeFrame = new LiveTileFrameLarge();
            TileLargeImage rAndBArtist1LargeContent = new TileLargeImage();
            SetTileImage(rAndBArtist1LargeContent, RAndBImagePath, 1, "RAndBArtist");

            rAndBArtist1LargeFrame.Content = rAndBArtist1LargeContent;
            rAndBArtist1LiveTile.DefaultView.LargeFrames.Add(rAndBArtist1LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            rAndBArtist1LiveTile.CurrentSize = TileSize.Wide;
            rAndBGroup.Tiles.Add(rAndBArtist1LiveTile);
            #endregion // RAndBArtist1

            #region RAndBArtist2
            LiveTile rAndBArtist2LiveTile = new LiveTile();
            rAndBArtist2LiveTile.Key = "RAndBArtist2";

            #region SmallImage
            SetTileSmallImage(rAndBArtist2LiveTile, RAndBImagePath, 2, "RAndBArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(rAndBArtist2LiveTile, RAndBImagePath, 2, "RAndBArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium rAndBArtist2MediumFrame = new LiveTileFrameMedium();
            TileMediumImage rAndBArtist2MediumContent = new TileMediumImage();
            SetTileImage(rAndBArtist2MediumContent, RAndBImagePath, 2, "RAndBArtist");

            rAndBArtist2MediumFrame.Content = rAndBArtist2MediumContent;
            rAndBArtist2LiveTile.DefaultView.MediumFrames.Add(rAndBArtist2MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide rAndBArtist2WideFrame = new LiveTileFrameWide();
            TileWideImage rAndBArtist2WideContent = new TileWideImage();
            SetTileImage(rAndBArtist2WideContent, RAndBImagePath, 2, "RAndBArtist");

            rAndBArtist2WideFrame.Content = rAndBArtist2WideContent;
            rAndBArtist2LiveTile.DefaultView.WideFrames.Add(rAndBArtist2WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge rAndBArtist2LargeFrame = new LiveTileFrameLarge();
            TileLargeImage rAndBArtist2LargeContent = new TileLargeImage();
            SetTileImage(rAndBArtist2LargeContent, RAndBImagePath, 2, "RAndBArtist");

            rAndBArtist2LargeFrame.Content = rAndBArtist2LargeContent;
            rAndBArtist2LiveTile.DefaultView.LargeFrames.Add(rAndBArtist2LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            rAndBArtist2LiveTile.CurrentSize = TileSize.Medium;
            rAndBGroup.Tiles.Add(rAndBArtist2LiveTile);
            #endregion // RAndBArtist2

            #region RAndBTracksSold
            LiveTile rAndBTracksSoldLiveTile = new LiveTile();
            rAndBTracksSoldLiveTile.Key = "RAndBTracksSoldTile";
            rAndBTracksSoldLiveTile.Appearance.Normal.BackColor = TracksSoldBackColor;
            rAndBTracksSoldLiveTile.Appearance.Normal.FontData.Name = "Arial";

            #region Animation Frames

            #region Medium Animation Frames

            #region Medium Frame 1
            LiveTileFrameMedium rAndBTracksSoldMediumFrame = new LiveTileFrameMedium();
            rAndBTracksSoldMediumFrame.Animation = TileFrameAnimation.Fade;
            rAndBTracksSoldMediumFrame.Interval = new TimeSpan(0, 0, 8);
            LiveTileMediumCustomContent liveTileRAndBMediumCustomContent = new LiveTileMediumCustomContent();
            liveTileRAndBMediumCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement rAndBBlockText = new LiveTileContentTextElement();
            rAndBBlockText.Text = "62";
            rAndBBlockText.TextSize = TileTemplateTextSize.Block;
            rAndBBlockText.Location = new System.Drawing.Point(10, 10);
            liveTileRAndBMediumCustomContent.Elements.Add(rAndBBlockText);

            LiveTileContentTextElement rAndBNormalText = new LiveTileContentTextElement();
            rAndBNormalText.Text = Properties.Resources.TracksSold;
            rAndBNormalText.TextSize = TileTemplateTextSize.Normal;
            rAndBNormalText.Location = new System.Drawing.Point(10, 85);
            liveTileRAndBMediumCustomContent.Elements.Add(rAndBNormalText);

            rAndBTracksSoldMediumFrame.Content = liveTileRAndBMediumCustomContent;
            rAndBTracksSoldLiveTile.DefaultView.MediumFrames.Add(rAndBTracksSoldMediumFrame);
            #endregion // Medium Frame 1

            #region Medium Frame 2
            rAndBTracksSoldMediumFrame = new LiveTileFrameMedium();
            rAndBTracksSoldMediumFrame.Interval = new TimeSpan(0, 0, 9);
            liveTileRAndBMediumCustomContent = new LiveTileMediumCustomContent();
            liveTileRAndBMediumCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            rAndBBlockText = new LiveTileContentTextElement();
            rAndBBlockText.Text = "74";
            rAndBBlockText.TextSize = TileTemplateTextSize.Block;
            rAndBBlockText.Location = new System.Drawing.Point(10, 10);
            liveTileRAndBMediumCustomContent.Elements.Add(rAndBBlockText);

            rAndBNormalText = new LiveTileContentTextElement();
            rAndBNormalText.Text = Properties.Resources.TracksSold;
            rAndBNormalText.TextSize = TileTemplateTextSize.Normal;
            rAndBNormalText.Location = new System.Drawing.Point(10, 85);
            liveTileRAndBMediumCustomContent.Elements.Add(rAndBNormalText);

            rAndBTracksSoldMediumFrame.Content = liveTileRAndBMediumCustomContent;
            rAndBTracksSoldLiveTile.DefaultView.MediumFrames.Add(rAndBTracksSoldMediumFrame);
            #endregion //Medium Frame 2

            #endregion // Medium Animation Frames

            #endregion Animation Frames

            rAndBTracksSoldLiveTile.Sizing = TileSizing.Medium;
            rAndBTracksSoldLiveTile.CurrentSize = TileSize.Medium;
            rAndBGroup.Tiles.Add(rAndBTracksSoldLiveTile);
            #endregion // RAndBTracksSold

            #region RAndBArtist3
            LiveTile rAndBArtist3LiveTile = new LiveTile();
            rAndBArtist3LiveTile.Key = "RAndBArtist3";

            #region SmallImage
            SetTileSmallImage(rAndBArtist3LiveTile, RAndBImagePath, 3, "RAndBArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(rAndBArtist3LiveTile, RAndBImagePath, 3, "RAndBArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium rAndBArtist3MediumFrame = new LiveTileFrameMedium();
            TileMediumImage rAndBArtist3MediumContent = new TileMediumImage();
            SetTileImage(rAndBArtist3MediumContent, RAndBImagePath, 3, "RAndBArtist");

            rAndBArtist3MediumFrame.Content = rAndBArtist3MediumContent;
            rAndBArtist3LiveTile.DefaultView.MediumFrames.Add(rAndBArtist3MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide rAndBArtist3WideFrame = new LiveTileFrameWide();
            TileWideImage rAndBArtist3WideContent = new TileWideImage();
            SetTileImage(rAndBArtist3WideContent, RAndBImagePath, 3, "RAndBArtist");

            rAndBArtist3WideFrame.Content = rAndBArtist3WideContent;
            rAndBArtist3LiveTile.DefaultView.WideFrames.Add(rAndBArtist3WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge rAndBArtist3LargeFrame = new LiveTileFrameLarge();
            TileLargeImage rAndBArtist3LargeContent = new TileLargeImage();
            SetTileImage(rAndBArtist3LargeContent, RAndBImagePath, 3, "RAndBArtist");

            rAndBArtist3LargeFrame.Content = rAndBArtist3LargeContent;
            rAndBArtist3LiveTile.DefaultView.LargeFrames.Add(rAndBArtist3LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            rAndBArtist3LiveTile.CurrentSize = TileSize.Large;
            rAndBGroup.Tiles.Add(rAndBArtist3LiveTile);
            #endregion // RAndBArtist3

            #region RAndBNews
            LiveTile rAndBNewsLiveTile = new LiveTile();
            rAndBNewsLiveTile.Key = "RAndBNewsTile";
            rAndBNewsLiveTile.Appearance.Normal.BackColor = NewsBackColor;

            #region Animation Frames

            #region Wide Animation Frames

            #region Wide Frame 1
            LiveTileFrameWide rAndBNewsWideFrame = new LiveTileFrameWide();
            rAndBNewsWideFrame.Animation = TileFrameAnimation.Fade;
            rAndBNewsWideFrame.Interval = new TimeSpan(0, 0, 11);
            LiveTileWideCustomContent liveTileRAndBNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileRAndBNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement rAndBHeaderText = new LiveTileContentTextElement();
            rAndBHeaderText.Text = Properties.Resources.RBNews;
            rAndBHeaderText.TextSize = TileTemplateTextSize.Heading;
            rAndBHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileRAndBNewsWideCustomContent.Elements.Add(rAndBHeaderText);

            LiveTileContentTextElement rAndBNewsText = new LiveTileContentTextElement();
            rAndBNewsText.Text = this.GetNews("RAndBNews1");
            rAndBNewsText.TextSize = TileTemplateTextSize.Normal;
            rAndBNewsText.Location = new System.Drawing.Point(10, 49);
            rAndBNewsText.MaxLines = 2;
            rAndBNewsText.Size = new System.Drawing.Size(200, 18);
            rAndBNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileRAndBNewsWideCustomContent.Elements.Add(rAndBNewsText);

            rAndBNewsWideFrame.Content = liveTileRAndBNewsWideCustomContent;
            rAndBNewsLiveTile.DefaultView.WideFrames.Add(rAndBNewsWideFrame);
            #endregion // Wide Frame 1

            #region Wide Frame 2
            rAndBNewsWideFrame = new LiveTileFrameWide();
            rAndBNewsWideFrame.Interval = new TimeSpan(0, 0, 5);
            liveTileRAndBNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileRAndBNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            rAndBHeaderText = new LiveTileContentTextElement();
            rAndBHeaderText.Text = Properties.Resources.RBNews;
            rAndBHeaderText.TextSize = TileTemplateTextSize.Heading;
            rAndBHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileRAndBNewsWideCustomContent.Elements.Add(rAndBHeaderText);

            rAndBNewsText = new LiveTileContentTextElement();
            rAndBNewsText.Text = this.GetNews("RAndBNews2");
            rAndBNewsText.TextSize = TileTemplateTextSize.Normal;
            rAndBNewsText.Location = new System.Drawing.Point(10, 49);
            rAndBNewsText.MaxLines = 2;
            rAndBNewsText.Size = new System.Drawing.Size(200, 18);
            rAndBNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileRAndBNewsWideCustomContent.Elements.Add(rAndBNewsText);

            rAndBNewsWideFrame.Content = liveTileRAndBNewsWideCustomContent;
            rAndBNewsLiveTile.DefaultView.WideFrames.Add(rAndBNewsWideFrame);
            #endregion // Wide Frame 2

            #region Wide Frame 3
            rAndBNewsWideFrame = new LiveTileFrameWide();
            rAndBNewsWideFrame.Interval = new TimeSpan(0, 0, 9);
            liveTileRAndBNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileRAndBNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            rAndBHeaderText = new LiveTileContentTextElement();
            rAndBHeaderText.Text = Properties.Resources.RBNews;
            rAndBHeaderText.TextSize = TileTemplateTextSize.Heading;
            rAndBHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileRAndBNewsWideCustomContent.Elements.Add(rAndBHeaderText);

            rAndBNewsText = new LiveTileContentTextElement();
            rAndBNewsText.Text = this.GetNews("RAndBNews3");
            rAndBNewsText.TextSize = TileTemplateTextSize.Normal;
            rAndBNewsText.Location = new System.Drawing.Point(10, 49);
            rAndBNewsText.MaxLines = 2;
            rAndBNewsText.Size = new System.Drawing.Size(200, 18);
            rAndBNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileRAndBNewsWideCustomContent.Elements.Add(rAndBNewsText);

            rAndBNewsWideFrame.Content = liveTileRAndBNewsWideCustomContent;
            rAndBNewsLiveTile.DefaultView.WideFrames.Add(rAndBNewsWideFrame);
            #endregion // Wide Frame 3

            #endregion // Wide Animation Frames

            #endregion Animation Frames

            rAndBNewsLiveTile.Sizing = TileSizing.Wide;
            rAndBNewsLiveTile.CurrentSize = TileSize.Wide;
            rAndBGroup.Tiles.Add(rAndBNewsLiveTile);
            #endregion // RAndBNews

            #region RAndBArtist4
            LiveTile rAndBArtist4LiveTile = new LiveTile();
            rAndBArtist4LiveTile.Key = "RAndBArtist4";

            #region SmallImage
            SetTileSmallImage(rAndBArtist4LiveTile, RAndBImagePath, 4, "RAndBArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(rAndBArtist4LiveTile, RAndBImagePath, 4, "RAndBArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium rAndBArtist4MediumFrame = new LiveTileFrameMedium();
            TileMediumImage rAndBArtist4MediumContent = new TileMediumImage();
            SetTileImage(rAndBArtist4MediumContent, RAndBImagePath, 4, "RAndBArtist");

            rAndBArtist4MediumFrame.Content = rAndBArtist4MediumContent;
            rAndBArtist4LiveTile.DefaultView.MediumFrames.Add(rAndBArtist4MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide rAndBArtist4WideFrame = new LiveTileFrameWide();
            TileWideImage rAndBArtist4WideContent = new TileWideImage();
            SetTileImage(rAndBArtist4WideContent, RAndBImagePath, 4, "RAndBArtist");

            rAndBArtist4WideFrame.Content = rAndBArtist4WideContent;
            rAndBArtist4LiveTile.DefaultView.WideFrames.Add(rAndBArtist4WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge rAndBArtist4LargeFrame = new LiveTileFrameLarge();
            TileLargeImage rAndBArtist4LargeContent = new TileLargeImage();
            SetTileImage(rAndBArtist4LargeContent, RAndBImagePath, 4, "RAndBArtist");

            rAndBArtist4LargeFrame.Content = rAndBArtist4LargeContent;
            rAndBArtist4LiveTile.DefaultView.LargeFrames.Add(rAndBArtist4LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            rAndBArtist4LiveTile.CurrentSize = TileSize.Wide;
            rAndBGroup.Tiles.Add(rAndBArtist4LiveTile);
            #endregion // RAndBArtist4

            #endregion // R and B

            #region Country
            TileGroup countryGroup = this.ultraLiveTileView1.Groups["Country"];

            #region CountryArtist1
            LiveTile countryArtist1LiveTile = new LiveTile();
            countryArtist1LiveTile.Key = "CountryArtist1";

            #region SmallImage
            SetTileSmallImage(countryArtist1LiveTile, CountryImagePath, 1, "CountryArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(countryArtist1LiveTile, CountryImagePath, 1, "CountryArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium countryArtist1MediumFrame = new LiveTileFrameMedium();
            TileMediumImage countryArtist1MediumContent = new TileMediumImage();
            SetTileImage(countryArtist1MediumContent, CountryImagePath, 1, "CountryArtist");

            countryArtist1MediumFrame.Content = countryArtist1MediumContent;
            countryArtist1LiveTile.DefaultView.MediumFrames.Add(countryArtist1MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide countryArtist1WideFrame = new LiveTileFrameWide();
            TileWideImage countryArtist1WideContent = new TileWideImage();
            SetTileImage(countryArtist1WideContent, CountryImagePath, 1, "CountryArtist");

            countryArtist1WideFrame.Content = countryArtist1WideContent;
            countryArtist1LiveTile.DefaultView.WideFrames.Add(countryArtist1WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge countryArtist1LargeFrame = new LiveTileFrameLarge();
            TileLargeImage countryArtist1LargeContent = new TileLargeImage();
            SetTileImage(countryArtist1LargeContent, CountryImagePath, 1, "CountryArtist");

            countryArtist1LargeFrame.Content = countryArtist1LargeContent;
            countryArtist1LiveTile.DefaultView.LargeFrames.Add(countryArtist1LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            countryArtist1LiveTile.CurrentSize = TileSize.Wide;
            countryGroup.Tiles.Add(countryArtist1LiveTile);
            #endregion // CountryArtist1

            #region CountryArtist2
            LiveTile countryArtist2LiveTile = new LiveTile();
            countryArtist2LiveTile.Key = "CountryArtist2";

            #region SmallImage
            SetTileSmallImage(countryArtist2LiveTile, CountryImagePath, 2, "CountryArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(countryArtist2LiveTile, CountryImagePath, 2, "CountryArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium countryArtist2MediumFrame = new LiveTileFrameMedium();
            TileMediumImage countryArtist2MediumContent = new TileMediumImage();
            SetTileImage(countryArtist2MediumContent, CountryImagePath, 2, "CountryArtist");

            countryArtist2MediumFrame.Content = countryArtist2MediumContent;
            countryArtist2LiveTile.DefaultView.MediumFrames.Add(countryArtist2MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide countryArtist2WideFrame = new LiveTileFrameWide();
            TileWideImage countryArtist2WideContent = new TileWideImage();
            SetTileImage(countryArtist2WideContent, CountryImagePath, 2, "CountryArtist");

            countryArtist2WideFrame.Content = countryArtist2WideContent;
            countryArtist2LiveTile.DefaultView.WideFrames.Add(countryArtist2WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge countryArtist2LargeFrame = new LiveTileFrameLarge();
            TileLargeImage countryArtist2LargeContent = new TileLargeImage();
            SetTileImage(countryArtist2LargeContent, CountryImagePath, 2, "CountryArtist");

            countryArtist2LargeFrame.Content = countryArtist2LargeContent;
            countryArtist2LiveTile.DefaultView.LargeFrames.Add(countryArtist2LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            countryArtist2LiveTile.CurrentSize = TileSize.Medium;
            countryGroup.Tiles.Add(countryArtist2LiveTile);
            #endregion // CountryArtist2

            #region CountryTracksSold
            LiveTile countryTracksSoldLiveTile = new LiveTile();
            countryTracksSoldLiveTile.Key = "CountryTracksSoldTile";
            countryTracksSoldLiveTile.Appearance.Normal.BackColor = TracksSoldBackColor;
            countryTracksSoldLiveTile.Appearance.Normal.FontData.Name = "Arial";

            #region Animation Frames

            #region Medium Animation Frames

            #region Medium Frame 1
            LiveTileFrameMedium countryTracksSoldMediumFrame = new LiveTileFrameMedium();
            countryTracksSoldMediumFrame.Interval = new TimeSpan(0, 0, 2);
            countryTracksSoldMediumFrame.Animation = TileFrameAnimation.Fade;
            LiveTileMediumCustomContent liveTileCountryMediumCustomContent = new LiveTileMediumCustomContent();
            liveTileCountryMediumCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement countryBlockText = new LiveTileContentTextElement();
            countryBlockText.Text = "70";
            countryBlockText.TextSize = TileTemplateTextSize.Block;
            countryBlockText.Location = new System.Drawing.Point(10, 10);
            liveTileCountryMediumCustomContent.Elements.Add(countryBlockText);

            LiveTileContentTextElement countryNormalText = new LiveTileContentTextElement();
            countryNormalText.Text = Properties.Resources.TracksSold;
            countryNormalText.TextSize = TileTemplateTextSize.Normal;
            countryNormalText.Location = new System.Drawing.Point(10, 85);
            liveTileCountryMediumCustomContent.Elements.Add(countryNormalText);

            countryTracksSoldMediumFrame.Content = liveTileCountryMediumCustomContent;
            countryTracksSoldLiveTile.DefaultView.MediumFrames.Add(countryTracksSoldMediumFrame);
            #endregion // Medium Frame 1

            #region Medium Frame 2
            countryTracksSoldMediumFrame = new LiveTileFrameMedium();
            countryTracksSoldMediumFrame.Interval = new TimeSpan(0, 0, 7);
            liveTileCountryMediumCustomContent = new LiveTileMediumCustomContent();
            liveTileCountryMediumCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            countryBlockText = new LiveTileContentTextElement();
            countryBlockText.Text = "88";
            countryBlockText.TextSize = TileTemplateTextSize.Block;
            countryBlockText.Location = new System.Drawing.Point(10, 10);
            liveTileCountryMediumCustomContent.Elements.Add(countryBlockText);

            countryNormalText = new LiveTileContentTextElement();
            countryNormalText.Text = Properties.Resources.TracksSold;
            countryNormalText.TextSize = TileTemplateTextSize.Normal;
            countryNormalText.Location = new System.Drawing.Point(10, 85);
            liveTileCountryMediumCustomContent.Elements.Add(countryNormalText);

            countryTracksSoldMediumFrame.Content = liveTileCountryMediumCustomContent;
            countryTracksSoldLiveTile.DefaultView.MediumFrames.Add(countryTracksSoldMediumFrame);
            #endregion //Medium Frame 2

            #endregion // Medium Animation Frames

            #endregion Animation Frames

            countryTracksSoldLiveTile.Sizing = TileSizing.Medium;
            countryTracksSoldLiveTile.CurrentSize = TileSize.Medium;
            countryGroup.Tiles.Add(countryTracksSoldLiveTile);
            #endregion // CountryTracksSold

            #region CountryArtist3
            LiveTile countryArtist3LiveTile = new LiveTile();
            countryArtist3LiveTile.Key = "CountryArtist3";

            #region SmallImage
            SetTileSmallImage(countryArtist3LiveTile, CountryImagePath, 3, "CountryArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(countryArtist3LiveTile, CountryImagePath, 3, "CountryArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium countryArtist3MediumFrame = new LiveTileFrameMedium();
            TileMediumImage countryArtist3MediumContent = new TileMediumImage();
            SetTileImage(countryArtist3MediumContent, CountryImagePath, 3, "CountryArtist");

            countryArtist3MediumFrame.Content = countryArtist3MediumContent;
            countryArtist3LiveTile.DefaultView.MediumFrames.Add(countryArtist3MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide countryArtist3WideFrame = new LiveTileFrameWide();
            TileWideImage countryArtist3WideContent = new TileWideImage();
            SetTileImage(countryArtist3WideContent, CountryImagePath, 3, "CountryArtist");

            countryArtist3WideFrame.Content = countryArtist3WideContent;
            countryArtist3LiveTile.DefaultView.WideFrames.Add(countryArtist3WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge countryArtist3LargeFrame = new LiveTileFrameLarge();
            TileLargeImage countryArtist3LargeContent = new TileLargeImage();
            SetTileImage(countryArtist3LargeContent, CountryImagePath, 3, "CountryArtist");

            countryArtist3LargeFrame.Content = countryArtist3LargeContent;
            countryArtist3LiveTile.DefaultView.LargeFrames.Add(countryArtist3LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            countryArtist3LiveTile.CurrentSize = TileSize.Wide;
            countryGroup.Tiles.Add(countryArtist3LiveTile);
            #endregion // CountryArtist3

            #region CountryArtist4
            LiveTile countryArtist4LiveTile = new LiveTile();
            countryArtist4LiveTile.Key = "CountryArtist4";

            #region SmallImage
            SetTileSmallImage(countryArtist4LiveTile, CountryImagePath, 4, "CountryArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(countryArtist4LiveTile, CountryImagePath, 4, "CountryArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium countryArtist4MediumFrame = new LiveTileFrameMedium();
            TileMediumImage countryArtist4MediumContent = new TileMediumImage();
            SetTileImage(countryArtist4MediumContent, CountryImagePath, 4, "CountryArtist");

            countryArtist4MediumFrame.Content = countryArtist4MediumContent;
            countryArtist4LiveTile.DefaultView.MediumFrames.Add(countryArtist4MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide countryArtist4WideFrame = new LiveTileFrameWide();
            TileWideImage countryArtist4WideContent = new TileWideImage();
            SetTileImage(countryArtist4WideContent, CountryImagePath, 4, "CountryArtist");

            countryArtist4WideFrame.Content = countryArtist4WideContent;
            countryArtist4LiveTile.DefaultView.WideFrames.Add(countryArtist4WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge countryArtist4LargeFrame = new LiveTileFrameLarge();
            TileLargeImage countryArtist4LargeContent = new TileLargeImage();
            SetTileImage(countryArtist4LargeContent, CountryImagePath, 4, "CountryArtist");

            countryArtist4LargeFrame.Content = countryArtist4LargeContent;
            countryArtist4LiveTile.DefaultView.LargeFrames.Add(countryArtist4LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            countryArtist4LiveTile.CurrentSize = TileSize.Wide;
            countryGroup.Tiles.Add(countryArtist4LiveTile);
            #endregion // CountryArtist4

            #region CountryNews
            LiveTile countryNewsLiveTile = new LiveTile();
            countryNewsLiveTile.Key = "CountryNewsTile";
            countryNewsLiveTile.Appearance.Normal.BackColor = NewsBackColor;

            #region Animation Frames

            #region Wide Animation Frames

            #region Wide Frame 1
            LiveTileFrameWide countryNewsWideFrame = new LiveTileFrameWide();
            countryNewsWideFrame.Animation = TileFrameAnimation.Fade;
            countryNewsWideFrame.Interval = new TimeSpan(0, 0, 7);
            LiveTileWideCustomContent liveTileCountryNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileCountryNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            LiveTileContentTextElement countryHeaderText = new LiveTileContentTextElement();
            countryHeaderText.Text = Properties.Resources.CountryNews;
            countryHeaderText.TextSize = TileTemplateTextSize.Heading;
            countryHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileCountryNewsWideCustomContent.Elements.Add(countryHeaderText);

            LiveTileContentTextElement countryNewsText = new LiveTileContentTextElement();
            countryNewsText.Text = this.GetNews("CountryNews1");
            countryNewsText.TextSize = TileTemplateTextSize.Normal;
            countryNewsText.Location = new System.Drawing.Point(10, 49);
            countryNewsText.MaxLines = 2;
            countryNewsText.Size = new System.Drawing.Size(200, 18);
            countryNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileCountryNewsWideCustomContent.Elements.Add(countryNewsText);

            countryNewsWideFrame.Content = liveTileCountryNewsWideCustomContent;
            countryNewsLiveTile.DefaultView.WideFrames.Add(countryNewsWideFrame);
            #endregion // Wide Frame 1

            #region Wide Frame 2
            countryNewsWideFrame = new LiveTileFrameWide();
            countryNewsWideFrame.Interval = new TimeSpan(0, 0, 12);
            liveTileCountryNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileCountryNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            countryHeaderText = new LiveTileContentTextElement();
            countryHeaderText.Text = Properties.Resources.CountryNews;
            countryHeaderText.TextSize = TileTemplateTextSize.Heading;
            countryHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileCountryNewsWideCustomContent.Elements.Add(countryHeaderText);

            countryNewsText = new LiveTileContentTextElement();
            countryNewsText.Text = this.GetNews("CountryNews2");
            countryNewsText.TextSize = TileTemplateTextSize.Normal;
            countryNewsText.Location = new System.Drawing.Point(10, 49);
            countryNewsText.MaxLines = 2;
            countryNewsText.Size = new System.Drawing.Size(200, 18);
            countryNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileCountryNewsWideCustomContent.Elements.Add(countryNewsText);

            countryNewsWideFrame.Content = liveTileCountryNewsWideCustomContent;
            countryNewsLiveTile.DefaultView.WideFrames.Add(countryNewsWideFrame);
            #endregion // Wide Frame 2

            #region Wide Frame 3
            countryNewsWideFrame = new LiveTileFrameWide();
            countryNewsWideFrame.Interval = new TimeSpan(0, 0, 6);
            liveTileCountryNewsWideCustomContent = new LiveTileWideCustomContent();
            liveTileCountryNewsWideCustomContent.DesignMetrics.ResolutionScale = ResolutionScale.Scale80Percent;

            countryHeaderText = new LiveTileContentTextElement();
            countryHeaderText.Text = Properties.Resources.CountryNews;
            countryHeaderText.TextSize = TileTemplateTextSize.Heading;
            countryHeaderText.Location = new System.Drawing.Point(10, 10);
            liveTileCountryNewsWideCustomContent.Elements.Add(countryHeaderText);

            countryNewsText = new LiveTileContentTextElement();
            countryNewsText.Text = this.GetNews("CountryNews3");
            countryNewsText.TextSize = TileTemplateTextSize.Normal;
            countryNewsText.Location = new System.Drawing.Point(10, 49);
            countryNewsText.MaxLines = 2;
            countryNewsText.Size = new System.Drawing.Size(200, 18);
            countryNewsText.Appearance.TextTrimming = TextTrimming.None;
            liveTileCountryNewsWideCustomContent.Elements.Add(countryNewsText);

            countryNewsWideFrame.Content = liveTileCountryNewsWideCustomContent;
            countryNewsLiveTile.DefaultView.WideFrames.Add(countryNewsWideFrame);
            #endregion // Wide Frame 3

            #endregion // Wide Animation Frames

            #endregion Animation Frames

            countryNewsLiveTile.Sizing = TileSizing.Wide;
            countryNewsLiveTile.CurrentSize = TileSize.Wide;
            countryGroup.Tiles.Add(countryNewsLiveTile);
            #endregion // CountryNews

            #region CountryArtist5
            LiveTile countryArtist5LiveTile = new LiveTile();
            countryArtist5LiveTile.Key = "CountryArtist5";

            #region SmallImage
            SetTileSmallImage(countryArtist5LiveTile, CountryImagePath, 5, "CountryArtist");
            #endregion // SmallImage

            #region CollapsedImage
            SetTileCollapsedImage(countryArtist5LiveTile, CountryImagePath, 5, "CountryArtist");
            #endregion // CollapsedImage

            #region Animation Frames

            #region Medium Animation Frames
            LiveTileFrameMedium countryArtist5MediumFrame = new LiveTileFrameMedium();
            TileMediumImage countryArtist5MediumContent = new TileMediumImage();
            SetTileImage(countryArtist5MediumContent, CountryImagePath, 5, "CountryArtist");

            countryArtist5MediumFrame.Content = countryArtist5MediumContent;
            countryArtist5LiveTile.DefaultView.MediumFrames.Add(countryArtist5MediumFrame);
            #endregion // Medium Animation Frames

            #region Wide Animation Frames
            LiveTileFrameWide countryArtist5WideFrame = new LiveTileFrameWide();
            TileWideImage countryArtist5WideContent = new TileWideImage();
            SetTileImage(countryArtist5WideContent, CountryImagePath, 5, "CountryArtist");

            countryArtist5WideFrame.Content = countryArtist5WideContent;
            countryArtist5LiveTile.DefaultView.WideFrames.Add(countryArtist5WideFrame);
            #endregion // Wide Animation Frames

            #region Large Animation Frames
            LiveTileFrameLarge countryArtist5LargeFrame = new LiveTileFrameLarge();
            TileLargeImage countryArtist5LargeContent = new TileLargeImage();
            SetTileImage(countryArtist5LargeContent, CountryImagePath, 5, "CountryArtist");

            countryArtist5LargeFrame.Content = countryArtist5LargeContent;
            countryArtist5LiveTile.DefaultView.LargeFrames.Add(countryArtist5LargeFrame);
            #endregion // Large Animation Frames

            #endregion // Animation Frames

            countryArtist5LiveTile.CurrentSize = TileSize.Wide;
            countryGroup.Tiles.Add(countryArtist5LiveTile);
            #endregion // CountryArtist5

            #endregion // Country
        }

        #endregion

        #region AddRandomArtistAnimationFrames
        private void AddRandomArtistAnimationFrames()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int randomArtistCount = 3;
            for (int i = 0; i < randomArtistCount; i++)
            {
                #region Random Live Tile

                int group = rand.Next(1, 6);
                TileGroup tileGroup = null;
                int artistNum = 0;
                LiveTile liveTile = null;
                string artistPrefix = "RandomArtist";
                switch (group)
                {
                    case 1:
                        tileGroup = this.ultraLiveTileView1.Groups["TopPerformingArtists"];
                        artistNum = rand.Next(1, 5);
                        liveTile = tileGroup.Tiles["TopArtist" + artistNum] as LiveTile;
                        while (!IsLiveTileGoodForRandomFrame(liveTile))
                        {
                            artistNum = rand.Next(1, 5);
                            liveTile = tileGroup.Tiles["TopArtist" + artistNum] as LiveTile;
                        }
                        break;
                    case 2:
                        tileGroup = this.ultraLiveTileView1.Groups["Pop"];
                        artistNum = rand.Next(1, 6);
                        liveTile = tileGroup.Tiles["PopArtist" + artistNum] as LiveTile;
                        while (!IsLiveTileGoodForRandomFrame(liveTile))
                        {
                            artistNum = rand.Next(1, 6);
                            liveTile = tileGroup.Tiles["PopArtist" + artistNum] as LiveTile;
                        }
                        break;
                    case 3:
                        tileGroup = this.ultraLiveTileView1.Groups["Rock"];
                        artistNum = rand.Next(1, 5);
                        liveTile = tileGroup.Tiles["RockArtist" + artistNum] as LiveTile;
                        while (!IsLiveTileGoodForRandomFrame(liveTile))
                        {
                            artistNum = rand.Next(1, 5);
                            liveTile = tileGroup.Tiles["RockArtist" + artistNum] as LiveTile;
                        }
                        break;
                    case 4:
                        tileGroup = this.ultraLiveTileView1.Groups["RAndB"];
                        artistNum = rand.Next(1, 5);
                        liveTile = tileGroup.Tiles["RAndBArtist" + artistNum] as LiveTile;
                        while (!IsLiveTileGoodForRandomFrame(liveTile))
                        {
                            artistNum = rand.Next(1, 5);
                            liveTile = tileGroup.Tiles["RAndBArtist" + artistNum] as LiveTile;    
                        }
                        break;
                    case 5:
                        tileGroup = this.ultraLiveTileView1.Groups["Country"];
                        artistNum = rand.Next(1, 6);
                        liveTile = tileGroup.Tiles["CountryArtist" + artistNum] as LiveTile;
                        while (!IsLiveTileGoodForRandomFrame(liveTile))
                        {
                            artistNum = rand.Next(1, 6);
                            liveTile = tileGroup.Tiles["CountryArtist" + artistNum] as LiveTile;
                        }
                        break;
                    default:
                        break;
                }

                if (tileGroup == null || artistNum == 0 || liveTile == null)
                {
                    continue;
                }

                #endregion // Random Live Tile

                #region AnimationFrames

                liveTile.FrameAnimation = TileFrameAnimation.Fade;

                #region Medium Animation Frames

                LiveTileFrameMedium randomArtist1MediumFrame = new LiveTileFrameMedium();
                TileMediumImage randomArtist1MediumContent = new TileMediumImage();
                SetTileImage(randomArtist1MediumContent, RandomArtistImagePath, i + 1, artistPrefix);

                randomArtist1MediumFrame.Content = randomArtist1MediumContent;
                liveTile.DefaultView.MediumFrames.Add(randomArtist1MediumFrame);

                #endregion // Medium Animation Frames

                #region Wide Animation Frames

                LiveTileFrameWide randomArtist1WideFrame = new LiveTileFrameWide();
                TileWideImage randomArtist1WideContent = new TileWideImage();
                SetTileImage(randomArtist1WideContent, RandomArtistImagePath, i + 1, artistPrefix);

                randomArtist1WideFrame.Content = randomArtist1WideContent;
                liveTile.DefaultView.WideFrames.Add(randomArtist1WideFrame);

                #endregion // Wide Animation Frames

                #region Large Animation Frames

                LiveTileFrameLarge randomArtist1LargeFrame = new LiveTileFrameLarge();
                TileLargeImage randomArtist1LargeContent = new TileLargeImage();
                SetTileImage(randomArtist1LargeContent, RandomArtistImagePath, i + 1, artistPrefix);

                randomArtist1LargeFrame.Content = randomArtist1LargeContent;
                liveTile.DefaultView.LargeFrames.Add(randomArtist1LargeFrame);

                #endregion // Large Animation Frames

                #endregion // AnimationFrames
            }
        }

        #region Random Live Tile helper
        private bool IsLiveTileGoodForRandomFrame(LiveTile liveTile)
        {
            switch (liveTile.CurrentSizeResolved)
            {
                case TileSize.Small:
                    return false;
                case TileSize.Medium:
                    return liveTile.DefaultView.MediumFrames.Count <= 1;
                case TileSize.Wide:
                    return liveTile.DefaultView.WideFrames.Count <= 1;
                case TileSize.Large:
                    return liveTile.DefaultView.LargeFrames.Count <= 1;
                default:
                    return false;
            }
        }
        #endregion // Random Live Tile helper

        #endregion // AddRandomArtistAnimationFrames

        #region SetTileImage Helpers

        private void SetTileSmallImage(LiveTile artistLiveTile, string imagePath, int artistIndex, string artistPrefix)
        {
            artistLiveTile.SmallImage.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Small", artistPrefix + artistIndex + Small100PercentImageSuffix);
            artistLiveTile.SmallImage.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Small", artistPrefix + artistIndex + Small140PercentImageSuffix);
            artistLiveTile.SmallImage.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Small", artistPrefix + artistIndex + Small180PercentImageSuffix);
            artistLiveTile.SmallImage.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Small", artistPrefix + artistIndex + Small80PercentImageSuffix);
        }

        private void SetTileCollapsedImage(LiveTile artistLiveTile, string imagePath, int artistIndex, string artistPrefix)
        {
            artistLiveTile.CollapsedImage.Image = Utilities.GetImageFromResource(imagePath + ".Collapsed", artistPrefix + artistIndex + CollapsedImageSuffix);
        }

        private void SetTileImage(TileMediumImage mediumContent, string imagePath, int artistIndex, string artistPrefix)
        {
            mediumContent.Image.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Medium", artistPrefix + artistIndex + Medium100PercentImageSuffix);
            mediumContent.Image.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Medium", artistPrefix + artistIndex + Medium140PercentImageSuffix);
            mediumContent.Image.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Medium", artistPrefix + artistIndex + Medium180PercentImageSuffix);
            mediumContent.Image.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Medium", artistPrefix + artistIndex + Medium80PercentImageSuffix);
        }

        private void SetTileImage(TileWideImage wideContent, string imagePath, int artistIndex, string artistPrefix)
        {
            wideContent.Image.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", artistPrefix + artistIndex + Wide100PercentImageSuffix);
            wideContent.Image.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", artistPrefix + artistIndex + Wide140PercentImageSuffix);
            wideContent.Image.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", artistPrefix + artistIndex + Wide180PercentImageSuffix);
            wideContent.Image.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", artistPrefix + artistIndex + Wide80PercentImageSuffix);
        }

        private void SetTileImage(TileLargeImage largeContent, string imagePath, int artistIndex, string artistPrefix)
        {

            largeContent.Image.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", artistPrefix + artistIndex + Large100PercentImageSuffix);
            largeContent.Image.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", artistPrefix + artistIndex + Large140PercentImageSuffix);
            largeContent.Image.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", artistPrefix + artistIndex + Large180PercentImageSuffix);
            largeContent.Image.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", artistPrefix + artistIndex + Large80PercentImageSuffix);
        }

        private void SetTileImageCollection(TileWideImageCollection imageCollectionContentWide, string imagePath, int artistIndex, string artistPrefix)
        {
            imageCollectionContentWide.ImageMain.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageMain" + artistPrefix + artistIndex + WideImageMain100PercentImageSuffix);
            imageCollectionContentWide.ImageMain.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageMain" + artistPrefix + artistIndex + WideImageMain140PercentImageSuffix);
            imageCollectionContentWide.ImageMain.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageMain" + artistPrefix + artistIndex + WideImageMain180PercentImageSuffix);
            imageCollectionContentWide.ImageMain.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageMain" + artistPrefix + artistIndex + WideImageMain80PercentImageSuffix);

            imageCollectionContentWide.ImageSmallCol1Row1.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol1Row1" + artistPrefix + artistIndex + Small100PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol1Row1.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol1Row1" + artistPrefix + artistIndex + Small140PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol1Row1.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol1Row1" + artistPrefix + artistIndex + Small180PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol1Row1.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol1Row1" + artistPrefix + artistIndex + Small80PercentImageSuffix);
            
            imageCollectionContentWide.ImageSmallCol1Row2.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol1Row2" + artistPrefix + artistIndex + Small100PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol1Row2.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol1Row2" + artistPrefix + artistIndex + Small140PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol1Row2.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol1Row2" + artistPrefix + artistIndex + Small180PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol1Row2.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol1Row2" + artistPrefix + artistIndex + Small80PercentImageSuffix);

            imageCollectionContentWide.ImageSmallCol2Row1.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol2Row1" + artistPrefix + artistIndex + Small100PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol2Row1.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol2Row1" + artistPrefix + artistIndex + Small140PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol2Row1.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol2Row1" + artistPrefix + artistIndex + Small180PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol2Row1.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol2Row1" + artistPrefix + artistIndex + Small80PercentImageSuffix);

            imageCollectionContentWide.ImageSmallCol2Row2.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol2Row2" + artistPrefix + artistIndex + Small100PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol2Row2.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol2Row2" + artistPrefix + artistIndex + Small140PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol2Row2.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol2Row2" + artistPrefix + artistIndex + Small180PercentImageSuffix);
            imageCollectionContentWide.ImageSmallCol2Row2.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Wide", "ImageSmallCol2Row2" + artistPrefix + artistIndex + Small80PercentImageSuffix);
        }

        private void SetTileImageCollection(TileLargeImageCollection imageCollectionContentLarge, string imagePath, int artistIndex, string artistPrefix)
        {
            imageCollectionContentLarge.ImageMain.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageMain" + artistPrefix + artistIndex + Large100PercentImageSuffix);
            imageCollectionContentLarge.ImageMain.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageMain" + artistPrefix + artistIndex + Large140PercentImageSuffix);
            imageCollectionContentLarge.ImageMain.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageMain" + artistPrefix + artistIndex + Large180PercentImageSuffix);
            imageCollectionContentLarge.ImageMain.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageMain" + artistPrefix + artistIndex + Large80PercentImageSuffix);

            imageCollectionContentLarge.ImageSmallCol1Row1.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol1Row1" + artistPrefix + artistIndex + SmallImageCollectionEdge100PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol1Row1.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol1Row1" + artistPrefix + artistIndex + SmallImageCollectionEdge140PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol1Row1.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol1Row1" + artistPrefix + artistIndex + SmallImageCollectionEdge180PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol1Row1.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol1Row1" + artistPrefix + artistIndex + SmallImageCollectionEdge80PercentImageSuffix);

            imageCollectionContentLarge.ImageSmallCol2Row1.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol2Row1" + artistPrefix + artistIndex + SmallImageCollectionInside100PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol2Row1.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol2Row1" + artistPrefix + artistIndex + SmallImageCollectionInside140PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol2Row1.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol2Row1" + artistPrefix + artistIndex + SmallImageCollectionInside180PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol2Row1.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol2Row1" + artistPrefix + artistIndex + SmallImageCollectionInside80PercentImageSuffix);

            imageCollectionContentLarge.ImageSmallCol3Row1.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol3Row1" + artistPrefix + artistIndex + SmallImageCollectionInside100PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol3Row1.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol3Row1" + artistPrefix + artistIndex + SmallImageCollectionInside140PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol3Row1.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol3Row1" + artistPrefix + artistIndex + SmallImageCollectionInside180PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol3Row1.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol3Row1" + artistPrefix + artistIndex + SmallImageCollectionInside80PercentImageSuffix);

            imageCollectionContentLarge.ImageSmallCol4Row1.Scale100Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol4Row1" + artistPrefix + artistIndex + SmallImageCollectionEdge100PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol4Row1.Scale140Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol4Row1" + artistPrefix + artistIndex + SmallImageCollectionEdge140PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol4Row1.Scale180Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol4Row1" + artistPrefix + artistIndex + SmallImageCollectionEdge180PercentImageSuffix);
            imageCollectionContentLarge.ImageSmallCol4Row1.Scale80Percent.Image = Utilities.GetImageFromResource(imagePath + ".Large", "ImageSmallCol4Row1" + artistPrefix + artistIndex + SmallImageCollectionEdge80PercentImageSuffix);
        }

        #endregion // SetTileImage Helpers

        #region GetGridData
        private DataSet GetGridData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Tracks");
            dt.Columns.Add("TrackTitle", typeof(string));
            dt.Columns.Add("Duration", typeof(string));
            dt.Columns.Add("AlbumName", typeof(string));
            dt.Columns.Add("MonthlyDownloads", typeof(int));
            dt.Columns.Add("TotalDownloads", typeof(int));

            DataRow dr = null;
            for (int i = 0; i < 15; i++)
            {
                dr = dt.NewRow();
                dr[0] = GetTrackTitle(i);
                dr[1] = GetTrackDuration(i);
                dr[2] = GetAlbumName(i);
                dr[3] = GetMonthlyDownloads(i);
                dr[4] = GetTotalDownloads(i);
                dt.Rows.Add(dr);
            }

            ds.Tables.Add(dt);
            return ds;
        }
        #endregion // GetGridData

        #region InitGrid
        private void InitGrid()
        {
            this.ultraGrid1.Location = new System.Drawing.Point(31, 85);
            System.Drawing.Size gridSize = new System.Drawing.Size(842, 549);
            this.ultraGrid1.MaximumSize = gridSize;
            this.ultraGrid1.MinimumSize = gridSize;
            this.ultraGrid1.Size = gridSize;
        }
        #endregion // InitGrid

        #region GetChartData

        private DataSet GetChartData()
        {
            DataSet ds = new DataSet("Downloads");
            int artistCount = this.Artists.Count;
            int max = MonthlyDownloads.Count;
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < artistCount; i++)
            {
                DataTable dt = new DataTable(Artists[i]);
                dt.Columns.Add("Month", typeof(string));
                dt.Columns.Add("MonthlyDownloads", typeof(int));

                DataRow dr = null;
                
                dr = dt.NewRow();
                dr[0] = this.Months[0];
                dr[1] = GetMonthlyDownloads(rand.Next(0, max));
                dt.Rows.Add(dr);

                for (int j = 1; j < 12; j++)
                {
                    dr = dt.NewRow();
                    dr[0] = this.Months[j];
                    dr[1] = Math.Max(10,(int)dt.Rows[j - 1][1] + rand.Next(-50, 50));
                    dt.Rows.Add(dr);
                }

                ds.Tables.Add(dt);
            }
            return ds;
        }

        #region GetChartData Helpers
        private List<string> months = null;
        private List<string> Months
        {
            get 
            {
                if (this.months == null)
                 {
                     string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
                     this.months = monthNames.Skip(DateTime.Today.Month)
                                .Concat(monthNames.Take(DateTime.Today.Month))
                                .Where(monthName => !String.IsNullOrEmpty(monthName))
                                .ToList();
                 }
                return this.months;
            }
        }

        private List<string> artists = null;
        private List<string> Artists
        {
            get {
                return this.artists ??
                       (this.artists =
                        new List<string>() {Utilities.GetLocalizedString("skittl3z"), Utilities.GetLocalizedString("Purple Gorillas"), Utilities.GetLocalizedString("Screeming Frogs"), Utilities.GetLocalizedString("Mazy Star")});
            }
        }
        #endregion //GetChartData Helpers

        #endregion // GetChartData

        #region InitChart
        private void InitChart()
        {
            
            this.ultraDataChart1.Series.Clear();

            // NumericSeries numericSeries = null;
            int artistCount = this.Artists.Count;
            DataSet ds = this.GetChartData();


            var yAxis = new NumericYAxis();
            yAxis.LabelExtent = 80;

            var xAxis = new CategoryXAxis()
            {
                DataSource = ds.Tables[0],
                Label = "Month"

            };
            xAxis.LabelExtent = 80;
            xAxis.LabelTextColor = Color.White;
            yAxis.LabelTextColor = Color.White;

            xAxis.LabelFontSize = 15D;
            yAxis.LabelFontSize = 15D;

            xAxis.LabelLocation = AxisLabelsLocation.OutsideBottom;
            yAxis.LabelLocation = AxisLabelsLocation.OutsideBottom;

            yAxis.Interval = 50;

            this.legend.Visible = true;            
            this.legend.BackColor = Color.Transparent;
            this.legend.ForeColor = Color.White;
            this.legend.Location = new Infragistics.Win.DataVisualization.Point(600, 0);
            this.legend.Size = new System.Drawing.Size(150, 150);
            this.legend.BorderStyle = BorderStyle.FixedSingle;         
            
            for (int i = 0; i < artistCount; i++)
            {
                var series = new SplineSeries()
                {
                    DataSource = ds.Tables[this.Artists[i]],
                    ValueMemberPath = "MonthlyDownloads",
                    Title = ds.Tables[this.Artists[i]],// "Month",
                    XAxis = xAxis,
                    YAxis = yAxis,
                    Thickness =3,
                };

                this.ultraDataChart1.Axes.Add(xAxis);
                this.ultraDataChart1.Axes.Add(yAxis);
                this.ultraDataChart1.Series.Add(series);

                this.ultraDataChart1.Legend = legend;               
            }

            this.ultraDataChart1.BackColor = Color.Transparent;          
            this.ultraDataChart1.Title = Properties.Resources.MonthlyDownloads;
            this.ultraDataChart1.TitleFontSize = 15D;
            this.ultraDataChart1.TitleFontFamily = "Arial";
            this.ultraDataChart1.TitleTextColor = Color.White;
            this.ultraDataChart1.TitleHorizontalAlignment = Infragistics.Portable.Components.UI.HorizontalAlignment.Center;            

            if (!chartInitialized)
            {             
                this.ultraDataChart1.Location = new System.Drawing.Point(31, 83);
                System.Drawing.Size chartSize = new System.Drawing.Size(842, 696);
                this.ultraDataChart1.MaximumSize = chartSize;
                this.ultraDataChart1.MinimumSize = chartSize;
                this.ultraDataChart1.Size = chartSize;
                chartInitialized = true;
            }
          
        }
        #endregion // InitChart

        #region InitLiveTileView

        private void InitLiveTileView()
        {
            this.ultraLiveTileView1.Appearance.FontData.Name = "Arial";
            
            this.ultraLiveTileView1.Groups["TopPerformingArtists"].Text = Properties.Resources.TopPerformingArtists;
            this.ultraLiveTileView1.Groups["Pop"].Text = Properties.Resources.Pop;
            this.ultraLiveTileView1.Groups["Rock"].Text = Properties.Resources.Rock;
            this.ultraLiveTileView1.Groups["RAndB"].Text = Properties.Resources.RAndB;
            this.ultraLiveTileView1.Groups["Country"].Text = Properties.Resources.Country;

            this.ultraLiveTileView1.ApplicationBar.ButtonAppearance.HotTracking.ForeColor = Color.FromArgb(146, 192, 224);

            this.ultraLiveTileView1.DrawFilter = new LiveTileViewDrawFilter();
        }

        #endregion // InitLiveTileView

        #region RandomizeChartData
        private bool chartInitialized = false;
        private void RandomizeChartData(object sender, EventArgs e)
        {
            this.InitChart();
        }
        #endregion // RandomizeChartData

        #region RandomizeGridData
        private void RandomizeGridData(object sender, EventArgs e)
        {
            this.ultraGrid1.Rows.Refresh(RefreshRow.FireInitializeRow);
        }
        #endregion // RandomizeGridData

        #region InitDictionaries

        private void InitDictionaries()
        {
            TrackTitles = new Dictionary<int, string>(15)
                              {
                                  {0, "Code of Silence"},
                                  {1, "Dragon Beat"},
                                  {2, "The Beast Of Riverdale"},
                                  {3, "Slammer"},
                                  {4, "Glimmer Forest"},
                                  {5, "The Leaf"},
                                  {6, "Big Flow"},
                                  {7, "Pocket Cats"},
                                  {8, "The Concept"},
                                  {9, "Ocean ABC"},
                                  {10, "The Grades"},
                                  {11, "Night Sky Woman"},
                                  {12, "The Basket"},
                                  {13, "Commoners"},
                                  {14, "Luna Pattern"}
                              };

            TrackDurations = new Dictionary<int, string>(15)
                              {
                                  {0, "2:35"},
                                  {1, "1:58"},
                                  {2, "2:36"},
                                  {3, "3:20"},
                                  {4, "2:25"},
                                  {5, "1:90"},
                                  {6, "2:32"},
                                  {7, "2:00"},
                                  {8, "1:56"},
                                  {9, "2:12"},
                                  {10, "2:46"},
                                  {11, "3:11"},
                                  {12, "2:44"},
                                  {13, "3:21"},
                                  {14, "1:46"}
                              };

            AlbumNames = new Dictionary<int, string>(15)
                              {
                                  {0, "The Failure"},
                                  {1, "Star Forum"},
                                  {2, "Star Forum"},
                                  {3, "Star Forum"},
                                  {4, "The Failure"},
                                  {5, "The Failure"},
                                  {6, "Silver Bully"},
                                  {7, "Star Forum"},
                                  {8, "The Failure"},
                                  {9, "Silver Bully"},
                                  {10, "Star Forum"},
                                  {11, "Star Forum"},
                                  {12, "The Failure"},
                                  {13, "Cloudy Hero"},
                                  {14, "Silver Bully"}
                              };

            MonthlyDownloads = new Dictionary<int, int>(15)
                              {
                                  {0, 223},
                                  {1, 221},
                                  {2, 219},
                                  {3, 216},
                                  {4, 200},
                                  {5, 198},
                                  {6, 175},
                                  {7, 168},
                                  {8, 153},
                                  {9, 142},
                                  {10, 135},
                                  {11, 132},
                                  {12, 120},
                                  {13, 99},
                                  {14, 78}
                              };

            TotalDownloads = new Dictionary<int, int>(15)
                              {
                                  {0, 290},
                                  {1, 320},
                                  {2, 266},
                                  {3, 280},
                                  {4, 230},
                                  {5, 245},
                                  {6, 190},
                                  {7, 188},
                                  {8, 220},
                                  {9, 216},
                                  {10, 155},
                                  {11, 176},
                                  {12, 145},
                                  {13, 311},
                                  {14, 120}
                              };

            NewsDictionary = new Dictionary<string, string>(15)
                                {
                                    {"TopNews1", Utilities.GetLocalizedString("Purple Mountain Gorillas Appearing Live in NYC this month.")},
                                    {"TopNews2", Utilities.GetLocalizedString("Tickets for the final tour of music legends go on sale soon.")},
                                    {"TopNews3", Utilities.GetLocalizedString("Tonight: Stars prepare to rock the red carpet!")},
                                    {"PopNews1", Utilities.GetLocalizedString("Mazy Star tops the climbing charts two weeks in a row.")},
                                    {"PopNews2", Utilities.GetLocalizedString("Ninth Forward cancels shows after singer leaves.")},
                                    {"PopNews3", Utilities.GetLocalizedString("Several groups collaborate to give benefit show.")},
                                    {"RockNews1", Utilities.GetLocalizedString("Screeming Frogs tear up Downtown LA Club with surprise performance.")},
                                    {"RockNews2", Utilities.GetLocalizedString("Review: Hungry Pumpkin delivers great flavor in latest album.")},
                                    {"RockNews3", Utilities.GetLocalizedString("Snow of Oatmeal brings in new members for upcoming tour.")},
                                    {"RAndBNews1", Utilities.GetLocalizedString("South's tour begins tonight in Atlanta.")},
                                    {"RAndBNews2", Utilities.GetLocalizedString("Sinister Piano continues to amaze.")},
                                    {"RAndBNews3", Utilities.GetLocalizedString("Creek Orchestra planning new album.")},
                                    {"CountryNews1", Utilities.GetLocalizedString("Awards continue to pile up for Outfitter Nimble.")},
                                    {"CountryNews2", Utilities.GetLocalizedString("Gray Fixation is anything but Gray.")},
                                    {"CountryNews3", Utilities.GetLocalizedString("Running Winter continues to sizzle on stage.")}
                                };
        }

        #endregion // InitDictionaries

        #region GetTrackTitle
        private string GetTrackTitle(int index)
        {
            return index >= 0 && index < TrackTitles.Count ? TrackTitles[index] : string.Empty;
        }
        #endregion // GetTrackTitle

        #region GetTrackDuration
        private string GetTrackDuration(int index)
        {
            return index >= 0 && index < TrackDurations.Count ? TrackDurations[index] : string.Empty;
        }
        #endregion // GetTrackDuration

        #region GetAlbumName
        private string GetAlbumName(int index)
        {
            return index >= 0 && index < AlbumNames.Count ? AlbumNames[index] : string.Empty;
        }
        #endregion // GetAlbumName

        #region GetMonthlyDownloads
        private int GetMonthlyDownloads(int index)
        {
            return index >= 0 && index < MonthlyDownloads.Count ? MonthlyDownloads[index] : -1;
        }
        #endregion // GetMonthlyDownloads

        #region GetTotalDownloads
        private int GetTotalDownloads(int index)
        {
            return index >= 0 && index < TotalDownloads.Count ? TotalDownloads[index] : -1;
        }
        #endregion // GetTotalDownloads

        #region GetNews
        private string GetNews(string newsKey)
        {
            if (this.NewsDictionary != null && this.NewsDictionary.ContainsKey(newsKey))
            {
                return this.NewsDictionary[newsKey];
            }
            return string.Empty;
        }
        #endregion // GetNews

        #region Grid_InitializeLayout
        private void ultraGrid1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout gridLayout = e.Layout;
            UltraGridBand band0 = gridLayout.Bands[0];

            gridLayout.Override.DefaultRowHeight = 33;
            gridLayout.Override.CellPadding = 0;
            gridLayout.Override.CellAppearance.TextVAlign = VAlign.Middle;
            gridLayout.Override.HeaderAppearance.FontData.Bold = DefaultableBoolean.True;

            band0.Columns[0].Header.Caption = Properties.Resources.TrackTitle;
            int columnWidthPadding = 15;
            band0.Columns[0].Width = 275 + columnWidthPadding;//100

            band0.Columns[1].Header.Caption = Properties.Resources.Duration;
            band0.Columns[1].Width = 115 + columnWidthPadding; //25

            band0.Columns[2].Header.Caption = Properties.Resources.AlbumName;
            band0.Columns[2].Width = 125 + columnWidthPadding;

            band0.Columns[3].Header.Caption = Properties.Resources.ThisMonth;
            band0.Columns[3].Width = 100 + columnWidthPadding;

            band0.Columns[4].Header.Caption = Properties.Resources.TotalDownloads;
            band0.Columns[4].Width = 125 + columnWidthPadding;

            band0.HeaderVisible = true;
            band0.Header.Caption = string.Empty;
            UltraGridColumn paddingColumn = band0.Columns.Insert(0, "Padding");
            paddingColumn.Width = 25;
            paddingColumn.Header.Caption = string.Empty;
        }
        #endregion // Grid_InitializeLayout

        #region Grid_InitializeRow
        private void ultraGrid1_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            
        }
        #endregion // Grid_InitializeRow

        #region Grid_BeforeCellActivate
        private void ultraGrid1_BeforeCellActivate(object sender, CancelableCellEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion // Grid_BeforeCellActivate

        #region Grid_BeforeSortChange
        private void ultraGrid1_BeforeSortChange(object sender, BeforeSortChangeEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion // Grid_BeforeSortChange

        private void ultraDataChart1_Click(object sender, EventArgs e)
        {
            OutputControls(this);
        }
    }

    #region GridDrawFilter
    internal class GridDrawFilter : IUIElementDrawFilter
    {
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            return true;
        }

        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            return DrawPhase.BeforeDrawFocus;
        }
    }
    #endregion // GridDrawFilter

    #region ToolbarsManagerCreationFilter
    internal class ToolbarsManagerUIElementFilter : Infragistics.Win.IUIElementCreationFilter
    {
        #region AfterCreateChildElements
        /// <summary>
        /// Called after an element's ChildElements have been
        /// created. The child element's can be repositioned here
        /// and/or new element's can be added.
        /// </summary>
        /// <param name="parent">The <see cref="T:Infragistics.Win.UIElement"/> whose child elements have been created/positioned.</param>
        public void AfterCreateChildElements(Infragistics.Win.UIElement parent)
        {
            if (parent is Infragistics.Win.UltraWinToolbars.RibbonCaptionAreaUIElement)
            {
                foreach (UIElement child in parent.ChildElements)
                {
                    // remove the UIElement for the Form Icon
                    if (child is Infragistics.Win.UltraWinToolbars.PopupToolUIElement)
                    {
                        parent.ChildElements.Remove(child);
                        return;
                    }
                }
            }
        }

        #endregion //AfterCreateChildElements

        #region BeforeCreateChildElements
        /// <summary>
        /// Called before child elements are to be created/positioned.
        /// This is called during a draw operation for an element
        /// whose ChildElementsDirty is set to true. Returning true from
        /// this method indicates that the default creation logic
        /// should be bypassed.
        /// </summary>
        /// <param name="parent">The <see cref="T:Infragistics.Win.UIElement"/> whose child elements are going to be created/positioned.</param>
        /// <returns>
        /// True if the default creation logic should be bypassed.
        /// </returns>
        public bool BeforeCreateChildElements(Infragistics.Win.UIElement parent)
        {
            return false;
        }

        #endregion //BeforeCreateChildElements
    }
    #endregion // ToolbarsManagerCreationFilter

    #region LiveTileViewDrawFilter
    internal class LiveTileViewDrawFilter : IUIElementDrawFilter
    {
        #region DrawElement
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement thumbUiElement = drawParams.Element as Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement;
            if (thumbUiElement != null && thumbUiElement.IsPressed)
            {
                drawParams.AppearanceData.AlphaLevel = 128;
                drawParams.AppearanceData.BackColorAlpha = Alpha.UseAlphaLevel;
            }

            return false;
        }
        #endregion // DrawElement

        #region GetPhasesToFilter
        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (drawParams.Element is Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement)
            {
                return DrawPhase.BeforeDrawElement;
            }
            return DrawPhase.None;
        }
        #endregion // GetPhasesToFilter
    }
    #endregion // LiveTileViewDrawFilter

}
