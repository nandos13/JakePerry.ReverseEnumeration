using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// Enumerates the elements of an <see cref="IList{T}"/> in reverse order.
    /// </summary>
    /// <typeparam name="T">The collection's element type.</typeparam>
    public struct ReverseEnumerator<T> :
        IEnumerator,
        IEnumerator<T>,
        IDisposable
    {
        // The list to enumerate
        private readonly IList<T> m_list;

        private int m_index;

        public T Current
        {
            get
            {
                var index = m_index;

                if (index < 0 || m_list is null || index >= m_list.Count)
                    return default;

                return m_list[index];
            }
        }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        object IEnumerator.Current => this.Current;

#pragma warning restore HAA0601

        public ReverseEnumerator(IList<T> list)
        {
            m_list = list;
            m_index = list?.Count ?? 0;
        }

        public ReverseEnumerator(List<T> list) : this((IList<T>)list) { }

        public ReverseEnumerator(T[] list) : this((IList<T>)list) { }

        public bool MoveNext()
        {
            if (m_index > 0)
            {
                --m_index;
                return true;
            }

            m_index = 0;
            return false;
        }

        public void Dispose() { /* Do nothing. */ }

        public void Reset()
        {
            m_index = m_list.Count;
        }
    }
}
