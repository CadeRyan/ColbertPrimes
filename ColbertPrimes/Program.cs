using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<Task> tasks = new List<Task>();

            for(int n = 13160; n < 13170; n ++)
            {
                object arg = n;
                var task = new TaskFactory().StartNew(new Action<object>((test) =>
                {
                    BigInteger proth = new BigInteger();
                    BigInteger k = 5;

                    //PrimeChecker pc = new PrimeChecker();
                    Console.WriteLine((int)test);
                    proth = k * BigInteger.Pow(2, (int)test) + 1;
                    if (proth.IsProbablyPrime())
                    {
                        //SendMeAnEmail(k.ToString(), n.ToString());
                        Console.WriteLine("Prime!   " + (int)test);
                    }
                    Console.WriteLine("Thread done");
                }), arg);

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            stopWatch.Stop();

            Console.WriteLine(stopWatch.ElapsedMilliseconds);
            Console.ReadKey();
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
