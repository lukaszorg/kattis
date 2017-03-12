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
            var m = scanner.NextInt();

            _timeTable = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    _timeTable[i, j] = scanner.NextInt();
                }
            }

            var stages = new int[m];

            _pending = new List<Status>();
            for (int i = 0; i < n; i++)
            {
                _pending.Add(new Status());
            }

            stages[0] = _timeTable[0, 0];
            _pending[0].Stage = 0;
            _pending[0].TimeLeft = _timeTable[0, 0];

           // var finished = 0;
           // var totalTime = 0;
           // var maxStage = 0;

/*            while (finished < n)
            {
                var time = GetMin(stages);
                totalTime += time;

                for (int i = 0; i < stages.Length; i++)
                {
                    stages[i] = Math.Max(0, stages[i] - time);
                }

                for (int i = finished; i < _pending.Count; i++)
                {
                    _pending[i].Wait(time);
                    if (_pending[i].TimeLeft == 0 && _pending[i].Stage == m-1)
                    {
                        _pending[i].TotalTime = totalTime;
                        finished++;
                    }
                    else if (_pending[i].TimeLeft == 0 && NexStageIsFree(stages, _pending[i].Stage))
                    {
                        var nextStage = _pending[i].Stage + 1;
                        if (nextStage > maxStage)
                            maxStage = nextStage;

                        _pending[i].Stage = nextStage;
                        _pending[i].TimeLeft = _timeTable[i, nextStage];
                        stages[nextStage] = _timeTable[i, nextStage];
                    }
                }
            }*/



            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    var time = _timeTable[i, j];
                    var left = (j - 1 < 0) ? 0 : _timeTable[i, j - 1];
                    var up = (i - 1 < 0) ? 0 : _timeTable[i-1, j];
                    var add = Math.Max(left, up);

                    _timeTable[i, j] += add;
                }
            }
/*
            var times = new int[n];
            var maxN = 0;
            var maxM = 0;
            var next = _timeTable[0, 0];

            var elapsed = _timeTable[0, 0];
            times[0] += _timeTable[0, 0];

            while (true)
            {
                for (int i = 0; i <= maxM; i++)
                {
                    for (int j = 0; j < maxN; j++)
                    {
                        
    
                    }
                }
            }
*/
            for (int i = 0; i < n; i++)
            {
                writer.Write(_timeTable[i,m-1]);
                if(i<n-1)
                    writer.Write(" ");
            }
            //var output = string.Join(" ", _timeTable.Select(status => status.TotalTime));
           // writer.Write(output);
            writer.Write("\n");
            writer.Flush();
        }

        private static bool NexStageIsFree(int[] stages, int i)
        {
            return stages[i+1] == 0;
        }

        private static int GetMin(int[] stages)
        {
            var min = int.MaxValue;
            for (int i = 0; i < stages.Length; i++)
            {
                if (stages[i] < min && stages[i] > 0)
                {
                    min = stages[i];
                }
            }

            return min;
        }

        private static List<Status> _pending;
        private static int[,] _timeTable;
    }

    public class Status
    {
        public int Stage = -1;
        public int TimeLeft = 0;
        public int TotalTime = 0;

        public void Wait(int time)
        {
            TimeLeft = Math.Max(0, TimeLeft - time);
        }
    }
}