// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTabControl.cs" company="Terry D. Eppler">
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
//   BudgetTabControl.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering a metro-style tab control.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.TabControl" />
    [ Description( "" ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( TabControl ) ) ]
    public class BudgetTabControl : TabControl
    {
        #region Private Fields

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The item color
        /// </summary>
        private Color _ItemColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The selected border color
        /// </summary>
        private Color _SelectedBorderColor;

        /// <summary>
        /// The selected item color
        /// </summary>
        private Color _SelectedItemColor;

        /// <summary>
        /// The selected fore color
        /// </summary>
        private Color _SelectedForeColor;

        /// <summary>
        /// The selected item line color
        /// </summary>
        private Color _SelectedItemLineColor;

        /// <summary>
        /// The item fore color
        /// </summary>
        private Color _ItemForeColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The hover border color
        /// </summary>
        private Color _HoverBorderColor;

        /// <summary>
        /// The header item color
        /// </summary>
        private Color _HeaderItemColor;

        /// <summary>
        /// The header fore color
        /// </summary>
        private Color _HeaderForeColor;

        /// <summary>
        /// The tab container color
        /// </summary>
        private Color _TabContainerColor;

        /// <summary>
        /// The draw selected line
        /// </summary>
        private bool _DrawSelectedLine;

        /// <summary>
        /// The draw polygon
        /// </summary>
        private bool _DrawPolygon;

        /// <summary>
        /// The polygon width
        /// </summary>
        private int _PolygonWidth;

        /// <summary>
        /// The selected item line width
        /// </summary>
        private int _SelectedItemLineWidth;

        /// <summary>
        /// The selected tab is bold
        /// </summary>
        private bool _SelectedTabIsBold;

        /// <summary>
        /// The has animation
        /// </summary>
        private bool _HasAnimation;

        /// <summary>
        /// The automatic back color
        /// </summary>
        private bool _AutoBackColor;

        /// <summary>
        /// The item text align
        /// </summary>
        private StringAlignment _ItemTextAlign;

        /// <summary>
        /// The header indexes
        /// </summary>
        private List<int> _HeaderIndexes;

        /// <summary>
        /// The image width
        /// </summary>
        private int _ImageWidth;

        /// <summary>
        /// The last index
        /// </summary>
        private int _LastIndex;

        /// <summary>
        /// The animation speed
        /// </summary>
        private int _AnimationSpeed;

        /// <summary>
        /// The initialized
        /// </summary>
        private bool _Initialized;

        /// <summary>
        /// The default item size
        /// </summary>
        public Size DefaultItemSize;

        /// <summary>
        /// The mouse state
        /// </summary>
        private Helpers.MouseState _MouseState;

        /// <summary>
        /// The hot tab
        /// </summary>
        private Rectangle _HotTab;

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
        /// Gets or sets the area of the control (for example, along the top) where the tabs are aligned.
        /// </summary>
        /// <value>The alignment.</value>
        [ DefaultValue( 2 ) ]
        public new TabAlignment Alignment
        {
            get
            {
                return base.Alignment;
            }
            set
            {
                Size itemSize;
                Size size;
                Size size1;

                if( value == TabAlignment.Bottom || value == TabAlignment.Top
                       ? true
                       : false )
                {
                    if( base.Alignment == TabAlignment.Left || base.Alignment == TabAlignment.Right
                           ? true
                           : false )
                    {
                        itemSize = ItemSize;
                        var height = itemSize.Height;
                        size = ItemSize;
                        size1 = new Size( height, size.Width );
                        ItemSize = size1;
                    }
                }
                else if( value == TabAlignment.Left || value == TabAlignment.Right
                            ? true
                            : false )
                {
                    if( base.Alignment == TabAlignment.Bottom || base.Alignment == TabAlignment.Top
                           ? true
                           : false )
                    {
                        size1 = ItemSize;
                        var num = size1.Height;
                        size = ItemSize;
                        itemSize = new Size( num, size.Width );
                        ItemSize = itemSize;
                    }
                }

                base.Alignment = value;
            }
        }

        /// <summary>
        /// Gets or sets the animation speed.
        /// </summary>
        /// <value>The animation speed.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( 5 ) ]
        [ Description( "Sets the animation speed." ) ]
        public int AnimationSpeed
        {
            get
            {
                return _AnimationSpeed;
            }
            set
            {
                if( _AnimationSpeed != value )
                {
                    _AnimationSpeed = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the visual appearance of the control's tabs.
        /// </summary>
        /// <value>The appearance.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new Appearance Appearance
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether enable automatic back color.
        /// </summary>
        /// <value><c>true</c> if automatic back color; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether enable automatic back color." ) ]
        public bool AutoBackColor
        {
            get
            {
                return _AutoBackColor;
            }
            set
            {
                if( _AutoBackColor != value )
                {
                    _AutoBackColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to set automatic style.
        /// </summary>
        /// <value><c>true</c> if [automatic style]; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to set automatic style." ) ]
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
        /// Gets or sets the background image.
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
        /// Gets or sets the background image layout.
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
                if( _BorderColor != value )
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
        /// Gets or sets a value indicating whether to draw the item selected line.
        /// </summary>
        /// <value><c>true</c> if draw item selected line; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to draw the item selected line." ) ]
        public bool DrawItemSelectedLine
        {
            get
            {
                return _DrawSelectedLine;
            }
            set
            {
                if( _DrawSelectedLine != value )
                {
                    _DrawSelectedLine = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw triangle.
        /// </summary>
        /// <value><c>true</c> if draw triangle; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( false ) ]
        [ Description( "Sets a value indicating whether to draw triangle." ) ]
        public bool DrawTriangle
        {
            get
            {
                return _DrawPolygon;
            }
            set
            {
                if( _DrawPolygon != value )
                {
                    _DrawPolygon = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this control has animation enabled/disabled.
        /// </summary>
        /// <value><c>true</c> if this control has animation; otherwise, <c>false</c>.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( true ) ]
        [ Description(
            "Sets a value indicating whether this control has animation enabled/disabled." ) ]
        public bool HasAnimation
        {
            get
            {
                return _HasAnimation;
            }
            set
            {
                if( _HasAnimation != value )
                {
                    _HasAnimation = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the header.
        /// </summary>
        /// <value>The color of the header.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the header." ) ]
        public Color HeaderForeColor
        {
            get
            {
                return _HeaderForeColor;
            }
            set
            {
                if( _HeaderForeColor != value )
                {
                    _HeaderForeColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the header item.
        /// </summary>
        /// <value>The color of the header item.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the header item." ) ]
        public Color HeaderItemColor
        {
            get
            {
                return _HeaderItemColor;
            }
            set
            {
                if( _HeaderItemColor != value )
                {
                    _HeaderItemColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the border when hovered.
        /// </summary>
        /// <value>The color of the hover border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the border when hovered." ) ]
        public Color HoverBorderColor
        {
            get
            {
                return _HoverBorderColor;
            }
            set
            {
                if( _HoverBorderColor != value )
                {
                    _HoverBorderColor = value;
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
        /// Gets or sets the images to display on the control's tabs.
        /// </summary>
        /// <value>The image list.</value>
        public new ImageList ImageList
        {
            get
            {
                return base.ImageList;
            }
            set
            {
                base.ImageList = value;

                if( value != null )
                {
                    _ImageWidth = ImageList.ImageSize.Width;
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the image.
        /// </summary>
        /// <value>The width of the image.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 16 ) ]
        [ Description( "Sets the width of the image." ) ]
        public int ImageWidth
        {
            get
            {
                return _ImageWidth;
            }
            set
            {
                if( _ImageWidth != value )
                {
                    _ImageWidth = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the item.
        /// </summary>
        /// <value>The color of the item.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the item." ) ]
        public Color ItemColor
        {
            get
            {
                return _ItemColor;
            }
            set
            {
                if( _ItemColor != value )
                {
                    _ItemColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the item.
        /// </summary>
        /// <value>The color of the item.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the item." ) ]
        public Color ItemForeColor
        {
            get
            {
                return _ItemForeColor;
            }
            set
            {
                if( _ItemForeColor != value )
                {
                    _ItemForeColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the item's text alignment.
        /// </summary>
        /// <value>The item text align.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( 1 ) ]
        [ Description( "Sets the item's text alignment." ) ]
        public StringAlignment ItemTextAlign
        {
            get
            {
                return _ItemTextAlign;
            }
            set
            {
                if( _ItemTextAlign != value )
                {
                    _ItemTextAlign = value;
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
        /// Gets or sets a value indicating whether right-to-left mirror placement is turned on.
        /// </summary>
        /// <value><c>true</c> if right to left layout; otherwise, <c>false</c>.</value>
        [ Browsable( false ) ]
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public new bool RightToLeftLayout
        {
            [ DebuggerNonUserCode ]
            get;
            [ DebuggerNonUserCode ]
            set;
        }

        /// <summary>
        /// Gets or sets the color of the selected border.
        /// </summary>
        /// <value>The color of the selected border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the selected border." ) ]
        public Color SelectedBorderColor
        {
            get
            {
                return _SelectedBorderColor;
            }
            set
            {
                if( _SelectedBorderColor != value )
                {
                    _SelectedBorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the selected fore.
        /// </summary>
        /// <value>The color of the selected fore.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the selected item." ) ]
        public Color SelectedForeColor
        {
            get
            {
                return _SelectedForeColor;
            }
            set
            {
                if( _SelectedForeColor != value )
                {
                    _SelectedForeColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the selected item.
        /// </summary>
        /// <value>The color of the selected item.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the selected item." ) ]
        public Color SelectedItemColor
        {
            get
            {
                return _SelectedItemColor;
            }
            set
            {
                if( _SelectedItemColor != value )
                {
                    _SelectedItemColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the selected item line.
        /// </summary>
        /// <value>The color of the selected item line.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the selected item line." ) ]
        public Color SelectedItemLineColor
        {
            get
            {
                return _SelectedItemLineColor;
            }
            set
            {
                if( _SelectedItemLineColor != value )
                {
                    _SelectedItemLineColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the selected item line.
        /// </summary>
        /// <value>The width of the selected item line.</value>
        [ Category( "Behavior" ) ]
        [ DefaultValue( 2 ) ]
        [ Description( "Sets the width of the selected item line." ) ]
        public int SelectedItemLineWidth
        {
            get
            {
                return _SelectedItemLineWidth;
            }
            set
            {
                if( _SelectedItemLineWidth != value )
                {
                    _SelectedItemLineWidth = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether selected tab is bold.
        /// </summary>
        /// <value><c>true</c> if selected tab is bold; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether selected tab is bold." ) ]
        public bool SelectedTabIsBold
        {
            get
            {
                return _SelectedTabIsBold;
            }
            set
            {
                if( _SelectedTabIsBold != value )
                {
                    _SelectedTabIsBold = value;
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
                            _ItemColor = Design.BudgetColors.LightDefault;
                            _BorderColor = Design.BudgetColors.PopUpBorder;
                            _SelectedBorderColor = Design.BudgetColors.AccentBlue;
                            _SelectedItemColor = Design.BudgetColors.AccentBlue;
                            _SelectedForeColor = Design.BudgetColors.LightDefault;
                            _ItemForeColor = Design.BudgetColors.LightFont;
                            _HoverColor = Design.BudgetColors.LightDefault;
                            _HoverBorderColor = Design.BudgetColors.AccentBlue;
                            _HeaderItemColor = Design.BudgetColors.LightDefault;
                            _HeaderForeColor = Design.BudgetColors.PopUpFont;
                            _TabContainerColor = Design.BudgetColors.LightDefault;
                            ForeColor = Design.BudgetColors.LightFont;

                            if( _AutoBackColor )
                            {
                                ChangeTabPageColors( GetParentColor( ) );
                            }

                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _ItemColor = Design.BudgetColors.DarkDefault;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            _SelectedBorderColor = Design.BudgetColors.AccentBlue;
                            _SelectedItemColor = Design.BudgetColors.AccentBlue;
                            _SelectedForeColor = Design.BudgetColors.LightDefault;
                            _ItemForeColor = Design.BudgetColors.DarkFont;
                            _HoverColor = Design.BudgetColors.DarkDefault;
                            _HoverBorderColor = Design.BudgetColors.AccentBlue;
                            _HeaderItemColor = Design.BudgetColors.DarkDefault;
                            _HeaderForeColor = Design.BudgetColors.PopUpFont;
                            _TabContainerColor = Design.BudgetColors.DarkDefault;
                            ForeColor = Design.BudgetColors.DarkFont;

                            if( _AutoBackColor )
                            {
                                ChangeTabPageColors( GetParentColor( ) );
                            }

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
        /// Gets or sets the color of the tab container.
        /// </summary>
        /// <value>The color of the tab container.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the tab container." ) ]
        public Color TabContainerColor
        {
            get
            {
                return _TabContainerColor;
            }
            set
            {
                if( _TabContainerColor != value )
                {
                    _TabContainerColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the triangle.
        /// </summary>
        /// <value>The width of the triangle.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( 10 ) ]
        [ Description( "Sets the width of the triangle." ) ]
        public int TriangleWidth
        {
            get
            {
                return _PolygonWidth;
            }
            set
            {
                if( _PolygonWidth != value )
                {
                    _PolygonWidth = value;
                    Invalidate( );
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTabControl" /> class.
        /// </summary>
        public BudgetTabControl( )
        {
            _Style = Design.Style.Light;
            _ItemColor = Design.BudgetColors.LightDefault;
            _BorderColor = Design.BudgetColors.PopUpBorder;
            _SelectedBorderColor = Design.BudgetColors.AccentBlue;
            _SelectedItemColor = Design.BudgetColors.AccentBlue;
            _SelectedForeColor = Design.BudgetColors.LightDefault;
            _SelectedItemLineColor = Design.BudgetColors.AccentBlue;
            _ItemForeColor = Design.BudgetColors.LightFont;
            _HoverColor = Design.BudgetColors.LightDefault;
            _HoverBorderColor = Design.BudgetColors.AccentBlue;
            _HeaderItemColor = Design.BudgetColors.LightDefault;
            _HeaderForeColor = Color.FromArgb( 150, 150, 150 );
            _TabContainerColor = Design.BudgetColors.LightDefault;
            _DrawSelectedLine = false;
            _DrawPolygon = false;
            _PolygonWidth = 10;
            _SelectedItemLineWidth = 2;
            _SelectedTabIsBold = true;
            _HasAnimation = true;
            _AutoBackColor = true;
            _ItemTextAlign = StringAlignment.Center;
            _HeaderIndexes = new List<int>( );
            _ImageWidth = 16;
            _LastIndex = 0;
            _AnimationSpeed = 5;
            _Initialized = false;
            DefaultItemSize = new Size( 45, 120 );
            _MouseState = Helpers.MouseState.None;
            _HotIndex = -1;
            _AutoStyle = true;
            Font = new Font( "Segoe UI", 9f );

            SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true );

            UpdateStyles( );
            SizeMode = TabSizeMode.Fixed;
            Alignment = TabAlignment.Left;
            HotTrack = true;
            ItemSize = new Size( 45, 120 );
        }

        /// <summary>
        /// Changes the tab page colors.
        /// </summary>
        /// <param name="color">The color.</param>
        public void ChangeTabPageColors( Color color )
        {
            var count = checked( TabPages.Count - 1 );

            for( var i = 0; i <= count; i = checked( i + 1 ) )
            {
                TabPages[ i ].BackColor = color;
            }
        }

        /// <summary>
        /// Changes the tab page to header.
        /// </summary>
        /// <param name="index">The index.</param>
        public void ChangeTabPageToHeader( int index )
        {
            _HeaderIndexes.Add( index );
            Invalidate( );
        }

        /// <summary>
        /// Changes the tab page to header.
        /// </summary>
        /// <param name="index">The index.</param>
        public void ChangeTabPageToHeader( int[ ] index )
        {
            var numArray = index;

            for( var i = 0; i < numArray.Length; i = checked( i + 1 ) )
            {
                ChangeTabPageToHeader( numArray[ i ] );
            }
        }

        /// <summary>
        /// Does the animation.
        /// </summary>
        /// <param name="Control1">The control1.</param>
        /// <param name="Control2">The control2.</param>
        /// <param name="isLeftScroll">if set to <c>true</c> [is left scroll].</param>
        private void DoAnimation( Control Control1, Control Control2, bool isLeftScroll )
        {
            int i;
            IEnumerator enumerator = null;
            IEnumerator enumerator1 = null;
            IEnumerator enumerator2 = null;
            IEnumerator enumerator3 = null;

            try
            {
                var graphic = Control1.CreateGraphics( );
                var bitmap = new Bitmap( Control1.Width, Control1.Height );
                var bitmap1 = new Bitmap( Control2.Width, Control2.Height );
                var rectangle = new Rectangle( 0, 0, Control1.Width, Control1.Height );
                Control1.DrawToBitmap( bitmap, rectangle );
                rectangle = new Rectangle( 0, 0, Control2.Width, Control2.Height );
                Control2.DrawToBitmap( bitmap1, rectangle );

                try
                {
                    enumerator = Control2.Controls.GetEnumerator( );

                    while( enumerator.MoveNext( ) )
                    {
                        var current = (Control)enumerator.Current;

                        if( !current.Visible )
                        {
                            current.Tag = "0";
                        }
                    }
                }
                finally
                {
                    if( enumerator is IDisposable )
                    {
                        ( enumerator as IDisposable ).Dispose( );
                    }
                }

                try
                {
                    enumerator1 = Control1.Controls.GetEnumerator( );

                    while( enumerator1.MoveNext( ) )
                    {
                        var control = (Control)enumerator1.Current;

                        if( !control.Visible )
                        {
                            control.Tag = "0";
                        }

                        control.Hide( );
                    }
                }
                finally
                {
                    if( enumerator1 is IDisposable )
                    {
                        ( enumerator1 as IDisposable ).Dispose( );
                    }
                }

                var width = checked( Control1.Width - Control1.Width % _AnimationSpeed );

                if( !isLeftScroll )
                {
                    var num = checked( 0 - width );
                    var num1 = checked( 0 - _AnimationSpeed );

                    for( i = 0; ( num1 >> 31 ^ i ) <= ( num1 >> 31 ^ num );
                        i = checked( i + num1 ) )
                    {
                        rectangle = new Rectangle( i, 0, Control1.Width, Control1.Height );
                        graphic.DrawImage( bitmap, rectangle );

                        rectangle = new Rectangle( checked( i + Control2.Width ), 0, Control2.Width,
                            Control2.Height );

                        graphic.DrawImage( bitmap1, rectangle );
                    }

                    i = Control1.Width;
                    rectangle = new Rectangle( i, 0, Control1.Width, Control1.Height );
                    graphic.DrawImage( bitmap, rectangle );

                    rectangle = new Rectangle( checked( i + Control2.Width ), 0, Control2.Width,
                        Control2.Height );

                    graphic.DrawImage( bitmap1, rectangle );
                }
                else
                {
                    var num2 = width;
                    var num3 = _AnimationSpeed;

                    for( i = 0; ( num3 >> 31 ^ i ) <= ( num3 >> 31 ^ num2 );
                        i = checked( i + num3 ) )
                    {
                        rectangle = new Rectangle( i, 0, Control1.Width, Control1.Height );
                        graphic.DrawImage( bitmap, rectangle );

                        rectangle = new Rectangle( checked( i - Control2.Width ), 0, Control2.Width,
                            Control2.Height );

                        graphic.DrawImage( bitmap1, rectangle );
                    }

                    i = Control1.Width;
                    rectangle = new Rectangle( i, 0, Control1.Width, Control1.Height );
                    graphic.DrawImage( bitmap, rectangle );

                    rectangle = new Rectangle( checked( i - Control2.Width ), 0, Control2.Width,
                        Control2.Height );

                    graphic.DrawImage( bitmap1, rectangle );
                }

                SelectedTab = (TabPage)Control2;

                try
                {
                    enumerator2 = Control2.Controls.GetEnumerator( );

                    while( enumerator2.MoveNext( ) )
                    {
                        var current1 = (Control)enumerator2.Current;

                        if( current1.Tag != null )
                        {
                            current1.Show( );
                        }

                        current1.Tag = null;
                    }
                }
                finally
                {
                    if( enumerator2 is IDisposable )
                    {
                        ( enumerator2 as IDisposable ).Dispose( );
                    }
                }

                try
                {
                    enumerator3 = Control1.Controls.GetEnumerator( );

                    while( enumerator3.MoveNext( ) )
                    {
                        var control1 = (Control)enumerator3.Current;

                        if( control1.Tag != null )
                        {
                            control1.Show( );
                        }

                        control1.Tag = null;
                    }
                }
                finally
                {
                    if( enumerator3 is IDisposable )
                    {
                        ( enumerator3 as IDisposable ).Dispose( );
                    }
                }

                graphic.Dispose( );
                bitmap.Dispose( );
                bitmap1.Dispose( );
                var animationCompleteEventHandler = AnimationComplete;

                if( animationCompleteEventHandler != null )
                {
                    animationCompleteEventHandler( this, new EventArgs( ) );
                }
            }
            catch( Exception exception )
            {
                ProjectData.SetProjectError( exception );
                ProjectData.ClearProjectError( );
            }
        }

        /// <summary>
        /// Draws the polygon.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected void DrawPolygon( PaintEventArgs e )
        {
            Point[ ] pointArray;
            Point point;
            Point point1;
            Point point2;
            Point[ ] pointArray1;
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if( _DrawPolygon )
            {
                var num = checked( (int)Math.Round( (double)_PolygonWidth / 2 ) );
                var tabRect = GetTabRect( SelectedIndex );

                switch( Alignment )
                {
                    case TabAlignment.Top:
                    {
                        pointArray1 = new Point[ 3 ];

                        point2 = new Point(
                            checked( checked( tabRect.X
                                    + checked( (int)Math.Round( (double)tabRect.Width / 2 ) ) )
                                - num ), checked( tabRect.Y + tabRect.Height ) );

                        pointArray1[ 0 ] = point2;

                        point1 = new Point(
                            checked( tabRect.X
                                + checked( (int)Math.Round( (double)tabRect.Width / 2 ) ) ),
                            checked( checked( tabRect.Y + tabRect.Height ) - num ) );

                        pointArray1[ 1 ] = point1;

                        point = new Point(
                            checked( checked( tabRect.X
                                    + checked( (int)Math.Round( (double)tabRect.Width / 2 ) ) )
                                + num ), checked( tabRect.Y + tabRect.Height ) );

                        pointArray1[ 2 ] = point;
                        pointArray = pointArray1;
                        break;
                    }
                    case TabAlignment.Bottom:
                    {
                        pointArray1 = new Point[ 3 ];

                        point2 = new Point(
                            checked( checked( tabRect.X
                                    + checked( (int)Math.Round( (double)tabRect.Width / 2 ) ) )
                                - num ), tabRect.Y );

                        pointArray1[ 0 ] = point2;

                        point1 = new Point(
                            checked( tabRect.X
                                + checked( (int)Math.Round( (double)tabRect.Width / 2 ) ) ),
                            checked( tabRect.Y + num ) );

                        pointArray1[ 1 ] = point1;

                        point = new Point(
                            checked( checked( tabRect.X
                                    + checked( (int)Math.Round( (double)tabRect.Width / 2 ) ) )
                                + num ), tabRect.Y );

                        pointArray1[ 2 ] = point;
                        pointArray = pointArray1;
                        break;
                    }
                    case TabAlignment.Left:
                    {
                        pointArray1 = new Point[ 3 ];

                        point = new Point( checked( tabRect.X + tabRect.Width ),
                            checked( checked( tabRect.Y
                                    + checked( (int)Math.Round( (double)tabRect.Height / 2 ) ) )
                                - num ) );

                        pointArray1[ 0 ] = point;

                        point1 = new Point( checked( checked( tabRect.X + tabRect.Width ) - num ),
                            checked( tabRect.Y
                                + checked( (int)Math.Round( (double)tabRect.Height / 2 ) ) ) );

                        pointArray1[ 1 ] = point1;

                        point2 = new Point( checked( tabRect.X + tabRect.Width ),
                            checked( checked( tabRect.Y
                                    + checked( (int)Math.Round( (double)tabRect.Height / 2 ) ) )
                                + num ) );

                        pointArray1[ 2 ] = point2;
                        pointArray = pointArray1;
                        break;
                    }
                    case TabAlignment.Right:
                    {
                        pointArray1 = new Point[ 3 ];

                        point2 = new Point( tabRect.X,
                            checked( checked( tabRect.Y
                                    + checked( (int)Math.Round( (double)tabRect.Height / 2 ) ) )
                                - num ) );

                        pointArray1[ 0 ] = point2;

                        point1 = new Point( checked( tabRect.X + num ),
                            checked( tabRect.Y
                                + checked( (int)Math.Round( (double)tabRect.Height / 2 ) ) ) );

                        pointArray1[ 1 ] = point1;

                        point = new Point( tabRect.X,
                            checked( checked( tabRect.Y
                                    + checked( (int)Math.Round( (double)tabRect.Height / 2 ) ) )
                                + num ) );

                        pointArray1[ 2 ] = point;
                        pointArray = pointArray1;
                        break;
                    }
                    default:
                    {
                        pointArray = null;
                        break;
                    }
                }

                using( var solidBrush = new SolidBrush( _SelectedBorderColor ) )
                {
                    if( pointArray != null )
                    {
                        graphics.FillPolygon( solidBrush, pointArray );
                    }
                }
            }

            graphics.SmoothingMode = SmoothingMode.HighSpeed;
        }

        /// <summary>
        /// Gets the correct tab rect.
        /// </summary>
        /// <param name="tabrect">The tabrect.</param>
        /// <param name="index">The index.</param>
        /// <returns>Rectangle.</returns>
        private Rectangle GetCorrectTabRect( Rectangle tabrect, int index )
        {
            Rectangle rectangle;

            if( !_HeaderIndexes.Contains( checked( index + 1 ) ) )
            {
                rectangle = tabrect;
            }
            else
            {
                rectangle = ( Alignment == TabAlignment.Top || Alignment == TabAlignment.Bottom
                    ? false
                    : true )
                    ? new Rectangle( tabrect.X, tabrect.Y, tabrect.Width,
                        checked( tabrect.Height - 1 ) )
                    : new Rectangle( tabrect.X, tabrect.Y, checked( tabrect.Width - 1 ),
                        tabrect.Height );
            }

            return rectangle;
        }

        /// <summary>
        /// Gets the color of the parent.
        /// </summary>
        /// <returns>Color.</returns>
        private Color GetParentColor( )
        {
            Color backColor;

            try
            {
                backColor = Parent.BackColor;
            }
            catch( Exception exception )
            {
                ProjectData.SetProjectError( exception );
                backColor = _ItemColor;
                ProjectData.ClearProjectError( );
            }

            return backColor;
        }

        /// <summary>
        /// Gets the tab container.
        /// </summary>
        /// <param name="align">The align.</param>
        /// <returns>Rectangle.</returns>
        private Rectangle GetTabContainer( TabAlignment align )
        {
            var rectangle = new Rectangle( );
            Rectangle rectangle1;
            Size size;
            Size itemSize;

            if( TabCount > 0 )
            {
                var tabRect = GetTabRect( 0 );

                switch( align )
                {
                    case TabAlignment.Top:
                    {
                        var location = tabRect.Location;
                        var width = Width;
                        itemSize = ItemSize;
                        size = new Size( width, itemSize.Height );
                        rectangle1 = new Rectangle( location, size );
                        rectangle = rectangle1;
                        break;
                    }
                    case TabAlignment.Bottom:
                    {
                        var point = tabRect.Location;
                        var num = Width;
                        itemSize = ItemSize;
                        size = new Size( num, checked( itemSize.Width - 1 ) );
                        rectangle1 = new Rectangle( point, size );
                        rectangle = rectangle1;
                        break;
                    }
                    case TabAlignment.Left:
                    {
                        var location1 = tabRect.Location;
                        size = ItemSize;
                        itemSize = new Size( checked( size.Height + 3 ), Height );
                        rectangle1 = new Rectangle( location1, itemSize );
                        rectangle = rectangle1;
                        break;
                    }
                    case TabAlignment.Right:
                    {
                        var point1 = tabRect.Location;
                        itemSize = ItemSize;
                        size = new Size( checked( itemSize.Height + 1 ), Height );
                        rectangle1 = new Rectangle( point1, size );
                        rectangle = rectangle1;
                        break;
                    }
                }
            }
            else
            {
                rectangle1 = new Rectangle( 0, 0, 0, 0 );
                rectangle = rectangle1;
            }

            return rectangle;
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
        /// Raises the <see cref="E:System.Windows.Forms.TabControl.Deselecting" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> that contains the event data.</param>
        protected override void OnDeselecting( TabControlCancelEventArgs e )
        {
            if( _HasAnimation && TabPages.Count > 0 )
            {
                _LastIndex = e.TabPageIndex;
            }
        }

        /// <summary>
        /// This member overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" />.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnHandleCreated( EventArgs e )
        {
            if( !_Initialized )
            {
                if( _HasAnimation )
                {
                    var num = _AnimationSpeed;
                    _AnimationSpeed = 20;
                    var count = TabPages.Count;

                    for( var i = 0; i <= count; i = checked( i + 1 ) )
                    {
                        SelectedIndex = i;
                    }

                    SelectedIndex = 0;
                    _AnimationSpeed = num;
                    _Initialized = true;
                }
            }

            base.OnHandleCreated( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave( EventArgs e )
        {
            _MouseState = Helpers.MouseState.None;
            _HotTab = new Rectangle( );
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
            var count = checked( TabPages.Count - 1 );

            for( var i = 0; i <= count; i = checked( i + 1 ) )
            {
                if( GetTabRect( i ).Contains( e.Location ) )
                {
                    if( _HotTab != GetTabRect( i ) )
                    {
                        if( !_HeaderIndexes.Contains( i ) )
                        {
                            _HotTab = GetTabRect( i );
                            _HotIndex = i;
                            _MouseState = Helpers.MouseState.Over;
                            Invalidate( );
                        }
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
            Rectangle correctTabRect;
            Rectangle rectangle;
            var rectangle1 = new Rectangle( );
            var graphics = e.Graphics;

            var stringFormat = new StringFormat( )
            {
                LineAlignment = StringAlignment.Center,
                Alignment = _ItemTextAlign
            };

            var stringFormat1 = stringFormat;
            graphics.Clear( GetParentColor( ) );

            using( var solidBrush = new SolidBrush( _TabContainerColor ) )
            {
                graphics.FillRectangle( solidBrush, GetTabContainer( Alignment ) );
            }

            using( var solidBrush1 = new SolidBrush( _ItemColor ) )
            {
                using( var pen = new Pen( _BorderColor ) )
                {
                    if( TabCount > 0 )
                    {
                        var tabCount = checked( TabCount - 1 );

                        for( var i = 0; i <= tabCount; i = checked( i + 1 ) )
                        {
                            solidBrush1.Color = _HeaderIndexes.Contains( i )
                                ? _HeaderItemColor
                                : _ItemColor;

                            correctTabRect = GetCorrectTabRect( GetTabRect( i ), i );

                            if( correctTabRect == _HotTab )
                            {
                                solidBrush1.Color = _HeaderIndexes.Contains( i )
                                    ? _HeaderItemColor
                                    : _HoverColor;
                            }

                            graphics.FillRectangle( solidBrush1, correctTabRect );
                            graphics.DrawRectangle( pen, correctTabRect );

                            using( var solidBrush2 = new SolidBrush( _HeaderIndexes.Contains( i )
                                      ? _HeaderForeColor
                                      : _ItemForeColor ) )
                            {
                                if( ImageList != null )
                                {
                                    if( TabPages[ i ].ImageIndex != -1 )
                                    {
                                        if( ImageList.Images[ TabPages[ i ].ImageIndex ] != null )
                                        {
                                            var item = ImageList.Images[ TabPages[ i ].ImageIndex ];

                                            rectangle = new Rectangle(
                                                checked( correctTabRect.X
                                                    + checked( (int)Math.Round( (double)_ImageWidth
                                                        / 2 ) ) ),
                                                checked( checked( checked( correctTabRect.Y
                                                            + checked( (int)Math.Round(
                                                                (double)correctTabRect.Height
                                                                / 2 ) ) )
                                                        - checked( (int)Math.Round(
                                                            (double)_ImageWidth / 2 ) ) )
                                                    + 1 ), _ImageWidth, _ImageWidth );

                                            graphics.DrawImage( item, rectangle );

                                            correctTabRect = new Rectangle(
                                                checked( checked( correctTabRect.X + 20 )
                                                    + _ImageWidth ), correctTabRect.Y,
                                                checked( correctTabRect.Width
                                                    - checked( 20 + _ImageWidth ) ),
                                                correctTabRect.Height );
                                        }
                                    }
                                    else if( _ItemTextAlign == StringAlignment.Near )
                                    {
                                        correctTabRect = new Rectangle(
                                            checked( correctTabRect.X
                                                + checked( (int)Math.Round(
                                                    (double)_ImageWidth / 2 ) ) ), correctTabRect.Y,
                                            checked( correctTabRect.Width
                                                - checked( (int)Math.Round(
                                                    (double)_ImageWidth / 2 ) ) ),
                                            correctTabRect.Height );
                                    }
                                }

                                graphics.DrawString( TabPages[ i ].Text, TabPages[ i ].Font,
                                    solidBrush2, correctTabRect, stringFormat1 );
                            }
                        }

                        pen.Color = _HoverBorderColor;

                        if( _HotTab != rectangle1
                           && HotTrack
                           && SelectedIndex != _HotIndex )
                        {
                            graphics.DrawRectangle( pen, _HotTab );
                        }
                    }

                    if( TabCount > 0 )
                    {
                        correctTabRect =
                            GetCorrectTabRect( GetTabRect( SelectedIndex ), SelectedIndex );

                        solidBrush1.Color = _SelectedItemColor;
                        pen.Color = _SelectedBorderColor;
                        graphics.FillRectangle( solidBrush1, correctTabRect );
                        graphics.DrawRectangle( pen, correctTabRect );
                        DrawPolygon( e );

                        if( _DrawSelectedLine )
                        {
                            pen.Color = _SelectedItemLineColor;
                            pen.Width = _SelectedItemLineWidth;

                            graphics.DrawLine( pen, correctTabRect.X,
                                checked( checked( correctTabRect.Y + correctTabRect.Height )
                                    - checked(
                                        (int)Math.Round( (double)_SelectedItemLineWidth / 2 ) ) ),
                                checked( checked( correctTabRect.X + correctTabRect.Width ) + 1 ),
                                checked( checked( correctTabRect.Y + correctTabRect.Height )
                                    - checked(
                                        (int)Math.Round( (double)_SelectedItemLineWidth / 2 ) ) ) );
                        }

                        using( var solidBrush3 = new SolidBrush( _SelectedForeColor ) )
                        {
                            var font = new Font( TabPages[ SelectedIndex ].Font.FontFamily,
                                TabPages[ SelectedIndex ].Font.Size, _SelectedTabIsBold
                                    ? FontStyle.Bold
                                    : FontStyle.Regular );

                            if( ImageList != null )
                            {
                                if( TabPages[ SelectedIndex ].ImageIndex != -1 )
                                {
                                    if( ImageList.Images[ TabPages[ SelectedIndex ].ImageIndex ]
                                       != null )
                                    {
                                        var image =
                                            ImageList.Images[
                                                TabPages[ SelectedIndex ].ImageIndex ];

                                        rectangle = new Rectangle(
                                            checked( correctTabRect.X
                                                + checked( (int)Math.Round(
                                                    (double)_ImageWidth / 2 ) ) ),
                                            checked( checked( checked( correctTabRect.Y
                                                        + checked( (int)Math.Round(
                                                            (double)correctTabRect.Height / 2 ) ) )
                                                    - checked( (int)Math.Round( (double)_ImageWidth
                                                        / 2 ) ) )
                                                + 1 ), _ImageWidth, _ImageWidth );

                                        graphics.DrawImage( image, rectangle );

                                        correctTabRect = new Rectangle(
                                            checked( checked( correctTabRect.X + 20 )
                                                + _ImageWidth ), correctTabRect.Y,
                                            checked( correctTabRect.Width
                                                - checked( 20 + _ImageWidth ) ),
                                            correctTabRect.Height );
                                    }
                                }
                                else if( _ItemTextAlign == StringAlignment.Near )
                                {
                                    correctTabRect = new Rectangle(
                                        checked( correctTabRect.X
                                            + checked( (int)Math.Round( (double)_ImageWidth
                                                / 2 ) ) ), correctTabRect.Y,
                                        checked( correctTabRect.Width
                                            - checked( (int)Math.Round( (double)_ImageWidth
                                                / 2 ) ) ), correctTabRect.Height );
                                }
                            }

                            graphics.DrawString( TabPages[ SelectedIndex ].Text, font, solidBrush3,
                                correctTabRect, stringFormat1 );

                            font.Dispose( );
                        }
                    }

                    stringFormat1.Dispose( );
                }
            }

            base.OnPaint( e );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TabControl.Selecting" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> that contains the event data.</param>
        protected override void OnSelecting( TabControlCancelEventArgs e )
        {
            if( _HeaderIndexes.Contains( e.TabPageIndex ) )
            {
                e.Cancel = true;
            }
            else if( _HasAnimation )
            {
                if( TabPages.Count > 0 )
                {
                    if( _LastIndex >= 0 )
                    {
                        if( _LastIndex >= e.TabPageIndex )
                        {
                            DoAnimation( TabPages[ _LastIndex ], TabPages[ e.TabPageIndex ], true );
                        }
                        else
                        {
                            DoAnimation( TabPages[ _LastIndex ], TabPages[ e.TabPageIndex ],
                                false );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes the header.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveHeader( int index )
        {
            try
            {
                _HeaderIndexes.Remove( index );
                Invalidate( );
            }
            catch( Exception exception )
            {
                ProjectData.SetProjectError( exception );
                ProjectData.ClearProjectError( );
            }
        }

        /// <summary>
        /// Occurs when [animation complete].
        /// </summary>
        public event AnimationCompleteEventHandler AnimationComplete;

        /// <summary>
        /// Delegate AnimationCompleteEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void AnimationCompleteEventHandler( object sender, EventArgs e );
    }
}