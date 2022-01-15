﻿using System.Collections.Generic;

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
        public static ReverseEnumerator<T> GetReverseEnumerator<T>(this IReadOnlyList<T> list)
        {
            return new ReverseEnumerable<T>(list).GetEnumerator();
        }

        /// <inheritdoc cref="ListReverseEnumerable{T}.GetEnumerator()"/>
        public static ListReverseEnumerator<T> GetReverseEnumerator<T>(this List<T> list)
        {
            return new ListReverseEnumerable<T>(list).GetEnumerator();
        }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public static ReverseEnumerator<T> GetReverseEnumerator<T>(this T[] array)
        {
            return new ReverseEnumerable<T>(array).GetEnumerator();
        }

        /// <summary>
        /// Enumerate the collection in reverse order.
        /// </summary>
        /// <typeparam name="T">The collection's element type.</typeparam>
        /// <param name="source">The current collection.</param>
        /// <returns>
        /// An enumerable object that will enumerate the collection in reverse order.
        /// </returns>
        /// <example>
        /// <code>
        /// foreach (var val in collection.InReverseOrder())
        ///     Console.Write(val);
        /// </code>
        /// </example>
        public static ReverseEnumerable<T> InReverseOrder<T>(this IList<T> source)
        {
            return new ReverseEnumerable<T>(new ListProxy<T>(source));
        }

        /// <inheritdoc cref="InReverseOrder{T}(IList{T})"/>
        public static ReverseEnumerable<T> InReverseOrder<T>(this IReadOnlyList<T> source)
        {
            return new ReverseEnumerable<T>(new ListProxy<T>(source));
        }

        /// <inheritdoc cref="InReverseOrder{T}(IList{T})"/>
        public static ListReverseEnumerable<T> InReverseOrder<T>(this List<T> source)
        {
            return new ListReverseEnumerable<T>(source);
        }

        /// <inheritdoc cref="InReverseOrder{T}(IList{T})"/>
        public static ReverseEnumerable<T> InReverseOrder<T>(this T[] source)
        {
            return new ReverseEnumerable<T>(source);
        }
    }
}
