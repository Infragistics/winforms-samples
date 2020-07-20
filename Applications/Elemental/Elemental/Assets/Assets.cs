using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using GDI = System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Elemental;

namespace Elemental.Assets
{
    #region Colors class
    /// <summary>
    /// Provides colors.
    /// </summary>
    public class Colors
    {
        #region FilterDropDownBackground
        /// <summary>
        /// Returns the background color of the date filter dropdown
        /// </summary>
        public Color FilterDropDownBackground
        {
            get { return Color.FromArgb(64, 50, 58); }
        }
        #endregion FilterDropDownBackground

        #region ScrollButtonForeColorHot
        /// <summary>
        /// Returns the hot tracking foreground color of the carousel scroll buttons.
        /// </summary>
        public Color ScrollButtonForeColorHot
        {
            get { return Color.FromArgb(163, 149, 161); }
        }
        #endregion ScrollButtonForeColorHot

        #region DateFilterDropDownButtonForeColor
        /// <summary>
        /// Returns the foreground color of the date filter dropdown button.
        /// </summary>
        public Color DateFilterDropDownButtonForeColor
        {
            get { return Color.White; }
        }
        #endregion DateFilterDropDownButtonForeColor

        #region DateFilterDropDownButtonForeColorHot
        /// <summary>
        /// Returns the foreground color of the date filter dropdown button.
        /// </summary>
        public Color DateFilterDropDownButtonForeColorHot
        {
            get { return Color.FromArgb(167, 149, 160); }
        }
        #endregion DateFilterDropDownButtonForeColorHot

        #region FormBackground
        /// <summary>
        /// Returns the background color of the form
        /// </summary>
        public Color FormBackground
        {
            get { return Color.FromArgb(239, 239, 239); }
        }
        #endregion FormBackground

        #region TitleDark
        /// <summary>
        /// Returns the dark shade of the title text color.
        /// </summary>
        public Color TitleDark
        {
            get { return Color.FromArgb(100, 78, 91); }
        }
        #endregion TitleDark

        #region TitleLight
        /// <summary>
        /// Returns the light shade of the title text color.
        /// </summary>
        public Color TitleLight
        {
            get { return Color.FromArgb(132, 114, 130); }
        }
        #endregion TitleLight

        #region ChartForeColor
        /// <summary>
        /// Returns the foreground color for the chart.
        /// </summary>
        public Color ChartForeColor
        {
            get { return Color.FromArgb(147, 147, 147); }
        }
        #endregion ChartForeColor

        #region GridRowHeaderForeColor
        /// <summary>
        /// Returns the foreground color for the pivot grid row headers.
        /// </summary>
        public Color GridRowHeaderForeColor
        {
            get { return Color.FromArgb(149, 149, 149); }
        }
        #endregion GridRowHeaderForeColor

        #region GridHeaderBackground
        /// <summary>
        /// Returns the color of the grid headers
        /// </summary>
        public Color GridHeaderBackground
        {
            get { return this.TitleLight; }
        }
        #endregion GridHeaderBackground

        #region DropAreaItemBackground
        /// <summary>
        /// Returns the color of the drop area items
        /// </summary>
        public Color DropAreaItemBackground
        {
            get { return Color.FromArgb(240, this.TitleLight); }
        }
        #endregion DropAreaItemBackground

        #region GridHeaderBorder
        /// <summary>
        /// Returns the color of the grid header border
        /// </summary>
        public Color GridHeaderBorder
        {
            get { return Color.FromArgb(64, Color.White); }
        }
        #endregion GridHeaderBorder

        #region LabelForeColor
        /// <summary>
        /// Returns the text color used by the label controls.
        /// </summary>
        public Color LabelForeColor
        {
            get { return Color.FromArgb(76, 78, 80); }
        }
        #endregion LabelForeColor

