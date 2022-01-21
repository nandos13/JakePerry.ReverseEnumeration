﻿using System;
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
        private readonly IList<T> m_list;

        public int Count => m_list?.Count ?? 0;

        public T this[int index] => m_list is null ? default : m_list[m_list.Count - 1 - index];

        public ReverseEnumerable(IList<T> list)
        {
            m_list = list;
        }

        public ReverseEnumerable(List<T> list) : this((IList<T>)list) { }

        public ReverseEnumerable(T[] list) : this((IList<T>)list) { }

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
    }
}
