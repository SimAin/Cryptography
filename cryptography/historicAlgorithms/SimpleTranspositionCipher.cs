using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cryptography.historicAlgorithms
{
    public class SimpleTranspositionCipher
    {
        internal void run()
        {
            SharedLib.printCipherName("Caesar Cipher");
            SharedLib.printCipherMenu();

            var option = Console.ReadLine();
            if (SharedLib.validateOption(option, new int[] {1,2,9})){
                var optionValue = int.Parse(option);
                int key = SharedLib.inputIntKey();
                switch (optionValue)
                {
                    case 1:
                        encode(key);
                        break;  
                    case 2:
                        decode(key);
                        break;
                    case 9:
                        break;
                    default:
                        break;
                }
            }
        }

        private void decode(int key)
        {
            var fileString = File.ReadAllText("files/output.txt");
            key = fileString.Length / key;
            var output = ReplaceVals(fileString, key, true);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/decoded.txt", output);
        }

        private void encode(int key)
        {
            var fileString = File.ReadAllText("files/input.txt");
            var output = ReplaceVals(fileString, key, false);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/output.txt", output);
        }

        private string ReplaceVals(string fileString, int key, bool v)
        {
            var messageChars = fileString.ToLower().ToCharArray();
            var arrayList = new List<char[]>();
            var csb = new StringBuilder();

            var blockSize = fileString.Length / key;

            Console.WriteLine("string length - " + messageChars.Length);
            Console.WriteLine("blocks - " + blockSize);

            for (int i = 0; i <= blockSize; i++)
            {
                var row = new char[key];
                var jump = i*key;
                for (int j = 0; j < key; j++)
                {
                    if((jump+j) >= messageChars.Length){
                        row[j] = 'z';
                    } else {
                        row[j] = messageChars[jump+j];
                    }
                }
                Console.WriteLine("");
                foreach (var item in row)
                {
                    Console.Write(item + ", ");
                }
                Console.WriteLine("");
                arrayList.Add(row);
            }

            for (int i = 0; i < key; i++)
            {
                foreach (var keyList in arrayList)
                {
                    csb.Append(keyList[i]);
                }
            }

            return csb.ToString();
        }
    }
}