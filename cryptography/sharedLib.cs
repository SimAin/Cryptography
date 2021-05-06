using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace cryptography
{
    public class sharedLib 
    {

        public static bool validateOption (string option, int[] acceptedValues) {
            int value;
            if (int.TryParse(option, out value))
            {
                if (acceptedValues.Contains(value)){
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public static List<int> GetCharIndexInString (string fullText, char val){
            var foundIndexes = new List<int>();
        
            // for loop end when i=-1 ('a' not found)
            for (int i = fullText.IndexOf(val); i > -1; i = fullText.IndexOf(val, i + 1))
            {
                foundIndexes.Add(i);
            }

            return foundIndexes;
        }

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
        
        public static List<char> inputKey()
        {
            var valid = true;
            var key = new List<char>();
            do
            {
                Console.WriteLine("Enter key:");
                var input = Console.ReadLine();
                if (input.Length == 26){
                    char[] charList = new char[input.Length];
                    charList = input.ToLower().ToCharArray();
                    foreach (var c in charList)
                    {
                        key.Add(c);
                    }
                } else {
                    valid = false;
                }
            } while (!valid);

            return key;
        }
    }
}