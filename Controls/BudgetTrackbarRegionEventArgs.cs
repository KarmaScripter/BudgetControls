// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTrackbarRegionEventArgs.cs" company="Terry D. Eppler">
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
//   BudgetTrackbarRegionEventArgs.cs
// </summary>
// ******************************************************************************************

using System;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetTrackbarRegionEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class BudgetTrackbarRegionEventArgs : EventArgs
    {
        /// <summary>
        /// The value
        /// </summary>
        private int _Value;

        /// <summary>
        /// The value two
        /// </summary>
        private int _ValueTwo;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get
            {
                return _Value;
            }
        }

        /// <summary>
        /// Gets the value two.
        /// </summary>
        /// <value>The value two.</value>
        public int ValueTwo
        {
            get
            {
                return _Value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTrackbarRegionEventArgs"/> class.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <param name="valTwo">The value two.</param>
        public BudgetTrackbarRegionEventArgs( int val, int valTwo )
        {
            _Value = val;
            _ValueTwo = valTwo;
        }
    }
}