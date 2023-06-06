// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetKnob.cs" company="Terry D. Eppler">
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
//   BudgetKnob.cs
// </summary>
// ******************************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering a metro-style knob.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ Description( "A class for creating metro knob." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetKnob ) ) ]
    [ Designer( typeof( BudgetKnobDesigner ) ) ]
    public class BudgetKnob : Control
    {
        #region Enum

        /// <summary>
        /// Enum representing the Knob Fill Modes
        /// </summary>
        public enum KnobFillModes
        {
            /// <summary>
            /// The solid
            /// </summary>
            Solid,

            /// <summary>
            /// The linear gradient
            /// </summary>
            LinearGradient,

            /// <summary>
            /// The radial gradient
            /// </summary>
            RadialGradient,

            /// <summary>
            /// The hatch
            /// </summary>
            Hatch
        }

        /// <summary>
        /// Enum representing Knob Styles
        /// </summary>
        public enum KnobStyles
        {
            /// <summary>
            /// The arc
            /// </summary>
            Arc,

            /// <summary>
            /// The arc filled
            /// </summary>
            ArcFilled,

            /// <summary>
            /// The circle
            /// </summary>
            Circle,

            /// <summary>
            /// The circle filled
            /// </summary>
            CircleFilled
        }

        #endregion

        #region Events and Delegates

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        public event ValueChangedEventHandler ValueChanged;

        /// <summary>
        /// Delegate ValueChangedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void ValueChangedEventHandler( object sender, EventArgs e );

        #endregion

        #region Private Fields

        /// <summary>
        /// The percentage
        /// </summary>
        private float percentage;

        /// <summary>
        /// The base angle
        /// </summary>
        private float baseAngle;

        /// <summary>
        /// The block angle
        /// </summary>
        private float blockAngle;

        /// <summary>
        /// The line end
        /// </summary>
        private int lineEnd;

        /// <summary>
        /// The minimum
        /// </summary>
        private int _Minimum;

        /// <summary>
        /// The maximum
        /// </summary>
        private int _Maximum;

        /// <summary>
        /// The value
        /// </summary>
        private int _Value;

        /// <summary>
        /// The line length
        /// </summary>
        private int _LineLength;

        /// <summary>
        /// The line width
        /// </summary>
        private int _LineWidth;

        /// <summary>
        /// The knob style
        /// </summary>
        private KnobStyles _KnobStyle;

        /// <summary>
        /// The fill mode
        /// </summary>
        private KnobFillModes _FillMode;

        /// <summary>
        /// The hatch style
        /// </summary>
        private HatchStyle _HatchStyle;

        /// <summary>
        /// The draw line shadow
        /// </summary>
        private bool _DrawLineShadow;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The line color
        /// </summary>
        private Color _LineColor;

        /// <summary>
        /// The accent color
        /// </summary>
        private Color _AccentColor;

        /// <summary>
        /// The fill color
        /// </summary>
        private Color _FillColor;

        /// <summary>
        /// The gradient color
        /// </summary>
        private Color _GradientColor;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        /// <summary>
        /// The mouse state
        /// </summary>
        private Helpers.MouseState _MouseState;

        /// <summary>
        /// The circle pen
        /// </summary>
        private PenParameters circlePen = new( );

        /// <summary>
        /// The line pen
        /// </summary>
        private PenParameters linePen = new( );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the circle properties.
        /// </summary>
        /// <value>The circle pen.</value>
        [ TypeConverter( typeof( ExpandableObjectConverter ) ) ]
        public PenParameters CirclePen
        {
            get { return circlePen; }
            set
            {
                circlePen = value;
                Invalidate( );
            }
        }

        /// <summary>
        /// Gets or sets the line properties.
        /// </summary>
        /// <value>The line pen.</value>
        [ TypeConverter( typeof( ExpandableObjectConverter ) ) ]
        public PenParameters LinePen
        {
            get { return linePen; }
            set
            {
                linePen = value;
                Invalidate( );
            }
        }

        /// <summary>
        /// Gets or sets the color of the accent.
        /// </summary>
        /// <value>The color of the accent.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the accent." ) ]
        public Color AccentColor
        {
            get
            {
                return _AccentColor;
            }
            set
            {
                if( _AccentColor != value )
                {
                    _AccentColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to automatic style.
        /// </summary>
        /// <value><c>true</c> if [automatic style]; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to automatic style." ) ]
        public bool AutoStyle
        {
            get
            {
                return _AutoStyle;
            }
            set
            {
                if( _AutoStyle != value )
                {
                    _AutoStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the blocked angle.
        /// </summary>
        /// <value>The blocked angle.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( 90 ) ]
        [ Description( "Sets the blocked angle." ) ]
        public float BlockedAngle
        {
            get
            {
                return blockAngle;
            }
            set
            {
                if( value != blockAngle )
                {
                    if( blockAngle >= 0f & blockAngle < 360f )
                    {
                        blockAngle = value;
                        Invalidate( );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the border." ) ]
        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                if( _BorderColor != value )
                {
                    _BorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.
        /// </summary>
        /// <value>The context menu strip.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new ContextMenuStrip ContextMenuStrip
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets the default color.
        /// </summary>
        /// <value>The default color.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the default color." ) ]
        public Color DefaultColor
        {
            get
            {
                return _DefaultColor;
            }
            set
            {
                if( _DefaultColor != value )
                {
                    _DefaultColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw the line shadow.
        /// </summary>
        /// <value><c>true</c> if [draw line shadow]; otherwise, <c>false</c>.</value>
        [ Category( "Apperance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw the line shadow." ) ]
        public bool DrawLineShadow
        {
            get
            {
                return _DrawLineShadow;
            }
            set
            {
                if( _DrawLineShadow != value )
                {
                    _DrawLineShadow = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the fill." ) ]
        public Color FillColor
        {
            get
            {
                return _FillColor;
            }
            set
            {
                if( _FillColor != value )
                {
                    _FillColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        /// <value>The fill mode.</value>
        [ Category( "Apperance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the fill mode." ) ]
        public KnobFillModes FillMode
        {
            get
            {
                return _FillMode;
            }
            set
            {
                if( _FillMode != value )
                {
                    _FillMode = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new Font Font
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new Color ForeColor
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets the color of the gradient.
        /// </summary>
        /// <value>The color of the gradient.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the gradient." ) ]
        public Color GradientColor
        {
            get
            {
                return _GradientColor;
            }
            set
            {
                if( _GradientColor != value )
                {
                    _GradientColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the hatch style.
        /// </summary>
        /// <value>The hatch style.</value>
        [ Category( "Apperance" ) ]
        [ DefaultValue( 3 ) ]
        [ Description( "Sets the hatch style." ) ]
        public HatchStyle HatchStyle
        {
            get
            {
                return _HatchStyle;
            }
            set
            {
                if( _HatchStyle != value )
                {
                    _HatchStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the knob style.
        /// </summary>
        /// <value>The knob style.</value>
        [ Category( "Apperance" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "Sets the knob style." ) ]
        public KnobStyles KnobStyle
        {
            get
            {
                return _KnobStyle;
            }
            set
            {
                if( _KnobStyle != value )
                {
                    _KnobStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the line." ) ]
        public Color LineColor
        {
            get
            {
                return _LineColor;
            }
            set
            {
                if( _LineColor != value )
                {
                    _LineColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the length of the line.
        /// </summary>
        /// <value>The length of the line.</value>
        [ Category( "Apperance" ) ]
        [ DefaultValue( 18 ) ]
        [ Description( "Sets the length of the line." ) ]
        public int LineLength
        {
            get
            {
                return _LineLength;
            }
            set
            {
                if( _LineLength != value )
                {
                    if( value <= 100 )
                    {
                        if( value <= 90 )
                        {
                            lineEnd = 90;
                        }
                        else
                        {
                            lineEnd = value;
                        }

                        _LineLength = value;
                        Invalidate( );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        [ Category( "Apperance" ) ]
        [ DefaultValue( 1 ) ]
        [ Description( "Die Dicke der Linie." ) ]
        public int LineWidth
        {
            get
            {
                return _LineWidth;
            }
            set
            {
                if( _LineWidth != value )
                {
                    _LineWidth = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 100 ) ]
        [ Description( "Sets the maximum." ) ]
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if( _Maximum != value )
                {
                    _Maximum = value;

                    if( _Value > _Maximum )
                    {
                        _Value = _Maximum;
                    }

                    if( _Minimum > _Maximum )
                    {
                        _Minimum = _Maximum;
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the minimum." ) ]
        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                if( _Minimum != value )
                {
                    _Minimum = value;

                    if( _Value < _Minimum )
                    {
                        _Value = _Minimum;
                    }

                    if( _Maximum < _Minimum )
                    {
                        _Maximum = _Minimum;
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
        /// </summary>
        /// <value>The right to left.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new RightToLeft RightToLeft
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the style." ) ]
        [ RefreshProperties( RefreshProperties.All ) ]
        public Design.Style Style
        {
            get
            {
                return _Style;
            }
            set
            {
                if( value != _Style )
                {
                    _Style = value;

                    switch( value )
                    {
                        case Design.Style.Light:
                        {
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _LineColor = Design.BudgetColors.LightBorder;
                            _AccentColor = Design.BudgetColors.AccentBlue;
                            _FillColor = Design.BudgetColors.AccentBlue;
                            _GradientColor = Design.BudgetColors.LightDefault;
                            _DefaultColor = Design.BudgetColors.LightDefault;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _LineColor = Design.BudgetColors.LightBorder;
                            _AccentColor = Design.BudgetColors.AccentBlue;
                            _FillColor = Design.BudgetColors.AccentBlue;
                            _GradientColor = Design.BudgetColors.DarkDefault;
                            _DefaultColor = Design.BudgetColors.DarkDefault;
                            break;
                        }
                        default:
                        {
                            _AutoStyle = false;
                            break;
                        }
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new string Text
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 50 ) ]
        [ Description( "Sets the current value." ) ]
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if( _Value != value )
                {
                    _Value = value;

                    if( _Value < _Minimum )
                    {
                        _Value = _Minimum;
                    }
                    else if( _Value > _Maximum )
                    {
                        _Value = _Maximum;
                    }

                    percentage = (float)( checked( _Value - _Minimum )
                        / (double)checked( _Maximum - _Minimum ) );

                    var valueChangedEventHandler = ValueChanged;

                    if( valueChangedEventHandler != null )
                    {
                        valueChangedEventHandler( this, new EventArgs( ) );
                    }

                    Invalidate( );
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetKnob" /> class.
        /// </summary>
        public BudgetKnob( )
        {
            percentage = 50f;
            baseAngle = 90f;
            blockAngle = 90f;
            lineEnd = 90;
            _Minimum = 0;
            _Maximum = 100;
            _Value = 50;
            _LineLength = 18;
            _LineWidth = 1;
            _KnobStyle = KnobStyles.Circle;
            _FillMode = KnobFillModes.Solid;
            _HatchStyle = HatchStyle.BackwardDiagonal;
            _DrawLineShadow = true;
            _Style = Design.Style.Light;
            _BorderColor = Design.BudgetColors.LightBorder;
            _LineColor = Design.BudgetColors.LightBorder;
            _AccentColor = Design.BudgetColors.AccentBlue;
            _FillColor = Design.BudgetColors.AccentBlue;
            _GradientColor = Design.BudgetColors.LightDefault;
            _DefaultColor = Design.BudgetColors.LightDefault;
            _AutoStyle = true;
            _MouseState = Helpers.MouseState.None;
            Font = new Font( "Segoe UI", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            //this.BackColor = this.FindForm().BackColor;
            UpdateStyles( );
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Gets the knob brush.
        /// </summary>
        /// <param name="fillMode">The fill mode.</param>
        /// <returns>Brush.</returns>
        private Brush GetKnobBrush( KnobFillModes fillMode )
        {
            Brush solidBrush;
            var point = new Point( checked( (int)Math.Round( (double)Width / 2 ) ), 0 );
            var point1 = new Point( checked( (int)Math.Round( (double)Width / 2 ) ), Height );

            switch( fillMode )
            {
                case KnobFillModes.Solid:
                {
                    solidBrush = new SolidBrush( _FillColor );
                    break;
                }
                case KnobFillModes.LinearGradient:
                {
                    solidBrush =
                        new LinearGradientBrush( point, point1, _FillColor, _GradientColor );

                    break;
                }
                case KnobFillModes.RadialGradient:
                {
                    using( var graphicsPath = new GraphicsPath( ) )
                    {
                        var width = checked( ClientSize.Width - 1 );
                        var clientSize = ClientSize;

                        var rectangle = new Rectangle( 0, 0, width,
                            checked( clientSize.Height - 1 ) );

                        graphicsPath.AddEllipse( rectangle );

                        var pathGradientBrush = new PathGradientBrush( graphicsPath )
                        {
                            CenterColor = _FillColor,
                            SurroundColors = new[ ]
                            {
                                _GradientColor
                            }
                        };

                        solidBrush = pathGradientBrush;
                    }

                    break;
                }
                default:
                {
                    solidBrush = new HatchBrush( _HatchStyle, _FillColor, BackColor );
                    break;
                }
            }

            return solidBrush;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnBackColorChanged( EventArgs e )
        {
            if( FindForm( ) is BudgetForm )
            {
                if( _AutoStyle )
                {
                    var _budgetForm = (BudgetForm)FindForm( );
                    Style = _budgetForm.Style;
                    _Style = _budgetForm.Style;
                    Invalidate( );
                }
            }
            else if( _AutoStyle )
            {
                //Color backColor = this.Parent.BackColor;
                var backColor = BackColor;

                if( backColor == Color.White )
                {
                    _Style = Design.Style.Light;
                }
                else if( backColor == Color.FromArgb( 40, 40, 40 ) )
                {
                    _Style = Design.Style.Dark;
                }

                Style = _Style;
                Invalidate( );
            }

            base.OnBackColorChanged( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown( MouseEventArgs e )
        {
            _MouseState = Helpers.MouseState.Pressed;

            if( e.Button == MouseButtons.Left )
            {
                UpdateAngle( e.X, e.Y );
            }

            base.OnMouseDown( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseEnter( EventArgs e )
        {
            _MouseState = Helpers.MouseState.Over;
            Invalidate( );
            base.OnMouseEnter( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave( EventArgs e )
        {
            _MouseState = Helpers.MouseState.None;
            Invalidate( );
            base.OnMouseLeave( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove( MouseEventArgs e )
        {
            if( ( e.Button & MouseButtons.Left ) == MouseButtons.Left )
            {
                UpdateAngle( e.X, e.Y );
            }

            base.OnMouseMove( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp( MouseEventArgs e )
        {
            _MouseState = Helpers.MouseState.Over;
            Invalidate( );
            base.OnMouseUp( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            TransInPaint( e.Graphics );
            Size size;
            var graphics = e.Graphics;
            var single = baseAngle + blockAngle / 2f + percentage * ( 360f - blockAngle );
            var clientSize = ClientSize;
            var width = checked( clientSize.Width - 1 );
            var clientSize1 = ClientSize;
            var rectangle = new Rectangle( 0, 0, width, checked( clientSize1.Height - 1 ) );
            clientSize1 = ClientSize;
            var num = checked( (int)Math.Round( (double)clientSize1.Width / 2 ) );
            clientSize = ClientSize;

            var point = new Point( num,
                checked( (int)Math.Round( (double)clientSize.Height / 2 ) ) );

            var point1 = new Point( 0, 0 );

            var circleIntersectionPoints =
                Design.Drawing.GetCircleIntersectionPoints( rectangle, point, point1 )[ 0 ];

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            //graphics.Clear(Parent.BackColor);

            using( var pen = new Pen( _MouseState == Helpers.MouseState.None
                      ? _BorderColor
                      : _AccentColor )
                  {
                      Width = circlePen.Width,
                      EndCap = circlePen.EndCap,
                      StartCap = circlePen.EndCap,
                      Alignment = circlePen.Alignment,
                      DashCap = circlePen.DashCap,
                      DashOffset = circlePen.DashOffset,
                      DashStyle = circlePen.DashStyle,
                      LineJoin = circlePen.LineJoin,
                  } )
            {
                switch( _KnobStyle )
                {
                    case KnobStyles.Arc:
                    {
                        graphics.DrawArc( pen, rectangle, baseAngle + blockAngle / 2f,
                            360f - blockAngle );

                        break;
                    }
                    case KnobStyles.ArcFilled:
                    {
                        var knobBrush = GetKnobBrush( _FillMode );
                        graphics.FillEllipse( knobBrush, rectangle );

                        using( var solidBrush = new SolidBrush( BackColor ) )
                        {
                            var point2 = circleIntersectionPoints;
                            clientSize1 = ClientSize;

                            var num1 =
                                checked( checked( checked( (int)Math.Round(
                                            (double)clientSize1.Width / 2 ) )
                                        - circleIntersectionPoints.X )
                                    * 2 );

                            clientSize = ClientSize;

                            size = new Size( num1,
                                checked( checked( checked( (int)Math.Round(
                                            (double)clientSize.Height / 2 ) )
                                        - circleIntersectionPoints.Y )
                                    * 2 ) );

                            var rectangle1 = new Rectangle( point2, size );
                            graphics.FillEllipse( solidBrush, rectangle1 );

                            rectangle1 = new Rectangle( -1, -1, checked( Width + 1 ),
                                checked( Height + 1 ) );

                            graphics.FillPie( solidBrush, rectangle1,
                                checked( 90 - checked( (int)Math.Round( blockAngle / 2f ) ) ),
                                blockAngle );
                        }

                        knobBrush.Dispose( );
                        break;
                    }
                    case KnobStyles.Circle:
                    {
                        graphics.DrawEllipse( pen, rectangle );
                        break;
                    }
                    case KnobStyles.CircleFilled:
                    {
                        using( var brush = GetKnobBrush( _FillMode ) )
                        {
                            graphics.FillEllipse( brush, rectangle );
                        }

                        break;
                    }
                }

                var pointOnLine = new Point[ 2 ];
                size = ClientSize;
                var num2 = checked( (int)Math.Round( size.Width * 0.5f ) );
                clientSize1 = ClientSize;
                var num3 = checked( (int)Math.Round( clientSize1.Height * 0.5f ) );
                clientSize = ClientSize;

                var num4 = checked( (int)Math.Round( clientSize.Width
                    * 0.5f
                    * ( (float)Math.Cos( single * 3.14159265358979 / 180 ) + 1f ) ) );

                var size1 = ClientSize;

                pointOnLine[ 0 ] = Design.Drawing.GetPointOnLine( num2, num3, num4,
                    checked( (int)Math.Round( size1.Height
                        * 0.5f
                        * ( (float)Math.Sin( single * 3.14159265358979 / 180 ) + 1f ) ) ),
                    lineEnd );

                var clientSize2 = ClientSize;
                var num5 = checked( (int)Math.Round( clientSize2.Width * 0.5f ) );
                var size2 = ClientSize;
                var num6 = checked( (int)Math.Round( size2.Height * 0.5f ) );
                var clientSize3 = ClientSize;

                var num7 = checked( (int)Math.Round( clientSize3.Width
                    * 0.5f
                    * ( (float)Math.Cos( single * 3.14159265358979 / 180 ) + 1f ) ) );

                var size3 = ClientSize;

                pointOnLine[ 1 ] = Design.Drawing.GetPointOnLine( num5, num6, num7,
                    checked( (int)Math.Round( size3.Height
                        * 0.5f
                        * ( (float)Math.Sin( single * 3.14159265358979 / 180 ) + 1f ) ) ),
                    checked( lineEnd - _LineLength ) );

                var pointArray = pointOnLine;

                var pen1 = new Pen( _MouseState == Helpers.MouseState.None
                    ? _BorderColor
                    : _AccentColor )
                {
                    Width = LinePen.Width,
                    EndCap = LinePen.EndCap,
                    StartCap = LinePen.StartCap,
                    Alignment = LinePen.Alignment,
                    DashCap = LinePen.DashCap,
                    DashOffset = LinePen.DashOffset,
                    DashStyle = LinePen.DashStyle,
                    LineJoin = LinePen.LineJoin,
                };

                if( _DrawLineShadow )
                {
                    pen1.Width = checked( _LineWidth + 2 );
                    pen1.Color = Color.FromArgb( 40, 0, 0, 0 );
                    graphics.DrawLine( pen1, pointArray[ 0 ], pointArray[ 1 ] );
                    pen1.Width = _LineWidth;
                }

                pen1.Color = _MouseState == Helpers.MouseState.None
                    ? _LineColor
                    : _AccentColor;

                graphics.DrawLine( pen1, pointArray[ 0 ], pointArray[ 1 ] );
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged( EventArgs e )
        {
            Size = new Size( Width, Width );
            base.OnSizeChanged( e );
        }

        /// <summary>
        /// Updates the angle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        private void UpdateAngle( int x, int y )
        {
            var single = 360f - blockAngle / 2f;
            var single1 = blockAngle / 2f;
            double num = y;
            var clientSize = ClientSize;
            var height = num - (double)clientSize.Height / 2;
            double num1 = x;
            var size = ClientSize;

            var single2 = Math.Min( single,
                Math.Max( single1,
                    ( (float)( Math.Atan2( height, num1 - (double)size.Width / 2 )
                            * 180
                            / 3.14159265358979 )
                        - baseAngle
                        + 720f )
                    % 360f ) );

            percentage = ( single2 - blockAngle / 2f ) / ( 360f - blockAngle );

            _Value = checked( checked( (int)Math.Round(
                    checked( _Maximum - _Minimum ) * percentage ) )
                + _Minimum );

            var valueChangedEventHandler = ValueChanged;

            if( valueChangedEventHandler != null )
            {
                valueChangedEventHandler( this, new EventArgs( ) );
            }

            Invalidate( );
        }

        #endregion

        #region Transparency

        #region Include in Paint

        private void TransInPaint( Graphics g )
        {
            if( AllowTransparency )
            {
                BudgetKnob.MakeTransparent( this, g );
            }
        }

        #endregion

        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;
                Invalidate( );
            }
        }

        #endregion

        #region Method

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        private static void MakeTransparent( Control control, Graphics g )
        {
            var parent = control.Parent;

            if( parent == null )
            {
                return;
            }

            var bounds = control.Bounds;
            var siblings = parent.Controls;
            var index = siblings.IndexOf( control );
            Bitmap behind = null;

            for( var i = siblings.Count - 1; i > index; i-- )
            {
                var c = siblings[ i ];

                if( !c.Bounds.IntersectsWith( bounds ) )
                {
                    continue;
                }

                if( behind == null )
                {
                    behind = new Bitmap( control.Parent.ClientSize.Width,
                        control.Parent.ClientSize.Height );
                }

                c.DrawToBitmap( behind, c.Bounds );
            }

            if( behind == null )
            {
                return;
            }

            g.DrawImage( behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel );
            behind.Dispose( );
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Class PenParameters.
    /// </summary>
    public class PenParameters
    {
        /// <summary>
        /// The end cap
        /// </summary>
        private LineCap endCap = LineCap.ArrowAnchor;

        /// <summary>
        /// The start cap
        /// </summary>
        private LineCap startCap = LineCap.DiamondAnchor;

        /// <summary>
        /// The alignment
        /// </summary>
        private PenAlignment alignment = PenAlignment.Center;

        /// <summary>
        /// The dash cap
        /// </summary>
        private DashCap dashCap = DashCap.Flat;

        /// <summary>
        /// The dash offset
        /// </summary>
        private float dashOffset = 0.5f;

        /// <summary>
        /// The dash style
        /// </summary>
        private DashStyle dashStyle = DashStyle.DashDot;

        /// <summary>
        /// The line join
        /// </summary>
        private LineJoin lineJoin = LineJoin.Bevel;

        /// <summary>
        /// The width
        /// </summary>
        private int width = 1;

        /// <summary>
        /// Gets or sets the end cap.
        /// </summary>
        /// <value>The end cap.</value>
        public LineCap EndCap
        {
            get { return endCap; }
            set { endCap = value; }
        }

        /// <summary>
        /// Gets or sets the start cap.
        /// </summary>
        /// <value>The start cap.</value>
        public LineCap StartCap
        {
            get { return startCap; }
            set { startCap = value; }
        }

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>The alignment.</value>
        public PenAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        /// <summary>
        /// Gets or sets the dash cap.
        /// </summary>
        /// <value>The dash cap.</value>
        public DashCap DashCap
        {
            get { return dashCap; }
            set { dashCap = value; }
        }

        /// <summary>
        /// Gets or sets the dash offset.
        /// </summary>
        /// <value>The dash offset.</value>
        public float DashOffset
        {
            get { return dashOffset; }
            set { dashOffset = value; }
        }

        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The dash style.</value>
        public DashStyle DashStyle
        {
            get { return dashStyle; }
            set { dashStyle = value; }
        }

        /// <summary>
        /// Gets or sets the line join.
        /// </summary>
        /// <value>The line join.</value>
        public LineJoin LineJoin
        {
            get { return lineJoin; }
            set { lineJoin = value; }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
    }
}