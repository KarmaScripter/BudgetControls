// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroCheckBoxActionList.cs" company="Terry D. Eppler">
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
//   MetroCheckBoxActionList.cs
// </summary>
// ******************************************************************************************

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace BudgetExecution
{
	/// <summary>
	/// Class MetroCheckBoxActionList.
	/// </summary>
	/// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
	public class BudgetCheckBoxActionList : DesignerActionList
	{
		/// <summary>
		/// The sep
		/// </summary>
		private BudgetCheckBox _sep;

		/// <summary>
		/// The designer action SVC
		/// </summary>
		private DesignerActionUIService designerActionSvc;

		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public Color BorderColor
		{
			get
			{
				return this._sep.ColorScheme.BorderColor;
			}
			set
			{
				this._sep.ColorScheme.BorderColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the box.
		/// </summary>
		/// <value>The color of the box.</value>
		public Color BoxColor
		{
			get
			{
				return this._sep.ColorScheme._InnerBoxColor;
			}
			set
			{
				this._sep.ColorScheme._InnerBoxColor = value;
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
				return this._sep.ColorScheme.FillColor;
			}
			set
			{
				this._sep.ColorScheme.FillColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the style.
		/// </summary>
		/// <value>The style.</value>
		public Design.Style Style
		{
			get
			{
				return this._sep.Style;
			}
			set
			{
				this._sep.Style = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BudgetCheckBoxActionList"/> class.
		/// </summary>
		/// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
		public BudgetCheckBoxActionList(IComponent component) : base(component)
		{
			this.designerActionSvc = null;
			this._sep = (BudgetCheckBox)component;
			this.designerActionSvc = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionHeaderItem("Farb-Eigenschaften"));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("BoxColor", "BoxColor:", "Farb-Eigenschaften", "Die Hauptfarbe des CheckCircles."));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("FillColor", "FillColor:", "Farb-Eigenschaften", "Die Füll-Farbe des CheckCircles."));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("BorderColor", "BorderColor:", "Farb-Eigenschaften", "Die Farbe der Umrandung des CheckCircles."));
			designerActionItemCollection.Add(new DesignerActionHeaderItem("Eigenschaften"));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("Style", "Style:", "Eigenschaften", "Der Style der BudgetProgressbar."));
			return designerActionItemCollection;
		}
	}
}