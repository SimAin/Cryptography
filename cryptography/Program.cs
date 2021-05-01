using System;
using System.IO;
using System.Text;

namespace cryptography
{
    class Program
    {
        private static caesarCipher caesar = new caesarCipher();
        static void Main(string[] args)
        {
            Console.WriteLine("Cryptography.");
            Console.WriteLine("");
            Console.WriteLine("Select algorithm below to encrypt/decrypt files.");
            Console.WriteLine("");
            Console.WriteLine("1. Caesar cypher");

            var option = Console.ReadLine();

            if (sharedLib.validateOption(option, new int[] {1})){
                var optionValue = int.Parse(option);
                switch (optionValue)
                {
                    case 1:
                        caesar.run();
                        break;
                    default:
                        break;
                }
            } else {
                Console.WriteLine("Input invalid.");
            }
        }
    }
}
