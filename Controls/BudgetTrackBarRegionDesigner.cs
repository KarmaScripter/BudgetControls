// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetTrackBarRegionDesigner.cs" company="Terry D. Eppler">
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
//   BudgetTrackBarRegionDesigner.cs
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

    //--------------- [Designer(typeof(BudgetTrackbarRegionDesigner))] --------------------//

    #endregion

    #region ControlDesigner

    /// <summary>
    /// Class BudgetTrackbarRegionDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [ PermissionSet( SecurityAction.Demand, Name = "FullTrust" ) ]
    public class BudgetTrackbarRegionDesigner : ControlDesigner
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
                    actionLists.Add( new BudgetTrackbarRegionSmartTagActionList( Component ) );
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
    /// Class BudgetTrackbarRegionSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class BudgetTrackbarRegionSmartTagActionList : DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private BudgetTrackbarRegion colUserControl;

        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetTrackbarRegionSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public BudgetTrackbarRegionSmartTagActionList( IComponent component )
            : base( component )
        {
            colUserControl = component as BudgetTrackbarRegion;

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
        /// Gets or sets a value indicating whether [use fixed size].
        /// </summary>
        /// <value><c>true</c> if [use fixed size]; otherwise, <c>false</c>.</value>
        public bool UseFixedSize
        {
            get
            {
                return colUserControl.UseFixedSize;
            }
            set
            {
                colUserControl.UseFixedSize = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use switch borders].
        /// </summary>
        /// <value><c>true</c> if [use switch borders]; otherwise, <c>false</c>.</value>
        public bool UseSwitchBorders
        {
            get
            {
                return colUserControl.UseSwitchBorders;
            }
            set
            {
                colUserControl.UseSwitchBorders = value;
            }
        }

        /// <summary>
        /// Gets or sets the color left.
        /// </summary>
        /// <value>The color left.</value>
        public Color ColorLeft
        {
            get
            {
                return colUserControl.ColorScheme.LeftColor;
            }
            set
            {
                colUserControl.ColorScheme.LeftColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color middle.
        /// </summary>
        /// <value>The color middle.</value>
        public Color ColorMiddle
        {
            get
            {
                return colUserControl.ColorScheme.MiddleColor;
            }
            set
            {
                colUserControl.ColorScheme.MiddleColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color right.
        /// </summary>
        /// <value>The color right.</value>
        public Color ColorRight
        {
            get
            {
                return colUserControl.ColorScheme.RightColor;
            }
            set
            {
                colUserControl.ColorScheme.RightColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color switch.
        /// </summary>
        /// <value>The color switch.</value>
        public Color ColorSwitch
        {
            get
            {
                return colUserControl.ColorScheme.SwitchColor;
            }
            set
            {
                colUserControl.ColorScheme.SwitchColor = value;
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
                return colUserControl.Style;
            }
            set
            {
                colUserControl.Style = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get
            {
                return colUserControl.Maximum;
            }
            set
            {
                colUserControl.Maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            get
            {
                return colUserControl.Minimum;
            }
            set
            {
                colUserControl.Minimum = value;
            }
        }

        /// <summary>
        /// Gets or sets the slider value1.
        /// </summary>
        /// <value>The slider value1.</value>
        public int SliderValue1
        {
            get
            {
                return colUserControl.SliderValue1;
            }
            set
            {
                colUserControl.SliderValue1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the slider value2.
        /// </summary>
        /// <value>The slider value2.</value>
        public int SliderValue2
        {
            get
            {
                return colUserControl.SliderValue2;
            }
            set
            {
                colUserControl.SliderValue2 = value;
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
                colUserControl.SwitchWidth = value;
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

            items.Add( new DesignerActionPropertyItem( "UseFixedSize", "Use Fixed Size",
                "Appearance", "Set to use fixed size." ) );

            items.Add( new DesignerActionPropertyItem( "UseSwitchBorders", "Use Capsule Borders",
                "Appearance", "Set to show capsule borders." ) );

            items.Add( new DesignerActionPropertyItem( "ForeColor", "Fore Color", "Appearance",
                "Sets the fore color." ) );

            items.Add( new DesignerActionPropertyItem( "ColorLeft", "Left Color", "Appearance",
                "Sets the left color." ) );

            items.Add( new DesignerActionPropertyItem( "ColorMiddle", "Middle Color", "Appearance",
                "Sets the middle color." ) );

            items.Add( new DesignerActionPropertyItem( "ColorRight", "Right Color", "Appearance",
                "Sets the right color." ) );

            items.Add( new DesignerActionPropertyItem( "ColorSwitch", "Capsule Color", "Appearance",
                "Sets the capsule color." ) );

            items.Add( new DesignerActionPropertyItem( "Maximum", "Maximum", "Appearance",
                "Sets the maximum value." ) );

            items.Add( new DesignerActionPropertyItem( "Minimum", "Minimum", "Appearance",
                "Sets the minimum value." ) );

            items.Add( new DesignerActionPropertyItem( "SliderValue1", "Main Slider Value",
                "Appearance", "Sets the main slider value." ) );

            items.Add( new DesignerActionPropertyItem( "SliderValue2", "Helper Slider",
                "Appearance", "Sets the helper slider value." ) );

            items.Add( new DesignerActionPropertyItem( "SwitchWidth", "Capsule Width", "Appearance",
                "Set the capsule size." ) );

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