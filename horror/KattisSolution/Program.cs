using System;
using System.Collections.Generic;
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

            var n = scanner.NextInt();
            var h = scanner.NextInt();
            var l = scanner.NextInt();

            var hi = new int [n];
            var connection = new Dictionary<int, HashSet<int>>(n);

            for (int i = 0; i < n; i++)
            {
                hi[i] = int.MaxValue-1;
                connection[i] = new HashSet<int>();
            }

            Queue<int> toProcess= new Queue<int>();

            for (int i = 0; i < h; i++)
            {
                var movieIndex = scanner.NextInt();
                hi[movieIndex] = 0;
                toProcess.Enqueue(movieIndex);
            }

            for (int i = 0; i < l; i++)
            {
                var m1 = scanner.NextInt();
                var m2 = scanner.NextInt();
                connection[m1].Add(m2);
                connection[m2].Add(m1);
            }

            while (toProcess.Count > 0)
            {
                var movie = toProcess.Dequeue();
                var connections = connection[movie];

                foreach (var other in connections)
                {
                    hi[other] = Math.Min(hi[movie] + 1, hi[other]);
                    connection[other].Remove(movie);
                    if(connection[other].Count > 0)
                        toProcess.Enqueue(other);
                }
                connections.Clear();

            }

            int maxi = 0;
            int max = hi[0];

            for (int i = 0; i < n; i++)
            {
                if (hi[i] > max)
                {
                    maxi = i;
                    max = hi[i];
                }

            }

            writer.Write(maxi);
            writer.Write("\n");
            writer.Flush();
        }
    }
}