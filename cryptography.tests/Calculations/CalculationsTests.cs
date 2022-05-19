using NUnit.Framework;
using static cryptography.Calculations.Calculations;

namespace cryptography.tests.Calculations
{
    public static class CalculationsTests
    {
        
        [Test]
        [TestCase(1358, 8, 6)]
        [TestCase(2985984, 15, 9)]
        [TestCase(308915776, 90, 46)]
        [TestCase(16, 8, 0)]
        public static void modTests(int inputValue, int modValue, int expectedResult)
        {
            //Act
            var actualResult = mod(inputValue, modValue);
           
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        [TestCase(55, 22, 11)]
        [TestCase(30, 21, 3)]
        [TestCase(18, 150, 6)]
        public static void gcdTests(int inputValue1, int inputValue2, int expectedResult)
        {
            //Act
            var actualResult = gcd(inputValue1, inputValue2);
           
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        [TestCase(10, 4)]
        [TestCase(11, 10)]
        public static void eulersTotientTests(int inputValue, int expectedResult)
        {
            //Act
            var actualResult = eulersTotientFunction(inputValue);
           
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}