namespace PDFiumDotNET.Samples.SimpleWpf
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Zoom;
    using PDFiumDotNET.Components.Factory;
    using PDFiumDotNET.Samples.SimpleWpf.Contracts;
    using PDFiumDotNET.Samples.SimpleWpf.Helper;

    /// <summary>
    /// View model class for <see cref="MainView"/>.
    /// </summary>
    public partial class MainViewModel : IViewModel, INotifyPropertyChanged
    {
        #region Private fields

        private IView _view;
        private IPDFComponent _pdfComponent;
        private IPDFPage _selectedThumbnail;
        private string _currentPageLabel;
        private int _currentPageIndex;
        private bool _isFindActive;
        private int _activeFindPage;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            FindResult = new ObservableCollection<IPDFFindPage>();
        }

        #endregion Constructors

        #region Public properties

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
        public IPDFPageComponent PageComponent
        {
            get
            {
                return _pdfComponent?.PageComponent;
            }
        }

        /// <summary>
        /// Gets the bookmark component.
        /// </summary>
        public IPDFBookmarkComponent BookmarkComponent
        {
            get
            {
                return _pdfComponent?.BookmarkComponent;
            }
        }

        /// <summary>
        /// Gets the zoom component.
        /// </summary>
        public IPDFZoomComponent ZoomComponent
        {
            get
            {
                return _pdfComponent?.ZoomComponent;
            }
        }

        /// <summary>
        /// Gets or sets selected thumbnail.
        /// </summary>
        public IPDFPage SelectedThumbnail
        {
            get => _selectedThumbnail;
            set
            {
                if (_selectedThumbnail != value)
                {
                    _selectedThumbnail = value;
                    _selectedThumbnail?.NavigteTo();
                }
            }
        }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        public double CurrentZoom
        {
            get
            {
                return (_pdfComponent != null && _pdfComponent.ZoomComponent != null) ? _pdfComponent.ZoomComponent.CurrentZoomFactor * 100d : 100d;
            }

            set
            {
                if (_pdfComponent != null && _pdfComponent.ZoomComponent != null)
                {
                    _pdfComponent.ZoomComponent.CurrentZoomFactor = value / 100d;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current page label.
        /// </summary>
        public string CurrentPageLabel
        {
            get
            {
                return _currentPageLabel;
            }

            set
            {
                if (!string.Equals(_currentPageLabel, value, StringComparison.OrdinalIgnoreCase)
                    && _pdfComponent != null)
                {
                    _pdfComponent.PageComponent.NavigateToPage(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current page index.
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                return _currentPageIndex;
            }

            set
            {
                if (_currentPageIndex != value
                    && _pdfComponent != null)
                {
                    _pdfComponent.PageComponent.NavigateToPage(value);
                }
            }
        }

        /// <summary>
        /// Gets the information whether the PDF document is opened.
        /// </summary>
        public bool IsDocumentOpened
        {
            get
            {
                return _pdfComponent != null
                    && _pdfComponent.IsDocumentOpened;
            }
        }

        /// <summary>
        /// Gets the information whether the PDF document is closed.
        /// </summary>
        public bool IsDocumentClosed
        {
            get
            {
                return _pdfComponent == null
                    || !_pdfComponent.IsDocumentOpened;
            }
        }

        /// <summary>
        /// Gets flag whether find process is active or not.
        /// </summary>
        public bool IsFindActive
        {
            get
            {
                return _isFindActive;
            }

            private set
            {
                if (value != _isFindActive)
                {
                    _isFindActive = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets the page where is find text active.
        /// </summary>
        public int ActiveFindPage
        {
            get
            {
                return _activeFindPage;
            }

            private set
            {
                if (value != _activeFindPage)
                {
                    _activeFindPage = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text to find.
        /// </summary>
        public string FindText { get; set; }

        /// <summary>
        /// Gets or sets the find filter.
        /// </summary>
        public bool IsFindCaseSensitive { get; set; }

        /// <summary>
        /// Gets or sets the find filter.
        /// </summary>
        public bool IsFindWholeWords { get; set; }

        /// <summary>
        /// Gets the collection of find result.
        /// </summary>
        public ObservableCollection<IPDFFindPage> FindResult { get; private set; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookmark"></param>
        public void NavigateTo(IPDFBookmark bookmark)
        {
            if (bookmark.Action != null)
            {
                _pdfComponent.PageComponent.PerformAction(bookmark.Action);
            }
            else if (bookmark.Destination != null)
            {
                _pdfComponent.PageComponent.NavigateToDestination(bookmark.Destination);
            }
        }

        #endregion Public methods

        #region Private invoke event methods

        /// <summary>
        /// The method invokes the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Parameter name used in <see cref="PropertyChangedEventArgs"/>.</param>
        private void InvokePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Private invoke event methods

        #region Implementation of IViewModel

        /// <summary>
        /// Called after view model was assigned to the view.
        /// </summary>
        /// <param name="view">View where is view model assigned.</param>
        public void AssignedToView(IView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            // Initialize commands
            // Base commands
            OpenCommand = new ViewModelCommand(ExecuteOpenCommand, CanExecuteOpenCommand);
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand, CanExecuteCloseCommand);
            InformationCommand = new ViewModelCommand(ExecuteInformationCommand, CanExecuteInformationCommand);
            ShowAnnotationsCommand = new ViewModelExtCommand<ToggleButton>(ExecuteShowAnnotationsCommand, CanExecuteShowAnnotationsCommand);
            // Zoom commands
            ZoomWidthCommand = new ViewModelCommand(ExecuteZoomWidthCommand, CanExecuteZoomWidthCommand);
            ZoomHeightCommand = new ViewModelCommand(ExecuteZoomHeightCommand, CanExecuteZoomHeightCommand);
            ZoomOutCommand = new ViewModelCommand(ExecuteZoomOutCommand, CanExecuteZoomOutCommand);
            ZoomInCommand = new ViewModelCommand(ExecuteZoomInCommand, CanExecuteZoomInCommand);
            // Go to commands
            GoToFirstPageCommand = new ViewModelCommand(ExecuteGoToFirstPageCommand, CanExecuteGoToFirstPageCommand);
            GoToPreviousPageCommand = new ViewModelCommand(ExecuteGoToPreviousPageCommand, CanExecuteGoToPreviousPageCommand);
            GoToNextPageCommand = new ViewModelCommand(ExecuteGoToNextPageCommand, CanExecuteGoToNextPageCommand);
            GoToLastPageCommand = new ViewModelCommand(ExecuteGoToLastPageCommand, CanExecuteGoToLastPageCommand);
            // Find commands
            FindCommand = new ViewModelCommand(ExecuteFindCommand, CanExecuteFindCommand);
            FindClearResultCommand = new ViewModelCommand(ExecuteFindClearResultCommand, CanExecuteFindClearResultCommand);
            FindCancelCommand = new ViewModelCommand(ExecuteFindCancelCommand, CanExecuteFindCancelCommand);

            // Initialize pdf component
            _pdfComponent = PDFFactory.PDFComponent;

            _pdfComponent.PropertyChanged += (s, e) =>
            {
                if (string.Equals(nameof(IPDFComponent.IsDocumentOpened), e?.PropertyName))
                {
                    IsFindActive = false;
                    FindResult.Clear();
                    InvokePropertyChangedEvent(nameof(IsDocumentOpened));
                    InvokePropertyChangedEvent(nameof(IsDocumentClosed));
                }
            };

            _pdfComponent.ZoomComponent.PropertyChanged += (s, e) =>
            {
                if (string.Equals(nameof(IPDFZoomComponent.CurrentZoomFactor), e.PropertyName)
                    || string.IsNullOrEmpty(e.PropertyName))
                {
                    InvokePropertyChangedEvent(nameof(CurrentZoom));
                }
            };

            _pdfComponent.PageComponent.PropertyChanged += (s, e) =>
            {
                if (string.Equals(nameof(IPDFPageComponent.CurrentPageIndex), e.PropertyName))
                {
                    _currentPageIndex = _pdfComponent.PageComponent.CurrentPageIndex;
                    InvokePropertyChangedEvent(nameof(CurrentPageIndex));
                }
                else if (string.Equals(nameof(IPDFPageComponent.CurrentPageLabel), e.PropertyName))
                {
                    _currentPageLabel = _pdfComponent.PageComponent.CurrentPageLabel;
                    InvokePropertyChangedEvent(nameof(CurrentPageLabel));
                }
            };

            _pdfComponent.PageComponent.PerformOutsideAction += (s, e) =>
            {
                if (e == null || e.Action == null)
                {
                    return;
                }

                // Simple example solution.
                var caption = "Perform action?";
                var text = string.Format(CultureInfo.InvariantCulture, "Action type: {0}\nUri path: {1}\nFile path: {2}",
                    e.Action.ActionType, Uri.UnescapeDataString(e.Action.UriPath ?? string.Empty), Uri.UnescapeDataString(e.Action.FilePath ?? string.Empty));
                if (MessageBox.Show(text, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Process.Start(Uri.UnescapeDataString(e.Action.UriPath ?? string.Empty) ?? Uri.UnescapeDataString(e.Action.FilePath ?? string.Empty));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            };
        }

        /// <summary>
        /// Gets or sets the selected find object.
        /// </summary>
        public object SelectedFindObject
        {
            get
            {
                return null;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                if (value is IPDFFindPage page)
                {
                    _pdfComponent.PageComponent.NavigateToFindPlace(page);
                }
                else if (value is IPDFFindPosition position)
                {
                    _pdfComponent.PageComponent.NavigateToFindPlace(position);
                }
            }
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
