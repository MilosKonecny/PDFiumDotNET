namespace PDFiumDotNET.Apps.SimpleWpf.Templates
{
    using System.Windows;
    using System.Windows.Controls;
    using PDFiumDotNET.Components.Contracts.Find;

    /// <summary>
    /// Template selector used for hiearchical tree to select two different templates.
    /// </summary>
    public class FindTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Template for page element.
        /// </summary>
        public DataTemplate PageTemplate { get; set; }

        /// <summary>
        /// Template for position element.
        /// </summary>
        public DataTemplate PositionTemplate { get; set; }

        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IPDFFindPage)
            {
                return PageTemplate;
            }
            else if (item is IPDFFindPosition)
            {
                return PositionTemplate;
            }

            return null;
        }
    }
}
