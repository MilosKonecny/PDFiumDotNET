namespace PDFiumDotNET.Samples.SimpleWpf.Templates
{
    using System.Windows;
    using System.Windows.Controls;
    using PDFiumDotNET.Components.Contracts.Find;

    internal class FindTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PageTemplate { get; set; }

        public DataTemplate PositionTemplate { get; set; }

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
