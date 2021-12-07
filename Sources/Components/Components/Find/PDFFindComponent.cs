namespace PDFiumDotNET.Components.Find
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Find;
    using PDFiumDotNET.Components.Contracts.Observers;
    using PDFiumDotNET.Components.Page;
    using static PDFiumDotNET.Wrapper.Bridge.PDFiumBridge;

    /// <summary>
    /// Implemeents <see cref="IPDFFindComponent"/>.
    /// </summary>
    internal sealed partial class PDFFindComponent : IPDFFindComponent, IPDFDocumentObserver
    {
        #region Private fields

        private PDFComponent _mainComponent;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFFindComponent"/> class.
        /// </summary>
        public PDFFindComponent()
        {
        }

        #endregion Constructors

        #region Private methods - invoke event

        private void InvokePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Private methods - invoke event

        #region Implementation of IPDFFindComponent

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
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

            if (_mainComponent.PageComponent is PDFPageComponent pageComponent)
            {
                pageComponent.ClearSelectionRectangles();
            }

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

            foreach (var page in _mainComponent.PageComponent.Pages)
            {
                if (cancelFind || (progress != null && !progress(page.PageIndex)))
                {
                    break;
                }

                var pageHandle = _mainComponent.PDFiumBridge.FPDF_LoadPage(_mainComponent.PDFiumDocument, page.PageIndex);
                var textPageHandle = _mainComponent.PDFiumBridge.FPDFText_LoadPage(pageHandle);
                var globalText = Marshal.StringToHGlobalUni(text);
                var findHandle = _mainComponent.PDFiumBridge.FPDFText_FindStart(textPageHandle, globalText, flags, 0);
                Marshal.FreeHGlobal(globalText);

                PDFFindPage findPage = null;
                var charsOnPage = _mainComponent.PDFiumBridge.FPDFText_CountChars(textPageHandle);

                while (_mainComponent.PDFiumBridge.FPDFText_FindNext(findHandle))
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

                    var contextLength = 20;
                    var foundPosition = _mainComponent.PDFiumBridge.FPDFText_GetSchResultIndex(findHandle);
                    var foundCount = _mainComponent.PDFiumBridge.FPDFText_GetSchCount(findHandle);

                    int startPosition = Math.Max(foundPosition - contextLength, 0);
                    int endPosition = Math.Min(foundPosition + foundCount + contextLength, charsOnPage);
                    var contextGlobal = Marshal.AllocHGlobal((2 * (endPosition - startPosition)) + 2);
                    var writtenCount = _mainComponent.PDFiumBridge.FPDFText_GetText(textPageHandle, startPosition, endPosition - startPosition, contextGlobal);
                    var context = text;
                    if (writtenCount > 0)
                    {
                        context = Marshal.PtrToStringUni(contextGlobal);
                    }

                    context = context.Replace('\r', ' ').Replace('\n', ' ').Replace('\t', ' ');
                    Marshal.FreeHGlobal(contextGlobal);

                    var position = new PDFFindPosition(findPage)
                    {
                        Position = foundPosition,
                        Length = foundCount,
                        Context = context,
                    };
                    if (addPosition != null && !addPosition(findPage, position))
                    {
                        cancelFind = true;
                        break;
                    }
                }

                _mainComponent.PDFiumBridge.FPDFText_FindClose(findHandle);
                _mainComponent.PDFiumBridge.FPDFText_ClosePage(textPageHandle);
                _mainComponent.PDFiumBridge.FPDF_ClosePage(pageHandle);
            }

            return somethingFound;
        }

        /// <summary>
        /// Clears any allocations related to last <see cref="FindText"/>
        /// and all text selections on any page of opened PDF document.
        /// </summary>
        public void ClearFindSelections()
        {
            if (_mainComponent.PageComponent is PDFPageComponent pageComponent)
            {
                pageComponent.ClearSelectionRectangles();
            }
        }

        #endregion Implementation of IPDFFindComponent

        #region Implementation of IPDFChildComponent

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IPDFComponent MainComponent => _mainComponent;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void AttachedTo(IPDFComponent mainComponent)
        {
            var mc = mainComponent as PDFComponent;

            _mainComponent = mc ?? throw new ArgumentException(
                string.Format(CultureInfo.InvariantCulture, "The parameter {0} is not of expected type.", nameof(mainComponent)));
        }

        #endregion Implementation of IPDFChildComponent

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged

        #region Implementation of IDisposable

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            IsDisposed = true;
        }

        #endregion Implementation of IDisposable
    }
}
