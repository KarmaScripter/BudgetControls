// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTrackerPath.cs" company="Terry D. Eppler">
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
//   BudgetTrackerPath.cs
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
using System.Runtime.CompilerServices;
using System.Threading;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetTrackerPath.
    /// </summary>
    /// <seealso cref="int" />
    /// <seealso cref="System.Collections.ICollection" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class BudgetTrackerPath : IEnumerable<int>, ICollection, INotifyPropertyChanged
    {
        /// <summary>
        /// The enc list
        /// </summary>
        private static List<WeakReference> __ENCList;

        /// <summary>
        /// The values
        /// </summary>
        private Queue<int> values;

        /// <summary>
        /// The maximum stored
        /// </summary>
        private int maxStored;

        /// <summary>
        /// The line color
        /// </summary>
        private Color _lineColor;

        /// <summary>
        /// The fill color
        /// </summary>
        private Color _fillColor;

        /// <summary>
        /// The is filled
        /// </summary>
        private bool _isFilled;

        /// <summary>
        /// The line width
        /// </summary>
        private float _lineWidth;

        /// <summary>
        /// The name
        /// </summary>
        private string _name;

        /// <summary>
        /// The maximum
        /// </summary>
        private int _maximum;

        /// <summary>
        /// The alert above
        /// </summary>
        private int _AlertAbove;

        /// <summary>
        /// The alert under
        /// </summary>
        private int _AlertUnder;

        /// <summary>
        /// The perform alers
        /// </summary>
        private bool _performAlers;

        /// <summary>
        /// The is over value
        /// </summary>
        private bool _IsOverValue;

        /// <summary>
        /// The is under value
        /// </summary>
        private bool _IsUnderValue;

        /// <summary>
        /// The style
        /// </summary>
        private PathStyle _Style;

        /// <summary>
        /// The drawing style
        /// </summary>
        private DashStyle _DrawingStyle;

        /// <summary>
        /// The dash offset
        /// </summary>
        private float _DashOffset;

        /// <summary>
        /// Gets or sets the alert above.
        /// </summary>
        /// <value>The alert above.</value>
        [ DefaultValue( 100 ) ]
        public int AlertAbove
        {
            get
            {
                return _AlertAbove;
            }
            set
            {
                if( _AlertAbove != value )
                {
                    _AlertAbove = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "AlertAbove" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the alert under.
        /// </summary>
        /// <value>The alert under.</value>
        [ DefaultValue( 0 ) ]
        public int AlertUnder
        {
            get
            {
                return _AlertUnder;
            }
            set
            {
                if( _AlertUnder != value )
                {
                    _AlertUnder = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "AlertUnder" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                return values.Count;
            }
        }

        /// <summary>
        /// Gets or sets the dash offset.
        /// </summary>
        /// <value>The dash offset.</value>
        public float DashOffset
        {
            get
            {
                return _DashOffset;
            }
            set
            {
                if( value != _DashOffset )
                {
                    _DashOffset = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "DashOffset" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the drawing style.
        /// </summary>
        /// <value>The drawing style.</value>
        public DashStyle DrawingStyle
        {
            get
            {
                return _DrawingStyle;
            }
            set
            {
                if( value != _DrawingStyle )
                {
                    _DrawingStyle = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "DrawingStyle" ) );
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
        /// Gets or sets a value indicating whether this instance is filled.
        /// </summary>
        /// <value><c>true</c> if this instance is filled; otherwise, <c>false</c>.</value>
        public bool IsFilled
        {
            get
            {
                return _isFilled;
            }
            set
            {
                if( _isFilled != value )
                {
                    _isFilled = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "IsFilled" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is over value.
        /// </summary>
        /// <value><c>true</c> if this instance is over value; otherwise, <c>false</c>.</value>
        public bool IsOverValue
        {
            get
            {
                return _IsOverValue;
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        /// <value><c>true</c> if this instance is synchronized; otherwise, <c>false</c>.</value>
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is under value.
        /// </summary>
        /// <value><c>true</c> if this instance is under value; otherwise, <c>false</c>.</value>
        public bool IsUnderValue
        {
            get
            {
                return _IsUnderValue;
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor
        {
            get
            {
                return _lineColor;
            }
            set
            {
                if( _lineColor != value )
                {
                    _lineColor = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "LineColor" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        public float LineWidth
        {
            get
            {
                return _lineWidth;
            }
            set
            {
                if( _lineWidth != value )
                {
                    _lineWidth = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "LineWidth" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                if( _maximum != value )
                {
                    _maximum = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "Maximum" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum stored values.
        /// </summary>
        /// <value>The maximum stored values.</value>
        public int MaxStoredValues
        {
            get
            {
                return maxStored;
            }
            set
            {
                if( maxStored != value )
                {
                    maxStored = value;

                    if( maxStored < values.Count )
                    {
                        CutQueue( );
                        var propertyChangedEventHandler = PropertyChanged;

                        if( propertyChangedEventHandler != null )
                        {
                            propertyChangedEventHandler( this,
                                new PropertyChangedEventArgs( "Items" ) );
                        }
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
        /// Gets or sets a value indicating whether [perform alerts].
        /// </summary>
        /// <value><c>true</c> if [perform alerts]; otherwise, <c>false</c>.</value>
        [ DefaultValue( false ) ]
        public bool PerformAlerts
        {
            get
            {
                return _performAlers;
            }
            set
            {
                if( _performAlers != value )
                {
                    _performAlers = value;
                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "PerformAlerts" ) );
                    }
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
        [ Description( "Gibt an, welches Farb-Schema der BudgetTracker besitzen soll." ) ]
        public PathStyle Style
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

                    switch( _Style )
                    {
                        case PathStyle.Light:
                        {
                            LineColor = Color.FromArgb( 0, 164, 240 );
                            FillColor = Color.FromArgb( 35, 0, 164, 240 );
                            break;
                        }
                        case PathStyle.Dark:
                        {
                            LineColor = Color.FromArgb( 0, 164, 240 );
                            FillColor = Color.FromArgb( 35, 0, 164, 240 );
                            break;
                        }
                        case PathStyle.CPU:
                        {
                            LineColor = Color.FromArgb( 17, 125, 187 );
                            FillColor = Color.FromArgb( 35, 152, 207, 249 );
                            break;
                        }
                        case PathStyle.Disk:
                        {
                            LineColor = Color.FromArgb( 77, 166, 12 );
                            FillColor = Color.FromArgb( 35, 103, 239, 0 );
                            break;
                        }
                        case PathStyle.Memory:
                        {
                            LineColor = Color.FromArgb( 149, 40, 180 );
                            FillColor = Color.FromArgb( 35, 242, 150, 242 );
                            break;
                        }
                        case PathStyle.Ethernet:
                        {
                            LineColor = Color.FromArgb( 167, 79, 1 );
                            FillColor = Color.FromArgb( 35, 165, 118, 77 );
                            break;
                        }
                    }

                    var propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "LineColor" ) );
                    }

                    propertyChangedEventHandler = PropertyChanged;

                    if( propertyChangedEventHandler != null )
                    {
                        propertyChangedEventHandler( this,
                            new PropertyChangedEventArgs( "Style" ) );
                    }
                }
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The synchronize root.</value>
        public object SyncRoot
        {
            get
            {
                return ( (ICollection)values ).SyncRoot;
            }
        }

        /// <summary>
        /// Initializes static members of the <see cref="BudgetTrackerPath"/> class.
        /// </summary>
        [ DebuggerNonUserCode ]
        static BudgetTrackerPath( )
        {
            BudgetTrackerPath.__ENCList = new List<WeakReference>( );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTrackerPath"/> class.
        /// </summary>
        public BudgetTrackerPath( )
        {
            BudgetTrackerPath.__ENCAddToList( this );
            _AlertAbove = 100;
            _AlertUnder = 0;
            _performAlers = false;
            _IsOverValue = false;
            _IsUnderValue = false;
            _DrawingStyle = DashStyle.Solid;
            _DashOffset = 2f;
            values = new Queue<int>( );
            maxStored = 100;
            _maximum = 100;
            _lineWidth = 1f;
            _isFilled = false;
        }

        /// <summary>
        /// Encs the add to list.
        /// </summary>
        /// <param name="value">The value.</param>
        [ DebuggerNonUserCode ]
        private static void __ENCAddToList( object value )
        {
            var _ENCList = BudgetTrackerPath.__ENCList;
            Monitor.Enter( _ENCList );

            try
            {
                if( BudgetTrackerPath.__ENCList.Count == BudgetTrackerPath.__ENCList.Capacity )
                {
                    var item = 0;
                    var count = checked( BudgetTrackerPath.__ENCList.Count - 1 );

                    for( var i = 0; i <= count; i = checked( i + 1 ) )
                    {
                        if( BudgetTrackerPath.__ENCList[ i ].IsAlive )
                        {
                            if( i != item )
                            {
                                BudgetTrackerPath.__ENCList[ item ] =
                                    BudgetTrackerPath.__ENCList[ i ];
                            }

                            item = checked( item + 1 );
                        }
                    }

                    BudgetTrackerPath.__ENCList.RemoveRange( item,
                        checked( BudgetTrackerPath.__ENCList.Count - item ) );

                    BudgetTrackerPath.__ENCList.Capacity = BudgetTrackerPath.__ENCList.Count;
                }

                BudgetTrackerPath.__ENCList.Add(
                    new WeakReference( RuntimeHelpers.GetObjectValue( value ) ) );
            }
            finally
            {
                Monitor.Exit( _ENCList );
            }
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add( int value )
        {
            EventHandler<BudgetTrackerPathAlertEventArgs> eventHandler;
            values.Enqueue( value );

            if( maxStored < values.Count )
            {
                CutQueue( );
            }

            if( PerformAlerts )
            {
                if( value > AlertAbove )
                {
                    eventHandler = AboveAlerted;

                    if( eventHandler != null )
                    {
                        eventHandler( this, new BudgetTrackerPathAlertEventArgs( this, value ) );
                    }
                }

                if( value < AlertUnder )
                {
                    eventHandler = UnderAlerted;

                    if( eventHandler != null )
                    {
                        eventHandler( this, new BudgetTrackerPathAlertEventArgs( this, value ) );
                    }
                }
            }

            if( value <= AlertAbove )
            {
                _IsOverValue = false;
            }
            else
            {
                _IsOverValue = true;
            }

            if( value >= AlertUnder )
            {
                _IsUnderValue = false;
            }
            else
            {
                _IsUnderValue = true;
            }

            var propertyChangedEventHandler = PropertyChanged;

            if( propertyChangedEventHandler != null )
            {
                propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Items" ) );
            }
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        public void CopyTo( int[ ] array, int index )
        {
            values.CopyTo( array, index );
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo( Array array, int index )
        {
            values.CopyTo( (int[ ])array, index );
        }

        /// <summary>
        /// Cuts the queue.
        /// </summary>
        private void CutQueue( )
        {
            while( values.Count > maxStored )
            {
                values.Dequeue( );
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<int> GetEnumerator( )
        {
            return values.GetEnumerator( );
        }

        /// <summary>
        /// Gets the enumerator1.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        private IEnumerator GetEnumerator1( )
        {
            return values.GetEnumerator( );
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator( )
        {
            return values.GetEnumerator( );
        }

        /// <summary>
        /// Occurs when [above alerted].
        /// </summary>
        public event EventHandler<BudgetTrackerPathAlertEventArgs> AboveAlerted;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [under alerted].
        /// </summary>
        public event EventHandler<BudgetTrackerPathAlertEventArgs> UnderAlerted;

        /// <summary>
        /// Enum PathStyle
        /// </summary>
        public enum PathStyle
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
            /// The cpu
            /// </summary>
            CPU,

            /// <summary>
            /// The disk
            /// </summary>
            Disk,

            /// <summary>
            /// The memory
            /// </summary>
            Memory,

            /// <summary>
            /// The ethernet
            /// </summary>
            Ethernet,

            /// <summary>
            /// The custom
            /// </summary>
            Custom
        }
    }
}