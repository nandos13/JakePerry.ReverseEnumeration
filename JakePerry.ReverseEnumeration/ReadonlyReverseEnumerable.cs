using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// An enumerable that wraps an <see cref="IReadOnlyList{T}"/> to be enumerated in reverse order.
    /// </summary>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public readonly struct ReadonlyReverseEnumerable<T> :
        IEnumerable,
        IEnumerable<T>,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
        IEquatable<ReadonlyReverseEnumerable<T>>
    {
        private readonly IReadOnlyList<T> m_list;

        public int Count => m_list?.Count ?? 0;

        public T this[int index] => m_list is null ? default : m_list[m_list.Count - 1 - index];

        public ReadonlyReverseEnumerable(IReadOnlyList<T> list)
        {
            m_list = list;
        }

        public ReadonlyReverseEnumerable(List<T> list) : this((IReadOnlyList<T>)list) { }

        public ReadonlyReverseEnumerable(T[] list) : this((IReadOnlyList<T>)list) { }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public ReadonlyReverseEnumerator<T> GetEnumerator()
        {
            return new ReadonlyReverseEnumerator<T>(m_list);
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

#pragma warning restore HAA0601

        public bool Equals(ReadonlyReverseEnumerable<T> other)
        {
            return m_list == other.m_list;
        }

        public override bool Equals(object obj)
        {
            return (obj is ReadonlyReverseEnumerable<T> other) && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_list?.GetHashCode() ?? -1;
        }

        public static bool operator ==(ReadonlyReverseEnumerable<T> left, ReadonlyReverseEnumerable<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ReadonlyReverseEnumerable<T> left, ReadonlyReverseEnumerable<T> right)
        {
            return !(left == right);
        }
    }
}
