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

            var left = new BinaryHeap<int>(100000, new MinComparer().Compare);
            var right = new BinaryHeap<int>(100000, new MaxComparer().Compare);


            while (scanner.HasNext())
            {
                var next = scanner.Next();

                if (next == "#")
                {
                    int cookie;
                    if (left.Size > right.Size)
                    {
                        cookie = left.Pop();
                    }
                    else
                    {
                        cookie = right.Pop();
                    }

                    writer.Write(cookie + "\n");
                }
                else
                {
                    var d = int.Parse(next);
                    if (left.Size == 0)
                    {
                        left.Insert(d);
                        continue;
                    }


                    var maxleft = left.Peak();

                    if (d <= maxleft)
                        left.Insert(d);
                    else
                        right.Insert(d);

                    if (right.Size > left.Size + 1)
                    {
                        var minright = right.Pop();
                        left.Insert(minright);
                    }
                    else if (left.Size > right.Size + 1)
                    {
                        maxleft = left.Pop();
                        right.Insert(maxleft);
                    }

                }
            }

            writer.Flush();
        }
    }

    public class MinComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var c = x.CompareTo(y);
            return c == 0 ? -1 : c;
        }
    }

    public class MaxComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var c = -1 * x.CompareTo(y);
            return c == 0 ? -1 : c;
        }
    }

    public class BinaryHeap<T>
    {
        protected T[] _data;

        protected int _size = 0;

        protected Comparison<T> _comparison;

        public BinaryHeap()
        {
            Constructor(4, null);
        }

        public BinaryHeap(Comparison<T> comparison)
        {
            Constructor(4, comparison);
        }

        public BinaryHeap(int capacity)
        {
            Constructor(capacity, null);
        }

        public BinaryHeap(int capacity, Comparison<T> comparison)
        {
            Constructor(capacity, comparison);
        }

        private void Constructor(int capacity, Comparison<T> comparison)
        {
            _data = new T[capacity];
            _comparison = comparison;
            if (_comparison == null)
                _comparison = Comparer<T>.Default.Compare;
        }

        public int Size
        {
            get { return _size; }
        }

        ///
        /// Add an item to the heap
        ///
        public void Insert(T item)
        {
            if (_size == _data.Length)
                Resize();
            _data[_size] = item;
            HeapifyUp(_size);
            _size++;
        }

        ///
        /// Get the item of the root
        ///
        public T Peak()
        {
            return _data[0];
        }

        ///
        /// Extract the item of the root
        ///
        public T Pop()
        {
            T item = _data[0];
            _size--;
            _data[0] = _data[_size];
            HeapifyDown(0);
            return item;
        }

        private void Resize()
        {
            T[] resizedData = new T[_data.Length * 2];
            Array.Copy(_data, 0, resizedData, 0, _data.Length);
            _data = resizedData;
        }

        private void HeapifyUp(int childIdx)
        {
            if (childIdx > 0)
            {
                int parentIdx = (childIdx - 1) / 2;
                if (_comparison.Invoke(_data[childIdx], _data[parentIdx]) > 0)
                {
                    // swap parent and child
                    T t = _data[parentIdx];
                    _data[parentIdx] = _data[childIdx];
                    _data[childIdx] = t;
                    HeapifyUp(parentIdx);
                }
            }
        }

        private void HeapifyDown(int parentIdx)
        {
            int leftChildIdx = 2 * parentIdx + 1;
            int rightChildIdx = leftChildIdx + 1;
            int largestChildIdx = parentIdx;
            if (leftChildIdx < _size && _comparison.Invoke(_data[leftChildIdx], _data[largestChildIdx]) > 0)
            {
                largestChildIdx = leftChildIdx;
            }
            if (rightChildIdx < _size && _comparison.Invoke(_data[rightChildIdx], _data[largestChildIdx]) > 0)
            {
                largestChildIdx = rightChildIdx;
            }
            if (largestChildIdx != parentIdx)
            {
                T t = _data[parentIdx];
                _data[parentIdx] = _data[largestChildIdx];
                _data[largestChildIdx] = t;
                HeapifyDown(largestChildIdx);
            }
        }
    }
}