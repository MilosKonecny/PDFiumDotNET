namespace PDFiumDotNET.App.ComponentsTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.App.ComponentsTest.Exceptions;
    using PDFiumDotNET.Components.Factory;

    /// <summary>
    /// Application's class.
    /// </summary>
    internal class Program
    {
        private const int _minTestCount = 2;
        private const int _maxTestCount = 1100;
        private static int _testCount = 1080;
        private static int _executedOperationGroups = 0;

        private static List<Tuple<char, ConsoleKey, string, Action>> _tests = new ()
        {
            Tuple.Create('\0', ConsoleKey.UpArrow, "Increase execution count for operation group", IncreaseTestCount),
            Tuple.Create('\0', ConsoleKey.DownArrow, "Decrease execution count for operation group", DecreaseTestCount),
            Tuple.Create('x', ConsoleKey.Oem102, "Exit application", ExitApplication),
            Tuple.Create('1', ConsoleKey.Oem102, "Collect GC", FreeMemory),
            Tuple.Create('a', ConsoleKey.Oem102, "Execute operation group 1", OperationGroup1),
            Tuple.Create('A', ConsoleKey.Oem102, "Execute operation group 1 (many times)", OperationGroup1Many),
        };

        private static void IncreaseTestCount()
        {
            _testCount = Math.Min(++_testCount, _maxTestCount);
        }

        private static void DecreaseTestCount()
        {
            _testCount = Math.Max(--_testCount, _minTestCount);
        }

        private static void FreeMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static void OperationGroup1()
        {
            _executedOperationGroups++;
        }

        private static void OperationGroup1Many()
        {
            for (var index = 0; index < _testCount; index++)
            {
                OperationGroup1();
                PrintMemoryUsage();
                PrintExecutedOperationGroups();
            }
        }

        private static void ExitApplication()
        {
            throw new TerminateApplicationRequiredException();
        }

        private static void PrintStartInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(RuntimeInformation.FrameworkDescription + " / " + RuntimeInformation.ProcessArchitecture);
            Console.ResetColor();

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
            Console.WriteLine($"Many times = {_testCount}    ");
            Console.ResetColor();

            Console.SetCursorPosition(0, 6);
            Console.WriteLine($"Available commands (count: {_tests.Count})    ");
            foreach (var test in _tests)
            {
                object key = test.Item1 != '\0' ? test.Item1 : test.Item2;
                Console.WriteLine($"\t{key} - {test.Item3}");
            }
        }

        private static void ProcessCommand(ConsoleKeyInfo key)
        {
            foreach (var test in _tests)
            {
                if ((test.Item1 != '\0' && test.Item1 == key.KeyChar)
                    || (test.Item2 != ConsoleKey.Oem102 && test.Item2 == key.Key))
                {
                    test.Item4();
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
