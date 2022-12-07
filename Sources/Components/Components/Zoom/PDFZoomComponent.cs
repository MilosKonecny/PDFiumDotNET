namespace PDFiumDotNET.Components.Zoom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PDFiumDotNET.Components.Contracts.Zoom;

    /// <inheritdoc cref="IPDFZoomComponent"/>
    internal sealed partial class PDFZoomComponent : PDFChildComponent, IPDFZoomComponent
    {
        #region Private consts

        private const double _doubleEpsilon = 0.01d;

        #endregion Private consts

        #region Private fields

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

        #region Implementation of IPDFZoomComponent

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public int CurrentZoomPercentage
        {
            get
            {
                return (int)Math.Round(100d * CurrentZoomFactor, 0);
            }

            set
            {
                CurrentZoomFactor = value / 100d;
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void IncreaseZoom()
        {
            // Find the nearest bigger value.
            var newValue = ZoomValues.Where(a => a > CurrentZoomFactor).ToList();
            if (newValue.Count > 0)
            {
                CurrentZoomFactor = newValue[0];
            }
        }

        /// <inheritdoc/>
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
    }
}
