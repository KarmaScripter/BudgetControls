// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroWebChartPoint.cs" company="Terry D. Eppler">
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
//   MetroWebChartPoint.cs
// </summary>
// ******************************************************************************************

using System.ComponentModel;

namespace BudgetExecution
{
    /// <summary>
    /// Class MetroWebChartPoint.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MetroWebChartPoint : INotifyPropertyChanged
    {

        /// <summary>
        /// The value
        /// </summary>
        private int _value = 0;

        /// <summary>
        /// The text
        /// </summary>
        private string _Text = string.Empty;

        /// <summary>
        /// The color
        /// </summary>
        private System.Drawing.Color _Color = Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, 0.3f);

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public System.Drawing.Color Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				this._Color = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Color"));
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
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Value"));
				}
			}
		}

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
	}
}