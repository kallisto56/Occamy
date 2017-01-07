namespace Occamy.Native {

	using System.Runtime.InteropServices;


	/// <summary>
	/// NativePoint structure defines the x- and y- coordinates of a point.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct NativePoint {

		/// <summary>
		/// The x-coordinate of the point.
		/// </summary>
		public short X;

		/// <summary>
		/// The y-coordinate of the point.
		/// </summary>
		public short Y;

		/// <summary>
		/// Converts NativePoint into a single integer value, that contains both coordinates.
		/// </summary>
		public int ToInt() {
			return (ushort)this.X | (this.Y << 16);
		}
	}

}
