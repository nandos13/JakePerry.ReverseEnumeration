using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <inheritdoc cref="ReverseEnumerable{T}"/>
    public struct ListReverseEnumerable<T> : IEnumerable<T>, IEnumerable, IEquatable<ListReverseEnumerable<T>>
    {
        private readonly List<T> m_list;

        /// <inheritdoc cref="ReverseEnumerable{T}.Target"/>
        public List<T> Target => m_list;

        public bool SuppressThrowOnCollectionModified { get; set; }

        public ListReverseEnumerable(List<T> list)
        {
            m_list = list;
            SuppressThrowOnCollectionModified = false;
        }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator()"/>
        public ListReverseEnumerator<T> GetEnumerator()
        {
            return new ListReverseEnumerator<T>(m_list)
            {
                SuppressThrowOnCollectionModified = SuppressThrowOnCollectionModified
            };
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
