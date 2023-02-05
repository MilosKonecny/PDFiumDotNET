namespace PDFiumDotNET.Components.Layout
{
    using System;
    using System.Linq;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Page;
    using PDFiumDotNET.Components.Transformation;

    /// <summary>
    /// Implementation class of <see cref="IPDFLayoutComponent"/>.
    /// </summary>
    internal sealed partial class PDFLayoutComponent : PDFChildComponent, IPDFLayoutComponent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFLayoutComponent"/> class.
        /// </summary>
        public PDFLayoutComponent()
        {
        }

        #endregion Constructors

        #region Private methods

        private PDFPageComponent GetPDFPageComponentByName(string name)
        {
            return ChildComponents.OfType<PDFPageComponent>().FirstOrDefault(
                                pageComponent => string.Equals(pageComponent.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        #endregion Private methods

        #region Implementation of IPDFLayoutComponent

        /// <inheritdoc/>
        public IPDFPageComponent this[int index]
        {
            get
            {
                if (IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(PDFLayoutComponent));
                }

                var pageComponents = ChildComponents.OfType<IPDFPageComponent>().ToList();
                if (pageComponents.Count > index && index >= 0)
                {
                    return pageComponents[index];
                }

                return null;
            }
        }

        /// <inheritdoc/>
        public IPDFPageComponent this[string name]
        {
            get
            {
                if (IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(PDFLayoutComponent));
                }

                return GetPDFPageComponentByName(name);
            }
        }

        /// <inheritdoc/>
        public IPDFPageComponent CreatePageComponent(string name, PageLayoutType pageLayout)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(PDFLayoutComponent));
            }

            var component = this[name];
            if (component == null)
            {
                component = new PDFPageComponent(name, pageLayout == PageLayoutType.Thumbnail ? new PageSizeThumbnailTransformation() : null);
                Attach(component);
            }

            return component;
        }

        /// <inheritdoc/>
        public void RemovePageComponent(string name)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(PDFLayoutComponent));
            }

            var component = GetPDFPageComponentByName(name);
            if (component != null)
            {
                DisposeChildComponent(component);
            }
        }

        #endregion Implementation of IPDFLayoutComponent
    }
}
