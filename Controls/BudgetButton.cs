// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetButton.cs" company="Terry D. Eppler">
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
//   BudgetButton.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace BudgetExecution
{
    using System.Security.Permissions;
    using System.Windows.Forms.Design;

    /// <summary>
    /// A class collection for rendering a metro-style button.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "Click" ) ]
    [ Description( "This is a metro style button." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( Button ) ) ]
    [ Designer( typeof( BudgetButtonDesigner ) ) ]
    public class BudgetButton : Control
    {
        #region Private Fields

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
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The draw borders
        /// </summary>
        private bool _DrawBorders;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The disabled color
        /// </summary>
        private Color _DisabledColor;

        /// <summary>
        /// The alignment
        /// </summary>
        private StringAlignment _alignment;

        /// <summary>
        /// The dialog result
        /// </summary>
        private DialogResult _DialogResult;

        /// <summary>
        /// The image
        /// </summary>
        private Image _Image;

        /// <summary>
        /// The is round
        /// </summary>
        private bool _IsRound;

        /// <summary>
        /// The rounding arc
        /// </summary>
        private int _RoundingArc;

        /// <summary>
        /// The invert fore color
        /// </summary>
        private bool _InvertForeColor;

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
        /// Gets or sets a value indicating whether to set automatic style.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets the AutoStyle." ) ]
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
        [ Description( "This sets the border color." ) ]
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
        [ Description( "This sets the default color." ) ]
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
        /// Gets or sets the dialog result.
        /// </summary>
        /// <value>The dialog result.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "This sets the dialog result." ) ]
        public DialogResult DialogResult
        {
            get
            {
                return _DialogResult;
            }
            set
            {
                if( _DialogResult != value )
                {
                    _DialogResult = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the disabled.
        /// </summary>
        /// <value>The color of the disabled.</value>
        [ Category( "Appearance" ) ]
        [ Description( "This sets the disabled color." ) ]
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
        /// Gets or sets a value indicating whether to draw border.
        /// </summary>
        /// <value><c>true</c> if draw border; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Set to draw the border." ) ]
        public bool DrawBorder
        {
            get
            {
                return _DrawBorders;
            }
            set
            {
                if( value != _DrawBorders )
                {
                    _DrawBorders = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the hover color." ) ]
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
                return _Image;
            }
            set
            {
                if( value != _Image )
                {
                    _Image = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to invert fore color.
        /// </summary>
        /// <value><c>true</c> if invert fore color; otherwise, <c>false</c>.</value>
        [ Category( "Appereance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Set to invert the forecolor." ) ]
        public bool InvertForeColor
        {
            get
            {
                return _InvertForeColor;
            }
            set
            {
                if( _InvertForeColor != value )
                {
                    _InvertForeColor = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this control is rounded.
        /// </summary>
        /// <value><c>true</c> if this control is rounded; otherwise, <c>false</c>.</value>
        [ Category( "Appereance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Set to enable rounding of the button." ) ]
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
        /// Gets or sets the color of the pressed.
        /// </summary>
        /// <value>The color of the pressed.</value>
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
        /// Gets or sets the rounding arc.
        /// </summary>
        /// <value>The rounding arc.</value>
        [ Browsable( true ) ]
        [ Category( "Appereance" ) ]
        [ Description( "Sets the rounding value." ) ]
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
                            _HoverColor = Design.BudgetColors.LightDefault;
                            _PressedColor = Design.BudgetColors.AccentBlue;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _DisabledColor = Design.BudgetColors.LightDisabled;
                            ForeColor = Design.BudgetColors.LightFont;
                            _InvertForeColor = false;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _DefaultColor = Design.BudgetColors.DarkDefault;
                            _HoverColor = Design.BudgetColors.DarkHover;
                            _PressedColor = Design.BudgetColors.AccentBlue;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _DisabledColor = Design.BudgetColors.DarkDisabled;
                            ForeColor = Design.BudgetColors.DarkFont;
                            _InvertForeColor = false;
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
        /// <value>The text alignment.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 1 ) ]
        [ Description( "Sets the text alignment." ) ]
        public StringAlignment TextAlignment
        {
            get
            {
                return _alignment;
            }
            set
            {
                if( value != _alignment )
                {
                    _alignment = value;
                    Invalidate( );
                }
            }
        }/**/

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetButton" /> class.
        /// </summary>
        public BudgetButton( )
        {
            _DefaultColor = Design.BudgetColors.LightDefault;
            _HoverColor = Design.BudgetColors.LightDefault;
            _PressedColor = Design.BudgetColors.AccentBlue;
            _BorderColor = Design.BudgetColors.LightBorder;
            _DrawBorders = true;
            _Style = Design.Style.Light;
            _DisabledColor = Design.BudgetColors.LightDisabled;
            _alignment = StringAlignment.Center;
            _DialogResult = DialogResult.None;
            _Image = null;
            _IsRound = false;
            _RoundingArc = 23;
            _InvertForeColor = false;
            _AutoStyle = true;
            _MouseState = Helpers.MouseState.None;
            Font = new Font( "Segoe UI", 9f );

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
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseClick( MouseEventArgs e )
        {
            if( FindForm( ) != null )
            {
                if( FindForm( ).Modal )
                {
                    FindForm( ).DialogResult = _DialogResult;
                }
            }

            base.OnMouseClick( e );
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
            Rectangle rectangle;
            var graphics = e.Graphics;

            using( var pen = new Pen( _DefaultColor ) )
            {
                var color = _BorderColor;
                var flag = false;

                switch( _MouseState )
                {
                    case Helpers.MouseState.None:
                    {
                        pen.Color = _DefaultColor;
                        color = _BorderColor;
                        break;
                    }
                    case Helpers.MouseState.Over:
                    {
                        pen.Color = _HoverColor;
                        color = _PressedColor;
                        break;
                    }
                    case Helpers.MouseState.Pressed:
                    {
                        pen.Color = _PressedColor;
                        color = _PressedColor;

                        if( _Style == Design.Style.Light || _Style == Design.Style.Dark
                               ? false
                               : true )
                        {
                            break;
                        }

                        flag = true;
                        break;
                    }
                }

                if( !Enabled )
                {
                    pen.Color = _DisabledColor;
                }

                if( !_IsRound )
                {
                    graphics.Clear( pen.Color );
                }
                else
                {
                    using( var solidBrush = new SolidBrush( pen.Color ) )
                    {
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                            checked( Height - 1 ) );

                        Design.Drawing.FillRoundedPath( graphics, solidBrush, rectangle,
                            _RoundingArc, true, true, true, true );
                    }
                }

                pen.Color = color;

                if( _DrawBorders )
                {
                    if( !_IsRound )
                    {
                        rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                            checked( Height - 1 ) );

                        graphics.DrawRectangle( pen, rectangle );
                    }
                    else
                    {
                        var color1 = pen.Color;

                        rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                            checked( Height - 1 ) );

                        Design.Drawing.DrawRoundedPath( graphics, color1, 1f, rectangle,
                            _RoundingArc, true, true, true, true );
                    }
                }

                var stringFormat = new StringFormat( )
                {
                    Alignment = _alignment,
                    LineAlignment = StringAlignment.Center
                };

                using( var stringFormat1 = stringFormat )
                {
                    using( var correctForeColor = new SolidBrush( flag
                              ? Color.White
                              : ForeColor ) )
                    {
                        correctForeColor.Color =
                            _InvertForeColor & _MouseState == Helpers.MouseState.Pressed
                                ? Design.BudgetColors.InvertColor( correctForeColor.Color )
                                : correctForeColor.Color;

                        if( !Enabled )
                        {
                            correctForeColor.Color =
                                Design.BudgetColors.GetCorrectForeColor( _Style, ForeColor,
                                    Enabled );
                        }

                        if( _Image == null )
                        {
                            var text = Text;
                            var font = Font;

                            rectangle = new Rectangle( 0, 0, checked( Width - 1 ),
                                checked( Height - 1 ) );

                            graphics.DrawString( text, font, correctForeColor, rectangle,
                                stringFormat1 );
                        }
                        else
                        {
                            var image = _Image;

                            rectangle = new Rectangle( 6,
                                checked( checked( (int)Math.Round( (double)Height / 2 ) )
                                    - checked( (int)Math.Round( (double)_Image.Height / 2 ) ) ),
                                _Image.Width, _Image.Height );

                            graphics.DrawImage( image, rectangle );
                            var str = Text;
                            var font1 = Font;

                            rectangle = new Rectangle( checked( 12 + _Image.Width ), 0,
                                checked( checked( Width - 12 ) - _Image.Width ),
                                checked( Height - 1 ) );

                            graphics.DrawString( str, font1, correctForeColor, rectangle,
                                stringFormat1 );
                        }
                    }
                }
            }

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
    }

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(BudgetButtonDesigner))] --------------------//

    #endregion

    #region ControlDesigner

    /// <summary>
    /// Class BudgetButtonDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [ PermissionSet( SecurityAction.Demand, Name = "FullTrust" ) ]
    public class BudgetButtonDesigner : ControlDesigner
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
                    actionLists.Add( new BudgetButtonSmartTagActionList( Component ) );
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
    /// Class BudgetButtonSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class BudgetButtonSmartTagActionList : DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private BudgetButton colUserControl;

        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetButtonSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public BudgetButtonSmartTagActionList( IComponent component )
            : base( component )
        {
            colUserControl = component as BudgetButton;

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
        /// Gets or sets the color of the disabled.
        /// </summary>
        /// <value>The color of the disabled.</value>
        public Color DisabledColor
        {
            get
            {
                return colUserControl.DisabledColor;
            }
            set
            {
                GetPropertyByName( "DisabledColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        public Color HoverColor
        {
            get
            {
                return colUserControl.HoverColor;
            }
            set
            {
                GetPropertyByName( "HoverColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the pressed.
        /// </summary>
        /// <value>The color of the pressed.</value>
        public Color PressedColor
        {
            get
            {
                return colUserControl.PressedColor;
            }
            set
            {
                GetPropertyByName( "PressedColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw border].
        /// </summary>
        /// <value><c>true</c> if [draw border]; otherwise, <c>false</c>.</value>
        public bool DrawBorder
        {
            get
            {
                return colUserControl.DrawBorder;
            }
            set
            {
                GetPropertyByName( "DrawBorder" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [invert fore color].
        /// </summary>
        /// <value><c>true</c> if [invert fore color]; otherwise, <c>false</c>.</value>
        public bool InvertForeColor
        {
            get
            {
                return colUserControl.InvertForeColor;
            }
            set
            {
                GetPropertyByName( "InvertForeColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is round.
        /// </summary>
        /// <value><c>true</c> if this instance is round; otherwise, <c>false</c>.</value>
        public bool IsRound
        {
            get
            {
                return colUserControl.IsRound;
            }
            set
            {
                GetPropertyByName( "IsRound" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the rounding arc.
        /// </summary>
        /// <value>The rounding arc.</value>
        public int RoundingArc
        {
            get
            {
                return colUserControl.RoundingArc;
            }
            set
            {
                GetPropertyByName( "RoundingArc" ).SetValue( colUserControl, value );
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
                GetPropertyByName( "ForeColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text alignment.</value>
        public StringAlignment TextAlignment
        {
            get
            {
                return colUserControl.TextAlignment;
            }
            set
            {
                GetPropertyByName( "TextAlignment" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return colUserControl.Text;
            }
            set
            {
                GetPropertyByName( "Text" ).SetValue( colUserControl, value );
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

            items.Add( new DesignerActionPropertyItem( "AutoStyle", "Auto Style", "Appearance",
                "Automatically style the button." ) );

            items.Add( new DesignerActionPropertyItem( "DrawBorder", "Draw Border", "Appearance",
                "Draws a border around the button." ) );

            items.Add( new DesignerActionPropertyItem( "InvertForeColor", "Invert ForeColor",
                "Appearance", "Invert the fore color." ) );

            items.Add( new DesignerActionPropertyItem( "IsRound", "Is Round", "Appearance",
                "Set to round the button." ) );

            items.Add( new DesignerActionPropertyItem( "DefaultColor", "Background", "Appearance",
                "Sets the default color." ) );

            items.Add( new DesignerActionPropertyItem( "ForeColor", "Fore Color", "Appearance",
                "Selects the foreground color." ) );

            items.Add( new DesignerActionPropertyItem( "BorderColor", "Border Color", "Appearance",
                "Sets the border color." ) );

            items.Add( new DesignerActionPropertyItem( "DisabledColor", "Disabled Color",
                "Appearance", "Sets the button's disabled color." ) );

            items.Add( new DesignerActionPropertyItem( "HoverColor", "Hover Color", "Appearance",
                "Sets the button's hovered color." ) );

            items.Add( new DesignerActionPropertyItem( "PressedColor", "Pressed Color",
                "Appearance", "Sets the background color when pressed." ) );

            items.Add( new DesignerActionPropertyItem( "RoundingArc", "Rounding Arc", "Appearance",
                "Sets the buttons corner radius." ) );

            items.Add( new DesignerActionPropertyItem( "TextAlignment", "Text Alignment",
                "Appearance", "Sets the text alignment." ) );

            items.Add( new DesignerActionPropertyItem( "Text", "Text", "Appearance",
                "Sets the text." ) );

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