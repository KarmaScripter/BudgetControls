// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetSwitchDesigner.cs" company="Terry D. Eppler">
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
//   BudgetSwitchDesigner.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

namespace BudgetExecution
{
    using System.Security.Permissions;
    using System.Windows.Forms.Design;

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(BudgetSwitchDesigner))] --------------------//

    #endregion

    #region ControlDesigner

    /// <summary>
    /// Class BudgetSwitchDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [ PermissionSet( SecurityAction.Demand, Name = "FullTrust" ) ]
    public class BudgetSwitchDesigner : ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if( null == actionLists )
                {
                    actionLists = new DesignerActionListCollection( );
                    actionLists.Add( new MetroSwitchSmartTagActionList( Component ) );
                }

                return actionLists;
            }
        }

        #region Budget Filter (Remove Properties)

        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties( IDictionary Properties )
        {
            //Properties.Remove("AllowDrop");
            //Properties.Remove("FlatStyle");
            //Properties.Remove("ForeColor");
            //Properties.Remove("ImageIndex");
            //Properties.Remove("ImageList");
        }

        #endregion
    }

    #endregion

    #region SmartTagActionList

    /// <summary>
    /// Class MetroSwitchSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroSwitchSmartTagActionList : DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private BudgetSwitch colUserControl;

        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroSwitchSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroSwitchSmartTagActionList( IComponent component )
            : base( component )
        {
            colUserControl = component as BudgetSwitch;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            designerActionUISvc =
                GetService( typeof( DesignerActionUIService ) ) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="System.ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName( String propName )
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties( colUserControl )[ propName ];

            if( null == prop )
            {
                throw new ArgumentException( "Matching ColorLabel property not found!", propName );
            }
            else
            {
                return prop;
            }
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName( "BackColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName( "ForeColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MetroSwitchSmartTagActionList"/> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public bool Checked
        {
            get
            {
                return colUserControl.Checked;
            }
            set
            {
                GetPropertyByName( "Checked" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
        {
            get
            {
                return colUserControl.BorderColor;
            }
            set
            {
                GetPropertyByName( "BorderColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the check.
        /// </summary>
        /// <value>The color of the check.</value>
        public Color CheckColor
        {
            get
            {
                return colUserControl.CheckColor;
            }
            set
            {
                GetPropertyByName( "CheckColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the default color.
        /// </summary>
        /// <value>The default color.</value>
        public Color DefaultColor
        {
            get
            {
                return colUserControl.DefaultColor;
            }
            set
            {
                GetPropertyByName( "DefaultColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        public Color HoverColor
        {
            get
            {
                return colUserControl.HoverColor;
            }
            set
            {
                GetPropertyByName( "HoverColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the switch.
        /// </summary>
        /// <value>The color of the switch.</value>
        public Color SwitchColor
        {
            get
            {
                return colUserControl.SwitchColor;
            }
            set
            {
                GetPropertyByName( "SwitchColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the on off text.
        /// </summary>
        /// <value>The on off text.</value>
        public string OnOffText
        {
            get
            {
                return colUserControl.OnOffText;
            }
            set
            {
                GetPropertyByName( "OnOffText" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the width of the rail.
        /// </summary>
        /// <value>The width of the rail.</value>
        public int RailWidth
        {
            get
            {
                return colUserControl.RailWidth;
            }
            set
            {
                GetPropertyByName( "RailWidth" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the width of the switch.
        /// </summary>
        /// <value>The width of the switch.</value>
        public int SwitchWidth
        {
            get
            {
                return colUserControl.SwitchWidth;
            }
            set
            {
                GetPropertyByName( "SwitchWidth" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the switch style.
        /// </summary>
        /// <value>The switch style.</value>
        public BudgetSwitch.MetroSwitchStyle SwitchStyle
        {
            get
            {
                return colUserControl.SwitchStyle;
            }
            set
            {
                GetPropertyByName( "SwitchStyle" ).SetValue( colUserControl, value );
            }
        }

        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems( )
        {
            var items = new DesignerActionItemCollection( );

            //Define static section header entries.
            items.Add( new DesignerActionHeaderItem( "Appearance" ) );

            items.Add( new DesignerActionPropertyItem( "Checked", "Checked", "Appearance",
                "Set to enable it to be checked." ) );

            items.Add( new DesignerActionPropertyItem( "ForeColor", "Fore Color", "Appearance",
                "Selects the foreground color." ) );

            items.Add( new DesignerActionPropertyItem( "CheckColor", "Checked Color", "Appearance",
                "Set the color when checked." ) );

            items.Add( new DesignerActionPropertyItem( "BorderColor", "Border Color", "Appearance",
                "Sets the border color." ) );

            items.Add( new DesignerActionPropertyItem( "DefaultColor", "Default Color",
                "Appearance", "Sets the default color." ) );

            items.Add( new DesignerActionPropertyItem( "HoverColor", "Hovered Color", "Appearance",
                "Sets the color when hovered." ) );

            items.Add( new DesignerActionPropertyItem( "SwitchColor", "Switch Color", "Appearance",
                "Sets the switch color." ) );

            items.Add( new DesignerActionPropertyItem( "OnOffText", "On / Off Text", "Appearance",
                "Sets the On or Off text." ) );

            items.Add( new DesignerActionPropertyItem( "RailWidth", "Rail width", "Appearance",
                "Sets the rail width." ) );

            items.Add( new DesignerActionPropertyItem( "SwitchWidth", "Switch Width", "Appearance",
                "Sets the switch width." ) );

            //Create entries for static Information section.
            var location = new StringBuilder( "Product: " );
            location.Append( colUserControl.ProductName );
            var size = new StringBuilder( "Version: " );
            size.Append( colUserControl.ProductVersion );
            items.Add( new DesignerActionTextItem( location.ToString( ), "Information" ) );
            items.Add( new DesignerActionTextItem( size.ToString( ), "Information" ) );
            return items;
        }

        #endregion
    }

    #endregion

    #endregion
}