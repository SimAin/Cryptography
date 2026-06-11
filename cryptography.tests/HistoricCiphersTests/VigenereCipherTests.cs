using System;
using System.Collections.Generic;
using cryptography.HistoricCiphers;
using cryptography.Models;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace cryptography.tests.HistoricCiphersTests
{
    public class VigenereCipherTests
    {
        private const string Plaintext1 = "a";
        private const string Plaintext2 = "b";
        private const string Plaintext3 = "abcdefghijklmnopqrstuvwxyz";
        private const string Plaintext4 = "This is a secret message";
        // The cipher lowercases its input, so decoding returns the lowercase plaintext.
        private const string DecodedPlaintext4 = "this is a secret message";

        private const string Key1 = "q";
        private const string Key2 = "g";
        private const string Key3 = "ab";
        private const string Key4 = "ab";

        private const string Ciphertext1 = "q";
        private const string Ciphertext2 = "h";
        private const string Ciphertext3 = "acceeggiikkmmooqqssuuwwyya";
        private const string Ciphertext4 = "tiit js a sfcseu netsbgf";

        [Test]
        [TestCase(Plaintext1, Key1, Ciphertext1)]
        [TestCase(Plaintext2, Key2, Ciphertext2)]
        [TestCase(Plaintext3, Key3, Ciphertext3)]
        [TestCase(Plaintext4, Key4, Ciphertext4)]
        public void replaceValsEncodeTest(string inputValue, string keyPhrase, string expectedResult)
        {
            //Arrange
            var cipher = new VigenereCipher("Vigenère Cipher", CipherType.Substitution);
            var key = generateFullKey(inputValue.Length, keyPhrase);

            //Act
            var actualResult = cipher.replaceValues(inputValue, key, true);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        [TestCase(Ciphertext1, Key1, Plaintext1)]
        [TestCase(Ciphertext2, Key2, Plaintext2)]
        [TestCase(Ciphertext3, Key3, Plaintext3)]
        [TestCase(Ciphertext4, Key4, DecodedPlaintext4)]
        public void replaceValsDecodeTest(string inputValue, string keyPhrase, string expectedResult)
        {
            //Arrange
            var cipher = new VigenereCipher("Vigenère Cipher", CipherType.Substitution);
            var key = generateFullKey(inputValue.Length, keyPhrase);

            //Act
            var actualResult = cipher.replaceValues(inputValue, key, false);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        // replaceValues expects the key already repeated to the message length
        // (one key character per position, including non-letters), as the
        // cipher's private generateFullKey produces.
        private static List<char> generateFullKey(int messageLength, string keyPhrase)
        {
            var key = new List<char>();
            for (int i = 0; i < messageLength; i++)
            {
                key.Add(keyPhrase[i % keyPhrase.Length]);
            }
            return key;
        }
    }
}
