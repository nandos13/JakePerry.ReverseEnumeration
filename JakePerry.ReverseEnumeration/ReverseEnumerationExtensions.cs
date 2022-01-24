using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JakePerry
{
    public static class ReverseEnumerationExtensions
    {
        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public static ReverseEnumerator<T> GetReverseEnumerator<T>(this IList<T> list)
        {
            return new ReverseEnumerable<T>(list).GetEnumerator();
        }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public static ReadonlyReverseEnumerator<T> GetReverseEnumerator<T>(this IReadOnlyList<T> list)
        {
            return new ReadonlyReverseEnumerable<T>(list).GetEnumerator();
        }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public static ReverseEnumerator<T> GetReverseEnumerator<T>(this List<T> list)
        {
            return new ReverseEnumerable<T>(list).GetEnumerator();
        }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public static ReverseEnumerator<T> GetReverseEnumerator<T>(this T[] array)
        {
            return new ReverseEnumerable<T>(array).GetEnumerator();
        }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public static ArraySegmentReverseEnumerator<T> GetReverseEnumerator<T>(this ArraySegment<T> arraySegment)
        {
            return new ArraySegmentReverseEnumerable<T>(arraySegment).GetEnumerator();
        }

        /// <inheritdoc cref="ImmutableListReverseEnumerable{T}.GetEnumerator()"/>
        /// <remarks>
        /// Note: This enumerator will throw an <see cref="InvalidOperationException"/> if the source
        /// list is modified during enumeration (see <see cref="ImmutableListReverseEnumerator{T}"/>
        /// documentation for details).
        /// <para>
        /// Immutable iteration is notably slower and should only be used in cases where the immutability
        /// guarantee is important.
        /// </para>
        /// </remarks>
        public static ImmutableListReverseEnumerator<T> GetImmutableReverseEnumerator<T>(this List<T> list)
        {
            return new ImmutableListReverseEnumerable<T>(list).GetEnumerator();
        }

        /// <summary>
        /// Enumerate the collection in reverse order.
        /// </summary>
        /// <typeparam name="T">The collection's element type.</typeparam>
        /// <param name="source">The current collection.</param>
        /// <returns>
        /// An enumerable object that will enumerate the collection in reverse order.
        /// </returns>
        public static ReverseEnumerable<T> Reversed<T>(this IList<T> source)
        {
            return new ReverseEnumerable<T>(source);
        }

        /// <inheritdoc cref="Reversed{T}(IList{T})"/>
        public static ReverseEnumerable<T> Reversed<T>(this List<T> source)
        {
            return new ReverseEnumerable<T>(source);
        }

        /// <inheritdoc cref="Reversed{T}(IList{T})"/>
        public static ReverseEnumerable<T> Reversed<T>(this T[] source)
        {
            return new ReverseEnumerable<T>(source);
        }

        /// <inheritdoc cref="Reversed{T}(IList{T})"/>
        public static ReadonlyReverseEnumerable<T> Reversed<T>(this IReadOnlyList<T> source)
        {
            return new ReadonlyReverseEnumerable<T>(source);
        }

        /// <inheritdoc cref="Reversed{T}(IList{T})"/>
        /// <remarks>
        /// Note: This enumerator will throw an <see cref="InvalidOperationException"/> if the source
        /// list is modified during enumeration (see <see cref="ImmutableListReverseEnumerator{T}"/>
        /// documentation for details).
        /// <para>
        /// Immutable iteration is notably slower and should only be used in cases where the immutability
        /// guarantee is important.
        /// </para>
        /// </remarks>
        public static ImmutableListReverseEnumerable<T> ReversedImmutable<T>(this List<T> source)
        {
            return new ImmutableListReverseEnumerable<T>(source);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Reversed() extension method instead.", false)]
        public static ReverseEnumerable<T> InReverseOrder<T>(this IList<T> source)
        {
            return new ReverseEnumerable<T>(source);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Reversed() extension method instead.", false)]
        public static ReadonlyReverseEnumerable<T> InReverseOrder<T>(this IReadOnlyList<T> source)
        {
            return new ReadonlyReverseEnumerable<T>(source);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Reversed() extension method instead.", false)]
        public static ReverseEnumerable<T> InReverseOrder<T>(this List<T> source)
        {
            return new ReverseEnumerable<T>(source);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Reversed() extension method instead.", false)]
        public static ReverseEnumerable<T> InReverseOrder<T>(this T[] source)
        {
            return new ReverseEnumerable<T>(source);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Reversed() extension method instead.", false)]
        public static ArraySegmentReverseEnumerable<T> InReverseOrder<T>(this ArraySegment<T> source)
        {
            return new ArraySegmentReverseEnumerable<T>(source);
        }
    }
}
