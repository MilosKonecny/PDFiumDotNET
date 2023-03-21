namespace PDFiumDotNET.Apps.PDFViewWPF
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Controls.Primitives;
    using Microsoft.Win32;
    using PDFiumDotNET.Apps.PDFViewWPF.CommonDialogs;
    using PDFiumDotNET.Apps.PDFViewWPF.Dialogs;
    using PDFiumDotNET.Apps.PDFViewWPF.Helper;
    using PDFiumDotNET.Apps.PDFViewWPF.Windows;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Layout;

    /// <summary>
    /// View model class vor <see cref="MainView"/>.
    /// </summary>
    public partial class MainViewModel
    {
        #region Public properties - commands

        /// <summary>
        /// Gets the open pdf command.
        /// </summary>
        public ViewModelCommand OpenCommand { get; private set; }

        /// <summary>
        /// Gets the close pdf command.
        /// </summary>
        public ViewModelCommand CloseCommand { get; private set; }

        /// <summary>
        /// Gets the exit command.
        /// </summary>
        public ViewModelCommand ExitCommand { get; private set; }

        /// <summary>
        /// Gets the about command.
        /// </summary>
        public ViewModelCommand AboutCommand { get; private set; }

        /// <summary>
        /// Gets the document information command.
        /// </summary>
        public ViewModelCommand InformationCommand { get; private set; }

        /// <summary>
        /// Gets the show annotations command.
        /// </summary>
        public ViewModelExtCommand<ToggleButton> ShowAnnotationsCommand { get; private set; }

        /// <summary>
        /// Gets the 'zoom width' command.
        /// </summary>
        public ViewModelCommand ZoomWidthCommand { get; private set; }

        /// <summary>
        /// Gets the 'zoom height' command.
        /// </summary>
        public ViewModelCommand ZoomHeightCommand { get; private set; }

        /// <summary>
        /// Gets the 'zoom in' command.
        /// </summary>
        public ViewModelCommand ZoomInCommand { get; private set; }

        /// <summary>
        /// Gets the 'zoom out' command.
        /// </summary>
        public ViewModelCommand ZoomOutCommand { get; private set; }

        /// <summary>
        /// Gets the 'go to first page' command.
        /// </summary>
        public ViewModelCommand GoToFirstPageCommand { get; private set; }

        /// <summary>
        /// Gets the 'go to previous page' command.
        /// </summary>
        public ViewModelCommand GoToPreviousPageCommand { get; private set; }

        /// <summary>
        /// Gets the 'go to next page' command.
        /// </summary>
        public ViewModelCommand GoToNextPageCommand { get; private set; }

        /// <summary>
        /// Gets the 'go to last page' command.
        /// </summary>
        public ViewModelCommand GoToLastPageCommand { get; private set; }

        /// <summary>
        /// Gets the 'pages in one column' command.
        /// </summary>
        public ViewModelCommand ViewOneColumnCommand { get; private set; }

        /// <summary>
        /// Gets the 'pages in two columns' command.
        /// </summary>
        public ViewModelCommand ViewTwoColumnsCommand { get; private set; }

        /// <summary>
        /// Gets the 'pages in two columns, but one page in firstRow' command.
        /// </summary>
        public ViewModelCommand ViewTwoColumnsSpecialCommand { get; private set; }

        /// <summary>
        /// Gets the find command.
        /// </summary>
        public ViewModelCommand FindCommand { get; private set; }

        /// <summary>
        /// Gets the clear find result command.
        /// </summary>
        public ViewModelCommand FindClearResultCommand { get; private set; }

        /// <summary>
        /// Gets the cancel find command.
        /// </summary>
        public ViewModelCommand FindCancelCommand { get; private set; }

        #endregion Public properties - commands

        #region Private methods - command related

        private void ExecuteOpenCommand()
        {
            if (_pdfComponent == null)
            {
                return;
            }

            var dialog = new OpenFileDialog
            {
                // ToDo: Hard coded text.
                Filter = "PDF Files|*.pdf|All files|*.*",
            };
            if (dialog.ShowDialog() == false)
            {
                return;
            }

            var result = _pdfComponent.OpenDocument(dialog.FileName, () =>
            {
                // ToDo: Hard coded text.
                var inputDialog = new TextInputDialog()
                {
                    UsePasswordInput = true,
                    InputDialogTitle = "PDF document is password protected",
                    InputTextHint = "Please enter a password:",
                };
                if (inputDialog.ShowDialog(_view.Window))
                {
                    return inputDialog.InputText;
                }

                return null;
            });
            InvokePropertyChangedEvent();

            if (result != OpenDocumentResult.Success)
            {
                OpenDocumentResultMessage(result);
            }
        }

        private bool CanExecuteOpenCommand()
        {
            return _pdfComponent != null && !_pdfComponent.IsDocumentOpened;
        }

        private void ExecuteCloseCommand()
        {
            if (_pdfComponent == null)
            {
                return;
            }

            _pdfComponent.CloseDocument();
        }

        private bool CanExecuteCloseCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteExitCommand()
        {
            App.Current.Shutdown();
        }

        private bool CanExecuteExitCommand()
        {
            return true;
        }

        private void ExecuteAboutCommand()
        {
            var view = new AboutView();
            view.Owner = _view.Window;
            view.ShowDialog();
        }

        private bool CanExecuteAboutCommand()
        {
            return true;
        }

        private void ExecuteInformationCommand()
        {
            var di = new DocumentInformation(_pdfComponent)
            {
                Owner = _view.Window,
            };
            di.ShowDialog();
        }

        private bool CanExecuteInformationCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteShowAnnotationsCommand(ToggleButton relatedButton)
        {
            if (_pdfComponent != null && _pdfComponent.IsDocumentOpened && relatedButton != null)
            {
                _viewPageComponent.IsAnnotationToRender = !_viewPageComponent.IsAnnotationToRender;
                InvokePropertyChangedEvent();
                _view.InvalidatePDFControl();
            }
        }

        private bool CanExecuteShowAnnotationsCommand(ToggleButton relatedButton)
        {
            if (_pdfComponent == null || !_pdfComponent.IsDocumentOpened || relatedButton == null)
            {
                return false;
            }

            relatedButton.IsChecked = _viewPageComponent.IsAnnotationToRender;
            return true;
        }

        private void ExecuteZoomWidthCommand()
        {
            var pageComponent = _viewPageComponent;
            var zoomComponent = pageComponent?.ZoomComponent;
            if (pageComponent == null || zoomComponent == null)
            {
                return;
            }

            var page = pageComponent.CurrentPage;
            if (page != null)
            {
                zoomComponent.CurrentZoomFactor = _view.PDFActualWidth / (page.Width + (2 * _view.PDFPageMargin.Width));
            }
        }

        private bool CanExecuteZoomWidthCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteZoomHeightCommand()
        {
            var pageComponent = _viewPageComponent;
            var zoomComponent = pageComponent?.ZoomComponent;
            if (pageComponent == null || zoomComponent == null)
            {
                return;
            }

            var page = pageComponent.CurrentPage;
            if (page != null)
            {
                zoomComponent.CurrentZoomFactor = _view.PDFActualHeight / (page.Height + (2 * _view.PDFPageMargin.Height));
            }
        }

        private bool CanExecuteZoomHeightCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteZoomInCommand()
        {
            _viewPageComponent?.ZoomComponent?.IncreaseZoom();
        }

        private bool CanExecuteZoomInCommand()
        {
            if (_pdfComponent == null || !_pdfComponent.IsDocumentOpened || _viewPageComponent == null)
            {
                return false;
            }

            var values = _viewPageComponent.ZoomComponent.ZoomValues.ToList();
            if (values.Count == 0)
            {
                return false;
            }

            return Math.Abs(values[values.Count - 1] - _viewPageComponent.ZoomComponent.CurrentZoomFactor) > 0.01;
        }

        private void ExecuteZoomOutCommand()
        {
            _viewPageComponent?.ZoomComponent?.DecreaseZoom();
        }

        private bool CanExecuteZoomOutCommand()
        {
            if (_pdfComponent == null || !_pdfComponent.IsDocumentOpened || _viewPageComponent == null)
            {
                return false;
            }

            var values = _viewPageComponent.ZoomComponent.ZoomValues.ToList();
            if (values.Count == 0)
            {
                return false;
            }

            return Math.Abs(values[0] - _viewPageComponent.ZoomComponent.CurrentZoomFactor) > 0.01;
        }

        private void ExecuteGoToFirstPageCommand()
        {
            _viewPageComponent.NavigateToPage(1);
        }

        private bool CanExecuteGoToFirstPageCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteGoToPreviousPageCommand()
        {
            _viewPageComponent.NavigateToPage(_viewPageComponent.CurrentPageIndex - 1);
        }

        private bool CanExecuteGoToPreviousPageCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteGoToNextPageCommand()
        {
            _viewPageComponent.NavigateToPage(_viewPageComponent.CurrentPageIndex + 1);
        }

        private bool CanExecuteGoToNextPageCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteGoToLastPageCommand()
        {
            _viewPageComponent.NavigateToPage(_viewPageComponent.PageCount);
        }

        private bool CanExecuteGoToLastPageCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteViewOneColumnCommand()
        {
            _pageLayoutType = PageLayoutType.Standard;
            _pdfComponent.LayoutComponent.ChangePageLayout(_standardPageComponentName, _pageLayoutType);
            InvokePropertyChangedEvent(string.Empty);
            _view.InvalidatePDFControl();
        }

        private bool CanExecuteViewOneColumnCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteViewTwoColumnsCommand()
        {
            _pageLayoutType = PageLayoutType.TwoColumns;
            _pdfComponent.LayoutComponent.ChangePageLayout(_standardPageComponentName, _pageLayoutType);
            InvokePropertyChangedEvent(string.Empty);
            _view.InvalidatePDFControl();
        }

        private bool CanExecuteViewTwoColumnsCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteViewTwoColumnsSpecialCommand()
        {
            _pageLayoutType = PageLayoutType.TwoColumnsSpecial;
            _pdfComponent.LayoutComponent.ChangePageLayout(_standardPageComponentName, _pageLayoutType);
            InvokePropertyChangedEvent(string.Empty);
            _view.InvalidatePDFControl();
        }

        private bool CanExecuteViewTwoColumnsSpecialCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private async void ExecuteFindCommand()
        {
            ActiveFindPage = 1;
            FindResult.Clear();
            IsFindActive = true;

            // Cancelation token not used.
            // The cancelation is implemented by the IsFindActive property
            var ct = new CancellationToken();
            await Task.Factory.StartNew(
                () =>
                {
                    _viewPageComponent.FindComponent.FindText(
                        FindText,
                        IsFindCaseSensitive,
                        IsFindWholeWords,
                        (pageIndex) =>
                        {
                            ActiveFindPage = pageIndex + 1;
                            return IsFindActive;
                        },
                        (page) =>
                        {
                            if (page != null)
                            {
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    FindResult.Add(page);
                                });
                            }

                            return IsFindActive;
                        },
                        (page, position) =>
                        {
                            if (page != null && position != null)
                            {
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    page.Positions.Add(position);
                                });
                            }

                            return IsFindActive;
                        });
                },
                ct,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default).ConfigureAwait(false);

            IsFindActive = false;
        }

        private bool CanExecuteFindCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteFindClearResultCommand()
        {
            FindResult.Clear();
            if (_pdfComponent != null && _pdfComponent.IsDocumentOpened)
            {
                _viewPageComponent.FindComponent.ClearFindSelections();
            }
        }

        private bool CanExecuteFindClearResultCommand()
        {
            return _pdfComponent != null && _pdfComponent.IsDocumentOpened;
        }

        private void ExecuteFindCancelCommand()
        {
            IsFindActive = false;
        }

        private bool CanExecuteFindCancelCommand()
        {
            return IsFindActive;
        }

        #endregion Private methods - command related
    }
}
