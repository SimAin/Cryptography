using System;
using System.Collections.Generic;
using cryptography.Models;
using cryptography.Services;

namespace cryptography.Calculations
{
    public class Calculations
    {
        static List<string> outputString = new List<string>();
        public void run()
        {
            var exit = false;
            do
            {
                printMenu();
                var option = Console.ReadLine();
                if (UserInteractionService.validateOption(option, new[] {1, 2, 3, 4, 9}))
                {
                    var optionValue = int.Parse(option);
                    switch (optionValue)
                    {
                        case 1:
                            int n = UserInteractionService.inputIntKey();
                            eulersTotientFunction(n);
                            break;
                        case 2:
                            var r = gcd(UserInteractionService.inputIntKey(), UserInteractionService.inputIntKey());
                            Console.WriteLine("Result: " + r);
                            break;
                        case 3:
                            outputString = new List<string>();
                            var a = UserInteractionService.inputIntKey();
                            var b = UserInteractionService.inputIntKey();
                            EeaResult result = extendedEuclidAlgorithm(a, b);
                            displayEeaResults(a, b, result);
                            break;
                        case 4:
                            Console.WriteLine("Result: " +
                                              mod(UserInteractionService.inputIntKey(), UserInteractionService.inputIntKey()));
                            break;
                        case 9:
                            exit = true;
                            break;
                    }
                }
            } while (!exit);
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
                var gcd = Calculations.gcd(n, k);
                Console.WriteLine("n: " + n + " k: " + k + " gcd:" + gcd);
                
                if (gcd == 1)
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
            Console.WriteLine("4. Modulo calculator ");
            Console.WriteLine("9. Exit");
        }

        private static void displayEeaResults(int a, int b, EeaResult result)
        {
            Console.WriteLine("");
            Console.WriteLine("Calculation table: ");
            outputString.Reverse();
            foreach (var s in outputString)
            {
                Console.WriteLine(s);
            }
            
            Console.WriteLine("");
            Console.WriteLine("Result d: " + result.d);
            Console.WriteLine("Result x: " + result.x);
            Console.WriteLine("Result y: " + result.y);
            Console.WriteLine("ExtendedEuclid(" + a + "," + b + ") = " + result.d + " = (" + a + " x (" + 
                                            result.x + ")) + (" + b + " x " + result.y + ")");
        }
    }
}