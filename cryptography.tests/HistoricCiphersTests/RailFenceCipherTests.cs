using System.Collections.Generic;
using cryptography.HistoricCiphers;
using NUnit.Framework;

namespace cryptography.tests.HistoricCiphersTests
{
    public class RailFenceCipherTests
    {
        [Test]
        [TestCase(2, 4, true, 3)]
        [TestCase(4, 4, false,3)]
        [TestCase(3, 4, false,2)]
        [TestCase(1, 4, false,0)]
        [TestCase(0, 4, false,1)]
        public void getDepthCountTest(int depthCounter, int depth, bool movingDownRail, int expectedResult)
        {
            //Act
            var actualResult = RailFenceCipher.getDepthCount(depthCounter, depth, movingDownRail);

            //Assert
            Assert.AreEqual(expectedResult, actualResult );
        }
        
        [Test]
        [TestCase(2, 4, true, true)]
        [TestCase(4, 4, false, false)]
        [TestCase(3, 4, false, false)]
        [TestCase(0, 4, false, true)]
        public void getRailDirectionTest(int depthCounter, int depth, bool movingDownRail, bool expectedResult)
        {
            //Act
            var actualResult = RailFenceCipher.getRailDirection(depthCounter, depth, movingDownRail);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}