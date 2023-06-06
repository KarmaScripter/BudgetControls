// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetPanelCategory.cs" company="Terry D. Eppler">
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
//   BudgetPanelCategory.cs
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
    /// Class BudgetPanelCategory.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Panel" />
    [ DefaultEvent( "Click" ) ]
    [ Description( "Ein Kategoriepanel-Steuerelement im Metrostil." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( Panel ) ) ]
    [ Designer( typeof( BudgetPanelCategoryDesigner ) ) ]
    public class BudgetPanelCategory : Panel
    {
        #region Private Fields

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The accent color
        /// </summary>
        private Color _AccentColor;

        /// <summary>
        /// The gradient color
        /// </summary>
        private Color _GradientColor;

        /// <summary>
        /// The line gradient color
        /// </summary>
        private Color _LineGradientColor;

        /// <summary>
        /// The line orientation
        /// </summary>
        private Orientation _LineOrientation;

        /// <summary>
        /// The line thickness
        /// </summary>
        private int _LineThickness;

        /// <summary>
        /// The use gradient
        /// </summary>
        private bool _UseGradient;

        /// <summary>
        /// The use gradient on line
        /// </summary>
        private bool _UseGradientOnLine;

        /// <summary>
        /// The text align
        /// </summary>
        private StringAlignment _TextAlign;

        /// <summary>
        /// The line align
        /// </summary>
        private StringAlignment _LineAlign;

        /// <summary>
        /// The appearance
        /// </summary>
        private PanelAppearance _Appearance;

        /// <summary>
        /// The slope a
        /// </summary>
        private int _SlopeA;

        /// <summary>
        /// The slope b
        /// </summary>
        private int _SlopeB;

        /// <summary>
        /// The rounding arc
        /// </summary>
        private int _RoundingArc;

        /// <summary>
        /// The allow form moving
        /// </summary>
        private bool _AllowFormMoving;

        /// <summary>
        /// The gradient point a
        /// </summary>
        private Point _GradientPointA;

        /// <summary>
        /// The gradient point b
        /// </summary>
        private Point _GradientPointB;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        #endregion

        #region Public Properties

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
                if( value != _AccentColor )
                {
                    _AccentColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow form moving.
        /// </summary>
        /// <value><c>true</c> if allow form moving; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to allow form moving." ) ]
        public bool AllowFormMoving
        {
            get
            {
                return _AllowFormMoving;
            }
            set
            {
                if( value != _AllowFormMoving )
                {
                    _AllowFormMoving = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the appearance.
        /// </summary>
        /// <value>The appearance.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the appearance." ) ]
        public PanelAppearance Appearance
        {
            get
            {
                return _Appearance;
            }
            set
            {
                if( value != _Appearance )
                {
                    _Appearance = value;
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
        [ Description( "sets a value indicating whether to enable automatic style." ) ]
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
                if( value != _GradientColor )
                {
                    _GradientColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the gradient point.
        /// </summary>
        /// <value>The gradient point a.</value>
        [ Browsable( false ) ]
        [ Category( "Appereance" ) ]
        [ Description( "Sets the gradient point." ) ]
        public Point GradientPointA
        {
            get
            {
                return _GradientPointA;
            }
            set
            {
                if( _GradientPointA != value )
                {
                    _GradientPointA = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the gradient point.
        /// </summary>
        /// <value>The gradient point b.</value>
        [ Browsable( false ) ]
        [ Category( "Appereance" ) ]
        [ Description( "Sets the gradient point." ) ]
        public Point GradientPointB
        {
            get
            {
                return _GradientPointB;
            }
            set
            {
                if( _GradientPointB != value )
                {
                    _GradientPointB = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the line align.
        /// </summary>
        /// <value>The line align.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 1 ) ]
        [ Description( "Sets the line align." ) ]
        public StringAlignment LineAlign
        {
            get
            {
                return _LineAlign;
            }
            set
            {
                if( value != _LineAlign )
                {
                    _LineAlign = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the line gradient.
        /// </summary>
        /// <value>The color of the line gradient.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the line gradient." ) ]
        public Color LineGradientColor
        {
            get
            {
                return _LineGradientColor;
            }
            set
            {
                if( value != _LineGradientColor )
                {
                    _LineGradientColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the line orientation.
        /// </summary>
        /// <value>The line orientation.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 1 ) ]
        [ Description( "Sets the line orientation." ) ]
        public Orientation LineOrientation
        {
            get
            {
                return _LineOrientation;
            }
            set
            {
                if( value != _LineOrientation )
                {
                    _LineOrientation = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the line thickness.
        /// </summary>
        /// <value>The line thickness.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "Sets the line thickness." ) ]
        public int LineThickness
        {
            get
            {
                return _LineThickness;
            }
            set
            {
                if( value != _LineThickness )
                {
                    _LineThickness = value;
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
        [ DefaultValue( 15 ) ]
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
        /// Gets or sets the slope.
        /// </summary>
        /// <value>The slope a.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 15 ) ]
        [ Description( "Sets the slope." ) ]
        public int SlopeA
        {
            get
            {
                return _SlopeA;
            }
            set
            {
                if( value != _SlopeA )
                {
                    _SlopeA = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the slope.
        /// </summary>
        /// <value>The slope b.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the slope." ) ]
        public int SlopeB
        {
            get
            {
                return _SlopeB;
            }
            set
            {
                if( value != _SlopeB )
                {
                    _SlopeB = value;
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
                            _DefaultColor = Design.BudgetColors.LightDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _AccentColor = Design.BudgetColors.AccentBlue;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _DefaultColor = Design.BudgetColors.DarkDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _AccentColor = Design.BudgetColors.AccentBlue;
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
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text align.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 1 ) ]
        [ Description( "Sets the text alignment." ) ]
        public StringAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                if( value != _TextAlign )
                {
                    _TextAlign = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use gradient.
        /// </summary>
        /// <value><c>true</c> if use gradient; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
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
        /// Gets or sets a value indicating whether to use gradient on line.
        /// </summary>
        /// <value><c>true</c> if use gradient on line; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Gibt an, ob für die Linie ein linearer Verlauf verwendet werden soll." ) ]
        public bool UseGradientOnLine
        {
            get
            {
                return _UseGradientOnLine;
            }
            set
            {
                if( value != _UseGradientOnLine )
                {
                    _UseGradientOnLine = value;
                    Invalidate( );
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetPanelCategory" /> class.
        /// </summary>
        public BudgetPanelCategory( )
        {
            _Style = Design.Style.Light;
            _DefaultColor = Design.BudgetColors.LightDefault;
            _BorderColor = Design.BudgetColors.LightBorder;
            _AccentColor = Design.BudgetColors.AccentBlue;
            _GradientColor = _DefaultColor;
            _LineGradientColor = Design.BudgetColors.ChangeColorBrightness( _AccentColor, -0.2f );
            _LineOrientation = Orientation.Vertical;
            _LineThickness = 2;
            _UseGradient = false;
            _UseGradientOnLine = true;
            _TextAlign = StringAlignment.Center;
            _LineAlign = StringAlignment.Center;
            _Appearance = PanelAppearance.Category;
            _SlopeA = 15;
            _SlopeB = 0;
            _RoundingArc = 15;
            _AllowFormMoving = false;
            _GradientPointA = new Point( 0, 0 );
            _GradientPointB = new Point( Width, Height );
            _AutoStyle = true;
            Font = new Font( "Segoe UI", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            DoubleBuffered = true;
            BackColor = Color.Transparent;
            UpdateStyles( );
        }

        /// <summary>
        /// Gets the sloped rectangle.
        /// </summary>
        /// <param name="bottom">if set to <c>true</c> [bottom].</param>
        /// <returns>Point[].</returns>
        private Point[ ] GetSlopedRectangle( bool bottom )
        {
            Point[ ] pointArray;
            Point point;
            Point point1;
            Point point2;
            Point point3;
            Point[ ] pointArray1;

            if( !bottom )
            {
                pointArray1 = new Point[ 4 ];
                point3 = new Point( 0, SlopeA );
                pointArray1[ 0 ] = point3;
                point2 = new Point( checked( Width - 1 ), SlopeB );
                pointArray1[ 1 ] = point2;
                point1 = new Point( checked( Width - 1 ), checked( Height - 1 ) );
                pointArray1[ 2 ] = point1;
                point = new Point( 0, checked( Height - 1 ) );
                pointArray1[ 3 ] = point;
                pointArray = pointArray1;
            }
            else
            {
                pointArray1 = new Point[ 4 ];
                point = new Point( 0, 0 );
                pointArray1[ 0 ] = point;
                point1 = new Point( checked( Width - 1 ), 0 );
                pointArray1[ 1 ] = point1;

                point2 = new Point( checked( Width - 1 ),
                    checked( checked( Height - 1 ) - SlopeB ) );

                pointArray1[ 2 ] = point2;
                point3 = new Point( 0, checked( checked( Height - 1 ) - SlopeA ) );
                pointArray1[ 3 ] = point3;
                pointArray = pointArray1;
            }

            return pointArray;
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
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown( MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {
                if( _AllowFormMoving )
                {
                    Capture = false;

                    try
                    {
                        var message = Message.Create( FindForm( ).Handle, 161, (IntPtr)2,
                            IntPtr.Zero );

                        WndProc( ref message );
                    }
                    catch( Exception exception )
                    {
                        ProjectData.SetProjectError( exception );
                        ProjectData.ClearProjectError( );
                    }
                }
            }

            base.OnMouseDown( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            Point point;
            Point point1;
            Rectangle rectangle;
            var graphics = e.Graphics;
            var pen = new Pen( _BorderColor );

            Brush linearGradientBrush = new LinearGradientBrush( _GradientPointA, _GradientPointB,
                _DefaultColor, _UseGradient
                    ? _GradientColor
                    : _DefaultColor );

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            switch( _Appearance )
            {
                case PanelAppearance.Category:
                {
                    graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    graphics.FillRectangle( linearGradientBrush, ClientRectangle );

                    graphics.DrawRectangle( pen, 0, 0, checked( Width - 1 ),
                        checked( Height - 1 ) );

                    pen.Color = _AccentColor;
                    pen.Width = _LineThickness;

                    switch( _LineOrientation )
                    {
                        case Orientation.Horizontal:
                        {
                            point1 = new Point( 0, 0 );
                            var point2 = point1;
                            point = new Point( Width, 0 );

                            linearGradientBrush = new LinearGradientBrush( point2, point,
                                _AccentColor, _UseGradientOnLine
                                    ? _LineGradientColor
                                    : _AccentColor );

                            graphics.FillRectangle( linearGradientBrush, 0, 0, Width,
                                _LineThickness );

                            break;
                        }
                        case Orientation.Vertical:
                        {
                            point = new Point( 0, 0 );
                            var point3 = point;
                            point1 = new Point( 0, Height );

                            linearGradientBrush = new LinearGradientBrush( point3, point1,
                                _AccentColor, _UseGradientOnLine
                                    ? _LineGradientColor
                                    : _AccentColor );

                            graphics.FillRectangle( linearGradientBrush, 0, 0, _LineThickness,
                                Height );

                            break;
                        }
                    }

                    break;
                }
                case PanelAppearance.Rounded:
                {
                    rectangle = new Rectangle( 0, 0, checked( Width - 1 ), checked( Height - 1 ) );

                    Design.Drawing.FillRoundedPath( graphics, linearGradientBrush, rectangle,
                        _RoundingArc, true, true, true, true );

                    var color = pen.Color;
                    rectangle = new Rectangle( 0, 0, checked( Width - 1 ), checked( Height - 1 ) );

                    Design.Drawing.DrawRoundedPath( graphics, color, 1f, rectangle, _RoundingArc,
                        true, true, true, true );

                    break;
                }
                case PanelAppearance.SlopedBottom:
                {
                    graphics.FillPolygon( linearGradientBrush, GetSlopedRectangle( true ) );
                    graphics.DrawPolygon( pen, GetSlopedRectangle( true ) );
                    break;
                }
                case PanelAppearance.SlopedTop:
                {
                    graphics.FillPolygon( linearGradientBrush, GetSlopedRectangle( false ) );
                    graphics.DrawPolygon( pen, GetSlopedRectangle( false ) );
                    break;
                }
            }

            if( linearGradientBrush != null )
            {
                linearGradientBrush.Dispose( );
            }

            pen.Dispose( );

            using( var solidBrush = new SolidBrush( ForeColor ) )
            {
                var stringFormat = new StringFormat( )
                {
                    Alignment = _TextAlign,
                    LineAlignment = _LineAlign
                };

                using( var stringFormat1 = stringFormat )
                {
                    if( _LineOrientation != Orientation.Vertical )
                    {
                        var text = Text;
                        var font = Font;

                        rectangle = new Rectangle( 0, _LineThickness, checked( Width - 1 ),
                            checked( Height - checked( _LineThickness + 1 ) ) );

                        graphics.DrawString( text, font, solidBrush, rectangle, stringFormat1 );
                    }
                    else
                    {
                        var str = Text;
                        var font1 = Font;

                        rectangle = new Rectangle( _LineThickness, 0,
                            checked( Width - checked( _LineThickness + 1 ) ),
                            checked( Height - 1 ) );

                        graphics.DrawString( str, font1, solidBrush, rectangle, stringFormat1 );
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
            if( DesignMode )
            {
                _GradientPointB = new Point( Width, Height );
            }

            base.OnSizeChanged( e );
        }

        /// <summary>
        /// Enum PanelAppearance
        /// </summary>
        public enum PanelAppearance
        {
            /// <summary>
            /// The category
            /// </summary>
            Category,

            /// <summary>
            /// The rounded
            /// </summary>
            Rounded,

            /// <summary>
            /// The sloped bottom
            /// </summary>
            SlopedBottom,

            /// <summary>
            /// The sloped top
            /// </summary>
            SlopedTop
        }
    }
}