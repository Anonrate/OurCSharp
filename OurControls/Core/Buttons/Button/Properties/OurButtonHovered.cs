﻿// ----------------------------------------------------------------------------
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

namespace OurCSharp.OurControls.Core.Buttons.Button.Properties
{
    using System.ComponentModel;
    using System.Drawing;

    using OurCSharp.OurControls.Core.Buttons.Button.Abstracts;
    using OurCSharp.OurControls.Core.Buttons.Button.Interfaces;

    internal class OurButtonHovered : IOurButtonDesigner
    {
        #region Fields
        private readonly OurButtonBase _buttonBase;

        private string _text;

        private bool _useText;
        #endregion

        #region Properties
        [Description("The background color of OurButton.")]
        public Color BackColor { get; set; } = Color.FromArgb(255, 70, 70, 70);

        [Description("The color of the Border on OurButton.")]
        public Color BorderColor { get; set; } = Color.FromArgb(255, 25, 25, 25);

        [Description("The Text displayed on OurButton.")]
        public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;
                if (this._buttonBase.IsInDesignerMode) { this._buttonBase.UpdateMinimumSize(); }
            }
        }

        [Description("The color of the Text on OurButton.")]
        public Color TextColor { get; set; } = Color.FromArgb(255, 150, 150, 150);

        [Description("Should we use the BorderColor given here when OurButton is in the corresponding state?")]
        public bool UseBorderColor { get; set; } = false;

        [DefaultValue(false)]
        [Description("Should we use the Text given here when OurButton is in the corresponding state?")]
        public bool UseText
        {
            get { return this._useText; }
            set
            {
                this._useText = value;
                if (this._buttonBase.IsInDesignerMode) { this._buttonBase.UpdateMinimumSize(); }
            }
        }

        [Description("Should we use the TextColor given here when OurButton is in the corresponding state?")]
        public bool UseTextColor { get; set; } = false;
        #endregion

        #region Constructors
        public OurButtonHovered(OurButtonBase buttonBase) { this._buttonBase = buttonBase; }
        #endregion
    }
}