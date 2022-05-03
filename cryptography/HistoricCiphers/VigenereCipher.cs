using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using cryptography.Models;

namespace cryptography.HistoricCiphers
{
    public class VigenereCipher : Cipher
    {
        public VigenereCipher(string name, CipherType type) : base(name, type)
        {
            Name = name;
            Type = type;
        }
        
        public override void run(string inputFile = "files/input.txt", string encodedFile = "files/output.txt", string decodedFile = "files/decoded.txt"){
            SharedLib.printCipherName("Vigenère Cipher");
            SharedLib.printCipherMenu();
            var option = Console.ReadLine();
            if (SharedLib.validateOption(option, new[] {1,2,9})){
                var optionValue = int.Parse(option);

                switch (optionValue)
                {
                    case 1:

                        var keyPhrase = SharedLib.generatePhraseKey(false);
                        encode(keyPhrase, inputFile, encodedFile);
                        break;  
                    case 2:
                        var key = SharedLib.inputKey(true);
                        decode(key, encodedFile, decodedFile);
                        break;
                    case 9:
                        break;
                }
            }
        }
        public void encode (List<char> keyPhrase, string readFromFile, string writeToFile) {
        
            var fileString = File.ReadAllText(readFromFile);
            var key = generateFullKey(fileString.Length, keyPhrase);
            var output = replaceValues(fileString, key, true);
            Console.WriteLine(output);
            File.WriteAllTextAsync(writeToFile, output);
        }
        private void decode (List<char> keyPhrase, string readFromFile, string writeToFile) {
            
            var fileString = File.ReadAllText(readFromFile);
            var key = generateFullKey(fileString.Length, keyPhrase);
            var output = replaceValues(fileString, key, false);
            Console.WriteLine(output);
            File.WriteAllTextAsync(writeToFile, output);
        }

        private string replaceValues(string fileString, List<char> key, bool encrypt)
        {
            var csb = new StringBuilder(fileString.ToLower());
            var messageChars = fileString.ToLower().ToCharArray();

            for (int i = 0; i < messageChars.Length; i++)
            {
                var skip = key[i]-97;
                if (!encrypt){
                    skip = -skip;
                }

                if (Char.IsLetter(messageChars[i])) {
                    csb.Remove(i,1);
                    if ((messageChars[i]+skip) > 122 && encrypt){
                        var rem =  (messageChars[i]+skip) -122;
                        csb.Insert(i,(char)(rem+96));
                    } else if ((messageChars[i]+skip) < 97 && !encrypt){
                        var rem =  96 -(messageChars[i]+skip);
                        csb.Insert(i,(char)(122-rem));
                    } else{
                        csb.Insert(i,(char)(messageChars[i]+skip));
                    }
                }

            }
            return csb.ToString();
        }

        private List<char> generateFullKey (int message, List<char> keyPhrase){
            var key = new List<char>();
            var counter = 0;
            for (int i = 0; i < message; i++)
            {
                if(counter == keyPhrase.Count){
                    counter = 0;
                    key.Add(keyPhrase[counter]);
                } else{
                    key.Add(keyPhrase[counter]);
                }
                counter++;
            }
            return key;
        }
    }
}