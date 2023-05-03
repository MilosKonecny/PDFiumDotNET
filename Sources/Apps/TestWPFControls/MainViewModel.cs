namespace PDFiumDotNET.Apps.TestWPFControls
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Apps.Common;
    using PDFiumDotNET.Apps.PDFViewWPF.Helper;
    using PDFiumDotNET.Apps.TestWPFControls.Contracts;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Factory;

    /// <summary>
    /// View model class for <see cref="MainView"/>.
    /// </summary>
    public partial class MainViewModel : INotifyPropertyChanged
    {
        #region Private constants

        private const string _standardPageComponentName = "StandardPageComponent";
        private const string _thumbnailPageComponentName = "ThumbnailPageComponent";
        private const string _pdfFile1 = @"Precalculus.pdf";
        private const string _pdfFile2 = @"..\..\..\..\..\..\TestData\PDFs\Precalculus.pdf";

        #endregion Private constants

        #region Private fields

        private readonly PageLayoutType _pageLayoutType = PageLayoutType.Standard;
        private readonly MemoryUsage _memoryUsage = new ();
        private string _pdfFileToUse;
        private IView _view;
        private IPDFComponent _pdfComponent;
        private IPDFPageComponent _viewPageComponent;
        private IPDFPageComponent _thumbnailPageComponent;

        private bool _isTestActive;
        private bool _isTestStopPending;
        private int _countOfTestCycles = 10;
        private int _currentTestCycle = 0;
        private string _testInfo;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            if (File.Exists(Path.GetFullPath(_pdfFile1)))
            {
                _pdfFileToUse = Path.GetFullPath(_pdfFile1);
            }
            else if (File.Exists(Path.GetFullPath(_pdfFile2)))
            {
                _pdfFileToUse = Path.GetFullPath(_pdfFile2);
            }
            else
            {
                _pdfFileToUse = "'Precalculus.pdf' not found!";
            }
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the information for application's title.
        /// </summary>
        public static string TitleInformation
        {
            get
            {
                return CommonInformation.Info;
            }
        }

        /// <summary>
        /// Gets the PDF document to use for test.
        /// </summary>
        public string PDFDocument
        {
            get
            {
                return _pdfFileToUse;
            }
        }

        /// <summary>
        /// Gets the memory usage text.
        /// </summary>
        public string MemoryUsage
        {
            get
            {
                return $"Memory usage: {_memoryUsage.CurrentMemoryUsage} KiB (min: {_memoryUsage.MinimumMemoryUsage}, max: {_memoryUsage.MaximumMemoryUsage})";
            }
        }

        /// <summary>
        /// Gets or sets the value indicating whether the test is active or not.
        /// </summary>
        public bool IsTestActive
        {
            get
            {
                return _isTestActive;
            }

            set
            {
                if (_isTestActive != value)
                {
                    _isTestActive = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value indicating whether the running test should be stopped.
        /// </summary>
        public bool IsTestStopPending
        {
            get
            {
                return _isTestStopPending;
            }

            set
            {
                if (_isTestStopPending != value)
                {
                    _isTestStopPending = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets the main PDF component.
        /// </summary>
        public IPDFComponent PDFComponent
        {
            get
            {
                return _pdfComponent;
            }
        }

        /// <summary>
        /// Gets the page component.
        /// </summary>
        public IPDFPageComponent ViewPageComponent
        {
            get
            {
                return _viewPageComponent;
            }
        }

        /// <summary>
        /// Gets the page component.
        /// </summary>
        public IPDFPageComponent ThumbnailPageComponent
        {
            get
            {
                return _thumbnailPageComponent;
            }
        }

        /// <summary>
        /// Gets or sets the count of test cycles to perform.
        /// </summary>
        public int CountOfTestCycles
        {
            get
            {
                return _countOfTestCycles;
            }

            set
            {
                if (_countOfTestCycles != value)
                {
                    _countOfTestCycles = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the current test cycle.
        /// </summary>
        public int CurrentTestCycle
        {
            get
            {
                return _currentTestCycle;
            }

            set
            {
                if (_currentTestCycle != value)
                {
                    _currentTestCycle = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the information about current test.
        /// </summary>
        public string TestInfo
        {
            get
            {
                return _testInfo;
            }

            set
            {
                if (_testInfo != value)
                {
                    _testInfo = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        #endregion Public properties

        #region Protected invoke event methods

        /// <summary>
        /// The method invokes the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Parameter name used in <see cref="PropertyChangedEventArgs"/>.</param>
        protected void InvokePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Protected invoke event methods

        #region Private methods

        private void SetMemoryUsage()
        {
            _memoryUsage.GatherMemoryUsage();
            InvokePropertyChangedEvent(nameof(MemoryUsage));
        }

        #endregion Private methods

        #region Implementation of IViewModel

        /// <summary>
        /// Called after view model was assigned to the view.
        /// </summary>
        /// <param name="view">View where is view model assigned.</param>
        public void AssignedToView(IView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            Test1Command = new ViewModelCommand(ExecuteTest1Command, CanExecuteTest1Command);
            Test2Command = new ViewModelCommand(ExecuteTest2Command, CanExecuteTest2Command);
            Test3Command = new ViewModelCommand(ExecuteTest3Command, CanExecuteTest3Command);
            Test4Command = new ViewModelCommand(ExecuteTest4Command, CanExecuteTest4Command);
            StopTestCommand = new ViewModelCommand(ExecuteStopTestCommand, CanExecuteStopTestCommand);

            // Initialize PDF component
            _pdfComponent = PDFFactory.PDFComponent;

            // We will use only two page components. One for thumbnails and one for standard view.
            _viewPageComponent = _pdfComponent.LayoutComponent.CreatePageComponent(_standardPageComponentName, _pageLayoutType);
            _thumbnailPageComponent = _pdfComponent.LayoutComponent.CreatePageComponent(_thumbnailPageComponentName, PageLayoutType.Thumbnail);
        }

        #endregion Implementation of IViewModel

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged
    }
}
