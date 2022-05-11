using System;
using cryptography.HistoricCiphers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace cryptography.tests.HistoricCiphersTests
{
    public static class CaesarCipherTests
    {
        private const string Plaintext1 = "a";
        private const string Plaintext2 = "b";
        private const string Plaintext3 = "abcdefghijklmnopqrstuvwxyz";
        private const string Plaintext4 = "This is a secret message";

        private const int Key1 = 2;
        private const int Key2 = 26;
        private const int Key3 = 13;
        private const int Key4 = 5;
        
        private const string Ciphertext1 = "c";
        private const string Ciphertext2 = "b";
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
            var actualResult = CaesarCipher.replaceValues(inputValue, skip);
           
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
            var actualResult = CaesarCipher.replaceValues(inputValue, skip);
           
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}