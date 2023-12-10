﻿namespace PDFiumDotNET.Apps.TestComponents
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
        private const int _minTestCount = 2;
        private const int _maxTestCount = 2000;
        private const string _pdfFile = @"..\..\..\..\..\..\TestData\PDFs\Precalculus.pdf";
        private static int _testCount = 100;
        private static int _executedOperationGroups = 0;
        private static MemoryUsage _memoryUsage = new ();
        private static bool _cancelOperation = false;

        private static List<ActionDescription> _tests = new ()
        {
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
                'c',
                ConsoleKey.Oem102,
                true,
                "This action stops the executed test.",
                "Exit application",
                "Exit application",
                StopTest),
            new ActionDescription(
                'r',
                ConsoleKey.Oem102,
                false,
                "This action resets min/max memory usage to current.",
                "Reset memory usage",
                "Reset memory usage",
                ResetMinMax),
            new ActionDescription(
                'x',
                ConsoleKey.Oem102,
                false,
                "This action terminates the application.",
                "Exit application",
                "Exit application",
                ExitApplication),
            new ActionDescription(
                '1',
                ConsoleKey.Oem102,
                false,
                "This action forces an immediate garbage collection of all generations.",
                "Collect GC",
                "Collect GC",
                FreeMemory),
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
            var result = pdfComponent.OpenDocument(_pdfFile);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document not open! {result}");
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
                PrintMemoryUsage();
                SetProgressForGroup(description.GroupText, index, _testCount);
                OperationGroup1(description);
                PrintMemoryUsage();
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
            var result = pdfComponent.OpenDocument(_pdfFile);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document not open! {result}");
                return;
            }

            var pageComponent = pdfComponent.LayoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var zoomComponent = pageComponent.ZoomComponent;
            for (var index = 0; index < pageComponent.PageCount; index++)
            {
                PrintMemoryUsage();
                SetProgressForTest(description.Text, index, pageComponent.PageCount);
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

            ClearProgressForTest();

            _executedOperationGroups++;
        }

        private static void OperationGroup2SeveralTimes(ActionDescription description)
        {
            for (var index = 0; index < _testCount; index++)
            {
                PrintMemoryUsage();
                SetProgressForGroup(description.GroupText, index, _testCount);
                OperationGroup2(description);
                PrintMemoryUsage();
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
            var result = pdfComponent.OpenDocument(_pdfFile);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document not open! {result}");
                return;
            }

            var zoomComponent = pageComponent.ZoomComponent;
            zoomComponent.CurrentZoomFactor = 0.1;
            for (var index = 0; index < pageComponent.PageCount; index++)
            {
                PrintMemoryUsage();
                SetProgressForTest(description.Text, index, pageComponent.PageCount);
                var page = pageComponent.Pages[index];

                var bitmap = new WriteableBitmap((int)page.Width, (int)page.Height, 96, 96, PixelFormats.Bgra32, null);
                var format = BitmapFormat.BitmapBGRA;

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
                PrintMemoryUsage();
                SetProgressForGroup(description.GroupText, index, _testCount);
                OperationGroup3(description);
                PrintMemoryUsage();
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
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(CommonInformation.Info);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Memory usage after start: Private={_memoryUsage.CurrentPrivateMemoryUsage:N0} / Physical={_memoryUsage.CurrentWorkingSetMemoryUsage:N0} / Virtual={_memoryUsage.CurrentVirtualMemoryUsage:N0} KiB       ");
            Console.ResetColor();
        }

        private static void PrintMemoryUsage()
        {
            _memoryUsage.GatherMemoryUsage();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Private memory usage   : {0,12:N0} KiB (min: {1,12:N0} max: {2,12:N0})         ",
                    _memoryUsage.CurrentPrivateMemoryUsage,
                    _memoryUsage.MinimumPrivateMemoryUsage,
                    _memoryUsage.MaximumPrivateMemoryUsage));
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Physical memory usage  : {0,12:N0} KiB (min: {1,12:N0} max: {2,12:N0})         ",
                    _memoryUsage.CurrentWorkingSetMemoryUsage,
                    _memoryUsage.MinimumWorkingSetMemoryUsage,
                    _memoryUsage.MaximumWorkingSetMemoryUsage));
            Console.SetCursorPosition(0, 4);
            Console.WriteLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Virtual memory usage   : {0,12:N0} KiB (min: {1,12:N0} max: {2,12:N0})         ",
                    _memoryUsage.CurrentVirtualMemoryUsage,
                    _memoryUsage.MinimumVirtualMemoryUsage,
                    _memoryUsage.MaximumVirtualMemoryUsage));
            Console.ResetColor();
        }

        private static void PrintExecutedOperationGroups()
        {
            Console.SetCursorPosition(0, 5);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Executed operation groups: {_executedOperationGroups}   ");
            Console.ResetColor();
        }

        private static void PrintAvailableCommands()
        {
            Console.SetCursorPosition(0, 6);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"'Several times' for operation group = {_testCount}    ");
            Console.ResetColor();

            Console.SetCursorPosition(0, 7);
            Console.WriteLine($"Available commands (count: {_tests.Count})    ");
            foreach (var test in _tests)
            {
                var ctrl = test.WithCTRLModifier ? "CTRL + " : string.Empty;
                object key = test.ActionCharacter != '\0' ? test.ActionCharacter : test.ActionConsoleKey;
                Console.WriteLine($"  {ctrl}{key} - {test.Description}");
            }
        }

        private static void PrintError(string error)
        {
            Console.SetCursorPosition(0, 10 + _tests.Count);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        private static void ClearError()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, 10 + _tests.Count);
            Console.WriteLine(text);
        }

        private static void SetProgressForGroup(string text, int position, int length)
        {
            Console.SetCursorPosition(0, 12 + _tests.Count);
            Console.WriteLine($"{text}: {100 * position / length}%");
        }

        private static void ClearProgressForGroup()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, 12 + _tests.Count);
            Console.WriteLine(text);
        }

        private static void SetProgressForTest(string text, int position, int length)
        {
            Console.SetCursorPosition(0, 13 + _tests.Count);
            Console.WriteLine($"{text}: {100 * position / length}%");
        }

        private static void ClearProgressForTest()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, 13 + _tests.Count);
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
                            PrintMemoryUsage();
                            PrintExecutedOperationGroups();
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
