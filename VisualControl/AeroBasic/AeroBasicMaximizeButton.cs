using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace VisualControl
{
    [DesignerCategory("Code")]
    public sealed class AeroBasicMaximizeButton : Control
    {

        public bool ParentMaximizeOnClick { get; set; } = true;

        Boolean mouseDown = false;
        Boolean enterDown = false;
        Boolean mouseEnter = false;

        protected override Size DefaultSize => new Size(32, 18);

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            try
            {
                if (ParentMaximizeOnClick)
                    if(FindForm().WindowState == FormWindowState.Normal)
                        FindForm().WindowState = FormWindowState.Maximized;
                    else
                        FindForm().WindowState = FormWindowState.Normal;
                Invalidate();
            }
            catch { }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if(fOld != null)
                try
                {
                    fOld.SizeChanged -= AeroBasicMaximizeButton_SizeChanged;
                }
                catch { }

            base.OnParentChanged(e);
            FindForm().SizeChanged += AeroBasicMaximizeButton_SizeChanged;
            fOld = FindForm();
        }

        Form fOld;

        private void AeroBasicMaximizeButton_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseEnter = true;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseEnter = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseDown = true;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseDown = false;
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                enterDown = true;
                Invalidate();
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Enter)
            {
                enterDown = false;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            VisualStyleRenderer renderer;
            bool RestoreMode = false;

            RestoreMode = FindForm().WindowState == FormWindowState.Maximized;

            if (!Enabled)
            {
                if (!RestoreMode)
                    renderer = new VisualStyleRenderer(VisualStyleElement.Window.MaxButton.Disabled);
                else
                    renderer = new VisualStyleRenderer(VisualStyleElement.Window.RestoreButton.Disabled);

                renderer.DrawBackground(e.Graphics, e.ClipRectangle);
                return;
            }
            if (mouseDown || enterDown)
            {
                if (!RestoreMode)
                    renderer = new VisualStyleRenderer(VisualStyleElement.Window.MaxButton.Pressed);
                else
                    renderer = new VisualStyleRenderer(VisualStyleElement.Window.RestoreButton.Pressed);

                renderer.DrawBackground(e.Graphics, e.ClipRectangle);
            }
            else if (mouseEnter)
            {
                if (!RestoreMode)
                    renderer = new VisualStyleRenderer(VisualStyleElement.Window.MaxButton.Hot);
                else
                    renderer = new VisualStyleRenderer(VisualStyleElement.Window.RestoreButton.Hot);

                renderer.DrawBackground(e.Graphics, e.ClipRectangle);
            }
            else
            {
                if (!RestoreMode)
                    renderer = new VisualStyleRenderer(VisualStyleElement.Window.MaxButton.Normal);
                else
                    renderer = new VisualStyleRenderer(VisualStyleElement.Window.RestoreButton.Normal);

                renderer.DrawBackground(e.Graphics, e.ClipRectangle);
            }
        }
    }
}
