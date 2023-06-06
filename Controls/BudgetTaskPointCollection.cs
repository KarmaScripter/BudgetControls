// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTaskPointCollection.cs" company="Terry D. Eppler">
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
//   BudgetTaskPointCollection.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetTaskPointCollection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{BudgetExecution.MetroTaskPoint}" />
    public class BudgetTaskPointCollection : Collection<BudgetTaskPoint>
    {
        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddItems( BudgetTaskPoint[ ] items )
        {
            var length = checked( items.Length - 1 );

            for( var i = 0; i <= length; i = checked( i + 1 ) )
            {
                Add( items[ i ] );
                var eventHandler = ItemAdded;

                if( eventHandler != null )
                {
                    eventHandler( this, new BudgetTaskPointCollectionEventArgs( items[ i ] ) );
                }
            }
        }

        /// <summary>
        /// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
        /// </summary>
        protected override void ClearItems( )
        {
            IEnumerator<BudgetTaskPoint> enumerator = null;

            using( enumerator )
            {
                enumerator = GetEnumerator( );

                while( enumerator.MoveNext( ) )
                {
                    var current = enumerator.Current;
                    var eventHandler = ItemRemoving;

                    if( eventHandler != null )
                    {
                        eventHandler( this, new BudgetTaskPointCollectionEventArgs( current ) );
                    }
                }
            }

            base.ClearItems( );
        }

        /// <summary>
        /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        protected override void InsertItem( int index, BudgetTaskPoint item )
        {
            base.InsertItem( index, item );
            var eventHandler = ItemAdded;

            if( eventHandler != null )
            {
                eventHandler( this, new BudgetTaskPointCollectionEventArgs( item ) );
            }
        }

        /// <summary>
        /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem( int index )
        {
            var eventHandler = ItemRemoving;

            if( eventHandler != null )
            {
                eventHandler( this, new BudgetTaskPointCollectionEventArgs( this[ index ] ) );
            }

            base.RemoveItem( index );
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
        protected override void SetItem( int index, BudgetTaskPoint item )
        {
            var eventHandler = ItemRemoving;

            if( eventHandler != null )
            {
                eventHandler( this, new BudgetTaskPointCollectionEventArgs( this[ index ] ) );
            }

            base.SetItem( index, item );
            eventHandler = ItemAdded;

            if( eventHandler != null )
            {
                eventHandler( this, new BudgetTaskPointCollectionEventArgs( item ) );
            }
        }

        /// <summary>
        /// Occurs when [item added].
        /// </summary>
        public event EventHandler<BudgetTaskPointCollectionEventArgs> ItemAdded;

        /// <summary>
        /// Occurs when [item removing].
        /// </summary>
        public event EventHandler<BudgetTaskPointCollectionEventArgs> ItemRemoving;
    }
}