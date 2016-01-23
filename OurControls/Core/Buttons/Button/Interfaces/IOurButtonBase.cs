// ----------------------------------------------------------------------------
// <copyright file="IOurButtonBase.cs" company="OurCSharp">
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

namespace OurCSharp.OurControls.Core.Buttons.Button.Interfaces
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    using OurCSharp.OurControls.Core.Buttons.Enums;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public interface IOurButtonBase
    {
        #region Properties
        bool IsInDesignerMode { get; }

        OurOrientation Orientation { get; set; }

        IOurButtonDesigner Normal { get; }

        IOurButtonDesigner Hovered { get; }

        IOurButtonDesigner Clicked { get; }

        IOurButtonDesigner Disabled { get; }
        #endregion

        #region Methods
        void UpdateMinimumSize();
        #endregion
    }
}