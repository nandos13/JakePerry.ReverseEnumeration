using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using static JakePerry.ReverseEnumeration.Tests.Helpers;

namespace JakePerry.ReverseEnumeration.Tests
{
    [TestClass]
    public class ReadonlyReverseEnumerableTests
    {
        [TestMethod("Empty enumerable")]
        public void TestMethod_EmptyDoesNotEnumerate()
        {
            var reverseEnumerable = new ReadonlyReverseEnumerable<string>(new List<string>());

            foreach (var e in reverseEnumerable)
            {
                // This code should not be reached
                Assert.Fail();
            }
        }

        [TestMethod("Empty enumerator")]
        public void TestMethod_EmptyDoesNotMoveNext()
        {
            var enumerator = new ReadonlyReverseEnumerator<string>(new List<string>());
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod("Enumerator order reversed")]
        public void TestMethod_OrderReversed()
        {
            var enumerator = new ReadonlyReverseEnumerator<string>(new List<string>(GetAbcArray()));

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
            var reverseEnumerable = new ReadonlyReverseEnumerable<string>(new List<string>(sourceArray));

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
            var reverseEnumerable = new ReadonlyReverseEnumerable<string>(source);

            Assert.AreEqual(source.Count, reverseEnumerable.Count);

            for (int i = 0; i < source.Count; i++)
                Assert.AreEqual(source[i], reverseEnumerable[source.Count - 1 - i]);
        }

        [TestMethod("Indexer throws - lower bound")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Indexer did not throw an exception when index is invalid.")]
        public void TestMethod_IndexerLowerBoundThrows()
        {
            var source = new List<string>(GetAbcArray());
            var reverseEnumerable = new ReadonlyReverseEnumerable<string>(source);

            string value = reverseEnumerable[-1];
            Assert.Fail("Accessing invalid index (less than zero) did not throw an exception.");
        }

        [TestMethod("Indexer throws - upper bound")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Indexer did not throw an exception when index is invalid.")]
        public void TestMethod_IndexerUpperBoundThrows()
        {
            var source = new List<string>(GetAbcArray());
            var reverseEnumerable = new ReadonlyReverseEnumerable<string>(source);

            string value = reverseEnumerable[source.Count];
            Assert.Fail("Accessing invalid index (greater than last index) did not throw an exception.");
        }
    }
}
