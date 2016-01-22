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
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
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
                this.Size = this.UpdateAndGetMinimumSize();
                this.Invalidate();
            }
        }

        [Category("OurButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurButtonDesigner Normal { get; }

        [Category("OurButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurButtonDesigner Hovered { get; }

        [Category("OurButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurButtonDesigner Clicked { get; }

        [Category("OurButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurButtonDesigner Disabled { get; }
        #endregion

        public bool IsInDesignerMode => this.DesignMode;

        // TODO May have problems with this...
        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set
            {
                base.MinimumSize = new Size(
                    value.Width <= base.MinimumSize.Width ? base.MinimumSize.Width : value.Width,
                    value.Height <= base.MinimumSize.Height ? base.MinimumSize.Height : value.Height);
            }
        }

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

            this.SizeChanged += (sender, args) => this.Invalidate();
        }
        #endregion

        public void UpdateMinimumSize() { this.MinimumSize = this.UpdateAndGetMinimumSize(); }


        private Size UpdateAndGetMinimumSize()
        {
            var minSizeF = SizeF.Empty;

            foreach (var useText in new[]
                                    {
                                        new Tuple<bool, string>(this.Normal.UseText, this.Normal.Text),
                                        new Tuple<bool, string>(this.Hovered.UseText, this.Hovered.Text),
                                        new Tuple<bool, string>(this.Clicked.UseText, this.Clicked.Text),
                                        new Tuple<bool, string>(this.Disabled.UseText, this.Disabled.Text)

                                    }.Where(useText => useText.Item1))
            {
                SizeF sizeF;

                minSizeF = (sizeF = this.CreateGraphics().MeasureString(useText.Item2, this.Font)).Width * sizeF.Height
                           > minSizeF.Width * minSizeF.Height
                               ? sizeF
                               : minSizeF;
            }

            return
                base.MinimumSize =
                this._orientation == OurOrientation.Horizontal
                    ? new SizeF(minSizeF.Width + this.Padding.Left + this.Padding.Top,
                                minSizeF.Height + this.Padding.Bottom + this.Padding.Top).ToSize()
                    : new SizeF(minSizeF.Height + this.Padding.Bottom + this.Padding.Top,
                                minSizeF.Width + this.Padding.Left + this.Padding.Right).ToSize();
        }

        #region Methods
        protected override void OnCreateControl()
        {
            this.Text = this.Normal.Text = this.Hovered.Text = this.Clicked.Text = this.Disabled.Text = this.Name;

            // TODO If doesn't work, might have to use 'base'..
            this.Size = this.MinimumSize = this.UpdateAndGetMinimumSize();

            this.BackColor = this._ourDesigner.BackColor;
            this.ForeColor = this._ourDesigner.TextColor;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            this.BackColor = (this._ourDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor;

            this.UpdateColor();
        }

        private void UpdateColor()
        {
            this.ForeColor = this._ourDesigner.UseTextColor ? this._ourDesigner.TextColor : this.Normal.TextColor;
            this.Text = this._ourDesigner.UseText ? this._ourDesigner.Text : this.Normal.Text;
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            this.BackColor = (this._ourDesigner = this.Enabled ? this.Hovered : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.BackColor = (this._ourDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.BackColor = (this._ourDesigner = this.Enabled ? this.Clicked : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.BackColor = (this._ourDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (this._ourDesigner.UseBorderColor
                || this.Normal.UseBorderColor)
            {
                var p =
                    new Pen(this._ourDesigner.UseBorderColor ? this._ourDesigner.BorderColor : this.Normal.BorderColor);

                var clientRect = this.ClientRectangle;

                clientRect.Width--;
                clientRect.Height--;

                g.DrawRectangle(p, clientRect);
            }

            var b = new SolidBrush(this.ForeColor);

            SizeF textSizeF = this.CreateGraphics().MeasureString(this.Text, this.Font);

            switch (this.Orientation)
            {
                case OurOrientation.Horizontal:
                    Debug.WriteLine(this.Text);

                    g.DrawString(this.Text,
                                 this.Font,
                                 b,
                                 this.Width / 2
                                 - (textSizeF = this.CreateGraphics().MeasureString(this.Text, this.Font)).Width / 2 + 1,
                                 this.Height / 2 - textSizeF.Height / 2);
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