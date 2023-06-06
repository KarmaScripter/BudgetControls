// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="BudgetPanelSelection.cs" company="Terry D. Eppler">
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
//   BudgetPanelSelection.cs
// </summary>
// ******************************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetExecution
{
	/// <summary>
	/// A class collection for rendering a panel selection.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Panel" />
	[DefaultEvent("Click")]
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(Panel))]
	[Designer(typeof(BudgetPanelSelectionDesigner))]
	public class BudgetPanelSelection : Panel
	{

		#region Private Fields
		/// <summary>
		/// The current state
		/// </summary>
		private BudgetPanelSelection.MouseState CurrentState;

		/// <summary>
		/// The style
		/// </summary>
		private Design.Style _Style = Design.Style.Custom;

		/// <summary>
		/// The check on click
		/// </summary>
		private bool _CheckOnClick;

		/// <summary>
		/// The draw borders
		/// </summary>
		private bool _DrawBorders;

		/// <summary>
		/// The headline
		/// </summary>
		private string _Headline;

		/// <summary>
		/// The sub text
		/// </summary>
		private string _SubText;

		/// <summary>
		/// The headline font
		/// </summary>
		private System.Drawing.Font _HeadlineFont;

		/// <summary>
		/// The sub text font
		/// </summary>
		private System.Drawing.Font _SubTextFont;

		/// <summary>
		/// The checked
		/// </summary>
		private bool _Checked;

		/// <summary>
		/// The fore color
		/// </summary>
		private Color[] foreColor = new Color[]
		{
			Color.Black,
			Color.Gray
		};
		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BudgetPanelSelection" /> is checked.
		/// </summary>
		/// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("sets a value indicating whether this control is checked.")]
		public bool Checked
		{
			get
			{
				return this._Checked;
			}
			set
			{
				this._Checked = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to check on click.
		/// </summary>
		/// <value><c>true</c> if check on click; otherwise, <c>false</c>.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Sets a value indicating whether to check on click.")]
		public bool CheckOnClick
		{
			get
			{
				return this._CheckOnClick;
			}
			set
			{
				this._CheckOnClick = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the color scheme.
		/// </summary>
		/// <value>The color scheme.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("Sets the color scheme.")]
		[ReadOnly(false)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public BudgetPanelSelection.MainColorScheme ColorScheme
		{
			[DebuggerNonUserCode]
			get;
			[DebuggerNonUserCode]
			set;
		}

		/// <summary>
		/// Gets a value indicating whether to draw the borders.
		/// </summary>
		/// <value><c>true</c> if [draw borders]; otherwise, <c>false</c>.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("Gets a value indicating whether to draw the borders.")]
		public bool DrawBorders
		{
			get
			{
				return this._DrawBorders;
			}
		}

		/// <summary>
		/// Gets or sets the headline font.
		/// </summary>
		/// <value>The headline font.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Sets the headline font.")]
		public System.Drawing.Font HeadlineFont
		{
			get
			{
				return this._HeadlineFont;
			}
			set
			{
				this._HeadlineFont = value;
			}
		}

		/// <summary>
		/// Gets or sets the style.
		/// </summary>
		/// <value>The style.</value>
		/// <exception cref="System.ArgumentOutOfRangeException">value - null</exception>
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
				this._Style = value;
				
				switch (value)
				{
					case Design.Style.Light:
						this.ColorScheme.HeaderColor = Color.Silver;
						this.ColorScheme.SubTextColor = Color.Gray;
						this.ColorScheme._BackgroundColorNormal = Color.White;
						this.ColorScheme._BorderColor = Color.FromArgb(0, 164, 240);
						this.ColorScheme._BorderColorN = Color.FromArgb(98, 98, 98);
						this.ColorScheme._BackgroundColorHover = Color.White;
						this.ColorScheme._BackgroundColorChecked = Color.FromArgb(101, 101, 101);
						break;
					case Design.Style.Dark:
						this.ColorScheme.HeaderColor = Color.Black;
						this.ColorScheme.SubTextColor = Color.DarkSlateGray;
						this.ColorScheme._BackgroundColorNormal = Color.FromArgb(40, 40, 40);
						this.ColorScheme._BorderColor = Color.FromArgb(0, 164, 240);
						this.ColorScheme._BorderColorN = Color.FromArgb(98, 98, 98);
						this.ColorScheme._BackgroundColorHover = Color.FromArgb(40, 40, 40);
						this.ColorScheme._BackgroundColorChecked = Color.FromArgb(21, 21, 21);
						break;
					case Design.Style.Custom:
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value), value, null);
				}

				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the sub text font.
		/// </summary>
		/// <value>The sub text font.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Sets the sub text font.")]
		public System.Drawing.Font SubTextFont
		{
			get
			{
				return this._SubTextFont;
			}
			set
			{
				this._SubTextFont = value;
			}
		}

		/// <summary>
		/// Gets or sets the text headline.
		/// </summary>
		/// <value>The text headline.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Sets the text headline.")]
		public string TextHeadline
		{
			get
			{
				return this._Headline;
			}
			set
			{
				this._Headline = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the text subline.
		/// </summary>
		/// <value>The text subline.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Sets the text subline.")]
		public string TextSubline
		{
			get
			{
				return this._SubText;
			}
			set
			{
				this._SubText = value;
				this.Invalidate();
			}
		}


		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="BudgetPanelSelection" /> class.
		/// </summary>
		public BudgetPanelSelection()
		{
			this.CurrentState = BudgetPanelSelection.MouseState.None;
			
			this._CheckOnClick = true;
			this._DrawBorders = true;
			this._Headline = "Headline Text";
			this._SubText = "Sub-Text";
			this._HeadlineFont = new System.Drawing.Font("Segoe UI Light", 14f);
			this._SubTextFont = new System.Drawing.Font("Segoe UI", 9f);
			this._Checked = false;
			this.ColorScheme = new BudgetPanelSelection.MainColorScheme();
			this.Font = new System.Drawing.Font("Segoe UI", 9f);
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.UpdateStyles();
			//this.Style = Design.Style.Light;
			this.Size = new System.Drawing.Size(275, 64);
		}


		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.CheckOnClick)
			{
				if (this.Checked)
				{
					this.Checked = false;
				}
				else if (!this.Checked)
				{
					this.Checked = true;
				}
			}
			this.CurrentState = BudgetPanelSelection.MouseState.Down;
			this.Invalidate();
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			this.CurrentState = BudgetPanelSelection.MouseState.Over;
			this.Invalidate();
			base.OnMouseEnter(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			this.CurrentState = BudgetPanelSelection.MouseState.None;
			this.Invalidate();
			base.OnMouseLeave(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.CurrentState = BudgetPanelSelection.MouseState.Over;
			this.Invalidate();
			base.OnMouseUp(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Color color = new Color();
			Graphics graphics = e.Graphics;
			graphics.Clear(this.BackColor);
			Point point = new Point(0, 0);
			Rectangle rectangle = new Rectangle(point, this.Size);
			ColorBlend colorBlend = new ColorBlend();
			if (this.CurrentState == BudgetPanelSelection.MouseState.Down & this.Checked)
			{
				color = this.ColorScheme._BackgroundColorChecked;
			}
			else if (this.CurrentState == BudgetPanelSelection.MouseState.Down & !this.Checked)
			{
				color = this.ColorScheme._BackgroundColorNormal;
			}
			else if (this.CurrentState == BudgetPanelSelection.MouseState.Over & !this.Checked)
			{
				color = this.ColorScheme._BackgroundColorHover;
			}
			else if (this.CurrentState == BudgetPanelSelection.MouseState.Over & this.Checked)
			{
				color = this.ColorScheme._BackgroundColorChecked;
			}
			else if (this.CurrentState == BudgetPanelSelection.MouseState.None & this.Checked)
			{
				color = this.ColorScheme._BackgroundColorChecked;
			}
			else if (this.CurrentState == BudgetPanelSelection.MouseState.None & !this.Checked)
			{
				color = this.ColorScheme._BackgroundColorNormal;
			}
			graphics.FillRectangle(new SolidBrush(color), rectangle);
			string str = this._Headline;
			System.Drawing.Font font = this._HeadlineFont;
			SolidBrush solidBrush = new SolidBrush(this.ColorScheme.HeaderColor);
			point = new Point(8, 5);
			graphics.DrawString(str, font, solidBrush, point);
			string str1 = this._SubText;
			System.Drawing.Font font1 = this._SubTextFont;
			SolidBrush solidBrush1 = new SolidBrush(this.ColorScheme.SubTextColor);
			Rectangle rectangle1 = new Rectangle(12, 35, checked(this.Width - 25), checked(this.Height - 50));
			graphics.DrawString(str1, font1, solidBrush1, rectangle1);
			if (this.DrawBorders)
			{
				if (this.CurrentState == BudgetPanelSelection.MouseState.Down)
				{
					Pen pen = new Pen(this.ColorScheme._BorderColor);
					rectangle1 = new Rectangle(0, 0, checked(this.Width - 1), checked(this.Height - 1));
					graphics.DrawRectangle(pen, rectangle1);
				}
				else if (this.CurrentState == BudgetPanelSelection.MouseState.None)
				{
					Pen pen1 = new Pen(this.ColorScheme._BorderColorN);
					rectangle1 = new Rectangle(0, 0, checked(this.Width - 1), checked(this.Height - 1));
					graphics.DrawRectangle(pen1, rectangle1);
				}
				else if (this.CurrentState == BudgetPanelSelection.MouseState.Over)
				{
					Pen pen2 = new Pen(this.ColorScheme._BorderColor);
					rectangle1 = new Rectangle(0, 0, checked(this.Width - 1), checked(this.Height - 1));
					graphics.DrawRectangle(pen2, rectangle1);
				}
			}
			base.OnPaint(e);
		}

		/// <summary>
		/// Class MainColorScheme.
		/// </summary>
		public class MainColorScheme
		{
			/// <summary>
			/// The border color n
			/// </summary>
			public Color _BorderColorN;

			/// <summary>
			/// The background color hover
			/// </summary>
			public Color _BackgroundColorHover;

			/// <summary>
			/// The background color checked
			/// </summary>
			public Color _BackgroundColorChecked;

			/// <summary>
			/// The background color normal
			/// </summary>
			public Color _BackgroundColorNormal;

			/// <summary>
			/// The border color
			/// </summary>
			public Color _BorderColor;

			/// <summary>
			/// Gets or sets the background color checked.
			/// </summary>
			/// <value>The background color checked.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Hintergrundfarbe bei aktivem Checked-Effekt.")]
			public Color BackgroundColorChecked
			{
				get
				{
					return this._BackgroundColorChecked;
				}
				set
				{
					this._BackgroundColorChecked = value;
				}
			}

			/// <summary>
			/// Gets or sets the background color hover.
			/// </summary>
			/// <value>The background color hover.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Hintergrundfarbe bei aktivem Hover-Effekt.")]
			public Color BackgroundColorHover
			{
				get
				{
					return this._BackgroundColorHover;
				}
				set
				{
					this._BackgroundColorHover = value;
				}
			}

			/// <summary>
			/// Gets or sets the background color normal.
			/// </summary>
			/// <value>The background color normal.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Hintergrundfarbe.")]
			public Color BackgroundColorNormal
			{
				get
				{
					return this._BackgroundColorNormal;
				}
				set
				{
					this._BackgroundColorNormal = value;
				}
			}

			/// <summary>
			/// Gets or sets the color of the effect border.
			/// </summary>
			/// <value>The color of the effect border.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Farbe für die Umrandung.")]
			public Color EffectBorderColor
			{
				get
				{
					return this._BorderColor;
				}
				set
				{
					this._BorderColor = value;
				}
			}

			/// <summary>
			/// Gets or sets the color of the normal border.
			/// </summary>
			/// <value>The color of the normal border.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Farbe der Umradung bei keinem Effekt.")]
			public Color NormalBorderColor
			{
				get
				{
					return this._BorderColorN;
				}
				set
				{
					this._BorderColorN = value;
				}
			}

			/// <summary>
			/// Gets or sets the color of the header.
			/// </summary>
			/// <value>The color of the header.</value>
			public Color HeaderColor { get; set; } = Color.Black;
			/// <summary>
			/// Gets or sets the color of the sub text.
			/// </summary>
			/// <value>The color of the sub text.</value>
			public Color SubTextColor { get; set; } = Color.DarkSlateGray;

			/// <summary>
			/// Initializes a new instance of the <see cref="MainColorScheme"/> class.
			/// </summary>
			public MainColorScheme()
			{
				this._BorderColorN = Color.FromArgb(98, 98, 98);
				this._BackgroundColorHover = Color.White;
				this._BackgroundColorChecked = Color.FromArgb(101, 101, 101);
				this._BackgroundColorNormal = Color.White;
				this._BorderColor = Color.FromArgb(0, 164, 240);
				
			}
		}

		/// <summary>
		/// Enum MouseState
		/// </summary>
		private enum MouseState
		{
			/// <summary>
			/// The none
			/// </summary>
			None,
			/// <summary>
			/// The over
			/// </summary>
			Over,
			/// <summary>
			/// Down
			/// </summary>
			Down
		}
	}
	
}