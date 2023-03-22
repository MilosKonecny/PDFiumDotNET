namespace PDFiumDotNET.Components.Contracts.EventArguments
{
    using System;

    /// <summary>
    /// Event arguments class used to inform that the zoom was changed.
    /// </summary>
    public class ZoomChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldZoomFactor">Zoom factor previously used.</param>
        /// <param name="newZoomFactor">Zoom factor currently used.</param>
        public ZoomChangedEventArgs(double oldZoomFactor, double newZoomFactor)
        {
            OldZoomFactor = oldZoomFactor;
            NewZoomFactor = newZoomFactor;
        }

        /// <summary>
        /// Gets the previously used zoom factor.
        /// </summary>
        public double OldZoomFactor { get; private set; }

        /// <summary>
        /// Gets the currently used zoom factor.
        /// </summary>
        public double NewZoomFactor { get; private set; }
    }
}
