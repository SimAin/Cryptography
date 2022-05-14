using System.Collections.Generic;
using System.Linq;
using System.Text;
using cryptography.HistoricCiphers;
using NUnit.Framework;

namespace cryptography.tests.HistoricCiphersTests
{
    public class RailFenceCipherTests
    {

        [Test]
        [TestCase("test string", 3, new string[] {"_..._..._..t i", "._._._._._.etsrn",".._..._..._stg"})]
        [TestCase("test string", 4, new string[] {"_....._....tt", "._..._._...esr",".._._..._._s ig","..._....._.tn"})]
        public void substituteValuesIntoRailStructureEncodeTest(string input, int depth, string[] expected)
        {
            //Arrange
            var charList = input.ToCharArray();
            var stringB = RailFenceCipher.createRailStructure(charList.Length, depth);
            
            
            //Act
            var railStructure = RailFenceCipher.substituteValuesIntoRailStructure(charList, stringB, depth, true);

            //Assert
            for (int i = 0; i < depth; i++)
            {
                Assert.AreEqual(expected[i], railStructure[i].ToString());
            }
        }
        
        [Test]
        [TestCase("test string", 3, new string[] {"t... ...i..", ".e.t.s.r.n.","..s...t...g"})]
        [TestCase("test string", 4, new string[] {"t.....t....", ".e...s.r...","..s. ...i.g","...t.....n."})]
        public void substituteValuesIntoRailStructureDecodeTest(string input, int depth, string[] expected)
        {
            //Arrange
            var charList = input.ToCharArray();
            var stringB = RailFenceCipher.createRailStructure(charList.Length, depth);
            
            
            //Act
            var railStructure = RailFenceCipher.substituteValuesIntoRailStructure(charList, stringB, depth, false);

            //Assert
            for (int i = 0; i < depth; i++)
            {
                Assert.AreEqual(expected[i], railStructure[i].ToString());
            }
        }
        
        [Test]
        [TestCase("Stshr etot",3)]
        [TestCase("Sshetotr t",5)]
        public void extractMessageTest(string input, int depth)
        {
            //Arrange
            var charList = input.ToCharArray();
            
            //Ripped from replaceValsD method to set up testing extractMessage
            var decryptedRailStructure = RailFenceCipher.createRailStructure(charList.Length,depth);

            var counter = 0;
            //Where a '_' is found, replace with corralling char from ciphertext. 
            foreach (var rail in decryptedRailStructure)
            {
                for (int i = 0; i < rail.Length; i++)
                {
                    if (rail[i] == '_')
                    {
                        rail.Remove(i, 1);
                        rail.Insert(i,charList[counter]);
                        counter++;
                    }
                }
            }
                
            //Act
            var actualResult = RailFenceCipher.extractMessage(decryptedRailStructure, depth);

            //Assert
            Assert.AreEqual("Short test", actualResult.ToString() );
        }
        
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