// ----------------------------------------------------------------------------
// <copyright file="OurMaximizeButtonClicked.cs" company="OurCSharp">
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

namespace OurCSharp.OurForm.Core.Properties.MaximizeButton.SubProperties
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    using OurCSharp.OurForm.Core.Interfaces;
    using OurCSharp.OurForm.Properties;
    using OurCSharp.OurUtils.Extensions;

    internal class OurMaximizeButtonClicked : IOurFormButtonDesigner
    {
        #region Fields
        private readonly OurForm _ourForm;

        private bool _drawCircle = Settings.Default.MxmzBttnClckdDrwCrcl;
        private bool _drawCircleBorder = Settings.Default.MxmzBttnClckdDrwCrclBrdr;
        private bool _drawBox = Settings.Default.MxmzBttnClckdDrwBx;
        private bool _drawBoxBorder = Settings.Default.MxmzBttnClckdDrwBx;

        private Color _circleColor = Settings.Default.MxmzBttnClckdBxClr;
        private Color _circleBorderColor = Settings.Default.MxmzBttnClckdCrclBrdrClr;
        private Color _boxColor = Settings.Default.MxmzBttnClckdBxClr;
        private Color _boxBorderColor = Settings.Default.MxmzBttnClckdBxBrdrClr;
        #endregion

        #region Properties
        [DefaultValue(true)]
        [Description("Draw the Circle on OurForm?")]
        public bool DrawCircle
        {
            get { return this._drawCircle; }
            set
            {
                this._drawCircle = value;
                this._ourForm.Invalidate();
            }
        }

        [DefaultValue(true)]
        [Description("Draw the Circle Border on OurForm?")]
        public bool DrawCircleBorder
        {
            get { return this._drawCircleBorder; }
            set
            {
                this._drawCircleBorder = value;
                this._ourForm.Invalidate();
            }
        }

        [DefaultValue(true)]
        [Description("Draw the Box on OurForm?")]
        public bool DrawBox
        {
            get { return this._drawBox; }
            set
            {
                this._drawBox = value;
                this._ourForm.Invalidate();
            }
        }

        [DefaultValue(true)]
        [Description("Draw the Box Border on OurForm?")]
        public bool DrawBoxBorder
        {
            get { return this._drawBoxBorder; }
            set
            {
                this._drawBoxBorder = value;
                this._ourForm.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "255, 100, 100, 100")]
        [Description("Color of the Circle on OurForm.")]
        public Color CircleColor
        {
            get { return this._circleColor; }
            set
            {
                this._circleColor = value;
                this._ourForm.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "255, 75, 75, 75")]
        [Description("Color of the Border on the Circle of OurForm.")]
        public Color CircleBorderColor
        {
            get { return this._circleBorderColor; }
            set
            {
                this._circleBorderColor = value;
                this._ourForm.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "255, 100, 100, 100")]
        [Description("Color of the Box on OurForm.")]
        public Color BoxColor
        {
            get { return this._boxColor; }
            set
            {
                this._boxColor = value;
                this._ourForm.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "255, 75, 75, 75")]
        [Description("Color of the Border on the Box of OurForm.")]
        public Color BoxBorderColor
        {
            get { return this._boxBorderColor; }
            set
            {
                this._boxBorderColor = value;
                this._ourForm.Invalidate();
            }
        }
        #endregion

        #region Constructors
        public OurMaximizeButtonClicked(OurForm ourForm) { this._ourForm = ourForm; }
        #endregion

        #region Methods
        [SuppressMessage("ReSharper", "UseStringInterpolation")]
        public override string ToString()
            =>
                string.Format(
                              "DrawCircle({0}), DrawCircleBorder({1}), DrawBox({2}), DrawBoxBorder({3}),"
                              + " CircleColor({4}), CircleBorderColor({5}), BoxColor({6}),  BoxBorderColor({7})",
                              this.DrawCircle,
                              this.DrawCircleBorder,
                              this.DrawBox,
                              this.DrawBoxBorder,
                              this.CircleColor.ToArgbString(),
                              this.CircleBorderColor.ToArgbString(),
                              this.BoxColor.ToArgbString(),
                              this.BoxBorderColor.ToArgbString());
        #endregion
    }
}