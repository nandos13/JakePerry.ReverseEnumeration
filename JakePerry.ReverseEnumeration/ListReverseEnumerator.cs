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
        private readonly ReverseEnumerator<T> m_reverseEnumerator;

        // An enumerator belonging to m_list, created in the constructor. Because 'List<T>._version'
        // is not publicly accessible, this enumerator is used to maintain functionality that throws
        // an exception if the List<T> is modified during enumeration.
        private readonly List<T>.Enumerator m_listEnumerator;

        public T Current => m_reverseEnumerator.Current;

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        object IEnumerator.Current => m_reverseEnumerator.Current;

#pragma warning restore HAA0601

        /// <exception cref="InvalidOperationException">
        /// Thrown if the collection has been modified during enumeration.
        /// </exception>
        private void PerformVersionCheck()
        {
            // List<T>.Enumerator.MoveNext() will perform a version check and throw an InvalidOperationException
            // if the collection has been modified since the enumerator was created.
            m_listEnumerator.MoveNext();
        }

        public ListReverseEnumerator(List<T> list)
        {
            if (list is null)
            {
                this = default;
                return;
            }

            m_reverseEnumerator = new ReverseEnumerator<T>(list);
            m_listEnumerator = list.GetEnumerator();
        }

        public bool MoveNext()
        {
            PerformVersionCheck();

            return m_reverseEnumerator.MoveNext();
        }

        public void Dispose() { /* Do nothing. */ }

        void IEnumerator.Reset()
        {
            PerformVersionCheck();

            m_reverseEnumerator.Reset();
        }
    }
}
