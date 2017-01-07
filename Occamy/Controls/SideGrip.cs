namespace Occamy.Controls {

	using System;
	using System.Drawing;

	using Native;



	/// <summary>
	/// Control for resizing one side of the <see cref="Form"/>.
	/// </summary>
	public sealed class SideGrip : FormGrip {

		private readonly HitTestValues _hitTestValue;



		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value">Value, that will be transmitted to <see cref="Form"/>, when triggered.</param>
		public SideGrip(HitTestValues value) {

			// ...
			_hitTestValue = value;
			Location = Point.Empty;

		}



		/// <summary>
		/// Updates size of current control, when size of parent control has been changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnParentSizeChanged(object sender, EventArgs e) {

			// ...
			BringToFront();

			// ...
			switch (_hitTestValue) {

				case HitTestValues.LEFT:
					Size = new Size(SideWidth, Parent.Height);
					break;

				case HitTestValues.RIGHT:
					Size = new Size(SideWidth, Parent.Height);
					Location = new Point(Parent.ClientSize.Width - SideWidth, 0);
					break;

				case HitTestValues.TOP:
					Size = new Size(Parent.ClientSize.Width, SideWidth);
					break;

				case HitTestValues.BOTTOM:
					Size = new Size(Parent.ClientSize.Width, SideWidth);
					Location = new Point(0, Parent.ClientSize.Height - SideWidth);
					break;

				default:
					throw new ArgumentOutOfRangeException();

			}

		}




	}



}
