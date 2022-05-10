using System;
using cryptography.Models;
using cryptography.Services;

namespace cryptography
{
    class Program
    {
        private static Calculations.Calculations calcs = new Calculations.Calculations();
        private static CipherList cipherList = new CipherList();

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

                foreach (var c in cipherList.getValidCipherOptions())
                {
                    Console.WriteLine(c + ") " + cipherList.Ciphers[c].Name + " (" + cipherList.Ciphers[c].Type + ")");
                }
                
                Console.WriteLine("");
                Console.WriteLine("7. Custom composite cipher");
                Console.WriteLine("");
                //TODO: Composite (product) ciphers - Feistel 
                Console.WriteLine("88. Calculations");
                Console.WriteLine("");
                Console.WriteLine("99. Exit");

                var option = Console.ReadLine();

                if (option != null)
                {
                    var optionValue = int.Parse(option);
                    
                    if (cipherList.getValidCipherOptions().Contains(optionValue)) {
                        cipherList.Ciphers[optionValue].run();
                    } else if (optionValue == 88) {
                        calcs.run();
                    } else if (optionValue == 99) {
                        exit = true;
                    } else {
                        UserInteractionService.printInvalidInput();
                    }
                }
            } while (!exit);
        }
    }
}
