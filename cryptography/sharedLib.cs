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
    }
}