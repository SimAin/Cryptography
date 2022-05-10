using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using cryptography.Models;
using cryptography.Services;

namespace cryptography.HistoricCiphers
{
    public class SimpleTranspositionCipher : Cipher
    {
        public SimpleTranspositionCipher(string name, CipherType type) : base(name, type)
        {
            Name = name;
            Type = type;
        }
        
        public override void run(string inputFile = "files/input.txt", string encodedFile = "files/output.txt", string decodedFile = "files/decoded.txt"){
        
            UserInteractionService.printCipherName("Caesar Cipher");
            UserInteractionService.printCipherMenu();

            var option = Console.ReadLine();
            if (UserInteractionService.validateOption(option, new[] {1,2,9})){
                var optionValue = int.Parse(option);
                int key = UserInteractionService.inputIntKey();
                switch (optionValue)
                {
                    case 1:
                        encode(key, inputFile, encodedFile);
                        break;  
                    case 2:
                        decode(key, encodedFile, decodedFile);
                        break;
                    case 9:
                        break;
                }
            }
        }

        private void encode (int key, string readFromFile, string writeToFile) {
        
            var fileString = File.ReadAllText(readFromFile);
            var output = replaceVals(fileString, key);
            Console.WriteLine(output);
            File.WriteAllTextAsync(writeToFile, output);
        }
        
        private void decode (int key, string readFromFile, string writeToFile) {
            
            var fileString = File.ReadAllText(readFromFile);
            key = fileString.Length / key;
            var output = replaceVals(fileString, key);
            Console.WriteLine(output);
            File.WriteAllTextAsync(writeToFile, output);
        }

        private string replaceVals(string fileString, int key)
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