using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VisualControl.AeroBasic
{
    /// <summary>
    /// Provides an selfwritten Implementation of the Windows-Aero-Basic-Style Window
    /// </summary>
    [DesignerCategory("Code")]
    public class AeroBasicForm : Form
    {
        #region Ctor
        /// <summary>
        /// Construct the <see cref="AeroBasicForm"/>
        /// </summary>
        public AeroBasicForm()
        {
            base.FormBorderStyle = FormBorderStyle.None;
            borderState = FormRenderer.BorderState.Active;
            DoubleBuffered = true;

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.MinimumSize = new Size(275, 150);
        }
        #endregion
        #region Fields
        FormRenderer.BorderState borderState = FormRenderer.BorderState.Active;
        #endregion
        #region WndProc Constants
        private const int cCaption = 30;
        private const int cGrip = 13;
        #endregion
        #region Boolean-Properties
        /// <summary>
        /// Set wheter ClearType and optimazions have to be used
        /// </summary>
        [Category("Design"), Description("Set wheter the ClearType and optimazions have to be used")]
        public Boolean HighQualityFont
        {
            get; set;
        } = true;
        /// <summary>
        /// Set wheter the Maximize Box is enabled
        /// </summary>
        [Category("WindowStyle"), Description("Set wheter the Maximize Box is enabled")]
        public new Boolean MaximizeBox
        {
            get => maxiButtonEnabled;
            set
            {
                maxiButtonEnabled = value;
                base.MaximizeBox = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Set wheter the Minimize Box is enabled
        /// </summary>
        [Category("WindowStyle"), Description("Set wheter the Minimize Box is enabled")]
        public new Boolean MinimizeBox
        {
            get => miniButtonEnabled;
            set
            {
                miniButtonEnabled = value;
                base.MinimizeBox = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Set wheter the Help Button have to be used when Maximize Box and Minimize Box are disabled
        /// </summary>
        [Category("WindowStyle"), Description("Set wheter the Help Button have to be used when Maximize Box and Minimize Box are disabled")]
        public new Boolean HelpButton
        {
            get => base.HelpButton;
            set
            {
                base.HelpButton = value;
            }
        }
        /// <summary>
        /// Set wheter the Close Box is enabled
        /// </summary>
        [Category("WindowStyle"), Description("Set wheter the Close Box is enabled")]
        public Boolean CloseBox
        {
            get => closeButtonEnabled;
            set
            {
                closeButtonEnabled = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Set wheter the Window is resizeable
        /// </summary>
        [Category("WindowStyle"), Description("Set wheter the Window is resizeable")]
        public Boolean Resizeable { get; set; } = true;
        #endregion
        #region SysButton Boolean-Fields
        bool closeButtonHovered = false;
        bool closeButtonPressed = false;
        bool closeButtonEnabled = true;
        bool minimizeButtonHovered = false;
        bool minimizeButtonPressed = false;
        bool miniButtonEnabled = true;
        bool maximizeButtonHovered = false;
        bool maximizeButtonPressed = false;
        bool maxiButtonEnabled = true;
        #endregion
        #region Title Content Rectangles
        /// <summary>
        /// Gets the CloseButton Rectangle
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        Rectangle CloseButtonRectangle
        {
            get
            {
                return new Rectangle(Width - 41, 7, 32, 18);
            }
        }
        /// <summary>
        /// Gets the Maximize, Restore and HelpButton Rectangle (only one or none is visible, not all at the same time)
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        Rectangle MaximizeButtonRectangle
        {
            get
            {
                return new Rectangle(Width - 77, 7, 32, 18);
            }
        }
        /// <summary>
        /// Gets the Minimize Button Rectangle
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        Rectangle MinimizeButtonRectangle
        {
            get
            {
                return new Rectangle(Width - 113, 7, 32, 18);
            }
        }
        /// <summary>
        /// Gets the Icon Rectangle
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        Rectangle IconRectangle
        {
            get
            {
                return new Rectangle(10, 10, 16, 16);
            }
        }
        #endregion
        #region Overwritten
        /// <summary>
        /// Sets or gets the smallest avaible size for the window (super-min-height: 150; super-min-width = 275)
        /// </summary>
        [Category("Layout"), Description("Sets or gets the smallest avaible size for the window")]
        public override Size MinimumSize { get => base.MinimumSize; set { if (value.Height >= 150 && value.Width >= 275) base.MinimumSize = value; } }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // MessageBox.Show(e.Location.ToString());
            bool InvalidateLayout = false;

            if (CloseButtonRectangle.Contains(e.Location))
            {
                closeButtonHovered = true;
                InvalidateLayout = true;
            }
            else if (closeButtonHovered)
            {
                closeButtonHovered = false;
                InvalidateLayout = true;
            }

            if (MinimizeButtonRectangle.Contains(e.Location))
            {
                minimizeButtonHovered = true;
                InvalidateLayout = true;
            }
            else if (minimizeButtonHovered)
            {
                minimizeButtonHovered = false;
                InvalidateLayout = true;
            }

            if (MaximizeButtonRectangle.Contains(e.Location))
            {
                maximizeButtonHovered = true;
                InvalidateLayout = true;
            }
            else if (maximizeButtonHovered)
            {
                maximizeButtonHovered = false;
                InvalidateLayout = true;
            }

            if (InvalidateLayout)
                Invalidate();

            base.OnMouseMove(e);
        }
        protected override void WndProc(ref Message m)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);

                bool Resizeable = this.Resizeable;
                if (WindowState == FormWindowState.Maximized)
                    Resizeable = false;

                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }

                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    if (Resizeable)
                        m.Result = (IntPtr)17;
                    return;
                }
                if (pos.X <= cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    if (Resizeable)
                        m.Result = (IntPtr)16;
                    return;
                }
                if (pos.Y >= ClientSize.Height - 7)
                {
                    if (Resizeable)
                        m.Result = (IntPtr)15;
                    return;
                }
                if (pos.X >= ClientSize.Width - 6)
                {
                    if (Resizeable)
                        m.Result = (IntPtr)11;
                    return;
                }
                if (pos.X <= 6)
                {
                    if (Resizeable)
                        m.Result = (IntPtr)10;
                    return;
                }
            }
            else
            if (m.Msg == 0x00a0)
            {
                Point pos = PointToClient(new Point(m.LParam.ToInt32()));
                OnMouseMove(new MouseEventArgs(MouseButtons.Left, 0, pos.X, pos.Y, 0));
                m.Result = (IntPtr)0;
                return;
            }
            else
            if (m.Msg == 0x00a2)
            {
                Point pos = PointToClient(new Point(m.LParam.ToInt32()));

                if (CloseButtonRectangle.Contains(pos) && closeButtonEnabled)
                    Close();

                if (MinimizeButtonRectangle.Contains(pos) && miniButtonEnabled)
                    WindowState = FormWindowState.Minimized;

                if (MaximizeButtonRectangle.Contains(pos) && maxiButtonEnabled)
                    WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;

                if (MaximizeButtonRectangle.Contains(pos) && HelpButton && !maxiButtonEnabled && !miniButtonEnabled)
                    OnHelpButtonClicked(new CancelEventArgs(false));

                if (IconRectangle.Contains(pos) && ShowIcon)
                    ShowSystemMenu();

                closeButtonPressed = false;
                minimizeButtonPressed = false;
                maximizeButtonPressed = false;

                Invalidate();

                m.Result = (IntPtr)0;
                return;
            }
            else
                if (m.Msg == 0x00a1)
            {
                Point pos = PointToClient(new Point(m.LParam.ToInt32()));
                bool InvalidateLayout = false;
                if (CloseButtonRectangle.Contains(pos) && closeButtonEnabled)
                {
                    closeButtonPressed = true;
                    InvalidateLayout = true;
                }

                if (MinimizeButtonRectangle.Contains(pos) && miniButtonEnabled)
                {
                    minimizeButtonPressed = true;
                    InvalidateLayout = true;
                }

                if (IconRectangle.Contains(pos) && ShowIcon)
                    InvalidateLayout = true;

                if (MaximizeButtonRectangle.Contains(pos) && maxiButtonEnabled)
                {
                    maximizeButtonPressed = true;
                    InvalidateLayout = true;
                }

                if (InvalidateLayout)
                { Invalidate(); m.Result = (IntPtr)0; return; }
            }

            if (minimizeButtonHovered || minimizeButtonPressed || maximizeButtonHovered || maximizeButtonPressed || closeButtonHovered || closeButtonPressed)
                Invalidate();

            base.WndProc(ref m);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.TransparencyKey = Color.Tomato;
            AllowTransparency = true;

            base.OnPaint(e);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            if (HighQualityFont)
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            }

            if ((closeButtonHovered || closeButtonPressed) && !CloseButtonRectangle.Contains(PointToClient(MousePosition)))
            {
                closeButtonHovered = false;
                closeButtonPressed = false;
            }

            if ((maximizeButtonHovered || maximizeButtonPressed) && !MaximizeButtonRectangle.Contains(PointToClient(MousePosition)))
            {
                maximizeButtonHovered = false;
                maximizeButtonPressed = false;
            }

            if ((minimizeButtonHovered || minimizeButtonPressed) && !MinimizeButtonRectangle.Contains(PointToClient(MousePosition)))
            {
                minimizeButtonHovered = false;
                minimizeButtonPressed = false;
            }

            FormRenderer.RenderBorder(e.Graphics, e.ClipRectangle, Enabled ? borderState : FormRenderer.BorderState.Disabled, base.TransparencyKey);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            RenderTitleAreaContent(e.Graphics);

        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            borderState = FormRenderer.BorderState.Active;
            Invalidate();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            borderState = FormRenderer.BorderState.Inactive;
            Invalidate();
        }
        #endregion
        #region Public Render-Members
        /// <summary>
        /// Show the own-System Menu
        /// </summary>
        public void ShowSystemMenu()
        {
            //TODO: Add System menu
        }
        /// <summary>
        /// Render the SystemButtons for the Window
        /// </summary>
        /// <param name="g">Graphics for Render, no clip!</param>
        /// <param name="closeState">The CloseButton-State</param>
        /// <param name="maximizeState">The MaximizeButton-State</param>
        /// <param name="minimizeState">The MinimizeButton-State</param>
        public void RenderSystemButtons(Graphics g, FormRenderer.ButtonState closeState, FormRenderer.ButtonState maximizeState, FormRenderer.ButtonState minimizeState)
        {
            if (!closeButtonEnabled)
                closeState = FormRenderer.ButtonState.Disabled;

            RenderCloseButtonState(g, closeState);

            if (!maxiButtonEnabled && !miniButtonEnabled)
                if (HelpButton)
                    RenderHelpButtonState(g, maximizeState);
                else;
            else
            {
                RenderMaxButtonState(g, maxiButtonEnabled ? maximizeState : FormRenderer.ButtonState.Disabled);
                RenderMinButtonState(g, miniButtonEnabled ? minimizeState : FormRenderer.ButtonState.Disabled);
            }
        }
        /// <summary>
        /// Render the TitleArea and their content using the given Graphics-Object
        /// </summary>
        /// <param name="g"></param>
        public void RenderTitleAreaContent(Graphics g)
        {
            if (!ControlBox)
            {
                FormRenderer.RenderTitle(g, new Rectangle(10, 10, Width - 116, 16), Text);
                return;
            }

            FormRenderer.ButtonState closeState = (closeButtonPressed ? FormRenderer.ButtonState.Pressed : closeButtonHovered ? FormRenderer.ButtonState.Hovered : FormRenderer.ButtonState.Normal);
            FormRenderer.ButtonState maximizeState = (maximizeButtonPressed ? FormRenderer.ButtonState.Pressed : maximizeButtonHovered ? FormRenderer.ButtonState.Hovered : FormRenderer.ButtonState.Normal);
            FormRenderer.ButtonState minimizeState = (minimizeButtonPressed ? FormRenderer.ButtonState.Pressed : minimizeButtonHovered ? FormRenderer.ButtonState.Hovered : FormRenderer.ButtonState.Normal);

            if (ShowIcon)
            {
                FormRenderer.RenderIcon(g, new Rectangle(10, 10, 16, 16), Icon);
                FormRenderer.RenderTitle(g, new Rectangle(29, 10, Width - 100, 16), Text);
            }
            else
                FormRenderer.RenderTitle(g, new Rectangle(10, 10, Width - 116, 16), Text);

            RenderSystemButtons(g, closeState, maximizeState, minimizeState);
        }
        #endregion
        #region Private SysButton Render-Members
        private void RenderCloseButtonState(Graphics g, FormRenderer.ButtonState state)
        {
            FormRenderer.RenderCloseButton(g, new Rectangle(Width - 41, 7, 32, 18), state);
        }
        private void RenderMinButtonState(Graphics g, FormRenderer.ButtonState state)
        {
            FormRenderer.RenderMinimizeButton(g, new Rectangle(Width - 113, 7, 32, 18), state);
        }
        private void RenderMaxButtonState(Graphics g, FormRenderer.ButtonState state)
        {
            if (WindowState == FormWindowState.Maximized)
                FormRenderer.RenderRestoreButton(g, new Rectangle(Width - 77, 7, 32, 18), state);
            else
                FormRenderer.RenderMaximizeButton(g, new Rectangle(Width - 77, 7, 32, 18), state);
        }
        private void RenderHelpButtonState(Graphics g, FormRenderer.ButtonState state)
        {
            FormRenderer.RenderHelpButton(g, new Rectangle(Width - 77, 7, 32, 18), state);
        }
        #endregion
        #region ReadOnly new Properties
        /// <summary>
        /// Gets the FormBorderStyle (readonly)
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return FormBorderStyle.None; }
        }
        /// <summary>
        /// Gets the TransparencyKey (readonly)
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new Color TransparencyKey
        {
            get { return base.TransparencyKey; }
        }
        #endregion
    }
}