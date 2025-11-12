using cYo.Common.Presentation.Tao.TaoOpenGL;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace cYo.Common.Presentation.Tao.TaoOpenGL;

public static class Wgl
{
    internal static class Imports
    {
        internal static SortedList<string, MethodInfo> FunctionMap;

        [DllImport("opengl32.dll", EntryPoint = "wglCreateContext", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr CreateContext(IntPtr hDc);

        [DllImport("opengl32.dll", EntryPoint = "wglDeleteContext", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool DeleteContext(IntPtr oldContext);

        [DllImport("opengl32.dll", EntryPoint = "wglGetCurrentContext", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr GetCurrentContext();

        [DllImport("opengl32.dll", EntryPoint = "wglMakeCurrent", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool MakeCurrent(IntPtr hDc, IntPtr newContext);

        [DllImport("opengl32.dll", EntryPoint = "wglCopyContext", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool CopyContext(IntPtr hglrcSrc, IntPtr hglrcDst, uint mask);

        [DllImport("opengl32.dll", EntryPoint = "wglChoosePixelFormat", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal unsafe static extern int ChoosePixelFormat(IntPtr hDc, Gdi.PIXELFORMATDESCRIPTOR* pPfd);

        [DllImport("opengl32.dll", EntryPoint = "wglDescribePixelFormat", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal unsafe static extern int DescribePixelFormat(IntPtr hdc, int ipfd, uint cjpfd, Gdi.PIXELFORMATDESCRIPTOR* ppfd);

        [DllImport("opengl32.dll", EntryPoint = "wglGetCurrentDC", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr GetCurrentDC();

        [DllImport("opengl32.dll", EntryPoint = "wglGetDefaultProcAddress", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr GetDefaultProcAddress(string lpszProc);

        [DllImport("opengl32.dll", EntryPoint = "wglGetProcAddress", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr GetProcAddress(string lpszProc);

        [DllImport("opengl32.dll", EntryPoint = "wglGetPixelFormat", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern int GetPixelFormat(IntPtr hdc);

        [DllImport("opengl32.dll", EntryPoint = "wglSetPixelFormat", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal unsafe static extern bool SetPixelFormat(IntPtr hdc, int ipfd, Gdi.PIXELFORMATDESCRIPTOR* ppfd);

        [DllImport("opengl32.dll", EntryPoint = "wglSwapBuffers", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool SwapBuffers(IntPtr hdc);

        [DllImport("opengl32.dll", EntryPoint = "wglShareLists", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool ShareLists(IntPtr hrcSrvShare, IntPtr hrcSrvSource);

        [DllImport("opengl32.dll", EntryPoint = "wglCreateLayerContext", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr CreateLayerContext(IntPtr hDc, int level);

        [DllImport("opengl32.dll", EntryPoint = "wglDescribeLayerPlane", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal unsafe static extern bool DescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane, uint nBytes, Gdi.LAYERPLANEDESCRIPTOR* plpd);

        [DllImport("opengl32.dll", EntryPoint = "wglSetLayerPaletteEntries", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal unsafe static extern int SetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, int* pcr);

        [DllImport("opengl32.dll", EntryPoint = "wglGetLayerPaletteEntries", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal unsafe static extern int GetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, int* pcr);

        [DllImport("opengl32.dll", EntryPoint = "wglRealizeLayerPalette", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool RealizeLayerPalette(IntPtr hdc, int iLayerPlane, bool bRealize);

        [DllImport("opengl32.dll", EntryPoint = "wglSwapLayerBuffers", ExactSpelling = true)]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool SwapLayerBuffers(IntPtr hdc, uint fuFlags);

        [DllImport("opengl32.dll", CharSet = CharSet.Auto, EntryPoint = "wglUseFontBitmapsA")]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool UseFontBitmapsA(IntPtr hDC, int first, int count, int listBase);

        [DllImport("opengl32.dll", CharSet = CharSet.Auto, EntryPoint = "wglUseFontBitmapsW")]
        [SuppressUnmanagedCodeSecurity]
        internal static extern bool UseFontBitmapsW(IntPtr hDC, int first, int count, int listBase);

        [DllImport("opengl32.dll", CharSet = CharSet.Auto, EntryPoint = "wglUseFontOutlinesA")]
        [SuppressUnmanagedCodeSecurity]
        internal unsafe static extern bool UseFontOutlinesA(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, Gdi.GLYPHMETRICSFLOAT* glyphMetrics);

        [DllImport("opengl32.dll", CharSet = CharSet.Auto, EntryPoint = "wglUseFontOutlinesW")]
        [SuppressUnmanagedCodeSecurity]
        internal unsafe static extern bool UseFontOutlinesW(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, Gdi.GLYPHMETRICSFLOAT* glyphMetrics);

        static Imports()
        {
            MethodInfo[] methods = importsClass.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            FunctionMap = new SortedList<string, MethodInfo>(methods.Length);
            MethodInfo[] array = methods;
            foreach (MethodInfo methodInfo in array)
            {
                FunctionMap.Add(methodInfo.Name, methodInfo);
            }
        }
    }

    internal static class Delegates
    {
        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr CreateContext(IntPtr hDc);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool DeleteContext(IntPtr oldContext);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetCurrentContext();

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool MakeCurrent(IntPtr hDc, IntPtr newContext);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool CopyContext(IntPtr hglrcSrc, IntPtr hglrcDst, uint mask);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate int ChoosePixelFormat(IntPtr hDc, Gdi.PIXELFORMATDESCRIPTOR* pPfd);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate int DescribePixelFormat(IntPtr hdc, int ipfd, uint cjpfd, Gdi.PIXELFORMATDESCRIPTOR* ppfd);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetCurrentDC();

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetDefaultProcAddress(string lpszProc);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetProcAddress(string lpszProc);

        [SuppressUnmanagedCodeSecurity]
        internal delegate int GetPixelFormat(IntPtr hdc);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool SetPixelFormat(IntPtr hdc, int ipfd, Gdi.PIXELFORMATDESCRIPTOR* ppfd);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool SwapBuffers(IntPtr hdc);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool ShareLists(IntPtr hrcSrvShare, IntPtr hrcSrvSource);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr CreateLayerContext(IntPtr hDc, int level);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool DescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane, uint nBytes, Gdi.LAYERPLANEDESCRIPTOR* plpd);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate int SetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, int* pcr);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate int GetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, int* pcr);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool RealizeLayerPalette(IntPtr hdc, int iLayerPlane, bool bRealize);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool SwapLayerBuffers(IntPtr hdc, uint fuFlags);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool UseFontBitmapsA(IntPtr hDC, int first, int count, int listBase);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool UseFontBitmapsW(IntPtr hDC, int first, int count, int listBase);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool UseFontOutlinesA(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, Gdi.GLYPHMETRICSFLOAT* glyphMetrics);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool UseFontOutlinesW(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, Gdi.GLYPHMETRICSFLOAT* glyphMetrics);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr CreateBufferRegionARB(IntPtr hDC, int iLayerPlane, uint uType);

        [SuppressUnmanagedCodeSecurity]
        internal delegate void DeleteBufferRegionARB(IntPtr hRegion);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool SaveBufferRegionARB(IntPtr hRegion, int x, int y, int width, int height);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool RestoreBufferRegionARB(IntPtr hRegion, int x, int y, int width, int height, int xSrc, int ySrc);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetExtensionsStringARB(IntPtr hdc);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int* piAttributes, [Out] int* piValues);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int* piAttributes, [Out] float* pfValues);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool ChoosePixelFormatARB(IntPtr hdc, int* piAttribIList, float* pfAttribFList, uint nMaxFormats, [Out] int* piFormats, [Out] uint* nNumFormats);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool MakeContextCurrentARB(IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetCurrentReadDCARB();

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate IntPtr CreatePbufferARB(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, int* piAttribList);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetPbufferDCARB(IntPtr hPbuffer);

        [SuppressUnmanagedCodeSecurity]
        internal delegate int ReleasePbufferDCARB(IntPtr hPbuffer, IntPtr hDC);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool DestroyPbufferARB(IntPtr hPbuffer);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool QueryPbufferARB(IntPtr hPbuffer, int iAttribute, [Out] int* piValue);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool BindTexImageARB(IntPtr hPbuffer, int iBuffer);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool ReleaseTexImageARB(IntPtr hPbuffer, int iBuffer);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool SetPbufferAttribARB(IntPtr hPbuffer, int* piAttribList);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool CreateDisplayColorTableEXT(ushort id);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool LoadDisplayColorTableEXT(ushort* table, uint length);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool BindDisplayColorTableEXT(ushort id);

        [SuppressUnmanagedCodeSecurity]
        internal delegate void DestroyDisplayColorTableEXT(ushort id);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetExtensionsStringEXT();

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool MakeContextCurrentEXT(IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetCurrentReadDCEXT();

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate IntPtr CreatePbufferEXT(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, int* piAttribList);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr GetPbufferDCEXT(IntPtr hPbuffer);

        [SuppressUnmanagedCodeSecurity]
        internal delegate int ReleasePbufferDCEXT(IntPtr hPbuffer, IntPtr hDC);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool DestroyPbufferEXT(IntPtr hPbuffer);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool QueryPbufferEXT(IntPtr hPbuffer, int iAttribute, [Out] int* piValue);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetPixelFormatAttribivEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, [Out] int* piAttributes, [Out] int* piValues);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetPixelFormatAttribfvEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, [Out] int* piAttributes, [Out] float* pfValues);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool ChoosePixelFormatEXT(IntPtr hdc, int* piAttribIList, float* pfAttribFList, uint nMaxFormats, [Out] int* piFormats, [Out] uint* nNumFormats);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool SwapIntervalEXT(int interval);

        [SuppressUnmanagedCodeSecurity]
        internal delegate int GetSwapIntervalEXT();

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr AllocateMemoryNV(int size, float readfreq, float writefreq, float priority);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate void FreeMemoryNV([Out] IntPtr* pointer);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetSyncValuesOML(IntPtr hdc, [Out] long* ust, [Out] long* msc, [Out] long* sbc);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetMscRateOML(IntPtr hdc, [Out] int* numerator, [Out] int* denominator);

        [SuppressUnmanagedCodeSecurity]
        internal delegate long SwapBuffersMscOML(IntPtr hdc, long target_msc, long divisor, long remainder);

        [SuppressUnmanagedCodeSecurity]
        internal delegate long SwapLayerBuffersMscOML(IntPtr hdc, int fuPlanes, long target_msc, long divisor, long remainder);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool WaitForMscOML(IntPtr hdc, long target_msc, long divisor, long remainder, [Out] long* ust, [Out] long* msc, [Out] long* sbc);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool WaitForSbcOML(IntPtr hdc, long target_sbc, [Out] long* ust, [Out] long* msc, [Out] long* sbc);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, [Out] int* piValue);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool SetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, int* piValue);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetGammaTableParametersI3D(IntPtr hDC, int iAttribute, [Out] int* piValue);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool SetGammaTableParametersI3D(IntPtr hDC, int iAttribute, int* piValue);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetGammaTableI3D(IntPtr hDC, int iEntries, [Out] ushort* puRed, [Out] ushort* puGreen, [Out] ushort* puBlue);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool SetGammaTableI3D(IntPtr hDC, int iEntries, ushort* puRed, ushort* puGreen, ushort* puBlue);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool EnableGenlockI3D(IntPtr hDC);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool DisableGenlockI3D(IntPtr hDC);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool IsEnabledGenlockI3D(IntPtr hDC, [Out] bool* pFlag);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool GenlockSourceI3D(IntPtr hDC, uint uSource);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetGenlockSourceI3D(IntPtr hDC, [Out] uint* uSource);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool GenlockSourceEdgeI3D(IntPtr hDC, uint uEdge);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetGenlockSourceEdgeI3D(IntPtr hDC, [Out] uint* uEdge);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool GenlockSampleRateI3D(IntPtr hDC, uint uRate);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetGenlockSampleRateI3D(IntPtr hDC, [Out] uint* uRate);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool GenlockSourceDelayI3D(IntPtr hDC, uint uDelay);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetGenlockSourceDelayI3D(IntPtr hDC, [Out] uint* uDelay);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool QueryGenlockMaxSourceDelayI3D(IntPtr hDC, [Out] uint* uMaxLineDelay, [Out] uint* uMaxPixelDelay);

        [SuppressUnmanagedCodeSecurity]
        internal delegate IntPtr CreateImageBufferI3D(IntPtr hDC, int dwSize, uint uFlags);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool DestroyImageBufferI3D(IntPtr hDC, IntPtr pAddress);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool AssociateImageBufferEventsI3D(IntPtr hDC, IntPtr* pEvent, IntPtr pAddress, int* pSize, uint count);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool ReleaseImageBufferEventsI3D(IntPtr hDC, IntPtr pAddress, uint count);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool EnableFrameLockI3D();

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool DisableFrameLockI3D();

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool IsEnabledFrameLockI3D([Out] bool* pFlag);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool QueryFrameLockMasterI3D([Out] bool* pFlag);

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool GetFrameUsageI3D([Out] float* pUsage);

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool BeginFrameTrackingI3D();

        [SuppressUnmanagedCodeSecurity]
        internal delegate bool EndFrameTrackingI3D();

        [SuppressUnmanagedCodeSecurity]
        internal unsafe delegate bool QueryFrameTrackingI3D([Out] int* pFrameCount, [Out] int* pMissedFrames, [Out] float* pLastMissedUsage);

        internal static CreateContext wglCreateContext;

        internal static DeleteContext wglDeleteContext;

        internal static GetCurrentContext wglGetCurrentContext;

        internal static MakeCurrent wglMakeCurrent;

        internal static CopyContext wglCopyContext;

        internal static ChoosePixelFormat wglChoosePixelFormat;

        internal static DescribePixelFormat wglDescribePixelFormat;

        internal static GetCurrentDC wglGetCurrentDC;

        internal static GetDefaultProcAddress wglGetDefaultProcAddress;

        internal static GetProcAddress wglGetProcAddress;

        internal static GetPixelFormat wglGetPixelFormat;

        internal static SetPixelFormat wglSetPixelFormat;

        internal static SwapBuffers wglSwapBuffers;

        internal static ShareLists wglShareLists;

        internal static CreateLayerContext wglCreateLayerContext;

        internal static DescribeLayerPlane wglDescribeLayerPlane;

        internal static SetLayerPaletteEntries wglSetLayerPaletteEntries;

        internal static GetLayerPaletteEntries wglGetLayerPaletteEntries;

        internal static RealizeLayerPalette wglRealizeLayerPalette;

        internal static SwapLayerBuffers wglSwapLayerBuffers;

        internal static UseFontBitmapsA wglUseFontBitmapsA;

        internal static UseFontBitmapsW wglUseFontBitmapsW;

        internal static UseFontOutlinesA wglUseFontOutlinesA;

        internal static UseFontOutlinesW wglUseFontOutlinesW;

        internal static CreateBufferRegionARB wglCreateBufferRegionARB;

        internal static DeleteBufferRegionARB wglDeleteBufferRegionARB;

        internal static SaveBufferRegionARB wglSaveBufferRegionARB;

        internal static RestoreBufferRegionARB wglRestoreBufferRegionARB;

        internal static GetExtensionsStringARB wglGetExtensionsStringARB;

        internal static GetPixelFormatAttribivARB wglGetPixelFormatAttribivARB;

        internal static GetPixelFormatAttribfvARB wglGetPixelFormatAttribfvARB;

        internal static ChoosePixelFormatARB wglChoosePixelFormatARB;

        internal static MakeContextCurrentARB wglMakeContextCurrentARB;

        internal static GetCurrentReadDCARB wglGetCurrentReadDCARB;

        internal static CreatePbufferARB wglCreatePbufferARB;

        internal static GetPbufferDCARB wglGetPbufferDCARB;

        internal static ReleasePbufferDCARB wglReleasePbufferDCARB;

        internal static DestroyPbufferARB wglDestroyPbufferARB;

        internal static QueryPbufferARB wglQueryPbufferARB;

        internal static BindTexImageARB wglBindTexImageARB;

        internal static ReleaseTexImageARB wglReleaseTexImageARB;

        internal static SetPbufferAttribARB wglSetPbufferAttribARB;

        internal static CreateDisplayColorTableEXT wglCreateDisplayColorTableEXT;

        internal static LoadDisplayColorTableEXT wglLoadDisplayColorTableEXT;

        internal static BindDisplayColorTableEXT wglBindDisplayColorTableEXT;

        internal static DestroyDisplayColorTableEXT wglDestroyDisplayColorTableEXT;

        internal static GetExtensionsStringEXT wglGetExtensionsStringEXT;

        internal static MakeContextCurrentEXT wglMakeContextCurrentEXT;

        internal static GetCurrentReadDCEXT wglGetCurrentReadDCEXT;

        internal static CreatePbufferEXT wglCreatePbufferEXT;

        internal static GetPbufferDCEXT wglGetPbufferDCEXT;

        internal static ReleasePbufferDCEXT wglReleasePbufferDCEXT;

        internal static DestroyPbufferEXT wglDestroyPbufferEXT;

        internal static QueryPbufferEXT wglQueryPbufferEXT;

        internal static GetPixelFormatAttribivEXT wglGetPixelFormatAttribivEXT;

        internal static GetPixelFormatAttribfvEXT wglGetPixelFormatAttribfvEXT;

        internal static ChoosePixelFormatEXT wglChoosePixelFormatEXT;

        internal static SwapIntervalEXT wglSwapIntervalEXT;

        internal static GetSwapIntervalEXT wglGetSwapIntervalEXT;

        internal static AllocateMemoryNV wglAllocateMemoryNV;

        internal static FreeMemoryNV wglFreeMemoryNV;

        internal static GetSyncValuesOML wglGetSyncValuesOML;

        internal static GetMscRateOML wglGetMscRateOML;

        internal static SwapBuffersMscOML wglSwapBuffersMscOML;

        internal static SwapLayerBuffersMscOML wglSwapLayerBuffersMscOML;

        internal static WaitForMscOML wglWaitForMscOML;

        internal static WaitForSbcOML wglWaitForSbcOML;

        internal static GetDigitalVideoParametersI3D wglGetDigitalVideoParametersI3D;

        internal static SetDigitalVideoParametersI3D wglSetDigitalVideoParametersI3D;

        internal static GetGammaTableParametersI3D wglGetGammaTableParametersI3D;

        internal static SetGammaTableParametersI3D wglSetGammaTableParametersI3D;

        internal static GetGammaTableI3D wglGetGammaTableI3D;

        internal static SetGammaTableI3D wglSetGammaTableI3D;

        internal static EnableGenlockI3D wglEnableGenlockI3D;

        internal static DisableGenlockI3D wglDisableGenlockI3D;

        internal static IsEnabledGenlockI3D wglIsEnabledGenlockI3D;

        internal static GenlockSourceI3D wglGenlockSourceI3D;

        internal static GetGenlockSourceI3D wglGetGenlockSourceI3D;

        internal static GenlockSourceEdgeI3D wglGenlockSourceEdgeI3D;

        internal static GetGenlockSourceEdgeI3D wglGetGenlockSourceEdgeI3D;

        internal static GenlockSampleRateI3D wglGenlockSampleRateI3D;

        internal static GetGenlockSampleRateI3D wglGetGenlockSampleRateI3D;

        internal static GenlockSourceDelayI3D wglGenlockSourceDelayI3D;

        internal static GetGenlockSourceDelayI3D wglGetGenlockSourceDelayI3D;

        internal static QueryGenlockMaxSourceDelayI3D wglQueryGenlockMaxSourceDelayI3D;

        internal static CreateImageBufferI3D wglCreateImageBufferI3D;

        internal static DestroyImageBufferI3D wglDestroyImageBufferI3D;

        internal static AssociateImageBufferEventsI3D wglAssociateImageBufferEventsI3D;

        internal static ReleaseImageBufferEventsI3D wglReleaseImageBufferEventsI3D;

        internal static EnableFrameLockI3D wglEnableFrameLockI3D;

        internal static DisableFrameLockI3D wglDisableFrameLockI3D;

        internal static IsEnabledFrameLockI3D wglIsEnabledFrameLockI3D;

        internal static QueryFrameLockMasterI3D wglQueryFrameLockMasterI3D;

        internal static GetFrameUsageI3D wglGetFrameUsageI3D;

        internal static BeginFrameTrackingI3D wglBeginFrameTrackingI3D;

        internal static EndFrameTrackingI3D wglEndFrameTrackingI3D;

        internal static QueryFrameTrackingI3D wglQueryFrameTrackingI3D;
    }

    public const int WGL_FONT_POLYGONS = 1;

    public const int WGL_SWAP_METHOD_EXT = 8199;

    public const int WGL_MIPMAP_TEXTURE_ARB = 8308;

    public const int WGL_NO_ACCELERATION_ARB = 8229;

    public const int WGL_SAMPLE_BUFFERS_EXT = 8257;

    public const int WGL_BIND_TO_TEXTURE_RGB_ARB = 8304;

    public const int WGL_AUX_BUFFERS_EXT = 8228;

    public const int WGL_AUX0_ARB = 8327;

    public const int WGL_GENLOCK_SOURCE_EDGE_FALLING_I3D = 8266;

    public const int WGL_ACCUM_ALPHA_BITS_EXT = 8225;

    public const int WGL_BIND_TO_TEXTURE_RECTANGLE_FLOAT_R_NV = 8369;

    public const int WGL_IMAGE_BUFFER_LOCK_I3D = 2;

    public const int WGL_BLUE_SHIFT_EXT = 8218;

    public const int WGL_NEED_SYSTEM_PALETTE_EXT = 8197;

    public const int WGL_SHARE_ACCUM_EXT = 8206;

    public const int WGL_TEXTURE_CUBE_MAP_NEGATIVE_X_ARB = 8318;

    public const int WGL_DRAW_TO_PBUFFER_ARB = 8237;

    public const int WGL_DIGITAL_VIDEO_GAMMA_CORRECTED_I3D = 8275;

    public const int WGL_TEXTURE_CUBE_MAP_ARB = 8312;

    public const int WGL_TYPE_RGBA_FLOAT_ARB = 8608;

    public const int WGL_FULL_ACCELERATION_EXT = 8231;

    public const int WGL_ACCUM_GREEN_BITS_EXT = 8223;

    public const int WGL_BACK_COLOR_BUFFER_BIT_ARB = 2;

    public const int WGL_ACCUM_GREEN_BITS_ARB = 8223;

    public const int WGL_MAX_PBUFFER_WIDTH_EXT = 8239;

    public const int WGL_ACCUM_RED_BITS_EXT = 8222;

    public const int WGL_AUX9_ARB = 8336;

    public const int WGL_TRANSPARENT_EXT = 8202;

    public const int WGL_ACCUM_ALPHA_BITS_ARB = 8225;

    public const int WGL_GENERIC_ACCELERATION_EXT = 8230;

    public const int WGL_AUX2_ARB = 8329;

    public const int WGL_PIXEL_TYPE_EXT = 8211;

    public const int WGL_NUMBER_PIXEL_FORMATS_EXT = 8192;

    public const int WGL_ACCELERATION_ARB = 8195;

    public const int WGL_IMAGE_BUFFER_MIN_ACCESS_I3D = 1;

    public const int WGL_DOUBLE_BUFFER_EXT = 8209;

    public const int WGL_NEED_PALETTE_EXT = 8196;

    public const int WGL_TEXTURE_FLOAT_RG_NV = 8374;

    public const int WGL_DEPTH_FLOAT_EXT = 8256;

    public const int WGL_BLUE_BITS_ARB = 8217;

    public const int WGL_ACCUM_BITS_EXT = 8221;

    public const int WGL_MAX_PBUFFER_WIDTH_ARB = 8239;

    public const int WGL_TEXTURE_CUBE_MAP_NEGATIVE_Y_ARB = 8320;

    public const int WGL_NUMBER_OVERLAYS_ARB = 8200;

    public const int WGL_TEXTURE_RGB_ARB = 8309;

    public const int WGL_SUPPORT_GDI_EXT = 8207;

    public const int WGL_PBUFFER_HEIGHT_EXT = 8245;

    public const int WGL_BIND_TO_TEXTURE_RECTANGLE_DEPTH_NV = 8356;

    public const int WGL_SAMPLE_BUFFERS_ARB = 8257;

    public const int WGL_TRANSPARENT_ALPHA_VALUE_ARB = 8250;

    public const int WGL_GREEN_SHIFT_ARB = 8216;

    public const int WGL_BIND_TO_TEXTURE_RECTANGLE_RGBA_NV = 8353;

    public const int WGL_TYPE_COLORINDEX_EXT = 8236;

    public const int WGL_SHARE_STENCIL_ARB = 8205;

    public const int WGL_GENLOCK_SOURCE_DIGITAL_SYNC_I3D = 8264;

    public const int WGL_SHARE_DEPTH_EXT = 8204;

    public const int WGL_NO_ACCELERATION_EXT = 8229;

    public const int WGL_BLUE_SHIFT_ARB = 8218;

    public const int WGL_SUPPORT_GDI_ARB = 8207;

    public const int WGL_NO_TEXTURE_ARB = 8311;

    public const int WGL_TEXTURE_FLOAT_RGBA_NV = 8376;

    public const int WGL_DEPTH_TEXTURE_FORMAT_NV = 8357;

    public const int WGL_TRANSPARENT_BLUE_VALUE_ARB = 8249;

    public const int WGL_DEPTH_BUFFER_BIT_ARB = 4;

    public const int WGL_TEXTURE_CUBE_MAP_NEGATIVE_Z_ARB = 8322;

    public const int WGL_SWAP_EXCHANGE_ARB = 8232;

    public const int WGL_TRANSPARENT_RED_VALUE_ARB = 8247;

    public const int WGL_SWAP_COPY_ARB = 8233;

    public const int WGL_GREEN_SHIFT_EXT = 8216;

    public const int WGL_TEXTURE_CUBE_MAP_POSITIVE_Z_ARB = 8321;

    public const int WGL_TYPE_RGBA_EXT = 8235;

    public const int WGL_TYPE_RGBA_FLOAT_ATI = 8608;

    public const int WGL_NUMBER_PIXEL_FORMATS_ARB = 8192;

    public const int WGL_TEXTURE_RGBA_ARB = 8310;

    public const int WGL_SWAP_COPY_EXT = 8233;

    public const int WGL_NEED_SYSTEM_PALETTE_ARB = 8197;

    public const int WGL_TEXTURE_FLOAT_RGB_NV = 8375;

    public const int WGL_SWAP_UNDEFINED_ARB = 8234;

    public const int WGL_GENLOCK_SOURCE_DIGITAL_FIELD_I3D = 8265;

    public const int WGL_GENLOCK_SOURCE_EDGE_RISING_I3D = 8267;

    public const int WGL_SWAP_LAYER_BUFFERS_EXT = 8198;

    public const int WGL_SWAP_UNDEFINED_EXT = 8234;

    public const int WGL_FULL_ACCELERATION_ARB = 8231;

    public const int WGL_NUMBER_UNDERLAYS_ARB = 8201;

    public const int WGL_BIND_TO_TEXTURE_RGBA_ARB = 8305;

    public const int WGL_TRANSPARENT_GREEN_VALUE_ARB = 8248;

    public const int WGL_PIXEL_TYPE_ARB = 8211;

    public const int WGL_NEED_PALETTE_ARB = 8196;

    public const int WGL_NUMBER_OVERLAYS_EXT = 8200;

    public const int WGL_DEPTH_BITS_EXT = 8226;

    public const int WGL_AUX3_ARB = 8330;

    public const int WGL_DEPTH_BITS_ARB = 8226;

    public const int WGL_GENERIC_ACCELERATION_ARB = 8230;

    public const int WGL_TYPE_RGBA_ARB = 8235;

    public const int WGL_DRAW_TO_WINDOW_EXT = 8193;

    public const int WGL_TEXTURE_2D_ARB = 8314;

    public const int WGL_STENCIL_BUFFER_BIT_ARB = 8;

    public const int WGL_SWAP_EXCHANGE_EXT = 8232;

    public const int WGL_SHARE_STENCIL_EXT = 8205;

    public const int WGL_STEREO_ARB = 8210;

    public const int WGL_SHARE_ACCUM_ARB = 8206;

    public const int WGL_TEXTURE_RECTANGLE_NV = 8354;

    public const int WGL_BIND_TO_TEXTURE_RECTANGLE_FLOAT_RGB_NV = 8371;

    public const int WGL_STENCIL_BITS_EXT = 8227;

    public const int WGL_MIPMAP_LEVEL_ARB = 8315;

    public const int WGL_DRAW_TO_WINDOW_ARB = 8193;

    public const int WGL_AUX5_ARB = 8332;

    public const int WGL_DEPTH_COMPONENT_NV = 8359;

    public const int WGL_AUX1_ARB = 8328;

    public const int WGL_TEXTURE_DEPTH_COMPONENT_NV = 8358;

    public const int WGL_FRONT_LEFT_ARB = 8323;

    public const int WGL_MAX_PBUFFER_HEIGHT_EXT = 8240;

    public const int WGL_RED_BITS_EXT = 8213;

    public const int WGL_GENLOCK_SOURCE_EXTENAL_TTL_I3D = 8263;

    public const int WGL_RED_SHIFT_ARB = 8214;

    public const int WGL_TEXTURE_CUBE_MAP_POSITIVE_Y_ARB = 8319;

    public const int WGL_DIGITAL_VIDEO_CURSOR_ALPHA_VALUE_I3D = 8273;

    public const int WGL_TRANSPARENT_INDEX_VALUE_ARB = 8251;

    public const int WGL_ALPHA_BITS_EXT = 8219;

    public const int WGL_TRANSPARENT_VALUE_EXT = 8203;

    public const int WGL_BIND_TO_TEXTURE_DEPTH_NV = 8355;

    public const int WGL_TEXTURE_1D_ARB = 8313;

    public const int WGL_MAX_PBUFFER_PIXELS_ARB = 8238;

    public const int WGL_SUPPORT_OPENGL_ARB = 8208;

    public const int WGL_TEXTURE_TARGET_ARB = 8307;

    public const int WGL_SAMPLE_BUFFERS_3DFX = 8288;

    public const int WGL_TEXTURE_FLOAT_R_NV = 8373;

    public const int WGL_COLOR_BITS_EXT = 8212;

    public const int WGL_BACK_RIGHT_ARB = 8326;

    public const int WGL_PBUFFER_HEIGHT_ARB = 8245;

    public const int WGL_ACCUM_BLUE_BITS_ARB = 8224;

    public const int WGL_TEXTURE_CUBE_MAP_POSITIVE_X_ARB = 8317;

    public const int WGL_GENLOCK_SOURCE_EXTENAL_SYNC_I3D = 8261;

    public const int WGL_SWAP_METHOD_ARB = 8199;

    public const int WGL_AUX8_ARB = 8335;

    public const int WGL_TYPE_COLORINDEX_ARB = 8236;

    public const int WGL_DRAW_TO_PBUFFER_EXT = 8237;

    public const int WGL_CUBE_MAP_FACE_ARB = 8316;

    public const int WGL_GENLOCK_SOURCE_EDGE_BOTH_I3D = 8268;

    public const int WGL_SAMPLES_3DFX = 8289;

    public const int WGL_MAX_PBUFFER_PIXELS_EXT = 8238;

    public const int WGL_DOUBLE_BUFFER_ARB = 8209;

    public const int WGL_STEREO_EXT = 8210;

    public const int WGL_RED_SHIFT_EXT = 8214;

    public const int WGL_ALPHA_BITS_ARB = 8219;

    public const int WGL_COLOR_BITS_ARB = 8212;

    public const int WGL_GAMMA_TABLE_SIZE_I3D = 8270;

    public const int WGL_AUX_BUFFERS_ARB = 8228;

    public const int WGL_ERROR_INCOMPATIBLE_DEVICE_CONTEXTS_ARB = 8276;

    public const int WGL_FRONT_COLOR_BUFFER_BIT_ARB = 1;

    public const int WGL_SAMPLES_EXT = 8258;

    public const int WGL_ALPHA_SHIFT_EXT = 8220;

    public const int WGL_ACCELERATION_EXT = 8195;

    public const int WGL_AUX6_ARB = 8333;

    public const int WGL_FRONT_RIGHT_ARB = 8324;

    public const int WGL_PBUFFER_WIDTH_ARB = 8244;

    public const int WGL_PBUFFER_LARGEST_ARB = 8243;

    public const int WGL_NUMBER_UNDERLAYS_EXT = 8201;

    public const int WGL_ACCUM_BITS_ARB = 8221;

    public const int WGL_STENCIL_BITS_ARB = 8227;

    public const int WGL_ACCUM_BLUE_BITS_EXT = 8224;

    public const int WGL_MAX_PBUFFER_HEIGHT_ARB = 8240;

    public const int WGL_AUX4_ARB = 8331;

    public const int WGL_TEXTURE_FORMAT_ARB = 8306;

    public const int WGL_ACCUM_RED_BITS_ARB = 8222;

    public const int WGL_BIND_TO_TEXTURE_RECTANGLE_FLOAT_RGBA_NV = 8372;

    public const int WGL_FLOAT_COMPONENTS_NV = 8368;

    public const int WGL_TRANSPARENT_ARB = 8202;

    public const int WGL_RED_BITS_ARB = 8213;

    public const int WGL_GREEN_BITS_ARB = 8215;

    public const int WGL_GENLOCK_SOURCE_MULTIVIEW_I3D = 8260;

    public const int WGL_BLUE_BITS_EXT = 8217;

    public const int WGL_BIND_TO_TEXTURE_RECTANGLE_FLOAT_RG_NV = 8370;

    public const int WGL_GREEN_BITS_EXT = 8215;

    public const int WGL_SHARE_DEPTH_ARB = 8204;

    public const int WGL_ALPHA_SHIFT_ARB = 8220;

    public const int WGL_PBUFFER_WIDTH_EXT = 8244;

    public const int WGL_BIND_TO_TEXTURE_RECTANGLE_RGB_NV = 8352;

    public const int WGL_SWAP_LAYER_BUFFERS_ARB = 8198;

    public const int WGL_ERROR_INVALID_PIXEL_TYPE_ARB = 8259;

    public const int WGL_AUX7_ARB = 8334;

    public const int WGL_PBUFFER_LOST_ARB = 8246;

    public const int WGL_ERROR_INVALID_PIXEL_TYPE_EXT = 8259;

    public const int WGL_SAMPLES_ARB = 8258;

    public const int WGL_GENLOCK_SOURCE_EXTENAL_FIELD_I3D = 8262;

    public const int WGL_GAMMA_EXCLUDE_DESKTOP_I3D = 8271;

    public const int WGL_FONT_LINES = 0;

    public const int WGL_DIGITAL_VIDEO_CURSOR_INCLUDED_I3D = 8274;

    public const int WGL_OPTIMAL_PBUFFER_HEIGHT_EXT = 8242;

    public const int WGL_PBUFFER_LARGEST_EXT = 8243;

    public const int WGL_DRAW_TO_BITMAP_EXT = 8194;

    public const int WGL_DIGITAL_VIDEO_CURSOR_ALPHA_FRAMEBUFFER_I3D = 8272;

    public const int WGL_OPTIMAL_PBUFFER_WIDTH_EXT = 8241;

    public const int WGL_DRAW_TO_BITMAP_ARB = 8194;

    public const int WGL_BACK_LEFT_ARB = 8325;

    public const int WGL_SUPPORT_OPENGL_EXT = 8208;

    internal const string Library = "opengl32.dll";

    private static StringBuilder sb;

    private static object gl_lock;

    private static SortedList<string, bool> AvailableExtensions;

    private static bool rebuildExtensionList;

    private static Type glClass;

    private static Type delegatesClass;

    private static Type importsClass;

    private static FieldInfo[] delegates;

    public static IntPtr wglCreateContext(IntPtr hDc)
    {
        return Delegates.wglCreateContext(hDc);
    }

    public static bool wglDeleteContext(IntPtr oldContext)
    {
        return Delegates.wglDeleteContext(oldContext);
    }

    public static IntPtr wglGetCurrentContext()
    {
        return Delegates.wglGetCurrentContext();
    }

    public static bool wglMakeCurrent(IntPtr hDc, IntPtr newContext)
    {
        return Delegates.wglMakeCurrent(hDc, newContext);
    }

    [CLSCompliant(false)]
    public static bool wglCopyContext(IntPtr hglrcSrc, IntPtr hglrcDst, uint mask)
    {
        return Delegates.wglCopyContext(hglrcSrc, hglrcDst, mask);
    }

    public static bool wglCopyContext(IntPtr hglrcSrc, IntPtr hglrcDst, int mask)
    {
        return Delegates.wglCopyContext(hglrcSrc, hglrcDst, (uint)mask);
    }

    public unsafe static int wglChoosePixelFormat(IntPtr hDc, Gdi.PIXELFORMATDESCRIPTOR[] pPfd)
    {
        fixed (Gdi.PIXELFORMATDESCRIPTOR* pPfd2 = pPfd)
        {
            return Delegates.wglChoosePixelFormat(hDc, pPfd2);
        }
    }

    public unsafe static int wglChoosePixelFormat(IntPtr hDc, ref Gdi.PIXELFORMATDESCRIPTOR pPfd)
    {
        fixed (Gdi.PIXELFORMATDESCRIPTOR* pPfd2 = &pPfd)
        {
            return Delegates.wglChoosePixelFormat(hDc, pPfd2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static int wglChoosePixelFormat(IntPtr hDc, IntPtr pPfd)
    {
        return Delegates.wglChoosePixelFormat(hDc, (Gdi.PIXELFORMATDESCRIPTOR*)(void*)pPfd);
    }

    [CLSCompliant(false)]
    public unsafe static int wglDescribePixelFormat(IntPtr hdc, int ipfd, uint cjpfd, Gdi.PIXELFORMATDESCRIPTOR[] ppfd)
    {
        fixed (Gdi.PIXELFORMATDESCRIPTOR* ppfd2 = ppfd)
        {
            return Delegates.wglDescribePixelFormat(hdc, ipfd, cjpfd, ppfd2);
        }
    }

    public unsafe static int wglDescribePixelFormat(IntPtr hdc, int ipfd, int cjpfd, Gdi.PIXELFORMATDESCRIPTOR[] ppfd)
    {
        fixed (Gdi.PIXELFORMATDESCRIPTOR* ppfd2 = ppfd)
        {
            return Delegates.wglDescribePixelFormat(hdc, ipfd, (uint)cjpfd, ppfd2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static int wglDescribePixelFormat(IntPtr hdc, int ipfd, uint cjpfd, ref Gdi.PIXELFORMATDESCRIPTOR ppfd)
    {
        fixed (Gdi.PIXELFORMATDESCRIPTOR* ppfd2 = &ppfd)
        {
            return Delegates.wglDescribePixelFormat(hdc, ipfd, cjpfd, ppfd2);
        }
    }

    public unsafe static int wglDescribePixelFormat(IntPtr hdc, int ipfd, int cjpfd, ref Gdi.PIXELFORMATDESCRIPTOR ppfd)
    {
        fixed (Gdi.PIXELFORMATDESCRIPTOR* ppfd2 = &ppfd)
        {
            return Delegates.wglDescribePixelFormat(hdc, ipfd, (uint)cjpfd, ppfd2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static int wglDescribePixelFormat(IntPtr hdc, int ipfd, uint cjpfd, IntPtr ppfd)
    {
        return Delegates.wglDescribePixelFormat(hdc, ipfd, cjpfd, (Gdi.PIXELFORMATDESCRIPTOR*)(void*)ppfd);
    }

    [CLSCompliant(false)]
    public unsafe static int wglDescribePixelFormat(IntPtr hdc, int ipfd, int cjpfd, IntPtr ppfd)
    {
        return Delegates.wglDescribePixelFormat(hdc, ipfd, (uint)cjpfd, (Gdi.PIXELFORMATDESCRIPTOR*)(void*)ppfd);
    }

    public static IntPtr wglGetCurrentDC()
    {
        return Delegates.wglGetCurrentDC();
    }

    public static IntPtr wglGetDefaultProcAddress(string lpszProc)
    {
        return Delegates.wglGetDefaultProcAddress(lpszProc);
    }

    public static IntPtr wglGetProcAddress(string lpszProc)
    {
        return Delegates.wglGetProcAddress(lpszProc);
    }

    public static int wglGetPixelFormat(IntPtr hdc)
    {
        return Delegates.wglGetPixelFormat(hdc);
    }

    public unsafe static bool wglSetPixelFormat(IntPtr hdc, int ipfd, Gdi.PIXELFORMATDESCRIPTOR[] ppfd)
    {
        fixed (Gdi.PIXELFORMATDESCRIPTOR* ppfd2 = ppfd)
        {
            return Delegates.wglSetPixelFormat(hdc, ipfd, ppfd2);
        }
    }

    public unsafe static bool wglSetPixelFormat(IntPtr hdc, int ipfd, ref Gdi.PIXELFORMATDESCRIPTOR ppfd)
    {
        fixed (Gdi.PIXELFORMATDESCRIPTOR* ppfd2 = &ppfd)
        {
            return Delegates.wglSetPixelFormat(hdc, ipfd, ppfd2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglSetPixelFormat(IntPtr hdc, int ipfd, IntPtr ppfd)
    {
        return Delegates.wglSetPixelFormat(hdc, ipfd, (Gdi.PIXELFORMATDESCRIPTOR*)(void*)ppfd);
    }

    public static bool wglSwapBuffers(IntPtr hdc)
    {
        return Delegates.wglSwapBuffers(hdc);
    }

    public static bool wglShareLists(IntPtr hrcSrvShare, IntPtr hrcSrvSource)
    {
        return Delegates.wglShareLists(hrcSrvShare, hrcSrvSource);
    }

    public static IntPtr wglCreateLayerContext(IntPtr hDc, int level)
    {
        return Delegates.wglCreateLayerContext(hDc, level);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglDescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane, uint nBytes, Gdi.LAYERPLANEDESCRIPTOR[] plpd)
    {
        fixed (Gdi.LAYERPLANEDESCRIPTOR* plpd2 = plpd)
        {
            return Delegates.wglDescribeLayerPlane(hDc, pixelFormat, layerPlane, nBytes, plpd2);
        }
    }

    public unsafe static bool wglDescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane, int nBytes, Gdi.LAYERPLANEDESCRIPTOR[] plpd)
    {
        fixed (Gdi.LAYERPLANEDESCRIPTOR* plpd2 = plpd)
        {
            return Delegates.wglDescribeLayerPlane(hDc, pixelFormat, layerPlane, (uint)nBytes, plpd2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglDescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane, uint nBytes, ref Gdi.LAYERPLANEDESCRIPTOR plpd)
    {
        fixed (Gdi.LAYERPLANEDESCRIPTOR* plpd2 = &plpd)
        {
            return Delegates.wglDescribeLayerPlane(hDc, pixelFormat, layerPlane, nBytes, plpd2);
        }
    }

    public unsafe static bool wglDescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane, int nBytes, ref Gdi.LAYERPLANEDESCRIPTOR plpd)
    {
        fixed (Gdi.LAYERPLANEDESCRIPTOR* plpd2 = &plpd)
        {
            return Delegates.wglDescribeLayerPlane(hDc, pixelFormat, layerPlane, (uint)nBytes, plpd2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglDescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane, uint nBytes, IntPtr plpd)
    {
        return Delegates.wglDescribeLayerPlane(hDc, pixelFormat, layerPlane, nBytes, (Gdi.LAYERPLANEDESCRIPTOR*)(void*)plpd);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglDescribeLayerPlane(IntPtr hDc, int pixelFormat, int layerPlane, int nBytes, IntPtr plpd)
    {
        return Delegates.wglDescribeLayerPlane(hDc, pixelFormat, layerPlane, (uint)nBytes, (Gdi.LAYERPLANEDESCRIPTOR*)(void*)plpd);
    }

    public unsafe static int wglSetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, int[] pcr)
    {
        fixed (int* pcr2 = pcr)
        {
            return Delegates.wglSetLayerPaletteEntries(hdc, iLayerPlane, iStart, cEntries, pcr2);
        }
    }

    public unsafe static int wglSetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, ref int pcr)
    {
        fixed (int* pcr2 = &pcr)
        {
            return Delegates.wglSetLayerPaletteEntries(hdc, iLayerPlane, iStart, cEntries, pcr2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static int wglSetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, IntPtr pcr)
    {
        return Delegates.wglSetLayerPaletteEntries(hdc, iLayerPlane, iStart, cEntries, (int*)(void*)pcr);
    }

    public unsafe static int wglGetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, int[] pcr)
    {
        fixed (int* pcr2 = pcr)
        {
            return Delegates.wglGetLayerPaletteEntries(hdc, iLayerPlane, iStart, cEntries, pcr2);
        }
    }

    public unsafe static int wglGetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, ref int pcr)
    {
        fixed (int* pcr2 = &pcr)
        {
            return Delegates.wglGetLayerPaletteEntries(hdc, iLayerPlane, iStart, cEntries, pcr2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static int wglGetLayerPaletteEntries(IntPtr hdc, int iLayerPlane, int iStart, int cEntries, IntPtr pcr)
    {
        return Delegates.wglGetLayerPaletteEntries(hdc, iLayerPlane, iStart, cEntries, (int*)(void*)pcr);
    }

    public static bool wglRealizeLayerPalette(IntPtr hdc, int iLayerPlane, bool bRealize)
    {
        return Delegates.wglRealizeLayerPalette(hdc, iLayerPlane, bRealize);
    }

    [CLSCompliant(false)]
    public static bool wglSwapLayerBuffers(IntPtr hdc, uint fuFlags)
    {
        return Delegates.wglSwapLayerBuffers(hdc, fuFlags);
    }

    public static bool wglSwapLayerBuffers(IntPtr hdc, int fuFlags)
    {
        return Delegates.wglSwapLayerBuffers(hdc, (uint)fuFlags);
    }

    public static bool wglUseFontBitmapsA(IntPtr hDC, int first, int count, int listBase)
    {
        return Delegates.wglUseFontBitmapsA(hDC, first, count, listBase);
    }

    public static bool wglUseFontBitmapsW(IntPtr hDC, int first, int count, int listBase)
    {
        return Delegates.wglUseFontBitmapsW(hDC, first, count, listBase);
    }

    public unsafe static bool wglUseFontOutlinesA(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, Gdi.GLYPHMETRICSFLOAT[] glyphMetrics)
    {
        fixed (Gdi.GLYPHMETRICSFLOAT* glyphMetrics2 = glyphMetrics)
        {
            return Delegates.wglUseFontOutlinesA(hDC, first, count, listBase, thickness, deviation, fontMode, glyphMetrics2);
        }
    }

    public unsafe static bool wglUseFontOutlinesA(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, ref Gdi.GLYPHMETRICSFLOAT glyphMetrics)
    {
        fixed (Gdi.GLYPHMETRICSFLOAT* glyphMetrics2 = &glyphMetrics)
        {
            return Delegates.wglUseFontOutlinesA(hDC, first, count, listBase, thickness, deviation, fontMode, glyphMetrics2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglUseFontOutlinesA(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, IntPtr glyphMetrics)
    {
        return Delegates.wglUseFontOutlinesA(hDC, first, count, listBase, thickness, deviation, fontMode, (Gdi.GLYPHMETRICSFLOAT*)(void*)glyphMetrics);
    }

    public unsafe static bool wglUseFontOutlinesW(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, Gdi.GLYPHMETRICSFLOAT[] glyphMetrics)
    {
        fixed (Gdi.GLYPHMETRICSFLOAT* glyphMetrics2 = glyphMetrics)
        {
            return Delegates.wglUseFontOutlinesW(hDC, first, count, listBase, thickness, deviation, fontMode, glyphMetrics2);
        }
    }

    public unsafe static bool wglUseFontOutlinesW(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, ref Gdi.GLYPHMETRICSFLOAT glyphMetrics)
    {
        fixed (Gdi.GLYPHMETRICSFLOAT* glyphMetrics2 = &glyphMetrics)
        {
            return Delegates.wglUseFontOutlinesW(hDC, first, count, listBase, thickness, deviation, fontMode, glyphMetrics2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglUseFontOutlinesW(IntPtr hDC, int first, int count, int listBase, float thickness, float deviation, int fontMode, IntPtr glyphMetrics)
    {
        return Delegates.wglUseFontOutlinesW(hDC, first, count, listBase, thickness, deviation, fontMode, (Gdi.GLYPHMETRICSFLOAT*)(void*)glyphMetrics);
    }

    [CLSCompliant(false)]
    public static IntPtr wglCreateBufferRegionARB(IntPtr hDC, int iLayerPlane, uint uType)
    {
        return Delegates.wglCreateBufferRegionARB(hDC, iLayerPlane, uType);
    }

    public static IntPtr wglCreateBufferRegionARB(IntPtr hDC, int iLayerPlane, int uType)
    {
        return Delegates.wglCreateBufferRegionARB(hDC, iLayerPlane, (uint)uType);
    }

    public static void wglDeleteBufferRegionARB(IntPtr hRegion)
    {
        Delegates.wglDeleteBufferRegionARB(hRegion);
    }

    public static bool wglSaveBufferRegionARB(IntPtr hRegion, int x, int y, int width, int height)
    {
        return Delegates.wglSaveBufferRegionARB(hRegion, x, y, width, height);
    }

    public static bool wglRestoreBufferRegionARB(IntPtr hRegion, int x, int y, int width, int height, int xSrc, int ySrc)
    {
        return Delegates.wglRestoreBufferRegionARB(hRegion, x, y, width, height, xSrc, ySrc);
    }

    public static string wglGetExtensionsStringARB(IntPtr hdc)
    {
        return Marshal.PtrToStringAnsi(Delegates.wglGetExtensionsStringARB(hdc));
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int[] piAttributes, [Out] int[] piValues)
    {
        fixed (int* piAttributes2 = piAttributes)
        {
            fixed (int* piValues2 = piValues)
            {
                return Delegates.wglGetPixelFormatAttribivARB(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes2, piValues2);
            }
        }
    }

    public unsafe static bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, [Out] int[] piValues)
    {
        fixed (int* piAttributes2 = piAttributes)
        {
            fixed (int* piValues2 = piValues)
            {
                return Delegates.wglGetPixelFormatAttribivARB(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, piAttributes2, piValues2);
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, ref int piAttributes, out int piValues)
    {
        fixed (int* piAttributes2 = &piAttributes)
        {
            fixed (int* ptr = &piValues)
            {
                bool result = Delegates.wglGetPixelFormatAttribivARB(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes2, ptr);
                piValues = *ptr;
                return result;
            }
        }
    }

    public unsafe static bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, ref int piAttributes, out int piValues)
    {
        fixed (int* piAttributes2 = &piAttributes)
        {
            fixed (int* ptr = &piValues)
            {
                bool result = Delegates.wglGetPixelFormatAttribivARB(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, piAttributes2, ptr);
                piValues = *ptr;
                return result;
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, [Out] IntPtr piValues)
    {
        return Delegates.wglGetPixelFormatAttribivARB(hdc, iPixelFormat, iLayerPlane, nAttributes, (int*)(void*)piAttributes, (int*)(void*)piValues);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, IntPtr piAttributes, [Out] IntPtr piValues)
    {
        return Delegates.wglGetPixelFormatAttribivARB(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, (int*)(void*)piAttributes, (int*)(void*)piValues);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int[] piAttributes, [Out] float[] pfValues)
    {
        fixed (int* piAttributes2 = piAttributes)
        {
            fixed (float* pfValues2 = pfValues)
            {
                return Delegates.wglGetPixelFormatAttribfvARB(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes2, pfValues2);
            }
        }
    }

    public unsafe static bool wglGetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, int[] piAttributes, [Out] float[] pfValues)
    {
        fixed (int* piAttributes2 = piAttributes)
        {
            fixed (float* pfValues2 = pfValues)
            {
                return Delegates.wglGetPixelFormatAttribfvARB(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, piAttributes2, pfValues2);
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, ref int piAttributes, out float pfValues)
    {
        fixed (int* piAttributes2 = &piAttributes)
        {
            fixed (float* ptr = &pfValues)
            {
                bool result = Delegates.wglGetPixelFormatAttribfvARB(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes2, ptr);
                pfValues = *ptr;
                return result;
            }
        }
    }

    public unsafe static bool wglGetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, ref int piAttributes, out float pfValues)
    {
        fixed (int* piAttributes2 = &piAttributes)
        {
            fixed (float* ptr = &pfValues)
            {
                bool result = Delegates.wglGetPixelFormatAttribfvARB(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, piAttributes2, ptr);
                pfValues = *ptr;
                return result;
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, [Out] IntPtr pfValues)
    {
        return Delegates.wglGetPixelFormatAttribfvARB(hdc, iPixelFormat, iLayerPlane, nAttributes, (int*)(void*)piAttributes, (float*)(void*)pfValues);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, IntPtr piAttributes, [Out] IntPtr pfValues)
    {
        return Delegates.wglGetPixelFormatAttribfvARB(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, (int*)(void*)piAttributes, (float*)(void*)pfValues);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, uint nMaxFormats, [Out] int[] piFormats, [Out] uint[] nNumFormats)
    {
        fixed (int* piAttribIList2 = piAttribIList)
        {
            fixed (float* pfAttribFList2 = pfAttribFList)
            {
                fixed (int* piFormats2 = piFormats)
                {
                    fixed (uint* nNumFormats2 = nNumFormats)
                    {
                        return Delegates.wglChoosePixelFormatARB(hdc, piAttribIList2, pfAttribFList2, nMaxFormats, piFormats2, nNumFormats2);
                    }
                }
            }
        }
    }

    public unsafe static bool wglChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, int nMaxFormats, [Out] int[] piFormats, [Out] int[] nNumFormats)
    {
        fixed (int* piAttribIList2 = piAttribIList)
        {
            fixed (float* pfAttribFList2 = pfAttribFList)
            {
                fixed (int* piFormats2 = piFormats)
                {
                    fixed (int* nNumFormats2 = nNumFormats)
                    {
                        return Delegates.wglChoosePixelFormatARB(hdc, piAttribIList2, pfAttribFList2, (uint)nMaxFormats, piFormats2, (uint*)nNumFormats2);
                    }
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglChoosePixelFormatARB(IntPtr hdc, ref int piAttribIList, ref float pfAttribFList, uint nMaxFormats, out int piFormats, out uint nNumFormats)
    {
        fixed (int* piAttribIList2 = &piAttribIList)
        {
            fixed (float* pfAttribFList2 = &pfAttribFList)
            {
                fixed (int* ptr = &piFormats)
                {
                    fixed (uint* ptr2 = &nNumFormats)
                    {
                        bool result = Delegates.wglChoosePixelFormatARB(hdc, piAttribIList2, pfAttribFList2, nMaxFormats, ptr, ptr2);
                        piFormats = *ptr;
                        nNumFormats = *ptr2;
                        return result;
                    }
                }
            }
        }
    }

    public unsafe static bool wglChoosePixelFormatARB(IntPtr hdc, ref int piAttribIList, ref float pfAttribFList, int nMaxFormats, out int piFormats, out int nNumFormats)
    {
        fixed (int* piAttribIList2 = &piAttribIList)
        {
            fixed (float* pfAttribFList2 = &pfAttribFList)
            {
                fixed (int* ptr = &piFormats)
                {
                    fixed (int* ptr2 = &nNumFormats)
                    {
                        bool result = Delegates.wglChoosePixelFormatARB(hdc, piAttribIList2, pfAttribFList2, (uint)nMaxFormats, ptr, (uint*)ptr2);
                        piFormats = *ptr;
                        nNumFormats = *ptr2;
                        return result;
                    }
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglChoosePixelFormatARB(IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, uint nMaxFormats, [Out] IntPtr piFormats, [Out] IntPtr nNumFormats)
    {
        return Delegates.wglChoosePixelFormatARB(hdc, (int*)(void*)piAttribIList, (float*)(void*)pfAttribFList, nMaxFormats, (int*)(void*)piFormats, (uint*)(void*)nNumFormats);
    }

    public unsafe static bool wglChoosePixelFormatARB(IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, int nMaxFormats, [Out] IntPtr piFormats, [Out] IntPtr nNumFormats)
    {
        return Delegates.wglChoosePixelFormatARB(hdc, (int*)(void*)piAttribIList, (float*)(void*)pfAttribFList, (uint)nMaxFormats, (int*)(void*)piFormats, (uint*)(void*)nNumFormats);
    }

    public static bool wglMakeContextCurrentARB(IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc)
    {
        return Delegates.wglMakeContextCurrentARB(hDrawDC, hReadDC, hglrc);
    }

    public static IntPtr wglGetCurrentReadDCARB()
    {
        return Delegates.wglGetCurrentReadDCARB();
    }

    public unsafe static IntPtr wglCreatePbufferARB(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, int[] piAttribList)
    {
        fixed (int* piAttribList2 = piAttribList)
        {
            return Delegates.wglCreatePbufferARB(hDC, iPixelFormat, iWidth, iHeight, piAttribList2);
        }
    }

    public unsafe static IntPtr wglCreatePbufferARB(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, ref int piAttribList)
    {
        fixed (int* piAttribList2 = &piAttribList)
        {
            return Delegates.wglCreatePbufferARB(hDC, iPixelFormat, iWidth, iHeight, piAttribList2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static IntPtr wglCreatePbufferARB(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, IntPtr piAttribList)
    {
        return Delegates.wglCreatePbufferARB(hDC, iPixelFormat, iWidth, iHeight, (int*)(void*)piAttribList);
    }

    public static IntPtr wglGetPbufferDCARB(IntPtr hPbuffer)
    {
        return Delegates.wglGetPbufferDCARB(hPbuffer);
    }

    public static int wglReleasePbufferDCARB(IntPtr hPbuffer, IntPtr hDC)
    {
        return Delegates.wglReleasePbufferDCARB(hPbuffer, hDC);
    }

    public static bool wglDestroyPbufferARB(IntPtr hPbuffer)
    {
        return Delegates.wglDestroyPbufferARB(hPbuffer);
    }

    public unsafe static bool wglQueryPbufferARB(IntPtr hPbuffer, int iAttribute, [Out] int[] piValue)
    {
        fixed (int* piValue2 = piValue)
        {
            return Delegates.wglQueryPbufferARB(hPbuffer, iAttribute, piValue2);
        }
    }

    public unsafe static bool wglQueryPbufferARB(IntPtr hPbuffer, int iAttribute, out int piValue)
    {
        fixed (int* ptr = &piValue)
        {
            bool result = Delegates.wglQueryPbufferARB(hPbuffer, iAttribute, ptr);
            piValue = *ptr;
            return result;
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglQueryPbufferARB(IntPtr hPbuffer, int iAttribute, [Out] IntPtr piValue)
    {
        return Delegates.wglQueryPbufferARB(hPbuffer, iAttribute, (int*)(void*)piValue);
    }

    public static bool wglBindTexImageARB(IntPtr hPbuffer, int iBuffer)
    {
        return Delegates.wglBindTexImageARB(hPbuffer, iBuffer);
    }

    public static bool wglReleaseTexImageARB(IntPtr hPbuffer, int iBuffer)
    {
        return Delegates.wglReleaseTexImageARB(hPbuffer, iBuffer);
    }

    public unsafe static bool wglSetPbufferAttribARB(IntPtr hPbuffer, int[] piAttribList)
    {
        fixed (int* piAttribList2 = piAttribList)
        {
            return Delegates.wglSetPbufferAttribARB(hPbuffer, piAttribList2);
        }
    }

    public unsafe static bool wglSetPbufferAttribARB(IntPtr hPbuffer, ref int piAttribList)
    {
        fixed (int* piAttribList2 = &piAttribList)
        {
            return Delegates.wglSetPbufferAttribARB(hPbuffer, piAttribList2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglSetPbufferAttribARB(IntPtr hPbuffer, IntPtr piAttribList)
    {
        return Delegates.wglSetPbufferAttribARB(hPbuffer, (int*)(void*)piAttribList);
    }

    [CLSCompliant(false)]
    public static bool wglCreateDisplayColorTableEXT(ushort id)
    {
        return Delegates.wglCreateDisplayColorTableEXT(id);
    }

    public static bool wglCreateDisplayColorTableEXT(short id)
    {
        return Delegates.wglCreateDisplayColorTableEXT((ushort)id);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglLoadDisplayColorTableEXT(ushort[] table, uint length)
    {
        fixed (ushort* table2 = table)
        {
            return Delegates.wglLoadDisplayColorTableEXT(table2, length);
        }
    }

    public unsafe static bool wglLoadDisplayColorTableEXT(short[] table, int length)
    {
        fixed (short* table2 = table)
        {
            return Delegates.wglLoadDisplayColorTableEXT((ushort*)table2, (uint)length);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglLoadDisplayColorTableEXT(ref ushort table, uint length)
    {
        fixed (ushort* table2 = &table)
        {
            return Delegates.wglLoadDisplayColorTableEXT(table2, length);
        }
    }

    public unsafe static bool wglLoadDisplayColorTableEXT(ref short table, int length)
    {
        fixed (short* table2 = &table)
        {
            return Delegates.wglLoadDisplayColorTableEXT((ushort*)table2, (uint)length);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglLoadDisplayColorTableEXT(IntPtr table, uint length)
    {
        return Delegates.wglLoadDisplayColorTableEXT((ushort*)(void*)table, length);
    }

    public unsafe static bool wglLoadDisplayColorTableEXT(IntPtr table, int length)
    {
        return Delegates.wglLoadDisplayColorTableEXT((ushort*)(void*)table, (uint)length);
    }

    [CLSCompliant(false)]
    public static bool wglBindDisplayColorTableEXT(ushort id)
    {
        return Delegates.wglBindDisplayColorTableEXT(id);
    }

    public static bool wglBindDisplayColorTableEXT(short id)
    {
        return Delegates.wglBindDisplayColorTableEXT((ushort)id);
    }

    [CLSCompliant(false)]
    public static void wglDestroyDisplayColorTableEXT(ushort id)
    {
        Delegates.wglDestroyDisplayColorTableEXT(id);
    }

    public static void wglDestroyDisplayColorTableEXT(short id)
    {
        Delegates.wglDestroyDisplayColorTableEXT((ushort)id);
    }

    public static string wglGetExtensionsStringEXT()
    {
        return Marshal.PtrToStringAnsi(Delegates.wglGetExtensionsStringEXT());
    }

    public static bool wglMakeContextCurrentEXT(IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc)
    {
        return Delegates.wglMakeContextCurrentEXT(hDrawDC, hReadDC, hglrc);
    }

    public static IntPtr wglGetCurrentReadDCEXT()
    {
        return Delegates.wglGetCurrentReadDCEXT();
    }

    public unsafe static IntPtr wglCreatePbufferEXT(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, int[] piAttribList)
    {
        fixed (int* piAttribList2 = piAttribList)
        {
            return Delegates.wglCreatePbufferEXT(hDC, iPixelFormat, iWidth, iHeight, piAttribList2);
        }
    }

    public unsafe static IntPtr wglCreatePbufferEXT(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, ref int piAttribList)
    {
        fixed (int* piAttribList2 = &piAttribList)
        {
            return Delegates.wglCreatePbufferEXT(hDC, iPixelFormat, iWidth, iHeight, piAttribList2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static IntPtr wglCreatePbufferEXT(IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, IntPtr piAttribList)
    {
        return Delegates.wglCreatePbufferEXT(hDC, iPixelFormat, iWidth, iHeight, (int*)(void*)piAttribList);
    }

    public static IntPtr wglGetPbufferDCEXT(IntPtr hPbuffer)
    {
        return Delegates.wglGetPbufferDCEXT(hPbuffer);
    }

    public static int wglReleasePbufferDCEXT(IntPtr hPbuffer, IntPtr hDC)
    {
        return Delegates.wglReleasePbufferDCEXT(hPbuffer, hDC);
    }

    public static bool wglDestroyPbufferEXT(IntPtr hPbuffer)
    {
        return Delegates.wglDestroyPbufferEXT(hPbuffer);
    }

    public unsafe static bool wglQueryPbufferEXT(IntPtr hPbuffer, int iAttribute, [Out] int[] piValue)
    {
        fixed (int* piValue2 = piValue)
        {
            return Delegates.wglQueryPbufferEXT(hPbuffer, iAttribute, piValue2);
        }
    }

    public unsafe static bool wglQueryPbufferEXT(IntPtr hPbuffer, int iAttribute, out int piValue)
    {
        fixed (int* ptr = &piValue)
        {
            bool result = Delegates.wglQueryPbufferEXT(hPbuffer, iAttribute, ptr);
            piValue = *ptr;
            return result;
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglQueryPbufferEXT(IntPtr hPbuffer, int iAttribute, [Out] IntPtr piValue)
    {
        return Delegates.wglQueryPbufferEXT(hPbuffer, iAttribute, (int*)(void*)piValue);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribivEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, [Out] int[] piAttributes, [Out] int[] piValues)
    {
        fixed (int* piAttributes2 = piAttributes)
        {
            fixed (int* piValues2 = piValues)
            {
                return Delegates.wglGetPixelFormatAttribivEXT(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes2, piValues2);
            }
        }
    }

    public unsafe static bool wglGetPixelFormatAttribivEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, [Out] int[] piAttributes, [Out] int[] piValues)
    {
        fixed (int* piAttributes2 = piAttributes)
        {
            fixed (int* piValues2 = piValues)
            {
                return Delegates.wglGetPixelFormatAttribivEXT(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, piAttributes2, piValues2);
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribivEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, out int piAttributes, out int piValues)
    {
        fixed (int* ptr = &piAttributes)
        {
            fixed (int* ptr2 = &piValues)
            {
                bool result = Delegates.wglGetPixelFormatAttribivEXT(hdc, iPixelFormat, iLayerPlane, nAttributes, ptr, ptr2);
                piAttributes = *ptr;
                piValues = *ptr2;
                return result;
            }
        }
    }

    public unsafe static bool wglGetPixelFormatAttribivEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, out int piAttributes, out int piValues)
    {
        fixed (int* ptr = &piAttributes)
        {
            fixed (int* ptr2 = &piValues)
            {
                bool result = Delegates.wglGetPixelFormatAttribivEXT(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, ptr, ptr2);
                piAttributes = *ptr;
                piValues = *ptr2;
                return result;
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribivEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, [Out] IntPtr piAttributes, [Out] IntPtr piValues)
    {
        return Delegates.wglGetPixelFormatAttribivEXT(hdc, iPixelFormat, iLayerPlane, nAttributes, (int*)(void*)piAttributes, (int*)(void*)piValues);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribivEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, [Out] IntPtr piAttributes, [Out] IntPtr piValues)
    {
        return Delegates.wglGetPixelFormatAttribivEXT(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, (int*)(void*)piAttributes, (int*)(void*)piValues);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribfvEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, [Out] int[] piAttributes, [Out] float[] pfValues)
    {
        fixed (int* piAttributes2 = piAttributes)
        {
            fixed (float* pfValues2 = pfValues)
            {
                return Delegates.wglGetPixelFormatAttribfvEXT(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes2, pfValues2);
            }
        }
    }

    public unsafe static bool wglGetPixelFormatAttribfvEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, [Out] int[] piAttributes, [Out] float[] pfValues)
    {
        fixed (int* piAttributes2 = piAttributes)
        {
            fixed (float* pfValues2 = pfValues)
            {
                return Delegates.wglGetPixelFormatAttribfvEXT(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, piAttributes2, pfValues2);
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribfvEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, out int piAttributes, out float pfValues)
    {
        fixed (int* ptr = &piAttributes)
        {
            fixed (float* ptr2 = &pfValues)
            {
                bool result = Delegates.wglGetPixelFormatAttribfvEXT(hdc, iPixelFormat, iLayerPlane, nAttributes, ptr, ptr2);
                piAttributes = *ptr;
                pfValues = *ptr2;
                return result;
            }
        }
    }

    public unsafe static bool wglGetPixelFormatAttribfvEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, out int piAttributes, out float pfValues)
    {
        fixed (int* ptr = &piAttributes)
        {
            fixed (float* ptr2 = &pfValues)
            {
                bool result = Delegates.wglGetPixelFormatAttribfvEXT(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, ptr, ptr2);
                piAttributes = *ptr;
                pfValues = *ptr2;
                return result;
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribfvEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, [Out] IntPtr piAttributes, [Out] IntPtr pfValues)
    {
        return Delegates.wglGetPixelFormatAttribfvEXT(hdc, iPixelFormat, iLayerPlane, nAttributes, (int*)(void*)piAttributes, (float*)(void*)pfValues);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetPixelFormatAttribfvEXT(IntPtr hdc, int iPixelFormat, int iLayerPlane, int nAttributes, [Out] IntPtr piAttributes, [Out] IntPtr pfValues)
    {
        return Delegates.wglGetPixelFormatAttribfvEXT(hdc, iPixelFormat, iLayerPlane, (uint)nAttributes, (int*)(void*)piAttributes, (float*)(void*)pfValues);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglChoosePixelFormatEXT(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, uint nMaxFormats, [Out] int[] piFormats, [Out] uint[] nNumFormats)
    {
        fixed (int* piAttribIList2 = piAttribIList)
        {
            fixed (float* pfAttribFList2 = pfAttribFList)
            {
                fixed (int* piFormats2 = piFormats)
                {
                    fixed (uint* nNumFormats2 = nNumFormats)
                    {
                        return Delegates.wglChoosePixelFormatEXT(hdc, piAttribIList2, pfAttribFList2, nMaxFormats, piFormats2, nNumFormats2);
                    }
                }
            }
        }
    }

    public unsafe static bool wglChoosePixelFormatEXT(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, int nMaxFormats, [Out] int[] piFormats, [Out] int[] nNumFormats)
    {
        fixed (int* piAttribIList2 = piAttribIList)
        {
            fixed (float* pfAttribFList2 = pfAttribFList)
            {
                fixed (int* piFormats2 = piFormats)
                {
                    fixed (int* nNumFormats2 = nNumFormats)
                    {
                        return Delegates.wglChoosePixelFormatEXT(hdc, piAttribIList2, pfAttribFList2, (uint)nMaxFormats, piFormats2, (uint*)nNumFormats2);
                    }
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglChoosePixelFormatEXT(IntPtr hdc, ref int piAttribIList, ref float pfAttribFList, uint nMaxFormats, out int piFormats, out uint nNumFormats)
    {
        fixed (int* piAttribIList2 = &piAttribIList)
        {
            fixed (float* pfAttribFList2 = &pfAttribFList)
            {
                fixed (int* ptr = &piFormats)
                {
                    fixed (uint* ptr2 = &nNumFormats)
                    {
                        bool result = Delegates.wglChoosePixelFormatEXT(hdc, piAttribIList2, pfAttribFList2, nMaxFormats, ptr, ptr2);
                        piFormats = *ptr;
                        nNumFormats = *ptr2;
                        return result;
                    }
                }
            }
        }
    }

    public unsafe static bool wglChoosePixelFormatEXT(IntPtr hdc, ref int piAttribIList, ref float pfAttribFList, int nMaxFormats, out int piFormats, out int nNumFormats)
    {
        fixed (int* piAttribIList2 = &piAttribIList)
        {
            fixed (float* pfAttribFList2 = &pfAttribFList)
            {
                fixed (int* ptr = &piFormats)
                {
                    fixed (int* ptr2 = &nNumFormats)
                    {
                        bool result = Delegates.wglChoosePixelFormatEXT(hdc, piAttribIList2, pfAttribFList2, (uint)nMaxFormats, ptr, (uint*)ptr2);
                        piFormats = *ptr;
                        nNumFormats = *ptr2;
                        return result;
                    }
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglChoosePixelFormatEXT(IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, uint nMaxFormats, [Out] IntPtr piFormats, [Out] IntPtr nNumFormats)
    {
        return Delegates.wglChoosePixelFormatEXT(hdc, (int*)(void*)piAttribIList, (float*)(void*)pfAttribFList, nMaxFormats, (int*)(void*)piFormats, (uint*)(void*)nNumFormats);
    }

    public unsafe static bool wglChoosePixelFormatEXT(IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, int nMaxFormats, [Out] IntPtr piFormats, [Out] IntPtr nNumFormats)
    {
        return Delegates.wglChoosePixelFormatEXT(hdc, (int*)(void*)piAttribIList, (float*)(void*)pfAttribFList, (uint)nMaxFormats, (int*)(void*)piFormats, (uint*)(void*)nNumFormats);
    }

    public static bool wglSwapIntervalEXT(int interval)
    {
        return Delegates.wglSwapIntervalEXT(interval);
    }

    public static int wglGetSwapIntervalEXT()
    {
        return Delegates.wglGetSwapIntervalEXT();
    }

    public static IntPtr wglAllocateMemoryNV(int size, float readfreq, float writefreq, float priority)
    {
        return Delegates.wglAllocateMemoryNV(size, readfreq, writefreq, priority);
    }

    public unsafe static void wglFreeMemoryNV([Out] IntPtr[] pointer)
    {
        fixed (IntPtr* pointer2 = pointer)
        {
            Delegates.wglFreeMemoryNV(pointer2);
        }
    }

    public unsafe static void wglFreeMemoryNV(out IntPtr pointer)
    {
        fixed (IntPtr* ptr = &pointer)
        {
            Delegates.wglFreeMemoryNV(ptr);
            pointer = *ptr;
        }
    }

    [CLSCompliant(false)]
    public unsafe static void wglFreeMemoryNV([Out] IntPtr pointer)
    {
        Delegates.wglFreeMemoryNV((IntPtr*)(void*)pointer);
    }

    public unsafe static bool wglGetSyncValuesOML(IntPtr hdc, [Out] long[] ust, [Out] long[] msc, [Out] long[] sbc)
    {
        fixed (long* ust2 = ust)
        {
            fixed (long* msc2 = msc)
            {
                fixed (long* sbc2 = sbc)
                {
                    return Delegates.wglGetSyncValuesOML(hdc, ust2, msc2, sbc2);
                }
            }
        }
    }

    public unsafe static bool wglGetSyncValuesOML(IntPtr hdc, out long ust, out long msc, out long sbc)
    {
        fixed (long* ptr = &ust)
        {
            fixed (long* ptr2 = &msc)
            {
                fixed (long* ptr3 = &sbc)
                {
                    bool result = Delegates.wglGetSyncValuesOML(hdc, ptr, ptr2, ptr3);
                    ust = *ptr;
                    msc = *ptr2;
                    sbc = *ptr3;
                    return result;
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetSyncValuesOML(IntPtr hdc, [Out] IntPtr ust, [Out] IntPtr msc, [Out] IntPtr sbc)
    {
        return Delegates.wglGetSyncValuesOML(hdc, (long*)(void*)ust, (long*)(void*)msc, (long*)(void*)sbc);
    }

    public unsafe static bool wglGetMscRateOML(IntPtr hdc, [Out] int[] numerator, [Out] int[] denominator)
    {
        fixed (int* numerator2 = numerator)
        {
            fixed (int* denominator2 = denominator)
            {
                return Delegates.wglGetMscRateOML(hdc, numerator2, denominator2);
            }
        }
    }

    public unsafe static bool wglGetMscRateOML(IntPtr hdc, out int numerator, out int denominator)
    {
        fixed (int* ptr = &numerator)
        {
            fixed (int* ptr2 = &denominator)
            {
                bool result = Delegates.wglGetMscRateOML(hdc, ptr, ptr2);
                numerator = *ptr;
                denominator = *ptr2;
                return result;
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetMscRateOML(IntPtr hdc, [Out] IntPtr numerator, [Out] IntPtr denominator)
    {
        return Delegates.wglGetMscRateOML(hdc, (int*)(void*)numerator, (int*)(void*)denominator);
    }

    public static long wglSwapBuffersMscOML(IntPtr hdc, long target_msc, long divisor, long remainder)
    {
        return Delegates.wglSwapBuffersMscOML(hdc, target_msc, divisor, remainder);
    }

    public static long wglSwapLayerBuffersMscOML(IntPtr hdc, int fuPlanes, long target_msc, long divisor, long remainder)
    {
        return Delegates.wglSwapLayerBuffersMscOML(hdc, fuPlanes, target_msc, divisor, remainder);
    }

    public unsafe static bool wglWaitForMscOML(IntPtr hdc, long target_msc, long divisor, long remainder, [Out] long[] ust, [Out] long[] msc, [Out] long[] sbc)
    {
        fixed (long* ust2 = ust)
        {
            fixed (long* msc2 = msc)
            {
                fixed (long* sbc2 = sbc)
                {
                    return Delegates.wglWaitForMscOML(hdc, target_msc, divisor, remainder, ust2, msc2, sbc2);
                }
            }
        }
    }

    public unsafe static bool wglWaitForMscOML(IntPtr hdc, long target_msc, long divisor, long remainder, out long ust, out long msc, out long sbc)
    {
        fixed (long* ptr = &ust)
        {
            fixed (long* ptr2 = &msc)
            {
                fixed (long* ptr3 = &sbc)
                {
                    bool result = Delegates.wglWaitForMscOML(hdc, target_msc, divisor, remainder, ptr, ptr2, ptr3);
                    ust = *ptr;
                    msc = *ptr2;
                    sbc = *ptr3;
                    return result;
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglWaitForMscOML(IntPtr hdc, long target_msc, long divisor, long remainder, [Out] IntPtr ust, [Out] IntPtr msc, [Out] IntPtr sbc)
    {
        return Delegates.wglWaitForMscOML(hdc, target_msc, divisor, remainder, (long*)(void*)ust, (long*)(void*)msc, (long*)(void*)sbc);
    }

    public unsafe static bool wglWaitForSbcOML(IntPtr hdc, long target_sbc, [Out] long[] ust, [Out] long[] msc, [Out] long[] sbc)
    {
        fixed (long* ust2 = ust)
        {
            fixed (long* msc2 = msc)
            {
                fixed (long* sbc2 = sbc)
                {
                    return Delegates.wglWaitForSbcOML(hdc, target_sbc, ust2, msc2, sbc2);
                }
            }
        }
    }

    public unsafe static bool wglWaitForSbcOML(IntPtr hdc, long target_sbc, out long ust, out long msc, out long sbc)
    {
        fixed (long* ptr = &ust)
        {
            fixed (long* ptr2 = &msc)
            {
                fixed (long* ptr3 = &sbc)
                {
                    bool result = Delegates.wglWaitForSbcOML(hdc, target_sbc, ptr, ptr2, ptr3);
                    ust = *ptr;
                    msc = *ptr2;
                    sbc = *ptr3;
                    return result;
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglWaitForSbcOML(IntPtr hdc, long target_sbc, [Out] IntPtr ust, [Out] IntPtr msc, [Out] IntPtr sbc)
    {
        return Delegates.wglWaitForSbcOML(hdc, target_sbc, (long*)(void*)ust, (long*)(void*)msc, (long*)(void*)sbc);
    }

    public unsafe static bool wglGetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, [Out] int[] piValue)
    {
        fixed (int* piValue2 = piValue)
        {
            return Delegates.wglGetDigitalVideoParametersI3D(hDC, iAttribute, piValue2);
        }
    }

    public unsafe static bool wglGetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, out int piValue)
    {
        fixed (int* ptr = &piValue)
        {
            bool result = Delegates.wglGetDigitalVideoParametersI3D(hDC, iAttribute, ptr);
            piValue = *ptr;
            return result;
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, [Out] IntPtr piValue)
    {
        return Delegates.wglGetDigitalVideoParametersI3D(hDC, iAttribute, (int*)(void*)piValue);
    }

    public unsafe static bool wglSetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, int[] piValue)
    {
        fixed (int* piValue2 = piValue)
        {
            return Delegates.wglSetDigitalVideoParametersI3D(hDC, iAttribute, piValue2);
        }
    }

    public unsafe static bool wglSetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, ref int piValue)
    {
        fixed (int* piValue2 = &piValue)
        {
            return Delegates.wglSetDigitalVideoParametersI3D(hDC, iAttribute, piValue2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglSetDigitalVideoParametersI3D(IntPtr hDC, int iAttribute, IntPtr piValue)
    {
        return Delegates.wglSetDigitalVideoParametersI3D(hDC, iAttribute, (int*)(void*)piValue);
    }

    public unsafe static bool wglGetGammaTableParametersI3D(IntPtr hDC, int iAttribute, [Out] int[] piValue)
    {
        fixed (int* piValue2 = piValue)
        {
            return Delegates.wglGetGammaTableParametersI3D(hDC, iAttribute, piValue2);
        }
    }

    public unsafe static bool wglGetGammaTableParametersI3D(IntPtr hDC, int iAttribute, out int piValue)
    {
        fixed (int* ptr = &piValue)
        {
            bool result = Delegates.wglGetGammaTableParametersI3D(hDC, iAttribute, ptr);
            piValue = *ptr;
            return result;
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGammaTableParametersI3D(IntPtr hDC, int iAttribute, [Out] IntPtr piValue)
    {
        return Delegates.wglGetGammaTableParametersI3D(hDC, iAttribute, (int*)(void*)piValue);
    }

    public unsafe static bool wglSetGammaTableParametersI3D(IntPtr hDC, int iAttribute, int[] piValue)
    {
        fixed (int* piValue2 = piValue)
        {
            return Delegates.wglSetGammaTableParametersI3D(hDC, iAttribute, piValue2);
        }
    }

    public unsafe static bool wglSetGammaTableParametersI3D(IntPtr hDC, int iAttribute, ref int piValue)
    {
        fixed (int* piValue2 = &piValue)
        {
            return Delegates.wglSetGammaTableParametersI3D(hDC, iAttribute, piValue2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglSetGammaTableParametersI3D(IntPtr hDC, int iAttribute, IntPtr piValue)
    {
        return Delegates.wglSetGammaTableParametersI3D(hDC, iAttribute, (int*)(void*)piValue);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGammaTableI3D(IntPtr hDC, int iEntries, [Out] ushort[] puRed, [Out] ushort[] puGreen, [Out] ushort[] puBlue)
    {
        fixed (ushort* puRed2 = puRed)
        {
            fixed (ushort* puGreen2 = puGreen)
            {
                fixed (ushort* puBlue2 = puBlue)
                {
                    return Delegates.wglGetGammaTableI3D(hDC, iEntries, puRed2, puGreen2, puBlue2);
                }
            }
        }
    }

    public unsafe static bool wglGetGammaTableI3D(IntPtr hDC, int iEntries, [Out] short[] puRed, [Out] short[] puGreen, [Out] short[] puBlue)
    {
        fixed (short* puRed2 = puRed)
        {
            fixed (short* puGreen2 = puGreen)
            {
                fixed (short* puBlue2 = puBlue)
                {
                    return Delegates.wglGetGammaTableI3D(hDC, iEntries, (ushort*)puRed2, (ushort*)puGreen2, (ushort*)puBlue2);
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGammaTableI3D(IntPtr hDC, int iEntries, out ushort puRed, out ushort puGreen, out ushort puBlue)
    {
        fixed (ushort* ptr = &puRed)
        {
            fixed (ushort* ptr2 = &puGreen)
            {
                fixed (ushort* ptr3 = &puBlue)
                {
                    bool result = Delegates.wglGetGammaTableI3D(hDC, iEntries, ptr, ptr2, ptr3);
                    puRed = *ptr;
                    puGreen = *ptr2;
                    puBlue = *ptr3;
                    return result;
                }
            }
        }
    }

    public unsafe static bool wglGetGammaTableI3D(IntPtr hDC, int iEntries, out short puRed, out short puGreen, out short puBlue)
    {
        fixed (short* ptr = &puRed)
        {
            fixed (short* ptr2 = &puGreen)
            {
                fixed (short* ptr3 = &puBlue)
                {
                    bool result = Delegates.wglGetGammaTableI3D(hDC, iEntries, (ushort*)ptr, (ushort*)ptr2, (ushort*)ptr3);
                    puRed = *ptr;
                    puGreen = *ptr2;
                    puBlue = *ptr3;
                    return result;
                }
            }
        }
    }

    public unsafe static bool wglGetGammaTableI3D(IntPtr hDC, int iEntries, [Out] IntPtr puRed, [Out] IntPtr puGreen, [Out] IntPtr puBlue)
    {
        return Delegates.wglGetGammaTableI3D(hDC, iEntries, (ushort*)(void*)puRed, (ushort*)(void*)puGreen, (ushort*)(void*)puBlue);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglSetGammaTableI3D(IntPtr hDC, int iEntries, ushort[] puRed, ushort[] puGreen, ushort[] puBlue)
    {
        fixed (ushort* puRed2 = puRed)
        {
            fixed (ushort* puGreen2 = puGreen)
            {
                fixed (ushort* puBlue2 = puBlue)
                {
                    return Delegates.wglSetGammaTableI3D(hDC, iEntries, puRed2, puGreen2, puBlue2);
                }
            }
        }
    }

    public unsafe static bool wglSetGammaTableI3D(IntPtr hDC, int iEntries, short[] puRed, short[] puGreen, short[] puBlue)
    {
        fixed (short* puRed2 = puRed)
        {
            fixed (short* puGreen2 = puGreen)
            {
                fixed (short* puBlue2 = puBlue)
                {
                    return Delegates.wglSetGammaTableI3D(hDC, iEntries, (ushort*)puRed2, (ushort*)puGreen2, (ushort*)puBlue2);
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglSetGammaTableI3D(IntPtr hDC, int iEntries, ref ushort puRed, ref ushort puGreen, ref ushort puBlue)
    {
        fixed (ushort* puRed2 = &puRed)
        {
            fixed (ushort* puGreen2 = &puGreen)
            {
                fixed (ushort* puBlue2 = &puBlue)
                {
                    return Delegates.wglSetGammaTableI3D(hDC, iEntries, puRed2, puGreen2, puBlue2);
                }
            }
        }
    }

    public unsafe static bool wglSetGammaTableI3D(IntPtr hDC, int iEntries, ref short puRed, ref short puGreen, ref short puBlue)
    {
        fixed (short* puRed2 = &puRed)
        {
            fixed (short* puGreen2 = &puGreen)
            {
                fixed (short* puBlue2 = &puBlue)
                {
                    return Delegates.wglSetGammaTableI3D(hDC, iEntries, (ushort*)puRed2, (ushort*)puGreen2, (ushort*)puBlue2);
                }
            }
        }
    }

    public unsafe static bool wglSetGammaTableI3D(IntPtr hDC, int iEntries, IntPtr puRed, IntPtr puGreen, IntPtr puBlue)
    {
        return Delegates.wglSetGammaTableI3D(hDC, iEntries, (ushort*)(void*)puRed, (ushort*)(void*)puGreen, (ushort*)(void*)puBlue);
    }

    public static bool wglEnableGenlockI3D(IntPtr hDC)
    {
        return Delegates.wglEnableGenlockI3D(hDC);
    }

    public static bool wglDisableGenlockI3D(IntPtr hDC)
    {
        return Delegates.wglDisableGenlockI3D(hDC);
    }

    public unsafe static bool wglIsEnabledGenlockI3D(IntPtr hDC, [Out] bool[] pFlag)
    {
        fixed (bool* pFlag2 = pFlag)
        {
            return Delegates.wglIsEnabledGenlockI3D(hDC, pFlag2);
        }
    }

    public unsafe static bool wglIsEnabledGenlockI3D(IntPtr hDC, out bool pFlag)
    {
        fixed (bool* ptr = &pFlag)
        {
            bool result = Delegates.wglIsEnabledGenlockI3D(hDC, ptr);
            pFlag = *ptr;
            return result;
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglIsEnabledGenlockI3D(IntPtr hDC, [Out] IntPtr pFlag)
    {
        return Delegates.wglIsEnabledGenlockI3D(hDC, (bool*)(void*)pFlag);
    }

    [CLSCompliant(false)]
    public static bool wglGenlockSourceI3D(IntPtr hDC, uint uSource)
    {
        return Delegates.wglGenlockSourceI3D(hDC, uSource);
    }

    public static bool wglGenlockSourceI3D(IntPtr hDC, int uSource)
    {
        return Delegates.wglGenlockSourceI3D(hDC, (uint)uSource);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGenlockSourceI3D(IntPtr hDC, [Out] uint[] uSource)
    {
        fixed (uint* uSource2 = uSource)
        {
            return Delegates.wglGetGenlockSourceI3D(hDC, uSource2);
        }
    }

    public unsafe static bool wglGetGenlockSourceI3D(IntPtr hDC, [Out] int[] uSource)
    {
        fixed (int* uSource2 = uSource)
        {
            return Delegates.wglGetGenlockSourceI3D(hDC, (uint*)uSource2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGenlockSourceI3D(IntPtr hDC, out uint uSource)
    {
        fixed (uint* ptr = &uSource)
        {
            bool result = Delegates.wglGetGenlockSourceI3D(hDC, ptr);
            uSource = *ptr;
            return result;
        }
    }

    public unsafe static bool wglGetGenlockSourceI3D(IntPtr hDC, out int uSource)
    {
        fixed (int* ptr = &uSource)
        {
            bool result = Delegates.wglGetGenlockSourceI3D(hDC, (uint*)ptr);
            uSource = *ptr;
            return result;
        }
    }

    public unsafe static bool wglGetGenlockSourceI3D(IntPtr hDC, [Out] IntPtr uSource)
    {
        return Delegates.wglGetGenlockSourceI3D(hDC, (uint*)(void*)uSource);
    }

    [CLSCompliant(false)]
    public static bool wglGenlockSourceEdgeI3D(IntPtr hDC, uint uEdge)
    {
        return Delegates.wglGenlockSourceEdgeI3D(hDC, uEdge);
    }

    public static bool wglGenlockSourceEdgeI3D(IntPtr hDC, int uEdge)
    {
        return Delegates.wglGenlockSourceEdgeI3D(hDC, (uint)uEdge);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGenlockSourceEdgeI3D(IntPtr hDC, [Out] uint[] uEdge)
    {
        fixed (uint* uEdge2 = uEdge)
        {
            return Delegates.wglGetGenlockSourceEdgeI3D(hDC, uEdge2);
        }
    }

    public unsafe static bool wglGetGenlockSourceEdgeI3D(IntPtr hDC, [Out] int[] uEdge)
    {
        fixed (int* uEdge2 = uEdge)
        {
            return Delegates.wglGetGenlockSourceEdgeI3D(hDC, (uint*)uEdge2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGenlockSourceEdgeI3D(IntPtr hDC, out uint uEdge)
    {
        fixed (uint* ptr = &uEdge)
        {
            bool result = Delegates.wglGetGenlockSourceEdgeI3D(hDC, ptr);
            uEdge = *ptr;
            return result;
        }
    }

    public unsafe static bool wglGetGenlockSourceEdgeI3D(IntPtr hDC, out int uEdge)
    {
        fixed (int* ptr = &uEdge)
        {
            bool result = Delegates.wglGetGenlockSourceEdgeI3D(hDC, (uint*)ptr);
            uEdge = *ptr;
            return result;
        }
    }

    public unsafe static bool wglGetGenlockSourceEdgeI3D(IntPtr hDC, [Out] IntPtr uEdge)
    {
        return Delegates.wglGetGenlockSourceEdgeI3D(hDC, (uint*)(void*)uEdge);
    }

    [CLSCompliant(false)]
    public static bool wglGenlockSampleRateI3D(IntPtr hDC, uint uRate)
    {
        return Delegates.wglGenlockSampleRateI3D(hDC, uRate);
    }

    public static bool wglGenlockSampleRateI3D(IntPtr hDC, int uRate)
    {
        return Delegates.wglGenlockSampleRateI3D(hDC, (uint)uRate);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGenlockSampleRateI3D(IntPtr hDC, [Out] uint[] uRate)
    {
        fixed (uint* uRate2 = uRate)
        {
            return Delegates.wglGetGenlockSampleRateI3D(hDC, uRate2);
        }
    }

    public unsafe static bool wglGetGenlockSampleRateI3D(IntPtr hDC, [Out] int[] uRate)
    {
        fixed (int* uRate2 = uRate)
        {
            return Delegates.wglGetGenlockSampleRateI3D(hDC, (uint*)uRate2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGenlockSampleRateI3D(IntPtr hDC, out uint uRate)
    {
        fixed (uint* ptr = &uRate)
        {
            bool result = Delegates.wglGetGenlockSampleRateI3D(hDC, ptr);
            uRate = *ptr;
            return result;
        }
    }

    public unsafe static bool wglGetGenlockSampleRateI3D(IntPtr hDC, out int uRate)
    {
        fixed (int* ptr = &uRate)
        {
            bool result = Delegates.wglGetGenlockSampleRateI3D(hDC, (uint*)ptr);
            uRate = *ptr;
            return result;
        }
    }

    public unsafe static bool wglGetGenlockSampleRateI3D(IntPtr hDC, [Out] IntPtr uRate)
    {
        return Delegates.wglGetGenlockSampleRateI3D(hDC, (uint*)(void*)uRate);
    }

    [CLSCompliant(false)]
    public static bool wglGenlockSourceDelayI3D(IntPtr hDC, uint uDelay)
    {
        return Delegates.wglGenlockSourceDelayI3D(hDC, uDelay);
    }

    public static bool wglGenlockSourceDelayI3D(IntPtr hDC, int uDelay)
    {
        return Delegates.wglGenlockSourceDelayI3D(hDC, (uint)uDelay);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGenlockSourceDelayI3D(IntPtr hDC, [Out] uint[] uDelay)
    {
        fixed (uint* uDelay2 = uDelay)
        {
            return Delegates.wglGetGenlockSourceDelayI3D(hDC, uDelay2);
        }
    }

    public unsafe static bool wglGetGenlockSourceDelayI3D(IntPtr hDC, [Out] int[] uDelay)
    {
        fixed (int* uDelay2 = uDelay)
        {
            return Delegates.wglGetGenlockSourceDelayI3D(hDC, (uint*)uDelay2);
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetGenlockSourceDelayI3D(IntPtr hDC, out uint uDelay)
    {
        fixed (uint* ptr = &uDelay)
        {
            bool result = Delegates.wglGetGenlockSourceDelayI3D(hDC, ptr);
            uDelay = *ptr;
            return result;
        }
    }

    public unsafe static bool wglGetGenlockSourceDelayI3D(IntPtr hDC, out int uDelay)
    {
        fixed (int* ptr = &uDelay)
        {
            bool result = Delegates.wglGetGenlockSourceDelayI3D(hDC, (uint*)ptr);
            uDelay = *ptr;
            return result;
        }
    }

    public unsafe static bool wglGetGenlockSourceDelayI3D(IntPtr hDC, [Out] IntPtr uDelay)
    {
        return Delegates.wglGetGenlockSourceDelayI3D(hDC, (uint*)(void*)uDelay);
    }

    [CLSCompliant(false)]
    public unsafe static bool wglQueryGenlockMaxSourceDelayI3D(IntPtr hDC, [Out] uint[] uMaxLineDelay, [Out] uint[] uMaxPixelDelay)
    {
        fixed (uint* uMaxLineDelay2 = uMaxLineDelay)
        {
            fixed (uint* uMaxPixelDelay2 = uMaxPixelDelay)
            {
                return Delegates.wglQueryGenlockMaxSourceDelayI3D(hDC, uMaxLineDelay2, uMaxPixelDelay2);
            }
        }
    }

    public unsafe static bool wglQueryGenlockMaxSourceDelayI3D(IntPtr hDC, [Out] int[] uMaxLineDelay, [Out] int[] uMaxPixelDelay)
    {
        fixed (int* uMaxLineDelay2 = uMaxLineDelay)
        {
            fixed (int* uMaxPixelDelay2 = uMaxPixelDelay)
            {
                return Delegates.wglQueryGenlockMaxSourceDelayI3D(hDC, (uint*)uMaxLineDelay2, (uint*)uMaxPixelDelay2);
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglQueryGenlockMaxSourceDelayI3D(IntPtr hDC, out uint uMaxLineDelay, out uint uMaxPixelDelay)
    {
        fixed (uint* ptr = &uMaxLineDelay)
        {
            fixed (uint* ptr2 = &uMaxPixelDelay)
            {
                bool result = Delegates.wglQueryGenlockMaxSourceDelayI3D(hDC, ptr, ptr2);
                uMaxLineDelay = *ptr;
                uMaxPixelDelay = *ptr2;
                return result;
            }
        }
    }

    public unsafe static bool wglQueryGenlockMaxSourceDelayI3D(IntPtr hDC, out int uMaxLineDelay, out int uMaxPixelDelay)
    {
        fixed (int* ptr = &uMaxLineDelay)
        {
            fixed (int* ptr2 = &uMaxPixelDelay)
            {
                bool result = Delegates.wglQueryGenlockMaxSourceDelayI3D(hDC, (uint*)ptr, (uint*)ptr2);
                uMaxLineDelay = *ptr;
                uMaxPixelDelay = *ptr2;
                return result;
            }
        }
    }

    public unsafe static bool wglQueryGenlockMaxSourceDelayI3D(IntPtr hDC, [Out] IntPtr uMaxLineDelay, [Out] IntPtr uMaxPixelDelay)
    {
        return Delegates.wglQueryGenlockMaxSourceDelayI3D(hDC, (uint*)(void*)uMaxLineDelay, (uint*)(void*)uMaxPixelDelay);
    }

    [CLSCompliant(false)]
    public static IntPtr wglCreateImageBufferI3D(IntPtr hDC, int dwSize, uint uFlags)
    {
        return Delegates.wglCreateImageBufferI3D(hDC, dwSize, uFlags);
    }

    public static IntPtr wglCreateImageBufferI3D(IntPtr hDC, int dwSize, int uFlags)
    {
        return Delegates.wglCreateImageBufferI3D(hDC, dwSize, (uint)uFlags);
    }

    public static bool wglDestroyImageBufferI3D(IntPtr hDC, IntPtr pAddress)
    {
        return Delegates.wglDestroyImageBufferI3D(hDC, pAddress);
    }

    public static bool wglDestroyImageBufferI3D(IntPtr hDC, [In][Out] object pAddress)
    {
        GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
        try
        {
            return Delegates.wglDestroyImageBufferI3D(hDC, gCHandle.AddrOfPinnedObject());
        }
        finally
        {
            gCHandle.Free();
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr[] pEvent, IntPtr pAddress, int[] pSize, uint count)
    {
        fixed (IntPtr* pEvent2 = pEvent)
        {
            fixed (int* pSize2 = pSize)
            {
                return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, pAddress, pSize2, count);
            }
        }
    }

    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr[] pEvent, IntPtr pAddress, int[] pSize, int count)
    {
        fixed (IntPtr* pEvent2 = pEvent)
        {
            fixed (int* pSize2 = pSize)
            {
                return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, pAddress, pSize2, (uint)count);
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr[] pEvent, [In][Out] object pAddress, int[] pSize, uint count)
    {
        fixed (IntPtr* pEvent2 = pEvent)
        {
            fixed (int* pSize2 = pSize)
            {
                GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
                try
                {
                    return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, gCHandle.AddrOfPinnedObject(), pSize2, count);
                }
                finally
                {
                    gCHandle.Free();
                }
            }
        }
    }

    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr[] pEvent, [In][Out] object pAddress, int[] pSize, int count)
    {
        fixed (IntPtr* pEvent2 = pEvent)
        {
            fixed (int* pSize2 = pSize)
            {
                GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
                try
                {
                    return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, gCHandle.AddrOfPinnedObject(), pSize2, (uint)count);
                }
                finally
                {
                    gCHandle.Free();
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr[] pEvent, [In][Out] object pAddress, ref int pSize, uint count)
    {
        fixed (IntPtr* pEvent2 = pEvent)
        {
            fixed (int* pSize2 = &pSize)
            {
                GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
                try
                {
                    return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, gCHandle.AddrOfPinnedObject(), pSize2, count);
                }
                finally
                {
                    gCHandle.Free();
                }
            }
        }
    }

    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr[] pEvent, [In][Out] object pAddress, ref int pSize, int count)
    {
        fixed (IntPtr* pEvent2 = pEvent)
        {
            fixed (int* pSize2 = &pSize)
            {
                GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
                try
                {
                    return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, gCHandle.AddrOfPinnedObject(), pSize2, (uint)count);
                }
                finally
                {
                    gCHandle.Free();
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, ref IntPtr pEvent, [In][Out] object pAddress, int[] pSize, uint count)
    {
        fixed (IntPtr* pEvent2 = &pEvent)
        {
            fixed (int* pSize2 = pSize)
            {
                GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
                try
                {
                    return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, gCHandle.AddrOfPinnedObject(), pSize2, count);
                }
                finally
                {
                    gCHandle.Free();
                }
            }
        }
    }

    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, ref IntPtr pEvent, [In][Out] object pAddress, int[] pSize, int count)
    {
        fixed (IntPtr* pEvent2 = &pEvent)
        {
            fixed (int* pSize2 = pSize)
            {
                GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
                try
                {
                    return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, gCHandle.AddrOfPinnedObject(), pSize2, (uint)count);
                }
                finally
                {
                    gCHandle.Free();
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, ref IntPtr pEvent, [In][Out] object pAddress, ref int pSize, uint count)
    {
        fixed (IntPtr* pEvent2 = &pEvent)
        {
            fixed (int* pSize2 = &pSize)
            {
                GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
                try
                {
                    return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, gCHandle.AddrOfPinnedObject(), pSize2, count);
                }
                finally
                {
                    gCHandle.Free();
                }
            }
        }
    }

    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, ref IntPtr pEvent, [In][Out] object pAddress, ref int pSize, int count)
    {
        fixed (IntPtr* pEvent2 = &pEvent)
        {
            fixed (int* pSize2 = &pSize)
            {
                GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
                try
                {
                    return Delegates.wglAssociateImageBufferEventsI3D(hDC, pEvent2, gCHandle.AddrOfPinnedObject(), pSize2, (uint)count);
                }
                finally
                {
                    gCHandle.Free();
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr pEvent, [In][Out] object pAddress, IntPtr pSize, uint count)
    {
        GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
        try
        {
            return Delegates.wglAssociateImageBufferEventsI3D(hDC, (IntPtr*)(void*)pEvent, gCHandle.AddrOfPinnedObject(), (int*)(void*)pSize, count);
        }
        finally
        {
            gCHandle.Free();
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr pEvent, [In][Out] object pAddress, IntPtr pSize, int count)
    {
        GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
        try
        {
            return Delegates.wglAssociateImageBufferEventsI3D(hDC, (IntPtr*)(void*)pEvent, gCHandle.AddrOfPinnedObject(), (int*)(void*)pSize, (uint)count);
        }
        finally
        {
            gCHandle.Free();
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr pEvent, [In][Out] object pAddress, int[] pSize, uint count)
    {
        fixed (int* pSize2 = pSize)
        {
            GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
            try
            {
                return Delegates.wglAssociateImageBufferEventsI3D(hDC, (IntPtr*)(void*)pEvent, gCHandle.AddrOfPinnedObject(), pSize2, count);
            }
            finally
            {
                gCHandle.Free();
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr pEvent, [In][Out] object pAddress, int[] pSize, int count)
    {
        fixed (int* pSize2 = pSize)
        {
            GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
            try
            {
                return Delegates.wglAssociateImageBufferEventsI3D(hDC, (IntPtr*)(void*)pEvent, gCHandle.AddrOfPinnedObject(), pSize2, (uint)count);
            }
            finally
            {
                gCHandle.Free();
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr pEvent, [In][Out] object pAddress, ref int pSize, uint count)
    {
        fixed (int* pSize2 = &pSize)
        {
            GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
            try
            {
                return Delegates.wglAssociateImageBufferEventsI3D(hDC, (IntPtr*)(void*)pEvent, gCHandle.AddrOfPinnedObject(), pSize2, count);
            }
            finally
            {
                gCHandle.Free();
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglAssociateImageBufferEventsI3D(IntPtr hDC, IntPtr pEvent, [In][Out] object pAddress, ref int pSize, int count)
    {
        fixed (int* pSize2 = &pSize)
        {
            GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
            try
            {
                return Delegates.wglAssociateImageBufferEventsI3D(hDC, (IntPtr*)(void*)pEvent, gCHandle.AddrOfPinnedObject(), pSize2, (uint)count);
            }
            finally
            {
                gCHandle.Free();
            }
        }
    }

    [CLSCompliant(false)]
    public static bool wglReleaseImageBufferEventsI3D(IntPtr hDC, IntPtr pAddress, uint count)
    {
        return Delegates.wglReleaseImageBufferEventsI3D(hDC, pAddress, count);
    }

    public static bool wglReleaseImageBufferEventsI3D(IntPtr hDC, IntPtr pAddress, int count)
    {
        return Delegates.wglReleaseImageBufferEventsI3D(hDC, pAddress, (uint)count);
    }

    [CLSCompliant(false)]
    public static bool wglReleaseImageBufferEventsI3D(IntPtr hDC, [In][Out] object pAddress, uint count)
    {
        GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
        try
        {
            return Delegates.wglReleaseImageBufferEventsI3D(hDC, gCHandle.AddrOfPinnedObject(), count);
        }
        finally
        {
            gCHandle.Free();
        }
    }

    public static bool wglReleaseImageBufferEventsI3D(IntPtr hDC, [In][Out] object pAddress, int count)
    {
        GCHandle gCHandle = GCHandle.Alloc(pAddress, GCHandleType.Pinned);
        try
        {
            return Delegates.wglReleaseImageBufferEventsI3D(hDC, gCHandle.AddrOfPinnedObject(), (uint)count);
        }
        finally
        {
            gCHandle.Free();
        }
    }

    public static bool wglEnableFrameLockI3D()
    {
        return Delegates.wglEnableFrameLockI3D();
    }

    public static bool wglDisableFrameLockI3D()
    {
        return Delegates.wglDisableFrameLockI3D();
    }

    public unsafe static bool wglIsEnabledFrameLockI3D([Out] bool[] pFlag)
    {
        fixed (bool* pFlag2 = pFlag)
        {
            return Delegates.wglIsEnabledFrameLockI3D(pFlag2);
        }
    }

    public unsafe static bool wglIsEnabledFrameLockI3D(out bool pFlag)
    {
        fixed (bool* ptr = &pFlag)
        {
            bool result = Delegates.wglIsEnabledFrameLockI3D(ptr);
            pFlag = *ptr;
            return result;
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglIsEnabledFrameLockI3D([Out] IntPtr pFlag)
    {
        return Delegates.wglIsEnabledFrameLockI3D((bool*)(void*)pFlag);
    }

    public unsafe static bool wglQueryFrameLockMasterI3D([Out] bool[] pFlag)
    {
        fixed (bool* pFlag2 = pFlag)
        {
            return Delegates.wglQueryFrameLockMasterI3D(pFlag2);
        }
    }

    public unsafe static bool wglQueryFrameLockMasterI3D(out bool pFlag)
    {
        fixed (bool* ptr = &pFlag)
        {
            bool result = Delegates.wglQueryFrameLockMasterI3D(ptr);
            pFlag = *ptr;
            return result;
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglQueryFrameLockMasterI3D([Out] IntPtr pFlag)
    {
        return Delegates.wglQueryFrameLockMasterI3D((bool*)(void*)pFlag);
    }

    public unsafe static bool wglGetFrameUsageI3D([Out] float[] pUsage)
    {
        fixed (float* pUsage2 = pUsage)
        {
            return Delegates.wglGetFrameUsageI3D(pUsage2);
        }
    }

    public unsafe static bool wglGetFrameUsageI3D(out float pUsage)
    {
        fixed (float* ptr = &pUsage)
        {
            bool result = Delegates.wglGetFrameUsageI3D(ptr);
            pUsage = *ptr;
            return result;
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglGetFrameUsageI3D([Out] IntPtr pUsage)
    {
        return Delegates.wglGetFrameUsageI3D((float*)(void*)pUsage);
    }

    public static bool wglBeginFrameTrackingI3D()
    {
        return Delegates.wglBeginFrameTrackingI3D();
    }

    public static bool wglEndFrameTrackingI3D()
    {
        return Delegates.wglEndFrameTrackingI3D();
    }

    public unsafe static bool wglQueryFrameTrackingI3D([Out] int[] pFrameCount, [Out] int[] pMissedFrames, [Out] float[] pLastMissedUsage)
    {
        fixed (int* pFrameCount2 = pFrameCount)
        {
            fixed (int* pMissedFrames2 = pMissedFrames)
            {
                fixed (float* pLastMissedUsage2 = pLastMissedUsage)
                {
                    return Delegates.wglQueryFrameTrackingI3D(pFrameCount2, pMissedFrames2, pLastMissedUsage2);
                }
            }
        }
    }

    public unsafe static bool wglQueryFrameTrackingI3D(out int pFrameCount, out int pMissedFrames, out float pLastMissedUsage)
    {
        fixed (int* ptr = &pFrameCount)
        {
            fixed (int* ptr2 = &pMissedFrames)
            {
                fixed (float* ptr3 = &pLastMissedUsage)
                {
                    bool result = Delegates.wglQueryFrameTrackingI3D(ptr, ptr2, ptr3);
                    pFrameCount = *ptr;
                    pMissedFrames = *ptr2;
                    pLastMissedUsage = *ptr3;
                    return result;
                }
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe static bool wglQueryFrameTrackingI3D([Out] IntPtr pFrameCount, [Out] IntPtr pMissedFrames, [Out] IntPtr pLastMissedUsage)
    {
        return Delegates.wglQueryFrameTrackingI3D((int*)(void*)pFrameCount, (int*)(void*)pMissedFrames, (float*)(void*)pLastMissedUsage);
    }

    static Wgl()
    {
        sb = new StringBuilder();
        gl_lock = new object();
        AvailableExtensions = new SortedList<string, bool>();
        glClass = typeof(Wgl);
        delegatesClass = glClass.GetNestedType("Delegates", BindingFlags.Static | BindingFlags.NonPublic);
        importsClass = glClass.GetNestedType("Imports", BindingFlags.Static | BindingFlags.NonPublic);
        if (Imports.FunctionMap != null)
        {
        }

        ReloadFunctions();
    }

    public static Delegate GetDelegate(string name, Type signature)
    {
        MethodInfo value;
        return GetExtensionDelegate(name, signature) ?? (Imports.FunctionMap.TryGetValue(name.Substring(3), out value) ? Delegate.CreateDelegate(signature, value) : null);
    }

    public static void ReloadFunctions()
    {
        if (delegates == null)
        {
            delegates = delegatesClass.GetFields(BindingFlags.Static | BindingFlags.NonPublic);
        }

        FieldInfo[] array = delegates;
        foreach (FieldInfo fieldInfo in array)
        {
            fieldInfo.SetValue(null, GetDelegate(fieldInfo.Name, fieldInfo.FieldType));
        }

        rebuildExtensionList = true;
    }

    private static void set(object d, Delegate value)
    {
        d = value;
    }

    public static bool Load(string function)
    {
        FieldInfo field = delegatesClass.GetField(function, BindingFlags.Static | BindingFlags.NonPublic);
        if ((object)field == null)
        {
            return false;
        }

        Delegate @delegate = field.GetValue(null) as Delegate;
        Delegate delegate2 = GetDelegate(field.Name, field.FieldType);
        if (@delegate.Target != delegate2.Target)
        {
            field.SetValue(null, delegate2);
        }

        return (object)delegate2 != null;
    }

    internal static Delegate GetExtensionDelegate(string name, Type signature)
    {
        IntPtr procAddress = Imports.GetProcAddress(name);
        if (procAddress == IntPtr.Zero || procAddress == new IntPtr(1) || procAddress == new IntPtr(2))
        {
            return null;
        }

        return Marshal.GetDelegateForFunctionPointer(procAddress, signature);
    }

    public static bool IsExtensionSupported(string name)
    {
        if (rebuildExtensionList)
        {
            BuildExtensionList();
        }

        lock (gl_lock)
        {
            sb.Remove(0, sb.Length);
            if (!name.StartsWith("WGL_"))
            {
                sb.Append("wgl_");
            }

            sb.Append(name.ToLower());
            return AvailableExtensions.ContainsKey(sb.ToString());
        }
    }

    internal static void BuildExtensionList()
    {
        AvailableExtensions.Clear();
        string text = "";
        try
        {
            text = wglGetExtensionsStringARB(wglGetCurrentDC());
        }
        catch (NullReferenceException)
        {
        }

        if (!string.IsNullOrEmpty(text))
        {
            string[] array = text.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] array2 = array;
            foreach (string text2 in array2)
            {
                AvailableExtensions.Add(text2.ToLower(), value: true);
            }
        }
    }
}