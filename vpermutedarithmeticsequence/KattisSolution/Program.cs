using System;
using System.IO;
using KattisSolution.IO;

namespace KattisSolution
{
    internal class Program
    {
        private static BufferedStdoutWriter writer;

        private static void Main(string[] args)
        {
            Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        static bool arithmetic(int len, int[] arr)
        {
            int val = arr[1] - arr[0];
            for (int i = 2; i < len; i++)
            {
                if (val != arr[i] - arr[i - 1])
                    return false;
            }
            return true;
        }

        static void type(int len, int[] arr)
        {
            if (arithmetic(len, arr))
            {
                writer.Write("arithmetic\n");
                return;
            }
            //qsort(arr, (size_t)len, sizeof(int), comparator);
            Array.Sort(arr);
           // var l = new List<int>(arr);
            //l.Sort();
           
            if (arithmetic(len, arr))
                writer.Write("permuted arithmetic\n");
            else writer.Write("non-arithmetic\n");
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            IScanner scanner = new Scanner(stdin);
            // uncomment when you need more advanced reader
            // IScanner scanner = new Scanner(stdin);
            // IScanner scanner = new LineReader(stdin);
            writer = new BufferedStdoutWriter(stdout);

            int n = scanner.NextInt();
            while (n-- > 0)
            {
                int k = scanner.NextInt(); 

                var arr = new int[k];
                for (int i = 0; i < k; i++)
                    arr[i] = scanner.NextInt();
                type(k, arr);
            }


            writer.Flush();
        }

        private static bool CheckSequence(int[] array)
        {
            var diff = array[1] - array[0];
            for (int i = 2; i < array.Length; i++)
            {
                if (array[i] - array[i - 1] != diff)
                    return false;
            }

            return true;
        }

    }
}