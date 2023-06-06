// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetChecker.cs" company="Terry D. Eppler">
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
//   BudgetChecker.cs
// </summary>
// ******************************************************************************************

namespace BudgetExecution
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    /// <summary>
    /// A class collection for rendering metro-style checker.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "CheckedChanged" ) ]
    [ Description( "Sets a metro-style checker." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( CheckBox ) ) ]
    public class BudgetChecker : Control
    {
        #region Private Fields

        //private static List<WeakReference> __ENCList;

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
        /// The check color
        /// </summary>
        private Color _CheckColor;

        /// <summary>
        /// The checked border color
        /// </summary>
        private Color _CheckedBorderColor;

        /// <summary>
        /// The checked
        /// </summary>
        private bool _Checked;

        /// <summary>
        /// The checker width
        /// </summary>
        private readonly int _CheckerWidth;

        /// <summary>
        /// The bounds width
        /// </summary>
        private int _BoundsWidth;

        /// <summary>
        /// The checker symbol
        /// </summary>
        private MetroCheckerSymbol _CheckerSymbol;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The text
        /// </summary>
        private string _Text;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        /// <summary>
        /// The mouse state
        /// </summary>
        private Helpers.MouseState _MouseState;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to automatically style the control.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to automatically style the control." ) ]
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
        /// Gets or sets the color of the check.
        /// </summary>
        /// <value>The color of the check.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the check." ) ]
        public Color CheckColor
        {
            get
            {
                return _CheckColor;
            }
            set
            {
                if( _CheckColor != value )
                {
                    _CheckColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BudgetChecker" /> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether this control should be checked." ) ]
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                CheckChanged( this, EventArgs.Empty );
                Invalidate( );
            }
        }

        /// <summary>
        /// Gets or sets the color of the checked border.
        /// </summary>
        /// <value>The color of the checked border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the checked border." ) ]
        public Color CheckedBorderColor
        {
            get
            {
                return _CheckedBorderColor;
            }
            set
            {
                if( _CheckedBorderColor != value )
                {
                    _CheckedBorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the checker symbol.
        /// </summary>
        /// <value>The checker symbol.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the checker symbol." ) ]
        public MetroCheckerSymbol CheckerSymbol
        {
            get
            {
                return _CheckerSymbol;
            }
            set
            {
                if( _CheckerSymbol != value )
                {
                    _CheckerSymbol = value;
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
        /// Gets or sets the color when the control is hovered.
        /// </summary>
        /// <value>The color of the hover.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the hover." ) ]
        public Color HoverColor
        {
            get
            {
                return _HoverColor;
            }
            set
            {
                if( _HoverColor != value )
                {
                    _HoverColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color when the control is pressed.
        /// </summary>
        /// <value>The color of the control when it is pressed.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color when the control is pressed." ) ]
        public Color PressedColor
        {
            get
            {
                return _PressedColor;
            }
            set
            {
                if( _PressedColor != value )
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

                    switch( _Style )
                    {
                        case Design.Style.Light:
                        {
                            BackColor = Design.BudgetColors.LightDefault;
                            _DefaultColor = Design.BudgetColors.LightHover;
                            _HoverColor = Design.BudgetColors.LightDarkDefault;
                            _PressedColor = Design.BudgetColors.AccentDarkBlue;
                            _CheckColor = Design.BudgetColors.AccentBlue;
                            _CheckedBorderColor = Design.BudgetColors.LightBorder;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            BackColor = Design.BudgetColors.DarkDefault;
                            _DefaultColor = Design.BudgetColors.DarkHover;
                            _HoverColor = Design.BudgetColors.LightBorder;
                            _PressedColor = Design.BudgetColors.AccentDarkBlue;
                            _CheckColor = Design.BudgetColors.AccentBlue;
                            _CheckedBorderColor = Design.BudgetColors.LightBorder;
                            ForeColor = Design.BudgetColors.DarkFont;
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
        public override string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                Invalidate( );
                Size = new Size( GetMaxWidth( ), 14 );
            }
        }

        /**/

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetChecker" /> class.
        /// </summary>
        public BudgetChecker( )
        {
            //MetroChecker.__ENCAddToList(this);
            _DefaultColor = Design.BudgetColors.LightHover;
            _HoverColor = Design.BudgetColors.LightDarkDefault;
            _PressedColor = Design.BudgetColors.AccentDarkBlue;
            _CheckColor = Design.BudgetColors.AccentBlue;
            _CheckedBorderColor = Design.BudgetColors.LightBorder;

            //this._Checked = false;
            _CheckerWidth = 13;
            _BoundsWidth = 16;
            _CheckerSymbol = MetroCheckerSymbol.Box;
            _Style = Design.Style.Light;
            _AutoStyle = true;
            _MouseState = Helpers.MouseState.None;
            Font = new Font( "Segoe UI", 9f );
            Size = new Size( GetMaxWidth( ), 14 );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            BackColor = Color.Transparent;
            UpdateStyles( );
        }

        /// <summary>
        /// Draws the tick.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="r">The r.</param>
        /// <param name="c">The c.</param>
        /// <param name="thickness">The thickness.</param>
        private void DrawTick( Graphics g, Rectangle r, Color c, int thickness = 1 )
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using( var pen = new Pen( c, 2f ) )
            {
                var point = new Point( checked( r.X + 3 ), checked( r.Y + 6 ) );
                var point1 = new Point( checked( r.X + 4 ), checked( r.Y + 10 ) );
                g.DrawLine( pen, point, point1 );
                point1 = new Point( checked( r.X + 4 ), checked( r.Y + 10 ) );
                point = new Point( checked( r.X + 5 ), checked( r.Y + 10 ) );
                g.DrawLine( pen, point1, point );
                point1 = new Point( checked( r.X + 5 ), checked( r.Y + 10 ) );
                point = new Point( checked( r.X + 10 ), checked( r.Y + 3 ) );
                g.DrawLine( pen, point1, point );
            }
        }

        /// <summary>
        /// Gets the maximum width.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int GetMaxWidth( )
        {
            int num;

            using( var graphic = CreateGraphics( ) )
            {
                var sizeF = graphic.MeasureString( _Text, Font );
                _BoundsWidth = checked( (int)Math.Round( sizeF.Width + 16f ) );
                num = _BoundsWidth;
            }

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
        /// The checking
        /// </summary>
        private int checking = 0;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClick( EventArgs e )
        {
            #region Old Code

            //         //this._Checked ^= 1;
            //         this._Checked = true;
            //         //Checked = true;
            //         BudgetSwitch.CheckedChangedEventHandler checkedChangedEventHandler = this.CheckedChanged;
            //if (checkedChangedEventHandler != null)
            //{
            //	checkedChangedEventHandler(this, this._Checked);
            //} 

            #endregion

            _Checked = !_Checked;

            if( checkedChanged != null )
            {
                checkedChanged( this, EventArgs.Empty );
            }

            Invalidate( );
            base.OnClick( e );
        }

        #region Event Creation

        /////Implement this in the Property you want to trigger the event///////////////////////////
        // 
        //  For Example this will be triggered by the Value Property
        //
        //  public int Value
        //   { 
        //      get { return _value;}
        //      set
        //         {
        //          
        //              _value = value;
        //
        //              this.CheckChanged(EventArgs.Empty);
        //              Invalidate();
        //          }
        //    }
        //
        ////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The checked changed
        /// </summary>
        private CheckedChangedEventHandler checkedChanged;

        /// <summary>
        /// Delegate CheckedChangedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void CheckedChangedEventHandler( object sender, EventArgs e );

        /// <summary>
        /// Triggered when the Value changes
        /// </summary>
        public event CheckedChangedEventHandler CheckedChanged
        {
            add
            {
                checkedChanged = checkedChanged + value;
            }
            remove
            {
                checkedChanged = checkedChanged - value;
            }
        }

        /// <summary>
        /// Checks the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void CheckChanged( object sender, EventArgs e )
        {
            if( checkedChanged == null )
            {
                return;
            }

            checkedChanged( this, e );
        }

        #endregion

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
            Checked = !Checked;
            Focus( );
            Invalidate( );
            base.OnMouseUp( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            Rectangle rectangle;
            var graphics = e.Graphics;
            var rectangle1 = new Rectangle( 0, 0, _CheckerWidth, _CheckerWidth );

            var rectangle2 = new Rectangle( 3, 3, checked( _CheckerWidth - 6 ),
                checked( _CheckerWidth - 6 ) );

            var location = rectangle2.Location;

            var point = new Point( checked( rectangle2.X + Width ),
                checked( rectangle2.Y + Height ) );

            using( var linearGradientBrush = new LinearGradientBrush( location, point, _CheckColor,
                      Design.BudgetColors.ChangeColorBrightness( _CheckColor, -0.2f ) ) )
            {
                using( var pen = new Pen( _DefaultColor ) )
                {
                    switch( _MouseState )
                    {
                        case Helpers.MouseState.None:
                        {
                            pen.Color = _DefaultColor;
                            break;
                        }
                        case Helpers.MouseState.Over:
                        {
                            pen.Color = _HoverColor;
                            break;
                        }
                        case Helpers.MouseState.Pressed:
                        {
                            pen.Color = _PressedColor;
                            break;
                        }
                    }

                    if( _Checked )
                    {
                        pen.Color = _CheckedBorderColor;
                    }

                    switch( _CheckerSymbol )
                    {
                        case MetroCheckerSymbol.Box:
                        {
                            graphics.DrawRectangle( pen, rectangle1 );

                            if( _Checked )
                            {
                                rectangle = new Rectangle( 3, 3, checked( _CheckerWidth - 5 ),
                                    checked( _CheckerWidth - 5 ) );

                                graphics.FillRectangle( linearGradientBrush, rectangle );
                            }

                            break;
                        }
                        case MetroCheckerSymbol.Circle:
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.DrawEllipse( pen, rectangle1 );

                            if( _Checked )
                            {
                                graphics.FillEllipse( linearGradientBrush, rectangle2 );
                            }

                            break;
                        }
                        case MetroCheckerSymbol.BoxWithTick:
                        {
                            graphics.DrawRectangle( pen, rectangle1 );

                            if( _Checked )
                            {
                                rectangle = new Rectangle( rectangle1.X, rectangle1.Y, 16, 16 );
                                DrawTick( graphics, rectangle, _CheckColor, 2 );
                            }

                            break;
                        }
                        case MetroCheckerSymbol.CircleWithTick:
                        {
                            graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            graphics.DrawEllipse( pen, rectangle1 );

                            if( !_Checked )
                            {
                                break;
                            }

                            rectangle = new Rectangle( rectangle1.X, rectangle1.Y, 16, 16 );
                            DrawTick( graphics, rectangle, _CheckColor, 2 );
                            break;
                        }
                    }

                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    using( var solidBrush = new SolidBrush( ForeColor ) )
                    {
                        var text = Text;
                        var font = Font;
                        point = new Point( 16, -1 );
                        graphics.DrawString( text, font, solidBrush, point );
                    }
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
        /// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
        /// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
        /// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
        /// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
        protected override void SetBoundsCore(
            int x, int y, int width, int height,
            BoundsSpecified specified )
        {
            height = 14;
            width = _BoundsWidth;
            base.SetBoundsCore( x, y, width, height, specified );
        }

        //public event MetroChecker.CheckedChangedEventHandler CheckedChanged;

        //public delegate void CheckedChangedEventHandler(object sender, bool isChecked);

        /// <summary>
        /// Enum MetroCheckerSymbol
        /// </summary>
        public enum MetroCheckerSymbol
        {
            /// <summary>
            /// The box
            /// </summary>
            Box,

            /// <summary>
            /// The circle
            /// </summary>
            Circle,

            /// <summary>
            /// The box with tick
            /// </summary>
            BoxWithTick,

            /// <summary>
            /// The circle with tick
            /// </summary>
            CircleWithTick
        }
    }
}