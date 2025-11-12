using System;
using System.Runtime.InteropServices;
using System.Security;

namespace cYo.Common.Presentation.Tao.TaoOpenGL;

public static class Glu
{
    public struct GLUnurbs
    {
        private IntPtr Data;
    }

    public struct GLUquadric
    {
        private IntPtr Data;
    }

    public struct GLUtesselator
    {
        private IntPtr Data;
    }

    public struct GLUnurbsObj
    {
        private IntPtr Data;
    }

    public struct GLUquadricObj
    {
        private IntPtr Data;
    }

    public struct GLUtesselatorObj
    {
        private IntPtr Data;
    }

    public struct GLUtriangulatorObj
    {
        private IntPtr Data;
    }

    public delegate void NurbsBeginCallback(int type);

    public delegate void NurbsBeginDataCallback(int type, [In] IntPtr[] userData);

    public delegate void NurbsColorCallback([In] float[] colorData);

    public delegate void NurbsColorDataCallback([In] float[] colorData, [In] IntPtr[] userData);

    public delegate void NurbsEndCallback();

    public delegate void NurbsEndDataCallback([In] IntPtr[] userData);

    public delegate void NurbsErrorCallback(int type);

    public delegate void NurbsNormalCallback([In] float[] normalData);

    public delegate void NurbsNormalDataCallback([In] float[] normalData, [In] IntPtr[] userData);

    public delegate void NurbsTexCoordCallback([In] float[] texCoord);

    public delegate void NurbsTexCoordDataCallback([In] float[] texCoord, [In] IntPtr[] userData);

    public delegate void NurbsVertexCallback([In] float[] vertexData);

    public delegate void NurbsVertexDataCallback([In] float[] vertexData, [In] IntPtr[] userData);

    public delegate void QuadricErrorCallback(int errorCode);

    public delegate void TessBeginCallback(int type);

    public delegate void TessBeginDataCallback(int type, [In] IntPtr polygonData);

    public delegate void TessCombineCallback([In] double[] coordinates, [In] IntPtr[] vertexData, [In] float[] weight, [Out] IntPtr[] outData);

    public delegate void TessCombineCallback1([In][MarshalAs(UnmanagedType.LPArray, SizeConst = 3)] double[] coordinates, [In][MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] double[] vertexData, [In][MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] float[] weight, [Out] double[] outData);

    public delegate void TessCombineDataCallback([In] double[] coordinates, [In] IntPtr[] vertexData, [In] float[] weight, [Out] IntPtr[] outData, [In] IntPtr polygonData);

    public delegate void TessEdgeFlagCallback(int flag);

    public delegate void TessEdgeFlagDataCallback(int flag, [In] IntPtr polygonData);

    public delegate void TessEndCallback();

    public delegate void TessEndDataCallback(IntPtr polygonData);

    public delegate void TessErrorCallback(int errorCode);

    public delegate void TessErrorDataCallback(int errorCode, [In] IntPtr polygonData);

    public delegate void TessVertexCallback([In] IntPtr vertexData);

    public delegate void TessVertexCallback1([In] double[] vertexData);

    public delegate void TessVertexDataCallback([In] IntPtr vertexData, [In] IntPtr polygonData);

    private const CallingConvention CALLING_CONVENTION = CallingConvention.Winapi;

    public const bool GLU_VERSION_1_1 = true;

    public const bool GLU_VERSION_1_2 = true;

    public const bool GLU_VERSION_1_3 = true;

    public const int GLU_INVALID_ENUM = 100900;

    public const int GLU_INVALID_VALUE = 100901;

    public const int GLU_OUT_OF_MEMORY = 100902;

    public const int GLU_INCOMPATIBLE_GL_VERSION = 100903;

    public const int GLU_INVALID_OPERATION = 100904;

    public const int GLU_VERSION = 100800;

    public const int GLU_EXTENSIONS = 100801;

    public const int GLU_TRUE = 1;

    public const int GLU_FALSE = 0;

    public const int GLU_SMOOTH = 100000;

    public const int GLU_FLAT = 100001;

    public const int GLU_NONE = 100002;

    public const int GLU_POINT = 100010;

    public const int GLU_LINE = 100011;

    public const int GLU_FILL = 100012;

    public const int GLU_SILHOUETTE = 100013;

    public const int GLU_OUTSIDE = 100020;

