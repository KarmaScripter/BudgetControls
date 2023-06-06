// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTrackbar.cs" company="Terry D. Eppler">
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
//   BudgetTrackbar.cs
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
    /// A class collection for rendering a metro-style trackbar.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "Scroll" ) ]
    [ Description( "A control for rendering a metro-style tracker." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( TrackBar ) ) ]
    [ Designer( typeof( BudgetTrackbarDesigner ) ) ]
    public class BudgetTrackbar : Control
    {
        #region Private Fields

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The is scrolling
        /// </summary>
        private int _IsScrolling;

        /// <summary>
        /// The SND slider position
        /// </summary>
        private int _SndSliderPos;

        /// <summary>
        /// The slider position
        /// </summary>
        private int _SliderPos;

        /// <summary>
        /// The slider style
        /// </summary>
        private BudgetSliderStyle _SliderStyle;

        /// <summary>
        /// The single slider
        /// </summary>
        private bool _SingleSlider;

        /// <summary>
        /// The value
        /// </summary>
        private int _Value;

        /// <summary>
        /// The second value
        /// </summary>
        private int _SecondValue;

        /// <summary>
        /// The rail width
        /// </summary>
        private int _RailWidth;

        /// <summary>
        /// The slider width
        /// </summary>
        private int _SliderWidth;

        /// <summary>
        /// The use gradient
        /// </summary>
        private bool _UseGradient;

        /// <summary>
        /// The maximum
        /// </summary>
        private int _Maximum;

        /// <summary>
        /// The minimum
        /// </summary>
        private int _Minimum;

        /// <summary>
        /// The left color
        /// </summary>
        private Color _LeftColor;

        /// <summary>
        /// The right color
        /// </summary>
        private Color _RightColor;

        /// <summary>
        /// The slider color
        /// </summary>
        private Color _SliderColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The region color
        /// </summary>
        private Color _RegionColor;

        /// <summary>
        /// The gradient color
        /// </summary>
        private Color _GradientColor;

        /// <summary>
        /// The rounding arc
        /// </summary>
        private int _RoundingArc;

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
        /// Gets or sets a value indicating whether to enable/disable automatic style.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to enable/disable automatic style." ) ]
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
        /// Gets or sets the gradient color.
        /// </summary>
        /// <value>The gradient.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the gradient color." ) ]
        public Color GradientColor
        {
            get
            {
                return _GradientColor;
            }
            set
            {
                if( value != _GradientColor )
                {
                    _GradientColor = value;
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
        /// Gets or sets the left color.
        /// </summary>
        /// <value>The left color.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Die linke Farbe des Steuerelements." ) ]
        public Color LeftColor
        {
            get
            {
                return _LeftColor;
            }
            set
            {
                if( value != _LeftColor )
                {
                    _LeftColor = value;
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
                if( value != _Maximum )
                {
                    if( _Value > _Maximum )
                    {
                        _Value = _Maximum;
                    }

                    if( _Minimum < _Maximum )
                    {
                        _Maximum = value;
                        Invalidate( );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the minimum value." ) ]
        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                if( value != _Minimum )
                {
                    if( _Value < _Minimum )
                    {
                        _Value = _Minimum;
                    }

                    if( _Minimum <= _Maximum )
                    {
                        _Minimum = value;
                        Invalidate( );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the rail.
        /// </summary>
        /// <value>The width of the rail.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 5 ) ]
        [ Description( "Sets the width of the rail." ) ]
        public int RailWidth
        {
            get
            {
                return _RailWidth;
            }
            set
            {
                if( value != _RailWidth )
                {
                    _RailWidth = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the region.
        /// </summary>
        /// <value>The color of the region.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the region." ) ]
        public Color RegionColor
        {
            get
            {
                return _RegionColor;
            }
            set
            {
                if( value != _RegionColor )
                {
                    _RegionColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the right color.
        /// </summary>
        /// <value>The color of the right.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the right color." ) ]
        public Color RightColor
        {
            get
            {
                return _RightColor;
            }
            set
            {
                if( value != _RightColor )
                {
                    _RightColor = value;
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
        /// Gets or sets the rounding arc.
        /// </summary>
        /// <value>The rounding arc.</value>
        [ Browsable( true ) ]
        [ Category( "Appereance" ) ]
        [ DefaultValue( 5 ) ]
        [ Description( "Sets the rounding arc." ) ]
        public int RoundingArc
        {
            get
            {
                return _RoundingArc;
            }
            set
            {
                if( _RoundingArc != value )
                {
                    _RoundingArc = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the second value.
        /// </summary>
        /// <value>The second value.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 30 ) ]
        [ Description( "Sets the second value." ) ]
        public int SecondValue
        {
            get
            {
                return _SecondValue;
            }
            set
            {
                if( value != _Value )
                {
                    if( _SecondValue < _Minimum )
                    {
                        _SecondValue = _Minimum;
                    }
                    else if( _SecondValue > _Maximum )
                    {
                        _SecondValue = _Maximum;
                    }
                    else if( !( !_SingleSlider & _SecondValue > _Value ) )
                    {
                        _SecondValue = value;
                    }
                    else
                    {
                        _SecondValue = _Value;
                    }

                    _SndSliderPos = checked( (int)Math.Round(
                        (float)( checked( _SecondValue - _Minimum )
                            / (double)checked( _Maximum - _Minimum ) )
                        * checked( Width - _SliderWidth ) ) );

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets the selected region.
        /// </summary>
        /// <value>The selected region.</value>
        [ Category( "Data" ) ]
        [ Description( "Gets the selected region." ) ]
        [ TypeConverter( typeof( ExpandableObjectConverter ) ) ]
        public TrackbarRegion SelectedRegion
        {
            get
            {
                TrackbarRegion trackbarRegion;
                TrackbarRegion trackbarRegion1;

                if( _SingleSlider )
                {
                    trackbarRegion1 = new TrackbarRegion( _Value, _Value );
                    trackbarRegion = trackbarRegion1;
                }
                else
                {
                    trackbarRegion1 = new TrackbarRegion( _SndSliderPos, _Value );
                    trackbarRegion = trackbarRegion1;
                }

                return trackbarRegion;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether single slider is enabled/disabled.
        /// </summary>
        /// <value><c>true</c> if [single slider]; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether single slider is enabled/disabled." ) ]
        public bool SingleSlider
        {
            get
            {
                return _SingleSlider;
            }
            set
            {
                if( value != _SingleSlider )
                {
                    _SingleSlider = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the slider.
        /// </summary>
        /// <value>The color of the slider.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the slider." ) ]
        public Color SliderColor
        {
            get
            {
                return _SliderColor;
            }
            set
            {
                if( value != _SliderColor )
                {
                    _SliderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the slider style.
        /// </summary>
        /// <value>The slider style.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the slider style." ) ]
        public BudgetSliderStyle SliderStyle
        {
            get
            {
                return _SliderStyle;
            }
            set
            {
                if( value != _SliderStyle )
                {
                    _SliderStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the slider.
        /// </summary>
        /// <value>The width of the slider.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 10 ) ]
        [ Description( "Sets the width of the slider." ) ]
        public int SliderWidth
        {
            get
            {
                return _SliderWidth;
            }
            set
            {
                if( value != _SliderWidth )
                {
                    _SliderWidth = value;
                    Invalidate( );
                }
            }
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
                            _LeftColor = Design.BudgetColors.AccentBlue;
                            _RightColor = Design.BudgetColors.LightSwitchRail;
                            _SliderColor = Design.BudgetColors.LightDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _HoverColor = Design.BudgetColors.AccentLightBlue;

                            _GradientColor =
                                Design.BudgetColors.ChangeColorBrightness( _LeftColor, -0.2f );

                            _RegionColor = Design.BudgetColors.AccentLightBlue;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _LeftColor = Design.BudgetColors.AccentBlue;
                            _RightColor = Design.BudgetColors.DarkHover;
                            _SliderColor = Design.BudgetColors.DarkDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _HoverColor = Design.BudgetColors.AccentLightBlue;

                            _GradientColor =
                                Design.BudgetColors.ChangeColorBrightness( _LeftColor, -0.2f );

                            _RegionColor = Design.BudgetColors.AccentLightBlue;
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
        /// Gets or sets a value indicating whether to use gradient.
        /// </summary>
        /// <value><c>true</c> if use gradient; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to use gradient." ) ]
        public bool UseGradient
        {
            get
            {
                return _UseGradient;
            }
            set
            {
                if( value != _UseGradient )
                {
                    _UseGradient = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 50 ) ]
        [ Description( "Sets the value." ) ]
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if( value != _Value )
                {
                    if( _Value < _Minimum )
                    {
                        _Value = _Minimum;
                    }
                    else if( _Value > _Maximum )
                    {
                        _Value = _Maximum;
                    }
                    else if( !( !_SingleSlider & _Value < _SecondValue ) )
                    {
                        _Value = value;
                    }
                    else
                    {
                        _Value = _SecondValue;
                    }

                    _SliderPos = checked( (int)Math.Round(
                        (float)( checked( _Value - _Minimum )
                            / (double)checked( _Maximum - _Minimum ) )
                        * checked( Width - _SliderWidth ) ) );

                    Invalidate( );
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTrackbar" /> class.
        /// </summary>
        public BudgetTrackbar( )
        {
            _Style = Design.Style.Light;
            _IsScrolling = 0;
            _SndSliderPos = 10;
            _SliderPos = 0;
            _SliderStyle = BudgetSliderStyle.Rectangular;
            _SingleSlider = true;
            _Value = 50;
            _SecondValue = 30;
            _RailWidth = 5;
            _SliderWidth = 10;
            _UseGradient = true;
            _Maximum = 100;
            _Minimum = 0;
            _LeftColor = Design.BudgetColors.AccentBlue;
            _RightColor = Design.BudgetColors.LightSwitchRail;
            _SliderColor = Design.BudgetColors.LightDefault;
            _BorderColor = Design.BudgetColors.LightBorder;
            _HoverColor = Design.BudgetColors.AccentLightBlue;
            _RegionColor = Design.BudgetColors.AccentLightBlue;
            _GradientColor = Design.BudgetColors.ChangeColorBrightness( _LeftColor, -0.2f );
            _RoundingArc = 5;
            _MouseState = Helpers.MouseState.None;
            _AutoStyle = true;
            Font = new Font( "Roboto", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            BackColor = Color.Transparent;
        }

        /// <summary>
        /// Moves the slider.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MoveSlider( MouseEventArgs e )
        {
            ScrollEventHandler scrollEventHandler;

            if( _IsScrolling == 1 )
            {
                if( !_SingleSlider )
                {
                    _SliderPos = checked( e.X - SliderWidth / 2 );

                    if( _SliderPos < _SndSliderPos )
                    {
                        _SliderPos = _SndSliderPos;
                    }

                    if( _SliderPos > checked( Width - SliderWidth ) )
                    {
                        _SliderPos = checked( Width - SliderWidth );
                    }

                    _Value = checked( Convert.ToInt32( _SliderPos
                            / (double)checked( Width - SliderWidth )
                            * checked( _Maximum - _Minimum ) )
                        + _Minimum );
                }
                else
                {
                    _SliderPos = checked( e.X - _SliderWidth / 2 );

                    if( _SliderPos < 0 )
                    {
                        _SliderPos = 0;
                    }

                    if( _SliderPos > checked( Width - _SliderWidth ) )
                    {
                        _SliderPos = checked( Width - _SliderWidth );
                    }

                    _Value = checked( Convert.ToInt32( _SliderPos
                            / (double)checked( Width - _SliderWidth )
                            * checked( _Maximum - _Minimum ) )
                        + _Minimum );
                }

                scrollEventHandler = Scroll;

                if( scrollEventHandler != null )
                {
                    scrollEventHandler( this, new TrackbarEventArgs( true, Value ) );
                }
            }
            else if( _IsScrolling == 2 )
            {
                _SndSliderPos = checked( e.X - SliderWidth / 2 );

                if( _SndSliderPos < 0 )
                {
                    _SndSliderPos = 0;
                }

                if( _SndSliderPos > _SliderPos )
                {
                    _SndSliderPos = _SliderPos;
                }

                _SecondValue = checked( Convert.ToInt32( _SndSliderPos
                        / (double)checked( Width - SliderWidth )
                        * checked( _Maximum - _Minimum ) )
                    + _Minimum );

                scrollEventHandler = Scroll;

                if( scrollEventHandler != null )
                {
                    scrollEventHandler( this, new TrackbarEventArgs( false, _SecondValue ) );
                }
            }

            Invalidate( );
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
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseClick( MouseEventArgs e )
        {
            MoveSlider( e );
            base.OnMouseClick( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown( MouseEventArgs e )
        {
            bool flag;
            _MouseState = Helpers.MouseState.Pressed;

            if( e.Button == MouseButtons.Left )
            {
                if( !_SingleSlider )
                {
                    if( e.X >= _SliderPos
                       && e.X < checked( _SliderPos + SliderWidth ) )
                    {
                        if( _SliderPos == checked( Width - SliderWidth )
                               ? _SliderPos <= checked( _SndSliderPos + SliderWidth )
                               : false )
                        {
                            goto Label1;
                        }

                        flag = true;
                    }

                    Label1:
                    flag = false;

                    if( flag )
                    {
                        _IsScrolling = 1;
                    }
                    else if( e.X < _SndSliderPos || e.X >= checked( _SndSliderPos + SliderWidth )
                                ? false
                                : true )
                    {
                        _IsScrolling = 2;
                    }
                }
                else
                {
                    _IsScrolling = 1;
                }
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
            MoveSlider( e );
            base.OnMouseMove( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp( MouseEventArgs e )
        {
            _MouseState = Helpers.MouseState.Over;

            if( e.Button == MouseButtons.Left )
            {
                _IsScrolling = 0;
            }

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
            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            var point = new Point( 0, checked( (int)Math.Round( (double)Height / 2 ) ) );
            var point1 = new Point( Width, checked( (int)Math.Round( (double)Height / 2 ) ) );

            using( var linearGradientBrush =
                  new LinearGradientBrush( point, point1, _LeftColor, _GradientColor ) )
            {
                var rectangle = new Rectangle( 0,
                    checked( checked( checked( (int)Math.Round( (double)Height / 2 ) )
                            - checked( (int)Math.Round( (double)_RailWidth / 2 ) ) )
                        - 1 ), _SliderPos, _RailWidth );

                graphics.FillRectangle( linearGradientBrush, rectangle );

                Color[ ] colorArray =
                {
                    _RightColor,
                    _RightColor
                };

                linearGradientBrush.LinearColors = colorArray;

                rectangle = new Rectangle( checked( _SliderPos + _SliderWidth ),
                    checked( checked( checked( (int)Math.Round( (double)Height / 2 ) )
                            - checked( (int)Math.Round( (double)_RailWidth / 2 ) ) )
                        - 1 ), checked( checked( Width - _SliderWidth ) + _SliderWidth ),
                    _RailWidth );

                graphics.FillRectangle( linearGradientBrush, rectangle );

                if( !_SingleSlider )
                {
                    colorArray = new[ ]
                    {
                        _RegionColor,
                        _RegionColor
                    };

                    linearGradientBrush.LinearColors = colorArray;

                    rectangle = new Rectangle(
                        checked( checked( _SndSliderPos + SliderWidth ) - 1 ),
                        checked( (int)Math.Round( (double)checked( Height - _RailWidth ) / 2 ) ),
                        checked( _SliderPos - _SndSliderPos ), _RailWidth );

                    graphics.FillRectangle( linearGradientBrush, rectangle );
                }

                colorArray = new[ ]
                {
                    _SliderColor,
                    _SliderColor
                };

                linearGradientBrush.LinearColors = colorArray;

                using( var pen = new Pen( _BorderColor ) )
                {
                    if( _MouseState == Helpers.MouseState.Over
                       || _MouseState == Helpers.MouseState.Pressed
                           ? true
                           : false )
                    {
                        pen.Color = _HoverColor;
                    }

                    switch( _SliderStyle )
                    {
                        case BudgetSliderStyle.Rectangular:
                        {
                            rectangle = new Rectangle( _SliderPos, 0, _SliderWidth, Height );
                            graphics.FillRectangle( linearGradientBrush, rectangle );

                            rectangle = new Rectangle( _SliderPos, 0, checked( _SliderWidth - 1 ),
                                checked( Height - 1 ) );

                            graphics.DrawRectangle( pen, rectangle );

                            if( !_SingleSlider )
                            {
                                rectangle = new Rectangle( _SndSliderPos, 0, _SliderWidth, Height );
                                graphics.FillRectangle( linearGradientBrush, rectangle );

                                rectangle = new Rectangle( _SndSliderPos, 0,
                                    checked( _SliderWidth - 1 ), checked( Height - 1 ) );

                                graphics.DrawRectangle( pen, rectangle );
                            }

                            break;
                        }
                        case BudgetSliderStyle.Round:
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;

                            rectangle = new Rectangle( _SliderPos,
                                checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                    - checked( (int)Math.Round( (double)_SliderWidth / 2 ) ) ),
                                _SliderWidth, _SliderWidth );

                            graphics.FillEllipse( linearGradientBrush, rectangle );

                            rectangle = new Rectangle( _SliderPos,
                                checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                    - checked( (int)Math.Round( (double)_SliderWidth / 2 ) ) ),
                                checked( _SliderWidth - 1 ), checked( _SliderWidth - 1 ) );

                            graphics.DrawEllipse( pen, rectangle );

                            if( !_SingleSlider )
                            {
                                rectangle = new Rectangle( _SndSliderPos,
                                    checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                        - checked( (int)Math.Round( (double)_SliderWidth / 2 ) ) ),
                                    _SliderWidth, _SliderWidth );

                                graphics.FillEllipse( linearGradientBrush, rectangle );

                                rectangle = new Rectangle( _SndSliderPos,
                                    checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                        - checked( (int)Math.Round( (double)_SliderWidth / 2 ) ) ),
                                    checked( _SliderWidth - 1 ), checked( _SliderWidth - 1 ) );

                                graphics.DrawEllipse( pen, rectangle );
                            }

                            break;
                        }
                        case BudgetSliderStyle.RoundedRectangle:
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;

                            rectangle = new Rectangle( _SliderPos, 0, _SliderWidth,
                                checked( Height - 1 ) );

                            Design.Drawing.FillRoundedPath( graphics, linearGradientBrush,
                                rectangle, _RoundingArc, true, true, true,
                                true );

                            var color = pen.Color;

                            rectangle = new Rectangle( _SliderPos, 0, _SliderWidth,
                                checked( Height - 1 ) );

                            Design.Drawing.DrawRoundedPath( graphics, color, 1f, rectangle,
                                _RoundingArc, true, true, true, true );

                            if( !_SingleSlider )
                            {
                                rectangle = new Rectangle( _SndSliderPos, 0, _SliderWidth,
                                    checked( Height - 1 ) );

                                Design.Drawing.FillRoundedPath( graphics, linearGradientBrush,
                                    rectangle, _RoundingArc, true, true, true,
                                    true );

                                var color1 = pen.Color;

                                rectangle = new Rectangle( _SndSliderPos, 0, _SliderWidth,
                                    checked( Height - 1 ) );

                                Design.Drawing.DrawRoundedPath( graphics, color1, 1f, rectangle,
                                    _RoundingArc, true, true, true, true );
                            }

                            break;
                        }
                    }
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged( EventArgs e )
        {
            SetSliderPos( );
            base.OnSizeChanged( e );
        }

        /// <summary>
        /// Sets the slider position.
        /// </summary>
        private void SetSliderPos( )
        {
            _SliderPos = Convert.ToInt32( checked( _Value - _Minimum )
                / (double)checked( _Maximum - _Minimum )
                * checked( Width - _SliderWidth ) );

            if( !_SingleSlider )
            {
                _SndSliderPos = Convert.ToInt32( checked( _SecondValue - _Minimum )
                    / (double)checked( _Maximum - _Minimum )
                    * checked( Width - _SliderWidth ) );
            }

            Invalidate( );
        }

        /// <summary>
        /// Occurs when [scroll].
        /// </summary>
        public event ScrollEventHandler Scroll;

        /// <summary>
        /// Enum BudgetSliderStyle
        /// </summary>
        public enum BudgetSliderStyle
        {
            /// <summary>
            /// The rectangular
            /// </summary>
            Rectangular,

            /// <summary>
            /// The round
            /// </summary>
            Round,

            /// <summary>
            /// The rounded rectangle
            /// </summary>
            RoundedRectangle
        }

        /// <summary>
        /// Delegate ScrollEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BudgetTrackbar.TrackbarEventArgs"/> instance containing the event data.</param>
        public delegate void ScrollEventHandler( object sender, TrackbarEventArgs e );

        /// <summary>
        /// Class TrackbarEventArgs.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class TrackbarEventArgs : EventArgs
        {
            /// <summary>
            /// The is primary slider
            /// </summary>
            private bool _IsPrimarySlider;

            /// <summary>
            /// The slider value
            /// </summary>
            private int _SliderValue;

            /// <summary>
            /// Gets a value indicating whether this instance is primary slider.
            /// </summary>
            /// <value><c>true</c> if this instance is primary slider; otherwise, <c>false</c>.</value>
            public bool IsPrimarySlider
            {
                get
                {
                    return _IsPrimarySlider;
                }
            }

            /// <summary>
            /// Gets the slider value.
            /// </summary>
            /// <value>The slider value.</value>
            public int SliderValue
            {
                get
                {
                    return _SliderValue;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TrackbarEventArgs"/> class.
            /// </summary>
            /// <param name="isPrimary">if set to <c>true</c> [is primary].</param>
            /// <param name="value">The value.</param>
            public TrackbarEventArgs( bool isPrimary, int value )
            {
                _IsPrimarySlider = isPrimary;
                _SliderValue = value;
            }
        }

        /// <summary>
        /// Struct TrackbarRegion
        /// </summary>
        public struct TrackbarRegion
        {
            /// <summary>
            /// The startpoint
            /// </summary>
            private int _Startpoint;

            /// <summary>
            /// The endpoint
            /// </summary>
            private int _Endpoint;

            /// <summary>
            /// Gets the endpoint.
            /// </summary>
            /// <value>The endpoint.</value>
            [ Category( "Data" ) ]
            [ Description( "Der Endpunkt der ausgewählten Region." ) ]
            public int Endpoint
            {
                get
                {
                    return _Endpoint;
                }
            }

            /// <summary>
            /// Gets the startpoint.
            /// </summary>
            /// <value>The startpoint.</value>
            [ Category( "Data" ) ]
            [ Description( "Der Startpunkt der ausgewählten Region." ) ]
            public int Startpoint
            {
                get
                {
                    return _Startpoint;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TrackbarRegion"/> struct.
            /// </summary>
            /// <param name="start">The start.</param>
            /// <param name="end">The end.</param>
            public TrackbarRegion( int start, int end )
            {
                this = new TrackbarRegion( )
                {
                    _Startpoint = start,
                    _Endpoint = end
                };
            }
        }
    }
}