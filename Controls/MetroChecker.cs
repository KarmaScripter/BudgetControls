// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroChecker.cs" company="Terry D. Eppler">
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
//   MetroChecker.cs
// </summary>
// ******************************************************************************************

namespace BudgetExecution
{
	using System;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	
	/// <summary>
	/// A class collection for rendering metro-style checker.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Control" />
	[DefaultEvent("CheckedChanged")]
	[Description("Sets a metro-style checker.")]
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(CheckBox))]
	public class MetroChecker : Control
	{
		#region Private Fields
		//private static List<WeakReference> __ENCList;

		/// <summary>
		/// The default color
		/// </summary>
		private Color _DefaultColor;

		/// <summary>
		/// The hover color
		/// </summary>
		private Color _HoverColor;

		/// <summary>
		/// The pressed color
		/// </summary>
		private Color _PressedColor;

		/// <summary>
		/// The check color
		/// </summary>
		private Color _CheckColor;

		/// <summary>
		/// The checked border color
		/// </summary>
		private Color _CheckedBorderColor;

		/// <summary>
		/// The checked
		/// </summary>
		private bool _Checked;

		/// <summary>
		/// The checker width
		/// </summary>
		private readonly int _CheckerWidth;

		/// <summary>
		/// The bounds width
		/// </summary>
		private int _BoundsWidth;

		/// <summary>
		/// The checker symbol
		/// </summary>
		private MetroChecker.MetroCheckerSymbol _CheckerSymbol;

		/// <summary>
		/// The style
		/// </summary>
		private Design.Style _Style;

		/// <summary>
		/// The text
		/// </summary>
		private string _Text;

		/// <summary>
		/// The automatic style
		/// </summary>
		private bool _AutoStyle;

		/// <summary>
		/// The mouse state
		/// </summary>
		private Helpers.MouseState _MouseState;
		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets a value indicating whether to automatically style the control.
		/// </summary>
		/// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Sets a value indicating whether to automatically style the control.")]
		public bool AutoStyle
		{
			get
			{
				return this._AutoStyle;
			}
			set
			{
				if (this._AutoStyle != value)
				{
					this._AutoStyle = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the background image displayed in the control.
		/// </summary>
		/// <value>The background image.</value>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Image BackgroundImage
		{
			[DebuggerNonUserCode]
			get;
			[DebuggerNonUserCode]
			set;
		}

		/// <summary>
		/// Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.
		/// </summary>
		/// <value>The background image layout.</value>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImageLayout BackgroundImageLayout
		{
			[DebuggerNonUserCode]
			get;
			[DebuggerNonUserCode]
			set;
		}

		/// <summary>
		/// Gets or sets the color of the check.
		/// </summary>
		/// <value>The color of the check.</value>
		[Category("Appearance")]
		[Description("Sets the color of the check.")]
		public Color CheckColor
		{
			get
			{
				return this._CheckColor;
			}
			set
			{
				if (this._CheckColor != value)
				{
					this._CheckColor = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MetroChecker" /> is checked.
		/// </summary>
		/// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Sets a value indicating whether this control should be checked.")]
		public bool Checked
		{
			get
			{
				return this._Checked;
			}
			set
			{
				_Checked = value;
				this.CheckChanged(this, EventArgs.Empty);
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the color of the checked border.
		/// </summary>
		/// <value>The color of the checked border.</value>
		[Category("Appearance")]
		[Description("Sets the color of the checked border.")]
		public Color CheckedBorderColor
		{
			get
			{
				return this._CheckedBorderColor;
			}
			set
			{
				if (this._CheckedBorderColor != value)
				{
					this._CheckedBorderColor = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the checker symbol.
		/// </summary>
		/// <value>The checker symbol.</value>
		[Category("Appearance")]
		[DefaultValue(0)]
		[Description("Sets the checker symbol.")]
		public MetroChecker.MetroCheckerSymbol CheckerSymbol
		{
			get
			{
				return this._CheckerSymbol;
			}
			set
			{
				if (this._CheckerSymbol != value)
				{
					this._CheckerSymbol = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.
		/// </summary>
		/// <value>The context menu strip.</value>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip
		{
			[DebuggerNonUserCode]
			get;
			[DebuggerNonUserCode]
			set;
		}

		/// <summary>
		/// Gets or sets the default color.
		/// </summary>
		/// <value>The default color.</value>
		[Category("Appearance")]
		[Description("Sets the default color.")]
		public Color DefaultColor
		{
			get
			{
				return this._DefaultColor;
			}
			set
			{
				if (this._DefaultColor != value)
				{
					this._DefaultColor = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the color when the control is hovered.
		/// </summary>
		/// <value>The color of the hover.</value>
		[Category("Appearance")]
		[Description("Sets the color of the hover.")]
		public Color HoverColor
		{
			get
			{
				return this._HoverColor;
			}
			set
			{
				if (this._HoverColor != value)
				{
					this._HoverColor = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the color when the control is pressed.
		/// </summary>
		/// <value>The color of the control when it is pressed.</value>
		[Category("Appearance")]
		[Description("Sets the color when the control is pressed.")]
		public Color PressedColor
		{
			get
			{
				return this._PressedColor;
			}
			set
			{
				if (this._PressedColor != value)
				{
					this._PressedColor = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
		/// </summary>
		/// <value>The right to left.</value>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new System.Windows.Forms.RightToLeft RightToLeft
		{
			[DebuggerNonUserCode]
			get;
			[DebuggerNonUserCode]
			set;
		}

		/// <summary>
		/// Gets or sets the style.
		/// </summary>
		/// <value>The style.</value>
		[Category("Appearance")]
		[DefaultValue(0)]
		[Description("Sets the style.")]
		[RefreshProperties(RefreshProperties.All)]
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
					switch (this._Style)
					{
						case Design.Style.Light:
							{
								this.BackColor = Design.MetroColors.LightDefault;
								this._DefaultColor = Design.MetroColors.LightHover;
								this._HoverColor = Design.MetroColors.LightDarkDefault;
								this._PressedColor = Design.MetroColors.AccentDarkBlue;
								this._CheckColor = Design.MetroColors.AccentBlue;
								this._CheckedBorderColor = Design.MetroColors.LightBorder;
								this.ForeColor = Design.MetroColors.LightFont;
								break;
							}
						case Design.Style.Dark:
							{
								this.BackColor = Design.MetroColors.DarkDefault;
								this._DefaultColor = Design.MetroColors.DarkHover;
								this._HoverColor = Design.MetroColors.LightBorder;
								this._PressedColor = Design.MetroColors.AccentDarkBlue;
								this._CheckColor = Design.MetroColors.AccentBlue;
								this._CheckedBorderColor = Design.MetroColors.LightBorder;
								this.ForeColor = Design.MetroColors.DarkFont;
								break;
							}
					}
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the text associated with this control.
		/// </summary>
		/// <value>The text.</value>
		public override string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				this._Text = value;
				this.Invalidate();
				this.Size = new System.Drawing.Size(this.GetMaxWidth(), 14);
			}
		}
		/**/
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="MetroChecker" /> class.
		/// </summary>
		public MetroChecker()
		{
			//MetroChecker.__ENCAddToList(this);
			this._DefaultColor = Design.MetroColors.LightHover;
			this._HoverColor = Design.MetroColors.LightDarkDefault;
			this._PressedColor = Design.MetroColors.AccentDarkBlue;
			this._CheckColor = Design.MetroColors.AccentBlue;
			this._CheckedBorderColor = Design.MetroColors.LightBorder;
			//this._Checked = false;
			this._CheckerWidth = 13;
			this._BoundsWidth = 16;
			this._CheckerSymbol = MetroChecker.MetroCheckerSymbol.Box;
			this._Style = Design.Style.Light;
			this._AutoStyle = true;
			this._MouseState = Helpers.MouseState.None;
			this.Font = new System.Drawing.Font("Segoe UI", 9f);
			this.Size = new System.Drawing.Size(this.GetMaxWidth(), 14);
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.BackColor = Color.Transparent;
			this.UpdateStyles();
		}


		/// <summary>
		/// Draws the tick.
		/// </summary>
		/// <param name="g">The g.</param>
		/// <param name="r">The r.</param>
		/// <param name="c">The c.</param>
		/// <param name="thickness">The thickness.</param>
		private void DrawTick(Graphics g, Rectangle r, Color c, int thickness = 1)
		{
			g.SmoothingMode = SmoothingMode.AntiAlias;
			using (Pen pen = new Pen(c, 2f))
			{
				Point point = new Point(checked(r.X + 3), checked(r.Y + 6));
				Point point1 = new Point(checked(r.X + 4), checked(r.Y + 10));
				g.DrawLine(pen, point, point1);
				point1 = new Point(checked(r.X + 4), checked(r.Y + 10));
				point = new Point(checked(r.X + 5), checked(r.Y + 10));
				g.DrawLine(pen, point1, point);
				point1 = new Point(checked(r.X + 5), checked(r.Y + 10));
				point = new Point(checked(r.X + 10), checked(r.Y + 3));
				g.DrawLine(pen, point1, point);
			}
		}

		/// <summary>
		/// Gets the maximum width.
		/// </summary>
		/// <returns>System.Int32.</returns>
		private int GetMaxWidth()
		{
			int num;
			using (Graphics graphic = this.CreateGraphics())
			{
				SizeF sizeF = graphic.MeasureString(this._Text, this.Font);
				this._BoundsWidth = checked((int)Math.Round((double)((float)(sizeF.Width + 16f))));
				num = this._BoundsWidth;
			}
			return num;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnBackColorChanged(EventArgs e)
		{
			if (this.FindForm() is MetroForm)
			{
				if (this._AutoStyle)
				{
					this.Style = ((MetroForm)this.FindForm()).Style;
					this.Invalidate();
				}
			}
			base.OnBackColorChanged(e);
		}

		/// <summary>
		/// The checking
		/// </summary>
		private int checking = 0;

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnClick(EventArgs e)
		{
			#region Old Code
			//         //this._Checked ^= 1;
			//         this._Checked = true;
			//         //Checked = true;
			//         MetroSwitch.CheckedChangedEventHandler checkedChangedEventHandler = this.CheckedChanged;
			//if (checkedChangedEventHandler != null)
			//{
			//	checkedChangedEventHandler(this, this._Checked);
			//} 
			#endregion

			_Checked = !_Checked;
			if (checkedChanged != null)
			{
				checkedChanged(this, EventArgs.Empty);
			}

			this.Invalidate();
			base.OnClick(e);
		}

		#region Event Creation

		/////Implement this in the Property you want to trigger the event///////////////////////////
		// 
		//  For Example this will be triggered by the Value Property
		//
		//  public int Value
		//   { 
		//      get { return _value;}
		//      set
		//         {
		//          
		//              _value = value;
		//
		//              this.CheckChanged(EventArgs.Empty);
		//              Invalidate();
		//          }
		//    }
		//
		////////////////////////////////////////////////////////////////////////////////////////////


		/// <summary>
		/// The checked changed
		/// </summary>
		private CheckedChangedEventHandler checkedChanged;

		/// <summary>
		/// Delegate CheckedChangedEventHandler
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

		/// <summary>
		/// Triggered when the Value changes
		/// </summary>

		public event CheckedChangedEventHandler CheckedChanged
		{
			add
			{
				this.checkedChanged = this.checkedChanged + value;
			}
			remove
			{
				this.checkedChanged = this.checkedChanged - value;
			}
		}

		/// <summary>
		/// Checks the changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected virtual void CheckChanged(object sender, EventArgs e)
		{
			if (this.checkedChanged == null)
				return;
			this.checkedChanged((object)this, e);
		}

		#endregion

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnGotFocus(EventArgs e)
		{
			this._MouseState = Helpers.MouseState.Over;
			this.Invalidate();
			base.OnGotFocus(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnLeave(EventArgs e)
		{
			this._MouseState = Helpers.MouseState.None;
			this.Invalidate();
			base.OnLeave(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnLostFocus(EventArgs e)
		{
			this._MouseState = Helpers.MouseState.None;
			this.Invalidate();
			base.OnLostFocus(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this._MouseState = Helpers.MouseState.Pressed;
			this.Invalidate();
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			this._MouseState = Helpers.MouseState.Over;
			this.Invalidate();
			base.OnMouseEnter(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			this._MouseState = Helpers.MouseState.None;
			this.Invalidate();
			base.OnMouseLeave(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this._MouseState = Helpers.MouseState.Over;

			Checked = !Checked;
			Focus();
			this.Invalidate();
			base.OnMouseUp(e);
		}


		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Rectangle rectangle;
			Graphics graphics = e.Graphics;
			Rectangle rectangle1 = new Rectangle(0, 0, this._CheckerWidth, this._CheckerWidth);
			Rectangle rectangle2 = new Rectangle(3, 3, checked(this._CheckerWidth - 6), checked(this._CheckerWidth - 6));
			Point location = rectangle2.Location;
			Point point = new Point(checked(rectangle2.X + this.Width), checked(rectangle2.Y + this.Height));
			using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(location, point, this._CheckColor, Design.MetroColors.ChangeColorBrightness(this._CheckColor, -0.2f)))
			{
				using (Pen pen = new Pen(this._DefaultColor))
				{
					switch (this._MouseState)
					{
						case Helpers.MouseState.None:
						{
							pen.Color = this._DefaultColor;
							break;
						}
						case Helpers.MouseState.Over:
						{
							pen.Color = this._HoverColor;
							break;
						}
						case Helpers.MouseState.Pressed:
						{
							pen.Color = this._PressedColor;
							break;
						}
					}
					if (this._Checked)
					{
						pen.Color = this._CheckedBorderColor;
					}
					switch (this._CheckerSymbol)
					{
						case MetroChecker.MetroCheckerSymbol.Box:
						{
							graphics.DrawRectangle(pen, rectangle1);
							if (this._Checked)
							{
								rectangle = new Rectangle(3, 3, checked(this._CheckerWidth - 5), checked(this._CheckerWidth - 5));
								graphics.FillRectangle(linearGradientBrush, rectangle);
							}
							break;
						}
						case MetroChecker.MetroCheckerSymbol.Circle:
						{
							graphics.SmoothingMode = SmoothingMode.AntiAlias;
							graphics.DrawEllipse(pen, rectangle1);
							if (this._Checked)
							{
								graphics.FillEllipse(linearGradientBrush, rectangle2);
							}
							break;
						}
						case MetroChecker.MetroCheckerSymbol.BoxWithTick:
						{
							graphics.DrawRectangle(pen, rectangle1);
							if (this._Checked)
							{
								rectangle = new Rectangle(rectangle1.X, rectangle1.Y, 16, 16);
								this.DrawTick(graphics, rectangle, this._CheckColor, 2);
							}
							break;
						}
						case MetroChecker.MetroCheckerSymbol.CircleWithTick:
						{
							graphics.SmoothingMode = SmoothingMode.AntiAlias;
							graphics.DrawEllipse(pen, rectangle1);
							if (!this._Checked)
							{
								break;
							}
							rectangle = new Rectangle(rectangle1.X, rectangle1.Y, 16, 16);
							this.DrawTick(graphics, rectangle, this._CheckColor, 2);
							break;
						}
					}
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
					{
						string text = this.Text;
						System.Drawing.Font font = this.Font;
						point = new Point(16, -1);
						graphics.DrawString(text, font, solidBrush, point);
					}
				}
			}
			base.OnPaint(e);
		}

		/// <summary>
		/// Performs the work of setting the specified bounds of this control.
		/// </summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			height = 14;
			width = this._BoundsWidth;
			base.SetBoundsCore(x, y, width, height, specified);
		}

		//public event MetroChecker.CheckedChangedEventHandler CheckedChanged;

		//public delegate void CheckedChangedEventHandler(object sender, bool isChecked);

		/// <summary>
		/// Enum MetroCheckerSymbol
		/// </summary>
		public enum MetroCheckerSymbol
		{
			/// <summary>
			/// The box
			/// </summary>
			Box,
			/// <summary>
			/// The circle
			/// </summary>
			Circle,
			/// <summary>
			/// The box with tick
			/// </summary>
			BoxWithTick,
			/// <summary>
			/// The circle with tick
			/// </summary>
			CircleWithTick
		}
	}
}