        #region Indexer (HoodieStyle)
        /// <summary>
        /// Returns the color for the specified hoodie style.
        /// </summary>
        /// <param name="style">The HoodieStyle for the color to be returned.</param>
        public Color this[HoodieStyle style]
        {
            get
            {
                int[] colors = null;

                switch ( style )
                {
                    case HoodieStyle.BigWavesSurfing:
                        colors = new int[]{156, 109, 31};
                        break;

                    case HoodieStyle.RockAndRoll:
                        colors = new int[]{136, 150, 37};
                        break;

                    case HoodieStyle.TattooLove:
                        colors = new int[]{100, 78, 91};
                        break;

                    case HoodieStyle.RockStar:
                        colors = new int[]{182, 182, 183};
                        break;

                    case HoodieStyle.SkullsAndFlourishes:
                        colors = new int[]{173, 133, 133};
                        break;

                    case HoodieStyle.BlackDragon:
                        colors = new int[]{47, 37, 43};
                        break;

                    case HoodieStyle.FastRider:
                        colors = new int[]{126, 163, 145};
                        break;

                    case HoodieStyle.AbstractFoliage:
                        colors = new int[]{109, 96, 74};
                        break;

                    default:
                        Debug.Fail( string.Format("Unrecognized HoodieStyle '{0}'", style));
                        break;
                }

                return colors != null ? Color.FromArgb(colors[0], colors[1], colors[2]) : Color.Empty;
            }
        }
        #endregion Indexer (HoodieStyle)

        #region ToHexString
        static public string ToHexString(Color color)
        {
            return string.Format("#{0}{1}{2}", color.R.ToString("x"), color.G.ToString("x"), color.B.ToString("x") );
        }
        #endregion ToHexString
    }
    #endregion Colors class

    #region ImageServer
    /// <summary>
    /// Provides images used by the application.
    /// </summary>
    public class ImageServer : IDisposable
    {
        #region Fields
        private const string ImagesPath = "Assets.Images";
        private const string ChartSeriesPath = ImagesPath + ".ChartSeries";
        private const string HoodiePath = ImagesPath + ".Hoodies";

        private Dictionary<string, System.Tuple<Stream, Image>> hoodieImages = null;
        private System.Tuple<Stream, Image>[] buttonImages = null;
        #endregion Fields

        #region Indexer (HoodieStyle)
        public Image this[HoodieStyle style]
        {
            get
            {
                this.CreateHoodieImageTable();
                string name = ImageServer.GetImageFilename(style);
                Tuple<Stream, Image> tuple = null;
                if ( this.hoodieImages.TryGetValue(name, out tuple) == false )
                {
                    Type type = typeof(frmMain);
                    Stream stream = type.Module.Assembly.GetManifestResourceStream(type, name);
                    Image image = Image.FromStream(stream);
                    tuple = new Tuple<Stream,Image>(stream, image);
                    this.hoodieImages.Add(name, tuple);
                }

                return tuple.Item2;
            }
        }
        #endregion Indexer (HoodieStyle)

        #region HoodieImages
        public IEnumerable<System.Tuple<Image, HoodieStyle>> HoodieImages
        {
            get
            {
                Array styles = Enum.GetValues(typeof(HoodieStyle));

                foreach ( object style in styles )
                {
                    HoodieStyle hs = (HoodieStyle)style;
                    Image image = this[hs];
                    yield return new System.Tuple<Image, HoodieStyle>(image, hs);
                }
            }
        }

        public List<System.Tuple<Image, HoodieStyle>> GetHoodieImages(HoodieStyle first)
        {
            List<System.Tuple<Image, HoodieStyle>> list = new List<Tuple<Image, HoodieStyle>>(this.HoodieImages);
            list.Sort(new Sorter(first));
            return list;
        }
        #endregion HoodieImages

        #region CreateHoodieImageTable
        private void CreateHoodieImageTable()
        {
            if ( this.hoodieImages == null )
                this.hoodieImages = new Dictionary<string, Tuple<Stream, Image>>();
        }
        #endregion CreateHoodieImageTable

        #region GetImageFilename
        static private string GetImageFilename(HoodieStyle style)
        {
            string folder = ImageServer.HoodiePath;
            string name = string.Format("{0}.{1}.png", folder, style);
            return name;
        }
        #endregion GetImageFilename

