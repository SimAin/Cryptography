
using System;
using System.IO;
using System.Text;

namespace cryptography.historicAlgorithms
{
    public class CaesarCipher 
    {
        public void run(){
            SharedLib.printCipherName("Caesar Cipher");
            SharedLib.printCipherMenu();

            var option = Console.ReadLine();
            if (SharedLib.validateOption(option, new[] {1,2,9})){
                var optionValue = int.Parse(option);
                int skip = SharedLib.inputIntKey();
                switch (optionValue)
                {
                    case 1:
                        encode(skip);
                        break;  
                    case 2:
                        decode(skip);
                        break;
                    case 9:
                        break;
                }
            }
        }

        public void encode (int skip) {
            var fileString = File.ReadAllText("files/input.txt");
            var output = replaceVals(fileString, skip);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/output.txt", output);
        }

        private void decode (int skip) {
            var fileString = File.ReadAllText("files/output.txt");
            var output = replaceVals(fileString, 26-skip);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/decoded.txt", output);
        }

        private static string replaceVals(string fileString, int skip){
            
            StringBuilder osb = new StringBuilder(fileString);
            StringBuilder csb = new StringBuilder(fileString);

            for (int i = 1; i <= 26; i++)
            {
                var ulocs = SharedLib.GetCharIndexInString(osb.ToString(), (char)(i+64));

                foreach (var uloc in ulocs)
                {
                    csb.Remove(uloc,1);
                    if ((i+64+skip) > 90){
                        var rem =  (i+64+skip) - 90;
                        csb.Insert(uloc,(char)(rem+64));
                    } else{
                        csb.Insert(uloc,(char)(i+64+skip));
                    }
                }

                var llocs = SharedLib.GetCharIndexInString(osb.ToString(), (char)(i+96));

                foreach (var lloc in llocs)
                {
                    csb.Remove(lloc,1);
                    if ((i+96+skip) > 122){
                        var rem =  (i+96+skip) - 122;
                        csb.Insert(lloc,(char)(rem+96));
                    } else{
                        csb.Insert(lloc,(char)(i+96+skip));
                    }
                }
            }
            return csb.ToString();
        }
    }
}