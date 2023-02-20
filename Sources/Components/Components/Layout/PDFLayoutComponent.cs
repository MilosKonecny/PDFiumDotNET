namespace PDFiumDotNET.Components.Layout
{
    using System;
    using System.Linq;
    using PDFiumDotNET.Components.Contracts.Layout;
    using PDFiumDotNET.Components.Contracts.Page;
    using PDFiumDotNET.Components.Page;
    using PDFiumDotNET.Components.Render;
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

        private static PDFRenderManager CreateRenderManager(PageLayoutType pageLayout)
        {
            PDFRenderManager renderManager;
            switch (pageLayout)
            {
                case PageLayoutType.Thumbnail:
                    renderManager = new PDFRenderManagerThumbnail();
                    break;
                case PageLayoutType.Standard:
                    renderManager = new PDFRenderManagerStandard();
                    break;
                case PageLayoutType.TwoColumns:
                    renderManager = new PDFRenderManagerTwoColumns(false);
                    break;
                case PageLayoutType.TwoColumnsSpecial:
                    renderManager = new PDFRenderManagerTwoColumns(true);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return renderManager;
        }

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
                // We are using page transformation only for thumbnail layout.
                var transformation = pageLayout == PageLayoutType.Thumbnail ? new PageSizeThumbnailTransformation() : null;
                PDFRenderManager renderManager = CreateRenderManager(pageLayout);
                component = new PDFPageComponent(name, renderManager, transformation);
                Attach(component);
            }

            return component;
        }

        /// <inheritdoc/>
        public IPDFPageComponent ChangePageLayout(string name, PageLayoutType pageLayout)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(PDFLayoutComponent));
            }

            if (this[name] is not PDFPageComponent component)
            {
                return null;
            }

            if (pageLayout == PageLayoutType.Thumbnail)
            {
                var transformation = new PageSizeThumbnailTransformation();
                component.Use(transformation);
            }

            PDFRenderManager renderManager = CreateRenderManager(pageLayout);
            component.Use(renderManager);

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
