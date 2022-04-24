using System;
using System.IO;
using System.Text;
using cryptography.historicAlgorithms;
using cryptography.Calculations;

namespace cryptography
{
    class Program
    {
        private static CaesarCipher caesar = new CaesarCipher();
        private static SimpleSubCipher simpleSub = new SimpleSubCipher();
        private static PlayfairCipher playfair = new PlayfairCipher();
        private static VigenereCipher vigenere = new VigenereCipher();
        private static SimpleTranspositionCipher trans = new SimpleTranspositionCipher();
        private static RailFenceCipher railFence = new RailFenceCipher();
        private static Calculations.Calculations calcs = new Calculations.Calculations();
        
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
                Console.WriteLine("Substitution ciphers:");
                Console.WriteLine("1. Caesar cipher");
                Console.WriteLine("2. Simple substitution cipher");
                Console.WriteLine("3. Playfair cipher");
                Console.WriteLine("4. Vigenère cipher");
                Console.WriteLine("");
                Console.WriteLine("Transposition ciphers:");
                //TODO: Columnar, Multi-stage columnar
                Console.WriteLine("5. Simple transposition cipher");
                Console.WriteLine("6. Rail fence cipher");
                Console.WriteLine("");
                //TODO: Composite (product) ciphers - Feistel 
                Console.WriteLine("88. Calculations");
                Console.WriteLine("");
                Console.WriteLine("99. Exit");

                var option = Console.ReadLine();

                if (SharedLib.validateOption(option, new int[] {1,2,3,4,5,6,88,99})){
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
                        case 5:
                            trans.run();
                            break;
                        case 6:
                            railFence.run();
                            break;
                        case 88:
                            calcs.run();
                            break;
                        case 99:
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
