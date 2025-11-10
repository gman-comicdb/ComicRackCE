using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace cYo.Common.Presentation.Tao.TaoOpenGL;

public static class Kernel
{
    public struct MEMORYSTATUS
    {
        public int Length;

        public int MemoryLoad;

        public int TotalPhys;

        public int AvailPhys;

        public int TotalPageFile;

        public int AvailPageFile;

        public int TotalVirtual;

        public int AvailVirtual;
    }

    public struct SYSTEM_INFO
    {
        public SYSTEM_INFO_UNION SystemInfoUnion;

        public int PageSize;

        public IntPtr MinimumApplicationAddress;

        public IntPtr MaximumApplicationAddress;

        public int ActiveProcessorMask;

        public int NumberOfProcessors;

        public int ProcessorType;

        public int AllocationGranularity;

        public int ProcessorLevel;

        public int ProcessorRevision;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct SYSTEM_INFO_UNION
    {
        [FieldOffset(0)]
        public int OemId;

        [FieldOffset(0)]
        public short ProcessorArchitecture;

        [FieldOffset(2)]
        public short Reserved;
    }

    private const string KERNEL_NATIVE_LIBRARY = "kernel32.dll";

    private const CallingConvention CALLING_CONVENTION = CallingConvention.StdCall;

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool Beep(int frequency, int duration);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool FreeLibrary(IntPtr moduleHandle);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int GetDllDirectory(int bufferLength, [Out] StringBuilder buffer);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int GetModuleFileName(IntPtr module, [Out] StringBuilder fileName, int size);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern IntPtr GetModuleHandle(string moduleName);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern IntPtr GetProcAddress(IntPtr module, string processName);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool GetProcessWorkingSetSize(IntPtr process, out int minimumWorkingSetSize, out int maximumWorkingSetSize);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int GetSystemDirectory([Out] StringBuilder buffer, int size);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void GetSystemInfo(out SYSTEM_INFO systemInfo);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int GetSystemWindowsDirectory([Out] StringBuilder buffer, int size);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int GetTickCount();

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int GetWindowsDirectory([Out] StringBuilder buffer, int size);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
    [SuppressUnmanagedCodeSecurity]
    public static extern void GlobalMemoryStatus(out MEMORYSTATUS buffer);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool IsProcessorFeaturePresent(int processorFeature);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern IntPtr LoadLibrary(string fileName);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool QueryPerformanceCounter(out long performanceCount);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "QueryPerformanceCounter")]
    [SuppressUnmanagedCodeSecurity]
    public static extern int QueryPerformanceCounterFast(out long performanceCount);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool QueryPerformanceFrequency(out long frequency);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool SetDllDirectory(string pathName);

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
}