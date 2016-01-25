// ----------------------------------------------------------------------------
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
    using System.Diagnostics;
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

        private IOurCheckButtonDesigner _previousDesigner;
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
                ////if (this.Width >= this.MinimumSize.Width
                ////    && this.Height >= this.MinimumSize.Height) { return; }
                ////this.Width = this.MinimumSize.Width > this.Width ? this.MinimumSize.Width : this.Width;
                ////this.Height = this.MinimumSize.Height > this.Height ? this.MinimumSize.Height : this.Height;

                ////this.Invalidate();
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

        public bool IsInDesignerMode => this.DesignMode;

        [DefaultValue(typeof(OurOrientation), "Horizontal")]
        [Description("The Orientation of OurCheckButton.")]
        public OurOrientation Orientation
        {
            get { return this._orientation; }
            set
            {
                this._orientation = value;
                base.MinimumSize = new Size(1, 1);
                this.Size = base.MinimumSize = this.MeassureStringSize(this.Text);
                this.UpdateSize();
                this.Invalidate();
            }
        }

        [Category("OurCheckButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurCheckButtonDesigner Normal { get; }

        [Category("OurCheckButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurCheckButtonDesigner Hovered { get; }

        [Category("OurCheckButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurCheckButtonDesigner Clicked { get; }

        [Category("OurCheckButton")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurCheckButtonDesigner Disabled { get; }

        public new Size Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;

                Debug.WriteLine(value);
                this.UpdateMinimumSize();
            }
        }

        protected IOurCheckButtonDesigner OurDesigner { get; private set; }
        #endregion

        #region Constructors
        protected OurCheckButtonBase()
        {
            base.DoubleBuffered = true;

            this.Padding = new Padding(6, 2, 6, 2);

            this._previousDesigner = this.OurDesigner = this.Normal = new OurCheckButtonNormal(this);

            this.Hovered = new OurCheckButtonHovered(this);
            this.Clicked = new OurCheckButtonClicked(this);
            this.Disabled = new OurCheckButtonDisabled(this);
            base.Text = this.Normal.Text = "ourCheckButton1";
            base.MinimumSize = new Size(1, 1);
        }
        #endregion

        #region Methods
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.UpdateMinimumSize();
        }

        protected override void OnCreateControl()
        {
            ////base.OnCreateControl();

            if (this.IsInDesignerMode) {
                this.Size = base.MinimumSize = this.MeassureStringSize(this.Text = this.Normal.Text = this.Name);
            }

            this.BackColor = this.OurDesigner.BackColor;
            this.ForeColor = this.OurDesigner.TextColor;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (this.Enabled)
            {
                this.BackColor = (this.OurDesigner = this._previousDesigner).UseBackColor
                                     ? this.OurDesigner.BackColor
                                     : this.Normal.BackColor;

                this.UpdateColor();
                return;
            }

            this._previousDesigner = this.OurDesigner;
            this.BackColor = (this.OurDesigner = this.Disabled).BackColor;
            this.UpdateColor();

            ////this.BackColor = !this.Enabled = (this._previousDesigner = this.OurDesigner)

            /* Doing it this way may not keep the state it was at previous when enabled. */
            ////this.BackColor = (this.OurDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor;

            ////this.BackColor = this.Enabled
            ////                 ? this.OurDesigner.UseBackColor ? this.OurDesigner.BackColor : this.Normal.BackColor
            ////             : this.Disabled.BackColor;

            ////this.UpdateColor();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (!this.Enabled
                || e.Button != MouseButtons.Left) {
                    return;
                }
            this.Checked = !this.Checked;

            /* Calling the update color rather than invalidate because the text should onlt change if the CheckButton is
             * checked, instead of every time you left click it.
             */
            this.UpdateColor();

            ////this.Invalidate();

            ////this.Checked = this.Enabled && e.Button == MouseButtons.Left ? !this.Checked : this.Checked;

            ////if (this.Enabled) { this.Invalidate(); }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!this.Enabled
                || e.Button != MouseButtons.Left) {
                    return;
                }

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

            /*
             * This makes sure that if the CheckButton is enabled and checked that it preserves its BackColor even when at its
             * normal state hense why we are in the 'OnMouseLeave', although maybe we want it to change and everything to change
             * except the text that way we still get that visual affect...  Hard to determine..
             *
             * I think for now we should actually just preserve the text setting, that way if the other values are changed, we
             * will still know what state we are at.
             */

            /* Only use this if you want to preserve everything that of when in the checked state when leaving the control.
             * If anyone wants this to be implemnted for each other state and don't know how to, just notify me and I will do so.
            this.BackColor =
                (this.OurDesigner =
                 this.Checked && this.Enabled
                     ? this.Clicked.UseBackColor ? this.Clicked : this.Normal
                     : this.Enabled ? this.Normal : this.Disabled).BackColor;
             */

            this.BackColor = (this.OurDesigner = this.Enabled ? this.Normal : this.Disabled).BackColor;

            this.UpdateColor();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.BackColor =
                (this.OurDesigner = this.Enabled ? e.Button == MouseButtons.Left ? this.Normal : this.OurDesigner : this.Disabled).
                    BackColor;

            this.UpdateColor();
        }

        public void UpdateSize() { base.Size = this.MinimumSize; }

        public void UpdateMinimumSize() { this.MinimumSize = this.UpdateAndGetMinimumSize(); }

        private Size MeassureStringSize(string str)
        {
            var size = this.CreateGraphics().MeasureString(str, this.Font).ToSize();

            return this.Orientation == OurOrientation.Horizontal ? size : new Size(size.Height, size.Width);
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

                ////minSizeF =
                ////    (sizeF =
                ////     this.CreateGraphics().
                ////          MeasureString(useText.Item2,
                ////                        this.Font,
                ////                        this.Size,
                ////                        this.Orientation == OurOrientation.Horizontal
                ////                            ? new StringFormat
                ////                              {
                ////                                  LineAlignment = StringAlignment.Center
                ////                              }
                ////                            : new StringFormat(StringFormatFlags.DirectionVertical)
                ////                              {
                ////                                  Alignment = StringAlignment.Center
                ////                              })).Width * sizeF.Height > minSizeF.Width * minSizeF.Height
                ////        ? sizeF
                ////        : minSizeF;

                minSizeF = (sizeF = this.CreateGraphics().MeasureString(useText.Item2, this.Font)).Width * sizeF.Height
                           > minSizeF.Width * minSizeF.Height
                               ? sizeF
                               : minSizeF;

                ////               Log(minSizeF.ToSize());
            }

            Size size = this._orientation == OurOrientation.Horizontal
                            ? new SizeF(minSizeF.Width + (this.Height <= minSizeF.Height ? minSizeF.Height : this.Height) + 4,
                                        minSizeF.Height).ToSize()
                            : new SizeF(minSizeF.Height,
                                        minSizeF.Height + (this.Width <= minSizeF.Width ? minSizeF.Width : this.Width) + 4).ToSize
                                  ();

            return base.MinimumSize = size;

            ////return
            ////    base.MinimumSize =
            ////    this._orientation == OurOrientation.Horizontal
            ////        ? new SizeF(minSizeF.Width + (this.Height <= minSizeF.Height ? minSizeF.Height : this.Height) + 4,
            ////                    minSizeF.Height).ToSize()
            ////        : new SizeF(minSizeF.Width,
            ////                    minSizeF.Height + minSizeF.Width
            ////                    + (this.Height <= minSizeF.Height ? minSizeF.Height : this.Height) + 4).ToSize();
        }

        private void UpdateColor()
        {
            /* This will preserve the text color when checked despite what state this CheckButton is currently at, that way it
             * can give the user more assuarnace than just the check to be able to tell if it's checked or not.
             */
            ////this.ForeColor = this.Checked && this.Enabled
            ////                 ? this.Clicked.UseTextColor ? this.Clicked.TextColor : this.Normal.TextColor
            ////             : this.OurDesigner.UseTextColor ? this.OurDesigner.TextColor : this.Normal.TextColor;

            this.ForeColor = this.Checked && this.Enabled
                                 ? this.Clicked.UseTextColor ? this.Clicked.TextColor : this.Normal.TextColor
                                 : this.OurDesigner.UseTextColor
                                       ? this.OurDesigner.Text == this.Clicked.Text
                                             ? this.Hovered.UseTextColor ? this.Hovered.TextColor : this.Normal.TextColor
                                             : this.OurDesigner.TextColor
                                       : this.Normal.TextColor;

            ////this.ForeColor = this.OurDesigner.UseTextColor ? this.OurDesigner.TextColor : this.Normal.TextColor;

            /* This will preserve the text when checked despite what state this CheckButton is currently at, that way it can give
             * the user more assuarnace than just the check to be able to tell if it's checked or not.
             */
            this.Text = this.Checked && this.Enabled
                            ? this.Clicked.UseText ? this.Clicked.Text : this.Normal.Text
                            : this.OurDesigner.UseText
                                  ? this.OurDesigner.Text == this.Clicked.Text
                                        ? this.Hovered.UseText ? this.Hovered.Text : this.Normal.Text
                                        : this.OurDesigner.Text
                                  : this.Normal.Text;

            ////this.Text = this.Checked && this.Enabled ? this.Clicked.UseText ? this.Clicked.Text : this.Normal.Text : this.OurDesigner.Text : this.Normal.Text;
            this.Invalidate();
        }
        #endregion
    }
}