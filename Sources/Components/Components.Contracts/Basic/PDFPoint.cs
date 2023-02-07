namespace PDFiumDotNET.Components.Contracts.Basic
{
    using System;

    /// <summary>
    /// Structure contains information about a point.
    /// </summary>
    /// <typeparam name="T">Type of used value.</typeparam>
    public struct PDFPoint<T> : IEquatable<PDFPoint<T>>
        where T : struct
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPoint{T}"/> struct.
        /// </summary>
        public PDFPoint()
        {
            X = default;
            Y = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPoint{T}"/> struct.
        /// </summary>
        /// <param name="x">Value for <see cref="X"/> property.</param>
        /// <param name="y">Value for <see cref="Y"/> property.</param>
        public PDFPoint(T x, T y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFPoint{T}"/> struct.
        /// </summary>
        /// <param name="point"><see cref="PDFPoint{T}"/> to copy values from.</param>
        public PDFPoint(PDFPoint<T> point)
        {
            this = point;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets a value indicating whether <see cref="PDFPoint{T}"/> contains 0 in properties <see cref="X"/> and <see cref="Y"/>.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return X.Equals(default(T)) && Y.Equals(default(T));
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="PDFPoint{T}"/> structure.
        /// </summary>
        public T X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="PDFPoint{T}"/> structure.
        /// </summary>
        public T Y { get; set; }

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

            if (obj is not PDFPoint<T>)
            {
                return false;
            }

            var point = (PDFPoint<T>)obj;
            return X.Equals(point.X) && Y.Equals(point.Y);
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
        /// Compares two <see cref="PDFPoint{T}"/> objects.
        /// The result specifies whether the values of the <see cref="PDFPoint{T}.X"/> and <see cref="PDFPoint{T}.Y"/> properties
        /// of the two <see cref="PDFPoint{T}"/> objects are equal.
        /// </summary>
        /// <param name="left">A first <see cref="PDFPoint{T}"/> to compare.</param>
        /// <param name="right">A second <see cref="PDFPoint{T}"/> to compare.</param>
        /// <returns><c>true</c> if the <see cref="PDFPoint{T}.X"/> and <see cref="PDFPoint{T}.Y"/> values of left and right are equal;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator ==(PDFPoint<T> left, PDFPoint<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="PDFPoint{T}"/> objects.
        /// The result specifies whether the values of the <see cref="PDFPoint{T}.X"/> or <see cref="PDFPoint{T}.Y"/> properties
        /// of the two <see cref="PDFPoint{T}"/> objects are unequal.
        /// </summary>
        /// <param name="left">A first <see cref="PDFPoint{T}"/> to compare.</param>
        /// <param name="right">A second <see cref="PDFPoint{T}"/> to compare.</param>
        /// <returns><c>true</c> if the values of either the <see cref="PDFPoint{T}.X"/> properties or the <see cref="PDFPoint{T}.Y"/> properties of left and right differ;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator !=(PDFPoint<T> left, PDFPoint<T> right)
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
        public bool Equals(PDFPoint<T> other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        #endregion Implementation of IEquatable<T>
    }
}
