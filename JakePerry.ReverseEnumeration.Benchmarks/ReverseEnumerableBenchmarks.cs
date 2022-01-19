using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace JakePerry.ReverseEnumerable.Benchmarks
{
    [MemoryDiagnoser]
    public class ReverseEnumerableBenchmarks
    {
        private const int kCollectionSize = 64;

        private List<object> m_list;

        [GlobalSetup]
        public void Initialize()
        {
            m_list = new List<object>();
            for (int i = 0; i < kCollectionSize; i++)
                m_list.Add(new object());
            m_list.TrimExcess();
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
        public void ForeachLoop_List()
        {
            foreach (var obj in new ReverseEnumerable<object>(m_list))
            { }
        }

        [Benchmark]
        public void MoveNext_List()
        {
            var enumerator = new ReverseEnumerator<object>(m_list);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
            }
        }
    }
}
