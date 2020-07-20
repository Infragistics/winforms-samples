using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Threading;
using System.Runtime.InteropServices;

namespace IGExcel.Splash
{
    public partial class SplashScreen : Form
    {

        #region Private Members

        private ISupportSplashScreen splashOwner;

        #endregion Private Members

        #region Constructor

        public SplashScreen(ISupportSplashScreen owner)
        {
            this.splashOwner = owner;

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
            this.splashOwner.InitializationStatusChanged += new InitializationStatusChangedEventHandler(this.Application_InitializationStatusChanged);
            this.splashOwner.InitializationComplete += new EventHandler(this.Application_InitializationComplete);
        }
        #endregion HookEvents

        #region UnHookEvents
        private void UnHookEvents()
        {
            this.splashOwner.InitializationStatusChanged -= new InitializationStatusChangedEventHandler(this.Application_InitializationStatusChanged);
            this.splashOwner.InitializationComplete -= new EventHandler(this.Application_InitializationComplete);
            this.splashOwner = null;
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
            this.lblStatus.Text = string.Format(IGExcel.ResourceStrings.Text_ApplicationStarting, IGExcel.Properties.Resources.Title.ToUpper());
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

            this.splashOwner.SplashLoadedEvent.Set();
        }
        #endregion //OnLoad

        #endregion //Base class overrides

        #region Event-related

        #region Delegates

        /// <summary>
        /// Event handler for the <see cref="StyleSetList.StyleSetSelectionCommitted"/> event
        /// </summary>
        public delegate void InitializationStatusChangedEventHandler(object sender, InitializationStatusChangedEventArgs e);

        #endregion Delegates

        #region Event Args

        #region InitializationStatusChangedEventArgs
        public class InitializationStatusChangedEventArgs : EventArgs
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

        #region Interface ISupportSplashScreen

        public interface ISupportSplashScreen
        {

            /// <summary>
            /// Fired when the status of the form initialization has changed. 
            /// </summary>
            event SplashScreen.InitializationStatusChangedEventHandler InitializationStatusChanged;

            /// <summary>
            /// Fired when the status of the form initialization has completed. 
            /// </summary>
            event EventHandler InitializationComplete;

            ManualResetEvent SplashLoadedEvent { get; }
        }
        #endregion //ISupportSplashScreen

        #endregion //Event-related

    }

    internal class NativeWindowMethods
    {
        #region Constants

        internal static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        internal const int SW_SHOWNA = 0x0008;
        internal const int SWP_NOACTIVATE = 0x0010;
        internal const int SWP_NOSIZE = 0x0001;
        internal const int WM_MOUSEACTIVATE = 0x0021;
        internal const int MA_NOACTIVATE = 3;
        internal const int WM_NCHITTEST = 0x84;
        internal const int HTTRANSPARENT = (-1);

        #endregion //Constants

        #region APIs

        #region SetWindowPos

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int x, int y, int cx, int cy, int flags);

        #endregion SetWindowPos

        #region ShowWindow

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion ShowWindow

        #endregion //APIs
    }
}
