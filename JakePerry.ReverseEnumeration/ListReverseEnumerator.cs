using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <remarks>
    /// Note: <see cref="ListReverseEnumerator{T}"/> will throw an exception if the collection is
    /// modified during enumeration, just as the default <see cref="List{T}.Enumerator"/> does.
    /// <para>
    /// If this is not desired, the list can be enumerated with a <see cref="ReverseEnumerator{T}"/>
    /// instead, or the <see cref="SuppressThrowOnCollectionModified"/> property can simply be set to <see langword="true"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="List{T}.Enumerator"/>
    /// <inheritdoc cref="ReverseEnumerator{T}"/>
    public struct ListReverseEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
    {
        // The list to enumerate
        private readonly List<T> m_list;

        // An enumerator belonging to m_list, created in the constructor. Because 'List<T>._version'
        // is not publicly accessible, this enumerator is used to maintain functionality that throws
        // an exception if the List<T> is modified during enumeration.
        private readonly List<T>.Enumerator m_enumerator;

        private int m_oneMoreThanIndex;
        private T m_current;

        public T Current => m_current;

        public bool SuppressThrowOnCollectionModified { get; set; }

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        object IEnumerator.Current => m_current;

#pragma warning restore HAA0601

        public ListReverseEnumerator(List<T> list)
        {
            m_list = list;
            m_enumerator = list.GetEnumerator();
            SuppressThrowOnCollectionModified = false;

            m_oneMoreThanIndex = list.Count;
            m_current = default;
        }

        public bool MoveNext()
        {
            if (!SuppressThrowOnCollectionModified)
            {
                // Invoke MoveNext on default enumerator to throw an exception if collection is modified (see m_enumerator comment).
                m_enumerator.MoveNext();
            }

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
            if (!SuppressThrowOnCollectionModified)
            {
                // List<T>.Enumerator.IEnumerator.Reset() implementation will throw an exception if the collection is modified
                // (see m_enumerator comment).
                // Note: Since we have to cast the struct to IEnumerator to invoke this method, this will not mutate the value
                // stored in m_enumerator - however this is not an issue.
                ((IEnumerator)m_enumerator).Reset();
            }

            m_oneMoreThanIndex = m_list.Count;
            m_current = default;
        }
    }
}