    public const int GLU_INSIDE = 100021;

    public const double GLU_TESS_MAX_COORD = 1E+150;

    public const int GLU_TESS_WINDING_RULE = 100140;

    public const int GLU_TESS_BOUNDARY_ONLY = 100141;

    public const int GLU_TESS_TOLERANCE = 100142;

    public const int GLU_TESS_WINDING_ODD = 100130;

    public const int GLU_TESS_WINDING_NONZERO = 100131;

    public const int GLU_TESS_WINDING_POSITIVE = 100132;

    public const int GLU_TESS_WINDING_NEGATIVE = 100133;

    public const int GLU_TESS_WINDING_ABS_GEQ_TWO = 100134;

    public const int GLU_TESS_BEGIN = 100100;

    public const int GLU_BEGIN = 100100;

    public const int GLU_TESS_VERTEX = 100101;

    public const int GLU_VERTEX = 100101;

    public const int GLU_TESS_END = 100102;

    public const int GLU_END = 100102;

    public const int GLU_TESS_ERROR = 100103;

    public const int GLU_TESS_EDGE_FLAG = 100104;

    public const int GLU_EDGE_FLAG = 100104;

    public const int GLU_TESS_COMBINE = 100105;

    public const int GLU_TESS_BEGIN_DATA = 100106;

    public const int GLU_TESS_VERTEX_DATA = 100107;

    public const int GLU_TESS_END_DATA = 100108;

    public const int GLU_TESS_ERROR_DATA = 100109;

    public const int GLU_TESS_EDGE_FLAG_DATA = 100110;

    public const int GLU_TESS_COMBINE_DATA = 100111;

    public const int GLU_TESS_ERROR1 = 100151;

    public const int GLU_TESS_ERROR2 = 100152;

    public const int GLU_TESS_ERROR3 = 100153;

    public const int GLU_TESS_ERROR4 = 100154;

    public const int GLU_TESS_ERROR5 = 100155;

    public const int GLU_TESS_ERROR6 = 100156;

    public const int GLU_TESS_ERROR7 = 100157;

    public const int GLU_TESS_ERROR8 = 100158;

    public const int GLU_TESS_MISSING_BEGIN_POLYGON = 100151;

    public const int GLU_TESS_MISSING_BEGIN_CONTOUR = 100152;

    public const int GLU_TESS_MISSING_END_POLYGON = 100153;

    public const int GLU_TESS_MISSING_END_CONTOUR = 100154;

    public const int GLU_TESS_COORD_TOO_LARGE = 100155;

    public const int GLU_TESS_NEED_COMBINE_CALLBACK = 100156;

    public const int GLU_AUTO_LOAD_MATRIX = 100200;

    public const int GLU_CULLING = 100201;

    public const int GLU_PARAMETRIC_TOLERANCE = 100202;

    public const int GLU_SAMPLING_TOLERANCE = 100203;

    public const int GLU_DISPLAY_MODE = 100204;

    public const int GLU_SAMPLING_METHOD = 100205;

    public const int GLU_U_STEP = 100206;

    public const int GLU_V_STEP = 100207;

    public const int GLU_NURBS_MODE = 100160;

    public const int GLU_NURBS_MODE_EXT = 100160;

    public const int GLU_NURBS_TESSELLATOR = 100161;

    public const int GLU_NURBS_TESSELLATOR_EXT = 100161;

    public const int GLU_NURBS_RENDERER = 100162;

    public const int GLU_NURBS_RENDERER_EXT = 100162;

    public const int GLU_OBJECT_PARAMETRIC_ERROR = 100208;

    public const int GLU_OBJECT_PARAMETRIC_ERROR_EXT = 100208;

    public const int GLU_OBJECT_PATH_LENGTH = 100209;

    public const int GLU_OBJECT_PATH_LENGTH_EXT = 100209;

    public const int GLU_PATH_LENGTH = 100215;

    public const int GLU_PARAMETRIC_ERROR = 100216;

    public const int GLU_DOMAIN_DISTANCE = 100217;

    public const int GLU_MAP1_TRIM_2 = 100210;

    public const int GLU_MAP1_TRIM_3 = 100211;

    public const int GLU_OUTLINE_POLYGON = 100240;

    public const int GLU_OUTLINE_PATCH = 100241;

    public const int GLU_NURBS_ERROR = 100103;

