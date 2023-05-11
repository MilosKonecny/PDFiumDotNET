namespace PDFiumDotNET.Apps.PDFMerge
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using Microsoft.Win32;
    using PDFiumDotNET.Apps.Common;
    using PDFiumDotNET.Apps.PDFMerge.Contracts;
    using PDFiumDotNET.Wrapper.Bridge;

    /// <summary>
    /// View model class implements <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class MainViewModel : IViewModel, INotifyPropertyChanged
    {
        #region Private fields

        private readonly Licenses _licenses = new Licenses();
        private readonly ObservableCollection<MergeElement> _filesToMerge = new ();
        private int _selectedFileIndex;
        private string _title;
        private string _pdfFileToCreate;

        private bool _isAboutAreaVisible = false;
        private bool _isWorkAreaVisible = true;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            Title = "PDF Merge";

            ChoosePDFFileToCreateCommand = new ViewModelCommand(ExecuteChoosePDFFileToCreateCommand);
            MoveUpElementCommand = new ViewModelCommand(ExecuteMoveUpElementCommand, CanExecuteMoveUpElementCommand);
            RemoveElementCommand = new ViewModelCommand(ExecuteRemoveElementCommand, CanExecuteRemoveElementCommand);
            MoveDownElementCommand = new ViewModelCommand(ExecuteMoveDownElementCommand, CanExecuteMoveDownElementCommand);
            SortElementsAscendingCommand = new ViewModelCommand(ExecuteSortElementsAscendingCommand, CanExecuteSortElementsAscendingCommand);
            SortElementsDescendingCommand = new ViewModelCommand(ExecuteSortElementsDescendingCommand, CanExecuteSortElementsDescendingCommand);
            AboutCommand = new ViewModelCommand(ExecuteAboutCommand);
            CloseAboutCommand = new ViewModelCommand(ExecuteCloseAboutCommand);
            RemoveAllElementsCommand = new ViewModelCommand(ExecuteRemoveAllElementsCommand, CanExecuteRemoveAllElementsCommand);
            MergeElementsCommand = new ViewModelCommand(ExecuteMergeElementsCommand, CanExecuteMergeElementsCommand);
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets Icon8 license.
        /// </summary>
        public static string Icon8License
        {
            get
            {
                return Licenses.Icon8License;
            }
        }

        /// <summary>
        /// Gets the associated view.
        /// </summary>
        public IView View { get; private set; }

        /// <summary>
        /// Gets or sets the title of view.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title != value)
                {
                    _title = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the file to create as a result of merge process.
        /// </summary>
        public string PDFFileToCreate
        {
            get
            {
                return _pdfFileToCreate;
            }

            set
            {
                if (_pdfFileToCreate != value)
                {
                    _pdfFileToCreate = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets the collection of files to merge into target file.
        /// </summary>
        public ObservableCollection<MergeElement> FilesToMerge
        {
            get
            {
                return _filesToMerge;
            }
        }

        /// <summary>
        /// Gets or sets the index of actual selected file.
        /// </summary>
        public int SelectedFileIndex
        {
            get
            {
                return _selectedFileIndex;
            }

            set
            {
                if (_selectedFileIndex != value)
                {
                    _selectedFileIndex = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the information whether the work area is visible or not.
        /// </summary>
        public bool IsWorkAreaVisible
        {
            get
            {
                return _isWorkAreaVisible;
            }

            set
            {
                if (_isWorkAreaVisible != value)
                {
                    _isWorkAreaVisible = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the information whether the about area is visible or not.
        /// </summary>
        public bool IsAboutAreaVisible
        {
            get
            {
                return _isAboutAreaVisible;
            }

            set
            {
                if (_isAboutAreaVisible != value)
                {
                    _isAboutAreaVisible = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets my own license.
        /// </summary>
        public string MyOwnLicense
        {
            get
            {
                return _licenses.MyOwnLicense;
            }
        }

        /// <summary>
        /// Gets PDFium license.
        /// </summary>
        public string PDFiumLicense
        {
            get
            {
                return _licenses.PDFiumLicense;
            }
        }

        #endregion Public properties

        #region Public properties - commands

        /// <summary>
        /// Command for 'choose file to create'.
        /// </summary>
        public ViewModelCommand ChoosePDFFileToCreateCommand { get; private set; }

        /// <summary>
        /// Command for 'move up element'.
        /// </summary>
        public ViewModelCommand MoveUpElementCommand { get; private set; }

        /// <summary>
        /// Command for 'remove element'.
        /// </summary>
        public ViewModelCommand RemoveElementCommand { get; private set; }

        /// <summary>
        /// Command for 'move down element'.
        /// </summary>
        public ViewModelCommand MoveDownElementCommand { get; private set; }

        /// <summary>
        /// Command for 'sort elements ascending'.
        /// </summary>
        public ViewModelCommand SortElementsAscendingCommand { get; private set; }

        /// <summary>
        /// Command for 'sort elements descending'.
        /// </summary>
        public ViewModelCommand SortElementsDescendingCommand { get; private set; }

        /// <summary>
        /// Command for 'about'.
        /// </summary>
        public ViewModelCommand AboutCommand { get; private set; }

        /// <summary>
        /// Command for 'remove all elements'.
        /// </summary>
        public ViewModelCommand RemoveAllElementsCommand { get; private set; }

        /// <summary>
        /// Command for 'merge elements'.
        /// </summary>
        public ViewModelCommand MergeElementsCommand { get; private set; }

        /// <summary>
        /// Command for 'close about'.
        /// </summary>
        public ViewModelCommand CloseAboutCommand { get; private set; }

        #endregion Public properties - commands

        #region Private static methods

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

        #endregion Private static methods

        #region Private methods - commands related

        private void ExecuteChoosePDFFileToCreateCommand()
        {
            var dialog = new OpenFileDialog
            {
                // ToDo: Hard coded text.
                Filter = "PDF Files|*.pdf|All files|*.*",
                Multiselect = false,
                CheckFileExists = false,
                CheckPathExists = true,
            };

            if (dialog.ShowDialog() == false)
            {
                return;
            }

            PDFFileToCreate = dialog.FileName;
        }

        private void ExecuteMoveUpElementCommand()
        {
            var index = SelectedFileIndex;
            var file = FilesToMerge[index];
            FilesToMerge.RemoveAt(index);
            FilesToMerge.Insert(index - 1, file);
            SelectedFileIndex = index - 1;
        }

        private bool CanExecuteMoveUpElementCommand()
        {
            return SelectedFileIndex >= 1 && SelectedFileIndex < _filesToMerge.Count;
        }

        private void ExecuteRemoveElementCommand()
        {
            var index = SelectedFileIndex;
            FilesToMerge.RemoveAt(index);
            SelectedFileIndex = index - 1 >= 0 ? index - 1 : (FilesToMerge.Count > 0 ? 0 : -1);
        }

        private bool CanExecuteRemoveElementCommand()
        {
            return SelectedFileIndex >= 0 && SelectedFileIndex < _filesToMerge.Count;
        }

        private void ExecuteMoveDownElementCommand()
        {
            var index = SelectedFileIndex;
            var file = FilesToMerge[index];
            FilesToMerge.RemoveAt(index);
            FilesToMerge.Insert(index + 1, file);
            SelectedFileIndex = index + 1;
        }

        private bool CanExecuteMoveDownElementCommand()
        {
            return SelectedFileIndex >= 0 && SelectedFileIndex < _filesToMerge.Count - 1;
        }

        private void ExecuteSortElementsAscendingCommand()
        {
            var list = new List<MergeElement>(FilesToMerge);
            list.Sort(
                (x, y) =>
                {
                    return string.Compare(x.PDFFileName, y.PDFFileName, StringComparison.OrdinalIgnoreCase);
                });
            FilesToMerge.Clear();
            list.ForEach(FilesToMerge.Add);
        }

        private bool CanExecuteSortElementsAscendingCommand()
        {
            return FilesToMerge.Count > 1;
        }

        private void ExecuteSortElementsDescendingCommand()
        {
            var list = new List<MergeElement>(FilesToMerge);
            list.Sort(
                (x, y) =>
                {
                    var retValue = string.Compare(x.PDFFileName, y.PDFFileName, StringComparison.OrdinalIgnoreCase);
                    if (retValue == 0)
                    {
                        return 0;
                    }

                    if (retValue > 0)
                    {
                        return -1;
                    }

                    return 1;
                });
            FilesToMerge.Clear();
            list.ForEach(FilesToMerge.Add);
        }

        private bool CanExecuteSortElementsDescendingCommand()
        {
            return FilesToMerge.Count > 1;
        }

        private void ExecuteAboutCommand()
        {
            IsAboutAreaVisible = true;
            IsWorkAreaVisible = false;
        }

        private void ExecuteCloseAboutCommand()
        {
            IsAboutAreaVisible = false;
            IsWorkAreaVisible = true;
        }

        private void ExecuteRemoveAllElementsCommand()
        {
            FilesToMerge.Clear();
        }

        private bool CanExecuteRemoveAllElementsCommand()
        {
            return FilesToMerge.Count > 0;
        }

        private void ExecuteMergeElementsCommand()
        {
            try
            {
                var bridge = new PDFiumBridge();
                var newDocument = bridge.FPDF_CreateNewDocument();

                foreach (var element in FilesToMerge)
                {
                    var document = bridge.FPDF_LoadDocument(element.PDFFile, null);
                    bridge.FPDF_ImportPages(newDocument, document, null, bridge.FPDF_GetPageCount(newDocument));
                }

                var blockNumber = 0;
                var fileWrite = new PDFiumBridge.FPDF_FILEWRITE
                {
                    Version = 1,
                    WriteBlockMethod = (ref PDFiumBridge.FPDF_FILEWRITE a, IntPtr b, ulong c) => WriteCallback(blockNumber++, PDFFileToCreate, ref a, b, c),
                };
                if (!bridge.FPDF_SaveAsCopy(newDocument, ref fileWrite, PDFiumBridge.FPDF_SAVEFLAGS.FPDF_INCREMENTAL))
                {
                    View.ShowMessage("PDFMerge", "Document was not created!");
                }

                bridge.Dispose();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                View.ShowExceptionInfo("PDFMerge", ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        private bool CanExecuteMergeElementsCommand()
        {
            return FilesToMerge.Count > 1;
        }

        #endregion Private methods - commands related

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

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged

        #region Implementation of IViewModel

        /// <inheritdoc/>
        public void AssignedToView(IView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <inheritdoc/>
        public void AddFilesToList(string[] files)
        {
            if (files == null || files.Length == 0)
            {
                return;
            }

            var bridge = new PDFiumBridge();

            foreach (var file in files)
            {
                var document = bridge.FPDF_LoadDocument(file, null);
                if (!document.IsValid)
                {
                    continue;
                }

                bridge.FPDF_CloseDocument(document);
                _filesToMerge.Add(new MergeElement()
                {
                    PDFFile = file,
                });
            }

            bridge.Dispose();
        }

        #endregion Implementation of IViewModel
    }
}
