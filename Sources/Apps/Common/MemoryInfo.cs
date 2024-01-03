namespace PDFiumDotNET.Apps.Common
{
    using System;

    /// <summary>
    /// The class contains information about memory usage.
    /// </summary>
    public class MemoryInfo
    {
        #region Private fields

        private long _currentMemoryUsage = 0;
        private long _minimumMemoryUsage = long.MaxValue;
        private long _maximumMemoryUsage = long.MinValue;

        #endregion Private fields

        #region Public properties

        /// <summary>
        /// Gets the current memory usage.
        /// </summary>
        public long CurrentMemoryUsage
        {
            get
            {
                return _currentMemoryUsage;
            }

            internal set
            {
                _currentMemoryUsage = value;
                _minimumMemoryUsage = Math.Min(_minimumMemoryUsage, _currentMemoryUsage);
                _maximumMemoryUsage = Math.Max(_maximumMemoryUsage, _currentMemoryUsage);
            }
        }

        /// <summary>
        /// Gets the minimum memory usage.
        /// </summary>
        public long MinimumMemoryUsage
        {
            get
            {
                return _minimumMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the maximum memory usage.
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
        /// The method resets all memory usage properties.
        /// </summary>
        public void Reset()
        {
            _currentMemoryUsage = 0;
            _minimumMemoryUsage = long.MaxValue;
            _maximumMemoryUsage = long.MinValue;
        }

        #endregion Public methods
    }
}