    public const int GLU_ERROR = 100103;

    public const int GLU_NURBS_BEGIN = 100164;

    public const int GLU_NURBS_BEGIN_EXT = 100164;

    public const int GLU_NURBS_VERTEX = 100165;

    public const int GLU_NURBS_VERTEX_EXT = 100165;

    public const int GLU_NURBS_NORMAL = 100166;

    public const int GLU_NURBS_NORMAL_EXT = 100166;

    public const int GLU_NURBS_COLOR = 100167;

    public const int GLU_NURBS_COLOR_EXT = 100167;

    public const int GLU_NURBS_TEXTURE_COORD = 100168;

    public const int GLU_NURBS_TEX_COORD_EXT = 100168;

    public const int GLU_NURBS_END = 100169;

    public const int GLU_NURBS_END_EXT = 100169;

    public const int GLU_NURBS_BEGIN_DATA = 100170;

    public const int GLU_NURBS_BEGIN_DATA_EXT = 100170;

    public const int GLU_NURBS_VERTEX_DATA = 100171;

    public const int GLU_NURBS_VERTEX_DATA_EXT = 100171;

    public const int GLU_NURBS_NORMAL_DATA = 100172;

    public const int GLU_NURBS_NORMAL_DATA_EXT = 100172;

    public const int GLU_NURBS_COLOR_DATA = 100173;

    public const int GLU_NURBS_COLOR_DATA_EXT = 100173;

    public const int GLU_NURBS_TEXTURE_COORD_DATA = 100174;

    public const int GLU_NURBS_TEX_COORD_DATA_EXT = 100174;

    public const int GLU_NURBS_END_DATA = 100175;

    public const int GLU_NURBS_END_DATA_EXT = 100175;

    public const int GLU_NURBS_ERROR1 = 100251;

    public const int GLU_NURBS_ERROR2 = 100252;

    public const int GLU_NURBS_ERROR3 = 100253;

    public const int GLU_NURBS_ERROR4 = 100254;

    public const int GLU_NURBS_ERROR5 = 100255;

    public const int GLU_NURBS_ERROR6 = 100256;

    public const int GLU_NURBS_ERROR7 = 100257;

    public const int GLU_NURBS_ERROR8 = 100258;

    public const int GLU_NURBS_ERROR9 = 100259;

    public const int GLU_NURBS_ERROR10 = 100260;

    public const int GLU_NURBS_ERROR11 = 100261;

    public const int GLU_NURBS_ERROR12 = 100262;

    public const int GLU_NURBS_ERROR13 = 100263;

    public const int GLU_NURBS_ERROR14 = 100264;

    public const int GLU_NURBS_ERROR15 = 100265;

    public const int GLU_NURBS_ERROR16 = 100266;

    public const int GLU_NURBS_ERROR17 = 100267;

    public const int GLU_NURBS_ERROR18 = 100268;

    public const int GLU_NURBS_ERROR19 = 100269;

    public const int GLU_NURBS_ERROR20 = 100270;

    public const int GLU_NURBS_ERROR21 = 100271;

    public const int GLU_NURBS_ERROR22 = 100272;

    public const int GLU_NURBS_ERROR23 = 100273;

    public const int GLU_NURBS_ERROR24 = 100274;

    public const int GLU_NURBS_ERROR25 = 100275;

    public const int GLU_NURBS_ERROR26 = 100276;

    public const int GLU_NURBS_ERROR27 = 100277;

    public const int GLU_NURBS_ERROR28 = 100278;

    public const int GLU_NURBS_ERROR29 = 100279;

    public const int GLU_NURBS_ERROR30 = 100280;

    public const int GLU_NURBS_ERROR31 = 100281;

    public const int GLU_NURBS_ERROR32 = 100282;

    public const int GLU_NURBS_ERROR33 = 100283;

    public const int GLU_NURBS_ERROR34 = 100284;

    public const int GLU_NURBS_ERROR35 = 100285;

    public const int GLU_NURBS_ERROR36 = 100286;

    public const int GLU_NURBS_ERROR37 = 100287;

    public const int GLU_CW = 100120;

    public const int GLU_CCW = 100121;

    public const int GLU_INTERIOR = 100122;

    public const int GLU_EXTERIOR = 100123;

    public const int GLU_UNKNOWN = 100124;

    public const int GLU_EXT_object_space_tess = 1;

