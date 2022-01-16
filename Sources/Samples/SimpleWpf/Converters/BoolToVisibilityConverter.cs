namespace PDFiumDotNET.Samples.SimpleWpf.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Class implements <see cref="IValueConverter"/> for conversion of <c>bool</c> value to <see cref="Visibility"/> value.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolToVisibilityConverter"/> class.
        /// </summary>
        public BoolToVisibilityConverter()
        {
            VisibilityForTrue = Visibility.Visible;
            VisibilityForFalse = Visibility.Collapsed;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets visibility for <c>true</c> value. Default value is <see cref="Visibility.Visible"/>.
        /// </summary>
        public Visibility VisibilityForTrue { get; set; }

        /// <summary>
        /// Gets visibility for <c>false</c> value. Default value is <see cref="Visibility.Collapsed"/>.
        /// </summary>
        public Visibility VisibilityForFalse { get; set; }

        #endregion Public properties

        #region Implementation of IValueConverter

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                if (boolValue)
                {
                    return VisibilityForTrue;
                }
                else
                {
                    return VisibilityForFalse;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Implementation of IValueConverter
    }
}
