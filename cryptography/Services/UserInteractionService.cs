using System;
using System.Collections.Generic;
using System.Linq;

namespace cryptography.Services
{
    public class UserInteractionService
    {

        public static int inputIntKey()
        {
            var valid = false;
            string key;
            do
            {
                Console.WriteLine("Input numeric key value:");
                key = Console.ReadLine();
                if (int.TryParse(key, out _))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Input error, retry.");
                }
            } while (!valid);

            return int.Parse(key);
        }

        /// <summary>
        /// Console output - Print cipher name for consistent style.
        /// </summary>
        /// <param name="name"></param>
        public static void printCipherName(string name)
        {
            Console.WriteLine("");
            Console.WriteLine("----------------------------");
            Console.WriteLine(name);
            Console.WriteLine("");
        }
        
        /// <summary>
        /// Console output - Print default menu for a cipher.
        /// </summary>
        public static void printCipherMenu(){
            Console.WriteLine("Select option: ");
            Console.WriteLine("1. Encode ");
            Console.WriteLine("2. Decode ");
            Console.WriteLine("9. Exit");
        }
        
        /// <summary>
        /// Console output - Print invalid input for consistent style.
        /// </summary>
        public static void printInvalidInput(){
            Console.WriteLine("");
            Console.WriteLine("Invalid input, please retry.");
            Console.WriteLine("");
        }
        
        /// <summary>
        /// Validate user input is a valid option.
        /// </summary>
        /// <param name="option"></param>
        /// <param name="acceptedValues"></param>
        /// <returns></returns>
        public static bool validateOption (string option, IEnumerable<int> acceptedValues) {
            int value;
            if (int.TryParse(option, out value))
            {
                if (acceptedValues.Contains(value)){
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
        
        /// <summary>
        /// Take user input and return key in char list format.
        /// </summary>
        /// <param name="phraseAllowed"></param>
        /// <returns></returns>
        public static List<char> inputKey(bool phraseAllowed = false)
        {
            var valid = true;
            var key = new List<char>();
            do
            {
                Console.WriteLine("Enter key:");
                var input = Console.ReadLine();
                if (phraseAllowed || ((input.Length == 26) && !phraseAllowed))
                {
                    var charList = input.ToLower().ToCharArray();
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
        
        /// <summary>
        /// Console output - Print char list key to console.
        /// </summary>
        /// <param name="key"></param>
        public static void printKey(List<char> key){

            Console.WriteLine("");
            Console.WriteLine("New Key: ");
            foreach (var letter in key)
            {
                Console.Write(letter);
            }

            Console.WriteLine("");
            Console.WriteLine("");
        }
        
        /// <summary>
        /// Manages user selection between random key and custom key.
        /// </summary>
        /// <param name="phraseFullAlpha"></param>
        /// <returns></returns>
        public static List<char> identifyKeyType(bool phraseFullAlpha)
        {
            var key = new List<char>();
            var valid = true;

            do
            {
                Console.WriteLine("Select option: ");
                Console.WriteLine("1. Random key");
                Console.WriteLine("2. Phrase key");
                var keySelect = Console.ReadLine();

                if (UserInteractionService.validateOption(keySelect, new int[] {1,2})){
                    var option = int.Parse(keySelect);
                    switch (option)
                    {
                        case 1:
                            key = KeyGenerationService.generateRandomKey();
                            break;
                        case 2:
                            Console.WriteLine("Enter key phrase: ");
                            var phrase = Console.ReadLine();
                            key = KeyGenerationService.generatePhraseKey(phrase, phraseFullAlpha);
                            break;
                    }
                    valid = true;
                } else {
                    Console.WriteLine("Input error retry.");
                }
            } while (!valid);

            return key;
        }
    }
}