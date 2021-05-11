using System;

namespace cryptography.historicAlgorithms
{
    public class vigenereCipher
    {
        public void run(){
            sharedLib.printCypherName("Vigenère Cipher");
            sharedLib.printCypherMenu();
            var option = Console.ReadLine();
            if (sharedLib.validateOption(option, new int[] {1,2,9})){
                var optionValue = int.Parse(option);

                switch (optionValue)
                {
                    case 1:
                        var key = sharedLib.generatePhraseKey();
                        break;  
                    case 2:
                        var inputKey = sharedLib.inputKey();
                        break;
                    case 9:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}