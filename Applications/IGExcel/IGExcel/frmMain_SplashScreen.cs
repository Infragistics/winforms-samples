using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IGExcel
{
    partial class frmMain : Splash.SplashScreen.ISupportSplashScreen
    {
        #region SplashScreen Related

        #region Splash Members

        private ManualResetEvent splashLoadedEvent;
        private bool initializationCompleted = false;

        #endregion

        #region SplashLoadedEvent
        public ManualResetEvent SplashLoadedEvent
        {
            get
            {
                return splashLoadedEvent;
            }
        }
        #endregion //SplashLoadedEvent

        #region Events

        /// <summary>
        /// Fired when the status of the form initialization has changed. 
        /// </summary>
        public event Splash.SplashScreen.InitializationStatusChangedEventHandler InitializationStatusChanged;

        /// <summary>
        /// Fired when the status of the form initialization has completed. 
        /// </summary>
        public event EventHandler InitializationComplete;

        #region OnInitializationComplete
        private void OnInitializationComplete()
        {
            EventHandler applicationIdleHandler = null;
            applicationIdleHandler = (p1, p2) =>
            {
                Application.Idle -= applicationIdleHandler;
                if (this.initializationCompleted == false)
                {
                    this.initializationCompleted = true;

                    if (this.InitializationComplete != null)
                        this.InitializationComplete(this, EventArgs.Empty);
                }
            };
            Application.Idle += applicationIdleHandler;   

        }
        #endregion OnInitializationStatusChanged

        #region OnInitializationStatusChanged
        private void OnInitializationStatusChanged(string status)
        {
            if (this.InitializationStatusChanged != null)
                this.InitializationStatusChanged(this, new Splash.SplashScreen.InitializationStatusChangedEventArgs(status));
        }

        private void OnInitializationStatusChanged(string status, bool showProgressBar, int percentComplete)
        {
            if (this.InitializationStatusChanged != null)
                this.InitializationStatusChanged(this, new Splash.SplashScreen.InitializationStatusChangedEventArgs(status, showProgressBar, percentComplete));
        }
        #endregion OnInitializationStatusChanged

        #endregion Events

        #region ShowSplashScreen

        /// <summary>
        /// Shows the splash screen.
        /// </summary>
        private void ShowSplashScreen()
        {
            splashLoadedEvent = new ManualResetEvent(false);

            ThreadStart threadStart = new ThreadStart((Action)(()=>             
            {
                Splash.SplashScreen splashScreen = new Splash.SplashScreen(this);
                Application.Run(splashScreen);
                Application.ExitThread();
            }));
                               
            Thread thread = new Thread(threadStart);
            thread.Name = "Splash Screen";
            thread.Start();
            splashLoadedEvent.WaitOne();
        }
        #endregion //ShowSplashScreen

        #endregion //SplashScreen Related
    }
}
