using System;
using System.Collections.Generic;
using cryptography.HistoricCiphers;
using NUnit.Framework;

namespace cryptography.tests.HistoricCiphersTests
{
    public class VigenereCipherTests
    {
        
        private const string Plaintext1 = "a";
        private const string Plaintext2 = "b";
        private const string Plaintext3 = "abcdefghijklmnopqrstuvwxyz";
        private const string Plaintext4 = "This is a secret message";

        private static readonly List<char> Key1 = new List<char>(){'q'};
        private static readonly List<char> Key2 = new List<char>(){'g'};
        private static readonly List<char> Key3 = new List<char>(){'a','b'};
        private static readonly List<char> Key4 = new List<char>(){'a','b'};
        
        private const string Ciphertext1 = "q";
        private const string Ciphertext2 = "h";
        private const string Ciphertext3 = "nopqrstuvwxyzabcdefghijklm";
        private const string Ciphertext4 = "Ymnx nx f xjhwjy rjxxflj";
        
        [Test]
        [TestCase(Plaintext1, Key1, Ciphertext1)]
        [TestCase(Plaintext2, Key2, Ciphertext2)]
        [TestCase(Plaintext3, Key3, Ciphertext3)]
        [TestCase(Plaintext4, Key4, Ciphertext4)]
        public static void replaceValsEncodeTest(string inputValue, int skip, string expectedResult)
        {
            //Act
            var actualResult = VigenereCipher.replaceValues(inputValue, skip);
           
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [Test]
        [TestCase(Ciphertext1, 26-Key1, Plaintext1)]
        [TestCase(Ciphertext2, 26-Key2, Plaintext2)]
        [TestCase(Ciphertext3, 26-Key3, Plaintext3)]
        [TestCase(Ciphertext4, 26-Key4, Plaintext4)]
        public static void replaceValsDecodeTest(String inputValue, int skip, string expectedResult)
        {
            //Act
            var actualResult = VigenereCipher.replaceValues(inputValue, skip);
           
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}