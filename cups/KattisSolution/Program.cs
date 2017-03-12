using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //IScanner scanner = new OptimizedPositiveIntReader(stdin);
            // uncomment when you need more advanced reader
            IScanner scanner = new Scanner(stdin);
            // IScanner scanner = new LineReader(stdin);
            var writer = new BufferedStdoutWriter(stdout);
            var n = scanner.NextInt();

            var list = new Dictionary<int, string>();

            for (int i = 0; i < n; i++)
            {
                var pierwszy = scanner.Next();
                var drugi = scanner.Next();

                if (pierwszy[0] > 'a' && pierwszy[0] < 'z')
                {
                    list.Add( int.Parse(drugi)*2 , pierwszy);
                }
                else
                {
                    list.Add(int.Parse(pierwszy), drugi);
                }
            }

            foreach (var keyValuePair in list.OrderBy(pair => pair.Key))
            {
                writer.Write(keyValuePair.Value);
                writer.Write("\n");
            }

            writer.Flush();
        }

        private static int ToInt(string s)
        {
            int result = 0;
            for (int i = 0; i < s.Length ; i++)
            {
                    result = result * 10 + s[i] - '0';
            }

            return result;
        }
    }
}