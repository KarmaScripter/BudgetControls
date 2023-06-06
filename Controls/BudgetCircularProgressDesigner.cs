// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetCircularProgressDesigner.cs" company="Terry D. Eppler">
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
//   BudgetCircularProgressDesigner.cs
// </summary>
// ******************************************************************************************

using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows.Forms.Design;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetCircularProgressDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    public class BudgetCircularProgressDesigner : ControlDesigner
    {
        /// <summary>
        /// The lists
        /// </summary>
        private DesignerActionListCollection lists;

        /// <summary>
        /// Gets the host control.
        /// </summary>
        /// <value>The host control.</value>
        private BudgetCircularProgress HostControl
        {
            get
            {
                return (BudgetCircularProgress)Control;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetCircularProgressDesigner"/> class.
        /// </summary>
        [ DebuggerNonUserCode ]
        public BudgetCircularProgressDesigner( )
        {
        }

        /// <summary>
        /// Allows a designer to change or remove items from the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.
        /// </summary>
        /// <param name="properties">The properties for the class of the component.</param>
        protected override void PostFilterProperties( IDictionary properties )
        {
            properties.Remove( "BackgroundImage" );
            properties.Remove( "BackgroundImageLayout" );
            properties.Remove( "BorderStyle" );
            properties.Remove( "RightToLeft" );
            properties.Remove( "Text" );
            base.PostFilterProperties( properties );
        }
    }
}