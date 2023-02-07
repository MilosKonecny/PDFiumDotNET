namespace PDFiumDotNET.Components.Contracts.Basic
{
    using System;

    /// <summary>
    /// Structure contains information about a point.
    /// </summary>
    /// <typeparam name="T">Type of used value.</typeparam>
    public struct PDFRectangle<T> : IEquatable<PDFRectangle<T>>
        where T : struct
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRectangle{T}"/> struct.
        /// </summary>
        public PDFRectangle()
        {
            Width = default;
            Height = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRectangle{T}"/> struct.
        /// </summary>
        /// <param name="x">Value for <see cref="X"/> property.</param>
        /// <param name="y">Value for <see cref="Y"/> property.</param>
        /// <param name="height">Value for <see cref="Height"/> property.</param>
        /// <param name="width">Value for <see cref="Width"/> property.</param>
        public PDFRectangle(T x, T y, T width, T height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRectangle{T}"/> struct.
        /// </summary>
        /// <param name="location">Values for <see cref="X"/> and <see cref="Y"/> properties.</param>
        /// <param name="size">Values for <see cref="Width"/> and <see cref="Height"/> properties.</param>
        public PDFRectangle(PDFPoint<T> location, PDFSize<T> size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRectangle{T}"/> struct.
        /// </summary>
        /// <param name="rectangle"><see cref="PDFRectangle{T}"/> to copy values from.</param>
        public PDFRectangle(PDFRectangle<T> rectangle)
        {
            this = rectangle;
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets a value indicating whether <see cref="PDFRectangle{T}"/> contains 0 in properties <see cref="Width"/> and <see cref="Height"/>.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Width.Equals(default(T)) && Height.Equals(default(T));
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the upper-left corner of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public T X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of the upper-left corner of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public T Y { get; set; }

        /// <summary>
        /// Gets or sets the width of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public T Width { get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public T Height { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public PDFPoint<T> Location
        {
            get
            {
                return new PDFPoint<T>(X, Y);
            }
        }

        /// <summary>
        /// Gets or sets the size of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public PDFSize<T> Size
        {
            get
            {
                return new PDFSize<T>(Width, Height);
            }
        }

        /// <summary>
        /// Gets the x-coordinate of the left edge of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public T Left
        {
            get
            {
                return X;
            }
        }

        /////// <summary>
        ///////  Gets the x-coordinate that is the sum of the <see cref="PDFRectangle{T}.X"/>
        ///////  and <see cref="PDFRectangle{T}.Width"/> property values
        ///////  of this <see cref="PDFRectangle{T}"/> structure.
        /////// </summary>
        ////public T Right
        ////{
        ////    get
        ////    {
        ////        // This solution is for .NET 5 and higher
        ////        dynamic value1 = X;
        ////        dynamic value2 = Width;
        ////        return value1 + value2;
        ////    }
        ////}

        /// <summary>
        /// Gets the y-coordinate of the top edge of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public T Top
        {
            get
            {
                return Y;
            }
        }

        /////// <summary>
        /////// Gets the y-coordinate that is the sum of the <see cref="PDFRectangle{T}.Y"/>
        /////// and <see cref="PDFRectangle{T}.Height"/> property values
        /////// of this <see cref="PDFRectangle{T}"/> structure.
        /////// </summary>
        ////public T Bottom
        ////{
        ////    get
        ////    {
        ////        // This solution is for .NET 5 and higher
        ////        dynamic value1 = Y;
        ////        dynamic value2 = Height;
        ////        return value1 + value2;
        ////    }
        ////}

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

            if (obj is not PDFRectangle<T>)
            {
                return false;
            }

            var rectangle = (PDFRectangle<T>)obj;
            return X.Equals(rectangle.X) && Y.Equals(rectangle.Y) && Width.Equals(rectangle.Width) && Height.Equals(rectangle.Height);
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
        /// Compares two <see cref="PDFRectangle{T}"/> objects.
        /// The result specifies whether the values of the <see cref="PDFRectangle{T}.X"/>, <see cref="PDFRectangle{T}.Y"/>,
        /// <see cref="PDFRectangle{T}.Width"/> and <see cref="PDFRectangle{T}.Height"/> properties
        /// of the two <see cref="PDFRectangle{T}"/> objects are equal.
        /// </summary>
        /// <param name="left">A first <see cref="PDFRectangle{T}"/> to compare.</param>
        /// <param name="right">A second <see cref="PDFRectangle{T}"/> to compare.</param>
        /// <returns><c>true</c> if the <see cref="PDFRectangle{T}.X"/> properties, <see cref="PDFRectangle{T}.Y"/> properties,
        /// <see cref="PDFRectangle{T}.Width"/> properties and <see cref="PDFRectangle{T}.Height"/> properties of left and right are equal;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator ==(PDFRectangle<T> left, PDFRectangle<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="PDFRectangle{T}"/> objects.
        /// The result specifies whether the values of the <see cref="PDFRectangle{T}.X"/>, <see cref="PDFRectangle{T}.Y"/>,
        /// <see cref="PDFRectangle{T}.Width"/> or <see cref="PDFRectangle{T}.Height"/> properties
        /// of the two <see cref="PDFRectangle{T}"/> objects are unequal.
        /// </summary>
        /// <param name="left">A first <see cref="PDFRectangle{T}"/> to compare.</param>
        /// <param name="right">A second <see cref="PDFRectangle{T}"/> to compare.</param>
        /// <returns><c>true</c> if the values of either the <see cref="PDFRectangle{T}.X"/> properties, <see cref="PDFRectangle{T}.Y"/> properties,
        /// <see cref="PDFRectangle{T}.Width"/> properties or the <see cref="PDFRectangle{T}.Height"/> properties of left and right differ;
        /// otherwise, <c>false</c>.</returns>
        public static bool operator !=(PDFRectangle<T> left, PDFRectangle<T> right)
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
        public bool Equals(PDFRectangle<T> other)
        {
            return Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        #endregion Implementation of IEquatable<T>
    }
}
