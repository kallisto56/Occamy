namespace Occamy.Controls {

	using CefSharp;



	/// <summary>
	/// Custom, empty menu handler, presence of which will disable default context menu
	/// </summary>
	public sealed class MenuHandler : IContextMenuHandler {




		public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model) {
			
		}




		public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags) {
			return true;
		}




		public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame) {
			
		}




		public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback) {
			return true;
		}

	}

}
