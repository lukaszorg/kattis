using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KattisSolution.IO;

namespace KattisSolution
{

    class Corner
    {

        public int x;
        public int y;

        public Corner(int a, int b)
        {
            x = a;
            y = b;
        }

        public String toString()
        {
            return x + " " + y;
        }

    }

    internal class Program
    {
        public static double angleVector(double x1, double y1, double x2, double y2)
        {

            return Math.Acos(((x1 * x2 + y1 * y2) / (Math.Sqrt(x1*x1+y1*y1) * Math.Sqrt(x2*x2+ y2*y2))));
        }

        public static void update(List<Corner> c, List<double> angles)
        {

            angles.Clear();

            angles.Add(anglePoints(c[c.Count - 1], c[0], c[1]));

            for (int i = 1; i < c.Count; i++)
                angles.Add(anglePoints(c[i - 1], c[i], c[(i + 1) % c.Count]));
        }

        public static double anglePoints(Corner a, Corner b, Corner c)
        {
            int x1 = a.x;
            int y1 = a.y;
            int x2 = b.x;
            int y2 = b.y;
            int x3 = c.x;
            int y3 = c.y;

            x1 -= x2;
            x3 -= x2;
            y1 -= y2;
            y3 -= y2;

            return angleVector(x1, y1, x3, y3);
        }


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


            while (true)
            {
                int n = scanner.NextInt();

                if (n == 0)
                    break;

                List<Corner> c = new List<Corner>(n);

                for (int i = 0; i < n; i++)
                    c.Add(new Corner(scanner.NextInt(), scanner.NextInt()));

                List<double> angles = new List<double>(n);

                update(c, angles);

                double uniMin = 0;
                Corner lastRemoved = new Corner(0, 0);
                int lastMinIndex = 0;

                while (true)
                {
                    double min = Double.MaxValue;
                    int minIndex = 0;

                    for (int i = 0; i < angles.Count; i++)
                        if (angles[i] < min)
                        {
                            min = angles[i];
                            minIndex = i;
                        }

                    if (min < uniMin)
                    {
                        c.Insert(lastMinIndex, lastRemoved);
                        break;
                    }

                    if (angles.Count == 3)
                        break;

                    lastRemoved = c[minIndex];
                    c.RemoveAt(minIndex);

                    uniMin = min;
                    lastMinIndex = minIndex;

                    update(c, angles);
                }

                writer.Write(c.Count + " " + string.Join(" ", c.Select(corner =>corner.x + " " + corner.y ) )+"\n");
            }

            writer.Flush();
        }
    }
}