namespace PDFiumDotNET.Apps.PDFViewWPF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using PDFiumDotNET.Apps.Common;
    using PDFiumDotNET.Apps.PDFViewWPF.Base;
    using PDFiumDotNET.Apps.PDFViewWPF.Contracts;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Annotation;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Zoom;
    using PDFiumDotNET.Components.Factory;

    /// <summary>
    /// View model class for <see cref="MainView"/>.
    /// </summary>
    public partial class MainViewModel : BaseViewModel, IViewModel
    {
        #region Private constants

        private const string _standardPageComponentName = "StandardPageComponent";
        private const string _thumbnailPageComponentName = "ThumbnailPageComponent";

        #endregion Private constants

        #region Private fields

        private IView _view;
        private IPDFComponent _pdfComponent;
        private IPDFPageComponent _viewPageComponent;
        private IPDFPageComponent _thumbnailPageComponent;
        private string _currentPageLabel;
        private int _currentPageIndex;
        private bool _isFindTextActive;
        private int _activeFindTextPage;
        private bool _isFindAnnotationsActive;
        private int _activeFindAnnotationsPage;
        private IPDFPageAnnotations _selectedPageWithAnnotations;
        private PageLayoutType _pageLayoutType = PageLayoutType.Standard;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            FindTextResult = new ObservableCollection<IPDFFindPage>();
            FindAnnotationsResult = new ObservableCollection<IPDFPageAnnotations>();
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
        /// Gets the current zoom in percentage.
        /// </summary>
        public int CurrentZoom
        {
            get
            {
                return _pdfComponent != null
                    && _viewPageComponent != null
                    && _viewPageComponent.ZoomComponent != null
                    ? _viewPageComponent.ZoomComponent.CurrentZoomPercentage : 100;
            }

            set
            {
                if (_pdfComponent != null && _viewPageComponent != null && _viewPageComponent.ZoomComponent != null)
                {
                    _viewPageComponent.ZoomComponent.CurrentZoomPercentage = value;
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
                    _viewPageComponent.NavigateToPage(value);
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
                    _viewPageComponent.NavigateToPage(value);
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
                    && _pdfComponent.IsDocumentOpen;
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
                    || !_pdfComponent.IsDocumentOpen;
            }
        }

        /// <summary>
        /// Gets flag whether find text process is active or not.
        /// </summary>
        public bool IsFindTextActive
        {
            get
            {
                return _isFindTextActive;
            }

            private set
            {
                if (value != _isFindTextActive)
                {
                    _isFindTextActive = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets the page where is find text active.
        /// </summary>
        public int ActiveFindTextPage
        {
            get
            {
                return _activeFindTextPage;
            }

            private set
            {
                if (value != _activeFindTextPage)
                {
                    _activeFindTextPage = value;
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
        public bool IsFindTextCaseSensitive { get; set; }

        /// <summary>
        /// Gets or sets the find filter.
        /// </summary>
        public bool IsFindTextWholeWords { get; set; }

        /// <summary>
        /// Gets flag whether find annotations process is active or not.
        /// </summary>
        public bool IsFindAnnotationsActive
        {
            get
            {
                return _isFindAnnotationsActive;
            }

            private set
            {
                if (value != _isFindAnnotationsActive)
                {
                    _isFindAnnotationsActive = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets the page where is find annotations active.
        /// </summary>
        public int ActiveFindAnnotationsPage
        {
            get
            {
                return _activeFindAnnotationsPage;
            }

            private set
            {
                if (value != _activeFindAnnotationsPage)
                {
                    _activeFindAnnotationsPage = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets the collection of find result.
        /// </summary>
        public ObservableCollection<IPDFFindPage> FindTextResult { get; private set; }

        /// <summary>
        /// Gets the collection of find annotations result.
        /// </summary>
        public ObservableCollection<IPDFPageAnnotations> FindAnnotationsResult { get; private set; }

        /// <summary>
        /// Gets the value indicating whether view with pages in one column is active.
        /// </summary>
        public bool IsViewOneColumnActive
        {
            get
            {
                return _pageLayoutType == PageLayoutType.Standard;
            }
        }

        /// <summary>
        /// Gets the value indicating whether view with pages in one column is active.
        /// </summary>
        public bool IsViewTwoColumnsActive
        {
            get
            {
                return _pageLayoutType == PageLayoutType.TwoColumns;
            }
        }

        /// <summary>
        /// Gets the value indicating whether view with pages in one column is active.
        /// </summary>
        public bool IsViewTwoColumnsSpecialActive
        {
            get
            {
                return _pageLayoutType == PageLayoutType.TwoColumnsSpecial;
            }
        }

        /// <summary>
        /// Gets or sets the selected page with annotation.
        /// </summary>
        public object SelectedPageWithAnnotation
        {
            get
            {
                return _selectedPageWithAnnotations;
            }

            set
            {
                var page = value as IPDFPageAnnotations;
                if (page != null)
                {
                    if (page != _selectedPageWithAnnotations)
                    {
                        _selectedPageWithAnnotations = page;
                        InvokePropertyChangedEvent();
                    }

                    page.RelatedPage?.NavigateTo();
                }
            }
        }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Navigates to the defined position by <paramref name="bookmark"/>.
        /// </summary>
        /// <param name="bookmark">Parameter defines the position in document where should be navigated to.</param>
        public void NavigateTo(IPDFBookmark bookmark)
        {
            if (bookmark == null)
            {
                return;
            }

            if (bookmark.Action != null)
            {
                _viewPageComponent.PerformAction(bookmark.Action);
            }
            else if (bookmark.Destination != null)
            {
                _viewPageComponent.NavigateToDestination(bookmark.Destination);
            }
        }

        #endregion Public methods

        #region Private methods

        private static string CreateMessageFromException(Exception e)
        {
            var message = e.Message;
            e = e.InnerException;
            while (e != null)
            {
                message += Environment.NewLine;
                message += e.Message;
            }

            return message;
        }

        private static void OpenDocumentResultMessage(OpenDocumentResult result)
        {
            // ToDo: Hard coded text
            var caption = "Open PDF document failed!";
            var text = "Error code: " + result.ToString();
            MessageBox.Show(text, caption);
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

            // Initialize commands
            // Base commands
            OpenCommand = new ViewModelCommand(ExecuteOpenCommand, CanExecuteOpenCommand);
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand, CanExecuteCloseCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
            AboutCommand = new ViewModelCommand(ExecuteAboutCommand, CanExecuteAboutCommand);
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

            // Layout
            ViewOneColumnCommand = new ViewModelCommand(ExecuteViewOneColumnCommand, CanExecuteViewOneColumnCommand);
            ViewTwoColumnsCommand = new ViewModelCommand(ExecuteViewTwoColumnsCommand, CanExecuteViewTwoColumnsCommand);
            ViewTwoColumnsSpecialCommand = new ViewModelCommand(ExecuteViewTwoColumnsSpecialCommand, CanExecuteViewTwoColumnsSpecialCommand);

            // Find text commands
            FindTextCommand = new ViewModelCommand(ExecuteFindTextCommand, CanExecuteFindTextCommand);
            FindTextClearResultCommand = new ViewModelCommand(ExecuteFindTextClearResultCommand, CanExecuteFindTextClearResultCommand);
            FindTextCancelCommand = new ViewModelCommand(ExecuteFindTextCancelCommand, CanExecuteFindTextCancelCommand);

            // Find annotations commands
            FindAnnotationsCommand = new ViewModelCommand(ExecuteFindAnnotationsCommand, CanExecuteFindAnnotationsCommand);
            FindAnnotationsClearResultCommand = new ViewModelCommand(ExecuteFindAnnotationsClearResultCommand, CanExecuteFindAnnotationsClearResultCommand);
            FindAnnotationsCancelCommand = new ViewModelCommand(ExecuteFindAnnotationsCancelCommand, CanExecuteFindAnnotationsCancelCommand);

            // Initialize PDF component
            _pdfComponent = PDFFactory.PDFComponent;

            _pdfComponent.PropertyChanged += (s, e) =>
            {
                if (string.Equals(nameof(IPDFComponent.IsDocumentOpen), e?.PropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    IsFindTextActive = false;
                    FindTextResult.Clear();
                    IsFindAnnotationsActive = false;
                    FindAnnotationsResult.Clear();
                    InvokePropertyChangedEvent(nameof(IsDocumentOpened));
                    InvokePropertyChangedEvent(nameof(IsDocumentClosed));
                }
            };

            // We will use only two page components. One for thumbnails and one for standard view.
            _viewPageComponent = _pdfComponent.LayoutComponent.CreatePageComponent(_standardPageComponentName, _pageLayoutType);
            _thumbnailPageComponent = _pdfComponent.LayoutComponent.CreatePageComponent(_thumbnailPageComponentName, PageLayoutType.Thumbnail);

            _viewPageComponent.ZoomComponent.PropertyChanged += (s, e) =>
            {
                if (string.Equals(nameof(IPDFZoomComponent.CurrentZoomFactor), e.PropertyName, StringComparison.OrdinalIgnoreCase)
                    || string.IsNullOrEmpty(e.PropertyName))
                {
                    InvokePropertyChangedEvent(nameof(CurrentZoom));
                }
            };

            _viewPageComponent.PropertyChanged += (s, e) =>
            {
                if (string.Equals(nameof(IPDFPageComponent.CurrentPageIndex), e.PropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    _currentPageIndex = _viewPageComponent.CurrentPageIndex;
                    InvokePropertyChangedEvent(nameof(CurrentPageIndex));
                }
                else if (string.Equals(nameof(IPDFPageComponent.CurrentPageLabel), e.PropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    _currentPageLabel = _viewPageComponent.CurrentPageLabel;
                    InvokePropertyChangedEvent(nameof(CurrentPageLabel));
                }
            };

            _thumbnailPageComponent.PropertyChanged += (s, e) =>
            {
                if (string.Equals(nameof(IPDFPageComponent.CurrentPageIndex), e.PropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    _viewPageComponent.NavigateToPage(_thumbnailPageComponent.CurrentPageIndex);
                }
            };

            _viewPageComponent.PerformOutsideAction += (s, e) =>
            {
                if (e == null || e.Action == null)
                {
                    return;
                }

                // Simple example solution.
                var caption = "Perform action?";
                var text = string.Format(
                    CultureInfo.InvariantCulture,
                    "Action type: {0}\nUri path: {1}\nFile path: {2}",
                    e.Action.ActionType,
                    Uri.UnescapeDataString(e.Action.UriPath ?? string.Empty),
                    Uri.UnescapeDataString(e.Action.FilePath ?? string.Empty));
                if (MessageBox.Show(text, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        var p = new Process()
                        {
                            StartInfo = new ProcessStartInfo()
                            {
                                FileName = Uri.UnescapeDataString(e.Action.UriPath ?? string.Empty) ?? Uri.UnescapeDataString(e.Action.FilePath ?? string.Empty),
                                UseShellExecute = true,
                            },
                        };
                        p.Start();
                    }
                    catch (ObjectDisposedException e1)
                    {
                        MessageBox.Show(CreateMessageFromException(e1), e1.GetType().ToString());
                    }
                    catch (InvalidOperationException e2)
                    {
                        MessageBox.Show(CreateMessageFromException(e2), e2.GetType().ToString());
                    }
                    catch (Win32Exception e3)
                    {
                        MessageBox.Show(CreateMessageFromException(e3), e3.GetType().ToString());
                    }
                    catch (PlatformNotSupportedException e4)
                    {
                        MessageBox.Show(CreateMessageFromException(e4), e4.GetType().ToString());
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
                    _viewPageComponent.NavigateToFindPlace(page);
                }
                else if (value is IPDFFindPosition position)
                {
                    _viewPageComponent.NavigateToFindPlace(position);
                }
            }
        }

        #endregion Implementation of IViewModel
    }
}
