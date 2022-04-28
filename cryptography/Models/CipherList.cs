using System.Collections;
using System.Collections.Generic;
using cryptography.HistoricCiphers;

namespace cryptography.Models
{
    public class CipherList 
    {
        public Dictionary<int,Cipher> Ciphers { get; }

        public CipherList()
        {
            Ciphers = new Dictionary<int, Cipher>
            {
                {1, new CaesarCipher("Caesar", CipherType.Substitution)},
                {2, new SimpleSubCipher("Simple Substitution", CipherType.Substitution)},
                {3, new PlayfairCipher("Playfair", CipherType.Substitution)},
                {4, new VigenereCipher("Vigenere", CipherType.Substitution)},
                {5, new SimpleTranspositionCipher("Simple Transposition", CipherType.Transposition)},
                {6, new RailFenceCipher("Rail Fence", CipherType.Transposition)}
            };
        }

        public List<int> getValidCipherOptions()
        {
            return new List<int>(Ciphers.Keys);
        }
    }
}