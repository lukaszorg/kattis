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

            var n = scanner.NextInt();
            var costs = new int[n+1]; //COST OD DOOR i to i+1
            var inimalCosts = new int[n+1]; //MINIMAL COST BEFORE  DOOR N
            var times = new int[n];

            for (int j = 1; j <= n - 1; j++)
            {
                costs[j] = scanner.NextInt();
                if (j == 1)
                    inimalCosts[j] = costs[j];
                else inimalCosts[j] = Math.Min(inimalCosts[j - 1], costs[j]);
            }
           
            for (int j = 0; j < n; j++)
            {
                times[j] = scanner.NextInt();
            }

            int currentTime = 0;           //current time
            int i = 0;
            long totalCost = 0;
            while (i < n-1)
            {
                i++;
                currentTime++;
                totalCost += costs[i];
                int nextGateOpen = times[i] - currentTime;                   

                while (nextGateOpen > 0)
                {
                    nextGateOpen -= 2;
                    totalCost += inimalCosts[i] * 2;
                    currentTime += 2;
                }
            }

            writer.Write(totalCost+"\n");
            writer.Flush();
        }

    }
}