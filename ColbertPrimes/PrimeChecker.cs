using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ColbertPrimes
{
    class PrimeChecker
    {
        public bool IsPrime(BigInteger proth)
        {
            int[] smallPrimes = { 2, 3, 5, 7, 11, 13 };
            BigInteger exp = (proth - 1) / 4;
            BigInteger remainderTest = -1 % proth;
            BigInteger remainderTest2 = 1 % proth;

            foreach (int a in smallPrimes)
            {
                BigInteger remainder = BigInteger.ModPow(a, exp, proth);
                if (remainder == remainderTest || remainder == remainderTest2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
