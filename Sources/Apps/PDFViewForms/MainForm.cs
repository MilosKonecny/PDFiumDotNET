namespace PDFiumDotNET.Apps.PDFViewForms
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Windows.Forms;
    using PDFiumDotNET.Apps.Common;
    using PDFiumDotNET.Apps.PDFViewForms.About;
    using PDFiumDotNET.Apps.PDFViewForms.Contracts;
    using PDFiumDotNET.Components.Contracts.Bookmark;
    using PDFiumDotNET.Components.Contracts.Information;
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Class implements main form of the 'PDFViewForms' example application.
    /// </summary>
    internal partial class MainForm : Form, IMainView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm(IMainPresenterForView presenter)
        {
            InitializeComponent();

            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the presenter of the MVP structural pattern.
        /// </summary>
        public IMainPresenterForView Presenter { get; private set; }

        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> used in PDF document view.
        /// </summary>
        public IPDFPageComponent PDFPageComponentForView
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> used in thumbnail PDF document view.
        /// </summary>
        public IPDFPageComponent PDFPageComponentForThumbnail
        {
            get;
            private set;
        }

        #endregion Public properties

        #region Protected override methods

        /// <inheritdoc/>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Presenter.ViewInitialized(this);

            Text += " (" + CommonInformation.Info + ")";
        }

        #endregion Protected override methods

        #region Private methods

        private void AddChildNodes(TreeNode treeNode, ObservableCollection<IPDFBookmark> childs)
        {
            if (childs == null || childs.Count == 0)
            {
                return;
            }

            foreach (var bookmark in childs)
            {
                var childNode = new TreeNode(bookmark.Text);
                childNode.Tag = bookmark;
                if (bookmark.IsOpened)
                {
                    childNode.Expand();
                }

                AddChildNodes(childNode, bookmark.Bookmarks);
                treeNode.Nodes.Add(childNode);
            }
        }

        #endregion Private methods

        #region Private event handler methods

        private void HandleOpenMenuItemClickEvent(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                // Disable "Method 'X' passes a literal string as parameter 'Y' of a call to 'Z'."
#pragma warning disable CA1303
                // ToDo: Hard coded text
                Filter = "PDF file (*.pdf)|*.pdf|All files (*.*)|*.*",
                Title = "Select PDF file",
#pragma warning restore
                Multiselect = false,
                ValidateNames = true,
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Presenter.OpenFile(dlg.FileName);
            }

            dlg.Dispose();
        }

        private void HandleCloseMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.CloseFile();
        }

        private void HandlePropertiesMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.ShowInformation();
        }

        private void HandleExitMenuItemClickEvent(object sender, EventArgs e)
        {
            Close();
        }

        private void HandleIncreaseZoomMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.IncreaseZoom(pdfControl);
        }

        private void HandleDecreaseZoomMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.DecreaseZoom(pdfControl);
        }

        private void HandlePageWidthZoomMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.SetPageWidthZoom();
        }

        private void HandlePageHeightZoomMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.SetPageHeightZoom();
        }

        private void HandleNavigateFirstPageMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.NavigateToFirstPage();
        }

        private void HandleNavigatePreviousPageMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.NavigateToPreviousPage();
        }

        private void HandleNavigateNextPageMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.NavigateToNextPage();
        }

        private void HandleNavigateLastPageMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.NavigateToLastPage();
        }

        private void HandleViewPagesInOneColumnMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.ViewPagesInOneColumn();
        }

        private void HandleViewPagesInTwoColumnsMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.ViewPagesInTwoColumns();
        }

        private void HandleViewPagesInTwoColumnsSpecialMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.ViewPagesInTwoColumnsSpecial();
        }

        private void HandleViewAnnotationsMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.ShowAnnotations();
        }

        private void HandleAboutMenuItemClickEvent(object sender, EventArgs e)
        {
            Presenter.ShowAbout();
        }

        private void HandleTreeViewBookmarksAfterSelectEvent(object sender, TreeViewEventArgs e)
        {
            Presenter.BookmarkActivated(e.Node.Tag as IPDFBookmark);
        }

        #endregion Private event handler methods

        #region Implementation of IMainView

        /// <inheritdoc/>
        public int PDFActualWidth => pdfControl.Width;

        /// <inheritdoc/>
        public int PDFActualHeight => pdfControl.Height;

        /// <inheritdoc/>
        public Size PDFPageMargin => pdfControl.PDFPageMargin;

        /// <inheritdoc/>
        public void InvalidatePDFView()
        {
            pdfControl?.Invalidate();
        }

        /// <inheritdoc/>
        public void ShowError(string title, string error)
        {
            MessageBox.Show(error, title);
        }

        /// <inheritdoc/>
        public void ShowDocumentInformation(string title, IPDFInformation information)
        {
            // ToDo: Text in source
            var text = "Properties" + Environment.NewLine;

            foreach (var property in information.GetType().GetProperties())
            {
                if (string.Equals(property.Name, nameof(information.DocumentPermissions), StringComparison.Ordinal))
                {
                    continue;
                }

                text += "\t" + property.Name + ": " + property.GetValue(information);
                text += Environment.NewLine;
            }

            text += Environment.NewLine;
            text += "Permissions" + Environment.NewLine;

            foreach (PDFPermissions enumValue in Enum.GetValues(typeof(PDFPermissions)))
            {
                text += "\t" + enumValue.ToString() + ": " + (((information.DocumentPermissions & enumValue) == enumValue) ? "yes" : "no");
                text += Environment.NewLine;
            }

            MessageBox.Show(text, title);
        }

        /// <inheritdoc/>
        public void ShowAboutBox()
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
            box.Dispose();
        }

        /// <inheritdoc/>
        public void SetPDFPageComponentForView(IPDFPageComponent pageComponent)
        {
            PDFPageComponentForView = pageComponent;
            pdfControl.PDFPageComponent = pageComponent;
        }

        /// <inheritdoc/>
        public void SetPDFPageComponentForThumbnail(IPDFPageComponent pageComponent)
        {
            PDFPageComponentForThumbnail = pageComponent;
            pdfThumbnailControl.PDFPageComponent = pageComponent;
        }

        /// <inheritdoc/>
        public void EnableFileOpen(bool enable)
        {
            toolStripMenuItemFileOpen.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableFileClose(bool enable)
        {
            toolStripMenuItemFileClose.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableFileProperties(bool enable)
        {
            toolStripMenuItemFileProperties.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableNavigateFirstPage(bool enable)
        {
            toolStripMenuItemNavigateFirstPage.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableNavigatePreviousPage(bool enable)
        {
            toolStripMenuItemNavigatePreviousPage.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableNavigateNextPage(bool enable)
        {
            toolStripMenuItemNavigateNextPage.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableNavigateLastPage(bool enable)
        {
            toolStripMenuItemNavigateLastPage.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableZoomIncrease(bool enable)
        {
            toolStripMenuItemZoomIncrease.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableZoomDecrease(bool enable)
        {
            toolStripMenuItemZoomDecrease.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableZoomPageWidth(bool enable)
        {
            toolStripMenuItemZoomPageWidth.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableZoomPageHeight(bool enable)
        {
            toolStripMenuItemZoomPageHeight.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableViewPagesInOneColumn(bool enable)
        {
            toolStripMenuItemViewPagesInOneColumn.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableViewPagesInTwoColumns(bool enable)
        {
            toolStripMenuItemViewPagesInTwoColumns.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableViewPagesInTwoColumnsSpecial(bool enable)
        {
            toolStripMenuItemViewPagesInTwoColumnsSpecial.Enabled = enable;
        }

        /// <inheritdoc/>
        public void EnableViewAnnotations(bool enable)
        {
            toolStripMenuItemViewAnnotations.Enabled = enable;
        }

        /// <inheritdoc/>
        public void ClearBookmarks()
        {
            treeViewBookmarks.Nodes.Clear();
        }

        /// <inheritdoc/>
        public void PopulateBookmarks(ObservableCollection<IPDFBookmark> bookmarks)
        {
            if (bookmarks == null)
            {
                return;
            }

            foreach (var bookmark in bookmarks)
            {
                var treeNode = new TreeNode(bookmark.Text);
                treeNode.Tag = bookmark;
                if (bookmark.IsOpened)
                {
                    treeNode.Expand();
                }

                AddChildNodes(treeNode, bookmark.Bookmarks);
                treeViewBookmarks.Nodes.Add(treeNode);
            }
        }

        #endregion Implementation of IMainView
    }
}
