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

    using OurCSharp.OurControls.Core.Buttons.Button;
    using OurCSharp.OurControls.Core.Buttons.Interfaces;

    internal class OurButtonDisabled : IOurButtonDesigner
    {
        #region Properties

        // TODO Might not need to default attribute
        // TODO Shouldn't need to invalidate here at all...
        ////[DefaultValue(false)]
        [Description("Should we use the BorderColor given here when OurButton is in the corresponding state?")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool UseBorderColor { get; set; } = false;

        // TODO Might not need to default attribute
        // TODO Shouldn't need to invalidate here at all...
        ////[DefaultValue(false)]
        [Description("Should we use the Text given here when OurButton is in the corresponding state?")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool UseText { get; set; } = false;

        // TODO Might not need to default attribute
        // TODO Shouldn't need to invalidate here at all...
        ////[DefaultValue(false)]
        [Description("Should we use the TextColor given here when OurButton is in the corresponding state?")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool UseTextColor { get; set; } = false;

        [DefaultValue(typeof(Color), "255, 50, 50, 50")]
        [Description("The background color of OurButton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Color BackColor { get; set; } = Color.FromArgb(255, 50, 50, 50);

        [DefaultValue(typeof(Color), "255, 25, 25, 25")]
        [Description("The color of the Border on OurButton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Color BorderColor { get; set; } = Color.FromArgb(255, 25, 25, 25);

        [DefaultValue(typeof(Color), "255, 150, 150, 150")]
        [Description("The color of the Text on OurButton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Color TextColor { get; set; } = Color.FromArgb(255, 150, 150, 150);

        [Description("The Text displayed on OurButton.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string Text { get; set; }
        #endregion

        #region Constructors

        // TODO Any other controls that will  be using this, add a cunstructor for.
        public OurButtonDisabled(OurButton ourButton) { this.Text = ourButton.Text; }
        #endregion
    }
}