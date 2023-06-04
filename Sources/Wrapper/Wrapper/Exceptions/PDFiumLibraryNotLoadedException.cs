namespace PDFiumDotNET.Wrapper.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception class used in case when PDFium DLL was not found.
    /// </summary>
    [Serializable]
    public class PDFiumLibraryNotLoadedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFiumLibraryNotLoadedException"/> class.
        /// </summary>
        public PDFiumLibraryNotLoadedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFiumLibraryNotLoadedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PDFiumLibraryNotLoadedException([Localizable(false)] string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFiumLibraryNotLoadedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PDFiumLibraryNotLoadedException([Localizable(false)] string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFiumLibraryNotLoadedException"/> class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected PDFiumLibraryNotLoadedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Creates an <see cref="PDFiumLibraryNotLoadedException"/> exception with the information that the library was found.
        /// </summary>
        /// <param name="libraryNames">Library names tried to load.</param>
        /// <returns>Created exception.</returns>
        public static PDFiumLibraryNotLoadedException CreateException(List<string> libraryNames)
        {
            var names = libraryNames.Aggregate(string.Empty, (agg, next) => agg + (string.IsNullOrEmpty(agg) ? string.Empty : ", ") + "'" + next + "'");
            return new PDFiumLibraryNotLoadedException($"No DLL was found. The following were tried to load: {names}.");
        }

        /// <summary>
        /// Creates <see cref="PDFiumLibraryNotLoadedException"/> exception. Explanation of last system error is set as message.
        /// </summary>
        /// <param name="library">Name of not loaded library.</param>
        /// <param name="lastWinError">Last system error to use to determine exception message.</param>
        /// <returns>Created exception.</returns>
        public static PDFiumLibraryNotLoadedException CreateException(string library, int lastWinError)
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

            return new PDFiumLibraryNotLoadedException($"DLL '{library}' not loaded. System error: {lastWinError} - {message}");
        }
    }
}
