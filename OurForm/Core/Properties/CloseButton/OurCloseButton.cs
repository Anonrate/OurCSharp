// ----------------------------------------------------------------------------
// <copyright file="OurCloseButton.cs" company="OurCSharp">
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

namespace OurCSharp.OurForm.Core.Properties.CloseButton
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    using OurCSharp.OurForm.Core.Enums;
    using OurCSharp.OurForm.Core.Interfaces;
    using OurCSharp.OurForm.Core.Properties.CloseButton.SubProperties;

    public class OurCloseButton : IOurFormButtonBase
    {
        #region Fields
        private readonly OurForm _ourForm;

        private OurFormButtonStates _state = OurFormButtonStates.Shown;
        #endregion

        #region Properties
        [Browsable(false)]
        public OurBounds ButtonBounds { get; } = OurBounds.CloseButton;

        [DefaultValue(typeof(OurFormButtonStates), "Shown")]
        [Description("Should this button be, 'Disabled', 'Hidden' or 'Shown'?")]
        public OurFormButtonStates State
        {
            get { return this._state; }
            set
            {
                this._state = value;
                this._ourForm.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurFormButtonDesigner Normal { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurFormButtonDesigner Hovered { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurFormButtonDesigner Clicked { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IOurFormButtonDesigner Disabled { get; }
        #endregion

        #region Constructors
        public OurCloseButton(OurForm ourForm)
        {
            this._ourForm = ourForm;
            this.Normal = new OurCloseButtonNormal(ourForm);
            this.Hovered = new OurCloseButtonHovered(ourForm);
            this.Clicked = new OurCloseButtonClicked(ourForm);
            this.Disabled = new OurCloseButtonDisabled(ourForm);
        }
        #endregion

        #region Methods
        [SuppressMessage("ReSharper", "UseStringInterpolation")]
        public override string ToString()
            =>
                string.Format(
                              "OurCloseButtonState({0}), OurCloseButtonNormal({1}),"
                              + " OurCloseButtonHovered({2}), OurCloseButtonClicked({3}), OurCloseButtonDisabled({4})",
                              this.State,
                              this.Normal,
                              this.Hovered,
                              this.Clicked,
                              this.Disabled);
        #endregion
    }
}