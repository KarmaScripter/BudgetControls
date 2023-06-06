// ******************************************************************************************
//     Assembly:                Budget Execution
//     Author:                  Terry D. Eppler
//     Created:                 06-05-2023
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        06-05-2023
// ******************************************************************************************
// <copyright file="Animator.cs" company="Terry D. Eppler">
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
//   Animator.cs
// </summary>
// ******************************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BudgetExecution
{
    using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// A class collection for rendering a click animation.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    [ Description( "This is a click animation control" ) ]
    [ DesignerCategory( "Code" ) ]
    [ ToolboxBitmap( typeof( ClickAnimator ) ) ]
    public class ClickAnimator : Component
    {
        #region Private Fields

        /// <summary>
        /// The click control
        /// </summary>
        private Control _ClickControl;

        /// <summary>
        /// The effect timer
        /// </summary>
        [ AccessedThroughProperty( "Timer" ) ]
        private Timer _effectTimer;

        /// <summary>
        /// The step
        /// </summary>
        private int _Step;

        /// <summary>
        /// The speed
        /// </summary>
        private int _Speed;

        /// <summary>
        /// The start point
        /// </summary>
        private Point startPoint;

        /// <summary>
        /// The animation color
        /// </summary>
        private Color _AnimationColor;

        /// <summary>
        /// The draw mode
        /// </summary>
        private DrawMode _drawMode = DrawMode.Circle;

        /// <summary>
        /// The sides
        /// </summary>
        private int sides = 3;

        /// <summary>
        /// The radius
        /// </summary>
        private int radius = 9;

        /// <summary>
        /// The starting angle
        /// </summary>
        private int startingAngle = 90;

        /// <summary>
        /// The center width
        /// </summary>
        private int centerWidth = 18;

        /// <summary>
        /// The center
        /// </summary>
        private Point center;

        #endregion

        #region Enumeration

        /// <summary>
        /// Enum representing the type of drawing animation.
        /// </summary>
        public enum DrawMode
        {
            /// <summary>
            /// The circle
            /// </summary>
            Circle,

            /// <summary>
            /// The rectangle
            /// </summary>
            Rectangle
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        /// <value>The shape.</value>
        public DrawMode Shape
        {
            get { return _drawMode; }
            set
            {
                _drawMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the animation.
        /// </summary>
        /// <value>The color of the animation.</value>
        [ Browsable( true ) ]
        [ DefaultValue( typeof( Color ), "120, 255, 255, 255" ) ]
        [ Description( "Sets the color of the animation." ) ]
        public Color AnimationColor
        {
            get
            {
                return _AnimationColor;
            }
            set
            {
                _AnimationColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the click control.
        /// </summary>
        /// <value>The click control.</value>
        [ Browsable( true ) ]
        [ DefaultValue( null ) ]
        [ Description( "Set the control to render the animation on." ) ]
        public Control ClickControl
        {
            get
            {
                return _ClickControl;
            }
            set
            {
                UnRegisterControl( );
                _ClickControl = value;
                RegisterControl( );

                if( _ClickControl != null )
                {
                    _Speed = checked( (int)Math.Round( (double)_ClickControl.Width / 10 ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the animation timer.
        /// </summary>
        /// <value>The timer.</value>
        public virtual Timer Timer
        {
            [ DebuggerNonUserCode ]
            get
            {
                return _effectTimer;
            }
            [ DebuggerNonUserCode ]
            [ MethodImpl( MethodImplOptions.Synchronized ) ]
            set
            {
                var metroAnimator = this;
                EventHandler eventHandler = metroAnimator.effectTimer_Tick;

                if( _effectTimer != null )
                {
                    _effectTimer.Tick -= eventHandler;
                }

                _effectTimer = value;

                if( _effectTimer != null )
                {
                    _effectTimer.Tick += eventHandler;
                }
            }
        }

        /// <summary>
        /// Gets or sets the animation speed.
        /// </summary>
        /// <value>The speed.</value>
        [ Browsable( true ) ]
        [ DefaultValue( 10 ) ]
        [ Description( "Sets the animation speed." ) ]
        public int Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClickAnimator" /> class.
        /// </summary>
        public ClickAnimator( )
        {
            Timer = new Timer( )
            {
                Interval = 10
            };

            _Step = 0;
            _Speed = 10;
            startPoint = new Point( );
            _AnimationColor = Color.FromArgb( 120, 255, 255, 255 );
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Handles the Click event of the control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void control_Click( object sender, EventArgs e )
        {
            Timer.Enabled = true;
            startPoint = _ClickControl.PointToClient( Cursor.Position );
            _Step = 0;
        }

        /// <summary>
        /// Handles the Paint event of the control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void control_Paint( object sender, PaintEventArgs e )
        {
            var graphics = e.Graphics;

            using( var solidBrush = new SolidBrush( _AnimationColor ) )
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var rectangle = new Rectangle(
                    checked( (int)Math.Round( startPoint.X - _Step * ( (double)_Speed / 2 ) ) ),
                    checked( (int)Math.Round( startPoint.Y - _Step * ( (double)_Speed / 2 ) ) ),
                    checked( _Step * _Speed ), checked( _Step * _Speed ) );

                center = new Point( 100 / 2, 100 / 2 );
                radius = _ClickControl.Width / 4;

                switch( _drawMode )
                {
                    case DrawMode.Circle:
                        graphics.FillEllipse( solidBrush, rectangle );
                        break;
                    case DrawMode.Rectangle:
                        graphics.FillRectangle( solidBrush, rectangle );
                        break;
                }
            }

            graphics = null;
        }

        /// <summary>
        /// Handles the Tick event of the effectTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void effectTimer_Tick( object sender, EventArgs e )
        {
            _Step = checked( _Step + 1 );

            if( _ClickControl != null )
            {
                _ClickControl.Invalidate( );
            }

            if( startPoint.X >= _Step * ( (double)_Speed / 2 )
               || startPoint.Y >= _Step * ( (double)_Speed / 2 )
               || checked( _ClickControl.Width + _Speed ) >= _Step * ( (double)_Speed / 2 )
               || checked( _ClickControl.Height + _Speed ) >= _Step * ( (double)_Speed / 2 )
                   ? false
                   : true )
            {
                Timer.Enabled = false;
                _Step = 0;
            }
        }

        /// <summary>
        /// Registers the control.
        /// </summary>
        private void RegisterControl( )
        {
            if( _ClickControl != null )
            {
                var metroAnimator = this;
                _ClickControl.Paint += metroAnimator.control_Paint;
                var metroAnimator1 = this;
                _ClickControl.Click += metroAnimator1.control_Click;
                SetDoubleBuffered( _ClickControl );
            }
        }

        /// <summary>
        /// Sets the double buffered.
        /// </summary>
        /// <param name="ctrl">The control.</param>
        private void SetDoubleBuffered( Control ctrl )
        {
            if( !SystemInformation.TerminalServerSession )
            {
                var property = typeof( Control ).GetProperty( "DoubleBuffered",
                    BindingFlags.Instance | BindingFlags.NonPublic );

                property.SetValue( ctrl, true, null );
            }
        }

        /// <summary>
        /// Uns the register control.
        /// </summary>
        private void UnRegisterControl( )
        {
            if( _ClickControl != null )
            {
                var metroAnimator = this;
                _ClickControl.Paint -= metroAnimator.control_Paint;
                var metroAnimator1 = this;
                _ClickControl.Click -= metroAnimator1.control_Click;
            }
        }

        #endregion
    }
}