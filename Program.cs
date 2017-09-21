using System;
using System.IO;
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
            IScanner scanner = new OptimizedPositiveIntReader(stdin);
            // uncomment when you need more advanced reader
            // IScanner scanner = new Scanner(stdin);
            // IScanner scanner = new LineReader(stdin);
            var writer = new BufferedStdoutWriter(stdout);

            var n = scanner.NextInt();
            var c = new int[n+1]; //COST OD DOOR i to i+1
            var m = new int[n+1]; //MINIMAL COST BEFORE  DOOR N
            for (int j = 1; j <= n - 1; j++)
            {
                c[j] = scanner.NextInt();
                if (j == 1)
                    m[j] = c[j];
                else m[j] = Math.Min(m[j - 1], c[j]);
            }

            var t = new int[n];
            for (int j = 0; j < n; j++)
            {
                t[j] = scanner.NextInt();
            }

            int tt = 0;           //current time
            int i = 0;
            long ret = 0;
            while (i < n-1)
            {
                i++;
                tt++;
                ret += c[i];
                int tmp = t[i] - tt;                   //how long left gate open

                while (tmp > 0)
                {
                    tmp -= 2;
                    ret += m[i] * 2;
                    tt += 2;
                }
            }

            writer.Write(ret+"\n");
            writer.Flush();
        }

    }
}