using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Win;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win.UltraWinToolbars;
using System.Resources;

namespace ProjectManager
{
    /// <summary>
    /// Utility class to perform context-independent functionality
    /// </summary>
    internal class Utilities
    {

        #region Static ExecutingAssembly

        private static Assembly executingAssembly;

        private static Assembly ExecutingAssembly
        {
            get
            {
                if (executingAssembly == null)
                    executingAssembly = Assembly.GetExecutingAssembly();
                return executingAssembly;
            }
        }

        #endregion //Static ExecutingAssembly

        #region SetRibbonGroupToolsEnabledState

        /// <summary>
        /// Sets the Enabled property of the tools within the specified RibbonGroup.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="enabledState">if set to <c>true</c> [enabled state].</param>
        internal static  void SetRibbonGroupToolsEnabledState(RibbonGroup group, bool enabled)
        {
            if (group == null)
                return;

            foreach (ToolBase tool in group.Tools)
            {
                tool.SharedProps.Enabled = enabled;
            }
        }

        #endregion // SetRibbonGroupToolsEnabledState

        #region ColorizeImages

        /// <summary>
        /// Creates new images using a pixel-based color replacement on the image from the provided imagelist. 
        /// </summary>
        /// <param name="oldColor">The old Color.</param>
        /// <param name="colors">The dictionary containing the new resolved colors</param>
        /// <param name="defaultImageList">The default ImageList.</param>
        /// <param name="colorizedImageList">The colorized ImageList.</param>
        internal static void ColorizeImages(Color oldColor, Dictionary<string, Color> colors, ref ImageList defaultImageList, ref ImageList colorizedImageList)
        {
            // loop through the default imageliss, colorize the image and put it into the colorized imagelist
            foreach (string key in defaultImageList.Images.Keys)
            {
                Bitmap bitmap = defaultImageList.Images[key] as Bitmap;
                if (bitmap != null)
                {
                    bitmap = bitmap.Clone() as Bitmap;
                    foreach (string colorKey in colors.Keys)
                    {
                        if (key.EndsWith(colorKey))
                        {
                            Infragistics.Win.DrawUtility.ReplaceColor(ref bitmap, oldColor, colors[colorKey]);
                            break;
                        }
                    }

                    int index = colorizedImageList.Images.IndexOfKey(key);
                    if (index >= 0)
                    {
                        Image oldImage = colorizedImageList.Images[index];
                        colorizedImageList.Images.RemoveByKey(key);
                        if (oldImage != null)
                            oldImage.Dispose();
                    }
                    colorizedImageList.Images.Add(key, bitmap);
                }
            }
        }

        #endregion // ColorizeImages

        #region GetAssemblyAttribute

        /// <summary>
        /// Retrieves the string representation of the request AssemblyAttribute from the project's AssemblyInfo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string GetAssemblyAttribute<T>(Func<T, string> value)
            where T : Attribute
        {
            T attribute = (T)Attribute.GetCustomAttribute(Utilities.ExecutingAssembly, typeof(T));
            return value.Invoke(attribute);
        }

        #endregion // GetAssemblyAttribute

        #region GetData

        /// <summary>
        /// Gets the bindable data.
        /// </summary>
        /// <returns></returns>
        internal static DataSet GetData(string fileName)
        {
            // Get the temporary data from the XML file
            System.IO.Stream stream = GetEmbeddedResourceStream(fileName);

            // Convert the stream to the dataset
            DataSet data = new DataSet();
            data.ReadXml(stream);
            return data;
        }

        #endregion //GetData

        #region GetEmbeddedResourceStream

        /// <summary>
        /// Gets the embedded resource stream.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        internal static System.IO.Stream GetEmbeddedResourceStream(string resourceName)
        {
            System.IO.Stream stream = Utilities.ExecutingAssembly.GetManifestResourceStream(resourceName);
            Debug.Assert(stream != null, "Unable to locate embedded resource.", "Resource name: {0}", resourceName);
            return stream;
        }

        #endregion //GetEmbeddedResourceStream

        #region GetLocalizedString
        /// <summary>
        /// Localizes a string using the ResourceManager.
        /// </summary>
        /// <param name="currentString"></param>
        /// <returns></returns>
        internal static string GetLocalizedString(string currentString)
        {
            ResourceManager rm = ProjectManager.Properties.Resources.ResourceManager;
            string localizedString = rm.GetString(currentString);
            return (string.IsNullOrEmpty(localizedString) ? currentString : localizedString).Replace('_', ' ');
        }
        #endregion // GetLocalizedString


        #region GetStyleLibraryResourceNames

        /// <summary>
        /// Gets an array of  style library resource names.
        /// </summary>
        /// <returns></returns>
        internal static string[] GetStyleLibraryResourceNames()
        {
            List<string> resourceStrings = new List<string>(Utilities.ExecutingAssembly.GetManifestResourceNames());

            return resourceStrings.FindAll(i => i.EndsWith(".isl")).ToArray();
        }

        #endregion // GetStyleLibraryResourceNames

        #region ToggleDefaultableBoolean
        /// <summary>
        /// Toggles a DefaultableBoolean value.
        /// </summary>
        /// <param name="value">Value to be toggled.</param>
        /// <returns>Changed value.</returns>
        internal static DefaultableBoolean ToggleDefaultableBoolean(DefaultableBoolean value)
        {
            if (value == Infragistics.Win.DefaultableBoolean.True)
            {
                value = Infragistics.Win.DefaultableBoolean.False;
            }
            else
            {
                value = Infragistics.Win.DefaultableBoolean.True;
            }

            return value;
        }
        #endregion // ToggleDefaultableBoolean

    }
}
