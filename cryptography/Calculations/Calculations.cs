using System;

namespace cryptography.Calculations
{
    public class Calculations
    {
        public void run()
        {
            printMenu();
            var option = Console.ReadLine();
            if (sharedLib.validateOption(option, new int[] {1,2,9})){
                var optionValue = int.Parse(option);
                switch (optionValue)
                {
                    case 1:
                        int n = sharedLib.inputIntKey();
                        eulersTotientFunction(n);
                        break; 
                    case 2:
                        int a = sharedLib.inputIntKey();
                        int b = sharedLib.inputIntKey();
                        int r = greatestCommonDivide(a, b);
                        Console.WriteLine("Result: " + r);
                        break;  
                    case 9:
                        break;
                }
            }
        }

        private void eulersTotientFunction(int n)
        {
            int count = 0;
            
            for (int k = 1; k < n; k++)
            {
                int u = greatestCommonDivide(n, k);
                if (u == 1)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }
        
        int mod(int x, int m) {
            int r = x%m;
            return r<0 ? r+m : r;
        }

        private int greatestCommonDivide(int x, int y)
        {
            return euclidAlgorithm(x, y);
        }

        private int euclidAlgorithm(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }

            return euclidAlgorithm(b, mod(a, b));
        }

        private static void printMenu(){
            Console.WriteLine("Select option: ");
            Console.WriteLine("1. Euler's totient function - ϕ(n)");
            Console.WriteLine("2. Greatest common divide GCD");
            Console.WriteLine("9. Exit");
        }
    }
    
}