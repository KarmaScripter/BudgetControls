// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="BudgetTask.cs" company="Terry D. Eppler">
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
//   BudgetTask.cs
// </summary>
// ******************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BudgetExecution
{
	/// <summary>
	/// A class collection for rendering a metro-style task.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Control" />
	[Description("A class for rendering a task.")]
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(BudgetTask), "BudgetTask.bmp")]
	[Designer(typeof(BudgetTaskDesigner))]
	[DefaultEvent("PointClicked")]
	public class BudgetTask : Control
	{
		/// <summary>
		/// Enum representing the Task Shape
		/// </summary>
		public enum TaskShape
		{
			/// <summary>
			/// The circle
			/// </summary>
			Circle,
			/// <summary>
			/// The rectangle
			/// </summary>
			Rectangle,
			/// <summary>
			/// The pie
			/// </summary>
			Pie
		}

		#region Private Fields
		/// <summary>
		/// The task shape
		/// </summary>
		private TaskShape taskShape = TaskShape.Circle;

		/// <summary>
		/// The pie angles
		/// </summary>
		private PieAngles pieAngles = new PieAngles();

		/// <summary>
		/// The point collection
		/// </summary>
		private BudgetTaskPointCollection _PointCollection = new BudgetTaskPointCollection();

		/// <summary>
		/// The finished color
		/// </summary>
		private Color _FinishedColor;

		/// <summary>
		/// The line color
		/// </summary>
		private Color _LineColor;

		/// <summary>
		/// The hover color
		/// </summary>
		private Color _HoverColor;

		/// <summary>
		/// The start point width
		/// </summary>
		private int _StartPointWidth;

		/// <summary>
		/// The style
		/// </summary>
		private Design.Style _Style;

		/// <summary>
		/// The point position
		/// </summary>
		private List<Rectangle> _PointPosition;

		/// <summary>
		/// The text rectangles
		/// </summary>
		private List<Rectangle> _TextRectangles;

		/// <summary>
		/// The distance
		/// </summary>
		private int distance;

		/// <summary>
		/// The hot point
		/// </summary>
		private Rectangle _HotPoint;

		/// <summary>
		/// The hot index
		/// </summary>
		private int _HotIndex;

		/// <summary>
		/// The automatic style
		/// </summary>
		private bool _AutoStyle;
		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the pie angle.
		/// </summary>
		/// <value>The pie angle.</value>
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public PieAngles PieAngle
		{
			get { return pieAngles; }
			set
			{
				pieAngles = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to enable automatic style.
		/// </summary>
		/// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Sets a value indicating whether to enable automatic style.")]
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
		/// Gets or sets the color of the finished.
		/// </summary>
		/// <value>The color of the finished.</value>
		[Category("Appearance")]
		[Description("Sets the color of the finished.")]
		public Color FinishedColor
		{
			get
			{
				return this._FinishedColor;
			}
			set
			{
				if (this._FinishedColor != value)
				{
					this._FinishedColor = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the color of the control when hovered.
		/// </summary>
		/// <value>The color of the hover.</value>
		[Category("Appearance")]
		[Description("Sets the color of the control when hovered.")]
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
		/// Gets or sets the color of the line.
		/// </summary>
		/// <value>The color of the line.</value>
		[Category("Appearance")]
		[Description("Sets the color of the line.")]
		public Color LineColor
		{
			get
			{
				return this._LineColor;
			}
			set
			{
				if (this._LineColor != value)
				{
					this._LineColor = value;
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the points.
		/// </summary>
		/// <value>The points.</value>
		[Browsable(true)]
		[Category("Data")]
		[Description("Sets the points.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BudgetTaskPointCollection Points
		{
			get
			{
				return this._PointCollection;
			}
			set
			{
				_PointCollection = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the width of the start point.
		/// </summary>
		/// <value>The start width of the point.</value>
		[Category("Appearance")]
		[DefaultValue(10)]
		[Description("Sets the width of the start point.")]
		public int StartPointWidth
		{
			get
			{
				return this._StartPointWidth;
			}
			set
			{
				if (this._StartPointWidth != value)
				{
					this._StartPointWidth = value;
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
					switch (value)
					{
						case Design.Style.Light:
							{
								this._FinishedColor = Design.BudgetColors.TaskColor;
								this._LineColor = Design.BudgetColors.TaskColor;
								this._HoverColor = Design.BudgetColors.AccentLightBlue;
								this.ForeColor = Design.BudgetColors.LightFont;
								break;
							}
						case Design.Style.Dark:
							{
								this._FinishedColor = Design.BudgetColors.DarkTaskColor;
								this._LineColor = Design.BudgetColors.DarkTaskColor;
								this._HoverColor = Design.BudgetColors.AccentLightBlue;
								this.ForeColor = Design.BudgetColors.DarkFont;
								break;
							}
						default:
							{
								this._AutoStyle = false;
								break;
							}
					}
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets or sets the shape.
		/// </summary>
		/// <value>The shape.</value>
		public TaskShape Shape
		{
			get { return taskShape; }
			set
			{
				taskShape = value;
				Invalidate();
			}
		}


		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="BudgetTask" /> class.
		/// </summary>
		public BudgetTask()
		{
			//this._PointCollection = new BudgetTaskPointCollection();
			this._FinishedColor = Design.BudgetColors.TaskColor;
			this._LineColor = Design.BudgetColors.TaskColor;
			this._HoverColor = Design.BudgetColors.AccentLightBlue;
			this._StartPointWidth = 10;
			this._Style = Design.Style.Light;
			this._PointPosition = new List<Rectangle>();
			this._TextRectangles = new List<Rectangle>();
			this._HotIndex = -1;
			this._AutoStyle = true;
			this.Font = new System.Drawing.Font("Segoe UI", 9f);
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.UpdateStyles();

			BudgetTask metroTask = this;
			this._PointCollection.ItemAdded += new EventHandler<BudgetTaskPointCollectionEventArgs>(metroTask.Point_Added);
			BudgetTask metroTask1 = this;
			this._PointCollection.ItemRemoving += new EventHandler<BudgetTaskPointCollectionEventArgs>(metroTask1.Point_Removed);
			Refresh();


		}


		/// <summary>
		/// Handles the PropertyChanged event of the Item control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.Invalidate();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnBackColorChanged(EventArgs e)
		{
			if (this.FindForm() is BudgetForm)
			{
				if (this._AutoStyle)
				{
					this.Style = ((BudgetForm)this.FindForm()).Style;
					this.Invalidate();
				}
			}
			base.OnBackColorChanged(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (this._HotIndex != -1 && this._PointCollection[this._HotIndex].Enabled)
			{
				BudgetTask.PointClickedEventHandler pointClickedEventHandler = this.PointClicked;
				if (pointClickedEventHandler != null)
				{
					pointClickedEventHandler(this, new BudgetTaskPointCollectionEventArgs(this._PointCollection[this._HotIndex]));
				}
			}
			base.OnMouseClick(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			this._HotPoint = new Rectangle();
			this._HotIndex = -1;
			this.Invalidate();
			base.OnMouseLeave(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this._PointPosition.Count > 0)
			{
				int count = checked(this._PointPosition.Count - 1);
				for (int i = 0; i <= count; i = checked(i + 1))
				{
					if ((this._PointPosition[i].Contains(e.Location) || this._TextRectangles[i].Contains(e.Location) ? true : false) && this._HotPoint != this._PointPosition[i])
					{
						this._HotPoint = this._PointPosition[i];
						this._HotIndex = i;
						this.Invalidate();
					}
				}
			}
			base.OnMouseMove(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			IEnumerator<BudgetTaskPoint> enumerator = this._PointCollection.GetEnumerator();
			Rectangle rectangle;
			Rectangle rectangle1 = new Rectangle();
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			int circleWidth = this._StartPointWidth;
			Point point = new Point(checked(circleWidth + 6), checked(circleWidth + 6));
			Point point1 = new Point(checked(circleWidth + 6), checked(this.Height - (checked(circleWidth + 6))));
			this._PointPosition.Clear();
			this._TextRectangles.Clear();

			if (this._PointCollection.Count > 0)
			{
				
					
					while (enumerator.MoveNext())
					{
						BudgetTaskPoint current = enumerator.Current;
						if (current.CircleWidth > circleWidth)
						{
							circleWidth = current.CircleWidth;
						}
					}
				
				point = new Point(checked(circleWidth + 6), checked(circleWidth + 6));
				point1 = new Point(checked(circleWidth + 6), checked(this.Height - (checked(circleWidth + 6))));
				this.distance = checked((int)Math.Round(Design.Drawing.MeasurePointDistance(point, point1)));
				this.distance = checked((int)Math.Round((double)this.distance / (double)this._PointCollection.Count));
				int count = checked(this._PointCollection.Count - 1);
				for (int i = 0; i <= count; i = checked(i + 1))
				{
					List<Rectangle> rectangles = this._PointPosition;
					rectangle = new Rectangle(checked(checked(circleWidth + 6) - checked((int)Math.Round((double)this._PointCollection[i].CircleWidth / 2))), checked(checked(checked(this.Height - (checked(circleWidth + 6))) - checked((int)Math.Round((double)this._PointCollection[i].CircleWidth / 2))) - checked(this.distance * (checked(i + 1)))), this._PointCollection[i].CircleWidth, this._PointCollection[i].CircleWidth);
					rectangles.Add(rectangle);
				}
			}
			Pen pen = new Pen(this._LineColor);
			SolidBrush solidBrush = new SolidBrush(this._LineColor);
			Pen pen1 = new Pen(this._HoverColor);


			graphics.DrawLine(pen, point, point1);

			
			Point point2 = new Point(checked(checked(circleWidth + 6) - checked((int)Math.Round((double)this._StartPointWidth / 2))), checked(checked(this.Height - (checked(circleWidth + 6))) - checked((int)Math.Round((double)this._StartPointWidth / 2))));
			System.Drawing.Size size = new System.Drawing.Size(this._StartPointWidth, this._StartPointWidth);
			rectangle = new Rectangle(point2, size);

			switch (taskShape)
			{
				case TaskShape.Circle:
					graphics.FillEllipse(solidBrush, rectangle);
					break;
				case TaskShape.Rectangle:
					graphics.FillRectangle(solidBrush, rectangle);
					break;
				case TaskShape.Pie:
					graphics.FillPie(solidBrush, rectangle, PieAngle.StartAngle, PieAngle.SweepAngle);
					break;

			}
			

			if (this._PointPosition.Count > 0)
			{
				int num = checked(this._PointCollection.Count - 1);
				for (int j = 0; j <= num; j = checked(j + 1))
				{
					SolidBrush solidBrush1 = new SolidBrush((this._PointCollection[j].Finished
						? this._FinishedColor
						: this._PointCollection[j].CirceColor));

					
						switch (taskShape)
						{
							case TaskShape.Circle:
								graphics.FillEllipse(solidBrush1, this._PointPosition[j]);

								break;
							case TaskShape.Rectangle:
								graphics.FillRectangle(solidBrush1, this._PointPosition[j]);

								break;
							case TaskShape.Pie:
								graphics.FillPie(solidBrush1, this._PointPosition[j], PieAngle.StartAngle, PieAngle.SweepAngle);

								break;
							  
						}
						
						if (this._HotPoint != rectangle1 && j == this._HotIndex && this._PointCollection[this._HotIndex].Enabled)
						{
							solidBrush1.Color = this._HoverColor;
						}
						SizeF sizeF = graphics.MeasureString(this._PointCollection[j].Text, this.Font);
						int num1 = checked(checked(circleWidth * 2) + 6);
						rectangle = this._PointPosition[j];
						Rectangle rectangle2 = new Rectangle(num1, checked(checked(rectangle.Y + checked((int)Math.Round((double)this._PointCollection[j].CircleWidth / 2))) - checked((int)Math.Round((double)((float)(sizeF.Height / 2f))))), checked((int)Math.Round((double)((float)(sizeF.Width + 2f)))), checked(this.distance - checked((int)Math.Round((double)((float)(sizeF.Height + 2f))))));
						this._TextRectangles.Add(rectangle2);
						graphics.DrawString(this._PointCollection[j].Text, this.Font, solidBrush1, rectangle2);
						int num2 = checked((int)Math.Round((double)this._PointCollection[j].CircleWidth / 4));
						if (this._PointCollection[j].Icon != null)
						{
							Image icon = this._PointCollection[j].Icon;
							rectangle = this._PointPosition[j];
							int x = checked(rectangle.X + num2);
							Rectangle item = this._PointPosition[j];
							Rectangle rectangle3 = new Rectangle(x, checked(item.Y + num2), checked(num2 * 2), checked(num2 * 2));
							graphics.DrawImage(icon, rectangle3);
						}
					
				}
				if (this._HotPoint != rectangle1)
				{
					
						if (this._PointCollection[this._HotIndex].Enabled)
						{
							switch (taskShape)
							{
								case TaskShape.Circle:
									graphics.DrawEllipse(pen1, this._HotPoint);
									break;
								case TaskShape.Rectangle:
									graphics.DrawRectangle(pen1, this._HotPoint);
									break;
								case TaskShape.Pie:
									graphics.DrawPie(pen1, this._HotPoint, PieAngle.StartAngle, PieAngle.SweepAngle);
									break;
								
							}
							
						}
					
				}
			}
			base.OnPaint(e);
		}

		/// <summary>
		/// Handles the Added event of the Point control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="BudgetTaskPointCollectionEventArgs"/> instance containing the event data.</param>
		private void Point_Added(object sender, BudgetTaskPointCollectionEventArgs e)
		{
			if (e.Item != null)
			{
				BudgetTask metroTask = this;
				e.Item.PropertyChanged += new PropertyChangedEventHandler(metroTask.Item_PropertyChanged);
			}
			BudgetTask.PointAddedEventHandler pointAddedEventHandler = this.PointAdded;
			if (pointAddedEventHandler != null)
			{
				pointAddedEventHandler(this, new BudgetTaskPointCollectionEventArgs(e.Item));
			}
		}

		/// <summary>
		/// Handles the Removed event of the Point control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="BudgetTaskPointCollectionEventArgs"/> instance containing the event data.</param>
		private void Point_Removed(object sender, BudgetTaskPointCollectionEventArgs e)
		{
			if (e.Item != null)
			{
				BudgetTask metroTask = this;
				e.Item.PropertyChanged -= new PropertyChangedEventHandler(metroTask.Item_PropertyChanged);
			}
		}

		/// <summary>
		/// Occurs when [point added].
		/// </summary>
		public event BudgetTask.PointAddedEventHandler PointAdded;

		/// <summary>
		/// Occurs when [point clicked].
		/// </summary>
		public event BudgetTask.PointClickedEventHandler PointClicked;

		/// <summary>
		/// Delegate PointAddedEventHandler
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="BudgetTaskPointCollectionEventArgs"/> instance containing the event data.</param>
		public delegate void PointAddedEventHandler(object sender, BudgetTaskPointCollectionEventArgs e);

		/// <summary>
		/// Delegate PointClickedEventHandler
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="BudgetTaskPointCollectionEventArgs"/> instance containing the event data.</param>
		public delegate void PointClickedEventHandler(object sender, BudgetTaskPointCollectionEventArgs e);
	}

	/// <summary>
	/// Class PieAngles.
	/// </summary>
	public class PieAngles
	{
		/// <summary>
		/// The start angle
		/// </summary>
		private float startAngle = 180f;
		/// <summary>
		/// The sweep angle
		/// </summary>
		private float sweepAngle = 360f;

		/// <summary>
		/// Gets or sets the start angle.
		/// </summary>
		/// <value>The start angle.</value>
		public float StartAngle
		{
			get { return startAngle; }
			set { startAngle = value; }
		}

		/// <summary>
		/// Gets or sets the sweep angle.
		/// </summary>
		/// <value>The sweep angle.</value>
		public float SweepAngle
		{
			get { return sweepAngle; }
			set { sweepAngle = value; }
		}
	}
}