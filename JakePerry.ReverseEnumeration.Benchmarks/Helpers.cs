using System;
using System.Collections.Generic;

namespace JakePerry.ReverseEnumeration.Benchmarks
{
    public static class Helpers
    {
        public const int kCollectionSize = 4096;

        public static List<object> CreateList(int count)
        {
            var list = new List<object>(count);
            for (int i = 0; i < count; i++)
                list.Add(new object());
            list.TrimExcess();

            return list;
        }

        public static ArraySegment<object> CreateArraySegment(int count)
        {
            var array = new object[count];
            for (int i = 0; i < count; i++)
                array[i] = new object();

            return new ArraySegment<object>(array, 0, count);
        }
    }
}
