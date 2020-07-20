using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing.Imaging;
using Infragistics.Win.Misc;

namespace Showcase.CustomControl
{
    #region ModalPanelManager

    /// <summary>
    /// Custom component to show a content control modally within the same form.
    /// </summary>
    public class ModalPanelManager : Component, IMessageFilter
    {
        #region Members

        private System.ComponentModel.IContainer components = null;
        private ModalPanel panel;
        private UltraExpandableGroupBox modalContainer;
        private Brush brush;
        private int alpha = 128;
        private Color color = Color.Black;
        private bool clickHandlerHooked = false;

        #endregion //Members

        #region Constructor

        public ModalPanelManager()
        {
            this.InitializeComponent();
        }

        public ModalPanelManager(IContainer container)
        {
            container.Add(this);
            this.InitializeComponent();
        }

        #endregion //Constructor

        #region Base Class Overrides

        #region Dispose

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                if (this.panel != null)
                {
                    if (this.panel.Parent != null)
                        panel.Parent.Controls.Remove(this.panel);

                    this.panel.Dispose();
                    this.panel = null;
                }

                if (this.brush != null)
                {
                    this.brush.Dispose();
                    this.brush = null;
                }

                if (this.modalContainer != null)
                {
                    this.modalContainer.ExpandedStateChanging -= new CancelEventHandler(modalContainer_ExpandedStateChanging);
                    this.modalContainer.Panel.Controls.Clear();
                    this.modalContainer.Dispose();
                    this.modalContainer = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion //Dispose

        #endregion //Base Class Overrides

        #region Event Handlers

        #region panel_Click

        /// <summary>
        /// Handles the Click event of the panel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void panel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        #endregion // panel_Click

        #endregion //Event Handlers

        #region Properties

        #region BackgroundBrush

        /// <summary>
        /// Gets the brush used to darken the background image.
        /// </summary>
        private Brush BackgroundBrush
        {
            get
            {
                if (this.brush == null)
                    this.brush = new SolidBrush(Color.FromArgb(this.alpha, this.Color));

                return this.brush;
            }
        }

        #endregion //BackgroundBrush

        #region AlphaLevel

        /// <summary>
        /// Gets or sets the alpha level.
        /// </summary>
        /// <value>
        /// The alpha level.
        /// </value>
        [DefaultValue(128)]
        public int AlphaLevel
        {
            get
            {
                return this.alpha;
            }
            set
            {
                if (this.brush != null)
                {
                    this.brush.Dispose();
                    this.brush = null;
                }
                this.alpha = value;
            }
        }

        #endregion //AlphaLevel

        #region BlurRadius

        /// <summary>
        /// Gets or sets the blur radius.
        /// </summary>
        /// <value>
        /// The blur radius.
        /// </value>
        [DefaultValue(0)]
        public int BlurRadius { get; set; }

        #endregion // BlurRadius

        #region Color

        /// <summary>
        /// Gets or sets the darkening color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [DefaultValue(typeof(Color), "0x000000")]
        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                if (this.brush != null)
                {
                    this.brush.Dispose();
                    this.brush = null;
                }
                this.color = value;
            }
        }

        #endregion // Color

        #region ModalContainer

        /// <summary>
        /// Gets the modal container used to display the contents.
        /// </summary>
        private UltraExpandableGroupBox ModalContainer
        {
            get
            {
                if (this.modalContainer == null)
                {
                    this.modalContainer = new UltraExpandableGroupBox();
                    this.modalContainer.ViewStyle = GroupBoxViewStyle.Office2003;
                    this.modalContainer.ExpansionIndicator = GroupBoxExpansionIndicator.Far;
                    this.modalContainer.BorderStyle = GroupBoxBorderStyle.RectangularSolid;
                    this.modalContainer.ExpandedStateChanging += new CancelEventHandler(modalContainer_ExpandedStateChanging);
                    this.modalContainer.CreateControl(); //explicitly call CreateControl() to keep from replacing the Panel later.
                }
                return this.modalContainer;
            }
        }

        #region modalContainer_ExpandedStateChanging

