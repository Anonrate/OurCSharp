// ----------------------------------------------------------------------------
// <copyright file="OurCheckButton.cs" company="OurCSharp">
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

namespace OurCSharp.OurControls.Core.Buttons.CheckButton
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    using OurCSharp.OurControls.Core.Buttons.CheckButton.Abstracts;
    using OurCSharp.OurControls.Core.Buttons.Enums;

    public class OurCheckButton : OurCheckButtonBase
    {
        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            var b = new SolidBrush(this.ForeColor);

            var r = this.Orientation == OurOrientation.Horizontal
                        ? new Rectangle(0, 0, this.Height - 1, this.Height - 1)
                        : new Rectangle(0, this.Height - this.Width - 1, this.Width - 1, this.Width - 1);

            g.DrawString(this.Text,
                         this.Font,
                         b,
                         this.Orientation == OurOrientation.Horizontal
                             ? new Rectangle(this.Height + 2, 0, this.Width - 1 - this.Height - 2, this.Height - 1)
                             : new Rectangle(0, 0, this.Width - 1, this.Height - 1 - this.Width - 2),
                         this.Orientation == OurOrientation.Horizontal
                             ? new StringFormat
                               {
                                   ////Alignment = StringAlignment.Center,
                                   LineAlignment = StringAlignment.Center
                               }
                             : new StringFormat(StringFormatFlags.DirectionVertical)
                               {
                                   Alignment = StringAlignment.Center,
                                   ////LineAlignment = StringAlignment.Center
                               });

            if (this.OurDesigner.UseCheckBackColor)
            {
                b.Color = this.OurDesigner.CheckBackColor;
                g.FillRectangle(b, r);
            }

            var p = new Pen(this.OurDesigner.UseBorderColor ? this.OurDesigner.BorderColor : this.Normal.BorderColor);

            g.DrawRectangle(p, r);

            if (!this.Checked) { return; }

            b.Color = this.OurDesigner.UseCheckColor ? this.OurDesigner.CheckColor : this.Normal.CheckColor;
            g.FillRectangle(b, r = new Rectangle(r.X + 2, r.Y + 2, r.Width - 4, r.Height - 4));

            if (!this.OurDesigner.UseCheckBorder
                && !this.Normal.UseCheckBorder) {
                    return;
                }

            p.Color = this.OurDesigner.UseCheckBorderColor ? this.OurDesigner.CheckBorderColor : this.Normal.CheckBorderColor;
            g.DrawRectangle(p, r);
        }
        #endregion
    }
}