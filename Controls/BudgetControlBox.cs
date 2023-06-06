// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetControlBox.cs" company="Terry D. Eppler">
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
//   BudgetControlBox.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetControlBox.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "AreaClicked" ) ]
    [ Description(
        "Ein ControlBox-Steuerelement im metro Stil, welches eigene Schaltflächen unterstützt." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetControlBox ), "BudgetControlBox.bmp" ) ]
    public class BudgetControlBox : Control
    {
        #region Private Fields

        /// <summary>
        /// The areas
        /// </summary>
        private List<Rectangle> _Areas = new( );

        /// <summary>
        /// The load default areas
        /// </summary>
        private bool _LoadDefaultAreas;

        /// <summary>
        /// The is ready
        /// </summary>
        private bool _IsReady;

        /// <summary>
        /// The draw design mode control
        /// </summary>
        private bool _DrawDesignModeControl;

        /// <summary>
        /// The area collection
        /// </summary>
        private BudgetControlBoxAreaCollection _AreaCollection = new( );

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        /// <summary>
        /// The mouse state
        /// </summary>
        private Helpers.MouseState _MouseState;

        /// <summary>
        /// The mouse location
        /// </summary>
        private Point _MouseLocation;

        /// <summary>
        /// The hot area
        /// </summary>
        private Rectangle _HotArea;

        /// <summary>
        /// The hot index
        /// </summary>
        private int _HotIndex;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the areas.
        /// </summary>
        /// <value>The areas.</value>
        [ Browsable( true ) ]
        [ Category( "Data" ) ]
        [ Description( "Sets the areas." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Content ) ]
        public BudgetControlBoxAreaCollection Areas
        {
            get
            {
                return _AreaCollection;
            }
            set
            {
                _AreaCollection = value;
                Invalidate( );
            }
        }

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
                    CreateFormHandlers( value );
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
        /// Gets or sets a value indicating whether to enable design mode control.
        /// </summary>
        /// <value><c>true</c> if design mode control; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to enable design mode control." ) ]
        public bool DesignModeControl
        {
            get
            {
                return _DrawDesignModeControl;
            }
            set
            {
                if( _DrawDesignModeControl != value )
                {
                    _DrawDesignModeControl = value;
                    Invalidate( );
                }
            }
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
        /// Gets a value indicating whether this control is ready.
        /// </summary>
        /// <value><c>true</c> if this instance is ready; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ Description( "Gets a value indicating whether this control is ready." ) ]
        private bool IsReady
        {
            get
            {
                return _IsReady;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to load default areas.
        /// </summary>
        /// <value><c>true</c> if load default areas; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to load default areas." ) ]
        public bool LoadDefaultAreas
        {
            get
            {
                return _LoadDefaultAreas;
            }
            set
            {
                _LoadDefaultAreas = value;
                _IsReady = false;

                if( !_LoadDefaultAreas )
                {
                    _AreaCollection.Clear( );
                }

                Invalidate( );
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

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetControlBox" /> class.
        /// </summary>
        public BudgetControlBox( )
        {
            _LoadDefaultAreas = true;
            _IsReady = false;
            _DrawDesignModeControl = true;

            //this._AreaCollection = new BudgetControlBoxAreaCollection();
            _AutoStyle = true;
            _MouseState = Helpers.MouseState.None;
            _HotArea = new Rectangle( );
            _HotIndex = 0;
            Size = new Size( 128, 32 );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            var metroControlBox = this;
            _AreaCollection.ItemAdded += metroControlBox.Area_Added;
            var metroControlBox1 = this;
            _AreaCollection.ItemRemoving += metroControlBox1.Area_Removed;
            CreateFormHandlers( true );
        }

        /// <summary>
        /// Handles the Added event of the Area control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetControlBoxAreaCollectionEventArgs"/> instance containing the event data.</param>
        private void Area_Added( object sender, BudgetControlBoxAreaCollectionEventArgs e )
        {
            if( e.Item != null )
            {
                var metroControlBox = this;
                e.Item.PropertyChanged += metroControlBox.Item_PropertyChanged;
            }

            var areaAddedEventHandler = AreaAdded;

            if( areaAddedEventHandler != null )
            {
                areaAddedEventHandler( this,
                    new BudgetControlBoxAreaCollectionEventArgs( e.Item ) );
            }

            RefreshAreas( );
        }

        /// <summary>
        /// Handles the Removed event of the Area control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetControlBoxAreaCollectionEventArgs"/> instance containing the event data.</param>
        private void Area_Removed( object sender, BudgetControlBoxAreaCollectionEventArgs e )
        {
            if( e.Item != null )
            {
                var metroControlBox = this;
                e.Item.PropertyChanged -= metroControlBox.Item_PropertyChanged;
                RefreshAreas( );
            }
        }

        /// <summary>
        /// Changes the color brightness.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="correctionFactor">The correction factor.</param>
        /// <returns>Color.</returns>
        private Color ChangeColorBrightness( Color color, float correctionFactor )
        {
            float r = color.R;
            float g = color.G;
            float b = color.B;

            if( correctionFactor >= 0f )
            {
                r = ( 255f - r ) * correctionFactor + r;
                g = ( 255f - g ) * correctionFactor + g;
                b = ( 255f - b ) * correctionFactor + b;
            }
            else
            {
                correctionFactor = 1f + correctionFactor;
                r *= correctionFactor;
                g *= correctionFactor;
                b *= correctionFactor;
            }

            var color1 = Color.FromArgb( color.A, checked( (int)Math.Round( r ) ),
                checked( (int)Math.Round( g ) ), checked( (int)Math.Round( b ) ) );

            return color1;
        }

        /// <summary>
        /// Creates the default areas.
        /// </summary>
        private void CreateDefaultAreas( )
        {
            _IsReady = true;
            var metroControlBoxArea = new BudgetControlBoxArea[ 3 ];
            var style = Design.Style.Light;

            if( Parent is BudgetForm )
            {
                if( ( (BudgetForm)Parent ).Style == Design.Style.Dark )
                {
                    style = Design.Style.Dark;
                }
            }
            else if( Parent.BackColor == Color.FromArgb( 40, 40, 40 ) )
            {
                style = Design.Style.Dark;
            }

            metroControlBoxArea[ 0 ] = new BudgetControlBoxArea(
                BudgetControlBoxArea.ControlBoxAreaType.Minimize, style, "defminimize", false );

            metroControlBoxArea[ 1 ] = new BudgetControlBoxArea(
                BudgetControlBoxArea.ControlBoxAreaType.Maximize, style, "defmaximize", false );

            metroControlBoxArea[ 2 ] = new BudgetControlBoxArea(
                BudgetControlBoxArea.ControlBoxAreaType.Close, style, "defclose", false );

            Areas.AddItems( metroControlBoxArea );
            Invalidate( );
            Size = new Size( 96, 32 );
        }

        /// <summary>
        /// Creates the form handlers.
        /// </summary>
        /// <param name="action">if set to <c>true</c> [action].</param>
        private void CreateFormHandlers( bool action )
        {
            try
            {
                if( FindForm( ) is BudgetForm )
                {
                    var _budgetForm = (BudgetForm)FindForm( );

                    if( !action )
                    {
                        var metroControlBox = this;
                        _budgetForm.FormStyleChanged -= metroControlBox.FormStyle_Changed;
                    }
                    else
                    {
                        var metroControlBox1 = this;
                        _budgetForm.FormStyleChanged += metroControlBox1.FormStyle_Changed;
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
        /// Creates a handle for the control.
        /// </summary>
        protected override void CreateHandle( )
        {
            CreateFormHandlers( true );
            base.CreateHandle( );
        }

        /// <summary>
        /// Draws the design mode control.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawDesignModeControl( Graphics g )
        {
            if( DesignMode )
            {
                using( var stringFormat = new StringFormat( ) )
                {
                    var color = ChangeColorBrightness( Parent.BackColor, -0.1f );
                    g.Clear( color );
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    using( var solidBrush = new SolidBrush( color ) )
                    {
                        if( color.GetBrightness( ) >= 0.3 )
                        {
                            solidBrush.Color = Color.Black;
                        }
                        else
                        {
                            solidBrush.Color = Color.White;
                        }

                        g.DrawString( "Gathers BudgetControlBox\r\n(Will disappear once loaded)",
                            new Font( Parent.Font.FontFamily, 7f ), solidBrush, ClientRectangle,
                            stringFormat );
                    }
                }
            }
        }

        /// <summary>
        /// Draws the icon.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="c">The c.</param>
        /// <param name="typ">The typ.</param>
        /// <param name="r">The r.</param>
        /// <param name="bc">The bc.</param>
        private void DrawIcon(
            Graphics g, Color c, BudgetControlBoxArea.ControlBoxAreaType typ, Rectangle r,
            Color bc )
        {
            Rectangle rectangle;
            Point point;
            Point point1;

            switch( typ )
            {
                case BudgetControlBoxArea.ControlBoxAreaType.Minimize:
                {
                    r = new Rectangle(
                        checked( checked( r.X + checked( (int)Math.Round( (double)r.Width / 2 ) ) )
                            - 4 ),
                        checked( checked( r.Y + checked( (int)Math.Round( (double)r.Height / 2 ) ) )
                            - 4 ), 8, 8 );

                    using( var pen = new Pen( c, 3f ) )
                    {
                        point1 = new Point( r.X, checked( checked( r.Y + r.Height ) - 2 ) );

                        point = new Point( checked( r.X + r.Width ),
                            checked( checked( r.Y + r.Height ) - 2 ) );

                        g.DrawLine( pen, point1, point );
                    }

                    break;
                }
                case BudgetControlBoxArea.ControlBoxAreaType.Maximize:
                {
                    r = new Rectangle(
                        checked( checked( r.X + checked( (int)Math.Round( (double)r.Width / 2 ) ) )
                            - 4 ),
                        checked( checked( r.Y + checked( (int)Math.Round( (double)r.Height / 2 ) ) )
                            - 4 ), 8, 8 );

                    using( var pen1 = new Pen( c, 1f ) )
                    {
                        if( FindForm( ).WindowState == FormWindowState.Normal )
                        {
                            rectangle = new Rectangle( checked( r.X + 3 ), r.Y, 7, 7 );
                            g.DrawRectangle( pen1, rectangle );

                            rectangle = new Rectangle( checked( r.X + 3 ), checked( r.Y + 1 ), 7,
                                6 );

                            g.DrawRectangle( pen1, rectangle );
                            rectangle = new Rectangle( r.X, checked( r.Y + 4 ), 7, 6 );
                            g.DrawRectangle( pen1, rectangle );
                            rectangle = new Rectangle( r.X, checked( r.Y + 3 ), 7, 7 );
                            g.DrawRectangle( pen1, rectangle );

                            using( var solidBrush = new SolidBrush( bc ) )
                            {
                                rectangle = new Rectangle( checked( r.X + 1 ), checked( r.Y + 5 ),
                                    6, 4 );

                                g.FillRectangle( solidBrush, rectangle );
                            }
                        }
                        else if( FindForm( ).WindowState == FormWindowState.Maximized )
                        {
                            rectangle = new Rectangle( checked( r.X + 1 ), checked( r.Y + 1 ),
                                checked( r.Width - 1 ), checked( r.Height - 1 ) );

                            g.DrawRectangle( pen1, rectangle );

                            rectangle = new Rectangle( checked( r.X + 1 ), checked( r.Y + 2 ),
                                checked( r.Width - 1 ), checked( r.Height - 2 ) );

                            g.DrawRectangle( pen1, rectangle );
                        }
                    }

                    break;
                }
                case BudgetControlBoxArea.ControlBoxAreaType.Close:
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    r = new Rectangle(
                        checked( checked( r.X + checked( (int)Math.Round( (double)r.Width / 2 ) ) )
                            - 4 ),
                        checked( checked( r.Y + checked( (int)Math.Round( (double)r.Height / 2 ) ) )
                            - 4 ), 8, 8 );

                    using( var pen2 = new Pen( c, 2f ) )
                    {
                        point = new Point( r.X, r.Y );
                        point1 = new Point( checked( r.X + r.Width ), checked( r.Y + r.Height ) );
                        g.DrawLine( pen2, point, point1 );
                        point1 = new Point( r.X, checked( r.Y + r.Height ) );
                        point = new Point( checked( r.X + r.Width ), r.Y );
                        g.DrawLine( pen2, point1, point );
                    }

                    break;
                }
            }

            g.SmoothingMode = SmoothingMode.HighSpeed;
        }

        /// <summary>
        /// Handles the Changed event of the FormStyle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetForm.BudgetFormEventArgs"/> instance containing the event data.</param>
        private void FormStyle_Changed( object sender, BudgetForm.BudgetFormEventArgs e )
        {
            IEnumerator<BudgetControlBoxArea> enumerator = null;

            if( _AutoStyle )
            {
                using( enumerator )
                {
                    enumerator = _AreaCollection.GetEnumerator( );

                    while( enumerator.MoveNext( ) )
                    {
                        enumerator.Current.Style = e.FormStyle;
                    }
                }

                Invalidate( );
            }
        }

        /// <summary>
        /// Gets the center point.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="rect">The rect.</param>
        /// <returns>Point.</returns>
        private Point GetCenterPoint( Image img, Rectangle rect )
        {
            var point = new Point(
                checked( checked( checked( rect.X
                            + checked( (int)Math.Round( (double)rect.Width / 2 ) ) )
                        - checked( (int)Math.Round( (double)img.Width / 2 ) ) )
                    + 1 ),
                checked( checked( checked( rect.Y
                            + checked( (int)Math.Round( (double)rect.Height / 2 ) ) )
                        - checked( (int)Math.Round( (double)img.Width / 2 ) ) )
                    + 1 ) );

            return point;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Item_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            RefreshAreas( );
            Invalidate( );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseClick( MouseEventArgs e )
        {
            if( _AreaCollection.Count > 0 )
            {
                if( _AreaCollection[ _HotIndex ].Enabled )
                {
                    var eventHandler = AreaClicked;

                    if( eventHandler != null )
                    {
                        eventHandler( this,
                            new BudgetControlBoxEventArgs( _AreaCollection[ _HotIndex ],
                                _HotIndex ) );
                    }

                    if( _LoadDefaultAreas )
                    {
                        var name = _AreaCollection[ _HotIndex ].Name;

                        if( Operators.CompareString( name, "defminimize", false ) == 0 )
                        {
                            FindForm( ).WindowState = FormWindowState.Minimized;
                        }
                        else if( Operators.CompareString( name, "defmaximize", false ) == 0 )
                        {
                            if( FindForm( ).WindowState != FormWindowState.Maximized )
                            {
                                FindForm( ).WindowState = FormWindowState.Maximized;
                            }
                            else
                            {
                                FindForm( ).WindowState = FormWindowState.Normal;
                            }
                        }
                        else if( Operators.CompareString( name, "defclose", false ) == 0 )
                        {
                            if( FindForm( ) != null )
                            {
                                FindForm( ).Close( );
                            }
                        }
                    }
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
            _HotArea = new Rectangle( );
            Invalidate( );
            base.OnMouseLeave( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove( MouseEventArgs e )
        {
            _MouseLocation = e.Location;

            if( _Areas.Count > 0 )
            {
                _HotArea = new Rectangle( );
                _HotIndex = 0;
                var count = checked( _Areas.Count - 1 );

                for( var i = 0; i <= count; i = checked( i + 1 ) )
                {
                    if( _Areas[ i ].Contains( _MouseLocation ) )
                    {
                        _HotArea = _Areas[ i ];
                        _HotIndex = i;
                        Invalidate( );
                    }
                }
            }

            base.OnMouseMove( e );
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
            Rectangle item;
            Rectangle rectangle;
            Rectangle item1;
            Point point;
            Rectangle rectangle1;
            Rectangle item2;
            Rectangle rectangle2;
            Rectangle item3;
            Point point1;
            var rectangle3 = new Rectangle( );
            var graphics = e.Graphics;

            if( _DrawDesignModeControl )
            {
                DrawDesignModeControl( graphics );
            }

            if( _LoadDefaultAreas && !_IsReady )
            {
                CreateDefaultAreas( );
            }

            if( _AreaCollection.Count > 0 )
            {
                var count = checked( _Areas.Count - 1 );

                for( var i = 0; i <= count; i = checked( i + 1 ) )
                {
                    var defaultColor = _AreaCollection[ i ].DefaultColor;
                    graphics.FillRectangle( new SolidBrush( defaultColor ), _Areas[ i ] );

                    if( _Areas[ i ] != _HotArea )
                    {
                        switch( _AreaCollection[ i ].AreaType )
                        {
                            case BudgetControlBoxArea.ControlBoxAreaType.Custom:
                            {
                                if( _AreaCollection[ i ].AreaImage != null )
                                {
                                    graphics.DrawImage( _AreaCollection[ i ].AreaImage,
                                        GetCenterPoint( _AreaCollection[ i ].AreaImage,
                                            _Areas[ i ] ) );
                                }

                                break;
                            }
                            case BudgetControlBoxArea.ControlBoxAreaType.Minimize:
                            {
                                DrawIcon( graphics, _AreaCollection[ i ].IconColor,
                                    BudgetControlBoxArea.ControlBoxAreaType.Minimize, _Areas[ i ],
                                    defaultColor );

                                break;
                            }
                            case BudgetControlBoxArea.ControlBoxAreaType.Maximize:
                            {
                                DrawIcon( graphics, _AreaCollection[ i ].IconColor,
                                    BudgetControlBoxArea.ControlBoxAreaType.Maximize, _Areas[ i ],
                                    defaultColor );

                                break;
                            }
                            case BudgetControlBoxArea.ControlBoxAreaType.Close:
                            {
                                DrawIcon( graphics, _AreaCollection[ i ].IconColor,
                                    BudgetControlBoxArea.ControlBoxAreaType.Close, _Areas[ i ],
                                    defaultColor );

                                break;
                            }
                        }

                        using( var pen = new Pen( _AreaCollection[ i ].HighlightColor ) )
                        {
                            if( _AreaCollection[ i ].IsHighlighted )
                            {
                                item = _Areas[ i ];
                                var x = checked( item.X + 1 );
                                rectangle = _Areas[ i ];
                                var y = rectangle.Y;
                                item1 = _Areas[ i ];
                                point = new Point( x, checked( checked( y + item1.Height ) - 1 ) );
                                rectangle1 = _Areas[ i ];
                                var num = rectangle1.X;
                                item2 = _Areas[ i ];
                                var width = checked( checked( num + item2.Width ) - 1 );
                                rectangle2 = _Areas[ i ];
                                var y1 = rectangle2.Y;
                                item3 = _Areas[ i ];

                                point1 = new Point( width,
                                    checked( checked( y1 + item3.Height ) - 1 ) );

                                graphics.DrawLine( pen, point, point1 );
                            }
                        }
                    }
                }
            }

            if( _HotArea != rectangle3 )
            {
                using( var red = new Pen( Brushes.Red ) )
                {
                    using( var solidBrush =
                          new SolidBrush( _AreaCollection[ _HotIndex ].HoverColor ) )
                    {
                        var flag = false;
                        var iconColor = _AreaCollection[ _HotIndex ].IconColor;

                        switch( _MouseState )
                        {
                            case Helpers.MouseState.Over:
                            {
                                red.Color = Color.Red;
                                solidBrush.Color = _AreaCollection[ _HotIndex ].HoverColor;
                                break;
                            }
                            case Helpers.MouseState.Pressed:
                            {
                                red.Color = Color.DarkRed;
                                solidBrush.Color = _AreaCollection[ _HotIndex ].PressedColor;

                                if( _AreaCollection[ _HotIndex ].Style == Design.Style.Light
                                   || _AreaCollection[ _HotIndex ].Style == Design.Style.Dark
                                       ? true
                                       : false )
                                {
                                    flag = true;
                                }

                                if( !_AreaCollection[ _HotIndex ].InvertIconColor )
                                {
                                    break;
                                }

                                iconColor =
                                    Design.BudgetColors.InvertColor( _AreaCollection[ _HotIndex ]
                                        .IconColor );

                                break;
                            }
                        }

                        if( _AreaCollection[ _HotIndex ].Enabled )
                        {
                            graphics.FillRectangle( solidBrush, _HotArea );
                        }

                        var color = flag
                            ? Color.White
                            : iconColor;

                        switch( _AreaCollection[ _HotIndex ].AreaType )
                        {
                            case BudgetControlBoxArea.ControlBoxAreaType.Custom:
                            {
                                if( _AreaCollection[ _HotIndex ].AreaImage != null )
                                {
                                    graphics.DrawImage( _AreaCollection[ _HotIndex ].AreaImage,
                                        GetCenterPoint( _AreaCollection[ _HotIndex ].AreaImage,
                                            _HotArea ) );
                                }

                                break;
                            }
                            case BudgetControlBoxArea.ControlBoxAreaType.Minimize:
                            {
                                DrawIcon( graphics, color,
                                    BudgetControlBoxArea.ControlBoxAreaType.Minimize, _HotArea,
                                    solidBrush.Color );

                                break;
                            }
                            case BudgetControlBoxArea.ControlBoxAreaType.Maximize:
                            {
                                DrawIcon( graphics, color,
                                    BudgetControlBoxArea.ControlBoxAreaType.Maximize, _HotArea,
                                    solidBrush.Color );

                                break;
                            }
                            case BudgetControlBoxArea.ControlBoxAreaType.Close:
                            {
                                DrawIcon( graphics, color,
                                    BudgetControlBoxArea.ControlBoxAreaType.Close, _HotArea,
                                    solidBrush.Color );

                                break;
                            }
                        }
                    }

                    red.Color = _AreaCollection[ _HotIndex ].HighlightColor;

                    if( _AreaCollection[ _HotIndex ].IsHighlighted )
                    {
                        item2 = _Areas[ _HotIndex ];
                        var x1 = item2.X;
                        item3 = _Areas[ _HotIndex ];
                        var num1 = item3.Y;
                        rectangle2 = _Areas[ _HotIndex ];

                        point1 = new Point( x1,
                            checked( checked( num1 + rectangle2.Height ) - 1 ) );

                        rectangle1 = _Areas[ _HotIndex ];
                        var x2 = rectangle1.X;
                        item1 = _Areas[ _HotIndex ];
                        var width1 = checked( checked( x2 + item1.Width ) - 1 );
                        rectangle = _Areas[ _HotIndex ];
                        var y2 = rectangle.Y;
                        item = _Areas[ _HotIndex ];
                        point = new Point( width1, checked( checked( y2 + item.Height ) - 1 ) );
                        graphics.DrawLine( red, point1, point );
                    }
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Refreshes the areas.
        /// </summary>
        public void RefreshAreas( )
        {
            Size areaSize;
            var width = 0;
            var height = 0;
            _Areas.Clear( );
            var count = checked( _AreaCollection.Count - 1 );

            for( var i = 0; i <= count; i = checked( i + 1 ) )
            {
                if( _AreaCollection[ i ].AreaSize.Height > height )
                {
                    areaSize = _AreaCollection[ i ].AreaSize;
                    height = areaSize.Height;
                }

                areaSize = _AreaCollection[ i ].AreaSize;
                var num = areaSize.Width;
                var size = _AreaCollection[ i ].AreaSize;
                var rectangle = new Rectangle( width, 0, num, size.Height );
                _Areas.Add( rectangle );
                areaSize = _AreaCollection[ i ].AreaSize;
                width = checked( width + areaSize.Width );
            }

            areaSize = new Size( width, height );
            Size = areaSize;
            Invalidate( );

            if( Parent is BudgetForm )
            {
                var parent = (BudgetForm)Parent;

                if( parent.MainControlBox == this )
                {
                    parent.RelocateMainControlBox( );
                }
            }
        }

        /// <summary>
        /// Occurs when [area added].
        /// </summary>
        public event AreaAddedEventHandler AreaAdded;

        /// <summary>
        /// Occurs when [area clicked].
        /// </summary>
        public event EventHandler<BudgetControlBoxEventArgs> AreaClicked;

        /// <summary>
        /// Delegate AreaAddedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BudgetControlBoxAreaCollectionEventArgs"/> instance containing the event data.</param>
        public delegate void AreaAddedEventHandler(
            object sender, BudgetControlBoxAreaCollectionEventArgs e );

        /// <summary>
        /// Class BudgetControlBoxEventArgs.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class BudgetControlBoxEventArgs : EventArgs
        {
            /// <summary>
            /// The item
            /// </summary>
            private BudgetControlBoxArea _item;

            /// <summary>
            /// The index
            /// </summary>
            private int _Index;

            /// <summary>
            /// Gets the index.
            /// </summary>
            /// <value>The index.</value>
            public int Index
            {
                get
                {
                    return _Index;
                }
            }

            /// <summary>
            /// Gets the item.
            /// </summary>
            /// <value>The item.</value>
            public BudgetControlBoxArea Item
            {
                get
                {
                    return _item;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="BudgetControlBoxEventArgs"/> class.
            /// </summary>
            /// <param name="item">The item.</param>
            /// <param name="index">The index.</param>
            public BudgetControlBoxEventArgs( BudgetControlBoxArea item, int index )
            {
                _item = item;
                _Index = index;
            }
        }
    }
}