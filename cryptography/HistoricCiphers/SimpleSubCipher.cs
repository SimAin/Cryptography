using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using cryptography.Models;

namespace cryptography.HistoricCiphers
{
    public class SimpleSubCipher : Cipher
    {
        public SimpleSubCipher(string name, CipherType type) : base(name, type)
        {
            Name = name;
            Type = type;
        }
        
        public override void run(string inputFile = "files/input.txt", string encodedFile = "files/output.txt", string decodedFile = "files/decoded.txt"){
        
            SharedLib.printCipherName("Simple Substitution Cipher");
            SharedLib.printCipherMenu();

            var option = Console.ReadLine();
            if (SharedLib.validateOption(option, new[] {1,2,9})){
                var optionValue = int.Parse(option);
                switch (optionValue)
                {
                    case 1:
                        var ekey = SharedLib.identifyKey(true);
                        SharedLib.printKey(ekey);
                        encode(ekey, inputFile, encodedFile);
                        break;  
                    case 2:
                        var dkey = SharedLib.inputKey();
                        decode(dkey, encodedFile, decodedFile);
                        break;
                    case 9:
                        break;
                }
            }
        }

        private void encode (List<char> key, string readFromFile, string writeToFile) {
        
            var fileString = File.ReadAllText(readFromFile);
            var output = replaceVals(fileString, key, false);
            Console.WriteLine(output);
            File.WriteAllTextAsync(writeToFile, output);
        }

        private void decode (List<char> key, string readFromFile, string writeToFile) {
            
            var fileString = File.ReadAllText(readFromFile);
            var output = replaceVals(fileString, key, true);
            Console.WriteLine(output);
            File.WriteAllTextAsync(writeToFile, output);
        }

        private static string replaceVals(string fileString, List<char> key, bool decode)
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

                var llocs = SharedLib.GetCharIndexInString(osb.ToString(), find);

                foreach (var lloc in llocs)
                {
                    csb.Remove(lloc,1);
                    csb.Insert(lloc, replace);
                }
            }
            return csb.ToString();
        }
    }
}