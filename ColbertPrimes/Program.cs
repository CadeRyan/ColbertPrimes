using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            int startingPoint = 31000000;

            Task[] taskArray = new Task[5];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {
                    InputData data = obj as InputData;
                    ProcessBatch(data.index, data.startingPoint);
                }, new InputData(i, startingPoint));

                startingPoint += 200;
            }
            Task.WaitAll(taskArray);

            MergeFiles(GetFiles());


            Console.WriteLine("done");
            Console.ReadKey();
        }

        static string[] GetFiles()
        {
            DirectoryInfo dinfo = new DirectoryInfo(@"C:\Users\cader\Desktop\primes\");
            return dinfo.GetFiles("*.txt").Select(f => f.FullName).ToArray();
        }

        static void MergeFiles(string[] files)
        {
            using (StreamWriter sw = File.CreateText(@"C:\Users\cader\Desktop\primes\possiblePrimes.txt"))
            {
                foreach (var filename in files)
                {
                    Console.WriteLine(filename);
                    using (StreamReader sr = File.OpenText(filename))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            sw.WriteLine(s);
                        }
                    }
                }
            }
        }

        static void ProcessBatch(int i, int startingPoint)
        {
            int n = startingPoint;
            BigInteger twoN = BigInteger.Pow(2, n);
            Console.WriteLine("Starting...");
            double composite = 0;
            List<int> possiblePrimes = new List<int>();

            for (int count = 0; count < 200; count++)
            {
                BigInteger proth = new BigInteger();
                int k = 21181;

                proth = k * twoN + 1;

                if (IsPossiblyPrime(proth))
                {
                    possiblePrimes.Add(n);
                }
                else
                {
                    composite++;
                }
                twoN *= 2;
                n++;
            }

            string path = $@"C:\Users\cader\Desktop\primes\possiblePrimes{i}.txt";
            TextWriter tw = new StreamWriter(path);

            foreach (int s in possiblePrimes)
            {
                tw.WriteLine(s);
            }
            tw.Close();
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
    class InputData
    {
        public int index;
        public int startingPoint;

        public InputData(int i, int _startingPoint)
        {
            index = i;
            startingPoint = _startingPoint;
        }
    }
}
