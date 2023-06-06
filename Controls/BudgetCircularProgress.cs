// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetCircularProgress.cs" company="Terry D. Eppler">
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
//   BudgetCircularProgress.cs
// </summary>
// ******************************************************************************************

namespace BudgetExecution
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// A class collection for rendering metro-style progress.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "Click" ) ]
    [ Description( "A control for rendering a circular progress." ) ]
    [ Designer( typeof( BudgetCircularProgressDesigner ) ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetCircularProgress ), "MetroCircularProgress.bmp" ) ]
    public class BudgetCircularProgress : Control
    {
        #region Private Fields

        /// <summary>
        /// The inner circle size
        /// </summary>
        private int _InnerCircleSize;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The value
        /// </summary>
        private int _Value;

        /// <summary>
        /// The thickness
        /// </summary>
        private int _Thickness;

        /// <summary>
        /// The draw inner circle
        /// </summary>
        private bool _DrawInnerCircle;

        /// <summary>
        /// The draw outlines
        /// </summary>
        private bool _DrawOutlines;

        /// <summary>
        /// The draw percentage
        /// </summary>
        private bool _DrawPercentage;

        /// <summary>
        /// The draw percentage symbol
        /// </summary>
        private bool _DrawPercentageSymbol;

        /// <summary>
        /// The percentage symbol
        /// </summary>
        private string _PercentageSymbol;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the color scheme.
        /// </summary>
        /// <value>The color scheme.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ Description( "sets the color scheme." ) ]
        [ ReadOnly( false ) ]
        [ TypeConverter( typeof( ExpandableObjectConverter ) ) ]
        public MainColorScheme ColorScheme
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw the inner circle.
        /// </summary>
        /// <value><c>true</c> if draw inner circle; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw the inner circle." ) ]
        public bool DrawInnerCircle
        {
            get
            {
                return _DrawInnerCircle;
            }
            set
            {
                if( value != _DrawInnerCircle )
                {
                    _DrawInnerCircle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw outlines.
        /// </summary>
        /// <value><c>true</c> if draw outlines; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to draw outlines." ) ]
        public bool DrawOutlines
        {
            get
            {
                return _DrawOutlines;
            }
            set
            {
                if( value != _DrawOutlines )
                {
                    _DrawOutlines = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw percentage.
        /// </summary>
        /// <value><c>true</c> if [draw percentage]; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw percentage." ) ]
        public bool DrawPercentage
        {
            get
            {
                return _DrawPercentage;
            }
            set
            {
                if( value != _DrawPercentage )
                {
                    _DrawPercentage = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw percentage symbol.
        /// </summary>
        /// <value><c>true</c> if draw percentage symbol; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw percentage symbol." ) ]
        public bool DrawPercentageSymbol
        {
            get
            {
                return _DrawPercentageSymbol;
            }
            set
            {
                if( value != _DrawPercentageSymbol )
                {
                    _DrawPercentageSymbol = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the inner circle.
        /// </summary>
        /// <value>The size of the inner circle.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 80 ) ]
        [ Description( "Sets the size of the inner circle." ) ]
        public int InnerCircleSize
        {
            get
            {
                return _InnerCircleSize;
            }
            set
            {
                if( value != _InnerCircleSize )
                {
                    _InnerCircleSize = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the percentage symbol.
        /// </summary>
        /// <value>The percentage symbol.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( "%" ) ]
        [ Description( "Sets the percentage symbol." ) ]
        public string PercentageSymbol
        {
            get
            {
                return _PercentageSymbol;
            }
            set
            {
                if( Operators.CompareString( value, _PercentageSymbol, false ) != 0 )
                {
                    _PercentageSymbol = value;
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
                            ColorScheme._OutlineColor = Color.FromArgb( 35, 0, 0, 0 );
                            ColorScheme._InnerCircleColor = Color.FromArgb( 200, 200, 200 );
                            ColorScheme._ProgressColor = Color.FromArgb( 54, 116, 178 );
                            ColorScheme._CircleColor = ColorScheme.SetShade( Color.White, -5 );
                            ForeColor = Color.Black;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            ColorScheme._OutlineColor = Color.FromArgb( 35, 0, 0, 0 );

                            ColorScheme._InnerCircleColor =
                                ColorScheme.SetShade( Color.FromArgb( 98, 98, 98 ), -50 );

                            ColorScheme._ProgressColor = Color.FromArgb( 54, 116, 178 );

                            ColorScheme._CircleColor =
                                ColorScheme.SetShade( Color.FromArgb( 40, 40, 40 ), 5 );

                            ForeColor = Color.FromArgb( 98, 98, 98 );
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        /// <value>The thickness.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 10 ) ]
        [ Description( "Sets the thickness." ) ]
        public int Thickness
        {
            get
            {
                return _Thickness;
            }
            set
            {
                if( value != _Thickness )
                {
                    if( value > 4 )
                    {
                        _Thickness = value;
                        Invalidate( );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [ Browsable( true ) ]
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
                    _Value = value;
                    OnValueChanged( EventArgs.Empty );
                    Invalidate( );
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetCircularProgress" /> class.
        /// </summary>
        public BudgetCircularProgress( )
        {
            _InnerCircleSize = 80;
            _Style = Design.Style.Light;
            _Value = 50;
            _Thickness = 10;
            _DrawInnerCircle = true;
            _DrawOutlines = false;
            _DrawPercentage = true;
            _DrawPercentageSymbol = true;
            _PercentageSymbol = "%";
            ColorScheme = new MainColorScheme( );
            Size = new Size( 128, 128 );
            ForeColor = Color.White;
            Font = new Font( "Segoe UI Light", 15f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
        }

        /// <summary>
        /// Draws the progress.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="percentage">The percentage.</param>
        private void DrawProgress( Graphics g, Rectangle rect, float percentage )
        {
            var single = (float)( 3.6 * percentage );
            var single1 = 360f - single;

            using( var pen = new Pen( ColorScheme._ProgressColor, _Thickness ) )
            {
                using( var pen1 = new Pen( ColorScheme._CircleColor, checked( _Thickness - 4 ) ) )
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    if( _DrawOutlines )
                    {
                        g.DrawArc( new Pen( ColorScheme._OutlineColor, _Thickness ), rect,
                            single - 90f, single1 );
                    }

                    g.DrawArc( pen, rect, -90f, single );
                    g.DrawArc( pen1, rect, single - 90f, single1 );
                }
            }

            if( _DrawPercentage )
            {
                using( var font = new Font( Font.FontFamily, Font.Size ) )
                {
                    var str = percentage.ToString( );

                    if( _DrawPercentageSymbol )
                    {
                        str = string.Concat( str, _PercentageSymbol );
                    }

                    var sizeF = g.MeasureString( str, font );

                    var point = new Point(
                        checked( (int)Math.Round( rect.Left
                            + (double)rect.Width / 2
                            - sizeF.Width / 2f ) ),
                        checked( (int)Math.Round( rect.Top
                            + (double)rect.Height / 2
                            - sizeF.Height / 2f ) ) );

                    g.DrawString( str, font, new SolidBrush( ForeColor ), point );
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear( Parent.BackColor );

            var rectangle = new Rectangle(
                checked( (int)Math.Round( (double)Width / 2 - (double)_InnerCircleSize / 2 - 1 ) ),
                checked( (int)Math.Round( (double)Height / 2 - (double)_InnerCircleSize / 2 - 1 ) ),
                _InnerCircleSize, _InnerCircleSize );

            if( _DrawInnerCircle )
            {
                graphics.FillEllipse( new SolidBrush( ColorScheme._InnerCircleColor ), rectangle );
            }

            var rectangle1 = new Rectangle( checked( (int)Math.Round( (double)_Thickness / 2 ) ),
                checked( (int)Math.Round( (double)_Thickness / 2 ) ),
                checked( Width - checked( _Thickness + 1 ) ),
                checked( Height - checked( _Thickness + 1 ) ) );

            DrawProgress( graphics, rectangle1, _Value );
        }

        /// <summary>
        /// Handles the <see cref="E:ValueChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnValueChanged( EventArgs e )
        {
            var eventHandler = ValueChanged;

            if( eventHandler != null )
            {
                eventHandler( this, e );
            }
        }

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        [ Category( "Data" ) ]
        [ Description( "Wird ausgelöst sobald der Fortschritt verändert wurde." ) ]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Class MainColorScheme.
        /// </summary>
        public class MainColorScheme
        {
            /// <summary>
            /// The outline color
            /// </summary>
            public Color _OutlineColor;

            /// <summary>
            /// The inner circle color
            /// </summary>
            public Color _InnerCircleColor;

            /// <summary>
            /// The progress color
            /// </summary>
            public Color _ProgressColor;

            /// <summary>
            /// The circle color
            /// </summary>
            public Color _CircleColor;

            /// <summary>
            /// Gets or sets the color of the circle.
            /// </summary>
            /// <value>The color of the circle.</value>
            [ Browsable( true ) ]
            [ Category( "Appearance" ) ]
            [ Description( "Die Farbe des Kreises" ) ]
            public Color CircleColor
            {
                get
                {
                    return _CircleColor;
                }
                set
                {
                    if( value != _CircleColor )
                    {
                        _CircleColor = value;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the color of the inner circle.
            /// </summary>
            /// <value>The color of the inner circle.</value>
            [ Browsable( true ) ]
            [ Category( "Appearance" ) ]
            [ Description( "Die Farbe des inneren Kreises an." ) ]
            public Color InnerCircleColor
            {
                get
                {
                    return _InnerCircleColor;
                }
                set
                {
                    if( value != _InnerCircleColor )
                    {
                        _InnerCircleColor = value;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the color of the outline.
            /// </summary>
            /// <value>The color of the outline.</value>
            [ Browsable( true ) ]
            [ Category( "Appearance" ) ]
            [ Description( "Die Farbe der inneren sowie äußeren Umrandung des Kreises." ) ]
            public Color OutlineColor
            {
                get
                {
                    return _OutlineColor;
                }
                set
                {
                    if( value != _OutlineColor )
                    {
                        _OutlineColor = value;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the color of the progress.
            /// </summary>
            /// <value>The color of the progress.</value>
            [ Browsable( true ) ]
            [ Category( "Appearance" ) ]
            [ Description( "Die Farbe des Fortschritts." ) ]
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
                        _InnerCircleColor = Color.FromArgb( 100, SetShade( _ProgressColor, 50 ) );
                    }
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MainColorScheme"/> class.
            /// </summary>
            public MainColorScheme( )
            {
                _OutlineColor = Color.FromArgb( 35, 0, 0, 0 );
                _InnerCircleColor = Color.FromArgb( 200, 200, 200 );
                _ProgressColor = Color.FromArgb( 54, 116, 178 );
                _CircleColor = SetShade( Color.White, -5 );
            }

            /// <summary>
            /// Sets the shade.
            /// </summary>
            /// <param name="InputColor">Color of the input.</param>
            /// <param name="Offset">The offset.</param>
            /// <returns>Color.</returns>
            public Color SetShade( Color InputColor, int Offset )
            {
                var r = checked( InputColor.R + Offset );
                var g = checked( InputColor.G + Offset );
                var b = checked( InputColor.B + Offset );

                if( r < 0 )
                {
                    r = checked( r * -1 );
                }

                if( g < 0 )
                {
                    g = checked( g * -1 );
                }

                if( b < 0 )
                {
                    b = checked( b * -1 );
                }

                var color = Color.FromArgb( Math.Min( 255, r ), Math.Min( 255, g ),
                    Math.Min( 255, b ) );

                return color;
            }
        }
    }

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(BudgetCircularProgressDesigner))] --------------------//

    #endregion

    #region SmartTagActionList

    /// <summary>
    /// Class MetroCircularProgressSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroCircularProgressSmartTagActionList : DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private BudgetCircularProgress colUserControl;

        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroCircularProgressSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroCircularProgressSmartTagActionList( IComponent component )
            : base( component )
        {
            colUserControl = component as BudgetCircularProgress;

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
        /// Gets or sets a value indicating whether [draw inner circle].
        /// </summary>
        /// <value><c>true</c> if [draw inner circle]; otherwise, <c>false</c>.</value>
        public bool DrawInnerCircle
        {
            get
            {
                return colUserControl.DrawInnerCircle;
            }
            set
            {
                GetPropertyByName( "DrawInnerCircle" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw outlines].
        /// </summary>
        /// <value><c>true</c> if [draw outlines]; otherwise, <c>false</c>.</value>
        public bool DrawOutlines
        {
            get
            {
                return colUserControl.DrawOutlines;
            }
            set
            {
                GetPropertyByName( "DrawOutlines" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw percentage].
        /// </summary>
        /// <value><c>true</c> if [draw percentage]; otherwise, <c>false</c>.</value>
        public bool DrawPercentage
        {
            get
            {
                return colUserControl.DrawPercentage;
            }
            set
            {
                GetPropertyByName( "DrawPercentage" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw percentage symbol].
        /// </summary>
        /// <value><c>true</c> if [draw percentage symbol]; otherwise, <c>false</c>.</value>
        public bool DrawPercentageSymbol
        {
            get
            {
                return colUserControl.DrawPercentageSymbol;
            }
            set
            {
                GetPropertyByName( "DrawPercentageSymbol" ).SetValue( colUserControl, value );
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
        /// Gets or sets the size of the inner circle.
        /// </summary>
        /// <value>The size of the inner circle.</value>
        public int InnerCircleSize
        {
            get
            {
                return colUserControl.InnerCircleSize;
            }
            set
            {
                GetPropertyByName( "InnerCircleSize" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the percentage symbol.
        /// </summary>
        /// <value>The percentage symbol.</value>
        public string PercentageSymbol
        {
            get
            {
                return colUserControl.PercentageSymbol;
            }
            set
            {
                GetPropertyByName( "PercentageSymbol" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        /// <value>The thickness.</value>
        public int Thickness
        {
            get
            {
                return colUserControl.Thickness;
            }
            set
            {
                GetPropertyByName( "Thickness" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get
            {
                return colUserControl.Value;
            }
            set
            {
                GetPropertyByName( "Value" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the circle.
        /// </summary>
        /// <value>The color of the circle.</value>
        public Color CircleColor
        {
            get
            {
                return colUserControl.ColorScheme.CircleColor;
            }
            set
            {
                colUserControl.ColorScheme.CircleColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the inner circle.
        /// </summary>
        /// <value>The color of the inner circle.</value>
        public Color InnerCircleColor
        {
            get
            {
                return colUserControl.ColorScheme.InnerCircleColor;
            }
            set
            {
                colUserControl.ColorScheme.InnerCircleColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the outline.
        /// </summary>
        /// <value>The color of the outline.</value>
        public Color OutlineColor
        {
            get
            {
                return colUserControl.ColorScheme.OutlineColor;
            }
            set
            {
                colUserControl.ColorScheme.OutlineColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the progress.
        /// </summary>
        /// <value>The color of the progress.</value>
        public Color ProgressColor
        {
            get
            {
                return colUserControl.ColorScheme.ProgressColor;
            }
            set
            {
                colUserControl.ColorScheme.ProgressColor = value;
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
            items.Add( new DesignerActionHeaderItem( "Behaviour" ) );

            items.Add( new DesignerActionPropertyItem( "DrawInnerCircle", "Show Inner Circle",
                "Behaviour", "Set to show the inner circle." ) );

            items.Add( new DesignerActionPropertyItem( "DrawOutlines", "Show Outlines", "Behaviour",
                "Set to show the outlines." ) );

            items.Add( new DesignerActionPropertyItem( "DrawPercentage", "Show Percentage",
                "Behaviour", "Set to show the percentage." ) );

            items.Add( new DesignerActionPropertyItem( "DrawPercentageSymbol",
                "Show Percentage Symbol", "Behaviour", "Set to show the percentage symbol." ) );

            items.Add( new DesignerActionHeaderItem( "Appearance" ) );

            items.Add( new DesignerActionPropertyItem( "CircleColor", "Circle Color", "Appearance",
                "Sets the circle color." ) );

            items.Add( new DesignerActionPropertyItem( "InnerCircleColor", "Inner Circle Color",
                "Appearance", "Sets the inner circle color." ) );

            items.Add( new DesignerActionPropertyItem( "OutlineColor", "Outline Color",
                "Appearance", "Sets the outline color." ) );

            items.Add( new DesignerActionPropertyItem( "ProgressColor", "Progress Color",
                "Appearance", "Sets the progress color." ) );

            items.Add( new DesignerActionPropertyItem( "Style", "Style", "Appearance",
                "Sets the style." ) );

            items.Add( new DesignerActionPropertyItem( "InnerCircleSize", "Inner Circle Size",
                "Appearance", "Sets the inner circle size." ) );

            items.Add( new DesignerActionPropertyItem( "PercentageSymbol", "Percentage Symbol",
                "Appearance", "Sets the percentage symbol." ) );

            items.Add( new DesignerActionPropertyItem( "Thickness", "Thickness", "Appearance",
                "Sets the thickness." ) );

            items.Add( new DesignerActionPropertyItem( "Value", "Value", "Appearance",
                "Sets the progress value." ) );

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