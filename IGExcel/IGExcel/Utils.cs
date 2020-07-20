using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Infragistics.Documents.Excel;

namespace IGExcel
{
    internal static class Utils
    {
        #region GetEmbeddedImage

        /// <summary>
        /// Retrieves the image from the embedded resources.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static Image GetEmbeddedImage(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream stream = asm.GetManifestResourceStream(string.Format("IGExcel.{0}", name)))
            {
                return Image.FromStream(stream).Clone() as Image;
            }
        }

        #endregion //GetEmbeddedImage

        #region GetNumberFormats

        /// <summary>
        /// Gets the list of NumberFormatInfo objects
        /// </summary>
        internal static List<NumberFormatInfo> GetNumberFormats()
        {
            var numberFormats = new List<NumberFormatInfo>();

            //General
            var general = new NumberFormatInfo
            {
                CategoryName = ResourceStrings.Text_General1,
                DecimalPlaces = -1,
                AreFormatsVisible = false,
                Description = ResourceStrings.Text_General_NumberFormat_Description
            };

            general.AddFormatInfo("General");

            numberFormats.Add(general);

            //Number
            var number = new NumberFormatInfoAdvanced
            {
                CategoryName = ResourceStrings.Text_Number,
                FormatsHeader = ResourceStrings.Lbl_NegativeNumbers,
                Description = ResourceStrings.Text_Number_NumberFormat_Description
            };

            number.Formats.Add(new FormatInfo("0.00", "-1234.10"));
            number.Formats.Add(new FormatInfo("0.00;[Red]0.00", "1234.01", "#FF0000"));
            number.Formats.Add(new FormatInfo("0.00;(#.00)", "(1234.10)"));
            number.Formats.Add(new FormatInfo("0.00;[Red](#.00)", "(1234.10)", "#FF0000"));

            numberFormats.Add(number);

            //Currency
            var currency = new NumberFormatInfoAdvanced
            {
                CategoryName = ResourceStrings.Text_Currency,
                FormatsHeader = ResourceStrings.Lbl_NegativeNumbers,
                Description = ResourceStrings.Text_Currency_NumberFormat_Description
            };


            currency.Formats.Add(new FormatInfo("$#,###.00", "-$1,234.01"));
            currency.Formats.Add(new FormatInfo("$#,###.00;[Red]$#,###.00", "$1,234.01", "#FF0000"));
            currency.Formats.Add(new FormatInfo("$#,###.00;($#,###.00)", "($1,234.01)"));
            currency.Formats.Add(new FormatInfo("$#,###.00;[Red]($#,###.00)", "($1,234.01)", "#FF0000"));

            numberFormats.Add(currency);

            //Date
            var date = new NumberFormatInfo
            {
                CategoryName = ResourceStrings.Text_Date,
                DecimalPlaces = -1,
                FormatsHeader = ResourceStrings.Lbl_Types,
                Description = ResourceStrings.Text_Date_NumberFormat_Description
            };
			
			date.Formats.Add(new FormatInfo(System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern + ";@", "*3/14/2012"));
			date.Formats.Add(new FormatInfo(System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.LongDatePattern.Replace("\'", string.Empty) + ";@", "*Wednesday, March 14, 2012"));
            date.Formats.Add(new FormatInfo("m/d;@", "3/14"));
            date.Formats.Add(new FormatInfo("m/d/yy;@", "3/14/12"));
            date.Formats.Add(new FormatInfo("mm/dd/yy;@", "03/14/12"));
            date.Formats.Add(new FormatInfo("[$-409]d-mmm;@", "14-Mar"));

            numberFormats.Add(date);

            //Time
            var time = new NumberFormatInfo
            {
                CategoryName = ResourceStrings.Text_Time,
                DecimalPlaces = -1,
                FormatsHeader = ResourceStrings.Lbl_Types,
                Description = ResourceStrings.Text_Time_NumberFormat_Description
            };
			
			time.Formats.Add(new FormatInfo(System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.LongTimePattern.Replace("tt", "AM/PM") + ";@", "*1:30:55 PM"));
			time.Formats.Add(new FormatInfo("h:mm;@", "13:30"));
            time.Formats.Add(new FormatInfo("[$-409]h:mm AM/PM;@", "13:30 PM"));
            time.Formats.Add(new FormatInfo("h:mm:ss;@", "13:30:55"));
            time.Formats.Add(new FormatInfo("[$-409]h:mm:ss AM/PM;@", "13:30:55 PM"));

            numberFormats.Add(time);

            //Percentage
            var percentage = new NumberFormatInfoAdvanced
            {
                CategoryName = ResourceStrings.Text_Percentage,
                FormatsHeader = ResourceStrings.Lbl_Types,
                Description = ResourceStrings.Text_Percentage_NumberFormat_Description,
                AreFormatsVisible = false,
                DecimalPlaces = 2
            };

            percentage.AddFormatInfo("0.00%");

            numberFormats.Add(percentage);

            //Text
            var text = new NumberFormatInfo
            {
                CategoryName = ResourceStrings.Text_Text,
                FormatsHeader = ResourceStrings.Lbl_Types,
                Description = ResourceStrings.Text_Text_NumberFormat_Description,
                AreFormatsVisible = false,
                DecimalPlaces = -1
            };

            text.AddFormatInfo("@");

            numberFormats.Add(text);

            //Custom
            var custom = new NumberFormatInfo
            {
                CategoryName = ResourceStrings.Text_Custom,
                DecimalPlaces = -1,
                FormatsHeader = ResourceStrings.Lbl_Types,
                IsCustom = true,
                Description = ResourceStrings.Text_Custom_NumberFormat_Description
            };

            for (int i = 1; i < numberFormats.Count; i++)
            {
                foreach (var format in numberFormats[i].Formats)
                {
                    custom.AddFormatInfo(format.Mask);
                }
            }

            custom.Formats = new System.ComponentModel.BindingList<FormatInfo>(custom.Formats.Select(s => s).OrderBy(s => s.Mask).ToList());
            custom.Formats.Insert(0, numberFormats[0].Formats[0]);

            numberFormats.Add(custom);

            return numberFormats;
        }

        #endregion //GetNumberFormats

        #region GetWorkbookFormatFromFileExtension

        internal static WorkbookFormat GetWorkbookFormatFromFileExtension(string extension)
        {
            switch (extension)
            {
                case ".xls": return WorkbookFormat.Excel97To2003;
                case ".xlt": return WorkbookFormat.Excel97To2003Template;
                case ".xlsx": return WorkbookFormat.Excel2007;
                case ".xlsm": return WorkbookFormat.Excel2007MacroEnabled;
                case ".xltm": return WorkbookFormat.Excel2007MacroEnabledTemplate;
                case ".xltx": return WorkbookFormat.Excel2007Template;

                default: return WorkbookFormat.Excel2007;
            }
        }

        #endregion //GetWorkbookFormatFromFileExtension
    }
}
