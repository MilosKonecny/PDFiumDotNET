namespace PDFiumDotNET.Components.Contracts.Layout
{
    using PDFiumDotNET.Components.Contracts.Page;

    /// <summary>
    /// Interface defines functionality of page layout component.
    /// Provides <see cref="IPDFPageComponent"/> in required layout.
    /// </summary>
    public interface IPDFLayoutComponent : IPDFChildComponent
    {
        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> at the specified position.
        /// </summary>
        /// <param name="index">Index of the position from which to return <see cref="IPDFPageComponent"/>.</param>
        /// <returns>Returns <see cref="IPDFPageComponent"/> if the index points to existing component. Otherwise <c>null</c>.</returns>
        IPDFPageComponent this[int index] { get; }

        /// <summary>
        /// Gets the <see cref="IPDFPageComponent"/> by its name.
        /// </summary>
        /// <param name="name">The name of <see cref="IPDFPageComponent"/> to return.</param>
        /// <returns>Returns <see cref="IPDFPageComponent"/> that was createt with the specified name. Otherwise <c>null</c>.</returns>
        IPDFPageComponent this[string name] { get; }

        /// <summary>
        /// Creates new <see cref="IPDFPageComponent"/> with specified properties.
        /// </summary>
        /// <param name="name">Name of <see cref="IPDFPageComponent"/> used to identify component.</param>
        /// <param name="pageLayout">Layout of <see cref="IPDFPageComponent"/> to use in created component.</param>
        /// <returns>Created <see cref="IPDFPageComponent"/> if no component exists with specified name.
        /// If there is a component with specified name, this component will be returned.</returns>
        IPDFPageComponent CreatePageComponent(string name, PageLayoutType pageLayout);

        /// <summary>
        /// Removes specified <see cref="IPDFPageComponent"/>. Component is identified by its name.
        /// </summary>
        /// <param name="name">Name of <see cref="IPDFPageComponent"/> to destroy and remove.</param>
        void RemovePageComponent(string name);
    }
}
