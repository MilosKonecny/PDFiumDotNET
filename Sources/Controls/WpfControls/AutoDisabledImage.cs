#if WpfControls
namespace PDFiumDotNET.WpfControls
#else
namespace PDFiumDotNET.WpfCoreControls
#endif
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Control implements auto disabled image based on property 'IsEnabled'.
    /// </summary>
    public class AutoDisabledImage : Image
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoDisabledImage"/> class.
        /// </summary>
        static AutoDisabledImage()
        {
            // Override the metadata of the IsEnabled and Source properties to be notified of changes
            IsEnabledProperty.OverrideMetadata(typeof(AutoDisabledImage), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(PropertyChanged)));
            SourceProperty.OverrideMetadata(typeof(AutoDisabledImage), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(PropertyChanged)));
        }

        #endregion Constructors

        #region Protected properties

        /// <summary>
        /// Gets value indicating whether the immage is grayed out.
        /// </summary>
        protected bool IsGrayedOut => Source is FormatConvertedBitmap;

        #endregion Protected properties

        #region Private static methods

        /// <summary>
        /// Callback method for 'IsEnabled' and 'Source' property called whenever one of these properties was changed.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void PropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            if (source is AutoDisabledImage me && me.IsEnabled == me.IsGrayedOut)
            {
                me.UpdateImage();
            }
        }

        #endregion Private static methods

        #region Protected methods

        /// <summary>
        /// Method updates image.
        /// </summary>
        protected void UpdateImage()
        {
            if (Source == null)
            {
                return;
            }

            if (IsEnabled)
            {
                // image is enabled (i.e. use the original image)
                if (IsGrayedOut)
                {
                    // restore the original image
                    Source = ((FormatConvertedBitmap)Source).Source;
                    // reset the Opcity Mask
                    OpacityMask = null;
                }
            }
            else
            {
                // image is disabled (i.e. grayscale the original image)
                if (!IsGrayedOut)
                {
                    // Get the source bitmap                        
                    if (Source is BitmapSource bitmapImage)
                    {
                        Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray8, null, 0);
                        // reuse the opacity mask from the original image as FormatConvertedBitmap does not keep transparency info
                        OpacityMask = new ImageBrush(bitmapImage);
                    }
                }
            }
        }

        #endregion Protected methods
    }
}
