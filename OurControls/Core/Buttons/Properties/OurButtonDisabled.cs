// ----------------------------------------------------------------------------
// <copyright file="OurButtonDisabled.cs" company="OurCSharp">
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

    using OurCSharp.OurControls.Core.Buttons.Abstracts;
    using OurCSharp.OurControls.Core.Buttons.Interfaces;

    internal class OurButtonDisabled : IOurButtonDesigner
    {
        #region Fields
        private readonly OurButtonsBase _ourControl;
        #endregion

        #region Properties
        [Description("Should we use the BorderColor given here when OurButton is in the corresponding state?")]
        public bool UseBorderColor { get; set; } = false;

        [Description("Should we use the Text given here when OurButton is in the corresponding state?")]
        public bool UseText { get; set; } = false;

        [Description("Should we use the TextColor given here when OurButton is in the corresponding state?")]
        public bool UseTextColor { get; set; } = true;

        [Description("The background color of OurButton.")]
        public Color BackColor { get; set; } = Color.FromArgb(255, 50, 50, 50);

        [Description("The color of the Border on OurButton.")]
        public Color BorderColor { get; set; } = Color.FromArgb(255, 25, 25, 25);

        [Description("The color of the Text on OurButton.")]
        public Color TextColor { get; set; } = Color.FromArgb(255, 100, 100, 100);

        [Description("The Text displayed on OurButton.")]
        public string Text { get; set; }
        #endregion

        #region Constructors
        public OurButtonDisabled(OurButtonsBase buttonBase) { this._ourControl = buttonBase; }
        #endregion

        ////public OurButtonDisabled(OurButton ourButton) { this._ourControl = ourButton; }

        // TODO Any other controls that will  be using this, add a cunstructor for.
    }
}