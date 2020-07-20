using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace IGExcel
{

    public class NumberFormatInfo : INotifyPropertyChanged
    {

        #region Constructor

        public NumberFormatInfo()
        {
            this.Formats = new BindingList<FormatInfo>();
            this.DecimalPlaces = 2;
            this.IsCustom = false;
            this.AreFormatsVisible = true;
        }
        #endregion //Constructor

        #region Properties

        #region AreFormatsVisible
        private const string AreFormatsVisiblePropertyName = "AreFormatsVisible";
        private bool areFormatsVisible;
        public bool AreFormatsVisible
        {
            get { return this.areFormatsVisible; }
            set 
            {
                if (this.areFormatsVisible != value)
                {
                    this.areFormatsVisible = value;
                    this.NotifyPropertyChanged(AreFormatsVisiblePropertyName);
                }
            }
        }
        #endregion //AreFormatsVisible

        #region CategoryName
        private const string CategoryNamePropertyName = "CategoryName";
        private string categoryName;
        public string CategoryName
        {
            get { return this.categoryName; }
            set 
            {
                if (this.categoryName != value)
                {
                    this.categoryName = value;
                    this.NotifyPropertyChanged(CategoryNamePropertyName);
                }
            }
        }
        #endregion //CategoryName

        #region DecimalPlaces
        private const string DecimalPlacesPropertyName = "DecimalPlaces";
        private int decimalPlaces;
        public int DecimalPlaces
        {
            get { return this.decimalPlaces; }
            set 
            {
                if (this.decimalPlaces != value)
                {
                    this.decimalPlaces = value;
                    this.NotifyPropertyChanged(DecimalPlacesPropertyName);
                }
            }
        }
        #endregion //DecimalPlaces

        #region Description
        private const string DescriptionPropertyName = "Description";
        private string description;
        public string Description
        {
            get { return this.description; }
            set 
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.NotifyPropertyChanged(DescriptionPropertyName);
                }
            }
        }
        #endregion //Description

        #region FormatsHeader
        private const string FormatsHeaderPropertyName = "FormatsHeader";
        private string formatsHeader;
        public string FormatsHeader
        {
            get { return this.formatsHeader; }
            set 
            {
                if (this.formatsHeader != value)
                {
                    this.formatsHeader = value;
                    this.NotifyPropertyChanged(FormatsHeaderPropertyName);
                }
            }
        }
        #endregion //FormatsHeader

        #region Formats
        private const string FormatsPropertyName = "Formats";
        private BindingList<FormatInfo> formats;
        internal BindingList<FormatInfo> Formats
        {
            get { return this.formats; }
            set 
            {
                if (this.formats != value)
                {
                    this.formats = value;
                    this.NotifyPropertyChanged(FormatsPropertyName);
                }
            }
        }
        #endregion //Formats

        #region IsCustom
        private const string IsCustomPropertyName = "IsCustom";
        private bool isCustom;
        public bool IsCustom
        {
            get { return this.isCustom; }
            set 
            {
                if (this.isCustom != value)
                {
                    this.isCustom = value;
                    this.NotifyPropertyChanged(IsCustomPropertyName);
                }
            }
        }
        #endregion //IsCustom

        #endregion //Properties

        #region Methods

        #region AddFormatInfo
        public FormatInfo AddFormatInfo(string mask)
        {
            var format = new FormatInfo(mask, mask);
            this.Formats.Add(format);
            return format;
        }

        public void AddFormatInfo(string mask, string previewText)
        {
            this.Formats.Add(new FormatInfo(mask, previewText));
        }

        #endregion //AddFormatInfo

        #region FindFormat

        public FormatInfo FindFormat(string mask)
        {
            return this.Formats.FirstOrDefault(x => x.Mask == mask);
        }

        #endregion //FindFormat

        #endregion //Methods

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion //PropertyChanged
    }

    public class FormatInfo : INotifyPropertyChanged
    {
        #region Constructors

        public FormatInfo(string mask, string previewText)
        {
            this.mask = mask;
            this.previewText = previewText;
            this.color = "#1E1E1E";
        }

        public FormatInfo(string mask, string previewText, string color)
        {
            this.mask = mask;
            this.previewText = previewText;
            this.color = color;
        }

        #endregion //Constructors

        #region Properties

        #region Color
        private const string ColorPropertyName = "Color";
        private string color;
        public string Color
        {
            get { return this.color; }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.NotifyPropertyChanged(ColorPropertyName);
                }
            }
        }
        #endregion //Color

        #region Mask
        private const string MaskPropertyName = "Mask";
        private string mask;
        public string Mask
        {
            get { return this.mask; }
            set
            {
                if (this.mask != value)
                {
                    this.mask = value;
                    this.NotifyPropertyChanged(MaskPropertyName);
                }
            }
        }
        #endregion //Mask

        #region PreviewText
        private const string PreviewTextPropertyName = "PreviewText";
        string previewText;
        public string PreviewText
        {
            get { return this.previewText; }
            set
            {
                if (this.previewText != value)
                {
                    this.previewText = value;
                    this.NotifyPropertyChanged(PreviewTextPropertyName);
                }
            }
        }
        #endregion //PreviewText

        #endregion //Properties

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion //PropertyChanged
    }

    public class NumberFormatInfoAdvanced : NumberFormatInfo
    {

        #region Members
        private const string Delimiter = ".";
        #endregion //Members

        #region Constructor

        public NumberFormatInfoAdvanced()
            : base()
        {
            this.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "DecimalPlaces")
                {
                    this.UpdateMaskAndPreviewText();
                }
            };
        }

        #endregion //Constructor

        #region Methods

        #region UpdateMaskAndPreviewText

        private void UpdateMaskAndPreviewText()
        {
            Regex regexDecimalPart = new Regex("\\" + Delimiter + "(\\d+)?");
            Regex regexDigit = new Regex("((\\d+[,]\\d+))|(\\d+)|([#]+[,]?([#]+)?)");

            StringBuilder newDecimalPartPreview = new StringBuilder();
            StringBuilder newDecimalPartMask = new StringBuilder();

            if (this.DecimalPlaces > 0)
            {
                newDecimalPartPreview.Append(Delimiter);
                newDecimalPartMask.Append(Delimiter);

                for (int i = 0; i < this.DecimalPlaces; i++)
                {
                    newDecimalPartPreview.Append("1");
                    newDecimalPartMask.Append("0");
                }
            }

            foreach (var format in Formats)
            {
                if (!format.PreviewText.Contains(Delimiter) && this.DecimalPlaces > 0)
                {
                    var matchPreviewDigit = regexDigit.Match(format.PreviewText);

                    if (matchPreviewDigit.Length > 0)
                    {
                        format.PreviewText = format.PreviewText.Replace(matchPreviewDigit.Value, matchPreviewDigit.Value + newDecimalPartPreview.ToString());
                    }

                    var matchesMaskDigit = regexDigit.Matches(format.Mask);

                    foreach (Match match in matchesMaskDigit)
                    {
                        format.Mask = format.Mask.Replace(match.Value, match.Value + newDecimalPartMask.ToString());

                        if (matchesMaskDigit.Count > 1)
                        {
                            if (matchesMaskDigit[0].Value == matchesMaskDigit[1].Value)
                                break;
                        }
                    }

                    continue;
                }

                var matchPreview = regexDecimalPart.Match(format.PreviewText);

                if (matchPreview.Length > 0)
                {
                    format.PreviewText = format.PreviewText.Replace(matchPreview.Value, newDecimalPartPreview.ToString());
                }

                var matchMask = regexDecimalPart.Match(format.Mask);

                if (matchPreview.Length > 0)
                {
                    format.Mask = format.Mask.Replace(matchMask.Value, newDecimalPartMask.ToString());
                }
            }
        }

        #endregion //UpdateMaskAndPreviewText

        #endregion //Methods
    }
}
