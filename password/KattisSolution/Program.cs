using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using KattisSolution.IO;

namespace KattisSolution
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            IScanner scanner = new Scanner(stdin);
            var writer = new BufferedStdoutWriter(stdout);

            var list = new List<BigInteger>();

            var count = scanner.NextInt();
            BigInteger total = new BigInteger(0);
            for (uint i = 0; i < count; i++)
            {
                string password = scanner.Next();
                list.Add(ToInt(scanner.Next()));
            }
            list.Sort();
            list.Reverse();
            for (int i = 0; i < list.Count; i++)
            {
                var multiplier = new BigInteger(i + 1);
                total += multiplier * list[i];
            }

            writer.Write(format(total));
            writer.Write("\n");
            writer.Flush();
        }

        private static BigInteger ToInt(string s)
        {
            if (s[0] == '1')
                return 10000;

            BigInteger result = 0;
            for (int i = 2; i < s.Length; i++)
            {
                result = result * 10;
                result += s[i] - '0';
            }

            for (int i = 0; i < 6-s.Length; i++)
            {
                result *= 10;
            }

            return result;
        }

        private static string format(BigInteger number)
        {
            string s = number.ToString();
            return s.Insert(s.Length - 4, ".");
            
        }
    }
}