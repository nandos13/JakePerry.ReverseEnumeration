using BenchmarkDotNet.Attributes;
using System;

using static JakePerry.ReverseEnumeration.Benchmarks.Helpers;

namespace JakePerry.ReverseEnumeration.Benchmarks
{
    [MemoryDiagnoser]
    public class ArraySegmentBenchmarks
    {
        private ArraySegment<object> m_segment;

        [GlobalSetup]
        public void Initialize()
        {
            m_segment = CreateArraySegment(kCollectionSize);
        }

        [Benchmark]
        public void ForLoop()
        {
            int count = m_segment.Count;
            int offset = m_segment.Offset;
            for (int i = 0; i < count; i++)
            {
                var current = m_segment.Array[i + offset];
            }
        }

        [Benchmark]
        public void Reversed_ForeachLoop()
        {
            var reverseEnumerable = new ArraySegmentReverseEnumerable<object>(m_segment);
            foreach (var obj in reverseEnumerable)
            { }
        }
    }
}
