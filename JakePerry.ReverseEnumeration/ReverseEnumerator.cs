using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// Enumerates the elements of a list in reverse order.
    /// </summary>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public struct ReverseEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
    {
        // The list to enumerate
        private readonly ListProxy<T> m_list;

        private int m_oneMoreThanIndex;
        private T m_current;

        public T Current => m_current;

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        object IEnumerator.Current => m_current;

#pragma warning restore HAA0601

        internal ReverseEnumerator(ListProxy<T> list)
        {
            m_list = list;

            m_oneMoreThanIndex = list.Count;
            m_current = default;
        }

        public ReverseEnumerator(IList<T> list) : this(new ListProxy<T>(list)) { }

        public ReverseEnumerator(IReadOnlyList<T> list) : this(new ListProxy<T>(list)) { }

        public ReverseEnumerator(List<T> list) : this(new ListProxy<T>(list)) { }

        public ReverseEnumerator(T[] list) : this(new ListProxy<T>(list)) { }

        public bool MoveNext()
        {
            if ((uint)m_oneMoreThanIndex > 0)
            {
                int index = --m_oneMoreThanIndex;
                m_current = m_list[index];

                return true;
            }

            m_oneMoreThanIndex = 0;
            m_current = default;

            return false;
        }

        public void Dispose() { /* Do nothing. */ }

        internal void Reset()
        {
            m_oneMoreThanIndex = m_list.Count;
            m_current = default;
        }

        void IEnumerator.Reset()
        {
            Reset();
        }
    }
}
