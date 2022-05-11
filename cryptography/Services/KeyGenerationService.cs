using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace cryptography.Services
{
    public static class KeyGenerationService
    {
        
        /// <summary>
        /// Generates random 26 char key. In which each char within the english alphabet only appears once. 
        /// </summary>
        /// <returns></returns>
        public static List<char> generateRandomKey()
        {
            var key = new List<char>();
            var keyPlacement = new List<int>();

            do
            {
                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                var byteArray = new byte[4];
                provider.GetBytes(byteArray);

                var randomInteger = (BitConverter.ToInt32(byteArray, 0) % 26) + 1;

                if (!keyPlacement.Contains(randomInteger) && (randomInteger < 27 && (randomInteger > 0))) {
                    keyPlacement.Add(randomInteger);
                }
            } while (keyPlacement.Count < 26);

            foreach (var item in keyPlacement)
            {
                key.Add((char)(item+96));
            }

            return key;
        }

        /// <summary>
        /// Generates a phrase based key given user input for the phrase. 
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="fullAlpha"></param>
        /// <returns></returns>
        public static List<char> generatePhraseKey(string phrase, bool fullAlpha)
        {
            var key = new List<char>();

            char[] charList = new char[phrase.Length];
 
            charList = phrase.ToLower().ToCharArray();
 
            foreach (char c in charList)
            {
                if (!key.Contains(c) && Char.IsLetter(c)) {
                    key.Add(c);
                }
            }
            
            if (fullAlpha){
                for (int i = 1; i < 27; i++)
                {
                    if (!key.Contains((char)(i+96))) {
                        key.Add((char)(i+96));
                    }
                }
            }
            
            return key;
        }
    }
}