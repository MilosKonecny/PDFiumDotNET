namespace PDFiumDotNET.Apps.Common
{
    using System;
    using System.Threading;
    using System.Windows;

    /// <summary>
    /// The class <see cref="ActionExtensions"/> implements extensions for the class <see cref="Action"/>.
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// The method executes <see cref="Action"/> in application's dispatcher.
        /// </summary>
        /// <param name="self"><see cref="Action"/> to invoke.</param>
        public static void SafeInvoke(this Action self)
        {
            if (self == null)
            {
                return;
            }

            if (Application.Current.Dispatcher.Thread == Thread.CurrentThread)
            {
                self();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(self);
            }
        }
    }
}
