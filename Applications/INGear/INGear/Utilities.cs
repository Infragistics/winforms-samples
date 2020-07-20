using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Resources;

namespace Showcase.INGear
{
    internal static class Utilities
    {

        #region Private Members
        private static Dictionary<CachedImages, Image> cachedImages;
        private static Random random = new Random();
        #endregion // Private Members

        #region Methods

        #region CachedImageFromCategory

        /// <summary>
        /// Converts a CategoryType enum value to the appropriate CachedImage enum value.
        /// </summary>
        internal static CachedImages CachedImagesFromCategory(CategoryType category, bool invoice = false)
        {
            switch (category)
            {
                case CategoryType.Batteries:
                    return invoice ? CachedImages.Invoice_Batteries : CachedImages.Batteries;
                case CategoryType.Brakes:
                    return invoice ? CachedImages.Invoice_Brakes : CachedImages.Brakes;
                case CategoryType.EngineBlocks:
                    return invoice ? CachedImages.Invoice_EngineBlocks : CachedImages.EngineBlocks;
                case CategoryType.Pistons:
                    return invoice ? CachedImages.Invoice_Pistons : CachedImages.Pistons;
                case CategoryType.Sparkplugs:
                    return invoice ? CachedImages.Invoice_Sparkplugs : CachedImages.Sparkplugs;
                case CategoryType.Suspension:
                    return invoice ? CachedImages.Invoice_Suspension : CachedImages.Suspension;
                case CategoryType.Tires:
                    return invoice ? CachedImages.Invoice_Tires : CachedImages.Tires;
                case CategoryType.Transmissions:
                    return invoice ? CachedImages.Invoice_Transmissions : CachedImages.Transmissions;
            }
            Debug.Fail("Unknown Category");
            return CachedImages.Logo;
        }

        #endregion CachedImageFromCategory

        #region CategoryTypeToString

        /// <summary>
        /// Converts the CategoryType to localized readable text.
        /// </summary>
        internal static string CategoryTypeToString(CategoryType category)
        {
            string toString;
            switch (category)
            {
                case CategoryType.EngineBlocks:
                    toString = "Engine Blocks";
                    break;
                case CategoryType.Sparkplugs:
                    toString = "Spark Plugs";
                    break;
                default:
                    toString = category.ToString();
                    break;
            }

            return LocalizeString(toString);
        }

        #endregion CategoryTypeToString

        #region CreateCachedImage
        private static Image CreateCachedImage(CachedImages cachedImages)
        {
            Bitmap bmp;
            switch (cachedImages)
            {
                case CachedImages.Background:
                case CachedImages.Batteries:
                case CachedImages.Brakes:
                case CachedImages.Daily:
                case CachedImages.EngineBlocks:
                case CachedImages.Inventory:
                case CachedImages.Invoice_Batteries:
                case CachedImages.Invoice_Brakes:
                case CachedImages.Invoice_EngineBlocks:
                case CachedImages.Invoice_Pistons:
                case CachedImages.Invoice_Sparkplugs:
                case CachedImages.Invoice_Suspension:
                case CachedImages.Invoice_Tires:
                case CachedImages.Invoice_Transmissions:
                case CachedImages.Logo:
                case CachedImages.Monthly:
                case CachedImages.Pistons:
                case CachedImages.RadialCenter:
                case CachedImages.Schedule:
                case CachedImages.Sparkplugs:
                case CachedImages.Suspension:
                case CachedImages.Tires:
                case CachedImages.Transmissions:
                case CachedImages.Weekly:
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

        #region GenerateSerialNumber

        /// <summary>
        /// Generates a randomized string from Guid using a passed in starting index and length.
        /// </summary>
        internal static string GenerateSerialNumber(int startIndex, int length)
        {
            string guid = Guid.NewGuid().ToString();

            if (startIndex >= guid.Length)
                startIndex = 0;

            return guid.Substring(startIndex, length).ToUpper();
        }

        #endregion //GenerateSerialNumber

        #region GetCachedImage

        /// <summary>
        /// Gets the cached image.
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
                
        #region GetImageFromResource
        /// <summary>
        /// Gets the image from the embedded resources
        /// </summary>
        private static Bitmap GetImageFromResource(CachedImages cachedImages)
        {
            Bitmap bmp;
            string resourceName = cachedImages.ToString();
            Type thisType = typeof(Utilities);
            string fullResourceName = string.Format("Showcase.INGear.Images.{0}.png", resourceName);
            Stream stream = thisType.Assembly.GetManifestResourceStream(fullResourceName);
            bmp = (Bitmap)Image.FromStream(stream);
            bmp.MakeTransparent(Color.Magenta);
            return bmp;
        }
        #endregion // GetImageFromResource

        #region LocalizeString

        /// <summary>
        /// Localizes a string using the ResourceManager.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string LocalizeString(string value)
        {
            ResourceManager rm = Showcase.INGear.Properties.Resources.ResourceManager;
            string localizedString = rm.GetString(value.Replace(' ', '_'));
            if (string.IsNullOrEmpty(localizedString))
                return value;

            return localizedString;
        }
        #endregion //LocalizeString

        #region RandomDouble

        private static double RandomDouble()
        {
            return random.NextDouble();
        }

        internal static double RandomDouble(int rangeMin, int rangeMax)
        {
            return RandomDouble() * (rangeMax - rangeMin);
        }

        #endregion //RandomDouble

        #region RandomInt

        internal static int RandomInt(int rangeMin, int rangeMax)
        {
            return random.Next(rangeMin, rangeMax);
        }

        #endregion //RandomInt

        #endregion //Methods
    }
}
