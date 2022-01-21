using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;

namespace JakePerry.ReverseEnumerable.Benchmarks
{
    [MemoryDiagnoser]
    public class ReverseEnumerableBenchmarks
    {
        private const int kCollectionSize = 16;

        private List<object> m_list;
        private ArraySegment<object> m_segment;

        private static List<object> CreateList(int count)
        {
            var list = new List<object>(count);
            for (int i = 0; i < count; i++)
                list.Add(new object());
            list.TrimExcess();

            return list;
        }

        private static ArraySegment<object> CreateArraySegment(int count)
        {
            var array = new object[count];
            for (int i = 0; i < count; i++)
                array[i] = new object();

            return new ArraySegment<object>(array, 0, count);
        }

        [GlobalSetup]
        public void Initialize()
        {
            m_list = CreateList(kCollectionSize);
            m_segment = CreateArraySegment(kCollectionSize);
        }

        [Benchmark]
        public void ForLoop_List()
        {
            int count = m_list.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                var current = m_list[i];
            }
        }

        [Benchmark]
        public void ReverseEnumerable()
        {
            var reverseEnumerable = new ReverseEnumerable<object>(m_list);
            foreach (var obj in reverseEnumerable)
            { }
        }

        [Benchmark]
        public void ReverseEnumerator()
        {
            var enumerator = new ReverseEnumerator<object>(m_list);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
            }
        }

        [Benchmark]
        public void ReadonlyReverseEnumerable()
        {
            var reverseEnumerable = new ReadonlyReverseEnumerable<object>(m_list);
            foreach (var obj in reverseEnumerable)
            { }
        }

        [Benchmark]
        public void ReadonlyReverseEnumerator()
        {
            var enumerator = new ReadonlyReverseEnumerator<object>(m_list);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
            }
        }

        [Benchmark]
        public void ListReverseEnumerable()
        {
            var reverseEnumerable = new ListReverseEnumerable<object>(m_list);
            foreach (var obj in reverseEnumerable)
            { }
        }

        [Benchmark]
        public void ListReverseEnumerator()
        {
            var enumerator = new ListReverseEnumerator<object>(m_list);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
            }
        }

        [Benchmark]
        public void ForLoop_ArraySegment()
        {
            int count = m_segment.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                var current = m_segment.Array[i + m_segment.Offset];
            }
        }

        [Benchmark]
        public void ArraySegmentReverseEnumerable()
        {
            var reverseEnumerable = new ArraySegmentReverseEnumerable<object>(m_segment);
            foreach (var obj in reverseEnumerable)
            { }
        }

        [Benchmark]
        public void ArraySegmentReverseEnumerator()
        {
            var enumerator = new ArraySegmentReverseEnumerator<object>(m_segment);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
            }
        }
    }
}
