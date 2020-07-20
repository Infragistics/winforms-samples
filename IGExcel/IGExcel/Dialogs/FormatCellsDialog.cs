using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Documents.Excel;
using Infragistics.Win;
using Infragistics.Win.UltraWinSpreadsheet;

namespace IGExcel.Dialogs
{
    internal partial class FormatCellsDialog : Form, IUIElementDrawFilter
    {
        #region Members

        private SpreadsheetSelectionAdapter adapter;
        private bool recordPropertiesChanges = false;
        private List<string> changedProperties = new List<string>();
        private List<NumberFormatInfo> numberFormats;
        private bool shouldEvaluateIsNormalFont = false;

        #endregion //Members

        #region Constants

        internal const string BackgroundColorPropertyName = "BackgroundColor";
        internal const string FillPatternStylePropertyName = "FillPatternStyle";
        internal const string FontColorPropertyName = "FontColor";
        internal const string FontNamePropertyName = "FontName";
        internal const string FontSizePropertyName = "FontSize";
        internal const string FontStylePropertyName = "FontStyle";
        internal const string FormatPropertyName = "Format";
        internal const string HorizontalCellAlignmentPropertyName = "HorizontalCellAlignment";
        internal const string IndentPropertyName = "Indent";
        internal const string PatternColorPropertyName = "PatternColor";
        internal const string SelectedNumberFormatCategoryPropertyName = "SelectedNumberFormatCategory";
        internal const string ShrinkToFitPropertyName = "ShrinkToFit";
        internal const string StrikethroughPropertyName = "Strikethrough";
        internal const string SubscriptSuperscriptPropertyName = "SubscriptSuperscript";
        internal const string UnderlinePropertyName = "Underline";
        internal const string VerticalCellAlignmentPropertyName = "VerticalCellAlignment";
        internal const string WrapTextPropertyName = "WrapText";

        #endregion //Constants

        #region Constructor

        internal FormatCellsDialog(InitialSelectedTab initialTab, SpreadsheetSelectionAdapter adapter)
        {
            this.InitializeComponent();

            this.adapter = adapter;

            this.InitializeUI(initialTab);
        }

        #endregion //Constructor

        #region Base Class Overrides

        #region OnShown

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var format = adapter.Format;
            var font = format.Font;

            // Initialize controls on the Numbers
            string formatString = format.FormatString;
            if (string.IsNullOrEmpty(formatString) == false)
            {
                FormatInfo formatInfo = null;
                foreach (NumberFormatInfo numberFormat in this.numberFormats)
                {
                    formatInfo = numberFormat.FindFormat(formatString);

                    if (formatInfo != null)
                    {
                        this.treeCategory.GetNodeByKey(numberFormat.CategoryName).Selected = true;
                        this.treeFormats.Nodes[numberFormat.Formats.IndexOf(formatInfo)].Selected = true;
                        break;
                    }
                }

                if (formatInfo == null)
                {
                    var customGroup = this.numberFormats.First(x => x.IsCustom);
                    this.treeCategory.GetNodeByKey(customGroup.CategoryName).Selected = true;

                    formatInfo = customGroup.AddFormatInfo(formatString);
                    this.treeFormats.Nodes[customGroup.Formats.IndexOf(formatInfo)].Selected = true;
                }
            }

            // Initialize controls on the Alignment tab
            #region Alignment tab

            this.cboHorizontal.Value = format.Alignment;
            this.cboVertical.Value = format.VerticalAlignment;
            this.nmeIndent.Value = format.Indent;
            this.cbWrapText.Checked = format.WrapText == ExcelDefaultableBoolean.True;
            this.cbShrinkToFit.Checked = format.ShrinkToFit == ExcelDefaultableBoolean.True;

            #endregion // Alignment tab

            // Initialize controls on the Font tab
            #region Font tab

            Infragistics.Win.UltraWinTree.UltraTreeNode node = this.treeFontFamily.GetNodeByKey(font.Name);
            if (node != null)
            {
                node.Selected = true;
                node.BringIntoView();
            }

            FontStylesCustom style = FontStylesCustom.Regular;
            if (font.Italic == ExcelDefaultableBoolean.True)
            {
                style = (font.Bold == ExcelDefaultableBoolean.True) ? FontStylesCustom.BoldItalic : FontStylesCustom.Italic;
            }
            else if (font.Bold == ExcelDefaultableBoolean.True)
                style = FontStylesCustom.Bold;

