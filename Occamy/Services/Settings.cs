namespace Occamy.Services {

	using System;
	using System.Drawing;
	using System.IO;
	using System.Windows.Forms;

	using Newtonsoft.Json;



	/// <summary>
	/// Application configuration.
	/// </summary>
	public sealed class Settings {

		public string Title = "Occamy";
		public string Icon = "resources\\application.ico";
		public string LayeredTheme = "resources\\layered.theme.png";
		public Rectangle LayeredInnerRectangle = new Rectangle(98, 78, 196, 196);
		public Padding LayeredMargin = new Padding(-49, -49, -50, -50);
		public Size LayeredMaximumSize = new Size(3000, 3000);

		public Color WindowDefaultColor = Color.White;
		public Size WindowDefaultSize = new Size(1024, 768);
		public Size WindowMinimumSize = new Size(512, 384);

		public FormStartPosition FormStartPosition = FormStartPosition.CenterScreen;
		public FormWindowState FormWindowState = FormWindowState.Normal;

		public int GripOffsetLeft = 0;
		public int GripOffsetRight = 0;
		public string ChromiumEventHanlderFormat = "$this.router.triggerEvent({0}, {1});";



		/// <summary>
		/// Returns instance of Services.Settings with data loaded from json-file
		/// </summary>
		public static Settings Load() {

			// ...
			var path = $"{AppDomain.CurrentDomain.BaseDirectory}settings.json";
			if (!File.Exists(path)) throw new FileNotFoundException("Configuration file not found", path);

			// ...
			var jsonContent = File.ReadAllText(path);
			return JsonConvert.DeserializeObject<Settings>(jsonContent);

		}



		/// <summary>
		/// Writes default configuration into json-file
		/// </summary>
		public static void WriteDefaultSettings() {

			// ...
			var settings = new Settings();
			var path = $"{AppDomain.CurrentDomain.BaseDirectory}settings.json";
			var jsonContent = JsonConvert.SerializeObject(settings, Formatting.Indented);

			// ...
			File.WriteAllText(path, jsonContent);

		}



	}



}
