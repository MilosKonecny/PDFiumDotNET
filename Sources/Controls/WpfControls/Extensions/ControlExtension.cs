namespace PDFiumDotNET.WpfControls.Extensions
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Extension class for <see cref="Control"/>.
    /// </summary>
    internal static class ControlExtension
    {
        /// <summary>
        /// Gets the information whether the model is used in design time.
        /// </summary>
        /// <param name="control">Instance of calling <see cref="Control"/>.</param>
        /// <returns><c>true</c> if the design time is active; otherwise <c>false</c>.</returns>
        public static bool IsDesignTime(this Control control)
        {
            return DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}
