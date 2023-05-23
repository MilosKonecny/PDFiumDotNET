namespace PDFiumDotNET.WpfControls.Helper
{
    using System;

    /// <summary>
    /// The class implements helper methods for double type.
    /// </summary>
    internal static class DoubleHelper
    {
        private const double _doubleEpsilonForOffsets = 0.1d;
        private const double _doubleEpsilonForZooms = 0.001d;

        /// <summary>
        /// Method compares two double offset values in less accuracy as original <see cref="double.Epsilon"/>.
        /// This method uses 0.1 value as Epsilon.
        /// </summary>
        /// <param name="a">First double value to compare with second.</param>
        /// <param name="b">Second double value to compare with first.</param>
        /// <returns><c>true</c> in case that the difference is less than 0.1.</returns>
        public static bool OffsetsAreEqual(double a, double b)
        {
            return Math.Abs(a - b) < _doubleEpsilonForOffsets;
        }

        /// <summary>
        /// Method compares two double zoom values in less accuracy as original <see cref="double.Epsilon"/>.
        /// This method uses 0.001 value as Epsilon.
        /// </summary>
        /// <param name="a">First double value to compare with second.</param>
        /// <param name="b">Second double value to compare with first.</param>
        /// <returns><c>true</c> in case that the difference is less than 0.001.</returns>
        public static bool ZoomsAreEqual(double a, double b)
        {
            return Math.Abs(a - b) < _doubleEpsilonForZooms;
        }
    }
}
