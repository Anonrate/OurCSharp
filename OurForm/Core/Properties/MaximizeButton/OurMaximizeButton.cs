// ----------------------------------------------------------------------------
// <copyright file="OurMaximizeButton.cs" company="OurCSharp">
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

namespace OurCSharp.OurForm.Core.Properties.MaximizeButton
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    using OurCSharp.OurForm.Core.Enums;
    using OurCSharp.OurForm.Core.Interfaces;
    using OurCSharp.OurForm.Core.Properties.MaximizeButton.SubProperties;

    public class OurMaximizeButton : IOurFormButtonBase
    {
        #region Fields
        private readonly OurForm _ourForm;

        private OurFormButtonStates _state = OurFormButtonStates.Shown;
        #endregion

        #region Properties
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

        [Browsable(false)]
        public OurBounds ButtonBounds { get; } = OurBounds.MaximizeButton;

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
        public OurMaximizeButton(OurForm ourForm)
        {
            this._ourForm = ourForm;
            this.Normal = new OurMaximizeButtonNormal(ourForm);
            this.Hovered = new OurMaximizeButtonHovered(ourForm);
            this.Clicked = new OurMaximizeButtonClicked(ourForm);
            this.Disabled = new OurMaximizeButtonDisabled(ourForm);
        }
        #endregion

        #region Methods
        [SuppressMessage("ReSharper", "UseStringInterpolation")]
        public override string ToString()
            =>
                string.Format(
                              "OurMaximizeButtonState({0}), OurMaximizeButtonNormal({1}),"
                              + " OurMaximizeButtonHovered({2}), OurMaximizeButtonClicked({3}), OurMaximizeButtonDisabled({4})",
                              this.State,
                              this.Normal,
                              this.Hovered,
                              this.Clicked,
                              this.Disabled);
        #endregion
    }
}