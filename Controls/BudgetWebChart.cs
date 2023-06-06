// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetWebChart.cs" company="Terry D. Eppler">
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
//   BudgetWebChart.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering a metro-style web chart.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ DefaultEvent( "Click" ) ]
    [ Description( "A class for creating a web chart." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetWebChart ) ) ]
    [ Designer( typeof( BudgetWebChartDesigner ) ) ]
    public class BudgetWebChart : Control
    {
        #region ENUM

        /// <summary>
        /// Enum CornerShapes
        /// </summary>
        public enum CornerShapes
        {
            /// <summary>
            /// The rectangular
            /// </summary>
            Rectangular,

            /// <summary>
            /// The circular
            /// </summary>
            Circular
        }

        /// <summary>
        /// Enum FillModes
        /// </summary>
        public enum FillModes
        {
            /// <summary>
            /// The solid
            /// </summary>
            Solid,

            /// <summary>
            /// The gradient
            /// </summary>
            Gradient,

            /// <summary>
            /// The multi gradient
            /// </summary>
            MultiGradient,

            /// <summary>
            /// The hatch
            /// </summary>
            Hatch
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// The point collection
        /// </summary>
        private BudgetWebChartPointCollection _PointCollection = new( );

        /// <summary>
        /// The web points
        /// </summary>
        private BudgetWebChartPoint webPoints = new( );

        /// <summary>
        /// The web chart points
        /// </summary>
        private List<BudgetWebChartPoint> webChartPoints = new( );

        /// <summary>
        /// The enumerator
        /// </summary>
        private IEnumerator<BudgetWebChartPoint> enumerator = null;

        /// <summary>
        /// The enumerator1
        /// </summary>
        private IEnumerator<BudgetWebChartPoint> enumerator1 = null;

        /// <summary>
        /// The tool tip
        /// </summary>
        [ AccessedThroughProperty( "ToolTip" ) ]
        private ToolTip toolTip = new( );

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style = Design.Style.Custom;

        /// <summary>
        /// The chart width
        /// </summary>
        private int _ChartWidth = 220;

        /// <summary>
        /// The point size
        /// </summary>
        private int _PointSize = 6;

        /// <summary>
        /// The inner structure stages
        /// </summary>
        private int _InnerStructureStages = 3;

        /// <summary>
        /// The web point width
        /// </summary>
        private int _WebPointWidth = 4;

        /// <summary>
        /// The web border width
        /// </summary>
        private int _WebBorderWidth = 2;

        /// <summary>
        /// The inner structure width
        /// </summary>
        private int _InnerStructureWidth = 2;

        /// <summary>
        /// The full circle
        /// </summary>
        private const int FullCircle = 360;

        /// <summary>
        /// The web border is gradient
        /// </summary>
        private bool _WebBorderIsGradient;

        /// <summary>
        /// The draw web points
        /// </summary>
        private bool _DrawWebPoints = true;

        //private bool _DrawDesignModeControl;

        /// <summary>
        /// The draw inner structure
        /// </summary>
        private bool _DrawInnerStructure = true;

        /// <summary>
        /// The show tool tip
        /// </summary>
        private bool _ShowToolTip;

        /// <summary>
        /// The fill color
        /// </summary>
        private Color _FillColor;

        /// <summary>
        /// The fill second color
        /// </summary>
        private Color _FillSecondColor;

        /// <summary>
        /// The web border color
        /// </summary>
        private Color _WebBorderColor;

        /// <summary>
        /// The outer structure border
        /// </summary>
        private Color _OuterStructureBorder;

        /// <summary>
        /// The design mode color
        /// </summary>
        private Color _DesignModeColor;

        /// <summary>
        /// The corner border color
        /// </summary>
        private Color _CornerBorderColor;

        /// <summary>
        /// The corner fill color
        /// </summary>
        private Color _CornerFillColor;

        /// <summary>
        /// The inner structure color
        /// </summary>
        private Color _InnerStructureColor;

        /// <summary>
        /// The hatch style
        /// </summary>
        private HatchStyle _HatchStyle;

        /// <summary>
        /// The fill mode
        /// </summary>
        private FillModes _FillMode;

        /// <summary>
        /// The corner shape
        /// </summary>
        private CornerShapes _CornerShape = CornerShapes.Circular;

        /// <summary>
        /// The outer points
        /// </summary>
        private List<Rectangle> _OuterPoints = new( );

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        /// <summary>
        /// The mouse state
        /// </summary>
        private Helpers.MouseState _MouseState;

        /// <summary>
        /// The hot index
        /// </summary>
        private int _HotIndex = -1;

        /// <summary>
        /// The hot rectangle
        /// </summary>
        private Rectangle _HotRectangle;

        /// <summary>
        /// The bezier curve
        /// </summary>
        private bool bezierCurve;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the tool tip.
        /// </summary>
        /// <value>The tool tip.</value>
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Content ) ]
        public virtual ToolTip ToolTip
        {
            get
            {
                return toolTip;
            }

            set
            {
                toolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable/disable automatic style.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to enable/disable automatic style." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
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
        /// Gets or sets the width of the chart.
        /// </summary>
        /// <value>The width of the chart.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 220 ) ]
        [ Description( "Sets the width of the chart." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public int ChartWidth
        {
            get
            {
                return _ChartWidth;
            }
            set
            {
                if( _ChartWidth != value
                   && value > 10 )
                {
                    _ChartWidth = value;
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
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color of the border's corner.
        /// </summary>
        /// <value>The color of the border's corner.</value>
        [ Category( "Appearance" ) ]
        [ Description( "The color of the border's corner." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public Color CornerBorderColor
        {
            get
            {
                return _CornerBorderColor;
            }
            set
            {
                if( _CornerBorderColor != value )
                {
                    _CornerBorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the corner fill.
        /// </summary>
        /// <value>The color of the corner fill.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the corner fill." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public Color CornerFillColor
        {
            get
            {
                return _CornerFillColor;
            }
            set
            {
                if( _CornerFillColor != value )
                {
                    _CornerFillColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the corner shape.
        /// </summary>
        /// <value>The corner shape.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 1 ) ]
        [ Description( "The corner shape." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public CornerShapes CornerShape
        {
            get
            {
                return _CornerShape;
            }
            set
            {
                if( _CornerShape != value )
                {
                    _CornerShape = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of control when in design mode.
        /// </summary>
        /// <value>The color of the design mode.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of control when in design mode." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public Color DesignModeColor
        {
            get
            {
                return _DesignModeColor;
            }
            set
            {
                if( _DesignModeColor != value )
                {
                    _DesignModeColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw inner structure.
        /// </summary>
        /// <value><c>true</c> if draw inner structure; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw inner structure." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public bool DrawInnerStructure
        {
            get
            {
                return _DrawInnerStructure;
            }
            set
            {
                if( _DrawInnerStructure != value )
                {
                    _DrawInnerStructure = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw web points.
        /// </summary>
        /// <value><c>true</c> if draw web points; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw web points." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public bool DrawWebPoints
        {
            get
            {
                return _DrawWebPoints;
            }
            set
            {
                if( _DrawWebPoints != value )
                {
                    _DrawWebPoints = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the fill." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public Color FillColor
        {
            get
            {
                return _FillColor;
            }
            set
            {
                if( _FillColor != value )
                {
                    _FillColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        /// <value>The fill mode.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the fill mode." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public FillModes FillMode
        {
            get
            {
                return _FillMode;
            }
            set
            {
                if( _FillMode != value )
                {
                    _FillMode = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the second fill.
        /// </summary>
        /// <value>The color of the fill second.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the second fill." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public Color FillSecondColor
        {
            get
            {
                return _FillSecondColor;
            }
            set
            {
                if( _FillSecondColor != value )
                {
                    _FillSecondColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the hatch style.
        /// </summary>
        /// <value>The hatch style.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 3 ) ]
        [ Description( "Sets the hatch style." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public HatchStyle HatchStyle
        {
            get
            {
                return _HatchStyle;
            }
            set
            {
                if( _HatchStyle != value )
                {
                    _HatchStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the inner structure.
        /// </summary>
        /// <value>The color of the inner structure.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the inner structure." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public Color InnerStructureColor
        {
            get
            {
                return _InnerStructureColor;
            }
            set
            {
                if( _InnerStructureColor != value )
                {
                    _InnerStructureColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the inner structure stages.
        /// </summary>
        /// <value>The inner structure stages.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 3 ) ]
        [ Description( "The inner structure stages." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public int InnerStructureStages
        {
            get
            {
                return _InnerStructureStages;
            }
            set
            {
                if( _InnerStructureStages != value )
                {
                    _InnerStructureStages = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the inner structure.
        /// </summary>
        /// <value>The width of the inner structure.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "The width of the inner structure." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public int InnerStructureWidth
        {
            get
            {
                return _InnerStructureWidth;
            }
            set
            {
                if( _InnerStructureWidth != value )
                {
                    _InnerStructureWidth = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the outer structure border.
        /// </summary>
        /// <value>The outer structure border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "The outer structure border." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public Color OuterStructureBorder
        {
            get
            {
                return _OuterStructureBorder;
            }
            set
            {
                if( _OuterStructureBorder != value )
                {
                    _OuterStructureBorder = value;
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
        [ Description( "The points." ) ]
        [ TypeConverter( typeof( ExpandableObjectConverter ) ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Content ) ]
        public BudgetWebChartPointCollection Points
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
        /// Gets or sets the web points.
        /// </summary>
        /// <value>The web points.</value>
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        [ TypeConverter( typeof( ExpandableObjectConverter ) ) ]
        public BudgetWebChartPoint WebPoints
        {
            get { return webPoints; }
            set
            {
                webPoints = value;
                Invalidate( );
            }
        }

        /// <summary>
        /// Gets or sets the size of the point.
        /// </summary>
        /// <value>The size of the point.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 6 ) ]
        [ Description( "The size of the point." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public int PointSize
        {
            get
            {
                return _PointSize;
            }
            set
            {
                if( _PointSize != value
                   && value >= 2 )
                {
                    _PointSize = value;
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
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show tool tip.
        /// </summary>
        /// <value><c>true</c> if show tool tip; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to show tool tip." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public bool ShowToolTip
        {
            get
            {
                return _ShowToolTip;
            }
            set
            {
                if( _ShowToolTip != value )
                {
                    _ShowToolTip = value;
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
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
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
                            _FillColor = Color.FromArgb( 50, Design.BudgetColors.AccentBlue );

                            _FillSecondColor =
                                Design.BudgetColors.ChangeColorBrightness(
                                    Design.BudgetColors.AccentBlue, 0.3f );

                            _WebBorderColor = Color.FromArgb( 100,
                                Design.BudgetColors.ChangeColorBrightness(
                                    Design.BudgetColors.AccentBlue, -0.2f ) );

                            _OuterStructureBorder = Design.BudgetColors.LightBorder;
                            _DesignModeColor = Design.BudgetColors.LightBorder;
                            _CornerBorderColor = Design.BudgetColors.LightBorder;

                            _CornerFillColor =
                                Design.BudgetColors.ChangeColorBrightness(
                                    Design.BudgetColors.LightBorder, 0.2f );

                            _InnerStructureColor =
                                Color.FromArgb( 100, Design.BudgetColors.LightBorder );

                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _FillColor = Color.FromArgb( 50, Design.BudgetColors.AccentBlue );

                            _FillSecondColor =
                                Design.BudgetColors.ChangeColorBrightness(
                                    Design.BudgetColors.AccentBlue, 0.3f );

                            _WebBorderColor = Color.FromArgb( 100,
                                Design.BudgetColors.ChangeColorBrightness(
                                    Design.BudgetColors.AccentBlue, -0.2f ) );

                            _OuterStructureBorder = Design.BudgetColors.LightBorder;
                            _DesignModeColor = Design.BudgetColors.LightBorder;
                            _CornerBorderColor = Design.BudgetColors.LightBorder;

                            _CornerFillColor =
                                Design.BudgetColors.ChangeColorBrightness(
                                    Design.BudgetColors.LightBorder, 0.2f );

                            _InnerStructureColor =
                                Color.FromArgb( 100, Design.BudgetColors.LightBorder );

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
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color of the web border.
        /// </summary>
        /// <value>The color of the web border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the web border." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public Color WebBorderColor
        {
            get
            {
                return _WebBorderColor;
            }
            set
            {
                if( _WebBorderColor != value )
                {
                    _WebBorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether web border is gradient.
        /// </summary>
        /// <value><c>true</c> if web border is gradient; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether web border is gradient." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public bool WebBorderIsGradient
        {
            get
            {
                return _WebBorderIsGradient;
            }
            set
            {
                if( _WebBorderIsGradient != value )
                {
                    _WebBorderIsGradient = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the web border.
        /// </summary>
        /// <value>The width of the web border.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "Sets the width of the web border." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public int WebBorderWidth
        {
            get
            {
                return _WebBorderWidth;
            }
            set
            {
                if( _WebBorderWidth != value )
                {
                    _WebBorderWidth = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the web point.
        /// </summary>
        /// <value>The width of the web point.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 4 ) ]
        [ Description( "Sets the width of the web point." ) ]
        [ DesignerSerializationVisibility( DesignerSerializationVisibility.Visible ) ]
        public int WebPointWidth
        {
            get
            {
                return _WebPointWidth;
            }
            set
            {
                if( _WebPointWidth != value )
                {
                    _WebPointWidth = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [bezier curve].
        /// </summary>
        /// <value><c>true</c> if [bezier curve]; otherwise, <c>false</c>.</value>
        public bool BezierCurve
        {
            get { return bezierCurve; }
            set
            {
                bezierCurve = value;
                Invalidate( );
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetWebChart" /> class.
        /// </summary>
        public BudgetWebChart( )
        {
            //this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            SetStyle(
                ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.UserPaint
                | ControlStyles.DoubleBuffer
                | ControlStyles.SupportsTransparentBackColor, true );

            _FillColor = Color.FromArgb( 50, Design.BudgetColors.AccentBlue );

            _FillSecondColor =
                Design.BudgetColors.ChangeColorBrightness( Design.BudgetColors.AccentBlue, 0.3f );

            _WebBorderColor = Color.FromArgb( 100,
                Design.BudgetColors.ChangeColorBrightness( Design.BudgetColors.AccentBlue,
                    -0.2f ) );

            _OuterStructureBorder = Design.BudgetColors.LightBorder;
            _DesignModeColor = Design.BudgetColors.LightBorder;
            _CornerBorderColor = Design.BudgetColors.LightBorder;

            _CornerFillColor =
                Design.BudgetColors.ChangeColorBrightness( Design.BudgetColors.LightBorder, 0.2f );

            _InnerStructureColor = Color.FromArgb( 100, Design.BudgetColors.LightBorder );
            _HatchStyle = HatchStyle.BackwardDiagonal;
            _FillMode = FillModes.Gradient;
            _AutoStyle = true;
            _MouseState = Helpers.MouseState.None;
            Font = new Font( "Segoe UI", 9f );
            Size = new Size( 250, 250 );
            UpdateStyles( );
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Draws the design control.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="r">The r.</param>
        protected void DrawDesignControl( Graphics g, Rectangle r )
        {
            using( var pen = new Pen( _DesignModeColor ) )
            {
                g.DrawEllipse( pen, r );

                using( var solidBrush = new SolidBrush( _DesignModeColor ) )
                {
                    var stringFormat = new StringFormat( )
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    using( var stringFormat1 = stringFormat )
                    {
                        g.DrawString(
                            "Points will appear on circle, once data added!\r\n(Will disappear once loaded)",
                            new Font( Font.FontFamily, 6f ), solidBrush, r, stringFormat1 );
                    }
                }
            }
        }

        /// <summary>
        /// Draws the inner structure grid.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <param name="outerPoints">The outer points.</param>
        /// <param name="stageCount">The stage count.</param>
        private void DrawInnerStructureGrid(
            Graphics g, Point centerPoint, Point[ ] outerPoints, int stageCount )
        {
            var num = 0;
            var num1 = stageCount;
            var num2 = 1;

            while( num2 <= num1 )
            {
                if( num2 != stageCount )
                {
                    num = checked( num + checked( (int)Math.Round( 100 / (double)stageCount ) ) );
                    var points = new List<Point>( );

                    using( var pen = new Pen( _InnerStructureColor, _InnerStructureWidth ) )
                    {
                        var length = checked( outerPoints.Length - 1 );

                        for( var i = 0; i <= length; i = checked( i + 1 ) )
                        {
                            points.Add( GetPointOnLine( centerPoint, outerPoints[ i ], num ) );
                            g.DrawLine( pen, centerPoint, outerPoints[ i ] );
                        }

                        if( !BezierCurve )
                        {
                            g.DrawPolygon( pen, points.ToArray( ) );
                        }
                        else
                        {
                            g.DrawClosedCurve( pen, points.ToArray( ) );
                        }
                    }

                    num2 = checked( num2 + 1 );
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the point on line.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="perc">The perc.</param>
        /// <returns>Point.</returns>
        private Point GetPointOnLine( Point p1, Point p2, int perc )
        {
            var x = (float)( checked( p2.X - p1.X ) * ( (double)perc / 100 ) + p1.X );
            var y = (float)( checked( p2.Y - p1.Y ) * ( (double)perc / 100 ) + p1.Y );

            var point = new Point( checked( (int)Math.Round( x ) ),
                checked( (int)Math.Round( y ) ) );

            return point;
        }

        /// <summary>
        /// Draws the web.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="item">The item.</param>
        /// <param name="point1">The point1.</param>
        /// <param name="points">The points.</param>
        /// <param name="points1">The points1.</param>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="enumerator1">The enumerator1.</param>
        /// <param name="graphics">The graphics.</param>
        /// <param name="linearGradientBrush">The linear gradient brush.</param>
        private void DrawWeb(
            Point point, Point item, Point point1, List<Point> points,
            List<Point> points1, IEnumerator<BudgetWebChartPoint> enumerator,
            IEnumerator<BudgetWebChartPoint> enumerator1, Graphics graphics,
            Brush linearGradientBrush )
        {
            if( Points.Count >= 3 )
            {
                var num = 0;
                enumerator = Points.GetEnumerator( );

                while( enumerator.MoveNext( ) )
                {
                    var current = enumerator.Current;
                    num = checked( num + checked( (int)Math.Round( 360 / (double)Points.Count ) ) );

                    var point2 = new Point(
                        checked( (int)Math.Round( point1.X
                            + checked( point1.X - _PointSize )
                            * Math.Cos( num * 3.14159265358979 / 180 ) ) ),
                        checked( (int)Math.Round( point1.Y
                            + checked( point1.X - _PointSize )
                            * Math.Sin( num * 3.14159265358979 / 180 ) ) ) );

                    points1.Add( point2 );
                    points.Add( GetPointOnLine( point2, point1, checked( 100 - current.Value ) ) );
                }

                if( _DrawInnerStructure )
                {
                    DrawInnerStructureGrid( graphics, point1, points1.ToArray( ),
                        _InnerStructureStages );
                }

                using( var pen = new Pen( _WebBorderColor, _WebBorderWidth ) )
                {
                    if( !_WebBorderIsGradient )
                    {
                        if( !BezierCurve )
                        {
                            graphics.DrawPolygon( pen, points.ToArray( ) );
                        }
                        else
                        {
                            graphics.DrawClosedCurve( pen, points.ToArray( ) );
                        }
                    }
                    else
                    {
                        var count = checked( points.Count - 1 );

                        for( var i = 0; i <= count; i = checked( i + 1 ) )
                        {
                            using( var linearGradientBrush1 = new LinearGradientBrush( points[ i ],
                                      i == checked( points.Count - 1 )
                                          ? points[ 0 ]
                                          : points[ checked( i + 1 ) ], Points[ i ].Color,
                                      ( i == checked( points.Count - 1 )
                                          ? Points[ 0 ]
                                          : Points[ checked( i + 1 ) ] ).Color ) )
                            {
                                using( var pen1 = new Pen( linearGradientBrush1, _WebBorderWidth ) )
                                {
                                    graphics.DrawLine( pen1, points[ i ],
                                        i == checked( points.Count - 1 )
                                            ? points[ 0 ]
                                            : points[ checked( i + 1 ) ] );
                                }
                            }
                        }
                    }

                    pen.Color = _OuterStructureBorder;

                    if( !BezierCurve )
                    {
                        graphics.DrawPolygon( pen, points1.ToArray( ) );
                    }
                    else
                    {
                        graphics.DrawClosedCurve( pen, points1.ToArray( ) );
                    }
                }

                switch( _FillMode )
                {
                    case FillModes.Gradient:
                    {
                        point = new Point( checked( (int)Math.Round( (double)_ChartWidth / 2 ) ),
                            _ChartWidth );

                        item = new Point( checked( (int)Math.Round( (double)_ChartWidth / 2 ) ),
                            0 );

                        linearGradientBrush = new LinearGradientBrush( point, item, _FillColor,
                            _FillSecondColor );

                        break;
                    }
                    case FillModes.MultiGradient:
                    {
                        var colors = new List<Color>( );
                        enumerator1 = Points.GetEnumerator( );

                        while( enumerator1.MoveNext( ) )
                        {
                            colors.Add( enumerator1.Current.Color );
                        }

                        var pathGradientBrush = new PathGradientBrush( points.ToArray( ) )
                        {
                            CenterColor = _FillColor,
                            SurroundColors = colors.ToArray( )
                        };

                        linearGradientBrush = pathGradientBrush;
                        break;
                    }
                    case FillModes.Hatch:
                    {
                        linearGradientBrush =
                            new HatchBrush( _HatchStyle, _FillColor, _FillSecondColor );

                        break;
                    }
                    default:
                    {
                        linearGradientBrush = new SolidBrush( _FillColor );
                        break;
                    }
                }

                if( !BezierCurve )
                {
                    graphics.FillPolygon( linearGradientBrush, points.ToArray( ) );
                }
                else
                {
                    graphics.FillClosedCurve( linearGradientBrush, points.ToArray( ) );
                }

                linearGradientBrush.Dispose( );
                var count1 = checked( points1.Count - 1 );

                for( var j = 0; j <= count1; j = checked( j + 1 ) )
                {
                    item = points1[ j ];

                    var x = checked( item.X
                        - checked( (int)Math.Round( (double)_PointSize / 2 ) ) );

                    point = points1[ j ];

                    var rectangle1 = new Rectangle( x,
                        checked( point.Y - checked( (int)Math.Round( (double)_PointSize / 2 ) ) ),
                        _PointSize, _PointSize );

                    using( var solidBrush = new SolidBrush( _CornerFillColor ) )
                    {
                        using( var pen2 = new Pen( _CornerBorderColor ) )
                        {
                            if( _CornerShape != CornerShapes.Rectangular )
                            {
                                graphics.DrawEllipse( pen2, rectangle1 );
                                graphics.FillEllipse( solidBrush, rectangle1 );
                            }
                            else
                            {
                                graphics.DrawRectangle( pen2, rectangle1 );
                                graphics.FillRectangle( solidBrush, rectangle1 );
                            }
                        }
                    }

                    _OuterPoints.Add( rectangle1 );
                }

                if( _DrawWebPoints )
                {
                    var num1 = checked( points.Count - 1 );

                    for( var k = 0; k <= num1; k = checked( k + 1 ) )
                    {
                        using( var solidBrush1 = new SolidBrush( Points[ k ].Color ) )
                        {
                            item = points[ k ];

                            var x1 = checked( item.X
                                - checked( (int)Math.Round( (double)_WebPointWidth / 2 ) ) );

                            point = points[ k ];

                            graphics.FillEllipse( solidBrush1, x1,
                                checked( point.Y
                                    - checked( (int)Math.Round( (double)_WebPointWidth / 2 ) ) ),
                                _WebPointWidth, _WebPointWidth );
                        }
                    }
                }
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            _ChartWidth = Width / 2 - 5 + ( Height / 2 - 5 );
            var point = new Point( );
            var item = new Point( );

            var linbrush = new LinearGradientBrush( new Point( 0, 0 ), new Point( 10, 10 ),
                Color.AliceBlue, Color.Red );

            var points = new List<Point>( );
            var points1 = new List<Point>( );

            var point1 =
                new Point(
                    checked( _PointSize + checked( (int)Math.Round( (double)_ChartWidth / 2 ) ) ),
                    checked( _PointSize + checked( (int)Math.Round( (double)_ChartWidth / 2 ) ) ) );

            var rectangle = new Rectangle( _PointSize, _PointSize, _ChartWidth, _ChartWidth );
            points1.Clear( );
            _OuterPoints.Clear( );

            if( Points.Count < 3 )
            {
                DrawDesignControl( graphics, rectangle );
            }

            DrawWeb( point, item, point1, points, points1,
                enumerator, enumerator1, graphics, linbrush );

            base.OnPaint( e );
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
                    var metroForm = (BudgetForm)FindForm( );
                    Style = metroForm.Style;
                    _Style = metroForm.Style;
                    Invalidate( );
                }
            }

            base.OnBackColorChanged( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave( EventArgs e )
        {
            _HotIndex = -1;
            _HotRectangle = new Rectangle( );
            base.OnMouseLeave( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove( MouseEventArgs e )
        {
            var rectangle = new Rectangle( );

            if( _ShowToolTip )
            {
                if( _HotRectangle == rectangle )
                {
                    var count = checked( _OuterPoints.Count - 1 );

                    for( var i = 0; i <= count; i = checked( i + 1 ) )
                    {
                        if( _OuterPoints[ i ].Contains( e.Location ) )
                        {
                            if( _HotIndex != i )
                            {
                                if( _HotRectangle == rectangle )
                                {
                                    _HotIndex = i;
                                    _HotRectangle = _OuterPoints[ i ];
                                    ToolTip.Show( Points[ i ].Text, this );
                                }
                            }
                        }
                    }
                }
                else if( !_HotRectangle.Contains( e.Location ) )
                {
                    _HotIndex = -1;
                    _HotRectangle = new Rectangle( );
                    ToolTip.Hide( this );
                }
            }

            base.OnMouseMove( e );
        }

        #endregion
    }
}