        void modalContainer_ExpandedStateChanging(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        #endregion //modalContainer_ExpandedStateChanging

        #endregion //ModalContainer

        #region Panel

        /// <summary>
        /// Gets the panel displayed within the parent control
        /// </summary>
        private ModalPanel Panel
        {
            get
            {
                if (this.panel == null)
                {
                    this.panel = new ModalPanel();
                }
                return this.panel;
            }
        }

        #endregion //Panel

        #region IsOpen

        /// <summary>
        /// Determines if the ModalPanel is being shown
        /// </summary>
        internal bool IsOpen
        {
            get { return this.panel != null && this.panel.Visible; }
        }

        #endregion //IsOpen

        #endregion //Properties

        #region Methods

        #region Show

        /// <summary>
        /// Shows the content control modally within the provided parent control.
        /// </summary>
        /// <param name="parent">The parent control.</param>
        /// <param name="contents">The content control.</param>
        /// <param name="useContainer">if set to <c>true</c>, the contents are shown within a closable pane</param>
        /// <param name="closeOnOutsideClick">if set to <c>true</c> clicking outside the contents will close the modal panel</param>
        public void Show(Control parent,  Control contents, bool useContainer = true, bool closeOnOutsideClick = false)
        {
            if (this.Panel.Visible || parent == null)
            {
                Debug.Fail("");
                return;
            }

            // fire the BeforeShown event
            this.OnBeforeShown(EventArgs.Empty);

            // reverse the z-order of controls as DrawToBitmap() draws them in reverse order.
            ModalPanelUtilities.InvertZOrderOfControls(parent);

            // create a bitmap of the parent to use as the background.
            // This is done to fake transparency, as true transparancy is 
            // Windows Forms was poorly designed and buggy.
            Bitmap bitmap = new Bitmap(parent.Width, parent.Height);
            parent.DrawToBitmap(bitmap, parent.ClientRectangle);

            // change the z-order back
            ModalPanelUtilities.InvertZOrderOfControls(parent);

            // darken the image
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillRectangle(this.BackgroundBrush, new Rectangle(Point.Empty, bitmap.Size));
            }

            // blur the background image if necessary
            ModalPanelUtilities.FastBlur(bitmap, this.BlurRadius);


            // set the background and position the ModalPanel
            this.Panel.CustomBackground = bitmap;
            this.panel.Bounds = parent.ClientRectangle;

            if (closeOnOutsideClick)
                this.panel.Click += new EventHandler(panel_Click);
            this.clickHandlerHooked = closeOnOutsideClick;

            // add the contents
            if (useContainer)
            {
                this.ModalContainer.Size = new Size(contents.Width, contents.Height + 22);
                this.modalContainer.Panel.Controls.Add(contents);
                this.modalContainer.Text = contents.Text;
                contents.Dock = DockStyle.Fill;
                this.panel.Contents = this.modalContainer;
            }
            else
                this.panel.Contents = contents;

            // show the panel
            parent.Controls.Add(this.panel);
            this.panel.BringToFront();
            this.panel.Visible = true;

            // add a message filter to handle the Escape key
            Infragistics.Win.Utilities.AddMessageFilter(this);

            // fire the AfterShown event
            this.OnAfterShown(EventArgs.Empty);

        }

        #endregion // Show

        #region Hide

        /// <summary>
        /// Hides the ModalPanel
        /// </summary>
        public void Hide()
        {
            if (this.clickHandlerHooked)
            {               
                this.panel.Click -= new EventHandler(panel_Click);
            }

            if (this.panel == null ||
                this.panel.Visible == false)
                return;

            // remove the MessageFilter
            Infragistics.Win.Utilities.RemoveMessageFilter(this);

            // hide and remove  the panel
            Control parent = this.panel.Parent;
            if (parent.Controls.Contains(this.panel))
                parent.Controls.Remove(this.panel);

            this.panel.Visible = false;
            this.panel.Contents = null;

            if (this.modalContainer != null)
                this.modalContainer.Panel.Controls.Clear();

            // fire the AfterClosed event
            this.OnAfterClosed(EventArgs.Empty);
        }

        #endregion // Hide

        #region InitializeComponent

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion // InitializeComponent

        #endregion //Methods

        #region Event Code

        public delegate void BeforeModalShownEventHandler(object sender, EventArgs e);
        public event BeforeModalShownEventHandler BeforeShown;
        public delegate void AfterModalShownEventHandler(object sender, EventArgs e);
        public event AfterModalShownEventHandler AfterShown;
        public delegate void AfterModalClosedEventHandler(object sender, EventArgs e);
        public event AfterModalClosedEventHandler AfterClosed;

        #region OnBeforeShown

        /// <summary>
        /// Raises the <see cref="E:BeforeShown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnBeforeShown(EventArgs e)
        {
            if (this.BeforeShown != null)
                this.BeforeShown(this, e);
        }

        #endregion // OnBeforeShown

        #region OnAfterShown

        /// <summary>
        /// Raises the <see cref="E:AfterShown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterShown(EventArgs e)
        {
            if (this.AfterShown != null)
                this.AfterShown(this, e);
        }

        #endregion // OnAfterShown

        #region OnAfterClosed

        /// <summary>
        /// Raises the <see cref="E:AfterClosed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterClosed(EventArgs e)
        {
            if (this.AfterClosed != null)
                this.AfterClosed(this, e);
        }

        #endregion // OnAfterClosed

        #endregion //Event Code

        #region ModalPanel

        private class ModalPanel : Control
        {

            #region Members

            private Control contents;
            private Bitmap customBackground;

