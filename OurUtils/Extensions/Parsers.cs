// ----------------------------------------------------------------------------
// <copyright file="Parsers.cs" company="OurCSharp">
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

namespace OurCSharp.OurUtils.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    [SuppressMessage("ReSharper", "UseStringInterpolation")]
    public static class Parsers
    {
        #region Methods
        public static string ToArgbString(this Color color)
            => string.Format("{0}, {1}, {2}, {3}", color.A, color.R, color.G, color.B);
        #endregion
    }
}