namespace PDFiumDotNET.Wrapper.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PDFiumDotNET.Wrapper.Bridge;

    /// <summary>
    /// Unit test class implements some unit test methods for class ...
    /// </summary>
    [TestClass]
    public class SaveTest
    {
        #region Private fields

        private static string _pdfFilesFolder;

        #endregion Private fields

        #region General test things

        /// <summary>
        /// This context is the context of the first test started in this class - don't use the property TestName.
        /// </summary>
        private static TestContext _startTestContext;

        /// <summary>
        /// This folder is set before any test will start and it is set for every test to another value.
        /// </summary>
        private string _actualTestFolder;

        /// <summary>
        /// This context is the test context of every test method - set before every test starts.
        /// </summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        /// <summary>
        /// Unit test class initialization.
        /// </summary>
        /// <param name="context">Test context.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _startTestContext = context;
            _startTestContext?.WriteLine("Start of test in the class SaveTest");

            var position = _startTestContext.DeploymentDirectory.IndexOf("Sources", StringComparison.InvariantCultureIgnoreCase);
            var rootFolder = _startTestContext.DeploymentDirectory.Substring(0, position);
            _pdfFilesFolder = Path.Combine(rootFolder, @"TestData\PDFs");
        }

        /// <summary>
        /// Initialization for each test method. It is called before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _actualTestFolder = Path.Combine(TestContext.ResultsDirectory, TestContext.TestName);
            Directory.CreateDirectory(_actualTestFolder);
            File.WriteAllText(Path.Combine(_actualTestFolder, "!TestName.txt"), TestContext.TestName);
        }

        #endregion General test things

        private static bool WriteCallback(int blockNumber, string filePath, ref PDFiumBridge.FPDF_FILEWRITE pThis, IntPtr pData, ulong size)
        {
            FileStream fileStream;
            if (blockNumber == 0)
            {
                fileStream = File.Open(filePath, FileMode.Create);
            }
            else
            {
                fileStream = File.Open(filePath, FileMode.Append);
                fileStream.Seek(0, SeekOrigin.End);
            }

            byte[] managedArray = new byte[size];
            Marshal.Copy(pData, managedArray, 0, (int)size);
            fileStream.Write(managedArray, 0, (int)size);
            fileStream.Close();
            return true;
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_SaveAsCopy"/>.
        /// </summary>
        [TestMethod]
        public void CreatePDFDocument()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            var bridge = new PDFiumBridge();

            var oldDocument = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsNotNull(oldDocument);

            var newDocument = bridge.FPDF_CreateNewDocument();
            Assert.IsNotNull(newDocument);

            Assert.AreEqual(0, bridge.FPDF_GetPageCount(newDocument));
            bridge.FPDF_ImportPages(newDocument, oldDocument, "1,3,5", 0);
            Assert.AreEqual(3, bridge.FPDF_GetPageCount(newDocument));
            bridge.FPDF_ImportPages(newDocument, oldDocument, "100,300,500", 0);
            Assert.AreEqual(6, bridge.FPDF_GetPageCount(newDocument));

            bridge.FPDF_ImportPagesByIndex(newDocument, oldDocument, new List<int> { 100, 99, 98 }.ToArray(), 3, 0);
            Assert.AreEqual(9, bridge.FPDF_GetPageCount(newDocument));

            var blockNumber = 0;
            var newPDFFile = Path.Combine(_actualTestFolder, TestContext.TestName + "_SaveAsCopy.pdf");
            var fileWrite = new PDFiumBridge.FPDF_FILEWRITE
            {
                Version = 1,
                WriteBlockMethod = (ref PDFiumBridge.FPDF_FILEWRITE a, IntPtr b, ulong c) => WriteCallback(blockNumber++, newPDFFile, ref a, b, c),
            };
            var result = bridge.FPDF_SaveAsCopy(newDocument, ref fileWrite, PDFiumBridge.FPDF_SAVEFLAGS.FPDF_INCREMENTAL);
            Assert.IsTrue(result);

            newPDFFile = Path.Combine(_actualTestFolder, TestContext.TestName + "_SaveWithVersion.pdf");
            result = bridge.FPDF_SaveWithVersion(newDocument, ref fileWrite, PDFiumBridge.FPDF_SAVEFLAGS.FPDF_INCREMENTAL, 160);
            Assert.IsTrue(result);

            bridge.FPDF_CloseDocument(oldDocument);
            bridge.FPDF_CloseDocument(newDocument);

            bridge.Dispose();
        }

        /// <summary>
        /// Test method for <see cref="PDFiumBridge.FPDF_SaveAsCopy"/>.
        /// </summary>
        [TestMethod]
        public void CreatePDFDocumentNPagesToOne()
        {
            var pdfFile = Path.Combine(_pdfFilesFolder, "Precalculus.pdf");

            var bridge = new PDFiumBridge();

            var oldDocument = bridge.FPDF_LoadDocument(pdfFile, null);
            Assert.IsNotNull(oldDocument);

            var newDocument = bridge.FPDF_ImportNPagesToOne(oldDocument, 100, 100, 20, 10);
            Assert.IsNotNull(newDocument);
            Assert.AreEqual(6, bridge.FPDF_GetPageCount(newDocument));

            var blockNumber = 0;
            var newPDFFile = Path.Combine(_actualTestFolder, TestContext.TestName + "_SaveAsCopy.pdf");
            var fileWrite = new PDFiumBridge.FPDF_FILEWRITE
            {
                Version = 1,
                WriteBlockMethod = (ref PDFiumBridge.FPDF_FILEWRITE a, IntPtr b, ulong c) => WriteCallback(blockNumber++, newPDFFile, ref a, b, c),
            };
            var result = bridge.FPDF_SaveAsCopy(newDocument, ref fileWrite, PDFiumBridge.FPDF_SAVEFLAGS.FPDF_INCREMENTAL);
            Assert.IsTrue(result);

            newPDFFile = Path.Combine(_actualTestFolder, TestContext.TestName + "_SaveWithVersion.pdf");
            result = bridge.FPDF_SaveWithVersion(newDocument, ref fileWrite, PDFiumBridge.FPDF_SAVEFLAGS.FPDF_INCREMENTAL, 160);
            Assert.IsTrue(result);

            bridge.FPDF_CloseDocument(oldDocument);
            bridge.FPDF_CloseDocument(newDocument);

            bridge.Dispose();
        }
    }
}
