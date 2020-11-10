namespace PDFiumDotNET.Components.Contracts.Zoom
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface defines functionality of zoom component.
    /// Component provides all information about zoom state of opened PDF document.
    /// </summary>
    public interface IPDFZoomComponent : IPDFChildComponent
    {
        /// <summary>
        /// Gets or sets the current type of zoom. Set causes recalculation of <see cref="CurrentZoomFactor"/>.
        /// </summary>
        ZoomType CurrentZoomType { get; set; }

        /// <summary>
        /// Gets or sets the current zoom factor. Factor 1.0 is 100%.
        /// Value is returned regardless of <see cref="CurrentZoomType"/> property.
        /// </summary>
        double CurrentZoomFactor { get; set; }

        /// <summary>
        /// Gets or sets predefined values in % used for particular zoom values. First and last value are used as min and max value.
        /// It is possible that the value of <see cref="CurrentZoomFactor"/> does not match any number of these values.
        /// </summary>
        /// <remarks>Predefined values: 0.10, 0.25, 0.50, 0.75, 1.00, 1.25, 1.50, 2.00, 4.00, 8.00.</remarks>
        IEnumerable<double> ZoomValues { get; set; }

        /// <summary>
        /// Increases the zoom to the nearest value of <see cref="ZoomValues"/>.
        /// </summary>
        void IncreaseZoom();

        /// <summary>
        /// Decreases the zoom to the nearest value of <see cref="ZoomValues"/>.
        /// </summary>
        void DecreaseZoom();
    }
}
