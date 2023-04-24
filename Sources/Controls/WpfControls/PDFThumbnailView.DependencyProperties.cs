namespace PDFiumDotNET.WpfControls
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Basic;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// View class shows page thumbnails from opened PDF document.
    /// </summary>
    public partial class PDFThumbnailView
    {
        #region Dependency properties - register

        /// <summary>
        /// Dependency property for 'PDFPageBackground' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageBackgroundProperty
            = DependencyProperty.Register("PDFPageBackground", typeof(Brush), typeof(PDFThumbnailView), new FrameworkPropertyMetadata(Brushes.White, HandlePDFPageBackgroundPropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageBorderBrush' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageBorderBrushProperty
            = DependencyProperty.Register("PDFPageBorderBrush", typeof(Brush), typeof(PDFThumbnailView), new FrameworkPropertyMetadata(Brushes.Black, HandlePDFPageBorderBrushPropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageBorderThickness' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageBorderThicknessProperty
            = DependencyProperty.Register("PDFPageBorderThickness", typeof(Thickness), typeof(PDFThumbnailView), new FrameworkPropertyMetadata(new Thickness(0.5), HandlePDFPageBorderThicknessPropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageComponent' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageComponentProperty
            = DependencyProperty.Register("PDFPageComponent", typeof(IPDFPageComponent), typeof(PDFThumbnailView), new FrameworkPropertyMetadata(null, HandlePDFPageComponentPropertyChanged));

        #endregion Dependency properties - register

        #region Dependency properties - properties

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public Brush PDFPageBackground
        {
            get => (Brush)GetValue(PDFPageBackgroundProperty);
            set => SetValue(PDFPageBackgroundProperty, value);
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public Brush PDFPageBorderBrush
        {
            get => (Brush)GetValue(PDFPageBorderBrushProperty);
            set => SetValue(PDFPageBorderBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public Thickness PDFPageBorderThickness
        {
            get => (Thickness)GetValue(PDFPageBorderThicknessProperty);
            set => SetValue(PDFPageBorderThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public IPDFPageComponent PDFPageComponent
        {
            get => (IPDFPageComponent)GetValue(PDFPageComponentProperty);
            set => SetValue(PDFPageComponentProperty, value);
        }

        #endregion Dependency properties - properties

        #region Dependency properties - private callback methods

        /// <summary>
        /// Callback method is called whenever <see cref="PDFPageBackground"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFPageBackgroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is not PDFThumbnailView view)
            {
                return;
            }

            view.InvalidateVisual();
        }

        /// <summary>
        /// Callback method is called whenever <see cref="PDFPageBorderBrush"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFPageBorderBrushPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is not PDFThumbnailView view)
            {
                return;
            }

            view.InvalidateVisual();
        }

        /// <summary>
        /// Callback method is called whenever <see cref="PDFPageBorderThickness"/> property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePDFPageBorderThicknessPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is not PDFThumbnailView view)
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
            if (o is not PDFThumbnailView view)
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

            component.MainComponent.PropertyChanged += HandlePDFComponentPropertyChangedEvent;

            // ToDo: Set margin if FontSize changes.
            component.RenderManager.PageMargin = new PDFSize<double>(FontSize, 2d * FontSize);
            ScrollOwner?.InvalidateScrollInfo();
        }

        private void Unuse(IPDFPageComponent component)
        {
            if (component == null)
            {
                return;
            }

            component.MainComponent.PropertyChanged -= HandlePDFComponentPropertyChangedEvent;
            ScrollOwner?.InvalidateScrollInfo();
        }

        #endregion Private methods

        #region Private event handler methods

        private void HandlePDFComponentPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName)
                || string.Equals(
                    nameof(IPDFComponent.IsDocumentOpen),
                    e.PropertyName,
                    StringComparison.OrdinalIgnoreCase))
            {
                ResetStatus();
                InvalidateVisual();
            }
        }

        #endregion Private event handler methods
    }
}
