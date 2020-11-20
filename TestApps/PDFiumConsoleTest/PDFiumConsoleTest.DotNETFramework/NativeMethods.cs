namespace PDFiumConsoleTest.DotNETFramework
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [DllImport("pdfium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FPDF_InitLibrary();

        [DllImport("pdfium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FPDF_DestroyLibrary();

        [DllImport("pdfium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr FPDF_LoadDocument([MarshalAs(UnmanagedType.LPStr)] string file_path, [MarshalAs(UnmanagedType.LPStr)] string password);

        [DllImport("pdfium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FPDF_CloseDocument(IntPtr document);

        [DllImport("pdfium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int FPDF_GetLastError();
    }
}
