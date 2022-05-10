using System.Collections.Generic;

namespace cryptography.Services
{
    public class StringOperationsService
    {
        
        /// <summary>
        /// Return list of index's in which a given char is found within a given string.
        /// </summary>
        /// <param name="fullText"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static List<int> getCharIndexInString (string fullText, char val){
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