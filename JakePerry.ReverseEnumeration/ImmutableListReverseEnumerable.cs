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
    /// collection is modified. Use the <see cref="AsMutable"/> method to convert to an enumerable
    /// that does not perform these checks.
    /// </remarks>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public readonly struct ImmutableListReverseEnumerable<T> :
        IEnumerable,
        IEnumerable<T>,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
        IEquatable<ImmutableListReverseEnumerable<T>>
    {
        private readonly List<T> m_list;

        public int Count => m_list?.Count ?? 0;

        public T this[int index]
        {
            get
            {
                if (m_list is null)
                    throw new InvalidOperationException("Can't use indexer; list has not been initialized.");
                return m_list[m_list.Count - 1 - index];
            }
        }

        public ImmutableListReverseEnumerable(List<T> list)
        {
            m_list = list;
        }

        /// <summary>
        /// Convert this enumerable to one that will not throw an exception if the list
        /// is modified during enumeration.
        /// </summary>
        public ReverseEnumerable<T> AsMutable()
        {
            return new ReverseEnumerable<T>(m_list);
        }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator"/>
        public ImmutableListReverseEnumerator<T> GetEnumerator()
        {
            return new ImmutableListReverseEnumerator<T>(m_list);
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

#pragma warning restore HAA0601

        public bool Equals(ImmutableListReverseEnumerable<T> other)
        {
            return m_list == other.m_list;
        }

        public override bool Equals(object obj)
        {
            return (obj is ImmutableListReverseEnumerable<T> other) && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_list?.GetHashCode() ?? -1;
        }

        public static explicit operator ReverseEnumerable<T>(ImmutableListReverseEnumerable<T> src)
        {
            return src.AsMutable();
        }

        public static bool operator ==(ImmutableListReverseEnumerable<T> left, ImmutableListReverseEnumerable<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ImmutableListReverseEnumerable<T> left, ImmutableListReverseEnumerable<T> right)
        {
            return !(left == right);
        }
    }
}
