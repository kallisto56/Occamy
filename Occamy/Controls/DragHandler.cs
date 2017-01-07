namespace Occamy.Controls {

	using System.Collections.Generic;
	using CefSharp;


	/// <summary>
	/// Custom DragHandler, which contains array of files, that was 'dropped' by user.
	/// </summary>
	public sealed class DragHandler : IDragHandler {

		public IList<string> Files;


		public DragHandler() {
			Files = new List<string>();
		}



		public bool OnDragEnter(IWebBrowser browserControl, IBrowser browser, IDragData dragData, DragOperationsMask mask) {
			if (dragData.IsFile) Files = dragData.FileNames;
			return false;
		}



		public void OnDraggableRegionsChanged(IWebBrowser browserControl, IBrowser browser, IList<DraggableRegion> regions) {

		}

	}



}
