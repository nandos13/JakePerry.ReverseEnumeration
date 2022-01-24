using System;
using System.Collections;
using System.Collections.Generic;

namespace JakePerry
{
    /// <summary>
    /// An alternative to the <see cref="ReverseEnumerator{T}"/> type for use with <see cref="List{T}"/>.
    /// Just as the standard enumerator for <see cref="List{T}"/> does, this type guarantees the immutability
    /// of a list by checking if it has been modified during enumeration and throwing an exception if so.
    /// </summary>
    /// <seealso cref="List{T}.Enumerator"/>
    /// <inheritdoc cref="ReverseEnumerator{T}"/>
    public struct ImmutableListReverseEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
    {
        // An enumerator belonging to the enumerated list, created in the constructor. The 'List<T>._version' field
        // is not publicly accessible, so we use this enumerator as a hacky means to access it.
        // This is done by calling Reset() on this enumerator, which will throw an InvalidOperationException
        // if the list has been modified during enumeration.
        private readonly List<T>.Enumerator m_listEnumerator;

        // The list to enumerate
        private ReverseEnumerator<T> m_reverseEnumerator;

        // This type must cache the current value as part of the guarantee of immutability
        private T m_current;

        public T Current => m_current;

#pragma warning disable HAA0601 // Value type to reference type conversion causing boxing allocation

        object IEnumerator.Current => this.Current;

#pragma warning restore HAA0601

        // Optimization:
        // List<T>.Enumerator performs version check in MoveNext & Reset methods.
        // Unfortunately, the Reset method is only exposed via the IEnumerator interface,
        // so this generic method is used to cast the enumerator without boxing the object.
        // Benchmarking reveals this approach is ~15% faster than calling MoveNext().
        private static void CheckVersion<V>(V v)
            where V : struct, IEnumerator
        {
            v.Reset();
        }

        public ImmutableListReverseEnumerator(List<T> list)
        {
            if (list is null)
            {
                this = default;
                return;
            }

            m_reverseEnumerator = new ReverseEnumerator<T>(list);
            m_listEnumerator = list.GetEnumerator();
            m_current = default;
        }

        public bool MoveNext()
        {
            CheckVersion(m_listEnumerator);

            if (m_reverseEnumerator.MoveNext())
            {
                m_current = m_reverseEnumerator.Current;
                return true;
            }

            m_current = default;
            return false;
        }

        public void Dispose() { /* Do nothing. */ }

        public void Reset()
        {
            CheckVersion(m_listEnumerator);

            m_current = default;
            m_reverseEnumerator.Reset();
        }
    }
}
