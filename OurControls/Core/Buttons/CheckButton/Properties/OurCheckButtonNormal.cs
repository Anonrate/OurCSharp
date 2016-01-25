// ----------------------------------------------------------------------------
// <copyright file="OurCheckButtonNormal.cs" company="OurCSharp">
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

    internal class OurCheckButtonNormal : IOurCheckButtonDesigner
    {
        #region Fields
        private readonly OurCheckButtonBase _checkButtonBase;

        private bool _useBackColor;
        private bool _useCheckBorder = true;
        private bool _useCheckBorderColor = true;
        private bool _useCheckBackColor = true;

        private Color _backColor = Color.FromArgb(255, 75, 75, 75);
        private Color _borderColor = Color.FromArgb(255, 25, 25, 25);
        private Color _checkColor = Color.Blue;
        private Color _checkBorderColor = Color.FromArgb(255, 65, 65, 65);
        private Color _checkBackColor = Color.FromArgb(255, 65, 65, 65);
        private Color _textColor = Color.FromArgb(255, 150, 150, 150);

        private string _text;
        #endregion

        #region Properties
        [DefaultValue(false)]
        [Description("Override the BackColor and use it?")]
        public bool UseBackColor
        {
            get { return this._useBackColor; }
            set
            {
                this._useBackColor = value;
                if (value && !this._checkButtonBase.Enabled) { this._checkButtonBase.BackColor = this.BackColor; }
            }
        }

        [Browsable(false)]
        [DefaultValue(true)]
        [Description("Use a color for the border if one?")]
        public bool UseBorderColor { get { return true; } set { } }

        [Browsable(false)]
        [DefaultValue(true)]
        [Description("Use Color for the Check?")]
        public bool UseCheckColor { get { return true; } set { } }

        [DefaultValue(true)]
        [Description("Use a border on the Check?")]
        public bool UseCheckBorder
        {
            get { return this._useCheckBorder; }
            set
            {
                this._useCheckBorder = value;
                if (this._checkButtonBase.Enabled
                    && this._checkButtonBase.Checked) { this._checkButtonBase.Invalidate(); }
            }
        }

        [DefaultValue(true)]
        [Description("Use the color on the border for the check?")]
        public bool UseCheckBorderColor
        {
            get { return this._useCheckBorderColor; }
            set
            {
                this._useCheckBorderColor = value;
                if (this._useCheckBorder
                    && this._checkButtonBase.Enabled
                    && this._checkButtonBase.Checked) { this._checkButtonBase.Invalidate(); }
            }
        }

        [DefaultValue(true)]
        [Description("Use the BackColor for the CheckBox?")]
        public bool UseCheckBackColor
        {
            get { return this._useCheckBackColor; }
            set
            {
                this._useCheckBackColor = value;
                if (value && this._checkButtonBase.Enabled) { this._checkButtonBase.Invalidate(); }
            }
        }

        [Browsable(false)]
        [DefaultValue(true)]
        [Description("Use this Text Color?")]
        public bool UseTextColor { get { return true; } set { } }

        [Browsable(false)]
        [DefaultValue(true)]
        [Description("Use this text?")]
        public bool UseText { get { return true; } set { } }

        [DefaultValue(typeof(Color), "255, 75, 75, 75")]
        [Description("BackColor of the whole ChecBox.")]
        public Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;
                if (this.UseBackColor
                    && this._checkButtonBase.Enabled) { this._checkButtonBase.BackColor = value; }
            }
        }

        [DefaultValue(typeof(Color), "255, 25, 25, 25")]
        [Description("Color of the border.")]
        public Color BorderColor
        {
            get { return this._borderColor; }
            set
            {
                this._borderColor = value;
                if (this._checkButtonBase.Enabled) { this._checkButtonBase.Invalidate(); }
            }
        }

        [DefaultValue(typeof(Color), "Blue")]
        [Description("Color of the check.")]
        public Color CheckColor
        {
            get { return this._checkColor; }
            set
            {
                this._checkColor = value;
                if (this._checkButtonBase.Enabled
                    && this._checkButtonBase.Checked) { this._checkButtonBase.Invalidate(); }
            }
        }

        [DefaultValue(typeof(Color), "255, 65, 65, 65")]
        [Description("Color of the border on the Check.")]
        public Color CheckBorderColor
        {
            get { return this._checkBorderColor; }
            set
            {
                this._checkBorderColor = value;
                if (this._useCheckBorder
                    && this._checkButtonBase.Enabled
                    && this._checkButtonBase.Checked) { this._checkButtonBase.Invalidate(); }
            }
        }

        [DefaultValue(typeof(Color), "255, 65, 65, 65")]
        [Description("Color of the background where the check is.")]
        public Color CheckBackColor
        {
            get { return this._checkBackColor; }
            set
            {
                this._checkBackColor = value;
                if (this.UseCheckBackColor
                    && this._checkButtonBase.Enabled) { this._checkButtonBase.Invalidate(); }
            }
        }

        [DefaultValue(typeof(Color), "255, 150, 150, 150")]
        [Description("Color of the text.")]
        public Color TextColor
        {
            get { return this._textColor; }
            set
            {
                this._textColor = value;

                if (this._checkButtonBase.Enabled) { this._checkButtonBase.ForeColor = value; }
            }
        }

        [Description("The Text on the CheckButton.")]
        public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;

                if (this._checkButtonBase.IsInDesignerMode)
                {
                    this._checkButtonBase.UpdateMinimumSize();
                    this._checkButtonBase.UpdateSize();
                }

                if (this._checkButtonBase.Enabled) { this._checkButtonBase.Text = value; }
            }
        }
        #endregion

        #region Constructors
        public OurCheckButtonNormal(OurCheckButtonBase checkButtonBase) { this._checkButtonBase = checkButtonBase; }
        #endregion
    }
}