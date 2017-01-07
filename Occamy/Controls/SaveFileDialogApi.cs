namespace Occamy.Controls {

	using System.Collections.Generic;
	using System.Windows.Forms;
	
	using Newtonsoft.Json;



	/// <summary>
	/// Application protocol interface between <see cref="Form"/>, <see cref="FileDialog"/> and <see cref="Chromium"/>.
	/// </summary>
	public sealed class SaveFileDialogApi : CefInterface {

		private Controls.Form _form;



		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="form">Form, to which actions are will be applied</param>
		public SaveFileDialogApi(Controls.Form form) {
			this._form = form;
		}



		/// <summary>
		/// Gets or sets the file dialog box title.
		/// </summary>
		/// <returns>The file dialog box title. The default value is an empty string ("").</returns>
		public string Title {
			get {
				var title = string.Empty;
				_form.Invoke((MethodInvoker)delegate {
					title = _form.SaveFileDialog.Title;
				});
				return title;
			}
			set {
				_form.Invoke((MethodInvoker)delegate {
					_form.SaveFileDialog.Title = value;
				});
			}
		}



		/// <summary>
		/// Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.
		/// </summary>
		/// <returns>The file filtering options available in the dialog box.</returns>
		public string Filter {
			get {
				var filter = string.Empty;
				_form.Invoke((MethodInvoker)delegate {
					filter = _form.SaveFileDialog.Filter;
				});
				return filter;
			}
			set {
				_form.Invoke((MethodInvoker)delegate {
					_form.SaveFileDialog.Filter = value;
				});
			}
		}



		/// <summary>
		/// Gets or sets the initial directory displayed by the file dialog box.
		/// </summary>
		/// <returns>The initial directory displayed by the file dialog box. The default is an empty string ("").</returns>
		public string InitialDirectory {
			get {
				var initialDirectory = string.Empty;
				_form.Invoke((MethodInvoker)delegate {
					initialDirectory = _form.SaveFileDialog.InitialDirectory;
				});
				return initialDirectory;
			}
			set {
				_form.Invoke((MethodInvoker)delegate {
					_form.SaveFileDialog.InitialDirectory = value;
				});
			}
		}



		/// <summary>
		/// Gets or sets a name of event, that will be fired on Cef-side, when dialog is closed with DialogResult.OK
		/// </summary>
		public string CallbackEvent {
			get {
				var callbackEvent = string.Empty;
				_form.Invoke((MethodInvoker)delegate {
					callbackEvent = (string)_form.SaveFileDialog.Tag;
				});
				return callbackEvent;
			}
			set {
				_form.Invoke((MethodInvoker)delegate {
					_form.SaveFileDialog.Tag = value;
				});
			}
		}



		/// <summary>
		/// Shows an FileDialog.
		/// </summary>
		public void Show() {

			_form.Invoke((MethodInvoker)delegate {

				// Showing dialog and waiting for result
				var dialogResult = _form.SaveFileDialog.ShowDialog(_form);

				// Creating dictionary in order to store detailed information about dialog result
				var details = new Dictionary<string, object>();
				details.Add("dialogResult", dialogResult.ToString());
				details.Add("files", dialogResult == DialogResult.OK ? _form.SaveFileDialog.FileNames : new string[0]);

				// Converting dictionary into json-string and firing event on Cef side.
				var jsonContent = JsonConvert.SerializeObject(details);
				_form.TriggerChromiumEvent((string)_form.SaveFileDialog.Tag, jsonContent);

			});

		}



	}



}
