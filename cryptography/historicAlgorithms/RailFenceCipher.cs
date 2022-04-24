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
            File.WriteAllTextAsync("files/decoded.txt", output);
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

            var csb2 = railStructure(charList, csb, depth, true);

            var output = "";
            foreach (var rail in csb2)
            {
                Console.WriteLine(rail.ToString());
                output += rail;
            }
            
            return output;
        }
        
        private string replaceValsD(string fileString, int depth)
        {
            StringBuilder osb = new StringBuilder(fileString);
            StringBuilder[] csb = new StringBuilder[depth];
            
            var charList = osb.ToString().ToLower().ToCharArray();
            var tempCharList = new char[charList.Length];

            for (int i = 0; i < charList.Length; i++)
            {
                tempCharList[i] = '_';
            }
            
            for (int i = 0; i < depth; i++)
            {
                csb[i] = new StringBuilder();
                foreach (var c in charList)
                {
                    csb[i].Append('.');
                }
            }

            var csb2 = railStructure(tempCharList, csb, depth, false);
            
            var counter = 0;
            foreach (var rail in csb2)
            {
                for (int i = 0; i < rail.Length; i++)
                {
                    if (rail[i] == '_')
                    {
                        rail.Remove(i, 1);
                        rail.Insert(i,charList[counter]);
                        counter++;
                    }
                }
            }

            foreach (var p in csb2)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine("");

            var output = extractMessage(csb2,depth);
           
            return output.ToString();
        }

        private StringBuilder[] railStructure(char[] charList, StringBuilder[] csb, int depth, bool encode)
        {
            var depthCounter = 0;
            var movingDownRail = false;

            for (int i = 0; i < charList.Length; i++)
            {
                if (encode)
                {
                    csb[depthCounter].Append(charList[i].ToString());
                }
                else
                {
                    csb[depthCounter].Remove(i, 1);
                    csb[depthCounter].Insert(i,charList[i].ToString());
                }
                
                var tempDepthCounter = getDepthCount(depthCounter, depth, movingDownRail);
                var tempMovingDownRail = getRailDirection(depthCounter, depth, movingDownRail);
                depthCounter = tempDepthCounter;
                movingDownRail = tempMovingDownRail;
            }

            return csb;
        }
        
        private StringBuilder extractMessage (StringBuilder[] csb, int depth)
        {
            var dsb = new StringBuilder(csb[1].Length);
            var depthCounter = 0;
            var movingDownRail = false;

            for (int i = 0; i < csb[1].Length; i++)
            {
                if (csb[depthCounter][i] != '.')
                {
                    dsb.Append(csb[depthCounter][i].ToString());
                }

                var tempDepthCounter = getDepthCount(depthCounter, depth, movingDownRail);
                var tempMovingDownRail = getRailDirection(depthCounter, depth, movingDownRail);
                depthCounter = tempDepthCounter;
                movingDownRail = tempMovingDownRail;
            }

            return dsb;
        }

        private int getDepthCount(int depthCounter, int depth, bool movingDownRail)
        {
            
            //If it is the lowest rail
            if (depthCounter == depth -1)
            {
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
                //Increase rail depth by 1
                depthCounter++;
            }

            return depthCounter;
        }
        
        private bool getRailDirection(int depthCounter, int depth, bool movingDownRail)
        {
            //If it is the lowest rail
            if (depthCounter == depth -1)
            {
                //Swap to moving up
                movingDownRail = false;
            } 
            // If moving up rails and at top
            else if (!movingDownRail && depthCounter == 0)
            {
                //Set to moving down rails
                movingDownRail = true;
            }

            return movingDownRail;
        }
    }
}