namespace PDFiumDotNET.Components
{
    using PDFiumDotNET.Components.Contracts;

    /// <inheritdoc/>
    internal abstract class PDFChildComponent : PDFBaseComponent, IPDFChildComponent
    {
        #region Protected virtual methods

        /// <summary>
        /// Protected virtual method called from <see cref="DocumentOpening(string)"/>.
        /// </summary>
        /// <param name="file">Opening PDF file.</param>
        protected virtual void ProcessDocumentOpening(string file)
        {
            ListOfChildComponents.ForEach(component => component.DocumentOpening(file));
        }

        /// <summary>
        /// Protected virtual method called from <see cref="DocumentOpened(string)"/>.
        /// </summary>
        /// <param name="file">Opened PDF file.</param>
        protected virtual void ProcessDocumentOpened(string file)
        {
            ListOfChildComponents.ForEach(component => component.DocumentOpened(file));
        }

        /// <summary>
        /// Protected virtual method called from <see cref="DocumentOpenFailed(string)"/>.
        /// </summary>
        /// <param name="file">PDF file attempted to open.</param>
        protected virtual void ProcessDocumentOpenFailed(string file)
        {
            ListOfChildComponents.ForEach(component => component.DocumentOpenFailed(file));
        }

        /// <summary>
        /// Protected virtual method called from <see cref="DocumentClosing"/>.
        /// </summary>
        protected virtual void ProcessDocumentClosing()
        {
            ListOfChildComponents.ForEach(component => component.DocumentClosing());
        }

        /// <summary>
        /// Protected virtual method called from <see cref="DocumentClosed"/>.
        /// </summary>
        protected virtual void ProcessDocumentClosed()
        {
            ListOfChildComponents.ForEach(component => component.DocumentClosed());
        }

        #endregion Protected virtual methods

        #region Implementation of IPDFDocumentObserver

        /// <inheritdoc/>
        public void DocumentOpening(string file)
        {
            ProcessDocumentOpening(file);
        }

        /// <inheritdoc/>
        public void DocumentOpened(string file)
        {
            ProcessDocumentOpened(file);
        }

        /// <inheritdoc/>
        public void DocumentOpenFailed(string file)
        {
            ProcessDocumentOpenFailed(file);
        }

        /// <inheritdoc/>
        public void DocumentClosing()
        {
            ProcessDocumentClosing();
        }

        /// <inheritdoc/>
        public void DocumentClosed()
        {
            ProcessDocumentClosed();
        }

        #endregion Implementation of IPDFDocumentObserver
    }
}
