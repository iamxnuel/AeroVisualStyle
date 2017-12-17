using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace VisualControl.AeroBasic
{
    public static class FormRenderer
    {
        public static void RenderBorder(Graphics g, Rectangle r, BorderState state, Color TransparencyKey)
        {
            VisualStyleRenderer captionRenderer;
            VisualStyleRenderer rightFrameRenderer;
            VisualStyleRenderer leftFrameRenderer;
            VisualStyleRenderer bottomFrameRenderer;

            if(state == BorderState.Active)
            {
                captionRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
                rightFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameRight.Active);
                leftFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameLeft.Active);
                bottomFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameBottom.Active);
            }
            else
            if (state == BorderState.Disabled)
            {
                captionRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Inactive);
                rightFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameRight.Inactive);
                leftFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameLeft.Inactive);
                bottomFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameBottom.Inactive);
            }
            else
            {
                captionRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Inactive);
                rightFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameRight.Inactive);
                leftFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameLeft.Inactive);
                bottomFrameRenderer = new VisualStyleRenderer(VisualStyleElement.Window.FrameBottom.Inactive);
            }

            leftFrameRenderer.DrawBackground(g, new Rectangle(new Point(0,0), new Size(6, r.Height)));
            rightFrameRenderer.DrawBackground(g, new Rectangle(new Point(r.Width - 6, 0), new Size(6, r.Height)));
            bottomFrameRenderer.DrawBackground(g, new Rectangle(new Point(0, r.Height - 7), new Size(r.Width, 7)));
            captionRenderer.DrawBackground(g, new Rectangle(new Point(0, 0), new Size(r.Width, 30)));

            g.DrawLine(new Pen(TransparencyKey, 1F), new Point(0,0), new Point(3,0));
            g.DrawLine(new Pen(TransparencyKey, 1F), new Point(0, 1), new Point(1,1));
            g.DrawLine(new Pen(TransparencyKey, 1F), new Point(0, 2), new Point(0, 3));

            g.DrawLine(new Pen(TransparencyKey, 1F), new Point(r.Width - 4, 0), new Point(r.Width, 0));
            g.DrawLine(new Pen(TransparencyKey, 1F), new Point(r.Width - 2, 1), new Point(r.Width, 1));
            g.DrawLine(new Pen(TransparencyKey, 1F), new Point(r.Width - 1, 2), new Point(r.Width - 1, 3));

        }
        public static void RenderIcon(Graphics g, Rectangle r, Icon i)
        {
            g.DrawIcon(i, r);
        }
        public static void RenderTitle(Graphics g, Rectangle r, string Text)
        {
            g.DrawString(Text, new Font("Segoe UI", 9), Brushes.Black, r);
        }
        public static void RenderCloseButton(Graphics g, Rectangle r, ButtonState state = ButtonState.Normal)
        {
            VisualStyleRenderer renderer;
            if (state == ButtonState.Disabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Disabled);
                renderer.DrawBackground(g, r);
                return;
            }
            if (state == ButtonState.Pressed)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Pressed);
                renderer.DrawBackground(g, r);
            }
            else if (state == ButtonState.Hovered)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Hot);
                renderer.DrawBackground(g, r);
            }
            else
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.CloseButton.Normal);
                renderer.DrawBackground(g, r);
            }
        }
        public static void RenderMaximizeButton(Graphics g, Rectangle r, ButtonState state = ButtonState.Normal)
        {
            VisualStyleRenderer renderer;
            if (state == ButtonState.Disabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.MaxButton.Disabled);
                renderer.DrawBackground(g, r);
                return;
            }
            if (state == ButtonState.Pressed)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.MaxButton.Pressed);
                renderer.DrawBackground(g, r);
            }
            else if (state == ButtonState.Hovered)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.MaxButton.Hot);
                renderer.DrawBackground(g, r);
            }
            else
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.MaxButton.Normal);
                renderer.DrawBackground(g, r);
            }
        }
        public static void RenderRestoreButton(Graphics g, Rectangle r, ButtonState state = ButtonState.Normal)
        {
            VisualStyleRenderer renderer;
            if (state == ButtonState.Disabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.RestoreButton.Disabled);
                renderer.DrawBackground(g, r);
                return;
            }
            if (state == ButtonState.Pressed)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.RestoreButton.Pressed);
                renderer.DrawBackground(g, r);
            }
            else if (state == ButtonState.Hovered)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.RestoreButton.Hot);
                renderer.DrawBackground(g, r);
            }
            else
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.RestoreButton.Normal);
                renderer.DrawBackground(g, r);
            }
        }
        public static void RenderMinimizeButton(Graphics g, Rectangle r, ButtonState state = ButtonState.Normal)
        {
            VisualStyleRenderer renderer;
            if (state == ButtonState.Disabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.MinButton.Disabled);
                renderer.DrawBackground(g, r);
                return;
            }
            if (state == ButtonState.Pressed)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.MinButton.Pressed);
                renderer.DrawBackground(g, r);
            }
            else if (state == ButtonState.Hovered)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.MinButton.Hot);
                renderer.DrawBackground(g, r);
            }
            else
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.MinButton.Normal);
                renderer.DrawBackground(g, r);
            }
        }
        public static void RenderHelpButton(Graphics g, Rectangle r, ButtonState state = ButtonState.Normal)
        {
            VisualStyleRenderer renderer;
            if (state == ButtonState.Disabled)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.HelpButton.Disabled);
                renderer.DrawBackground(g, r);
                return;
            }
            if (state == ButtonState.Pressed)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.HelpButton.Pressed);
                renderer.DrawBackground(g, r);
            }
            else if (state == ButtonState.Hovered)
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.HelpButton.Hot);
                renderer.DrawBackground(g, r);
            }
            else
            {
                renderer = new VisualStyleRenderer(VisualStyleElement.Window.HelpButton.Normal);
                renderer.DrawBackground(g, r);
            }
        }
       
        public enum ButtonState
        {
            Pressed,
            Hovered,
            Normal,
            Disabled
        }
        public enum BorderState
        {
            Active = 4,
            Inactive = 1,
            Disabled = 19
        }
    }
}
