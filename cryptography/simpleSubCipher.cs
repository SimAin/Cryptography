using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace cryptography
{
    public class simpleSubCipher
    {
        public void run()
        {
            Console.WriteLine("Select option: ");
            Console.WriteLine("1. Encode ");
            Console.WriteLine("2. Decode ");

            var option = Console.ReadLine();
            if (sharedLib.validateOption(option, new int[] {1,2})){
                var optionValue = int.Parse(option);
                switch (optionValue)
                {
                    case 1:
                        var ekey = identifyKey();
                        printKey(ekey);
                        encode(ekey);
                        break;  
                    case 2:
                        var dkey = inputKey();
                        decode(dkey);
                        break;
                    default:
                        break;
                }
            }
        }

        private void encode(List<char> key)
        {
            var fileString = File.ReadAllText("files/input.txt");
            var output = ReplaceVals(fileString, key, false);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/output.txt", output);
        }

        private void decode (List<char> key) {
            var fileString = File.ReadAllText("files/output.txt");
            var output = ReplaceVals(fileString, key, true);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/decoded.txt", output);
        }

        private static string ReplaceVals(string fileString, List<char> key, bool decode)
        {
            StringBuilder osb = new StringBuilder(fileString.ToLower());
            StringBuilder csb = new StringBuilder(fileString.ToLower());

            for (int i = 1; i < 27; i++)
            {
                var find = (char)(i+96);
                var replace = key[i-1];
                if (decode){
                    find = key[i-1];
                    replace = (char)(i+96);
                }

                var llocs = sharedLib.GetCharIndexInString(osb.ToString(), find);

                foreach (var lloc in llocs)
                {
                    csb.Remove(lloc,1);
                    csb.Insert(lloc, replace);
                }
            }
            return csb.ToString();
        }

        private List<char> inputKey()
        {
            var valid = true;
            var key = new List<char>();
            do
            {
                Console.WriteLine("Enter Key:");
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

        private List<char> identifyKey()
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
                            key = generatePhraseKey();
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

        private List<char> generateRandomKey()
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

        private List<char> generatePhraseKey()
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

            for (int i = 1; i < 27; i++)
            {
                if (!key.Contains((char)(i+96))) {
                    key.Add((char)(i+96));
                }
            }
            
            return key;

        }

        private void printKey(List<char> key)
        {
            Console.WriteLine("");
            Console.WriteLine("New key: ");
            foreach (var item in key)
            {
                Console.Write(item);
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}