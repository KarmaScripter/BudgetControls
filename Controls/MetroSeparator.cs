// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroSeparator.cs" company="Terry D. Eppler">
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
//   MetroSeparator.cs
// </summary>
// ******************************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// A class collection for rendering a metro-style seperator.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("Click")]
	[Designer(typeof(MetroSeparatorDesigner))]
	[DesignerCategory("Value")]
	[ToolboxBitmap(typeof(MetroSeparator), "MetroSeperator.bmp")]
	public class MetroSeparator : Control
	{

        #region Private Fields
        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// The orientation
        /// </summary>
        private Design.Orientation _Orientation;
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the color scheme.
        /// </summary>
        /// <value>The color scheme.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Sets the color scheme.")]
        [ReadOnly(false)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MetroSeparator.MainColorScheme ColorScheme
        {
            [DebuggerNonUserCode]
            get;
            [DebuggerNonUserCode]
            set;
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("Sets the orientation.")]
        public Design.Orientation Orientation
        {
            get
            {
                return this._Orientation;
            }
            set
            {
                if (value != this._Orientation)
                {
                    this._Orientation = value;
                    if (value != Design.Orientation.Horizontal)
                    {
                        this.Height = this.Width;
                        this.Width = 2;
                    }
                    else
                    {
                        this.Width = this.Height;
                        this.Height = 2;
                    }
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("Sets the style.")]
        public Design.Style Style
        {
            get
            {
                return this._Style;
            }
            set
            {
                if (value != this._Style)
                {
                    this._Style = value;
                    if (this.Style == Design.Style.Light)
                    {
                        this.ColorScheme._Color1 = Color.FromArgb(98, 98, 98);
                        this.ColorScheme._Color2 = Color.White;
                    }
                    else if (this.Style != Design.Style.Dark)
                    {
                        this.Style = Design.Style.Custom;
                    }
                    else
                    {
                        this.ColorScheme._Color1 = Color.FromArgb(51, 51, 51);
                        this.ColorScheme._Color2 = Color.FromArgb(98, 98, 98);
                    }
                    this.Invalidate();
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroSeparator" /> class.
        /// </summary>
        public MetroSeparator()
		{
			this._Style = Design.Style.Light;
			this._Orientation = Design.Orientation.Horizontal;
			this.ColorScheme = new MetroSeparator.MainColorScheme();
			this.Height = 2;
			this._Style = Design.Style.Light;
		}


        /// <summary>
        /// Handles the <see cref="E:PaintBackground" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			if (this.Orientation != Design.Orientation.Horizontal)
			{
				e.Graphics.DrawLine(new Pen(this.ColorScheme.Color1), 0, 0, 0, this.Height);
				e.Graphics.DrawLine(new Pen(this.ColorScheme.Color2), 1, 0, 1, this.Height);
			}
			else
			{
				e.Graphics.DrawLine(new Pen(this.ColorScheme.Color1), 0, 0, this.Width, 0);
				e.Graphics.DrawLine(new Pen(this.ColorScheme.Color2), 0, 1, this.Width, 1);
			}
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.Orientation != Design.Orientation.Horizontal)
			{
				this.Width = 2;
			}
			else
			{
				this.Height = 2;
			}
		}

        /// <summary>
        /// Class MainColorScheme.
        /// </summary>
        public class MainColorScheme
		{
            /// <summary>
            /// The color1
            /// </summary>
            public Color _Color1;

            /// <summary>
            /// The color2
            /// </summary>
            public Color _Color2;

            /// <summary>
            /// Gets or sets the color1.
            /// </summary>
            /// <value>The color1.</value>
            [Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Hauptfarbe des Seperators.")]
			public Color Color1
			{
				get
				{
					return this._Color1;
				}
				set
				{
					if (value != this._Color1)
					{
						this._Color1 = value;
					}
				}
			}

            /// <summary>
            /// Gets or sets the color2.
            /// </summary>
            /// <value>The color2.</value>
            [Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Farbe für den Akzent des Seperators.")]
			public Color Color2
			{
				get
				{
					return this._Color2;
				}
				set
				{
					if (value != this.Color2)
					{
						this._Color2 = value;
					}
				}
			}

            /// <summary>
            /// Initializes a new instance of the <see cref="MainColorScheme"/> class.
            /// </summary>
            public MainColorScheme()
			{
				this._Color1 = Color.FromArgb(98, 98, 98);
				this._Color2 = Color.White;
			}
		}
	}
}