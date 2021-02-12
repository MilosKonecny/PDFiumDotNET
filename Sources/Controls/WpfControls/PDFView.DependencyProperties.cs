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
    using PDFiumDotNET.Components.Contracts.Adapters;
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

            component.PropertyChanged += HandlePDFPageComponentPropertyChangedEvent;
            component.NavigatedToPage += HandlePDFPageComponentNavigatedToPageEvent;
            ScrollOwner?.InvalidateScrollInfo();
        }

        private void Unuse(IPDFPageComponent component)
        {
            if (component == null)
            {
                return;
            }

            component.PropertyChanged -= HandlePDFPageComponentPropertyChangedEvent;
            component.NavigatedToPage -= HandlePDFPageComponentNavigatedToPageEvent;
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

        private void HandlePDFPageComponentPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName))
            {
                // Everything is changed
                ResetStatus();
            }

            if (!string.Equals(nameof(IPDFPageComponent.CurrentPageIndex), e.PropertyName, StringComparison.OrdinalIgnoreCase))
            {
                InvalidateVisual();
            }
        }

        private void HandlePDFPageComponentNavigatedToPageEvent(object sender, System.EventArgs e)
        {
            // Current page is changed. Scroll to this page.
            VerticalOffset = PDFPageComponent[PageLayoutType.Standard].GetPageTopLine(PDFPageComponent.CurrentPageIndex - 1, PDFPageMargin, PDFZoomComponent.CurrentZoomFactor);
        }

        private void HandlePDFZoomComponentPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            InvalidateVisual();
        }

        #endregion Private event handler methods
    }
}
