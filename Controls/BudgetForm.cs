// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetForm.cs" company="Terry D. Eppler">
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
//   BudgetForm.cs
// </summary>
// ******************************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using System.Collections;
using System.Runtime.InteropServices;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetForm.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    [ Description( "Ein Windows.Forms-Steuerelement im Budget Stil." ) ]
    public class BudgetForm : Form
    {
        #region Private Fields

        /// <summary>
        /// The aero enabled
        /// </summary>
        private bool _aeroEnabled;

        /// <summary>
        /// The accent color
        /// </summary>
        private Color _AccentColor;

        /// <summary>
        /// The draw borders
        /// </summary>
        private bool _DrawBorders;

        /// <summary>
        /// The allow resize
        /// </summary>
        private bool _AllowResize;

        /// <summary>
        /// The state
        /// </summary>
        private FormState _State;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The resize border width
        /// </summary>
        private int _ResizeBorderWidth;

        /// <summary>
        /// The text rectangle
        /// </summary>
        private Rectangle _TextRectangle;

        /// <summary>
        /// The resize dir
        /// </summary>
        private ResizeDirection _resizeDir;

        /// <summary>
        /// The hide border when maximized
        /// </summary>
        private bool _HideBorderWhenMaximized;

        /// <summary>
        /// The align text to control box
        /// </summary>
        private bool _AlignTextToControlBox;

        /// <summary>
        /// The main control box
        /// </summary>
        private BudgetControlBox _MainControlBox;

        /// <summary>
        /// The is active
        /// </summary>
        private bool _IsActive;

        /// <summary>
        /// The use gradient back color
        /// </summary>
        private bool _UseGradientBackColor;

        /// <summary>
        /// The gradient brush
        /// </summary>
        private LinearGradientBrush _GradientBrush;

        /// <summary>
        /// The form border style
        /// </summary>
        private FormBorderStyle _FormBorderStyle;

        /// <summary>
        /// The wm nclbuttondown
        /// </summary>
        private const int WM_NCLBUTTONDOWN = 161;

        /// <summary>
        /// The htborder
        /// </summary>
        private const int HTBORDER = 18;

        /// <summary>
        /// The htbottom
        /// </summary>
        private const int HTBOTTOM = 15;

        /// <summary>
        /// The htbottomleft
        /// </summary>
        private const int HTBOTTOMLEFT = 16;

        /// <summary>
        /// The htbottomright
        /// </summary>
        private const int HTBOTTOMRIGHT = 17;

        /// <summary>
        /// The htcaption
        /// </summary>
        private const int HTCAPTION = 2;

        /// <summary>
        /// The htleft
        /// </summary>
        private const int HTLEFT = 10;

        /// <summary>
        /// The htright
        /// </summary>
        private const int HTRIGHT = 11;

        /// <summary>
        /// The httop
        /// </summary>
        private const int HTTOP = 12;

        /// <summary>
        /// The httopleft
        /// </summary>
        private const int HTTOPLEFT = 13;

        /// <summary>
        /// The httopright
        /// </summary>
        private const int HTTOPRIGHT = 14;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the color of the accent.
        /// </summary>
        /// <value>The color of the accent.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( typeof( Color ), "0, 122, 204" ) ]
        [ Description( "Sets the color of the accent." ) ]
        [ RefreshProperties( RefreshProperties.All ) ]
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
                    var color = _AccentColor;

                    if( color == Color.FromArgb( 0, 122, 204 ) )
                    {
                        _State = FormState.Normal;
                    }
                    else if( color == Color.FromArgb( 104, 33, 122 ) )
                    {
                        _State = FormState.Finished;
                    }
                    else if( color != Color.FromArgb( 202, 81, 0 ) )
                    {
                        _State = FormState.Custom;
                    }
                    else
                    {
                        _State = FormState.Busy;
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to align text to control box.
        /// </summary>
        /// <value><c>true</c> if align text to control box; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to align text to control box." ) ]
        public bool AlignTextToControlBox
        {
            get
            {
                return _AlignTextToControlBox;
            }
            set
            {
                if( _AlignTextToControlBox != value )
                {
                    _AlignTextToControlBox = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow resize.
        /// </summary>
        /// <value><c>true</c> if allow resize; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to allow resize." ) ]
        public bool AllowResize
        {
            get
            {
                return _AllowResize;
            }
            set
            {
                if( _AllowResize != value )
                {
                    _AllowResize = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.
        /// </summary>
        /// <value><c>true</c> if [control box]; otherwise, <c>false</c>.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new bool ControlBox
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw borders.
        /// </summary>
        /// <value><c>true</c> if draw borders; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw borders." ) ]
        public bool DrawBorders
        {
            get
            {
                return _DrawBorders;
            }
            set
            {
                if( _DrawBorders != value )
                {
                    _DrawBorders = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the border style of the form.
        /// </summary>
        /// <value>The form border style.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new FormBorderStyle FormBorderStyle
        {
            get
            {
                return _FormBorderStyle;
            }
            set
            {
                FindForm( ).FormBorderStyle = FormBorderStyle.None;
            }
        }

        /// <summary>
        /// Gets or sets the gradient brush.
        /// </summary>
        /// <value>The gradient brush.</value>
        [ Browsable( false ) ]
        [ Category( "Appearance" ) ]
        [ Description( "Sets the gradient brush." ) ]
        public LinearGradientBrush GradientBrush
        {
            get
            {
                return _GradientBrush;
            }
            set
            {
                if( value != _GradientBrush )
                {
                    _GradientBrush = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the control has aero enabled.
        /// </summary>
        /// <value><c>true</c> if has aero; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Gets a value indicating whether the control has aero enabled." ) ]
        public bool HasAero
        {
            get
            {
                return _aeroEnabled;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this control has focus.
        /// </summary>
        /// <value><c>true</c> if this control has focus; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ Description( "Gets a value indicating whether this control has focus." ) ]
        public bool HasFocus
        {
            get
            {
                return _IsActive;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.
        /// </summary>
        /// <value><c>true</c> if help button; otherwise, <c>false</c>.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new bool HelpButton
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide border when maximized.
        /// </summary>
        /// <value><c>true</c> if hide border when maximized; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to hide border when maximized." ) ]
        public bool HideBorderWhenMaximized
        {
            get
            {
                return _HideBorderWhenMaximized;
            }
            set
            {
                if( _HideBorderWhenMaximized != value )
                {
                    _HideBorderWhenMaximized = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the main control box.
        /// </summary>
        /// <value>The main control box.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( null ) ]
        [ Description( "Sets the main control box." ) ]
        public BudgetControlBox MainControlBox
        {
            get
            {
                return _MainControlBox;
            }
            set
            {
                if( value != _MainControlBox )
                {
                    _MainControlBox = value;
                    RelocateMainControlBox( );
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.
        /// </summary>
        /// <value><c>true</c> if [maximize box]; otherwise, <c>false</c>.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new bool MaximizeBox
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.
        /// </summary>
        /// <value><c>true</c> if minimize box; otherwise, <c>false</c>.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new bool MinimizeBox
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets the width of the resize border.
        /// </summary>
        /// <value>The width of the resize border.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( 6 ) ]
        [ Description( "Sets the width of the resize border." ) ]
        public int ResizeBorderWidth
        {
            get
            {
                return _ResizeBorderWidth;
            }
            set
            {
                if( _ResizeBorderWidth != value )
                {
                    _ResizeBorderWidth = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the resize direction.
        /// </summary>
        /// <value>The resize direction.</value>
        [ Browsable( false ) ]
        [ Category( "Behavior" ) ]
        [ Description( "Sets the resize direction." ) ]
        private ResizeDirection ResizeDirect
        {
            get
            {
                return _resizeDir;
            }
            set
            {
                _resizeDir = value;

                switch( value )
                {
                    case ResizeDirection.Left:
                    {
                        Cursor = Cursors.SizeWE;
                        break;
                    }
                    case ResizeDirection.TopLeft:
                    {
                        Cursor = Cursors.SizeNWSE;
                        break;
                    }
                    case ResizeDirection.Top:
                    {
                        Cursor = Cursors.SizeNS;
                        break;
                    }
                    case ResizeDirection.TopRight:
                    {
                        Cursor = Cursors.SizeNESW;
                        break;
                    }
                    case ResizeDirection.Right:
                    {
                        Cursor = Cursors.SizeWE;
                        break;
                    }
                    case ResizeDirection.BottomRight:
                    {
                        Cursor = Cursors.SizeNWSE;
                        break;
                    }
                    case ResizeDirection.Bottom:
                    {
                        Cursor = Cursors.SizeNS;
                        break;
                    }
                    case ResizeDirection.BottomLeft:
                    {
                        Cursor = Cursors.SizeNESW;
                        break;
                    }
                    default:
                    {
                        Cursor = Cursors.Default;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the state of the form." ) ]
        [ RefreshProperties( RefreshProperties.All ) ]
        public FormState State
        {
            get
            {
                return _State;
            }
            set
            {
                if( value != _State )
                {
                    _State = value;

                    if( _Style == Design.Style.Light )
                    {
                        switch( _State )
                        {
                            case FormState.Normal:
                            {
                                _AccentColor = Color.FromArgb( 0, 122, 204 );
                                break;
                            }
                            case FormState.Finished:
                            {
                                _AccentColor = Color.FromArgb( 104, 33, 122 );
                                break;
                            }
                            case FormState.Busy:
                            {
                                _AccentColor = Color.FromArgb( 202, 81, 0 );
                                break;
                            }
                        }
                    }
                    else if( _Style == Design.Style.Dark )
                    {
                        switch( _State )
                        {
                            case FormState.Normal:
                            {
                                _AccentColor = Color.FromArgb( 0, 122, 204 );
                                break;
                            }
                            case FormState.Finished:
                            {
                                _AccentColor = Color.FromArgb( 104, 33, 122 );
                                break;
                            }
                            case FormState.Busy:
                            {
                                _AccentColor = Color.FromArgb( 202, 81, 0 );
                                break;
                            }
                        }
                    }

                    Invalidate( );
                }
            }
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
                if( _Style != value )
                {
                    _Style = value;

                    switch( _State )
                    {
                        case FormState.Normal:
                        {
                            _AccentColor = Color.FromArgb( 0, 122, 204 );
                            break;
                        }
                        case FormState.Finished:
                        {
                            _AccentColor = Color.FromArgb( 104, 33, 122 );
                            break;
                        }
                        case FormState.Busy:
                        {
                            _AccentColor = Color.FromArgb( 202, 81, 0 );
                            break;
                        }
                    }

                    if( value == Design.Style.Light )
                    {
                        BackColor = Color.White;
                        ForeColor = Color.Black;
                    }
                    else if( _Style == Design.Style.Dark )
                    {
                        BackColor = Color.FromArgb( 40, 40, 40 );
                        ForeColor = SystemColors.ControlDark;
                    }

                    StyleSpecialControls( this );
                    var eventHandler = FormStyleChanged;

                    if( eventHandler != null )
                    {
                        eventHandler( this, new BudgetFormEventArgs( value ) );
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use gradient back color.
        /// </summary>
        /// <value><c>true</c> if use gradient back color; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to use gradient back color." ) ]
        public bool UseGradientBackColor
        {
            get
            {
                return _UseGradientBackColor;
            }
            set
            {
                if( value != _UseGradientBackColor )
                {
                    _UseGradientBackColor = value;
                    Invalidate( );
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetForm" /> class.
        /// </summary>
        public BudgetForm( )
        {
            _AccentColor = Color.FromArgb( 0, 122, 204 );
            _DrawBorders = true;
            _AllowResize = true;
            _State = FormState.Normal;
            _Style = Design.Style.Light;
            _ResizeBorderWidth = 6;
            _TextRectangle = new Rectangle( 32, 7, checked( Width - 1 ), checked( Height - 1 ) );
            _resizeDir = ResizeDirection.None;
            _HideBorderWhenMaximized = true;
            _AlignTextToControlBox = true;
            _MainControlBox = null;
            _IsActive = false;
            _UseGradientBackColor = false;
            var point = new Point( 0, 0 );
            var point1 = new Point( Width, Height );

            _GradientBrush = new LinearGradientBrush( point, point1, Color.FromArgb( 184, 43, 86 ),
                Color.FromArgb( 94, 59, 149 ) );

            _FormBorderStyle = FormBorderStyle.None;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.White;
            ForeColor = Color.Black;
            Font = new Font( "Segoe UI", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Gets the create parameters.
        /// </summary>
        /// <value>The create parameters.</value>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParam;
                CheckAeroEnabled( );
                var createParams = base.CreateParams;

                if( _aeroEnabled )
                {
                    createParam = createParams;
                }
                else
                {
                    createParams.ClassStyle = createParams.ClassStyle | 131072;
                    createParam = createParams;
                }

                return createParam;
            }
        }

        /// <summary>
        /// Checks the aero enabled.
        /// </summary>
        private void CheckAeroEnabled( )
        {
            if( Environment.OSVersion.Version.Major < 6 )
            {
                _aeroEnabled = false;
            }
            else
            {
                var num = 0;
                NativeMethods.DwmIsCompositionEnabled( ref num );
                _aeroEnabled = num == 1;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnActivated( EventArgs e )
        {
            _IsActive = true;
            Invalidate( );
            base.OnActivated( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Deactivate" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnDeactivate( EventArgs e )
        {
            _IsActive = false;
            Invalidate( );
            base.OnDeactivate( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown( MouseEventArgs e )
        {
            if( _AllowResize
               && e.Button == MouseButtons.Left & WindowState != FormWindowState.Maximized )
            {
                ResizeForm( ResizeDirect );
            }

            base.OnMouseDown( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove( MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {
                Capture = false;
                var message = Message.Create( Handle, 161, (IntPtr)2, IntPtr.Zero );
                WndProc( ref message );
            }

            if( _AllowResize )
            {
                if( e.Location.X < _ResizeBorderWidth & e.Location.Y < _ResizeBorderWidth )
                {
                    ResizeDirect = ResizeDirection.TopLeft;
                }
                else if( e.Location.X < _ResizeBorderWidth
                        & e.Location.Y > checked( Height - _ResizeBorderWidth ) )
                {
                    ResizeDirect = ResizeDirection.BottomLeft;
                }
                else if( e.Location.X > checked( Width - _ResizeBorderWidth )
                        & e.Location.Y > checked( Height - _ResizeBorderWidth ) )
                {
                    ResizeDirect = ResizeDirection.BottomRight;
                }
                else if( e.Location.X > checked( Width - _ResizeBorderWidth )
                        & e.Location.Y < _ResizeBorderWidth )
                {
                    ResizeDirect = ResizeDirection.TopRight;
                }
                else if( e.Location.X < _ResizeBorderWidth )
                {
                    ResizeDirect = ResizeDirection.Left;
                }
                else if( e.Location.X > checked( Width - _ResizeBorderWidth ) )
                {
                    ResizeDirect = ResizeDirection.Right;
                }
                else if( e.Location.Y < _ResizeBorderWidth )
                {
                    ResizeDirect = ResizeDirection.Top;
                }
                else if( e.Location.Y <= checked( Height - _ResizeBorderWidth ) )
                {
                    ResizeDirect = ResizeDirection.None;
                }
                else
                {
                    ResizeDirect = ResizeDirection.Bottom;
                }
            }

            base.OnMouseMove( e );
        }

        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            Rectangle rectangle;
            Rectangle rectangle1;
            var graphics = e.Graphics;

            if( _UseGradientBackColor )
            {
                graphics.FillRectangle( _GradientBrush, ClientRectangle );
            }

            using( var pen = new Pen( _AccentColor ) )
            {
                if( _DrawBorders )
                {
                    if( WindowState != FormWindowState.Maximized )
                    {
                        rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                            checked( Height - 1 ) );

                        graphics.DrawRectangle( pen, rectangle );
                    }
                    else if( !_HideBorderWhenMaximized )
                    {
                        rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                            checked( Height - 1 ) );

                        graphics.DrawRectangle( pen, rectangle );
                    }
                }
            }

            using( var solidBrush = new SolidBrush( _IsActive
                      ? ForeColor
                      : Design.BudgetColors.ChangeColorBrightness( ForeColor, -0.2f ) ) )
            {
                if( ShowIcon )
                {
                    rectangle = new Rectangle( 32, 7, checked( Width - 1 ), checked( Height - 1 ) );
                    rectangle1 = rectangle;
                }
                else
                {
                    var rectangle2 = new Rectangle( 8, 7, checked( Width - 1 ),
                        checked( Height - 1 ) );

                    rectangle1 = rectangle2;
                }

                _TextRectangle = rectangle1;
                var rectangle3 = new Rectangle( 8, 6, 16, 16 );

                if( _MainControlBox != null )
                {
                    if( _AlignTextToControlBox )
                    {
                        var location = _TextRectangle.Location;
                        var x = location.X;
                        var size = MainControlBox.Size;

                        var point = new Point( x,
                            checked( checked( (int)Math.Round( (double)size.Height / 2 ) ) - 4 ) );

                        _TextRectangle.Location = point;
                        point = rectangle3.Location;
                        var num = point.X;
                        size = MainControlBox.Size;

                        location = new Point( num,
                            checked( checked( (int)Math.Round( (double)size.Height / 2 ) ) - 4 ) );

                        rectangle3.Location = location;
                    }
                }

                if( ShowIcon )
                {
                    graphics.DrawImage( Design.Drawing.ExtractIcon( Icon, 16 ), rectangle3 );
                }

                graphics.DrawString( Text, Font, solidBrush, _TextRectangle,
                    StringFormat.GenericDefault );
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnShown( EventArgs e )
        {
            if( _MainControlBox != null )
            {
                RelocateMainControlBox( );
            }

            if( _Style == Design.Style.Dark )
            {
                Style = Design.Style.Light;
                Style = Design.Style.Dark;
            }
            else if( Style == Design.Style.Light )
            {
                Style = Design.Style.Dark;
                Style = Design.Style.Light;
            }

            base.OnShown( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged( EventArgs e )
        {
            if( _MainControlBox != null )
            {
                RelocateMainControlBox( );
            }

            _GradientBrush.Dispose( );
            var point = new Point( 0, 0 );
            var point1 = new Point( Width, Height );

            _GradientBrush = new LinearGradientBrush( point, point1, Color.FromArgb( 184, 43, 86 ),
                Color.FromArgb( 94, 59, 149 ) );

            base.OnSizeChanged( e );
        }

        /// <summary>
        /// Releases the capture.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [ DllImport( "user32.dll", CharSet = CharSet.None, ExactSpelling = false ) ]
        public static extern bool ReleaseCapture( );

        /// <summary>
        /// Relocates the main control box.
        /// </summary>
        public void RelocateMainControlBox( )
        {
            if( _MainControlBox != null )
            {
                var metroControlBox = _MainControlBox;
                var width = Width;
                var size = _MainControlBox.Size;
                var point = new Point( checked( checked( width - size.Width ) - 1 ), 1 );
                metroControlBox.Location = point;
            }
        }

        /// <summary>
        /// Resizes the form.
        /// </summary>
        /// <param name="direction">The direction.</param>
        private void ResizeForm( ResizeDirection direction )
        {
            var num = -1;

            switch( direction )
            {
                case ResizeDirection.Left:
                {
                    num = 10;
                    break;
                }
                case ResizeDirection.TopLeft:
                {
                    num = 13;
                    break;
                }
                case ResizeDirection.Top:
                {
                    num = 12;
                    break;
                }
                case ResizeDirection.TopRight:
                {
                    num = 14;
                    break;
                }
                case ResizeDirection.Right:
                {
                    num = 11;
                    break;
                }
                case ResizeDirection.BottomRight:
                {
                    num = 17;
                    break;
                }
                case ResizeDirection.Bottom:
                {
                    num = 15;
                    break;
                }
                case ResizeDirection.BottomLeft:
                {
                    num = 16;
                    break;
                }
            }

            if( num != -1 )
            {
                BudgetForm.ReleaseCapture( );
                BudgetForm.SendMessage( Handle, 161, num, 0 );
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="Msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>System.Int32.</returns>
        [ DllImport( "user32.dll", CharSet = CharSet.None, ExactSpelling = false ) ]
        public static extern int SendMessage( IntPtr hWnd, int Msg, int wParam, int lParam );

        /// <summary>
        /// Styles the special controls.
        /// </summary>
        /// <param name="ct">The ct.</param>
        private void StyleSpecialControls( Control ct )
        {
            IEnumerator enumerator = null;
            bool flag;

            try
            {
                try
                {
                    enumerator = ct.Controls.GetEnumerator( );

                    while( enumerator.MoveNext( ) )
                    {
                        var current = (Control)enumerator.Current;

                        if( current is BudgetListbox )
                        {
                            var style = (BudgetListbox)current;

                            if( style.AutoStyle )
                            {
                                style.Style = Style;
                            }

                            style.Invalidate( );
                        }
                        else if( current is BudgetChecker )
                        {
                            var metroChecker = (BudgetChecker)current;

                            if( metroChecker.AutoStyle )
                            {
                                metroChecker.Style = Style;
                            }

                            metroChecker.Invalidate( );
                        }
                        else if( !( current is BudgetTracker ) )
                        {
                            if( !( current is TabPage )
                               && !( current is Panel ) )
                            {
                                if( current is BudgetPanelCategory )
                                {
                                    goto Label1;
                                }

                                flag = false;
                                goto Label0;
                            }

                            Label1:
                            flag = true;
                            Label0:

                            if( flag )
                            {
                                StyleSpecialControls( current );
                            }
                        }
                        else
                        {
                            var metroTracker = (BudgetTracker)current;

                            if( metroTracker.AutoStyle )
                            {
                                metroTracker.Style = Style;
                            }

                            metroTracker.Invalidate( );
                        }
                    }
                }
                finally
                {
                    if( enumerator is IDisposable )
                    {
                        ( enumerator as IDisposable ).Dispose( );
                    }
                }
            }
            catch( Exception exception )
            {
                ProjectData.SetProjectError( exception );
                ProjectData.ClearProjectError( );
            }
        }

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc( ref Message m )
        {
            if( m.Msg == 133 )
            {
                var num = 2;

                if( _aeroEnabled )
                {
                    NativeMethods.DwmSetWindowAttribute( Handle, 2, ref num, 4 );

                    var mARGIN = new NativeStructs.MARGINS( )
                    {
                        bottomHeight = 1,
                        leftWidth = 1,
                        rightWidth = 1,
                        topHeight = 1
                    };

                    NativeMethods.DwmExtendFrameIntoClientArea( Handle, ref mARGIN );
                }
            }

            base.WndProc( ref m );
        }

        /// <summary>
        /// Occurs when [form style changed].
        /// </summary>
        public event EventHandler<BudgetFormEventArgs> FormStyleChanged;

        /// <summary>
        /// Enum FormState
        /// </summary>
        public enum FormState
        {
            /// <summary>
            /// The normal
            /// </summary>
            Normal,

            /// <summary>
            /// The finished
            /// </summary>
            Finished,

            /// <summary>
            /// The busy
            /// </summary>
            Busy,

            /// <summary>
            /// The custom
            /// </summary>
            Custom
        }

        /// <summary>
        /// Class BudgetFormEventArgs.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class BudgetFormEventArgs : EventArgs
        {
            /// <summary>
            /// The style
            /// </summary>
            private Design.Style _style;

            /// <summary>
            /// Gets the form style.
            /// </summary>
            /// <value>The form style.</value>
            public Design.Style FormStyle
            {
                get
                {
                    return _style;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="BudgetFormEventArgs"/> class.
            /// </summary>
            /// <param name="style">The style.</param>
            public BudgetFormEventArgs( Design.Style style )
            {
                _style = style;
            }
        }

        /// <summary>
        /// Class NativeConstants.
        /// </summary>
        private class NativeConstants
        {
            /// <summary>
            /// The cs dropshadow
            /// </summary>
            public const int CS_DROPSHADOW = 131072;

            /// <summary>
            /// The wm ncpaint
            /// </summary>
            public const int WM_NCPAINT = 133;

            /// <summary>
            /// Initializes a new instance of the <see cref="NativeConstants"/> class.
            /// </summary>
            [ DebuggerNonUserCode ]
            public NativeConstants( )
            {
            }
        }

        /// <summary>
        /// Class NativeMethods.
        /// </summary>
        private class NativeMethods
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NativeMethods"/> class.
            /// </summary>
            [ DebuggerNonUserCode ]
            public NativeMethods( )
            {
            }

            /// <summary>
            /// DWMs the extend frame into client area.
            /// </summary>
            /// <param name="hWnd">The h WND.</param>
            /// <param name="pMarInset">The p mar inset.</param>
            /// <returns>System.Int32.</returns>
            [ DllImport( "dwmapi", CharSet = CharSet.None, ExactSpelling = false ) ]
            public static extern int DwmExtendFrameIntoClientArea(
                IntPtr hWnd, ref NativeStructs.MARGINS pMarInset );

            /// <summary>
            /// DWMs the is composition enabled.
            /// </summary>
            /// <param name="pfEnabled">The pf enabled.</param>
            /// <returns>System.Int32.</returns>
            [ DllImport( "dwmapi.dll", CharSet = CharSet.None, ExactSpelling = false ) ]
            public static extern int DwmIsCompositionEnabled( ref int pfEnabled );

            /// <summary>
            /// DWMs the set window attribute.
            /// </summary>
            /// <param name="hwnd">The HWND.</param>
            /// <param name="attr">The attribute.</param>
            /// <param name="attrValue">The attribute value.</param>
            /// <param name="attrSize">Size of the attribute.</param>
            /// <returns>System.Int32.</returns>
            [ DllImport( "dwmapi", CharSet = CharSet.None, ExactSpelling = false ) ]
            internal static extern int DwmSetWindowAttribute(
                IntPtr hwnd, int attr, ref int attrValue, int attrSize );
        }

        /// <summary>
        /// Class NativeStructs.
        /// </summary>
        private class NativeStructs
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NativeStructs"/> class.
            /// </summary>
            [ DebuggerNonUserCode ]
            public NativeStructs( )
            {
            }

            /// <summary>
            /// Struct MARGINS
            /// </summary>
            public struct MARGINS
            {
                /// <summary>
                /// The left width
                /// </summary>
                public int leftWidth;

                /// <summary>
                /// The right width
                /// </summary>
                public int rightWidth;

                /// <summary>
                /// The top height
                /// </summary>
                public int topHeight;

                /// <summary>
                /// The bottom height
                /// </summary>
                public int bottomHeight;
            }
        }

        /// <summary>
        /// Enum ResizeDirection
        /// </summary>
        private enum ResizeDirection
        {
            /// <summary>
            /// The none
            /// </summary>
            None,

            /// <summary>
            /// The left
            /// </summary>
            Left,

            /// <summary>
            /// The top left
            /// </summary>
            TopLeft,

            /// <summary>
            /// The top
            /// </summary>
            Top,

            /// <summary>
            /// The top right
            /// </summary>
            TopRight,

            /// <summary>
            /// The right
            /// </summary>
            Right,

            /// <summary>
            /// The bottom right
            /// </summary>
            BottomRight,

            /// <summary>
            /// The bottom
            /// </summary>
            Bottom,

            /// <summary>
            /// The bottom left
            /// </summary>
            BottomLeft
        }

        #endregion
    }
}