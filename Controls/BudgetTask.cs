// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTask.cs" company="Terry D. Eppler">
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
//   BudgetTask.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering a metro-style task.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ Description( "A class for rendering a task." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetTask ), "BudgetTask.bmp" ) ]
    [ Designer( typeof( BudgetTaskDesigner ) ) ]
    [ DefaultEvent( "PointClicked" ) ]
    public class BudgetTask : Control
    {
        /// <summary>
        /// Enum representing the Task Shape
        /// </summary>
        public enum TaskShape
        {
            /// <summary>
            /// The circle
            /// </summary>
            Circle,

            /// <summary>
            /// The rectangle
            /// </summary>
            Rectangle,

            /// <summary>
            /// The pie
            /// </summary>
            Pie
        }

        #region Private Fields

        /// <summary>
        /// The task shape
        /// </summary>
        private TaskShape taskShape = TaskShape.Circle;

        /// <summary>
        /// The pie angles
        /// </summary>
        private PieAngles pieAngles = new( );

        /// <summary>
        /// The point collection
        /// </summary>
        private BudgetTaskPointCollection _PointCollection = new( );

        /// <summary>
        /// The finished color
        /// </summary>
        private Color _FinishedColor;

        /// <summary>
        /// The line color
        /// </summary>
        private Color _LineColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The start point width
        /// </summary>
        private int _StartPointWidth;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The point position
        /// </summary>
        private List<Rectangle> _PointPosition;

        /// <summary>
        /// The text rectangles
        /// </summary>
        private List<Rectangle> _TextRectangles;

        /// <summary>
        /// The distance
        /// </summary>
        private int distance;

        /// <summary>
        /// The hot point
        /// </summary>
        private Rectangle _HotPoint;

        /// <summary>
        /// The hot index
        /// </summary>
        private int _HotIndex;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the pie angle.
        /// </summary>
        /// <value>The pie angle.</value>
        [ TypeConverter( typeof( ExpandableObjectConverter ) ) ]
        public PieAngles PieAngle
        {
            get { return pieAngles; }
            set
            {
                pieAngles = value;
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
                    _AutoStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the finished.
        /// </summary>
        /// <value>The color of the finished.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the finished." ) ]
        public Color FinishedColor
        {
            get
            {
                return _FinishedColor;
            }
            set
            {
                if( _FinishedColor != value )
                {
                    _FinishedColor = value;
                    Invalidate( );
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
                if( _HoverColor != value )
                {
                    _HoverColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the line." ) ]
        public Color LineColor
        {
            get
            {
                return _LineColor;
            }
            set
            {
                if( _LineColor != value )
                {
                    _LineColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        [ Browsable( true ) ]
        [ Category( "Data" ) ]
        [ Description( "Sets the points." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Content ) ]
        public BudgetTaskPointCollection Points
        {
            get
            {
                return _PointCollection;
            }
            set
            {
                _PointCollection = value;
                Invalidate( );
            }
        }

        /// <summary>
        /// Gets or sets the width of the start point.
        /// </summary>
        /// <value>The start width of the point.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 10 ) ]
        [ Description( "Sets the width of the start point." ) ]
        public int StartPointWidth
        {
            get
            {
                return _StartPointWidth;
            }
            set
            {
                if( _StartPointWidth != value )
                {
                    _StartPointWidth = value;
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
                            _FinishedColor = Design.BudgetColors.TaskColor;
                            _LineColor = Design.BudgetColors.TaskColor;
                            _HoverColor = Design.BudgetColors.AccentLightBlue;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _FinishedColor = Design.BudgetColors.DarkTaskColor;
                            _LineColor = Design.BudgetColors.DarkTaskColor;
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
        /// Gets or sets the shape.
        /// </summary>
        /// <value>The shape.</value>
        public TaskShape Shape
        {
            get { return taskShape; }
            set
            {
                taskShape = value;
                Invalidate( );
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTask" /> class.
        /// </summary>
        public BudgetTask( )
        {
            //this._PointCollection = new BudgetTaskPointCollection();
            _FinishedColor = Design.BudgetColors.TaskColor;
            _LineColor = Design.BudgetColors.TaskColor;
            _HoverColor = Design.BudgetColors.AccentLightBlue;
            _StartPointWidth = 10;
            _Style = Design.Style.Light;
            _PointPosition = new List<Rectangle>( );
            _TextRectangles = new List<Rectangle>( );
            _HotIndex = -1;
            _AutoStyle = true;
            Font = new Font( "Segoe UI", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            var metroTask = this;
            _PointCollection.ItemAdded += metroTask.Point_Added;
            var metroTask1 = this;
            _PointCollection.ItemRemoving += metroTask1.Point_Removed;
            Refresh( );
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Item control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Item_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            Invalidate( );
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
            if( _HotIndex != -1
               && _PointCollection[ _HotIndex ].Enabled )
            {
                var pointClickedEventHandler = PointClicked;

                if( pointClickedEventHandler != null )
                {
                    pointClickedEventHandler( this,
                        new BudgetTaskPointCollectionEventArgs( _PointCollection[ _HotIndex ] ) );
                }
            }

            base.OnMouseClick( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave( EventArgs e )
        {
            _HotPoint = new Rectangle( );
            _HotIndex = -1;
            Invalidate( );
            base.OnMouseLeave( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove( MouseEventArgs e )
        {
            if( _PointPosition.Count > 0 )
            {
                var count = checked( _PointPosition.Count - 1 );

                for( var i = 0; i <= count; i = checked( i + 1 ) )
                {
                    if( ( _PointPosition[ i ].Contains( e.Location )
                           || _TextRectangles[ i ].Contains( e.Location )
                               ? true
                               : false )
                       && _HotPoint != _PointPosition[ i ] )
                    {
                        _HotPoint = _PointPosition[ i ];
                        _HotIndex = i;
                        Invalidate( );
                    }
                }
            }

            base.OnMouseMove( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            var enumerator = _PointCollection.GetEnumerator( );
            Rectangle rectangle;
            var rectangle1 = new Rectangle( );
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var circleWidth = _StartPointWidth;
            var point = new Point( checked( circleWidth + 6 ), checked( circleWidth + 6 ) );

            var point1 = new Point( checked( circleWidth + 6 ),
                checked( Height - checked( circleWidth + 6 ) ) );

            _PointPosition.Clear( );
            _TextRectangles.Clear( );

            if( _PointCollection.Count > 0 )
            {
                while( enumerator.MoveNext( ) )
                {
                    var current = enumerator.Current;

                    if( current.CircleWidth > circleWidth )
                    {
                        circleWidth = current.CircleWidth;
                    }
                }

                point = new Point( checked( circleWidth + 6 ), checked( circleWidth + 6 ) );

                point1 = new Point( checked( circleWidth + 6 ),
                    checked( Height - checked( circleWidth + 6 ) ) );

                distance =
                    checked( (int)Math.Round(
                        Design.Drawing.MeasurePointDistance( point, point1 ) ) );

                distance = checked( (int)Math.Round( distance / (double)_PointCollection.Count ) );
                var count = checked( _PointCollection.Count - 1 );

                for( var i = 0; i <= count; i = checked( i + 1 ) )
                {
                    var rectangles = _PointPosition;

                    rectangle = new Rectangle(
                        checked( checked( circleWidth + 6 )
                            - checked( (int)Math.Round( (double)_PointCollection[ i ].CircleWidth
                                / 2 ) ) ),
                        checked( checked( checked( Height - checked( circleWidth + 6 ) )
                                - checked( (int)Math.Round(
                                    (double)_PointCollection[ i ].CircleWidth
                                    / 2 ) ) )
                            - checked( distance * checked( i + 1 ) ) ),
                        _PointCollection[ i ].CircleWidth, _PointCollection[ i ].CircleWidth );

                    rectangles.Add( rectangle );
                }
            }

            var pen = new Pen( _LineColor );
            var solidBrush = new SolidBrush( _LineColor );
            var pen1 = new Pen( _HoverColor );
            graphics.DrawLine( pen, point, point1 );

            var point2 = new Point(
                checked( checked( circleWidth + 6 )
                    - checked( (int)Math.Round( (double)_StartPointWidth / 2 ) ) ),
                checked( checked( Height - checked( circleWidth + 6 ) )
                    - checked( (int)Math.Round( (double)_StartPointWidth / 2 ) ) ) );

            var size = new Size( _StartPointWidth, _StartPointWidth );
            rectangle = new Rectangle( point2, size );

            switch( taskShape )
            {
                case TaskShape.Circle:
                    graphics.FillEllipse( solidBrush, rectangle );
                    break;
                case TaskShape.Rectangle:
                    graphics.FillRectangle( solidBrush, rectangle );
                    break;
                case TaskShape.Pie:
                    graphics.FillPie( solidBrush, rectangle, PieAngle.StartAngle,
                        PieAngle.SweepAngle );

                    break;
            }

            if( _PointPosition.Count > 0 )
            {
                var num = checked( _PointCollection.Count - 1 );

                for( var j = 0; j <= num; j = checked( j + 1 ) )
                {
                    var solidBrush1 = new SolidBrush( _PointCollection[ j ].Finished
                        ? _FinishedColor
                        : _PointCollection[ j ].CirceColor );

                    switch( taskShape )
                    {
                        case TaskShape.Circle:
                            graphics.FillEllipse( solidBrush1, _PointPosition[ j ] );
                            break;
                        case TaskShape.Rectangle:
                            graphics.FillRectangle( solidBrush1, _PointPosition[ j ] );
                            break;
                        case TaskShape.Pie:
                            graphics.FillPie( solidBrush1, _PointPosition[ j ], PieAngle.StartAngle,
                                PieAngle.SweepAngle );

                            break;
                    }

                    if( _HotPoint != rectangle1
                       && j == _HotIndex
                       && _PointCollection[ _HotIndex ].Enabled )
                    {
                        solidBrush1.Color = _HoverColor;
                    }

                    var sizeF = graphics.MeasureString( _PointCollection[ j ].Text, Font );
                    var num1 = checked( checked( circleWidth * 2 ) + 6 );
                    rectangle = _PointPosition[ j ];

                    var rectangle2 = new Rectangle( num1,
                        checked( checked( rectangle.Y
                                + checked( (int)Math.Round(
                                    (double)_PointCollection[ j ].CircleWidth
                                    / 2 ) ) )
                            - checked( (int)Math.Round( sizeF.Height / 2f ) ) ),
                        checked( (int)Math.Round( sizeF.Width + 2f ) ),
                        checked( distance - checked( (int)Math.Round( sizeF.Height + 2f ) ) ) );

                    _TextRectangles.Add( rectangle2 );

                    graphics.DrawString( _PointCollection[ j ].Text, Font, solidBrush1,
                        rectangle2 );

                    var num2 =
                        checked( (int)Math.Round( (double)_PointCollection[ j ].CircleWidth / 4 ) );

                    if( _PointCollection[ j ].Icon != null )
                    {
                        var icon = _PointCollection[ j ].Icon;
                        rectangle = _PointPosition[ j ];
                        var x = checked( rectangle.X + num2 );
                        var item = _PointPosition[ j ];

                        var rectangle3 = new Rectangle( x, checked( item.Y + num2 ),
                            checked( num2 * 2 ), checked( num2 * 2 ) );

                        graphics.DrawImage( icon, rectangle3 );
                    }
                }

                if( _HotPoint != rectangle1 )
                {
                    if( _PointCollection[ _HotIndex ].Enabled )
                    {
                        switch( taskShape )
                        {
                            case TaskShape.Circle:
                                graphics.DrawEllipse( pen1, _HotPoint );
                                break;
                            case TaskShape.Rectangle:
                                graphics.DrawRectangle( pen1, _HotPoint );
                                break;
                            case TaskShape.Pie:
                                graphics.DrawPie( pen1, _HotPoint, PieAngle.StartAngle,
                                    PieAngle.SweepAngle );

                                break;
                        }
                    }
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Handles the Added event of the Point control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetTaskPointCollectionEventArgs"/> instance containing the event data.</param>
        private void Point_Added( object sender, BudgetTaskPointCollectionEventArgs e )
        {
            if( e.Item != null )
            {
                var metroTask = this;
                e.Item.PropertyChanged += metroTask.Item_PropertyChanged;
            }

            var pointAddedEventHandler = PointAdded;

            if( pointAddedEventHandler != null )
            {
                pointAddedEventHandler( this, new BudgetTaskPointCollectionEventArgs( e.Item ) );
            }
        }

        /// <summary>
        /// Handles the Removed event of the Point control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetTaskPointCollectionEventArgs"/> instance containing the event data.</param>
        private void Point_Removed( object sender, BudgetTaskPointCollectionEventArgs e )
        {
            if( e.Item != null )
            {
                var metroTask = this;
                e.Item.PropertyChanged -= metroTask.Item_PropertyChanged;
            }
        }

        /// <summary>
        /// Occurs when [point added].
        /// </summary>
        public event PointAddedEventHandler PointAdded;

        /// <summary>
        /// Occurs when [point clicked].
        /// </summary>
        public event PointClickedEventHandler PointClicked;

        /// <summary>
        /// Delegate PointAddedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BudgetTaskPointCollectionEventArgs"/> instance containing the event data.</param>
        public delegate void PointAddedEventHandler(
            object sender, BudgetTaskPointCollectionEventArgs e );

        /// <summary>
        /// Delegate PointClickedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BudgetTaskPointCollectionEventArgs"/> instance containing the event data.</param>
        public delegate void PointClickedEventHandler(
            object sender, BudgetTaskPointCollectionEventArgs e );
    }

    /// <summary>
    /// Class PieAngles.
    /// </summary>
    public class PieAngles
    {
        /// <summary>
        /// The start angle
        /// </summary>
        private float startAngle = 180f;

        /// <summary>
        /// The sweep angle
        /// </summary>
        private float sweepAngle = 360f;

        /// <summary>
        /// Gets or sets the start angle.
        /// </summary>
        /// <value>The start angle.</value>
        public float StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }

        /// <summary>
        /// Gets or sets the sweep angle.
        /// </summary>
        /// <value>The sweep angle.</value>
        public float SweepAngle
        {
            get { return sweepAngle; }
            set { sweepAngle = value; }
        }
    }
}