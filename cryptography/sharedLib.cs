using System.Collections.Generic;
using System.Linq;

namespace cryptography
{
    public class sharedLib 
    {

        public static bool validateOption (string option, int[] acceptedValues) {
            int value;
            if (int.TryParse(option, out value))
            {
                if (acceptedValues.Contains(value)){
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public static List<int> GetCharIndexInString (string fullText, char val){
            var foundIndexes = new List<int>();
        
            // for loop end when i=-1 ('a' not found)
            for (int i = fullText.IndexOf(val); i > -1; i = fullText.IndexOf(val, i + 1))
            {
                foundIndexes.Add(i);
            }

            return foundIndexes;
        }
    }
}