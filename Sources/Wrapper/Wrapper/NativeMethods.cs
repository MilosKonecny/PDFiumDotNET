using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("PDFiumDotNET.Wrapper.Test")]
[assembly: InternalsVisibleTo("PDFiumDotNET.Components")]

namespace PDFiumDotNET.Wrapper
{
    /// <summary>
    /// Class contains win api native methods.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// The function allocates a buffer large enough to hold the formatted message, and places a pointer to the allocated buffer at the address specified by lpBuffer.
        /// Used in <see cref="FormatMessage"/> as flag.
        /// </summary>
        internal const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;

        /// <summary>
        /// Insert sequences in the message definition are to be ignored and passed through to the output buffer unchanged.
        /// Used in <see cref="FormatMessage"/> as flag.
        /// </summary>
        internal const uint FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;

        /// <summary>
        /// The function should search the system message-table resource(s) for the requested message.
        /// Used in <see cref="FormatMessage"/> as flag.
        /// </summary>
        internal const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

        /// <summary>
        /// The Arguments parameter is not a va_list structure, but is a pointer to an array of values that represent the arguments.
        /// Used in <see cref="FormatMessage"/> as flag.
        /// </summary>
        internal const uint FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000;

        /// <summary>
        /// The lpSource parameter is a module handle containing the message-table resource(s) to search.
        /// Used in <see cref="FormatMessage"/> as flag.
        /// </summary>
        internal const uint FORMAT_MESSAGE_FROM_HMODULE = 0x00000800;

        /// <summary>
        /// The lpSource parameter is a pointer to a null-terminated string that contains a message definition.
        /// Used in <see cref="FormatMessage"/> as flag.
        /// </summary>
        internal const uint FORMAT_MESSAGE_FROM_STRING = 0x00000400;

        /// <summary>
        /// Retrieves the calling thread's last-error code value.
        /// The last-error code is maintained on a per-thread basis.
        /// Multiple threads do not overwrite each other's last-error code.
        /// </summary>
        /// <returns>The return value is the calling thread's last-error code.</returns>
        [DllImport("kernel32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern uint GetLastError();

        /// <summary>
        /// Formats a message string.
        /// </summary>
        /// <param name="dwFlags">The formatting options, and how to interpret the lpSource parameter.</param>
        /// <param name="lpSource">The location of the message definition.</param>
        /// <param name="dwMessageId">The message identifier for the requested message.</param>
        /// <param name="dwLanguageId">The language identifier for the requested message.</param>
        /// <param name="lpBuffer">A pointer to a buffer that receives the null-terminated string that specifies the formatted message.</param>
        /// <param name="nSize">
        /// If the FORMAT_MESSAGE_ALLOCATE_BUFFER flag is not set, this parameter specifies the size of the output buffer, in TCHARs.
        /// If FORMAT_MESSAGE_ALLOCATE_BUFFER is set, this parameter specifies the minimum number of TCHARs to allocate for an output buffer.
        /// </param>
        /// <param name="arguments">An array of values that are used as insert values in the formatted message.</param>
        /// <returns>
        /// If the function succeeds, the return value is the number of TCHARs stored in the output buffer, excluding the terminating null character.
        /// If the function fails, the return value is zero.To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("Kernel32.dll", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, string[] arguments);

        /// <summary>
        /// Frees the specified local memory object and invalidates its handle.
        /// </summary>
        /// <param name="hMem">A handle to the local memory object.</param>
        /// <returns>
        /// If the function succeeds, the return value is NULL.
        /// If the function fails, the return value is equal to a handle to the local memory object. To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport("kernel32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="lpLibFileName">The name of the module.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the module.
        /// If the function fails, the return value is NULL.To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary", SetLastError = true, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="hModule">A handle to the DLL module that contains the function or variable.</param>
        /// <param name="lpProcName">The function or variable name, or the function's ordinal value.
        /// If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.</param>
        /// <returns>
        /// If the function succeeds, the return value is the address of the exported function or variable.
        /// If the function fails, the return value is NULL.To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments
        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true, CharSet = CharSet.Ansi)]
#pragma warning restore CA2101 // Specify marshaling for P/Invoke string arguments
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern IntPtr GetProcAddressAnsi(IntPtr hModule, string lpProcName);

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="hModule">A handle to the DLL module that contains the function or variable.</param>
        /// <param name="lpProcName">The function or variable name, or the function's ordinal value.
        /// If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.</param>
        /// <returns>
        /// If the function succeeds, the return value is the address of the exported function or variable.
        /// If the function fails, the return value is NULL.To get extended error information, call <see cref="GetLastError"/>.
        /// </returns>
        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern IntPtr GetProcAddressUnicode(IntPtr hModule, string lpProcName);

        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// When the reference count reaches zero, the module is unloaded from the address space of the calling process and the handle is no longer valid.
        /// </summary>
        /// <param name="hLibModule">A handle to the loaded library module.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.To get extended error information, call the <see cref="GetLastError"/> function.
        /// </returns>
        [DllImport("kernel32.dll")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern bool FreeLibrary(IntPtr hLibModule);

        /// <summary>
        /// Converts the struct array to <see cref="IntPtr"/> to provide parameter for win api functions.
        /// </summary>
        /// <typeparam name="T">Type of structure.</typeparam>
        /// <param name="array">Array to convert.</param>
        /// <returns>Converted array.</returns>
        public static IntPtr StructureArrayToIntPtr<T>(T[] array)
            where T : struct
        {
            int structSize = Marshal.SizeOf(typeof(T));
            int size = array.Length * structSize;
            IntPtr ptr = Marshal.AllocHGlobal(size);
            for (var index = 0; index < array.Length; index++)
            {
                Marshal.StructureToPtr(array[index], ptr + (index * structSize), false);
            }

            return ptr;
        }
    }
}
