using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibrary;

namespace UnitTest
{
    [TestClass]
    public class StringViewTest
    {
        [TestMethod]
        public void AllTest()
        {
            var view = new StringView("12345");
            var sub0 = view.Substring(1, 3);
            var sub1 = view.Substring(2, 3);
            Assert.IsTrue(sub0.Contains(sub0));
            Assert.IsFalse(sub0.Contains(sub1));
            Assert.IsTrue(sub0.Contains("23"));
            Assert.IsFalse(sub0.Contains("45"));
            Assert.IsTrue(sub0.Equals(view.Substring(1, 3)));
            Assert.IsFalse(sub0.Equals(sub1));
            Assert.IsTrue(sub0.Equals("234"));
            Assert.IsFalse(sub0.Equals("345"));

            Assert.IsTrue(Algorithm.Equal(sub0, "234"));
            Assert.IsFalse(Algorithm.Equal(sub0, "2345"));
        }
    }
}
