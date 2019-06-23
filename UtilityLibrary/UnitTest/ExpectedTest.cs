using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibrary;

namespace UnitTest
{
    [TestClass]
    public class ExpectedTest
    {
        [TestMethod]
        public void AllTest()
        {
            {
                var val = Expected.Success(42);
                Assert.IsTrue(val.TryGet(out var value));
                Assert.IsTrue(val);
                Assert.AreEqual(value, 42);
            }
            {
                var val = Expected.Failure<int>(new Exception("Test"));
                Assert.IsFalse(val.TryGet(out _));
                Assert.IsFalse(val);
                Assert.AreEqual(val.GetException().Message, "Test");
            }
            {
                Expected<int, Exception> val = default;
                Assert.IsFalse(val.TryGet(out _));
                Assert.IsFalse(val);
                Assert.IsTrue(val.GetException() is null);
            }
            {
                var val = Expected<NullReferenceException>.Success(42);
                Assert.IsTrue(val.TryGet(out var value));
                Assert.IsTrue(val);
                Assert.AreEqual(value, 42);
            }
            {
                var val = Expected<NullReferenceException>.Failure<int>(new NullReferenceException());
                Assert.IsFalse(val.TryGet(out _));
                Assert.IsFalse(val);
                Assert.IsTrue(val.GetException() is NullReferenceException);
            }
        }
    }
}
