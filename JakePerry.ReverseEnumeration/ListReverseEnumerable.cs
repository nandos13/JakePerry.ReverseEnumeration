using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// A simple struct that acts as a wrapper for a list which is to be enumerated in reverse order.
    /// </summary>
    /// <typeparam name="T">List generic type.</typeparam>
    public readonly struct ListReverseEnumerable<T> : IEnumerable<T>, IEnumerable, IEquatable<ListReverseEnumerable<T>>
    {
        private readonly List<T> m_list;

        /// <summary>
        /// The <see cref="List{T}"/> targeted by this reversed enumerable.
        /// </summary>
        public List<T> Target => m_list;

        public ListReverseEnumerable(List<T> list)
        {
            m_list = list;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="List{T}"/> in reverse order.
        /// </summary>
        /// <returns>
        /// A <see cref="ListReverseEnumerator{T}"/> for the <see cref="List{T}"/>.
        /// </returns>
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
