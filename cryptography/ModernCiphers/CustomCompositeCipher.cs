using System;
using System.Collections.Generic;
using System.Linq;
using cryptography.HistoricCiphers;
using cryptography.Models;
using cryptography.Services;

namespace cryptography.ModernCiphers
{
    public class CustomCompositeCipher
    {
        private int chainMaxLength = 10;
        
        public void run(CipherList cipherList){
            UserInteractionService.printCipherName("Custom composite cipher");
            Console.WriteLine("");
            Console.WriteLine("This function allows chaining of historic ciphers.");
            Console.WriteLine("After each selection either select the next cipher in the chain, complete, or exit.");
            //TODO: correct to enable exit at any point.
            Console.WriteLine("Once the started, please complete to enable exit.");
            Console.WriteLine("The preset max chain length is: " + chainMaxLength);
            Console.WriteLine("");
            Console.WriteLine("Please select the first cipher to be applied.");

            
            var complete = false;
            
            var chainList = new List<KeyValuePair<int,int>>();

            do
            {
                foreach (var c in cipherList.getValidCipherOptions())
                {
                    Console.WriteLine(c + ") " + cipherList.Ciphers[c].Name + " (" + cipherList.Ciphers[c].Type + ")");
                }
                
                Console.WriteLine("");
                Console.WriteLine("88. Complete");
                //TODO:Implement Exit
                //Console.WriteLine("");
                //Console.WriteLine("99. Exit");

                var option = Console.ReadLine();
                
                
                if (option != null)
                {
                    var optionValue = int.Parse(option);
                    
                    if (cipherList.getValidCipherOptions().Contains(optionValue)) {
                        chainList.Add(new KeyValuePair<int, int>(optionValue,1));
                    } else if (optionValue == 88) {
                        complete = true;
                    } else {
                        UserInteractionService.printInvalidInput();
                    }
                }
            } while (!complete);
        }

        private void encode(List<KeyValuePair<int,int>> chainList, CipherList cipherList)
        {
            for (int i = 0; i < chainList.Count; i++)
            {
                //TODO: sort files for each step and the order required.
                if (i == 1)
                {
                    cipherList.Ciphers[chainList[i].Key].run("files/input.txt", "files/tempCompositeOutput.txt", "files/tempCompositeOutput.txt");
                }
                else if (i == chainList.Count)
                {
                    cipherList.Ciphers[chainList[i].Key].run("files/tempCompositeOutput.txt", "files/.txt", "files/tempCompositeOutput.txt");
                }
                else
                {
                    cipherList.Ciphers[chainList[i].Key].run();
                }
            }
            foreach (var cipher in chainList)
            {
                cipherList.Ciphers[cipher.Key].run();
            }
        }
    }
}