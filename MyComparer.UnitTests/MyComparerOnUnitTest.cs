using System.Collections.Generic;
using System.Linq;
using MyComparer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;

namespace MyComparer.UnitTests
{
    [TestClass]
    public class MyComparerOnUnitTest
    {
        public TestContext TestContext { get; set; }
        public (int, int) Tuple1 { get; set; }
        public (int, int) Tuple2 { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Tuple1 = (1, 2);
            Tuple2 = (1, 3);
        }

        [TestMethod]
        public void TestEqualsWillReturnTrue()
        {
            IEqualityComparer<(int, int)> comparer = MyComparer<(int, int)>.On(t => t.Item1);

            Assert.IsTrue(comparer.Equals(Tuple1, Tuple2));
        }

        [TestMethod]
        public void TestEqualsWillReturnFalse()
        {
            IEqualityComparer<(int, int)> comparer = MyComparer<(int, int)>.On(t => t.Item2);

            Assert.IsFalse(comparer.Equals(Tuple1, Tuple2));
        }

        [TestMethod]
        public void TestGetHashcodeReturnsInt32()
        {
            IEqualityComparer<(int, int)> comparer = MyComparer<(int, int)>.On(t => t.Item1);
            Assert.That.IsOfType<int>(comparer.GetHashCode(Tuple1));
        }

        [TestMethod]
        public void TestGetHashcodeReturnsDifferentValuesForDifferentInputs()
        {
            IEqualityComparer<(int, int)> comparer = MyComparer<(int, int)>.On(t => t.Item2);
            Assert.AreNotEqual(comparer.GetHashCode(Tuple1), comparer.GetHashCode(Tuple2));
        }

        [TestMethod]
        public void TestGetHashcodeReturnsSameValuesForSameInputs()
        {
            IEqualityComparer<(int, int)> comparer = MyComparer<(int, int)>.On(t => t.Item1);
            Assert.AreEqual(comparer.GetHashCode(Tuple1), comparer.GetHashCode(Tuple2));
        }
    }
}
