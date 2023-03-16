namespace PDFiumDotNET.App.ComponentsTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using PDFiumDotNET.App.ComponentsTest.Exceptions;
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
        private static int _testCount = 1000;
        private static int _executedOperationGroups = 0;

        private static List<Tuple<char, ConsoleKey, string, Action<string>>> _tests = new ()
        {
            Tuple.Create('\0', ConsoleKey.UpArrow, "Increase 'several times' for operation group", Increase1TestCount),
            Tuple.Create('\0', ConsoleKey.RightArrow, "Increase 'several times' by 100 for operation group", Increase100TestCount),
            Tuple.Create('\0', ConsoleKey.DownArrow, "Decrease 'several times' for operation group", Decrease1TestCount),
            Tuple.Create('\0', ConsoleKey.LeftArrow, "Decrease 'several times' by 100 for operation group", Decrease100TestCount),
            Tuple.Create('x', ConsoleKey.Oem102, "Exit application", ExitApplication),
            Tuple.Create('1', ConsoleKey.Oem102, "Collect GC", FreeMemory),
            Tuple.Create('a', ConsoleKey.Oem102, "Operation group 1 - open & close", OperationGroup1),
            Tuple.Create('A', ConsoleKey.Oem102, "Operation group 1 (several times)", OperationGroup1SeveralTimes),
            Tuple.Create('b', ConsoleKey.Oem102, "Operation group 2 - 1 & navigate & zoom & position", OperationGroup2),
            Tuple.Create('B', ConsoleKey.Oem102, "Operation group 2 (several times)", OperationGroup2SeveralTimes),
            Tuple.Create('c', ConsoleKey.Oem102, "Operation group 3 - 1 & render", OperationGroup3),
            Tuple.Create('C', ConsoleKey.Oem102, "Operation group 3 (several times)", OperationGroup3SeveralTimes),
        };

        private static void Increase1TestCount(string description)
        {
            _testCount = Math.Min(_testCount + 1, _maxTestCount);
        }

        private static void Increase100TestCount(string description)
        {
            _testCount = Math.Min(_testCount + 100, _maxTestCount);
        }

        private static void Decrease1TestCount(string description)
        {
            _testCount = Math.Max(_testCount - 1, _minTestCount);
        }

        private static void Decrease100TestCount(string description)
        {
            _testCount = Math.Max(_testCount - 100, _minTestCount);
        }

        private static void FreeMemory(string description)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static void OperationGroup1(string description)
        {
            var pdfComponent = PDFFactory.PDFComponent;
            var result = pdfComponent.OpenDocument(_pdfFile);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document not opened! {result}");
                return;
            }

            pdfComponent.CloseDocument();
            pdfComponent.Dispose();

            _executedOperationGroups++;
        }

        private static void OperationGroup1SeveralTimes(string description)
        {
            for (var index = 0; index < _testCount; index++)
            {
                SetProgress1(description, index, _testCount);
                OperationGroup1(description);
                PrintMemoryUsage();
                PrintExecutedOperationGroups();
            }

            ClearProgress1();
        }

        private static void OperationGroup2(string description)
        {
            var pdfComponent = PDFFactory.PDFComponent;
            var pageComponent = pdfComponent.LayoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var result = pdfComponent.OpenDocument(_pdfFile);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document not opened! {result}");
                return;
            }

            var zoomComponent = pageComponent.ZoomComponent;
            for (var index = 0; index < pageComponent.PageCount; index++)
            {
                pageComponent.NavigateToPage(index + 1);
                var position1 = pageComponent.RenderManager.PagePosition(index);
                if (index % 2 == 0)
                {
                    zoomComponent.IncreaseZoom();
                }
                else
                {
                    zoomComponent.DecreaseZoom();
                }

                var position2 = pageComponent.RenderManager.PagePosition(index);
            }

            pdfComponent.CloseDocument();
            pdfComponent.Dispose();

            _executedOperationGroups++;
        }

        private static void OperationGroup2SeveralTimes(string description)
        {
            for (var index = 0; index < _testCount; index++)
            {
                SetProgress1(description, index, _testCount);
                OperationGroup2(description);
                PrintMemoryUsage();
                PrintExecutedOperationGroups();
            }

            ClearProgress1();
        }

        private static void OperationGroup3(string description)
        {
            var pdfComponent = PDFFactory.PDFComponent;
            var pageComponent = pdfComponent.LayoutComponent.CreatePageComponent("standard", PageLayoutType.Standard);
            var result = pdfComponent.OpenDocument(_pdfFile);
            if (result != OpenDocumentResult.Success)
            {
                PrintError($"Document not opened! {result}");
                return;
            }

            var zoomComponent = pageComponent.ZoomComponent;
            zoomComponent.CurrentZoomFactor = 0.1;
            for (var index = 0; index < pageComponent.PageCount; index++)
            {
                PrintMemoryUsage();
                SetProgress2(description, index, pageComponent.PageCount);
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
            }

            pdfComponent.CloseDocument();
            pdfComponent.Dispose();

            ClearProgress2();

            _executedOperationGroups++;
        }

        private static void OperationGroup3SeveralTimes(string description)
        {
            for (var index = 0; index < _testCount; index++)
            {
                SetProgress1(description, index, _testCount);
                OperationGroup3(description);
                PrintMemoryUsage();
                PrintExecutedOperationGroups();
            }

            ClearProgress1();
        }

        private static void ExitApplication(string description)
        {
            throw new TerminateApplicationRequiredException();
        }

        private static void PrintStartInfo()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(RuntimeInformation.FrameworkDescription + " / " + RuntimeInformation.ProcessArchitecture);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Memory usage after start: {Process.GetCurrentProcess().PrivateMemorySize64 / 1024} KiB       ");
            Console.ResetColor();
        }

        private static void PrintMemoryUsage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"Memory usage: {Process.GetCurrentProcess().PrivateMemorySize64 / 1024} KiB       ");
            Console.ResetColor();
        }

        private static void PrintExecutedOperationGroups()
        {
            Console.SetCursorPosition(0, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Executed operation groups: {_executedOperationGroups}   ");
            Console.ResetColor();
        }

        private static void PrintAvailableCommands()
        {
            Console.SetCursorPosition(0, 4);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"'Several times' for operation group = {_testCount}    ");
            Console.ResetColor();

            Console.SetCursorPosition(0, 6);
            Console.WriteLine($"Available commands (count: {_tests.Count})    ");
            foreach (var test in _tests)
            {
                object key = test.Item1 != '\0' ? test.Item1 : test.Item2;
                Console.WriteLine($"\t{key} - {test.Item3}");
            }
        }

        private static void PrintError(string error)
        {
            Console.SetCursorPosition(0, 8 + _tests.Count);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        private static void ClearError()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, 8 + _tests.Count);
            Console.WriteLine(text);
        }

        private static void SetProgress1(string text, int position, int length)
        {
            Console.SetCursorPosition(0, 10 + _tests.Count);
            Console.WriteLine($"{text}: {100 * position / length}%");
        }

        private static void ClearProgress1()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, 10 + _tests.Count);
            Console.WriteLine(text);
        }

        private static void SetProgress2(string text, int position, int length)
        {
            Console.SetCursorPosition(0, 11 + _tests.Count);
            Console.WriteLine($"{text}: {100 * position / length}%");
        }

        private static void ClearProgress2()
        {
            var text = new string(' ', Console.BufferWidth);
            Console.SetCursorPosition(0, 11 + _tests.Count);
            Console.WriteLine(text);
        }

        private static void ProcessCommand(ConsoleKeyInfo key)
        {
            ClearError();
            foreach (var test in _tests)
            {
                if ((test.Item1 != '\0' && test.Item1 == key.KeyChar)
                    || (test.Item2 != ConsoleKey.Oem102 && test.Item2 == key.Key))
                {
                    test.Item4(test.Item3);
                    break;
                }
            }
        }

        /// <summary>
        /// Application's method.
        /// </summary>
        public static void Main()
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
                }
            }
            catch (TerminateApplicationRequiredException)
            {
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
