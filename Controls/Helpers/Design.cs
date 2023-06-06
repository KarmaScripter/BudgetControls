// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="Design.cs" company="Terry D. Eppler">
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
//   Design.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// Class Design.
    /// </summary>
    public class Design
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Design"/> class.
        /// </summary>
        [ DebuggerNonUserCode ]
        public Design( )
        {
        }

        /// <summary>
        /// Class Controls.
        /// </summary>
        public class Controls
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Controls"/> class.
            /// </summary>
            [ DebuggerNonUserCode ]
            public Controls( )
            {
            }

            /// <summary>
            /// Sets the double buffered.
            /// </summary>
            /// <param name="ctrl">The control.</param>
            /// <param name="value">if set to <c>true</c> [value].</param>
            public static void SetDoubleBuffered( Control ctrl, bool value )
            {
                if( !SystemInformation.TerminalServerSession )
                {
                    var property = typeof( Control ).GetProperty( "DoubleBuffered",
                        BindingFlags.Instance | BindingFlags.NonPublic );

                    property.SetValue( ctrl, value, null );
                }
            }
        }

        /// <summary>
        /// Class Drawing.
        /// </summary>
        public class Drawing
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Drawing"/> class.
            /// </summary>
            [ DebuggerNonUserCode ]
            public Drawing( )
            {
            }

            /// <summary>
            /// Draws the rounded path.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="size">The size.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="curve">The curve.</param>
            /// <param name="TopLeft">if set to <c>true</c> [top left].</param>
            /// <param name="TopRight">if set to <c>true</c> [top right].</param>
            /// <param name="BottomLeft">if set to <c>true</c> [bottom left].</param>
            /// <param name="BottomRight">if set to <c>true</c> [bottom right].</param>
            public static void DrawRoundedPath(
                Graphics g, Color c, float size, Rectangle rect,
                int curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true,
                bool BottomRight = true )
            {
                using( var pen = new Pen( c, size ) )
                {
                    g.DrawPath( pen,
                        Drawing.RoundRectangle( rect, curve, TopLeft, TopRight, BottomLeft,
                            BottomRight ) );
                }
            }

            /// <summary>
            /// Extracts the icon.
            /// </summary>
            /// <param name="original">The original.</param>
            /// <param name="size">The size.</param>
            /// <returns>Bitmap.</returns>
            public static Bitmap ExtractIcon( Icon original, int size )
            {
                Bitmap bitmap;

                using( var icon = new Icon( original, new Size( size, size ) ) )
                {
                    bitmap = icon.ToBitmap( );
                }

                return bitmap;
            }

            /// <summary>
            /// Fades the ellipse.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="stages">The stages.</param>
            /// <param name="stageWidth">Width of the stage.</param>
            public static void FadeEllipse(
                Graphics g, Color c, Rectangle rect, int stages = 5,
                int stageWidth = 4 )
            {
                var num = checked( checked( (int)Math.Round( c.A / (double)stages ) ) - 1 );
                var num1 = stages;

                for( var i = 0; i <= num1; i = checked( i + 1 ) )
                {
                    using( var solidBrush = new SolidBrush( Color.FromArgb( checked( num
                              * ( num == 0
                                  ? 1
                                  : i ) ), c ) ) )
                    {
                        var rectangle = new Rectangle(
                            checked( rect.X + checked( stageWidth * i ) ),
                            checked( rect.Y + checked( stageWidth * i ) ),
                            checked( rect.Width - checked( checked( stageWidth * i ) * 2 ) ),
                            checked( rect.Height - checked( checked( stageWidth * i ) * 2 ) ) );

                        g.FillEllipse( solidBrush, rectangle );
                    }
                }
            }

            /// <summary>
            /// Fades the ellipse.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            /// <param name="width">The width.</param>
            /// <param name="height">The height.</param>
            /// <param name="stages">The stages.</param>
            /// <param name="stageWidth">Width of the stage.</param>
            public static void FadeEllipse(
                Graphics g, Color c, int x, int y,
                int width, int height, int stages = 5, int stageWidth = 4 )
            {
                var rectangle = new Rectangle( x, y, width, height );
                Drawing.FadeEllipse( g, c, rectangle, stages, stageWidth );
            }

            /// <summary>
            /// Fades the rectangle.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="stages">The stages.</param>
            /// <param name="stageWidth">Width of the stage.</param>
            public static void FadeRectangle(
                Graphics g, Color c, Rectangle rect, int stages = 5,
                int stageWidth = 4 )
            {
                var num = checked( checked( (int)Math.Round( 255 / (double)stages ) ) - 1 );
                var num1 = stages;

                for( var i = 0; i <= num1; i = checked( i + 1 ) )
                {
                    using( var solidBrush = new SolidBrush( Color.FromArgb( checked( num
                              * ( num == 0
                                  ? 1
                                  : i ) ), c ) ) )
                    {
                        var rectangle = new Rectangle(
                            checked( rect.X + checked( stageWidth * i ) ),
                            checked( rect.Y + checked( stageWidth * i ) ),
                            checked( rect.Width - checked( checked( stageWidth * i ) * 2 ) ),
                            checked( rect.Height - checked( checked( stageWidth * i ) * 2 ) ) );

                        g.FillRectangle( solidBrush, rectangle );
                    }
                }
            }

            /// <summary>
            /// Fades the rectangle.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            /// <param name="width">The width.</param>
            /// <param name="height">The height.</param>
            /// <param name="stages">The stages.</param>
            /// <param name="stageWidth">Width of the stage.</param>
            public static void FadeRectangle(
                Graphics g, Color c, int x, int y,
                int width, int height, int stages = 5, int stageWidth = 4 )
            {
                var rectangle = new Rectangle( x, y, width, height );
                Drawing.FadeRectangle( g, c, rectangle, stages, stageWidth );
            }

            /// <summary>
            /// Fills the rounded path.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="curve">The curve.</param>
            /// <param name="TopLeft">if set to <c>true</c> [top left].</param>
            /// <param name="TopRight">if set to <c>true</c> [top right].</param>
            /// <param name="BottomLeft">if set to <c>true</c> [bottom left].</param>
            /// <param name="BottomRight">if set to <c>true</c> [bottom right].</param>
            public static void FillRoundedPath(
                Graphics g, Color c, Rectangle rect, int curve,
                bool TopLeft = true, bool TopRight = true, bool BottomLeft = true,
                bool BottomRight = true )
            {
                using( var solidBrush = new SolidBrush( c ) )
                {
                    g.FillPath( solidBrush,
                        Drawing.RoundRectangle( rect, curve, TopLeft, TopRight, BottomLeft,
                            BottomRight ) );
                }
            }

            /// <summary>
            /// Fills the rounded path.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="b">The b.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="curve">The curve.</param>
            /// <param name="TopLeft">if set to <c>true</c> [top left].</param>
            /// <param name="TopRight">if set to <c>true</c> [top right].</param>
            /// <param name="BottomLeft">if set to <c>true</c> [bottom left].</param>
            /// <param name="BottomRight">if set to <c>true</c> [bottom right].</param>
            public static void FillRoundedPath(
                Graphics g, Brush b, Rectangle rect, int curve,
                bool TopLeft = true, bool TopRight = true, bool BottomLeft = true,
                bool BottomRight = true )
            {
                g.FillPath( b, Drawing.RoundRectangle( rect, curve, TopLeft, TopRight, BottomLeft,
                    BottomRight ) );
            }

            /// <summary>
            /// Gets the circle intersection points.
            /// </summary>
            /// <param name="ellipse">The ellipse.</param>
            /// <param name="point1">The point1.</param>
            /// <param name="point2">The point2.</param>
            /// <returns>Point[].</returns>
            public static Point[ ] GetCircleIntersectionPoints(
                Rectangle ellipse, Point point1, Point point2 )
            {
                float single;
                Point point;
                var points = new List<Point>( );

                float x = checked( ellipse.X
                    + checked( (int)Math.Round( (double)ellipse.Width / 2 ) ) );

                float y = checked( ellipse.Y
                    + checked( (int)Math.Round( (double)ellipse.Height / 2 ) ) );

                float x1 = checked( point2.X - point1.X );
                float y1 = checked( point2.Y - point1.Y );
                var single1 = x1 * x1 + y1 * y1;
                var x2 = 2f * ( x1 * ( point1.X - x ) + y1 * ( point1.Y - y ) );

                var single2 = ( point1.X - x ) * ( point1.X - x )
                    + ( point1.Y - y ) * ( point1.Y - y )
                    - checked( checked( (int)Math.Round( (double)ellipse.Width / 2 ) )
                        * checked( (int)Math.Round( (double)ellipse.Width / 2 ) ) );

                var single3 = x2 * x2 - 4f * single1 * single2;

                if( !( single1 <= 1E-07 | single3 < 0f ) )
                {
                    if( single3 != 0f )
                    {
                        single = (float)( ( -x2 + Math.Sqrt( single3 ) ) / ( 2f * single1 ) );

                        point = new Point( checked( (int)Math.Round( point1.X + single * x1 ) ),
                            checked( (int)Math.Round( point1.Y + single * y1 ) ) );

                        points.Add( point );
                        single = (float)( ( -x2 - Math.Sqrt( single3 ) ) / ( 2f * single1 ) );

                        point = new Point( checked( (int)Math.Round( point1.X + single * x1 ) ),
                            checked( (int)Math.Round( point1.Y + single * y1 ) ) );

                        points.Add( point );
                    }
                    else
                    {
                        single = -x2 / ( 2f * single1 );

                        point = new Point( checked( (int)Math.Round( point1.X + single * x1 ) ),
                            checked( (int)Math.Round( point1.Y + single * y1 ) ) );

                        points.Add( point );
                    }
                }

                return points.ToArray( );
            }

            /// <summary>
            /// Gets the circle intersection points.
            /// </summary>
            /// <param name="ellipse">The ellipse.</param>
            /// <param name="x1">The x1.</param>
            /// <param name="y1">The y1.</param>
            /// <param name="x2">The x2.</param>
            /// <param name="y2">The y2.</param>
            /// <returns>Point[].</returns>
            public static Point[ ] GetCircleIntersectionPoints(
                Rectangle ellipse, int x1, int y1, int x2,
                int y2 )
            {
                var point = new Point( x1, y1 );
                var point1 = new Point( x2, y2 );
                return Drawing.GetCircleIntersectionPoints( ellipse, point, point1 );
            }

            /// <summary>
            /// Gets the point on line.
            /// </summary>
            /// <param name="p1">The p1.</param>
            /// <param name="p2">The p2.</param>
            /// <param name="perc">The perc.</param>
            /// <returns>Point.</returns>
            public static Point GetPointOnLine( Point p1, Point p2, int perc )
            {
                var x = (float)( checked( p2.X - p1.X ) * ( (double)perc / 100 ) + p1.X );
                var y = (float)( checked( p2.Y - p1.Y ) * ( (double)perc / 100 ) + p1.Y );

                var point = new Point( checked( (int)Math.Round( x ) ),
                    checked( (int)Math.Round( y ) ) );

                return point;
            }

            /// <summary>
            /// Gets the point on line.
            /// </summary>
            /// <param name="x1">The x1.</param>
            /// <param name="y1">The y1.</param>
            /// <param name="x2">The x2.</param>
            /// <param name="y2">The y2.</param>
            /// <param name="perc">The perc.</param>
            /// <returns>Point.</returns>
            public static Point GetPointOnLine(
                int x1, int y1, int x2, int y2,
                int perc )
            {
                var point = new Point( x1, y1 );
                var point1 = new Point( x2, y2 );
                return Drawing.GetPointOnLine( point, point1, perc );
            }

            /// <summary>
            /// Gets the point on line.
            /// </summary>
            /// <param name="x1">The x1.</param>
            /// <param name="y1">The y1.</param>
            /// <param name="point2">The point2.</param>
            /// <param name="perc">The perc.</param>
            /// <returns>Point.</returns>
            public static Point GetPointOnLine( int x1, int y1, Point point2, int perc )
            {
                var point = new Point( x1, y1 );
                return Drawing.GetPointOnLine( point, point2, perc );
            }

            /// <summary>
            /// Gets the point on line.
            /// </summary>
            /// <param name="point1">The point1.</param>
            /// <param name="x2">The x2.</param>
            /// <param name="y2">The y2.</param>
            /// <param name="perc">The perc.</param>
            /// <returns>Point.</returns>
            public static Point GetPointOnLine( Point point1, int x2, int y2, int perc )
            {
                var point = new Point( x2, y2 );
                return Drawing.GetPointOnLine( point1, point, perc );
            }

            /// <summary>
            /// Measures the point distance.
            /// </summary>
            /// <param name="p1">The p1.</param>
            /// <param name="p2">The p2.</param>
            /// <returns>System.Double.</returns>
            public static double MeasurePointDistance( Point p1, Point p2 )
            {
                var x = checked( p2.X - p1.X );
                var y = checked( p2.Y - p1.Y );
                var num = Math.Sqrt( checked( checked( x * x ) + checked( y * y ) ) );
                return num;
            }

            /// <summary>
            /// Measures the point distance.
            /// </summary>
            /// <param name="x1">The x1.</param>
            /// <param name="y1">The y1.</param>
            /// <param name="x2">The x2.</param>
            /// <param name="y2">The y2.</param>
            /// <returns>System.Double.</returns>
            public static double MeasurePointDistance( int x1, int y1, int x2, int y2 )
            {
                var point = new Point( x1, y1 );
                return Drawing.MeasurePointDistance( point, new Point( x2, y2 ) );
            }

            /// <summary>
            /// Rounds the rectangle.
            /// </summary>
            /// <param name="r">The r.</param>
            /// <param name="Curve">The curve.</param>
            /// <param name="TopLeft">if set to <c>true</c> [top left].</param>
            /// <param name="TopRight">if set to <c>true</c> [top right].</param>
            /// <param name="BottomLeft">if set to <c>true</c> [bottom left].</param>
            /// <param name="BottomRight">if set to <c>true</c> [bottom right].</param>
            /// <returns>GraphicsPath.</returns>
            public static GraphicsPath RoundRectangle(
                Rectangle r, int Curve, bool TopLeft = true, bool TopRight = true,
                bool BottomLeft = true, bool BottomRight = true )
            {
                var graphicsPath = new GraphicsPath( FillMode.Winding );

                if( !TopLeft )
                {
                    graphicsPath.AddLine( r.X, r.Y, r.X, r.Y );
                }
                else
                {
                    graphicsPath.AddArc( r.X, r.Y, Curve, Curve, 180f,
                        90f );
                }

                if( !TopRight )
                {
                    graphicsPath.AddLine( checked( r.Right - r.Width ), r.Y, r.Width, r.Y );
                }
                else
                {
                    graphicsPath.AddArc( checked( r.Right - Curve ), r.Y, Curve, Curve, 270f,
                        90f );
                }

                if( !BottomRight )
                {
                    graphicsPath.AddLine( r.Right, r.Bottom, r.Right, r.Bottom );
                }
                else
                {
                    graphicsPath.AddArc( checked( r.Right - Curve ), checked( r.Bottom - Curve ),
                        Curve, Curve, 0f, 90f );
                }

                if( !BottomLeft )
                {
                    graphicsPath.AddLine( r.X, r.Bottom, r.X, r.Bottom );
                }
                else
                {
                    graphicsPath.AddArc( r.X, checked( r.Bottom - Curve ), Curve, Curve, 90f,
                        90f );
                }

                graphicsPath.CloseFigure( );
                return graphicsPath;
            }
        }

        /// <summary>
        /// Enum GridStyle
        /// </summary>
        public enum GridStyle
        {
            /// <summary>
            /// The none
            /// </summary>
            None,

            /// <summary>
            /// The horizontal
            /// </summary>
            Horizontal,

            /// <summary>
            /// The vertical
            /// </summary>
            Vertical,

            /// <summary>
            /// The crossed
            /// </summary>
            Crossed
        }

        /// <summary>
        /// Enum Orientation
        /// </summary>
        public enum Orientation
        {
            /// <summary>
            /// The horizontal
            /// </summary>
            Horizontal,

            /// <summary>
            /// The vertical
            /// </summary>
            Vertical
        }

        /// <summary>
        /// Class BudgetColors.
        /// </summary>
        public class BudgetColors
        {
            /// <summary>
            /// The accent blue
            /// </summary>
            private static Color _AccentBlue;

            /// <summary>
            /// The accent purple
            /// </summary>
            private static Color _AccentPurple;

            /// <summary>
            /// The accent orange
            /// </summary>
            private static Color _AccentOrange;

            /// <summary>
            /// The accent dark blue
            /// </summary>
            private static Color _AccentDarkBlue;

            /// <summary>
            /// The accent light blue
            /// </summary>
            private static Color _AccentLightBlue;

            /// <summary>
            /// The selection color
            /// </summary>
            private static Color _SelectionColor;

            /// <summary>
            /// The pop up border
            /// </summary>
            private static Color _PopUpBorder;

            /// <summary>
            /// The pop up font
            /// </summary>
            private static Color _PopUpFont;

            /// <summary>
            /// The tab item hover
            /// </summary>
            private static Color _TabItemHover;

            /// <summary>
            /// The disabled border
            /// </summary>
            private static Color _DisabledBorder;

            /// <summary>
            /// The task color
            /// </summary>
            private static Color _TaskColor;

            /// <summary>
            /// The dark task color
            /// </summary>
            private static Color _DarkTaskColor;

            /// <summary>
            /// The light default
            /// </summary>
            private static Color _LightDefault;

            /// <summary>
            /// The light dark default
            /// </summary>
            private static Color _LightDarkDefault;

            /// <summary>
            /// The light hover
            /// </summary>
            private static Color _LightHover;

            /// <summary>
            /// The light icon
            /// </summary>
            private static Color _LightIcon;

            /// <summary>
            /// The light border
            /// </summary>
            private static Color _LightBorder;

            /// <summary>
            /// The light font
            /// </summary>
            private static Color _LightFont;

            /// <summary>
            /// The light disabled
            /// </summary>
            private static Color _LightDisabled;

            /// <summary>
            /// The light disabled font
            /// </summary>
            private static Color _LightDisabledFont;

            /// <summary>
            /// The light switch rail
            /// </summary>
            private static Color _LightSwitchRail;

            /// <summary>
            /// The dark default
            /// </summary>
            private static Color _DarkDefault;

            /// <summary>
            /// The dark hover
            /// </summary>
            private static Color _DarkHover;

            /// <summary>
            /// The dark icon
            /// </summary>
            private static Color _DarkIcon;

            /// <summary>
            /// The dark font
            /// </summary>
            private static Color _DarkFont;

            /// <summary>
            /// The dark disabled
            /// </summary>
            private static Color _DarkDisabled;

            /// <summary>
            /// The dark disabled font
            /// </summary>
            private static Color _DarkDisabledFont;

            /// <summary>
            /// The text shadow
            /// </summary>
            private static Color _TextShadow;

            /// <summary>
            /// Gets the accent blue.
            /// </summary>
            /// <value>The accent blue.</value>
            public static Color AccentBlue
            {
                get
                {
                    return BudgetColors._AccentBlue;
                }
            }

            /// <summary>
            /// Gets the accent dark blue.
            /// </summary>
            /// <value>The accent dark blue.</value>
            public static Color AccentDarkBlue
            {
                get
                {
                    return BudgetColors._AccentDarkBlue;
                }
            }

            /// <summary>
            /// Gets the accent light blue.
            /// </summary>
            /// <value>The accent light blue.</value>
            public static Color AccentLightBlue
            {
                get
                {
                    return BudgetColors._AccentLightBlue;
                }
            }

            /// <summary>
            /// Gets the accent orange.
            /// </summary>
            /// <value>The accent orange.</value>
            public static Color AccentOrange
            {
                get
                {
                    return BudgetColors._AccentOrange;
                }
            }

            /// <summary>
            /// Gets the accent purple.
            /// </summary>
            /// <value>The accent purple.</value>
            public static Color AccentPurple
            {
                get
                {
                    return BudgetColors._AccentPurple;
                }
            }

            /// <summary>
            /// Gets the dark default.
            /// </summary>
            /// <value>The dark default.</value>
            public static Color DarkDefault
            {
                get
                {
                    return BudgetColors._DarkDefault;
                }
            }

            /// <summary>
            /// Gets the dark disabled.
            /// </summary>
            /// <value>The dark disabled.</value>
            public static Color DarkDisabled
            {
                get
                {
                    return BudgetColors._DarkDisabled;
                }
            }

            /// <summary>
            /// Gets the dark disabled font.
            /// </summary>
            /// <value>The dark disabled font.</value>
            public static Color DarkDisabledFont
            {
                get
                {
                    return BudgetColors._DarkDisabledFont;
                }
            }

            /// <summary>
            /// Gets the dark font.
            /// </summary>
            /// <value>The dark font.</value>
            public static Color DarkFont
            {
                get
                {
                    return BudgetColors._DarkFont;
                }
            }

            /// <summary>
            /// Gets the dark hover.
            /// </summary>
            /// <value>The dark hover.</value>
            public static Color DarkHover
            {
                get
                {
                    return BudgetColors._DarkHover;
                }
            }

            /// <summary>
            /// Gets the dark icon.
            /// </summary>
            /// <value>The dark icon.</value>
            public static Color DarkIcon
            {
                get
                {
                    return BudgetColors._DarkIcon;
                }
            }

            /// <summary>
            /// Gets the color of the dark task.
            /// </summary>
            /// <value>The color of the dark task.</value>
            public static Color DarkTaskColor
            {
                get
                {
                    return BudgetColors._DarkTaskColor;
                }
            }

            /// <summary>
            /// Gets the disabled border.
            /// </summary>
            /// <value>The disabled border.</value>
            public static Color DisabledBorder
            {
                get
                {
                    return BudgetColors._DisabledBorder;
                }
            }

            /// <summary>
            /// Gets the light border.
            /// </summary>
            /// <value>The light border.</value>
            public static Color LightBorder
            {
                get
                {
                    return BudgetColors._LightBorder;
                }
            }

            /// <summary>
            /// Gets the light dark default.
            /// </summary>
            /// <value>The light dark default.</value>
            public static Color LightDarkDefault
            {
                get
                {
                    return BudgetColors._LightDarkDefault;
                }
            }

            /// <summary>
            /// Gets the light default.
            /// </summary>
            /// <value>The light default.</value>
            public static Color LightDefault
            {
                get
                {
                    return BudgetColors._LightDefault;
                }
            }

            /// <summary>
            /// Gets the light disabled.
            /// </summary>
            /// <value>The light disabled.</value>
            public static Color LightDisabled
            {
                get
                {
                    return BudgetColors._LightDisabled;
                }
            }

            /// <summary>
            /// Gets the light disabled font.
            /// </summary>
            /// <value>The light disabled font.</value>
            public static Color LightDisabledFont
            {
                get
                {
                    return BudgetColors._LightDisabledFont;
                }
            }

            /// <summary>
            /// Gets the light font.
            /// </summary>
            /// <value>The light font.</value>
            public static Color LightFont
            {
                get
                {
                    return BudgetColors._LightFont;
                }
            }

            /// <summary>
            /// Gets the light hover.
            /// </summary>
            /// <value>The light hover.</value>
            public static Color LightHover
            {
                get
                {
                    return BudgetColors._LightHover;
                }
            }

            /// <summary>
            /// Gets the light icon.
            /// </summary>
            /// <value>The light icon.</value>
            public static Color LightIcon
            {
                get
                {
                    return BudgetColors._LightIcon;
                }
            }

            /// <summary>
            /// Gets the light switch rail.
            /// </summary>
            /// <value>The light switch rail.</value>
            public static Color LightSwitchRail
            {
                get
                {
                    return BudgetColors._LightSwitchRail;
                }
            }

            /// <summary>
            /// Gets the pop up border.
            /// </summary>
            /// <value>The pop up border.</value>
            public static Color PopUpBorder
            {
                get
                {
                    return BudgetColors._PopUpBorder;
                }
            }

            /// <summary>
            /// Gets the pop up font.
            /// </summary>
            /// <value>The pop up font.</value>
            public static Color PopUpFont
            {
                get
                {
                    return BudgetColors._PopUpFont;
                }
            }

            /// <summary>
            /// Gets the color of the selection.
            /// </summary>
            /// <value>The color of the selection.</value>
            public static Color SelectionColor
            {
                get
                {
                    return BudgetColors._SelectionColor;
                }
            }

            /// <summary>
            /// Gets the tab item hover.
            /// </summary>
            /// <value>The tab item hover.</value>
            public static Color TabItemHover
            {
                get
                {
                    return BudgetColors._TabItemHover;
                }
            }

            /// <summary>
            /// Gets the color of the task.
            /// </summary>
            /// <value>The color of the task.</value>
            public static Color TaskColor
            {
                get
                {
                    return BudgetColors._TaskColor;
                }
            }

            /// <summary>
            /// Gets the text shadow.
            /// </summary>
            /// <value>The text shadow.</value>
            public static Color TextShadow
            {
                get
                {
                    return BudgetColors._TextShadow;
                }
            }

            /// <summary>
            /// Initializes static members of the <see cref="BudgetColors"/> class.
            /// </summary>
            static BudgetColors( )
            {
                BudgetColors._AccentBlue = Color.FromArgb( 0, 122, 204 );
                BudgetColors._AccentPurple = Color.FromArgb( 104, 33, 122 );
                BudgetColors._AccentOrange = Color.FromArgb( 202, 81, 0 );
                BudgetColors._AccentDarkBlue = Color.FromArgb( 0, 99, 165 );
                BudgetColors._AccentLightBlue = Color.FromArgb( 0, 153, 255 );
                BudgetColors._SelectionColor = Color.FromArgb( 30, 0, 122, 204 );
                BudgetColors._PopUpBorder = Color.FromArgb( 240, 240, 240 );
                BudgetColors._PopUpFont = Color.FromArgb( 106, 115, 124 );
                BudgetColors._TabItemHover = Color.FromArgb( 10, 0, 122, 204 );
                BudgetColors._DisabledBorder = Color.FromArgb( 200, 200, 200 );
                BudgetColors._TaskColor = Color.FromArgb( 230, 230, 230 );
                BudgetColors._DarkTaskColor = Color.FromArgb( 25, 25, 25 );
                BudgetColors._LightDefault = Color.White;
                BudgetColors._LightDarkDefault = Color.FromArgb( 230, 230, 230 );
                BudgetColors._LightHover = Color.FromArgb( 240, 240, 240 );
                BudgetColors._LightIcon = Color.Black;
                BudgetColors._LightBorder = Color.FromArgb( 98, 98, 98 );
                BudgetColors._LightFont = Color.Black;
                BudgetColors._LightDisabled = Color.FromArgb( 250, 250, 250 );
                BudgetColors._LightDisabledFont = Color.Gray;
                BudgetColors._LightSwitchRail = Color.FromArgb( 190, 190, 190 );
                BudgetColors._DarkDefault = Color.FromArgb( 40, 40, 40 );
                BudgetColors._DarkHover = Color.FromArgb( 63, 63, 63 );
                BudgetColors._DarkIcon = Color.FromArgb( 241, 241, 241 );
                BudgetColors._DarkFont = Color.FromArgb( 153, 153, 153 );
                BudgetColors._DarkDisabled = Color.FromArgb( 35, 35, 35 );
                BudgetColors._DarkDisabledFont = Color.FromArgb( 133, 133, 133 );
                BudgetColors._TextShadow = Color.FromArgb( 30, Color.Black );
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="BudgetColors"/> class.
            /// </summary>
            [ DebuggerNonUserCode ]
            public BudgetColors( )
            {
            }

            /// <summary>
            /// Changes the color brightness.
            /// </summary>
            /// <param name="color">The color.</param>
            /// <param name="correctionFactor">The correction factor.</param>
            /// <returns>Color.</returns>
            public static Color ChangeColorBrightness( Color color, float correctionFactor )
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
            /// Colors to HTML.
            /// </summary>
            /// <param name="C">The c.</param>
            /// <returns>System.String.</returns>
            public static string ColorToHTML( Color C )
            {
                return ColorTranslator.ToHtml( C );
            }

            /// <summary>
            /// Gets the color of the correct back.
            /// </summary>
            /// <param name="style">The style.</param>
            /// <param name="defaultColor">The default color.</param>
            /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
            /// <returns>Color.</returns>
            public static Color GetCorrectBackColor(
                Style style, Color defaultColor, bool isEnabled = true )
            {
                var color = defaultColor;

                if( style == Style.Light )
                {
                    color = isEnabled
                        ? BudgetColors.LightDefault
                        : BudgetColors.LightDisabled;
                }
                else if( style == Style.Dark )
                {
                    color = isEnabled
                        ? BudgetColors.DarkDefault
                        : BudgetColors.DarkDisabled;
                }

                return color;
            }

            /// <summary>
            /// Gets the color of the correct fore.
            /// </summary>
            /// <param name="style">The style.</param>
            /// <param name="defaultColor">The default color.</param>
            /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
            /// <returns>Color.</returns>
            public static Color GetCorrectForeColor(
                Style style, Color defaultColor, bool isEnabled = true )
            {
                var color = defaultColor;

                if( style == Style.Light )
                {
                    color = isEnabled
                        ? BudgetColors.LightFont
                        : BudgetColors.LightDisabledFont;
                }
                else if( style == Style.Dark )
                {
                    color = isEnabled
                        ? BudgetColors.DarkFont
                        : BudgetColors.DarkDisabledFont;
                }

                return color;
            }

            /// <summary>
            /// HTMLs to color.
            /// </summary>
            /// <param name="sColor">Color of the s.</param>
            /// <returns>Color.</returns>
            public static Color HTMLToColor( string sColor )
            {
                return ColorTranslator.FromHtml( string.Concat( "#", sColor ) );
            }

            /// <summary>
            /// Inverts the color.
            /// </summary>
            /// <param name="c">The c.</param>
            /// <returns>Color.</returns>
            public static Color InvertColor( Color c )
            {
                return Color.FromArgb( c.ToArgb( ) ^ 16777215 );
            }
        }

        /// <summary>
        /// Enum Style
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// The light
            /// </summary>
            Light,

            /// <summary>
            /// The dark
            /// </summary>
            Dark,

            /// <summary>
            /// The custom
            /// </summary>
            Custom
        }
    }
}