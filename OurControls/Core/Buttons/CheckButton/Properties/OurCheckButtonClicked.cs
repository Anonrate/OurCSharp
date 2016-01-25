// ----------------------------------------------------------------------------
// <copyright file="OurCheckButtonClicked.cs" company="OurCSharp">
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

namespace OurCSharp.OurControls.Core.Buttons.CheckButton.Properties
{
    using System.ComponentModel;
    using System.Drawing;

    using OurCSharp.OurControls.Core.Buttons.CheckButton.Abstracts;
    using OurCSharp.OurControls.Core.Buttons.CheckButton.Interfaces;

    internal class OurCheckButtonClicked : IOurCheckButtonDesigner
    {
        #region Fields
        private readonly OurCheckButtonBase _checkButtonBase;

        private bool _useText;

        private string _text;
        #endregion

        #region Properties
        [Description("Override the BackColor and use it?")]
        public bool UseBackColor { get; set; } = false;

        [Description("Use a color for the border if one?")]
        public bool UseBorderColor { get; set; } = false;

        [Description("Use Color for the Check?")]
        public bool UseCheckColor { get; set; } = false;

        [Description("Use a border on the Check?")]
        public bool UseCheckBorder { get; set; } = true;

        [Description("Use the color on the border for the check?")]
        public bool UseCheckBorderColor { get; set; } = true;

        [Description("Use the BackColor for the CheckBox?")]
        public bool UseCheckBackColor { get; set; } = true;

        [DefaultValue(false)]
        [Description("Use this text?")]
        public bool UseText
        {
            get { return this._useText; }
            set
            {
                this._useText = value;
                if (!this._checkButtonBase.IsInDesignerMode) { return; }
                this._checkButtonBase.UpdateMinimumSize();
                this._checkButtonBase.UpdateSize();
            }
        }

        [Description("Use this Text Color?")]
        public bool UseTextColor { get; set; } = false;

        [Description("BackColor of the whole ChecBox.")]
        public Color BackColor { get; set; } = Color.FromArgb(255, 75, 75, 75);

        [Description("Color of the border.")]
        public Color BorderColor { get; set; } = Color.FromArgb(255, 25, 25, 25);

        [Description("Color of the check.")]
        public Color CheckColor { get; set; } = Color.Blue;

        [Description("Color of the border of the check.")]
        public Color CheckBorderColor { get; set; } = Color.FromArgb(255, 60, 60, 60);

        [Description("Color of the background where the check is.")]
        public Color CheckBackColor { get; set; } = Color.FromArgb(255, 60, 60, 60);

        [Description("Color of the text.")]
        public Color TextColor { get; set; } = Color.FromArgb(255, 150, 150, 150);

        [Description("The Text on the CheckButton.")]
        public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;

                if (!this._checkButtonBase.IsInDesignerMode) { return; }
                this._checkButtonBase.UpdateMinimumSize();
                this._checkButtonBase.UpdateSize();
            }
        }
        #endregion

        #region Constructors
        public OurCheckButtonClicked(OurCheckButtonBase checkButtonBase) { this._checkButtonBase = checkButtonBase; }
        #endregion
    }
}