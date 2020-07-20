using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGExcel
{
    internal static class StyleUtils
    {

        #region GetTableStyles

        /// <summary>
        /// Gets a list of table styles based on the supplied TableStyles value
        /// </summary>
        internal static List<string> GetTableStyles(TableStyles style)
        {
            switch (style)
            {
                case TableStyles.Light:
                    return StyleUtils.GetLightTableStyles();
                case TableStyles.Medium:
                    return StyleUtils.GetMediumTableStyles();
                case TableStyles.Dark:
                    return StyleUtils.GetDarkTableStyles();
                default:
                    return null;
            }
        }

        #endregion //GetTableStyles

        #region GetDarkTableStyles

        /// <summary>
        /// Gets a list of dark table styles.
        /// </summary>
        private static List<string> GetDarkTableStyles()
        {
            return new List<string>
            {
                 "TableStyleDark1",
                 "TableStyleDark2",
                 "TableStyleDark3",
                 "TableStyleDark4",
                 "TableStyleDark5",
                 "TableStyleDark6",
                 "TableStyleDark7",
                 "TableStyleDark8",
                 "TableStyleDark9",
                 "TableStyleDark10",
                 "TableStyleDark11"
            };
        }

        #endregion //GetDarkTableStyles

        #region GetLightTableStyles
        /// <summary>
        /// Gets a list of light table styles.
        /// </summary>
        private static List<string> GetLightTableStyles()
        {
            return new List<string>
            {
                 "TableStyleLight1",
                 "TableStyleLight2",
                 "TableStyleLight3",
                 "TableStyleLight4",
                 "TableStyleLight5",
                 "TableStyleLight6",
                 "TableStyleLight7",
                 "TableStyleLight8",
                 "TableStyleLight9",
                 "TableStyleLight10",
                 "TableStyleLight11",
                 "TableStyleLight12",
                 "TableStyleLight13",
                 "TableStyleLight14",
                 "TableStyleLight15",
                 "TableStyleLight16",
                 "TableStyleLight17",
                 "TableStyleLight18",
                 "TableStyleLight19",
                 "TableStyleLight20",
                 "TableStyleLight21",
            };
        }

        #endregion //GetLightTableStyles

        #region GetMediumTableStyles

        /// <summary>
        /// Gets a list of medium table styles.
        /// </summary>
        private static List<string> GetMediumTableStyles()
        {
            return new List<string>
            {
                 "TableStyleMedium1",  
                 "TableStyleMedium2", 
                 "TableStyleMedium3", 
                 "TableStyleMedium4", 
                 "TableStyleMedium5", 
                 "TableStyleMedium6",  
                 "TableStyleMedium7",  
                 "TableStyleMedium8",  
                 "TableStyleMedium9",
                 "TableStyleMedium10",
                 "TableStyleMedium11",
                 "TableStyleMedium12",
                 "TableStyleMedium13",
                 "TableStyleMedium14",
                 "TableStyleMedium15",
                 "TableStyleMedium16",
                 "TableStyleMedium17",
                 "TableStyleMedium18",
                 "TableStyleMedium19",
                 "TableStyleMedium20",
                 "TableStyleMedium21",
                 "TableStyleMedium22",
                 "TableStyleMedium23",
                 "TableStyleMedium24",
                 "TableStyleMedium25",
                 "TableStyleMedium26",
                 "TableStyleMedium27",
                 "TableStyleMedium28"
            };
        }

        #endregion //GetMediumTableStyles

        #region GetCellStyles

        /// <summary>
        /// Gets the list of cell styles based on the supplied CellStyles value
        /// </summary>
        internal static List<string> GetCellStyles(CellStyles style)
        {
            switch (style)
            {
                case CellStyles.GoodBadAndNeutral:
                    return StyleUtils.GetGoodBadAndNeutralCellStyles();
                case CellStyles.DataAndModel:
                    return StyleUtils.GetDataAndModelCellStyles();
                case CellStyles.TitlesAndHeadings:
                    return StyleUtils.GetTitlesAndHeadingsCellStyles();
                case CellStyles.Themed:
                    return StyleUtils.GetThemedCellStyles();
                case CellStyles.NumberFormat:
                    return StyleUtils.GetNumberFormatCellStyles();
                default:
                    return null;
            }
        }

        #endregion //GetTableStyles

        #region GetGoodBadAndNeutralCellStyles

        /// <summary>
        /// Gets a list of the good, bad and neutral cell styles.
        /// </summary>
        private static List<string> GetGoodBadAndNeutralCellStyles()
        {
            return new List<string>
            {
                "Normal",
                "Good",
                "Bad",
                "Neutral"
            };
        }

        #endregion //GetGoodBadAndNeutralCellStyles

        #region GetDataAndModelCellStyles

        /// <summary>
        /// Gets a list of data and model cell styles.
        /// </summary>
        private static List<string> GetDataAndModelCellStyles()
        {
            return new List<string>
            {
                 "Calculation",
                 "Check Cell",
                 "Explanatory Text",
                 "Input",
                 "Linked Cell",
                 "Note",
                 "Output",
                 "Warning Text"
            };
        }

        #endregion //GetDataAndModelCellStyles

        #region GetTitlesAndHeadingsCellStyles

        /// <summary>
        /// Gets a list of title and headings cell styles.
        /// </summary>
        private static List<string> GetTitlesAndHeadingsCellStyles()
        {
            return new List<string>
            {
                 "Heading 1",
                 "Heading 2",
                 "Heading 3",
                 "Heading 4",
                 "Title",
                 "Total"
            };
        }

        #endregion //GetTitlesAndHeadingsCellStyles

        #region GetThemedCellStyles

        /// <summary>
        /// Gets the list of themed cell styles
        /// </summary>
        private static List<string> GetThemedCellStyles()
        {
            return new List<string>
            {
                 "20% - Accent1",
                 "20% - Accent2",
                 "20% - Accent3",
                 "20% - Accent4",
                 "20% - Accent5",
                 "20% - Accent6",
                 "40% - Accent1",
                 "40% - Accent2",
                 "40% - Accent3",
                 "40% - Accent4",
                 "40% - Accent5",
                 "40% - Accent6",
                 "60% - Accent1",
                 "60% - Accent2",
                 "60% - Accent3",
                 "60% - Accent4",
                 "60% - Accent5",
                 "60% - Accent6",
                 "Accent1",
                 "Accent2",
                 "Accent3",
                 "Accent4",
                 "Accent5",
                 "Accent6"
            };
        }

        #endregion //GetThemedCellStyles

        #region GetNumberFormatCellStyles

        /// <summary>
        /// Gets the list of number-specific cell styles.
        /// </summary>
        private static List<string> GetNumberFormatCellStyles()
        {
            return new List<string>
            {
                 "Comma",
                 "Comma [0]",
                 "Currency",
                 "Currency [0]",
                 "Percent"
            };
        }

        #endregion //GetNumberFormatCellStyles

        #region GetFontFamilyList

        /// <summary>
        /// Gets a list of unique FontFamily names.
        /// </summary>
        internal static List<string> GetFontFamilyList()
        {
            List<string> list = new List<string>(50);
            using (System.Drawing.Text.InstalledFontCollection installedFonts = new System.Drawing.Text.InstalledFontCollection())
            {
                foreach (System.Drawing.FontFamily family in installedFonts.Families)
                {
                    list.Add(family.Name);
                }
            }
            list.Sort();

            return list;
        }

        #endregion //GetFontFamilyList

        #region GetFontSizeValueList

        /// <summary>
        /// Creates a ValueList containing all the predefined font sizes.
        /// </summary>
        internal static Infragistics.Win.ValueList GetFontSizeValueList()
        {
            Infragistics.Win.ValueList list = new Infragistics.Win.ValueList();
            foreach(int i in new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 })
            {
                list.ValueListItems.Add(i * 20, i.ToString());
            }
            return list;
        }

        #endregion //GetFontSizeValueList

    }
}
