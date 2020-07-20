using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IGExcel
{

    public enum FontStylesCustom { Regular, Italic, Bold, BoldItalic }

    public enum ExcelBorders
    {
        BottomBorder,
        TopBorder,
        LeftBorder,
        RightBorder,
        NoBorder,
        AllBorders,
        OutsideBorder,
        ThickBoxBorder,
        BottomDoubleBorder,
        ThickBottomBorder,
        TopAndBottomBorder,
        TopAndThickBottomBorder,
        TopAndDoubleBottomBorder,
    }

    public enum TableStyles
    {
        Light,
        Medium,
        Dark
    }

    public enum CellStyles
    {
        GoodBadAndNeutral,
        DataAndModel,
        TitlesAndHeadings,
        Themed,
        NumberFormat,
    }
}