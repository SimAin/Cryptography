using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cryptography
{
    public class playfairCypher 
    {
        public void run(){
            Console.WriteLine("Select option: ");
            Console.WriteLine("1. Encode ");
            Console.WriteLine("2. Decode ");

            var option = Console.ReadLine();
            if (sharedLib.validateOption(option, new int[] {1,2})){
                var optionValue = int.Parse(option);

                switch (optionValue)
                {
                    case 1:
                        var randomKey = sharedLib.generateRandomKey();
                        var ekey = processKey(randomKey);
                        printKey(randomKey, ekey);
                        encode(ekey);
                        break;  
                    case 2:
                        var inputKey = sharedLib.inputKey();
                        var dkey = processKey(inputKey);
                        decode(dkey);
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
            var output = ReplaceVals(message, key);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/output.txt", output);
        }

        private void decode (char[,] key) {
            var fileString = File.ReadAllText("files/output.txt");
            var message = dSplitMessage(fileString);
            var output = ReplaceVals(message, key);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/decoded.txt", output);
        }

        private string ReplaceVals(List<KeyValuePair<char, char>> message, char[,] key)
        {
            var encryptedPairs = new List<KeyValuePair<char,char>>();
            var encryptedMessage = "";
            foreach (var pair in message)
            {
                encryptedPairs.Add(replacePair(pair, key));
            }

            foreach (var pair in encryptedPairs)
            {
                encryptedMessage = encryptedMessage + pair.Key + pair.Value;
            }
            return encryptedMessage;
        }

        private KeyValuePair<char,char> replacePair(KeyValuePair<char,char> pair, char[,] key) 
        {
            var first = new KeyValuePair<int, int>();
            var second = new KeyValuePair<int, int>();
            var encryptedPair = new KeyValuePair<char, char>();
            var counter = 0;
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (key[row, col] == pair.Key)
                    {
                        first = new KeyValuePair<int, int> (row,col);
                        counter++;
                    } else if (key[row, col] == pair.Value) 
                    {
                        second = new KeyValuePair<int, int> (row, col);
                        counter++;
                    }
                    if (counter == 2)
                    {
                        break;
                    }
                }

                if (counter == 2)
                {
                    break;
                }
            }

            // If same row
            if (first.Key == second.Key){
                if (first.Value != 4 && second.Value != 4){
                    encryptedPair = new KeyValuePair<char, char>(key[first.Key, first.Value+1],key[second.Key, second.Value + 1]);
                } else if(first.Value == 4){
                    encryptedPair = new KeyValuePair<char, char>(key[first.Key, 0],key[second.Key, second.Value + 1]);
                } else if (second.Value == 4){
                    encryptedPair = new KeyValuePair<char, char>(key[first.Key, first.Value+1],key[second.Key, 0]);
                }
            }
            else if (first.Value == second.Value)
            {
                if (first.Key != 4 && second.Key != 4){
                    encryptedPair = new KeyValuePair<char, char>(key[first.Key+1, first.Value],key[second.Key+1, second.Value]);
                } else if(first.Value == 4){
                    encryptedPair = new KeyValuePair<char, char>(key[0, first.Value],key[second.Key, second.Value + 1]);
                } else if (second.Value == 4){
                    encryptedPair = new KeyValuePair<char, char>(key[first.Key+1, first.Value],key[0, second.Value]);
                }
                // same col
            } else{
                encryptedPair = new KeyValuePair<char, char>(key[first.Key, second.Value],key[second.Key, first.Value]);
            }

            return encryptedPair;
        }

        private List<KeyValuePair<char, char>> prepMessage (string inputMessage)
        {
            var osb = new StringBuilder(inputMessage.ToLower());
            var csb = new StringBuilder(inputMessage.ToLower());

            var llocs = sharedLib.GetCharIndexInString(osb.ToString(), (char)'j');

            foreach (var lloc in llocs)
            {
                csb.Remove(lloc,1);
                csb.Insert(lloc,(char)'i');
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

        private void printKey(List<char> randomKey, char[,] key)
        {
            Console.WriteLine("");
            Console.WriteLine("Random Key: ");
            foreach (var letter in randomKey)
            {
                Console.Write(letter);
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Random key formatted: ");
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
    }
}