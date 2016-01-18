// ----------------------------------------------------------------------------
// <copyright file="OurForm.cs" company="OurCSharp">
//     Copyright © 2016 OurCSharp
// 
//     This program is free software; you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation; either version 2 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// </copyright>
// ----------------------------------------------------------------------------

namespace OurCSharp.OurForm.Core
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    using OurCSharp.OurForm.Core.Enums;
    using OurCSharp.OurForm.Core.Interfaces;
    using OurCSharp.OurForm.Core.Properties.CloseButton;
    using OurCSharp.OurForm.Core.Properties.MaximizeButton;
    using OurCSharp.OurForm.Core.Properties.MinimizeButton;

    public class OurForm : Form
    {
        #region Fields
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTCAPTION = 2;
        private const int HTCLIENT = 1;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private const int WM_NCHITTEST = 0x0084;

        private readonly Font _titleBarFont;

        private Color _backColor;
        private Color _borderTrimColor;

        private Rectangle _closeRect;
        private Rectangle _maximizeRect;
        private Rectangle _minimizeRect;

        // TODO May try to implement a different way to achieve same results as using the enums
        private MouseAction _mouseAction;
        private OurBounds _mouseIsOver;
        #endregion

        #region Properties
        [Category("OurForm")]
        [Description("Should OurForm be Movable?")]
        public bool Movable { get; set; } = true;

        [Category("OurForm")]
        [Description("Should OurForm be Sisable?")]
        public bool Sizable { get; set; } = true;

        [Category("OurForm")]
        [DefaultValue(typeof(Color), "255, 0, 0, 0")]
        [Description("Color of the Border around OurForm.")]
        public Color BorderColor
        {
            /*
             * Uses the 'base.BackColor' because the BorderColor is the BackGround and instead of handling the
             * BackColor with OnPaint event, it's more efficient and will use less resrouces.
             */ // BUG Lets check to see if this fixes the BackColor issue we previously had.
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]
        [DefaultValue(typeof(Color), "255, 25, 25, 25")]
        [Description("Color of the Trim on the Border around OurFrom.")]
        public Color BorderTrimColor
        {
            get { return this._borderTrimColor; }
            set
            {
                this._borderTrimColor = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]

        // TODO Need to create Resource File with the Embeded.
        // TODO Decorate with 'DeffaultValue'
        // TODO May use external Lib for Images and Icons.
        // TODO Have special folders for specific items.
        [Description("The Icon shown at the top left of OurForm if ShowIcon is true.")]
        public new Icon Icon
        {
            get { return base.Icon; }
            set
            {
                base.Icon = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]
        [DefaultValue(typeof(Color), "255, 75, 75, 75")]
        [Description("The background Color of OurForm.")]
        public override Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]
        [DefaultValue(typeof(Color), "255, 255, 255, 255")]
        [Description("The foreground Color of OurForm.")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]
        [Description("The Minimum Size for OurForm.")]
        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set
            {
                int minWidth;

                base.MinimumSize = value.Width
                                   >= (minWidth =
                                       (int)this.CreateGraphics().MeasureString(this.Text, this._titleBarFont).Width
                                       + 116) && value.Height >= 52
                                       ? value
                                       : new Size(value.Width < minWidth ? minWidth : value.Width,
                                                  value.Height < 52 ? 52 : value.Height);
            }
        }

        [Category("OurForm")]
        [DefaultValue("OurForm")]
        [Description("Text displayed in the title bar of OurForm.")]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                this.Invalidate();
            }
        }

        [DefaultValue(true)]
        [Description("This cannot be changed for rendering and visual appearance reasons.")]
        protected override bool DoubleBuffered { get { return base.DoubleBuffered; } set { } }

        [Category("DisabledProperties")]

        // TODO If this is not set automatically, first try decorating with 'DefaultValue' else assign in constructor.
        [Description("This property cannot be changed for the reason of the appearance of OurForm.")]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            private set { base.FormBorderStyle = FormBorderStyle.None; }
        }

        [Category("DisabledProperties")]
        [Description("Please see 'CloseButton', 'MaximizeButton' and 'MinimizeButton' to achieve duplicate results.")]
        [Obsolete("Please see 'CloseButton', 'MaximizeButton' and 'MinimizeButton' to achieve duplicate results.")]
        public new string ControlBox { get; } = "Deprecated!  Read Description.";

        [Category("DisabledProperties")]
        [Description("Please use 'MaximizeButton' to achieve duplicate results.")]
        [Obsolete("Please use 'MaximizeButton' to achieve duplicate results.")]
        public new string MaximizeBox { get; } = "Deprecated!  Read Description.";

        [Category("DisabledProperties")]
        [Description("Please use 'MinimizeButton' to achieve duplicate results.")]
        [Obsolete("Please use 'MinimizeButton' to achieve duplicate results.")]
        public new string MinimizeBox { get; } = "Deprecated!  Read Description.";

        [Category("OurForm")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]

        // TODO See if this shows up in the property grid even though it's internal.
        internal OurCloseButton CloseButton { get; }

        [Category("OurForm")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public OurMaximizeButton MaximizeButton { get; }

        [Category("OurForm")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public OurMinimizeButton MinimizeButton { get; }
        #endregion

        #region Constructors
        protected OurForm()
        {
            this._titleBarFont = new Font("Consolas", 10F, FontStyle.Regular);

            /*
             * Going to assign the overriden properties in the 'OnCreateControl' despite that it's in practice to do so
             * in the Constructor, but being that they are overriden, I'm hoping that I'm not going to have to use
             * the qualifier 'base'.
             */

            // TODO Make sure everthing is set accordingly, else modify it to as it was previously.

            this.Padding = new Padding(43, 25, 7, 7);

            this.CloseButton = new OurCloseButton(this);
            this.MaximizeButton = new OurMaximizeButton(this);
            this.MinimizeButton = new OurMinimizeButton(this);

            this._closeRect = new Rectangle(this.Width - 30, 0, 24, 24);
            this._maximizeRect = new Rectangle(this.Width - 54, 0, 24, 24);
            this._minimizeRect = new Rectangle(this.Width - 78, 0, 24, 24);

            // TODO Not sure if should Invalidate here or just in the 'OnCreateControl'..
        }
        #endregion

        #region Methods
        private bool ControlBoxContains(Point p)
        {
            if (this.ControlBoxContains(this.CloseButton, p, this._closeRect)
                || this.ControlBoxContains(this.MaximizeButton, p, this._maximizeRect)
                || this.ControlBoxContains(this.MinimizeButton, p, this._minimizeRect)) {
                    return true;
                }

            this._mouseIsOver = OurBounds.Client;

            if (this._mouseAction == MouseAction.Hover) { this.Invalidate(); }

            this._mouseAction = MouseAction.None;

            return false;
        }

        private bool ControlBoxContains(IOurFormButtonBase buttonBase, Point p, Rectangle r)
        {
            // Might have an issue with this...
            if (!this.IsVisible(buttonBase)
                || !r.Contains(p)) {
                    return false;
                }

            this._mouseIsOver = buttonBase.ButtonBounds;
            this._mouseAction = buttonBase.State != OurFormButtonStates.Disabled
                                    /* 
                                     * The extra Trenary is required otherwise when the mouse is clicked
                                     * is the _mouseAction would always of been set to MouseAction.Hover
                                     */
                                    ? this._mouseAction == MouseAction.Click ? this._mouseAction : MouseAction.Hover
                                    : MouseAction.None;
            return true;
        }

        private bool IsVisible(IOurFormButtonBase buttonBase)
        {
            switch (buttonBase.State)
            {
                case OurFormButtonStates.Disabled:
                case OurFormButtonStates.Shown:
                    return true;
                case OurFormButtonStates.Hidden:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonBase.State),
                                                          buttonBase.State,
                                                          "Something Fucked Up..");
            }
        }

        private void PaintButtons(Graphics g, ref Pen p, ref SolidBrush sB)
        {
            switch (this._mouseAction)
            {
                case MouseAction.Click:
                case MouseAction.Hover:
                    this.PaintButtons(this._mouseAction, g, ref p, ref sB);
                    break;
                case MouseAction.None:
                    this.PaintButton(this.CloseButton, g, ref p, ref sB, this._closeRect);
                    this.PaintButton(this.MaximizeButton, g, ref p, ref sB, this._maximizeRect);
                    this.PaintButton(this.MinimizeButton, g, ref p, ref sB, this._minimizeRect);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(this._mouseAction));
            }
        }

        private void PaintButtons(MouseAction mouseAction, Graphics g, ref Pen p, ref SolidBrush sB)
        {
            switch (this._mouseIsOver)
            {
                case OurBounds.Client:
                    throw new Exception("Something Fucked Up..");
                case OurBounds.CloseButton:
                    this.PaintButton(
                                     this.CloseButton.State != OurFormButtonStates.Disabled
                                         ? mouseAction == MouseAction.Hover
                                               ? this.CloseButton.Hovered
                                               : this.CloseButton.Clicked
                                         : this.CloseButton.Disabled,
                                     g,
                                     ref p,
                                     ref sB,
                                     this._closeRect);
                    this.PaintButton(this.MaximizeButton, g, ref p, ref sB, this._maximizeRect);
                    this.PaintButton(this.MinimizeButton, g, ref p, ref sB, this._minimizeRect);
                    break;
                case OurBounds.MaximizeButton:
                    this.PaintButton(
                                     this.MaximizeButton.State != OurFormButtonStates.Disabled
                                         ? mouseAction == MouseAction.Hover
                                               ? this.MaximizeButton.Hovered
                                               : this.MaximizeButton.Clicked
                                         : this.MaximizeButton.Disabled,
                                     g,
                                     ref p,
                                     ref sB,
                                     this._maximizeRect);
                    this.PaintButton(this.CloseButton, g, ref p, ref sB, this._closeRect);
                    this.PaintButton(this.MinimizeButton, g, ref p, ref sB, this._minimizeRect);
                    break;
                case OurBounds.MinimizeButton:
                    this.PaintButton(
                                     this.MinimizeButton.State != OurFormButtonStates.Disabled
                                         ? mouseAction == MouseAction.Hover
                                               ? this.MinimizeButton.Hovered
                                               : this.MinimizeButton.Clicked
                                         : this.MinimizeButton.Disabled,
                                     g,
                                     ref p,
                                     ref sB,
                                     this._minimizeRect);
                    this.PaintButton(this.CloseButton, g, ref p, ref sB, this._closeRect);
                    this.PaintButton(this.MaximizeButton, g, ref p, ref sB, this._maximizeRect);
                    break;
                case OurBounds.OffClient:
                    this.PaintButton(this.CloseButton, g, ref p, ref sB, this._closeRect);
                    this.PaintButton(this.MaximizeButton, g, ref p, ref sB, this._maximizeRect);
                    this.PaintButton(this.MinimizeButton, g, ref p, ref sB, this._minimizeRect);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(this._mouseIsOver),
                                                          this._mouseIsOver,
                                                          "Something Fucked Up..");
            }
        }

        private void PaintButton(IOurFormButtonBase buttonBase, Graphics g, ref Pen p, ref SolidBrush sB, Rectangle r)
            =>
                this.PaintButton(
                                 buttonBase.State != OurFormButtonStates.Disabled
                                     ? buttonBase.Normal
                                     : buttonBase.Disabled,
                                 g,
                                 ref p,
                                 ref sB,
                                 r);

        private void PaintButton(IOurFormButtonDesigner buttonDesigner,
                                 Graphics g,
                                 ref Pen p,
                                 ref SolidBrush sB,
                                 Rectangle r)
        {
            if (buttonDesigner.DrawBox)
            {
                sB.Color = buttonDesigner.BoxColor;
                g.FillRectangle(sB, r.X + 1, r.Y + 1, r.Width - 2, r.Height - 2);
            }

            if (buttonDesigner.DrawBoxBorder)
            {
                p.Color = buttonDesigner.BoxBorderColor;
                g.FillRectangle(sB, r.X + 1, r.Y + 1, r.Width - 2, r.Height - 2);
            }

            if (buttonDesigner.DrawCircle)
            {
                sB.Color = buttonDesigner.CircleColor;
                g.FillEllipse(sB, r.X + 7, r.Y + 7, r.Width - 14, r.Height - 14);
            }

            if (!buttonDesigner.DrawCircleBorder) { return; }

            p.Color = buttonDesigner.CircleBorderColor;
            g.DrawEllipse(p, r.X + 7, r.Y + 7, r.Width - 14, r.Height - 14);
        }

        protected override void OnCreateControl()
        {
            // TODO See if anything noticable happens if this is not called.
            // TODO May just keep it there anyways because it's best in practice.
            ////base.OnCreateControl();

            this.BackColor = Color.FromArgb(255, 75, 75, 75);
            this.DoubleBuffered = true;

            this.DockPadding.Bottom = 7;
            this.DockPadding.Left = 52;
            this.DockPadding.Right = 7;
            this.DockPadding.Top = 44;

            this.ForeColor = Color.FromArgb(255, 255, 255, 255);

            // TODO Check if 'FormBorderStyle' is set to None.  If not assign it to None.
            // TODO Assign Icon once Resources File has been created.

            // TODO This may not worrk accordingly.
            this.MinimumSize =
                new Size(
                    (int)this.CreateGraphics().MeasureString(this.Text = "OurForm", this._titleBarFont).Width + 116,
                    52);

            // TODO If any filckering occurs, set this in the Constructor.
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            // TODO Not sure if we should Invalidate here, just in the Constructor or both..
            this.Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            switch (this._mouseIsOver)
            {
                case OurBounds.Client: // TODO May need to handle 'Focus' manually.
                    break;
                case OurBounds.CloseButton:
                    Application.ExitThread();
                    break;
                case OurBounds.MaximizeButton:
                    this.WindowState = this.WindowState == FormWindowState.Maximized
                                           ? FormWindowState.Normal
                                           : FormWindowState.Maximized;
                    break;
                case OurBounds.MinimizeButton:
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case OurBounds.OffClient:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(this._mouseIsOver),
                                                          this._mouseIsOver,
                                                          "Something Fucked Up..");
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            switch (this._mouseIsOver)
            {
                case OurBounds.CloseButton:
                case OurBounds.MaximizeButton:
                case OurBounds.MinimizeButton:
                    this._mouseAction = MouseAction.Click;
                    this.Invalidate();
                    break;
                case OurBounds.Client:
                case OurBounds.OffClient:
                    this._mouseAction = MouseAction.None;
                    this.Invalidate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(this._mouseIsOver),
                                                          this._mouseIsOver,
                                                          "Something Fucked Up..");
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this._mouseIsOver = OurBounds.OffClient;
            this._mouseAction = MouseAction.None;

            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this._mouseAction == MouseAction.Click) { return; }
            if (this.ControlBoxContains(e.Location)) { this.Invalidate(); }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            switch (this._mouseAction)
            {
                case MouseAction.Click:
                case MouseAction.Hover:
                    this._mouseAction = MouseAction.None;
                    this.Invalidate();
                    break;
                case MouseAction.None:
                    this.Invalidate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(this._mouseAction),
                                                          this._mouseAction,
                                                          "Something Fucked Up..");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            var p = new Pen(this._borderTrimColor);
            var b = new SolidBrush(this.BorderColor);

            var clientRect = this.ClientRectangle;

            ////g.FillRectangle(b, clientRect);

            clientRect.Width--;
            clientRect.Height--;

            g.DrawRectangle(p, clientRect);

            clientRect.X += 6;
            clientRect.Y += 24;
            clientRect.Width -= 12;
            clientRect.Height -= 30;

            b.Color = this.BackColor;
            g.FillRectangle(b, clientRect);

            g.DrawRectangle(p, clientRect);

            b.Color = this.BorderColor;

            g.FillRectangle(b, 5, 5, 38, 38);

            this.PaintButtons(g, ref p, ref b);

            g.DrawIcon(this.Icon, 6, 6);

            b.Color = this.ForeColor;
            g.DrawString(this.Text, new Font("Consolas", 10F, FontStyle.Regular), b, 42, 7);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this._closeRect.X = this.Width - 24;
            this._maximizeRect.X = this.Width - 48;
            this._minimizeRect.X = this.Width - 72;

            this.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    var p = this.PointToClient(new Point(m.LParam.ToInt32()));

                    if (m.Result == (IntPtr)HTCLIENT
                        && !this.ControlBoxContains(p))
                    {
                        m.Result =
                            (IntPtr)
                            (this.Sizable
                                 ? p.X <= 6
                                       ? p.Y <= 6 ? HTTOPLEFT : p.Y >= this.Height - 7 ? HTBOTTOMLEFT : HTLEFT
                                       : p.X >= this.Width - 7
                                             ? p.Y <= 6 ? HTTOPRIGHT : p.Y >= this.Height - 7 ? HTBOTTOMRIGHT : HTRIGHT
                                             : p.Y <= 6
                                                   ? HTTOP
                                                   : p.Y >= this.Height - 7
                                                         ? HTBOTTOM
                                                         : p.Y <= 24 && this.Movable ? HTCAPTION : HTCLIENT
                                 : this.Movable && p.Y <= 24 ? HTCAPTION : HTCLIENT);
                    }

                    break;
            }
        }
        #endregion

        private enum MouseAction
        {
            Click,
            Hover,
            None
        }
    }
}