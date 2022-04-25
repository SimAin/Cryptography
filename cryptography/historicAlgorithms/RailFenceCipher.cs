using System;
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
            if (SharedLib.validateOption(option, new[] {1,2,9})){
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
                }
            }
        }

        /// <summary>
        /// Takes key, reads input file and produces ciphertext.
        /// </summary>
        /// <param name="depth"></param>
        private void encode (int depth) {
            var fileString = File.ReadAllText("files/input.txt");
            var output = replaceValsE(fileString, depth);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/output.txt", output);
        }

        /// <summary>
        /// Takes key, reads output file and produces plaintext.
        /// </summary>
        /// <param name="depth"></param>
        private void decode (int depth) {
            var fileString = File.ReadAllText("files/output.txt");
            var output = replaceValsD(fileString, depth);
            Console.WriteLine(output);
            File.WriteAllTextAsync("files/decoded.txt", output);
        }

        /// <summary>
        /// Takes plaintext and key and transposes the values to return ciphertext
        /// </summary>
        /// <param name="fileString"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private string replaceValsE(string fileString, int depth)
        {
            var osb = new StringBuilder(fileString);
            var csb = new StringBuilder[depth];
            
            var charList = osb.ToString().ToLower().ToCharArray();

            //Create each rail.
            for (int i = 0; i < depth; i++)
            {
                csb[i] = new StringBuilder();
            }

            //Get rails populated with correct strings from input.
            var csb2 = substituteValsIntoRailStructure(charList, csb, depth, true);

            var output = "";
            //Create ciphertext but concatenating rails.  
            foreach (var rail in csb2)
            {
                Console.WriteLine(rail.ToString());
                output += rail;
            }
            
            return output;
        }
        
        /// <summary>
        /// Takes ciphertext and key and transposes the values to return plaintext
        /// </summary>
        /// <param name="fileString"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private string replaceValsD(string fileString, int depth)
        {
            StringBuilder osb = new StringBuilder(fileString);
            var charList = osb.ToString().ToLower().ToCharArray();
            StringBuilder[] csb = createRailStructureForDecoding(charList.Length, depth); 
            
            var counter = 0;
            //Where a '_' is found, replace with corralling char from ciphertext. 
            foreach (var rail in csb)
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
            
            //Output rails with chars for clarity. 
            foreach (var p in csb)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine("");

            return extractMessage(csb,depth).ToString();
        }

        /// <summary>
        /// Creates structure and placement of placeholders for decoding process.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private StringBuilder[] createRailStructureForDecoding(int length, int depth)
        {
            StringBuilder[] csb = new StringBuilder[depth];
            var tempCharList = new char[length];

            //Create temp char array of '_' to build rail structure
            //This will allow correct characters to be found and replaced later.
            for (int i = 0; i < length; i++)
            {
                tempCharList[i] = '_';
            }
            
            //Populate all chars in all elements of csb array with '.' placeholder.
            for (int i = 0; i < depth; i++)
            {
                csb[i] = new StringBuilder();
                var charCounter = 0;
                do
                {
                    csb[i].Append('.');
                    charCounter++;
                } while (charCounter<length);
            }

            //Create rail structure of '.' and '_' of correct size.
            var csb2 = substituteValsIntoRailStructure(tempCharList, csb, depth, false);
            return csb2;
        }

        /// <summary>
        /// Iterate through charList input and substitute each value into correct place within rail structure.
        /// </summary>
        /// <param name="charList"></param>
        /// <param name="csb"></param>
        /// <param name="depth"></param>
        /// <param name="encode"></param>
        /// <returns>Rail structure with substituted input</returns>
        private StringBuilder[] substituteValsIntoRailStructure(char[] charList, StringBuilder[] csb, int depth, bool encode)
        {
            var depthCounter = 0;
            var movingDownRail = false;

            for (int i = 0; i < charList.Length; i++)
            {
                //If encoding, just add chars to correct rail.
                if (encode)
                {
                    csb[depthCounter].Append(charList[i].ToString());
                }
                //If decoding, replace correct values in structure with values in charList.
                else
                {
                    csb[depthCounter].Remove(i, 1);
                    csb[depthCounter].Insert(i,charList[i].ToString());
                }
                
                //Calculate current rail depth and direction within iteration.
                var tempDepthCounter = getDepthCount(depthCounter, depth, movingDownRail);
                var tempMovingDownRail = getRailDirection(depthCounter, depth, movingDownRail);
                depthCounter = tempDepthCounter;
                movingDownRail = tempMovingDownRail;
            }

            return csb;
        }
        
        /// <summary>
        /// Given a rail structure with correct plaintext values, extract plaintext.
        /// </summary>
        /// <param name="csb"></param>
        /// <param name="depth"></param>
        /// <returns>Plaintext</returns>
        private StringBuilder extractMessage (StringBuilder[] csb, int depth)
        {
            var dsb = new StringBuilder(csb[1].Length);
            var depthCounter = 0;
            var movingDownRail = false;

            for (int i = 0; i < csb[1].Length; i++)
            {
                //If value is not placeholder '.' append value to output string.
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

        /// <summary>
        /// Given current depth, count and direction, return next rail depth count.
        /// </summary>
        /// <param name="depthCounter"></param>
        /// <param name="depth"></param>
        /// <param name="movingDownRail"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Given current depth, count and direction, return next direction.
        /// </summary>
        /// <param name="depthCounter"></param>
        /// <param name="depth"></param>
        /// <param name="movingDownRail"></param>
        /// <returns></returns>
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