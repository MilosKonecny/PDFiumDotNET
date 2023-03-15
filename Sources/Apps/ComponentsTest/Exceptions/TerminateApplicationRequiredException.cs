namespace PDFiumDotNET.App.ComponentsTest.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Specific exception is thrown on request to terminate the application.
    /// </summary>
    [Serializable]
    public class TerminateApplicationRequiredException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerminateApplicationRequiredException"/> class.
        /// </summary>
        public TerminateApplicationRequiredException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminateApplicationRequiredException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public TerminateApplicationRequiredException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminateApplicationRequiredException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference,
        /// the current exception is raised in a catch block that handles the inner exception.</param>
        public TerminateApplicationRequiredException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminateApplicationRequiredException"/> class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected TerminateApplicationRequiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /////// <summary>
        /////// Sets the System.Runtime.Serialization.SerializationInfo with information about the exception.
        /////// </summary>
        /////// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /////// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        ////public override void GetObjectData(SerializationInfo info, StreamingContext context)
        ////{
        ////    base.GetObjectData(info, context);
        ////}
    }
}
