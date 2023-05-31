// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroCircularProgress.cs" company="Terry D. Eppler">
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
//   MetroCircularProgress.cs
// </summary>
// ******************************************************************************************

namespace BudgetExecution
{
	using Microsoft.VisualBasic.CompilerServices;
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.ComponentModel.Design;
	using System.Diagnostics;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Text;
	using System.Windows.Forms;
	
	/// <summary>
	/// A class collection for rendering metro-style progress.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Control" />
	[DefaultEvent("Click")]
	[Description("A control for rendering a circular progress.")]
	[Designer(typeof(MetroCircularProgressDesigner))]
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(MetroCircularProgress), "MetroCircularProgress.bmp")]
	public class MetroCircularProgress : Control
	{

		#region Private Fields
		/// <summary>
		/// The inner circle size
		/// </summary>
		private int _InnerCircleSize;

		/// <summary>
		/// The style
		/// </summary>
		private Design.Style _Style;

		/// <summary>
		/// The value
		/// </summary>
		private int _Value;

		/// <summary>
		/// The thickness
		/// </summary>
		private int _Thickness;

		/// <summary>
		/// The draw inner circle
		/// </summary>
		private bool _DrawInnerCircle;

		/// <summary>
		/// The draw outlines
		/// </summary>
		private bool _DrawOutlines;

		/// <summary>
		/// The draw percentage
		/// </summary>
		private bool _DrawPercentage;

		/// <summary>
		/// The draw percentage symbol
		/// </summary>
		private bool _DrawPercentageSymbol;

		/// <summary>
		/// The percentage symbol
		/// </summary>
		private string _PercentageSymbol;
		#endregion

		#region Public Properties        
		/// <summary>
		/// Gets or sets the color scheme.
		/// </summary>
		/// <value>The color scheme.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("sets the color scheme.")]
		[ReadOnly(false)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public MetroCircularProgress.MainColorScheme ColorScheme
		{
			[DebuggerNonUserCode]
			get;
			[DebuggerNonUserCode]
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether to draw the inner circle.
		/// </summary>
		/// <value><c>true</c> if draw inner circle; otherwise, <c>false</c>.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Sets a value indicating whether to draw the inner circle.")]
		public bool DrawInnerCircle
		{
			get
			{
				return this._DrawInnerCircle;
			}
			set
			{
				if (value != this._DrawInnerCircle)
				{
					this._DrawInnerCircle = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to draw outlines.
		/// </summary>
		/// <value><c>true</c> if draw outlines; otherwise, <c>false</c>.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Sets a value indicating whether to draw outlines.")]
		public bool DrawOutlines
		{
			get
			{
				return this._DrawOutlines;
			}
			set
			{
				if (value != this._DrawOutlines)
				{
					this._DrawOutlines = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to draw percentage.
		/// </summary>
		/// <value><c>true</c> if [draw percentage]; otherwise, <c>false</c>.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Sets a value indicating whether to draw percentage.")]
		public bool DrawPercentage
		{
			get
			{
				return this._DrawPercentage;
			}
			set
			{
				if (value != this._DrawPercentage)
				{
					this._DrawPercentage = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to draw percentage symbol.
		/// </summary>
		/// <value><c>true</c> if draw percentage symbol; otherwise, <c>false</c>.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Sets a value indicating whether to draw percentage symbol.")]
		public bool DrawPercentageSymbol
		{
			get
			{
				return this._DrawPercentageSymbol;
			}
			set
			{
				if (value != this._DrawPercentageSymbol)
				{
					this._DrawPercentageSymbol = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the size of the inner circle.
		/// </summary>
		/// <value>The size of the inner circle.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(80)]
		[Description("Sets the size of the inner circle.")]
		public int InnerCircleSize
		{
			get
			{
				return this._InnerCircleSize;
			}
			set
			{
				if (value != this._InnerCircleSize)
				{
					this._InnerCircleSize = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the percentage symbol.
		/// </summary>
		/// <value>The percentage symbol.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue("%")]
		[Description("Sets the percentage symbol.")]
		public string PercentageSymbol
		{
			get
			{
				return this._PercentageSymbol;
			}
			set
			{
				if (Operators.CompareString(value, this._PercentageSymbol, false) != 0)
				{
					this._PercentageSymbol = value;
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
					switch (this._Style)
					{
						case Design.Style.Light:
							{
								this.ColorScheme._OutlineColor = Color.FromArgb(35, 0, 0, 0);
								this.ColorScheme._InnerCircleColor = Color.FromArgb(200, 200, 200);
								this.ColorScheme._ProgressColor = Color.FromArgb(54, 116, 178);
								this.ColorScheme._CircleColor = this.ColorScheme.SetShade(Color.White, -5);
								this.ForeColor = Color.Black;
								break;
							}
						case Design.Style.Dark:
							{
								this.ColorScheme._OutlineColor = Color.FromArgb(35, 0, 0, 0);
								this.ColorScheme._InnerCircleColor = this.ColorScheme.SetShade(Color.FromArgb(98, 98, 98), -50);
								this.ColorScheme._ProgressColor = Color.FromArgb(54, 116, 178);
								this.ColorScheme._CircleColor = this.ColorScheme.SetShade(Color.FromArgb(40, 40, 40), 5);
								this.ForeColor = Color.FromArgb(98, 98, 98);
								break;
							}


					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the thickness.
		/// </summary>
		/// <value>The thickness.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(10)]
		[Description("Sets the thickness.")]
		public int Thickness
		{
			get
			{
				return this._Thickness;
			}
			set
			{
				if (value != this._Thickness)
				{
					if (value > 4)
					{
						this._Thickness = value;
						this.Invalidate();
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		[Browsable(true)]
		[Category("Data")]
		[DefaultValue(50)]
		[Description("Sets the value.")]
		public int Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if (value != this._Value)
				{
					this._Value = value;
					this.OnValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}


		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="MetroCircularProgress" /> class.
		/// </summary>
		public MetroCircularProgress()
		{
			this._InnerCircleSize = 80;
			this._Style = Design.Style.Light;
			this._Value = 50;
			this._Thickness = 10;
			this._DrawInnerCircle = true;
			this._DrawOutlines = false;
			this._DrawPercentage = true;
			this._DrawPercentageSymbol = true;
			this._PercentageSymbol = "%";
			this.ColorScheme = new MetroCircularProgress.MainColorScheme();
			this.Size = new System.Drawing.Size(128, 128);
			this.ForeColor = Color.White;
			this.Font = new System.Drawing.Font("Segoe UI Light", 15f);
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.UpdateStyles();
		}


		/// <summary>
		/// Draws the progress.
		/// </summary>
		/// <param name="g">The g.</param>
		/// <param name="rect">The rect.</param>
		/// <param name="percentage">The percentage.</param>
		private void DrawProgress(Graphics g, Rectangle rect, float percentage)
		{
			float single = (float)(3.6 * (double)percentage);
			float single1 = 360f - single;
			using (Pen pen = new Pen(this.ColorScheme._ProgressColor, (float)this._Thickness))
			{
				using (Pen pen1 = new Pen(this.ColorScheme._CircleColor, (float)(checked(this._Thickness - 4))))
				{
					g.SmoothingMode = SmoothingMode.AntiAlias;
					if (this._DrawOutlines)
					{
						g.DrawArc(new Pen(this.ColorScheme._OutlineColor, (float)this._Thickness), rect, single - 90f, single1);
					}
					g.DrawArc(pen, rect, -90f, single);
					g.DrawArc(pen1, rect, single - 90f, single1);
				}
			}
			if (this._DrawPercentage)
			{
				using (System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size))
				{
					string str = percentage.ToString();
					if (this._DrawPercentageSymbol)
					{
						str = string.Concat(str, this._PercentageSymbol);
					}
					SizeF sizeF = g.MeasureString(str, font);
					Point point = new Point(checked((int)Math.Round((double)rect.Left + (double)rect.Width / 2 - (double)(sizeF.Width / 2f))), checked((int)Math.Round((double)rect.Top + (double)rect.Height / 2 - (double)(sizeF.Height / 2f))));
					g.DrawString(str, font, new SolidBrush(this.ForeColor), point);
				}
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(Parent.BackColor);
			Rectangle rectangle = new Rectangle(checked((int)Math.Round((double)this.Width / 2 - (double)this._InnerCircleSize / 2 - 1)), checked((int)Math.Round((double)this.Height / 2 - (double)this._InnerCircleSize / 2 - 1)), this._InnerCircleSize, this._InnerCircleSize);
			if (this._DrawInnerCircle)
			{
				graphics.FillEllipse(new SolidBrush(this.ColorScheme._InnerCircleColor), rectangle);
			}
			Rectangle rectangle1 = new Rectangle(checked((int)Math.Round((double)this._Thickness / 2)), checked((int)Math.Round((double)this._Thickness / 2)), checked(this.Width - (checked(this._Thickness + 1))), checked(this.Height - (checked(this._Thickness + 1))));
			this.DrawProgress(graphics, rectangle1, (float)this._Value);
		}

		/// <summary>
		/// Handles the <see cref="E:ValueChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = this.ValueChanged;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>
		/// Occurs when [value changed].
		/// </summary>
		[Category("Data")]
		[Description("Wird ausgelöst sobald der Fortschritt verändert wurde.")]
		public event EventHandler ValueChanged;

		/// <summary>
		/// Class MainColorScheme.
		/// </summary>
		public class MainColorScheme
		{
			/// <summary>
			/// The outline color
			/// </summary>
			public Color _OutlineColor;

			/// <summary>
			/// The inner circle color
			/// </summary>
			public Color _InnerCircleColor;

			/// <summary>
			/// The progress color
			/// </summary>
			public Color _ProgressColor;

			/// <summary>
			/// The circle color
			/// </summary>
			public Color _CircleColor;

			/// <summary>
			/// Gets or sets the color of the circle.
			/// </summary>
			/// <value>The color of the circle.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Die Farbe des Kreises")]
			public Color CircleColor
			{
				get
				{
					return this._CircleColor;
				}
				set
				{
					if (value != this._CircleColor)
					{
						this._CircleColor = value;
					}
				}
			}

			/// <summary>
			/// Gets or sets the color of the inner circle.
			/// </summary>
			/// <value>The color of the inner circle.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Die Farbe des inneren Kreises an.")]
			public Color InnerCircleColor
			{
				get
				{
					return this._InnerCircleColor;
				}
				set
				{
					if (value != this._InnerCircleColor)
					{
						this._InnerCircleColor = value;
					}
				}
			}

			/// <summary>
			/// Gets or sets the color of the outline.
			/// </summary>
			/// <value>The color of the outline.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Die Farbe der inneren sowie äußeren Umrandung des Kreises.")]
			public Color OutlineColor
			{
				get
				{
					return this._OutlineColor;
				}
				set
				{
					if (value != this._OutlineColor)
					{
						this._OutlineColor = value;
					}
				}
			}

			/// <summary>
			/// Gets or sets the color of the progress.
			/// </summary>
			/// <value>The color of the progress.</value>
			[Browsable(true)]
			[Category("Appearance")]
			[Description("Die Farbe des Fortschritts.")]
			public Color ProgressColor
			{
				get
				{
					return this._ProgressColor;
				}
				set
				{
					if (value != this._ProgressColor)
					{
						this._ProgressColor = value;
						this._InnerCircleColor = Color.FromArgb(100, this.SetShade(this._ProgressColor, 50));
					}
				}
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="MainColorScheme"/> class.
			/// </summary>
			public MainColorScheme()
			{
				this._OutlineColor = Color.FromArgb(35, 0, 0, 0);
				this._InnerCircleColor = Color.FromArgb(200, 200, 200);
				this._ProgressColor = Color.FromArgb(54, 116, 178);
				this._CircleColor = this.SetShade(Color.White, -5);
			}

			/// <summary>
			/// Sets the shade.
			/// </summary>
			/// <param name="InputColor">Color of the input.</param>
			/// <param name="Offset">The offset.</param>
			/// <returns>Color.</returns>
			public Color SetShade(Color InputColor, int Offset)
			{
				int r = checked(InputColor.R + Offset);
				int g = checked(InputColor.G + Offset);
				int b = checked(InputColor.B + Offset);
				if (r < 0)
				{
					r = checked(r * -1);
				}
				if (g < 0)
				{
					g = checked(g * -1);
				}
				if (b < 0)
				{
					b = checked(b * -1);
				}
				Color color = Color.FromArgb(Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
				return color;
			}
		}
		
	}



	#region Smart Tag Code

	#region Cut and Paste it on top of the component class

	//--------------- [Designer(typeof(MetroCircularProgressDesigner))] --------------------//
	#endregion
	
	#region SmartTagActionList
	/// <summary>
	/// Class MetroCircularProgressSmartTagActionList.
	/// </summary>
	/// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
	public class MetroCircularProgressSmartTagActionList : System.ComponentModel.Design.DesignerActionList
	{
		//Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
		/// <summary>
		/// The col user control
		/// </summary>
		private MetroCircularProgress colUserControl;


		/// <summary>
		/// The designer action UI SVC
		/// </summary>
		private DesignerActionUIService designerActionUISvc = null;


		/// <summary>
		/// Initializes a new instance of the <see cref="MetroCircularProgressSmartTagActionList"/> class.
		/// </summary>
		/// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
		public MetroCircularProgressSmartTagActionList(IComponent component) : base(component)
		{
			this.colUserControl = component as MetroCircularProgress;

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
		/// Gets or sets a value indicating whether [draw inner circle].
		/// </summary>
		/// <value><c>true</c> if [draw inner circle]; otherwise, <c>false</c>.</value>
		public bool DrawInnerCircle
		{
			get
			{
				return colUserControl.DrawInnerCircle;
			}
			set
			{
				GetPropertyByName("DrawInnerCircle").SetValue(colUserControl, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [draw outlines].
		/// </summary>
		/// <value><c>true</c> if [draw outlines]; otherwise, <c>false</c>.</value>
		public bool DrawOutlines
		{
			get
			{
				return colUserControl.DrawOutlines;
			}
			set
			{
				GetPropertyByName("DrawOutlines").SetValue(colUserControl, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [draw percentage].
		/// </summary>
		/// <value><c>true</c> if [draw percentage]; otherwise, <c>false</c>.</value>
		public bool DrawPercentage
		{
			get
			{
				return colUserControl.DrawPercentage;
			}
			set
			{
				GetPropertyByName("DrawPercentage").SetValue(colUserControl, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [draw percentage symbol].
		/// </summary>
		/// <value><c>true</c> if [draw percentage symbol]; otherwise, <c>false</c>.</value>
		public bool DrawPercentageSymbol
		{
			get
			{
				return colUserControl.DrawPercentageSymbol;
			}
			set
			{
				GetPropertyByName("DrawPercentageSymbol").SetValue(colUserControl, value);
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
				GetPropertyByName("Style").SetValue(colUserControl, value);
			}
		}

		/// <summary>
		/// Gets or sets the size of the inner circle.
		/// </summary>
		/// <value>The size of the inner circle.</value>
		public int InnerCircleSize
		{
			get
			{
				return colUserControl.InnerCircleSize;
			}
			set
			{
				GetPropertyByName("InnerCircleSize").SetValue(colUserControl, value);
			}
		}

		/// <summary>
		/// Gets or sets the percentage symbol.
		/// </summary>
		/// <value>The percentage symbol.</value>
		public string PercentageSymbol
		{
			get
			{
				return colUserControl.PercentageSymbol;
			}
			set
			{
				GetPropertyByName("PercentageSymbol").SetValue(colUserControl, value);
			}
		}

		/// <summary>
		/// Gets or sets the thickness.
		/// </summary>
		/// <value>The thickness.</value>
		public int Thickness
		{
			get
			{
				return colUserControl.Thickness;
			}
			set
			{
				GetPropertyByName("Thickness").SetValue(colUserControl, value);
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public int Value
		{
			get
			{
				return colUserControl.Value;
			}
			set
			{
				GetPropertyByName("Value").SetValue(colUserControl, value);
			}
		}

		/// <summary>
		/// Gets or sets the color of the circle.
		/// </summary>
		/// <value>The color of the circle.</value>
		public Color CircleColor
		{
			get
			{
				return colUserControl.ColorScheme.CircleColor;
			}
			set
			{
				this.colUserControl.ColorScheme.CircleColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the inner circle.
		/// </summary>
		/// <value>The color of the inner circle.</value>
		public Color InnerCircleColor
		{
			get
			{
				return colUserControl.ColorScheme.InnerCircleColor;
			}
			set
			{
				this.colUserControl.ColorScheme.InnerCircleColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the outline.
		/// </summary>
		/// <value>The color of the outline.</value>
		public Color OutlineColor
		{
			get
			{
				return colUserControl.ColorScheme.OutlineColor;
			}
			set
			{
				this.colUserControl.ColorScheme.OutlineColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the progress.
		/// </summary>
		/// <value>The color of the progress.</value>
		public Color ProgressColor
		{
			get
			{
				return colUserControl.ColorScheme.ProgressColor;
			}
			set
			{
				this.colUserControl.ColorScheme.ProgressColor = value;
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


			items.Add(new DesignerActionPropertyItem("DrawInnerCircle",
				"Show Inner Circle", "Behaviour",
				"Set to show the inner circle."));

			items.Add(new DesignerActionPropertyItem("DrawOutlines",
				"Show Outlines", "Behaviour",
				"Set to show the outlines."));

			items.Add(new DesignerActionPropertyItem("DrawPercentage",
				"Show Percentage", "Behaviour",
				"Set to show the percentage."));

			items.Add(new DesignerActionPropertyItem("DrawPercentageSymbol",
				"Show Percentage Symbol", "Behaviour",
				"Set to show the percentage symbol."));

			items.Add(new DesignerActionHeaderItem("Appearance"));

			items.Add(new DesignerActionPropertyItem("CircleColor",
								 "Circle Color", "Appearance",
								 "Sets the circle color."));

			items.Add(new DesignerActionPropertyItem("InnerCircleColor",
								 "Inner Circle Color", "Appearance",
								 "Sets the inner circle color."));

			items.Add(new DesignerActionPropertyItem("OutlineColor",
				"Outline Color", "Appearance",
				"Sets the outline color."));

			items.Add(new DesignerActionPropertyItem("ProgressColor",
				"Progress Color", "Appearance",
				"Sets the progress color."));

			items.Add(new DesignerActionPropertyItem("Style",
				"Style", "Appearance",
				"Sets the style."));

			items.Add(new DesignerActionPropertyItem("InnerCircleSize",
				"Inner Circle Size", "Appearance",
				"Sets the inner circle size."));


			items.Add(new DesignerActionPropertyItem("PercentageSymbol",
				"Percentage Symbol", "Appearance",
				"Sets the percentage symbol."));

			items.Add(new DesignerActionPropertyItem("Thickness",
				"Thickness", "Appearance",
				"Sets the thickness."));

			items.Add(new DesignerActionPropertyItem("Value",
				"Value", "Appearance",
				"Sets the progress value."));

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