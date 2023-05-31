// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroPieChartDesigner.cs" company="Terry D. Eppler">
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
//   MetroPieChartDesigner.cs
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

    //--------------- [Designer(typeof(MetroPieChartDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class MetroPieChartDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class MetroPieChartDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new MetroPieChartSmartTagActionList(this.Component));
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
    /// Class MetroPieChartSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroPieChartSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private MetroPieChart colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="MetroPieChartSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroPieChartSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as MetroPieChart;

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
        /// Gets or sets a value indicating whether [draw border].
        /// </summary>
        /// <value><c>true</c> if [draw border]; otherwise, <c>false</c>.</value>
        public bool DrawBorder
        {
            get
            {
                return colUserControl.DrawBorder;
            }
            set
            {
                GetPropertyByName("DrawBorder").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw segment borders].
        /// </summary>
        /// <value><c>true</c> if [draw segment borders]; otherwise, <c>false</c>.</value>
        public bool DrawSegmentBorders
        {
            get
            {
                return colUserControl.DrawSegmentBorders;
            }
            set
            {
                GetPropertyByName("DrawSegmentBorders").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [seperate segments].
        /// </summary>
        /// <value><c>true</c> if [seperate segments]; otherwise, <c>false</c>.</value>
        public bool SeperateSegments
        {
            get
            {
                return colUserControl.SeperateSegments;
            }
            set
            {
                GetPropertyByName("SeperateSegments").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show donut effect].
        /// </summary>
        /// <value><c>true</c> if [show donut effect]; otherwise, <c>false</c>.</value>
        public bool ShowDonutEffect
        {
            get
            {
                return colUserControl.ShowDonutEffect;
            }
            set
            {
                GetPropertyByName("ShowDonutEffect").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show effect].
        /// </summary>
        /// <value><c>true</c> if [show effect]; otherwise, <c>false</c>.</value>
        public bool ShowEffect
        {
            get
            {
                return colUserControl.ShowEffect;
            }
            set
            {
                GetPropertyByName("ShowEffect").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show segment names].
        /// </summary>
        /// <value><c>true</c> if [show segment names]; otherwise, <c>false</c>.</value>
        public bool ShowSegmentNames
        {
            get
            {
                return colUserControl.ShowSegmentNames;
            }
            set
            {
                GetPropertyByName("ShowSegmentNames").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use dynamic border colors].
        /// </summary>
        /// <value><c>true</c> if [use dynamic border colors]; otherwise, <c>false</c>.</value>
        public bool UseDynamicBorderColors
        {
            get
            {
                return colUserControl.UseDynamicBorderColors;
            }
            set
            {
                GetPropertyByName("UseDynamicBorderColors").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use dynamic fill colors].
        /// </summary>
        /// <value><c>true</c> if [use dynamic fill colors]; otherwise, <c>false</c>.</value>
        public bool UseDynamicFillColors
        {
            get
            {
                return colUserControl.UseDynamicFillColors;
            }
            set
            {
                GetPropertyByName("UseDynamicFillColors").SetValue(colUserControl, value);
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
        /// Gets or sets the size of the border.
        /// </summary>
        /// <value>The size of the border.</value>
        public int BorderSize
        {
            get
            {
                return colUserControl.BorderSize;
            }
            set
            {
                GetPropertyByName("BorderSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the donut effect.
        /// </summary>
        /// <value>The size of the donut effect.</value>
        public int DonutEffectSize
        {
            get
            {
                return colUserControl.DonutEffectSize;
            }
            set
            {
                GetPropertyByName("DonutEffectSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the dynamic color offset.
        /// </summary>
        /// <value>The dynamic color offset.</value>
        public int DynamicColorOffset
        {
            get
            {
                return colUserControl.DynamicColorOffset;
            }
            set
            {
                GetPropertyByName("DynamicColorOffset").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the effect.
        /// </summary>
        /// <value>The size of the effect.</value>
        public int EffectSize
        {
            get
            {
                return colUserControl.EffectSize;
            }
            set
            {
                GetPropertyByName("EffectSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public int Radius
        {
            get
            {
                return colUserControl.Radius;
            }
            set
            {
                GetPropertyByName("Radius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the segment border.
        /// </summary>
        /// <value>The size of the segment border.</value>
        public int SegmentBorderSize
        {
            get
            {
                return colUserControl.SegmentBorderSize;
            }
            set
            {
                GetPropertyByName("SegmentBorderSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the segments.
        /// </summary>
        /// <value>The segments.</value>
        public MetroPieChartSegmentCollection Segments
        {
            get
            {
                return colUserControl.Segments;
            }
            set
            {
                GetPropertyByName("Segments").SetValue(colUserControl, value);
            }

        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public MetroPieChart.TrackerStyle Style
        {
            get
            {
                return colUserControl.Style;
            }
            set
            {
                GetPropertyByName("Style").SetValue(colUserControl, value);
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
            items.Add(new DesignerActionHeaderItem("Behaviour"));

            items.Add(new DesignerActionPropertyItem("DrawBorder",
                                 "Border", "Behaviour",
                                 "Set to show the border color."));

            items.Add(new DesignerActionPropertyItem("DrawSegmentBorders",
                                 "Segment Borders", "Behaviour",
                                 "Set to show segment borders."));

            items.Add(new DesignerActionPropertyItem("SeperateSegments",
                                 "Separate Segments", "Behaviour",
                                 "Set to separate segments."));

            items.Add(new DesignerActionPropertyItem("ShowDonutEffect",
                                 "Donut Effect", "Behaviour",
                                 "Set to show donut effect."));

            items.Add(new DesignerActionPropertyItem("ShowEffect",
                "Show Effect", "Behaviour",
                "Set to show the effect."));

            items.Add(new DesignerActionPropertyItem("ShowSegmentNames",
                "Segment Names", "Behaviour",
                "Set to show segment names."));

            items.Add(new DesignerActionPropertyItem("UseDynamicBorderColors",
                "Dynamic Border Colors", "Behaviour",
                "Set to use dynamic border colors."));

            items.Add(new DesignerActionPropertyItem("UseDynamicFillColors",
                "Dynamic Fill Colors", "Behaviour",
                "Enable dynamic fill colors."));

            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("Segments",
                "Segments", "Appearance",
                "Enter the values here to create the chart."));

            items.Add(new DesignerActionPropertyItem("BorderColor",
                "Border Color", "Appearance",
                "Sets the border color."));

            items.Add(new DesignerActionPropertyItem("BorderSize",
                "Border Size", "Appearance",
                "Sets the border size."));

            items.Add(new DesignerActionPropertyItem("DonutEffectSize",
                "Donut Effect Size", "Appearance",
                "Sets the donut effect size."));

            items.Add(new DesignerActionPropertyItem("DynamicColorOffset",
                "Dynamic Color Offset", "Appearance",
                "Sets the color offset."));

            items.Add(new DesignerActionPropertyItem("EffectSize",
                "Effect Size", "Appearance",
                "Sets the effect size."));

            items.Add(new DesignerActionPropertyItem("Radius",
                "Radius", "Appearance",
                "Sets the Radius."));

            items.Add(new DesignerActionPropertyItem("SegmentBorderSize",
                "Segment Border Size", "Appearance",
                "Sets the segment border size."));

            
            items.Add(new DesignerActionPropertyItem("Style",
                "Style", "Appearance",
                "Sets the theme to use."));

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