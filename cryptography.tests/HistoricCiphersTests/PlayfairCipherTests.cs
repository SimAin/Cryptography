using System.Collections.Generic;
using System.Text;
using cryptography.HistoricCiphers;
using NUnit.Framework;

namespace cryptography.tests.HistoricCiphersTests
{
    public class PlayfairCipherTests
    {
        
        /// <summary>
        /// Test prep message function.
        /// To test, message has duplicated letters, j and i's to test rules.
        /// </summary>
        [Test]
        public void prepMessageTest()
        {
            //Arrange 
            var inputValue = "A small message is just here to test the function.";
            var expectedResult = new List<KeyValuePair<char, char>>
            {
                new KeyValuePair<char, char>('a', 's'), 
                new KeyValuePair<char, char>('m', 'a'), 
                new KeyValuePair<char, char>('l', 'z'), 
                new KeyValuePair<char, char>('l', 'm'), 
                new KeyValuePair<char, char>('e', 's'),
                new KeyValuePair<char, char>('s', 'a'),
                new KeyValuePair<char, char>('g', 'e'),
                new KeyValuePair<char, char>('i', 's'),
                new KeyValuePair<char, char>('i', 'u'),
                new KeyValuePair<char, char>('s', 't'),
                new KeyValuePair<char, char>('h', 'e'),
                new KeyValuePair<char, char>('r', 'e'),
                new KeyValuePair<char, char>('t', 'o'),
                new KeyValuePair<char, char>('t', 'e'),
                new KeyValuePair<char, char>('s', 't'),
                new KeyValuePair<char, char>('t', 'h'),
                new KeyValuePair<char, char>('e', 'f'),
                new KeyValuePair<char, char>('u', 'n'),
                new KeyValuePair<char, char>('c', 't'),
                new KeyValuePair<char, char>('i', 'o'),
                new KeyValuePair<char, char>('n', 'z') 
            };
            
            //Act
            var actualResult = PlayfairCipher.prepMessage(inputValue);
           
            //Assert
            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }
        
        /// <summary>
        /// Tests esplit method.
        /// Method splits values into pairs. It also validates no duplicates are pared. 
        /// </summary>
        [Test]
        public void eSplitMessageTest()
        {
            //Arrange 
            var inputValue = "A small message is just here to test the function.";
            var input = new StringBuilder(inputValue.ToLower());
            
            var expectedResult = new List<KeyValuePair<char, char>>
            {
                new KeyValuePair<char, char>('a', ' '), 
                new KeyValuePair<char, char>('s', 'm'), 
                new KeyValuePair<char, char>('a', 'l'), 
                new KeyValuePair<char, char>('l', ' '), 
                new KeyValuePair<char, char>('m', 'e'),
                new KeyValuePair<char, char>('s', 'z'),
                new KeyValuePair<char, char>('s', 'a'),
                new KeyValuePair<char, char>('g', 'e'),
                new KeyValuePair<char, char>(' ', 'i'),
                new KeyValuePair<char, char>('s', ' '),
                new KeyValuePair<char, char>('j', 'u'),
                new KeyValuePair<char, char>('s', 't'),
                new KeyValuePair<char, char>(' ', 'h'),
                new KeyValuePair<char, char>('e', 'r'),
                new KeyValuePair<char, char>('e', ' '),
                new KeyValuePair<char, char>('t', 'o'),
                new KeyValuePair<char, char>(' ', 't'),
                new KeyValuePair<char, char>('e', 's'),
                new KeyValuePair<char, char>('t', ' '),
                new KeyValuePair<char, char>('t', 'h'),
                new KeyValuePair<char, char>('e', ' '),
                new KeyValuePair<char, char>('f', 'u'),
                new KeyValuePair<char, char>('n', 'c'),
                new KeyValuePair<char, char>('t', 'i'),
                new KeyValuePair<char, char>('o', 'n'),
                new KeyValuePair<char, char>('.', 'z'),
            };
            
            //Act
            var actualResult = PlayfairCipher.eSplitMessage(input);
           
            //Assert
            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }
        
