using System;
using System.Collections.Generic;
using cryptography.Models;

namespace cryptography.Calculations
{
    public class Calculations
    {
        static List<string> outputString = new List<string>();
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
                        var r = gcd(sharedLib.inputIntKey(), sharedLib.inputIntKey());
                        Console.WriteLine("Result: " + r);
                        break;  
                    case 3:
                        outputString = new List<string>();
                        var a = sharedLib.inputIntKey();
                        var b = sharedLib.inputIntKey();
                        EeaResult z = extendedEuclidAlgorithm(a,b);
                        
                        Console.WriteLine("");
                        Console.WriteLine("Calculation table: ");
                        outputString.Reverse();
                        foreach (var s in outputString)
                        {
                            Console.WriteLine(s);
                        }
                        
                        Console.WriteLine("");
                        Console.WriteLine("Result d: " + z.d);
                        Console.WriteLine("Result x: " + z.x);
                        Console.WriteLine("Result y: " + z.y);
                        Console.WriteLine("ExtendedEuclid(" + a + "," + b + ") = " + z.d + " = (" + a + " x (" + 
                                            z.x + ")) + (" + b + " x " + z.y + ")");
                        break;  
                    case 9:
                        break;
                }
            }
        }
        
        private static EeaResult extendedEuclidAlgorithm(int a, int b)
        {
            var eea = new EeaResult(0,1,1);
            
            if (a == 0)
            {
                eea.x = 0;
                eea.y = 1;
                return new EeaResult(a,1,0);
            }
 
            eea = extendedEuclidAlgorithm(b % a, a);
 
            var q = b/a;
            if (q != 0)
            {
                var d = gcd(a, b);

                var ty = eea.y;
                var tx = eea.x;
                
                eea.d = d;
                eea.y = tx - (b/a) * ty;
                eea.x = ty;
                
                outputString.Add("a: " + b + " - b: " + mod(a, b) + " - [a/b]: " + q + " - d: "
                                                               + d + " - x: " + eea.x + " - y:" + eea.y);
                return eea;
            }

            return eea;
        }

        private void eulersTotientFunction(int n)
        {
            var count = 0;
            
            for (int k = 1; k < n; k++)
            {
                if (gcd(n, k) == 1)
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

        private static int gcd(int x, int y)
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