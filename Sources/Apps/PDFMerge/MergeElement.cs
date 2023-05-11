namespace PDFiumDotNET.Apps.PDFMerge
{
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// View model class implements <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class MergeElement : INotifyPropertyChanged
    {
        #region Private fields

        private string _pdfFile;
        private bool _mergeAllPages;
        private int _addPagesBeforeMergedDocument;
        private int _addPagesAfterMergedDocument;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MergeElement"/> class.
        /// </summary>
        public MergeElement()
        {
            MergeAllPages = true;
            AddPagesBeforeMergedDocument = 0;
            AddPagesAfterMergedDocument = 0;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets or sets the PDF file to merge.
        /// </summary>
        public string PDFFile
        {
            get
            {
                return _pdfFile;
            }

            set
            {
                if (_pdfFile != value)
                {
                    _pdfFile = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets the name of file. Used for sorting.
        /// </summary>
        public string PDFFileName
        {
            get
            {
                return Path.GetFileName(PDFFile ?? string.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the information whether all pages are to import.
        /// </summary>
        public bool MergeAllPages
        {
            get
            {
                return _mergeAllPages;
            }

            set
            {
                if (_mergeAllPages != value)
                {
                    _mergeAllPages = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the count of pages to add before merged PDF document.
        /// </summary>
        public int AddPagesBeforeMergedDocument
        {
            get
            {
                return _addPagesBeforeMergedDocument;
            }

            set
            {
                if (_addPagesBeforeMergedDocument != value)
                {
                    _addPagesBeforeMergedDocument = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the count of pages to add after merged PDF document.
        /// </summary>
        public int AddPagesAfterMergedDocument
        {
            get
            {
                return _addPagesAfterMergedDocument;
            }

            set
            {
                if (_addPagesAfterMergedDocument != value)
                {
                    _addPagesAfterMergedDocument = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        #endregion Public properties

        #region Private invoke event methods

        /// <summary>
        /// The method invokes the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Parameter name used in <see cref="PropertyChangedEventArgs"/>.</param>
        private void InvokePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Private invoke event methods

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged
    }
}
