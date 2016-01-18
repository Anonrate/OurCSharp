namespace OurCSharp.OurForm.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public class OurForm : Form
    {
        #region Fields
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTCAPTION = 2;
        private const int HTCLIENT = 1;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private const int WM_NCHITTEST = 0x0084;

        private readonly Font _titleBarFont;

        private Color _backColor;
        private Color _borderTrimColor;

        private Rectangle _closeRect;
        private Rectangle _maximizeRect;
        private Rectangle _minimizeRect;

        // TODO May try to implement a different way to achieve same results as using the enums
        private MouseAction _mouseAction;
        private MouseIsOver _mouseIsOver;
        #endregion Fields

        #region Enums
        private enum MouseAction
        {
            Click,
            Hover,
            None
        }

        private enum MouseIsOver
        {
            Client,
            CloseButton,
            MaximizeButton,
            MinimizeButton,
            OffClient
        }
        #endregion Enums

        #region Properties

        #region OurProperties
        [Category("OurForm")]
        [Description("Should OurForm be Movable?")]
        public bool Movable { get; set; } = true;

        [Category("OurForm")]
        [Description("Should OurForm be Sisable?")]
        public bool Sizable { get; set; } = true;

        [Category("OurForm")]
        [DefaultValue(typeof(Color), "255, 0, 0, 0")]
        [Description("Color of the Border around OurForm.")]
        public Color BorderColor
        {
            /*
             * Uses the 'base.BackColor' because the BorderColor is the BackGround and instead of handling the
             * BackColor with OnPaint event, it's more efficient and will use less resrouces.
             */

            // BUG Lets check to see if this fixes the BackColor issue we previously had.
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]
        [DefaultValue(typeof(Color), "255, 25, 25, 25")]
        [Description("Color of the Trim on the Border around OurFrom.")]
        public Color BorderTrimColor
        {
            get { return this._borderTrimColor; }
            set
            {
                this._borderTrimColor = value;
                this.Invalidate();
            }
        }

        // TODO CloseButton

        // TODO MaximizeButton

        // TODO MinimizeButton

        // TODO Decorate the three properties with '[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]'
        #endregion OurProperties

        #region Overriden Properties
        [Category("OurForm")]
        // TODO Need to create Resource File with the Embeded.
        // TODO Decorate with 'DeffaultValue'
        // TODO May use external Lib for Images and Icons.
        // TODO Have special folders for specific items.
        [Description("The Icon shown at the top left of OurForm if ShowIcon is true.")]
        public new Icon Icon
        {
            get { return base.Icon; }
            set
            {
                base.Icon = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]
        [DefaultValue(typeof(Color), "255, 75, 75, 75")]
        [Description("The background Color of OurForm.")]
        public override Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]
        [DefaultValue(typeof(Color), "255, 255, 255, 255")]
        [Description("The foreground Color of OurForm.")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                this.Invalidate();
            }
        }

        [Category("OurForm")]
        [Description("The Minimum Size for OurForm.")]
        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set
            {
                int minWidth;

                base.MinimumSize = value.Width
                                   >= (minWidth =
                                       (int)this.CreateGraphics().MeasureString(this.Text, this._titleBarFont).Width
                                       + 116) && value.Height >= 52
                                       ? value
                                       : new Size(value.Width < minWidth ? minWidth : value.Width,
                                                  value.Height < 52 ? 52 : value.Height);
            }
        }

        [Category("OurForm")]
        [DefaultValue("OurForm")]
        [Description("Text displayed in the title bar of OurForm.")]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                this.Invalidate();
            }
        }

        [DefaultValue(true)]
        [Description("This cannot be changed for rendering and visual appearance reasons.")]
        protected override bool DoubleBuffered { get { return base.DoubleBuffered; } set { } }
        #endregion Overriden Properties

        #region Disabled Properties
        [Category("DisabledProperties")]
        // TODO If this is not set automatically, first try decorating with 'DefaultValue' else assign in constructor.
        [Description("This property cannot be changed for the reason of the appearance of OurForm.")]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            private set { base.FormBorderStyle = FormBorderStyle.None; }
        }

        [Category("DisabledProperties")]
        [Description("Please see 'CloseButton', 'MaximizeButton' and 'MinimizeButton' to achieve duplicate results.")]
        [Obsolete("Please see 'CloseButton', 'MaximizeButton' and 'MinimizeButton' to achieve duplicate results.")]
        public new string ControlBox { get; } = "Deprecated!  Read Description.";

        [Category("DisabledProperties")]
        [Description("Please use 'MaximizeButton' to achieve duplicate results.")]
        [Obsolete("Please use 'MaximizeButton' to achieve duplicate results.")]
        public new string MaximizeBox { get; } = "Deprecated!  Read Description.";

        [Category("DisabledProperties")]
        [Description("Please use 'MinimizeButton' to achieve duplicate results.")]
        [Obsolete("Please use 'MinimizeButton' to achieve duplicate results.")]
        public new string MinimizeBox { get; } = "Deprecated!  Read Description.";
        #endregion Disabled Properties
        #endregion Properties

        protected OurForm()
        {
            this._titleBarFont = new Font("Consolas", 10F, FontStyle.Regular);

            /*
             * Going to assign the overriden properties in the 'OnCreateControl' despite that it's in practice to do so
             * in the Constructor, but being that they are overriden, I'm hoping that I'm not going to have to use
             * the qualifier 'base'.
             */

            // TODO Make sure everthing is set accordingly, else modify it to as it was previously.

            this.Padding = new Padding(43, 25, 7, 7);

            // TODO Assign and instantiate 'CloseButton' after 'OurCloseButton' has been created.
            // TODO Assign and instantiate 'MaximizeButton' after 'OurMaximizeButton' has been created.
            // TODO Assign and instantiate 'MinimizeButton' after 'OurMinimizeButton' has been created.

            this._closeRect = new Rectangle(this.Width - 30, 0, 24, 24);
            this._maximizeRect = new Rectangle(this.Width - 54, 0, 24, 24);
            this._minimizeRect = new Rectangle(this.Width - 78, 0, 24, 24);

            // TODO Not sure if should Invalidate here or just in the 'OnCreateControl'..
        }

        #region Methods

        #region Overriden Events
        protected override void OnCreateControl()
        {
            // TODO See if anything noticable happens if this is not called.
            // TODO May just keep it there anyways because it's best in practice.
            ////base.OnCreateControl();

            this.BackColor = Color.FromArgb(255, 75, 75, 75);
            this.DoubleBuffered = true;

            this.DockPadding.Bottom = 7;
            this.DockPadding.Left = 52;
            this.DockPadding.Right = 7;
            this.DockPadding.Top = 44;

            this.ForeColor = Color.FromArgb(255, 255, 255, 255);

            // TODO Check if 'FormBorderStyle' is set to None.  If not assign it to None.
            // TODO Assign Icon once Resources File has been created.

            // TODO This may not worrk accordingly.
            this.MinimumSize =
                (new Size(
                    (int)this.CreateGraphics().MeasureString(this.Text = "OurForm", this._titleBarFont).Width + 116,
                    52));

            // TODO If any filckering occurs, set this in the Constructor.
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            // TODO Not sure if we should Invalidate here, just in the Constructor or both..
            this.Invalidate();
        }
        #endregion Overriden Events

        #region OurMethods

        #endregion OurMethods
        #endregion Methods
    }
}
