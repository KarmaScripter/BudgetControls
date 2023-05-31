// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroNavigationButtonDesigner.cs" company="Terry D. Eppler">
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
//   MetroNavigationButtonDesigner.cs
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

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(MetroNavigationButtonDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class MetroNavigationButtonDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class MetroNavigationButtonDesigner : System.Windows.Forms.Design.ControlDesigner
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
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new MetroNavigationButtonSmartTagActionList(this.Component));
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
        protected override void PostFilterProperties(IDictionary Properties)
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
    /// Class MetroNavigationButtonSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroNavigationButtonSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private MetroNavigationButton colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="MetroNavigationButtonSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroNavigationButtonSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as MetroNavigationButton;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="System.ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
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
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
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
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the arrow direction.
        /// </summary>
        /// <value>The arrow direction.</value>
        public System.Windows.Forms.ArrowDirection ArrowDirection
        {
            get
            {
                return colUserControl.ArrowDirection;
            }
            set
            {
                GetPropertyByName("ArrowDirection").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the arrow.
        /// </summary>
        /// <value>The color of the arrow.</value>
        public Color ArrowColor
        {
            get
            {
                return colUserControl.ArrowColor;
            }
            set
            {
                GetPropertyByName("ArrowColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the arrow hover.
        /// </summary>
        /// <value>The color of the arrow hover.</value>
        public Color ArrowHoverColor
        {
            get
            {
                return colUserControl.ArrowHoverColor;
            }
            set
            {
                GetPropertyByName("ArrowHoverColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the arrow pressed.
        /// </summary>
        /// <value>The color of the arrow pressed.</value>
        public Color ArrowPressedColor
        {
            get
            {
                return colUserControl.ArrowPressedColor;
            }
            set
            {
                GetPropertyByName("ArrowPressedColor").SetValue(colUserControl, value);
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
                GetPropertyByName("BorderColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the border hover.
        /// </summary>
        /// <value>The color of the border hover.</value>
        public Color BorderHoverColor
        {
            get
            {
                return colUserControl.BorderHoverColor;
            }
            set
            {
                GetPropertyByName("BorderHoverColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the border pressed.
        /// </summary>
        /// <value>The color of the border pressed.</value>
        public Color BorderPressedColor
        {
            get
            {
                return colUserControl.BorderPressedColor;
            }
            set
            {
                GetPropertyByName("BorderPressedColor").SetValue(colUserControl, value);
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
                GetPropertyByName("DefaultColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the disabled arrow.
        /// </summary>
        /// <value>The color of the disabled arrow.</value>
        public Color DisabledArrowColor
        {
            get
            {
                return colUserControl.DisabledArrowColor;
            }
            set
            {
                GetPropertyByName("DisabledArrowColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the disabled.
        /// </summary>
        /// <value>The color of the disabled.</value>
        public new Color DisabledColor
        {
            get
            {
                return colUserControl.DisabledColor;
            }
            set
            {
                GetPropertyByName("DisabledColor").SetValue(colUserControl, value);
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
                GetPropertyByName("HoverColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the pressed.
        /// </summary>
        /// <value>The color of the pressed.</value>
        public Color PressedColor
        {
            get
            {
                return colUserControl.PressedColor;
            }
            set
            {
                GetPropertyByName("PressedColor").SetValue(colUserControl, value);
            }
        }


        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("ArrowDirection",
                                 "Arrow Direction", "Appearance",
                                 "Sets the arrow direction."));

            items.Add(new DesignerActionPropertyItem("ArrowColor",
                                 "Arrow Color", "Appearance",
                                 "Sets the arrow color."));

            items.Add(new DesignerActionPropertyItem("ArrowHoverColor",
                                 "Hovered Color", "Appearance",
                                 "Sets the hovered arrow color."));

            items.Add(new DesignerActionPropertyItem("ArrowPressedColor",
                                 "Pressed Color", "Appearance",
                                 "Sets the color when pressed."));


            items.Add(new DesignerActionPropertyItem("BorderColor",
                "Border Color", "Appearance",
                "Sets the border color."));

            items.Add(new DesignerActionPropertyItem("BorderHoverColor",
                "Border Hover Color", "Appearance",
                "Sets the hovered border color."));


            items.Add(new DesignerActionPropertyItem("BorderPressedColor",
                "Border Pressed Color", "Appearance",
                "Sets the pressed border color."));

            items.Add(new DesignerActionPropertyItem("DefaultColor",
                "Default Color", "Appearance",
                "Sets the default color."));


            items.Add(new DesignerActionPropertyItem("DisabledArrowColor",
                "Disabled Arrow Color", "Appearance",
                "Sets the disabled arrow color."));

            items.Add(new DesignerActionPropertyItem("DisabledColor",
                "Inner Disabled Color", "Appearance",
                "Sets the disabled color."));


            items.Add(new DesignerActionPropertyItem("HoverColor",
                "Inner Hover Color", "Appearance",
                "Sets the hovered color."));

            items.Add(new DesignerActionPropertyItem("PressedColor",
                "Inner Pressed Color", "Appearance",
                "Sets the pressed color."));
            

            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Product: ");
            location.Append(colUserControl.ProductName);
            StringBuilder size = new StringBuilder("Version: ");
            size.Append(colUserControl.ProductVersion);
            items.Add(new DesignerActionTextItem(location.ToString(),
                             "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(),
                             "Information"));

            return items;
        }

        #endregion




    }

    #endregion

    #endregion


}