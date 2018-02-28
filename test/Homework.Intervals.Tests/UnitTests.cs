using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Homework.Intervals.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_BasicIntervalIntersection()
        {
            Interval[] expected = new Interval[] { new Interval(0, 40), new Interval(60, 100) };
            Interval[] cut = new Interval[] { new Interval(40, 60) };
            Interval master = new Interval(0, 100);

            Interval[] result = IntervalCalculator.CalculateMissingCoverage(master, cut);

            AssertIntervalArrayEquality(expected, result);
        }
        
        [TestMethod]
        public void Test_BasicIntervalIntersectionTwo()
        {
            Interval[] expected = new Interval[] { new Interval(0, 40), new Interval(45, 55), new Interval(60,100) };
            Interval[] cut = new Interval[] { new Interval(40, 45), new Interval(55, 60) };
            Interval master = new Interval(0, 100);

            Interval[] result = IntervalCalculator.CalculateMissingCoverage(master, cut);

            AssertIntervalArrayEquality(expected, result);
        }

        [TestMethod]
        public void Test_BasicIntervalIntersectionTwo_PreceedingDiscard()
        {
            Interval[] expected = new Interval[] { new Interval(0, 40), new Interval(45, 55), new Interval(60, 100) };
            Interval[] cut = new Interval[] {new Interval(-100, -50), new Interval(40, 45), new Interval(55, 60) };
            Interval master = new Interval(0, 100);

            Interval[] result = IntervalCalculator.CalculateMissingCoverage(master, cut);

            AssertIntervalArrayEquality(expected, result);
        }


        [TestMethod]
        public void Test_BasicIntervalIntersectionTwo_SuffixDiscard()
        {
            Interval[] expected = new Interval[] { new Interval(0, 40), new Interval(45, 55), new Interval(60, 100) };
            Interval[] cut = new Interval[] { new Interval(200, 300), new Interval(40, 45), new Interval(55, 60) };
            Interval master = new Interval(0, 100);

            Interval[] result = IntervalCalculator.CalculateMissingCoverage(master, cut);

            AssertIntervalArrayEquality(expected, result);
        }

        [TestMethod]
        public void Test_BasicIntervalIntersection_MiddleSubIntersection()
        {
            Interval[] expected = new Interval[] { new Interval(0, 40), new Interval(45, 55), new Interval(66, 100) };
            Interval[] cut = new Interval[] { new Interval(40, 45), new Interval(55, 60), new Interval(58, 66) };
            Interval master = new Interval(0, 100);

            Interval[] result = IntervalCalculator.CalculateMissingCoverage(master, cut);

            AssertIntervalArrayEquality(expected, result);
        }

        [TestMethod]
        public void Test_Giant()
        {
            Interval[] expected = new Interval[0];
            Interval[] cut = new Interval[] { new Interval(-10002, 45234), new Interval(55, 60) };
            Interval master = new Interval(0, 100);

            Interval[] result = IntervalCalculator.CalculateMissingCoverage(master, cut);

            AssertIntervalArrayEquality(expected, result);
        }

        private static void AssertIntervalArrayEquality(Interval[] expected, Interval[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Arrays are not equal in length");

            // equal in length
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], $"Values are not equal at {i}");
            }
        }
    }


}
