using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using Infragistics.Win;
using Infragistics.Win.UltraWinSchedule;
using Infragistics.Win.UltraWinGanttView.Grid;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGanttView.Internal;
using Infragistics.Win.UltraWinToolbars;
using Infragistics.Win.UltraWinSchedule.TaskUI;
using Infragistics.Win.UltraWinGanttView;
using Infragistics.Win.UltraMessageBox;
using Infragistics.Win.UltraWinListView;
using System.Diagnostics;
using Infragistics.Shared;
using Infragistics.Win.Printing;
using System.Threading;

namespace ProjectManager
{
    public partial class ProjectManagerForm : Form
    {
        #region Enums

        #region GanttViewAction
        /// <summary>
        /// Enumeration of actions that can be perfomed.
        /// </summary>
        private enum GanttViewAction
        {
            IndentTask,
            OutdentTask,
            MoveTaskDateForward,
            MoveTaskDateBackward,
        }
        #endregion // GanttViewAction

        #region MoveStartDate
        /// <summary>
        /// Enumeration of TimeSpans
        /// </summary>
        private enum TimeSpanForMoving
        {
            OneDay,
            OneWeek,
            FourWeeks,
        }
        #endregion // MoveStartDate

        #region FontProperties
        /// <summary>
        /// Enumeration of font related properties.
        /// </summary>
        private enum FontProperties
        {
            Bold,
            Italics,
            Underline,
        }
        #endregion // FontProperties

        #endregion // Enums

        #region Member Variables

        private bool cellActivationRecursionFlag = false;
        private int currentThemeIndex;
        private ResourceManager rm = ProjectManager.Properties.Resources.ResourceManager;
        private static ManualResetEvent splashLoadedEvent;
        private const int TaskRowHeight = 30;
        private const int TaskBarHeight = 20;
        private string[] themePaths;

        #endregion // Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectManagerForm"/> class.
        /// </summary>
        public ProjectManagerForm()
        {
            splashLoadedEvent = new ManualResetEvent(false);

            ThreadStart threadStart = new ThreadStart(this.ShowSplashScreen);
            Thread thread = new Thread(threadStart);
            thread.Name = "Splash Screen";
            thread.Start();
            splashLoadedEvent.WaitOne();

            // Minimize the initialization time by loading the style library before the InitializeComponent().
            // Otherwise, all the metrics are recalculated again after the theme changes
            this.themePaths = Utilities.GetStyleLibraryResourceNames();
            for (int i = 0; i < this.themePaths.Length; i++)
            {
                if (this.themePaths[i].Contains("02"))
                {
                    this.currentThemeIndex = i;
                    break;
                }
            }

            Infragistics.Win.AppStyling.StyleManager.Load(Utilities.GetEmbeddedResourceStream(this.themePaths[this.currentThemeIndex]));

            InitializeComponent();
        }
        #endregion // Constructor

        #region Base Class Overrides

        #region Dispose

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                // Unhook the StyleChanged event handler
                Infragistics.Win.AppStyling.StyleManager.StyleChanged -= new Infragistics.Win.AppStyling.StyleChangedEventHandler(Application_OnStyleChanged);

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion //Dispose

        #region OnLoad

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            this.ChangeIcon();

            this.OnInitializationStatusChanged(Properties.Resources.Loading);
            base.OnLoad(e);

            // Retrieves the sample data from the provided XML file
            this.OnInitializationStatusChanged(Properties.Resources.Retrieving);
            DataSet dataset = Utilities.GetData("ProjectManager.Data.ProjManager.XML");

            // Bind the ultraCalendarInfo to the provided data. 
            this.OnInitializationStatusChanged(Properties.Resources.Binding);
            this.BindProjectData(dataset);

            //Initialize the controls on the form
            this.OnInitializationStatusChanged(Properties.Resources.Initializing);
            this.ColorizeImages();
            this.InitializeUI();

            Infragistics.Win.AppStyling.StyleManager.StyleChanged += new Infragistics.Win.AppStyling.StyleChangedEventHandler(Application_OnStyleChanged);
        }

        #endregion //OnLoad

        #region OnShown

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Process other events before firing the event. 
            // Otherwise, the form will not be completely rendered before the splash screen is closed
            Application.DoEvents();
            
