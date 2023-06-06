// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="BudgetListbox.cs" company="Terry D. Eppler">
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
//   BudgetListbox.cs
// </summary>
// ******************************************************************************************

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// Class BudgetListbox.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ListBox" />
    [ Description( "Ein Listbox-Steuerelement im Metrostil." ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( ListBox ) ) ]
    public class BudgetListbox : ListBox
    {
        #region Private Fields

        /// <summary>
        /// The selection color
        /// </summary>
        private Color _SelectionColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to enable automatic style.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [ Category( "Appearance" ) ]
        [ DefaultValue( true ) ]
        [ Description( "Sets a value indicating whether to enable automatic style." ) ]
        public bool AutoStyle
        {
            get
            {
                return _AutoStyle;
            }
            set
            {
                if( _AutoStyle != value )
                {
                    _AutoStyle = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the border." ) ]
        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                if( _BorderColor != value )
                {
                    _BorderColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection.
        /// </summary>
        /// <value>The color of the selection.</value>
        [ Category( "Appearance" ) ]
        [ Description( "Sets the color of the selection." ) ]
        public Color SelectionColor
        {
            get
            {
                return _SelectionColor;
            }
            set
            {
                if( _SelectionColor != value )
                {
                    _SelectionColor = value;
                    Invalidate( );
                }
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        [ Browsable( true ) ]
        [ Category( "Appearance" ) ]
        [ DefaultValue( 0 ) ]
        [ Description( "Sets the style." ) ]
        [ RefreshProperties( RefreshProperties.All ) ]
        public Design.Style Style
        {
            get
            {
                return _Style;
            }
            set
            {
                if( value != _Style )
                {
                    _Style = value;

                    switch( value )
                    {
                        case Design.Style.Light:
                        {
                            _SelectionColor = Design.BudgetColors.AccentBlue;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            BackColor = Design.BudgetColors.LightDefault;
                            ForeColor = Design.BudgetColors.LightFont;
                            break;
                        }
                        case Design.Style.Dark:
                        {
                            _SelectionColor = Design.BudgetColors.AccentBlue;
                            _BorderColor = Design.BudgetColors.LightBorder;
                            BackColor = Design.BudgetColors.DarkDefault;
                            ForeColor = Design.BudgetColors.DarkFont;
                            break;
                        }
                    }

                    Invalidate( );
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetListbox" /> class.
        /// </summary>
        public BudgetListbox( )
        {
            _SelectionColor = Design.BudgetColors.AccentBlue;
            _BorderColor = Design.BudgetColors.LightBorder;
            _Style = Design.Style.Light;
            _AutoStyle = true;
            Font = new Font( "Segoe UI", 8f );
            DoubleBuffered = true;
            BackColor = Design.BudgetColors.LightDefault;
            ForeColor = Design.BudgetColors.LightFont;
            BorderStyle = BorderStyle.None;
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListBox.DrawItem" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data.</param>
        protected override void OnDrawItem( DrawItemEventArgs e )
        {
            e.DrawBackground( );

            if( ( e.State & DrawItemState.Selected ) == DrawItemState.Selected )
            {
                using( var solidBrush = new SolidBrush( _SelectionColor ) )
                {
                    e.Graphics.FillRectangle( solidBrush, e.Bounds );
                }
            }

            using( var pen = new Pen( _BorderColor ) )
            {
                e.Graphics.DrawRectangle( pen, 0, 0, checked( Width - 1 ), checked( Height - 1 ) );
            }

            if( Items.Count > 0 )
            {
                using( var solidBrush1 = new SolidBrush( e.ForeColor ) )
                {
                    e.Graphics.DrawString(
                        GetItemText( RuntimeHelpers.GetObjectValue( Items[ e.Index ] ) ), e.Font,
                        solidBrush1, e.Bounds );
                }
            }

            e.DrawFocusRectangle( );
            base.OnDrawItem( e );
        }

        /// <summary>
        /// Specifies when the window handle has been created so that column width and other characteristics can be set. Inheriting classes should call base.OnHandleCreated.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnHandleCreated( EventArgs e )
        {
            Select( );
            base.OnHandleCreated( e );
        }

        #endregion
    }
}