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
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    using OurCSharp.OurControls.Core.Buttons.Button.Abstracts;
    using OurCSharp.OurControls.Core.Buttons.Enums;

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class OurButton : OurButtonBase
    {
        #region Methods
        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (this.OurDesigner.UseBorderColor
                || this.Normal.UseBorderColor)
            {
                var p = new Pen(this.OurDesigner.UseBorderColor ? this.OurDesigner.BorderColor : this.Normal.BorderColor);

                var clientRect = this.ClientRectangle;

                clientRect.Width--;
                clientRect.Height--;

                g.DrawRectangle(p, clientRect);
            }

            var b = new SolidBrush(this.ForeColor);

            g.DrawString(this.Text,
                         this.Font,
                         b,
                         this.DisplayRectangle,
                         this.Orientation == OurOrientation.Horizontal
                             ? new StringFormat
                               {
                                   Alignment = StringAlignment.Center
                               }
                             : new StringFormat(StringFormatFlags.DirectionVertical
                                                | StringFormatFlags.DirectionRightToLeft)
                               {
                                   LineAlignment = StringAlignment.Center
                               });

            ////switch (this.Orientation)
            ////{
            ////    case OurOrientation.Horizontal:

            ////        g.DrawString(this.Text,
            ////                     this.Font,
            ////                     b,
            ////                     this.ClientRectangle,
            ////                     new StringFormat
            ////                     {
            ////                         Alignment = StringAlignment.Center
            ////                     });

            ////        break;
            ////    case OurOrientation.Verticle:
            ////        g.DrawString(this.Text,
            ////                     this.Font,
            ////                     b,
            ////                     this.ClientRectangle,
            ////                     new StringFormat(StringFormatFlags.DirectionVertical)
            ////                     {
            ////                         LineAlignment = StringAlignment.Center
            ////                     });
            ////        break;
            ////    default:
            ////        throw new ArgumentOutOfRangeException();
            ////}
        }
        #endregion
    }
}