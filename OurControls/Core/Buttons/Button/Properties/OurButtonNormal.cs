// ----------------------------------------------------------------------------
// <copyright file="OurButtonNormal.cs" company="OurCSharp">
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

    internal class OurButtonNormal : IOurButtonDesigner
    {
        #region Fields
        private readonly OurButtonBase _buttonBase;

        private Color _backColor = Color.FromArgb(255, 65, 65, 65);
        private Color _borderColor = Color.FromArgb(255, 25, 25, 25);

        private string _text;
        private Color _textColor = Color.FromArgb(255, 150, 150, 150);

        private bool _useBorderColor = true;
        #endregion

        #region Properties
        [DefaultValue(typeof(Color), "255, 65, 65, 65")]
        [Description("The background color of OurButton.")]
        public Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;

                if (!this._buttonBase.Enabled) { return; }
                this._buttonBase.BackColor = value;
            }
        }

        [DefaultValue(typeof(Color), "255, 25, 25, 25")]
        [Description("The color of the Border on OurButton.")]
        public Color BorderColor
        {
            get { return this._borderColor; }
            set
            {
                this._borderColor = value;
                this._buttonBase.Invalidate();
            }
        }

        [Description("The Text displayed on OurButton.")]
        public string Text
        {
            get { return this._text; }
            set
            {
                this._buttonBase.Text = this._text = value;
                if (this._buttonBase.IsInDesignerMode) { this._buttonBase.UpdateMinimumSize(); }
            }
        }

        [DefaultValue(typeof(Color), "255, 150, 150, 150")]
        [Description("The color of the Text on OurButton.")]
        public Color TextColor
        {
            get { return this._textColor; }
            set { this._buttonBase.ForeColor = this._textColor = value; }
        }

        [DefaultValue(true)]
        [Description("Should we use the BorderColor given here when OurButton is in the corresponding state?")]
        public bool UseBorderColor
        {
            get { return this._useBorderColor; }
            set
            {
                this._useBorderColor = value;
                this._buttonBase.Invalidate();
            }
        }

        [Browsable(false)]
        [DefaultValue(true)]
        [Description("Should we use the Text given here when OurButton is in the corresponding state?")]
        public bool UseText { get { return true; } set { } }

        [Browsable(false)]
        [DefaultValue(true)]
        [Description("Should we use the TextColor given here when OurButton is in the corresponding state?")]
        public bool UseTextColor { get { return true; } set { } }
        #endregion

        #region Constructors
        public OurButtonNormal(OurButtonBase buttonBase) { this._buttonBase = buttonBase; }
        #endregion
    }
}