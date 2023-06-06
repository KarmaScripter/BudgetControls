// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTrackbarRegion.cs" company="Terry D. Eppler">
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
//   BudgetTrackbarRegion.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetTrackbarRegion.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "Click" ) ]
    [ Designer( typeof( BudgetTrackbarRegionDesigner ) ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( TrackBar ) ) ]
    public class BudgetTrackbarRegion : Control
    {
        /// <summary>
        /// The enc list
        /// </summary>
        private static List<WeakReference> __ENCList;

        /// <summary>
        /// The maximum value
        /// </summary>
        public int maxVal;

        /// <summary>
        /// The minimum value
        /// </summary>
        public int minVal;

        /// <summary>
        /// The value
        /// </summary>
        public int val;

        /// <summary>
        /// The val2
        /// </summary>
        public int val2;

        /// <summary>
        /// The slider1 position
        /// </summary>
        private int slider1Pos;

        /// <summary>
        /// The slider2 position
        /// </summary>
        private int slider2Pos;

        /// <summary>
        /// The scrolling1
        /// </summary>
        private bool scrolling1;

        /// <summary>
        /// The scrolling2
        /// </summary>
        private bool scrolling2;

        /// <summary>
        /// The bar size
        /// </summary>
        private int BarSize;

        /// <summary>
        /// The slider width
        /// </summary>
        private int sliderWidth;

        /// <summary>
        /// The slider height
        /// </summary>
        private const int sliderHeight = 16;

        /// <summary>
        /// The use switch borders
        /// </summary>
        private bool _UseSwitchBorders;

        /// <summary>
        /// The use fixed size
        /// </summary>
        private bool _UseFixedSize;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// Gets or sets the color scheme.
        /// </summary>
        /// <value>The color scheme.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ Description( "Eine Ober-Eigenschaft, die alle Farbeigenschaften enthält." ) ]
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
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [ Browsable( true ) ]
        [ Category( "Behavior" ) ]
        [ DefaultValue( 100 ) ]
        [ Description( "Setzt den maximalen Wert der Trackbar." ) ]
        public int Maximum
        {
            get
            {
                return maxVal;
            }
            set
            {
                maxVal = value;

                if( maxVal < minVal )
                {
                    minVal = maxVal;
                }

                if( val > maxVal )
                {
                    val = maxVal;
                }

                SetPos( );
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        [ Browsable( true ) ]
        [ Category( "Behavior" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Setzt den minimalen Wert der Trackbar." ) ]
        public int Minimum
        {
            get
            {
                return minVal;
            }
            set
            {
                minVal = value;

                if( minVal > maxVal )
                {
                    maxVal = minVal;
                }

                if( val < minVal )
                {
                    val = Minimum;
                }

                SetPos( );
            }
        }

        /// <summary>
        /// Gets the selected value.
        /// </summary>
        /// <value>The selected value.</value>
        [ Browsable( true ) ]
        [ Category( "Behavior" ) ]
        [ Description( "Gibt den derzeitigen Wertbereich der Trackbar an." ) ]
        public string SelectedValue
        {
            get
            {
                var str = string.Concat( Conversions.ToString( val2 ), "-",
                    Conversions.ToString( val ) );

                return str;
            }
        }

        /// <summary>
        /// Gets or sets the slider value1.
        /// </summary>
        /// <value>The slider value1.</value>
        [ Browsable( true ) ]
        [ Category( "Behavior" ) ]
        [ DefaultValue( 50 ) ]
        [ Description( "Setzt den derzeitigen Wert des ersten Sliders der Trackbar." ) ]
        public int SliderValue1
        {
            get
            {
                return val;
            }
            set
            {
                val = value;

                if( val < minVal )
                {
                    val = minVal;
                }

                if( val > maxVal )
                {
                    val = maxVal;
                }

                SetPos( );
            }
        }

        /// <summary>
        /// Gets or sets the slider value2.
        /// </summary>
        /// <value>The slider value2.</value>
        [ Browsable( true ) ]
        [ Category( "Behavior" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Setzt den derzeitigen Wert des zweiten Sliders der Trackbar." ) ]
        public int SliderValue2
        {
            get
            {
                return val2;
            }
            set
            {
                val2 = value;

                if( val2 < minVal )
                {
                    val2 = minVal;
                }

                if( val2 > maxVal )
                {
                    val2 = maxVal;
                }

                SetPos( );
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Setzt den Style der Trackbar." ) ]
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
                    if( value == Design.Style.Light )
                    {
                        ColorScheme._RightColor = Color.FromArgb( 229, 229, 229 );
                        ColorScheme._LeftColor = Color.FromArgb( 229, 229, 229 );
                        ColorScheme._MiddleColor = Color.FromArgb( 0, 164, 240 );
                        ColorScheme._SwitchColor = Color.FromArgb( 101, 101, 101 );
                        UseSwitchBorders = true;
                    }
                    else if( value != Design.Style.Dark )
                    {
                        value = Design.Style.Custom;
                    }
                    else
                    {
                        ColorScheme._RightColor = Color.FromArgb( 98, 98, 98 );
                        ColorScheme._LeftColor = Color.FromArgb( 98, 98, 98 );
                        ColorScheme._MiddleColor = Color.FromArgb( 0, 164, 240 );
                        ColorScheme._SwitchColor = Color.FromArgb( 51, 51, 51 );
                        UseSwitchBorders = false;
                    }

                    _Style = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the switch.
        /// </summary>
        /// <value>The width of the switch.</value>
        [ Browsable( true ) ]
        [ Category( "Behavior" ) ]
        [ DefaultValue( 6 ) ]
        [ Description(
            "Setzt die Dicke des Schalters. (Kann nur bei \"UseFixedSize = False\" angewendet werden.)" ) ]
        public int SwitchWidth
        {
            get
            {
                return sliderWidth;
            }
            set
            {
                if( value != sliderWidth )
                {
                    if( !UseFixedSize )
                    {
                        sliderWidth = value;
                        Invalidate( );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use fixed size].
        /// </summary>
        /// <value><c>true</c> if [use fixed size]; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Gibt an, ob der Schalter und die Linie eine fixe Größe haben sollen." ) ]
        public bool UseFixedSize
        {
            get
            {
                return _UseFixedSize;
            }
            set
            {
                if( value != _UseFixedSize )
                {
                    if( value )
                    {
                        Size = new Size( 200, 20 );
                        sliderWidth = 6;
                        UseSwitchBorders = true;
                    }
                    else if( !value )
                    {
                        UseSwitchBorders = false;
                    }

                    _UseFixedSize = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use switch borders].
        /// </summary>
        /// <value><c>true</c> if [use switch borders]; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Gibt an, ob der Schalter umrandet werden soll." ) ]
        public bool UseSwitchBorders
        {
            get
            {
                return _UseSwitchBorders;
            }
            set
            {
                if( value != _UseSwitchBorders )
                {
                    if( UseFixedSize )
                    {
                        _UseSwitchBorders = value;
                        Invalidate( );
                    }
                }
            }
        }

        /// <summary>
        /// Initializes static members of the <see cref="BudgetTrackbarRegion"/> class.
        /// </summary>
        [ DebuggerNonUserCode ]
        static BudgetTrackbarRegion( )
        {
            BudgetTrackbarRegion.__ENCList = new List<WeakReference>( );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTrackbarRegion"/> class.
        /// </summary>
        public BudgetTrackbarRegion( )
        {
            var metroTrackbarRegion = this;
            MouseDown += metroTrackbarRegion.BudgetTrackBarRegion_MouseDown;
            var metroTrackbarRegion1 = this;
            MouseMove += metroTrackbarRegion1.BudgetTrackBarRegion_MouseMove;
            var metroTrackbarRegion2 = this;
            MouseUp += metroTrackbarRegion2.BudgetTrackBarRegion_MouseUp;
            var metroTrackbarRegion3 = this;
            Paint += metroTrackbarRegion3.BudgetTrackBarRegion_Paint;
            BudgetTrackbarRegion.__ENCAddToList( this );
            BarSize = 6;
            sliderWidth = 6;
            _UseSwitchBorders = true;
            _UseFixedSize = true;
            _Style = Design.Style.Light;
            ColorScheme = new MainColorScheme( );
            val = 50;
            minVal = 0;
            maxVal = 100;
            scrolling1 = false;
            scrolling2 = false;
            slider1Pos = 0;
            slider2Pos = 0;

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            Size = new Size( 200, 20 );
            _Style = Design.Style.Dark;
            Invalidate( );
            _Style = Design.Style.Light;
            SetPos( );
        }

        /// <summary>
        /// Encs the add to list.
        /// </summary>
        /// <param name="value">The value.</param>
        [ DebuggerNonUserCode ]
        private static void __ENCAddToList( object value )
        {
            var _ENCList = BudgetTrackbarRegion.__ENCList;
            Monitor.Enter( _ENCList );

            try
            {
                if( BudgetTrackbarRegion.__ENCList.Count
                   == BudgetTrackbarRegion.__ENCList.Capacity )
                {
                    var item = 0;
                    var count = checked( BudgetTrackbarRegion.__ENCList.Count - 1 );

                    for( var i = 0; i <= count; i = checked( i + 1 ) )
                    {
                        if( BudgetTrackbarRegion.__ENCList[ i ].IsAlive )
                        {
                            if( i != item )
                            {
                                BudgetTrackbarRegion.__ENCList[ item ] =
                                    BudgetTrackbarRegion.__ENCList[ i ];
                            }

                            item = checked( item + 1 );
                        }
                    }

                    BudgetTrackbarRegion.__ENCList.RemoveRange( item,
                        checked( BudgetTrackbarRegion.__ENCList.Count - item ) );

                    BudgetTrackbarRegion.__ENCList.Capacity = BudgetTrackbarRegion.__ENCList.Count;
                }

                BudgetTrackbarRegion.__ENCList.Add(
                    new WeakReference( RuntimeHelpers.GetObjectValue( value ) ) );
            }
            finally
            {
                Monitor.Exit( _ENCList );
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the BudgetTrackBarRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void BudgetTrackBarRegion_MouseDown( object sender, MouseEventArgs e )
        {
            bool flag;

            if( e.Button == MouseButtons.Left )
            {
                if( e.X >= slider1Pos
                   && e.X < checked( slider1Pos + sliderWidth ) )
                {
                    if( slider1Pos == checked( Width - sliderWidth )
                           ? slider1Pos <= checked( slider2Pos + sliderWidth )
                           : false )
                    {
                        goto Label1;
                    }

                    flag = true;
                }

                Label1:
                flag = false;

                if( flag )
                {
                    scrolling1 = true;
                }
                else if( e.X < slider2Pos || e.X >= checked( slider2Pos + sliderWidth )
                            ? false
                            : true )
                {
                    scrolling2 = true;
                }
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the BudgetTrackBarRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void BudgetTrackBarRegion_MouseMove( object sender, MouseEventArgs e )
        {
            if( scrolling1 )
            {
                slider1Pos = checked( e.X - sliderWidth / 2 );

                if( slider1Pos < slider2Pos )
                {
                    slider1Pos = slider2Pos;
                }

                if( slider1Pos > checked( Width - sliderWidth ) )
                {
                    slider1Pos = checked( Width - sliderWidth );
                }

                val = checked( Convert.ToInt32( slider1Pos
                        / (double)checked( Width - sliderWidth )
                        * checked( maxVal - minVal ) )
                    + minVal );

                Invalidate( );
                var scrollEventHandler = BudgetTrackbarRegion.Scroll;

                if( scrollEventHandler != null )
                {
                    scrollEventHandler( this,
                        new BudgetTrackbarRegionEventArgs( SliderValue1, SliderValue2 ) );
                }
            }

            if( scrolling2 )
            {
                slider2Pos = checked( e.X - sliderWidth / 2 );

                if( slider2Pos < 0 )
                {
                    slider2Pos = 0;
                }

                if( slider2Pos > slider1Pos )
                {
                    slider2Pos = slider1Pos;
                }

                val2 = checked( Convert.ToInt32( slider2Pos
                        / (double)checked( Width - sliderWidth )
                        * checked( maxVal - minVal ) )
                    + minVal );

                Invalidate( );
                var scrollTwoEventHandler = BudgetTrackbarRegion.ScrollTwo;

                if( scrollTwoEventHandler != null )
                {
                    scrollTwoEventHandler( this,
                        new BudgetTrackbarRegionEventArgs( SliderValue1, SliderValue2 ) );
                }
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the BudgetTrackBarRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void BudgetTrackBarRegion_MouseUp( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {
                scrolling1 = false;
                scrolling2 = false;
            }
        }

        /// <summary>
        /// Handles the Paint event of the BudgetTrackBarRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void BudgetTrackBarRegion_Paint( object sender, PaintEventArgs e )
        {
            Rectangle rectangle;
            var num = checked( (int)Math.Round( (double)Height / 4 ) );
            var num1 = checked( (int)Math.Round( (double)Height / 2 ) );
            var pen = new Pen( ColorScheme.LeftColor );
            var pen1 = new Pen( ColorScheme.RightColor );
            var pen2 = new Pen( ColorScheme.MiddleColor );
            Brush solidBrush = new SolidBrush( ColorScheme.LeftColor );
            Brush brush = new SolidBrush( ColorScheme.RightColor );
            Brush solidBrush1 = new SolidBrush( ColorScheme.SwitchColor );
            Brush brush1 = new SolidBrush( ColorScheme.MiddleColor );

            if( UseFixedSize )
            {
                var graphics = e.Graphics;

                rectangle = new Rectangle( 0,
                    checked( (int)Math.Round( (double)checked( Height - BarSize ) / 2 ) ),
                    slider1Pos, BarSize );

                graphics.DrawRectangle( pen, rectangle );
                var graphic = e.Graphics;

                rectangle = new Rectangle( 0,
                    checked( (int)Math.Round( (double)checked( Height - BarSize ) / 2 ) ),
                    slider1Pos, BarSize );

                graphic.FillRectangle( solidBrush, rectangle );
                var graphics1 = e.Graphics;

                rectangle = new Rectangle( checked( slider2Pos + sliderWidth ),
                    checked( (int)Math.Round( (double)checked( Height - BarSize ) / 2 ) ),
                    slider1Pos, BarSize );

                graphics1.DrawRectangle( pen2, rectangle );
                var graphic1 = e.Graphics;

                rectangle = new Rectangle( checked( slider2Pos + sliderWidth ),
                    checked( (int)Math.Round( (double)checked( Height - BarSize ) / 2 ) ),
                    slider1Pos, BarSize );

                graphic1.FillRectangle( brush1, rectangle );
                var graphics2 = e.Graphics;

                rectangle = new Rectangle( checked( slider1Pos + sliderWidth ),
                    checked( (int)Math.Round( (double)checked( Height - BarSize ) / 2 ) ), Width,
                    BarSize );

                graphics2.DrawRectangle( pen1, rectangle );
                var graphic2 = e.Graphics;

                rectangle = new Rectangle( checked( slider1Pos + sliderWidth ),
                    checked( (int)Math.Round( (double)checked( Height - BarSize ) / 2 ) ), Width,
                    BarSize );

                graphic2.FillRectangle( brush, rectangle );
                var graphics3 = e.Graphics;

                rectangle = new Rectangle( slider1Pos,
                    checked( (int)Math.Round( (double)checked( Height - 16 ) / 2 ) ), sliderWidth,
                    16 );

                graphics3.FillRectangle( solidBrush1, rectangle );
                var graphic3 = e.Graphics;

                rectangle = new Rectangle( slider2Pos,
                    checked( (int)Math.Round( (double)checked( Height - 16 ) / 2 ) ), sliderWidth,
                    16 );

                graphic3.FillRectangle( solidBrush1, rectangle );

                if( UseSwitchBorders )
                {
                    var graphics4 = e.Graphics;
                    var white = Pens.White;

                    rectangle = new Rectangle( checked( slider2Pos - 1 ),
                        checked( (int)Math.Round( (double)checked( Height - 16 ) / 2 ) ),
                        checked( sliderWidth + 1 ), 16 );

                    graphics4.DrawRectangle( white, rectangle );
                    var graphic4 = e.Graphics;
                    var white1 = Pens.White;

                    rectangle = new Rectangle( checked( slider1Pos - 1 ),
                        checked( (int)Math.Round( (double)checked( Height - 16 ) / 2 ) ),
                        checked( sliderWidth + 1 ), 16 );

                    graphic4.DrawRectangle( white1, rectangle );
                }
            }
            else if( !UseFixedSize )
            {
                var graphics5 = e.Graphics;
                rectangle = new Rectangle( 0, num, slider1Pos, num1 );
                graphics5.DrawRectangle( pen, rectangle );
                var graphic5 = e.Graphics;
                rectangle = new Rectangle( 0, num, slider1Pos, num1 );
                graphic5.FillRectangle( solidBrush, rectangle );
                var graphics6 = e.Graphics;

                rectangle = new Rectangle( checked( slider2Pos + sliderWidth ), num, slider1Pos,
                    num1 );

                graphics6.DrawRectangle( pen2, rectangle );
                var graphic6 = e.Graphics;

                rectangle = new Rectangle( checked( slider2Pos + sliderWidth ), num, slider1Pos,
                    num1 );

                graphic6.FillRectangle( brush1, rectangle );
                var graphics7 = e.Graphics;
                rectangle = new Rectangle( checked( slider1Pos + sliderWidth ), num, Width, num1 );
                graphics7.DrawRectangle( pen1, rectangle );
                var graphic7 = e.Graphics;
                rectangle = new Rectangle( checked( slider1Pos + sliderWidth ), num, Width, num1 );
                graphic7.FillRectangle( brush, rectangle );
                var graphics8 = e.Graphics;
                rectangle = new Rectangle( slider1Pos, 0, sliderWidth, Height );
                graphics8.FillRectangle( solidBrush1, rectangle );
                var graphic8 = e.Graphics;
                rectangle = new Rectangle( slider2Pos, 0, sliderWidth, Height );
                graphic8.FillRectangle( solidBrush1, rectangle );

                if( UseSwitchBorders )
                {
                    var graphics9 = e.Graphics;
                    var white2 = Pens.White;

                    rectangle = new Rectangle( checked( slider2Pos - 1 ),
                        checked( (int)Math.Round( (double)checked( Height - 16 ) / 2 ) ),
                        checked( sliderWidth + 1 ), 16 );

                    graphics9.DrawRectangle( white2, rectangle );
                    var graphic9 = e.Graphics;
                    var white3 = Pens.White;

                    rectangle = new Rectangle( checked( slider1Pos - 1 ),
                        checked( (int)Math.Round( (double)checked( Height - 16 ) / 2 ) ),
                        checked( sliderWidth + 1 ), 16 );

                    graphic9.DrawRectangle( white3, rectangle );
                }
            }
        }

        /// <summary>
        /// Sets the position.
        /// </summary>
        private void SetPos( )
        {
            slider1Pos = Convert.ToInt32( checked( val - minVal )
                / (double)checked( maxVal - minVal )
                * checked( Width - sliderWidth ) );

            slider2Pos = Convert.ToInt32( checked( val2 - minVal )
                / (double)checked( maxVal - minVal )
                * checked( Width - sliderWidth ) );

            Invalidate( );
        }

        /// <summary>
        /// Occurs when [scroll].
        /// </summary>
        public static event ScrollEventHandler Scroll;

        /// <summary>
        /// Occurs when [scroll two].
        /// </summary>
        public static event ScrollTwoEventHandler ScrollTwo;

        /// <summary>
        /// Class MainColorScheme.
        /// </summary>
        public class MainColorScheme
        {
            /// <summary>
            /// The left color
            /// </summary>
            public Color _LeftColor;

            /// <summary>
            /// The middle color
            /// </summary>
            public Color _MiddleColor;

            /// <summary>
            /// The switch color
            /// </summary>
            public Color _SwitchColor;

            /// <summary>
            /// The right color
            /// </summary>
            public Color _RightColor;

            /// <summary>
            /// Gets or sets the color of the left.
            /// </summary>
            /// <value>The color of the left.</value>
            [ Browsable( true ) ]
            [ Category( "Appearance" ) ]
            [ Description( "Setzt die Farbe der Trackbar links von den Reglern." ) ]
            public Color LeftColor
            {
                get
                {
                    return _LeftColor;
                }
                set
                {
                    if( value != _LeftColor )
                    {
                        _LeftColor = value;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the color of the middle.
            /// </summary>
            /// <value>The color of the middle.</value>
            [ Browsable( true ) ]
            [ Category( "Appearance" ) ]
            [ Description( "Setzt die Farbe der Trackbar zwischen den Reglern." ) ]
            public Color MiddleColor
            {
                get
                {
                    return _MiddleColor;
                }
                set
                {
                    if( value != _MiddleColor )
                    {
                        _MiddleColor = value;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the color of the right.
            /// </summary>
            /// <value>The color of the right.</value>
            [ Browsable( true ) ]
            [ Category( "Appearance" ) ]
            [ Description( "Setzt die Farbe der Trackbar rechts von den Reglern." ) ]
            public Color RightColor
            {
                get
                {
                    return _RightColor;
                }
                set
                {
                    if( value != _RightColor )
                    {
                        _RightColor = value;
                    }
                }
            }

            /// <summary>
            /// Gets or sets the color of the switch.
            /// </summary>
            /// <value>The color of the switch.</value>
            [ Browsable( true ) ]
            [ Category( "Appearance" ) ]
            [ Description( "Setzt die Farbe der Regler." ) ]
            public Color SwitchColor
            {
                get
                {
                    return _SwitchColor;
                }
                set
                {
                    if( value != _SwitchColor )
                    {
                        _SwitchColor = value;
                    }
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MainColorScheme"/> class.
            /// </summary>
            public MainColorScheme( )
            {
                _LeftColor = Color.FromArgb( 229, 229, 229 );
                _MiddleColor = Color.FromArgb( 0, 164, 240 );
                _SwitchColor = Color.FromArgb( 101, 101, 101 );
                _RightColor = Color.FromArgb( 229, 229, 229 );
            }
        }

        /// <summary>
        /// Delegate ScrollEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BudgetTrackbarRegionEventArgs"/> instance containing the event data.</param>
        public delegate void ScrollEventHandler( object sender, BudgetTrackbarRegionEventArgs e );

        /// <summary>
        /// Delegate ScrollTwoEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BudgetTrackbarRegionEventArgs"/> instance containing the event data.</param>
        public delegate void ScrollTwoEventHandler(
            object sender, BudgetTrackbarRegionEventArgs e );
    }
}