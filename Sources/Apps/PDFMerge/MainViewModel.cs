namespace PDFiumDotNET.Apps.PDFMerge
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Apps.PDFMerge.Contracts;

    /// <summary>
    /// View model class implements <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class MainViewModel : IViewModel, INotifyPropertyChanged
    {
        #region Private fields

        private string _title;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            Title = "PDF Merge";
        }

        #endregion Constructors

        #region Public properties

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

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged

        #region Implementation of IViewModel

        /// <inheritdoc/>
        public void AssignedToView(IView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        #endregion Implementation of IViewModel
    }
}
