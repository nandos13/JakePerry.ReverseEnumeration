using System.Collections.Generic;

namespace JakePerry
{
    public static class ReverseEnumerationExtensions
    {
        /// <remarks>
        /// Note: This method will incur boxing. Consider casting <paramref name="list"/> to
        /// a <see cref="List{T}"/> or an array first where possible.
        /// </remarks>
        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public static IEnumerator<T> GetReverseEnumerator<T>(this IList<T> list)
        {
#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

            if (list is List<T> listT)
                return new ListReverseEnumerable<T>(listT).GetEnumerator();

            return new ReverseEnumerable<T>(list).GetEnumerator();

#pragma warning restore HAA0601
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
            return new ReverseEnumerable<T>(source);
        }

        /// <param name="source">The current collection.</param>
        /// <param name="throwOnCollectionModified">
        /// Indicates whether an exception should be thrown if the collection is modified during iteration.
        /// </param>
        /// <inheritdoc cref="InReverseOrder{T}(IList{T})"/>
        public static ListReverseEnumerable<T> InReverseOrder<T>(this List<T> source, bool throwOnCollectionModified = true)
        {
            return new ListReverseEnumerable<T>(source)
            {
                SuppressThrowOnCollectionModified = !throwOnCollectionModified
            };
        }
    }
}
