// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetPanelSelectionDesigner.cs" company="Terry D. Eppler">
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
//   BudgetPanelSelectionDesigner.cs
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

    //--------------- [Designer(typeof(BudgetPanelSelectionDesigner))] --------------------//

    #endregion

    #region ControlDesigner

    /// <summary>
    /// Class BudgetPanelSelectionDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [ PermissionSet( SecurityAction.Demand, Name = "FullTrust" ) ]
    public class BudgetPanelSelectionDesigner : ControlDesigner
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
                    actionLists.Add( new MetroPanelSelectionSmartTagActionList( Component ) );
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
    /// Class MetroPanelSelectionSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroPanelSelectionSmartTagActionList : DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private BudgetPanelSelection colUserControl;

        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroPanelSelectionSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroPanelSelectionSmartTagActionList( IComponent component )
            : base( component )
        {
            colUserControl = component as BudgetPanelSelection;

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
        /// Gets or sets the color of the header.
        /// </summary>
        /// <value>The color of the header.</value>
        public Color HeaderColor
        {
            get
            {
                return colUserControl.ColorScheme.HeaderColor;
            }
            set
            {
                colUserControl.ColorScheme.HeaderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the sub text.
        /// </summary>
        /// <value>The color of the sub text.</value>
        public Color SubTextColor
        {
            get
            {
                return colUserControl.ColorScheme.SubTextColor;
            }
            set
            {
                colUserControl.ColorScheme.SubTextColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MetroPanelSelectionSmartTagActionList"/> is checked.
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
                colUserControl.Checked = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [check on click].
        /// </summary>
        /// <value><c>true</c> if [check on click]; otherwise, <c>false</c>.</value>
        public bool CheckOnClick
        {
            get
            {
                return colUserControl.CheckOnClick;
            }
            set
            {
                colUserControl.CheckOnClick = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color checked.
        /// </summary>
        /// <value>The background color checked.</value>
        public Color BackgroundColorChecked
        {
            get
            {
                return colUserControl.ColorScheme.BackgroundColorChecked;
            }
            set
            {
                colUserControl.ColorScheme.BackgroundColorChecked = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color hover.
        /// </summary>
        /// <value>The background color hover.</value>
        public Color BackgroundColorHover
        {
            get
            {
                return colUserControl.ColorScheme.BackgroundColorHover;
            }
            set
            {
                colUserControl.ColorScheme.BackgroundColorHover = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color normal.
        /// </summary>
        /// <value>The background color normal.</value>
        public Color BackgroundColorNormal
        {
            get
            {
                return colUserControl.ColorScheme.BackgroundColorNormal;
            }
            set
            {
                colUserControl.ColorScheme.BackgroundColorNormal = value;
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
                return colUserControl.ColorScheme._BorderColor;
            }
            set
            {
                colUserControl.ColorScheme._BorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the border color n.
        /// </summary>
        /// <value>The border color n.</value>
        public Color BorderColorN
        {
            get
            {
                return colUserControl.ColorScheme._BorderColorN;
            }
            set
            {
                colUserControl.ColorScheme._BorderColorN = value;
            }
        }

        /// <summary>
        /// Gets or sets the headline font.
        /// </summary>
        /// <value>The headline font.</value>
        public Font HeadlineFont
        {
            get
            {
                return colUserControl.HeadlineFont;
            }
            set
            {
                colUserControl.HeadlineFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the sub text font.
        /// </summary>
        /// <value>The sub text font.</value>
        public Font SubTextFont
        {
            get
            {
                return colUserControl.SubTextFont;
            }
            set
            {
                colUserControl.SubTextFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the text headline.
        /// </summary>
        /// <value>The text headline.</value>
        public string TextHeadline
        {
            get
            {
                return colUserControl.TextHeadline;
            }
            set
            {
                colUserControl.TextHeadline = value;
            }
        }

        /// <summary>
        /// Gets or sets the text subline.
        /// </summary>
        /// <value>The text subline.</value>
        public string TextSubline
        {
            get
            {
                return colUserControl.TextSubline;
            }
            set
            {
                colUserControl.TextSubline = value;
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
                "Set to enable the panel checked." ) );

            items.Add( new DesignerActionPropertyItem( "CheckOnClick", "Check On Click",
                "Appearance", "Set to enable panel to be checked when clicked." ) );

            items.Add( new DesignerActionPropertyItem( "BackgroundColorChecked", "Checked",
                "Appearance", "Sets the background color when checked." ) );

            items.Add( new DesignerActionPropertyItem( "BackgroundColorHover", "Hovered",
                "Appearance", "Sets the background color when hovered." ) );

            items.Add( new DesignerActionPropertyItem( "BackgroundColorNormal", "Inactive",
                "Appearance", "Sets the inactive background color." ) );

            items.Add( new DesignerActionPropertyItem( "BorderColor", "Border", "Appearance",
                "Sets the active border color." ) );

            items.Add( new DesignerActionPropertyItem( "BorderColorN", "Border Inactive",
                "Appearance", "Sets the inactive border color." ) );

            items.Add( new DesignerActionPropertyItem( "HeaderColor", "Header Color", "Appearance",
                "Sets the header color." ) );

            items.Add( new DesignerActionPropertyItem( "SubTextColor", "Sub-Text Color",
                "Appearance", "Sets the sub-text color." ) );

            items.Add( new DesignerActionPropertyItem( "HeadlineFont", "Headline Font",
                "Appearance", "Sets the headline font." ) );

            items.Add( new DesignerActionPropertyItem( "SubTextFont", "Sub-Text Font", "Appearance",
                "Sets the sub-text font." ) );

            items.Add( new DesignerActionPropertyItem( "TextHeadline", "Headline", "Appearance",
                "Sets the headline text." ) );

            items.Add( new DesignerActionPropertyItem( "TextSubline", "Sub-Text", "Appearance",
                "Sets the sub-text." ) );

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