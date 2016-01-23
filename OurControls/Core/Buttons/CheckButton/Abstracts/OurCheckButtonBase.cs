﻿// ----------------------------------------------------------------------------
// <copyright file="OurCheckButtonBase.cs" company="OurCSharp">
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

namespace OurCSharp.OurControls.Core.Buttons.CheckButton.Abstracts
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using OurCSharp.OurControls.Core.Buttons.CheckButton.Interfaces;
    using OurCSharp.OurControls.Core.Buttons.CheckButton.Properties;
    using OurCSharp.OurControls.Core.Buttons.Enums;

    public abstract class OurCheckButtonBase : Control
    {
        #region Fields
        private bool _checked;

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
                base.MinimumSize = new Size(
                    value.Width <= base.MinimumSize.Width ? base.MinimumSize.Width : value.Width,
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

        [Category("OurCheckButton")]
        [DefaultValue(false)]
        [Description("Is OurCheckButton Checked?")]
        public bool Checked
        {
            get { return this._checked; }
            set
            {
                this._checked = value;
                this.Invalidate();
            }
        }

        [Category("OurCheckButton")]
        public IOurCheckButtonDesigner Clicked { get; }

        [Category("OurCheckButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurCheckButtonDesigner Disabled { get; }

        [Category("OurCheckButton")]
        public IOurCheckButtonDesigner Hovered { get; }

        public bool IsInDesignerMode => this.DesignMode;

        [Category("OurCheckButton")]
        public IOurCheckButtonDesigner Normal { get; }

        [DefaultValue(typeof(OurOrientation), "Horizontal")]
        [Description("The Orientation of OurCheckButton.")]
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

        [DefaultValue(typeof(Padding), "6, 2, 6, 2")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Needs to be visable in Designer.")]
        public new Padding Padding { get { return base.Padding; } set { base.Padding = value; } }

        protected IOurCheckButtonDesigner OurDesigner { get; private set; }
        #endregion

        #region Constructors
        protected OurCheckButtonBase()
        {
            base.DoubleBuffered = true;

            this.Padding = new Padding(6, 2, 6, 2);

            this.OurDesigner = this.Normal = new OurCheckButtonNormal(this);

            this.Hovered = new OurCheckButtonHovered(this);
            this.Clicked = new OurCheckButtonClicked(this);
            this.Disabled = new OurCheckButtonDisabled(this);

            this.SizeChanged += (sender, args) => this.Invalidate();
        }
        #endregion

        #region Methods
        public void UpdateMinimumSize() { this.MinimumSize = this.UpdateAndGetMinimumSize(); }

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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            this.Checked = this.Enabled ? !this.Checked : this.Checked;

            if (this.Enabled) { this.Invalidate(); }
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
                    ? new SizeF(minSizeF.Width + minSizeF.Height + 3, minSizeF.Height).ToSize()
                    : new SizeF(minSizeF.Height + minSizeF.Width + 3, minSizeF.Width).ToSize();
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