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

            var fizz = scanner.NextInt();
            var buzz = scanner.NextInt();
            var n = scanner.NextInt();

            for (int i = 1; i <= n; i++)
            {
                if (i % fizz == 0) 
                    writer.Write("Fizz");
                if(i%buzz == 0)
                    writer.Write("Buzz");
                if(i % fizz != 0 && i % buzz != 0)
                    writer.Write(i.ToString());
                writer.Write("\n");
            }

            writer.Flush();
        }
    }
}