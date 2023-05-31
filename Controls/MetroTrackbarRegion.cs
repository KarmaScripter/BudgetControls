// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroTrackbarRegion.cs" company="Terry D. Eppler">
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
//   MetroTrackbarRegion.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace BudgetExecution
{
    /// <summary>
    /// Class MetroTrackbarRegion.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("Click")]
	[Designer(typeof(MetroTrackbarRegionDesigner))]
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(TrackBar))]
	public class MetroTrackbarRegion : Control
	{
        /// <summary>
        /// The enc list
        /// </summary>
        private static List<WeakReference> __ENCList;

        /// <summary>
        /// The maximum value
        /// </summary>
        public int maxVal;

        /// <summary>
        /// The minimum value
        /// </summary>
        public int minVal;

        /// <summary>
        /// The value
        /// </summary>
        public int val;

        /// <summary>
        /// The val2
        /// </summary>
        public int val2;

        /// <summary>
        /// The slider1 position
        /// </summary>
        private int slider1Pos;

        /// <summary>
        /// The slider2 position
        /// </summary>
        private int slider2Pos;

        /// <summary>
        /// The scrolling1
        /// </summary>
        private bool scrolling1;

        /// <summary>
        /// The scrolling2
        /// </summary>
        private bool scrolling2;

        /// <summary>
        /// The bar size
        /// </summary>
        private int BarSize;

        /// <summary>
        /// The slider width
        /// </summary>
        private int sliderWidth;

        /// <summary>
        /// The slider height
        /// </summary>
        private const int sliderHeight = 16;

        /// <summary>
        /// The use switch borders
        /// </summary>
        private bool _UseSwitchBorders;

        /// <summary>
        /// The use fixed size
        /// </summary>
        private bool _UseFixedSize;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        /// <summary>
        /// Gets or sets the color scheme.
        /// </summary>
        /// <value>The color scheme.</value>
        [Browsable(true)]
		[Category("Appearance")]
		[Description("Eine Ober-Eigenschaft, die alle Farbeigenschaften enthält.")]
		[ReadOnly(false)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public MetroTrackbarRegion.MainColorScheme ColorScheme
		{
			[DebuggerNonUserCode]
			get;
			[DebuggerNonUserCode]
			set;
		}

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(100)]
		[Description("Setzt den maximalen Wert der Trackbar.")]
		public int Maximum
		{
			get
			{
				return this.maxVal;
			}
			set
			{
				this.maxVal = value;
				if (this.maxVal < this.minVal)
				{
					this.minVal = this.maxVal;
				}
				if (this.val > this.maxVal)
				{
					this.val = this.maxVal;
				}
				this.SetPos();
			}
		}

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        [Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(0)]
		[Description("Setzt den minimalen Wert der Trackbar.")]
		public int Minimum
		{
			get
			{
				return this.minVal;
			}
			set
			{
				this.minVal = value;
				if (this.minVal > this.maxVal)
				{
					this.maxVal = this.minVal;
				}
				if (this.val < this.minVal)
				{
					this.val = this.Minimum;
				}
				this.SetPos();
			}
		}

        /// <summary>
        /// Gets the selected value.
        /// </summary>
        /// <value>The selected value.</value>
        [Browsable(true)]
		[Category("Behavior")]
		[Description("Gibt den derzeitigen Wertbereich der Trackbar an.")]
		public string SelectedValue
		{
			get
			{
				string str = string.Concat(Conversions.ToString(this.val2), "-", Conversions.ToString(this.val));
				return str;
			}
		}

        /// <summary>
        /// Gets or sets the slider value1.
        /// </summary>
        /// <value>The slider value1.</value>
        [Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(50)]
		[Description("Setzt den derzeitigen Wert des ersten Sliders der Trackbar.")]
		public int SliderValue1
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
				if (this.val < this.minVal)
				{
					this.val = this.minVal;
				}
				if (this.val > this.maxVal)
				{
					this.val = this.maxVal;
				}
				this.SetPos();
			}
		}

        /// <summary>
        /// Gets or sets the slider value2.
        /// </summary>
        /// <value>The slider value2.</value>
        [Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(0)]
		[Description("Setzt den derzeitigen Wert des zweiten Sliders der Trackbar.")]
		public int SliderValue2
		{
			get
			{
				return this.val2;
			}
			set
			{
				this.val2 = value;
				if (this.val2 < this.minVal)
				{
					this.val2 = this.minVal;
				}
				if (this.val2 > this.maxVal)
				{
					this.val2 = this.maxVal;
				}
				this.SetPos();
			}
		}

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        [Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(0)]
		[Description("Setzt den Style der Trackbar.")]
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
					if (value == Design.Style.Light)
					{
						this.ColorScheme._RightColor = Color.FromArgb(229, 229, 229);
						this.ColorScheme._LeftColor = Color.FromArgb(229, 229, 229);
						this.ColorScheme._MiddleColor = Color.FromArgb(0, 164, 240);
						this.ColorScheme._SwitchColor = Color.FromArgb(101, 101, 101);
						this.UseSwitchBorders = true;
					}
					else if (value != Design.Style.Dark)
					{
						value = Design.Style.Custom;
					}
					else
					{
						this.ColorScheme._RightColor = Color.FromArgb(98, 98, 98);
						this.ColorScheme._LeftColor = Color.FromArgb(98, 98, 98);
						this.ColorScheme._MiddleColor = Color.FromArgb(0, 164, 240);
						this.ColorScheme._SwitchColor = Color.FromArgb(51, 51, 51);
						this.UseSwitchBorders = false;
					}
					this._Style = value;
					this.Invalidate();
				}
			}
		}

        /// <summary>
        /// Gets or sets the width of the switch.
        /// </summary>
        /// <value>The width of the switch.</value>
        [Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(6)]
		[Description("Setzt die Dicke des Schalters. (Kann nur bei \"UseFixedSize = False\" angewendet werden.)")]
		public int SwitchWidth
		{
			get
			{
				return this.sliderWidth;
			}
			set
			{
				if (value != this.sliderWidth)
				{
					if (!this.UseFixedSize)
					{
						this.sliderWidth = value;
						this.Invalidate();
					}
				}
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether [use fixed size].
        /// </summary>
        /// <value><c>true</c> if [use fixed size]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Gibt an, ob der Schalter und die Linie eine fixe Größe haben sollen.")]
		public bool UseFixedSize
		{
			get
			{
				return this._UseFixedSize;
			}
			set
			{
				if (value != this._UseFixedSize)
				{
					if (value)
					{
						this.Size = new System.Drawing.Size(200, 20);
						this.sliderWidth = 6;
						this.UseSwitchBorders = true;
					}
					else if (!value)
					{
						this.UseSwitchBorders = false;
					}
					this._UseFixedSize = value;
				}
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether [use switch borders].
        /// </summary>
        /// <value><c>true</c> if [use switch borders]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Gibt an, ob der Schalter umrandet werden soll.")]
		public bool UseSwitchBorders
		{
			get
			{
				return this._UseSwitchBorders;
			}
			set
			{
				if (value != this._UseSwitchBorders)
				{
					if (this.UseFixedSize)
					{
						this._UseSwitchBorders = value;
						this.Invalidate();
					}
				}
			}
		}

        /// <summary>
        /// Initializes static members of the <see cref="MetroTrackbarRegion"/> class.
        /// </summary>
        [DebuggerNonUserCode]
		static MetroTrackbarRegion()
		{
			MetroTrackbarRegion.__ENCList = new List<WeakReference>();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroTrackbarRegion"/> class.
        /// </summary>
        public MetroTrackbarRegion()
		{
			MetroTrackbarRegion metroTrackbarRegion = this;
			base.MouseDown += new MouseEventHandler(metroTrackbarRegion.MetroTrackBarRegion_MouseDown);
			MetroTrackbarRegion metroTrackbarRegion1 = this;
			base.MouseMove += new MouseEventHandler(metroTrackbarRegion1.MetroTrackBarRegion_MouseMove);
			MetroTrackbarRegion metroTrackbarRegion2 = this;
			base.MouseUp += new MouseEventHandler(metroTrackbarRegion2.MetroTrackBarRegion_MouseUp);
			MetroTrackbarRegion metroTrackbarRegion3 = this;
			base.Paint += new PaintEventHandler(metroTrackbarRegion3.MetroTrackBarRegion_Paint);
			MetroTrackbarRegion.__ENCAddToList(this);
			this.BarSize = 6;
			this.sliderWidth = 6;
			this._UseSwitchBorders = true;
			this._UseFixedSize = true;
			this._Style = Design.Style.Light;
			this.ColorScheme = new MetroTrackbarRegion.MainColorScheme();
			this.val = 50;
			this.minVal = 0;
			this.maxVal = 100;
			this.scrolling1 = false;
			this.scrolling2 = false;
			this.slider1Pos = 0;
			this.slider2Pos = 0;
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.UpdateStyles();
			this.Size = new System.Drawing.Size(200, 20);
			this._Style = Design.Style.Dark;
			this.Invalidate();
			this._Style = Design.Style.Light;
			this.SetPos();
		}

        /// <summary>
        /// Encs the add to list.
        /// </summary>
        /// <param name="value">The value.</param>
        [DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = MetroTrackbarRegion.__ENCList;
			Monitor.Enter(_ENCList);
			try
			{
				if (MetroTrackbarRegion.__ENCList.Count == MetroTrackbarRegion.__ENCList.Capacity)
				{
					int item = 0;
					int count = checked(MetroTrackbarRegion.__ENCList.Count - 1);
					for (int i = 0; i <= count; i = checked(i + 1))
					{
						if (MetroTrackbarRegion.__ENCList[i].IsAlive)
						{
							if (i != item)
							{
								MetroTrackbarRegion.__ENCList[item] = MetroTrackbarRegion.__ENCList[i];
							}
							item = checked(item + 1);
						}
					}
					MetroTrackbarRegion.__ENCList.RemoveRange(item, checked(MetroTrackbarRegion.__ENCList.Count - item));
					MetroTrackbarRegion.__ENCList.Capacity = MetroTrackbarRegion.__ENCList.Count;
				}
				MetroTrackbarRegion.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
			}
			finally
			{
				Monitor.Exit(_ENCList);
			}
		}

        /// <summary>
        /// Handles the MouseDown event of the MetroTrackBarRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MetroTrackBarRegion_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag;
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (e.X >= this.slider1Pos && e.X < checked(this.slider1Pos + this.sliderWidth))
				{
					if ((this.slider1Pos == checked(this.Width - this.sliderWidth) ? this.slider1Pos <= checked(this.slider2Pos + this.sliderWidth) : false))
					{
						goto Label1;
					}
					flag = true;
					
				}
			Label1:
				flag = false;
				if (flag)
				{
					this.scrolling1 = true;
				}
				else if ((e.X < this.slider2Pos || e.X >= checked(this.slider2Pos + this.sliderWidth) ? false : true))
				{
					this.scrolling2 = true;
				}
			}
		}

        /// <summary>
        /// Handles the MouseMove event of the MetroTrackBarRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MetroTrackBarRegion_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.scrolling1)
			{
				this.slider1Pos = checked(e.X - this.sliderWidth / 2);
				if (this.slider1Pos < this.slider2Pos)
				{
					this.slider1Pos = this.slider2Pos;
				}
				if (this.slider1Pos > checked(this.Width - this.sliderWidth))
				{
					this.slider1Pos = checked(this.Width - this.sliderWidth);
				}
				this.val = checked(Convert.ToInt32((double)this.slider1Pos / (double)(checked(this.Width - this.sliderWidth)) * (double)(checked(this.maxVal - this.minVal))) + this.minVal);
				this.Invalidate();
				MetroTrackbarRegion.ScrollEventHandler scrollEventHandler = MetroTrackbarRegion.Scroll;
				if (scrollEventHandler != null)
				{
					scrollEventHandler(this, new MetroTrackbarRegionEventArgs(this.SliderValue1, this.SliderValue2));
				}
			}
			if (this.scrolling2)
			{
				this.slider2Pos = checked(e.X - this.sliderWidth / 2);
				if (this.slider2Pos < 0)
				{
					this.slider2Pos = 0;
				}
				if (this.slider2Pos > this.slider1Pos)
				{
					this.slider2Pos = this.slider1Pos;
				}
				this.val2 = checked(Convert.ToInt32((double)this.slider2Pos / (double)(checked(this.Width - this.sliderWidth)) * (double)(checked(this.maxVal - this.minVal))) + this.minVal);
				this.Invalidate();
				MetroTrackbarRegion.ScrollTwoEventHandler scrollTwoEventHandler = MetroTrackbarRegion.ScrollTwo;
				if (scrollTwoEventHandler != null)
				{
					scrollTwoEventHandler(this, new MetroTrackbarRegionEventArgs(this.SliderValue1, this.SliderValue2));
				}
			}
		}

        /// <summary>
        /// Handles the MouseUp event of the MetroTrackBarRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void MetroTrackBarRegion_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.scrolling1 = false;
				this.scrolling2 = false;
			}
		}

        /// <summary>
        /// Handles the Paint event of the MetroTrackBarRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void MetroTrackBarRegion_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rectangle;
			int num = checked((int)Math.Round((double)this.Height / 4));
			int num1 = checked((int)Math.Round((double)this.Height / 2));
			Pen pen = new Pen(this.ColorScheme.LeftColor);
			Pen pen1 = new Pen(this.ColorScheme.RightColor);
			Pen pen2 = new Pen(this.ColorScheme.MiddleColor);
			Brush solidBrush = new SolidBrush(this.ColorScheme.LeftColor);
			Brush brush = new SolidBrush(this.ColorScheme.RightColor);
			Brush solidBrush1 = new SolidBrush(this.ColorScheme.SwitchColor);
			Brush brush1 = new SolidBrush(this.ColorScheme.MiddleColor);
			if (this.UseFixedSize)
			{
				Graphics graphics = e.Graphics;
				rectangle = new Rectangle(0, checked((int)Math.Round((double)(checked(this.Height - this.BarSize)) / 2)), this.slider1Pos, this.BarSize);
				graphics.DrawRectangle(pen, rectangle);
				Graphics graphic = e.Graphics;
				rectangle = new Rectangle(0, checked((int)Math.Round((double)(checked(this.Height - this.BarSize)) / 2)), this.slider1Pos, this.BarSize);
				graphic.FillRectangle(solidBrush, rectangle);
				Graphics graphics1 = e.Graphics;
				rectangle = new Rectangle(checked(this.slider2Pos + this.sliderWidth), checked((int)Math.Round((double)(checked(this.Height - this.BarSize)) / 2)), this.slider1Pos, this.BarSize);
				graphics1.DrawRectangle(pen2, rectangle);
				Graphics graphic1 = e.Graphics;
				rectangle = new Rectangle(checked(this.slider2Pos + this.sliderWidth), checked((int)Math.Round((double)(checked(this.Height - this.BarSize)) / 2)), this.slider1Pos, this.BarSize);
				graphic1.FillRectangle(brush1, rectangle);
				Graphics graphics2 = e.Graphics;
				rectangle = new Rectangle(checked(this.slider1Pos + this.sliderWidth), checked((int)Math.Round((double)(checked(this.Height - this.BarSize)) / 2)), this.Width, this.BarSize);
				graphics2.DrawRectangle(pen1, rectangle);
				Graphics graphic2 = e.Graphics;
				rectangle = new Rectangle(checked(this.slider1Pos + this.sliderWidth), checked((int)Math.Round((double)(checked(this.Height - this.BarSize)) / 2)), this.Width, this.BarSize);
				graphic2.FillRectangle(brush, rectangle);
				Graphics graphics3 = e.Graphics;
				rectangle = new Rectangle(this.slider1Pos, checked((int)Math.Round((double)(checked(this.Height - 16)) / 2)), this.sliderWidth, 16);
				graphics3.FillRectangle(solidBrush1, rectangle);
				Graphics graphic3 = e.Graphics;
				rectangle = new Rectangle(this.slider2Pos, checked((int)Math.Round((double)(checked(this.Height - 16)) / 2)), this.sliderWidth, 16);
				graphic3.FillRectangle(solidBrush1, rectangle);
				if (this.UseSwitchBorders)
				{
					Graphics graphics4 = e.Graphics;
					Pen white = Pens.White;
					rectangle = new Rectangle(checked(this.slider2Pos - 1), checked((int)Math.Round((double)(checked(this.Height - 16)) / 2)), checked(this.sliderWidth + 1), 16);
					graphics4.DrawRectangle(white, rectangle);
					Graphics graphic4 = e.Graphics;
					Pen white1 = Pens.White;
					rectangle = new Rectangle(checked(this.slider1Pos - 1), checked((int)Math.Round((double)(checked(this.Height - 16)) / 2)), checked(this.sliderWidth + 1), 16);
					graphic4.DrawRectangle(white1, rectangle);
				}
			}
			else if (!this.UseFixedSize)
			{
				Graphics graphics5 = e.Graphics;
				rectangle = new Rectangle(0, num, this.slider1Pos, num1);
				graphics5.DrawRectangle(pen, rectangle);
				Graphics graphic5 = e.Graphics;
				rectangle = new Rectangle(0, num, this.slider1Pos, num1);
				graphic5.FillRectangle(solidBrush, rectangle);
				Graphics graphics6 = e.Graphics;
				rectangle = new Rectangle(checked(this.slider2Pos + this.sliderWidth), num, this.slider1Pos, num1);
				graphics6.DrawRectangle(pen2, rectangle);
				Graphics graphic6 = e.Graphics;
				rectangle = new Rectangle(checked(this.slider2Pos + this.sliderWidth), num, this.slider1Pos, num1);
				graphic6.FillRectangle(brush1, rectangle);
				Graphics graphics7 = e.Graphics;
				rectangle = new Rectangle(checked(this.slider1Pos + this.sliderWidth), num, this.Width, num1);
				graphics7.DrawRectangle(pen1, rectangle);
				Graphics graphic7 = e.Graphics;
				rectangle = new Rectangle(checked(this.slider1Pos + this.sliderWidth), num, this.Width, num1);
				graphic7.FillRectangle(brush, rectangle);
				Graphics graphics8 = e.Graphics;
				rectangle = new Rectangle(this.slider1Pos, 0, this.sliderWidth, this.Height);
				graphics8.FillRectangle(solidBrush1, rectangle);
				Graphics graphic8 = e.Graphics;
				rectangle = new Rectangle(this.slider2Pos, 0, this.sliderWidth, this.Height);
				graphic8.FillRectangle(solidBrush1, rectangle);
				if (this.UseSwitchBorders)
				{
					Graphics graphics9 = e.Graphics;
					Pen white2 = Pens.White;
					rectangle = new Rectangle(checked(this.slider2Pos - 1), checked((int)Math.Round((double)(checked(this.Height - 16)) / 2)), checked(this.sliderWidth + 1), 16);
					graphics9.DrawRectangle(white2, rectangle);
					Graphics graphic9 = e.Graphics;
					Pen white3 = Pens.White;
					rectangle = new Rectangle(checked(this.slider1Pos - 1), checked((int)Math.Round((double)(checked(this.Height - 16)) / 2)), checked(this.sliderWidth + 1), 16);
					graphic9.DrawRectangle(white3, rectangle);
				}
			}
		}

        /// <summary>
        /// Sets the position.
        /// </summary>
        private void SetPos()
		{
			this.slider1Pos = Convert.ToInt32((double)(checked(this.val - this.minVal)) / (double)(checked(this.maxVal - this.minVal)) * (double)(checked(this.Width - this.sliderWidth)));
			this.slider2Pos = Convert.ToInt32((double)(checked(this.val2 - this.minVal)) / (double)(checked(this.maxVal - this.minVal)) * (double)(checked(this.Width - this.sliderWidth)));
			this.Invalidate();
		}

        /// <summary>
        /// Occurs when [scroll].
        /// </summary>
        public static event MetroTrackbarRegion.ScrollEventHandler Scroll;

        /// <summary>
        /// Occurs when [scroll two].
        /// </summary>
        public static event MetroTrackbarRegion.ScrollTwoEventHandler ScrollTwo;

        /// <summary>
        /// Class MainColorScheme.
        /// </summary>
        public class MainColorScheme
		{
            /// <summary>
            /// The left color
            /// </summary>
            public Color _LeftColor;

            /// <summary>
            /// The middle color
            /// </summary>
            public Color _MiddleColor;

            /// <summary>
            /// The switch color
            /// </summary>
            public Color _SwitchColor;

            /// <summary>
            /// The right color
            /// </summary>
            public Color _RightColor;

            /// <summary>
            /// Gets or sets the color of the left.
            /// </summary>
            /// <value>The color of the left.</value>
            [Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Farbe der Trackbar links von den Reglern.")]
			public Color LeftColor
			{
				get
				{
					return this._LeftColor;
				}
				set
				{
					if (value != this._LeftColor)
					{
						this._LeftColor = value;
					}
				}
			}

            /// <summary>
            /// Gets or sets the color of the middle.
            /// </summary>
            /// <value>The color of the middle.</value>
            [Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Farbe der Trackbar zwischen den Reglern.")]
			public Color MiddleColor
			{
				get
				{
					return this._MiddleColor;
				}
				set
				{
					if (value != this._MiddleColor)
					{
						this._MiddleColor = value;
					}
				}
			}

            /// <summary>
            /// Gets or sets the color of the right.
            /// </summary>
            /// <value>The color of the right.</value>
            [Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Farbe der Trackbar rechts von den Reglern.")]
			public Color RightColor
			{
				get
				{
					return this._RightColor;
				}
				set
				{
					if (value != this._RightColor)
					{
						this._RightColor = value;
					}
				}
			}

            /// <summary>
            /// Gets or sets the color of the switch.
            /// </summary>
            /// <value>The color of the switch.</value>
            [Browsable(true)]
			[Category("Appearance")]
			[Description("Setzt die Farbe der Regler.")]
			public Color SwitchColor
			{
				get
				{
					return this._SwitchColor;
				}
				set
				{
					if (value != this._SwitchColor)
					{
						this._SwitchColor = value;
					}
				}
			}

            /// <summary>
            /// Initializes a new instance of the <see cref="MainColorScheme"/> class.
            /// </summary>
            public MainColorScheme()
			{
				this._LeftColor = Color.FromArgb(229, 229, 229);
				this._MiddleColor = Color.FromArgb(0, 164, 240);
				this._SwitchColor = Color.FromArgb(101, 101, 101);
				this._RightColor = Color.FromArgb(229, 229, 229);
			}
		}

        /// <summary>
        /// Delegate ScrollEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MetroTrackbarRegionEventArgs"/> instance containing the event data.</param>
        public delegate void ScrollEventHandler(object sender, MetroTrackbarRegionEventArgs e);

        /// <summary>
        /// Delegate ScrollTwoEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MetroTrackbarRegionEventArgs"/> instance containing the event data.</param>
        public delegate void ScrollTwoEventHandler(object sender, MetroTrackbarRegionEventArgs e);
	}

}