using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JakePerry.ReverseEnumeration.Tests
{
    [TestClass]
    public class ReverseEnumerationTests
    {
        private List<string> GetAbcTestList()
        {
            return new List<string>() { "a", "b", "c" };
        }

        [TestMethod("Empty list enumerator does not iterate")]
        public void TestMethod_EmptyListDoesNotEnumerate()
        {
            var emptyList = new List<string>();

            foreach (var e in emptyList.InReverseOrder())
            {
                // This code should not be reached
                Assert.Fail();
            }
        }

        [TestMethod("Empty list enumerator does not MoveNext")]
        public void TestMethod_EmptyListDoesNotMoveNext()
        {
            var emptyList = new List<string>();
            var enumerator = emptyList.GetReverseEnumerator();

            // MoveNext should return false because there are no elements in the list
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod("GetReverseEnumerator order reversed")]
        public void TestMethod_EnumeratorOrderIsReversed()
        {
            var list = GetAbcTestList();
            var enumerator = list.GetReverseEnumerator();

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
        public void TestMethod_ForeachOrderIsReversed()
        {
            var list = GetAbcTestList();

            var extracted = new string[3];

            int i = 0;
            foreach (var obj in list.InReverseOrder())
            {
                extracted[i++] = obj;
            }

            // Check elements pulled out of reversed enumerable are in expected order
            Assert.AreEqual("c", extracted[0]);
            Assert.AreEqual("b", extracted[1]);
            Assert.AreEqual("a", extracted[2]);
        }

        [TestMethod("Exception throws on collection modified during iteration")]
        [ExpectedException(typeof(InvalidOperationException), "Enumerator did not throw an exception after the collection was modified.")]
        public void TestMethod_ExceptionOnCollectionModified()
        {
            var list = GetAbcTestList();

            foreach (var obj in list.InReverseOrder())
            {
                // Modify the collection while it's being iterated
                list.Add("new value");
            }

            Assert.Fail("Test code advanced past the foreach loop without experiencing an exception.");
        }

        [TestMethod("Exception throws on collection modified after iterating last element")]
        [ExpectedException(typeof(InvalidOperationException), "Enumerator did not throw an exception after the collection was modified.")]
        public void TestMethod_ExceptionOnCollectionModifiedAtEnd()
        {
            var element = new object();

            // Create list with only 1 element
            var list = new List<object>();
            list.Add(element);

            // Enumeration will iterate over the only existing element, then add a new one.
            foreach (var obj in list.InReverseOrder())
            {
                if (obj == element)
                {
                    // Modify the collection after iterating over the only element present at initial loop
                    list.Add(null);
                }
            }

            Assert.Fail("Test code advanced past the foreach loop without experiencing an exception.");
        }
    }
}
