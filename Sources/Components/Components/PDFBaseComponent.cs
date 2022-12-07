namespace PDFiumDotNET.Components
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using PDFiumDotNET.Components.Contracts;

    /// <summary>
    /// Base component class implements <see cref="INotifyPropertyChanged"/> and <see cref="IDisposable"/>.
    /// </summary>
    internal abstract class PDFBaseComponent : IPDFBaseComponent
    {
        #region Private fields

        private readonly List<IPDFChildComponent> _childComponents = new List<IPDFChildComponent>();
        private IPDFBaseComponent _parentComponent;

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFBaseComponent"/> class.
        /// </summary>
        protected PDFBaseComponent()
        {
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the <see cref="PDFComponent"/> - main component.
        /// </summary>
        public PDFComponent PDFComponent
        {
            get
            {
                return MainComponent as PDFComponent;
            }
        }

        #endregion Public properties

        #region Protected properties

        /// <summary>
        /// Gets all child components attached to this component.
        /// </summary>
        protected List<IPDFChildComponent> ListOfChildComponents => _childComponents;

        #endregion Protected properties

        #region Protected methods

        /// <summary>
        /// Method invokes <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of property to use in <see cref="PropertyChangedEventArgs"/>.
        /// If not defined, the name is determined from the call stack.</param>
        protected void InvokePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }

        #endregion Protected methods

        #region Protected virtual methods

        /// <summary>
        /// The method provides the possibility of a specific initialization in a derived class
        /// after the component was attached to other component.
        /// This method is called from <see cref="AttachedTo(IPDFBaseComponent)"/>.
        /// </summary>
        /// <param name="parentComponent">Component where was the component attached.</param>
        protected virtual void ComponentWasAttachedTo(IPDFBaseComponent parentComponent)
        {
        }

        /// <summary>
        /// This method performs initialization specific to the processed PDF document in <see cref="IPDFComponent"/>.
        /// It is necessary to check if the PDF document is already open.
        /// </summary>
        protected virtual void InitializeComponentAfterAttachedTo()
        {
        }

        /// <summary>
        /// This method is called from <see cref="Dispose()"/> to enable possibility of clean up in derived class.
        /// </summary>
        /// <param name="disposing"><c>true</c> if this method is called from <see cref="Dispose()"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            _childComponents.ForEach(childComponent => childComponent.Dispose());
            _childComponents.Clear();

            IsDisposed = true;
        }

        #endregion Protected virtual methods

        #region Implementation of IPDFBaseComponent

        /// <inheritdoc/>
        public IPDFComponent MainComponent
        {
            get
            {
                if (GetType() == typeof(PDFComponent))
                {
                    return this as IPDFComponent;
                }

                return _parentComponent?.MainComponent;
            }
        }

        /// <inheritdoc/>
        public IPDFBaseComponent ParentComponent
        {
            get
            {
                return _parentComponent;
            }

            private set
            {
                _parentComponent = value;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IPDFChildComponent> ChildComponents
        {
            get
            {
                return _childComponents;
            }
        }

        /// <inheritdoc/>
        public bool IsDisposed
        {
            get;
            protected set;
        }

        /// <inheritdoc/>
        public void AttachedTo(IPDFBaseComponent parentComponent)
        {
            _parentComponent = parentComponent ?? throw new ArgumentNullException(nameof(parentComponent));
            ComponentWasAttachedTo(parentComponent);
            InitializeComponentAfterAttachedTo();
        }

        /// <inheritdoc/>
        public void Attach(IPDFChildComponent childComponent)
        {
            if (childComponent == null)
            {
                throw new ArgumentNullException(nameof(childComponent));
            }

            _childComponents.Add(childComponent);
            childComponent.AttachedTo(this);
        }

        #endregion Implementation of IPDFBaseComponent

        #region Implementation of INotifyPropertyChanged

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implementation of INotifyPropertyChanged

        #region Implementation of IDisposable

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Implementation of IDisposable
    }
}
