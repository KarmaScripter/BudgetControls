// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTracker.cs" company="Terry D. Eppler">
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
//   BudgetTracker.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering metro-style tracker.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ Description( "A class for rendering a tracker." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetTracker ), "BudgetTracker.bmp" ) ]
    public class BudgetTracker : Control
    {
        #region Private Fields

        /// <summary>
        /// The paths
        /// </summary>
        private BudgetTrackerPathCollection _paths;

        /// <summary>
        /// The pens
        /// </summary>
        private Dictionary<BudgetTrackerPath, Pen> _pens;

        /// <summary>
        /// The brushes
        /// </summary>
        private Dictionary<BudgetTrackerPath, Brush> _brushes;

        /// <summary>
        /// The name brushes
        /// </summary>
        private Dictionary<BudgetTrackerPath, Brush> _nameBrushes;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The grid color
        /// </summary>
        private Color _GridColor;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The grid style
        /// </summary>
        private Design.GridStyle _GridStyle;

        /// <summary>
        /// The grid size
        /// </summary>
        private int _GridSize;

        /// <summary>
        /// The show path names
        /// </summary>
        private bool _ShowPathNames;

        /// <summary>
        /// The show custom value
        /// </summary>
        private bool _ShowCustomValue;

        /// <summary>
        /// The showed value count
        /// </summary>
        private int _ShowedValueCount;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to enable automatic style.
        /// </summary>
        /// <value><c>true</c> if [automatic style]; otherwise, <c>false</c>.</value>
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
        /// Gets or sets a value indicating whether to display text.
        /// </summary>
        /// <value><c>true</c> if display text; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to display text." ) ]
        public bool DisplayText
        {
            get
            {
                return _ShowCustomValue;
            }
            set
            {
                if( value != _ShowCustomValue )
                {
                    _ShowCustomValue = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the grid.
        /// </summary>
        /// <value>The color of the grid.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the grid." ) ]
        public Color GridColor
        {
            get
            {
                return _GridColor;
            }
            set
            {
                if( value != _GridColor )
                {
                    _GridColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the grid.
        /// </summary>
        /// <value>The size of the grid.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 10 ) ]
        [ Description( "Sets the size of the grid." ) ]
        public int GridSize
        {
            get
            {
                return _GridSize;
            }
            set
            {
                if( value != _GridSize )
                {
                    _GridSize = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the grid style.
        /// </summary>
        /// <value>The grid style.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 3 ) ]
        [ Description( "Sets the grid style." ) ]
        public Design.GridStyle GridStyle
        {
            get
            {
                return _GridStyle;
            }
            set
            {
                if( value != _GridStyle )
                {
                    _GridStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets the paths.
        /// </summary>
        /// <value>The paths.</value>
        [ Browsable( true ) ]
        [ Category( "Data" ) ]
        [ Description( "Gets the paths." ) ]
        public BudgetTrackerPathCollection Paths
        {
            get
            {
                return _paths;
            }
        }

        /// <summary>
        /// Gets or sets the showed value count.
        /// </summary>
        /// <value>The showed value count.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( 1000 ) ]
        [ Description( "Sets the showed value count." ) ]
        public int ShowedValueCount
        {
            get
            {
                return _ShowedValueCount;
            }
            set
            {
                if( value != _ShowedValueCount )
                {
                    _ShowedValueCount = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show path names.
        /// </summary>
        /// <value><c>true</c> if show path names; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to show path names." ) ]
        public bool ShowPathNames
        {
            get
            {
                return _ShowPathNames;
            }
            set
            {
                if( value != _ShowPathNames )
                {
                    _ShowPathNames = value;
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
                            _GridColor = Design.BudgetColors.PopUpBorder;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            BackColor = Design.BudgetColors.LightDefault;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _GridColor = Color.FromArgb( 200, Design.BudgetColors.LightBorder );
                            _BorderColor = Design.BudgetColors.LightBorder;
                            BackColor = Design.BudgetColors.DarkDefault;
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

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTracker" /> class.
        /// </summary>
        public BudgetTracker( )
        {
            _paths = new BudgetTrackerPathCollection( );
            _pens = new Dictionary<BudgetTrackerPath, Pen>( );
            _brushes = new Dictionary<BudgetTrackerPath, Brush>( );
            _nameBrushes = new Dictionary<BudgetTrackerPath, Brush>( );
            _BorderColor = Design.BudgetColors.LightBorder;
            _GridColor = Design.BudgetColors.PopUpBorder;
            _Style = Design.Style.Light;
            _GridStyle = Design.GridStyle.Crossed;
            _GridSize = 10;
            _ShowPathNames = true;
            _ShowCustomValue = true;
            _ShowedValueCount = 1000;
            _AutoStyle = true;

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            BackColor = Color.White;
            _paths = new BudgetTrackerPathCollection( );
            var metroTracker = this;
            Paths.ItemAdded += metroTracker.Paths_Added;
            var metroTracker1 = this;
            Paths.ItemRemoving += metroTracker1.Paths_Removing;
            _pens = new Dictionary<BudgetTrackerPath, Pen>( );
            _brushes = new Dictionary<BudgetTrackerPath, Brush>( );
            _nameBrushes = new Dictionary<BudgetTrackerPath, Brush>( );
        }

        /// <summary>
        /// Finalizes this instance.
        /// </summary>
        protected virtual void Finalize( )
        {
            var enumerator = new Dictionary<BudgetTrackerPath, Pen>.Enumerator( );
            var enumerator1 = new Dictionary<BudgetTrackerPath, Brush>.Enumerator( );
            var enumerator2 = new Dictionary<BudgetTrackerPath, Brush>.Enumerator( );

            try
            {
                enumerator = _pens.GetEnumerator( );

                while( enumerator.MoveNext( ) )
                {
                    enumerator.Current.Value.Dispose( );
                }
            }
            finally
            {
                enumerator.Dispose( );
            }

            _pens.Clear( );

            try
            {
                enumerator1 = _brushes.GetEnumerator( );

                while( enumerator1.MoveNext( ) )
                {
                    enumerator1.Current.Value.Dispose( );
                }
            }
            finally
            {
                enumerator1.Dispose( );
            }

            _brushes.Clear( );

            try
            {
                enumerator2 = _nameBrushes.GetEnumerator( );

                while( enumerator2.MoveNext( ) )
                {
                    enumerator2.Current.Value.Dispose( );
                }
            }
            finally
            {
                enumerator2.Dispose( );
            }

            _nameBrushes.Clear( );
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
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            StringFormat stringFormat;
            var graphics = e.Graphics;
            var width = (float)( Width / (double)checked( _ShowedValueCount - 1 ) );

            using( var pen = new Pen( _GridColor ) )
            {
                switch( _GridStyle )
                {
                    case Design.GridStyle.Horizontal:
                    {
                        var num = checked( (int)Math.Round( Height / (double)_GridSize ) );

                        for( var i = 1; i <= num; i = checked( i + 1 ) )
                        {
                            graphics.DrawLine( pen, 0, checked( i * _GridSize ), Width,
                                checked( i * _GridSize ) );
                        }

                        break;
                    }
                    case Design.GridStyle.Vertical:
                    {
                        var num1 = checked( (int)Math.Round( Width / (double)_GridSize ) );

                        for( var j = 1; j <= num1; j = checked( j + 1 ) )
                        {
                            graphics.DrawLine( pen, checked( j * _GridSize ), 0,
                                checked( j * _GridSize ), Height );
                        }

                        break;
                    }
                    case Design.GridStyle.Crossed:
                    {
                        var num2 = checked( (int)Math.Round( Height / (double)_GridSize ) );

                        for( var k = 1; k <= num2; k = checked( k + 1 ) )
                        {
                            graphics.DrawLine( pen, 0, checked( k * _GridSize ), Width,
                                checked( k * _GridSize ) );
                        }

                        var num3 = checked( (int)Math.Round( Width / (double)_GridSize ) );

                        for( var l = 1; l <= num3; l = checked( l + 1 ) )
                        {
                            graphics.DrawLine( pen, checked( l * _GridSize ), 0,
                                checked( l * _GridSize ), Height );
                        }

                        break;
                    }
                }
            }

            var count = checked( Paths.Count - 1 );

            for( var m = 0; m <= count; m = checked( m + 1 ) )
            {
                var item = Paths[ m ];
                var pointF = new PointF[ checked( checked( _ShowedValueCount - 1 ) + 1 ) ];

                using( var enumerator = item.GetEnumerator( ) )
                {
                    if( item.Count > _ShowedValueCount )
                    {
                        var count1 = checked( checked( item.Count - _ShowedValueCount ) - 1 );

                        for( var n = 0; n <= count1; n = checked( n + 1 ) )
                        {
                            enumerator.MoveNext( );
                        }
                    }

                    var num4 = checked( _ShowedValueCount - 1 );

                    for( var o = 0; o <= num4; o = checked( o + 1 ) )
                    {
                        var single = o * width;

                        if( o < checked( _ShowedValueCount - item.Count ) )
                        {
                            pointF[ o ] = new PointF( single, Height );
                        }
                        else
                        {
                            enumerator.MoveNext( );

                            var height = Height
                                - (float)( enumerator.Current / (double)item.Maximum ) * Height;

                            pointF[ o ] = new PointF( single, height );
                        }
                    }
                }

                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                var graphicsPath = new GraphicsPath( );
                graphicsPath.StartFigure( );
                graphicsPath.AddLines( pointF );
                var point = new Point( Width, Height );
                graphicsPath.AddLine( point, new Point( 0, Height ) );
                graphicsPath.CloseFigure( );

                if( item.IsFilled )
                {
                    graphics.FillPath( _brushes[ item ], graphicsPath );
                }

                _pens[ item ].DashStyle = item.DrawingStyle;
                _pens[ item ].DashOffset = item.DashOffset;
                _pens[ item ].DashCap = DashCap.Round;
                graphics.DrawPath( _pens[ item ], graphicsPath );
                graphicsPath.Dispose( );
            }

            graphics.SmoothingMode = SmoothingMode.HighSpeed;
            graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            if( _ShowPathNames )
            {
                var num5 = Math.Max( checked( Height / 2 - 10 ) / 18, 1 );
                var num6 = 10;
                var x = 0;
                var count2 = checked( Paths.Count - 1 );

                for( var p = 0; p <= count2; p = checked( p + 1 ) )
                {
                    if( p % num5 == 0 )
                    {
                        num6 = checked( x + 10 );
                    }

                    var metroTrackerPaths = Paths[ p ];

                    var rectangle = new Rectangle( num6, checked( 10 + checked( p % num5 * 18 ) ),
                        10, 10 );

                    graphics.FillRectangle( _nameBrushes[ metroTrackerPaths ], rectangle );
                    graphics.DrawRectangle( Pens.Black, rectangle );

                    var rectangle1 =
                        new Rectangle( checked( checked( rectangle.X + rectangle.Width ) + 5 ),
                            checked( rectangle.Y - 3 ), checked( Width - 25 ), 18 );

                    using( var solidBrush = new SolidBrush( ForeColor ) )
                    {
                        stringFormat = new StringFormat( )
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Center
                        };

                        using( var stringFormat1 = stringFormat )
                        {
                            graphics.DrawString( metroTrackerPaths.Name, Font, solidBrush,
                                rectangle1, stringFormat1 );
                        }
                    }

                    var sizeF = graphics.MeasureString( metroTrackerPaths.Name, Font );
                    var width1 = sizeF.Width;

                    if( rectangle1.X + width1 > x )
                    {
                        x = checked( rectangle1.X + checked( (int)Math.Round( width1 ) ) );
                    }
                }
            }

            if( _ShowCustomValue )
            {
                var sizeF1 = graphics.MeasureString( Text, Font );

                var rectangle2 = new Rectangle(
                    checked( (int)Math.Round( checked( Width - 10 ) - sizeF1.Width ) ), 10,
                    checked( (int)Math.Round( sizeF1.Width + 5f ) ),
                    checked( (int)Math.Round( sizeF1.Height ) ) );

                stringFormat = new StringFormat( )
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                using( var stringFormat2 = stringFormat )
                {
                    using( var solidBrush1 = new SolidBrush( ForeColor ) )
                    {
                        graphics.DrawString( Text, Font, solidBrush1, rectangle2, stringFormat2 );
                    }
                }
            }

            using( var pen1 = new Pen( _BorderColor ) )
            {
                var rectangle3 = new Rectangle( 0, 0, checked( Width - 1 ), checked( Height - 1 ) );
                graphics.DrawRectangle( pen1, rectangle3 );
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
            var solidBrush = (BudgetTrackerPath)sender;
            var propertyName = e.PropertyName;

            if( Operators.CompareString( propertyName, "LineColor", false ) == 0 )
            {
                _nameBrushes[ solidBrush ].Dispose( );
                _nameBrushes[ solidBrush ] = new SolidBrush( solidBrush.LineColor );
                _pens[ solidBrush ].Dispose( );
                _pens[ solidBrush ] = new Pen( solidBrush.LineColor, solidBrush.LineWidth );
            }
            else if( Operators.CompareString( propertyName, "LineWidth", false ) == 0 )
            {
                _pens[ solidBrush ].Dispose( );
                _pens[ solidBrush ] = new Pen( solidBrush.LineColor, solidBrush.LineWidth );
            }
            else if( Operators.CompareString( propertyName, "FillColor", false ) == 0 )
            {
                _brushes[ solidBrush ].Dispose( );
                _brushes[ solidBrush ] = new SolidBrush( solidBrush.FillColor );
            }

            Invalidate( );
        }

        /// <summary>
        /// Handles the Added event of the Paths control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetTrackerPathCollectionEventArgs"/> instance containing the event data.</param>
        private void Paths_Added( object sender, BudgetTrackerPathCollectionEventArgs e )
        {
            if( e.Item != null )
            {
                var metroTracker = this;
                e.Item.PropertyChanged += metroTracker.Path_PropertyChanged;
                _pens.Add( e.Item, new Pen( e.Item.LineColor, e.Item.LineWidth ) );
                _brushes.Add( e.Item, new SolidBrush( e.Item.FillColor ) );
                _nameBrushes.Add( e.Item, new SolidBrush( e.Item.LineColor ) );
            }
        }

        /// <summary>
        /// Handles the Removing event of the Paths control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BudgetTrackerPathCollectionEventArgs"/> instance containing the event data.</param>
        private void Paths_Removing( object sender, BudgetTrackerPathCollectionEventArgs e )
        {
            if( e.Item != null )
            {
                var metroTracker = this;
                e.Item.PropertyChanged -= metroTracker.Path_PropertyChanged;
                _pens[ e.Item ].Dispose( );
                _pens.Remove( e.Item );
                _brushes[ e.Item ].Dispose( );
                _brushes.Remove( e.Item );
                _nameBrushes[ e.Item ].Dispose( );
                _nameBrushes.Remove( e.Item );
            }
        }
    }
}