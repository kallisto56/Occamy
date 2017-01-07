namespace Occamy.Controls {

	using System;
	using System.Collections.Generic;
	using System.Windows.Forms;

	using Newtonsoft.Json;
	using CefSharp;



	/// <summary>
	/// Application protocol interface between <see cref="Form"/> and <see cref="Chromium"/>.
	/// </summary>
	public sealed class WindowApi : CefInterface {

		private Controls.Form _form;


		/// <summary>Allows to set or get title of target form</summary>
		public string Title {
			get {
				var title = string.Empty;
				_form.Invoke((MethodInvoker)delegate {
					title = _form.Text;
				});
				return title;
			}
			set {
				_form.Invoke((MethodInvoker)delegate {
					_form.Text = value;
				});
			}
		}


		/// <summary>Returns version of occamy</summary>
		public string Version {
			get { return Application.ProductVersion; }
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="form">Form, to which actions are will be applied</param>
		public WindowApi(Controls.Form form) {
			this._form = form;
		}


		/// <summary>
		/// Set window state to be maximized
		/// </summary>
		public void Maximize() {
			_form.Invoke((MethodInvoker)delegate {
				_form.WindowState = FormWindowState.Maximized;
			});
		}



		/// <summary>
		/// Returns state of window.
		/// </summary>
		/// <returns>Return true, if window is maximized, otherwise false.</returns>
		public bool IsMaximized() {
			var isMaximized = false;
			_form.Invoke((MethodInvoker)delegate {
				isMaximized = (_form.MinMaxState == FormWindowState.Maximized);
			});
			return isMaximized;
		}


		/// <summary>
		/// Set window state to be minimized.
		/// </summary>
		public void Minimize() {
			_form.Invoke((MethodInvoker)delegate {
				_form.WindowState = FormWindowState.Minimized;
			});
		}


		/// <summary>
		/// Set window state to be normal
		/// </summary>
		public void Restore() {
			_form.Invoke((MethodInvoker)delegate {
				_form.WindowState = FormWindowState.Normal;
			});
		}


		/// <summary>
		/// Closes window
		/// </summary>
		public void Close() {
			_form.Invoke((MethodInvoker)delegate {
				_form.Close();
			});
		}


		/// <summary>
		/// Shows CEF developer tools
		/// </summary>
		public void ShowDevTools() {
			_form.Invoke((MethodInvoker)delegate {
				_form.Chromium.ShowDevTools();
			});
		}



		/// <summary>
		/// Set state of form grip area
		/// </summary>
		/// <param name="state">State, if set to true, form grip area will be interactive.</param>
		public void ToggleGripArea(bool state) {
			_form.Invoke((MethodInvoker)delegate {
				foreach (Control control in _form.Controls) {
					if (control is FormGrip) control.Visible = state;
				}
			});

		}



		/// <summary>
		/// Sets offset for form grip area from left and right side of window, allowing to interact with that area from CEF-side.
		/// </summary>
		/// <param name="leftOffset">Offset from left side of the window</param>
		/// <param name="rightOffset">Offset from right side of the window</param>
		public void SetCaptionFormGrip(int leftOffset, int rightOffset) {
			_form.Invoke((MethodInvoker)delegate {
				_form.CaptionGrip.LeftOffset = leftOffset;
				_form.CaptionGrip.RightOffset = rightOffset;
				_form.CaptionGrip.OnParentSizeChanged(this, EventArgs.Empty);
			});

		}


		/// <summary>
		/// Returns list of files, that was dropped by user into window.
		/// </summary>
		/// <returns>JSON-format array of filenames.</returns>
		public string GetDragHandlerFiles() {
			var dragHandler = (DragHandler)_form.Chromium.DragHandler;
			return JsonConvert.SerializeObject(dragHandler.Files);
		}



	}



}
