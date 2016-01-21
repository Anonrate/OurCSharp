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

namespace OurCSharp.OurControls.Core.Buttons.Properties
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using OurCSharp.OurControls.Core.Buttons.Button;
    using OurCSharp.OurControls.Core.Buttons.Interfaces;

    internal class OurButtonNormal : IOurButtonDesigner
    {
        #region Fields
        private readonly Control _ourControl;

        private Color _backColor = Color.FromArgb(255, 65, 65, 65);
        private Color _borderColor = Color.FromArgb(255, 25, 25, 25);

        private string _text;
        private Color _textColor = Color.FromArgb(255, 150, 150, 150);

        private bool _useBorderColor = true;
        #endregion

        #region Properties
        [DefaultValue(true)]
        [Description("Should we use the BorderColor given here when OurButton is in the corresponding state?")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool UseBorderColor
        {
            get { return this._useBorderColor; }
            set
            {
                this._useBorderColor = value;
                this._ourControl.Invalidate();
            }
        }

        [Browsable(false)]
        [DefaultValue(true)]
        [Description("Should we use the Text given here when OurButton is in the corresponding state?")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool UseText { get { return true; } set { } }

        [Browsable(false)]
        [DefaultValue(true)]
        [Description("Should we use the TextColor given here when OurButton is in the corresponding state?")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool UseTextColor { get { return true; } set { } }

        [DefaultValue(typeof(Color), "255, 65, 65, 65")]
        [Description("The background color of OurButton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;
                this._ourControl.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "255, 25, 25, 25")]
        [Description("The color of the Border on OurButton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Color BorderColor
        {
            get { return this._borderColor; }
            set
            {
                this._borderColor = value;
                this._ourControl.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "255, 150, 150, 150")]
        [Description("The color of the Text on OurButton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Color TextColor
        {
            get { return this._textColor; }
            set
            {
                this._textColor = value;
                this._ourControl.Invalidate();
            }
        }

        [Description("The Text displayed on OurButton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;
                this._ourControl.Invalidate();
            }
        }
        #endregion

        #region Constructors

        // TODO Any other controls that will  be using this, add a cunstructor for.
        public OurButtonNormal(OurButton ourControl)
        {
            this._ourControl = ourControl;
            this.Text = ourControl.Text;
        }
        #endregion
    }
}