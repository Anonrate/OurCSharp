// ----------------------------------------------------------------------------
// <copyright file="OurButton.cs" company="OurCSharp">
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

namespace OurCSharp.OurControls.Core.Buttons.Button
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    using OurCSharp.OurControls.Core.Buttons.Enums;
    using OurCSharp.OurControls.Core.Buttons.Interfaces;
    using OurCSharp.OurControls.Core.Buttons.Properties;

    public class OurButton : Control, IOurButtonBase
    {
        #region Fields
        private OurOrientation _orientation = OurOrientation.Horizontal;
        private IOurButtonDesigner _ourDesigner;
        #endregion

        #region Properties
        [DefaultValue(typeof(Padding), "6, 2, 6, 2")]
        public new Padding Padding { get { return base.Padding; } set { base.Padding = value; } }

        [Browsable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
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

        [Category("OurButton")]
        [DefaultValue(typeof(OurOrientation), "Horizontal")]
        [Description("The Orientation of OurButton.")]
        public OurOrientation Orientation
        {
            get { return this._orientation; }
            set
            {
                this._orientation = value;
                this.Invalidate();
            }
        }

        [Category("OurButton")]
        public IOurButtonDesigner Normal { get; }

        [Category("OurButton")]
        public IOurButtonDesigner Hovered { get; }

        [Category("OurButton")]
        public IOurButtonDesigner Clicked { get; }

        [Category("OurButton")]
        public IOurButtonDesigner Disabled { get; }
        #endregion

        #region Constructors
        public OurButton()
        {
            base.DoubleBuffered = true;

            this.Padding = new Padding(6, 2, 6, 2);

            /*
             * I assigned it this way, because it just makes sense that the far left is more 'Dependable' even though
             * modifying the one beside it on the right wont change it after being assigned....  Having mixed thoughts
             * about this actually because it refering to another class.  Nonetheless it should still work as
             * inteded despite the way it's assigned...
             */
            this._ourDesigner = this.Normal = new OurButtonNormal(this);

            this.Hovered = new OurButtonHovered(this);
            this.Clicked = new OurButtonClicked(this);
            this.Disabled = new OurButtonDisabled(this);
        }
        #endregion

        #region Methods
        protected override void OnCreateControl()
        {
            this.BackColor = this.Normal.BackColor;
            this.ForeColor = this.Normal.TextColor;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = this.Enabled
                                 ? (this._ourDesigner = this.Enabled ? this.Hovered : this.Disabled).BackColor
                                 : this.Disabled.BackColor;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = this.Enabled
                                 ? (this._ourDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor
                                 : this.Disabled.BackColor;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.BackColor = this.Enabled
                                 ? (this._ourDesigner = this.Enabled ? this.Clicked : this.Disabled).BackColor
                                 : this.Disabled.BackColor;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.BackColor = this.Enabled
                                 ? (this._ourDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor
                                 : this.Disabled.BackColor;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            this.ForeColor = this._ourDesigner.UseTextColor ? this._ourDesigner.TextColor : this.Normal.TextColor;

            var p = new Pen(this._ourDesigner.UseBorderColor ? this._ourDesigner.BorderColor : this.Normal.BorderColor);

            if (this._ourDesigner.UseBorderColor
                || this.Normal.UseBorderColor)
            {
                var clientRect = this.ClientRectangle;

                clientRect.Width--;
                clientRect.Height--;

                g.DrawRectangle(p, clientRect);
            }

            var b = new SolidBrush(this._ourDesigner.UseTextColor ? this._ourDesigner.TextColor : this.Normal.TextColor);

            SizeF textSizeF;

            switch (this.Orientation)
            {
                case OurOrientation.Horizontal:
                    g.DrawString((this.Text = this._ourDesigner.UseText ? this._ourDesigner.Text : this.Normal.Text),
                                 this.Font,
                                 b,
                                 (textSizeF = this.CreateGraphics().MeasureString(this.Text, this.Font)).Width / 2F
                                 + this.Padding.Left + this.Padding.Right,
                                 this.Padding.Top);
                    break;
                case OurOrientation.Verticle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}