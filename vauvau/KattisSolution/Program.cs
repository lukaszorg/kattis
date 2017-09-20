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

            var dog1Aggresive = scanner.NextInt();
            var dog1Calm = scanner.NextInt();

            var dog2Aggresive = scanner.NextInt();
            var dog2Calm = scanner.NextInt();

            var postman = scanner.NextInt();
            var milkman = scanner.NextInt();
            var garbageman = scanner.NextInt();


            GetValue(postman, dog1Aggresive, dog1Calm, dog2Aggresive, dog2Calm, writer);
            GetValue(milkman, dog1Aggresive, dog1Calm, dog2Aggresive, dog2Calm, writer);
            GetValue(garbageman, dog1Aggresive, dog1Calm, dog2Aggresive, dog2Calm, writer);

            writer.Flush();
        }

        private static void GetValue(int postman, int dog1Aggresive, int dog1Calm, int dog2Aggresive, int dog2Calm, StreamWriter writer)
        {
            var aa = postman % (dog1Aggresive + dog1Calm);
            var bb = postman % (dog2Aggresive + dog2Calm);
            var a = aa <= dog1Aggresive && aa != 0 ? 1 : 0;
            var b = bb <= dog2Aggresive &&  bb != 0 ? 1 : 0;
            if (a+b == 2)
                writer.Write("both");
            else if(a+b==1)
                writer.Write("one");
            else
                writer.Write("none");

            writer.Write("\n");

        }
    }
}