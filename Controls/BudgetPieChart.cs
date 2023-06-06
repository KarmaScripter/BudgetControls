// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetPieChart.cs" company="Terry D. Eppler">
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
//   BudgetPieChart.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering a metro-style pie chart.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "Click" ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetPieChart ), "BudgetPieChart.bmp" ) ]
    [ Designer( typeof( BudgetPieChartDesigner ) ) ]
    public class BudgetPieChart : Control
    {
        #region Private Fields

        /// <summary>
        /// The segments
        /// </summary>
        private BudgetPieChartSegmentCollection _Segments = new( );

        /// <summary>
        /// The radius
        /// </summary>
        private int _radius;

        /// <summary>
        /// The effect size
        /// </summary>
        private int _EffectSize;

        /// <summary>
        /// The show effect
        /// </summary>
        private bool _ShowEffect;

        /// <summary>
        /// The drawsegmentborders
        /// </summary>
        private bool _drawsegmentborders;

        /// <summary>
        /// The use dynamic border colors
        /// </summary>
        private bool _UseDynamicBorderColors;

        /// <summary>
        /// The segment border size
        /// </summary>
        private int _SegmentBorderSize;

        /// <summary>
        /// The style
        /// </summary>
        private TrackerStyle _Style;

        /// <summary>
        /// The show segment names
        /// </summary>
        private bool _ShowSegmentNames;

        /// <summary>
        /// The segment names per row
        /// </summary>
        private int _SegmentNamesPerRow;

        /// <summary>
        /// The use dynamic fill colors
        /// </summary>
        private bool _UseDynamicFillColors;

        /// <summary>
        /// The fill color alpha
        /// </summary>
        private int _FillColorAlpha;

        /// <summary>
        /// The show donut effect
        /// </summary>
        private bool _showDonutEffect;

        /// <summary>
        /// The donut effect size
        /// </summary>
        private int _DonutEffectSize;

        /// <summary>
        /// The seperate segments
        /// </summary>
        private bool _SeperateSegments;

        /// <summary>
        /// The draw border
        /// </summary>
        private bool _DrawBorder;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The border size
        /// </summary>
        private int _BorderSize;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [ Browsable( true ) ]
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
        /// Gets or sets the size of the border.
        /// </summary>
        /// <value>The size of the border.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "Sets the size of the border." ) ]
        public int BorderSize
        {
            get
            {
                return _BorderSize;
            }
            set
            {
                if( value != _BorderSize )
                {
                    _BorderSize = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the donut effect.
        /// </summary>
        /// <value>The size of the donut effect.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 100 ) ]
        [ Description( "Sets the size of the donut effect." ) ]
        public int DonutEffectSize
        {
            get
            {
                return _DonutEffectSize;
            }
            set
            {
                if( value != _DonutEffectSize )
                {
                    _DonutEffectSize = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw the border.
        /// </summary>
        /// <value><c>true</c> if draw border; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Gibt an, ob das PieChart umrandet werden soll." ) ]
        public bool DrawBorder
        {
            get
            {
                return _DrawBorder;
            }
            set
            {
                if( _DrawBorder != value )
                {
                    _DrawBorder = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw segment borders.
        /// </summary>
        /// <value><c>true</c> if draw segment borders; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to draw segment borders." ) ]
        public bool DrawSegmentBorders
        {
            get
            {
                return _drawsegmentborders;
            }
            set
            {
                if( value != _drawsegmentborders )
                {
                    _drawsegmentborders = value;

                    if( !_drawsegmentborders )
                    {
                        _SeperateSegments = false;
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the dynamic color offset.
        /// </summary>
        /// <value>The dynamic color offset.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 82 ) ]
        [ Description( "Sets the dynamic color offset." ) ]
        public int DynamicColorOffset
        {
            get
            {
                return _FillColorAlpha;
            }
            set
            {
                if( value != _FillColorAlpha )
                {
                    _FillColorAlpha = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the effect.
        /// </summary>
        /// <value>The size of the effect.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 10 ) ]
        [ Description( "Sets the size of the effect." ) ]
        public int EffectSize
        {
            get
            {
                return _EffectSize;
            }
            set
            {
                if( value != _EffectSize )
                {
                    _EffectSize = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 200 ) ]
        [ Description( "Sets the radius." ) ]
        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if( value != _radius )
                {
                    _radius = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the border segment.
        /// </summary>
        /// <value>The size of the border segment.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "Sets the size of the border segment." ) ]
        public int SegmentBorderSize
        {
            get
            {
                return _SegmentBorderSize;
            }
            set
            {
                if( value != _SegmentBorderSize )
                {
                    _SegmentBorderSize = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets the segment names per row.
        /// </summary>
        /// <value>The segment names per row.</value>
        [ Browsable( true ) ]
        [ Category( "Behavoir" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "Gets the segment names per row." ) ]
        public int SegmentNamesPerRow
        {
            get
            {
                return _SegmentNamesPerRow;
            }
        }

        /// <summary>
        /// Gets the segments.
        /// </summary>
        /// <value>The segments.</value>
        [ Browsable( true ) ]
        [ Category( "Data" ) ]
        [ Description( "Gets the segments." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Content ) ]
        public BudgetPieChartSegmentCollection Segments
        {
            get
            {
                return _Segments;
            }
            set
            {
                _Segments = value;
                Invalidate( );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to seperate segments.
        /// </summary>
        /// <value><c>true</c> if seperate segments; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to seperate segments." ) ]
        public bool SeperateSegments
        {
            get
            {
                return _SeperateSegments;
            }
            set
            {
                if( value != _SeperateSegments )
                {
                    _SeperateSegments = value;
                    _drawsegmentborders = true;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show donut effect.
        /// </summary>
        /// <value><c>true</c> if show donut effect; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to show donut effect." ) ]
        public bool ShowDonutEffect
        {
            get
            {
                return _showDonutEffect;
            }
            set
            {
                if( value != _showDonutEffect )
                {
                    _showDonutEffect = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show effect.
        /// </summary>
        /// <value><c>true</c> if show effect; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to show effect." ) ]
        public bool ShowEffect
        {
            get
            {
                return _ShowEffect;
            }
            set
            {
                if( value != _ShowEffect )
                {
                    _ShowEffect = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show segment names.
        /// </summary>
        /// <value><c>true</c> if show segment names; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Behavoir" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to show segment names." ) ]
        public bool ShowSegmentNames
        {
            get
            {
                return _ShowSegmentNames;
            }
            set
            {
                if( value != _ShowSegmentNames )
                {
                    _ShowSegmentNames = value;
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
        [ DefaultValue( 1 ) ]
        [ Description( "Sets the style." ) ]
        public TrackerStyle Style
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

                    if( _Style == TrackerStyle.Abstract )
                    {
                        _DrawBorder = true;
                        _BorderColor = Color.FromArgb( 18, 173, 196 );
                    }
                    else if( _Style == TrackerStyle.Normal )
                    {
                        _DrawBorder = false;
                    }

                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use dynamic border colors.
        /// </summary>
        /// <value><c>true</c> if use dynamic border colors; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to use dynamic border colors." ) ]
        public bool UseDynamicBorderColors
        {
            get
            {
                return _UseDynamicBorderColors;
            }
            set
            {
                if( value != _UseDynamicBorderColors )
                {
                    _UseDynamicBorderColors = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use dynamic fill colors.
        /// </summary>
        /// <value><c>true</c> if use dynamic fill colors; otherwise, <c>false</c>.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to use dynamic fill colors." ) ]
        public bool UseDynamicFillColors
        {
            get
            {
                return _UseDynamicFillColors;
            }
            set
            {
                if( value != _UseDynamicFillColors )
                {
                    _UseDynamicFillColors = value;
                    Invalidate( );
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetPieChart" /> class.
        /// </summary>
        public BudgetPieChart( )
        {
            _radius = 200;
            _EffectSize = 10;
            _ShowEffect = true;
            _drawsegmentborders = false;
            _UseDynamicBorderColors = false;
            _SegmentBorderSize = 2;
            _Style = TrackerStyle.Normal;
            _ShowSegmentNames = false;
            _SegmentNamesPerRow = 2;
            _UseDynamicFillColors = false;
            _FillColorAlpha = 82;
            _showDonutEffect = false;
            _DonutEffectSize = 100;
            _SeperateSegments = false;
            _DrawBorder = false;
            _BorderColor = Color.FromArgb( 18, 173, 196 );
            _BorderSize = 2;
            Size = new Size( 215, 280 );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            var _budgetPieChart = this;
            _Segments.ItemAdded += _budgetPieChart.Paths_Added;
            var _budgetPieChart1 = this;
            _Segments.ItemRemoving += _budgetPieChart1.Paths_Removing;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnBackColorChanged( EventArgs e )
        {
            Invalidate( );
            base.OnBackColorChanged( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            var value = 0f;
            Rectangle rectangle;
            var graphics = e.Graphics;
            var pen = new Pen( Color.FromArgb( 50, 255, 255, 255 ), _EffectSize );

            var rectangle1 =
                new Rectangle(
                    checked( (int)Math.Round( (double)Width / 2 - (double)_radius / 2 - 1 ) ), 2,
                    _radius, _radius );

            var rectangle2 = new Rectangle(
                checked( (int)Math.Round( (double)Width / 2
                    - (double)_radius / 2
                    + (double)_EffectSize / 2
                    - 0.25 ) ), checked( (int)Math.Round( (double)_EffectSize / 2 + 2 ) ),
                checked( checked( _radius - _EffectSize ) - 3 ),
                checked( checked( _radius - _EffectSize ) + 0 ) );

            var rectangle3 = new Rectangle(
                checked( (int)Math.Round( (double)Width / 2
                    - (double)_radius / 2
                    + (double)_DonutEffectSize / 2
                    + 0 ) ), checked( (int)Math.Round( (double)_DonutEffectSize / 2 + 2 ) ),
                checked( checked( 0 + _radius ) - _DonutEffectSize ),
                checked( checked( 0 + _radius ) - _DonutEffectSize ) );

            var rectangle4 =
                new Rectangle(
                    checked( (int)Math.Round( (double)Width / 2 - (double)_radius / 2 + 5 - 1 ) ),
                    7, checked( _radius - 10 ), checked( _radius - 10 ) );

            var num = 0;
            var font = new Font( Font.FontFamily, 10f, FontStyle.Regular, GraphicsUnit.Pixel );
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            var count = checked( Segments.Count - 1 );

            for( var i = 0; i <= count; i = checked( i + 1 ) )
            {
                value += Segments[ i ].Value;
            }

            var single = 0f;

            if( Style != TrackerStyle.Abstract )
            {
                var count1 = checked( Segments.Count - 1 );

                for( var j = 0; j <= count1; j = checked( j + 1 ) )
                {
                    var value1 = Segments[ j ].Value / value * 360f;

                    using( var solidBrush = new SolidBrush( Segments[ j ].FillColor ) )
                    {
                        if( !Segments[ j ].UseFillStyle )
                        {
                            graphics.FillPie( solidBrush, rectangle1, single, value1 );
                        }
                        else
                        {
                            graphics.FillPie(
                                new HatchBrush( Segments[ j ].FillStyle, Segments[ j ].BorderColor,
                                    Segments[ j ].FillColor ), rectangle1, single, value1 );
                        }
                    }

                    single += value1;
                }
            }
            else
            {
                graphics.DrawEllipse( new Pen( Color.FromArgb( 18, 173, 196 ), 2f ), rectangle1 );
                var num1 = checked( Segments.Count - 1 );

                for( var k = 0; k <= num1; k = checked( k + 1 ) )
                {
                    var single1 = Segments[ k ].Value / value * 360f;

                    using( var solidBrush1 = new SolidBrush( Segments[ k ].FillColor ) )
                    {
                        if( !UseDynamicFillColors )
                        {
                            graphics.FillPie( solidBrush1, rectangle4, single, single1 );
                        }
                        else
                        {
                            graphics.FillPie( new SolidBrush( Segments[ k ].BorderColor ),
                                rectangle4, single, single1 );

                            graphics.FillPie(
                                new SolidBrush( Color.FromArgb( _FillColorAlpha, 38, 15, 0 ) ),
                                rectangle4, single, single1 );
                        }
                    }

                    single += single1;
                }
            }

            if( DrawSegmentBorders )
            {
                single = 0f;

                if( Style != TrackerStyle.Abstract )
                {
                    var count2 = checked( Segments.Count - 1 );

                    for( var l = 0; l <= count2; l = checked( l + 1 ) )
                    {
                        var value2 = Segments[ l ].Value / value * 360f;

                        using( var solidBrush2 = new SolidBrush( Segments[ l ].FillColor ) )
                        {
                            if( SeperateSegments )
                            {
                                graphics.DrawPie( new Pen( BackColor, 3f ), rectangle1, single,
                                    value2 );
                            }
                            else if( !UseDynamicBorderColors )
                            {
                                graphics.DrawPie(
                                    new Pen( Segments[ l ].BorderColor, SegmentBorderSize ),
                                    rectangle1, single, value2 );
                            }
                            else
                            {
                                var color = SetShade( solidBrush2.Color, 40 );

                                graphics.DrawPie( new Pen( color, SegmentBorderSize ), rectangle1,
                                    single, value2 );
                            }

                            solidBrush2.Dispose( );
                        }

                        single += value2;
                    }
                }
                else
                {
                    var num2 = checked( Segments.Count - 1 );

                    for( var m = 0; m <= num2; m = checked( m + 1 ) )
                    {
                        var single2 = Segments[ m ].Value / value * 360f;

                        using( var solidBrush3 = new SolidBrush( Segments[ m ].FillColor ) )
                        {
                            if( !UseDynamicBorderColors )
                            {
                                graphics.DrawPie(
                                    new Pen( Segments[ m ].BorderColor, SegmentBorderSize ),
                                    rectangle4, single, single2 );
                            }
                            else
                            {
                                var color1 = SetShade( solidBrush3.Color, 40 );

                                graphics.DrawPie( new Pen( color1, SegmentBorderSize ), rectangle4,
                                    single, single2 );
                            }

                            solidBrush3.Dispose( );
                        }

                        single += single2;
                    }
                }
            }

            if( ShowEffect )
            {
                graphics.DrawEllipse( pen, rectangle2 );
            }

            if( ShowDonutEffect )
            {
                graphics.FillEllipse( new SolidBrush( BackColor ), rectangle3 );
            }

            if( DrawBorder )
            {
                graphics.DrawEllipse( new Pen( _BorderColor, BorderSize ), rectangle1 );
            }

            if( ShowSegmentNames )
            {
                var num3 = 0;
                var count3 = checked( Segments.Count - 1 );

                for( var n = 0; n <= count3; n = checked( n + 1 ) )
                {
                    using( var solidBrush4 = new SolidBrush( Segments[ n ].FillColor ) )
                    {
                        if( num != 0 )
                        {
                            var black = Pens.Black;

                            rectangle =
                                new Rectangle( checked( (int)Math.Round( (double)Width / 2 ) ),
                                    checked( checked( checked( _radius + 2 ) + 10 ) + num3 ), 10,
                                    10 );

                            graphics.DrawRectangle( black, rectangle );

                            if( !( Style == TrackerStyle.Abstract & UseDynamicFillColors ) )
                            {
                                rectangle = new Rectangle(
                                    checked( (int)Math.Round( (double)Width / 2 ) ),
                                    checked( checked( checked( _radius + 2 ) + 10 ) + num3 ), 10,
                                    10 );

                                graphics.FillRectangle( solidBrush4, rectangle );
                            }
                            else
                            {
                                var solidBrush5 = new SolidBrush( Segments[ n ].BorderColor );

                                rectangle = new Rectangle(
                                    checked( (int)Math.Round( (double)Width / 2 ) ),
                                    checked( checked( checked( _radius + 2 ) + 10 ) + num3 ), 10,
                                    10 );

                                graphics.FillRectangle( solidBrush5, rectangle );

                                var solidBrush6 =
                                    new SolidBrush( Color.FromArgb( _FillColorAlpha, 38, 15, 0 ) );

                                rectangle = new Rectangle(
                                    checked( (int)Math.Round( (double)Width / 2 ) ),
                                    checked( checked( checked( _radius + 2 ) + 10 ) + num3 ), 10,
                                    10 );

                                graphics.FillRectangle( solidBrush6, rectangle );
                            }

                            var name = Segments[ n ].Name;
                            var brush = Brushes.Black;

                            rectangle =
                                new Rectangle( checked( (int)Math.Round( (double)Width / 2 + 13 ) ),
                                    checked( checked( checked( _radius + 2 ) + 8 ) + num3 ), 80,
                                    15 );

                            graphics.DrawString( name, font, brush, rectangle );
                            num3 = checked( num3 + 20 );
                            num = 0;
                        }
                        else
                        {
                            var black1 = Pens.Black;

                            rectangle = new Rectangle( 2,
                                checked( checked( checked( _radius + 2 ) + 10 ) + num3 ), 10, 10 );

                            graphics.DrawRectangle( black1, rectangle );

                            if( !( Style == TrackerStyle.Abstract & UseDynamicFillColors ) )
                            {
                                rectangle = new Rectangle( 2,
                                    checked( checked( checked( _radius + 2 ) + 10 ) + num3 ), 10,
                                    10 );

                                graphics.FillRectangle( solidBrush4, rectangle );
                            }
                            else
                            {
                                var solidBrush7 = new SolidBrush( Segments[ n ].BorderColor );

                                rectangle = new Rectangle( 2,
                                    checked( checked( checked( _radius + 2 ) + 10 ) + num3 ), 10,
                                    10 );

                                graphics.FillRectangle( solidBrush7, rectangle );

                                var solidBrush8 =
                                    new SolidBrush( Color.FromArgb( _FillColorAlpha, 38, 15, 0 ) );

                                rectangle = new Rectangle( 2,
                                    checked( checked( checked( _radius + 2 ) + 10 ) + num3 ), 10,
                                    10 );

                                graphics.FillRectangle( solidBrush8, rectangle );
                            }

                            var str = Segments[ n ].Name;
                            var brush1 = Brushes.Black;

                            rectangle = new Rectangle( 15,
                                checked( checked( checked( _radius + 2 ) + 8 ) + num3 ), 80, 15 );

                            graphics.DrawString( str, font, brush1, rectangle );
                            num = 1;
                        }
                    }
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Path control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Path_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            var _budgetPieChartSegment = (BudgetPieChartSegment)sender;

            if( Operators.CompareString( e.PropertyName, "FillColor", false ) == 0 )
            {
            }

            Invalidate( );
        }

        /// <summary>
        /// Handles the Added event of the Paths control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetPieChartSegmentCollectionEventArgs"/> instance containing the event data.</param>
        private void Paths_Added( object sender, BudgetPieChartSegmentCollectionEventArgs e )
        {
            if( e.Item != null )
            {
                var _budgetPieChart = this;
                e.Item.PropertyChanged += _budgetPieChart.Path_PropertyChanged;
            }

            var segmentAddedEventHandler = BudgetPieChart.SegmentAdded;

            if( segmentAddedEventHandler != null )
            {
                segmentAddedEventHandler( this,
                    new BudgetPieChartSegmentCollectionEventArgs( e.Item ) );
            }

            Invalidate( );
        }

        /// <summary>
        /// Handles the Removing event of the Paths control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetPieChartSegmentCollectionEventArgs"/> instance containing the event data.</param>
        private void Paths_Removing( object sender, BudgetPieChartSegmentCollectionEventArgs e )
        {
            if( e.Item != null )
            {
                var _budgetPieChart = this;
                e.Item.PropertyChanged -= _budgetPieChart.Path_PropertyChanged;
            }
        }

        /// <summary>
        /// Sets the shade.
        /// </summary>
        /// <param name="InputColor">Color of the input.</param>
        /// <param name="Offset">The offset.</param>
        /// <returns>Color.</returns>
        private Color SetShade( Color InputColor, int Offset )
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

        /// <summary>
        /// Occurs when [segment added].
        /// </summary>
        public static event SegmentAddedEventHandler SegmentAdded;

        /// <summary>
        /// Delegate SegmentAddedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BudgetPieChartSegmentCollectionEventArgs"/> instance containing the event data.</param>
        public delegate void SegmentAddedEventHandler(
            object sender, BudgetPieChartSegmentCollectionEventArgs e );

        /// <summary>
        /// Enum TrackerStyle
        /// </summary>
        public enum TrackerStyle
        {
            /// <summary>
            /// The abstract
            /// </summary>
            Abstract,

            /// <summary>
            /// The normal
            /// </summary>
            Normal
        }
    }

    #region Old Smart Tag

    /// <summary>
    /// Class MetroPieChartActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroPieChartActionList : DesignerActionList
    {
        /// <summary>
        /// The pc
        /// </summary>
        private BudgetPieChart _pc;

        /// <summary>
        /// The designer action SVC
        /// </summary>
        private DesignerActionUIService designerActionSvc;

        /// <summary>
        /// Gets or sets the size of the donut effect.
        /// </summary>
        /// <value>The size of the donut effect.</value>
        public int DonutEffectSize
        {
            get
            {
                return _pc.DonutEffectSize;
            }
            set
            {
                _pc.DonutEffectSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the dynamic color offset.
        /// </summary>
        /// <value>The dynamic color offset.</value>
        public int DynamicColorOffset
        {
            get
            {
                return _pc.DynamicColorOffset;
            }
            set
            {
                _pc.DynamicColorOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the effect.
        /// </summary>
        /// <value>The size of the effect.</value>
        public int EffectSize
        {
            get
            {
                return _pc.EffectSize;
            }
            set
            {
                _pc.EffectSize = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [seperate segments].
        /// </summary>
        /// <value><c>true</c> if [seperate segments]; otherwise, <c>false</c>.</value>
        public bool SeperateSegments
        {
            get
            {
                return _pc.SeperateSegments;
            }
            set
            {
                _pc.SeperateSegments = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show donut effect].
        /// </summary>
        /// <value><c>true</c> if [show donut effect]; otherwise, <c>false</c>.</value>
        public bool ShowDonutEffect
        {
            get
            {
                return _pc.ShowDonutEffect;
            }
            set
            {
                _pc.ShowDonutEffect = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show effect].
        /// </summary>
        /// <value><c>true</c> if [show effect]; otherwise, <c>false</c>.</value>
        public bool ShowEffect
        {
            get
            {
                return _pc.ShowEffect;
            }
            set
            {
                _pc.ShowEffect = value;
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public BudgetPieChart.TrackerStyle Style
        {
            get
            {
                return _pc.Style;
            }
            set
            {
                _pc.Style = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use dynamic border colors].
        /// </summary>
        /// <value><c>true</c> if [use dynamic border colors]; otherwise, <c>false</c>.</value>
        public bool UseDynamicBorderColors
        {
            get
            {
                return _pc.UseDynamicBorderColors;
            }
            set
            {
                _pc.UseDynamicBorderColors = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use dynamic fill colors].
        /// </summary>
        /// <value><c>true</c> if [use dynamic fill colors]; otherwise, <c>false</c>.</value>
        public bool UseDynamicFillColors
        {
            get
            {
                return _pc.UseDynamicFillColors;
            }
            set
            {
                _pc.UseDynamicFillColors = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroPieChartActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroPieChartActionList( IComponent component )
            : base( component )
        {
            designerActionSvc = null;
            _pc = (BudgetPieChart)component;

            designerActionSvc =
                (DesignerActionUIService)GetService( typeof( DesignerActionUIService ) );
        }

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems( )
        {
            var designerActionItemCollection = new DesignerActionItemCollection( );
            designerActionItemCollection.Add( new DesignerActionHeaderItem( "Effekte" ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem( "ShowEffect",
                "ShowEffect:", "Effekte", "Gibt an, ob ein Glanzeffekt gezeichnet werden soll." ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem( "ShowDonutEffect",
                "ShowDonutEffect:", "Effekte",
                "Gibt an, ob ein Donuteffekt gezeichnet werden soll." ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem( "SeperateSegments",
                "SeperateSegments:", "Effekte",
                "Gibt an, ob die Segmente getrennt werden sollen." ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem( "EffectSize",
                "EffectSize:", "Effekt", "Gibt an, wie dick der Glanzeffect sein soll.." ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem( "DonutEffectSize",
                "DonutEffectSize:", "Effekt", "Gibt an, wie gro� der Donuteffekt sein soll." ) );

            designerActionItemCollection.Add( new DesignerActionHeaderItem( "Stil und Farben" ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem( "Style", "Style:",
                "Stil und Farben", "Der Style des MetroCharts." ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem(
                "UseDynamicFillColors", "UseDynamicFillColors:", "Stil und Farben",
                "Gibt an, ob die F�llfarbe der Segmente generiert werden soll. Funktioniert nur, wenn zu den Segmente einen Umrandungsfarbe zugeordnet wurde." ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem(
                "UseDynamicBorderColors", "UseDynamicBorderColors:", "Stil und Farben",
                "Gibt an, ob die Umrandungsfarbe der Segmente generiert werden soll." ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem( "DynamicColorOffset",
                "DynamicColorOffset:", "Stil und Farben",
                "Gibt an, wie stark die F�llfarbe verdunkelt werden soll, wenn \"UseDynamicFillColors\"= True entspricht." ) );

            return designerActionItemCollection;
        }
    }

    #endregion
}