            // Fire the InitializationComplete event so the SplashScreen is closed.
            this.OnInitializationComplete();
        }

        #endregion //OnShown

        #endregion //Overrides

        #region Event Handlers

        #region Application_OnStyleChanged

        /// <summary>
        /// Handles the StyleChanged event of the Application Styling Manager
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.AppStyling.StyleChangedEventArgs"/> instance containing the event data.</param>
        private void Application_OnStyleChanged(object sender, Infragistics.Win.AppStyling.StyleChangedEventArgs e)
        {
            this.ultraGanttView1.PerformAutoSizeAllGridColumns();

            // Colorize the images to match the current theme.
            this.ColorizeImages();
            this.ChangeIcon();
        }

        #endregion //Application_OnStyleChanged

        #region ultraCalendarInfo1_CalendarInfoChanged

        /// <summary>
        /// Handles the CalendarInfoChanged event of the ultraCalendarInfo1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CalendarInfoChangedEventArgs"/> instance containing the event data.</param>
        private void ultraCalendarInfo1_CalendarInfoChanged(object sender, CalendarInfoChangedEventArgs e)
        {
            Task activeTask = this.ultraGanttView1.ActiveTask;
            if (activeTask == null)
                return;

            // Check to see if the level of the active task changed.
            // If so, make sure the state of the Tasks tools is verified.
            PropChangeInfo propInfo = e.PropChangeInfo.FindTrigger(activeTask);
            if (propInfo != null &&
                propInfo.PropId is TaskPropertyIds &&
                (TaskPropertyIds)propInfo.PropId == TaskPropertyIds.Level)
            {
                this.UpdateTasksToolsState(activeTask);
            }
        }

        #endregion //ultraCalendarInfo1_CalendarInfoChanged

        #region ultraGanttView1_ActiveTaskChanging

        /// <summary>
        /// Handles the ActiveTaskChanging event of the ultraGanttView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ActiveTaskChangingEventArgs"/> instance containing the event data.</param>
        private void ultraGanttView1_ActiveTaskChanging(object sender, ActiveTaskChangingEventArgs e)
        {
            Task newActiveTask = e.NewActiveTask;
            this.UpdateTasksToolsState(newActiveTask);
            this.UpdateToolsRequiringActiveTask(newActiveTask != null);
        }

        #endregion // ultraGanttView1_ActiveTaskChanging

        #region ultraGanttView1_CellActivating
        
        /// <summary>
        /// Handles the CellActivating event of the ultraGanttView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CellActivatingEventArgs"/> instance containing the event data.</param>
        private void ultraGanttView1_CellActivating(object sender, CellActivatingEventArgs e)
        {
            bool originalValue = this.cellActivationRecursionFlag;
            this.cellActivationRecursionFlag = true;
            try
            {
                Task activeTask = e.TaskFieldInfo.Task;

                if (activeTask != null)
                {
                    TaskField? activeField = e.TaskFieldInfo.TaskField;
                    if (activeField.HasValue)
                    {
                        Infragistics.Win.AppearanceBase appearance = activeTask.GridSettings.CellSettings[(TaskField)activeField].Appearance;
                        FontData fontData = appearance.FontData;

                        // Set state of Bold button depending upon the active cell
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["Font_Bold"]).Checked = (fontData.Bold == DefaultableBoolean.True);

                        // Set state of Italics button depending upon the active cell
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["Font_Italic"]).Checked = (fontData.Italic == DefaultableBoolean.True);

                        // Set state of Underline button depending upon the active cell
                        ((StateButtonTool)this.ultraToolbarsManager1.Tools["Font_Underline"]).Checked = (fontData.Underline == DefaultableBoolean.True);

                        // Update the name of the font in the FontListTool
                        string fontName = fontData.Name;
                        if (fontName != null)
                        {
                            ((FontListTool)this.ultraToolbarsManager1.Tools["FontList"]).Text = fontName;
                        }
                        else
                        {
                            ((FontListTool)this.ultraToolbarsManager1.Tools["FontList"]).SelectedIndex = 0;
                        }

                        // Update font size in the ComboBoxTool that shows the size of the font
                        float fontSize = fontData.SizeInPoints;
                        if (fontSize != 0)
                        {
                            ((ComboBoxTool)(this.ultraToolbarsManager1.Tools["FontSize"])).Value = fontSize;
                        }
                        else
                        {
                            ((ComboBoxTool)(this.ultraToolbarsManager1.Tools["FontSize"])).SelectedIndex = 0;
                        }
                    }
                }

                this.UpdateFontToolsState(e.TaskFieldInfo.TaskField.HasValue);
            }
            finally
            {
                this.cellActivationRecursionFlag = originalValue;
            }
        }
        #endregion // ultraGanttView1_CellActivating

        #region ultraGanttView1_CellDeactivating

        /// <summary>
        /// Handles the CellDeactivating event of the ultraGanttView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGanttView.CellDeactivatingEventArgs"/> instance containing the event data.</param>
        private void ultraGanttView1_CellDeactivating(object sender, Infragistics.Win.UltraWinGanttView.CellDeactivatingEventArgs e)
        {
            this.UpdateFontToolsState(false);
            this.UpdateTasksToolsState(null);
        }

        #endregion // ultraGanttView1_CellDeactivating

        #region ultraGanttView1_TaskAdded

        /// <summary>
        /// Handles the TaskAdded event of the ultraGanttView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TaskAddedEventArgs"/> instance containing the event data.</param>
        private void ultraGanttView1_TaskAdded(object sender, TaskAddedEventArgs e)
        {
            this.UpdateToolsRequiringActiveTask(this.ultraGanttView1.ActiveTask != null);
        }

        #endregion // ultraGanttView1_TaskAdded

        #region ultraGanttView1_TaskDeleted

        /// <summary>
        /// Handles the TaskDeleted event of the ultraGanttView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TaskDeletedEventArgs"/> instance containing the event data.</param>
        private void ultraGanttView1_TaskDeleted(object sender, TaskDeletedEventArgs e)
        {
            this.UpdateToolsRequiringActiveTask(this.ultraGanttView1.ActiveTask != null);
        }

        #endregion // ultraGanttView1_TaskDeleted

        #region ultraGanttView1_TaskDialogDisplaying

        /// <summary>
        /// Handles the displaying task dialog.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TaskDialogDisplayingEventArgs"/> instance containing the event data.</param>
        private void ultraGanttView1_TaskDialogDisplaying(object sender, TaskDialogDisplayingEventArgs e)
        {
            // Show Notes page when task dialog gets launched
            e.Dialog.SelectPage(TaskDialogPage.Notes);
        }
        #endregion // ultraGanttView1_TaskDialogDisplaying

        #region ultraToolbarsManager1_PropertyChanged

        /// <summary>
        /// Handles the PropertyChanged event of the ultraToolbarsManager1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ultraToolbarsManager1_PropertyChanged(object sender, Infragistics.Win.PropertyChangedEventArgs e)
        {
            PropChangeInfo trigger = e.ChangeInfo.FindTrigger(null);
            if (trigger != null &&
                trigger.Source is SharedProps &&
                trigger.PropId is Infragistics.Win.UltraWinToolbars.PropertyIds)
            {
                switch ((Infragistics.Win.UltraWinToolbars.PropertyIds)trigger.PropId)
                {
                    case Infragistics.Win.UltraWinToolbars.PropertyIds.Enabled:

                        SharedProps sharedProps = (SharedProps)trigger.Source;

                        ToolBase tool = (sharedProps.ToolInstances.Count > 0) ? sharedProps.ToolInstances[0] : sharedProps.RootTool;
                        string imageKey = string.Format("{0}_{1}", tool.Key, tool.EnabledResolved ? "Normal" : "Disabled");
                        if (this.ilColorizedImagesLarge.Images.ContainsKey(imageKey))
                            sharedProps.AppearancesLarge.Appearance.Image = imageKey;
                        if (this.ilColorizedImagesSmall.Images.ContainsKey(imageKey))
                            sharedProps.AppearancesSmall.Appearance.Image = imageKey;
                        
                        break;
                }
            }
        }

        #endregion //ultraToolbarsManager1_PropertyChanged

        #region ultraToolbarsManager1_ToolClick

        /// <summary>
        /// Handles the ToolClick event of the ultraToolbarsManager1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinToolbars.ToolClickEventArgs"/> instance containing the event data.</param>
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Font_Bold":
                    if (cellActivationRecursionFlag == false)
                        this.UpdateFontProperty(FontProperties.Bold);
                    break;

                case "Font_Italic":
                    if (cellActivationRecursionFlag == false)
                        this.UpdateFontProperty(FontProperties.Italics);
                    break;

                case "Font_Underline":
                    if (cellActivationRecursionFlag == false)
                        this.UpdateFontProperty(FontProperties.Underline);
                    break;

                case "Font_BackColor":
                    this.SetTextBackColor();
                    break;

                case "Font_ForeColor":
                    this.SetTextForeColor();
                    break;

                case "FontList":
                    this.UpdateFontName();
                    break;

                case "FontSize":
                    this.UpdateFontSize();
                    break;

                case "Insert_Task_Task":
                    this.AddNewTask(false);
                    break;

                case "Insert_Task_TaskAtSelectedRow":
                    this.AddNewTask(true);
                    break;

                case "Tasks_PercentComplete_0":
                    this.SetTaskPercentage(0);
                    break;

                case "Tasks_PercentComplete_25":
                    this.SetTaskPercentage(25);
                    break;

                case "Tasks_PercentComplete_50":
                    this.SetTaskPercentage(50);
                    break;

                case "Tasks_PercentComplete_75":
                    this.SetTaskPercentage(75);
                    break;

                case "Tasks_PercentComplete_100":
                    this.SetTaskPercentage(100);
                    break;

                case "Tasks_MoveLeft":
                    this.PerformIndentOrOutdent(GanttViewAction.OutdentTask);
                    break;

                case "Tasks_MoveRight":
                    this.PerformIndentOrOutdent(GanttViewAction.IndentTask);
                    break;

                case "Tasks_Delete":
                    this.DeleteTask();
                    break;

                case "Schedule_MoveTask_1Day":
                    this.MoveTask(GanttViewAction.MoveTaskDateForward, TimeSpanForMoving.OneDay);
                    break;
                
                case "Schedule_MoveTask_1Week":
                    this.MoveTask(GanttViewAction.MoveTaskDateForward, TimeSpanForMoving.OneWeek);
                    break;
                
                case "Schedule_MoveTask_4Weeks":
                    this.MoveTask(GanttViewAction.MoveTaskDateForward, TimeSpanForMoving.FourWeeks);
                    break;
                
                case "Schedule_MoveTask_MoveTaskBackwards1Day":
                    this.MoveTask(GanttViewAction.MoveTaskDateBackward, TimeSpanForMoving.OneDay);
                    break;
                
                case "Schedule_MoveTask_MoveTaskBackwards1Week":
                    this.MoveTask(GanttViewAction.MoveTaskDateBackward, TimeSpanForMoving.OneWeek);
                    break;
                
                case "Schedule_MoveTask_MoveTaskBackwards4Weeks":
                    this.MoveTask(GanttViewAction.MoveTaskDateBackward, TimeSpanForMoving.FourWeeks);
                    break;

                case "Properties_TaskInformation":
                    this.ultraGanttView1.DisplayTaskDialog(this.ultraGanttView1.ActiveTask);
                    break;

                case "Properties_Notes":
                    this.ultraGanttView1.TaskDialogDisplaying += new TaskDialogDisplayingHandler(ultraGanttView1_TaskDialogDisplaying);
                    this.ultraGanttView1.DisplayTaskDialog(this.ultraGanttView1.ActiveTask);
                    this.ultraGanttView1.TaskDialogDisplaying -= new TaskDialogDisplayingHandler(ultraGanttView1_TaskDialogDisplaying);
                    break;

                case "Insert_Milestone":
                    this.ultraGanttView1.ActiveTask.Milestone = !this.ultraGanttView1.ActiveTask.Milestone;
                    break;

                case "TouchMode":
                    ListTool touchModeListTool = e.Tool as ListTool;
                    if (touchModeListTool.SelectedItem == null)
                        touchModeListTool.SelectedItemIndex = e.ListToolItem.Index;
                    this.ultraTouchProvider1.Enabled = (e.ListToolItem.Key == "Touch");
                    break;

                case "ThemeList":                    
                    ListTool themeListTool = e.Tool as ListTool;
                    if (themeListTool.SelectedItem == null)
                        themeListTool.SelectedItemIndex = e.ListToolItem.Index;

                    string key = e.ListToolItem.Key;
                    if (this.themePaths[this.currentThemeIndex] != key)
                    {
                        this.currentThemeIndex = e.ListToolItem.Index;
                        Infragistics.Win.AppStyling.StyleManager.Load(Utilities.GetEmbeddedResourceStream(key));
                    }
                    break;

                case "Print":
                    UltraPrintPreviewDialog printPreview = new UltraPrintPreviewDialog();
                    printPreview.Document = this.ultraGanttViewPrintDocument1;
                    printPreview.ShowDialog(this);
                    break;

                case "Exit":
                case "Close":
                    Application.Exit();
                    break;
            }
        }
        #endregion // ultraToolbarsManager1_ToolClick

        #region ultraToolbarsManager1_ToolValueChanged

        /// <summary>
        /// Handles the ToolValueChanged event of the ultraToolbarsManager1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ToolEventArgs"/> instance containing the event data.</param>
        private void ultraToolbarsManager1_ToolValueChanged(object sender, ToolEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Font_BackColor":
                    this.SetTextBackColor();
                    break;
                case "Font_ForeColor":
                    this.SetTextForeColor();
                    break;
                case "FontList":
                    this.UpdateFontName();
                    break;
                case "FontSize":
                    this.UpdateFontSize();
                    break;
            }
        }
        #endregion // ultraToolbarsManager1_ToolValueChanged

        #region ultraTouchProvider1_PropertyChanged

        /// <summary>
        /// Handles the PropertyChanged event of the ultraTouchProvider1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ultraTouchProvider1_PropertyChanged(object sender, Infragistics.Win.PropertyChangedEventArgs e)
        {
            PropChangeInfo propChanged = e.ChangeInfo;
            if (propChanged.PropId is Infragistics.Win.Touch.TouchProviderPropertyIds &&
                ((Infragistics.Win.Touch.TouchProviderPropertyIds)propChanged.PropId) == Infragistics.Win.Touch.TouchProviderPropertyIds.Enabled)
            {
                this.ultraGanttView1.PerformAutoSizeAllGridColumns();
            }
        }

        #endregion //ultraTouchProvider1_PropertyChanged

        #endregion // Event Handlers

        #region Properties

        #region SplashLoadedEvent
        internal static ManualResetEvent SplashLoadedEvent
        {
            get
            {
                return splashLoadedEvent;
            }
        }
        #endregion //SplashLoadedEvent

        #endregion //Properties

        #region Methods

        #region AddNewTask

        /// <summary>
        /// Adds a new task to the GanttView
        /// </summary>
        /// <param name="addAtSelectedRow">Insert a task at the selected row or at the bottom of 
        /// the ganttView</param>
        private void AddNewTask(bool addAtSelectedRow)
        {
            TasksCollection parentCollection = null;
            UltraCalendarInfo calendarInfo = this.ultraGanttView1.CalendarInfo;
            Task activeTask = this.ultraGanttView1.ActiveTask;
            Project project = calendarInfo.Projects[1];
            int insertionIndex;
            DateTime start;
            bool addToRootcollection = true;
            if (addAtSelectedRow == true)
            {
                if (activeTask != null)
                {
                    Task parentTask = activeTask.Parent;
                    parentCollection = parentTask != null ? parentTask.Tasks : calendarInfo.Tasks;
                    insertionIndex = parentCollection.IndexOf(activeTask);
                    start = parentTask != null ? parentTask.StartDateTime : project.StartDate;
                    addToRootcollection = false;
                }
                else
                {
                    insertionIndex = calendarInfo.Tasks.Count;
                    start = project.StartDate;
                }
            }
            else
            {
                parentCollection = calendarInfo.Tasks;
                insertionIndex = calendarInfo.Tasks.Count;
                start = project.StartDate;
            }

            if (parentCollection != null)
            {
                //  Insert the task
                string taskName = rm.GetString("NewTaskName");
                Task newTask;
                if (addToRootcollection == false &&
                    activeTask != null &&
                    activeTask.Parent != null)
                {
                  newTask = activeTask.Parent.Tasks.Insert(insertionIndex, start, TimeSpan.FromDays(1), taskName);// newTask);
                }
                else
                {
                   newTask = calendarInfo.Tasks.Insert(insertionIndex, start, TimeSpan.FromDays(1), taskName);
                }
                newTask.Project = project;
                newTask.RowHeight = TaskRowHeight;
            }
        }
        #endregion // AddNewTask

        #region BindProjectData

        /// <summary>
        /// Binds the project data to the UltraCalendarInfo
        /// </summary>
        /// <param name="data">The data.</param>
        private void BindProjectData(DataSet data)
        {

            //  Set the BindingContextControl property to reference this form
            #region BindingContext
            this.ultraCalendarInfo1.DataBindingsForTasks.BindingContextControl = this;
            this.ultraCalendarInfo1.DataBindingsForProjects.BindingContextControl = this;
            this.ultraCalendarInfo1.DataBindingsForOwners.BindingContextControl = this;
            #endregion //BindingContext

            //  Set the DataBinding members for Projects 
            #region Projects
            this.ultraCalendarInfo1.DataBindingsForProjects.SetDataBinding(data, "Projects");
            this.ultraCalendarInfo1.DataBindingsForProjects.IdMember = "ProjectID";
            this.ultraCalendarInfo1.DataBindingsForProjects.KeyMember = "ProjectKey";
            this.ultraCalendarInfo1.DataBindingsForProjects.NameMember = "ProjectName";
            this.ultraCalendarInfo1.DataBindingsForProjects.StartDateMember = "ProjectStartTime";
            #endregion //Projects

            //  Set the DataBinding members for Tasks
            #region Tasks
            this.ultraCalendarInfo1.DataBindingsForTasks.SetDataBinding(data, "Tasks");

            // Basic Task properties 
            this.ultraCalendarInfo1.DataBindingsForTasks.NameMember = "TaskName";
            this.ultraCalendarInfo1.DataBindingsForTasks.DurationMember = "TaskDuration";
            this.ultraCalendarInfo1.DataBindingsForTasks.StartDateTimeMember = "TaskStartTime";
            this.ultraCalendarInfo1.DataBindingsForTasks.IdMember = "TaskID";
            this.ultraCalendarInfo1.DataBindingsForTasks.ProjectKeyMember = "ProjectKey";
            this.ultraCalendarInfo1.DataBindingsForTasks.ParentTaskIdMember = "ParentTaskID";

            this.ultraCalendarInfo1.DataBindingsForTasks.ConstraintMember = "Constraint";
            this.ultraCalendarInfo1.DataBindingsForTasks.PercentCompleteMember = "TaskPercentComplete";

            // All other properties
            this.ultraCalendarInfo1.DataBindingsForTasks.AllPropertiesMember = "AllProperties";
            #endregion //Tasks

            // Set the DataBinding members for Owners
            #region Owners
            this.ultraCalendarInfo1.DataBindingsForOwners.SetDataBinding(data, "Owners");
            this.ultraCalendarInfo1.DataBindingsForOwners.BindingContextControl = this;
            this.ultraCalendarInfo1.DataBindingsForOwners.KeyMember = "Key";
            this.ultraCalendarInfo1.DataBindingsForOwners.NameMember = "Name";
            this.ultraCalendarInfo1.DataBindingsForOwners.EmailAddressMember = "EmailAddress";
            this.ultraCalendarInfo1.DataBindingsForOwners.VisibleMember = "Visible";
            this.ultraCalendarInfo1.DataBindingsForOwners.AllPropertiesMember = "AllProperties";
            #endregion //Owners

            // Assign sample project to the GanttView control.
            this.ultraGanttView1.Project = this.ultraGanttView1.CalendarInfo.Projects[1];

        }

        #endregion //BindProjectData

        #region ChangeIcon

        private void ChangeIcon()
        {
            string iconPath = this.themePaths[this.currentThemeIndex].Replace("StyleLibraries.", "Images.AppIcon - ").Replace(".isl", ".ico");

            System.IO.Stream stream = Utilities.GetEmbeddedResourceStream(iconPath);
            if (stream != null)
                this.Icon = new Icon(stream);
        }

        #endregion //ChangeIcon

        #region ColorizeImages

        /// <summary>
        /// Colorizes the images in the large and small imagelists containing the default images, and place the new images in the colorized imagelists.
        /// </summary>
        private void ColorizeImages()
        {
            // Suspend painting to the UltraToolbarsManager
            bool shouldSuspendPainting = !this.ultraToolbarsManager1.IsUpdating;
            if (shouldSuspendPainting)
                this.ultraToolbarsManager1.BeginUpdate();

            ImageList largeImageList = this.ultraToolbarsManager1.ImageListLarge;
            ImageList smallImageList = this.ultraToolbarsManager1.ImageListSmall;

            try
            {
                this.ultraToolbarsManager1.ImageListLarge = null;
                this.ultraToolbarsManager1.ImageListSmall = null;

                ToolBase resolveTool = null;

                if (this.ultraToolbarsManager1.Tools.Exists("Insert_Task"))
                {
                    resolveTool = this.ultraToolbarsManager1.Tools["Insert_Task"];

                    // loop through all instances looking for the tool in the RibbonGroup.
                    foreach (ToolBase instanceTool in resolveTool.SharedProps.ToolInstances)
                    {
                        if (instanceTool.OwnerIsRibbonGroup)
                        {
                            resolveTool = instanceTool;
                            break;
                        }
                    }
                }

                if (resolveTool == null)
                    return;

                // Get the resolved colors
                Dictionary<string, Color> colors = new Dictionary<string, Color>();
                AppearanceData appData = new AppearanceData();
                AppearancePropFlags requestedProps = AppearancePropFlags.ForeColor; 
                resolveTool.ResolveAppearance(ref appData, ref requestedProps);
                colors["Normal"] = appData.ForeColor;

                appData = new AppearanceData();
                requestedProps = AppearancePropFlags.ForeColor | AppearancePropFlags.BackColor;
                resolveTool.ResolveAppearance(ref appData, ref requestedProps, true, false);
                colors["Active"] = appData.ForeColor;

                if (appData.BackColor.IsEmpty || appData.BackColor.Equals(Color.Transparent))
                {
                    appData = new AppearanceData();
                    requestedProps = AppearancePropFlags.BackColor;
                    this.ultraToolbarsManager1.Ribbon.Tabs[0].ResolveTabItemAppearance(ref appData, ref requestedProps);
                    colors["Disabled"] = appData.BackColor;
                }
                else
                    colors["Disabled"] = appData.BackColor;

                Color replacementColor = Color.Magenta;

                Utilities.ColorizeImages(replacementColor, colors, ref this.ilDefaultImagesLarge, ref this.ilColorizedImagesLarge);
                Utilities.ColorizeImages(replacementColor, colors, ref this.ilDefaultImagesSmall, ref this.ilColorizedImagesSmall);

                // Make sure the UltraToolbarsManager is using the new colorized images
                largeImageList = this.ilColorizedImagesLarge;
                smallImageList = this.ilColorizedImagesSmall;
            }
            catch
            {
                // Make sure the UltraToolbarsManager is using the new colorized images
                largeImageList = this.ilDefaultImagesLarge;
                smallImageList = this.ilDefaultImagesSmall;
            }
            finally
            {
                this.ultraToolbarsManager1.ImageListLarge = largeImageList;
                this.ultraToolbarsManager1.ImageListSmall = smallImageList;

                // Resume painting on the UltraToolbarsManager
                if (shouldSuspendPainting)
                    this.ultraToolbarsManager1.EndUpdate();
            }
        }

        #endregion // ColorizeImages

        #region DeleteTask
        
        /// <summary>
        /// Deletes the active task
        /// </summary>
        private void DeleteTask()
        {
            Task activeTask = this.ultraGanttView1.ActiveTask;
            try
            {
                if (activeTask != null)
                {
                    Task parent = activeTask.Parent;

                    if (parent == null)
                        this.ultraCalendarInfo1.Tasks.Remove(activeTask);
                    else
                        parent.Tasks.Remove(activeTask);
                }

                Task newActiveTask = this.ultraGanttView1.ActiveTask;
                this.UpdateTasksToolsState(newActiveTask);
                this.UpdateToolsRequiringActiveTask(newActiveTask != null);
            }
            catch (TaskException ex)
            {
                UltraMessageBoxManager.Show(ex.Message, rm.GetString("MessageBox_Error"));
            }
        }
        #endregion // DeleteTask

        #region InitializeUI

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        private void InitializeUI()
        {
            // Populate the themes list
            int selectedIndex = 0;
            ListTool themeTool = (ListTool)this.ultraToolbarsManager1.Tools["ThemeList"];
            foreach (string resourceName in this.themePaths)
            {
                ListToolItem item = new ListToolItem(resourceName);
                string libraryName = resourceName.Replace(".isl", string.Empty);
                item.Text = libraryName.Remove(0, libraryName.LastIndexOf('.') + 1);
                themeTool.ListToolItems.Add(item);

                if (item.Text.Contains("02"))
                    selectedIndex = item.Index;
            }
            themeTool.SelectedItemIndex = selectedIndex;

            // Select the proper touch mode list item.
            ((ListTool)this.ultraToolbarsManager1.Tools["TouchMode"]).SelectedItemIndex = 0;

            // Creates a valueList with various font sizes
            this.PopulateFontSizeValueList();
            ((ComboBoxTool)(this.ultraToolbarsManager1.Tools["FontSize"])).SelectedIndex = 0;
            ((FontListTool)this.ultraToolbarsManager1.Tools["FontList"]).SelectedIndex = 0;
            this.UpdateFontToolsState(false);

            Control control = new AboutControl();
            control.Visible = false;
            control.Parent = this;
            ((PopupControlContainerTool)this.ultraToolbarsManager1.Tools["About"]).Control = control;

            // Autosize the columns so all the data is visible.
            this.ultraGanttView1.PerformAutoSizeAllGridColumns();

            // Colorize the images to match the current theme.
            this.ColorizeImages();

            this.ultraToolbarsManager1.Ribbon.FileMenuButtonCaption = Properties.Resources.ribbonFileTabCaption;
        }

        #endregion //InitializeUI

        #region MoveTask
        
        /// <summary>
        /// Moves start and end dates of the task backward or foward by a specific timespan
        /// </summary>
        /// <param name="action">Enumeration of ganttView actions supported</param>
        /// <param name="moveTimeSpan">TimeSpan for moving the start and end dates of the task</param>
        private void MoveTask(GanttViewAction action, TimeSpanForMoving moveTimeSpan)
        {
            Task activeTask = this.ultraGanttView1.ActiveTask;

            if (activeTask != null && activeTask.IsSummary == false)
            {
                switch (action)
                {
                    case GanttViewAction.MoveTaskDateForward:
                        {
                            switch (moveTimeSpan)
                            {
                                case TimeSpanForMoving.OneDay:
                                    activeTask.StartDateTime = activeTask.StartDateTime.AddDays(1);
                                    break;
                                case TimeSpanForMoving.OneWeek:
                                    activeTask.StartDateTime = activeTask.StartDateTime.AddDays(7);
                                    break;
                                case TimeSpanForMoving.FourWeeks:
                                    activeTask.StartDateTime = activeTask.StartDateTime.AddDays(28);
                                    break;
                            }
                        }
                        break;

                    case GanttViewAction.MoveTaskDateBackward:
                        {
                            switch (moveTimeSpan)
                            {
                                case TimeSpanForMoving.OneDay:
                                    activeTask.StartDateTime = activeTask.StartDateTime.Subtract(TimeSpan.FromDays(1));
                                    break;
                                case TimeSpanForMoving.OneWeek:
                                    activeTask.StartDateTime = activeTask.StartDateTime.Subtract(TimeSpan.FromDays(7));
                                    break;
                                case TimeSpanForMoving.FourWeeks:
                                    activeTask.StartDateTime = activeTask.StartDateTime.Subtract(TimeSpan.FromDays(28));
                                    break;
                            }
                        }
                        break;
                }
            }
        }
        #endregion  // MoveTask

        #region PerformIndentOrOutdent
        
        /// <summary>
        /// Perfoms indent or outdent on active task
        /// </summary>
        /// <param name="action">Action to be performed(indent or outdent)</param>
        private void PerformIndentOrOutdent(GanttViewAction action)
        {
            Task activeTask = this.ultraGanttView1.ActiveTask;

            try
            {
                if (activeTask != null)
                {
                    switch (action)
                    {
                        case GanttViewAction.IndentTask:
                            if (activeTask.CanIndent())
                                activeTask.Indent();
                            break;
                        case GanttViewAction.OutdentTask:
                            if (activeTask.CanOutdent())
                                activeTask.Outdent();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                UltraMessageBoxManager.Show(ex.Message, rm.GetString("MessageBox_Error"));
            }
        }
        #endregion // PerformIndentOrOutdent

        #region PopulateFontSizeValueList
        
        /// <summary>
        /// Populates the list of font sizes
        /// </summary>
        private void PopulateFontSizeValueList()
        {
            List<float> fontSizeList = new List<float> ( new float [] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72}); 
            foreach(float i in fontSizeList)
            {
               ((ComboBoxTool)(this.ultraToolbarsManager1.Tools["FontSize"])).ValueList.ValueListItems.Add(i);
            }
        }
        #endregion // PopulateFontSizeValueList

        #region SetTaskPercentage
        
        /// <summary>
        /// Assigns a percentage complete value to the active task
        /// </summary>
        /// <param name="percent">Percentage to be assigned to the active task</param>
        private void SetTaskPercentage(float percent)
        {
            Task activeTask = this.ultraGanttView1.ActiveTask;
            try
            {
                if (activeTask != null)
                {
                    activeTask.PercentComplete = percent;
                }
            }
            catch (TaskException ex)
            {
                UltraMessageBoxManager.Show(ex.Message, rm.GetString("MessageBox_Error"));
            }
        }
        #endregion // SetTaskPercentage

        #region SetTextBackColor
        
        /// <summary>
        /// Updates size of the background color of the active cell depending upon the color
        /// selected from the PopupColorPickerTool.
        /// </summary>
        private void SetTextBackColor()
        {
            Color fontBGColor = ((PopupColorPickerTool)this.ultraToolbarsManager1.Tools["Font_BackColor"]).SelectedColor;
            Task activeTask = this.ultraGanttView1.ActiveTask;
            if (activeTask != null)
            {
                TaskField? activeField = this.ultraGanttView1.ActiveField;
                if (activeField.HasValue)
                {
                    activeTask.GridSettings.CellSettings[(TaskField)activeField].Appearance.BackColor = fontBGColor;
                }
            }
        }
        #endregion //SetTextBackColor

        #region SetTextForeColor
        
        /// <summary>
        /// Updates fore color of the text in the active cell depending upon the color
        /// selected from the PopupColorPickerTool.
        /// </summary>
        private void SetTextForeColor()
        {
            Color fontColor = ((PopupColorPickerTool)this.ultraToolbarsManager1.Tools["Font_ForeColor"]).SelectedColor;
            Task activeTask = this.ultraGanttView1.ActiveTask;
            if (activeTask != null)
            {
                TaskField? activeField = this.ultraGanttView1.ActiveField;
                if (activeField.HasValue)
                {
                    activeTask.GridSettings.CellSettings[(TaskField)activeField].Appearance.ForeColor = fontColor;
                }
            }
        }
        #endregion // SetTextForeColor

        #region ShowSplashScreen

        /// <summary>
        /// Shows the splash screen.
        /// </summary>
        private void ShowSplashScreen()
        {
            SplashScreen splashScreen = new SplashScreen();
            Application.Run(splashScreen);
            Application.ExitThread();
        }
        #endregion //ShowSplashScreen

        #region UpdateFontToolsState

        /// <summary>
        /// Updates the Enabled property for tools in the Font RibbonGroup 
        /// </summary>
        private void UpdateFontToolsState(bool enabled)
        {
            Utilities.SetRibbonGroupToolsEnabledState(this.ultraToolbarsManager1.Ribbon.Tabs[0].Groups["RibbonGrp_Font"], enabled);
        }

        #endregion // UpdateFontToolsState

        #region UpdateFontName
        
        /// <summary>
        /// Updates the font depending upon the value selected from the FontListTool.
        /// </summary>
        private void UpdateFontName()
        {
            string fontName = ((FontListTool)this.ultraToolbarsManager1.Tools["FontList"]).Text;
            Task activeTask = this.ultraGanttView1.ActiveTask;
            if (activeTask != null)
            {
                TaskField? activeField = this.ultraGanttView1.ActiveField;
                if (activeField.HasValue)
                {
                    activeTask.GridSettings.CellSettings[(TaskField)activeField].Appearance.FontData.Name = fontName;
                }
            }
        }
        #endregion // UpdateFontName

        #region UpdateFontSize
        
        /// <summary>
        /// Updates size of the font depending upon the value selected from the ComboBoxTool.
        /// </summary>
        private void UpdateFontSize()
        {
            ValueListItem item = (ValueListItem)((ComboBoxTool)(this.ultraToolbarsManager1.Tools["FontSize"])).SelectedItem;
            if (item != null)
            {
                float fontSize = (float)item.DataValue;
                Task activeTask = this.ultraGanttView1.ActiveTask;
                if (activeTask != null)
                {
                    TaskField? activeField = this.ultraGanttView1.ActiveField;
                    if (activeField.HasValue)
                    {
                        activeTask.GridSettings.CellSettings[(TaskField)activeField].Appearance.FontData.SizeInPoints = fontSize;
                    }
                }
            }
        }
        #endregion // UpdateFontSize

        #region UpdateFontProperty
        
        /// <summary>
        /// Method to update various font properties.
        /// </summary>
        /// <param name="propertyToUpdate">Enumeration of font related properties</param>
        private void UpdateFontProperty(FontProperties propertyToUpdate)
        {
            Task activeTask = this.ultraGanttView1.ActiveTask;
            if (activeTask != null)
            {
                TaskField? activeField = this.ultraGanttView1.ActiveField;
                if (activeField.HasValue)
                {
                    FontData activeTaskActiveCellFontData = activeTask.GridSettings.CellSettings[(TaskField)activeField].Appearance.FontData;
                    switch (propertyToUpdate)
                    {
                        case FontProperties.Bold:
                            activeTaskActiveCellFontData.Bold = Utilities.ToggleDefaultableBoolean(activeTaskActiveCellFontData.Bold);
                            break;
                        case FontProperties.Italics:
                            activeTaskActiveCellFontData.Italic = Utilities.ToggleDefaultableBoolean(activeTaskActiveCellFontData.Italic);
                            break;
                        case FontProperties.Underline:
                            activeTaskActiveCellFontData.Underline = Utilities.ToggleDefaultableBoolean(activeTaskActiveCellFontData.Underline);
                            break;
                    }
                }
            }
            this.cellActivationRecursionFlag = false;
        }
        #endregion // UpdateFontProperty

        #region UpdateTasksToolsState

        /// <summary>
        /// Verifies the state of the tools in the Tasks RibbonGroup.
        /// </summary>
        /// <param name="activeTask">The active task.</param>
        private void UpdateTasksToolsState(Task activeTask)
        {
            RibbonGroup group = this.ultraToolbarsManager1.Ribbon.Tabs["Ribbon_Task"].Groups["RibbonGrp_Tasks"];

            if (activeTask != null)
            {
                // For summary tasks, the completion percentage is based on it's child tasks
                Utilities.SetRibbonGroupToolsEnabledState(group, !activeTask.IsSummary);

                group.Tools["Tasks_MoveLeft"].SharedProps.Enabled = activeTask.CanOutdent();
                group.Tools["Tasks_MoveRight"].SharedProps.Enabled = activeTask.CanIndent();
                group.Tools["Tasks_Delete"].SharedProps.Enabled = true;
            }
            else
            {
                Utilities.SetRibbonGroupToolsEnabledState(group, false);
            }
        }

        #endregion // UpdateTasksToolsState

        #region UpdateToolsRequiringActiveTask

        /// <summary>
        ///  Verifies the state of all tools requiring an active task.
        /// </summary>
        private void UpdateToolsRequiringActiveTask(bool enabled)
        {
            this.ultraToolbarsManager1.Tools["Tasks_Delete"].SharedProps.Enabled = enabled;
            this.ultraToolbarsManager1.Tools["Insert_Milestone"].SharedProps.Enabled = enabled;
            this.ultraToolbarsManager1.Tools["Properties_TaskInformation"].SharedProps.Enabled = enabled;
            this.ultraToolbarsManager1.Tools["Properties_Notes"].SharedProps.Enabled = enabled;
            this.ultraToolbarsManager1.Tools["Insert_Task_TaskAtSelectedRow"].SharedProps.Enabled = enabled;
        }

        #endregion // UpdateToolsRequiringActiveTask

        #endregion // Methods

        #region SplashScreen Events

        #region Events

        /// <summary>
        /// Fired when the staus of the form initialization has changed. 
        /// </summary>
        internal static event ProjectManager.SplashScreen.InitializationStatusChangedEventHandler InitializationStatusChanged;

        /// <summary>
        /// Fired when the staus of the form initialization has completed. 
        /// </summary>
        internal static event EventHandler InitializationComplete;

        bool initializationCompleted = false;

        #region OnInitializationComplete
        protected virtual void OnInitializationComplete()
        {
            if (this.initializationCompleted == false)
            {
                this.initializationCompleted = true;

                if (ProjectManagerForm.InitializationComplete != null)
                    ProjectManagerForm.InitializationComplete(this, EventArgs.Empty);
            }
        }
        #endregion OnInitializationStatusChanged

        #region OnInitializationStatusChanged
        protected virtual void OnInitializationStatusChanged(string status)
        {
            if (ProjectManagerForm.InitializationStatusChanged != null)
                ProjectManagerForm.InitializationStatusChanged(this, new ProjectManager.SplashScreen.InitializationStatusChangedEventArgs(status));
        }

        protected virtual void OnInitializationStatusChanged(string status, bool showProgressBar, int percentComplete)
        {
            if (ProjectManagerForm.InitializationStatusChanged != null)
                ProjectManagerForm.InitializationStatusChanged(this, new ProjectManager.SplashScreen.InitializationStatusChangedEventArgs(status, showProgressBar, percentComplete));
        }
        #endregion OnInitializationStatusChanged

        #endregion Events		

        #endregion //SplashScreen Events
    }
}
