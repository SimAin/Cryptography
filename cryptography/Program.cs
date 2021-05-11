using System;
using System.IO;
using System.Text;
using cryptography.historicAlgorithms;

namespace cryptography
{
    class Program
    {
        private static caesarCipher caesar = new caesarCipher();
        private static simpleSubCipher simpleSub = new simpleSubCipher();
        private static playfairCypher playfair = new playfairCypher();
        private static vigenereCipher vigenere = new vigenereCipher();
        static void Main(string[] args)
        {
            var exit = false;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("Cryptography.");
                Console.WriteLine("");
                Console.WriteLine("Select algorithm below to encrypt/decrypt files.");
                Console.WriteLine("");
                Console.WriteLine("1. Caesar cypher");
                Console.WriteLine("2. Simple substitution cypher");
                Console.WriteLine("3. Playfair cypher");
                Console.WriteLine("4. Vigenère cypher");
                Console.WriteLine("9. Exit");

                var option = Console.ReadLine();

                if (sharedLib.validateOption(option, new int[] {1,2,3,4,9})){
                    var optionValue = int.Parse(option);
                    switch (optionValue)
                    {
                        case 1:
                            caesar.run();
                            break;
                        case 2:
                            simpleSub.run();
                            break;
                        case 3:
                            playfair.run();
                            break;
                        case 4: 
                            vigenere.run();
                            break;
                        case 9:
                            exit = true;
                            break;
                        default:
                            break;
                    }
                } else {
                    Console.WriteLine("Input invalid.");
                }
            } while (!exit);
        }
    }
}
