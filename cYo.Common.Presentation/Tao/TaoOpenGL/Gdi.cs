using System;
using System.Runtime.InteropServices;
using System.Security;

namespace cYo.Common.Presentation.Tao.TaoOpenGL;

public static class Gdi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;

        public short dmSpecVersion;

        public short dmDriverVersion;

        public short dmSize;

        public short dmDriverExtra;

        public int dmFields;

        public short dmOrientation;

        public short dmPaperSize;

        public short dmPaperLength;

        public short dmPaperWidth;

        public short dmScale;

        public short dmCopies;

        public short dmDefaultSource;

        public short dmPrintQuality;

        public short dmColor;

        public short dmDuplex;

        public short dmYResolution;

        public short dmTTOption;

        public short dmCollate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;

        public short dmLogPixels;

        public int dmBitsPerPel;

        public int dmPelsWidth;

        public int dmPelsHeight;

        public int dmDisplayFlags;

        public int dmDisplayFrequency;

        public int dmICMMethod;

        public int dmICMIntent;

        public int dmMediaType;

        public int dmDitherType;

        public int dmReserved1;

        public int dmReserved2;

        public int dmPanningWidth;

        public int dmPanningHeight;
    }

    public struct GLYPHMETRICSFLOAT
    {
        public float gmfBlackBoxX;

        public float gmfBlackBoxY;

        public POINTFLOAT gmfptGlyphOrigin;

        public float gmfCellIncX;

        public float gmfCellIncY;
    }

    public struct PIXELFORMATDESCRIPTOR
    {
        public short nSize;

        public short nVersion;

        public int dwFlags;

        public byte iPixelType;

        public byte cColorBits;

        public byte cRedBits;

        public byte cRedShift;

        public byte cGreenBits;

        public byte cGreenShift;

        public byte cBlueBits;

        public byte cBlueShift;

        public byte cAlphaBits;

        public byte cAlphaShift;

        public byte cAccumBits;

        public byte cAccumRedBits;

        public byte cAccumGreenBits;

        public byte cAccumBlueBits;

        public byte cAccumAlphaBits;

        public byte cDepthBits;

        public byte cStencilBits;

        public byte cAuxBuffers;

        public byte iLayerType;

        public byte bReserved;

        public int dwLayerMask;

        public int dwVisibleMask;

        public int dwDamageMask;
    }

    public struct POINTFLOAT
    {
        public float X;

        public float Y;
    }

    public struct LAYERPLANEDESCRIPTOR
    {
        public short nSize;

        public short nVersion;

        public int dwFlags;

        public byte iPixelType;

        public byte cColorBits;

        public byte cRedBits;

        public byte cRedShift;

        public byte cGreenBits;

        public byte cGreenShift;

        public byte cBlueBits;

        public byte cBlueShift;

        public byte cAlphaBits;

        public byte cAlphaShift;

        public byte cAccumBits;

        public byte cAccumRedBits;

        public byte cAccumGreenBits;

        public byte cAccumBlueBits;

        public byte cAccumAlphaBits;

        public byte cDepthBits;

        public byte cStencilBits;

        public byte cAuxBuffers;

        public byte iLayerPlane;

        public byte bReserved;

        public int crTransparent;
    }

    [Flags]
    public enum DevCaps
    {
        DRIVERVERSION = 0,
        TECHNOLOGY = 2,
        HORZSIZE = 4,
        VERTSIZE = 6,
        HORZRES = 8,
        VERTRES = 0xA,
        BITSPIXEL = 0xC,
        PLANES = 0xE,
        NUMBRUSHES = 0x10,
        NUMPENS = 0x12,
        NUMMARKERS = 0x14,
        NUMFONTS = 0x16,
        NUMCOLORS = 0x18,
        PDEVICESIZE = 0x1A,
        CURVECAPS = 0x1C,
        LINECAPS = 0x1E,
        POLYGONALCAPS = 0x20,
        TEXTCAPS = 0x22,
        CLIPCAPS = 0x24,
        RASTERCAPS = 0x26,
        ASPECTX = 0x28,
        ASPECTY = 0x2A,
        ASPECTXY = 0x2C,
        SHADEBLENDCAPS = 0x2D,
        LOGPIXELSX = 0x58,
        LOGPIXELSY = 0x5A,
        SIZEPALETTE = 0x68,
        NUMRESERVED = 0x6A,
        COLORRES = 0x6C,
        PHYSICALWIDTH = 0x6E,
        PHYSICALHEIGHT = 0x6F,
        PHYSICALOFFSETX = 0x70,
        PHYSICALOFFSETY = 0x71,
        SCALINGFACTORX = 0x72,
        SCALINGFACTORY = 0x73,
        VREFRESH = 0x74,
        DESKTOPVERTRES = 0x75,
        DESKTOPHORZRES = 0x76,
        BLTALIGNMENT = 0x77
    }

    private const string GDI_NATIVE_LIBRARY = "gdi32.dll";

    private const CallingConvention CALLING_CONVENTION = CallingConvention.StdCall;

    public const int LPD_TYPE_RGBA = 0;

    public const int LPD_TYPE_COLORINDEX = 1;

    public const int LPD_DOUBLEBUFFER = 1;

    public const int LPD_STEREO = 2;

    public const int LPD_SUPPORT_GDI = 16;

    public const int LPD_SUPPORT_OPENGL = 32;

    public const int LPD_SHARE_DEPTH = 64;

    public const int LPD_SHARE_STENCIL = 128;

    public const int LPD_SHARE_ACCUM = 256;

    public const int LPD_SWAP_EXCHANGE = 512;

    public const int LPD_SWAP_COPY = 1024;

    public const int LPD_TRANSPARENT = 4096;

    public const int PFD_TYPE_RGBA = 0;

    public const int PFD_TYPE_COLORINDEX = 1;

    public const int PFD_MAIN_PLANE = 0;

    public const int PFD_OVERLAY_PLANE = 1;

    public const int PFD_UNDERLAY_PLANE = -1;

    public const int PFD_DOUBLEBUFFER = 1;

    public const int PFD_STEREO = 2;

    public const int PFD_DRAW_TO_WINDOW = 4;

    public const int PFD_DRAW_TO_BITMAP = 8;

    public const int PFD_SUPPORT_GDI = 16;

    public const int PFD_SUPPORT_OPENGL = 32;

    public const int PFD_GENERIC_FORMAT = 64;

    public const int PFD_NEED_PALETTE = 128;

    public const int PFD_NEED_SYSTEM_PALETTE = 256;

    public const int PFD_SWAP_EXCHANGE = 512;

    public const int PFD_SWAP_COPY = 1024;

    public const int PFD_SWAP_LAYER_BUFFERS = 2048;

    public const int PFD_GENERIC_ACCELERATED = 4096;

    public const int PFD_SUPPORT_DIRECTDRAW = 8192;

    public const int PFD_DEPTH_DONTCARE = 536870912;

    public const int PFD_DOUBLEBUFFER_DONTCARE = 1073741824;

    public const int PFD_STEREO_DONTCARE = int.MinValue;

    public const int DM_BITSPERPEL = 262144;

    public const int DM_PELSWIDTH = 524288;

    public const int DM_PELSHEIGHT = 1048576;

    public const int DM_DISPLAYFLAGS = 2097152;

    public const int DM_DISPLAYFREQUENCY = 4194304;

    public const int OUT_TT_PRECIS = 4;

    public const int CLIP_DEFAULT_PRECIS = 0;

    public const int DEFAULT_QUALITY = 0;

    public const int DRAFT_QUALITY = 1;

    public const int PROOF_QUALITY = 2;

    public const int NONANTIALIASED_QUALITY = 3;

    public const int ANTIALIASED_QUALITY = 4;

    public const int CLEARTYPE_QUALITY = 5;

    public const int CLEARTYPE_NATURAL_QUALITY = 6;

    public const int DEFAULT_PITCH = 0;

    public const int FIXED_PITCH = 1;

    public const int VARIABLE_PITCH = 2;

    public const int MONO_FONT = 8;

    public const int ANSI_CHARSET = 0;

    public const int DEFAULT_CHARSET = 1;

    public const int SYMBOL_CHARSET = 2;

    public const int SHIFTJIS_CHARSET = 128;

    public const int FF_DONTCARE = 0;

    public const int FW_BOLD = 700;

    [DllImport("gdi32.dll", EntryPoint = "SetPixelFormat", SetLastError = true)]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool _SetPixelFormat(IntPtr deviceContext, int pixelFormat, ref PIXELFORMATDESCRIPTOR pixelFormatDescriptor);

    [DllImport("gdi32.dll", SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int ChoosePixelFormat(IntPtr deviceContext, ref PIXELFORMATDESCRIPTOR pixelFormatDescriptor);

    public static bool SetPixelFormat(IntPtr deviceContext, int pixelFormat, ref PIXELFORMATDESCRIPTOR pixelFormatDescriptor)
    {
        Kernel.LoadLibrary("opengl32.dll");
        return _SetPixelFormat(deviceContext, pixelFormat, ref pixelFormatDescriptor);
    }

    [DllImport("gdi32.dll", SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool SwapBuffers(IntPtr deviceContext);

    [DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SwapBuffers")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int SwapBuffersFast([In] IntPtr deviceContext);

    [DllImport("gdi32.dll", SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern IntPtr CreateFont(int height, int width, int escapement, int orientation, int weight, bool italic, bool underline, bool strikeOut, int charSet, int outputPrecision, int clipPrecision, int quality, int pitchAndFamily, string typeFace);

    [DllImport("gdi32.dll", SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool DeleteObject(IntPtr objectHandle);

    [DllImport("gdi32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern IntPtr SelectObject(IntPtr deviceContext, IntPtr objectHandle);

    [DllImport("gdi32.dll", SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int GetDeviceCaps(IntPtr deviceContext, int nIndex);
}