// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroCheckCircle.cs" company="Terry D. Eppler">
//    This is a Federal Budget, Finance, and Accounting application for the
//    US Environmental Protection Agency (US EPA).
//    Copyright ©  2023  Terry Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the “Software”),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at:   terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   MetroCheckCircle.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace BudgetExecution
{
    #region Control

    /// <summary>
    /// A class collection for rendering a metro-like circle checkbox.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("Click")]
    [Description("Draws a circular checkbox.")]
    [Designer(typeof(MetroCheckCircleDesigner))]
    [DesignerCategory("Code")]
    [ToolboxBitmap(typeof(CheckBox))]
    public class MetroCheckCircle : Control
    {

        #region Private Fields
        /// <summary>
        /// The rect
        /// </summary>
        private Rectangle rect;

        /// <summary>
        /// The current state
        /// </summary>
        private MetroCheckCircle.MouseState CurrentState;

        /// <summary>
        /// The text
        /// </summary>
        private string _Text;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The draw borders
        /// </summary>
        private bool _DrawBorders;

        /// <summary>
        /// The checked
        /// </summary>
        private bool _Checked;

        /// <summary>
        /// The check on click
        /// </summary>
        private bool _CheckOnClick;

        /// <summary>
        /// The use full detection area
        /// </summary>
        private bool _UseFullDetectionArea;
        #endregion

        #region Public Properties  


        /// <summary>
        /// Gets or sets the color scheme.
        /// </summary>
        /// <value>The color scheme.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("sets the color scheme.")]
        [ReadOnly(false)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MetroCheckCircle.MainColorScheme ColorScheme
        {
            [DebuggerNonUserCode]
            get;
            [DebuggerNonUserCode]
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MetroCheckCircle" /> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Set to enable the control to be checked.")]
        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                if (value != this._Checked)
                {
                    this._Checked = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to check on click.
        /// </summary>
        /// <value><c>true</c> if [check on click]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to check on click.")]
        public bool CheckOnClick
        {
            get
            {
                return this._CheckOnClick;
            }
            set
            {
                if (value != this._CheckOnClick)
                {
                    this._CheckOnClick = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether [draw borders].
        /// </summary>
        /// <value><c>true</c> if [draw borders]; otherwise, <c>false</c>.</value>
        public bool DrawBorders
        {
            get
            {
                return this._DrawBorders;
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("sets the style.")]
        public Design.Style Style
        {
            get
            {
                return this._Style;
            }
            set
            {
                if (value != this._Style)
                {
                    this._Style = value;
                    if (this._Style == Design.Style.Light)
                    {
                        this.ForeColor = Color.Black;
                        this.ColorScheme._InnerCircleColor = Color.FromArgb(98, 98, 98);
                        this.ColorScheme._BorderColor = Color.FromArgb(250, 250, 250);
                        this.ColorScheme._FillColor = Color.FromArgb(0, 164, 240);
                    }
                    else if (this._Style != Design.Style.Dark)
                    {
                        this._Style = Design.Style.Custom;
                    }
                    else
                    {
                        this.ForeColor = Color.FromArgb(153, 153, 153);
                        this.ColorScheme._InnerCircleColor = Color.FromArgb(98, 98, 98);
                        this.ColorScheme._BorderColor = Color.FromArgb(0, 164, 240);
                        this.ColorScheme._FillColor = Color.FromArgb(0, 164, 240);
                    }
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("sets the text associated with this control.")]
        public override string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                if (Operators.CompareString(value, this._Text, false) != 0)
                {
                    this._Text = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use full detection area.
        /// </summary>
        /// <value><c>true</c> if use full detection area; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to use full detection area.")]
        public bool UseFullDetectionArea
        {
            get
            {
                return this._UseFullDetectionArea;
            }
            set
            {
                if (value != this._UseFullDetectionArea)
                {
                    this._UseFullDetectionArea = value;
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroCheckCircle" /> class.
        /// </summary>
        public MetroCheckCircle()
        {
            this.rect = new Rectangle(0 + 2, 0 + 2, 16, 16);
            this.CurrentState = MetroCheckCircle.MouseState.None;
            this.Size = new Size(159, 22);
            this._Text = this.Name;
            this._Style = Design.Style.Light;
            this._DrawBorders = true;
            this._Checked = true;
            this._CheckOnClick = true;
            this._UseFullDetectionArea = false;
            this.ColorScheme = new MetroCheckCircle.MainColorScheme();
            this.Size = new System.Drawing.Size(115, 18);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        #endregion

        /// <summary>
        /// Fixes the bug.
        /// </summary>
        private void FixBug()
        {
            this.Style = Design.Style.Dark;
            this.Invalidate();
            this.Style = Design.Style.Light;
            this.Invalidate();
        }

        /// <summary>
        /// Gets the color of the back.
        /// </summary>
        /// <returns>Color.</returns>
        private Color GetBackColor()
        {
            System.Windows.Forms.Form form = base.FindForm();
            Color backColor = form.BackColor;
            int r = checked(backColor.R + 11);
            backColor = form.BackColor;
            int b = checked(backColor.B + 11);
            backColor = form.BackColor;
            int g = checked(backColor.G + 11);
            if (r >= 255)
            {
                r = 255;
            }
            if (b >= 255)
            {
                b = 255;
            }
            if (g >= 255)
            {
                g = 255;
            }
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.CurrentState = MetroCheckCircle.MouseState.Over;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            this.CurrentState = MetroCheckCircle.MouseState.Over;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            this.CurrentState = MetroCheckCircle.MouseState.None;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            MetroCheckCircle.CheckedChangedEventHandler checkedChangedEventHandler;
            if (this.UseFullDetectionArea)
            {
                if (this.CheckOnClick)
                {
                    if (this.Checked)
                    {
                        this.Checked = false;
                    }
                    else if (!this.Checked)
                    {
                        this.Checked = true;
                    }
                    checkedChangedEventHandler = MetroCheckCircle.CheckedChanged;
                    if (checkedChangedEventHandler != null)
                    {
                        checkedChangedEventHandler(this, new EventArgs());
                    }
                    this.Invalidate();
                }
            }
            else if (this.rect.Contains(e.Location))
            {
                if (this.CheckOnClick)
                {
                    if (this.Checked)
                    {
                        this.Checked = false;
                    }
                    else if (!this.Checked)
                    {
                        this.Checked = true;
                    }
                    checkedChangedEventHandler = MetroCheckCircle.CheckedChanged;
                    if (checkedChangedEventHandler != null)
                    {
                        checkedChangedEventHandler(this, new EventArgs());
                    }
                    this.Invalidate();
                }
            }
            this.CurrentState = MetroCheckCircle.MouseState.Over;
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 22;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Rectangle rectangle1 = new Rectangle(3, 3, 10, 10);
            Rectangle rectangle = new Rectangle(5, 5, 10, 10);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            switch (this.CurrentState)
            {
                case MetroCheckCircle.MouseState.None:
                    {
                        if (this.Checked)
                        {
                            graphics.DrawEllipse(new Pen(this.ColorScheme._FillColor), this.rect);
                            graphics.FillEllipse(new SolidBrush(this.ColorScheme._FillColor), rectangle);
                        }
                        else
                        {
                            graphics.DrawEllipse(new Pen(this.ColorScheme._InnerCircleColor), this.rect);
                        }
                        break;
                    }
                case MetroCheckCircle.MouseState.Over:
                    {
                        if (this.Checked)
                        {
                            graphics.DrawEllipse(new Pen(this.ColorScheme._FillColor), this.rect);
                            graphics.DrawEllipse(new Pen(this.ColorScheme._BorderColor), rectangle1);
                            graphics.FillEllipse(new SolidBrush(this.ColorScheme._FillColor), rectangle);
                        }
                        else
                        {
                            graphics.DrawEllipse(new Pen(this.ColorScheme._FillColor), this.rect);
                        }
                        break;
                    }
                case MetroCheckCircle.MouseState.Down:
                    {
                        if (this.Checked)
                        {
                            graphics.DrawEllipse(new Pen(this.ColorScheme._InnerCircleColor), this.rect);
                            graphics.DrawEllipse(new Pen(this.ColorScheme._BorderColor), rectangle1);
                            graphics.FillEllipse(new SolidBrush(this.ColorScheme._FillColor), rectangle);
                        }
                        else
                        {
                            graphics.DrawEllipse(new Pen(this.ColorScheme._FillColor), this.rect);
                        }
                        break;
                    }
            }
            Point point = new Point(21, 2);
            Brush solidBrush = new SolidBrush(this.ForeColor);
            graphics.DrawString(this.Text, this.Font, solidBrush, point);
            base.OnPaint(e);
        }

        /// <summary>
        /// Occurs when [checked changed].
        /// </summary>
        public static event MetroCheckCircle.CheckedChangedEventHandler CheckedChanged;

        /// <summary>
        /// Delegate CheckedChangedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Class MainColorScheme.
        /// </summary>
        public class MainColorScheme
        {
            /// <summary>
            /// The inner circle color
            /// </summary>
            public Color _InnerCircleColor;

            /// <summary>
            /// The fill color
            /// </summary>
            public Color _FillColor;

            /// <summary>
            /// The border color
            /// </summary>
            public Color _BorderColor;

            /// <summary>
            /// Gets or sets the color of the border.
            /// </summary>
            /// <value>The color of the border.</value>
            [Browsable(true)]
            [Category("Appearance")]
            [Description("Gibt die Farbe für die Umrandung an.")]
            public Color BorderColor
            {
                get
                {
                    return this._BorderColor;
                }
                set
                {
                    if (value != this._BorderColor)
                    {
                        this._BorderColor = value;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the color of the fill.
            /// </summary>
            /// <value>The color of the fill.</value>
            [Browsable(true)]
            [Category("Appearance")]
            [Description("Gibt die Hintergrundfarbe des CheckCircles an.")]
            public Color FillColor
            {
                get
                {
                    return this._FillColor;
                }
                set
                {
                    if (value != this._FillColor)
                    {
                        this._FillColor = value;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the inner circlecolor.
            /// </summary>
            /// <value>The inner circlecolor.</value>
            [Browsable(true)]
            [Category("Appearance")]
            [Description("Gibt die Hauptfarbe des CheckCircles an.")]
            public Color InnerCirclecolor
            {
                get
                {
                    return this._InnerCircleColor;
                }
                set
                {
                    if (value != this._InnerCircleColor)
                    {
                        this._InnerCircleColor = value;
                    }
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MainColorScheme"/> class.
            /// </summary>
            public MainColorScheme()
            {
                this._InnerCircleColor = Color.FromArgb(98, 98, 98);
                this._FillColor = Color.FromArgb(0, 164, 240);
                this._BorderColor = Color.FromArgb(250, 250, 250);
            }
        }

        /// <summary>
        /// Enum MouseState
        /// </summary>
        private enum MouseState
        {
            /// <summary>
            /// The none
            /// </summary>
            None,
            /// <summary>
            /// The over
            /// </summary>
            Over,
            /// <summary>
            /// Down
            /// </summary>
            Down
        }
    }

    #endregion

    #region New Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(MetroCheckCircleDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class MetroCheckCircleDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class MetroCheckCircleDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new MetroCheckCircleSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }

        #region Budget Filter (Remove Properties)
        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("AllowDrop");
            //Properties.Remove("FlatStyle");
            //Properties.Remove("ForeColor");
            //Properties.Remove("ImageIndex");
            //Properties.Remove("ImageList");
        }
        #endregion

    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class MetroCheckCircleSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroCheckCircleSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private MetroCheckCircle colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="MetroCheckCircleSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroCheckCircleSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as MetroCheckCircle;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="System.ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MetroCheckCircleSmartTagActionList"/> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public bool Checked
        {
            get
            {
                return colUserControl.Checked;
            }
            set
            {
                GetPropertyByName("Checked").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [check on click].
        /// </summary>
        /// <value><c>true</c> if [check on click]; otherwise, <c>false</c>.</value>
        public bool CheckOnClick
        {
            get
            {
                return colUserControl.CheckOnClick;
            }
            set
            {
                GetPropertyByName("CheckOnClick").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use full detection area].
        /// </summary>
        /// <value><c>true</c> if [use full detection area]; otherwise, <c>false</c>.</value>
        public bool UseFullDetectionArea
        {
            get
            {
                return colUserControl.UseFullDetectionArea;
            }
            set
            {
                GetPropertyByName("UseFullDetectionArea").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
        {
            get
            {
                return colUserControl.ColorScheme.BorderColor;
            }
            set
            {
                this.colUserControl.ColorScheme.BorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        public Color FillColor
        {
            get
            {
                return this.colUserControl.ColorScheme.FillColor;
            }
            set
            {
                this.colUserControl.ColorScheme.FillColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the main.
        /// </summary>
        /// <value>The color of the main.</value>
        public Color MainColor
        {
            get
            {
                return this.colUserControl.ColorScheme._InnerCircleColor;
            }
            set
            {
                this.colUserControl.ColorScheme._InnerCircleColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public Design.Style Style
        {
            get
            {
                return colUserControl.Style;
            }
            set
            {
                GetPropertyByName("Style").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public new string Text
        {
            get
            {
                return colUserControl.Text;
            }
            set
            {
                GetPropertyByName("Text").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Behaviour"));


            items.Add(new DesignerActionPropertyItem("Checked",
                "Checked", "Behaviour",
                "Sets the checked state."));

            items.Add(new DesignerActionPropertyItem("CheckOnClick",
                "Check On Click", "Behaviour",
                "Set to change checked state on mouse click."));

            items.Add(new DesignerActionPropertyItem("UseFullDetectionArea",
                "Use Full Detection Area", "Behaviour",
                "Set to maximize detection area."));

            items.Add(new DesignerActionHeaderItem("Appearance"));


            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("BorderColor",
                "Border Color", "Appearance",
                "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("FillColor",
                "Fill Color", "Appearance",
                "Sets the border color."));

            items.Add(new DesignerActionPropertyItem("MainColor",
                "Main Color", "Appearance",
                "Sets the main color."));

            items.Add(new DesignerActionPropertyItem("Style",
                "Style", "Appearance",
                "Sets the theme style."));

            items.Add(new DesignerActionPropertyItem("Text",
                "Text", "Appearance",
                "Sets the text."));

            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Product: ");
            location.Append(colUserControl.ProductName);
            StringBuilder size = new StringBuilder("Version: ");
            size.Append(colUserControl.ProductVersion);
            items.Add(new DesignerActionTextItem(location.ToString(),
                             "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(),
                             "Information"));

            return items;
        }

        #endregion




    }

    #endregion

    #endregion


    #region Old SmartTag

    /// <summary>
    /// Class MetroCheckCircleActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroCheckCircleActionList : DesignerActionList
    {
        /// <summary>
        /// The CCL
        /// </summary>
        private MetroCheckCircle _ccl;

        /// <summary>
        /// The designer action SVC
        /// </summary>
        private DesignerActionUIService designerActionSvc;

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
        {
            get
            {
                return this._ccl.ColorScheme.BorderColor;
            }
            set
            {
                this._ccl.ColorScheme.BorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        public Color FillColor
        {
            get
            {
                return this._ccl.ColorScheme.FillColor;
            }
            set
            {
                this._ccl.ColorScheme.FillColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the main.
        /// </summary>
        /// <value>The color of the main.</value>
        public Color MainColor
        {
            get
            {
                return this._ccl.ColorScheme._InnerCircleColor;
            }
            set
            {
                this._ccl.ColorScheme._InnerCircleColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public Design.Style Style
        {
            get
            {
                return this._ccl.Style;
            }
            set
            {
                this._ccl.Style = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroCheckCircleActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroCheckCircleActionList(IComponent component) : base(component)
        {
            this.designerActionSvc = null;
            this._ccl = (MetroCheckCircle)component;
            this.designerActionSvc = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
        }

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
            designerActionItemCollection.Add(new DesignerActionHeaderItem("Farb-Eigenschaften"));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("MainColor", "MainColor:", "Farb-Eigenschaften", "Die Hauptfarbe des CheckCircles."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("FillColor", "FillColor:", "Farb-Eigenschaften", "Die Füll-Farbe des CheckCircles."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("BorderColor", "BorderColor:", "Farb-Eigenschaften", "Die Farbe der Umrandung des CheckCircles."));
            designerActionItemCollection.Add(new DesignerActionHeaderItem("Eigenschaften"));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("Style", "Style:", "Eigenschaften", "Der Style der MetroProgressbar."));
            return designerActionItemCollection;
        }
    }
    
    #endregion

    
}