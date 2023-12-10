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

        private long _currentPrivateMemoryUsage;
        private long _minimumPrivateMemoryUsage = long.MaxValue;
        private long _maximumPrivateMemoryUsage;
        private long _currentVirtualMemoryUsage;
        private long _minimumVirtualMemoryUsage = long.MaxValue;
        private long _maximumVirtualMemoryUsage;
        private long _currentWorkingSetMemoryUsage;
        private long _minimumWorkingSetMemoryUsage = long.MaxValue;
        private long _maximumWorkingSetMemoryUsage;

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
        /// Gets the current private memory usage of current process.
        /// (Private memory allocated for the associated process.)
        /// </summary>
        public long CurrentPrivateMemoryUsage
        {
            get
            {
                return _currentPrivateMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the minimum private memory usage of current process since this class was instantiated.
        /// </summary>
        public long MinimumPrivateMemoryUsage
        {
            get
            {
                return _minimumPrivateMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the maximum private memory usage of current process since this class was instantiated.
        /// </summary>
        public long MaximumPrivateMemoryUsage
        {
            get
            {
                return _maximumPrivateMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the current virtual memory usage of current process.
        /// (Virtual memory allocated for the associated process.)
        /// </summary>
        public long CurrentVirtualMemoryUsage
        {
            get
            {
                return _currentVirtualMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the minimum virtual memory usage of current process since this class was instantiated.
        /// </summary>
        public long MinimumVirtualMemoryUsage
        {
            get
            {
                return _minimumVirtualMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the maximum virtual memory usage of current process since this class was instantiated.
        /// </summary>
        public long MaximumVirtualMemoryUsage
        {
            get
            {
                return _maximumVirtualMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the current working set memory usage of current process.
        /// (Physical memory allocated for the associated process.)
        /// </summary>
        public long CurrentWorkingSetMemoryUsage
        {
            get
            {
                return _currentWorkingSetMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the minimum working set memory usage of current process since this class was instantiated.
        /// </summary>
        public long MinimumWorkingSetMemoryUsage
        {
            get
            {
                return _minimumWorkingSetMemoryUsage;
            }
        }

        /// <summary>
        /// Gets the maximum working set memory usage of current process since this class was instantiated.
        /// </summary>
        public long MaximumWorkingSetMemoryUsage
        {
            get
            {
                return _maximumWorkingSetMemoryUsage;
            }
        }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// The method gather all memory usage information.
        /// </summary>
        public void GatherMemoryUsage()
        {
            _currentPrivateMemoryUsage = Process.GetCurrentProcess().PrivateMemorySize64 / 1024;
            _minimumPrivateMemoryUsage = Math.Min(_minimumPrivateMemoryUsage, _currentPrivateMemoryUsage);
            _maximumPrivateMemoryUsage = Math.Max(_maximumPrivateMemoryUsage, _currentPrivateMemoryUsage);
            _currentVirtualMemoryUsage = Process.GetCurrentProcess().VirtualMemorySize64 / 1024;
            _minimumVirtualMemoryUsage = Math.Min(_minimumVirtualMemoryUsage, _currentVirtualMemoryUsage);
            _maximumVirtualMemoryUsage = Math.Max(_maximumVirtualMemoryUsage, _currentVirtualMemoryUsage);
            _currentWorkingSetMemoryUsage = Process.GetCurrentProcess().WorkingSet64 / 1024;
            _minimumWorkingSetMemoryUsage = Math.Min(_minimumWorkingSetMemoryUsage, _currentWorkingSetMemoryUsage);
            _maximumWorkingSetMemoryUsage = Math.Max(_maximumWorkingSetMemoryUsage, _currentWorkingSetMemoryUsage);
        }

        /// <summary>
        /// The method sets the min and max memory usage to current memory usage.
        /// </summary>
        public void Reset()
        {
            _minimumPrivateMemoryUsage = long.MaxValue;
            _maximumPrivateMemoryUsage = 0;
            _minimumVirtualMemoryUsage = long.MaxValue;
            _maximumVirtualMemoryUsage = 0;
            _minimumWorkingSetMemoryUsage = long.MaxValue;
            _maximumWorkingSetMemoryUsage = 0;
            GatherMemoryUsage();
        }

        #endregion Public methods
    }
}
