using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace IGExcel
{
    public class RecentFilesManager:
        INotifyPropertyChanged
    {

        #region Members

        private const int historyLimit = 25;
        private PropertyInfo recentFilesRepositoryProperty;
        private PropertyInfo recentFoldersRepositoryProperty;

        private ApplicationSettingsBase settings;
        private ObservableCollection<RecentFileInfo> recentFiles;
        private ObservableCollection<RecentFileInfo> recentFolders;

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentFilesManager"/> class.
        /// </summary>
        public RecentFilesManager()
        {
            RecentFolders = new ObservableCollection<RecentFileInfo>();
            RecentFiles = new ObservableCollection<RecentFileInfo>();
        }

        #endregion //Constructor

        #region Properties

        #region RecentFiles

        /// <summary>
        /// Gets the recent files.
        /// </summary>
        public ObservableCollection<RecentFileInfo> RecentFiles
        {
            get { return recentFiles; }
            private set { recentFiles = value; NotifyPropertyChanged("RecentFiles"); }
        }

        #endregion //RecentFiles

        #region RecentFolders

        /// <summary>
        /// Gets the recent folders.
        /// </summary>
        public ObservableCollection<RecentFileInfo> RecentFolders
        {
            get { return recentFolders; }
            private set { recentFolders = value; NotifyPropertyChanged("RecentFolders"); }
        }

        #endregion //RecentFolders

        #endregion //Properties

        #region Methods

        #region SetRepositories

        /// <summary>
        /// Populate the RecentFiles and RecentFolder collections.
        /// </summary>
        public void SetRepositories(ApplicationSettingsBase settings,
                                    Expression<Func<ApplicationSettingsBase, string>> recentFilesRepositoryExpresion,
                                    Expression<Func<ApplicationSettingsBase, string>> recentFoldersRepositoryExpresion)
        {
            this.settings = settings;


            recentFilesRepositoryProperty = (PropertyInfo)((MemberExpression)recentFilesRepositoryExpresion.Body).Member;
            var filesSerialized = (string)recentFilesRepositoryProperty.GetValue(settings, null);

            bool saveChanges = false;
            if (string.IsNullOrEmpty(filesSerialized))
            {
                this.recentFiles = new ObservableCollection<RecentFileInfo>();
                saveChanges = true;

            }
            else
            {
                this.recentFiles = DeserializeObject<ObservableCollection<RecentFileInfo>>(filesSerialized);
            }


            recentFoldersRepositoryProperty = (PropertyInfo)((MemberExpression)recentFoldersRepositoryExpresion.Body).Member;
            var foldersSerizlized = (string)recentFoldersRepositoryProperty.GetValue(settings, null);


            if (string.IsNullOrEmpty(foldersSerizlized))
            {
                this.recentFolders = new ObservableCollection<RecentFileInfo>();
                saveChanges = true;
            }
            else
            {
                this.recentFolders = DeserializeObject<ObservableCollection<RecentFileInfo>>(foldersSerizlized);
            }

            if (saveChanges)
                Save();

            if (this.recentFolders.Count == 0)
            {
                TryToDetectFolders();
            }
        }

        #endregion //SetRepositories

        #region Save

        /// <summary>
        /// Saves the RecentFiles and RecentFolders in the application settings
        /// </summary>
        public void Save()
        {
            SortRecentFolders();
            SortRecentFiles();

            this.recentFilesRepositoryProperty.SetValue(settings, SerializeObject(this.recentFiles), null);
            this.recentFoldersRepositoryProperty.SetValue(settings, SerializeObject(this.recentFolders), null);

            this.settings.Save();
        }

        #endregion //Save

        #region SortRecentFolders

        private void SortRecentFolders()
        {
            var sorted = this.recentFolders.OrderByDescending(x => x.DateOpened).ToList();

            this.recentFolders.Clear();

            foreach (var item in sorted)
            {
                this.recentFolders.Add(item);
            }

            NotifyPropertyChanged("RecentFolders");
        }

        #endregion //SortRecentFolders

        #region SortRecentFiles

        private void SortRecentFiles()
        {
            var sorted = this.RecentFiles.OrderByDescending(x => x.DateOpened).ToList();

            this.recentFiles.Clear();

            foreach (var item in sorted)
            {
                this.recentFiles.Add(item);
            }

            NotifyPropertyChanged("RecentFiles");
        }

        #endregion //SortRecentFiles

        #region AddFile

        /// <summary>
        /// Adds the file to the RecentFiles collection
        /// </summary>
        public void AddFile(string filePath, bool save = false)
        {
            if (!RecentFilesContains(filePath))
            {
                if (RecentFiles.Count > historyLimit)
                {
                    RecentFiles.RemoveAt(RecentFiles.Count - 1);
                }

                RecentFiles.Add(new RecentFileInfo(filePath));
            }


            if (save)
                Save();
            else
                SortRecentFiles();
        }

        #endregion //AddFile

        #region AddFolder

        /// <summary>
        /// Adds the folder to the RecentFolders collection
        /// </summary>
        public void AddFolder(string dir, bool save = false)
        {
            if (!RecentFoldersContains(dir))
            {
                if (RecentFolders.Count > historyLimit)
                {
                    RecentFolders.RemoveAt(RecentFolders.Count - 1);
                }

                RecentFolders.Add(new RecentFileInfo(dir));
            }

            if (save)
                Save();
            else
                SortRecentFolders();
        }

        #endregion //AddFolder

        #region GetCollection

        internal ObservableCollection<RecentFileInfo> GetCollection(string key)
        {
            switch (key)
            {
                case "RecentFiles":
                    return this.RecentFiles;
                case "RecentFolders":
                    return this.RecentFolders;
                default:
                    return null;
            }
        }

        #endregion //GetCollection

        #region RecentFilesContains

        private bool RecentFilesContains(string filePath)
        {
            filePath = filePath.ToLower();
            var item = RecentFiles.FirstOrDefault(f => f.FullName.ToLower() == filePath);

            if (item != null)
            {
                item.DateOpened = DateTime.Now;
                return true;
            }

            return false;
        }

        #endregion //RecentFilesContains

        #region RemoveFile

        /// <summary>
        /// Removes the file from the RecentFiles collection
        /// </summary>
        public void RemoveFile(string filePath, bool save = false)
        {
            filePath = filePath.ToLower();

            var file = RecentFiles.FirstOrDefault(f => f.FullName.ToLower() == filePath);

            if (file != null)
            {
                RecentFiles.Remove(file);
            }


            if (save)
                Save();
        }

        #endregion //RemoveFile

        #region RemoveFolder

        /// <summary>
        /// Removes the folder from the RecentFolders collection
        /// </summary>
        public void RemoveFolder(string dirPath, bool save = false)
        {
            dirPath = dirPath.ToLower();

            var dir = RecentFolders.FirstOrDefault(d => d.FullName.ToLower() == dirPath);

            if (dir != null)
            {
                RecentFolders.Remove(dir);
            }

            if (save)
                Save();
        }

        #endregion //RemoveFolder

        #region TryToDetectFolders

        private void TryToDetectFolders()
        {
            try
            {
                AddFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                AddFolder(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));
            }
            catch { }
        }

        #endregion //TryToDetectFolders

        #region RecentFoldersContains

        private bool RecentFoldersContains(string directory)
        {
            directory = directory.ToLower();
            var item = RecentFolders.FirstOrDefault(d => d.FullName.ToLower() == directory);

            if (item != null)
            {
                item.DateOpened = DateTime.Now;
                return true;
            }

            return false;
        }

        #endregion //RecentFoldersContains

        #region SerializeObject

        private static string SerializeObject<T>(T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, objectToSerialize);
            return textWriter.ToString();
        }

        #endregion  SerializeObject

        #region DeserializeObject

        private static T DeserializeObject<T>(string str)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            TextReader reader = new StringReader(str);

            return (T)xmlSerializer.Deserialize(reader);

        }

        #endregion //DeserializeObject

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion //PropertyChanged

        #endregion //Methods

        #region RecentFileInfo -  Nested Class

        [Serializable()]
        public class RecentFileInfo
        {
            public string FullName { get; set; }
            public DateTime DateOpened;


            [XmlIgnoreAttribute]
            public string Name
            {
                get { return Path.GetFileName(FullName); }
            }


            public RecentFileInfo()
            {

            }

            public RecentFileInfo(string filePath)
            {
                FullName = filePath;
                DateOpened = DateTime.Now;
            }
        }

        #endregion //RecentFileInfo

    }
}
