namespace PDFiumDotNET.Apps.Common
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The helper class implements collecting information about memory usage of current process.
    /// </summary>
    public class MemoryUsage
    {
        #region Private fields

        private long _currentMemoryUsage;
        private long _minimumMemoryUsage = long.MaxValue;
        private long _maximumMemoryUsage;

        #endregion Private fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryUsage"/> class.
        /// </summary>
        public MemoryUsage()
        {
            GatherMemoryUsage();
        }

        #endregion Constructor

        #region Public properties

        /// <summary>
        /// Gets the current memory usage of current process.
        /// </summary>
        public long CurrentMemoryUsage
        {
            get
            {
                return _currentMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the minimum memory usage of current process since this class was instantiated.
        /// </summary>
        public long MinimumMemoryUsage
        {
            get
            {
                return _minimumMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the maximum memory usage of current process since this class was instantiated.
        /// </summary>
        public long MaximumMemoryUsage
        {
            get
            {
                return _maximumMemoryUsage;
            }
        }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// The method gather all memory usage information.
        /// </summary>
        public void GatherMemoryUsage()
        {
            _currentMemoryUsage = Process.GetCurrentProcess().PrivateMemorySize64 / 1024;
            _minimumMemoryUsage = Math.Min(_minimumMemoryUsage, _currentMemoryUsage);
            _maximumMemoryUsage = Math.Max(_maximumMemoryUsage, _currentMemoryUsage);
        }

        #endregion Public methods
    }
}
