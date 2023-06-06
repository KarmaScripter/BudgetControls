// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetNavigationButton.cs" company="Terry D. Eppler">
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
//   BudgetNavigationButton.cs
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
    /// A class collection for rendering a metro-style navigation control.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ Description( "This class enables the rendering of a navigation button." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetNavigationButton ), "BudgetNavigationButton.bmp" ) ]
    [ Designer( typeof( BudgetNavigationButtonDesigner ) ) ]
    public class BudgetNavigationButton : Control
    {
        #region Private Fields

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The arrow direction
        /// </summary>
        private ArrowDirection _ArrowDirection;

        /// <summary>
        /// The arrow path
        /// </summary>
        private GraphicsPath _ArrowPath;

        /// <summary>
        /// The circle rect
        /// </summary>
        private RectangleF _CircleRect;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The border hover color
        /// </summary>
        private Color _BorderHoverColor;

        /// <summary>
        /// The border pressed color
        /// </summary>
        private Color _BorderPressedColor;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The pressed color
        /// </summary>
        private Color _PressedColor;

        /// <summary>
        /// The arrow color
        /// </summary>
        private Color _ArrowColor;

        /// <summary>
        /// The arrow hover color
        /// </summary>
        private Color _ArrowHoverColor;

        /// <summary>
        /// The arrow pressed color
        /// </summary>
        private Color _ArrowPressedColor;

        /// <summary>
        /// The disabled color
        /// </summary>
        private Color _DisabledColor;

        /// <summary>
        /// The disabled arrow color
        /// </summary>
        private Color _DisabledArrowColor;

        /// <summary>
        /// The mouse state
        /// </summary>
        private Helpers.MouseState _MouseState;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the color of the arrow.
        /// </summary>
        /// <value>The color of the arrow.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the arrow." ) ]
        public Color ArrowColor
        {
            get
            {
                return _ArrowColor;
            }
            set
            {
                if( value != _ArrowColor )
                {
                    _ArrowColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the arrow direction.
        /// </summary>
        /// <value>The arrow direction.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the arrow direction." ) ]
        public ArrowDirection ArrowDirection
        {
            get
            {
                return _ArrowDirection;
            }
            set
            {
                if( value != _ArrowDirection )
                {
                    _ArrowDirection = value;
                    var arrowDirectionChangedEventHandler = ArrowDirectionChanged;

                    if( arrowDirectionChangedEventHandler != null )
                    {
                        arrowDirectionChangedEventHandler( this, new EventArgs( ) );
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the arrow when hovered.
        /// </summary>
        /// <value>The color of the arrow hover.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the arrow when hovered." ) ]
        public Color ArrowHoverColor
        {
            get
            {
                return _ArrowHoverColor;
            }
            set
            {
                if( value != _ArrowHoverColor )
                {
                    _ArrowHoverColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the arrow when pressed.
        /// </summary>
        /// <value>The color of the arrow pressed.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the arrow when pressed." ) ]
        public Color ArrowPressedColor
        {
            get
            {
                return _ArrowPressedColor;
            }
            set
            {
                if( value != _ArrowPressedColor )
                {
                    _ArrowPressedColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable automatic style.
        /// </summary>
        /// <value><c>true</c> if [automatic style]; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to enable automatic style." ) ]
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
        /// Gets or sets the background image displayed in the control.
        /// </summary>
        /// <value>The background image.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new Image BackgroundImage
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.
        /// </summary>
        /// <value>The background image layout.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new ImageLayout BackgroundImageLayout
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
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
                if( value != _BorderColor )
                {
                    _BorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the border when hovered.
        /// </summary>
        /// <value>The color of the border hover.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the border when hovered." ) ]
        public Color BorderHoverColor
        {
            get
            {
                return _BorderHoverColor;
            }
            set
            {
                if( value != _BorderHoverColor )
                {
                    _BorderHoverColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the border when pressed.
        /// </summary>
        /// <value>The color of the border pressed.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the border when pressed." ) ]
        public Color BorderPressedColor
        {
            get
            {
                return _BorderPressedColor;
            }
            set
            {
                if( value != _BorderPressedColor )
                {
                    _BorderPressedColor = value;
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
                if( value != _DefaultColor )
                {
                    _DefaultColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the disabled arrow.
        /// </summary>
        /// <value>The color of the disabled arrow.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the disabled arrow." ) ]
        public Color DisabledArrowColor
        {
            get
            {
                return _DisabledArrowColor;
            }
            set
            {
                if( value != _DisabledArrowColor )
                {
                    _DisabledArrowColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the control when not active.
        /// </summary>
        /// <value>The color of the disabled.</value>
        [ Category( "Appearance" ) ]
        [ Description( "sets the color of the control when not active." ) ]
        public Color DisabledColor
        {
            get
            {
                return _DisabledColor;
            }
            set
            {
                if( value != _DisabledColor )
                {
                    _DisabledColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the control when hovered.
        /// </summary>
        /// <value>The color of the hover.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the control when hovered." ) ]
        public Color HoverColor
        {
            get
            {
                return _HoverColor;
            }
            set
            {
                if( value != _HoverColor )
                {
                    _HoverColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the control when pressed.
        /// </summary>
        /// <value>The color of the pressed.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the control when pressed." ) ]
        public Color PressedColor
        {
            get
            {
                return _PressedColor;
            }
            set
            {
                if( value != _PressedColor )
                {
                    _PressedColor = value;
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
                            _BorderHoverColor = Design.BudgetColors.AccentBlue;
                            _BorderPressedColor = Design.BudgetColors.AccentBlue;
                            _DefaultColor = Design.BudgetColors.LightDefault;
                            _HoverColor = Design.BudgetColors.LightHover;
                            _PressedColor = Design.BudgetColors.AccentBlue;
                            _ArrowColor = Design.BudgetColors.LightBorder;
                            _ArrowHoverColor = Design.BudgetColors.AccentBlue;
                            _ArrowPressedColor = Design.BudgetColors.LightDefault;
                            _DisabledColor = Design.BudgetColors.LightDisabled;
                            _DisabledArrowColor = Design.BudgetColors.DisabledBorder;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _BorderHoverColor = Design.BudgetColors.AccentBlue;
                            _BorderPressedColor = Design.BudgetColors.AccentBlue;
                            _DefaultColor = Design.BudgetColors.DarkDefault;
                            _HoverColor = Design.BudgetColors.DarkHover;
                            _PressedColor = Design.BudgetColors.AccentBlue;
                            _ArrowColor = Design.BudgetColors.LightBorder;
                            _ArrowHoverColor = Design.BudgetColors.AccentBlue;
                            _ArrowPressedColor = Design.BudgetColors.LightDefault;
                            _DisabledColor = Design.BudgetColors.DarkDisabled;
                            _DisabledArrowColor = Design.BudgetColors.DisabledBorder;
                            ForeColor = Design.BudgetColors.DarkFont;
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetNavigationButton" /> class.
        /// </summary>
        public BudgetNavigationButton( )
        {
            _Style = Design.Style.Light;
            _ArrowDirection = ArrowDirection.Left;
            _ArrowPath = null;
            _CircleRect = new RectangleF( );
            _BorderColor = Design.BudgetColors.LightBorder;
            _BorderHoverColor = Design.BudgetColors.AccentBlue;
            _BorderPressedColor = Design.BudgetColors.AccentBlue;
            _DefaultColor = Design.BudgetColors.LightDefault;
            _HoverColor = Design.BudgetColors.LightHover;
            _PressedColor = Design.BudgetColors.AccentBlue;
            _ArrowColor = Design.BudgetColors.LightBorder;
            _ArrowHoverColor = Design.BudgetColors.AccentBlue;
            _ArrowPressedColor = Design.BudgetColors.LightDefault;
            _DisabledColor = Design.BudgetColors.LightDisabled;
            _DisabledArrowColor = Design.BudgetColors.DisabledBorder;
            _MouseState = Helpers.MouseState.None;
            _AutoStyle = true;
            Font = new Font( "Segoe UI", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            BackColor = Color.Transparent;
            Size = new Size( 24, 24 );
            _CircleRect = new RectangleF( 2.5f, 2.5f, 18f, 18f );
            _ArrowPath = new GraphicsPath( FillMode.Alternate );
            var pointF = new PointF( 7.5f, 11.5f );
            var graphicsPath = _ArrowPath;
            var pointF1 = new PointF( 11.5f, 15.5f );
            graphicsPath.AddLine( pointF1, pointF );
            var graphicsPath1 = _ArrowPath;
            pointF1 = new PointF( 11.5f, 7.5f );
            graphicsPath1.AddLine( pointF, pointF1 );
            _ArrowPath.StartFigure( );
            var graphicsPath2 = _ArrowPath;
            pointF1 = new PointF( 16.5f, 11.5f );
            graphicsPath2.AddLine( pointF, pointF1 );
        }

        #endregion

        #region Methods and Overrides

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
                    Style = ( (BudgetForm)FindForm( ) ).Style;
                    Invalidate( );
                }
            }

            base.OnBackColorChanged( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus( EventArgs e )
        {
            _MouseState = Helpers.MouseState.Over;
            Invalidate( );
            base.OnGotFocus( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLeave( EventArgs e )
        {
            _MouseState = Helpers.MouseState.None;
            Invalidate( );
            base.OnLeave( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLostFocus( EventArgs e )
        {
            _MouseState = Helpers.MouseState.None;
            Invalidate( );
            base.OnLostFocus( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown( MouseEventArgs e )
        {
            _MouseState = Helpers.MouseState.Pressed;
            Invalidate( );
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
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            var num = Math.Min( Width, Height );
            var single = num / 24f;

            graphics.Transform = new Matrix( single, 0f, 0f, single, checked( Width - num ) / 2f,
                checked( Height - num ) / 2f );

            using( var pen = new Pen( _BorderColor, 1.5f ) )
            {
                using( var solidBrush = new SolidBrush( _DefaultColor ) )
                {
                    var color = _ArrowColor;

                    switch( _MouseState )
                    {
                        case Helpers.MouseState.None:
                        {
                            pen.Color = _BorderColor;
                            solidBrush.Color = _DefaultColor;
                            color = _ArrowColor;
                            break;
                        }
                        case Helpers.MouseState.Over:
                        {
                            pen.Color = _BorderHoverColor;
                            solidBrush.Color = _HoverColor;
                            color = _ArrowHoverColor;
                            break;
                        }
                        case Helpers.MouseState.Pressed:
                        {
                            pen.Color = _BorderPressedColor;
                            solidBrush.Color = _PressedColor;
                            color = _ArrowPressedColor;
                            break;
                        }
                    }

                    if( !Enabled )
                    {
                        pen.Color = _DisabledArrowColor;
                        solidBrush.Color = _DisabledColor;
                        color = _DisabledArrowColor;
                    }

                    graphics.FillEllipse( solidBrush, _CircleRect );
                    graphics.DrawEllipse( pen, _CircleRect );
                    var arrowDirection = _ArrowDirection;

                    if( arrowDirection == ArrowDirection.Right )
                    {
                        graphics.MultiplyTransform( new Matrix( -1f, 0f, 0f, 1f, 23f,
                            0f ) );
                    }
                    else if( arrowDirection == ArrowDirection.Up )
                    {
                        graphics.MultiplyTransform( new Matrix( 0f, 1f, -1f, 0f, 23f,
                            0f ) );
                    }
                    else if( arrowDirection == ArrowDirection.Down )
                    {
                        graphics.RotateTransform( -90f );
                        graphics.TranslateTransform( -23f, 0f );
                    }

                    pen.Width = 2f;
                    pen.Color = color;
                    graphics.DrawPath( pen, _ArrowPath );
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Occurs when [arrow direction changed].
        /// </summary>
        [ Category( "Behavior" ) ]
        [ Description( "Wird ausgelöst sobald die Pfeilausrichtung verändert wurde." ) ]
        public event ArrowDirectionChangedEventHandler ArrowDirectionChanged;

        /// <summary>
        /// Delegate ArrowDirectionChangedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void ArrowDirectionChangedEventHandler( object sender, EventArgs e );

        #endregion
    }
}