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
            var writer = new BufferedStdoutWriter(stdout);

            var input = scanner.NextInt();

            writer.Write(input*(input-1)*(input-2)*(input-3)/24);
            writer.Write("\n");
            writer.Flush();
        }
    }
}