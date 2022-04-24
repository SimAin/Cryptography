using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cryptography.historicAlgorithms
{
    public class RailFenceCipher
    {
        public void run(){
            SharedLib.printCipherName("Rail Fence Cipher");
            SharedLib.printCipherMenu();

            var option = Console.ReadLine();
            if (SharedLib.validateOption(option, new int[] {1,2,9})){
                var optionValue = int.Parse(option);
                int depth = SharedLib.inputIntKey();
                switch (optionValue)
                {
                    case 1:
                        encode(depth);
                        break;  
                    case 2:
                        decode(depth);
                        break;
                    case 9:
                        break;
                    default:
                        break;
                }
            }
        }

        private void encode (int depth) {
            var fileString = File.ReadAllText("files/input.txt");
            var output = replaceValsE(fileString, depth);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/output.txt", output);
        }

        private void decode (int depth) {
            var fileString = File.ReadAllText("files/output.txt");
            var output = replaceValsD(fileString, depth);
            Console.WriteLine(output);
            //File.WriteAllTextAsync("files/decoded.txt", output);
        }

        private bool replaceValsD(string fileString, int depth)
        {
            throw new NotImplementedException();
        }

        private string replaceValsE(string fileString, int depth)
        {
            StringBuilder osb = new StringBuilder(fileString);
            StringBuilder[] csb = new StringBuilder[depth];
            
            var charList = osb.ToString().ToLower().ToCharArray();

            for (int i = 0; i < depth; i++)
            {
                csb[i] = new StringBuilder();
            }
            var depthCounter = 0;
            var movingDownRail = false;

            for (int i = 0; i < charList.Length; i++)
            {
                
                csb[depthCounter].Append(charList[i].ToString());
                
                //If it is the lowest rail
                if (depthCounter == depth -1)
                {
                    //Swap to moving up
                    movingDownRail = false;
                    //set rail counter to next rail up
                    depthCounter -= 1;
                } 
                //If moving down rails and not at the bottom
                else if (movingDownRail && depthCounter != depth -1)
                {
                    //Increase rail depth by 1 
                    depthCounter++;
                }
                // If moving up rails and not at top
                else if (!movingDownRail && depthCounter != 0)
                {
                    //Decrease depth count by 1
                    depthCounter -= 1;
                }
                // If moving up rails and at top
                else if (!movingDownRail && depthCounter == 0)
                {
                    //Set to moving down rails
                    movingDownRail = true;
                    //Increase rail depth by 1
                    depthCounter++;
                }
            }

            var output = "";
            foreach (var rail in csb)
            {
                output += rail;
            }
            
            return output;
        }
    }
}