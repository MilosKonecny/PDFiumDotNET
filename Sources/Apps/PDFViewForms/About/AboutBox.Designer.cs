namespace PDFiumDotNET.Apps.PDFViewForms.About
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPageAbout = new System.Windows.Forms.TabPage();
            textBoxAbout = new System.Windows.Forms.TextBox();
            tabPageOSSLicenses = new System.Windows.Forms.TabPage();
            textBoxOSSLicenses = new System.Windows.Forms.TextBox();
            buttonClose = new System.Windows.Forms.Button();
            tabControl1.SuspendLayout();
            tabPageAbout.SuspendLayout();
            tabPageOSSLicenses.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPageAbout);
            tabControl1.Controls.Add(tabPageOSSLicenses);
            tabControl1.Location = new System.Drawing.Point(18, 20);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(689, 460);
            tabControl1.TabIndex = 0;
            // 
            // tabPageAbout
            // 
            tabPageAbout.Controls.Add(textBoxAbout);
            tabPageAbout.Location = new System.Drawing.Point(4, 34);
            tabPageAbout.Name = "tabPageAbout";
            tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            tabPageAbout.Size = new System.Drawing.Size(681, 422);
            tabPageAbout.TabIndex = 0;
            tabPageAbout.Text = "About";
            tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // textBoxAbout
            // 
            textBoxAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxAbout.Location = new System.Drawing.Point(3, 3);
            textBoxAbout.Multiline = true;
            textBoxAbout.Name = "textBoxAbout";
            textBoxAbout.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxAbout.Size = new System.Drawing.Size(675, 416);
            textBoxAbout.TabIndex = 0;
            // 
            // tabPageOSSLicenses
            // 
            tabPageOSSLicenses.Controls.Add(textBoxOSSLicenses);
            tabPageOSSLicenses.Location = new System.Drawing.Point(4, 34);
            tabPageOSSLicenses.Name = "tabPageOSSLicenses";
            tabPageOSSLicenses.Padding = new System.Windows.Forms.Padding(3);
            tabPageOSSLicenses.Size = new System.Drawing.Size(681, 422);
            tabPageOSSLicenses.TabIndex = 1;
            tabPageOSSLicenses.Text = "OSS Licenses";
            tabPageOSSLicenses.UseVisualStyleBackColor = true;
            // 
            // textBoxOSSLicenses
            // 
            textBoxOSSLicenses.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxOSSLicenses.Location = new System.Drawing.Point(3, 3);
            textBoxOSSLicenses.Multiline = true;
            textBoxOSSLicenses.Name = "textBoxOSSLicenses";
            textBoxOSSLicenses.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxOSSLicenses.Size = new System.Drawing.Size(675, 416);
            textBoxOSSLicenses.TabIndex = 0;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonClose.Location = new System.Drawing.Point(591, 486);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(116, 38);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "&Close";
            buttonClose.UseVisualStyleBackColor = true;
            // 
            // AboutBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(725, 544);
            Controls.Add(buttonClose);
            Controls.Add(tabControl1);
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox";
            Padding = new System.Windows.Forms.Padding(15, 17, 15, 17);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "About";
            tabControl1.ResumeLayout(false);
            tabPageAbout.ResumeLayout(false);
            tabPageAbout.PerformLayout();
            tabPageOSSLicenses.ResumeLayout(false);
            tabPageOSSLicenses.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.TabPage tabPageOSSLicenses;
        private System.Windows.Forms.TextBox textBoxAbout;
        private System.Windows.Forms.TextBox textBoxOSSLicenses;
        private System.Windows.Forms.Button buttonClose;
    }
}
