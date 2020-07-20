using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ProjectManager
{
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
