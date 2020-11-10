namespace PDFiumDotNET.WpfControls
{
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Controls;
    using System;
    using System.Windows.Input;

    /// <summary>
    /// View class shows pages from opend PDF document.
    /// </summary>
    public partial class PDFView
    {
        #region Implementation of IScrollInfo

        /// <summary>
        /// Gets or sets a <see cref="ScrollViewer"/> element that controls scrolling behavior.
        /// </summary>
        /// <value>A <see cref="ScrollViewer"/> element that controls scrolling behavior.
        /// This property has no default value.</value>
        public ScrollViewer ScrollOwner { get; set; }

        /// <summary>
        /// Gets the horizontal offset of the scrolled content.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the horizontal offset.
        /// This property has no default value.</value>
        public double HorizontalOffset
        {
            get => _horizontalOffset;
            private set
            {
                value = Math.Max(0, Math.Min(value, ExtentWidth - ViewportWidth));
                if (_horizontalOffset != value)
                {
                    _horizontalOffset = value;
                    InvalidateVisual();
                    ScrollOwner?.InvalidateScrollInfo();
                }
            }
        }

        /// <summary>
        /// Gets the vertical offset of the scrolled content.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the vertical offset of the scrolled content.
        /// Valid values are between zero and the <see cref="ExtentHeight"/> minus the <see cref="ViewportHeight"/>.
        /// This property has no default value.</value>
        public double VerticalOffset
        {
            get => _verticalOffset;
            private set
            {
                value = Math.Max(0, Math.Min(value, ExtentHeight - ViewportHeight));
                if (_verticalOffset != value)
                {
                    _verticalOffset = value;
                    InvalidateVisual();
                    ScrollOwner?.InvalidateScrollInfo();
                }
            }
        }

        /// <summary>
        /// Gets the vertical size of the viewport for this content.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the vertical size of the viewport for this content.
        /// This property has no default value.</value>
        public double ViewportHeight => _viewport.Height;

        /// <summary>
        /// Gets the horizontal size of the viewport for this content.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the horizontal size of the viewport for this content.
        /// This property has no default value.</value>
        public double ViewportWidth => _viewport.Width;

        /// <summary>
        /// Gets the vertical size of the extent.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the vertical size of the extent.
        /// This property has no default value.</value>
        public double ExtentHeight => _workArea.Height;

        /// <summary>
        /// Gets the horizontal size of the extent.
        /// </summary>
        /// <value>A <see cref="double"/> that represents, in device independent pixels, the horizontal size of the extent.
        /// This property has no default value.</value>
        public double ExtentWidth => _workArea.Width;

        /// <summary>
        /// Gets or sets a value that indicates whether scrolling on the horizontal axis is possible.
        /// </summary>
        /// <value><c>true</c> if scrolling is possible; otherwise, <c>false</c>.
        /// This property has no default value.</value>
        public bool CanHorizontallyScroll
        {
            get => _canHorizontallyScroll;
            set
            {
                if (_canHorizontallyScroll != value)
                {
                    _canHorizontallyScroll = value;
                    InvalidateVisual();
                    ScrollOwner?.InvalidateScrollInfo();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether scrolling on the vertical axis is possible.
        /// </summary>
        /// <value><c>true</c> if scrolling is possible; otherwise, <c>false</c>.
        /// This property has no default value.</value>
        public bool CanVerticallyScroll
        {
            get => _canVerticallyScroll;
            set
            {
                if (_canVerticallyScroll != value)
                {
                    _canVerticallyScroll = value;
                    InvalidateVisual();
                    ScrollOwner?.InvalidateScrollInfo();
                }
            }
        }

        /// <summary>
        /// Scrolls down within content by one logical unit.
        /// </summary>
        public void LineDown()
        {
            VerticalOffset += ViewportHeight / 10;
        }

        /// <summary>
        /// Scrolls left within content by one logical unit.
        /// </summary>
        public void LineLeft()
        {
            HorizontalOffset -= ViewportWidth / 10;
        }

        /// <summary>
        /// Scrolls right within content by one logical unit.
        /// </summary>
        public void LineRight()
        {
            HorizontalOffset += ViewportWidth / 10;
        }

        /// <summary>
        /// Shift the content offset one line up.
        /// </summary>
        public void LineUp()
        {
            VerticalOffset -= ViewportHeight / 10;
        }

        /// <summary>
        /// Scrolls down within content after a user clicks the wheel button on a mouse.
        /// </summary>
        /// <remarks>Don't handle mouse wheel input from the ScrollViewer, the mouse wheel is used for zooming in and out, not for manipulating the scrollbars.</remarks>
        public void MouseWheelDown()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                // Zoom
                PDFZoomComponent?.DecreaseZoom();
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                // Move right
                LineRight();
            }
            else
            {
                // Move down
                LineDown();
            }
        }

        /// <summary>
        /// Scrolls left within content after a user clicks the wheel button on a mouse.
        /// </summary>
        /// <remarks>Don't handle mouse wheel input from the ScrollViewer, the mouse wheel is used for zooming in and out, not for manipulating the scrollbars.</remarks>
        public void MouseWheelLeft()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                // Zoom
                PDFZoomComponent?.DecreaseZoom();
            }
            else
            {
                // Move up
                LineLeft();
            }
        }

        /// <summary>
        /// Scrolls right within content after a user clicks the wheel button on a mouse.
        /// </summary>
        /// <remarks>Don't handle mouse wheel input from the ScrollViewer, the mouse wheel is used for zooming in and out, not for manipulating the scrollbars.</remarks>
        public void MouseWheelRight()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                // Zoom
                PDFZoomComponent?.IncreaseZoom();
            }
            else
            {
                // Move
                LineRight();
            }
        }

        /// <summary>
        /// Scrolls up within content after a user clicks the wheel button on a mouse.
        /// </summary>
        /// <remarks>Don't handle mouse wheel input from the ScrollViewer, the mouse wheel is used for zooming in and out, not for manipulating the scrollbars.</remarks>
        public void MouseWheelUp()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                // Zoom
                PDFZoomComponent?.IncreaseZoom();
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                // Move right
                LineLeft();
            }
            else
            {
                // Move down
                LineUp();
            }
        }

        /// <summary>
        /// Scrolls down within content by one page.
        /// </summary>
        public void PageDown()
        {
            VerticalOffset += ViewportHeight;
        }

        /// <summary>
        /// Scrolls left within content by one page.
        /// </summary>
        public void PageLeft()
        {
            HorizontalOffset -= ViewportWidth;
        }

        /// <summary>
        /// Scrolls up within content by one page.
        /// </summary>
        public void PageRight()
        {
            HorizontalOffset += ViewportWidth;
        }

        /// <summary>
        /// Scrolls up within content by one page.
        /// </summary>
        public void PageUp()
        {
            VerticalOffset -= ViewportHeight;
        }

        /// <summary>
        /// Forces content to scroll until the coordinate space of a <see cref="Visual"/> object is visible.
        /// </summary>
        /// <param name="visual">A <see cref="Visual"/> that becomes visible.</param>
        /// <param name="rectangle">A bounding rectangle that identifies the coordinate space to make visible.</param>
        /// <returns>A <see cref="Rect"/> that is visible.</returns>
        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            // ToDo: Not implemented
            return rectangle;
        }

        /// <summary>
        /// Sets the amount of horizontal offset.
        /// </summary>
        /// <param name="offset">The degree to which content is horizontally offset from the containing viewport.</param>
        public void SetHorizontalOffset(double offset)
        {
            HorizontalOffset = offset;
        }

        /// <summary>
        /// Sets the amount of vertical offset.
        /// </summary>
        /// <param name="offset">The degree to which content is vertically offset from the containing viewport.</param>
        public void SetVerticalOffset(double offset)
        {
            VerticalOffset = offset;
        }

        #endregion Implementation of IScrollInfo
    }
}
