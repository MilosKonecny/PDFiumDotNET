namespace GetLastError.App.NetCore
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [DllImport("GetLastError.Library.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr FPDF_LoadDocument([MarshalAs(UnmanagedType.LPStr)] string file_path, [MarshalAs(UnmanagedType.LPStr)] string password);

        [DllImport("GetLastError.Library.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int FPDF_GetLastError();
    }
}
