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
            var x = scanner.NextInt();
            var y = scanner.NextInt();
            var x2 = 0.0;
            var y2 = 0.0;
            if (x == 0)
            {
                if (y < 125)
                {
                    x2 = 15625.0 * 2 / (250 - y);
                    y2 = 250 - x2;
                } 
               else if (y > 125)
                {
                    x2 = 15625.0 * 2 / y;
                    y2 = 0;
                }
                else
                {
                    x2 = 250;
                    y2 = 0;
                }
            }
            else if (y == 0)
            {
                if (x < 125)
                {
                    y2 = 15625.0 * 2 / (250 - x);
                    x2 = 250 - y2;
                }

                else if (x > 125)
                {
                    y2 = 15625.0 * 2 / x;
                    x2 = 0;
                }
                else
                {
                    x2 = 0;
                    y2 = 250;
                }

            }
            else
            {
                if (x > 125)
                {
                    y2 = 250.0 - 15625.0 * 2 / x;
                    x2 = 0;
                }
                else if (y > 125)
                {
                    x2 = 250.0 - 15625.0 * 2 / y;
                    y2 = 0;
                }
                else
                {
                    x2 = 0;
                    y2 = 0;
                }
               
            }

            writer.Write(x2.ToString("0.00")+ " " + y2.ToString("0.00"));
            writer.Write("\n");
            writer.Flush();
        }
    }
}