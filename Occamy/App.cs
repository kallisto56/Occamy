namespace Occamy {

	using System;
	using System.Windows.Forms;



	/// <summary>
	/// Application bootstrap
	/// </summary>
	public static class App {

		public static Services.Debugger Debugger;
		public static Services.Settings Settings;



		[STAThread]
		internal static void Main(string[] args) {

			// Checking command line arguments
			if (args.Length > 0) {
				for (var n = 0; n < args.Length; n++) {
					switch (args[n]) {
						case "-reset-settings":
							Services.Settings.WriteDefaultSettings();
							break;
						default:
							break;
					}
				}
			}
			
			// Application services
			App.Debugger = new Services.Debugger(AppDomain.CurrentDomain.BaseDirectory);
			App.Settings = Services.Settings.Load();

			// ...
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Controls.Form());

		}



	}



}
