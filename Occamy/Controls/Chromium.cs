namespace Occamy.Controls {

	using System;
	using System.Windows.Forms;
	using CefSharp;



	/// <summary>
	/// Decorator for <see cref="CefSharp.WinForms.ChromiumWebBrowser"/>.
	/// </summary>
	public sealed class Chromium : CefSharp.WinForms.ChromiumWebBrowser {

		internal static int InstanceCount;



		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="targetForm"><see cref="Form"/>, which will be container for current <see cref="Chromium"/>.</param>
		public Chromium(Form targetForm) : base($"{Application.StartupPath}/../app/layout.html") {

			// ...
			InitializeCef();


			// ...
			ConsoleMessage += OnConsoleMessage;

			// ...
			BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
			BrowserSettings.WebSecurity = CefState.Disabled;
			BrowserSettings.OffScreenTransparentBackground = false;
			DragHandler = new DragHandler();
			MenuHandler = new MenuHandler();
			Dock = DockStyle.Fill;

			// ...
			RegisterJsObject("WindowApi", new WindowApi(targetForm));
			RegisterJsObject("FileSystemApi", new FileSystemApi(targetForm));
			RegisterJsObject("OpenFileDialogApi", new OpenFileDialogApi(targetForm));
			RegisterJsObject("SaveFileDialogApi", new SaveFileDialogApi(targetForm));

			// Converting System.Drawing.Color to unsigned integer
			BrowserSettings.BackgroundColor = (uint)(
				(App.Settings.WindowDefaultColor.A << 24) |
				(App.Settings.WindowDefaultColor.R << 16) |
				(App.Settings.WindowDefaultColor.G << 8) |
				(App.Settings.WindowDefaultColor.B << 0)
			);

		}



		private static void InitializeCef() {

			// ...
			++InstanceCount;

			// ...
			if (Cef.IsInitialized) return;

			// ...
			var settings = new CefSettings {
				IgnoreCertificateErrors = true,
				WindowlessRenderingEnabled = true,
			};

			// ...
			if (!Cef.Initialize(settings)) {
				throw new Exception("Failed during Chromium Embedded Framework initialization.");
			}

		}



		private static void ShutdownCef() {

			// ...
			if (--InstanceCount > 0 || !Cef.IsInitialized) return;

			// ...
			Cef.Shutdown();

		}



		private static void OnConsoleMessage(object sender, ConsoleMessageEventArgs e) {
			App.Debugger.WriteLine(e.Message);
		}



		protected override void Dispose(bool disposing) {

			// ...
			base.Dispose(disposing);
			ShutdownCef();

		}

	}



}
