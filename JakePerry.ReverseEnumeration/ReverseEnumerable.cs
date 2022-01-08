using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// A simple struct that acts as a wrapper for a collection which is to be enumerated in reverse order.
    /// </summary>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public readonly struct ReverseEnumerable<T> : IEnumerable<T>, IEnumerable, IEquatable<ReverseEnumerable<T>>
    {
        private readonly IList<T> m_list;

        /// <summary>
        /// The collection targeted by this reversed enumerable.
        /// </summary>
        public IList<T> Target => m_list;

        public ReverseEnumerable(IList<T> list)
        {
            m_list = list;
        }

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
            return m_list?.GetHashCode() ?? -1;
        }

        public static bool operator ==(ReverseEnumerable<T> left, ReverseEnumerable<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ReverseEnumerable<T> left, ReverseEnumerable<T> right)
        {
            return !(left == right);
        }

        public static ReverseEnumerable<T> Create(IList<T> list)
        {
            return new ReverseEnumerable<T>(list);
        }
    }
}
