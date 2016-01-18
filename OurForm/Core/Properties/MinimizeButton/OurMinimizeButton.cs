// ----------------------------------------------------------------------------
// <copyright file="OurMinimizeButton.cs" company="OurCSharp">
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

namespace OurCSharp.OurForm.Core.Properties.MinimizeButton
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    using OurCSharp.OurForm.Core.Enums;
    using OurCSharp.OurForm.Core.Interfaces;
    using OurCSharp.OurForm.Core.Properties.MinimizeButton.SubProperties;

    // TODO Double check that this is accessable though being internal.
    public class OurMinimizeButton : IOurFormButtonBase
    {
        #region Fields
        private readonly OurForm _ourForm;

        private OurFormButtonStates _state = OurFormButtonStates.Shown;
        #endregion

        #region Properties
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
        public OurBounds ButtonBounds { get; } = OurBounds.MinimizeButton;

        public IOurFormButtonDesigner Normal { get; }

        public IOurFormButtonDesigner Hovered { get; }

        public IOurFormButtonDesigner Clicked { get; }

        public IOurFormButtonDesigner Disabled { get; }
        #endregion

        #region Constructors
        public OurMinimizeButton(OurForm ourForm)
        {
            this._ourForm = ourForm;
            this.Normal = new OurMinimizeButtonNormal(ourForm);
            this.Hovered = new OurMinimizeButtonHovered(ourForm);
            this.Clicked = new OurMinimizeButtonClicked(ourForm);
            this.Disabled = new OurMinimizeButtonDisabled(ourForm);
        }
        #endregion

        #region Methods
        [SuppressMessage("ReSharper", "UseStringInterpolation")]
        public override string ToString()
            =>
                string.Format(
                              "OurMinimizeButtonState({0}), OurMinimizeButtonNormal({1}),"
                              + " OurMinimizeButtonHovered({2}), OurMinimizeButtonClicked({3}), OurMinimizeButtonDisabled({4})",
                              this.State,
                              this.Normal,
                              this.Hovered,
                              this.Clicked,
                              this.Disabled);
        #endregion
    }
}