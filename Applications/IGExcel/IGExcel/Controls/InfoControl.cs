using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Documents.Excel;
using Infragistics.Win.UltraWinEditors;

namespace IGExcel.Controls
{
    public partial class InfoControl : UserControl
    {

        #region Members

        private bool isInitializing = false;
        private DocumentProperties documentProperties;

        #endregion //Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoControl"/> class.
        /// </summary>
        public InfoControl()
        {
            this.InitializeComponent();
            this.InitializeUI();
        }

        #endregion

        #region Base Class Overrides

        #region OnVisibleChanged

        /// <summary>
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible == false)
                this.documentProperties = null;
        }

        #endregion //OnVisibleChanged

        #endregion //Base Class Overrides

        #region Methods

        #region Initialize

        internal void Initialize(string workbookName, Workbook workbook)
        {
            if (workbook == null)
                return;

            this.documentProperties = workbook.DocumentProperties;

            this.lblInfo.Value = string.Format("{0} / <font color=\"Gray\">{1}</font>", ResourceStrings.ResourceManager.GetString("Text_Info"), workbookName);

            try
            {
                this.isInitializing = true;
                this.txtTitle.Text = this.documentProperties.Title;
                this.txtStatus.Text = this.documentProperties.Status;
                this.txtSubject.Text = this.documentProperties.Subject;
                this.txtCategory.Text = this.documentProperties.Category;
                this.txtCompany.Text = this.documentProperties.Company;
                this.txtComments.Text = this.documentProperties.Comments;
                this.txtTags.Text = this.documentProperties.Keywords;
                this.txtManager.Text = this.documentProperties.Manager;
                this.txtAuthor.Text = this.documentProperties.Author;
            }
            finally
            {
                this.isInitializing = false;
            }
        }

        #endregion //Initialize

        #region InitializeUI

        private void InitializeUI()
        {
            this.txtTitle.Tag = DocumentPropertyIds.Title;
            this.txtStatus.Tag = DocumentPropertyIds.Status;
            this.txtSubject.Tag = DocumentPropertyIds.Subject;
            this.txtCategory.Tag = DocumentPropertyIds.Category;
            this.txtCompany.Tag = DocumentPropertyIds.Company;
            this.txtComments.Tag = DocumentPropertyIds.Comments;
            this.txtTags.Tag = DocumentPropertyIds.Tags;
            this.txtManager.Tag = DocumentPropertyIds.Manager;
            this.txtAuthor.Tag = DocumentPropertyIds.Author;

            this.LocalizeStrings();
        }

        #endregion //InitializeUI

        #region LocalizeStrings

        private void LocalizeStrings()
        {
            System.Resources.ResourceManager rm = ResourceStrings.ResourceManager;
            this.lblTitle.Text = rm.GetString("Text_Title");
            this.txtTitle.NullText = rm.GetString("Text_AddATitle");

            this.lblStatus.Text = rm.GetString("Text_Status");
            this.txtStatus.NullText = rm.GetString("Text_Status");

            this.lblSubject.Text = rm.GetString("Text_Subject");
            this.txtSubject.NullText = rm.GetString("Text_SpecifyTheSubject");

            this.lblCategory.Text = rm.GetString("Text_Category");
            this.txtCategory.NullText = rm.GetString("Text_AddACategory");

            this.lblCompany.Text = rm.GetString("Text_Company");
            this.txtCompany.NullText = rm.GetString("Text_SpecifyTheCompany");

            this.lblComments.Text = rm.GetString("Text_Comments");
            this.txtComments.NullText = rm.GetString("Text_AddComments");

            this.lblTags.Text = rm.GetString("Text_Tags");
            this.txtTags.NullText = rm.GetString("Text_AddATag");

            this.lblManager.Text = rm.GetString("Text_Manager");
            this.txtManager.NullText = rm.GetString("Text_SpecifyTheManager");

            this.lblAuthor.Text = rm.GetString("Text_Author");
            this.txtAuthor.NullText = rm.GetString("Text_SpecifyTheAuthor");

            this.lblProperties.Text = rm.GetString("Text_Properties");
            this.lblRelatedPeople.Text = rm.GetString("Text_RelatedPeople");
        }

        #endregion //LocalizeStrings

        #endregion //Methods

        #region Event Handlers

        #region txtEditor_ValueChanged

        private void txtEditor_ValueChanged(object sender, EventArgs e)
        {
            if (this.isInitializing)
                return;

            if (this.documentProperties == null)
                return;

            UltraTextEditor editor = sender as UltraTextEditor;
            if (editor == null)
                return;

            if (editor.Tag is DocumentPropertyIds == false)
                return;

            switch ((DocumentPropertyIds)editor.Tag)
            {
                case DocumentPropertyIds.Title:
                    documentProperties.Title = editor.Text;
                    break;
                case DocumentPropertyIds.Status:
                    documentProperties.Status = editor.Text;
                    break;
                case DocumentPropertyIds.Subject:
                    documentProperties.Subject = editor.Text;
                    break;
                case DocumentPropertyIds.Category:
                    documentProperties.Category = editor.Text;
                    break;
                case DocumentPropertyIds.Company:
                    documentProperties.Company = editor.Text;
                    break;
                case DocumentPropertyIds.Comments:
                    documentProperties.Comments = editor.Text;
                    break;
                case DocumentPropertyIds.Tags:
                    documentProperties.Keywords = editor.Text;
                    break;
                case DocumentPropertyIds.Manager:
                    documentProperties.Manager = editor.Text;
                    break;
                case DocumentPropertyIds.Author:
                    documentProperties.Author = editor.Text;
                    break;
            }
        }

        #endregion //txtEditor_ValueChanged

        #endregion //Event Handlers

        #region DocumentPropertyIds - Nested Enum

        private enum DocumentPropertyIds
        {
            Title,
            Status,
            Subject,
            Category,
            Company,
            Comments,
            Tags,
            Manager,
            Author,
        }

        #endregion 

    }
}
