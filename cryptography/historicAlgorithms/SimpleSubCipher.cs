using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace cryptography.historicAlgorithms
{
    public class SimpleSubCipher
    {
        public void run()
        {
            SharedLib.printCipherName("Simple Substitution Cipher");
            SharedLib.printCipherMenu();

            var option = Console.ReadLine();
            if (SharedLib.validateOption(option, new int[] {1,2,9})){
                var optionValue = int.Parse(option);
                switch (optionValue)
                {
                    case 1:
                        var ekey = SharedLib.identifyKey(true);
                        SharedLib.printKey(ekey);
                        encode(ekey);
                        break;  
                    case 2:
                        var dkey = SharedLib.inputKey();
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