            node = this.treeFontStyle.GetNodeByKey(style.ToString());
            if (node != null)
            {
                node.Selected = true;
                node.BringIntoView();
            }

            node = this.treeFontSize.GetNodeByKey((font.Height / 20).ToString());
            if (node != null)
            {
                node.Selected = true;
                node.BringIntoView();
            }

            this.cboUnderline.Value = font.UnderlineStyle;
            this.cbSubscript.Checked = (font.SuperscriptSubscriptStyle == FontSuperscriptSubscriptStyle.Subscript);
            this.cbSuperscript.Checked = (font.SuperscriptSubscriptStyle == FontSuperscriptSubscriptStyle.Superscript);
            this.cbStrikethrough.Checked = (font.Strikeout == ExcelDefaultableBoolean.True);
            this.cpFontColor.Color = adapter.ForegroundColor;

            this.shouldEvaluateIsNormalFont = true;
            this.EvaluateIsNormalFont();
            #endregion //Font tab

            // Initialize controls on the Fill tab
            #region Fill tab

            CellFillPattern fillPattern = format.Fill as CellFillPattern;
            if (fillPattern != null)
            {
                this.cboPatternStyle.Value = fillPattern.PatternStyle;                
                this.cpBackColor.Color = adapter.FillColor;                      

                if (fillPattern.PatternColorInfo.IsAutomatic)
                {
                    this.cpPatternColor.Color = Color.Empty;
                }
                else
                {
                    if(fillPattern.PatternColorInfo.Color != null)
                    {
                        this.cpPatternColor.Color = fillPattern.PatternColorInfo.Color.Value;
                    }
                    else
                    {                        
                        this.cpPatternColor.Color = Color.Empty;                        
                    }
                }                               
            }

            #endregion // Fill tab

            this.recordPropertiesChanges = true;
        }

        #endregion //OnShown

        #endregion //Base Class Overrides

        #region Methods

        #region CommitChanges

