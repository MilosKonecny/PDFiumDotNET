namespace PDFiumDotNET.Components.Zoom
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Observers;
    using PDFiumDotNET.Components.Contracts.Zoom;

    /// <summary>
    /// <inheritdoc cref="IPDFZoomComponent"/>
    /// </summary>
    internal sealed partial class PDFZoomComponent : IPDFZoomComponent, IPDFDocumentObserver
    {
        #region Private consts

        private const double _doubleEpsilon = 0.01d;

        #endregion Private consts

        #region Private fields

        private PDFComponent _mainComponent;
        private ZoomType _currentZoomType;
        private double _currentZoomFactor;
        private List<double> _zoomList;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFZoomComponent"/> class.
        /// </summary>
        public PDFZoomComponent()
        {
            SetDefaultValues();
        }

        #endregion Constructors

        #region Private methods

        private void SetDefaultValues()
        {
            CurrentZoomType = ZoomType.DefinedValue;
            CurrentZoomFactor = 1d;
            SetDefaultZoomList();
            InvokePropertyChangedEvent(null);
        }

        private void SetDefaultZoomList()
        {
            ZoomValues = new List<double> { 0.10, 0.25, 0.50, 0.75, 1.00, 1.25, 1.50, 2.00, 4.00, 8.00 };
        }

        #endregion Private methods

        #region Private methods - invoke event

        private void InvokePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Private methods - invoke event

        #region Implementation of IPDFZoomComponent

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ZoomType CurrentZoomType
        {
            get => _currentZoomType;
            set
            {
                if (_currentZoomType != value)
                {
                    _currentZoomType = value;
                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public double CurrentZoomFactor
        {
            get => _currentZoomFactor;
            set
            {
                value = Math.Round(value, 2);
                if (Math.Abs(_currentZoomFactor - value) > _doubleEpsilon)
                {
                    _currentZoomFactor = value;
                    if (_currentZoomFactor < ZoomValues.First())
                    {
                        _currentZoomFactor = ZoomValues.First();
                    }
                    else if (_currentZoomFactor > ZoomValues.Last())
                    {
                        _currentZoomFactor = ZoomValues.Last();
                    }

                    InvokePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<double> ZoomValues
        {
            get
            {
                if (_zoomList == null || _zoomList.Count == 0)
                {
                    SetDefaultZoomList();
                }

                return _zoomList;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                var list = value.ToList();
                _zoomList = list;
                _zoomList.Sort();
                InvokePropertyChangedEvent();
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void IncreaseZoom()
        {
            // Find the nearest bigger value.
            var newValue = ZoomValues.Where(a => a > CurrentZoomFactor).ToList();
            if (newValue.Count > 0)
            {
                CurrentZoomFactor = newValue[0];
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void DecreaseZoom()
        {
            // Find the nearest lower value.
            var newValue = ZoomValues.Reverse().Where(a => a < CurrentZoomFactor).ToList();
            if (newValue.Count > 0)
            {
                CurrentZoomFactor = newValue[0];
            }
        }

        #endregion Implementation of IPDFZoomComponent

        #region Implementation of IComponent

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

        #endregion Implementation of IComponent

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
