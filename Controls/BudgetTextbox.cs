// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTextbox.cs" company="Terry D. Eppler">
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
//   BudgetTextbox.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering a metro-style texbox.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "TextChanged" ) ]
    [ Description( "Ein Textbox-Steuerelement im MetroStil." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( TextBox ) ) ]
    [ Designer( typeof( BudgetTextboxDesigner ) ) ]
    public class BudgetTextbox : Control
    {
        #region Private Fields

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The disabled color
        /// </summary>
        private Color _DisabledColor;

        /// <summary>
        /// The illegal chars
        /// </summary>
        private string _IllegalChars;

        /// <summary>
        /// The ban illegal chars
        /// </summary>
        private bool _BanIllegalChars;

        /// <summary>
        /// The watermark
        /// </summary>
        private string _Watermark;

        /// <summary>
        /// The line only
        /// </summary>
        private bool _LineOnly;

        /// <summary>
        /// The base
        /// </summary>
        private TextBox Base;

        /// <summary>
        /// The maximum length
        /// </summary>
        private int _MaxLength;

        /// <summary>
        /// The accepts return
        /// </summary>
        private bool _AcceptsReturn;

        /// <summary>
        /// The accepts tab
        /// </summary>
        private bool _AcceptsTab;

        /// <summary>
        /// The character casing
        /// </summary>
        private CharacterCasing _CharacterCasing;

        /// <summary>
        /// The hide selection
        /// </summary>
        private bool _HideSelection;

        /// <summary>
        /// The password character
        /// </summary>
        private char _PasswordChar;

        /// <summary>
        /// The read only
        /// </summary>
        private bool _ReadOnly;

        /// <summary>
        /// The shortcuts enabled
        /// </summary>
        private bool _ShortcutsEnabled;

        /// <summary>
        /// The use system password character
        /// </summary>
        private bool _UseSystemPasswordChar;

        /// <summary>
        /// The word wrap
        /// </summary>
        private bool _WordWrap;

        /// <summary>
        /// The multiline
        /// </summary>
        private bool _Multiline;

        /// <summary>
        /// The text align
        /// </summary>
        private HorizontalAlignment _TextAlign;

        /// <summary>
        /// The scrollbars
        /// </summary>
        private ScrollBars _Scrollbars;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        /// <summary>
        /// The mouse state
        /// </summary>
        private Helpers.MouseState _MouseState;

        /// <summary>
        /// The border
        /// </summary>
        private int border = 1;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether it accepts return.
        /// </summary>
        /// <value><c>true</c> if accepts return; otherwise, <c>false</c>.</value>
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether it accepts return." ) ]
        public bool AcceptsReturn
        {
            get
            {
                return _AcceptsReturn;
            }
            set
            {
                if( Base != null )
                {
                    _AcceptsReturn = value;
                    Base.AcceptsReturn = _AcceptsReturn;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it accepts tab.
        /// </summary>
        /// <value><c>true</c> if accepts tab; otherwise, <c>false</c>.</value>
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether it accepts tab." ) ]
        public bool AcceptsTab
        {
            get
            {
                return _AcceptsTab;
            }
            set
            {
                if( Base != null )
                {
                    _AcceptsTab = value;
                    Base.AcceptsTab = _AcceptsTab;
                }
            }
        }

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
        /// Gets or sets a value indicating whether to ban illegal characters.
        /// </summary>
        /// <value><c>true</c> if ban illegal chars; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to ban illegal characters." ) ]
        public bool BanIllegalChars
        {
            get
            {
                return _BanIllegalChars;
            }
            set
            {
                if( value != _BanIllegalChars )
                {
                    _BanIllegalChars = value;
                    Invalidate( );
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
                if( value != _BorderColor )
                {
                    _BorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the character casing.
        /// </summary>
        /// <value>The character casing.</value>
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the character casing." ) ]
        public CharacterCasing CharacterCasing
        {
            get
            {
                return _CharacterCasing;
            }
            set
            {
                if( Base != null )
                {
                    _CharacterCasing = value;
                    Base.CharacterCasing = _CharacterCasing;
                }
            }
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
        /// Gets or sets the color of the disabled.
        /// </summary>
        /// <value>The color of the disabled.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the disabled." ) ]
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
        /// Gets a value indicating whether the control has input focus.
        /// </summary>
        /// <value><c>true</c> if focused; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ Description( "Gets a value indicating whether the control has input focus." ) ]
        public override bool Focused
        {
            get
            {
                return Base.Focused;
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the font of the text displayed by the control." ) ]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;

                if( Base != null )
                {
                    Base.Font = value;
                    Base.Location = new Point( 3, 5 );
                    Base.Width = checked( Width - 6 );
                }
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the foreground color of the control." ) ]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;

                if( Base != null )
                {
                    Base.ForeColor = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide selection.
        /// </summary>
        /// <value><c>true</c> if hide selection; otherwise, <c>false</c>.</value>
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to hide selection." ) ]
        public bool HideSelection
        {
            get
            {
                return _HideSelection;
            }
            set
            {
                if( Base != null )
                {
                    _HideSelection = value;
                    Base.HideSelection = _HideSelection;
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
        /// Gets or sets the illegal chars.
        /// </summary>
        /// <value>The illegal chars.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( "" ) ]
        [ Description( "Sets the illegal chars." ) ]
        public string IllegalChars
        {
            get
            {
                return _IllegalChars;
            }
            set
            {
                if( Operators.CompareString( value, _IllegalChars, false ) != 0 )
                {
                    _IllegalChars = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether line only is enabled/disabled.
        /// </summary>
        /// <value><c>true</c> if line only; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether line only is enabled/disabled." ) ]
        public bool LineOnly
        {
            get
            {
                return _LineOnly;
            }
            set
            {
                if( value != _LineOnly )
                {
                    _LineOnly = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        [ DefaultValue( 32767 ) ]
        [ Description( "Gets or sets the maximum length." ) ]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                if( Base != null )
                {
                    _MaxLength = value;
                    Base.MaxLength = _MaxLength;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BudgetTextbox" /> is multiline.
        /// </summary>
        /// <value><c>true</c> if multiline; otherwise, <c>false</c>.</value>
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether this control has multiline support." ) ]
        public bool Multiline
        {
            get
            {
                return _Multiline;
            }
            set
            {
                if( Base != null )
                {
                    _Multiline = value;
                    Base.Multiline = _Multiline;

                    if( value )
                    {
                        Base.Height = checked( Height - 11 );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the password character.
        /// </summary>
        /// <value>The password character.</value>
        [ DefaultValue( "" ) ]
        [ Description( "Sets the password character." ) ]
        public char PasswordChar
        {
            get
            {
                return _PasswordChar;
            }
            set
            {
                if( Base != null )
                {
                    _PasswordChar = value;
                    Base.PasswordChar = _PasswordChar;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it has read only support.
        /// </summary>
        /// <value><c>true</c> if [read only]; otherwise, <c>false</c>.</value>
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether it has read only support." ) ]
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                if( Base != null )
                {
                    _ReadOnly = value;
                    Base.ReadOnly = _ReadOnly;
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
        /// Gets or sets the scroll bars.
        /// </summary>
        /// <value>The scroll bars.</value>
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the scroll bars." ) ]
        public ScrollBars ScrollBars
        {
            get
            {
                return _Scrollbars;
            }
            set
            {
                if( Base != null )
                {
                    _Scrollbars = value;
                    Base.ScrollBars = _Scrollbars;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether shortcuts is enabled.
        /// </summary>
        /// <value><c>true</c> if [shortcuts enabled]; otherwise, <c>false</c>.</value>
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether shortcuts is enabled." ) ]
        public bool ShortcutsEnabled
        {
            get
            {
                return _ShortcutsEnabled;
            }
            set
            {
                if( Base != null )
                {
                    _ShortcutsEnabled = value;
                    Base.ShortcutsEnabled = _ShortcutsEnabled;
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
                            _HoverColor = Design.BudgetColors.AccentBlue;
                            _DisabledColor = Design.BudgetColors.LightDisabled;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _DefaultColor = Design.BudgetColors.DarkDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _HoverColor = Design.BudgetColors.AccentBlue;
                            _DisabledColor = Design.BudgetColors.DarkDisabled;
                            ForeColor = Design.BudgetColors.DarkFont;
                            break;
                        }
                        default:
                        {
                            _AutoStyle = false;
                            break;
                        }
                    }

                    Base.BackColor = _DefaultColor;
                    Base.ForeColor = ForeColor;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [ Category( "Behavior" ) ]
        [ Description( "Sets the text associated with this control." ) ]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;

                if( Base != null )
                {
                    Base.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the text align.
        /// </summary>
        /// <value>The text align.</value>
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the text align." ) ]
        public HorizontalAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                if( Base != null )
                {
                    _TextAlign = value;
                    Base.TextAlign = _TextAlign;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use system password character.
        /// </summary>
        /// <value><c>true</c> if use system password character; otherwise, <c>false</c>.</value>
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to use system password character." ) ]
        public bool UseSystemPasswordChar
        {
            get
            {
                return _UseSystemPasswordChar;
            }
            set
            {
                if( Base != null )
                {
                    _UseSystemPasswordChar = value;
                    Base.UseSystemPasswordChar = _UseSystemPasswordChar;
                }
            }
        }

        /// <summary>
        /// Gets or sets the watermark.
        /// </summary>
        /// <value>The watermark.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( "" ) ]
        [ Description( "Sets the watermark." ) ]
        [ Localizable( true ) ]
        public string Watermark
        {
            get
            {
                return _Watermark;
            }
            set
            {
                _Watermark = value;
                UpdateWatermark( );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether allow word wrap.
        /// </summary>
        /// <value><c>true</c> if [word wrap]; otherwise, <c>false</c>.</value>
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether allow word wrap." ) ]
        public bool WordWrap
        {
            get
            {
                return _WordWrap;
            }
            set
            {
                if( Base != null )
                {
                    _WordWrap = value;
                    Base.WordWrap = _WordWrap;
                }
            }
        }

        /// <summary>
        /// Gets or sets the border.
        /// </summary>
        /// <value>The border.</value>
        public int Border
        {
            get { return border; }
            set
            {
                border = value;
                Invalidate( );
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTextbox" /> class.
        /// </summary>
        public BudgetTextbox( )
        {
            _Style = Design.Style.Light;
            _BorderColor = Design.BudgetColors.LightBorder;
            _HoverColor = Design.BudgetColors.AccentBlue;
            _DefaultColor = Design.BudgetColors.LightDefault;
            _DisabledColor = Design.BudgetColors.LightDisabled;
            _IllegalChars = "";
            _BanIllegalChars = false;
            _Watermark = "";
            _LineOnly = false;
            _MaxLength = 32767;
            _AcceptsReturn = false;
            _AcceptsTab = false;
            _CharacterCasing = CharacterCasing.Normal;
            _HideSelection = false;
            _PasswordChar = '\0';
            _ReadOnly = false;
            _ShortcutsEnabled = true;
            _UseSystemPasswordChar = false;
            _WordWrap = true;
            _Multiline = false;
            _TextAlign = HorizontalAlignment.Left;
            _Scrollbars = ScrollBars.None;
            _AutoStyle = true;
            _MouseState = Helpers.MouseState.None;
            Font = new Font( "Segoe UI", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            CreateBase( );
        }

        /// <summary>
        /// Creates the base.
        /// </summary>
        private void CreateBase( )
        {
            Base = new TextBox( )
            {
                Font = Font,
                Text = Text,
                MaxLength = _MaxLength,
                Cursor = Cursors.IBeam,
                Multiline = _Multiline,
                ReadOnly = _ReadOnly,
                UseSystemPasswordChar = _UseSystemPasswordChar,
                BorderStyle = BorderStyle.None,
                Location = new Point( 5, 4 ),
                Width = checked( Width - 10 ),
                BackColor = _DefaultColor,
                ForeColor = ForeColor
            };

            var metroTextbox = this;
            Base.TextChanged += metroTextbox.OnBaseTextChanged;
            var metroTextbox1 = this;
            Base.GotFocus += metroTextbox1.OnBaseGotFocus;
            var metroTextbox2 = this;
            Base.LostFocus += metroTextbox2.OnBaseLostFocus;
            var metroTextbox3 = this;
            Base.HandleCreated += metroTextbox3.OnBaseHandleCreated;
            var metroTextbox4 = this;
            Base.MouseDoubleClick += metroTextbox4.OnBaseMouseDoubleClick;
            var metroTextbox5 = this;
            Base.MouseHover += metroTextbox5.OnBaseMouseHover;
            var metroTextbox6 = this;
            Base.MouseMove += metroTextbox6.OnBaseMouseMove;
            var metroTextbox7 = this;
            Base.MouseWheel += metroTextbox7.OnBaseMouseWheel;
            var metroTextbox8 = this;
            Base.MouseClick += metroTextbox8.OnBaseMouseClick;
            var metroTextbox9 = this;
            Base.MouseEnter += metroTextbox9.OnBaseMouseEnter;
            var metroTextbox10 = this;
            Base.KeyDown += metroTextbox10.OnBaseKeyDown;
            var metroTextbox11 = this;
            Base.KeyPress += metroTextbox11.OnBaseKeyPress;
            var metroTextbox12 = this;
            Base.KeyUp += metroTextbox12.OnBaseKeyUp;
        }

        /// <summary>
        /// Focuses this instance.
        /// </summary>
        protected new void Focus( )
        {
            Base.Focus( );
            Base.Select( );
        }

        /// <summary>
        /// Gets the ASCII character.
        /// </summary>
        /// <param name="uVirtKey">The u virt key.</param>
        /// <returns>System.Char.</returns>
        private char GetAsciiCharacter( int uVirtKey )
        {
            char chr;
            var numArray = new byte[ 256 ];
            BudgetTextbox.GetKeyboardState( numArray );
            var numArray1 = new byte[ 2 ];

            chr = BudgetTextbox.ToAscii( uVirtKey, 0, numArray, numArray1, 0 ) != 1
                ? new char( )
                : Convert.ToChar( numArray1[ 0 ] );

            return chr;
        }

        /// <summary>
        /// Gets the state of the keyboard.
        /// </summary>
        /// <param name="pbKeyState">State of the pb key.</param>
        /// <returns>System.Int32.</returns>
        [ DllImport( "User32.dll", CharSet = CharSet.None, ExactSpelling = false ) ]
        private static extern int GetKeyboardState( byte[ ] pbKeyState );

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
        /// Handles the <see cref="E:BaseGotFocus" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnBaseGotFocus( object sender, EventArgs e )
        {
            _MouseState = Helpers.MouseState.Over;
            Focus( );
            var gotFocusEventHandler = GotFocus;

            if( gotFocusEventHandler != null )
            {
                gotFocusEventHandler( this, e );
            }

            Invalidate( );
        }

        /// <summary>
        /// Handles the <see cref="E:BaseHandleCreated" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnBaseHandleCreated( object sender, EventArgs e )
        {
            UpdateWatermark( );
        }

        /// <summary>
        /// Handles the <see cref="E:BaseKeyDown" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void OnBaseKeyDown( object sender, KeyEventArgs e )
        {
            if( _BanIllegalChars )
            {
                if( !_IllegalChars.Contains(
                       Conversions.ToString( GetAsciiCharacter( e.KeyValue ) ) ) )
                {
                    var keyDownEventHandler = KeyDown;

                    if( keyDownEventHandler != null )
                    {
                        keyDownEventHandler( this, e );
                    }
                }
                else
                {
                    e.SuppressKeyPress = true;
                    var illegalCharEnteredEventHandler = IllegalCharEntered;

                    if( illegalCharEnteredEventHandler != null )
                    {
                        illegalCharEnteredEventHandler( this, e );
                    }
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="E:BaseKeyPress" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void OnBaseKeyPress( object sender, KeyPressEventArgs e )
        {
            var keyPressEventHandler = KeyPress;

            if( keyPressEventHandler != null )
            {
                keyPressEventHandler( this, e );
            }
        }

        /// <summary>
        /// Handles the <see cref="E:BaseKeyUp" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void OnBaseKeyUp( object sender, KeyEventArgs e )
        {
            var keyDownEventHandler = KeyDown;

            if( keyDownEventHandler != null )
            {
                keyDownEventHandler( this, e );
            }
        }

        /// <summary>
        /// Handles the <see cref="E:BaseLostFocus" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnBaseLostFocus( object sender, EventArgs e )
        {
            _MouseState = Helpers.MouseState.None;
            var lostFocusEventHandler = LostFocus;

            if( lostFocusEventHandler != null )
            {
                lostFocusEventHandler( this, e );
            }

            Invalidate( );
        }

        /// <summary>
        /// Handles the <see cref="E:BaseMouseClick" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void OnBaseMouseClick( object sender, MouseEventArgs e )
        {
            var mouseClickEventHandler = MouseClick;

            if( mouseClickEventHandler != null )
            {
                mouseClickEventHandler( this, e );
            }
        }

        /// <summary>
        /// Handles the <see cref="E:BaseMouseDoubleClick" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void OnBaseMouseDoubleClick( object sender, MouseEventArgs e )
        {
            var mouseDoubleClickEventHandler = MouseDoubleClick;

            if( mouseDoubleClickEventHandler != null )
            {
                mouseDoubleClickEventHandler( this, e );
            }
        }

        /// <summary>
        /// Handles the <see cref="E:BaseMouseEnter" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnBaseMouseEnter( object sender, EventArgs e )
        {
            _MouseState = Helpers.MouseState.Over;
            Invalidate( );
        }

        /// <summary>
        /// Handles the <see cref="E:BaseMouseHover" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnBaseMouseHover( object sender, EventArgs e )
        {
            var mouseHoverEventHandler = MouseHover;

            if( mouseHoverEventHandler != null )
            {
                mouseHoverEventHandler( this, e );
            }
        }

        /// <summary>
        /// Handles the <see cref="E:BaseMouseMove" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void OnBaseMouseMove( object sender, MouseEventArgs e )
        {
            var mouseMoveEventHandler = MouseMove;

            if( mouseMoveEventHandler != null )
            {
                mouseMoveEventHandler( this, e );
            }
        }

        /// <summary>
        /// Handles the <see cref="E:BaseMouseWheel" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void OnBaseMouseWheel( object sender, MouseEventArgs e )
        {
            var mouseWheelEventHandler = MouseWheel;

            if( mouseWheelEventHandler != null )
            {
                mouseWheelEventHandler( this, e );
            }
        }

        /// <summary>
        /// Handles the <see cref="E:BaseTextChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnBaseTextChanged( object sender, EventArgs e )
        {
            Text = Base.Text;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus( EventArgs e )
        {
            _MouseState = Helpers.MouseState.Over;
            Focus( );
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
            if( !Base.Focused )
            {
                _MouseState = Helpers.MouseState.None;
                Invalidate( );
            }

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

            var color = Enabled
                ? _DefaultColor
                : _DisabledColor;

            graphics.Clear( color );
            Base.BackColor = color;

            using( var pen = new Pen( _BorderColor )
                  {
                      Width = Border
                  } )
            {
                if( _MouseState == Helpers.MouseState.Over
                   || _MouseState == Helpers.MouseState.Pressed
                       ? true
                       : false )
                {
                    pen.Color = _HoverColor;
                }

                if( !_LineOnly )
                {
                    var rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                        checked( Height - 1 ) );

                    graphics.DrawRectangle( pen, rectangle );
                }
                else
                {
                    graphics.DrawLine( pen, 0, checked( Height - 1 ), checked( Width - 1 ),
                        checked( Height - 1 ) );
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnParentChanged( EventArgs e )
        {
            if( !Controls.Contains( Base ) )
            {
                Controls.Add( Base );
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize( EventArgs e )
        {
            Base.Location = new Point( 5, 4 );
            Base.Width = checked( Width - 10 );

            if( _Multiline )
            {
                Base.Height = checked( Height - 11 );
            }

            base.OnResize( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnVisibleChanged( EventArgs e )
        {
            Base.Visible = Visible;
            base.OnVisibleChanged( e );
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wp">The wp.</param>
        /// <param name="lp">The lp.</param>
        /// <returns>IntPtr.</returns>
        [ DllImport( "user32.dll", CharSet = CharSet.Unicode, ExactSpelling = false ) ]
        private static extern IntPtr SendMessage( IntPtr hWnd, int msg, IntPtr wp, string lp );

        /// <summary>
        /// To the ASCII.
        /// </summary>
        /// <param name="uVirtKey">The u virt key.</param>
        /// <param name="uScanCode">The u scan code.</param>
        /// <param name="lpbKeyState">State of the LPB key.</param>
        /// <param name="lpChar">The lp character.</param>
        /// <param name="uFlags">The u flags.</param>
        /// <returns>System.Int32.</returns>
        [ DllImport( "User32.dll", CharSet = CharSet.None, ExactSpelling = false ) ]
        private static extern int ToAscii(
            int uVirtKey, int uScanCode, byte[ ] lpbKeyState, byte[ ] lpChar,
            int uFlags );

        /// <summary>
        /// Updates the watermark.
        /// </summary>
        private void UpdateWatermark( )
        {
            if( !Base.IsHandleCreated || _Watermark == null
                   ? false
                   : true )
            {
                var handle = Base.Handle;
                var intPtr = new IntPtr( 1 );
                BudgetTextbox.SendMessage( handle, 5377, intPtr, _Watermark );
            }
        }

        /// <summary>
        /// Occurs when [got focus].
        /// </summary>
        public event GotFocusEventHandler GotFocus;

        /// <summary>
        /// Occurs when [illegal character entered].
        /// </summary>
        public event IllegalCharEnteredEventHandler IllegalCharEntered;

        /// <summary>
        /// Occurs when [key down].
        /// </summary>
        public event KeyDownEventHandler KeyDown;

        /// <summary>
        /// Occurs when [key press].
        /// </summary>
        public event KeyPressEventHandler KeyPress;

        /// <summary>
        /// Occurs when [key up].
        /// </summary>
        public event KeyUpEventHandler KeyUp;

        /// <summary>
        /// Occurs when [lost focus].
        /// </summary>
        public event LostFocusEventHandler LostFocus;

        /// <summary>
        /// Occurs when [mouse click].
        /// </summary>
        public event MouseClickEventHandler MouseClick;

        /// <summary>
        /// Occurs when [mouse double click].
        /// </summary>
        public event MouseDoubleClickEventHandler MouseDoubleClick;

        /// <summary>
        /// Occurs when [mouse hover].
        /// </summary>
        public event MouseHoverEventHandler MouseHover;

        /// <summary>
        /// Occurs when [mouse move].
        /// </summary>
        public event MouseMoveEventHandler MouseMove;

        /// <summary>
        /// Occurs when [mouse wheel].
        /// </summary>
        public event MouseWheelEventHandler MouseWheel;

        /// <summary>
        /// Delegate GotFocusEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void GotFocusEventHandler( object sender, EventArgs e );

        /// <summary>
        /// Delegate IllegalCharEnteredEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public delegate void IllegalCharEnteredEventHandler( object sender, KeyEventArgs e );

        /// <summary>
        /// Delegate KeyDownEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public delegate void KeyDownEventHandler( object sender, KeyEventArgs e );

        /// <summary>
        /// Delegate KeyPressEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public delegate void KeyPressEventHandler( object sender, KeyPressEventArgs e );

        /// <summary>
        /// Delegate KeyUpEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public delegate void KeyUpEventHandler( object sender, KeyEventArgs e );

        /// <summary>
        /// Delegate LostFocusEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void LostFocusEventHandler( object sender, EventArgs e );

        /// <summary>
        /// Delegate MouseClickEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public delegate void MouseClickEventHandler( object sender, MouseEventArgs e );

        /// <summary>
        /// Delegate MouseDoubleClickEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public delegate void MouseDoubleClickEventHandler( object sender, MouseEventArgs e );

        /// <summary>
        /// Delegate MouseHoverEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void MouseHoverEventHandler( object sender, EventArgs e );

        /// <summary>
        /// Delegate MouseMoveEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public delegate void MouseMoveEventHandler( object sender, MouseEventArgs e );

        /// <summary>
        /// Delegate MouseWheelEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        public delegate void MouseWheelEventHandler( object sender, MouseEventArgs e );
    }
}