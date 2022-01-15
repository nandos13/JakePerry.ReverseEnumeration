using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// An enumerable that wraps a <see cref="List{T}"/> to be enumerated in reverse order.
    /// </summary>
    /// <remarks>
    /// By default, enumerating this object will use an enumerator that throws an exception if the
    /// collection is modified. Use the <see cref="WithoutModifiedChecks"/> method to convert to an enumerable
    /// that does not perform these checks.
    /// </remarks>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public readonly struct ListReverseEnumerable<T> :
        IEnumerable,
        IEnumerable<T>,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
        IEquatable<ListReverseEnumerable<T>>
    {
        private readonly List<T> m_list;

        public int Count => m_list?.Count ?? 0;

        public T this[int index]
        {
            get
            {
                if (m_list is null)
                    throw new InvalidOperationException("Can't use indexer; list has not been initialized.");
                return m_list[index];
            }
        }

        public ListReverseEnumerable(List<T> list)
        {
            m_list = list;
        }

        /// <summary>
        /// Convert this enumerable to one that will not throw an exception if the list
        /// is modified during enumeration.
        /// </summary>
        public ReverseEnumerable<T> WithoutModifiedChecks()
        {
            return new ReverseEnumerable<T>(m_list);
        }

        public ListReverseEnumerator<T> GetEnumerator()
        {
            return new ListReverseEnumerator<T>(m_list);
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

#pragma warning restore HAA0601

        public bool Equals(ListReverseEnumerable<T> other)
        {
            return m_list == other.m_list;
        }

        public override bool Equals(object obj)
        {
            return (obj is ListReverseEnumerable<T> other) && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_list?.GetHashCode() ?? -1;
        }

        public static explicit operator ReverseEnumerable<T>(ListReverseEnumerable<T> src)
        {
            return src.WithoutModifiedChecks();
        }

        public static bool operator ==(ListReverseEnumerable<T> left, ListReverseEnumerable<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ListReverseEnumerable<T> left, ListReverseEnumerable<T> right)
        {
            return !(left == right);
        }
    }
}