    public const int GLU_EXT_nurbs_tessellator = 1;

    private static NurbsBeginCallback nurbsBeginCallback;

    private static NurbsBeginDataCallback nurbsBeginDataCallback;

    private static NurbsColorCallback nurbsColorCallback;

    private static NurbsColorDataCallback nurbsColorDataCallback;

    private static NurbsEndCallback nurbsEndCallback;

    private static NurbsEndDataCallback nurbsEndDataCallback;

    private static NurbsErrorCallback nurbsErrorCallback;

    private static NurbsNormalCallback nurbsNormalCallback;

    private static NurbsNormalDataCallback nurbsNormalDataCallback;

    private static NurbsTexCoordCallback nurbsTexCoordCallback;

    private static NurbsTexCoordDataCallback nurbsTexCoordDataCallback;

    private static NurbsVertexCallback nurbsVertexCallback;

    private static NurbsVertexDataCallback nurbsVertexDataCallback;

    private static QuadricErrorCallback quadricErrorCallback;

    private static TessBeginCallback tessBeginCallback;

    private static TessBeginDataCallback tessBeginDataCallback;

    private static TessCombineCallback tessCombineCallback;

    private static TessCombineCallback1 tessCombineCallback1;

    private static TessCombineDataCallback tessCombineDataCallback;

    private static TessEdgeFlagCallback tessEdgeFlagCallback;

    private static TessEdgeFlagDataCallback tessEdgeFlagDataCallback;

    private static TessEndCallback tessEndCallback;

    private static TessEndDataCallback tessEndDataCallback;

    private static TessErrorCallback tessErrorCallback;

    private static TessErrorDataCallback tessErrorDataCallback;

    private static TessVertexCallback tessVertexCallback;

    private static TessVertexCallback1 tessVertexCallback1;

    private static TessVertexDataCallback tessVertexDataCallback;

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsBeginCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsBeginDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsColorCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsColorDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsEndCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsEndDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsErrorCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsNormalCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsNormalDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsTexCoordCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsTexCoordDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsVertexCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluNurbsCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsVertexDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluQuadricCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluQuadricCallback([In] GLUquadric quad, int which, [In] QuadricErrorCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessBeginCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessBeginDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessCombineCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessCombineCallback1 func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessCombineDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessEdgeFlagCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessEdgeFlagDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessEndCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessEndDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessErrorCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessErrorDataCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessVertexCallback func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessVertexCallback1 func);

