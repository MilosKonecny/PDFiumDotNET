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
            tabPageThumbnails = new System.Windows.Forms.TabPage();
            tabPageFindText = new System.Windows.Forms.TabPage();
            menuStrip = new System.Windows.Forms.MenuStrip();
            toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemFileClose = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemFileProperties = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            tabControl.SuspendLayout();
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
            pdfControl.AutoScrollMinSize = new System.Drawing.Size(1255, 765);
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
            pdfControl.Size = new System.Drawing.Size(1255, 765);
            pdfControl.TabIndex = 2;
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            splitContainer.Location = new System.Drawing.Point(0, 45);
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
            splitContainer.SplitterWidth = 6;
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
            tabPageBookmarks.Location = new System.Drawing.Point(4, 34);
            tabPageBookmarks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPageBookmarks.Name = "tabPageBookmarks";
            tabPageBookmarks.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPageBookmarks.Size = new System.Drawing.Size(620, 727);
            tabPageBookmarks.TabIndex = 0;
            tabPageBookmarks.Text = "Bookmarks";
            tabPageBookmarks.UseVisualStyleBackColor = true;
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
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemFile, toolStripMenuItemHelp });
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
            toolStripMenuItemFileOpen.Size = new System.Drawing.Size(270, 34);
            toolStripMenuItemFileOpen.Text = "&Open";
            toolStripMenuItemFileOpen.Click += HandleOpenMenuItemClickEvent;
            // 
            // toolStripMenuItemFileClose
            // 
            toolStripMenuItemFileClose.Name = "toolStripMenuItemFileClose";
            toolStripMenuItemFileClose.Size = new System.Drawing.Size(270, 34);
            toolStripMenuItemFileClose.Text = "&Close";
            toolStripMenuItemFileClose.Click += HandleCloseMenuItemClickEvent;
            // 
            // toolStripMenuItemFileProperties
            // 
            toolStripMenuItemFileProperties.Name = "toolStripMenuItemFileProperties";
            toolStripMenuItemFileProperties.Size = new System.Drawing.Size(270, 34);
            toolStripMenuItemFileProperties.Text = "&Properties";
            toolStripMenuItemFileProperties.Click += HandlePropertiesMenuItemClickEvent;
            // 
            // toolStripMenuItemFileExit
            // 
            toolStripMenuItemFileExit.Name = "toolStripMenuItemFileExit";
            toolStripMenuItemFileExit.Size = new System.Drawing.Size(270, 34);
            toolStripMenuItemFileExit.Text = "E&xit";
            toolStripMenuItemFileExit.Click += HandleExitMenuItemClickEvent;
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
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileClose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileProperties;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelpAbout;
    }
}
