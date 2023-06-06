// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetBalloon.cs" company="Terry D. Eppler">
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
//   BudgetBalloon.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace BudgetExecution
{
    using System.Security.Permissions;
    using System.Windows.Forms.Design;
    using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// Class BudgetBalloon.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ Description( "Used for drawing a balloon text box." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetBalloon ), "BudgetBalloon.bmp" ) ]
    [ Designer( typeof( BudgetBalloonDesigner ) ) ]
    public class BudgetBalloon : Control
    {
        #region Private Fields

        //private static List<WeakReference> __ENCList;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The triangle width
        /// </summary>
        private int _TriangleWidth;

        /// <summary>
        /// The triangle position
        /// </summary>
        private int _TrianglePos;

        /// <summary>
        /// The has triangle
        /// </summary>
        private bool _HasTriangle;

        /// <summary>
        /// The is bound to cursor
        /// </summary>
        private bool _IsBoundToCursor;

        /// <summary>
        /// The balloon text
        /// </summary>
        private string _BalloonText;

        /// <summary>
        /// The has animation
        /// </summary>
        private bool _HasAnimation;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _style = Design.Style.Custom;

        /// <summary>
        /// The icon
        /// </summary>
        private Image _Icon;

        /// <summary>
        /// The host control
        /// </summary>
        private Control _HostControl;

        /// <summary>
        /// The control opacity
        /// </summary>
        private float _ControlOpacity;

        /// <summary>
        /// The is fading
        /// </summary>
        private bool _IsFading;

        /// <summary>
        /// The is visible
        /// </summary>
        private bool _IsVisible;

        /// <summary>
        /// The TMR fade
        /// </summary>
        [ AccessedThroughProperty( "FadeTimer" ) ]
        private Timer _tmrFade;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to automatically style the control.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Automatically style the control." ) ]
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
        /// Gets or sets the balloon text.
        /// </summary>
        /// <value>The balloon text.</value>
        [ Category( "Appearance" ) ]
        [ Description( "The text to use in the balloon." ) ]
        public string BalloonText
        {
            get
            {
                return _BalloonText;
            }
            set
            {
                if( Operators.CompareString( value, _BalloonText, false ) != 0 )
                {
                    _BalloonText = value;

                    using( var graphic = CreateGraphics( ) )
                    {
                        var sizeF = graphic.MeasureString( _BalloonText, Font );
                        var num = 50;
                        var num1 = 30;

                        if( sizeF.Width > 50f )
                        {
                            num = checked( checked( (int)Math.Round( sizeF.Width ) ) + 4 );

                            if( sizeF.Height > 30f )
                            {
                                num1 = checked( checked( checked( (int)Math.Round( sizeF.Height ) )
                                        + 4 )
                                    + ( _HasTriangle
                                        ? _TriangleWidth
                                        : 0 ) );
                            }
                        }

                        Size = new Size( num, num1 );
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Die Umrandungsfarbe des Steuerelements." ) ]
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
        /// Gets or sets the cursor that is displayed when the mouse pointer is over the control.
        /// </summary>
        /// <value>The cursor.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new Cursor Cursor
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
        /// Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.
        /// </summary>
        /// <value>The dock.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new DockStyle Dock
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has animation.
        /// </summary>
        /// <value><c>true</c> if this instance has animation; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Set to enable animation." ) ]
        public bool HasAnimation
        {
            get
            {
                return _HasAnimation;
            }
            set
            {
                if( value != _HasAnimation )
                {
                    _HasAnimation = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this control has triangle.
        /// </summary>
        /// <value><c>true</c> if this control has triangle; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Set to show the triangle." ) ]
        public bool HasTriangle
        {
            get
            {
                return _HasTriangle;
            }
            set
            {
                if( value != _HasTriangle )
                {
                    _HasTriangle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( null ) ]
        [ Description( "Sets the icon." ) ]
        public Image Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                if( value != _Icon )
                {
                    _Icon = value;
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
                return _style;
            }
            set
            {
                if( value != _style )
                {
                    _style = value;

                    switch( value )
                    {
                        case Design.Style.Light:
                        {
                            _DefaultColor = Design.BudgetColors.LightDefault;
                            _BorderColor = Design.BudgetColors.PopUpBorder;
                            ForeColor = Design.BudgetColors.PopUpFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _DefaultColor = Design.BudgetColors.DarkDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;
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
        /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
        /// </summary>
        /// <value><c>true</c> if [tab stop]; otherwise, <c>false</c>.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new bool TabStop
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
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
        /// Gets or sets the fade timer.
        /// </summary>
        /// <value>The fade.</value>
        public virtual Timer FadeTimer
        {
            [ DebuggerNonUserCode ]
            get
            {
                return _tmrFade;
            }
            [ DebuggerNonUserCode ]
            [ MethodImpl( MethodImplOptions.Synchronized ) ]
            set
            {
                var metroBalloon = this;
                EventHandler eventHandler = metroBalloon.FadeEffect;

                if( _tmrFade != null )
                {
                    _tmrFade.Tick -= eventHandler;
                }

                _tmrFade = value;

                if( _tmrFade != null )
                {
                    _tmrFade.Tick += eventHandler;
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the triangle.
        /// </summary>
        /// <value>The width of the triangle.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 5 ) ]
        [ Description( "Sets the width of the triangle." ) ]
        public int TriangleWidth
        {
            get
            {
                return _TriangleWidth;
            }
            set
            {
                if( value != _TriangleWidth )
                {
                    _TriangleWidth = value;

                    if( _HasTriangle )
                    {
                        var size = new Size( Width, checked( Height + _TriangleWidth ) );
                        Size = size;
                        Invalidate( );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the wait cursor for the current control and all child controls.
        /// </summary>
        /// <value><c>true</c> if use wait cursor; otherwise, <c>false</c>.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new bool UseWaitCursor
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetBalloon" /> class.
        /// </summary>
        public BudgetBalloon( )
        {
            _BorderColor = Design.BudgetColors.PopUpBorder;
            _DefaultColor = Design.BudgetColors.LightDefault;
            _TriangleWidth = 5;
            _TrianglePos = 0;
            _HasTriangle = true;
            _IsBoundToCursor = true;
            _BalloonText = string.Empty;
            _HasAnimation = true;
            _Icon = null;
            _HostControl = null;
            _ControlOpacity = 0f;
            _IsFading = false;
            _IsVisible = true;

            FadeTimer = new Timer( )
            {
                Interval = 40
            };

            _AutoStyle = true;
            Font = new Font( "Roboto", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            ForeColor = Design.BudgetColors.PopUpFont;

            //this.Visible = false;
            UpdateStyles( );
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Creates the image.
        /// </summary>
        /// <returns>Bitmap.</returns>
        private Bitmap CreateImage( )
        {
            var bitmap = new Bitmap( Width, Height );
            var graphic = Graphics.FromImage( bitmap );

            var rectangle = new Rectangle( 0, 0, checked( Width - 1 ), checked( Height
                - ( _HasTriangle
                    ? _TriangleWidth
                    : 1 ) ) );

            using( var solidBrush = new SolidBrush( _DefaultColor ) )
            {
                graphic.FillRectangle( solidBrush, rectangle );
            }

            using( var pen = new Pen( _BorderColor ) )
            {
                graphic.DrawRectangle( pen, rectangle );

                if( _HasTriangle )
                {
                    DrawTriangle( graphic );
                }
            }

            if( _Icon != null )
            {
                rectangle = new Rectangle( checked( _Icon.Width + 4 ), 0,
                    checked( checked( Width - 1 ) - checked( _Icon.Width + 4 ) ), checked( Height
                        - ( _HasTriangle
                            ? _TriangleWidth
                            : 1 ) ) );

                var graphic1 = graphic;
                var image = _Icon;

                var point = new Point( 4,
                    checked( checked( checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                - checked( (int)Math.Round( (double)_Icon.Height / 2 ) ) )
                            - ( _HasTriangle
                                ? _TriangleWidth
                                : 1 ) )
                        + 2 ) );

                graphic1.DrawImage( image, point );
            }

            using( var solidBrush1 = new SolidBrush( ForeColor ) )
            {
                var stringFormat = new StringFormat( )
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                using( var stringFormat1 = stringFormat )
                {
                    graphic.DrawString( _BalloonText, Font, solidBrush1, rectangle, stringFormat1 );
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Draws the triangle.
        /// </summary>
        /// <param name="e">The e.</param>
        protected void DrawTriangle( Graphics e )
        {
            var num = 1;
            var pointArray = new Point[ 3 ];

            var point = new Point( _TrianglePos,
                checked( checked( Height - _TriangleWidth ) - num ) );

            pointArray[ 0 ] = point;

            var point1 =
                new Point(
                    checked( _TrianglePos
                        + checked( (int)Math.Round( (double)_TriangleWidth / 2 ) ) ),
                    checked( Height - num ) );

            pointArray[ 1 ] = point1;

            var point2 = new Point( checked( _TrianglePos + _TriangleWidth ),
                checked( checked( Height - _TriangleWidth ) - num ) );

            pointArray[ 2 ] = point2;
            var pointArray1 = pointArray;
            var graphic = e;
            graphic.SmoothingMode = SmoothingMode.AntiAlias;

            using( var solidBrush = new SolidBrush( _DefaultColor ) )
            {
                graphic.FillPolygon( solidBrush, pointArray1 );
            }

            using( var pen = new Pen( _BorderColor ) )
            {
                graphic.DrawLine( pen, pointArray1[ 0 ], pointArray1[ 1 ] );
                graphic.DrawLine( pen, pointArray1[ 1 ], pointArray1[ 2 ] );
            }

            graphic.SmoothingMode = SmoothingMode.HighSpeed;
            graphic = null;
        }

        /// <summary>
        /// Fades the effect.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FadeEffect( object sender, EventArgs e )
        {
            _IsFading = true;

            if( _IsVisible )
            {
                _ControlOpacity = (float)( _ControlOpacity - 0.1 );

                if( _ControlOpacity <= 0f )
                {
                    FadeTimer.Stop( );
                    _IsFading = false;
                    _IsVisible = false;
                    Visible = false;
                    _ControlOpacity = 0f;
                }
            }
            else
            {
                Visible = true;
                _ControlOpacity = (float)( _ControlOpacity + 0.1 );

                if( _ControlOpacity >= 1f )
                {
                    FadeTimer.Stop( );
                    _IsFading = false;
                    _IsVisible = true;
                    _ControlOpacity = 1f;
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
        /// Handles the <see cref="E:HostMouseEnter" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnHostMouseEnter( object sender, EventArgs e )
        {
            if( !_HasAnimation )
            {
                Visible = true;
                _IsVisible = true;
            }
            else if( !_IsFading )
            {
                if( !_IsVisible )
                {
                    BringToFront( );
                    FadeTimer.Start( );
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="E:HostMouseLeave" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnHostMouseLeave( object sender, EventArgs e )
        {
            if( !_HasAnimation )
            {
                Visible = false;
                _IsVisible = false;
            }
            else
            {
                FadeTimer.Stop( );
                _IsVisible = true;
                FadeTimer.Start( );
            }
        }

        /// <summary>
        /// Handles the <see cref="E:HostMouseMove" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void OnHostMouseMove( object sender, MouseEventArgs e )
        {
            if( _IsBoundToCursor )
            {
                var location = _HostControl.Location;
                var x = location.X;
                location = _HostControl.Location;
                var y = location.Y;
                var parent = _HostControl.Parent ?? _HostControl;

                if( checked( checked( x + e.X ) + Width ) <= parent.Width )
                {
                    location = _HostControl.Location;
                    x = checked( checked( location.X + e.X ) + 5 );
                }
                else
                {
                    x = checked( checked( parent.Width - Width ) - 1 );
                }

                y = checked( checked( y - Height ) - 1 ) >= 0
                    ? checked( checked( y - Height ) - 1 )
                    : 0;

                location = new Point( x, y );
                Location = location;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            var graphics = e.Graphics;

            if( _IsFading )
            {
                var bitmap = (Bitmap)SetImageOpacity( CreateImage( ), _ControlOpacity );
                graphics.DrawImage( bitmap, 0, 0 );
            }
            else if( _IsVisible/*|| this.DesignMode*/
                        ? true
                        : false )
            {
                var rectangle = new Rectangle( 0, 0, checked( Width - 1 ), checked( Height
                    - ( _HasTriangle
                        ? _TriangleWidth
                        : 1 ) ) );

                using( var solidBrush = new SolidBrush( _DefaultColor ) )
                {
                    graphics.FillRectangle( solidBrush, rectangle );
                }

                using( var pen = new Pen( _BorderColor ) )
                {
                    graphics.DrawRectangle( pen, rectangle );

                    if( _HasTriangle )
                    {
                        DrawTriangle( graphics );
                    }
                }

                if( _Icon != null )
                {
                    rectangle = new Rectangle( checked( _Icon.Width + 4 ), 0,
                        checked( checked( Width - 1 ) - checked( _Icon.Width + 4 ) ),
                        checked( Height
                            - ( _HasTriangle
                                ? _TriangleWidth
                                : 1 ) ) );

                    var graphic = graphics;
                    var image = _Icon;

                    var point = new Point( 4,
                        checked( checked( checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                    - checked( (int)Math.Round( (double)_Icon.Height / 2 ) ) )
                                - ( _HasTriangle
                                    ? _TriangleWidth
                                    : 1 ) )
                            + 2 ) );

                    graphic.DrawImage( image, point );
                }

                using( var solidBrush1 = new SolidBrush( ForeColor ) )
                {
                    var stringFormat = new StringFormat( )
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    using( var stringFormat1 = stringFormat )
                    {
                        graphics.DrawString( _BalloonText, Font, solidBrush1, rectangle,
                            stringFormat1 );
                    }
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Sets the image opacity.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="opacity">The opacity.</param>
        /// <returns>Image.</returns>
        private Image SetImageOpacity( Image image, float opacity )
        {
            Image image1;

            try
            {
                var bitmap = new Bitmap( image.Width, image.Height );

                using( var graphic = Graphics.FromImage( bitmap ) )
                {
                    var colorMatrix = new ColorMatrix( )
                    {
                        Matrix33 = opacity
                    };

                    var imageAttribute = new ImageAttributes( );

                    imageAttribute.SetColorMatrix( colorMatrix, ColorMatrixFlag.Default,
                        ColorAdjustType.Bitmap );

                    var rectangle = new Rectangle( 0, 0, bitmap.Width, bitmap.Height );

                    graphic.DrawImage( image, rectangle, 0, 0, image.Width,
                        image.Height, GraphicsUnit.Pixel, imageAttribute );
                }

                image1 = bitmap;
            }
            catch( Exception exception )
            {
                ProjectData.SetProjectError( exception );
                image1 = null;
                ProjectData.ClearProjectError( );
            }

            return image1;
        }

        /// <summary>
        /// Displays the control to the user.
        /// </summary>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new void Show( )
        {
        }

        /// <summary>
        /// Shows the balloon.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <param name="cHost">The c host.</param>
        public void ShowBalloon( string sText, Control cHost )
        {
            BalloonText = sText;
            _IsBoundToCursor = true;
            Visible = false;

            if( cHost != null )
            {
                _HostControl = cHost;
                var metroBalloon = this;
                cHost.MouseMove += metroBalloon.OnHostMouseMove;
                var metroBalloon1 = this;
                cHost.MouseEnter += metroBalloon1.OnHostMouseEnter;
                var metroBalloon2 = this;
                cHost.MouseLeave += metroBalloon2.OnHostMouseLeave;
            }
        }

        /// <summary>
        /// Shows the balloon.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <param name="cHost">The c host.</param>
        /// <param name="iIcon">The i icon.</param>
        public void ShowBalloon( string sText, Control cHost, Image iIcon )
        {
            BalloonText = sText;
            _IsBoundToCursor = true;
            _Icon = iIcon;
            Visible = false;

            if( cHost != null )
            {
                _HostControl = cHost;
                var metroBalloon = this;
                cHost.MouseMove += metroBalloon.OnHostMouseMove;
                var metroBalloon1 = this;
                cHost.MouseEnter += metroBalloon1.OnHostMouseEnter;
                var metroBalloon2 = this;
                cHost.MouseLeave += metroBalloon2.OnHostMouseLeave;
            }
        }

        /// <summary>
        /// Shows the balloon.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <param name="p">The p.</param>
        public void ShowBalloon( string sText, Point p )
        {
            BalloonText = sText;
            _IsBoundToCursor = false;
            _IsVisible = true;
            Location = p;
        }

        /// <summary>
        /// Shows the balloon.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <param name="p">The p.</param>
        /// <param name="iIcon">The i icon.</param>
        public void ShowBalloon( string sText, Point p, Image iIcon )
        {
            BalloonText = sText;
            _Icon = iIcon;
            _IsBoundToCursor = false;
            _IsVisible = true;
            Location = p;
        }

        #endregion
    }

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(BudgetBalloonDesigner))] --------------------//

    #endregion

    #region ControlDesigner

    /// <summary>
    /// Class BudgetBalloonDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [ PermissionSet( SecurityAction.Demand, Name = "FullTrust" ) ]
    public class BudgetBalloonDesigner : ControlDesigner
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
                if( null == actionLists )
                {
                    actionLists = new DesignerActionListCollection( );
                    actionLists.Add( new BudgetBalloonSmartTagActionList( Component ) );
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
        protected override void PostFilterProperties( IDictionary Properties )
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
    /// Class BudgetBalloonSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class BudgetBalloonSmartTagActionList : DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private BudgetBalloon colUserControl;

        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetBalloonSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public BudgetBalloonSmartTagActionList( IComponent component )
            : base( component )
        {
            colUserControl = component as BudgetBalloon;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            designerActionUISvc =
                GetService( typeof( DesignerActionUIService ) ) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="System.ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName( String propName )
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties( colUserControl )[ propName ];

            if( null == prop )
            {
                throw new ArgumentException( "Matching ColorLabel property not found!", propName );
            }
            else
            {
                return prop;
            }
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.

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
                GetPropertyByName( "BackColor" ).SetValue( colUserControl, value );
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
                GetPropertyByName( "ForeColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic style].
        /// </summary>
        /// <value><c>true</c> if [automatic style]; otherwise, <c>false</c>.</value>
        public bool AutoStyle
        {
            get
            {
                return colUserControl.AutoStyle;
            }
            set
            {
                GetPropertyByName( "AutoStyle" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the balloon text.
        /// </summary>
        /// <value>The balloon text.</value>
        public string BalloonText
        {
            get
            {
                return colUserControl.BalloonText;
            }
            set
            {
                GetPropertyByName( "BalloonText" ).SetValue( colUserControl, value );
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
                return colUserControl.BorderColor;
            }
            set
            {
                GetPropertyByName( "BorderColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the default color.
        /// </summary>
        /// <value>The default color.</value>
        public Color DefaultColor
        {
            get
            {
                return colUserControl.DefaultColor;
            }
            set
            {
                GetPropertyByName( "DefaultColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has animation.
        /// </summary>
        /// <value><c>true</c> if this instance has animation; otherwise, <c>false</c>.</value>
        public bool HasAnimation
        {
            get
            {
                return colUserControl.HasAnimation;
            }
            set
            {
                GetPropertyByName( "HasAnimation" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has triangle.
        /// </summary>
        /// <value><c>true</c> if this instance has triangle; otherwise, <c>false</c>.</value>
        public bool HasTriangle
        {
            get
            {
                return colUserControl.HasTriangle;
            }
            set
            {
                GetPropertyByName( "HasTriangle" ).SetValue( colUserControl, value );
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
                GetPropertyByName( "Style" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the fade timer.
        /// </summary>
        /// <value>The fade timer.</value>
        public virtual Timer FadeTimer
        {
            get
            {
                return colUserControl.FadeTimer;
            }
            set
            {
                GetPropertyByName( "FadeTimer" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the width of the triangle.
        /// </summary>
        /// <value>The width of the triangle.</value>
        public int TriangleWidth
        {
            get
            {
                return colUserControl.TriangleWidth;
            }
            set
            {
                GetPropertyByName( "TriangleWidth" ).SetValue( colUserControl, value );
            }
        }

        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems( )
        {
            var items = new DesignerActionItemCollection( );

            //Define static section header entries.
            items.Add( new DesignerActionHeaderItem( "Appearance" ) );

            items.Add( new DesignerActionPropertyItem( "HasAnimation", "Has Animation",
                "Appearance", "Set to show if its." ) );

            items.Add( new DesignerActionPropertyItem( "HasTriangle", "Has Triangle", "Appearance",
                "Set to enable triangle." ) );

            items.Add( new DesignerActionPropertyItem( "AutoStyle", "Auto Style", "Appearance",
                "Sets the auto style." ) );

            items.Add( new DesignerActionPropertyItem( "ForeColor", "Fore Color", "Appearance",
                "Selects the foreground color." ) );

            items.Add( new DesignerActionPropertyItem( "BorderColor", "Border Color", "Appearance",
                "Sets the border color." ) );

            items.Add( new DesignerActionPropertyItem( "DefaultColor", "Default Color",
                "Appearance", "Sets the default color." ) );

            items.Add( new DesignerActionPropertyItem( "Style", "Style", "Appearance",
                "Sets the balloon style." ) );

            items.Add( new DesignerActionPropertyItem( "TriangleWidth", "Triangle Width",
                "Appearance", "Sets the triangle width." ) );

            items.Add( new DesignerActionPropertyItem( "BalloonText", "Balloon Text", "Appearance",
                "Sets the balloon text." ) );

            //Create entries for static Information section.
            var location = new StringBuilder( "Product: " );
            location.Append( colUserControl.ProductName );
            var size = new StringBuilder( "Version: " );
            size.Append( colUserControl.ProductVersion );
            items.Add( new DesignerActionTextItem( location.ToString( ), "Information" ) );
            items.Add( new DesignerActionTextItem( size.ToString( ), "Information" ) );
            return items;
        }

        #endregion
    }

    #endregion

    #endregion
}