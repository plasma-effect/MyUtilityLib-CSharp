using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibrary;

namespace UnitTest
{
    [TestClass]
    public class AlgorithmTest
    {
        [TestMethod]
        public void EqualTest()
        {
            var a = new int[] { 0, 1, 2 };
            var b = new int[] { 0, 1, 2 };
            var c = new int[] { 0, 1, 2, 3 };
            var d = new int[] { 0, 1, 3 };
            Assert.IsTrue(RangeAlgorithm.Equal(a, b));
            Assert.IsFalse(RangeAlgorithm.Equal(a, c));
            Assert.IsFalse(RangeAlgorithm.Equal(a, d));
            Assert.IsFalse(RangeAlgorithm.Equal(c, a));
            Assert.IsFalse(RangeAlgorithm.Equal(c, d));
        }
    }
}
