// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="BudgetTrackerPathAlertEventArgs.cs" company="Terry D. Eppler">
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
//   BudgetTrackerPathAlertEventArgs.cs
// </summary>
// ******************************************************************************************

using System;

namespace BudgetExecution
{
	/// <summary>
	/// Class BudgetTrackerPathAlertEventArgs.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class BudgetTrackerPathAlertEventArgs : EventArgs
	{
		/// <summary>
		/// The item
		/// </summary>
		private BudgetTrackerPath _item;

		/// <summary>
		/// The value
		/// </summary>
		private int _value;

		/// <summary>
		/// Gets the item.
		/// </summary>
		/// <value>The item.</value>
		public BudgetTrackerPath Item
		{
			get
			{
				return this._item;
			}
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public int Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BudgetTrackerPathAlertEventArgs"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="value">The value.</param>
		public BudgetTrackerPathAlertEventArgs(BudgetTrackerPath item, int value)
		{
			this._item = item;
			this._value = value;
		}
	}
}