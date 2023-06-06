// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetGraph.cs" company="Terry D. Eppler">
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
//   BudgetGraph.cs
// </summary>
// ******************************************************************************************

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
    /// A class collection for rendering metro graph.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ Description( "A class for creating a metro graph." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( BudgetGraph ), "BudgetGraph.bmp" ) ]
    [ Designer( typeof( BudgetGraphDesigner ) ) ]
    public class BudgetGraph : Control
    {
        #region Private Fields

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The grid color
        /// </summary>
        private Color _GridColor;

        /// <summary>
        /// The single line color
        /// </summary>
        private Color _SingleLineColor;

        /// <summary>
        /// The classic line color
        /// </summary>
        private Color _ClassicLineColor;

        /// <summary>
        /// The classic fill color
        /// </summary>
        private Color _ClassicFillColor;

        /// <summary>
        /// The gradient color
        /// </summary>
        private Color _GradientColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The hover box color
        /// </summary>
        private Color _HoverBoxColor;

        /// <summary>
        /// The hover box border color
        /// </summary>
        private Color _HoverBoxBorderColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The draw hover line
        /// </summary>
        private bool _DrawHoverLine;

        /// <summary>
        /// The draw hover data
        /// </summary>
        private bool _DrawHoverData;

        /// <summary>
        /// The classic line thickness
        /// </summary>
        private int _ClassicLineThickness;

        /// <summary>
        /// The use gradient
        /// </summary>
        private bool _UseGradient;

        /// <summary>
        /// The dash style
        /// </summary>
        private DashStyle _DashStyle;

        /// <summary>
        /// The draw vertical lines
        /// </summary>
        private bool _DrawVerticalLines;

        /// <summary>
        /// The draw horizontal lines
        /// </summary>
        private bool _DrawHorizontalLines;

        /// <summary>
        /// The single line
        /// </summary>
        private bool _SingleLine;

        /// <summary>
        /// The single line thickness
        /// </summary>
        private int _SingleLineThickness;

        /// <summary>
        /// The single line shadow
        /// </summary>
        private bool _SingleLineShadow;

        /// <summary>
        /// The side padding
        /// </summary>
        private bool _SidePadding;

        /// <summary>
        /// The override minimum
        /// </summary>
        private bool _OverrideMinimum;

        /// <summary>
        /// The override maximum
        /// </summary>
        private bool _OverrideMaximum;

        /// <summary>
        /// The overridden minimum
        /// </summary>
        private int _OverriddenMinimum;

        /// <summary>
        /// The overridden maximum
        /// </summary>
        private int _OverriddenMaximum;

        /// <summary>
        /// The values
        /// </summary>
        private List<float> _Values;

        /// <summary>
        /// The smooth values
        /// </summary>
        private List<float> _SmoothValues;

        /// <summary>
        /// The current value
        /// </summary>
        private float _CurrentValue;

        /// <summary>
        /// The minimum
        /// </summary>
        private float _Minimum;

        /// <summary>
        /// The maximum
        /// </summary>
        private float _Maximum;

        /// <summary>
        /// The index
        /// </summary>
        private int _Index;

        /// <summary>
        /// The gradient point a
        /// </summary>
        private Point _GradientPointA;

        /// <summary>
        /// The gradient point b
        /// </summary>
        private Point _GradientPointB;

        /// <summary>
        /// The f b1
        /// </summary>
        private float FB1;

        /// <summary>
        /// The f b2
        /// </summary>
        private float FB2;

        /// <summary>
        /// The r1
        /// </summary>
        private Rectangle R1;

        /// <summary>
        /// The r2
        /// </summary>
        private Rectangle R2;

        /// <summary>
        /// The r3
        /// </summary>
        private Rectangle R3;

        /// <summary>
        /// The sw
        /// </summary>
        private int SW;

        /// <summary>
        /// The sh
        /// </summary>
        private int SH;

        /// <summary>
        /// The SHH
        /// </summary>
        private int SHH;

        /// <summary>
        /// The last move
        /// </summary>
        private DateTime LastMove;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        /// <summary>
        /// The outer border
        /// </summary>
        private bool outerBorder = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to draw the outer border.
        /// </summary>
        /// <value><c>true</c> if outer border; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Draws the outer border." ) ]
        public bool OuterBorder
        {
            get { return outerBorder; }
            set
            {
                outerBorder = value;
                Invalidate( );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to automatically style the control.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to automatically style the control." ) ]
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
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>The color of the back.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new Color BackColor
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
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
        /// Gets or sets the color of the classic fill.
        /// </summary>
        /// <value>The color of the classic fill.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the classic fill." ) ]
        public Color ClassicFillColor
        {
            get
            {
                return _ClassicFillColor;
            }
            set
            {
                if( value != _ClassicFillColor )
                {
                    _ClassicFillColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the classic line.
        /// </summary>
        /// <value>The color of the classic line.</value>
        [ Category( "Appearance" ) ]
        [ Description( "." ) ]
        public Color ClassicLineColor
        {
            get
            {
                return _ClassicLineColor;
            }
            set
            {
                if( value != _ClassicLineColor )
                {
                    _ClassicLineColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the classic line thickness.
        /// </summary>
        /// <value>The classic line thickness.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "Sets the classic line thickness." ) ]
        public int ClassicLineThickness
        {
            get
            {
                return _ClassicLineThickness;
            }
            set
            {
                if( value != _ClassicLineThickness )
                {
                    _ClassicLineThickness = value;
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
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The dash style.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the dash style." ) ]
        public DashStyle DashStyle
        {
            get
            {
                return _DashStyle;
            }
            set
            {
                if( value != _DashStyle )
                {
                    _DashStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the default color.
        /// </summary>
        /// <value>The default color.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the default color." ) ]
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
        /// Gets or sets a value indicating whether to draw horizontal lines.
        /// </summary>
        /// <value><c>true</c> if draw horizontal lines; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw horizontal lines." ) ]
        public bool DrawHorizontalLines
        {
            get
            {
                return _DrawHorizontalLines;
            }
            set
            {
                if( value != _DrawHorizontalLines )
                {
                    _DrawHorizontalLines = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw hover data.
        /// </summary>
        /// <value><c>true</c> if [draw hover data]; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw hover data." ) ]
        public bool DrawHoverData
        {
            get
            {
                return _DrawHoverData;
            }
            set
            {
                if( value != _DrawHoverData )
                {
                    _DrawHoverData = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw hover line.
        /// </summary>
        /// <value><c>true</c> if draw hover line; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to draw a hover line." ) ]
        public bool DrawHoverLine
        {
            get
            {
                return _DrawHoverLine;
            }
            set
            {
                if( value != _DrawHoverLine )
                {
                    _DrawHoverLine = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw vertical lines.
        /// </summary>
        /// <value><c>true</c> if draw vertical lines; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to draw vertical lines." ) ]
        public bool DrawVerticalLines
        {
            get
            {
                return _DrawVerticalLines;
            }
            set
            {
                if( value != _DrawVerticalLines )
                {
                    _DrawVerticalLines = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the gradient.
        /// </summary>
        /// <value>The color of the gradient.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the gradient." ) ]
        public Color GradientColor
        {
            get
            {
                return _GradientColor;
            }
            set
            {
                if( value != _GradientColor )
                {
                    _GradientColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the gradient point.
        /// </summary>
        /// <value>The gradient point a.</value>
        [ Browsable( false ) ]
        [ Category( "Appereance" ) ]
        [ Description( "Sets the gradient point." ) ]
        public Point GradientPointA
        {
            get
            {
                return _GradientPointA;
            }
            set
            {
                if( _GradientPointA != value )
                {
                    _GradientPointA = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the gradient point.
        /// </summary>
        /// <value>The gradient point b.</value>
        [ Browsable( false ) ]
        [ Category( "Appereance" ) ]
        [ Description( "Sets the gradient point." ) ]
        public Point GradientPointB
        {
            get
            {
                return _GradientPointB;
            }
            set
            {
                if( _GradientPointB != value )
                {
                    _GradientPointB = value;
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
        /// Gets or sets the color of the hover box border.
        /// </summary>
        /// <value>The color of the hover box border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the hover box border." ) ]
        public Color HoverBoxBorderColor
        {
            get
            {
                return _HoverBoxBorderColor;
            }
            set
            {
                if( value != _HoverBoxBorderColor )
                {
                    _HoverBoxBorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover box.
        /// </summary>
        /// <value>The color of the hover box.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the hover box." ) ]
        public Color HoverBoxColor
        {
            get
            {
                return _HoverBoxColor;
            }
            set
            {
                if( value != _HoverBoxColor )
                {
                    _HoverBoxColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the hover." ) ]
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
        /// Gets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [ Category( "Data" ) ]
        [ Description( "Der Maximumwert des Steuerelements." ) ]
        public float Maximum
        {
            get
            {
                return _Maximum;
            }
        }

        /// <summary>
        /// Gets the Minimum.
        /// </summary>
        /// <value>The minimum.</value>
        [ Category( "Data" ) ]
        [ Description( "Gets the Minimum." ) ]
        public float Minimum
        {
            get
            {
                return _Minimum;
            }
        }

        /// <summary>
        /// Gets or sets the overridden maximum.
        /// </summary>
        /// <value>The overridden maximum.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 100 ) ]
        [ Description( "Sets the overridden maximum." ) ]
        public int OverriddenMaximum
        {
            get
            {
                return _OverriddenMaximum;
            }
            set
            {
                if( value != _OverriddenMaximum )
                {
                    _OverriddenMaximum = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the overridden minimum.
        /// </summary>
        /// <value>The overridden minimum.</value>
        [ Category( "Data" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the overridden minimum." ) ]
        public int OverriddenMinimum
        {
            get
            {
                return _OverriddenMinimum;
            }
            set
            {
                if( value != _OverriddenMinimum )
                {
                    _OverriddenMinimum = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to override the maximum.
        /// </summary>
        /// <value><c>true</c> if override maximum; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to override the maximum." ) ]
        public bool OverrideMaximum
        {
            get
            {
                return _OverrideMaximum;
            }
            set
            {
                if( value != _OverrideMaximum )
                {
                    _OverrideMaximum = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to override minimum.
        /// </summary>
        /// <value><c>true</c> if override minimum; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to override minimum." ) ]
        public bool OverrideMinimum
        {
            get
            {
                return _OverrideMinimum;
            }
            set
            {
                if( value != _OverrideMinimum )
                {
                    _OverrideMinimum = value;
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
        /// Gets or sets a value indicating whether to enable side padding.
        /// </summary>
        /// <value><c>true</c> if side padding; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to enable side padding." ) ]
        public bool SidePadding
        {
            get
            {
                return _SidePadding;
            }
            set
            {
                if( value != _SidePadding )
                {
                    _SidePadding = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable single line.
        /// </summary>
        /// <value><c>true</c> if single line; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to enable single line." ) ]
        public bool SingleLine
        {
            get
            {
                return _SingleLine;
            }
            set
            {
                if( value != _SingleLine )
                {
                    _SingleLine = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the single line.
        /// </summary>
        /// <value>The color of the single line.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the single line." ) ]
        public Color SingleLineColor
        {
            get
            {
                return _SingleLineColor;
            }
            set
            {
                if( value != _SingleLineColor )
                {
                    _SingleLineColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable single line shadow.
        /// </summary>
        /// <value><c>true</c> if [single line shadow]; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to enable single line shadow." ) ]
        public bool SingleLineShadow
        {
            get
            {
                return _SingleLineShadow;
            }
            set
            {
                if( value != _SingleLineShadow )
                {
                    _SingleLineShadow = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the single line thickness.
        /// </summary>
        /// <value>The single line thickness.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 5 ) ]
        [ Description( "Sets the single line thickness." ) ]
        public int SingleLineThickness
        {
            get
            {
                return _SingleLineThickness;
            }
            set
            {
                if( value != _SingleLineThickness )
                {
                    _SingleLineThickness = value;
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
                            _DefaultColor = Design.BudgetColors.LightDefault;
                            _GridColor = Design.BudgetColors.PopUpBorder;
                            _SingleLineColor = Design.BudgetColors.AccentBlue;
                            _ClassicLineColor = Design.BudgetColors.AccentBlue;

                            _ClassicFillColor =
                                Color.FromArgb( 50, Design.BudgetColors.AccentBlue );

                            _GradientColor = Color.FromArgb( 50,
                                Design.BudgetColors.ChangeColorBrightness(
                                    Design.BudgetColors.AccentBlue, -0.2f ) );

                            _HoverColor = Design.BudgetColors.AccentLightBlue;
                            _HoverBoxColor = Design.BudgetColors.LightDefault;
                            _HoverBoxBorderColor = Design.BudgetColors.PopUpBorder;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _DefaultColor = Design.BudgetColors.DarkDefault;
                            _GridColor = Design.BudgetColors.LightBorder;
                            _SingleLineColor = Design.BudgetColors.AccentBlue;
                            _ClassicLineColor = Design.BudgetColors.AccentBlue;

                            _ClassicFillColor =
                                Color.FromArgb( 50, Design.BudgetColors.AccentBlue );

                            _GradientColor = Color.FromArgb( 50,
                                Design.BudgetColors.ChangeColorBrightness(
                                    Design.BudgetColors.AccentBlue, -0.2f ) );

                            _HoverColor = Design.BudgetColors.AccentLightBlue;
                            _HoverBoxColor = Design.BudgetColors.DarkDefault;
                            _HoverBoxBorderColor = Design.BudgetColors.LightBorder;
                            _BorderColor = Design.BudgetColors.LightBorder;
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
        /// Gets or sets a value indicating whether to use gradient.
        /// </summary>
        /// <value><c>true</c> if use gradient; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to use gradient." ) ]
        public bool UseGradient
        {
            get
            {
                return _UseGradient;
            }
            set
            {
                if( value != _UseGradient )
                {
                    _UseGradient = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        [ Browsable( true ) ]
        [ Category( "Data" ) ]
        [ Description( "Sets the values." ) ]
        public float[ ] Values
        {
            get
            {
                return _Values.ToArray( );
            }
            set
            {
                Clear( );
                AddValues( value );
                FindMinMax( );
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetGraph" /> class.
        /// </summary>
        public BudgetGraph( )
        {
            _Style = Design.Style.Light;
            _DefaultColor = Design.BudgetColors.LightDefault;
            _GridColor = Design.BudgetColors.PopUpBorder;
            _SingleLineColor = Design.BudgetColors.AccentBlue;
            _ClassicLineColor = Design.BudgetColors.AccentBlue;
            _ClassicFillColor = Color.FromArgb( 50, Design.BudgetColors.AccentBlue );

            _GradientColor = Color.FromArgb( 50,
                Design.BudgetColors.ChangeColorBrightness( Design.BudgetColors.AccentBlue,
                    -0.2f ) );

            _HoverColor = Design.BudgetColors.AccentLightBlue;
            _HoverBoxColor = Design.BudgetColors.LightDefault;
            _HoverBoxBorderColor = Design.BudgetColors.PopUpBorder;
            _BorderColor = Design.BudgetColors.LightBorder;
            _DrawHoverLine = true;
            _DrawHoverData = true;
            _ClassicLineThickness = 2;
            _UseGradient = true;
            _DashStyle = DashStyle.Solid;
            _DrawVerticalLines = false;
            _DrawHorizontalLines = true;
            _SingleLine = false;
            _SingleLineThickness = 5;
            _SingleLineShadow = true;
            _SidePadding = false;
            _OverrideMinimum = false;
            _OverrideMaximum = false;
            _OverriddenMinimum = 0;
            _OverriddenMaximum = 100;
            _Minimum = float.MinValue;
            _Maximum = float.MaxValue;
            _Index = -1;
            _GradientPointA = new Point( checked( (int)Math.Round( (double)Width / 2 ) ), 0 );
            _GradientPointB = new Point( checked( (int)Math.Round( (double)Width / 2 ) ), Height );
            _AutoStyle = true;
            Font = new Font( "Roboto", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            _Values = new List<float>( );
            _SmoothValues = new List<float>( );
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddValue( float value )
        {
            _Index = -1;
            _Values.Add( value );
            CleanValues( );
            FindMinMax( );
            Invalidate( );
        }

        /// <summary>
        /// Adds the values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void AddValues( float[ ] values )
        {
            _Index = -1;
            _Values.AddRange( values );
            CleanValues( );
            FindMinMax( );
            Invalidate( );
        }

        /// <summary>
        /// Cleans the values.
        /// </summary>
        private void CleanValues( )
        {
            if( _Values.Count > Width )
            {
                _Values.RemoveRange( 0, checked( _Values.Count - Width ) );
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear( )
        {
            _Values.Clear( );
            _SmoothValues.Clear( );
            _Maximum = float.MinValue;
            _Minimum = float.MaxValue;
            InvalidateMinMax( );
        }

        /// <summary>
        /// Finds the minimum maximum.
        /// </summary>
        private void FindMinMax( )
        {
            _Maximum = float.MinValue;
            _Minimum = float.MaxValue;
            var count = checked( _Values.Count - 1 );

            for( var i = 0; i <= count; i = checked( i + 1 ) )
            {
                _CurrentValue = _Values[ i ];

                if( _CurrentValue > _Maximum )
                {
                    _Maximum = _CurrentValue;
                }

                if( _CurrentValue < _Minimum )
                {
                    _Minimum = _CurrentValue;
                }
            }

            InvalidateMinMax( );
        }

        /// <summary>
        /// Invalidates the minimum maximum.
        /// </summary>
        private void InvalidateMinMax( )
        {
            if( _OverrideMinimum )
            {
                _Minimum = _OverriddenMinimum;
            }

            if( _OverrideMaximum )
            {
                _Maximum = _OverriddenMaximum;
            }
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
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave( EventArgs e )
        {
            if( _DrawHoverData )
            {
                _Index = -1;
                Invalidate( );
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove( MouseEventArgs e )
        {
            if( _DrawHoverData )
            {
                R1 = new Rectangle( SW, 0, checked( Width - SW ), checked( Height - SH ) );

                R2 = new Rectangle( checked( R1.X + 8 ), checked( R1.Y + 8 ),
                    checked( R1.Width - 16 ), checked( R1.Height - 16 ) );

                FB1 = (float)( checked( R2.Width - 1 ) / (double)checked( _Values.Count - 1 ) );

                if( !R1.Contains( e.Location ) )
                {
                    _Index = -1;
                }
                else
                {
                    _Index = checked( (int)Math.Round( checked( e.X - R2.X ) / FB1 ) );

                    if( _Index >= _Values.Count )
                    {
                        _Index = -1;
                    }
                }

                if( DateTime.Compare( DateTime.Now, LastMove.AddMilliseconds( 33 ) ) > 0 )
                {
                    LastMove = DateTime.Now;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint( PaintEventArgs e )
        {
            Point point;
            var graphics = e.Graphics;
            var graphicsPath = new GraphicsPath( );
            R1 = new Rectangle( SW, 0, checked( Width - SW ), Height );

            R2 = new Rectangle( checked( R1.X + 10 ), checked( R1.Y + 10 ),
                checked( R1.Width - 20 ), checked( R1.Height - 20 ) );

            if( !_SidePadding )
            {
                R2.X = R1.X;
                R2.Width = R1.Width;
            }

            using( var solidBrush = new SolidBrush( _DefaultColor ) )
            {
                graphics.FillRectangle( solidBrush, R1 );
            }

            var pen = new Pen( _GridColor );
            var num = 0;

            do
            {
                FB1 = R2.Y + (float)( checked( R2.Height - 1 ) * ( num * 0.1 ) );

                if( _DrawHorizontalLines )
                {
                    graphics.DrawLine( pen, R1.X, FB1, checked( R1.Right - 1 ), FB1 );
                }

                num = checked( num + 1 );
            }
            while( num <= 10 );

            if( _Values.Count > 1 )
            {
                var pointF = new PointF[ checked( checked( _Values.Count + 1 ) + 1 ) ];
                FB1 = (float)( checked( R2.Width - 1 ) / (double)checked( _Values.Count - 1 ) );
                var count = checked( _Values.Count - 1 );

                for( var i = 0; i <= count; i = checked( i + 1 ) )
                {
                    FB2 = R2.X + i * FB1;

                    _CurrentValue = ( _Values[ i ] - _Minimum )
                        / Math.Max( _Maximum - _Minimum, 1f );

                    if( _CurrentValue > 1f )
                    {
                        _CurrentValue = 1f;
                    }
                    else if( _CurrentValue < 0f )
                    {
                        _CurrentValue = 0f;
                    }

                    pointF[ i ] = new PointF( FB2,
                        checked( (int)Math.Round( R2.Bottom
                            - checked( R2.Height - 1 ) * _CurrentValue
                            - 1f ) ) );

                    if( _DrawVerticalLines )
                    {
                        graphics.DrawLine( pen, FB2, R1.Y, FB2, R1.Bottom );
                    }
                }

                pointF[ checked( pointF.Length - 2 ) ] = new PointF( checked( R2.Right - 1 ),
                    checked( R1.Bottom - 1 ) );

                pointF[ checked( pointF.Length - 1 ) ] =
                    new PointF( R2.X, checked( R1.Bottom - 1 ) );

                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pen.DashStyle = _DashStyle;

                if( !_SingleLine )
                {
                    graphicsPath.AddPolygon( pointF );
                    graphicsPath.CloseFigure( );
                    pen.Color = _ClassicLineColor;
                    pen.Width = _ClassicLineThickness;

                    var color = _UseGradient
                        ? _GradientColor
                        : _ClassicFillColor;

                    using( var linearGradientBrush = new LinearGradientBrush( _GradientPointA,
                              _GradientPointB, _ClassicFillColor, color ) )
                    {
                        graphics.FillPath( linearGradientBrush, graphicsPath );
                        graphics.DrawPath( pen, graphicsPath );

                        using( var pen1 = new Pen( linearGradientBrush, 3f ) )
                        {
                            var point1 = new Point( 0, Height );
                            graphics.DrawLine( pen1, graphicsPath.PathPoints[ 0 ], point1 );

                            point1 = new Point(
                                checked( (int)Math.Round( graphicsPath
                                        .PathPoints[ checked( pointF.Length - 3 ) ].X
                                    - 1f ) ),
                                checked( checked( (int)Math.Round( graphicsPath
                                        .PathPoints[ checked( pointF.Length - 3 ) ].Y ) )
                                    + 4 ) );

                            point = new Point( checked( Width - 2 ), Height );
                            graphics.DrawLine( pen1, point1, point );
                            point = new Point( Width, checked( Height - 1 ) );
                            point1 = new Point( 0, checked( Height - 1 ) );
                            graphics.DrawLine( pen1, point, point1 );
                        }
                    }

                    graphicsPath.Reset( );
                    graphicsPath.Dispose( );
                }
                else
                {
                    Array.Resize( ref pointF, checked( pointF.Length - 2 ) );

                    if( _SingleLineShadow )
                    {
                        pen.Color = Design.BudgetColors.TextShadow;
                        pen.Width = checked( _SingleLineThickness + 2 );
                        graphics.DrawLines( pen, pointF );
                    }

                    pen.Color = _SingleLineColor;
                    pen.Width = _SingleLineThickness;
                    graphics.DrawLines( pen, pointF );
                }

                if( !_DrawHoverData || _Index < 0
                       ? false
                       : true )
                {
                    graphics.SetClip( R1 );

                    var client = new Point( checked( (int)Math.Round( pointF[ _Index ].X ) ),
                        checked( (int)Math.Round( pointF[ _Index ].Y ) ) );

                    R3 = new Rectangle( checked( client.X - 4 ), checked( client.Y - 4 ), 8, 8 );
                    pen.Color = _HoverColor;
                    pen.Width = 1f;

                    if( _DrawHoverLine )
                    {
                        graphics.DrawLine( pen, client.X, R1.Y, client.X,
                            checked( R1.Bottom - 1 ) );
                    }

                    var foreColor =
                        new SolidBrush(
                            Design.BudgetColors.ChangeColorBrightness( _HoverColor, 0.2f ) );

                    graphics.FillEllipse( foreColor, R3 );
                    graphics.DrawEllipse( pen, R3 );
                    var str = _Values[ _Index ].ToString( );
                    var sizeF = graphics.MeasureString( str, Font );
                    var size = sizeF.ToSize( );
                    client = PointToClient( BudgetGraph.MousePosition );

                    R3 = new Rectangle( checked( client.X + 24 ), client.Y,
                        checked( size.Width + 20 ), checked( size.Height + 10 ) );

                    if( checked( R3.X + R3.Width ) > checked( R1.Right - 1 ) )
                    {
                        R3.X = checked( checked( client.X - R3.Width ) - 16 );
                    }

                    if( checked( R3.Y + R3.Height ) > checked( R1.Bottom - 1 ) )
                    {
                        R3.Y = checked( checked( R1.Bottom - R3.Height ) - 1 );
                    }

                    foreColor.Color = _HoverBoxColor;
                    pen.Color = _HoverBoxBorderColor;
                    graphics.FillRectangle( foreColor, R3 );
                    graphics.DrawRectangle( pen, R3 );
                    foreColor.Color = ForeColor;
                    var font = Font;
                    point = new Point( checked( R3.X + 10 ), checked( R3.Y + 5 ) );
                    graphics.DrawString( str, font, foreColor, point );
                    foreColor.Dispose( );
                }

                graphics.ResetClip( );
                graphics.SmoothingMode = SmoothingMode.None;
            }

            pen.Color = _BorderColor;
            pen.Width = 1f;
            pen.DashStyle = DashStyle.Solid;

            if( outerBorder )
            {
                graphics.DrawRectangle( pen, R1.X, R1.Y, checked( R1.Width - 1 ),
                    checked( R1.Height - 1 ) );
            }

            pen.Dispose( );
            base.OnPaint( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged( EventArgs e )
        {
            if( DesignMode )
            {
                _GradientPointA = new Point( checked( (int)Math.Round( (double)Width / 2 ) ), 0 );

                _GradientPointB =
                    new Point( checked( (int)Math.Round( (double)Width / 2 ) ), Height );
            }

            base.OnSizeChanged( e );
        }

        /**/

        #endregion
    }
}