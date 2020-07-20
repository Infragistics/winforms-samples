using System;
using System.Drawing;
using System.IO;
using System.Resources;

namespace INTune
{
    internal static class Utilities
    {
        #region GetImageFromResource
        /// <summary>
        /// Returns an Image from the EmbeddedResources.
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="imgName"></param>
        /// <returns></returns>
        internal static Bitmap GetImageFromResource(string imgPath, string imgName)
        {
            Bitmap bmp = null;
            Type thisType = typeof(Utilities);

            
            string fullResourceName = string.Format("INTune.Images.{0}.{1}", imgPath, imgName);
            using (Stream stream = thisType.Assembly.GetManifestResourceStream(fullResourceName))
            {
                if (stream != null)
                {
                    bmp = (Bitmap) Image.FromStream(stream);
                }
            }
            return bmp;
        }
        #endregion // GetImageFromResource

        #region GetLocalizedString
        /// <summary>
        /// Localizes a string using the ResourceManager.
        /// </summary>
        /// <param name="currentString"></param>
        /// <returns></returns>
        internal static string GetLocalizedString(string currentString)
        {
            ResourceManager rm = INTune.Properties.Resources.ResourceManager;
            string localizedString = rm.GetString(currentString);
            return string.IsNullOrEmpty(localizedString) ? currentString : localizedString;
        }
        #endregion // GetLocalizedString
    }


}
