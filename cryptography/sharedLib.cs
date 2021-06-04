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
        public static int inputIntKey(){
            var valid = false;
            int value; 
            string key;
            do
            {
                Console.WriteLine("Input numeric key value:");
                key = Console.ReadLine();
                if(int.TryParse(key, out value)){
                    valid = true;
                } else {
                    Console.WriteLine("Input error, retry.");
                }
            } while (!valid);

            return int.Parse(key);
        }
        
        public static List<char> inputKey(bool phraseAllowed = false)
        {
            var valid = true;
            var key = new List<char>();
            do
            {
                Console.WriteLine("Enter key:");
                var input = Console.ReadLine();
                if (phraseAllowed || ((input.Length == 26) && !phraseAllowed)){
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

        public static void printKey(List<char> randomKey){

            Console.WriteLine("");
            Console.WriteLine("New Key: ");
            foreach (var letter in randomKey)
            {
                Console.Write(letter);
            }

            Console.WriteLine("");
            Console.WriteLine("");
        }
        public static void printCypherName(string name){
            Console.WriteLine("");
            Console.WriteLine("----------------------------");
            Console.WriteLine(name);
            Console.WriteLine("");
        }

        public static void printCypherMenu(){
            Console.WriteLine("Select option: ");
            Console.WriteLine("1. Encode ");
            Console.WriteLine("2. Decode ");
            Console.WriteLine("9. Exit");
        }

        public static List<char> identifyKey(bool phraseFullAlpha)
        {
            var key = new List<char>();
            var valid = true;

            do
            {
                Console.WriteLine("Select option: ");
                Console.WriteLine("1. Random key");
                Console.WriteLine("2. Phrase key");
                var keySelect = Console.ReadLine();

                if (sharedLib.validateOption(keySelect, new int[] {1,2})){
                    var option = int.Parse(keySelect);
                    switch (option)
                    {
                        case 1:
                            key = generateRandomKey();
                            break;
                        case 2:
                            key = generatePhraseKey(phraseFullAlpha);
                            break;
                        default:
                            break;
                    }
                    valid = true;
                } else {
                    Console.WriteLine("Input error retry.");
                }
            } while (!valid);

            return key;
        }

        public static List<char> generatePhraseKey(bool fullAlpha)
        {
            var key = new List<char>();

            Console.WriteLine("Enter key phrase: ");
            var phrase = Console.ReadLine();

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