// ----------------------------------------------------------------------------
// <copyright file="IOurCheckButtonDesigner.cs" company="OurCSharp">
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

namespace OurCSharp.OurControls.Core.Buttons.CheckButton.Interfaces
{
    using System.ComponentModel;
    using System.Drawing;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IOurCheckButtonDesigner
    {
        #region Properties
        bool UseBackColor { get; set; }

        bool UseBorderColor { get; set; }

        bool UseCheckColor { get; set; }

        bool UseCheckBorder { get; set; }

        bool UseCheckBorderColor { get; set; }

        bool UseCheckBackColor { get; set; }

        bool UseText { get; set; }

        bool UseTextColor { get; set; }

        Color BackColor { get; set; }

        Color BorderColor { get; set; }

        Color CheckColor { get; set; }

        Color CheckBorderColor { get; set; }

        Color CheckBackColor { get; set; }

        Color TextColor { get; set; }

        string Text { get; set; }
        #endregion
    }
}