using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace KattisSolution
{
    internal class Program
    {
        public static readonly HashSet<long> tab = new HashSet<long>();

        private static void Main(string[] args)
        {

           Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            var scanner = new OptimizedPositiveIntReader(stdin);
            var writer = new BufferedStdoutWriter(stdout);
            for (long i = 1; i < 50000; i++)
            {
                tab.Add(i * (i + 1));
            }


            var p = scanner.NextLong();
            var q = scanner.NextLong();

            while (q != 0)
            {
                var result = Calculate(p, q);
                writer.Write(result);
                writer.Write("\n");
                p = scanner.NextLong();
                q = scanner.NextLong();
            }

            writer.Flush();
        }

        public static string Calculate(long p, long q)
        {
            if (p == 0)
                return "0 2";

            var gcd = (long)BigInteger.GreatestCommonDivisor(p, q);
            p = p / gcd;
            q = q / gcd;

            foreach (var l in tab)
            {
                if(l % q != 0)
                    continue;

                var x = l / q;
                var newp = p * x;
                if (tab.Contains(newp))
                {
                    var redS = (long)Math.Ceiling(Math.Sqrt(newp));
                    var blackS = (long)Math.Ceiling(Math.Sqrt(l));

                    return redS + " " + (blackS-redS);
                }
            }

            return "impossible";
        }
    }

    #region IO
    public class OptimizedPositiveIntReader
    {
        private readonly StreamReader _reader;

        public OptimizedPositiveIntReader(Stream inStream)
        {
            _reader = new StreamReader(inStream);
        }

        public OptimizedPositiveIntReader(StreamReader inStream)
        {
            _reader = inStream;
        }

        public int NextInt()
        {
            int c, result = 0;
            var isInt = false;
            while (!_reader.EndOfStream)
            {
                c = _reader.Read();

                while (c >= '0' && c <= '9')
                {
                    result = result * 10 + c - '0';
                    c = _reader.Read();
                    isInt = true;
                }

                if (isInt)
                    return result;
            }

            throw new Exception();
        }

        public long NextLong()
        {
            long c, result = 0;
            var isInt = false;
            while (!_reader.EndOfStream)
            {
                c = _reader.Read();

                while (c >= '0' && c <= '9')
                {
                    result = result * 10 + c - '0';
                    c = _reader.Read();
                    isInt = true;
                }

                if (isInt)
                    return result;
            }

            throw new Exception();
        }

    }

    public class BufferedStdoutWriter : StreamWriter
    {
        public BufferedStdoutWriter(Stream outStream)
            : base(new BufferedStream(outStream))
        {
        }
    }

    #endregion
}