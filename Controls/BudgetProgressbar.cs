// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetProgressbar.cs" company="Terry D. Eppler">
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
//   BudgetProgressbar.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering a metro-style progress bar.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "ProgressChanged" ) ]
    [ Description( "This is a control that executes the functions of progress bar." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( ProgressBar ) ) ]
    [ Designer( typeof( BudgetProgressbarDesigner ) ) ]
    public class BudgetProgressbar : Control
    {
        #region Private Fields

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The orientation
        /// </summary>
        private Orientation _Orientation;

        /// <summary>
        /// The value
        /// </summary>
        private int _Value;

        /// <summary>
        /// The maximum
        /// </summary>
        private int _Maximum;

        /// <summary>
        /// The progress color
        /// </summary>
        private Color _ProgressColor;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The gradient color
        /// </summary>
        private Color _gradientColor;

        /// <summary>
        /// The draw border
        /// </summary>
        private bool _DrawBorder;

        /// <summary>
        /// The show value as text
        /// </summary>
        private bool _ShowValueAsText;

        /// <summary>
        /// The use gradient
        /// </summary>
        private bool _UseGradient;

        /// <summary>
        /// The special symbol
        /// </summary>
        private string _SpecialSymbol;

        /// <summary>
        /// The is round
        /// </summary>
        private bool _IsRound;

        /// <summary>
        /// The rounding arc
        /// </summary>
        private int _RoundingArc;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to enable automatic style.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
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
        /// Gets or sets a value indicating whether to draw border.
        /// </summary>
        /// <value><c>true</c> if [draw border]; otherwise, <c>false</c>.</value>
        [ Category( "Appereance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw border." ) ]
        public bool DrawBorder
        {
            get
            {
                return _DrawBorder;
            }
            set
            {
                if( _DrawBorder != value )
                {
                    _DrawBorder = value;
                }
            }
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
                return _gradientColor;
            }
            set
            {
                if( value != _gradientColor )
                {
                    _gradientColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this control is rounded.
        /// </summary>
        /// <value><c>true</c> if this control is rounded; otherwise, <c>false</c>.</value>
        [ Category( "Appereance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether this control is rounded." ) ]
        public bool IsRound
        {
            get
            {
                return _IsRound;
            }
            set
            {
                if( _IsRound != value )
                {
                    _IsRound = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 100 ) ]
        [ Description( "Sets the maximum value." ) ]
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
                    _Maximum = value;

                    if( _Value > Maximum )
                    {
                        _Value = _Maximum;
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the orientation." ) ]
        public Orientation Orientation
        {
            get
            {
                return _Orientation;
            }
            set
            {
                if( value != _Orientation )
                {
                    var height = Height;
                    Height = Width;
                    Width = height;
                    _Orientation = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the progress.
        /// </summary>
        /// <value>The color of the progress.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the progress." ) ]
        public Color ProgressColor
        {
            get
            {
                return _ProgressColor;
            }
            set
            {
                if( value != _ProgressColor )
                {
                    _ProgressColor = value;
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
        /// Gets or sets a value indicating whether to show value as text.
        /// </summary>
        /// <value><c>true</c> if show value as text; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to show value as text." ) ]
        public bool ShowValueAsText
        {
            get
            {
                return _ShowValueAsText;
            }
            set
            {
                if( value != _ShowValueAsText )
                {
                    _ShowValueAsText = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the special symbol.
        /// </summary>
        /// <value>The special symbol.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( "" ) ]
        [ Description( "Sets the special symbol." ) ]
        public string SpecialSymbol
        {
            get
            {
                return _SpecialSymbol;
            }
            set
            {
                if( Operators.CompareString( value, _SpecialSymbol, false ) != 0 )
                {
                    _SpecialSymbol = value;
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
                            _ProgressColor = Design.BudgetColors.AccentBlue;
                            _DefaultColor = Design.BudgetColors.LightDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;

                            _gradientColor =
                                Design.BudgetColors.ChangeColorBrightness( _ProgressColor, -0.2f );

                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _ProgressColor = Design.BudgetColors.AccentBlue;
                            _DefaultColor = Design.BudgetColors.DarkDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;

                            _gradientColor =
                                Design.BudgetColors.ChangeColorBrightness( _ProgressColor, -0.2f );

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
        /// Gets or sets the current value.
        /// </summary>
        /// <value>The value.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 50 ) ]
        [ Description( "Sets the initial value." ) ]
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
                    if( value <= _Maximum )
                    {
                        _Value = value;
                    }
                    else
                    {
                        _Value = _Maximum;
                    }

                    var progressChangedEventHandler = ProgressChanged;

                    if( progressChangedEventHandler != null )
                    {
                        progressChangedEventHandler( this, _Value );
                    }

                    Invalidate( );
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetProgressbar" /> class.
        /// </summary>
        public BudgetProgressbar( )
        {
            _Style = Design.Style.Light;
            _Orientation = Orientation.Horizontal;
            _Value = 50;
            _Maximum = 100;
            _ProgressColor = Design.BudgetColors.AccentBlue;
            _DefaultColor = Design.BudgetColors.LightDefault;
            _BorderColor = Design.BudgetColors.LightBorder;
            _gradientColor = Design.BudgetColors.ChangeColorBrightness( _ProgressColor, -0.2f );
            _DrawBorder = true;
            _ShowValueAsText = false;
            _UseGradient = true;
            _SpecialSymbol = "%";
            _IsRound = false;
            _RoundingArc = Height;
            _AutoStyle = true;
            Size = new Size( 200, 25 );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
        }

        /// <summary>
        /// Gets the progress.
        /// </summary>
        /// <param name="orientation">The orientation.</param>
        /// <returns>System.Int32.</returns>
        private int GetProgress( Orientation orientation )
        {
            int num;

            num = orientation != Orientation.Horizontal
                ? checked( checked( (int)Math.Round( _Value
                        / (double)_Maximum
                        * checked( Height - 2 ) ) )
                    + 1 )
                : checked( checked( (int)Math.Round( _Value
                        / (double)_Maximum
                        * checked( Width - 2 ) ) )
                    + 1 );

            return num;
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
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            Rectangle rectangle;
            var graphics = e.Graphics;

            if( !_IsRound )
            {
                graphics.Clear( _DefaultColor );
            }
            else
            {
                using( var solidBrush = new SolidBrush( _DefaultColor ) )
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    rectangle = new Rectangle( 0, 0, checked( Width - 1 ), checked( Height - 1 ) );

                    Design.Drawing.FillRoundedPath( graphics, solidBrush, rectangle, _RoundingArc,
                        true, true, true, true );
                }
            }

            var point = new Point( 0, checked( (int)Math.Round( (double)Height / 2 ) ) );
            var point1 = new Point( Width, checked( (int)Math.Round( (double)Height / 2 ) ) );

            var linearGradientBrush =
                new LinearGradientBrush( point, point1, _ProgressColor, _gradientColor );

            if( _Orientation == Orientation.Vertical )
            {
                point1 = new Point( checked( (int)Math.Round( (double)Width / 2 ) ), 0 );
                point = new Point( checked( (int)Math.Round( (double)Width / 2 ) ), Height );

                linearGradientBrush =
                    new LinearGradientBrush( point1, point, _ProgressColor, _gradientColor );
            }

            using( linearGradientBrush )
            {
                if( !_UseGradient )
                {
                    Color[ ] colorArray =
                    {
                        _ProgressColor,
                        _ProgressColor
                    };

                    linearGradientBrush.LinearColors = colorArray;
                }

                var progress = GetProgress( _Orientation );

                if( progress > 0 )
                {
                    if( !_IsRound )
                    {
                        switch( _Orientation )
                        {
                            case Orientation.Horizontal:
                            {
                                rectangle = new Rectangle( 0, 0, progress, checked( Height - 1 ) );
                                graphics.FillRectangle( linearGradientBrush, rectangle );
                                break;
                            }
                            case Orientation.Vertical:
                            {
                                rectangle = new Rectangle( 0, 1, Width, progress );
                                graphics.FillRectangle( linearGradientBrush, rectangle );
                                break;
                            }
                        }
                    }
                    else
                    {
                        switch( _Orientation )
                        {
                            case Orientation.Horizontal:
                            {
                                rectangle = new Rectangle( 0, 0, progress, checked( Height - 1 ) );

                                Design.Drawing.FillRoundedPath( graphics, linearGradientBrush,
                                    rectangle, _RoundingArc, true, true, true,
                                    true );

                                break;
                            }
                            case Orientation.Vertical:
                            {
                                rectangle = new Rectangle( 0, 1, Width, progress );

                                Design.Drawing.FillRoundedPath( graphics, linearGradientBrush,
                                    rectangle, _RoundingArc, true, true, true,
                                    true );

                                break;
                            }
                        }
                    }
                }
            }

            if( _DrawBorder )
            {
                using( var pen = new Pen( _BorderColor ) )
                {
                    if( !_IsRound )
                    {
                        rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                            checked( Height - 1 ) );

                        graphics.DrawRectangle( pen, rectangle );
                    }
                    else
                    {
                        var color = pen.Color;

                        rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                            checked( Height - 1 ) );

                        Design.Drawing.DrawRoundedPath( graphics, color, 1f, rectangle,
                            _RoundingArc, true, true, true, true );
                    }
                }
            }

            if( _ShowValueAsText )
            {
                var stringFormat = new StringFormat( )
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                using( var stringFormat1 = stringFormat )
                {
                    using( var solidBrush1 = new SolidBrush(
                              Design.BudgetColors.GetCorrectForeColor( _Style, ForeColor,
                                  Enabled ) ) )
                    {
                        var str = string.Concat( _Value.ToString( ), _SpecialSymbol );
                        var font = Font;

                        rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                            checked( Height - 1 ) );

                        graphics.DrawString( str, font, solidBrush1, rectangle, stringFormat1 );
                    }
                }
            }

            linearGradientBrush.Dispose( );
            base.OnPaint( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize( EventArgs e )
        {
            if( DesignMode )
            {
                _RoundingArc = Height;
            }

            base.OnResize( e );
        }

        /// <summary>
        /// Occurs when [progress changed].
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Delegate ProgressChangedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="Value">The value.</param>
        public delegate void ProgressChangedEventHandler( object sender, int Value );
    }
}