        private void CommitChanges()
        {
            var format = this.adapter.Format;
            var font = format.Font;

            WorkbookColorInfo newBackgroundColorInfo = null;
            WorkbookColorInfo newPatternColorInfo = null;
            bool updatePatternStyle = false;

            foreach (var propertyName in this.changedProperties.Distinct())
            {
                switch (propertyName)
                {
                    case BackgroundColorPropertyName:
                        #region BackgroundColorPropertyName
                        if (!this.cpBackColor.Color.IsEmpty)
                        {
                            try
                            {
                                newBackgroundColorInfo = new WorkbookColorInfo(this.cpBackColor.Color);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;
                        #endregion //BackgroundColorPropertyName

                    case FillPatternStylePropertyName:
                        #region FillPatternStylePropertyName
                        updatePatternStyle = true;
                        break;
                        #endregion //FillPatternStylePropertyName

                    case FontColorPropertyName:
                        #region FontColorPropertyName
                        if (!this.cpFontColor.Color.IsEmpty)
                        {
                            try
                            {
                                font.ColorInfo = new WorkbookColorInfo(this.cpFontColor.Color);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;
                        #endregion //FontColorPropertyName

                    case FontNamePropertyName:
                        #region FontNamePropertyName
                        font.Name = (string)this.treeFontFamily.ActiveNode.Cells["Value"].Value;
                        break;
                        #endregion //FontNamePropertyName

                    case FontSizePropertyName:
                        #region FontSizePropertyName
                        font.Height = (int)this.treeFontSize.ActiveNode.Cells["DataValue"].Value;
                        break;
                        #endregion //FontSizePropertyName

                    case FontStylePropertyName:
                        #region FontStylePropertyName
                        ExcelDefaultableBoolean isBold = ExcelDefaultableBoolean.False;
                        ExcelDefaultableBoolean isItalic = ExcelDefaultableBoolean.False;
                        switch ((FontStylesCustom)this.treeFontStyle.ActiveNode.Cells["Value"].Value)
                        {
                            case FontStylesCustom.Bold:
                                isBold = ExcelDefaultableBoolean.True;
                                break;
                            case FontStylesCustom.Italic:
                                isItalic = ExcelDefaultableBoolean.True;
                                break;
                            case FontStylesCustom.BoldItalic:
                                isBold = ExcelDefaultableBoolean.True;
                                isItalic = ExcelDefaultableBoolean.True;
                                break;
                        }
                        font.Bold = isBold;
                        font.Italic = isItalic;
                        break;
                        #endregion //FontStylePropertyName

                    case HorizontalCellAlignmentPropertyName:
                        #region HorizontalCellAlignmentPropertyName
                        format.Alignment = (HorizontalCellAlignment)this.cboHorizontal.Value;
                        break;
                        #endregion //HorizontalCellAlignmentPropertyName

                    case IndentPropertyName:
                        #region IndentPropertyName
                        format.Indent = (int)this.nmeIndent.Value;
                        break;
                        #endregion //IndentPropertyName

                    case PatternColorPropertyName:
                        #region PatternColorPropertyName
                        if (!this.cpPatternColor.Color.IsEmpty)
                        {
                            try
                            {
                                newPatternColorInfo = new WorkbookColorInfo(this.cpPatternColor.Color);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;
                        #endregion //PatternColorPropertyName

                    case ShrinkToFitPropertyName:
                        #region ShrinkToFitPropertyName
                        format.ShrinkToFit = (this.cbShrinkToFit.Checked) ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
                        break;
                        #endregion //ShrinkToFitPropertyName

                    case StrikethroughPropertyName:
                        #region StrikethroughPropertyName
                        font.Strikeout = this.cbStrikethrough.Checked ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
                        break;
                        #endregion //StrikethroughPropertyName

                    case SubscriptSuperscriptPropertyName:
                        #region SubscriptSuperscriptPropertyName
                        if (this.cbSubscript.Checked)
                            font.SuperscriptSubscriptStyle = FontSuperscriptSubscriptStyle.Subscript;
                        else if (this.cbSuperscript.Checked)
                            font.SuperscriptSubscriptStyle = FontSuperscriptSubscriptStyle.Superscript;
                        break;
                        #endregion //SubscriptSuperscriptPropertyName

                    case UnderlinePropertyName:
                        #region UnderlinePropertyName
                        font.UnderlineStyle = (FontUnderlineStyle)this.cboUnderline.Value;
                        break;
                        #endregion //UnderlinePropertyName

                    case VerticalCellAlignmentPropertyName:
                        #region VerticalCellAlignmentPropertyName
                        format.VerticalAlignment = (VerticalCellAlignment)this.cboVertical.Value;
                        break;
                        #endregion //VerticalCellAlignmentPropertyName

                    case WrapTextPropertyName:
                        #region WrapTextPropertyName
                        format.WrapText = (this.cbWrapText.Checked) ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
                        break;
                        #endregion //WrapTextPropertyName

                    case FormatPropertyName:
                        #region FormatPropertyName
                        if (this.treeFormats.SelectedNodes.Count > 0)
                            format.FormatString = ((FormatInfo)this.treeFormats.SelectedNodes[0].ListObject).Mask;

                        break;
                        #endregion //FormatPropertyName

                    default:
                        Debug.Fail("Unhandled " + propertyName);
                        break;
                }
            }

            if (updatePatternStyle ||
                newBackgroundColorInfo != null ||
                newPatternColorInfo != null)
            {
                CellFillPattern oldPattern = format.Fill as CellFillPattern;

                format.Fill = new CellFillPattern(
                    newBackgroundColorInfo ?? oldPattern.BackgroundColorInfo,
                    newPatternColorInfo ?? oldPattern.PatternColorInfo,
                    updatePatternStyle ? (FillPatternStyle)this.cboPatternStyle.Value : oldPattern.PatternStyle);
            }
        }

        #endregion //CommitChanges

        #region EvaluateIsNormalFont

        private void EvaluateIsNormalFont()
        {
            if (this.shouldEvaluateIsNormalFont)
            {
                this.shouldEvaluateIsNormalFont = false;
                try
                {
                    this.cbNormalFont.Checked = (this.fontPreviewControl1.PreviewFontSize == 220 &&
                        this.fontPreviewControl1.PreviewFontFamily == "Calibri" &&
                        this.fontPreviewControl1.PreviewFontStyle == FontStylesCustom.Regular);
                }
                finally
                {
                    this.shouldEvaluateIsNormalFont = true;
                }
            }
        }

        #endregion //EvaluateIsNormalFont

        #region InitializeUI

        private void InitializeUI(InitialSelectedTab initialTab)
        {
            try
            {
                this.SuspendLayout();
                var font = adapter.Format.Font;
                var isInEditmode = this.adapter.IsInEditMode;

                if (isInEditmode)
                {
                    this.tcMain.Tabs["Number"].Visible = false;
                    this.tcMain.Tabs["Alignment"].Visible = false;
                    this.tcMain.Tabs["Fill"].Visible = false;
                    initialTab = InitialSelectedTab.Font;
                }

                // Initialize Number Tab
                this.numberFormats = Utils.GetNumberFormats();
                this.treeCategory.SetDataBinding(this.numberFormats, null);

                // Initialize Alignment Tab
                this.cboHorizontal.DataSource = Enum.GetValues(typeof(HorizontalCellAlignment));
                this.cboVertical.DataSource = Enum.GetValues(typeof(VerticalCellAlignment));


                // Initialize Font Tab
                this.cboUnderline.DataSource = Enum.GetValues(typeof(FontUnderlineStyle));
                this.treeFontFamily.SetDataBinding(StyleUtils.GetFontFamilyList(), null);
                this.treeFontStyle.SetDataBinding(Enum.GetValues(typeof(FontStylesCustom)), null);
                this.treeFontSize.SetDataBinding(StyleUtils.GetFontSizeValueList().ValueListItems, null);

                // Initialize Fill Tab
                this.cboPatternStyle.DataSource = Enum.GetValues(typeof(FillPatternStyle)).Cast<FillPatternStyle>().Where(x => x != FillPatternStyle.Default).ToList();
                this.fillPreviewControl1.PreviewFill = adapter.Format.Fill;
                
                // Initialize Main TabControl
                this.tcMain.DrawFilter = this;
                this.tcMain.SelectedTab = this.tcMain.Tabs[initialTab.ToString()];

                var selection = adapter.Selection;
                this.formatPreviewControl1.PreviewValue = adapter.SpreadSheet.ActiveWorksheet.Rows[adapter.Selection.ActiveCell.Row].Cells[selection.ActiveCell.Column].Value;
                this.LocalizeStrings();
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        #endregion //InitializeUI

        #region LocalizeStrings

        private void LocalizeStrings()
        {
            var rm = ResourceStrings.ResourceManager;
            this.ultraFormManager1.FormStyleSettings.Caption = rm.GetString("Text_FormatCells");
            this.btnOK.Text = rm.GetString("Text_Ok");
            this.btnCancel.Text = rm.GetString("Text_Cancel");

            // Number Tab
            this.tcMain.Tabs["Number"].Text = rm.GetString("Text_Number");
            this.lblCategory.Text = rm.GetString("Text_Category");
            this.lblSample.Text = rm.GetString("Text_Sample");
            this.lblDecimalPlaces.Text = rm.GetString("Lbl_DecimalPlaces");

            // Alignment Tab
            this.tcMain.Tabs["Alignment"].Text = rm.GetString("Text_Alignment");
            this.lblTextAlignment.Text = rm.GetString("Text_TextAlignment");
            this.lblHorizontal.Text = rm.GetString("Lbl_Horizontal");
            this.lblVertical.Text = rm.GetString("Lbl_Vertical");
            this.lblIndent.Text = rm.GetString("Lbl_Indent");
            this.lblTextControl.Text = rm.GetString("Text_TextControl");
            this.cbWrapText.Text = rm.GetString("Text_ToggleWrapText");
            this.cbShrinkToFit.Text = rm.GetString("Lbl_ShrinkToFit");

            // Font Tab
            this.tcMain.Tabs["Font"].Text = rm.GetString("Text_Font");
            this.lblFont.Text = rm.GetString("Text_Font");
            this.lblFont2.Text = rm.GetString("Lbl_Font");
            this.lblFontStyle.Text = rm.GetString("Lbl_FontStyle");
            this.lblSize.Text = rm.GetString("Lbl_Size");
            this.lblUnderline.Text = rm.GetString("Lbl_Underline");
            this.lblColor.Text = rm.GetString("Lbl_Color");
            this.lblEffects.Text = rm.GetString("Text_Effects");
            this.cbStrikethrough.Text = rm.GetString("Lbl_ToggleStrikeThrough");
            this.cbSuperscript.Text = rm.GetString("Lbl_Superscript");
            this.cbSubscript.Text = rm.GetString("Lbl_Subscript");
            this.lblPreview.Text = rm.GetString("Text_Preview");
            this.cbNormalFont.Text = rm.GetString("Lbl_NormalFont");

            // Fill Tab
            this.tcMain.Tabs["Fill"].Text = rm.GetString("Text_Fill");
            this.lblBackground.Text = rm.GetString("Lbl_BackgroundColor");
            this.lblBackgroundColor.Text = rm.GetString("Lbl_BackgroundColor");
            this.lblPatternColor.Text = rm.GetString("Lbl_PatternColor");
            this.lblPattern.Text = rm.GetString("Lbl_PatternStyle");
            this.lblStyle.Text = rm.GetString("Lbl_PatternStyle");
            this.lblSample2.Text = rm.GetString("Text_Sample");            
        }

        #endregion //LocalizeStrings

        #region SetFill

        private void SetFill()
        {
            WorkbookColorInfo patternColorInfo = WorkbookColorInfo.Automatic;
            WorkbookColorInfo backgroundColorInfo = WorkbookColorInfo.Automatic;

            Color backgroundColor = this.cpBackColor.Color;
            Color patternColor = this.cpPatternColor.Color;

            if (backgroundColor.IsEmpty == false &&
                backgroundColor != Color.Transparent)
            {
                backgroundColorInfo = new WorkbookColorInfo(backgroundColor);
            }

            if (patternColor.IsEmpty == false &&
                patternColor != Color.Transparent)
            {
                patternColorInfo = new WorkbookColorInfo(patternColor);
            }

            this.fillPreviewControl1.PreviewFill = new CellFillPattern(backgroundColorInfo, patternColorInfo, (FillPatternStyle)this.cboPatternStyle.Value);
        }

        #endregion //SetFill

        #endregion //Methods

        #region Event Handlers

        #region cbNormalFont_CheckedChanged

        private void cbNormalFont_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbNormalFont.Checked == false)
                return;

            var currentValue = this.shouldEvaluateIsNormalFont;
            try
            {
                this.shouldEvaluateIsNormalFont = false;

                foreach(var node in new Infragistics.Win.UltraWinTree.UltraTreeNode[]  {
                    this.treeFontFamily.GetNodeByKey("Calibri"),
                    this.treeFontStyle.GetNodeByKey("Regular"),
                    this.treeFontSize.GetNodeByKey("11")})
                {
                    if (node == null)
                        continue;

                    node.Selected = true;
                    node.BringIntoView();
                }

            }
            finally
            {
                this.shouldEvaluateIsNormalFont = currentValue;
            }

        }

        #endregion //cbNormalFont_CheckedChanged

        #region checkBox_CheckedChanged

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == this.cbStrikethrough)
                this.fontPreviewControl1.PreviewStrikethrough = this.cbStrikethrough.Checked;
            else if (sender == this.cbSubscript)
                this.fontPreviewControl1.PreviewSubscript = this.cbSubscript.Checked;
            else if (sender == this.cbSuperscript)
                this.fontPreviewControl1.PreviewSuperscript = this.cbSuperscript.Checked;

            if (this.recordPropertiesChanges == false)
                return;

            if (sender == this.cbWrapText)
                this.changedProperties.Add(WrapTextPropertyName);
            else if (sender == this.cbShrinkToFit)
                this.changedProperties.Add(ShrinkToFitPropertyName);
            else if (sender == this.cbStrikethrough)
                this.changedProperties.Add(StrikethroughPropertyName);
            else if (sender == this.cbSuperscript)
            {
                if (this.cbSuperscript.Checked)
                    this.cbSubscript.Checked = false;

                this.changedProperties.Add(SubscriptSuperscriptPropertyName);
            }
            else if (sender == this.cbSubscript)
            {
                if (this.cbSubscript.Checked)
                    this.cbSuperscript.Checked = false;

                this.changedProperties.Add(SubscriptSuperscriptPropertyName);
            }
        }

        #endregion //checkBox_CheckedChanged

        #region colorPicker_ColorChanged

        private void colorPicker_ColorChanged(object sender, EventArgs e)
        {

            if (sender == this.cpFontColor)
            {
                this.fontPreviewControl1.PreviewFontColor = this.cpFontColor.Color;
            }

            if (this.recordPropertiesChanges == false)
                return;

            if (sender == this.cpFontColor)
                this.changedProperties.Add(FontColorPropertyName);
            else if (sender == this.cpBackColor)
            {
                this.changedProperties.Add(BackgroundColorPropertyName);
                this.SetFill();
            }
            else if (sender == this.cpPatternColor)
            {
                this.changedProperties.Add(PatternColorPropertyName);
                this.SetFill();
            }
        }
        #endregion //colorPicker_ColorChanged

        #region comboEditor_ValueChanged

        private void comboEditor_ValueChanged(object sender, EventArgs e)
        {
            if (sender == this.cboUnderline)
            {
                this.fontPreviewControl1.PreviewUnderline = (FontUnderlineStyle)this.cboUnderline.Value;
            }

            if (this.recordPropertiesChanges == false)
                return;

            if (sender == this.cboHorizontal)
                this.changedProperties.Add(HorizontalCellAlignmentPropertyName);
            else if (sender == this.cboVertical)
                this.changedProperties.Add(VerticalCellAlignmentPropertyName);
            else if (sender == this.cboUnderline)
                this.changedProperties.Add(UnderlinePropertyName);
            else if (sender == this.cboPatternStyle)
            {
                this.changedProperties.Add(FillPatternStylePropertyName);
                this.SetFill();
            }
        }
        #endregion //comboEditor_ValueChanged

        #region DimensionDialog_FormClosing

        private void DimensionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.CommitChanges();
            }
        }

        #endregion //DimensionDialog_FormClosing

        #region numEditor_ValueChanged

        private void numEditor_ValueChanged(object sender, EventArgs e)
        {
            if (this.recordPropertiesChanges == false)
                return;

            if (sender == this.nmeDecimalPlaces)
            {
                if (this.treeCategory.SelectedNodes.Count > 0)
                {
                    NumberFormatInfo numberFormat = (NumberFormatInfo)this.treeCategory.SelectedNodes[0].ListObject;
                    numberFormat.DecimalPlaces = (int)this.nmeDecimalPlaces.Value;
                    this.changedProperties.Add(FormatPropertyName);
                }
            }
            else if (sender == this.nmeIndent)
                this.changedProperties.Add(IndentPropertyName);
        }
        #endregion //numEditor_ValueChanged

        #region tree_AfterSelect

        private void tree_AfterSelect(object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e)
        {

            if (sender == this.treeFormats)
            {
                if (this.treeFormats.SelectedNodes.Count > 0)
                    this.formatPreviewControl1.PreviewFormatMask = ((FormatInfo)this.treeFormats.SelectedNodes[0].ListObject).Mask;
            }
            else if (sender == this.treeFontFamily)
            {
                if (this.treeFontFamily.SelectedNodes.Count > 0)
                    this.fontPreviewControl1.PreviewFontFamily = this.treeFontFamily.SelectedNodes[0].Key;

                this.EvaluateIsNormalFont();
            }
            else if (sender == this.treeFontStyle)
            {
                if (this.treeFontStyle.SelectedNodes.Count > 0)
                    this.fontPreviewControl1.PreviewFontStyle = (FontStylesCustom)this.treeFontStyle.SelectedNodes[0].ListObject;

                this.EvaluateIsNormalFont();
            }
            else if (sender == this.treeFontSize)
            {
                if (this.treeFontSize.SelectedNodes.Count > 0)
                    this.fontPreviewControl1.PreviewFontSize = (int)this.treeFontSize.SelectedNodes[0].Cells["DataValue"].Value;

                this.EvaluateIsNormalFont();
            }

            if (this.recordPropertiesChanges == false)
                return;

            if (sender == this.treeFontFamily)
                this.changedProperties.Add(FontNamePropertyName);
            else if (sender == this.treeFontStyle)
                this.changedProperties.Add(FontStylePropertyName);
            else if (sender == this.treeFontSize)
                this.changedProperties.Add(FontSizePropertyName);
            else if (sender == this.treeFormats)
                this.changedProperties.Add(FormatPropertyName);
        }

        #endregion //tree_AfterSelect

        #region treeCategory_AfterSelect

        private void treeCategory_AfterSelect(object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e)
        {
            if (e.NewSelections.Count > 0)
            {
                var formatInfo = (NumberFormatInfo)e.NewSelections[0].ListObject;
                this.lblFormatDescription.Text = formatInfo.Description;
                var decimalPlaces = formatInfo.DecimalPlaces;
                if (decimalPlaces < 0)
                    this.tableLayoutPanel6.Visible = false;
                else
                {
                    var originalValue = this.recordPropertiesChanges;
                    try
                    {
                        this.recordPropertiesChanges = false;
                        this.nmeDecimalPlaces.Value = decimalPlaces;
                    }
                    finally
                    {
                        this.recordPropertiesChanges = originalValue;
                    }
                    this.tableLayoutPanel6.Visible = true;
                }

                this.treeFormats.Visible = formatInfo.AreFormatsVisible;
                this.treeFormats.SetDataBinding(formatInfo.Formats, null);
                if (this.treeFormats.Nodes.Count > 0)
                    this.treeFormats.Nodes[0].Selected = true;
            }
        }

        #endregion //treeCategory_AfterSelect

        #region tree_InitializeDataNode

        private void tree_InitializeDataNode(object sender, Infragistics.Win.UltraWinTree.InitializeDataNodeEventArgs e)
        {
            if (sender == this.treeFormats)
            {
                e.Node.Override.NodeAppearance.ForeColor = ColorTranslator.FromHtml((string)e.Node.Cells["Color"].Value);
            }
            else
            {
                e.Node.Key = e.Node.Text;
            }
        }

        #endregion //tree_InitializeDataNode

        #region tree_ColumnSetGenerated

        private void tree_ColumnSetGenerated(object sender, Infragistics.Win.UltraWinTree.ColumnSetGeneratedEventArgs e)
        {
            // Set the NodeTextColumn so that the appropriate property on the data object is used for the node text
            if (sender == this.treeFontFamily ||
                sender == this.treeFontSize)
                e.ColumnSet.NodeTextColumn = e.ColumnSet.Columns["DisplayText"];
            else if (sender == this.treeCategory)
                e.ColumnSet.NodeTextColumn = e.ColumnSet.Columns["CategoryName"];
            else if (sender == this.treeFormats)
            {
                e.ColumnSet.NodeTextColumn = e.ColumnSet.Columns["PreviewText"];               
            }
        }

        #endregion //tree_ColumnSetGenerated

        #endregion //Event Handlers

        #region IUIElementDrawFilter

        /// <summary>
        /// Called during the drawing operation of a UIElement for a specific phase
        /// of the operation. This will only be called for the phases returned
        /// from the GetPhasesToFilter method.
        /// </summary>
        /// <param name="drawPhase">Contains a single bit which identifies the current draw phase.</param>
        /// <param name="drawParams">The <see cref="T:Infragistics.Win.UIElementDrawParams" /> used to provide rendering information.</param>
        /// <returns>
        /// Returning true from this method indicates that this phase has been handled and the default processing should be skipped.
        /// </returns>
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            return true;
        }

        /// <summary>
        /// Called before each element is about to be drawn.
        /// </summary>
        /// <param name="drawParams">The <see cref="T:Infragistics.Win.UIElementDrawParams" /> used to provide rendering information.</param>
        /// <returns>
        /// Bit flags indicating which phases of the drawing operation to filter. The DrawElement method will be called only for those phases.
        /// </returns>
        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams)
        {
            if (drawParams.Element is Infragistics.Win.UltraWinTabControl.TabPageAreaUIElement)
                return DrawPhase.BeforeDrawBorders;
            else if (drawParams.Element is Infragistics.Win.UltraWinTabs.TabItemUIElement)
                return DrawPhase.BeforeDrawFocus;
            return DrawPhase.None;
        }

        #endregion //IUIElementDrawFilter

        #region Enum InitialSelectedTab

        internal enum InitialSelectedTab
        {
            Number,
            Alignment,
            Font,
            Fill,
        }

        #endregion // Enum InitialSelectedTab

    }
}
