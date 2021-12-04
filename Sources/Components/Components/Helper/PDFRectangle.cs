namespace PDFiumDotNET.Components.Helper
{
    /// <summary>
    /// Implementation of rectangle.
    /// </summary>
    internal class PDFRectangle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRectangle"/> class.
        /// </summary>
        /// <param name="left">Left position of the rectangle.</param>
        /// <param name="top">Top position of the rectangle.</param>
        /// <param name="right">Right position of the rectangle.</param>
        /// <param name="bottom">Bottom position of the rectangle.</param>
        public PDFRectangle(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Gets the left position of rectangle.
        /// </summary>
        public double Left { get; private set; }

        /// <summary>
        /// Gets the top position of rectangle.
        /// </summary>
        public double Top { get; private set; }

        /// <summary>
        /// Gets the right position of rectangle.
        /// </summary>
        public double Right { get; private set; }

        /// <summary>
        /// Gets the bottom position of rectangle.
        /// </summary>
        public double Bottom { get; private set; }

        /// <summary>
        /// Gets the width of rectangle.
        /// </summary>
        public double Width
        {
            get
            {
                return Right - Left;
            }
        }

        /// <summary>
        /// Gets the height of rectangle.
        /// </summary>
        public double Height
        {
            get
            {
                return Bottom - Top;
            }
        }
    }
}
