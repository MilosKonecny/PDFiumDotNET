namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Basic;
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
        /// Dependency property for 'PDFFindSelectionBorder' - source of information to draw find selection border.
        /// </summary>
        public static readonly DependencyProperty PDFFindSelectionBorderProperty
            = DependencyProperty.Register("PDFFindSelectionBorder", typeof(Color), typeof(PDFView), new PropertyMetadata(new Color() { R = 0x00, G = 0x00, B = 0xFF, A = 0xFF }));

        /// <summary>
        /// Dependency property for 'PDFFindSelectionBackground' - source of information to draw find selection background.
        /// </summary>
        public static readonly DependencyProperty PDFFindSelectionBackgroundProperty
            = DependencyProperty.Register("PDFFindSelectionBackground", typeof(Color), typeof(PDFView), new PropertyMetadata(new Color() { R = 0xFF, G = 0xFF, B = 0xA0, A = 0x3F }));

        /// <summary>
        /// Dependency property for 'PDFPageMargin' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageMarginProperty
            = DependencyProperty.Register("PDFPageMargin", typeof(Size), typeof(PDFView), new FrameworkPropertyMetadata(new Size(5, 5), HandlePDFPageMarginPropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageBackground' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageBackgroundProperty
            = DependencyProperty.Register("PDFPageBackground", typeof(Brush), typeof(PDFView), new FrameworkPropertyMetadata(Brushes.White, HandlePDFPageBackgroundPropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageComponent' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageComponentProperty
            = DependencyProperty.Register("PDFPageComponent", typeof(IPDFPageComponent), typeof(PDFView), new FrameworkPropertyMetadata(null, HandlePDFPageComponentPropertyChanged));

        #endregion Dependency properties - register

        #region Dependency properties - properties

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        public Color PDFFindSelectionBorder
        {
            get { return (Color)GetValue(PDFFindSelectionBorderProperty); }
            set { SetValue(PDFFindSelectionBorderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        public Color PDFFindSelectionBackground
        {
            get { return (Color)GetValue(PDFFindSelectionBackgroundProperty); }
            set { SetValue(PDFFindSelectionBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        public Size PDFPageMargin
        {
            get => (Size)GetValue(PDFPageMarginProperty);
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

        #endregion Dependency properties - properties

        #region Dependency properties - private callback methods

        /// <summary>
        /// Callback method is called whenever <see cref="PDFPageMargin"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFPageMarginPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is not PDFView view)
            {
                return;
            }

            view.PDFPageComponent.PageMargin = new PDFSize<double>(view.PDFPageMargin.Width, view.PDFPageMargin.Height);
            view.InvalidateVisual();
        }

        /// <summary>
        /// Callback method is called whenever <see cref="PDFPageBackground"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFPageBackgroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is not PDFView view)
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
            if (o is not PDFView view)
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

        #endregion Dependency properties - private callback methods

        #region Private methods

        private void Use(IPDFPageComponent component)
        {
            if (component == null)
            {
                return;
            }

            component.FindSelectionBackgroundFunc = () =>
            BitConverter.ToInt32(new byte[] { PDFFindSelectionBackground.B, PDFFindSelectionBackground.G, PDFFindSelectionBackground.R, PDFFindSelectionBackground.A }, 0);
            component.FindSelectionBorderFunc = () =>
            BitConverter.ToInt32(new byte[] { PDFFindSelectionBorder.B, PDFFindSelectionBorder.G, PDFFindSelectionBorder.R, PDFFindSelectionBorder.A }, 0);
            component.MainComponent.PropertyChanged += HandlePDFComponentPropertyChangedEvent;
            component.NavigatedToPage += HandlePDFPageComponentNavigatedToPageEvent;
            component.TextSelectionsRemoved += HandlePDFPageComponentTextSelectionsRemovedEvent;

            component.ZoomComponent.ZoomChanged += HandlePDFZoomComponentPropertyChangedEvent;

            component.PageMargin = new PDFSize<double>(PDFPageMargin.Width, PDFPageMargin.Height);

            ScrollOwner?.InvalidateScrollInfo();
        }

        private void Unuse(IPDFPageComponent component)
        {
            if (component == null)
            {
                return;
            }

            component.FindSelectionBackgroundFunc = null;
            component.FindSelectionBorderFunc = null;
            component.MainComponent.PropertyChanged -= HandlePDFComponentPropertyChangedEvent;
            component.NavigatedToPage -= HandlePDFPageComponentNavigatedToPageEvent;
            component.TextSelectionsRemoved -= HandlePDFPageComponentTextSelectionsRemovedEvent;
            component.ZoomComponent.ZoomChanged -= HandlePDFZoomComponentPropertyChangedEvent;
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
            var verticalOffset = PDFPageComponent.RenderManager.PagePosition(e.CurrentPageIndex - 1).Y;
            var horizontalOffset = double.NaN;
            if (e.IsDetailedNavigation)
            {
                // Get target page
                var page = PDFPageComponent.Pages[e.CurrentPageIndex - 1];

                // Center vertically
                var pageHeight = page.Height * PDFPageComponent.ZoomComponent.CurrentZoomFactor;
                var detailedPositionYFromTop = pageHeight - (e.DetailedPositionY * PDFPageComponent.ZoomComponent.CurrentZoomFactor);
                verticalOffset += detailedPositionYFromTop - (ActualHeight / 2);

                // Center horizontally
                var pageWidth = page.Width * PDFPageComponent.ZoomComponent.CurrentZoomFactor;
                var detailedPositionXFromLeft = e.DetailedPositionX * PDFPageComponent.ZoomComponent.CurrentZoomFactor;
                horizontalOffset = ((_documentArea.Width - pageWidth) / 2) + detailedPositionXFromLeft - (ActualWidth / 2);
            }

            VerticalOffset = verticalOffset;
            if (!double.IsNaN(horizontalOffset))
            {
                HorizontalOffset = horizontalOffset;
            }
        }

        private void HandlePDFPageComponentTextSelectionsRemovedEvent(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => InvalidateVisual());
        }

        private void HandlePDFZoomComponentPropertyChangedEvent(object sender, ZoomChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => InvalidateVisual());
        }

        #endregion Private event handler methods
    }
}
