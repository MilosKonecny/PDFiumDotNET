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
            = DependencyProperty.Register("PDFPageBackground", typeof(Brush), typeof(PDFView), new FrameworkPropertyMetadata(Brushes.White, HandlePropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageActiveBorderBrush' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageActiveBorderBrushProperty
            = DependencyProperty.Register("PDFPageActiveBorderBrush", typeof(Brush), typeof(PDFView), new FrameworkPropertyMetadata(Brushes.Black, HandlePropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageActiveBorderThickness' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageActiveBorderThicknessProperty
            = DependencyProperty.Register("PDFPageActiveBorderThickness", typeof(Thickness), typeof(PDFView), new FrameworkPropertyMetadata(new Thickness(0.5), HandlePropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageBorderBrush' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageBorderBrushProperty
            = DependencyProperty.Register("PDFPageBorderBrush", typeof(Brush), typeof(PDFView), new FrameworkPropertyMetadata(Brushes.DarkGray, HandlePropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageBorderThickness' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageBorderThicknessProperty
            = DependencyProperty.Register("PDFPageBorderThickness", typeof(Thickness), typeof(PDFView), new FrameworkPropertyMetadata(new Thickness(0.5), HandlePropertyChanged));

        /// <summary>
        /// Dependency property for 'PDFPageComponent' - source of information to draw content.
        /// </summary>
        public static readonly DependencyProperty PDFPageComponentProperty
            = DependencyProperty.Register("PDFPageComponent", typeof(IPDFPageComponent), typeof(PDFView), new FrameworkPropertyMetadata(null, HandlePDFPageComponentPropertyChanged));

        /// <summary>
        /// Dependency property for 'ShowPageLabel' - label will be drawn beneath of page.
        /// </summary>
        public static readonly DependencyProperty ShowPageLabelProperty =
            DependencyProperty.Register("ShowPageLabel", typeof(bool), typeof(PDFView), new FrameworkPropertyMetadata(false, HandlePropertyChanged));

        /// <summary>
        /// Dependency property for 'ActivatePageOnClick' - set current page where was clicked.
        /// </summary>
        public static readonly DependencyProperty ActivatePageOnClickProperty =
            DependencyProperty.Register("ActivatePageOnClick", typeof(bool), typeof(PDFView), new FrameworkPropertyMetadata(false, HandlePropertyChanged));

        /// <summary>
        /// Dependency property for 'ActivatePageInCenter' - set current page where was clicked.
        /// </summary>
        public static readonly DependencyProperty ActivatePageInCenterProperty =
            DependencyProperty.Register("ActivatePageInCenter", typeof(bool), typeof(PDFView), new FrameworkPropertyMetadata(true, HandlePropertyChanged));

        #endregion Dependency properties - register

        #region Dependency properties - properties

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public Color PDFFindSelectionBorder
        {
            get { return (Color)GetValue(PDFFindSelectionBorderProperty); }
            set { SetValue(PDFFindSelectionBorderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public Color PDFFindSelectionBackground
        {
            get { return (Color)GetValue(PDFFindSelectionBackgroundProperty); }
            set { SetValue(PDFFindSelectionBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public Size PDFPageMargin
        {
            get => (Size)GetValue(PDFPageMarginProperty);
            set => SetValue(PDFPageMarginProperty, value);
        }

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
        public Brush PDFPageActiveBorderBrush
        {
            get => (Brush)GetValue(PDFPageActiveBorderBrushProperty);
            set => SetValue(PDFPageActiveBorderBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public Thickness PDFPageActiveBorderThickness
        {
            get => (Thickness)GetValue(PDFPageActiveBorderThicknessProperty);
            set => SetValue(PDFPageActiveBorderThicknessProperty, value);
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

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public bool ShowPageLabel
        {
            get { return (bool)GetValue(ShowPageLabelProperty); }
            set { SetValue(ShowPageLabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public bool ActivatePageOnClick
        {
            get { return (bool)GetValue(ActivatePageOnClickProperty); }
            set { SetValue(ActivatePageOnClickProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value of dependency property.
        /// </summary>
        [Category(Constants.PDFiumDotNETPropertyCategory)]
        public bool ActivatePageInCenter
        {
            get { return (bool)GetValue(ActivatePageInCenterProperty); }
            set { SetValue(ActivatePageInCenterProperty, value); }
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

            if (view.PDFPageComponent != null)
            {
                view.PDFPageComponent.PageMargin = new PDFSize<double>(view.PDFPageMargin.Width, view.PDFPageMargin.Height);
            }

            view.InvalidateVisual();
        }

        /// <summary>
        /// Callback method is called whenever 'simple' property has changed value.
        /// </summary>
        /// <param name="o">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void HandlePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
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
            component.NavigatedToPage -= HandlePDFPageComponentNavigatedToPageEvent;
            component.TextSelectionsRemoved -= HandlePDFPageComponentTextSelectionsRemovedEvent;

            if (component.MainComponent != null)
            {
                component.MainComponent.PropertyChanged -= HandlePDFComponentPropertyChangedEvent;
            }

            if (component.ZoomComponent != null)
            {
                component.ZoomComponent.ZoomChanged -= HandlePDFZoomComponentPropertyChangedEvent;
            }

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

        private void HandlePDFPageComponentNavigatedToPageEvent(object sender, NavigatedToPageEventArgs e)
        {
            if (ActivatePageOnClick)
            {
                // Don't scroll the content to display the current page at the top of the viewport.
                return;
            }

            // Current page is changed. Scroll to this page.
            var verticalOffset = PDFPageComponent.RenderManager.DeterminePagePosition(e.CurrentPageIndex - 1).Y;
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
                horizontalOffset = ((DocumentArea.Width - pageWidth) / 2) + detailedPositionXFromLeft - (ActualWidth / 2);
            }

            if (double.IsNaN(horizontalOffset))
            {
                VerticalOffset = verticalOffset;
            }
            else
            {
                SetOffsets(horizontalOffset, verticalOffset, true);
            }
        }

        private void HandlePDFPageComponentTextSelectionsRemovedEvent(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => InvalidateVisual());
        }

        private void HandlePDFZoomComponentPropertyChangedEvent(object sender, ZoomChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                HorizontalOffset = PDFPageComponent.RenderManager.DetermineHorizontalOffset(RenderInformation, e.NewZoomFactor);
                VerticalOffset = PDFPageComponent.RenderManager.DetermineVerticalOffset(RenderInformation, e.NewZoomFactor);
                InvalidateVisual();
            });
        }

        #endregion Private event handler methods
    }
}
