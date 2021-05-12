using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cryptography.historicAlgorithms
{
    public class vigenereCipher
    {
        private caesarCipher caesar = new caesarCipher();
        public void run(){
            sharedLib.printCypherName("Vigenère Cipher");
            sharedLib.printCypherMenu();
            var option = Console.ReadLine();
            if (sharedLib.validateOption(option, new int[] {1,2,9})){
                var optionValue = int.Parse(option);

                switch (optionValue)
                {
                    case 1:

                        var keyPhrase = sharedLib.generatePhraseKey(false);
                        encode(keyPhrase);
                        break;  
                    case 2:
                        var key = sharedLib.inputKey(true);
                        decode(key);
                        break;
                    case 9:
                        break;
                    default:
                        break;
                }
            }
        }
        private void encode(List<char> keyPhrase)
        {
            var fileString = File.ReadAllText("files/input.txt");
            var key = generateFullKey(fileString.Length, keyPhrase);
            var output = eReplaceValues(fileString, key);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/output.txt", output);
        }
        private void decode (List<char> keyPhrase) {
            var fileString = File.ReadAllText("files/output.txt");
            var key = generateFullKey(fileString.Length, keyPhrase);
            var output = dReplaceValues(fileString, key);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/decoded.txt", output);
        }

        private string eReplaceValues(string fileString, List<char> key)
        {
            var csb = new StringBuilder(fileString.ToLower());
            var messageChars = fileString.ToLower().ToCharArray();

            for (int i = 0; i < messageChars.Length; i++)
            {
                var skip = (int) (key[i]-97) ;

                if (Char.IsLetter(messageChars[i])) {
                    csb.Remove(i,1);
                    if (((int)messageChars[i]+skip) > 122){
                        var rem =  (messageChars[i]+skip) -122;
                        csb.Insert(i,(char)(rem+96));
                    } else{
                        csb.Insert(i,(char)(messageChars[i]+skip));
                    }
                }

            }
            return csb.ToString();
        }

        private string dReplaceValues(string fileString, List<char> key)
        {
            var csb = new StringBuilder(fileString.ToLower());
            var messageChars = fileString.ToLower().ToCharArray();

            for (int i = 0; i < messageChars.Length; i++)
            {
                var skip = (int) -(key[i]-97) ;

                if (Char.IsLetter(messageChars[i])) {
                    csb.Remove(i,1);
                    if (((int)messageChars[i]+skip) < 97){
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