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

        private readonly MemoryInfo _privateMemory = new ();
        private readonly MemoryInfo _virtualMemory = new ();
        private readonly MemoryInfo _physicalMemory = new ();
        private readonly MemoryInfo _managedMemory = new ();

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
        public MemoryInfo PrivateMemory
        {
            get
            {
                return _privateMemory;
            }
        }

        /// <summary>
        /// Gets the current virtual memory usage of current process.
        /// (Virtual memory allocated for the associated process.)
        /// </summary>
        public MemoryInfo VirtualMemory
        {
            get
            {
                return _virtualMemory;
            }
        }

        /// <summary>
        /// Gets the current physical memory usage of current process.
        /// (Physical memory allocated for the associated process.)
        /// </summary>
        public MemoryInfo PhysicalMemory
        {
            get
            {
                return _physicalMemory;
            }
        }

        /// <summary>
        /// Gets the currently allocated managed memory of current process.
        /// (A number that is the best available approximation of the number of bytes currently allocated in managed memory.)
        /// </summary>
        public MemoryInfo ManagedMemory
        {
            get
            {
                return _managedMemory;
            }
        }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// The method gather all memory usage information.
        /// </summary>
        public void GatherMemoryUsage()
        {
            _privateMemory.CurrentMemoryUsage = Process.GetCurrentProcess().PrivateMemorySize64 / 1024;
            _virtualMemory.CurrentMemoryUsage = Process.GetCurrentProcess().VirtualMemorySize64 / 1024;
            _physicalMemory.CurrentMemoryUsage = Process.GetCurrentProcess().WorkingSet64 / 1024;
            _managedMemory.CurrentMemoryUsage = GC.GetTotalMemory(true) / 1024;
        }

        /// <summary>
        /// The method sets the min and max memory usage to current memory usage.
        /// </summary>
        public void Reset()
        {
            _privateMemory.Reset();
            _virtualMemory.Reset();
            _physicalMemory.Reset();
            _managedMemory.Reset();
            GatherMemoryUsage();
        }

        #endregion Public methods
    }
}
