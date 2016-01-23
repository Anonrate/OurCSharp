// ----------------------------------------------------------------------------
// <copyright file="IOurFormButtonBase.cs" company="OurCSharp">
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

    using OurCSharp.OurForm.Core.Enums;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IOurFormButtonBase
    {
        #region Properties
        OurBounds ButtonBounds { get; }

        OurFormButtonStates State { get; set; }

        IOurFormButtonDesigner Normal { get; }

        IOurFormButtonDesigner Disabled { get; }

        IOurFormButtonDesigner Hovered { get; }

        IOurFormButtonDesigner Clicked { get; }
        #endregion
    }
}