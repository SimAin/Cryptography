using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cryptography.historicAlgorithms
{
    public class PlayfairCipher 
    {
        public void run(){
            printMenu();
            var option = Console.ReadLine();
            if (SharedLib.validateOption(option, new int[] {1,2,9})){
                var optionValue = int.Parse(option);

                switch (optionValue)
                {
                    case 1:
                        var key = SharedLib.identifyKey(true);
                        var ekey = processKey(key);
                        printKey(key, ekey);
                        encode(ekey);
                        break;  
                    case 2:
                        var inputKey = SharedLib.inputKey();
                        var dkey = processKey(inputKey);
                        decode(dkey);
                        break;
                    case 9:
                        break;
                    default:
                        break;
                }
            }
        }
        private void encode(char[,] key)
        {
            var fileString = File.ReadAllText("files/input.txt");
            var message = prepMessage(fileString);
            var output = replaceValues(message, key, true);
            printMessage(output, true);
            File.WriteAllTextAsync("files/output.txt", output);
        }

        private void decode (char[,] key) {
            var fileString = File.ReadAllText("files/output.txt");
            var message = dSplitMessage(fileString);
            var output = replaceValues(message, key, false);
            printMessage(output, false);
            File.WriteAllTextAsync("files/decoded.txt", output);
        }

        private string replaceValues(List<KeyValuePair<char, char>> message, char[,] key, bool encrypt)
        {
            var encryptedPairs = new List<KeyValuePair<char,char>>();
            var encryptedMessage = "";
            foreach (var pair in message)
            {
                var replacement = new KeyValuePair<char,char>();
                replacement = replacePair(pair,key, encrypt);
                encryptedPairs.Add(replacement);
            }

            foreach (var pair in encryptedPairs)
            {
                encryptedMessage = encryptedMessage + pair.Key + pair.Value;
            }
            return encryptedMessage;
        }
        private KeyValuePair<char,char> replacePair(KeyValuePair<char,char> pair, char[,] key, bool encrypt) 
        {
            var first = new KeyValuePair<int, int>();
            var second = new KeyValuePair<int, int>();
            var updatedPair = new KeyValuePair<char, char>();
            var checkEdge = 4;
            var rollEdge = 0;
            var shift = 1;
            if (!encrypt) {
                checkEdge = 0;
                rollEdge = 4;
                shift = -1;
            }

            var counter = 0;
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++) 
                {
                    if (key[row, col] == pair.Key) {
                        first = new KeyValuePair<int, int> (row,col);
                        counter++;
                    } else if (key[row, col] == pair.Value) {
                        second = new KeyValuePair<int, int> (row, col);
                        counter++;
                    }
                    if (counter == 2) {
                        break;
                    }
                }

                if (counter == 2) {
                    break;
                }
            }

            //Same Row
            if (first.Key == second.Key){
                if (first.Value != checkEdge && second.Value != checkEdge){
                    updatedPair = new KeyValuePair<char, char>(key[first.Key, first.Value+shift],key[second.Key, second.Value-+shift]);
                } else if(first.Value == checkEdge){
                    updatedPair = new KeyValuePair<char, char>(key[first.Key, rollEdge],key[second.Key, second.Value+shift]);
                } else if (second.Value == checkEdge){
                    updatedPair = new KeyValuePair<char, char>(key[first.Key, first.Value+shift],key[second.Key, rollEdge]);
                }
            }
            //Same Col
            else if (first.Value == second.Value)
            {
                if (first.Key != checkEdge && second.Key != checkEdge){
                    updatedPair = new KeyValuePair<char, char>(key[first.Key+shift, first.Value],key[second.Key+shift, second.Value]);
                } else if(first.Key == checkEdge && second.Key != checkEdge){
                    updatedPair = new KeyValuePair<char, char>(key[rollEdge, first.Value],key[second.Key+shift, second.Value]);
                } else if (second.Key == checkEdge && first.Key != checkEdge){
                    updatedPair = new KeyValuePair<char, char>(key[first.Key+shift, first.Value],key[rollEdge, second.Value]);
                } 
            } else{
                updatedPair = new KeyValuePair<char, char>(key[first.Key, second.Value],key[second.Key, first.Value]);
            }

            return updatedPair;
        }

        private List<KeyValuePair<char, char>> prepMessage (string inputMessage)
        {
            var osb = new StringBuilder(inputMessage.ToLower());
            var csb = new StringBuilder(inputMessage.ToLower());

            //Find and replace all 'j' chars with 'i'
            var jlocs = SharedLib.GetCharIndexInString(osb.ToString(), (char)'j');

            foreach (var jloc in jlocs)
            {
                csb.Remove(jloc,1);
                csb.Insert(jloc,(char)'i');
            }
            
            //Find and remove all whitespace char
            //Counter required as sb length changes unlike array
            var counter = 0;
            var chararray = osb.ToString().ToCharArray();
            for (int i = 0; i < chararray.Length; i++)
            {
                
                if(!Char.IsLetter(chararray[i])){
                    csb.Remove(i-counter,1);
                    counter++;
                }
            }

            var couplets = eSplitMessage(csb);
            return couplets;
        }

        private List<KeyValuePair<char,char>> eSplitMessage (StringBuilder csb)
        {
            var couplets = new List<KeyValuePair<char, char>>();
            char[] charList = new char[csb.Length];
            charList = csb.ToString().ToCharArray();
            for (int i = 0; i < charList.Length; i+=2)
            {
                if (i+1 < charList.Length){
                    if (charList[i] != charList[i+1]){
                        couplets.Add(new KeyValuePair<char, char> (charList[i], charList[i+1]));
                    } else {
                        couplets.Add(new KeyValuePair<char, char> (charList[i], 'z'));
                        i = i-1;
                    }
                } else {
                    couplets.Add(new KeyValuePair<char, char> (charList[i], 'z'));
                }
            }
            return couplets;
        }

        private List<KeyValuePair<char,char>> dSplitMessage (string message)
        {
            var couplets = new List<KeyValuePair<char, char>>();
            char[] charList = new char[message.Length];
            charList = message.ToString().ToCharArray();
            for (int i = 0; i < charList.Length; i+=2)
            {
                couplets.Add(new KeyValuePair<char, char> (charList[i], charList[i+1]));
            }
            return couplets;
        }

        private char[,] processKey(List<char> randomKey)
        {
            var key = new char[5,5];
            var counter = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (randomKey[counter] != 'j'){
                        key[i,j] = randomKey[counter];
                    } else if (randomKey[counter] == 'j' && counter != (randomKey.Count -1)){
                        counter++;
                        key[i,j] = randomKey[counter];
                    }
                    counter++;
                }
            }

            return key;
        }

        private void printMessage(string message, bool encrypted){
            Console.WriteLine("");
            if (encrypted){
                Console.WriteLine("Encrypted message: ");
            } else {
                Console.WriteLine("Decrypted message: ");
            }
            Console.WriteLine(message);
        }

        private void printKey(List<char> randomKey, char[,] key)
        {
            SharedLib.printKey(randomKey);
            Console.WriteLine("Random key formatted: ");
            Console.WriteLine("");
            for (int i = 0; i < 5; i++)
            {
                if (i != 0){
                    Console.WriteLine("");
                }
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(key[i,j]);
                }
            }
            Console.WriteLine("");
        }

        private void printMenu(){
            SharedLib.printCipherName("Playfair Cipher");
            Console.WriteLine("Rules: ");
            Console.WriteLine("a) Instances of the letter `j` are replaced with `i`. ");
            Console.WriteLine("b) Identical pairs of letters with be broken with a `z`. ");
            Console.WriteLine("c) If there are an odd no of letters a `z` will be placed at the end. ");
            Console.WriteLine("d) Spaces and capitals will be removed.");
            Console.WriteLine("");
            SharedLib.printCipherMenu();
        }
    }
}