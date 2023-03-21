namespace PDFiumDotNET.Apps.SimpleWpf.Helper
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Implementation of <see cref="ICommand"/> usable in MVVM.
    /// </summary>
    /// <typeparam name="T">Type of command parameter.</typeparam>
    /// <seealso cref="ICommand" />
    public class ViewModelExtCommand<T> : ICommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelExtCommand{T}"/> class.
        /// </summary>
        /// <param name="executeCommand">The method used to execute command.</param>
        public ViewModelExtCommand(Action<T> executeCommand)
        {
            ExecuteCommand = executeCommand;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelExtCommand{T}"/> class.
        /// </summary>
        /// <param name="executeCommand">The method used to execute command.</param>
        /// <param name="canExecuteCommand">The method to check if the command can be executed.</param>
        public ViewModelExtCommand(Action<T> executeCommand, Func<T, bool> canExecuteCommand)
        {
            ExecuteCommand = executeCommand;
            CanExecuteCommand = canExecuteCommand;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the method to execute command.
        /// </summary>
        /// <value>
        /// The method to execute command.
        /// </value>
        public Action<T> ExecuteCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the method to check if the command can be executed.
        /// </summary>
        /// <value>
        /// The can execute command.
        /// </value>
        public Func<T, bool> CanExecuteCommand
        {
            get;
            private set;
        }

        #endregion Public properties

        #region Private properties

        private event EventHandler CanExecuteChangedPrivate;

        #endregion Private properties

        #region Public methods

        /// <summary>
        /// The method invokes the event <see cref="CanExecuteChanged"/>.
        /// </summary>
        public void InvokeCanExecuteChangedEvent()
        {
            CanExecuteChangedPrivate?.Invoke(this, EventArgs.Empty);
        }

        #endregion Public methods

        #region Implementation of ICommand

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            var commandValue = (T)parameter;
            if (CanExecuteCommand == null)
            {
                return true;
            }

            return CanExecuteCommand(commandValue);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            var commandValue = (T)parameter;
            ExecuteCommand?.Invoke(commandValue);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedPrivate += value;
            }

            remove
            {
                CanExecuteChangedPrivate -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion Implementation of ICommand
    }
}
