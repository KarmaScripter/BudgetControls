// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTaskPoint.cs" company="Terry D. Eppler">
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
//   BudgetTaskPoint.cs
// </summary>
// ******************************************************************************************

using System.ComponentModel;
using System.Drawing;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetTaskPoint.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class BudgetTaskPoint : INotifyPropertyChanged
    {
        /// <summary>
        /// The finished
        /// </summary>
        private bool _Finished;

        /// <summary>
        /// The enabled
        /// </summary>
        private bool _Enabled;

        /// <summary>
        /// The circe color
        /// </summary>
        private Color _CirceColor;

        /// <summary>
        /// The icon
        /// </summary>
        private Image _Icon;

        /// <summary>
        /// The circle width
        /// </summary>
        private int _CircleWidth;

        /// <summary>
        /// The text
        /// </summary>
        private string _Text;

        /// <summary>
        /// Gets or sets the color of the circe.
        /// </summary>
        /// <value>The color of the circe.</value>
        public Color CirceColor
        {
            get
            {
                return _CirceColor;
            }
            set
            {
                _CirceColor = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "CirceColor" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the circle.
        /// </summary>
        /// <value>The width of the circle.</value>
        public int CircleWidth
        {
            get
            {
                return _CircleWidth;
            }
            set
            {
                _CircleWidth = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this,
                        new PropertyChangedEventArgs( "CircleWidth" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BudgetTaskPoint"/> is enabled.
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
        /// Gets or sets a value indicating whether this <see cref="BudgetTaskPoint"/> is finished.
        /// </summary>
        /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
        public bool Finished
        {
            get
            {
                return _Finished;
            }
            set
            {
                _Finished = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Finished" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public Image Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                _Icon = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Icon" ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                var propertyChangedEventHandler = PropertyChanged;

                if( propertyChangedEventHandler != null )
                {
                    propertyChangedEventHandler( this, new PropertyChangedEventArgs( "Text" ) );
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTaskPoint"/> class.
        /// </summary>
        public BudgetTaskPoint( )
        {
            _Finished = false;
            _Enabled = true;
            _CirceColor = Design.BudgetColors.AccentBlue;
            _Icon = null;
            _CircleWidth = 20;
            _Text = string.Empty;
        }

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}