using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

namespace ProjectManager
{
    public partial class SplashScreen : Form
    {

        #region Private Members

        private ResourceManager rm = Properties.Resources.ResourceManager;

        #endregion Private Members

        #region Constructor

        public SplashScreen()
        {
            this.DoubleBuffered = true;

            // The splash screen should not take focus.
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.StandardClick, false);

            // Required for Windows Form Designer support
            InitializeComponent();

            this.InitializeUI();
        }

        #endregion //Constructor

        #region Private Methods

        #region CloseMe
        private void CloseMe()
        {           
            this.Close();
            this.Dispose();
        }
        #endregion CloseMe

        #region InitializeUI
        private void InitializeUI()
        {
            this.LocalizeStrings();

            this.HookEvents();
        }
        #endregion InitializeUI

        #region HookEvents
        private void HookEvents()
        {
            ProjectManagerForm.InitializationStatusChanged += new InitializationStatusChangedEventHandler(this.Application_InitializationStatusChanged);
            ProjectManagerForm.InitializationComplete += new EventHandler(this.Application_InitializationComplete);
        }
        #endregion HookEvents

        #region UnHookEvents
        private void UnHookEvents()
        {
            ProjectManagerForm.InitializationStatusChanged -= new InitializationStatusChangedEventHandler(this.Application_InitializationStatusChanged);
            ProjectManagerForm.InitializationComplete -= new EventHandler(this.Application_InitializationComplete);
        }
        #endregion UnHookEvents

        #region UpdateStatusLabel
        private void UpdateStatusLabel(string status)
        {
            this.lblStatus.Text = status;
        }
        #endregion UpdateStatusLabel

        #region LocalizeStrings
        private void LocalizeStrings()
        {
            this.lblAppName.Text = AboutControl.ApplicationName;
            this.lblVersion.Text = string.Format(" v {0}", Infragistics.Shared.AssemblyVersion.MajorMinor);
            this.lblStatus.Text = string.Format(rm.GetString("Application_Starting"), Properties.Resources.Title.ToUpper());
        }
        #endregion //LocalizeStrings

        #endregion Private Methods

        #region Delegates
        private delegate void UpdateStringDelegate(string text);
        #endregion Delegates

        #region Event Handlers

        #region Application_InitializationStatusChanged

        private void Application_InitializationStatusChanged(object sender, InitializationStatusChangedEventArgs e)
        {
            if (this.lblStatus.InvokeRequired)
            {
                this.lblStatus.Invoke(new UpdateStringDelegate(this.UpdateStatusLabel), new object[] { e.Status });
            }
            else
                this.UpdateStatusLabel(e.Status);
        }

        #endregion // Application_InitializationStatusChanged

        #region Application_InitializationComplete
        private void Application_InitializationComplete(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                try
                {
                    this.Invoke(new MethodInvoker(this.CloseMe));
                }
                catch { }
            else
                this.Close();
        }
        #endregion //Application_InitializationComplete

        #endregion Event Handlers

        #region Base class overrides

        #region SetVisibleCore
        protected override void SetVisibleCore(bool visible)
        {
            if (visible)
            {
                // The Application.Run call will force the visible property to 
                // true but that will cause the splash screen to be activated
                // thereby deactivating other windows before the app has fully
                // loaded. In an effort to prevent this, we'll use apis to show
                // the splash screen without activating it. Note, since we are 
                // showing the form, we have to do the centering and so I removed
                // the FormStartPosition property setting.
                //
                bool topMost = true;
                Rectangle formRect = new Rectangle(Point.Empty, this.Size);
                Rectangle screenRect = Infragistics.Win.Utilities.ScreenFromPoint(Cursor.Position).Bounds;
                Infragistics.Win.DrawUtility.AdjustHAlign(Infragistics.Win.HAlign.Center, ref formRect, screenRect);
                Infragistics.Win.DrawUtility.AdjustVAlign(Infragistics.Win.VAlign.Middle, ref formRect, screenRect);
                Point location = formRect.Location;

                IntPtr insertAfter = topMost ? NativeWindowMethods.HWND_TOPMOST : IntPtr.Zero;

                Form form = this;
                NativeWindowMethods.SetWindowPos(form.Handle, insertAfter, location.X, location.Y, 0, 0, NativeWindowMethods.SWP_NOACTIVATE | NativeWindowMethods.SWP_NOSIZE);
                NativeWindowMethods.ShowWindow(form.Handle, NativeWindowMethods.SW_SHOWNA);

                base.SetVisibleCore(visible);
            }

            base.SetVisibleCore(visible);
        }
        #endregion //SetVisibleCore

        #region WndProc
        protected override void WndProc(ref System.Windows.Forms.Message message)
        {
            // We don't want the splash screen to be clickable and if possible
            // prevent its activation.
            switch (message.Msg)
            {
                case NativeWindowMethods.WM_MOUSEACTIVATE:
                    message.Result = (IntPtr)NativeWindowMethods.MA_NOACTIVATE;
                    break;
                case NativeWindowMethods.WM_NCHITTEST:
                    message.Result = (IntPtr)NativeWindowMethods.HTTRANSPARENT;
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine(message.ToString(), DateTime.Now.ToString("hh:mm:ss:ffffff"));
                    base.WndProc(ref message);
                    break;
            }
        }
        #endregion //WndProc

        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ProjectManagerForm.SplashLoadedEvent.Set();
        }
        #endregion //OnLoad

        #endregion //Base class overrides

        #region Event-related

        #region Delegates

        /// <summary>
        /// Event handler for the <see cref="StyleSetList.StyleSetSelectionCommitted"/> event
        /// </summary>
        internal delegate void InitializationStatusChangedEventHandler(object sender, InitializationStatusChangedEventArgs e);

        #endregion Delegates

        #region Event Args

        #region InitializationStatusChangedEventArgs
        internal class InitializationStatusChangedEventArgs : EventArgs
        {
            private bool showProgressBar = false;
            private int percentComplete = 100;
            private string status = null;

            public InitializationStatusChangedEventArgs(string status) : this(status, false, 0) { }

            public InitializationStatusChangedEventArgs(string status, bool showProgressBar, int percentComplete)
            {
                this.status = status;
                this.showProgressBar = showProgressBar;
                this.percentComplete = percentComplete;
            }

            public string Status
            {
                get { return this.status; }
            }

            public bool ShowProgressBar
            {
                get { return this.showProgressBar; }
            }

            public int PercentComplete
            {
                get { return this.percentComplete; }
            }
        }
        #endregion InitializationStatusChangedEventArgs

        #endregion Event Args	

        #endregion //Event-related
    }
}
