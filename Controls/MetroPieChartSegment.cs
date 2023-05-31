// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 05-29-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        05-31-2023
// ******************************************************************************************
// <copyright file="MetroPieChartSegment.cs" company="Terry D. Eppler">
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
//   MetroPieChartSegment.cs
// </summary>
// ******************************************************************************************

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BudgetExecution
{
	/// <summary>
	/// Class MetroPieChartSegment.
	/// </summary>
	/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
	public class MetroPieChartSegment : INotifyPropertyChanged
	{
		/// <summary>
		/// The enc list
		/// </summary>
		private static List<WeakReference> __ENCList;

		/// <summary>
		/// The value
		/// </summary>
		private int _value;

		/// <summary>
		/// The fill color
		/// </summary>
		private Color _fillColor;

		/// <summary>
		/// The name
		/// </summary>
		private string _name;

		/// <summary>
		/// The maximum stored
		/// </summary>
		private int maxStored;

		/// <summary>
		/// The border color
		/// </summary>
		private Color _borderColor;

		/// <summary>
		/// The style
		/// </summary>
		private MetroPieChartSegment.eStyle _style;

		/// <summary>
		/// The fill style
		/// </summary>
		private HatchStyle _fillStyle;

		/// <summary>
		/// The use fill style
		/// </summary>
		private bool _UseFillStyle;

		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public Color BorderColor
		{
			get
			{
				return this._borderColor;
			}
			set
			{
				if (this._borderColor != value)
				{
					this._borderColor = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("BorderColor"));
					}
				}
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
				return this._fillColor;
			}
			set
			{
				if (this._fillColor != value)
				{
					this._fillColor = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("FillColor"));
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the fill style.
		/// </summary>
		/// <value>The fill style.</value>
		public HatchStyle FillStyle
		{
			get
			{
				return this._fillStyle;
			}
			set
			{
				if (this._fillStyle != value)
				{
					this._fillStyle = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("FillStyle"));
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (Operators.CompareString(this._name, value, false) != 0)
				{
					this._name = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("Name"));
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the style.
		/// </summary>
		/// <value>The style.</value>
		public MetroPieChartSegment.eStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				if (this._style != value)
				{
					this._style = value;
					if (this._style == MetroPieChartSegment.eStyle.AbstractBlue)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.AbstractBlue);
					}
					else if (this._style == MetroPieChartSegment.eStyle.AbstractPurple)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.AbstractPurple);
					}
					else if (this._style == MetroPieChartSegment.eStyle.AbstractRed)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.AbstractRed);
					}
					else if (this._style == MetroPieChartSegment.eStyle.LightBlue)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.LightBlue);
					}
					else if (this._style == MetroPieChartSegment.eStyle.LightOrange)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.LightOrange);
					}
					else if (this._style == MetroPieChartSegment.eStyle.LightRed)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.LightRed);
					}
					else if (this._style == MetroPieChartSegment.eStyle.LightCyan)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.LightCyan);
					}
					else if (this._style == MetroPieChartSegment.eStyle.DarkBlue)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.DarkBlue);
					}
					else if (this._style == MetroPieChartSegment.eStyle.SoapGreen)
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.SoapGreen);
					}
					else if (this._style != MetroPieChartSegment.eStyle.SoapRed)
					{
						this._style = MetroPieChartSegment.eStyle.Custom;
					}
					else
					{
						this.ApplyStyle(MetroPieChartSegment.eStyle.SoapRed);
					}
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("Style"));
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [use fill style].
		/// </summary>
		/// <value><c>true</c> if [use fill style]; otherwise, <c>false</c>.</value>
		public bool UseFillStyle
		{
			get
			{
				return this._UseFillStyle;
			}
			set
			{
				if (this._UseFillStyle != value)
				{
					this._UseFillStyle = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("UseFillStyle"));
					}
				}
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
				return this._value;
			}
			set
			{
				if (this._value != value)
				{
					this._value = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("Value"));
					}
				}
			}
		}

		/// <summary>
		/// Initializes static members of the <see cref="MetroPieChartSegment"/> class.
		/// </summary>
		[DebuggerNonUserCode]
		static MetroPieChartSegment()
		{
			MetroPieChartSegment.__ENCList = new List<WeakReference>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MetroPieChartSegment"/> class.
		/// </summary>
		public MetroPieChartSegment()
		{
			MetroPieChartSegment.__ENCAddToList(this);
			this._style = MetroPieChartSegment.eStyle.Custom;
			this._fillStyle = HatchStyle.BackwardDiagonal;
			this._UseFillStyle = false;
			this._value = 10;
			this._fillColor = Color.FromArgb(255, 129, 0);
			this._borderColor = Color.FromArgb(255, 129, 0);
			this._style = MetroPieChartSegment.eStyle.LightOrange;
		}

		/// <summary>
		/// Encs the add to list.
		/// </summary>
		/// <param name="value">The value.</param>
		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = MetroPieChartSegment.__ENCList;
			Monitor.Enter(_ENCList);
			try
			{
				if (MetroPieChartSegment.__ENCList.Count == MetroPieChartSegment.__ENCList.Capacity)
				{
					int item = 0;
					int count = checked(MetroPieChartSegment.__ENCList.Count - 1);
					for (int i = 0; i <= count; i = checked(i + 1))
					{
						if (MetroPieChartSegment.__ENCList[i].IsAlive)
						{
							if (i != item)
							{
								MetroPieChartSegment.__ENCList[item] = MetroPieChartSegment.__ENCList[i];
							}
							item = checked(item + 1);
						}
					}
					MetroPieChartSegment.__ENCList.RemoveRange(item, checked(MetroPieChartSegment.__ENCList.Count - item));
					MetroPieChartSegment.__ENCList.Capacity = MetroPieChartSegment.__ENCList.Count;
				}
				MetroPieChartSegment.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
			}
			finally
			{
				Monitor.Exit(_ENCList);
			}
		}

		/// <summary>
		/// Applies the style.
		/// </summary>
		/// <param name="eStyle">The e style.</param>
		private void ApplyStyle(MetroPieChartSegment.eStyle eStyle)
		{
			switch (eStyle)
			{
				case MetroPieChartSegment.eStyle.LightCyan:
				{
					this._fillColor = Color.FromArgb(0, 255, 155);
					this._borderColor = Color.FromArgb(0, 255, 155);
					break;
				}
				case MetroPieChartSegment.eStyle.LightBlue:
				{
					this._fillColor = Color.FromArgb(30, 151, 227);
					this._borderColor = Color.FromArgb(30, 151, 227);
					break;
				}
				case MetroPieChartSegment.eStyle.LightRed:
				{
					this._fillColor = Color.FromArgb(255, 42, 0);
					this._borderColor = Color.FromArgb(255, 42, 0);
					break;
				}
				case MetroPieChartSegment.eStyle.LightOrange:
				{
					this._fillColor = Color.FromArgb(255, 129, 0);
					this._borderColor = Color.FromArgb(255, 129, 0);
					break;
				}
				case MetroPieChartSegment.eStyle.AbstractRed:
				{
					this._fillColor = Color.FromArgb(91, 46, 49);
					this._borderColor = Color.FromArgb(193, 66, 72);
					break;
				}
				case MetroPieChartSegment.eStyle.AbstractBlue:
				{
					this._fillColor = Color.FromArgb(33, 73, 130);
					this._borderColor = Color.FromArgb(50, 109, 212);
					break;
				}
				case MetroPieChartSegment.eStyle.AbstractPurple:
				{
					this._fillColor = Color.FromArgb(79, 50, 136);
					this._borderColor = Color.FromArgb(124, 68, 208);
					break;
				}
				case MetroPieChartSegment.eStyle.DarkBlue:
				{
					this._fillColor = Color.FromArgb(40, 40, 40);
					this._borderColor = Color.FromArgb(0, 164, 240);
					break;
				}
				case MetroPieChartSegment.eStyle.SoapRed:
				{
					this._fillColor = Color.FromArgb(255, 63, 53);
					this._borderColor = Color.White;
					break;
				}
				case MetroPieChartSegment.eStyle.SoapGreen:
				{
					this._fillColor = Color.FromArgb(21, 159, 79);
					this._borderColor = Color.White;
					break;
				}
			}
		}

		/// <summary>
		/// Occurs when [property changed].
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Enum eStyle
		/// </summary>
		public enum eStyle
		{
			/// <summary>
			/// The light cyan
			/// </summary>
			LightCyan,
			/// <summary>
			/// The light blue
			/// </summary>
			LightBlue,
			/// <summary>
			/// The light red
			/// </summary>
			LightRed,
			/// <summary>
			/// The light orange
			/// </summary>
			LightOrange,
			/// <summary>
			/// The abstract red
			/// </summary>
			AbstractRed,
			/// <summary>
			/// The abstract blue
			/// </summary>
			AbstractBlue,
			/// <summary>
			/// The abstract purple
			/// </summary>
			AbstractPurple,
			/// <summary>
			/// The dark blue
			/// </summary>
			DarkBlue,
			/// <summary>
			/// The SOAP red
			/// </summary>
			SoapRed,
			/// <summary>
			/// The SOAP green
			/// </summary>
			SoapGreen,
			/// <summary>
			/// The custom
			/// </summary>
			Custom
		}
	}
}