namespace PDFiumDotNET.WpfControls.WritableBitmapExtension
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Static class imports native functions and implements unsafe methods.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Copies bytes between buffers.
        /// </summary>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="src">Source buffer.</param>
        /// <param name="count">Number of bytes to copy.</param>
        /// <returns>The destination buffer.</returns>
        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern unsafe byte* memcpy(byte* dst, byte* src, int count);

        /// <summary>
        /// Sets a buffer to a specified byte.
        /// </summary>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="filler">Value to set.</param>
        /// <param name="count">Number of bytes to set.</param>
        [DllImport("msvcrt.dll", EntryPoint = "memset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern void memset(IntPtr dst, int filler, int count);

        /// <summary>
        /// Copies bytes between buffers.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="srcOffset">Offset in source buffer where is start of the data to copy.</param>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="dstOffset">Offset in destination buffer where is start of buffer to start with copy.</param>
        /// <param name="count">Number of bytes to copy.</param>
        internal static unsafe void CopyMemory(byte* src, int srcOffset, byte* dst, int dstOffset, int count)
        {
            src += srcOffset;
            dst += dstOffset;

            memcpy(dst, src, count);
        }

        /// <summary>
        /// Sets a buffer to a specified byte.
        /// </summary>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="filler">Value to set.</param>
        /// <param name="count">Number of bytes to set.</param>
        internal static void SetMemory(IntPtr dst, int filler, int count)
        {
            memset(dst, filler, count);
        }
    }
}
