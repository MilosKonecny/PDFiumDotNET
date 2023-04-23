namespace PDFiumDotNET.Apps.PDFViewForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pdfThumbnailControl = new WinFormsControls.PDFThumbnailControl();
            pdfControl = new WinFormsControls.PDFControl();
            splitContainer = new System.Windows.Forms.SplitContainer();
            tabControl = new System.Windows.Forms.TabControl();
            tabPageBookmarks = new System.Windows.Forms.TabPage();
            treeViewBookmarks = new System.Windows.Forms.TreeView();
            tabPageThumbnails = new System.Windows.Forms.TabPage();
            tabPageFindText = new System.Windows.Forms.TabPage();
            menuStrip = new System.Windows.Forms.MenuStrip();
            toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemFileClose = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemFileProperties = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemNavigate = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemNavigateFirstPage = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemNavigatePreviousPage = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemNavigateNextPage = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemNavigateLastPage = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemZoom = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemZoomIncrease = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemZoomDecrease = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItemZoomPageWidth = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemZoomPageHeight = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemViewPagesInOneColumn = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemViewPagesInTwoColumns = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemViewPagesInTwoColumnsSpecial = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItemViewAnnotations = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageBookmarks.SuspendLayout();
            tabPageThumbnails.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pdfThumbnailControl
            // 
            pdfThumbnailControl.AutoScroll = true;
            pdfThumbnailControl.AutoScrollMinSize = new System.Drawing.Size(612, 717);
            pdfThumbnailControl.BackColor = System.Drawing.SystemColors.ControlLight;
            pdfThumbnailControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pdfThumbnailControl.Dock = System.Windows.Forms.DockStyle.Fill;
            pdfThumbnailControl.Location = new System.Drawing.Point(4, 5);
            pdfThumbnailControl.Name = "pdfThumbnailControl";
            pdfThumbnailControl.PDFPageBackground = System.Drawing.Color.White;
            pdfThumbnailControl.PDFPageBorder = System.Drawing.Color.Silver;
            pdfThumbnailControl.PDFPageBorderThickness = new System.Drawing.Printing.Margins(1, 1, 1, 1);
            pdfThumbnailControl.PDFPageComponent = null;
            pdfThumbnailControl.Size = new System.Drawing.Size(612, 717);
            pdfThumbnailControl.TabIndex = 1;
            // 
            // pdfControl
            // 
            pdfControl.ActivatePageInCenter = true;
            pdfControl.ActivatePageOnClick = false;
            pdfControl.AutoScroll = true;
            pdfControl.AutoScrollMinSize = new System.Drawing.Size(1251, 765);
            pdfControl.BackColor = System.Drawing.SystemColors.ControlLight;
            pdfControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pdfControl.Dock = System.Windows.Forms.DockStyle.Fill;
            pdfControl.Location = new System.Drawing.Point(0, 0);
            pdfControl.Name = "pdfControl";
            pdfControl.PDFFindSelectionBackground = System.Drawing.Color.Transparent;
            pdfControl.PDFFindSelectionBorder = System.Drawing.Color.FromArgb(128, 128, 255);
            pdfControl.PDFPageActiveBorder = System.Drawing.Color.Black;
            pdfControl.PDFPageActiveBorderThickness = new System.Drawing.Printing.Margins(1, 1, 1, 1);
            pdfControl.PDFPageBackground = System.Drawing.Color.White;
            pdfControl.PDFPageBorder = System.Drawing.Color.Silver;
            pdfControl.PDFPageBorderThickness = new System.Drawing.Printing.Margins(1, 1, 1, 1);
            pdfControl.PDFPageComponent = null;
            pdfControl.PDFPageMargin = new System.Drawing.Size(5, 5);
            pdfControl.ShowPageLabel = false;
            pdfControl.Size = new System.Drawing.Size(1251, 765);
            pdfControl.TabIndex = 2;
            // 
            // splitContainer
            // 
            splitContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer.Location = new System.Drawing.Point(0, 50);
            splitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(tabControl);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(pdfControl);
            splitContainer.Size = new System.Drawing.Size(1889, 765);
            splitContainer.SplitterDistance = 628;
            splitContainer.SplitterWidth = 10;
            splitContainer.TabIndex = 3;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageBookmarks);
            tabControl.Controls.Add(tabPageThumbnails);
            tabControl.Controls.Add(tabPageFindText);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl.Location = new System.Drawing.Point(0, 0);
            tabControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(628, 765);
            tabControl.TabIndex = 0;
            // 
            // tabPageBookmarks
            // 
            tabPageBookmarks.Controls.Add(treeViewBookmarks);
            tabPageBookmarks.Location = new System.Drawing.Point(4, 34);
            tabPageBookmarks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPageBookmarks.Name = "tabPageBookmarks";
            tabPageBookmarks.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPageBookmarks.Size = new System.Drawing.Size(620, 727);
            tabPageBookmarks.TabIndex = 0;
            tabPageBookmarks.Text = "Bookmarks";
            tabPageBookmarks.UseVisualStyleBackColor = true;
            // 
            // treeViewBookmarks
            // 
            treeViewBookmarks.Dock = System.Windows.Forms.DockStyle.Fill;
            treeViewBookmarks.Location = new System.Drawing.Point(4, 5);
            treeViewBookmarks.Name = "treeViewBookmarks";
            treeViewBookmarks.Size = new System.Drawing.Size(612, 717);
            treeViewBookmarks.TabIndex = 0;
            treeViewBookmarks.AfterSelect += HandleTreeViewBookmarksAfterSelectEvent;
            // 
            // tabPageThumbnails
            // 
            tabPageThumbnails.Controls.Add(pdfThumbnailControl);
            tabPageThumbnails.Location = new System.Drawing.Point(4, 34);
            tabPageThumbnails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPageThumbnails.Name = "tabPageThumbnails";
            tabPageThumbnails.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPageThumbnails.Size = new System.Drawing.Size(620, 727);
            tabPageThumbnails.TabIndex = 1;
            tabPageThumbnails.Text = "Thumbnails";
            tabPageThumbnails.UseVisualStyleBackColor = true;
            // 
            // tabPageFindText
            // 
            tabPageFindText.Location = new System.Drawing.Point(4, 34);
            tabPageFindText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPageFindText.Name = "tabPageFindText";
            tabPageFindText.Size = new System.Drawing.Size(620, 727);
            tabPageFindText.TabIndex = 2;
            tabPageFindText.Text = "Find text";
            tabPageFindText.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemFile, toolStripMenuItemNavigate, toolStripMenuItemZoom, toolStripMenuItemView, toolStripMenuItemHelp });
            menuStrip.Location = new System.Drawing.Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            menuStrip.Size = new System.Drawing.Size(1889, 35);
            menuStrip.TabIndex = 4;
            menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemFileOpen, toolStripMenuItemFileClose, toolStripMenuItemFileProperties, toolStripMenuItemFileExit });
            toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            toolStripMenuItemFile.Size = new System.Drawing.Size(54, 29);
            toolStripMenuItemFile.Text = "File";
            // 
            // toolStripMenuItemFileOpen
            // 
            toolStripMenuItemFileOpen.Name = "toolStripMenuItemFileOpen";
            toolStripMenuItemFileOpen.Size = new System.Drawing.Size(194, 34);
            toolStripMenuItemFileOpen.Text = "&Open";
            toolStripMenuItemFileOpen.Click += HandleOpenMenuItemClickEvent;
            // 
            // toolStripMenuItemFileClose
            // 
            toolStripMenuItemFileClose.Name = "toolStripMenuItemFileClose";
            toolStripMenuItemFileClose.Size = new System.Drawing.Size(194, 34);
            toolStripMenuItemFileClose.Text = "&Close";
            toolStripMenuItemFileClose.Click += HandleCloseMenuItemClickEvent;
            // 
            // toolStripMenuItemFileProperties
            // 
            toolStripMenuItemFileProperties.Name = "toolStripMenuItemFileProperties";
            toolStripMenuItemFileProperties.Size = new System.Drawing.Size(194, 34);
            toolStripMenuItemFileProperties.Text = "&Properties";
            toolStripMenuItemFileProperties.Click += HandlePropertiesMenuItemClickEvent;
            // 
            // toolStripMenuItemFileExit
            // 
            toolStripMenuItemFileExit.Name = "toolStripMenuItemFileExit";
            toolStripMenuItemFileExit.Size = new System.Drawing.Size(194, 34);
            toolStripMenuItemFileExit.Text = "E&xit";
            toolStripMenuItemFileExit.Click += HandleExitMenuItemClickEvent;
            // 
            // toolStripMenuItemNavigate
            // 
            toolStripMenuItemNavigate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemNavigateFirstPage, toolStripMenuItemNavigatePreviousPage, toolStripMenuItemNavigateNextPage, toolStripMenuItemNavigateLastPage });
            toolStripMenuItemNavigate.Name = "toolStripMenuItemNavigate";
            toolStripMenuItemNavigate.Size = new System.Drawing.Size(98, 29);
            toolStripMenuItemNavigate.Text = "Navigate";
            // 
            // toolStripMenuItemNavigateFirstPage
            // 
            toolStripMenuItemNavigateFirstPage.Name = "toolStripMenuItemNavigateFirstPage";
            toolStripMenuItemNavigateFirstPage.Size = new System.Drawing.Size(270, 34);
            toolStripMenuItemNavigateFirstPage.Text = "First page";
            toolStripMenuItemNavigateFirstPage.Click += HandleNavigateFirstPageMenuItemClickEvent;
            // 
            // toolStripMenuItemNavigatePreviousPage
            // 
            toolStripMenuItemNavigatePreviousPage.Name = "toolStripMenuItemNavigatePreviousPage";
            toolStripMenuItemNavigatePreviousPage.Size = new System.Drawing.Size(270, 34);
            toolStripMenuItemNavigatePreviousPage.Text = "Previous page";
            toolStripMenuItemNavigatePreviousPage.Click += HandleNavigatePreviousPageMenuItemClickEvent;
            // 
            // toolStripMenuItemNavigateNextPage
            // 
            toolStripMenuItemNavigateNextPage.Name = "toolStripMenuItemNavigateNextPage";
            toolStripMenuItemNavigateNextPage.Size = new System.Drawing.Size(270, 34);
            toolStripMenuItemNavigateNextPage.Text = "Next page";
            toolStripMenuItemNavigateNextPage.Click += HandleNavigateNextPageMenuItemClickEvent;
            // 
            // toolStripMenuItemNavigateLastPage
            // 
            toolStripMenuItemNavigateLastPage.Name = "toolStripMenuItemNavigateLastPage";
            toolStripMenuItemNavigateLastPage.Size = new System.Drawing.Size(270, 34);
            toolStripMenuItemNavigateLastPage.Text = "Last page";
            toolStripMenuItemNavigateLastPage.Click += HandleNavigateLastPageMenuItemClickEvent;
            // 
            // toolStripMenuItemZoom
            // 
            toolStripMenuItemZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemZoomIncrease, toolStripMenuItemZoomDecrease, toolStripMenuItem3, toolStripMenuItemZoomPageWidth, toolStripMenuItemZoomPageHeight });
            toolStripMenuItemZoom.Name = "toolStripMenuItemZoom";
            toolStripMenuItemZoom.Size = new System.Drawing.Size(76, 29);
            toolStripMenuItemZoom.Text = "Zoom";
            // 
            // toolStripMenuItemZoomIncrease
            // 
            toolStripMenuItemZoomIncrease.Name = "toolStripMenuItemZoomIncrease";
            toolStripMenuItemZoomIncrease.Size = new System.Drawing.Size(236, 34);
            toolStripMenuItemZoomIncrease.Text = "&Increase zoom";
            toolStripMenuItemZoomIncrease.Click += HandleIncreaseZoomMenuItemClickEvent;
            // 
            // toolStripMenuItemZoomDecrease
            // 
            toolStripMenuItemZoomDecrease.Name = "toolStripMenuItemZoomDecrease";
            toolStripMenuItemZoomDecrease.Size = new System.Drawing.Size(236, 34);
            toolStripMenuItemZoomDecrease.Text = "&Decrease zoom";
            toolStripMenuItemZoomDecrease.Click += HandleDecreaseZoomMenuItemClickEvent;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(233, 6);
            // 
            // toolStripMenuItemZoomPageWidth
            // 
            toolStripMenuItemZoomPageWidth.Name = "toolStripMenuItemZoomPageWidth";
            toolStripMenuItemZoomPageWidth.Size = new System.Drawing.Size(236, 34);
            toolStripMenuItemZoomPageWidth.Text = "Page &width";
            toolStripMenuItemZoomPageWidth.Click += HandlePageWidthZoomMenuItemClickEvent;
            // 
            // toolStripMenuItemZoomPageHeight
            // 
            toolStripMenuItemZoomPageHeight.Name = "toolStripMenuItemZoomPageHeight";
            toolStripMenuItemZoomPageHeight.Size = new System.Drawing.Size(236, 34);
            toolStripMenuItemZoomPageHeight.Text = "Page &height";
            toolStripMenuItemZoomPageHeight.Click += HandlePageHeightZoomMenuItemClickEvent;
            // 
            // toolStripMenuItemView
            // 
            toolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemViewPagesInOneColumn, toolStripMenuItemViewPagesInTwoColumns, toolStripMenuItemViewPagesInTwoColumnsSpecial, toolStripMenuItem2, toolStripMenuItemViewAnnotations });
            toolStripMenuItemView.Name = "toolStripMenuItemView";
            toolStripMenuItemView.Size = new System.Drawing.Size(65, 29);
            toolStripMenuItemView.Text = "View";
            // 
            // toolStripMenuItemViewPagesInOneColumn
            // 
            toolStripMenuItemViewPagesInOneColumn.Name = "toolStripMenuItemViewPagesInOneColumn";
            toolStripMenuItemViewPagesInOneColumn.Size = new System.Drawing.Size(344, 34);
            toolStripMenuItemViewPagesInOneColumn.Text = "Pages in one column";
            toolStripMenuItemViewPagesInOneColumn.Click += HandleViewPagesInOneColumnMenuItemClickEvent;
            // 
            // toolStripMenuItemViewPagesInTwoColumns
            // 
            toolStripMenuItemViewPagesInTwoColumns.Name = "toolStripMenuItemViewPagesInTwoColumns";
            toolStripMenuItemViewPagesInTwoColumns.Size = new System.Drawing.Size(344, 34);
            toolStripMenuItemViewPagesInTwoColumns.Text = "Pages in two columns";
            toolStripMenuItemViewPagesInTwoColumns.Click += HandleViewPagesInTwoColumnsMenuItemClickEvent;
            // 
            // toolStripMenuItemViewPagesInTwoColumnsSpecial
            // 
            toolStripMenuItemViewPagesInTwoColumnsSpecial.Name = "toolStripMenuItemViewPagesInTwoColumnsSpecial";
            toolStripMenuItemViewPagesInTwoColumnsSpecial.Size = new System.Drawing.Size(344, 34);
            toolStripMenuItemViewPagesInTwoColumnsSpecial.Text = "Pages in two columns special";
            toolStripMenuItemViewPagesInTwoColumnsSpecial.Click += HandleViewPagesInTwoColumnsSpecialMenuItemClickEvent;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(341, 6);
            // 
            // toolStripMenuItemViewAnnotations
            // 
            toolStripMenuItemViewAnnotations.Name = "toolStripMenuItemViewAnnotations";
            toolStripMenuItemViewAnnotations.Size = new System.Drawing.Size(344, 34);
            toolStripMenuItemViewAnnotations.Text = "Annotations";
            toolStripMenuItemViewAnnotations.Click += HandleViewAnnotationsMenuItemClickEvent;
            // 
            // toolStripMenuItemHelp
            // 
            toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemHelpAbout });
            toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            toolStripMenuItemHelp.Size = new System.Drawing.Size(65, 29);
            toolStripMenuItemHelp.Text = "Help";
            // 
            // toolStripMenuItemHelpAbout
            // 
            toolStripMenuItemHelpAbout.Name = "toolStripMenuItemHelpAbout";
            toolStripMenuItemHelpAbout.Size = new System.Drawing.Size(164, 34);
            toolStripMenuItemHelpAbout.Text = "&About";
            toolStripMenuItemHelpAbout.Click += HandleAboutMenuItemClickEvent;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1889, 810);
            Controls.Add(menuStrip);
            Controls.Add(splitContainer);
            MainMenuStrip = menuStrip;
            Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            Name = "MainForm";
            Text = "PDFViewForms example application";
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPageBookmarks.ResumeLayout(false);
            tabPageThumbnails.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WinFormsControls.PDFThumbnailControl pdfThumbnailControl;
        private WinFormsControls.PDFControl pdfControl;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBookmarks;
        private System.Windows.Forms.TabPage tabPageThumbnails;
        private System.Windows.Forms.TabPage tabPageFindText;
        private System.Windows.Forms.TreeView treeViewBookmarks;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileClose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileProperties;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoomIncrease;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoomDecrease;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoomPageWidth;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZoomPageHeight;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemViewPagesInOneColumn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemViewPagesInTwoColumns;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemViewPagesInTwoColumnsSpecial;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemViewAnnotations;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelpAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNavigate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNavigateFirstPage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNavigatePreviousPage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNavigateNextPage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNavigateLastPage;
    }
}
