using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace VisualControl
{
    [DesignerCategory("Code")]
    public sealed class AeroBasicCloseButton : Control
    {

        public bool ParentCloseOnClick { get; set; } = true;

        Boolean mouseDown = false;
        Boolean enterDown = false;
        Boolean mouseEnter = false;

        protected override Size DefaultSize => new Size(32, 18);

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            try
            {
                if (ParentCloseOnClick)
                    FindForm().Close();
            }
            catch { }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (fOld != null)
                try
                {
                    fOld.SizeChanged -= AeroBasicCloseButton_SizeChanged;
                }
                catch { }

            base.OnParentChanged(e);
            FindForm().SizeChanged += AeroBasicCloseButton_SizeChanged;
            fOld = FindForm();
        }

        private void AeroBasicCloseButton_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        Form fOld;

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
            if(!Enabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Disabled);
                renderer.DrawBackground(e.Graphics, e.ClipRectangle);
                return;
            }
            if(mouseDown || enterDown)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Pressed);
                renderer.DrawBackground(e.Graphics, e.ClipRectangle);
            }else if(mouseEnter)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Hot);
                renderer.DrawBackground(e.Graphics, e.ClipRectangle);
            }
            else
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Normal);
                renderer.DrawBackground(e.Graphics, e.ClipRectangle);
            }
        }
    }
}
