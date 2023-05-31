// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroExpanderActionList.cs" company="Terry D. Eppler">
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
//   MetroExpanderActionList.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace BudgetExecution
{
    /// <summary>
    /// Class MetroExpanderActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroExpanderActionList : DesignerActionList
	{
        /// <summary>
        /// The ex
        /// </summary>
        private MetroExpander _ex;

        /// <summary>
        /// The designer action SVC
        /// </summary>
        private DesignerActionUIService designerActionSvc;

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public MetroExpander.eState State
		{
			get
			{
				return this._ex.State;
			}
			set
			{
				this._ex.State = value;
				this.designerActionSvc.Refresh(this._ex);
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroExpanderActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroExpanderActionList(IComponent component) : base(component)
		{
			this.designerActionSvc = null;
			this._ex = (MetroExpander)component;
			this.designerActionSvc = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
		}

        /// <summary>
        /// Betas this instance.
        /// </summary>
        public void BETA()
		{
			Interaction.MsgBox("Bitte beachten Sie, dass dieses Control noch in der BETA-Test-Phase ist und somit den ein oder anderen Fehler aufweist.\r\nIch bitte um Ihr Verständnis!", MsgBoxStyle.Information, "Hinweis!");
			this.designerActionSvc.Refresh(this._ex);
		}

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionHeaderItem("Properties"));
			designerActionItemCollection.Add(new DesignerActionPropertyItem("State", "State:", "Properties", "Der Status des Expanders."));
			return designerActionItemCollection;
		}
	}
}