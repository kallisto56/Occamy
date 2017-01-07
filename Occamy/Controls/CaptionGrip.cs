namespace Occamy.Controls {

	using System;
	using System.Drawing;



	/// <summary>
	/// Control for changing position of <see cref="Form"/>.
	/// </summary>
	public sealed class CaptionGrip : FormGrip {

		public int LeftOffset = 0;
		public int RightOffset = 128;



		/// <summary>
		/// Updates size of current control, when size of parent control has been changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void OnParentSizeChanged(object sender, EventArgs e) {

			// ...
			BringToFront();

			// ...
			Size = new Size(Parent.ClientSize.Width - LeftOffset - RightOffset, CaptionHeight);
			Location = new Point(LeftOffset, 0);

		}



	}



}