        /// <summary>
        /// Tests dsplit method.
        /// Method splits values into pairs. 
        /// </summary>
        [Test]
        public void dSplitMessageTest()
        {
            //Arrange 
            var inputValue = "A small message is just here to test the function.";
            var input = new StringBuilder(inputValue.ToLower());
            
            var expectedResult = new List<KeyValuePair<char, char>>
            {
                new KeyValuePair<char, char>('a', ' '), 
                new KeyValuePair<char, char>('s', 'm'), 
                new KeyValuePair<char, char>('a', 'l'), 
                new KeyValuePair<char, char>('l', ' '), 
                new KeyValuePair<char, char>('m', 'e'),
                new KeyValuePair<char, char>('s', 's'),
                new KeyValuePair<char, char>('a', 'g'),
                new KeyValuePair<char, char>('e', ' '),
                new KeyValuePair<char, char>('i', 's'),
                new KeyValuePair<char, char>(' ', 'j'),
                new KeyValuePair<char, char>('u', 's'),
                new KeyValuePair<char, char>('t', ' '),
                new KeyValuePair<char, char>('h', 'e'),
                new KeyValuePair<char, char>('r', 'e'),
                new KeyValuePair<char, char>(' ', 't'),
                new KeyValuePair<char, char>('o', ' '),
                new KeyValuePair<char, char>('t', 'e'),
                new KeyValuePair<char, char>('s', 't'),
                new KeyValuePair<char, char>(' ', 't'),
                new KeyValuePair<char, char>('h', 'e'),
                new KeyValuePair<char, char>(' ', 'f'),
                new KeyValuePair<char, char>('u', 'n'),
                new KeyValuePair<char, char>('c', 't'),
                new KeyValuePair<char, char>('i', 'o'),
                new KeyValuePair<char, char>('n', '.'),
            };
            
            //Act
            var actualResult = PlayfairCipher.dSplitMessage(inputValue.ToLower());
           
            //Assert
            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }

        [Test]
        [TestCase('g','h','y','a')]
        [TestCase('d','f','f','p')]
        [TestCase('a','x','g','a')]
        [TestCase('q','r','r','m')]
        [TestCase('h','a','e','t')]
        [TestCase('v','e','e','d')]
        [TestCase('g','z','z','y')]
        [TestCase('n','q','p','r')]
        public void replacePairEncryptTest(char keyToReplace, char valueToReplace, char expectedKey, char expectedValue)
        {
            //Arrange
            var pairToReplace = new KeyValuePair<char, char>(keyToReplace,valueToReplace);
            var key = new[,]
            {
                {'t','h','e','l','a'},
                {'z','y','d','o','g'},
                {'b','c','f','i','k'},
                {'m','n','p','q','r'},
                {'s','u','v','w','x'}
            };
            var expectedResult = new KeyValuePair<char, char>(expectedKey, expectedValue);
            
            //Act
            var actualResult = PlayfairCipher.replacePair(pairToReplace, key, true);

            //Assert
            Assert.AreEqual(actualResult.Key, expectedResult.Key);
            Assert.AreEqual(actualResult.Value, expectedResult.Value);
        }
        
        [Test]
        [TestCase('y','a','g','h')]
        [TestCase('f','p','d','f')]
        [TestCase('g','a','a','x')]
        [TestCase('r','m','q','r')]
        [TestCase('e','t','h','a')]
        [TestCase('e','d','v','e')]
        [TestCase('z','y','g','z')]
        [TestCase('p','r','n','q')]
        public void replacePairDecryptTest(char keyToReplace, char valueToReplace, char expectedKey, char expectedValue)
        {
            //Arrange
            var pairToReplace = new KeyValuePair<char, char>(keyToReplace,valueToReplace);
            var key = new[,]
            {
                {'t','h','e','l','a'},
                {'z','y','d','o','g'},
                {'b','c','f','i','k'},
                {'m','n','p','q','r'},
                {'s','u','v','w','x'}
            };
            var expectedResult = new KeyValuePair<char, char>(expectedKey, expectedValue);
            
            //Act
            var actualResult = PlayfairCipher.replacePair(pairToReplace, key, false);

            //Assert
            Assert.AreEqual(actualResult.Key, expectedResult.Key);
            Assert.AreEqual(actualResult.Value, expectedResult.Value);
        }
    }
}