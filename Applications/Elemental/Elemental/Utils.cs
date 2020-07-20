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
using Infragistics.Win.UltraWinCarousel;
using Infragistics.Win.UltraWinPivotGrid;
using PivotGridData = Infragistics.Win.UltraWinPivotGrid.Data;
using Infragistics.Win.Misc;
using Infragistics.Win.DataVisualization;

namespace Elemental
{
    static public class Utils
    {
        #region FormatCurrencyValue
        static public string FormatCurrencyValue(decimal value)
        {
            int pattern = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyPositivePattern;
            string symbol = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;
            string val =
                value == 0m ?
                "0" :
                value.ToString("###,###,###");

            return
                (pattern == 0 || pattern == 2) ?
                string.Format("{0}{1}", symbol, val) :
                string.Format("{0}{1}", val, symbol);
        }
        #endregion FormatCurrencyValue

        #region Subtract
        static public HoodieStyle Subtract(HoodieStyle style, int value)
        {
            int length = Enum.GetValues(typeof(HoodieStyle)).Length;
            int a = (int)style - value;
            if ( a >= 0 )
                return (HoodieStyle)a;
            else
                return (HoodieStyle)(length + a);
        }
        #endregion Subtract

        #region CreateDropDownButtonImage
        static private Image CreateDropDownButtonImage()
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(System.Drawing.Point.Empty, new Size(24, 24));
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using ( System.Drawing.Brush brush = new System.Drawing.SolidBrush(Color.Transparent) )
            {
                g.FillRectangle(brush, rect);
            }

            using ( Pen pen = new Pen(Color.White, 2f) )
            {
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                rect.Inflate(-2, -2);
                g.DrawEllipse(pen, rect);

                rect.Y += 1;

                int dx = -5;
                int dy = -7;
                rect.Inflate(dx, dy);

                System.Drawing.Point p1, p2;

                p1 = new System.Drawing.Point(rect.X, rect.Y);
                p2 = new System.Drawing.Point(rect.X + (rect.Width / 2), rect.Bottom);
                g.DrawLine(pen, p1, p2);

                p1 = new System.Drawing.Point(rect.Right, rect.Y);
                p2 = new System.Drawing.Point(rect.X + (rect.Width / 2), rect.Bottom);
                g.DrawLine(pen, p1, p2);

            }

            return bmp;
        }
        #endregion CreateDropDownButtonImage

        #region ColumnFromStyle
        static public PivotGridData.PivotGridColumn ColumnFromStyle(UltraPivotGrid grid, HoodieStyle style)
        {
            PivotGridData.PivotGridColumnsCollection columns = grid.Columns;

            foreach ( PivotGridData.PivotGridColumn column in columns )
            {
                string key = Data.ProductData.GetDisplayString(style);
                if ( string.Equals(column.Key, key, StringComparison.InvariantCultureIgnoreCase) )
                    return column;
            }

            return null;
        }
        #endregion ColumnFromStyle
    }
}