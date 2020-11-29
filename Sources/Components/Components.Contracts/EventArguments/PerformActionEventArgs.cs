namespace PDFiumDotNET.Components.Contracts.EventArguments
{
    using System;
    using PDFiumDotNET.Components.Contracts.Action;

    /// <summary>
    /// Event arguments class used to inform that the action outside of current pdf document is to perform.
    /// </summary>
    public class PerformActionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerformActionEventArgs"/> class.
        /// </summary>
        /// <param name="action">Action to perform.</param>
        public PerformActionEventArgs(IPDFAction action)
        {
            Action = action;
        }

        /// <summary>
        /// Gets the action to perform.
        /// </summary>
        public IPDFAction Action { get; }
    }
}
