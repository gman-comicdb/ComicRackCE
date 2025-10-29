using cYo.Common.ComponentModel;
using System;
using System.Drawing;

namespace cYo.Common.Drawing;

public class RendererGdiImage : RendererImage
{
    private readonly WeakReference<Bitmap> weakReference;

    public override bool IsValid => weakReference.IsAlive();

    public override Bitmap Bitmap => weakReference.GetData();

    public RendererGdiImage(Bitmap bitmap)
    {
        weakReference = new WeakReference<Bitmap>(bitmap);
    }
}
