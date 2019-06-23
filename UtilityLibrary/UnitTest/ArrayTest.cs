using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibrary;
using static UtilityLibrary.IntegerEnumerable;

namespace UnitTest
{
    [TestClass]
    public class ArrayTest
    {
        [TestMethod]
        public void Dim1Test()
        {
            var ar = new ShiftedArray1<int>(-2, 3);
            ar[-2] = 0;
            ar[-1] = 2;
            ar[0] = 4;
            ar[1] = 1;
            ar[2] = 3;

            Assert.IsTrue(Algorithm.Equal(ar, new int[] { 0, 2, 4, 1, 3 }));
            Assert.AreEqual(ar.Length, 5);
            Assert.AreEqual(ar.LongLength, 5L);
            Assert.AreEqual(ar.Rank, 1);

            ar.Sort();
            Assert.IsTrue(Algorithm.Equal(ar, new int[] { 0, 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void Dim2Test()
        {
            var ar = new ShiftedArray2<int>((-1, 2), (-1, 2));
            var val = 0;
            foreach(var i in Range(-1, 2))
            {
                foreach(var j in Range(-1, 2))
                {
                    ar[i, j] = ++val;
                }
            }

            Assert.IsTrue(Algorithm.Equal(ar, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            Assert.AreEqual(ar.Dim0Length, 3);
            Assert.AreEqual(ar.Dim1Length, 3);
            Assert.AreEqual(ar.Size, 9);
            Assert.AreEqual(ar.Rank, 2);
            Assert.AreEqual(ar[-1, -1], 1);
            Assert.AreEqual(ar[0, 0], 5);
            Assert.AreEqual(ar[1, 1], 9);
        }
    }
}
