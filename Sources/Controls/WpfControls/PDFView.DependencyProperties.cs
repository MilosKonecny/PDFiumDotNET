#if WpfControls
namespace PDFiumDotNET.WpfControls
#else
namespace PDFiumDotNET.WpfCoreControls
#endif
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Adapters;
    using PDFiumDotNET.Components.Contracts.EventArguments;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Contracts.Zoom;

    /// <summary>
    /// View class shows pages from opened PDF document.
    /// </summary>
    public partial class PDFView
    {
        #region Dependency properties - register

        /// <summary>
        /// Dependency property for 'PDFPageMargin' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageMarginProperty
            = DependencyProperty.Register("PDFPageMargin", typeof(double), typeof(PDFView),
                new FrameworkPropertyMetadata(5d, HandlePDFPageMarginPropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageBackground' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageBackgroundProperty
            = DependencyProperty.Register("PDFPageBackground", typeof(Brush), typeof(PDFView),
                new FrameworkPropertyMetadata(Brushes.White, HandlePDFPageBackgroundPropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageComponent' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageComponentProperty
            = DependencyProperty.Register("PDFPageComponent", typeof(IPDFPageComponent), typeof(PDFView),
                new FrameworkPropertyMetadata(null, HandlePDFPageComponentPropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFZoomComponent' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFZoomComponentProperty
            = DependencyProperty.Register("PDFZoomComponent", typeof(IPDFZoomComponent), typeof(PDFView),
                new FrameworkPropertyMetadata(null, HandlePDFZoomComponentPropertyChanged));

        #endregion Dependency properties - register

        #region Dependency properties - properties

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        public double PDFPageMargin
        {
            get => (double)GetValue(PDFPageMarginProperty);
            set => SetValue(PDFPageMarginProperty, value);
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        public Brush PDFPageBackground
        {
            get => (Brush)GetValue(PDFPageBackgroundProperty);
            set => SetValue(PDFPageBackgroundProperty, value);
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        public IPDFPageComponent PDFPageComponent
        {
            get => (IPDFPageComponent)GetValue(PDFPageComponentProperty);
            set => SetValue(PDFPageComponentProperty, value);
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        public IPDFZoomComponent PDFZoomComponent
        {
            get => (IPDFZoomComponent)GetValue(PDFZoomComponentProperty);
            set => SetValue(PDFZoomComponentProperty, value);
        }

        #endregion Dependency properties - properties

        #region Dependency properties - private callback methods

        /// <summary>
        /// Callback method is called whenever <see cref="PDFPageMargin"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFPageMarginPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var view = o as PDFView;
            if (view == null)
            {
                return;
            }

            view.InvalidateVisual();
        }

        /// <summary>
        /// Callback method is called whenever <see cref="PDFPageBackground"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFPageBackgroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var view = o as PDFView;
            if (view == null)
            {
                return;
            }

            view.InvalidateVisual();
        }

        /// <summary>
        /// Callback method is called whenever <see cref="PDFPageComponent"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFPageComponentPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var view = o as PDFView;
            if (view == null)
            {
                return;
            }

            if (e.OldValue != null)
            {
                view.Unuse(e.OldValue as IPDFPageComponent);
            }

            if (e.NewValue != null)
            {
                view.Use(e.NewValue as IPDFPageComponent);
            }
        }

        /// <summary>
        /// Callback method is called whenever <see cref="PDFZoomComponent"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFZoomComponentPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var view = o as PDFView;
            if (view == null)
            {
                return;
            }

            if (e.OldValue != null)
            {
                view.Unuse(e.OldValue as IPDFZoomComponent);
            }

            if (e.NewValue != null)
            {
                view.Use(e.NewValue as IPDFZoomComponent);
            }
        }

        #endregion Dependency properties - private callback methods

        #region Private methods

        private void Use(IPDFPageComponent component)
        {
            if (component == null)
            {
                return;
            }

            component.MainComponent.PropertyChanged += HandlePDFComponentPropertyChangedEvent;
            component.NavigatedToPage += HandlePDFPageComponentNavigatedToPageEvent;
            component.TextSelectionsRemoved += HandlePDFPageComponentTextSelectionsRemovedEvent;
            ScrollOwner?.InvalidateScrollInfo();
        }

        private void Unuse(IPDFPageComponent component)
        {
            if (component == null)
            {
                return;
            }

            component.MainComponent.PropertyChanged -= HandlePDFComponentPropertyChangedEvent;
            component.NavigatedToPage -= HandlePDFPageComponentNavigatedToPageEvent;
            component.TextSelectionsRemoved -= HandlePDFPageComponentTextSelectionsRemovedEvent;
            ScrollOwner?.InvalidateScrollInfo();
        }

        private void Use(IPDFZoomComponent component)
        {
            if (component == null)
            {
                return;
            }

            component.PropertyChanged += HandlePDFZoomComponentPropertyChangedEvent;
            ScrollOwner?.InvalidateScrollInfo();
        }

        private void Unuse(IPDFZoomComponent component)
        {
            if (component == null)
            {
                return;
            }

            component.PropertyChanged -= HandlePDFZoomComponentPropertyChangedEvent;
            ScrollOwner?.InvalidateScrollInfo();
        }

        #endregion Private methods

        #region Private event handler methods

        private void HandlePDFComponentPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName)
                || string.Equals(
                    nameof(IPDFComponent.IsDocumentOpened),
                    e.PropertyName,
                    StringComparison.OrdinalIgnoreCase))
            {
                ResetStatus();
                InvalidateVisual();
            }
        }

        private void HandlePDFPageComponentNavigatedToPageEvent(object sender, NavigatedToPageEventArgs e)
        {
            // Current page is changed. Scroll to this page.
            var verticalOffset = PDFPageComponent[PageLayoutType.Standard].GetPageTopLine(e.CurrentPageIndex - 1, PDFPageMargin, PDFZoomComponent.CurrentZoomFactor);
            var horizontalOffset = double.NaN;
            if (e.IsDetailedNavigation)
            {
                // Get target page
                var page = PDFPageComponent.Pages[e.CurrentPageIndex - 1];

                // Center vertically
                var pageHeight = page.Height * PDFZoomComponent.CurrentZoomFactor;
                var detailedPositionYFromTop = pageHeight - e.DetailedPositionY * PDFZoomComponent.CurrentZoomFactor;
                verticalOffset += detailedPositionYFromTop - ActualHeight / 2;

                // Center horizontally
                var pageWidth = page.Width * PDFZoomComponent.CurrentZoomFactor;
                var detailedPositionXFromLeft = e.DetailedPositionX * PDFZoomComponent.CurrentZoomFactor;
                horizontalOffset = (_workArea.Width - pageWidth) / 2 + detailedPositionXFromLeft - ActualWidth / 2;
            }

            VerticalOffset = verticalOffset;
            if (!double.IsNaN(horizontalOffset))
            {
                HorizontalOffset = horizontalOffset;
            }

        }

        private void HandlePDFPageComponentTextSelectionsRemovedEvent(object sender, EventArgs e)
        {
            // Application.Current.Dispatcher.Invoke(() => InvalidateVisual());
        }

        private void HandlePDFZoomComponentPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => InvalidateVisual());
        }

        #endregion Private event handler methods
    }
}
