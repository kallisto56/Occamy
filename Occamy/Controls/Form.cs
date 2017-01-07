namespace Occamy.Controls {

	using System;
	using System.IO;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Runtime.InteropServices;

	using CefSharp;
	using Native;



	/// <summary>
	/// ...
	/// </summary>
	public sealed class Form : System.Windows.Forms.Form {

		public static Layered.Theme LayeredTheme = null;
		public Layered.Window LayeredWindow = null;

		public event EventHandler OnWindowPosChanged;
		public FormWindowState PreviousFormWindowState;

		public CaptionGrip CaptionGrip;

		public Chromium Chromium;
		public OpenFileDialog OpenFileDialog = new OpenFileDialog();
		public SaveFileDialog SaveFileDialog = new SaveFileDialog();



		/// <summary>
		/// Constructor.
		/// </summary>
		public Form() {

			// ...
			if (LayeredTheme == null) {
				var path = $"{Application.StartupPath}/{App.Settings.LayeredTheme}";
				if (File.Exists(path)) {
					using (var bitmap = new Bitmap(path)) {
						LayeredTheme = new Layered.Theme(bitmap, App.Settings.LayeredInnerRectangle, App.Settings.LayeredMargin);
					}
				}
			}

			// ...
			if (LayeredTheme != null) {
				LayeredWindow = new Layered.Window(this, LayeredTheme, App.Settings.LayeredMaximumSize);
				OnWindowPosChanged += LayeredWindow.Update;
			}

			// ...
			Text = App.Settings.Title;
			StartPosition = App.Settings.FormStartPosition;
			Size = App.Settings.WindowDefaultSize;
			BackColor = App.Settings.WindowDefaultColor;
			WindowState = PreviousFormWindowState = App.Settings.FormWindowState;
			MinimumSize = App.Settings.WindowMinimumSize;
			FormBorderStyle = FormBorderStyle.Sizable;
			AllowTransparency = false;

			// ...
			var icoPath = $"{Application.StartupPath}/{App.Settings.Icon}";
			Icon = File.Exists(icoPath) ? new Icon(icoPath) : Properties.Resources.AppIcon;

			// ...
			Controls.AddRange(new Control[] {

				CaptionGrip = new CaptionGrip(),

				new SideGrip(HitTestValues.TOP),
				new SideGrip(HitTestValues.BOTTOM),

				new SideGrip(HitTestValues.LEFT),
				new SideGrip(HitTestValues.RIGHT),

			});

			// ...
			var timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += (s, e) => {
				Controls.Add(Chromium = new Chromium(this));
				timer.Dispose();
			};
			timer.Start();

		}



		protected override void OnSizeChanged(EventArgs e) {

			// ...
			base.OnSizeChanged(e);

			// ...
			var value = MinMaxState;
			if (PreviousFormWindowState != value) {
				PreviousFormWindowState = value;
				TriggerChromiumEvent("window/state-changed", "{state:"+(int)value+"}");
			}

			// ...
			Region = MinMaxState != FormWindowState.Maximized
				? Region.FromHrgn(NativeMethods.CreateRoundRectRgn(0, 0, Width + 1, Height + 1, 2, 2))
				: null;

		}



		public void TriggerChromiumEvent(string name, string json) {
			
			// ...
			if (string.IsNullOrEmpty(App.Settings.ChromiumEventHanlderFormat)) return;
			if (!Chromium.IsBrowserInitialized) return;
			Chromium.EvaluateScriptAsync(string.Format(App.Settings.ChromiumEventHanlderFormat, name, json));

		}



		protected override CreateParams CreateParams {
			get {
				var parameters = base.CreateParams;
				parameters.ExStyle |= (int)ExtendedWindowStyles.WS_EX_ACCEPTFILES;
				return parameters;
			}
		}



		protected override void WndProc(ref Message m) {

			switch (m.Msg) {

				case (int)WindowMessages.WM_WINDOWPOSCHANGED:
					WM_WINDOWPOSCHANGED(ref m);
					break;

				case (int)WindowMessages.WM_NCCALCSIZE:
					WM_NCCALCSIZE(ref m);
					return;

				case (int)WindowMessages.WM_NCACTIVATE:
					if (MinMaxState != FormWindowState.Minimized) {
						m.Result = (IntPtr)1;
					} else {
						base.WndProc(ref m);

					}
					break;

				case 174:
					return;

				default:
					base.WndProc(ref m);
					break;
			}

		}



		private void WM_WINDOWPOSCHANGED(ref Message m) {

			// ...
			DefWndProc(ref m);
			UpdateBounds();
			m.Result = (IntPtr)1;

			// ...
			OnWindowPosChanged?.Invoke(this, EventArgs.Empty);

		}



		public FormWindowState MinMaxState {
			get {
				var style = NativeMethods.GetWindowLong(Handle, (int)WindowLongFlags.GWL_STYLE);
				if ((style & (int)WindowStyles.WS_MAXIMIZE) > 0) return FormWindowState.Maximized;
				return ((style & (int)WindowStyles.WS_MINIMIZE) > 0) ? FormWindowState.Minimized : FormWindowState.Normal;
			}
		}



		private void WM_NCCALCSIZE(ref Message m) {

			if (MinMaxState == FormWindowState.Maximized) {

				// ...
				var rectangle = (NativeRect)Marshal.PtrToStructure(m.LParam, typeof(NativeRect));

				// ...
				var paddedBorder = NativeMethods.GetSystemMetrics(SystemMetric.SM_CXPADDEDBORDER);
				var width = NativeMethods.GetSystemMetrics(SystemMetric.SM_CXSIZEFRAME) + paddedBorder;
				var height = NativeMethods.GetSystemMetrics(SystemMetric.SM_CYSIZEFRAME) + paddedBorder;

				// ...
				rectangle.Left += width;
				rectangle.Top += height;
				rectangle.Right -= width;
				rectangle.Bottom -= height;

				// ...
				var appBarData = new AppBarData();
				appBarData.cbSize = Marshal.SizeOf(typeof(AppBarData));

				// ...
				var state = (AppBarState)NativeMethods.SHAppBarMessage(AppBarMessages.ABM_GETSTATE, ref appBarData);
				if ((state & AppBarState.ABS_AUTOHIDE) != 0) {
					rectangle.Bottom -= 1;
				}

				// ...
				Marshal.StructureToPtr(rectangle, m.LParam, true);

			}

			// ...
			m.Result = IntPtr.Zero;

		}



		protected override void Dispose(bool disposing) {
			LayeredWindow.Dispose();
			base.Dispose(disposing);
		}



	}



}
