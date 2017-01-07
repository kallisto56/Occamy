namespace Occamy.Native {

	using System;



	[Flags]
	internal enum AppBarState {

		/// <summary>No autohide, not always top.</summary>
		ABS_MANUAL = 0,

		/// <summary>The taskbar is in the autohide state.</summary>
		ABS_AUTOHIDE = 1,

		/// <summary>The taskbar is in the always-on-top state. As of Windows 7, ABS_ALWAYSONTOP is no longer returned because the taskbar is always in that state. Older code should be updated to ignore the absence of this value in not assume that return value to mean that the taskbar is not in the always-on-top state.</summary>
		ABS_ALWAYSONTOP = 2,

		/// <summary>The taskbar is in the always-on-top state and in autohide state</summary>
		ABS_AUTOHIDEANDONTOP = 3,

	}

}
