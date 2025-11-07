using System;
using System.Drawing;
using System.Windows.Forms;
using cYo.Common.Drawing;
using Win32Interop = cYo.Common.Win32.Win32;

namespace cYo.Common.Windows.Forms
{
	public partial class LayeredForm : FormEx
	{
		private Bitmap surface;

		private int alpha;

		public Bitmap Surface
		{
			get
			{
				return surface;
			}
			set
			{
				if (surface != value)
				{
					surface = value;
					base.Width = surface.Width;
					base.Height = surface.Height;
					UpdateSurface();
				}
			}
		}

		public int Alpha
		{
			get
			{
				return alpha;
			}
			set
			{
				if (alpha != value)
				{
					alpha = value;
					Invalidate();
				}
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				if (!base.DesignMode)
				{
					createParams.ExStyle |= Win32Interop.WS_EX_LAYERED;
				}
				return createParams;
			}
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
			if (!base.DesignMode)
			{
				UpdateSurface();
			}
		}

		private void UpdateSurface()
		{
			try
			{
				if (this.InvokeIfRequired(UpdateSurface))
				{
					return;
				}
				using (Bitmap bitmap = new Bitmap(base.Width, base.Height))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						graphics.Clear(Color.Transparent);
						if (surface != null)
						{
							graphics.DrawImage(surface, surface.Size.ToRectangle());
						}
						OnPaint(new PaintEventArgs(graphics, bitmap.Size.ToRectangle()));
					}
                    Win32Interop.SelectBitmap(this, bitmap, alpha);
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
