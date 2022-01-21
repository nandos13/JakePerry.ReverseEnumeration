using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// Enumerates the elements of the array delimited by an <see cref="ArraySegment{T}"/> in reverse order.
    /// </summary>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public struct ArraySegmentReverseEnumerator<T> :
        IEnumerator,
        IEnumerator<T>,
        IDisposable
    {
        private readonly ArraySegment<T> m_arraySegment;

        private int m_index;

        public T Current
        {
            get
            {
                var index = m_index;

                if (index < 0 || index >= m_arraySegment.Count)
                    return default;

                return m_arraySegment.Array[index + m_arraySegment.Offset];
            }
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        object IEnumerator.Current => this.Current;

#pragma warning restore HAA0601

        public ArraySegmentReverseEnumerator(ArraySegment<T> arraySegment)
        {
            m_arraySegment = arraySegment;

            m_index = arraySegment.Count;
        }

        public bool MoveNext()
        {
            if (m_index > 0)
            {
                --m_index;
                return true;
            }

            m_index = -1;
            return false;
        }

        public void Dispose() { /* Do nothing. */ }

        public void Reset()
        {
            m_index = m_arraySegment.Count;
        }
    }
}
