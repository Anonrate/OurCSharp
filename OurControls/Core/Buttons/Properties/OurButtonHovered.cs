// ----------------------------------------------------------------------------
// <copyright file="OurButtonHovered.cs" company="OurCSharp">
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

namespace OurCSharp.OurControls.Core.Buttons.Properties
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using OurCSharp.OurControls.Core.Buttons.Button;
    using OurCSharp.OurControls.Core.Buttons.Interfaces;

    internal class OurButtonHovered : IOurButtonDesigner
    {
        private readonly Control _ourControl;

        #region Properties
        [Description("Should we use the BorderColor given here when OurButton is in the corresponding state?")]
        public bool UseBorderColor { get; set; } = false;

        [Description("Should we use the Text given here when OurButton is in the corresponding state?")]
        public bool UseText { get; set; } = false;

        [Description("Should we use the TextColor given here when OurButton is in the corresponding state?")]
        public bool UseTextColor { get; set; } = false;

        [Description("The background color of OurButton.")]
        public Color BackColor { get; set; } = Color.FromArgb(255, 70, 70, 70);

        [Description("The color of the Border on OurButton.")]
        public Color BorderColor { get; set; } = Color.FromArgb(255, 25, 25, 25);

        [Description("The color of the Text on OurButton.")]
        public Color TextColor { get; set; } = Color.FromArgb(255, 150, 150, 150);

        [Description("The Text displayed on OurButton.")]
        public string Text { get; set; }
        #endregion

        #region Constructors

        // TODO Any other controls that will  be using this, add a cunstructor for.
        public OurButtonHovered(OurButton ourButton) { this._ourControl = ourButton; }
        #endregion
    }
}