    [DllImport("glu32.dll", EntryPoint = "gluTessCallback")]
    [SuppressUnmanagedCodeSecurity]
    private static extern void __gluTessCallback([In] GLUtesselator tess, int which, [In] TessVertexDataCallback func);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluBeginCurve([In] GLUnurbs nurb);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluBeginPolygon([In] GLUtesselator tess);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluBeginSurface([In] GLUnurbs nurb);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluBeginTrim([In] GLUnurbs nurb);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] byte[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] byte[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] byte[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] double[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] double[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] double[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] short[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] short[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] short[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] int[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] int[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] int[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] float[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] float[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] float[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] ushort[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] ushort[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] ushort[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] uint[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] uint[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] uint[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] IntPtr data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public unsafe static extern int gluBuild1DMipmapLevels(int target, int internalFormat, int width, int format, int type, int level, int min, int max, [In] void* data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] byte[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] byte[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] byte[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] double[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] double[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] double[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] short[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] short[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] short[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] int[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] int[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] int[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] float[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] float[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] float[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] ushort[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] ushort[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] ushort[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] uint[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] uint[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] uint[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] IntPtr data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public unsafe static extern int gluBuild1DMipmaps(int target, int internalFormat, int width, int format, int type, [In] void* data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] byte[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] byte[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] byte[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] double[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] double[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] double[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] short[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] short[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] short[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] int[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] int[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] int[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] float[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] float[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] float[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] ushort[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] ushort[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] ushort[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] uint[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] uint[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] uint[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] IntPtr data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public unsafe static extern int gluBuild2DMipmapLevels(int target, int internalFormat, int width, int height, int format, int type, int level, int min, int max, [In] void* data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] byte[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] byte[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] byte[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] double[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] double[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] double[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] short[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] short[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] short[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] int[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] int[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] int[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] float[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] float[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] float[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] ushort[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] ushort[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] ushort[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] uint[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] uint[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] uint[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] IntPtr data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public unsafe static extern int gluBuild2DMipmaps(int target, int internalFormat, int width, int height, int format, int type, [In] void* data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] byte[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] byte[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] byte[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] double[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] double[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] double[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] short[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] short[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] short[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] int[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] int[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] int[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] float[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] float[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] float[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] ushort[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] ushort[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] ushort[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] uint[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] uint[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] uint[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] IntPtr data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public unsafe static extern int gluBuild3DMipmapLevels(int target, int internalFormat, int width, int height, int depth, int format, int type, int level, int min, int max, [In] void* data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] byte[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] byte[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] byte[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] double[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] double[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] double[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] short[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] short[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] short[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] int[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] int[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] int[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] float[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] float[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] float[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] ushort[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] ushort[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] ushort[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] uint[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] uint[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] uint[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] IntPtr data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public unsafe static extern int gluBuild3DMipmaps(int target, int internalFormat, int width, int height, int depth, int format, int type, [In] void* data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluCheckExtension(string extensionName, string extensionString);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluCylinder([In] GLUquadric quad, double baseRadius, double topRadius, double height, int slices, int stacks);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluDeleteNurbsRenderer([In] GLUnurbs nurb);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluDeleteQuadric([In] GLUquadric quad);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluDeleteTess([In] GLUtesselator tess);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluDisk([In] GLUquadric quad, double innerRadius, double outerRadius, int slices, int loops);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluEndCurve([In] GLUnurbs nurb);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluEndPolygon([In] GLUtesselator tess);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluEndSurface([In] GLUnurbs nurb);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluEndTrim([In] GLUnurbs nurb);

    [DllImport("glu32.dll", EntryPoint = "gluErrorString")]
    [SuppressUnmanagedCodeSecurity]
    private static extern IntPtr gluErrorStringUnsafe(int errorCode);

    public static string gluErrorString(int errorCode)
    {
        return Marshal.PtrToStringAnsi(gluErrorStringUnsafe(errorCode));
    }

    public static string gluErrorStringWIN(int errorCode)
    {
        return gluErrorUnicodeStringEXT(errorCode);
    }

    [DllImport("glu32.dll", EntryPoint = "gluErrorUnicodeStringEXT")]
    [SuppressUnmanagedCodeSecurity]
    private static extern IntPtr gluErrorUnicodeStringEXTUnsafe(int errorCode);

    public static string gluErrorUnicodeStringEXT(int errorCode)
    {
        return Marshal.PtrToStringAnsi(gluErrorUnicodeStringEXTUnsafe(errorCode));
    }

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluGetNurbsProperty([In] GLUnurbs nurb, int property, [Out] float[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluGetNurbsProperty([In] GLUnurbs nurb, int property, out float data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluGetNurbsProperty([In] GLUnurbs nurb, int property, [Out] IntPtr data);

    [DllImport("glu32.dll", EntryPoint = "gluGetString")]
    [SuppressUnmanagedCodeSecurity]
    private static extern IntPtr gluGetStringUnsafe(int name);

    public static string gluGetString(int name)
    {
        return Marshal.PtrToStringAnsi(gluGetStringUnsafe(name));
    }

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluGetTessProperty([In] GLUtesselator tess, int which, [Out] double[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluGetTessProperty([In] GLUtesselator tess, int which, out double data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluGetTessProperty([In] GLUtesselator tess, int which, [Out] IntPtr data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluLoadSamplingMatrices([In] GLUnurbs nurb, [In] float[] modelMatrix, [In] float[] projectionMatrix, [In] int[] viewport);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluLookAt(double eyeX, double eyeY, double eyeZ, double centerX, double centerY, double centerZ, double upX, double upY, double upZ);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern GLUnurbs gluNewNurbsRenderer();

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern GLUquadric gluNewQuadric();

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern GLUtesselator gluNewTess();

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNextContour([In] GLUtesselator tess, int type);

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsBeginCallback func)
    {
        nurbsBeginCallback = func;
        __gluNurbsCallback(nurb, which, nurbsBeginCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsBeginDataCallback func)
    {
        nurbsBeginDataCallback = func;
        __gluNurbsCallback(nurb, which, nurbsBeginDataCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsColorCallback func)
    {
        nurbsColorCallback = func;
        __gluNurbsCallback(nurb, which, nurbsColorCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsColorDataCallback func)
    {
        nurbsColorDataCallback = func;
        __gluNurbsCallback(nurb, which, nurbsColorDataCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsEndCallback func)
    {
        nurbsEndCallback = func;
        __gluNurbsCallback(nurb, which, nurbsEndCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsEndDataCallback func)
    {
        nurbsEndDataCallback = func;
        __gluNurbsCallback(nurb, which, nurbsEndDataCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsErrorCallback func)
    {
        nurbsErrorCallback = func;
        __gluNurbsCallback(nurb, which, nurbsErrorCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsNormalCallback func)
    {
        nurbsNormalCallback = func;
        __gluNurbsCallback(nurb, which, nurbsNormalCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsNormalDataCallback func)
    {
        nurbsNormalDataCallback = func;
        __gluNurbsCallback(nurb, which, nurbsNormalDataCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsTexCoordCallback func)
    {
        nurbsTexCoordCallback = func;
        __gluNurbsCallback(nurb, which, nurbsTexCoordCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsTexCoordDataCallback func)
    {
        nurbsTexCoordDataCallback = func;
        __gluNurbsCallback(nurb, which, nurbsTexCoordDataCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsVertexCallback func)
    {
        nurbsVertexCallback = func;
        __gluNurbsCallback(nurb, which, nurbsVertexCallback);
    }

    public static void gluNurbsCallback([In] GLUnurbs nurb, int which, [In] NurbsVertexDataCallback func)
    {
        nurbsVertexDataCallback = func;
        __gluNurbsCallback(nurb, which, nurbsVertexDataCallback);
    }

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] byte[] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] byte[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] byte[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] double[] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] double[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] double[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] short[] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] short[,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] short[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] int[] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] int[,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] int[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] float[] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] float[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] float[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] ushort[] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] ushort[,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] ushort[,,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] uint[] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] uint[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] uint[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] IntPtr userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public unsafe static extern void gluNurbsCallbackData([In] GLUnurbs nurb, [In] void* userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] byte[] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] byte[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] byte[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] double[] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] double[,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] double[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] short[] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] short[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] short[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] int[] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] int[,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] int[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] float[] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] float[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] float[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] ushort[] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] ushort[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] ushort[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] uint[] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] uint[,] userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] uint[,,] userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] IntPtr userData);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public unsafe static extern void gluNurbsCallbackDataEXT([In] GLUnurbs nurb, [In] void* userData);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCurve([In] GLUnurbs nurb, int knotCount, [In] float[] knots, int stride, [In] float[] control, int order, int type);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsCurve([In] GLUnurbs nurb, int knotCount, [In] float[] knots, int stride, [In] float[,] control, int order, int type);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsProperty([In] GLUnurbs nurb, int property, float val);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsSurface([In] GLUnurbs nurb, int sKnotCount, [In] float[] sKnots, int tKnotCount, [In] float[] tKnots, int sStride, int tStride, float[] control, int sOrder, int tOrder, int type);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsSurface([In] GLUnurbs nurb, int sKnotCount, [In] float[] sKnots, int tKnotCount, [In] float[] tKnots, int sStride, int tStride, float[,] control, int sOrder, int tOrder, int type);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluNurbsSurface([In] GLUnurbs nurb, int sKnotCount, [In] float[] sKnots, int tKnotCount, [In] float[] tKnots, int sStride, int tStride, float[,,] control, int sOrder, int tOrder, int type);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluOrtho2D(double left, double right, double bottom, double top);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluPartialDisk([In] GLUquadric quad, double innerRadius, double outerRadius, int slices, int loops, double startAngle, double sweepAngle);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluPerspective(double fovY, double aspectRatio, double zNear, double zFar);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluPickMatrix(double x, double y, double width, double height, [In] int[] viewport);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluProject(double objX, double objY, double objZ, [In] double[] modelMatrix, [In] double[] projectionMatrix, [In] int[] viewport, out double winX, out double winY, out double winZ);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluPwlCurve([In] GLUnurbs nurb, int count, [In] float[] data, int stride, int type);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluPwlCurve([In] GLUnurbs nurb, int count, [In] float[,] data, int stride, int type);

    public static void gluQuadricCallback([In] GLUquadric quad, int which, [In] QuadricErrorCallback func)
    {
        quadricErrorCallback = func;
        __gluQuadricCallback(quad, which, quadricErrorCallback);
    }

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluQuadricDrawStyle([In] GLUquadric quad, int drawStyle);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluQuadricNormals([In] GLUquadric quad, int normal);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluQuadricOrientation([In] GLUquadric quad, int orientation);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluQuadricTexture([In] GLUquadric quad, int texture);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluScaleImage(int format, int widthIn, int heightIn, int typeIn, [In] IntPtr dataIn, int widthOut, int heightOut, int typeOut, [Out] IntPtr dataOut);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluScaleImage(int format, int widthIn, int heightIn, int typeIn, [In] byte[] dataIn, int widthOut, int heightOut, int typeOut, [Out] byte[] dataOut);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluSphere([In] GLUquadric quad, double radius, int slices, int stacks);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginContour([In] GLUtesselator tess);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] byte[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] byte[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] byte[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] double[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] double[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] double[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] short[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] short[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] short[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] int[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] int[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] int[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] float[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] float[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] float[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] ushort[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] ushort[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] ushort[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] uint[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] uint[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] uint[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] IntPtr data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public unsafe static extern void gluTessBeginPolygon([In] GLUtesselator tess, [In] void* data);

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessBeginCallback func)
    {
        tessBeginCallback = func;
        __gluTessCallback(tess, which, tessBeginCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessBeginDataCallback func)
    {
        tessBeginDataCallback = func;
        __gluTessCallback(tess, which, tessBeginDataCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessCombineCallback func)
    {
        tessCombineCallback = func;
        __gluTessCallback(tess, which, tessCombineCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessCombineCallback1 func)
    {
        tessCombineCallback1 = func;
        __gluTessCallback(tess, which, tessCombineCallback1);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessCombineDataCallback func)
    {
        tessCombineDataCallback = func;
        __gluTessCallback(tess, which, tessCombineDataCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessEdgeFlagCallback func)
    {
        tessEdgeFlagCallback = func;
        __gluTessCallback(tess, which, tessEdgeFlagCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessEdgeFlagDataCallback func)
    {
        tessEdgeFlagDataCallback = func;
        __gluTessCallback(tess, which, tessEdgeFlagDataCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessEndCallback func)
    {
        tessEndCallback = func;
        __gluTessCallback(tess, which, tessEndCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessEndDataCallback func)
    {
        tessEndDataCallback = func;
        __gluTessCallback(tess, which, tessEndDataCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessErrorCallback func)
    {
        tessErrorCallback = func;
        __gluTessCallback(tess, which, tessErrorCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessErrorDataCallback func)
    {
        tessErrorDataCallback = func;
        __gluTessCallback(tess, which, tessErrorDataCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessVertexCallback func)
    {
        tessVertexCallback = func;
        __gluTessCallback(tess, which, tessVertexCallback);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessVertexCallback1 func)
    {
        tessVertexCallback1 = func;
        __gluTessCallback(tess, which, tessVertexCallback1);
    }

    public static void gluTessCallback([In] GLUtesselator tess, int which, [In] TessVertexDataCallback func)
    {
        tessVertexDataCallback = func;
        __gluTessCallback(tess, which, tessVertexDataCallback);
    }

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessEndContour([In] GLUtesselator tess);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessEndPolygon([In] GLUtesselator tess);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessNormal([In] GLUtesselator tess, double x, double y, double z);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessProperty([In] GLUtesselator tess, int which, double data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] byte[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] byte[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] byte[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] double[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] double[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] double[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] short[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] short[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] short[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] int[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] int[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] int[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] float[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] float[,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] float[,,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] ushort[] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] ushort[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] ushort[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] uint[] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] uint[,] data);

    [DllImport("glu32.dll")]
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] uint[,,] data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] IntPtr data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    [CLSCompliant(false)]
    public unsafe static extern void gluTessVertex([In] GLUtesselator tess, [In] double[] location, [In] void* data);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluUnProject(double winX, double winY, double winZ, [In] double[] modelMatrix, [In] double[] projectionMatrix, [In] int[] viewport, out double objX, out double objY, out double objZ);

    [DllImport("glu32.dll")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int gluUnProject4(double winX, double winY, double winZ, double clipW, [In] double[] modelMatrix, [In] double[] projectionMatrix, [In] int[] viewport, double nearVal, double farVal, out double objX, out double objY, out double objZ, out double objW);
}