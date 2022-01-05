using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// Enumerates the elements of a <see cref="List{T}"/> in reverse order.
    /// </summary>
    /// <typeparam name="T">List generic type.</typeparam>
    /// <remarks>
    /// Implementation notes:
    /// The default List.Enumerator will throw an exception if the collection is modified
    /// during enumeration. This struct maintains said functionality by accessing a default enumerator,
    /// as the version integer used by List is not publicly accessible.
    /// </remarks>
    /// <seealso cref="List{T}.Enumerator"/>
    public struct ListReverseEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
    {
        // The list to enumerate
        private readonly List<T> m_list;

        // An enumerator belonging to m_list, created in the constructor
        private readonly List<T>.Enumerator m_enumerator;

        private int m_oneMoreThanIndex;
        private T m_current;

        public T Current => m_current;

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        object IEnumerator.Current => m_current;

#pragma warning restore HAA0601

        public ListReverseEnumerator(List<T> list)
        {
            m_list = list;
            m_enumerator = list.GetEnumerator();

            m_oneMoreThanIndex = list.Count;
            m_current = default;
        }

        public bool MoveNext()
        {
            // Invoke MoveNext on default enumerator to throw an exception if collection is modified (see impl notes at class level).
            m_enumerator.MoveNext();

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

        void IEnumerator.Reset()
        {
            // List<T>.Enumerator.IEnumerator.Reset() implementation will throw an exception if the collection is modified
            // (see impl notes at class level).
            // Note: Since we have to cast the struct to IEnumerator to invoke this method, this will not mutate the value
            // stored in m_enumerator - however this is not an issue.
            ((IEnumerator)m_enumerator).Reset();

            m_oneMoreThanIndex = m_list.Count;
            m_current = default;
        }
    }
}
