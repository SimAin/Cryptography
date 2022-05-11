using System;
using System.Collections.Generic;
using System.Linq;
using cryptography.Services;
using NUnit.Framework;

namespace cryptography.tests.ServiceTests
{
    public class KeyGenerationServiceTests
    {
        /// <summary>
        /// Test to ensure random key generated is unique.
        /// A list of 5 random generated keys is used to test uniqueness.
        /// </summary>
        [Test]
        public void generateRandomKeyRandomTest()
        {
            //Arrange
            List<string> randomKeyList = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                var keyCharList = KeyGenerationService.generateRandomKey();
                var key = "";
                foreach (var c in keyCharList)
                {
                    key += c;
                }
                randomKeyList.Add(key);
            }
            
            //Act
            var randomKey = KeyGenerationService.generateRandomKey().ToString();
            var keyString = randomKey.Aggregate("", (current, c) => current + c);

            //Assert
            Assert.That(randomKeyList, Has.No.Member(keyString));
        }
        
        /// <summary>
        /// Test to ensure length of random key is 26 chars..
        /// </summary>
        [Test]
        public void generateRandomKeyLengthTest()
        {
            //Act
            var randomKey = KeyGenerationService.generateRandomKey();

            //Assert
            Assert.AreEqual(26, randomKey.Count);
        }
        
        [Test]
        [TestCase("phrase",  "phrase")]
        [TestCase("the lazy dog",  "thelazydog")]
        [TestCase("a well known phrase",  "awelknophrs")]
        public void generatePhraseKeyTest(string phrase, string expectedKey)
        {
            //Arrange
            var expectedResult = expectedKey.ToCharArray().ToList();
            
            //Act
            var randomKey = KeyGenerationService.generatePhraseKey(phrase, false);

            //Assert
            Assert.That(randomKey, Is.EquivalentTo(expectedResult));
        }
        
        [Test]
        [TestCase("phrase",  "phrasebcdfgijklmnoqtuvwxyz")]
        [TestCase("the lazy dog",  "thelazydogbcfijkmnpqrsuvwx")]
        [TestCase("a well known phrase",  "awelknophrsbcdfgijmqtuvxyz")]
        public void generatePhraseKeyFullAlphaTest(string phrase, string expectedKey)
        {
            //Arrange
            var expectedResult = expectedKey.ToCharArray().ToList();
            
            //Act
            var randomKey = KeyGenerationService.generatePhraseKey(phrase, true);

            //Assert
            Assert.That(randomKey, Is.EquivalentTo(expectedResult));
        }
    }
}