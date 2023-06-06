// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="BudgetExpanderDesigner.cs" company="Terry D. Eppler">
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
//   BudgetExpanderDesigner.cs
// </summary>
// ******************************************************************************************

using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace BudgetExecution
{
	/// <summary>
	/// Class BudgetExpanderDesigner.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
	public class BudgetExpanderDesigner : ControlDesigner
	{
		/// <summary>
		/// The lists
		/// </summary>
		private DesignerActionListCollection lists;

		/// <summary>
		/// Gets the design-time action lists supported by the component associated with the designer.
		/// </summary>
		/// <value>The action lists.</value>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this.lists == null)
				{
					this.lists = new DesignerActionListCollection();
					this.lists.Add(new BudgetExpanderActionList(this.Component));
				}
				return this.lists;
			}
		}

		/// <summary>
		/// Gets the host control.
		/// </summary>
		/// <value>The host control.</value>
		private BudgetExpander HostControl
		{
			get
			{
				return (BudgetExpander)this.Control;
			}
		}

		/// <summary>
		/// Gets the selection rules that indicate the movement capabilities of a component.
		/// </summary>
		/// <value>The selection rules.</value>
		public override System.Windows.Forms.Design.SelectionRules SelectionRules
		{
			get
			{
				System.Windows.Forms.Design.SelectionRules selectionRule = System.Windows.Forms.Design.SelectionRules.Moveable;
				if (this.HostControl.State != BudgetExpander.eState.Expanded)
				{
					selectionRule = selectionRule;
				}
				else
				{
					selectionRule |= System.Windows.Forms.Design.SelectionRules.AllSizeable;
				}
				selectionRule = selectionRule;
				return selectionRule;
			}
		}


		/// <summary>
		/// Allows a designer to change or remove items from the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.
		/// </summary>
		/// <param name="properties">The properties for the class of the component.</param>
		protected override void PostFilterProperties(IDictionary properties)
		{
			properties.Remove("BackColor");
			properties.Remove("BackgroundImage");
			properties.Remove("BackgroundImageLayout");
			properties.Remove("BorderStyle");
			properties.Remove("Font");
			properties.Remove("ForeColor");
			properties.Remove("RightToLeft");
			properties.Remove("Text");
			base.PostFilterProperties(properties);
		}
	}
}