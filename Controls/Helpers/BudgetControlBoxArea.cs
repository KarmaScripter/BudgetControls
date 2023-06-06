// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetControlBoxArea.cs" company="Terry D. Eppler">
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
//   BudgetControlBoxArea.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetControlBoxArea.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class BudgetControlBoxArea : INotifyPropertyChanged
    {
        /// <summary>
        /// The enc list
        /// </summary>
        private static List<WeakReference> __ENCList;

        /// <summary>
        /// The area image
        /// </summary>
        private Image _AreaImage;

        /// <summary>
        /// The area size
        /// </summary>
        private Size _AreaSize;

        /// <summary>
        /// The default color
        /// </summary>
        private Color _DefaultColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The pressed color
        /// </summary>
        private Color _PressedColor;

        /// <summary>
        /// The icon color
        /// </summary>
        private Color _IconColor;

        /// <summary>
        /// The area type
        /// </summary>
        private ControlBoxAreaType _AreaType;

        /// <summary>
        /// The name
        /// </summary>
        private string _Name;

        /// <summary>
        /// The highlight color
        /// </summary>
        private Color _HighlightColor;

        /// <summary>
        /// The is highlighted
        /// </summary>
        private bool _IsHighlighted;

        /// <summary>
        /// The enabled
        /// </summary>
        private bool _Enabled;

        /// <summary>
        /// The invert icon color
        /// </summary>
        private bool _InvertIconColor;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// Gets or sets the area image.
        /// </summary>
        /// <value>The area image.</value>
        public Image AreaImage
        {
            get
            {
                return _AreaImage;
            }
            set
            {
                _AreaImage = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "AreaImage" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the area.
        /// </summary>
        /// <value>The size of the area.</value>
        public Size AreaSize
        {
            get
            {
                return _AreaSize;
            }
            set
            {
                _AreaSize = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this, new PropertyChangedEventArgs( "AreaSize" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the area.
        /// </summary>
        /// <value>The type of the area.</value>
        public ControlBoxAreaType AreaType
        {
            get
            {
                return _AreaType;
            }
            set
            {
                _AreaType = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this, new PropertyChangedEventArgs( "AreaType" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the default color.
        /// </summary>
        /// <value>The default color.</value>
        public Color DefaultColor
        {
            get
            {
                return _DefaultColor;
            }
            set
            {
                _DefaultColor = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "DefaultColor" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BudgetControlBoxArea"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Enabled" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the highlight.
        /// </summary>
        /// <value>The color of the highlight.</value>
        public Color HighlightColor
        {
            get
            {
                return _HighlightColor;
            }
            set
            {
                _HighlightColor = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "HighlightColor" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        public Color HoverColor
        {
            get
            {
                return _HoverColor;
            }
            set
            {
                _HoverColor = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "HoverColor" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the icon.
        /// </summary>
        /// <value>The color of the icon.</value>
        public Color IconColor
        {
            get
            {
                return _IconColor;
            }
            set
            {
                _IconColor = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "IconColor" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [invert icon color].
        /// </summary>
        /// <value><c>true</c> if [invert icon color]; otherwise, <c>false</c>.</value>
        public bool InvertIconColor
        {
            get
            {
                return _InvertIconColor;
            }
            set
            {
                _InvertIconColor = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "InvertIconColor" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is highlighted.
        /// </summary>
        /// <value><c>true</c> if this instance is highlighted; otherwise, <c>false</c>.</value>
        public bool IsHighlighted
        {
            get
            {
                return _IsHighlighted;
            }
            set
            {
                _IsHighlighted = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "IsHighlighted" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Name" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the pressed.
        /// </summary>
        /// <value>The color of the pressed.</value>
        public Color PressedColor
        {
            get
            {
                return _PressedColor;
            }
            set
            {
                _PressedColor = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "PressedColor" ) );
                }
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
                return _Style;
            }
            set
            {
                if( value == Design.Style.Light )
                {
                    _DefaultColor = Color.White;
                    _HoverColor = Color.FromArgb( 240, 240, 240 );
                    _PressedColor = Color.FromArgb( 0, 122, 204 );
                    _IconColor = Color.Black;
                }
                else if( value == Design.Style.Dark )
                {
                    _DefaultColor = Color.FromArgb( 40, 40, 40 );
                    _HoverColor = Color.FromArgb( 63, 63, 63 );
                    _PressedColor = Color.FromArgb( 0, 122, 204 );
                    _IconColor = Color.FromArgb( 241, 241, 241 );
                }

                _Style = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Style" ) );
                }
            }
        }

        /// <summary>
        /// Initializes static members of the <see cref="BudgetControlBoxArea"/> class.
        /// </summary>
        [ DebuggerNonUserCode ]
        static BudgetControlBoxArea( )
        {
            BudgetControlBoxArea.__ENCList = new List<WeakReference>( );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetControlBoxArea"/> class.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="size">The size.</param>
        /// <param name="defaultcolor">The defaultcolor.</param>
        /// <param name="hovercolor">The hovercolor.</param>
        /// <param name="pressedcolor">The pressedcolor.</param>
        /// <param name="areatype">The areatype.</param>
        /// <param name="name">The name.</param>
        /// <param name="highlightarea">if set to <c>true</c> [highlightarea].</param>
        public BudgetControlBoxArea(
            Image img, Size size, Color defaultcolor, Color hovercolor,
            Color pressedcolor, ControlBoxAreaType areatype = 0, string name = "",
            bool highlightarea = false )
        {
            var size1 = new Size( );
            var color = new Color( );
            BudgetControlBoxArea.__ENCAddToList( this );
            _AreaImage = null;
            _AreaSize = new Size( 32, 32 );
            _DefaultColor = Color.White;
            _HoverColor = Color.FromArgb( 240, 240, 240 );
            _PressedColor = Color.FromArgb( 0, 122, 204 );
            _IconColor = Color.Black;
            _AreaType = ControlBoxAreaType.Custom;
            _Name = string.Empty;
            _HighlightColor = Color.FromArgb( 0, 122, 204 );
            _IsHighlighted = false;
            _Enabled = true;
            _InvertIconColor = false;
            _Style = Design.Style.Light;

            if( img != null )
            {
                _AreaImage = img;
            }

            if( size != size1 )
            {
                _AreaSize = size;
            }

            if( defaultcolor != color )
            {
                _DefaultColor = defaultcolor;
            }

            if( hovercolor != color )
            {
                _HoverColor = hovercolor;
            }

            if( _PressedColor != color )
            {
                _PressedColor = pressedcolor;
            }

            _IsHighlighted = highlightarea;
            _HighlightColor = _PressedColor;
            _AreaType = areatype;
            _Name = name;
            var propertyChangedEventHandler = PropertyChanged;

            if( propertyChangedEventHandler != null )
            {
                propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Items" ) );
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetControlBoxArea"/> class.
        /// </summary>
        /// <param name="areatype">The areatype.</param>
        /// <param name="style">The style.</param>
        /// <param name="name">The name.</param>
        /// <param name="highlightarea">if set to <c>true</c> [highlightarea].</param>
        public BudgetControlBoxArea(
            ControlBoxAreaType areatype, Design.Style style, string name = "",
            bool highlightarea = false )
        {
            BudgetControlBoxArea.__ENCAddToList( this );
            _AreaImage = null;
            _AreaSize = new Size( 32, 32 );
            _DefaultColor = Color.White;
            _HoverColor = Color.FromArgb( 240, 240, 240 );
            _PressedColor = Color.FromArgb( 0, 122, 204 );
            _IconColor = Color.Black;
            _AreaType = ControlBoxAreaType.Custom;
            _Name = string.Empty;
            _HighlightColor = Color.FromArgb( 0, 122, 204 );
            _IsHighlighted = false;
            _Enabled = true;
            _InvertIconColor = false;
            _Style = Design.Style.Light;

            if( style == Design.Style.Light )
            {
                _DefaultColor = Design.BudgetColors.LightDefault;
                _HoverColor = Design.BudgetColors.LightHover;
                _PressedColor = Design.BudgetColors.AccentBlue;
                _IconColor = Design.BudgetColors.LightIcon;
            }
            else if( style == Design.Style.Dark )
            {
                _DefaultColor = Design.BudgetColors.DarkDefault;
                _HoverColor = Design.BudgetColors.DarkHover;
                _PressedColor = Design.BudgetColors.AccentBlue;
                _IconColor = Design.BudgetColors.LightHover;
            }

            _HighlightColor = _PressedColor;
            _IsHighlighted = highlightarea;
            _Name = name;
            _AreaType = areatype;
            var propertyChangedEventHandler = PropertyChanged;

            if( propertyChangedEventHandler != null )
            {
                propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Items" ) );
            }
        }

        /// <summary>
        /// Encs the add to list.
        /// </summary>
        /// <param name="value">The value.</param>
        [ DebuggerNonUserCode ]
        private static void __ENCAddToList( object value )
        {
            var _ENCList = BudgetControlBoxArea.__ENCList;
            Monitor.Enter( _ENCList );

            try
            {
                if( BudgetControlBoxArea.__ENCList.Count
                   == BudgetControlBoxArea.__ENCList.Capacity )
                {
                    var item = 0;
                    var count = checked( BudgetControlBoxArea.__ENCList.Count - 1 );

                    for( var i = 0; i <= count; i = checked( i + 1 ) )
                    {
                        if( BudgetControlBoxArea.__ENCList[ i ].IsAlive )
                        {
                            if( i != item )
                            {
                                BudgetControlBoxArea.__ENCList[ item ] =
                                    BudgetControlBoxArea.__ENCList[ i ];
                            }

                            item = checked( item + 1 );
                        }
                    }

                    BudgetControlBoxArea.__ENCList.RemoveRange( item,
                        checked( BudgetControlBoxArea.__ENCList.Count - item ) );

                    BudgetControlBoxArea.__ENCList.Capacity = BudgetControlBoxArea.__ENCList.Count;
                }

                BudgetControlBoxArea.__ENCList.Add(
                    new WeakReference( RuntimeHelpers.GetObjectValue( value ) ) );
            }
            finally
            {
                Monitor.Exit( _ENCList );
            }
        }

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Enum ControlBoxAreaType
        /// </summary>
        public enum ControlBoxAreaType
        {
            /// <summary>
            /// The custom
            /// </summary>
            Custom,

            /// <summary>
            /// The minimize
            /// </summary>
            Minimize,

            /// <summary>
            /// The maximize
            /// </summary>
            Maximize,

            /// <summary>
            /// The close
            /// </summary>
            Close
        }
    }
}