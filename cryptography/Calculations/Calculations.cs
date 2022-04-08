using System;

namespace cryptography.Calculations
{
    public class Calculations
    {
        public void run()
        {
            printMenu();
            var option = Console.ReadLine();
            if (sharedLib.validateOption(option, new[] {1,2,3,9})){
                var optionValue = int.Parse(option);
                switch (optionValue)
                {
                    case 1:
                        int n = sharedLib.inputIntKey();
                        eulersTotientFunction(n);
                        break; 
                    case 2:
                        var r = greatestCommonDivide(sharedLib.inputIntKey(), sharedLib.inputIntKey());
                        Console.WriteLine("Result: " + r);
                        break;  
                    case 3:
                        int z = extendedEuclidAlgorithm(sharedLib.inputIntKey(),sharedLib.inputIntKey(), 1, 1);
                        Console.WriteLine("Result: " + z);
                        break;  
                    case 9:
                        break;
                }
            }
        }
        
        private static int extendedEuclidAlgorithm(int a, int b, int x, int y)
        {
            Console.WriteLine("a: " + a + " - b: " + b + " - [a/b]: " + "n/a" + " - d: " + "n/a" + " - x: " + x + " - y:" + y);
            // Base Case
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
 
            // To store results of
            // recursive call
            int x1 = 1, y1 = 1;
            int gcd = extendedEuclidAlgorithm(b % a, a, x1, y1);
 
            // Update x and y using
            // results of recursive call
            x = y1 - (b / a) * x1;
            y = x1;
            Console.WriteLine("a: " + a + " - b: " + b + " - [a/b]: " +  b/a + " - d: " + greatestCommonDivide(a,b) + " - x: " + x + " - y:" + y);
 
            return gcd;
        }

        private void eulersTotientFunction(int n)
        {
            var count = 0;
            
            for (int k = 1; k < n; k++)
            {
                if (greatestCommonDivide(n, k) == 1)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }
        
        private static int mod(int x, int m) {
            var r = x%m;
            return r<0 ? r+m : r;
        }

        private static int greatestCommonDivide(int x, int y)
        {
            return euclidAlgorithm(x, y);
        }

        private static int euclidAlgorithm(int a, int b)
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
            Console.WriteLine("3. Extended Euclid's algorithm ");
            Console.WriteLine("9. Exit");
        }
    }
}