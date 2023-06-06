// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetExpanderActionList.cs" company="Terry D. Eppler">
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
//   BudgetExpanderActionList.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetExpanderActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class BudgetExpanderActionList : DesignerActionList
    {
        /// <summary>
        /// The ex
        /// </summary>
        private BudgetExpander _ex;

        /// <summary>
        /// The designer action SVC
        /// </summary>
        private DesignerActionUIService designerActionSvc;

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public BudgetExpander.eState State
        {
            get
            {
                return _ex.State;
            }
            set
            {
                _ex.State = value;
                designerActionSvc.Refresh( _ex );
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetExpanderActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public BudgetExpanderActionList( IComponent component )
            : base( component )
        {
            designerActionSvc = null;
            _ex = (BudgetExpander)component;

            designerActionSvc =
                (DesignerActionUIService)GetService( typeof( DesignerActionUIService ) );
        }

        /// <summary>
        /// Betas this instance.
        /// </summary>
        public void BETA( )
        {
            Interaction.MsgBox(
                "Bitte beachten Sie, dass dieses Control noch in der BETA-Test-Phase ist und somit den ein oder anderen Fehler aufweist.\r\nIch bitte um Ihr Verständnis!",
                MsgBoxStyle.Information, "Hinweis!" );

            designerActionSvc.Refresh( _ex );
        }

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems( )
        {
            var designerActionItemCollection = new DesignerActionItemCollection( );
            designerActionItemCollection.Add( new DesignerActionHeaderItem( "Properties" ) );

            designerActionItemCollection.Add( new DesignerActionPropertyItem( "State", "State:",
                "Properties", "Der Status des Expanders." ) );

            return designerActionItemCollection;
        }
    }
}