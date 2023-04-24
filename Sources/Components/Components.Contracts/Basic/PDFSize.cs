namespace PDFiumDotNET.Components.Contracts.Basic
{
    using System;

    /// <summary>
    /// Structure contains information about size.
    /// </summary>
    /// <typeparam name="T">Type of used value.</typeparam>
    public struct PDFSize<T> : IEquatable<PDFSize<T>>
        where T : struct
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFSize{T}"/> structure.
        /// </summary>
        public PDFSize()
        {
            Width = default;
            Height = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFSize{T}"/> structure.
        /// </summary>
        /// <param name="width">Value for <see cref="Width"/> property.</param>
        /// <param name="height">Value for <see cref="Height"/> property.</param>
        public PDFSize(T width, T height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFSize{T}"/> structure.
        /// </summary>
        /// <param name="size"><see cref="PDFSize{T}"/> to copy values from.</param>
        public PDFSize(PDFSize<T> size)
        {
            this = size;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets a value indicating whether <see cref="PDFSize{T}"/> contains 0 in properties <see cref="Width"/> and <see cref="Height"/>.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Width.Equals(default(T)) && Height.Equals(default(T));
            }
        }

        /// <summary>
        /// Gets or sets the width of this <see cref="PDFSize{T}"/> structure.
        /// </summary>
        public T Width { get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="PDFSize{T}"/> structure.
        /// </summary>
        public T Height { get; set; }

        #endregion Public properties

        #region Public override methods

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is not PDFSize<T>)
            {
                return false;
            }

            var size = (PDFSize<T>)obj;
            return Width.Equals(size.Width) && Height.Equals(size.Height);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Compares two <see cref="PDFSize{T}"/> objects.
        /// The result specifies whether the values of the <see cref="PDFSize{T}.Width"/> and <see cref="PDFSize{T}.Height"/> properties
        /// of the two <see cref="PDFSize{T}"/> objects are equal.
        /// </summary>
        /// <param name="left">A first <see cref="PDFSize{T}"/> to compare.</param>
        /// <param name="right">A second <see cref="PDFSize{T}"/> to compare.</param>
        /// <returns><c>true</c> if the <see cref="PDFSize{T}.Width"/> and <see cref="PDFSize{T}.Height"/> values of left and right are equal;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator ==(PDFSize<T> left, PDFSize<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="PDFSize{T}"/> objects.
        /// The result specifies whether the values of the <see cref="PDFSize{T}.Width"/> or <see cref="PDFSize{T}.Height"/> properties
        /// of the two <see cref="PDFSize{T}"/> objects are unequal.
        /// </summary>
        /// <param name="left">A first <see cref="PDFSize{T}"/> to compare.</param>
        /// <param name="right">A second <see cref="PDFSize{T}"/> to compare.</param>
        /// <returns><c>true</c> if the values of either the <see cref="PDFSize{T}.Width"/> properties or the <see cref="PDFSize{T}.Height"/> properties of left and right differ;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator !=(PDFSize<T> left, PDFSize<T> right)
        {
            return !(left == right);
        }

        #endregion Public override methods

        #region Implementation of IEquatable<T>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><c>true</c> if the current object is equal to the other parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(PDFSize<T> other)
        {
            return Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        #endregion Implementation of IEquatable<T>
    }
}
