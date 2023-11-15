namespace PDFiumDotNET.Components.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using PDFiumDotNET.Components;
    using PDFiumDotNET.Components.Contracts;

    /// <summary>
    /// Extension class implements extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// The method registers <see cref="IPDFComponent"/> as a singleton service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/> to register <see cref="IPDFComponent"/> in.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection ConfigurePDFiumDotNETAsSingleton(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton(typeof(IPDFComponent), typeof(PDFComponent));
        }

        /// <summary>
        /// The method registers <see cref="IPDFComponent"/> as a scoped service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/> to register <see cref="IPDFComponent"/> in.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection ConfigurePDFiumDotNETAsScoped(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped(typeof(IPDFComponent), typeof(PDFComponent));
        }

        /// <summary>
        /// The method registers <see cref="IPDFComponent"/> as a transient service in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection"><see cref="IServiceCollection"/> to register <see cref="IPDFComponent"/> in.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection ConfigurePDFiumDotNETAsTransient(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient(typeof(IPDFComponent), typeof(PDFComponent));
        }
    }
}
