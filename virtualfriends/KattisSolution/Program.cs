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
            IScanner scanner = new Scanner(stdin);
            var writer = new BufferedStdoutWriter(stdout);

            var testcases = scanner.NextInt();

            var dic = new Dictionary<string, int>();
            var groups = new List<HashSet<string>>(0);


            for (int i = 0; i < testcases; i++)
            {
                var pairs = scanner.NextInt();
                for (int j = 0; j < pairs; j++)
                {
                    var p1 = scanner.Next();
                    var p2 = scanner.Next();

                    int g1 = -1;
                    int g2 = -2;
                    if (!dic.TryGetValue(p1, out g1)) g1 = -1;
                    if (!dic.TryGetValue(p2, out g2)) g2 = -1;

                    if (g1 == -1 && g2 == -1)
                    {
                        groups.Add(new HashSet<string>(new[] {p1, p2}));
                        dic[p1] = dic[p2] = groups.Count - 1;
                    }
                    else if (g1 == -1)
                    {
                        dic[p1] = g2;
                        groups[g2].Add(p1);
                    }
                    else if (g2 == -1)
                    {
                        dic[p2] = g1;
                        groups[g1].Add(p2);
                    }
                    else if(g1 != g2)
                    {
                        var group2 = groups[g2];
                        dic[p2] = g1;
                        foreach (var name in group2)
                        {
                            dic[name] = g1;
                            groups[g1].Add(name);
                        }
                        group2.Clear();
                    }


                    var finalg = dic[p1];


                    writer.Write(groups[finalg].Count);

                    writer.Write("\n");
                }

                dic.Clear();
                groups.Clear();
            }

           
            writer.Flush();
        }
    }
}