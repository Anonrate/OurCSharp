// ----------------------------------------------------------------------------
// <copyright file="OurButtonR1.cs" company="OurCSharp">
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
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    using OurCSharp.OurControls.Core.Buttons.Abstracts;
    using OurCSharp.OurControls.Core.Buttons.Enums;

    public class OurButtonR1 : OurButtonsBase
    {
        #region Methods
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