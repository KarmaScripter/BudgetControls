// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetPieChartSegment.cs" company="Terry D. Eppler">
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
//   BudgetPieChartSegment.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetPieChartSegment.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class BudgetPieChartSegment : INotifyPropertyChanged
    {
        /// <summary>
        /// The enc list
        /// </summary>
        private static List<WeakReference> __ENCList;

        /// <summary>
        /// The value
        /// </summary>
        private int _value;

        /// <summary>
        /// The fill color
        /// </summary>
        private Color _fillColor;

        /// <summary>
        /// The name
        /// </summary>
        private string _name;

        /// <summary>
        /// The maximum stored
        /// </summary>
        private int maxStored;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _borderColor;

        /// <summary>
        /// The style
        /// </summary>
        private eStyle _style;

        /// <summary>
        /// The fill style
        /// </summary>
        private HatchStyle _fillStyle;

        /// <summary>
        /// The use fill style
        /// </summary>
        private bool _UseFillStyle;

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                if( _borderColor != value )
                {
                    _borderColor = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "BorderColor" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        public Color FillColor
        {
            get
            {
                return _fillColor;
            }
            set
            {
                if( _fillColor != value )
                {
                    _fillColor = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "FillColor" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the fill style.
        /// </summary>
        /// <value>The fill style.</value>
        public HatchStyle FillStyle
        {
            get
            {
                return _fillStyle;
            }
            set
            {
                if( _fillStyle != value )
                {
                    _fillStyle = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "FillStyle" ) );
                    }
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
                return _name;
            }
            set
            {
                if( Operators.CompareString( _name, value, false ) != 0 )
                {
                    _name = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Name" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public eStyle Style
        {
            get
            {
                return _style;
            }
            set
            {
                if( _style != value )
                {
                    _style = value;

                    if( _style == eStyle.AbstractBlue )
                    {
                        ApplyStyle( eStyle.AbstractBlue );
                    }
                    else if( _style == eStyle.AbstractPurple )
                    {
                        ApplyStyle( eStyle.AbstractPurple );
                    }
                    else if( _style == eStyle.AbstractRed )
                    {
                        ApplyStyle( eStyle.AbstractRed );
                    }
                    else if( _style == eStyle.LightBlue )
                    {
                        ApplyStyle( eStyle.LightBlue );
                    }
                    else if( _style == eStyle.LightOrange )
                    {
                        ApplyStyle( eStyle.LightOrange );
                    }
                    else if( _style == eStyle.LightRed )
                    {
                        ApplyStyle( eStyle.LightRed );
                    }
                    else if( _style == eStyle.LightCyan )
                    {
                        ApplyStyle( eStyle.LightCyan );
                    }
                    else if( _style == eStyle.DarkBlue )
                    {
                        ApplyStyle( eStyle.DarkBlue );
                    }
                    else if( _style == eStyle.SoapGreen )
                    {
                        ApplyStyle( eStyle.SoapGreen );
                    }
                    else if( _style != eStyle.SoapRed )
                    {
                        _style = eStyle.Custom;
                    }
                    else
                    {
                        ApplyStyle( eStyle.SoapRed );
                    }

                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "Style" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use fill style].
        /// </summary>
        /// <value><c>true</c> if [use fill style]; otherwise, <c>false</c>.</value>
        public bool UseFillStyle
        {
            get
            {
                return _UseFillStyle;
            }
            set
            {
                if( _UseFillStyle != value )
                {
                    _UseFillStyle = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "UseFillStyle" ) );
                    }
                }
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
                return _value;
            }
            set
            {
                if( _value != value )
                {
                    _value = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "Value" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Initializes static members of the <see cref="BudgetPieChartSegment"/> class.
        /// </summary>
        [ DebuggerNonUserCode ]
        static BudgetPieChartSegment( )
        {
            BudgetPieChartSegment.__ENCList = new List<WeakReference>( );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetPieChartSegment"/> class.
        /// </summary>
        public BudgetPieChartSegment( )
        {
            BudgetPieChartSegment.__ENCAddToList( this );
            _style = eStyle.Custom;
            _fillStyle = HatchStyle.BackwardDiagonal;
            _UseFillStyle = false;
            _value = 10;
            _fillColor = Color.FromArgb( 255, 129, 0 );
            _borderColor = Color.FromArgb( 255, 129, 0 );
            _style = eStyle.LightOrange;
        }

        /// <summary>
        /// Encs the add to list.
        /// </summary>
        /// <param name="value">The value.</param>
        [ DebuggerNonUserCode ]
        private static void __ENCAddToList( object value )
        {
            var _ENCList = BudgetPieChartSegment.__ENCList;
            Monitor.Enter( _ENCList );

            try
            {
                if( BudgetPieChartSegment.__ENCList.Count
                   == BudgetPieChartSegment.__ENCList.Capacity )
                {
                    var item = 0;
                    var count = checked( BudgetPieChartSegment.__ENCList.Count - 1 );

                    for( var i = 0; i <= count; i = checked( i + 1 ) )
                    {
                        if( BudgetPieChartSegment.__ENCList[ i ].IsAlive )
                        {
                            if( i != item )
                            {
                                BudgetPieChartSegment.__ENCList[ item ] =
                                    BudgetPieChartSegment.__ENCList[ i ];
                            }

                            item = checked( item + 1 );
                        }
                    }

                    BudgetPieChartSegment.__ENCList.RemoveRange( item,
                        checked( BudgetPieChartSegment.__ENCList.Count - item ) );

                    BudgetPieChartSegment.__ENCList.Capacity =
                        BudgetPieChartSegment.__ENCList.Count;
                }

                BudgetPieChartSegment.__ENCList.Add(
                    new WeakReference( RuntimeHelpers.GetObjectValue( value ) ) );
            }
            finally
            {
                Monitor.Exit( _ENCList );
            }
        }

        /// <summary>
        /// Applies the style.
        /// </summary>
        /// <param name="eStyle">The e style.</param>
        private void ApplyStyle( eStyle eStyle )
        {
            switch( eStyle )
            {
                case eStyle.LightCyan:
                {
                    _fillColor = Color.FromArgb( 0, 255, 155 );
                    _borderColor = Color.FromArgb( 0, 255, 155 );
                    break;
                }
                case eStyle.LightBlue:
                {
                    _fillColor = Color.FromArgb( 30, 151, 227 );
                    _borderColor = Color.FromArgb( 30, 151, 227 );
                    break;
                }
                case eStyle.LightRed:
                {
                    _fillColor = Color.FromArgb( 255, 42, 0 );
                    _borderColor = Color.FromArgb( 255, 42, 0 );
                    break;
                }
                case eStyle.LightOrange:
                {
                    _fillColor = Color.FromArgb( 255, 129, 0 );
                    _borderColor = Color.FromArgb( 255, 129, 0 );
                    break;
                }
                case eStyle.AbstractRed:
                {
                    _fillColor = Color.FromArgb( 91, 46, 49 );
                    _borderColor = Color.FromArgb( 193, 66, 72 );
                    break;
                }
                case eStyle.AbstractBlue:
                {
                    _fillColor = Color.FromArgb( 33, 73, 130 );
                    _borderColor = Color.FromArgb( 50, 109, 212 );
                    break;
                }
                case eStyle.AbstractPurple:
                {
                    _fillColor = Color.FromArgb( 79, 50, 136 );
                    _borderColor = Color.FromArgb( 124, 68, 208 );
                    break;
                }
                case eStyle.DarkBlue:
                {
                    _fillColor = Color.FromArgb( 40, 40, 40 );
                    _borderColor = Color.FromArgb( 0, 164, 240 );
                    break;
                }
                case eStyle.SoapRed:
                {
                    _fillColor = Color.FromArgb( 255, 63, 53 );
                    _borderColor = Color.White;
                    break;
                }
                case eStyle.SoapGreen:
                {
                    _fillColor = Color.FromArgb( 21, 159, 79 );
                    _borderColor = Color.White;
                    break;
                }
            }
        }

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Enum eStyle
        /// </summary>
        public enum eStyle
        {
            /// <summary>
            /// The light cyan
            /// </summary>
            LightCyan,

            /// <summary>
            /// The light blue
            /// </summary>
            LightBlue,

            /// <summary>
            /// The light red
            /// </summary>
            LightRed,

            /// <summary>
            /// The light orange
            /// </summary>
            LightOrange,

            /// <summary>
            /// The abstract red
            /// </summary>
            AbstractRed,

            /// <summary>
            /// The abstract blue
            /// </summary>
            AbstractBlue,

            /// <summary>
            /// The abstract purple
            /// </summary>
            AbstractPurple,

            /// <summary>
            /// The dark blue
            /// </summary>
            DarkBlue,

            /// <summary>
            /// The SOAP red
            /// </summary>
            SoapRed,

            /// <summary>
            /// The SOAP green
            /// </summary>
            SoapGreen,

            /// <summary>
            /// The custom
            /// </summary>
            Custom
        }
    }
}