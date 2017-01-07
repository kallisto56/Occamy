namespace Occamy.Native {

	using System.Runtime.InteropServices;



	/// <summary>
	/// NativeRect structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct NativeRect {

		/// <summary>
		/// The x-coordinate of the upper-left corner of the rectangle.
		/// </summary>
		public int Left;

		/// <summary>
		/// The y-coordinate of the upper-left corner of the rectangle.
		/// </summary>
		public int Top;

		/// <summary>
		/// The x-coordinate of the lower-right corner of the rectangle.
		/// </summary>
		public int Right;

		/// <summary>
		/// The y-coordinate of the lower-right corner of the rectangle.
		/// </summary>
		public int Bottom;

	}

}
