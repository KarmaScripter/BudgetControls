// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetWebChartDesigner.cs" company="Terry D. Eppler">
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
//   BudgetWebChartDesigner.cs
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
    using System.Drawing.Drawing2D;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(BudgetWebChartDesigner))] --------------------//

    #endregion

    #region ControlDesigner

    /// <summary>
    /// Class BudgetWebChartDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [ PermissionSet( SecurityAction.Demand, Name = "FullTrust" ) ]
    public class BudgetWebChartDesigner : ControlDesigner
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
                    actionLists.Add( new BudgetWebChartSmartTagActionList( Component ) );
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
    /// Class BudgetWebChartSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class BudgetWebChartSmartTagActionList : DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private BudgetWebChart colUserControl;

        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetWebChartSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public BudgetWebChartSmartTagActionList( IComponent component )
            : base( component )
        {
            colUserControl = component as BudgetWebChart;

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
        /// Gets or sets the tool tip.
        /// </summary>
        /// <value>The tool tip.</value>
        public virtual ToolTip ToolTip
        {
            get
            {
                return colUserControl.ToolTip;
            }
            set
            {
                GetPropertyByName( "ToolTip" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the width of the chart.
        /// </summary>
        /// <value>The width of the chart.</value>
        public int ChartWidth
        {
            get
            {
                return colUserControl.ChartWidth;
            }
            set
            {
                GetPropertyByName( "ChartWidth" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the corner border.
        /// </summary>
        /// <value>The color of the corner border.</value>
        public Color CornerBorderColor
        {
            get
            {
                return colUserControl.CornerBorderColor;
            }
            set
            {
                GetPropertyByName( "CornerBorderColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the corner fill.
        /// </summary>
        /// <value>The color of the corner fill.</value>
        public Color CornerFillColor
        {
            get
            {
                return colUserControl.CornerFillColor;
            }
            set
            {
                GetPropertyByName( "CornerFillColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the design mode.
        /// </summary>
        /// <value>The color of the design mode.</value>
        public Color DesignModeColor
        {
            get
            {
                return colUserControl.DesignModeColor;
            }
            set
            {
                GetPropertyByName( "DesignModeColor" ).SetValue( colUserControl, value );
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
                return colUserControl.FillColor;
            }
            set
            {
                GetPropertyByName( "FillColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the inner structure.
        /// </summary>
        /// <value>The color of the inner structure.</value>
        public Color InnerStructureColor
        {
            get
            {
                return colUserControl.InnerStructureColor;
            }
            set
            {
                GetPropertyByName( "InnerStructureColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill second.
        /// </summary>
        /// <value>The color of the fill second.</value>
        public Color FillSecondColor
        {
            get
            {
                return colUserControl.FillSecondColor;
            }
            set
            {
                GetPropertyByName( "FillSecondColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the outer structure border.
        /// </summary>
        /// <value>The outer structure border.</value>
        public Color OuterStructureBorder
        {
            get
            {
                return colUserControl.OuterStructureBorder;
            }
            set
            {
                GetPropertyByName( "OuterStructureBorder" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the color of the web border.
        /// </summary>
        /// <value>The color of the web border.</value>
        public Color WebBorderColor
        {
            get
            {
                return colUserControl.WebBorderColor;
            }
            set
            {
                GetPropertyByName( "WebBorderColor" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the corner shape.
        /// </summary>
        /// <value>The corner shape.</value>
        public BudgetWebChart.CornerShapes CornerShape
        {
            get
            {
                return colUserControl.CornerShape;
            }
            set
            {
                GetPropertyByName( "CornerShape" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw inner structure].
        /// </summary>
        /// <value><c>true</c> if [draw inner structure]; otherwise, <c>false</c>.</value>
        public bool DrawInnerStructure
        {
            get
            {
                return colUserControl.DrawInnerStructure;
            }
            set
            {
                GetPropertyByName( "DrawInnerStructure" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw web points].
        /// </summary>
        /// <value><c>true</c> if [draw web points]; otherwise, <c>false</c>.</value>
        public bool DrawWebPoints
        {
            get
            {
                return colUserControl.DrawWebPoints;
            }
            set
            {
                GetPropertyByName( "DrawWebPoints" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show tool tip].
        /// </summary>
        /// <value><c>true</c> if [show tool tip]; otherwise, <c>false</c>.</value>
        public bool ShowToolTip
        {
            get
            {
                return colUserControl.ShowToolTip;
            }
            set
            {
                GetPropertyByName( "ShowToolTip" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [web border is gradient].
        /// </summary>
        /// <value><c>true</c> if [web border is gradient]; otherwise, <c>false</c>.</value>
        public bool WebBorderIsGradient
        {
            get
            {
                return colUserControl.WebBorderIsGradient;
            }
            set
            {
                GetPropertyByName( "WebBorderIsGradient" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [bezier curve].
        /// </summary>
        /// <value><c>true</c> if [bezier curve]; otherwise, <c>false</c>.</value>
        public bool BezierCurve
        {
            get
            {
                return colUserControl.BezierCurve;
            }
            set
            {
                GetPropertyByName( "BezierCurve" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        /// <value>The fill mode.</value>
        public BudgetWebChart.FillModes FillMode
        {
            get
            {
                return colUserControl.FillMode;
            }
            set
            {
                GetPropertyByName( "FillMode" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the hatch style.
        /// </summary>
        /// <value>The hatch style.</value>
        public HatchStyle HatchStyle
        {
            get
            {
                return colUserControl.HatchStyle;
            }
            set
            {
                GetPropertyByName( "HatchStyle" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the inner structure stages.
        /// </summary>
        /// <value>The inner structure stages.</value>
        public int InnerStructureStages
        {
            get
            {
                return colUserControl.InnerStructureStages;
            }
            set
            {
                GetPropertyByName( "InnerStructureStages" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the width of the inner structure.
        /// </summary>
        /// <value>The width of the inner structure.</value>
        public int InnerStructureWidth
        {
            get
            {
                return colUserControl.InnerStructureWidth;
            }
            set
            {
                GetPropertyByName( "InnerStructureWidth" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        public BudgetWebChartPointCollection Points
        {
            get
            {
                return colUserControl.Points;
            }
            set
            {
                GetPropertyByName( "Points" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the web points.
        /// </summary>
        /// <value>The web points.</value>
        public BudgetWebChartPoint WebPoints
        {
            get
            {
                return colUserControl.WebPoints;
            }
            set
            {
                GetPropertyByName( "WebPoints" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the size of the point.
        /// </summary>
        /// <value>The size of the point.</value>
        public int PointSize
        {
            get
            {
                return colUserControl.PointSize;
            }
            set
            {
                GetPropertyByName( "PointSize" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the width of the web border.
        /// </summary>
        /// <value>The width of the web border.</value>
        public int WebBorderWidth
        {
            get
            {
                return colUserControl.WebBorderWidth;
            }
            set
            {
                GetPropertyByName( "WebBorderWidth" ).SetValue( colUserControl, value );
            }
        }

        /// <summary>
        /// Gets or sets the width of the web point.
        /// </summary>
        /// <value>The width of the web point.</value>
        public int WebPointWidth
        {
            get
            {
                return colUserControl.WebPointWidth;
            }
            set
            {
                GetPropertyByName( "WebPointWidth" ).SetValue( colUserControl, value );
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
            items.Add( new DesignerActionHeaderItem( "Behaviour" ) );

            items.Add( new DesignerActionPropertyItem( "BezierCurve", "Bezier Curve", "Behaviour",
                "Set to use bezier curve." ) );

            items.Add( new DesignerActionPropertyItem( "ShowToolTip", "Show ToolTip", "Behaviour",
                "Set to show the tooltip." ) );

            items.Add( new DesignerActionPropertyItem( "WebBorderIsGradient", "Gradient Web Border",
                "Behaviour", "Enable web border to be gradient." ) );

            items.Add( new DesignerActionPropertyItem( "DrawInnerStructure", "Show Inner Shape",
                "Behaviour", "Set to show the inner shape." ) );

            items.Add( new DesignerActionPropertyItem( "DrawWebPoints", "Show Web Points",
                "Behaviour", "Set to show the web points." ) );

            items.Add( new DesignerActionHeaderItem( "Colors" ) );

            items.Add( new DesignerActionPropertyItem( "CornerBorderColor", "Corner Border Color",
                "Colors", "Sets the border color of the corners." ) );

            items.Add( new DesignerActionPropertyItem( "CornerFillColor", "Corner Fill Color",
                "Colors", "Sets the solid fill color of the corner." ) );

            items.Add( new DesignerActionPropertyItem( "DesignModeColor", "Design Mode Color",
                "Colors", "Sets the design mode color." ) );

            items.Add( new DesignerActionPropertyItem( "FillColor", "Fill Color", "Colors",
                "Sets the solid fill color." ) );

            items.Add( new DesignerActionPropertyItem( "InnerStructureColor", "Inner Shape Color",
                "Colors", "Sets the inner shape color." ) );

            items.Add( new DesignerActionPropertyItem( "FillSecondColor", "Second Fill Color",
                "Colors", "Set the second fill color." ) );

            items.Add( new DesignerActionPropertyItem( "OuterStructureBorder", "Outer Shape Border",
                "Colors", "Sets the outer border color." ) );

            items.Add( new DesignerActionPropertyItem( "WebBorderColor", "Web Border Color",
                "Colors", "Sets the web border color." ) );

            items.Add( new DesignerActionHeaderItem( "Appearance" ) );

            items.Add( new DesignerActionPropertyItem( "Points", "Points", "Appearance",
                "Sets the points." ) );

            items.Add( new DesignerActionPropertyItem( "CornerShape", "Corner Shape", "Appearance",
                "Sets the corner shape." ) );

            items.Add( new DesignerActionPropertyItem( "FillMode", "Fill Mode", "Appearance",
                "Sets the fill mode." ) );

            items.Add( new DesignerActionPropertyItem( "HatchStyle", "Hatch Style", "Appearance",
                "Sets the hatch style." ) );

            items.Add( new DesignerActionPropertyItem( "InnerStructureStages", "Inner Shape Stages",
                "Appearance", "Sets the inner shape stages." ) );

            items.Add( new DesignerActionPropertyItem( "InnerStructureWidth", "Inner Shape Width",
                "Appearance", "Sets the innner shape width." ) );

            items.Add( new DesignerActionPropertyItem( "PointSize", "Point Size", "Appearance",
                "Sets the point size." ) );

            items.Add( new DesignerActionPropertyItem( "ChartWidth", "Chart Width", "Appearance",
                "Sets the chart width." ) );

            items.Add( new DesignerActionPropertyItem( "WebBorderWidth", "Web Border Width",
                "Appearance", "Sets the web border width." ) );

            items.Add( new DesignerActionPropertyItem( "WebPointWidth", "Web Point Width",
                "Appearance", "Sets the web point width." ) );

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