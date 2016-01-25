// ----------------------------------------------------------------------------
// <copyright file="OurButtonBase.cs" company="OurCSharp">
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

namespace OurCSharp.OurControls.Core.Buttons.Button.Abstracts
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using OurCSharp.OurControls.Core.Buttons.Button.Interfaces;
    using OurCSharp.OurControls.Core.Buttons.Button.Properties;
    using OurCSharp.OurControls.Core.Buttons.Enums;

    public abstract class OurButtonBase : Control
    {
        #region Fields
        private OurOrientation _orientation = OurOrientation.Horizontal;
        #endregion

        #region Properties
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

        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set
            {
                base.MinimumSize = new Size(value.Width <= base.MinimumSize.Width ? base.MinimumSize.Width : value.Width,
                                            value.Height <= base.MinimumSize.Height ? base.MinimumSize.Height : value.Height);
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

        [DefaultValue(typeof(Padding), "6, 2, 6, 2")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Needs to be visable in Designer.")]
        public new Padding Padding { get { return base.Padding; } set { base.Padding = value; } }

        public bool IsInDesignerMode => this.DesignMode;

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

        protected IOurButtonDesigner OurDesigner { get; private set; }
        #endregion

        #region Constructors
        protected OurButtonBase()
        {
            base.DoubleBuffered = true;

            this.Padding = new Padding(6, 2, 6, 2);

            this.OurDesigner = this.Normal = new OurButtonNormal(this);

            this.Hovered = new OurButtonHovered(this);
            this.Clicked = new OurButtonClicked(this);
            this.Disabled = new OurButtonDisabled(this);

            this.SizeChanged += (sender, args) => this.Invalidate();
        }
        #endregion

        #region Methods
        protected override void OnCreateControl()
        {
            this.Text = this.Normal.Text = this.Hovered.Text = this.Clicked.Text = this.Disabled.Text = this.Name;

            // TODO If doesn't work, might have to use 'base'..
            this.MinimumSize = this.UpdateAndGetMinimumSize();

            this.BackColor = this.OurDesigner.BackColor;
            this.ForeColor = this.OurDesigner.TextColor;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            this.BackColor = (this.OurDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.BackColor = (this.OurDesigner = this.Enabled ? this.Clicked : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            this.BackColor = (this.OurDesigner = this.Enabled ? this.Hovered : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.BackColor = (this.OurDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.BackColor = (this.OurDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor;

            this.UpdateColor();
        }

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

        private void UpdateColor()
        {
            this.ForeColor = this.OurDesigner.UseTextColor ? this.OurDesigner.TextColor : this.Normal.TextColor;
            this.Text = this.OurDesigner.UseText ? this.OurDesigner.Text : this.Normal.Text;
            this.Invalidate();
        }
        #endregion
    }
}