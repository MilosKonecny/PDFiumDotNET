namespace PDFiumDotNET.Apps.TestWPFControls
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
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

        private void TestResetMemoryUsage()
        {
            TestInfo = "Reset memory usage" + Environment.NewLine;
            TestInfo += CreateTestInfo("Reset memory usage - start");
            _memoryUsage.Reset();
            TestInfo += CreateTestInfo("Reset memory usage - end");
        }

        private void TestGCCollect()
        {
            TestInfo = "GC.Collect()" + Environment.NewLine;
            TestInfo += CreateTestInfo("GC.Collect() - start");
            GC.Collect();
            TestInfo += CreateTestInfo("GC.Collect() - end");
        }

        private async Task Test1()
        {
            IsTestActive = true;

            TestInfo = "Open, navigate to every page, close" + Environment.NewLine;
            TestInfo += CreateTestInfo("Test 1 - start");

            var count = CountOfTestCycles;
            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                GUIPrepareForTest();

                CurrentTestCycle = docIndex + 1;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() => result = _pdfComponent.OpenDocument(_pdfFileToUse));
                await Task.Delay(1).ConfigureAwait(false);

                for (var pageIndex = 0; pageIndex < _viewPageComponent.PageCount; pageIndex++)
                {
                    Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(pageIndex + 1));
                    await Task.Delay(1).ConfigureAwait(false);
                    if (IsTestStopPending)
                    {
                        break;
                    }
                }

                Application.Current.Dispatcher.Invoke(_pdfComponent.CloseDocument);
                GUICleanupAfterTest();
                await Task.Delay(1).ConfigureAwait(false);

                if (IsTestStopPending)
                {
                    break;
                }

                if (!ShowMemoryUsageOnlyTwoTimes)
                {
                    TestInfo += CreateTestInfo($"Cycle {CurrentTestCycle}");
                }
            }

            TestInfo += CreateTestInfo("Test1 - end");
            IsTestActive = false;
            IsTestStopPending = false;
        }

        private async Task Test2()
        {
            IsTestActive = true;

            TestInfo = "Open, navigate to every fiftieth page, use all zooms twice, close" + Environment.NewLine;
            TestInfo += CreateTestInfo("Test 2 - start");

            var count = CountOfTestCycles;
            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                GUIPrepareForTest();

                CurrentTestCycle = docIndex + 1;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    result = _pdfComponent.OpenDocument(_pdfFileToUse);
                    _viewPageComponent.NavigateToPage(1);
                });
                await Task.Delay(1).ConfigureAwait(false);

                for (var pageIndex = 0; pageIndex < _viewPageComponent.PageCount; pageIndex += 50)
                {
                    Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(pageIndex + 1));
                    await Task.Delay(1).ConfigureAwait(false);

                    var currentZoom = _viewPageComponent.ZoomComponent.CurrentZoomFactor;
                    do
                    {
                        currentZoom = _viewPageComponent.ZoomComponent.CurrentZoomFactor;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _viewPageComponent.ZoomComponent.IncreaseZoom();
                            _viewPageComponent.NavigateToPage(pageIndex + 1);
                        });
                        await Task.Delay(1).ConfigureAwait(false);
                    }
                    while (Math.Abs(currentZoom - _viewPageComponent.ZoomComponent.CurrentZoomFactor) > double.Epsilon);
                    do
                    {
                        currentZoom = _viewPageComponent.ZoomComponent.CurrentZoomFactor;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _viewPageComponent.ZoomComponent.DecreaseZoom();
                            _viewPageComponent.NavigateToPage(pageIndex + 1);
                        });
                        await Task.Delay(1).ConfigureAwait(false);
                    }
                    while (Math.Abs(currentZoom - _viewPageComponent.ZoomComponent.CurrentZoomFactor) > double.Epsilon);
                    do
                    {
                        currentZoom = _viewPageComponent.ZoomComponent.CurrentZoomFactor;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _viewPageComponent.ZoomComponent.IncreaseZoom();
                            _viewPageComponent.NavigateToPage(pageIndex + 1);
                        });
                        await Task.Delay(1).ConfigureAwait(false);
                    }
                    while (Math.Abs(1.0 - _viewPageComponent.ZoomComponent.CurrentZoomFactor) > double.Epsilon);

                    if (IsTestStopPending)
                    {
                        break;
                    }
                }

                Application.Current.Dispatcher.Invoke(_pdfComponent.CloseDocument);
                GUICleanupAfterTest();
                await Task.Delay(1).ConfigureAwait(false);

                if (IsTestStopPending)
                {
                    break;
                }

                if (!ShowMemoryUsageOnlyTwoTimes)
                {
                    TestInfo += CreateTestInfo($"Cycle {CurrentTestCycle}");
                }
            }

            TestInfo += CreateTestInfo("Test2 - end");
            IsTestActive = false;
            IsTestStopPending = false;
        }

        private async Task Test3()
        {
            IsTestActive = true;

            var count = CountOfTestCycles;
            TestInfo = "Open, scroll whole way down by 200, close" + Environment.NewLine;
            TestInfo += CreateTestInfo("Test 3 - start");

            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                GUIPrepareForTest();

                CurrentTestCycle = docIndex + 1;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    result = _pdfComponent.OpenDocument(_pdfFileToUse);
                    _viewPageComponent.NavigateToPage(1);
                });
                await Task.Delay(1).ConfigureAwait(false);

                for (var offset = 0; offset < Math.Max(_pdfView.ExtentHeight, _pdfView.ActualHeight); offset += 200)
                {
                    Application.Current.Dispatcher.Invoke(() => _pdfView.ScrollOwner.ScrollToVerticalOffset(offset));
                    await Task.Delay(1).ConfigureAwait(false);
                    if (IsTestStopPending)
                    {
                        break;
                    }
                }

                Application.Current.Dispatcher.Invoke(() => _pdfComponent.CloseDocument());
                GUICleanupAfterTest();
                await Task.Delay(1).ConfigureAwait(false);

                if (IsTestStopPending)
                {
                    break;
                }

                if (!ShowMemoryUsageOnlyTwoTimes)
                {
                    TestInfo += CreateTestInfo($"Cycle {CurrentTestCycle}");
                }
            }

            TestInfo += CreateTestInfo("Test3 - end");
            IsTestActive = false;
            IsTestStopPending = false;
        }

        private async Task Test4()
        {
            IsTestActive = true;

            TestInfo = "Open, jump to every bookmark destination, close" + Environment.NewLine;
            TestInfo += CreateTestInfo("Test 4 - start");

            var count = CountOfTestCycles;
            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                GUIPrepareForTest();

                CurrentTestCycle = docIndex + 1;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    result = _pdfComponent.OpenDocument(_pdfFileToUse);
                    _viewPageComponent.NavigateToPage(1);
                });
                await Task.Delay(1).ConfigureAwait(false);

                List<IPDFBookmark> bookmarks = null;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    bookmarks = GetBookmarks(_pdfComponent.BookmarkComponent.Bookmarks);
                });

                foreach (var bookmark in bookmarks)
                {
                    Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToDestination(bookmark.Destination));
                    await Task.Delay(1).ConfigureAwait(false);
                    if (IsTestStopPending)
                    {
                        break;
                    }
                }

                Application.Current.Dispatcher.Invoke(_pdfComponent.CloseDocument);
                GUICleanupAfterTest();
                await Task.Delay(1).ConfigureAwait(false);

                if (IsTestStopPending)
                {
                    break;
                }

                if (!ShowMemoryUsageOnlyTwoTimes)
                {
                    TestInfo += CreateTestInfo($"Cycle {CurrentTestCycle}");
                }
            }

            TestInfo += CreateTestInfo("Test4 - end");
            IsTestActive = false;
            IsTestStopPending = false;
        }

        private async Task Test5()
        {
            IsTestActive = true;

            TestInfo = "Open, navigate to first page, close" + Environment.NewLine;
            TestInfo += CreateTestInfo("Test 5 - start");

            var count = CountOfTestCycles;
            for (var docIndex = 0; docIndex < count; docIndex++)
            {
                GUIPrepareForTest();

                CurrentTestCycle = docIndex + 1;
                OpenDocumentResult result;
                Application.Current.Dispatcher.Invoke(() => result = _pdfComponent.OpenDocument(_pdfFileToUse));
                await Task.Delay(1).ConfigureAwait(false);

                Application.Current.Dispatcher.Invoke(() => _viewPageComponent.NavigateToPage(1));
                await Task.Delay(1).ConfigureAwait(false);

                Application.Current.Dispatcher.Invoke(_pdfComponent.CloseDocument);
                GUICleanupAfterTest();
                await Task.Delay(1).ConfigureAwait(false);

                if (IsTestStopPending)
                {
                    break;
                }

                if (!ShowMemoryUsageOnlyTwoTimes)
                {
                    TestInfo += CreateTestInfo($"Cycle {CurrentTestCycle}");
                }
            }

            TestInfo += CreateTestInfo("Test5 - end");
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

        private string CreateTestInfo(string startText)
        {
            _memoryUsage.GatherMemoryUsage();
            var sb = new StringBuilder();
            sb.AppendLine(startText);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "\t{0}", PrivateMemoryUsage));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "\t{0}", PhysicalMemoryUsage));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "\t{0}", VirtualMemoryUsage));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "\t{0}", ManagedMemoryUsage));
            return sb.ToString();
        }

        #endregion Private methods
    }
}
