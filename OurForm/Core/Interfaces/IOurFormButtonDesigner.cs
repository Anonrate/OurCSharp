// ----------------------------------------------------------------------------
// <copyright file="IOurFormButtonDesigner.cs" company="OurCSharp">
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

namespace OurCSharp.OurForm.Core.Interfaces
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IOurFormButtonDesigner
    {
        #region Properties
        bool DrawCircle { get; set; }

        bool DrawCircleBorder { get; set; }

        bool DrawBox { get; set; }

        bool DrawBoxBorder { get; set; }

        Color CircleColor { get; set; }

        Color CircleBorderColor { get; set; }

        Color BoxColor { get; set; }

        Color BoxBorderColor { get; set; }
        #endregion
    }
}