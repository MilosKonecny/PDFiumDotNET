namespace PDFiumDotNET.Components.Factory
{
    using PDFiumDotNET.Components.Contracts;

    /// <summary>
    /// Factory class used to avoid to reference the implementation assembly.
    /// </summary>
    public static class PDFFactory
    {
        /// <summary>
        /// Gets the new instance of <see cref="IPDFComponent"/> implementation.
        /// </summary>
        public static IPDFComponent PDFComponent
        {
            get
            {
                ////// Other possibility to get the instance from already loaded assembly.
                ////// The condition 'already loaded' should be fulfilled by another part of application.
                ////// For example MEF.
                ////var specificType = AppDomain.CurrentDomain.GetAssemblies()
                ////    .SelectMany(x => x.GetTypes())
                ////    .Where(x => typeof(IPDFComponent).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                ////    .FirstOrDefault();
                ////if (specificType != null)
                ////{
                ////    return Activator.CreateInstance(specificType) as IPDFComponent;
                ////}
                ////// PDFiumDotNET.Components not loaded?
                ////return null;

                return new PDFComponent();
            }
        }
    }
}
