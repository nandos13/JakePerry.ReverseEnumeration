using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

using static JakePerry.ReverseEnumeration.Benchmarks.Helpers;

namespace JakePerry.ReverseEnumeration.Benchmarks
{
    [MemoryDiagnoser]
    public class ListBenchmarks
    {
        private List<object> m_list;

        [GlobalSetup]
        public void Initialize()
        {
            m_list = CreateList(kCollectionSize);
        }

        [Benchmark]
        public void ForLoop()
        {
            int count = m_list.Count;
            for (int i = 0; i < count; i++)
            {
                var current = m_list[i];
            }
        }

        [Benchmark]
        public void Standard_ForeachLoop()
        {
            foreach (var obj in m_list)
            { }
        }

        [Benchmark]
        public void Reversed_ForeachLoop()
        {
            var reverseEnumerable = new ReverseEnumerable<object>(m_list);
            foreach (var obj in reverseEnumerable)
            { }
        }

        [Benchmark]
        public void Immutable_Reversed_ForeachLoop()
        {
            var reverseEnumerable = new ImmutableListReverseEnumerable<object>(m_list);
            foreach (var obj in reverseEnumerable)
            { }
        }
    }
}
