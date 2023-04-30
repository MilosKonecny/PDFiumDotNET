namespace PDFiumDotNET.Apps.Common
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The class provides build information.
    /// </summary>
    public static class CommonInformation
    {
        /// <summary>
        /// Gets the build information.
        /// </summary>
        public static string Info
        {
            get
            {
                return Framework + " / " + Framework + " / " + Configuration;
            }
        }

        /// <summary>
        /// Gets the build configuration.
        /// </summary>
        public static string Configuration
        {
            get
            {
#if DEBUG
                return "Debug";
#elif RELEASE
                return "Release";
#else
                return "? ? ?";
#endif
            }
        }

        /// <summary>
        /// Gets the framework text.
        /// </summary>
        public static string Framework
        {
            get
            {
                return RuntimeInformation.FrameworkDescription;
            }
        }

        /// <summary>
        /// Gets the architecture text.
        /// </summary>
        public static string Architecture
        {
            get
            {
                return RuntimeInformation.ProcessArchitecture.ToString();
            }
        }
    }
}
