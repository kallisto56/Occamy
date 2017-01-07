namespace Occamy.Native {

	using System;
	using System.Runtime.InteropServices;



	/// <summary>
	/// Platform Invocation methods, such as those that are marked by using the System.Runtime.InteropServices.DllImportAttribute attribute and access unmanaged code.
	/// </summary>
	public static class NativeMethods {



		/// <summary>
		/// Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message. To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function. To post a message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.
		/// </summary>
		/// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows. Message sending is subject to UIPI. The thread of a process can send messages only to message queues of threads in processes of lesser or equal integrity level.</param>
		/// <param name="msg">The message to be sent. For lists of the system-provided messages, see System-Defined Messages.</param>
		/// <param name="wParam">Additional message-specific information.</param>
		/// <param name="lParam">Additional message-specific information.</param>
		/// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
		[DllImport("user32.dll")]
		internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);



		/// <summary>
		/// Releases the mouse capture from a window in the current thread and restores normal mouse input processing. A window that has captured the mouse receives all mouse input, regardless of the position of the cursor, except when a mouse button is clicked while the cursor hot spot is in the window of another thread.
		/// </summary>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
		[DllImport("user32.dll")]
		internal static extern bool ReleaseCapture();



		/// <summary>
		/// Retrieves information about the specified window. The function also retrieves the 32-bit (DWORD) value at the specified offset into the extra window memory.
		/// </summary>
		/// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
		/// <param name="nIndex">The zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus four; for example, if you specified 12 or more bytes of extra memory, a value of 8 would be an index to the third 32-bit integer. To retrieve any other value, specify one of the following values.</param>
		/// <returns>If the function succeeds, the return value is the requested value. If the function fails, the return value is zero. To get extended error information, call GetLastError. If SetWindowLong has not been called previously, GetWindowLong returns zero for values in the extra window or class memory.</returns>
		[DllImport("user32.dll")]
		public static extern Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);



		/// <summary>
		/// Retrieves the specified system metric or system configuration setting. Note that all dimensions retrieved by GetSystemMetrics are in pixels.
		/// </summary>
		/// <param name="nIndex">The system metric or configuration setting to be retrieved. This parameter can be one of the following values. Note that all SM_CX* values are widths and all SM_CY* values are heights. Also note that all settings designed to return Boolean data represent TRUE as any nonzero value, and FALSE as a zero value.</param>
		/// <returns>If the function succeeds, the return value is the requested system metric or configuration setting. If the function fails, the return value is 0. GetLastError does not provide extended error information.</returns>
		[DllImport("user32.dll")]
		internal static extern int GetSystemMetrics(SystemMetric nIndex);



		/// <summary>
		/// Sends an appbar message to the system.
		/// </summary>
		/// <param name="dwMessage">Appbar message value to send. This parameter can be one of the following values.</param>
		/// <param name="pData">A pointer to an AppBarData structure. The content of the structure on entry and on exit depends on the value set in the dwMessage parameter. See the individual message pages for specifics.</param>
		/// <returns></returns>
		[DllImport("shell32.dll")]
		internal static extern int SHAppBarMessage(AppBarMessages dwMessage, [In] ref AppBarData pData);



		/// <summary>
		/// The CreateRoundRectRgn function creates a rectangular region with rounded corners.
		/// </summary>
		/// <param name="nLeftRect">Specifies the x-coordinate of the upper-left corner of the region in device units.</param>
		/// <param name="nTopRect">Specifies the y-coordinate of the upper-left corner of the region in device units.</param>
		/// <param name="nRightRect">Specifies the x-coordinate of the lower-right corner of the region in device units.</param>
		/// <param name="nBottomRect">Specifies the y-coordinate of the lower-right corner of the region in device units.</param>
		/// <param name="nWidthEllipse">Specifies the width of the ellipse used to create the rounded corners in device units.</param>
		/// <param name="nHeightEllipse">Specifies the height of the ellipse used to create the rounded corners in device units.</param>
		/// <returns>If the function succeeds, the return value is the handle to the region. If the function fails, the return value is NULL.</returns>
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		internal static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);



		/// <summary>
		/// Opens a Windows Explorer window with specified items in a particular folder selected.
		/// </summary>
		/// <param name="pidlFolder">A pointer to a fully qualified item ID list that specifies the folder.</param>
		/// <param name="cidl">A count of items in the selection array, apidl. If cidl is zero, then pidlFolder must point to a fully specified ITEMIDLIST describing a single item to select. This function opens the parent folder and selects that item.</param>
		/// <param name="apidl">A pointer to an array of PIDL structures, each of which is an item to select in the target folder referenced by pidlFolder.</param>
		/// <param name="dwFlags">The optional flags. Under Windows XP this parameter is ignored. In Windows Vista, the following flags are defined.</param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		[DllImport("shell32.dll", SetLastError = true)]
		internal static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);



		/// <summary>
		/// Translates a Shell namespace object's display name into an item identifier list and returns the attributes of the object. This function is the preferred method to convert a string to a pointer to an item identifier list (PIDL).
		/// </summary>
		/// <param name="name">A pointer to a zero-terminated wide string that contains the display name to parse.</param>
		/// <param name="bindingContext">A bind context that controls the parsing operation. This parameter is normally set to NULL.</param>
		/// <param name="pidl">The address of a pointer to a variable of type ITEMIDLIST that receives the item identifier list for the object. If an error occurs, then this parameter is set to NULL.</param>
		/// <param name="sfgaoIn">A ULONG value that specifies the attributes to query. To query for one or more attributes, initialize this parameter with the flags that represent the attributes of interest. For a list of available SFGAO flags, see IShellFolder::GetAttributesOf.</param>
		/// <param name="psfgaoOut">A pointer to a ULONG. On return, those attributes that are true for the object and were requested in sfgaoIn are set. An object's attribute flags can be zero or a combination of SFGAO flags. For a list of available SFGAO flags, see IShellFolder::GetAttributesOf.</param>
		/// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
		[DllImport("shell32.dll", SetLastError = true)]
		internal static extern int SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);



	}

}
