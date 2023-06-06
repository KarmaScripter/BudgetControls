// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="BudgetSeparatorActionList.cs" company="Terry D. Eppler">
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
//   BudgetSeparatorActionList.cs
// </summary>
// ******************************************************************************************

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace BudgetExecution
{
	/// <summary>
	/// Class BudgetSeparatorActionList.
	/// </summary>
	/// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
	public class BudgetSeparatorActionList : DesignerActionList
	{
		/// <summary>
		/// The sep
		/// </summary>
		private BudgetSeparator _sep;

		/// <summary>
		/// The designer action SVC
		/// </summary>
		private DesignerActionUIService designerActionSvc;

		/// <summary>
		/// Gets or sets the color1.
		/// </summary>
		/// <value>The color1.</value>
		public Color Color1
		{
			get
			{
				return this._sep.ColorScheme.Color1;
			}
			set
			{
				this._sep.ColorScheme.Color1 = value;
			}
		}

		/// <summary>
		/// Gets or sets the color2.
		/// </summary>
		/// <value>The color2.</value>
		public Color Color2
		{
			get
			{
				return this._sep.ColorScheme.Color2;
			}
			set
			{
				this._sep.ColorScheme.Color2 = value;
			}
		}

		/// <summary>
		/// Gets or sets the orientation.
		/// </summary>
		/// <value>The orientation.</value>
		public Design.Orientation Orientation
		{
			get
			{
				return this._sep.Orientation;
			}
			set
			{
				this._sep.Orientation = value;
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
		/// Initializes a new instance of the <see cref="BudgetSeparatorActionList"/> class.
		/// </summary>
		/// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
		public BudgetSeparatorActionList(IComponent component) : base(component)
		{
			this.designerActionSvc = null;
			this._sep = (BudgetSeparator)component;
			this.designerActionSvc = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionHeaderItem("Properties"));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("Color1", "Color1:", "Properties", "Gibt die erste Farbe an. (=Oben/Links)"));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("Color2", "Color2:", "Properties", "Gibt die zweite Farbe an. (= Unten/Rechts)"));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("Orientation", "Orientation:", "Properties", "Gibt die Orientierung des Separators an."));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("Style", "Style:", "Properties", "Setzt das Design."));
			return designerActionItemCollection;
		}
	}
}