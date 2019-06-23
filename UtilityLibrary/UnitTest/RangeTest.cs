using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UtilityLibrary.IntegerEnumerable;
using static UtilityLibrary.Algorithm;

namespace UnitTest
{
    [TestClass]
    public class RangeTest
    {
        [TestMethod]
        public void IntRange1()
        {
            var rng = Range(5);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 0);
            Assert.AreEqual(rng.MaxValue, 5);
            Assert.IsTrue(Equal(rng, new int[] { 0, 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void IntRange2Plus()
        {
            var rng = Range(2, 7);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 2);
            Assert.AreEqual(rng.MaxValue, 7);
            Assert.IsTrue(Equal(rng, new int[] { 2, 3, 4, 5, 6 }));
        }

        [TestMethod]
        public void IntRange2Minus()
        {
            var rng = Range(7, 2);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 2);
            Assert.AreEqual(rng.MaxValue, 7);
            Assert.IsTrue(Equal(rng, new int[] { 7, 6, 5, 4, 3 }));
        }

        [TestMethod]
        public void IntRange3Plus()
        {
            var rng = Range(1, 10, 2);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 1);
            Assert.AreEqual(rng.MaxValue, 10);
            Assert.IsTrue(Equal(rng, new int[] { 1, 3, 5, 7, 9 }));
        }

        [TestMethod]
        public void IntRange3Minus()
        {
            var rng = Range(10, 1, -2);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 1);
            Assert.AreEqual(rng.MaxValue, 10);
            Assert.IsTrue(Equal(rng, new int[] { 10, 8, 6, 4, 2 }));
        }

        [TestMethod]
        public void LongRange1()
        {
            var rng = Range(5L);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 0L);
            Assert.AreEqual(rng.MaxValue, 5L);
            Assert.IsTrue(Equal(rng, new long[] { 0, 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void LongRange2Plus()
        {
            var rng = Range(2L, 7L);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 2L);
            Assert.AreEqual(rng.MaxValue, 7L);
            Assert.IsTrue(Equal(rng, new long[] { 2, 3, 4, 5, 6 }));
        }

        [TestMethod]
        public void LongRange2Minus()
        {
            var rng = Range(7L, 2L);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 2L);
            Assert.AreEqual(rng.MaxValue, 7L);
            Assert.IsTrue(Equal(rng, new long[] { 7, 6, 5, 4, 3 }));
        }

        [TestMethod]
        public void LongRange3Plus()
        {
            var rng = Range(1L, 10L, 2L);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 1L);
            Assert.AreEqual(rng.MaxValue, 10L);
            Assert.IsTrue(Equal(rng, new long[] { 1, 3, 5, 7, 9 }));
        }

        [TestMethod]
        public void LongRange3Minus()
        {
            var rng = Range(10L, 1L, -2L);
            Assert.AreEqual(rng.Count(), 5);
            Assert.AreEqual(rng.LongCount(), 5L);
            Assert.AreEqual(rng.MinValue, 1L);
            Assert.AreEqual(rng.MaxValue, 10L);
            Assert.IsTrue(Equal(rng, new long[] { 10, 8, 6, 4, 2 }));
        }
    }
}
