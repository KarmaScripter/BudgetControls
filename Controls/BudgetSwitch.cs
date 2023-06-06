// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetSwitch.cs" company="Terry D. Eppler">
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
//   BudgetSwitch.cs
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
    /// A class collection for rendering metro-style switch.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "CheckedChanged" ) ]
    [ Description( "This is a implements a metro switch" ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetSwitch ), "BudgetSwitch.bmp" ) ]
    [ Designer( typeof( BudgetSwitchDesigner ) ) ]
    public class BudgetSwitch : Control
    {
        #region Private Fields

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The checked
        /// </summary>
        private bool _Checked;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The check color
        /// </summary>
        private Color _CheckColor;

        /// <summary>
        /// The switch color
        /// </summary>
        private Color _SwitchColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The switch style
        /// </summary>
        private MetroSwitchStyle _SwitchStyle;

        /// <summary>
        /// The switch width
        /// </summary>
        private int _SwitchWidth;

        /// <summary>
        /// The rail width
        /// </summary>
        private int _RailWidth;

        /// <summary>
        /// The on off text
        /// </summary>
        private string _OnOffText;

        /// <summary>
        /// The on text
        /// </summary>
        private string _OnText;

        /// <summary>
        /// The off text
        /// </summary>
        private string _OffText;

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
                if( _BorderColor != value )
                {
                    _BorderColor = value;
                    Invalidate( );
                }
            }
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
        /// Gets or sets a value indicating whether this <see cref="BudgetSwitch" /> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether this control is checked." ) ]
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                //if (value != this._Checked)
                //{
                //    this._Checked = value;
                //    this.Invalidate();
                //}
                _Checked = value;
                CheckChanged( this, EventArgs.Empty );
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
        /// Gets or sets the color when hovered.
        /// </summary>
        /// <value>The color of the hover.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color when hovered." ) ]
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
        /// Gets or sets the on/off text.
        /// </summary>
        /// <value>The on off text.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( "OFF|ON" ) ]
        [ Description( "Sets the on/off text" ) ]
        public string OnOffText
        {
            get
            {
                return _OnOffText;
            }
            set
            {
                if( Operators.CompareString( value, _OnOffText, false ) != 0 )
                {
                    if( _OnOffText.Contains( "|" ) )
                    {
                        _OnOffText = value;
                        var str = _OnOffText;

                        char[ ] chrArray =
                        {
                            '|'
                        };

                        _OnText = str.Split( chrArray )[ 1 ];
                        var str1 = _OnOffText;

                        chrArray = new[ ]
                        {
                            '|'
                        };

                        _OffText = str1.Split( chrArray )[ 0 ];
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
        [ DefaultValue( 10 ) ]
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
                            if( _SwitchStyle != MetroSwitchStyle.OnOff )
                            {
                                _DefaultColor = Design.BudgetColors.LightSwitchRail;
                            }
                            else
                            {
                                _DefaultColor = Design.BudgetColors.LightDefault;
                            }

                            _BorderColor = Design.BudgetColors.LightBorder;
                            _CheckColor = Design.BudgetColors.AccentBlue;
                            _SwitchColor = Design.BudgetColors.LightDefault;
                            _HoverColor = Design.BudgetColors.AccentLightBlue;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _SwitchColor = Design.BudgetColors.DarkDefault;
                            _DefaultColor = Design.BudgetColors.DarkHover;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _CheckColor = Design.BudgetColors.AccentBlue;
                            _HoverColor = Design.BudgetColors.AccentLightBlue;
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
        /// Gets or sets the color of the switch.
        /// </summary>
        /// <value>The color of the switch.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the switch." ) ]
        public Color SwitchColor
        {
            get
            {
                return _SwitchColor;
            }
            set
            {
                if( _SwitchColor != value )
                {
                    _SwitchColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the switch style.
        /// </summary>
        /// <value>The switch style.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the switch style." ) ]
        public MetroSwitchStyle SwitchStyle
        {
            get
            {
                return _SwitchStyle;
            }
            set
            {
                if( value != _SwitchStyle )
                {
                    if( value == MetroSwitchStyle.Round )
                    {
                        Size = new Size( 30, 19 );
                    }

                    _SwitchStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the switch.
        /// </summary>
        /// <value>The width of the switch.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 15 ) ]
        [ Description( "Sets the width of the switch." ) ]
        public int SwitchWidth
        {
            get
            {
                return _SwitchWidth;
            }
            set
            {
                if( value != _SwitchWidth )
                {
                    _SwitchWidth = value;
                    Invalidate( );
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetSwitch" /> class.
        /// </summary>
        public BudgetSwitch( )
        {
            _Style = Design.Style.Light;
            _Checked = false;
            _DefaultColor = Design.BudgetColors.LightSwitchRail;
            _BorderColor = Design.BudgetColors.LightBorder;
            _CheckColor = Design.BudgetColors.AccentBlue;
            _SwitchColor = Design.BudgetColors.LightDefault;
            _HoverColor = Design.BudgetColors.AccentLightBlue;
            _SwitchStyle = MetroSwitchStyle.Round;
            _SwitchWidth = 15;
            _RailWidth = 10;
            _OnOffText = "OFF|ON";
            _OnText = "ON";
            _OffText = "OFF";
            _AutoStyle = true;
            _MouseState = Helpers.MouseState.None;
            Font = new Font( "Segoe UI", 9f );

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
        /// Draws the switch.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="switchstyle">The switchstyle.</param>
        protected void DrawSwitch( Graphics g, MetroSwitchStyle switchstyle )
        {
            Rectangle rectangle;
            Rectangle rectangle1;
            Rectangle rectangle2;
            Rectangle rectangle3;
            Rectangle rectangle4;

            switch( switchstyle )
            {
                case MetroSwitchStyle.Round:
                {
                    using( var solidBrush = new SolidBrush( _Checked
                              ? _DefaultColor
                              : _CheckColor ) )
                    {
                        rectangle1 = new Rectangle( 0, 1, checked( Width - 2 ), 16 );

                        Design.Drawing.FillRoundedPath( g, solidBrush, rectangle1, 16, true,
                            true, true, true );

                        if( _Checked )
                        {
                            rectangle1 = new Rectangle( checked( Width - 19 ), 0, 18, 18 );
                            rectangle2 = rectangle1;
                        }
                        else
                        {
                            rectangle = new Rectangle( 0, 0, 18, 18 );
                            rectangle2 = rectangle;
                        }

                        var rectangle5 = rectangle2;
                        solidBrush.Color = _SwitchColor;
                        g.FillEllipse( solidBrush, rectangle5 );

                        using( var pen = new Pen( _BorderColor ) )
                        {
                            if( _MouseState == Helpers.MouseState.Over
                               || _MouseState == Helpers.MouseState.Pressed
                                   ? true
                                   : false )
                            {
                                pen.Color = _HoverColor;
                            }

                            g.DrawEllipse( pen, rectangle5 );
                        }
                    }

                    break;
                }
                case MetroSwitchStyle.Rectangular:
                {
                    using( var solidBrush1 = new SolidBrush( _DefaultColor ) )
                    {
                        if( !_Checked )
                        {
                            rectangle = new Rectangle( _SwitchWidth,
                                checked( checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                        - checked( (int)Math.Round( (double)_RailWidth / 2 ) ) )
                                    - 1 ), checked( Width - _SwitchWidth ),
                                checked( _RailWidth + 1 ) );

                            rectangle3 = rectangle;
                        }
                        else
                        {
                            rectangle1 = new Rectangle( 0,
                                checked( checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                        - checked( (int)Math.Round( (double)_RailWidth / 2 ) ) )
                                    - 1 ), checked( Width - _SwitchWidth ),
                                checked( _RailWidth + 1 ) );

                            rectangle3 = rectangle1;
                        }

                        var rectangle6 = rectangle3;

                        solidBrush1.Color = _Checked
                            ? _DefaultColor
                            : _CheckColor;

                        g.FillRectangle( solidBrush1, rectangle6 );

                        if( !_Checked )
                        {
                            rectangle1 = new Rectangle( 0, 0, _SwitchWidth, checked( Height - 1 ) );
                            rectangle4 = rectangle1;
                        }
                        else
                        {
                            rectangle = new Rectangle( checked( Width - _SwitchWidth ), 0,
                                _SwitchWidth, Height );

                            rectangle4 = rectangle;
                        }

                        rectangle6 = rectangle4;
                        solidBrush1.Color = _SwitchColor;
                        g.FillRectangle( solidBrush1, rectangle6 );

                        using( var pen1 = new Pen( _BorderColor ) )
                        {
                            if( _MouseState == Helpers.MouseState.Over
                               || _MouseState == Helpers.MouseState.Pressed
                                   ? true
                                   : false )
                            {
                                pen1.Color = _HoverColor;
                            }

                            rectangle1 = new Rectangle( rectangle6.X, rectangle6.Y,
                                checked( rectangle6.Width - 1 ), checked( rectangle6.Height - 1 ) );

                            g.DrawRectangle( pen1, rectangle1 );
                        }
                    }

                    break;
                }
                case MetroSwitchStyle.OnOff:
                {
                    var color = Design.BudgetColors.ChangeColorBrightness( _DefaultColor, -0.4f );

                    using( var solidBrush2 = new SolidBrush( _Checked
                              ? color
                              : _DefaultColor ) )
                    {
                        using( var pen2 = new Pen( _BorderColor ) )
                        {
                            rectangle1 =
                                new Rectangle( checked( (int)Math.Round( (double)Width / 2 ) ), 0,
                                    checked( (int)Math.Round( (double)Width / 2 ) ),
                                    checked( Height - 1 ) );

                            g.FillRectangle( solidBrush2, rectangle1 );

                            solidBrush2.Color = _Checked
                                ? _DefaultColor
                                : color;

                            rectangle1 = new Rectangle( 0, 0,
                                checked( (int)Math.Round( (double)Width / 2 ) ),
                                checked( Height - 1 ) );

                            g.FillRectangle( solidBrush2, rectangle1 );

                            rectangle1 =
                                new Rectangle( checked( (int)Math.Round( (double)Width / 2 ) ), 0,
                                    checked( (int)Math.Round( (double)Width / 2 ) ),
                                    checked( Height - 1 ) );

                            g.DrawRectangle( pen2, rectangle1 );

                            rectangle1 = new Rectangle( 0, 0, checked( Width - 1 ),
                                checked( Height - 1 ) );

                            g.DrawRectangle( pen2, rectangle1 );
                        }

                        color = Design.BudgetColors.ChangeColorBrightness( ForeColor, -0.4f );

                        solidBrush2.Color = _Checked
                            ? color
                            : _CheckColor;

                        if( _MouseState == Helpers.MouseState.Over
                           || _MouseState == Helpers.MouseState.Pressed
                               ? true
                               : false )
                        {
                            solidBrush2.Color = _Checked
                                ? solidBrush2.Color
                                : _HoverColor;
                        }

                        var stringFormat = new StringFormat( )
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        using( var stringFormat1 = stringFormat )
                        {
                            var str = _OnText;
                            var font = Font;

                            rectangle1 =
                                new Rectangle( checked( (int)Math.Round( (double)Width / 2 ) ), 0,
                                    checked( (int)Math.Round( (double)Width / 2 ) ), Height );

                            g.DrawString( str, font, solidBrush2, rectangle1, stringFormat1 );

                            solidBrush2.Color = _Checked
                                ? _CheckColor
                                : color;

                            if( _MouseState == Helpers.MouseState.Over
                               || _MouseState == Helpers.MouseState.Pressed
                                   ? true
                                   : false )
                            {
                                solidBrush2.Color = _Checked
                                    ? _HoverColor
                                    : solidBrush2.Color;
                            }

                            var str1 = _OffText;
                            var font1 = Font;

                            rectangle1 = new Rectangle( 0, 0,
                                checked( (int)Math.Round( (double)Width / 2 ) ), Height );

                            g.DrawString( str1, font1, solidBrush2, rectangle1, stringFormat1 );
                        }
                    }

                    break;
                }
            }
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
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighSpeed;

            switch( _SwitchStyle )
            {
                case MetroSwitchStyle.Round:
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    DrawSwitch( graphics, MetroSwitchStyle.Round );
                    break;
                }
                case MetroSwitchStyle.Rectangular:
                {
                    DrawSwitch( graphics, MetroSwitchStyle.Rectangular );
                    break;
                }
                case MetroSwitchStyle.OnOff:
                {
                    DrawSwitch( graphics, MetroSwitchStyle.OnOff );
                    break;
                }
            }

            base.OnPaint( e );
        }

        //public event BudgetSwitch.CheckedChangedEventHandler CheckedChanged;

        //public delegate void CheckedChangedEventHandler(object sender, bool isChecked);

        /// <summary>
        /// Enum MetroSwitchStyle
        /// </summary>
        public enum MetroSwitchStyle
        {
            /// <summary>
            /// The round
            /// </summary>
            Round,

            /// <summary>
            /// The rectangular
            /// </summary>
            Rectangular,

            /// <summary>
            /// The on off
            /// </summary>
            OnOff
        }
    }
}