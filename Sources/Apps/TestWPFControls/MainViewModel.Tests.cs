namespace PDFiumDotNET.Apps.TestWPFControls
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;

    /// <summary>
    /// View model class for <see cref="MainView"/>.
    /// </summary>
    public partial class MainViewModel
    {
        #region Private methods - test

        private async Task Test1()
        {
            IsTestActive = true;
            SetMemoryUsage();

            var count = CountOfTestCycles;
            TestInfo = "Open, navigate to every page, close" + Environment.NewLine;
            TestInfo += $"Test1 start: {MemoryUsage}" + Environment.NewLine;

            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                CurrentTestCycle = docIndex;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() => result = _pdfComponent.OpenDocument(_pdfFileToUse));

                for (var pageIndex = 0; pageIndex < _viewPageComponent.PageCount; pageIndex++)
                {
                    Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(pageIndex + 1));
                    await Task.Delay(1).ConfigureAwait(false);
                    if (IsTestStopPending)
                    {
                        break;
                    }

                    SetMemoryUsage();
                }

                Application.Current.Dispatcher.Invoke(() => _pdfComponent.CloseDocument());
                if (IsTestStopPending)
                {
                    break;
                }

                SetMemoryUsage();
                TestInfo += $"Cycle {docIndex}: {MemoryUsage}" + Environment.NewLine;
            }

            IsTestActive = false;
            IsTestStopPending = false;
        }

        private async Task Test2()
        {
            IsTestActive = true;
            SetMemoryUsage();

            var count = CountOfTestCycles;
            TestInfo = "Open, navigate to every fiftieth page, use all zooms twice, close" + Environment.NewLine;
            TestInfo += $"Test2 start: {MemoryUsage}" + Environment.NewLine;

            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                CurrentTestCycle = docIndex;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() => result = _pdfComponent.OpenDocument(_pdfFileToUse));
                Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(1));

                for (var pageIndex = 0; pageIndex < _viewPageComponent.PageCount; pageIndex += 50)
                {
                    Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(pageIndex + 1));
                    await Task.Delay(1).ConfigureAwait(false);

                    var currentZoom = _viewPageComponent.ZoomComponent.CurrentZoomFactor;
                    do
                    {
                        currentZoom = _viewPageComponent.ZoomComponent.CurrentZoomFactor;
                        Application.Current.Dispatcher.Invoke(() => _viewPageComponent.ZoomComponent.IncreaseZoom());
                        Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(pageIndex + 1));
                        await Task.Delay(1).ConfigureAwait(false);
                    }
                    while (Math.Abs(currentZoom - _viewPageComponent.ZoomComponent.CurrentZoomFactor) > double.Epsilon);
                    do
                    {
                        currentZoom = _viewPageComponent.ZoomComponent.CurrentZoomFactor;
                        Application.Current.Dispatcher.Invoke(() => _viewPageComponent.ZoomComponent.DecreaseZoom());
                        Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(pageIndex + 1));
                        await Task.Delay(1).ConfigureAwait(false);
                    }
                    while (Math.Abs(currentZoom - _viewPageComponent.ZoomComponent.CurrentZoomFactor) > double.Epsilon);
                    do
                    {
                        currentZoom = _viewPageComponent.ZoomComponent.CurrentZoomFactor;
                        Application.Current.Dispatcher.Invoke(() => _viewPageComponent.ZoomComponent.IncreaseZoom());
                        Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(pageIndex + 1));
                        await Task.Delay(1).ConfigureAwait(false);
                    }
                    while (Math.Abs(1.0 - _viewPageComponent.ZoomComponent.CurrentZoomFactor) > double.Epsilon);

                    if (IsTestStopPending)
                    {
                        break;
                    }

                    SetMemoryUsage();
                }

                Application.Current.Dispatcher.Invoke(() => _pdfComponent.CloseDocument());
                if (IsTestStopPending)
                {
                    break;
                }

                SetMemoryUsage();
                TestInfo += $"Cycle {docIndex}: {MemoryUsage}" + Environment.NewLine;
            }

            IsTestActive = false;
            IsTestStopPending = false;
        }

        private async Task Test3()
        {
            IsTestActive = true;
            SetMemoryUsage();

            var count = CountOfTestCycles;
            TestInfo = "Open, scroll whole way down by 100, close" + Environment.NewLine;
            TestInfo += $"Test3 start: {MemoryUsage}" + Environment.NewLine;

            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                CurrentTestCycle = docIndex;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() => result = _pdfComponent.OpenDocument(_pdfFileToUse));
                Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(1));
                await Task.Delay(1).ConfigureAwait(false);

                for (var offset = 0; offset < Math.Max(_view.PDFViewControl.ExtentHeight, _view.PDFViewControl.ActualHeight); offset += 100)
                {
                    Application.Current.Dispatcher.Invoke(() => _view.PDFViewControl.ScrollOwner.ScrollToVerticalOffset(offset));
                    await Task.Delay(1).ConfigureAwait(false);
                    if (IsTestStopPending)
                    {
                        break;
                    }

                    SetMemoryUsage();
                }

                Application.Current.Dispatcher.Invoke(() => _pdfComponent.CloseDocument());
                if (IsTestStopPending)
                {
                    break;
                }

                SetMemoryUsage();
                TestInfo += $"Cycle {docIndex}: {MemoryUsage}" + Environment.NewLine;
            }

            IsTestActive = false;
            IsTestStopPending = false;
        }

        private async Task Test4()
        {
            IsTestActive = true;
            SetMemoryUsage();

            var count = CountOfTestCycles;
            TestInfo = "Open, jump to every bookmark destination, close" + Environment.NewLine;
            TestInfo += $"Test4 start: {MemoryUsage}" + Environment.NewLine;

            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                CurrentTestCycle = docIndex;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() => result = _pdfComponent.OpenDocument(_pdfFileToUse));
                Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(1));
                await Task.Delay(1).ConfigureAwait(false);

                var bookmarks = GetBookmarks(_pdfComponent.BookmarkComponent.Bookmarks);

                foreach (var bookmark in bookmarks)
                {
                    Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToDestination(bookmark.Destination));
                    await Task.Delay(1).ConfigureAwait(false);
                    if (IsTestStopPending)
                    {
                        break;
                    }

                    SetMemoryUsage();
                }

                Application.Current.Dispatcher.Invoke(() => _pdfComponent.CloseDocument());
                if (IsTestStopPending)
                {
                    break;
                }

                SetMemoryUsage();
                TestInfo += $"Cycle {docIndex}: {MemoryUsage}" + Environment.NewLine;
            }

            IsTestActive = false;
            IsTestStopPending = false;
        }

        #endregion Private methods - tests

        #region Private methods

        private List<IPDFBookmark> GetBookmarks(IEnumerable<IPDFBookmark> bookmarks)
        {
            var list = new List<IPDFBookmark>();
            if (bookmarks == null)
            {
                return list;
            }

            foreach (var bookmark in bookmarks)
            {
                list.Add(bookmark);
                list.AddRange(GetBookmarks(bookmark.Bookmarks));
            }

            return list;
        }

        #endregion Private methods
    }
}
