using System;
using System.Collections.Generic;
using System.IO;
using KattisSolution.IO;

namespace KattisSolution
{
    public class mycomp : IComparer<string>
    {
        public int Compare(string x, string y)
        {
           var r = x[0] - y[0] == 0 ? x[1]-y[1] : x[0] - y[0];
            return r == 0 ? 1 : r;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            IScanner scanner = new LineReader(stdin);
            // uncomment when you need more advanced reader
            // IScanner scanner = new Scanner(stdin);
            // IScanner scanner = new LineReader(stdin);
            var writer = new BufferedStdoutWriter(stdout);

            int n = 0;
            int set = 0;

            while ((n = scanner.NextInt()) != 0)
            {
                if(set!=0)
                    writer.Write("\n");
                var arr = new SortedSet<string>(new mycomp());
                for (int i = 0; i < n; i++)
                {
                    arr.Add(scanner.Next());
                }

                foreach (var s in arr)
                {
                    writer.Write(s+"\n");
                }
                set++;

            }

            writer.Flush();
        }
    }
}