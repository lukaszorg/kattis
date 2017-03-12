using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KattisSolution
{
    class TestGenerator
    {
        public static readonly HashSet<long> tab = new HashSet<long>();

        private static void VerifyTests()
        {
            for (long i = 1; i < 50000; i++)
            {
                tab.Add(i * (i + 1));
            }

            StreamReader input = File.OpenText("redsocks2.in");
/*            Stream ans = File.OpenRead("redsocks2.ans");
            var scanner = new OptimizedPositiveIntReader(input);
            var ansScanner = new LineReader(ans);
            int j = 0;
            while (scanner.HasNext())
            {
                j++;
                var p = scanner.NextLong();
                var q = scanner.NextLong();

                var result = Program.Calculate(p, q);
                var ans1 = ansScanner.Next();

                if (result != ans1)
                {
                    Console.WriteLine(p + " " + q);
                }
            }*/

            Console.WriteLine("end");
            Console.ReadLine();
        }

        private static void GeberateTests()
        {
            var input = File.OpenWrite("redsocks2.in");
            var answers = File.OpenWrite("redsocks2.ans");
            var answerWriter = new StreamWriter(answers);
            var inputWriter = new StreamWriter(input);

            var odpowiedzi = new HashSet<Tuple<long, long>>();

            for (long total = 2; total <= 50000; total++)
            {
                for (long red = 0; red <= total; red++)
                {
                    if (red == 0 || red == 1)
                    {
                        answerWriter.Write("0 2");
                        inputWriter.Write("0 4");
                    }
                    else
                    {
                        var licznik = red * (red - 1);
                        var mianownik = (total) * (total - 1);
                        var gcd = (long)BigInteger.GreatestCommonDivisor(licznik, mianownik);
                        var t = Tuple.Create(licznik / gcd, mianownik / gcd);
                        if (odpowiedzi.Contains(t))
                        {
                            continue;
                        }

                        odpowiedzi.Add(t);
                        inputWriter.Write(t.Item1 + " " + t.Item2 + "\n");
                        answerWriter.Write(red + " " + (total - red) + "\n");
                    }

                }
                Console.WriteLine("total: " + total);
            }

            answerWriter.Flush();
            inputWriter.Flush();
            input.Close();
            answers.Close();

            Console.WriteLine("done");
            Console.ReadLine();
        }

    }
}
