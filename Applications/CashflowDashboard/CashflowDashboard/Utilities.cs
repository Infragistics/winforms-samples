using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Showcase.CashflowDashboard
{
    /// <summary>
    /// Some useful utilities methods applicaiton-wide.
    /// </summary>
    internal static class Utilities
    {
        #region Private Members

        // A dictionary of cached image resources. 
        private static Dictionary<CachedImages, Image> cachedImages;        

        #endregion // Private Members

        #region Methods

        #region CreateCachedImage

        /// <summary>
        /// Creates a cached resource from an embedded resource. 
        /// </summary>
        private static Image CreateCachedImage(CachedImages cachedImages)
        {
            Bitmap bmp;
            switch (cachedImages)
            {                
                case CachedImages.WhiteButtonTransparent:
                case CachedImages.OperationsInflowLegend:
                case CachedImages.OperationsOutflowLegend:
                case CachedImages.InvestingInflowLegend:
                case CachedImages.InvestingOutflowLegend:
                case CachedImages.FinancingInflowLegend:
                case CachedImages.FinancingOutflowLegend:
                case CachedImages.OtherInflowLegend:
                case CachedImages.OtherOutflowLegend:
                    {
                        bmp = GetImageFromResource(cachedImages);
                    }
                    break;
                default:
                    {
                        Debug.Fail("Unknown CachedImages value");
                        bmp = null;
                    }
                    break;

            }
            return bmp;
        }
        #endregion // CreateCachedImage

        #region DrawBorder
        /// <summary>
        /// Draws a rectangle into the specified Graphics object.
        /// </summary>
        /// <param name="g">The graphics object into which to draw.</param>
        /// <param name="rect">The rect of the border.</param>
        /// <param name="borderSides">The sides of the border to be drawn.</param>
        /// <param name="pen">The pen with which to draw the border.</param>
        public static void DrawBorder(Graphics g, Rectangle rect, Border3DSide borderSides, Pen pen)
        {
            if (Border3DSide.Left == (borderSides & Border3DSide.Left))
                g.DrawLine(pen, new PointF((float)rect.Left, (float)rect.Bottom), new PointF((float)rect.Left, (float)rect.Top));

            if (Border3DSide.Top == (borderSides & Border3DSide.Top))
                g.DrawLine(pen, new PointF((float)rect.Left, (float)rect.Top), new PointF((float)rect.Right, (float)rect.Top));

            if (Border3DSide.Right == (borderSides & Border3DSide.Right))
                g.DrawLine(pen, new PointF((float)rect.Right, (float)rect.Top), new PointF((float)rect.Right, (float)rect.Bottom));

            if (Border3DSide.Bottom == (borderSides & Border3DSide.Bottom))
                g.DrawLine(pen, new PointF((float)rect.Left, (float)rect.Bottom), new PointF((float)rect.Right, (float)rect.Bottom));
        } 
        #endregion // DrawBorder

        #region GetCachedImage

        /// <summary>
        /// Gets a cached image.
        /// </summary>
        internal static Image GetCachedImage(CachedImages cachedImage)
        {
            if (null == Utilities.cachedImages)
                Utilities.cachedImages = new Dictionary<CachedImages, Image>();

            Image image;
            bool success = Utilities.cachedImages.TryGetValue(cachedImage, out image);
            if (false == success)
            {
                image = Utilities.CreateCachedImage(cachedImage);
                Utilities.cachedImages[cachedImage] = image;
            }

            return image;
        }       
        #endregion // GetCachedImage  
        
        #region GetComparisonString
        /// <summary>
        /// Builds a string that indicates a comparison between the two values. This is used for the 
        /// grid in the CashflowDetails control to indicate the relative values between this month
        /// last month, last year, and the projected value. 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static string GetComparisonString(decimal value1, decimal value2)
        {
            // Which value is bigger?
            int sign = Math.Sign(value1 - value2);

            // Determine the percentage difference. 
            decimal percentage = Math.Abs(value1 - value2) / value1;
            string symbol;
                        
            // Do we need a plus sign, a minus sign, or no sign?
            if (sign > 0)
            {
                symbol = "+";
            }
            else if (sign < 0)
            {
                symbol = "-";
            }
            else
            {
                symbol = " ";
            }

            // Return the comparison string. 
            return string.Format(
                "{0} {1}{2}",
                value2.ToString("c0"),
                symbol,
                percentage.ToString("0%"));
        } 
        #endregion // GetComparisonString

        #region GetDisplayValueInMillions
        /// <summary>
        /// Formats a decimal value into a more readable form like "21M", "22M", "12.5M", etc. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimalPlaces"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string GetDisplayValueInMillions(decimal value, int decimalPlaces = 0, string suffix = null)
        {
            // Divide by a million. 
            decimal roundedValue = value / 1000000;

            // Format the string to the appropriate number of decimal places. 
            string formatString = string.Format("0.{0}", new string('0', decimalPlaces));
            string formattedText = roundedValue.ToString(formatString);
            
            // Add the suffic, if needed. 
            if (null != suffix)
                formattedText += suffix;

            // Put parens around negative numbers. 
            if (value < 0)
                formattedText = string.Format("({0})", formattedText);

            return formattedText;
        }  
        #endregion // GetDisplayValueInMillions

        #region GetImageFromResource
        /// <summary>
        /// Gets the image from the embedded resources
        /// </summary>
        private static Bitmap GetImageFromResource(CachedImages cachedImages)
        {
            string resourceName = cachedImages.ToString();
            return GetImageFromResource(resourceName);
        }

        /// <summary>
        /// Gets the image from the embedded resources
        /// </summary>
        private static Bitmap GetImageFromResource(string resourceName)
        {
            Bitmap bmp;
            Type thisType = typeof(Utilities);
            string fullResourceName = string.Format("Showcase.CashflowDashboard.Images.{0}.png", resourceName);
            
            
            foreach (var resourceName1 in thisType.Assembly.GetManifestResourceNames()) { Debug.WriteLine(resourceName1); }
            
            Stream stream = thisType.Assembly.GetManifestResourceStream(fullResourceName);
            bmp = (Bitmap)Image.FromStream(stream);
            bmp.MakeTransparent(Color.Magenta);
            return bmp;
        }
        #endregion // GetImageFromResource

        #region GetPoint
        /// <summary>
        /// Get a point within a control based on the specified ContentAlignement.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="contentAlignment"></param>
        /// <returns></returns>
        internal static Point GetPoint(Control control, ContentAlignment contentAlignment)
        {            
            int x;
            int y;

            switch (contentAlignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    x = 0;
                    break;

                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    x = control.Width / 2;
                    break;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    x = control.Width - 1;
                    break;
                default:
                    throw new ArgumentException("Unknown contentAlignment");
            }


            switch (contentAlignment)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                    y = 0;
                    break;
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                    y = control.Height / 2;
                    break;
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    y = control.Height - 1;
                    break;
                default:
                    throw new ArgumentException("Unknown contentAlignment");
            }

            return new Point(x, y);
        } 
        #endregion // GetPoint

        #region LocalizeString

        /// <summary>
        /// Localizes a string using the ResourceManager.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string LocalizeString(string value, params object[] args)
        {
            ResourceManager rm = Showcase.CashflowDashboard.Properties.Resources.ResourceManager;
            value = value.Replace(' ', '_');
            string localizedString = string.Format(rm.GetString(value), args);

            if (string.IsNullOrEmpty(localizedString))
                return value;

            return localizedString;
           // return "test" ;
        }
        #endregion //LocalizeString              

        #endregion //Methods
    }
}
