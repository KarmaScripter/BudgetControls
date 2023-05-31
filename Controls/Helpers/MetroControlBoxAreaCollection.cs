// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroControlBoxAreaCollection.cs" company="Terry D. Eppler">
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
//   MetroControlBoxAreaCollection.cs
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
    /// Class MetroControlBoxAreaCollection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{BudgetExecution.MetroControlBoxArea}" />
    public class MetroControlBoxAreaCollection : Collection<MetroControlBoxArea>
	{
        /// <summary>
        /// The enc list
        /// </summary>
        private static List<WeakReference> __ENCList;

        /// <summary>
        /// Initializes static members of the <see cref="MetroControlBoxAreaCollection"/> class.
        /// </summary>
        [DebuggerNonUserCode]
		static MetroControlBoxAreaCollection()
		{
			MetroControlBoxAreaCollection.__ENCList = new List<WeakReference>();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroControlBoxAreaCollection"/> class.
        /// </summary>
        [DebuggerNonUserCode]
		public MetroControlBoxAreaCollection()
		{
			MetroControlBoxAreaCollection.__ENCAddToList(this);
		}

        /// <summary>
        /// Encs the add to list.
        /// </summary>
        /// <param name="value">The value.</param>
        [DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = MetroControlBoxAreaCollection.__ENCList;
			Monitor.Enter(_ENCList);
			try
			{
				if (MetroControlBoxAreaCollection.__ENCList.Count == MetroControlBoxAreaCollection.__ENCList.Capacity)
				{
					int item = 0;
					int count = checked(MetroControlBoxAreaCollection.__ENCList.Count - 1);
					for (int i = 0; i <= count; i = checked(i + 1))
					{
						if (MetroControlBoxAreaCollection.__ENCList[i].IsAlive)
						{
							if (i != item)
							{
								MetroControlBoxAreaCollection.__ENCList[item] = MetroControlBoxAreaCollection.__ENCList[i];
							}
							item = checked(item + 1);
						}
					}
					MetroControlBoxAreaCollection.__ENCList.RemoveRange(item, checked(MetroControlBoxAreaCollection.__ENCList.Count - item));
					MetroControlBoxAreaCollection.__ENCList.Capacity = MetroControlBoxAreaCollection.__ENCList.Count;
				}
				MetroControlBoxAreaCollection.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
			}
			finally
			{
				Monitor.Exit(_ENCList);
			}
		}

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddItems(MetroControlBoxArea[] items)
		{
			int length = checked(checked((int)items.Length) - 1);
			for (int i = 0; i <= length; i = checked(i + 1))
			{
				this.Add(items[i]);
				EventHandler<MetroControlBoxAreaCollectionEventArgs> eventHandler = this.ItemAdded;
				if (eventHandler != null)
				{
					eventHandler(this, new MetroControlBoxAreaCollectionEventArgs(items[i]));
				}
			}
		}

        /// <summary>
        /// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
        /// </summary>
        protected override void ClearItems()
		{
			IEnumerator<MetroControlBoxArea> enumerator = null;
			using (enumerator)
			{
				enumerator = this.GetEnumerator();
				while (enumerator.MoveNext())
				{
					MetroControlBoxArea current = enumerator.Current;
					EventHandler<MetroControlBoxAreaCollectionEventArgs> eventHandler = this.ItemRemoving;
					if (eventHandler != null)
					{
						eventHandler(this, new MetroControlBoxAreaCollectionEventArgs(current));
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
        protected override void InsertItem(int index, MetroControlBoxArea item)
		{
			base.InsertItem(index, item);
			EventHandler<MetroControlBoxAreaCollectionEventArgs> eventHandler = this.ItemAdded;
			if (eventHandler != null)
			{
				eventHandler(this, new MetroControlBoxAreaCollectionEventArgs(item));
			}
		}

        /// <summary>
        /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem(int index)
		{
			EventHandler<MetroControlBoxAreaCollectionEventArgs> eventHandler = this.ItemRemoving;
			if (eventHandler != null)
			{
				eventHandler(this, new MetroControlBoxAreaCollectionEventArgs(this[index]));
			}
			base.RemoveItem(index);
		}

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
        protected override void SetItem(int index, MetroControlBoxArea item)
		{
			EventHandler<MetroControlBoxAreaCollectionEventArgs> eventHandler = this.ItemRemoving;
			if (eventHandler != null)
			{
				eventHandler(this, new MetroControlBoxAreaCollectionEventArgs(this[index]));
			}
			base.SetItem(index, item);
			eventHandler = this.ItemAdded;
			if (eventHandler != null)
			{
				eventHandler(this, new MetroControlBoxAreaCollectionEventArgs(item));
			}
		}

        /// <summary>
        /// Occurs when [item added].
        /// </summary>
        public event EventHandler<MetroControlBoxAreaCollectionEventArgs> ItemAdded;

        /// <summary>
        /// Occurs when [item removing].
        /// </summary>
        public event EventHandler<MetroControlBoxAreaCollectionEventArgs> ItemRemoving;
	}
}