namespace PDFiumDotNET.Components.Contracts.Basic
{
    using System;
    using System.Drawing;
    using System.Globalization;

    /// <summary>
    /// Structure contains information about a point.
    /// </summary>
    /// <typeparam name="T">Type of used value.</typeparam>
    public struct PDFRectangle<T> : IEquatable<PDFRectangle<T>>
        where T : struct
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public PDFRectangle()
        {
            X = default;
            Y = default;
            Width = default;
            Height = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDFRectangle{T}"/> structure.
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
        /// Initializes a new instance of the <see cref="PDFRectangle{T}"/> structure.
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
        /// Initializes a new instance of the <see cref="PDFRectangle{T}"/> structure.
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

        /// <summary>
        ///  Gets the x-coordinate that is the sum of the <see cref="PDFRectangle{T}.X"/>
        ///  and <see cref="PDFRectangle{T}.Width"/> property values
        ///  of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public T Right
        {
            get
            {
#if NET5_0_OR_GREATER
                // This solution is for .NET 5 and higher
                return (dynamic)X + (dynamic)Width;
#else
                if (typeof(T) == typeof(double))
                {
                    return (T)(object)((double)(object)X + (double)(object)Width);
                }
                else if (typeof(T) == typeof(float))
                {
                    return (T)(object)((float)(object)X + (float)(object)Width);
                }
                else if (typeof(T) == typeof(int))
                {
                    return (T)(object)((int)(object)X + (int)(object)Width);
                }
                else
                {
                    throw new InvalidOperationException($"Not implemented for type {typeof(T)}!");
                }
#endif
            }
        }

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

        /// <summary>
        /// Gets the y-coordinate that is the sum of the <see cref="PDFRectangle{T}.Y"/>
        /// and <see cref="PDFRectangle{T}.Height"/> property values
        /// of this <see cref="PDFRectangle{T}"/> structure.
        /// </summary>
        public T Bottom
        {
            get
            {
#if NET5_0_OR_GREATER
                // This solution is for .NET 5 and higher
                return (dynamic)Y + (dynamic)Height;
#else
                if (typeof(T) == typeof(double))
                {
                    return (T)(object)((double)(object)Y + (double)(object)Height);
                }
                else if (typeof(T) == typeof(float))
                {
                    return (T)(object)((float)(object)Y + (float)(object)Height);
                }
                else if (typeof(T) == typeof(int))
                {
                    return (T)(object)((int)(object)Y + (int)(object)Height);
                }
                else
                {
                    throw new InvalidOperationException($"Not implemented for type {typeof(T)}!");
                }
#endif
            }
        }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Gets the string containing information about rectangle values.
        /// </summary>
        /// <returns>String containing rectangle values.</returns>
        /// <exception cref="InvalidOperationException">Thrown for unsupported type. Implement this type.</exception>
        public string Info()
        {
            string x1, y1, x2, y2, w, h;
            if (typeof(T) == typeof(double))
            {
                x1 = $"{Math.Round((double)(object)Left, 2)}";
                y1 = $"{Math.Round((double)(object)Top, 2)}";
                x2 = $"{Math.Round((double)(object)Right, 2)}";
                y2 = $"{Math.Round((double)(object)Bottom, 2)}";
                w = $"{Math.Round((double)(object)Width, 2)}";
                h = $"{Math.Round((double)(object)Height, 2)}";
            }
            else if (typeof(T) == typeof(float))
            {
                x1 = $"{Math.Round((float)(object)Left, 2)}";
                y1 = $"{Math.Round((float)(object)Top, 2)}";
                x2 = $"{Math.Round((float)(object)Right, 2)}";
                y2 = $"{Math.Round((float)(object)Bottom, 2)}";
                w = $"{Math.Round((float)(object)Width, 2)}";
                h = $"{Math.Round((float)(object)Height, 2)}";
            }
            else if (typeof(T) == typeof(int))
            {
                x1 = $"{Left}";
                y1 = $"{Top}";
                x2 = $"{Right}";
                y2 = $"{Bottom}";
                w = $"{Width}";
                h = $"{Height}";
            }
            else
            {
                throw new InvalidOperationException($"Not implemented for type {typeof(T)}!");
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                $"x: ({x1} - {x2}; w: {w})  y: ({y1} - {y2}; h: {h})");
        }

        /// <summary>
        /// Determines if this rectangle intersects with another rectangle.
        /// </summary>
        /// <param name="rect">Rectangle to check the intersection with.</param>
        /// <returns><c>true</c> if there is any intersection, otherwise <c>false</c>.</returns>
        public bool IntersectsWith(Rectangle rect)
        {
#if NET5_0_OR_GREATER
            return ((dynamic)rect.X < (dynamic)Right) && ((dynamic)X < (dynamic)rect.Right) &&
                ((dynamic)rect.Y < (dynamic)Bottom) && ((dynamic)Y < (dynamic)rect.Bottom);
#else
            if (typeof(T) == typeof(double))
            {
                return ((double)(object)rect.X < (double)(object)Right) && ((double)(object)X < (double)(object)rect.Right) &&
                    ((double)(object)rect.Y < (double)(object)Bottom) && ((double)(object)Y < (double)(object)rect.Bottom);
            }
            else if (typeof(T) == typeof(float))
            {
                return ((float)(object)rect.X < (float)(object)Right) && ((float)(object)X < (float)(object)rect.Right) &&
                    ((float)(object)rect.Y < (float)(object)Bottom) && ((float)(object)Y < (float)(object)rect.Bottom);
            }
            else if (typeof(T) == typeof(int))
            {
                return ((int)(object)rect.X < (int)(object)Right) && ((int)(object)X < (int)(object)rect.Right) &&
                    ((int)(object)rect.Y < (int)(object)Bottom) && ((int)(object)Y < (int)(object)rect.Bottom);
            }
            else
            {
                throw new InvalidOperationException($"Not implemented for type {typeof(T)}!");
            }
#endif
        }

        /// <summary>
        /// Creates a rectangle that represents the intersection between this and other rectangle.
        /// If there is no intersection, an empty rectangle is returned.
        /// </summary>
        /// <param name="other">Another rectangle to create intersections with.</param>
        public PDFRectangle<T> Intersect(PDFRectangle<T> other)
        {
#if NET5_0_OR_GREATER
            dynamic x1 = Math.Max((dynamic)this.X, (dynamic)other.X);
            dynamic x2 = Math.Min((dynamic)this.Right, (dynamic)other.Right);
            dynamic y1 = Math.Max((dynamic)this.Y, (dynamic)other.Y);
            dynamic y2 = Math.Min((dynamic)this.Bottom, (dynamic)other.Bottom);

            if (x2 >= x1 && y2 >= y1)
            {
                return new PDFRectangle<T>(x1, y1, x2 - x1, y2 - y1);
            }
#else
            if (typeof(T) == typeof(double))
            {
                double x1 = Math.Max((double)(object)this.X, (double)(object)other.X);
                double x2 = Math.Min((double)(object)this.Right, (double)(object)other.Right);
                double y1 = Math.Max((double)(object)this.Y, (double)(object)other.Y);
                double y2 = Math.Min((double)(object)this.Bottom, (double)(object)other.Bottom);

                if (x2 >= x1 && y2 >= y1)
                {
                    return new PDFRectangle<T>((T)(object)x1, (T)(object)y1, (T)(object)(x2 - x1), (T)(object)(y2 - y1));
                }
            }
            else if (typeof(T) == typeof(float))
            {
                float x1 = Math.Max((float)(object)this.X, (float)(object)other.X);
                float x2 = Math.Min((float)(object)this.Right, (float)(object)other.Right);
                float y1 = Math.Max((float)(object)this.Y, (float)(object)other.Y);
                float y2 = Math.Min((float)(object)this.Bottom, (float)(object)other.Bottom);

                if (x2 >= x1 && y2 >= y1)
                {
                    return new PDFRectangle<T>((T)(object)x1, (T)(object)y1, (T)(object)(x2 - x1), (T)(object)(y2 - y1));
                }
            }
            else if (typeof(T) == typeof(int))
            {
                int x1 = Math.Max((int)(object)this.X, (int)(object)other.X);
                int x2 = Math.Min((int)(object)this.Right, (int)(object)other.Right);
                int y1 = Math.Max((int)(object)this.Y, (int)(object)other.Y);
                int y2 = Math.Min((int)(object)this.Bottom, (int)(object)other.Bottom);

                if (x2 >= x1 && y2 >= y1)
                {
                    return new PDFRectangle<T>((T)(object)x1, (T)(object)y1, (T)(object)(x2 - x1), (T)(object)(y2 - y1));
                }
            }
            else
            {
                throw new InvalidOperationException($"Not implemented for type {typeof(T)}!");
            }
#endif

            return new PDFRectangle<T>();
        }

        #endregion Public methods

        #region Public override methods

        /// <summary>
        /// Returns the values of rectangle.
        /// </summary>
        /// <returns>Rectangle values.</returns>
        public override string ToString()
        {
            return Info();
        }

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
