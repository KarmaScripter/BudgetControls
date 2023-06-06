// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="BudgetExpander.cs" company="Terry D. Eppler">
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
//   BudgetExpander.cs
// </summary>
// ******************************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace BudgetExecution
{
	/// <summary>
	/// A class collection for rendering an expander.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Panel" />
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(BudgetExpander), "BudgetExpander.bmp")]
	public class BudgetExpander : Panel
	{
		#region Private Fields

		/// <summary>
		/// The current state
		/// </summary>
		private BudgetExpander.MouseState CurrentState;

		/// <summary>
		/// The expanded size
		/// </summary>
		private System.Drawing.Size _ExpandedSize;

		/// <summary>
		/// The none size
		/// </summary>
		private System.Drawing.Size _NoneSize;

		/// <summary>
		/// The state
		/// </summary>
		private BudgetExpander.eState _State;
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating whether to draw interect area.
		/// </summary>
		/// <value><c>true</c> if draw interect area; otherwise, <c>false</c>.</value>
		[Browsable(false)]
		[Category("Developing")]
		[ComVisible(false)]
		[Description("Sets a value indicating whether to draw interect area")]
		private bool DrawInterectArea
		{
			[DebuggerNonUserCode]
			get;
			[DebuggerNonUserCode]
			set;
		}

		/// <summary>
		/// Gets or sets the size to be expanded.
		/// </summary>
		/// <value>The size of the expanded.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Sets the size to be expanded.")]
		public System.Drawing.Size ExpandedSize
		{
			get
			{
				return this._ExpandedSize;
			}
			set
			{
				this._ExpandedSize = value;
			}
		}

		/// <summary>
		/// Gets or sets the size when not expanded.
		/// </summary>
		/// <value>The size when not expanded.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Sets the size when not expanded.")]
		public System.Drawing.Size NoneSize
		{
			get
			{
				return this._NoneSize;
			}
			set
			{
				this._NoneSize = value;
			}
		}

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>The state.</value>
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(0)]
		[Description("Sets the state.")]
		public BudgetExpander.eState State
		{
			get
			{
				return this._State;
			}
			set
			{
				if (value != BudgetExpander.eState.Expanded)
				{
					this.Size = this._NoneSize;
					this._State = BudgetExpander.eState.None;
				}
				else
				{
					this.Size = this._ExpandedSize;
					this._State = BudgetExpander.eState.Expanded;
				}
				this._State = value;
			}
		}
		#endregion


		/// <summary>
		/// Initializes a new instance of the <see cref="BudgetExpander" /> class.
		/// </summary>
		public BudgetExpander()
		{
			this.CurrentState = BudgetExpander.MouseState.None;
			this._State = BudgetExpander.eState.None;
			this.DrawInterectArea = false;
			this.Size = new System.Drawing.Size(150, 15);
			this._NoneSize = new System.Drawing.Size(150, 15);
			this._ExpandedSize = new System.Drawing.Size(300, 150);
			this.Font = new System.Drawing.Font("Segoe UI", 9f);
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.UpdateStyles();
		}



		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			Rectangle rectangle = new Rectangle(2, 2, Width, Height);
			if (!rectangle.Contains(e.Location))
			{
				this.CurrentState = BudgetExpander.MouseState.None;
			}
			else
			{
				this.CurrentState = BudgetExpander.MouseState.Over;
			}
			this.Invalidate();
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			this.CurrentState = BudgetExpander.MouseState.None;
			this.Invalidate();
			base.OnMouseLeave(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Rectangle rectangle = new Rectangle(checked(this.Width - 14), 2, 10, 10);
			if (!rectangle.Contains(e.Location))
			{
				this.CurrentState = BudgetExpander.MouseState.None;
			}
			else
			{
				this.CurrentState = BudgetExpander.MouseState.Over;
			}
			this.Invalidate();
			base.OnMouseMove(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Rectangle rectangle = new Rectangle(checked(this.Width - 14), 2, 10, 10);
			if (!rectangle.Contains(e.Location))
			{
				this.CurrentState = BudgetExpander.MouseState.None;
			}
			else
			{
				if (this._State != BudgetExpander.eState.None)
				{
					this.Size = this._NoneSize;
					this._State = BudgetExpander.eState.None;
				}
				else
				{
					this.Size = this._ExpandedSize;
					this._State = BudgetExpander.eState.Expanded;
				}
				this.CurrentState = BudgetExpander.MouseState.Over;
			}
			this.Invalidate();
			base.OnMouseUp(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			Point point;
			Point point1;
			Point point2;
			Point[] pointArray;
			Rectangle rectangle = new Rectangle(checked(this.Width - 14), 2, 10, 10);
			Graphics graphics = e.Graphics;
			Rectangle rectangle1 = new Rectangle(0, 5, checked(this.Width - 16), 5);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(this.BackColor);
			if (this._State != BudgetExpander.eState.None)
			{
				pointArray = new Point[3];
				point2 = new Point(checked(this.Width - 12), 8);
				pointArray[0] = point2;
				point1 = new Point(checked(this.Width - 6), 8);
				pointArray[1] = point1;
				point = new Point(checked(this.Width - 9), 5);
				pointArray[2] = point;
				Point[] pointArray1 = pointArray;
				if (this.CurrentState == BudgetExpander.MouseState.None)
				{
					Brush solidBrush = new SolidBrush(Color.FromArgb(98, 98, 98));
					graphics.FillPolygon(solidBrush, pointArray1);
					graphics.DrawPolygon(new Pen(new SolidBrush(Color.FromArgb(98, 98, 98))), pointArray1);
				}
				else if (this.CurrentState == BudgetExpander.MouseState.Over)
				{
					graphics.FillPolygon(new SolidBrush(this.BackColor), pointArray1);
					graphics.DrawPolygon(new Pen(new SolidBrush(Color.FromArgb(0, 164, 240))), pointArray1);
				}
			}
			else
			{
				pointArray = new Point[3];
				point = new Point(checked(this.Width - 12), 5);
				pointArray[0] = point;
				point1 = new Point(checked(this.Width - 6), 5);
				pointArray[1] = point1;
				point2 = new Point(checked(this.Width - 9), 8);
				pointArray[2] = point2;
				Point[] pointArray2 = pointArray;
				if (this.CurrentState == BudgetExpander.MouseState.None)
				{
					Brush brush = new SolidBrush(Color.FromArgb(98, 98, 98));
					graphics.FillPolygon(brush, pointArray2);
					graphics.DrawPolygon(new Pen(new SolidBrush(Color.FromArgb(98, 98, 98))), pointArray2);
				}
				else if (this.CurrentState == BudgetExpander.MouseState.Over)
				{
					graphics.FillPolygon(new SolidBrush(this.BackColor), pointArray2);
					graphics.DrawPolygon(new Pen(new SolidBrush(Color.FromArgb(0, 164, 240))), pointArray2);
				}
			}
			graphics.DrawImageUnscaledAndClipped(Properties.Resources.PointRow, rectangle1);
			if (this.DrawInterectArea)
			{
				graphics.DrawRectangle(Pens.Red, rectangle);
			}
			base.OnPaint(e);
		}

		/// <summary>
		/// Enum eState
		/// </summary>
		public enum eState
		{
			/// <summary>
			/// The none
			/// </summary>
			None,
			/// <summary>
			/// The expanded
			/// </summary>
			Expanded
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