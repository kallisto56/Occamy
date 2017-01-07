namespace Occamy.Native {



	internal enum AppBarMessages : uint {

		/// <summary>Registers a new appbar and specifies the message identifier that the system should use to send notification messages to the appbar.</summary>
		ABM_NEW = 0x00000000,

		/// <summary>Unregisters an appbar, removing the bar from the system's internal list.</summary>
		ABM_REMOVE = 0x00000001,

		/// <summary>Requests a size and screen position for an appbar.</summary>
		ABM_QUERYPOS = 0x00000002,

		/// <summary>Sets the size and screen position of an appbar.</summary>
		ABM_SETPOS = 0x00000003,

		/// <summary>Retrieves the autohide and always-on-top states of the Windows taskbar.</summary>
		ABM_GETSTATE = 0x00000004,

		/// <summary>Retrieves the bounding rectangle of the Windows taskbar. Note that this applies only to the system taskbar. Other objects, particularly toolbars supplied with third-party software, also can be present. As a result, some of the screen area not covered by the Windows taskbar might not be visible to the user. To retrieve the area of the screen not covered by both the taskbar and other app bars—the working area available to your application—, use the GetMonitorInfo function.</summary>
		ABM_GETTASKBARPOS = 0x00000005,

		/// <summary>Notifies the system to activate or deactivate an appbar. The lParam member of the APPBARDATA pointed to by pData is set to TRUE to activate or FALSE to deactivate.</summary>
		ABM_ACTIVATE = 0x00000006,

		/// <summary>Retrieves the handle to the autohide appbar associated with a particular edge of the screen.</summary>
		ABM_GETAUTOHIDEBAR = 0x00000007,

		/// <summary>Registers or unregisters an autohide appbar for an edge of the screen.</summary>
		ABM_SETAUTOHIDEBAR = 0x00000008,

		/// <summary>Notifies the system when an appbar's position has changed.</summary>
		ABM_WINDOWPOSCHANGED = 0x00000009,

		/// <summary>Windows XP and later: Sets the state of the appbar's autohide and always-on-top attributes.</summary>
		ABM_SETSTATE = 0x0000000A,


	}

}
