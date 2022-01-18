using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// An enumerable that wraps an <see cref="ArraySegment{T}"/> to be enumerated in reverse order.
    /// </summary>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public readonly struct ArraySegmentReverseEnumerable<T> :
        IEnumerable,
        IEnumerable<T>,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
        IEquatable<ArraySegmentReverseEnumerable<T>>
    {
        private readonly ArraySegment<T> m_arraySegment;

        public int Count => m_arraySegment.Count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= m_arraySegment.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                var reverseIndex = m_arraySegment.Count - 1 - index + m_arraySegment.Offset;
                return m_arraySegment.Array[reverseIndex];
            }
        }

        public ArraySegmentReverseEnumerable(ArraySegment<T> arraySegment)
        {
            m_arraySegment = arraySegment;
        }

        /// <inheritdoc cref="ReverseEnumerable{T}.GetEnumerator"/>
        public ArraySegmentReverseEnumerator<T> GetEnumerator()
        {
            return new ArraySegmentReverseEnumerator<T>(m_arraySegment);
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

#pragma warning restore HAA0601

        public bool Equals(ArraySegmentReverseEnumerable<T> other)
        {
            return m_arraySegment == other.m_arraySegment;
        }

        public override bool Equals(object obj)
        {
            return (obj is ArraySegmentReverseEnumerable<T> other) && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_arraySegment.GetHashCode();
        }

        public static bool operator ==(ArraySegmentReverseEnumerable<T> left, ArraySegmentReverseEnumerable<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ArraySegmentReverseEnumerable<T> left, ArraySegmentReverseEnumerable<T> right)
        {
            return !(left == right);
        }
    }
}
