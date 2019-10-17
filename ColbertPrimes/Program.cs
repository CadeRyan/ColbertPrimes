using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Threading.Tasks;

namespace ColbertPrimes
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 31100000; //update value for n as starting value
            BigInteger twoN = BigInteger.Pow(2, n);
            Console.WriteLine("Starting...");
            double composite = 0;
            List<int> possiblePrimes = new List<int>();

            for (int count = 0; count < 1000; count ++)
            {
                BigInteger proth = new BigInteger();
                int k = 21181;

                Stopwatch watch = new Stopwatch();

                watch.Start();
                proth = k * twoN + 1;
                watch.Stop();
                Console.WriteLine("Time to calculate proth number: " + watch.ElapsedMilliseconds);


                watch.Restart();
                if (IsPossiblyPrime(proth))
                {
                    Console.WriteLine("Possible Prime");
                    possiblePrimes.Add(n);
                    //write number to a file
                }
                else
                {
                    Console.WriteLine("Not Prime");
                    composite++;
                }
                watch.Stop();
                Console.WriteLine("Time to evalute possible primality: " + watch.ElapsedMilliseconds);

                // do something with the list of n's

                twoN *= 2;
                n++;
            }

            string path = @"C:\Users\cader\Desktop\possiblePrimes.txt";
            TextWriter tw = new StreamWriter(path);

            foreach (int s in possiblePrimes)
            {
                Console.WriteLine(s);
                tw.WriteLine(s);
            }

            tw.Close();

            Console.WriteLine(n);

            Console.WriteLine("Percentage eliminated: " + composite/1000);
            Console.ReadKey();
        }

        static bool IsPossiblyPrime(BigInteger proth)
        {
            for(int i = 0; i < Primes.smallPrimes.Length; i++)
            {
                if(proth % Primes.smallPrimes[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void SendMeAnEmail(string k, string n)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("prothprimenotifier@gmail.com", "14nth.rpqowieury"),
                EnableSsl = true
            };
            client.Send("prothprimenotifier@gmail.com", "ryanc45@tcd.ie", "Found a Prime!", $"{k} * 2^{n} + 1 is Prime!");
        }
    }
}
