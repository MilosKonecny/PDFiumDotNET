namespace PDFiumDotNET.Wrapper.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception class used in case when some function was not found in PDFium DLL.
    /// </summary>
    public class PDFiumFunctionNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFiumFunctionNotFoundException"/> class.
        /// </summary>
        public PDFiumFunctionNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFiumFunctionNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PDFiumFunctionNotFoundException([Localizable(false)] string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFiumFunctionNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PDFiumFunctionNotFoundException([Localizable(false)] string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates <see cref="PDFiumFunctionNotFoundException"/> exception. Explanation of last system error is set as message.
        /// </summary>
        /// <param name="library">Name of not loaded library.</param>
        /// <param name="function">The name of the function that was not found.</param>
        /// <param name="lastWinError">Last system error to use to determine exception message.</param>
        /// <returns>Created exception.</returns>
        public static PDFiumFunctionNotFoundException CreateException(string library, string function, int lastWinError)
        {
            var message = string.Empty;
            var lpMsgBuf = IntPtr.Zero;
            var paramList = new List<string> { library };

            uint dwChars = NativeMethods.FormatMessage(
                NativeMethods.FORMAT_MESSAGE_ALLOCATE_BUFFER | NativeMethods.FORMAT_MESSAGE_FROM_SYSTEM | NativeMethods.FORMAT_MESSAGE_ARGUMENT_ARRAY,
                IntPtr.Zero,
                (uint)lastWinError,
                0,
                ref lpMsgBuf,
                0,
                paramList.ToArray());
            if (dwChars != 0)
            {
                message = Marshal.PtrToStringAnsi(lpMsgBuf).Trim();
                NativeMethods.LocalFree(lpMsgBuf);
            }

            return new PDFiumFunctionNotFoundException($"Function '{function}' in '{library}' not found. System error: {lastWinError} - {message}");
        }
    }
}
