using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyComparer.UnitTests
{

    [TestClass]
    public class MyComparerAndUnitTest
    {
        public TestContext TestContext { get; set; }
        public MyComparer<(int, int, int)> ComparerOn { get; set; }
        public (int, int, int) Tuple1 { get; set; }
        public (int, int, int) Tuple2 { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            ComparerOn = MyComparer<(int, int, int)>.On(t => t.Item1);

            Tuple1 = (1, 2, 3);
            Tuple2 = (1, 2, 4);

            TestContext.WriteLine("initialize");
        }
        [TestMethod]
        public void TestEqualsWillReturnTrue()
        {
            IEqualityComparer<(int, int, int)> comparer = ComparerOn.And(t => t.Item2);
            
            Assert.IsTrue(comparer.Equals(Tuple1, Tuple2));
        }

        [TestMethod]
        public void TestEqualsWillReturnFalse()
        {
            IEqualityComparer<(int, int, int)> comparer = ComparerOn.And(t => t.Item3);
            Assert.IsFalse(comparer.Equals(Tuple1, Tuple2));
        }

        [TestMethod]
        public void TestGetHashcodeReturnsInt32()
        {
            IEqualityComparer<(int, int, int)> comparer = ComparerOn.And(t => t.Item2);
            Assert.That.IsOfType<int>(comparer.GetHashCode(Tuple1));
        }

        [TestMethod]
        public void TestGetHashcodeReturnsDifferentValuesForDifferentInputs()
        {
            IEqualityComparer<(int, int, int)> comparer = ComparerOn.And(t => t.Item3);
            Assert.AreNotEqual(comparer.GetHashCode(Tuple1), comparer.GetHashCode(Tuple2));
        }

        [TestMethod]
        public void TestGetHashcodeReturnsSameValuesForSameInputs()
        {
            IEqualityComparer<(int, int, int)> comparer = ComparerOn.And(t => t.Item2);
            Assert.AreEqual(comparer.GetHashCode(Tuple1), comparer.GetHashCode(Tuple2));
        }

    }
}
