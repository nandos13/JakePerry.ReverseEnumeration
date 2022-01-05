using System.Collections.Generic;

namespace JakePerry
{
    public static class ReverseEnumerationExtensions
    {
        /// <inheritdoc cref="ListReverseEnumerable{T}.GetEnumerator()"/>
        public static ListReverseEnumerator<T> GetReverseEnumerator<T>(this List<T> list)
        {
            return new ListReverseEnumerable<T>(list).GetEnumerator();
        }

        /// <summary>
        /// Enumerate the list in reverse order.
        /// </summary>
        /// <typeparam name="T">List generic type.</typeparam>
        /// <param name="list">The current list.</param>
        /// <returns>
        /// An enumerable object that will enumerate the list in reverse order.
        /// </returns>
        /// <example>
        /// The following code:
        /// <code>
        /// var list = new List&lt;string&gt;() { "a", "b", "c" };
        /// foreach (var val in list.InReverseOrder())
        ///     Console.Write(val);
        /// </code>
        /// prints: <i>cba</i>
        /// </example>
        public static ListReverseEnumerable<T> InReverseOrder<T>(this List<T> list)
        {
            return new ListReverseEnumerable<T>(list);
        }
    }
}
