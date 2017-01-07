namespace Occamy.Native {

	using System;
	using System.Runtime.InteropServices;



	/// <summary>
	/// Contains information about a system appbar message.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct AppBarData {

		/// <summary>The size of the structure, in bytes. Initialize using: Marshal.SizeOf(typeof(AppBarData));</summary>
		public int cbSize;

		/// <summary>The handle to the appbar window. Not all messages use this member. See the individual message page to see if you need to provide an hWind value.</summary>
		public IntPtr hWnd;

		/// <summary>An application-defined message identifier. The application uses the specified identifier for notification messages that it sends to the appbar identified by the hWnd member. This member is used when sending the ABM_NEW message.</summary>
		public uint uCallbackMessage;

		/// <summary>A value that specifies an edge of the screen. This member is used when sending one of these messages: ABM_GETAUTOHIDEBAR, ABM_SETAUTOHIDEBAR, ABM_GETAUTOHIDEBAREX, ABM_SETAUTOHIDEBAREX, ABM_QUERYPOS, ABM_SETPOS</summary>
		public uint uEdge;

		/// <summary>A RECT structure whose use varies depending on the message: When ABM_GETTASKBARPOS, ABM_QUERYPOS, ABM_SETPOS: The bounding rectangle, in screen coordinates, of an appbar or the Windows taskbar. When ABM_GETAUTOHIDEBAREX, ABM_SETAUTOHIDEBAREX: The monitor on which the operation is being performed. This information can be retrieved through the GetMonitorInfo function.</summary>
		public NativeRect rc;

		/// <summary>A message-dependent value. This member is used with these messages: ABM_SETAUTOHIDEBAR, ABM_SETAUTOHIDEBAREX, ABM_SETSTATE</summary>
		public int lParam;

	}

}
