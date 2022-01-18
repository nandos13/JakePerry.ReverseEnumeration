using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// An enumerable that wraps an <see cref="IList{T}"/> to be enumerated in reverse order.
    /// </summary>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public readonly struct ReverseEnumerable<T> :
        IEnumerable,
        IEnumerable<T>,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
        IEquatable<ReverseEnumerable<T>>
    {
        private readonly ListProxy<T> m_list;

        public int Count => m_list.Count;

        public T this[int index] => m_list[m_list.Count - 1 - index];

        internal ReverseEnumerable(ListProxy<T> list)
        {
            m_list = list;
        }

        public ReverseEnumerable(IList<T> list) : this(new ListProxy<T>(list)) { }

        public ReverseEnumerable(IReadOnlyList<T> list) : this(new ListProxy<T>(list)) { }

        public ReverseEnumerable(List<T> list) : this((IReadOnlyList<T>)list) { }

        public ReverseEnumerable(T[] list) : this((IReadOnlyList<T>)list) { }

        /// <returns>
        /// An enumerator that iterates through the collection in reverse order.
        /// </returns>
        public ReverseEnumerator<T> GetEnumerator()
        {
            return new ReverseEnumerator<T>(m_list);
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

#pragma warning restore HAA0601

        public bool Equals(ReverseEnumerable<T> other)
        {
            return m_list == other.m_list;
        }

        public override bool Equals(object obj)
        {
            return (obj is ReverseEnumerable<T> other) && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_list.GetHashCode();
        }

        public static bool operator ==(ReverseEnumerable<T> left, ReverseEnumerable<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ReverseEnumerable<T> left, ReverseEnumerable<T> right)
        {
            return !(left == right);
        }
    }
}
