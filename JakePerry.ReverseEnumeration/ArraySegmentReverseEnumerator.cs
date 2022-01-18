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

        private int m_oneMoreThanIndex;
        private T m_current;

        public T Current => m_current;

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        object IEnumerator.Current => m_current;

#pragma warning restore HAA0601

        public ArraySegmentReverseEnumerator(ArraySegment<T> arraySegment)
        {
            m_arraySegment = arraySegment;

            m_oneMoreThanIndex = arraySegment.Count;
            m_current = default;
        }

        public bool MoveNext()
        {
            if (m_oneMoreThanIndex > 0)
            {
                int index = --m_oneMoreThanIndex;
                index += m_arraySegment.Offset;
                m_current = m_arraySegment.Array[index];

                return true;
            }

            m_oneMoreThanIndex = 0;
            m_current = default;

            return false;
        }

        public void Dispose() { /* Do nothing. */ }

        public void Reset()
        {
            m_oneMoreThanIndex = m_arraySegment.Count;
            m_current = default;
        }
    }
}
