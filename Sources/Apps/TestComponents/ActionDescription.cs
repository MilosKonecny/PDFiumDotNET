namespace PDFiumDotNET.Apps.TestComponents
{
    using System;

    /// <summary>
    /// The class describes certain action that can be performed during the test.
    /// </summary>
    internal class ActionDescription
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionDescription"/> class.
        /// </summary>
        public ActionDescription(
            char actionCharacter,
            ConsoleKey actionConsoleKey,
            bool withCTRLModifier,
            string description,
            string text,
            string groupText,
            Action<ActionDescription> action)
        {
            ActionCharacter = actionCharacter;
            ActionConsoleKey = actionConsoleKey;
            WithCTRLModifier = withCTRLModifier;
            Description = description;
            Text = text;
            GroupText = groupText;
            Action = action;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the character that starts the action.
        /// In case this character is '\0', the property <see cref="ActionConsoleKey"/> is used.
        /// </summary>
        public char ActionCharacter { get; }

        /// <summary>
        /// Gets the <see cref="ConsoleKey"/> that starts the action.
        /// </summary>
        public ConsoleKey ActionConsoleKey { get; }

        /// <summary>
        /// Gets the information whether the CTRL key have to be pressed.
        /// </summary>
        public bool WithCTRLModifier { get; }

        /// <summary>
        /// Gets the action description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the action short text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the action short text for group.
        /// </summary>
        public string GroupText { get; }

        /// <summary>
        /// Gets the action for execution.
        /// </summary>
        public Action<ActionDescription> Action { get; }

        #endregion Public properties
    }
}
