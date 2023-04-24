namespace PDFiumDotNET.Apps.PDFViewWPF.Behaviors
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Microsoft.Xaml.Behaviors;

    /// <summary>
    /// Behavior for got focus and lost focus handling.
    /// </summary>
    public class TextBoxEnterBehavior : Behavior<TextBox>
    {
        #region Protected override methods

        /// <inheritdoc/>
#if NET5_0_OR_GREATER
        [System.Runtime.Versioning.SupportedOSPlatform("windows7.0")]
#endif
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += HandleAssociatedObjectPreviewKeyDownEvent;
            AssociatedObject.GotFocus += HandleAssociatedObjectGotFocusEvent;
            base.OnAttached();
        }

        /// <inheritdoc/>
#if NET5_0_OR_GREATER
        [System.Runtime.Versioning.SupportedOSPlatform("windows7.0")]
#endif
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= HandleAssociatedObjectPreviewKeyDownEvent;
            AssociatedObject.GotFocus -= HandleAssociatedObjectGotFocusEvent;
            base.OnDetaching();
        }

        #endregion Protected override methods

        #region Private event handler methods

        private void HandleAssociatedObjectGotFocusEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            tb.Dispatcher.BeginInvoke(new Action(() => tb.SelectAll()), DispatcherPriority.ApplicationIdle);
        }

        private void HandleAssociatedObjectPreviewKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var tb = sender as TextBox;
                BindingOperations.GetBindingExpression(tb, TextBox.TextProperty).UpdateSource();
            }
        }

        #endregion Private event handler methods
    }
}