            #endregion //Members

            #region Constructor

            internal ModalPanel()
            {
                this.Visible = false;
                this.DoubleBuffered = true;
            }
            #endregion // Constructor

            #region Base Class Overrides

            #region Dispose

            /// <summary>
            /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control"/> and its child controls and optionally releases the managed resources.
            /// </summary>
            /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (this.contents != null)
                    {
                        this.Controls.Remove(this.contents);
                        this.contents = null;
                    }


                    this.BackgroundImage = null;
                    if (this.customBackground != null)
                    {
                        this.customBackground.Dispose();
                        this.customBackground = null;
                    }

                }

                base.Dispose(disposing);
            }

            #endregion // Dispose

            #endregion // Base Class Overrides

            #region Contents

            /// <summary>
            /// Gets or sets the contents of the ModalPanel
            /// </summary>
            internal Control Contents
            {
                get
                {
                    return this.contents;
                }
                set
                {
                    if (this.contents != null)
                        this.Controls.Remove(this.contents);

                    this.contents = value;

                    if (this.contents != null)
                    {
                        this.Controls.Add(this.contents);
                        this.contents.Left = (this.ClientSize.Width - this.contents.Width) / 2;
                        this.contents.Top = (this.ClientSize.Height - this.contents.Height) / 2;
                        this.contents.Anchor = AnchorStyles.None;
                        this.BringToFront();
                    }
                }
            }

            #endregion //Contents

            #region CustomBackground

            /// <summary>
            /// Gets or sets the custom background used to fake transparency
            /// </summary>
            internal Bitmap CustomBackground
            {
                get
                {
                    return this.customBackground;
                }
                set
                {
                    // clean up an existing background image.
                    if (this.customBackground != null)
                    {
                        this.BackgroundImage = null;
                        this.customBackground.Dispose();
                        this.customBackground = null;
                    }

                    // assign the new image
                    this.customBackground = value;
                    if (this.customBackground != null)
                    {                        
                        this.BackColor = Color.Black;
                        this.BackgroundImageLayout = ImageLayout.None;
                        this.BackgroundImage = this.customBackground;
                    }
                }
            }

            #endregion //CustomBackground

        }

        #endregion // ModalPanel

        #region ModalPanelUtilities

        /// <summary>
        /// Utility class for the ModalPanelManager
        /// </summary>
        internal class ModalPanelUtilities
        {
            #region FastBlur

            /// <summary>
            /// Blurs an image
            /// </summary>
            /// <param name="SourceImage">The source image.</param>
            /// <param name="radius">The radius.</param>
            public static void FastBlur(Bitmap SourceImage, int radius)
            {
                if (radius == 0)
                    return;

                var rct = new Rectangle(0, 0, SourceImage.Width, SourceImage.Height);
                var dest = new int[rct.Width * rct.Height];
                var source = new int[rct.Width * rct.Height];
                var bits = SourceImage.LockBits(rct, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                System.Runtime.InteropServices.Marshal.Copy(bits.Scan0, source, 0, source.Length);
                SourceImage.UnlockBits(bits);

                if (radius < 1) return;

                int w = rct.Width;
                int h = rct.Height;
                int wm = w - 1;
                int hm = h - 1;
                int wh = w * h;
                int div = radius + radius + 1;
                var r = new int[wh];
                var g = new int[wh];
                var b = new int[wh];
                int rsum, gsum, bsum, x, y, i, p1, p2, yi;
                var vmin = new int[max(w, h)];
                var vmax = new int[max(w, h)];

                var dv = new int[256 * div];
                for (i = 0; i < 256 * div; i++)
                {
                    dv[i] = (i / div);
                }

                int yw = yi = 0;

                for (y = 0; y < h; y++)
                { // blur horizontal
                    rsum = gsum = bsum = 0;
                    for (i = -radius; i <= radius; i++)
                    {
                        int p = source[yi + min(wm, max(i, 0))];
                        rsum += (p & 0xff0000) >> 16;
                        gsum += (p & 0x00ff00) >> 8;
                        bsum += p & 0x0000ff;
                    }
                    for (x = 0; x < w; x++)
                    {

                        r[yi] = dv[rsum];
                        g[yi] = dv[gsum];
                        b[yi] = dv[bsum];

                        if (y == 0)
                        {
                            vmin[x] = min(x + radius + 1, wm);
                            vmax[x] = max(x - radius, 0);
                        }
                        p1 = source[yw + vmin[x]];
                        p2 = source[yw + vmax[x]];

                        rsum += ((p1 & 0xff0000) - (p2 & 0xff0000)) >> 16;
                        gsum += ((p1 & 0x00ff00) - (p2 & 0x00ff00)) >> 8;
                        bsum += (p1 & 0x0000ff) - (p2 & 0x0000ff);
                        yi++;
                    }
                    yw += w;
                }

                for (x = 0; x < w; x++)
                { // blur vertical
                    rsum = gsum = bsum = 0;
                    int yp = -radius * w;
                    for (i = -radius; i <= radius; i++)
                    {
                        yi = max(0, yp) + x;
                        rsum += r[yi];
                        gsum += g[yi];
                        bsum += b[yi];
                        yp += w;
                    }
                    yi = x;
                    for (y = 0; y < h; y++)
                    {
                        dest[yi] = (int)(0xff000000u | (uint)(dv[rsum] << 16) | (uint)(dv[gsum] << 8) | (uint)dv[bsum]);
                        if (x == 0)
                        {
                            vmin[y] = min(y + radius + 1, hm) * w;
                            vmax[y] = max(y - radius, 0) * w;
                        }
                        p1 = x + vmin[y];
                        p2 = x + vmax[y];

                        rsum += r[p1] - r[p2];
                        gsum += g[p1] - g[p2];
                        bsum += b[p1] - b[p2];

                        yi += w;
                    }
                }

                // copy back to image
                var bits2 = SourceImage.LockBits(rct, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                System.Runtime.InteropServices.Marshal.Copy(dest, 0, bits2.Scan0, dest.Length);
                SourceImage.UnlockBits(bits);
            }

            private static int min(int a, int b) { return Math.Min(a, b); }
            private static int max(int a, int b) { return Math.Max(a, b); }

            #endregion //FastBlur

            #region InvertZOrderOfControls
            /// <summary>
            /// Inverts the Z order of controls.
            /// </summary>
            /// <param name="ctrlParent">The CTRL parent.</param>
            internal static void InvertZOrderOfControls(Control ctrlParent)
            {
                if (ctrlParent.Controls.Count == 0)
                {
                    return;
                }

                if (ctrlParent is FlowLayoutPanel == false)
                {
                    List<Control> childControls = new List<Control>();
                    List<Control> sortedChildControls = new List<Control>();
                    foreach (Control ctrlChild in ctrlParent.Controls)
                    {
                        if (ctrlChild.Dock != DockStyle.None)
                            continue;

                        childControls.Add(ctrlChild);
                    }
                    while (childControls.Count > 0)
                    {
                        Control ctrlMin = FindControlWithMinZOrder(ctrlParent, childControls);
                        sortedChildControls.Add(ctrlMin);
                        childControls.Remove(ctrlMin);
                    }
                    for (int i = 0; i < sortedChildControls.Count / 2; i++)
                    {
                        Control ctrlChild1 = sortedChildControls[i];
                        Control ctrlChild2 = sortedChildControls[sortedChildControls.Count - 1 - i];
                        int zOrder1 = ctrlParent.Controls.GetChildIndex(ctrlChild1);
                        int zOrder2 = ctrlParent.Controls.GetChildIndex(ctrlChild2);
                        ctrlParent.Controls.SetChildIndex(ctrlChild1, zOrder2);
                        ctrlParent.Controls.SetChildIndex(ctrlChild2, zOrder1);
                    }
                }

                foreach (Control ctrlChild in ctrlParent.Controls)
                {
                    InvertZOrderOfControls(ctrlChild);
                }
            }
            #endregion //InvertZOrderOfControls

            #region FindControlWithMinZOrder
            /// <summary>
            /// Finds the control with lowest Z-order.
            /// </summary>
            /// <param name="ctrlParent">The CTRL parent.</param>
            /// <param name="children">The children.</param>
            /// <returns></returns>
            internal static Control FindControlWithMinZOrder(Control ctrlParent, List<Control> children)
            {
                if (children.Count == 0)
                {
                    return null;
                }
                Control ctrlMin = children[0];
                foreach (Control ctrl in children)
                {
                    if (ctrlParent.Controls.GetChildIndex(ctrl) < ctrlParent.Controls.GetChildIndex(ctrlMin))
                    {
                        ctrlMin = ctrl;
                    }
                }
                return ctrlMin;
            }
            #endregion //FindControlWithMinZOrder
        }

        #endregion //ModalPanelUtilities

        #region IMessageFilter

        #region PreFilterMessage

        /// <summary>
        /// Filters out a message before it is dispatched.
        /// </summary>
        /// <param name="m">The message to be dispatched. You cannot modify this message.</param>
        /// <returns>
        /// true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.
        /// </returns>
        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            // the Escape key is pressed, hide the ModalPanel
            if (m.Msg == 0x0100) // WM_KEYDOWN
              {
                Keys keyCode = (Keys)(int)m.WParam & Keys.KeyCode;
                if (keyCode == Keys.Escape)
                {
                    if (this.IsOpen)
                    {
                        this.Hide();
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion //PreFilterMessage

        #endregion //IMessageFilter
    }

    #endregion //ModalPanelManager
}
