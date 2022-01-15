using System;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// Unfortunately the <see cref="IList{T}"/> interface does not extend the <see cref="IReadOnlyList{T}"/>
    /// interface despite re-implementing all the same properties.
    /// This struct wraps an object of either type and exposes a Count and indexer property for internal use.
    /// </summary>
    /// <typeparam name="T">The collection's element type.</typeparam>
    internal readonly struct ListProxy<T> : IEquatable<ListProxy<T>>
    {
        private readonly IList<T> m_list;
        private readonly IReadOnlyList<T> m_readonlyList;

        /// <summary>
        /// Returns the list targeted by this object. The returned object's type will be one of
        /// <see cref="IList{T}"/> or <see cref="IReadOnlyList{T}"/>.
        /// Returns <see langword="null"/> if this object is uninitialized (default).
        /// </summary>
        internal object Target => (object)m_list ?? (object)m_readonlyList;

        public int Count => m_list?.Count ?? m_readonlyList?.Count ?? 0;

        public T this[int index]
        {
            get
            {
                if (m_list != null)
                    return m_list[index];

                if (m_readonlyList != null)
                    return m_readonlyList[index];

                throw new InvalidOperationException();
            }
        }

        public ListProxy(IList<T> list)
        {
            m_list = list;
            m_readonlyList = null;
        }

        public ListProxy(IReadOnlyList<T> list)
        {
            m_list = null;
            m_readonlyList = list;
        }

        public ListProxy(List<T> list) : this((IReadOnlyList<T>)list) { }

        public ListProxy(T[] list) : this((IReadOnlyList<T>)list) { }

        public bool Equals(ListProxy<T> other)
        {
            return ReferenceEquals(m_list, other.m_list)
                && ReferenceEquals(m_readonlyList, other.m_readonlyList);
        }

        public override bool Equals(object obj)
        {
            return obj is ListProxy<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_list?.GetHashCode() ?? m_readonlyList?.GetHashCode() ?? -1;
        }

        public static bool operator ==(ListProxy<T> left, ListProxy<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ListProxy<T> left, ListProxy<T> right)
        {
            return !(left == right);
        }
    }
}