        #region GenerateColorImage
        static public Image GenerateColorImage(Color color, Size size)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            GDI.Graphics g = GDI.Graphics.FromImage(bmp);
            Rectangle rect = new Rectangle(Point.Empty, size);
            using ( Brush brush = new SolidBrush(color) )
            {
                g.FillRectangle(brush, rect);
            }

            Color dark = Color.FromArgb(128, 96, 96, 96);
            rect.Width -= 1;
            rect.Height -= 1;
            using ( Pen pen = new Pen(dark) )
            {
                g.DrawRectangle(pen, rect);
            }

            return bmp;
        }
        #endregion GenerateColorImage

        #region Indexer (ButtonImages)
        public Image this[ButtonImages button]
        {
            get
            {
                int len = Enum.GetValues(typeof(ButtonImages)).Length;

                if ( this.buttonImages == null )
                    this.buttonImages = new Tuple<Stream,Image>[len];

                Tuple<Stream,Image> tuple = this.buttonImages[(int)button];

                if ( tuple == null )
                {
                    Type type = typeof(frmMain);
                    string resName = string.Format("{0}.{1}.png", ImageServer.ImagesPath, button.ToString());
                    Stream stream = type.Module.Assembly.GetManifestResourceStream(type, resName);
                    Image image = Image.FromStream(stream);
                    this.buttonImages[(int)button] = new Tuple<Stream,Image>(stream, image);
                }

                return this.buttonImages[(int)button].Item2;
            }
        }
        #endregion Indexer (ButtonImages)

        #region AllImages
        private IEnumerable<Tuple<Stream, Image>> AllImages
        {
            get
            {
                if ( this.buttonImages != null )
                {
                    foreach ( Tuple<Stream, Image> t in this.buttonImages )
                    {
                        if ( t != null )
                            yield return t;
                    }
                }

                if ( this.hoodieImages != null )
                {
                    foreach ( Tuple<Stream, Image> t in this.hoodieImages.Values )
                    {
                        if ( t != null )
                            yield return t;
                    }
                }

                yield break;
            }
        }
        #endregion AllImages

        #region IDisposable

        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            foreach ( Tuple<Stream, Image> t in this.AllImages )
            {
                if ( t.Item1 != null )
                {
                    t.Item1.Close();
                    t.Item1.Dispose();
                }

                if ( t.Item2 != null )
                {
                    t.Item2.Dispose();
                }
            }

            this.hoodieImages = null;
            this.buttonImages = null;
        }

        #endregion IDisposable

        #region Sorter class
        private class Sorter : IComparer<Tuple<Image, HoodieStyle>>
        {
            public Sorter(HoodieStyle firstItem)
            {
                this.FirstItem = firstItem;
                this.Length = (int)Enum.GetValues(typeof(HoodieStyle)).Length;
            }

            private HoodieStyle FirstItem { get; set; }
            private int Length { get; set; }


            #region IComparer<Tuple<Image,HoodieStyle>>

            int IComparer<Tuple<Image, HoodieStyle>>.Compare(Tuple<Image, HoodieStyle> style1, Tuple<Image, HoodieStyle> style2)
            {

                if ( style1 == style2 || style1 == null || style2 == null || style1.Item2 == style2.Item2 )
                    return 0;

                int x = Sorter.RelativeIndex(this.FirstItem, style1.Item2, this.Length);
                int y = Sorter.RelativeIndex(this.FirstItem, style2.Item2, this.Length);
                return x.CompareTo(y);
            }

            #endregion IComparer<Tuple<Image,HoodieStyle>>

            #region RelativeIndex
            static private int RelativeIndex(HoodieStyle first, HoodieStyle style, int length)
            {
                int a = (int)first;
                int i = (int)style;
                return i < a ? i + length : i > a ? i : a;
            }
            #endregion RelativeIndex
        }
        #endregion Sorter class

        #region ButtonImages enumeration
        public enum ButtonImages
        {
            DropDownButton,
            DropDownButtonHot,
            ScrollButton,
            ScrollButtonHot,
            ScrollButtonPressed,
        }
        #endregion ButtonImages enumeration
    }
    #endregion ImageServer

}