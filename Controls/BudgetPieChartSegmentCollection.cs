// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="BudgetPieChartSegmentCollection.cs" company="Terry D. Eppler">
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
//   BudgetPieChartSegmentCollection.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BudgetExecution
{
	/// <summary>
	/// Class BudgetPieChartSegmentCollection.
	/// </summary>
	/// <seealso cref="System.Collections.ObjectModel.Collection{BudgetExecution.MetroPieChartSegment}" />
	public class BudgetPieChartSegmentCollection : Collection<BudgetPieChartSegment>
	{
		/// <summary>
		/// The enc list
		/// </summary>
		private static List<WeakReference> __ENCList;

		/// <summary>
		/// Initializes static members of the <see cref="BudgetPieChartSegmentCollection"/> class.
		/// </summary>
		[DebuggerNonUserCode]
		static BudgetPieChartSegmentCollection()
		{
			BudgetPieChartSegmentCollection.__ENCList = new List<WeakReference>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BudgetPieChartSegmentCollection"/> class.
		/// </summary>
		[DebuggerNonUserCode]
		public BudgetPieChartSegmentCollection()
		{
			BudgetPieChartSegmentCollection.__ENCAddToList(this);
		}

		/// <summary>
		/// Encs the add to list.
		/// </summary>
		/// <param name="value">The value.</param>
		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = BudgetPieChartSegmentCollection.__ENCList;
			Monitor.Enter(_ENCList);
			try
			{
				if (BudgetPieChartSegmentCollection.__ENCList.Count == BudgetPieChartSegmentCollection.__ENCList.Capacity)
				{
					int item = 0;
					int count = checked(BudgetPieChartSegmentCollection.__ENCList.Count - 1);
					for (int i = 0; i <= count; i = checked(i + 1))
					{
						if (BudgetPieChartSegmentCollection.__ENCList[i].IsAlive)
						{
							if (i != item)
							{
								BudgetPieChartSegmentCollection.__ENCList[item] = BudgetPieChartSegmentCollection.__ENCList[i];
							}
							item = checked(item + 1);
						}
					}
					BudgetPieChartSegmentCollection.__ENCList.RemoveRange(item, checked(BudgetPieChartSegmentCollection.__ENCList.Count - item));
					BudgetPieChartSegmentCollection.__ENCList.Capacity = BudgetPieChartSegmentCollection.__ENCList.Count;
				}
				BudgetPieChartSegmentCollection.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
			}
			finally
			{
				Monitor.Exit(_ENCList);
			}
		}

		/// <summary>
		/// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
		/// </summary>
		protected override void ClearItems()
		{
			IEnumerator<BudgetPieChartSegment> enumerator = null;
			using (enumerator)
			{
				enumerator = this.GetEnumerator();
				while (enumerator.MoveNext())
				{
					BudgetPieChartSegment current = enumerator.Current;
					EventHandler<BudgetPieChartSegmentCollectionEventArgs> eventHandler = this.ItemRemoving;
					if (eventHandler != null)
					{
						eventHandler(this, new BudgetPieChartSegmentCollectionEventArgs(current));
					}
				}
			}
			base.ClearItems();
		}

		/// <summary>
		/// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert. The value can be null for reference types.</param>
		protected override void InsertItem(int index, BudgetPieChartSegment item)
		{
			base.InsertItem(index, item);
			EventHandler<BudgetPieChartSegmentCollectionEventArgs> eventHandler = this.ItemAdded;
			if (eventHandler != null)
			{
				eventHandler(this, new BudgetPieChartSegmentCollectionEventArgs(item));
			}
		}

		/// <summary>
		/// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		protected override void RemoveItem(int index)
		{
			EventHandler<BudgetPieChartSegmentCollectionEventArgs> eventHandler = this.ItemRemoving;
			if (eventHandler != null)
			{
				eventHandler(this, new BudgetPieChartSegmentCollectionEventArgs(this[index]));
			}
			base.RemoveItem(index);
		}

		/// <summary>
		/// Replaces the element at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
		protected override void SetItem(int index, BudgetPieChartSegment item)
		{
			EventHandler<BudgetPieChartSegmentCollectionEventArgs> eventHandler = this.ItemRemoving;
			if (eventHandler != null)
			{
				eventHandler(this, new BudgetPieChartSegmentCollectionEventArgs(this[index]));
			}
			base.SetItem(index, item);
			eventHandler = this.ItemAdded;
			if (eventHandler != null)
			{
				eventHandler(this, new BudgetPieChartSegmentCollectionEventArgs(item));
			}
		}

		/// <summary>
		/// Occurs when [item added].
		/// </summary>
		public event EventHandler<BudgetPieChartSegmentCollectionEventArgs> ItemAdded;

		/// <summary>
		/// Occurs when [item removing].
		/// </summary>
		public event EventHandler<BudgetPieChartSegmentCollectionEventArgs> ItemRemoving;
	}
}