namespace PDFiumDotNET.Apps.FactoryDI
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using PDFiumDotNET.Components.Contracts;
    using PDFiumDotNET.Components.Contracts.Layout;

    /// <summary>
    /// Application service.
    /// </summary>
    public class ApplicationWorker : BackgroundService
    {
        private const string _pdfFile1 = @"Precalculus.pdf";
        private const string _pdfFile2 = @"..\..\..\..\..\..\TestData\PDFs\Precalculus.pdf";
        private readonly ILogger<ApplicationWorker> _logger;
        private readonly IPDFComponent _pdfComponent1;
        private readonly IPDFComponent _pdfComponent2;
        private string _pdfFileToUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationWorker"/> class.
        /// </summary>
        public ApplicationWorker(ILogger<ApplicationWorker> logger, IPDFComponent pdfComponent1, IPDFComponent pdfComponent2)
        {
            _logger = logger;
            _pdfComponent1 = pdfComponent1;
            _pdfComponent2 = pdfComponent2;
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (File.Exists(Path.GetFullPath(_pdfFile2)))
            {
                _pdfFileToUse = Path.GetFullPath(_pdfFile2);
            }
            else
            {
                _pdfFileToUse = Path.GetFullPath(_pdfFile1);
            }

            var automat = 0;
            _logger.LogInformation("Application worker started at: {time}", DateTimeOffset.Now);
            await Task.Delay(2000, stoppingToken).ConfigureAwait(false);
            _logger.LogInformation($"PDFComponent 1: {_pdfComponent1.GetHashCode()}");
            _logger.LogInformation($"PDFComponent 2: {_pdfComponent2.GetHashCode()}");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken).ConfigureAwait(false);

                switch (automat)
                {
                    case 0:
                        var folder = Directory.GetCurrentDirectory();
                        var documentToOpen = Path.Combine(folder, _pdfFileToUse);
                        documentToOpen = Path.GetFullPath(documentToOpen);
                        _logger.LogWarning($"Open document: {documentToOpen}");
                        var result = _pdfComponent1.OpenDocument(documentToOpen);
                        _logger.LogWarning($"Result: {result}");
                        automat = result == OpenDocumentResult.Success ? 1 : 100;
                        break;
                    case 1:
                        var pageComponent = _pdfComponent1.LayoutComponent.CreatePageComponent("Some Name", PageLayoutType.Standard);
                        _logger.LogWarning($"Count of pages in document: {pageComponent.PageCount}");
                        _pdfComponent1.LayoutComponent.RemovePageComponent("Some Name");
                        automat = 2;
                        break;
                    case 2:
                        _logger.LogWarning("Close document");
                        automat = 100;
                        break;
                    case 100:
                    default:
                        break;
                }
            }
        }
    }
}
