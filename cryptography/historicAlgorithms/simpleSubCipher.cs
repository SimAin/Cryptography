using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace cryptography.historicAlgorithms
{
    public class simpleSubCipher
    {
        public void run()
        {
            sharedLib.printCypherName("Simple Substitution Cypher");
            sharedLib.printCypherMenu();

            var option = Console.ReadLine();
            if (sharedLib.validateOption(option, new int[] {1,2,9})){
                var optionValue = int.Parse(option);
                switch (optionValue)
                {
                    case 1:
                        var ekey = identifyKey();
                        sharedLib.printKey(ekey);
                        encode(ekey);
                        break;  
                    case 2:
                        var dkey = sharedLib.inputKey();
                        decode(dkey);
                        break;
                    case 9:
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
                            key = sharedLib.generateRandomKey();
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
    }
}