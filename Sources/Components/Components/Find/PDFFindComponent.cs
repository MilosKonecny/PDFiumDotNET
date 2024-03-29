﻿namespace PDFiumDotNET.Components.Find
{
    using System;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Page;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <summary>
    /// The class implements the functionality defined by <see cref="IPDFFindComponent"/>.
    /// </summary>
    internal sealed partial class PDFFindComponent : PDFChildComponent, IPDFFindComponent
    {
        #region Private fields

        private readonly PDFPageComponent _pageComponent;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFFindComponent"/> class.
        /// </summary>
        /// <param name="pageComponent">Page component where the find will be performed.</param>
        public PDFFindComponent(PDFPageComponent pageComponent)
        {
            _pageComponent = pageComponent ?? throw new ArgumentNullException(nameof(pageComponent));
        }

        #endregion Constructors

        #region Implementation of IPDFFindComponent

        /// <inheritdoc/>
        public bool FindText(
            string text,
            bool caseSensitive,
            bool wholeWords,
            Func<int, bool> progress,
            Func<IPDFFindPage, bool> addPage,
            Func<IPDFFindPage, IPDFFindPosition, bool> addPosition)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            if (progress == null && addPage == null && addPosition == null)
            {
                return false;
            }

            _pageComponent.ClearSelectionRectangles();

            FPDF_FIND_FLAGS flags = FPDF_FIND_FLAGS.FPDF_NONE;
            if (caseSensitive)
            {
                flags |= FPDF_FIND_FLAGS.FPDF_MATCHCASE;
            }

            if (wholeWords)
            {
                flags |= FPDF_FIND_FLAGS.FPDF_MATCHWHOLEWORD;
            }

            var somethingFound = false;
            var cancelFind = false;

            foreach (var page in _pageComponent.Pages)
            {
                if (cancelFind || (progress != null && !progress(page.PageIndex)))
                {
                    break;
                }

                var pageHandle = PDFComponent.PDFiumBridge.FPDF_LoadPage(PDFComponent.PDFiumDocument, page.PageIndex);
                var textPageHandle = PDFComponent.PDFiumBridge.FPDFText_LoadPage(pageHandle);
                var globalText = Marshal.StringToHGlobalUni(text);
                var findHandle = PDFComponent.PDFiumBridge.FPDFText_FindStart(textPageHandle, globalText, flags, 0);
                Marshal.FreeHGlobal(globalText);

                PDFFindPage findPage = null;
                var charsOnPage = PDFComponent.PDFiumBridge.FPDFText_CountChars(textPageHandle);

                while (PDFComponent.PDFiumBridge.FPDFText_FindNext(findHandle))
                {
                    somethingFound = true;
                    if (findPage == null)
                    {
                        findPage = new PDFFindPage(page);
                        if (addPage != null && !addPage(findPage))
                        {
                            cancelFind = true;
                            break;
                        }
                    }

                    var contextLengthOneHalf = 20;
                    var foundCharIndex = PDFComponent.PDFiumBridge.FPDFText_GetSchResultIndex(findHandle);
                    var foundTextIndex = PDFComponent.PDFiumBridge.FPDFText_GetTextIndexFromCharIndex(textPageHandle, foundCharIndex);
                    var foundTextCount = PDFComponent.PDFiumBridge.FPDFText_GetSchCount(findHandle);

                    int startPosition = Math.Max(foundTextIndex - contextLengthOneHalf, 0);
                    int endPosition = Math.Min(foundTextIndex + foundTextCount + contextLengthOneHalf, charsOnPage);
                    var contextGlobal = Marshal.AllocHGlobal((2 * (endPosition - startPosition)) + 2);
                    var writtenCount = PDFComponent.PDFiumBridge.FPDFText_GetText(textPageHandle, startPosition, endPosition - startPosition, contextGlobal);
                    var context = text;
                    if (writtenCount > 0)
                    {
                        context = Marshal.PtrToStringUni(contextGlobal);
                    }

                    context = context.Replace('\r', ' ').Replace('\n', ' ').Replace('\t', ' ');
                    Marshal.FreeHGlobal(contextGlobal);

                    var position = new PDFFindPosition(findPage)
                    {
                        Position = foundTextIndex,
                        Length = foundTextCount,
                        Context = context,
                    };
                    if (addPosition != null && !addPosition(findPage, position))
                    {
                        cancelFind = true;
                        break;
                    }
                }

                PDFComponent.PDFiumBridge.FPDFText_FindClose(findHandle);
                PDFComponent.PDFiumBridge.FPDFText_ClosePage(textPageHandle);
                PDFComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);
            }

            return somethingFound;
        }

        /// <summary>
        /// Clears any allocations related to last <see cref="FindText"/>
        /// and all text selections on any page of opened PDF document.
        /// </summary>
        public void ClearFindSelections()
        {
            _pageComponent.ClearSelectionRectangles();
        }

        #endregion Implementation of IPDFFindComponent
    }
}
