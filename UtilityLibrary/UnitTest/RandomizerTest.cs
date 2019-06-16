using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibrary;
using static UtilityLibrary.IntegerEnumerable;

namespace UnitTest
{
    [TestClass]
    public class RandomizerTest
    {
        [TestMethod]
        public void ClassTest()
        {
            var data = new string[] { "a", "b", "c", "d" };
            var random = new RandomizerSet<string>(data);
            foreach (var i in Range(4))
            {
                var s = random.Pop();
                Assert.IsTrue(data.Contains(s));
            }
            Assert.AreEqual(random.Count, 0);
            foreach(var d in data)
            {
                random.Add(d);
            }
            Assert.AreEqual(random.Count, 4);
            random.Add("e");
            var data2 = Range(5).Select(_ => random.Pop()).ToArray();
            Assert.IsTrue(data2.Contains("e"));
            Assert.AreEqual(random.Count, 0);
        }

        [TestMethod]
        public void StructTest()
        {
            var data = new int[] { 2, 4, 6, 8 };
            var random = new RandomizerSet<int>(data);
            foreach (var i in Range(4))
            {
                var s = random.Pop();
                Assert.IsTrue(data.Contains(s));
            }
            Assert.AreEqual(random.Count, 0);
            foreach (var d in data)
            {
                random.Add(d);
            }
            Assert.AreEqual(random.Count, 4);
            random.Add(10);
            var data2 = Range(5).Select(_ => random.Pop()).ToArray();
            Assert.AreEqual(data2.Sum(), 30);
            Assert.AreEqual(random.Count, 0);
        }
    }
}
