namespace PDFiumDotNET.Apps.TestComponents
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using PDFiumDotNET.Apps.Common;
    using PDFiumDotNET.Apps.TestComponents.Exceptions;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Bitmap;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Factory;

    /// <summary>
    /// Application's class.
    /// </summary>
    internal class Program
    {
        private const int _lineForStartInfo1 = 0;
        private const int _lineForStartInfo2 = 1;
        private const int _lineForMemoryUsage1 = 2;
        private const int _lineForMemoryUsage2 = 3;
        private const int _lineForMemoryUsage3 = 4;
        private const int _lineForMemoryUsage4 = 5;
        private const int _lineForExecutedOperationGroups = 6;
        private const int _lineForStates1 = 7;
        private const int _lineForStates2 = 8;
        private const int _lineForError = 9;
        private const int _lineForTestGroup = 10;
        private const int _lineForSingleTest = 11;
        private const int _lineForFirstAvailableCommand = 13;

        private const int _minTestCount = 2;
        private const int _maxTestCount = 2000;
        private const string _pdfFile1 = @"Precalculus.pdf";
        private const string _pdfFile2 = @"..\..\..\..\..\..\TestData\PDFs\Precalculus.pdf";
        private static int _testCount = 100;
        private static bool _showMemoryUsageOnlyTwoTimes = false;
        private static int _executedOperationGroups = 0;
        private static MemoryUsage _memoryUsage = new ();
        private static bool _cancelOperation = false;
        private static string _pdfFileToUse;

        private static List<ActionDescription> _tests = new ()
        {
            new ActionDescription(
                'c',
                ConsoleKey.Oem102,
                true,
                "This action stops the executed test.",
                "Exit application",
                "Exit application",
                StopTest),
            new ActionDescription(
                'x',
                ConsoleKey.Oem102,
                false,
                "This action terminates the application.",
                "Exit application",
                "Exit application",
                ExitApplication),
            new ActionDescription(
                'r',
                ConsoleKey.Oem102,
                false,
                "This action resets min/max memory usage to current.",
                "Reset memory usage",
                "Reset memory usage",
                ResetMinMax),
            new ActionDescription(
                '1',
                ConsoleKey.Oem102,
                false,
                "Change flag indicating whether the memory usage is shown only after last test.",
                "Change show memory usage",
                "Change show memory usage",
                ChangeShowMemoryUsageOnlyTwoTimes),
            new ActionDescription(
                '2',
                ConsoleKey.Oem102,
                false,
                "This action forces an immediate garbage collection of all generations.",
                "Collect GC",
                "Collect GC",
                FreeMemory),
            new ActionDescription(
                '\0',
                ConsoleKey.UpArrow,
                false,
                "This action increases the number of times a particular group should be executed",
                "Increase 'several times' for operation group",
                "Increase 'several times' for operation group",
                Increase1TestCount),
            new ActionDescription(
                '\0',
                ConsoleKey.RightArrow,
                false,
                "This action increases the number of times a particular group should be executed by 100",
                "Increase 'several times' for operation group by 100",
                "Increase 'several times' for operation group by 100",
                Increase100TestCount),
            new ActionDescription(
                '\0',
                ConsoleKey.DownArrow,
                false,
                "This action decreases the number of times a particular group should be executed",
                "Decrease 'several times' for operation group",
                "Decrease 'several times' for operation group",
                Decrease1TestCount),
            new ActionDescription(
                '\0',
                ConsoleKey.LeftArrow,
                false,
                "This action decreases the number of times a particular group should be executed by 100",
                "Decrease 'several times' for operation group by 100",
                "Decrease 'several times' for operation group by 100",
                Decrease100TestCount),
            new ActionDescription(
                'a',
                ConsoleKey.Oem102,
                false,
                "This action opens and closes a PDF document",
                "Open & close",
                "Open & close (several times)",
                OperationGroup1),
            new ActionDescription(
                'A',
                ConsoleKey.Oem102,
                false,
                "'a' several times",
                "Open & close",
                "Open & close (several times)",
                OperationGroup1SeveralTimes),
            new ActionDescription(
                'b',
                ConsoleKey.Oem102,
                false,
                "This action opens, navigates to every page, gets position, zooms and closes a PDF document",
                "Open & navigate to every page & get position & zoom & get position & close",
                "Open & navigate to every page & get position & zoom & get position & close (several times)",
                OperationGroup2),
            new ActionDescription(
                'B',
                ConsoleKey.Oem102,
                false,
                "'b' several times",
                "Open & navigate to every page & get position & zoom & get position & close",
                "Open & navigate to every page & get position & zoom & get position & close (several times)",
                OperationGroup2SeveralTimes),
            new ActionDescription(
                'c',
                ConsoleKey.Oem102,
                false,
                "This action opens, renders every page and closes a PDF document",
                "Open & render every page & close",
                "Open & render every page & close (several times)",
                OperationGroup3),
            new ActionDescription(
                'C',
                ConsoleKey.Oem102,
                false,
                "'c' several times",
                "Open & render every page & close",
                "Open & render every page & close (several times)",
                OperationGroup3SeveralTimes),
        };

        private static void Increase1TestCount(ActionDescription description)
        {
            _testCount = Math.Min(_testCount + 1, _maxTestCount);
        }

        private static void Increase100TestCount(ActionDescription description)
        {
            _testCount = Math.Min(_testCount + 100, _maxTestCount);
        }

        private static void Decrease1TestCount(ActionDescription description)
        {
            _testCount = Math.Max(_testCount - 1, _minTestCount);
        }

        private static void Decrease100TestCount(ActionDescription description)
        {
            _testCount = Math.Max(_testCount - 100, _minTestCount);
        }

        private static void ChangeShowMemoryUsageOnlyTwoTimes(ActionDescription description)
        {
            _showMemoryUsageOnlyTwoTimes = !_showMemoryUsageOnlyTwoTimes;
        }

        private static void FreeMemory(ActionDescription description)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static void OperationGroup1(ActionDescription description)
        {
            var pdfComponent = PDFFactory.PDFComponent;
            var pageComponent = pdfComponent.LayoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var zoomComponent = pageComponent.ZoomComponent;
            var bookmarkComponent = pdfComponent.BookmarkComponent;

            var result = pdfComponent.OpenDocument(_pdfFileToUse);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document '{_pdfFileToUse}' not open! {result}");
                return;
            }

            pdfComponent.CloseDocument();
            pdfComponent.Dispose();

            _executedOperationGroups++;
        }

        private static void OperationGroup1SeveralTimes(ActionDescription description)
        {
            for (var index = 0; index < _testCount; index++)
            {
                if (!_showMemoryUsageOnlyTwoTimes)
                {
                    PrintMemoryUsage();
                }

                SetProgressForGroup(description.GroupText, index, _testCount);
                OperationGroup1(description);
                PrintExecutedOperationGroups();
                if (_cancelOperation)
                {
                    break;
                }
            }

            ClearProgressForGroup();
        }

        private static void OperationGroup2(ActionDescription description)
        {
            var pdfComponent = PDFFactory.PDFComponent;
            var pageComponent = pdfComponent.LayoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var zoomComponent = pageComponent.ZoomComponent;
            var bookmarkComponent = pdfComponent.BookmarkComponent;

            var result = pdfComponent.OpenDocument(_pdfFileToUse);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document not open! {result}");
                return;
            }

            for (var index = 0; index < pageComponent.PageCount; index++)
            {
                pageComponent.NavigateToPage(index + 1);

                var position1 = pageComponent.RenderManager.DeterminePagePosition(index);
                if (index % 2 == 0)
                {
                    zoomComponent.IncreaseZoom();
                }
                else
                {
                    zoomComponent.DecreaseZoom();
                }

                var position2 = pageComponent.RenderManager.DeterminePagePosition(index);
                if (_cancelOperation)
                {
                    break;
                }
            }

            pdfComponent.CloseDocument();
            pdfComponent.Dispose();

            _executedOperationGroups++;
        }

        private static void OperationGroup2SeveralTimes(ActionDescription description)
        {
            for (var index = 0; index < _testCount; index++)
            {
                if (!_showMemoryUsageOnlyTwoTimes)
                {
                    PrintMemoryUsage();
                }

                SetProgressForGroup(description.GroupText, index, _testCount);
                OperationGroup2(description);
                PrintExecutedOperationGroups();
                if (_cancelOperation)
                {
                    break;
                }
            }

            ClearProgressForGroup();
        }

        private static void OperationGroup3(ActionDescription description)
        {
            var pdfComponent = PDFFactory.PDFComponent;
            var pageComponent = pdfComponent.LayoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var zoomComponent = pageComponent.ZoomComponent;
            var bookmarkComponent = pdfComponent.BookmarkComponent;

            var result = pdfComponent.OpenDocument(_pdfFileToUse);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document not open! {result}");
                return;
            }

            double width = 0d;
            double height = 0d;
            WriteableBitmap bitmap = null;
            var format = BitmapFormat.BitmapBGRA;

            //////zoomComponent.CurrentZoomFactor = 0.1;
            for (var index = 0; index < pageComponent.PageCount; index++)
            {
                SetProgressForTest(description.Text, index, pageComponent.PageCount);
                var page = pageComponent.Pages[index];

                if (bitmap == null || width != page.Width || height != page.Height)
                {
                    width = page.Width;
                    height = page.Height;
                    bitmap = new WriteableBitmap((int)page.Width, (int)page.Height, 96, 96, PixelFormats.Bgra32, null);
                }

                bitmap.Lock();
                page.RenderPageBitmap(
                    zoomComponent.CurrentZoomFactor,
                    0,
                    0,
                    (int)page.Width,
                    (int)page.Height,
                    (int)page.Width,
                    (int)page.Height,
                    format,
                    bitmap.BackBuffer,
                    bitmap.BackBufferStride);
                bitmap.AddDirtyRect(new Int32Rect(0, 0, (int)page.Width, (int)page.Height));
                bitmap.Unlock();

                if (_cancelOperation)
                {
                    break;
                }
            }

            pdfComponent.CloseDocument();
            pdfComponent.Dispose();

            ClearProgressForTest();

            _executedOperationGroups++;
        }

        private static void OperationGroup3SeveralTimes(ActionDescription description)
        {
            for (var index = 0; index < _testCount; index++)
            {
                if (!_showMemoryUsageOnlyTwoTimes)
                {
                    PrintMemoryUsage();
                }

                SetProgressForGroup(description.GroupText, index, _testCount);
                OperationGroup3(description);
                PrintExecutedOperationGroups();

                if (_cancelOperation)
                {
                    break;
                }
            }

            ClearProgressForGroup();
        }

        private static void StopTest(ActionDescription description)
        {
        }

        private static void ExitApplication(ActionDescription description)
        {
            throw new TerminateApplicationRequiredException();
        }

        private static void ResetMinMax(ActionDescription description)
        {
            _memoryUsage.Reset();
        }

        private static void PrintStartInfo()
        {
            Console.SetCursorPosition(0, _lineForStartInfo1);
            Console.WriteLine(CommonInformation.Info);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, _lineForStartInfo2);
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Memory usage after start: Private={0:N0} / Physical={1:N0} / Virtual={2:N0} / Managed={3:N0} KiB       ",
                    _memoryUsage.PrivateMemory.CurrentMemoryUsage,
                    _memoryUsage.PhysicalMemory.CurrentMemoryUsage,
                    _memoryUsage.VirtualMemory.CurrentMemoryUsage,
                    _memoryUsage.ManagedMemory.CurrentMemoryUsage));
            Console.ResetColor();
        }

        private static void PrintMemoryUsage()
        {
            _memoryUsage.GatherMemoryUsage();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, _lineForMemoryUsage1);
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Private memory usage           : {0,13:N0} KiB (min: {1,13:N0} max: {2,13:N0})         ",
                    _memoryUsage.PrivateMemory.CurrentMemoryUsage,
                    _memoryUsage.PrivateMemory.MinimumMemoryUsage,
                    _memoryUsage.PrivateMemory.MaximumMemoryUsage));
            Console.SetCursorPosition(0, _lineForMemoryUsage2);
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Physical memory usage          : {0,13:N0} KiB (min: {1,13:N0} max: {2,13:N0})         ",
                    _memoryUsage.PhysicalMemory.CurrentMemoryUsage,
                    _memoryUsage.PhysicalMemory.MinimumMemoryUsage,
                    _memoryUsage.PhysicalMemory.MaximumMemoryUsage));
            Console.SetCursorPosition(0, _lineForMemoryUsage3);
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Virtual memory usage           : {0,13:N0} KiB (min: {1,13:N0} max: {2,13:N0})         ",
                    _memoryUsage.VirtualMemory.CurrentMemoryUsage,
                    _memoryUsage.VirtualMemory.MinimumMemoryUsage,
                    _memoryUsage.VirtualMemory.MaximumMemoryUsage));
            Console.SetCursorPosition(0, _lineForMemoryUsage4);
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Allocated managed memory usage : {0,13:N0} KiB (min: {1,13:N0} max: {2,13:N0})         ",
                    _memoryUsage.ManagedMemory.CurrentMemoryUsage,
                    _memoryUsage.ManagedMemory.MinimumMemoryUsage,
                    _memoryUsage.ManagedMemory.MaximumMemoryUsage));
            Console.ResetColor();
        }

        private static void PrintExecutedOperationGroups()
        {
            Console.SetCursorPosition(0, _lineForExecutedOperationGroups);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Executed operation groups: {_executedOperationGroups}   ");
            Console.ResetColor();
        }

        private static void PrintStates()
        {
            Console.SetCursorPosition(0, _lineForStates1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Count of 'Several times' for operation group = {_testCount}    ");
            Console.SetCursorPosition(0, _lineForStates2);
            Console.WriteLine($"Show memory usage only after the last test = {_showMemoryUsageOnlyTwoTimes}    ");
            Console.ResetColor();
        }

        private static void PrintAvailableCommands()
        {
            Console.SetCursorPosition(0, _lineForFirstAvailableCommand);
            Console.WriteLine($"Available commands (count: {_tests.Count})    ");
            foreach (var test in _tests)
            {
                var ctrl = test.WithCTRLModifier ? "CTRL + " : string.Empty;
                object key = test.ActionCharacter != '\0' ? test.ActionCharacter : test.ActionConsoleKey;
                Console.WriteLine(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0,12} - {1}",
                        ctrl.ToString() + key.ToString(),
                        test.Description));
            }
        }

        private static void PrintError(string error)
        {
            Console.SetCursorPosition(0, _lineForError);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        private static void ClearError()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, _lineForError);
            Console.WriteLine(text);
        }

        private static void SetProgressForGroup(string text, int position, int length)
        {
            Console.SetCursorPosition(0, _lineForTestGroup);
            Console.WriteLine($"{text}: {100 * position / length}%");
        }

        private static void ClearProgressForGroup()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, _lineForTestGroup);
            Console.WriteLine(text);
        }

        private static void SetProgressForTest(string text, int position, int length)
        {
            Console.SetCursorPosition(0, _lineForSingleTest);
            Console.WriteLine($"{text}: {100 * position / length}%");
        }

        private static void ClearProgressForTest()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, _lineForSingleTest);
            Console.WriteLine(text);
        }

        private static void ProcessCommand(ConsoleKeyInfo key)
        {
            ClearError();
            foreach (var test in _tests)
            {
                var ctrlOk = test.WithCTRLModifier ? key.Modifiers.HasFlag(ConsoleModifiers.Control) : !key.Modifiers.HasFlag(ConsoleModifiers.Control);
                var actionCharacterOk = test.ActionCharacter != '\0' && test.ActionCharacter == key.KeyChar;
                var actionConsoleKeyOk = test.ActionConsoleKey != ConsoleKey.Oem102 && test.ActionConsoleKey == key.Key;
                if (ctrlOk && (actionCharacterOk || actionConsoleKeyOk))
                {
                    PrintMemoryUsage();
                    test.Action(test);
                    PrintMemoryUsage();
                    break;
                }
            }
        }

        /// <summary>
        /// Application's method.
        /// </summary>
        public static void Main()
        {
            if (File.Exists(Path.GetFullPath(_pdfFile2)))
            {
                _pdfFileToUse = Path.GetFullPath(_pdfFile2);
            }
            else
            {
                _pdfFileToUse = Path.GetFullPath(_pdfFile1);
            }

            Console.CancelKeyPress += (s, e) =>
            {
                _cancelOperation = true;
                e.Cancel = true;
            };

            try
            {
                while (true)
                {
                    try
                    {
                        Console.Title = Assembly.GetEntryAssembly().GetName().Name;
                        Console.CursorVisible = false;
                        Console.Clear();

                        PrintStartInfo();
                        while (true)
                        {
                            PrintExecutedOperationGroups();
                            PrintStates();
                            PrintAvailableCommands();

                            var key = Console.ReadKey(true);
                            ProcessCommand(key);
                            _cancelOperation = false;
                        }
                    }
                    catch (TerminateApplicationRequiredException)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }
    }
}
