namespace PDFiumDotNET.Apps.PDFViewForms
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Starter class of the 'PDFViewForms' example application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            // ApplicationConfiguration.Initialize();
            var form = new MainForm();
            Application.Run(form);
            form.Dispose();
        }
    }
}
