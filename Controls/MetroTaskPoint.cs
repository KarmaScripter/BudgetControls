// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroTaskPoint.cs" company="Terry D. Eppler">
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
//   MetroTaskPoint.cs
// </summary>
// ******************************************************************************************

using System.ComponentModel;
using System.Drawing;

namespace BudgetExecution
{


    /// <summary>
    /// Class MetroTaskPoint.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MetroTaskPoint : INotifyPropertyChanged
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
				return this._CirceColor;
			}
			set
			{
				this._CirceColor = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("CirceColor"));
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
				return this._CircleWidth;
			}
			set
			{
				this._CircleWidth = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("CircleWidth"));
				}
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MetroTaskPoint"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
		{
			get
			{
				return this._Enabled;
			}
			set
			{
				this._Enabled = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Enabled"));
				}
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MetroTaskPoint"/> is finished.
        /// </summary>
        /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
        public bool Finished
		{
			get
			{
				return this._Finished;
			}
			set
			{
				this._Finished = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Finished"));
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
				return this._Icon;
			}
			set
			{
				this._Icon = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Icon"));
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
				return this._Text;
			}
			set
			{
				this._Text = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Text"));
				}
			}
		}


        /// <summary>
        /// Initializes a new instance of the <see cref="MetroTaskPoint"/> class.
        /// </summary>
        public MetroTaskPoint()
		{
			this._Finished = false;
			this._Enabled = true;
			this._CirceColor = Design.MetroColors.AccentBlue;
			this._Icon = null;
			this._CircleWidth = 20;
			this._Text = string.Empty;
		}


        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
	}
    


}