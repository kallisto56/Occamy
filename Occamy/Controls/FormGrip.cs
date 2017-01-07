namespace Occamy.Controls {

	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using Native;



	/// <summary>
	/// Area, that can be used as trigger for changing position or size of the <see cref="Form"/>.
	/// </summary>
	public abstract class FormGrip : Control {

		protected const int SideWidth = 10;
		protected const int CaptionHeight = 32;
		public override bool AllowDrop => true;



		/// <summary>
		/// Constructor.
		/// </summary>
		protected FormGrip() {

			// Since we're doing painting on our own
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

		}



		protected override void OnDragEnter(DragEventArgs e) {
			e.Effect = DragDropEffects.Move;
			Visible = false;
			base.OnDragEnter(e);
		}



		protected override void OnMouseMove(MouseEventArgs e) {

			// ...
			var value = HitTestOnMouseMove();
			if (Cursor != value) Cursor = value;

			// ...
			base.OnMouseMove(e);

		}



		protected override void OnMouseDown(MouseEventArgs e) {

			// ...
			var wParam = HitTestOnMouseDown();

			// ...
			if (wParam != HitTestValues.NOWHERE) {

				// ...
				NativeMethods.ReleaseCapture();

				// ...
				var location = new NativePoint { X = (short)MousePosition.X, Y = (short)MousePosition.Y };
				var button = SystemInformation.MouseButtonsSwapped
					? (int)WindowMessages.WM_NCRBUTTONDOWN
					: (int)WindowMessages.WM_NCLBUTTONDOWN;

				// ...
				var lParam = (ushort)location.X | (location.Y << 16);
				NativeMethods.SendMessage(Parent.Handle, button, (int)wParam, lParam);

			}

			// ...
			base.OnMouseDown(e);

		}



		protected Cursor HitTestOnMouseMove() {

			// ...
			var x = MousePosition.X;
			var y = MousePosition.Y;
			var r = new Rectangle(new Point(Parent.Left, Parent.Top), Parent.ClientSize);

			// ...
			var top = (y > r.Top) && (y < r.Top + SideWidth);
			var bottom = (y < r.Bottom) && (y > r.Bottom - SideWidth);
			var left = (x > r.Left) && (x < r.Left + SideWidth);
			var right = (x < r.Right + Size.Width) && (x > r.Right - SideWidth);

			// Corners
			if (top && left) return Cursors.SizeNWSE;
			if (top && right) return Cursors.SizeNESW;
			if (bottom && left) return Cursors.SizeNESW;
			if (bottom && right) return Cursors.SizeNWSE;

			// Sides
			if (left || right) return Cursors.SizeWE;
			if (top || bottom) return Cursors.SizeNS;

			// None
			return Cursors.Default;

		}



		protected HitTestValues HitTestOnMouseDown() {

			// ...
			var x = MousePosition.X;
			var y = MousePosition.Y;
			var r = new Rectangle(new Point(Parent.Left, Parent.Top), Parent.ClientSize);

			// ...
			var top = (y > r.Top) && (y < r.Top + SideWidth);
			var bottom = (y < r.Bottom) && (y > r.Bottom - SideWidth);
			var left = (x > r.Left) && (x < r.Left + SideWidth);
			var right = (x < r.Right + Size.Width) && (x > r.Right - SideWidth);

			// Corners
			if (top && left) return HitTestValues.TOPLEFT;
			if (top && right) return HitTestValues.TOPRIGHT;
			if (bottom && left) return HitTestValues.BOTTOMLEFT;
			if (bottom && right) return HitTestValues.BOTTOMRIGHT;

			// Sides
			if (left) return HitTestValues.LEFT;
			if (right) return HitTestValues.RIGHT;
			if (top) return HitTestValues.TOP;
			if (bottom) return HitTestValues.BOTTOM;

			// ...
			if (y > r.Top && y < r.Top + CaptionHeight) {
				return HitTestValues.CAPTION;
			}

			// None
			return HitTestValues.NOWHERE;

		}



		protected override CreateParams CreateParams {
			get {
				var parameters = base.CreateParams;
				parameters.ExStyle |= (int)ExtendedWindowStyles.WS_EX_TRANSPARENT;
				return parameters;
			}
		}



		protected override void OnParentChanged(EventArgs e) {

			// Unsubscribe
			if (Parent != null) Parent.SizeChanged -= OnParentSizeChanged;

			// ...
			base.OnParentChanged(e);

			// Subscribe
			if (Parent != null) {
				Parent.SizeChanged += OnParentSizeChanged;
				OnParentSizeChanged(null, EventArgs.Empty);
			}

		}



		public abstract void OnParentSizeChanged(object sender, EventArgs e);



		protected override void OnPaint(PaintEventArgs e) {
			
		}



		protected override void OnPaintBackground(PaintEventArgs pevent) {
			
		}




	}



}
