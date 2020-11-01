namespace PDFiumDotNET.Components.Contracts.Action
{
    using System;
    using PDFiumDotNET.Components.Contracts.Destination;

    /// <summary>
    /// Interface defines action within PDF document.
    /// </summary>
    public interface IPDFAction
    {
        /// <summary>
        /// Gets the action type.
        /// </summary>
        PDFActionType ActionType { get; }

        /// <summary>
        /// Gets destination associated with action.
        /// Returned value is not <c>null</c> only if <see cref="Type"/> is <see cref="PDFActionType.Goto"/> or <see cref="PDFActionType.RemoteGoto"/>.
        /// </summary>
        IPDFDestination Destination { get; }

        /// <summary>
        /// Gets the file associated with action.
        /// Returned value is not <c>null</c> only if <see cref="Type"/> is <see cref="PDFActionType.Uri"/>.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Gets the Uri associated with action.
        /// Returned value is not <c>null</c> only if <see cref="Type"/> is <see cref="PDFActionType.Launch"/> or <see cref="PDFActionType.RemoteGoto"/>.
        /// </summary>
        Uri UriPath { get; }

        /// <summary>
        /// Gets the simple info about destination for test purposes.
        /// </summary>
        string Info { get; }
    }
}
