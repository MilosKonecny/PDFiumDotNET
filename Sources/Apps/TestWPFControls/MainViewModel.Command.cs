namespace PDFiumDotNET.Apps.TestWPFControls
{
    using System.Windows;
    using PDFiumDotNET.Apps.PDFViewWPF.Helper;

    /// <summary>
    /// View model class for <see cref="MainView"/>.
    /// </summary>
    public partial class MainViewModel
    {
        #region Public properties - commands

        /// <summary>
        /// Gets the command for test 1.
        /// </summary>
        public ViewModelCommand Test1Command { get; private set; }

        /// <summary>
        /// Gets the command for test 2.
        /// </summary>
        public ViewModelCommand Test2Command { get; private set; }

        /// <summary>
        /// Gets the command for test 3.
        /// </summary>
        public ViewModelCommand Test3Command { get; private set; }

        /// <summary>
        /// Gets the command for test 4.
        /// </summary>
        public ViewModelCommand Test4Command { get; private set; }

        /// <summary>
        /// Gets the command for stop test.
        /// </summary>
        public ViewModelCommand StopTestCommand { get; private set; }

        #endregion Public properties - commands

        #region Private methods - command related

        private async void ExecuteTest1Command()
        {
            await Test1().ConfigureAwait(false);
            Application.Current.Dispatcher.Invoke(() => InvokePropertyChangedEvent(string.Empty));
        }

        private bool CanExecuteTest1Command()
        {
            return !IsTestActive;
        }

        private async void ExecuteTest2Command()
        {
            await Test2().ConfigureAwait(false);
            InvokePropertyChangedEvent();
        }

        private bool CanExecuteTest2Command()
        {
            return !IsTestActive;
        }

        private async void ExecuteTest3Command()
        {
            await Test3().ConfigureAwait(false);
            InvokePropertyChangedEvent();
        }

        private bool CanExecuteTest3Command()
        {
            return !IsTestActive;
        }

        private async void ExecuteTest4Command()
        {
            await Test4().ConfigureAwait(false);
            InvokePropertyChangedEvent();
        }

        private bool CanExecuteTest4Command()
        {
            return !IsTestActive;
        }

        private void ExecuteStopTestCommand()
        {
            IsTestStopPending = true;
            InvokePropertyChangedEvent();
        }

        private bool CanExecuteStopTestCommand()
        {
            return IsTestActive && !IsTestStopPending;
        }

        #endregion Private methods - command related
    }
}
