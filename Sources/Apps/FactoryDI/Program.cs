namespace PDFiumDotNET.Apps.FactoryDI
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PDFiumDotNET.Components.Extensions;

    /// <summary>
    /// Main application class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method of application.
        /// </summary>
        /// <param name="args">Arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.ConfigurePDFiumDotNETAsTransient();
            builder.Services.AddSingleton(typeof(IHostedService), typeof(ApplicationWorker));
            var host = builder.Build();
            host.Run();
        }
    }
}
