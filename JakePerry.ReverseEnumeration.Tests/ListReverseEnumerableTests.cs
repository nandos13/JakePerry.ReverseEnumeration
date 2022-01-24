using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using static JakePerry.ReverseEnumeration.Tests.Helpers;

namespace JakePerry.ReverseEnumeration.Tests
{
    [TestClass]
    public class ImmutableListReverseEnumerableTests
    {
        [TestMethod("Empty enumerable")]
        public void TestMethod_EmptyDoesNotEnumerate()
        {
            var reverseEnumerable = new ImmutableListReverseEnumerable<string>(new List<string>());

            foreach (var e in reverseEnumerable)
            {
                // This code should not be reached
                Assert.Fail();
            }
        }

        [TestMethod("Empty enumerator")]
        public void TestMethod_EmptyDoesNotMoveNext()
        {
            var enumerator = new ImmutableListReverseEnumerator<string>(new List<string>());
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod("Order reversed")]
        public void TestMethod_OrderReversed()
        {
            var enumerator = new ImmutableListReverseEnumerator<string>(new List<string>(GetAbcArray()));

            // Current should return "c", "b", "a" in that order
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("c", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("b", enumerator.Current);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("a", enumerator.Current);

            // MoveNext should now return false because all elements have been iterated
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod("Foreach order reversed")]
        public void TestMethod_ForeachOrderReversed()
        {
            var sourceArray = GetAbcArray();
            var reverseEnumerable = new ImmutableListReverseEnumerable<string>(new List<string>(sourceArray));

            int i = sourceArray.Length - 1;
            foreach (var obj in reverseEnumerable)
            {
                Assert.AreEqual(sourceArray[i--], obj);
            }
        }

        [TestMethod("Indexer reversed")]
        public void TestMethod_Indexer()
        {
            var source = new List<string>(GetAbcArray());
            var reverseEnumerable = new ImmutableListReverseEnumerable<string>(source);

            Assert.AreEqual(source.Count, reverseEnumerable.Count);

            for (int i = 0; i < source.Count; i++)
                Assert.AreEqual(source[i], reverseEnumerable[source.Count - 1 - i]);
        }

        [TestMethod("Indexer throws - lower bound")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Indexer did not throw an exception when index is invalid.")]
        public void TestMethod_IndexerLowerBoundThrows()
        {
            var source = new List<string>(GetAbcArray());
            var reverseEnumerable = new ImmutableListReverseEnumerable<string>(source);

            string value = reverseEnumerable[-1];
            Assert.Fail("Accessing invalid index (less than zero) did not throw an exception.");
        }

        [TestMethod("Indexer throws - upper bound")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Indexer did not throw an exception when index is invalid.")]
        public void TestMethod_IndexerUpperBoundThrows()
        {
            var source = new List<string>(GetAbcArray());
            var reverseEnumerable = new ImmutableListReverseEnumerable<string>(source);

            string value = reverseEnumerable[source.Count];
            Assert.Fail("Accessing invalid index (greater than last index) did not throw an exception.");
        }

        [TestMethod("Exception on modified during iteration")]
        [ExpectedException(typeof(InvalidOperationException), "Enumerator did not throw an exception after the collection was modified.")]
        public void TestMethod_ExceptionOnCollectionModified()
        {
            var list = new List<string>(GetAbcArray());
            var reverseEnumerable = new ImmutableListReverseEnumerable<string>(list);

            foreach (var obj in reverseEnumerable)
            {
                // Modify the collection while it's being iterated
                list.Add("new value");
            }
        }

        [TestMethod("Exception on modified after iteration")]
        [ExpectedException(typeof(InvalidOperationException), "Enumerator did not throw an exception after the collection was modified.")]
        public void TestMethod_ExceptionOnCollectionModifiedAtEnd()
        {
            var list = new List<string>(GetAbcArray());
            var reverseEnumerable = new ImmutableListReverseEnumerable<string>(list);

            var firstElement = list[0];

            foreach (var obj in reverseEnumerable)
            {
                // Detect first element in list (last element in reverse order).
                if (obj == firstElement)
                {
                    // Modify the list after last element has been iterated.
                    list.Add("new value");
                }
            }
        }

        [TestMethod("Exception on modified and reset")]
        [ExpectedException(typeof(InvalidOperationException), "Enumerator did not throw an exception after the collection was modified.")]
        public void TestMethod_ExceptionOnResetModified()
        {
            var list = new List<string>(GetAbcArray());
            var reverseEnumerator = new ImmutableListReverseEnumerable<string>(list).GetEnumerator();

            try
            {
                reverseEnumerator.Reset();
            }
            catch (Exception e) { Assert.Fail($"Exception of type {e.GetType()} thrown on Reset invocation before collection was modified."); }

            list.Add("new value");

            reverseEnumerator.Reset();
        }
    }
}
