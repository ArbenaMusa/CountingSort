using System;
using System.Diagnostics;
using System.Linq;

namespace CountingSort
{
    class Program
    {
        static void Main(string[] args)
        {

            Random randNum = new Random();
            int[] list = Enumerable
                .Repeat(0, 100000)
                .Select(i => randNum.Next(0, 20))
                .ToArray();

            int max = list[0], min = list[0];
            for (int i = 1; i < list.Length; i++)
            {
                if (list[i] > max)
                    max = list[i];
                if (list[i] < min)
                    min = list[i];
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int[] result1 = comparison_counting_sort(list);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();

            stopwatch.Start();
            int[] result2 = distribution_counting_sort(list, min, max);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static int[] comparison_counting_sort(int[] list)
        {
            int[] count = Enumerable.Repeat(0, list.Length).ToArray();

            int[] result = new int[list.Length];

            for (int i = 0; i < list.Length - 1; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[i] < list[j])
                        count[j] = count[j] + 1;
                    else
                        count[i] = count[i] + 1;
                }
            }


            for (int k = 0; k < list.Length; k++)
            {
                result[count[k]] = list[k];
            }

            return result;
        }

        static int[] distribution_counting_sort(int[] list, int min, int max)
        {

            int[] D = Enumerable.Repeat(0, max - min + 1).ToArray();
            int[] result = new int[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                D[list[i] - min] += 1;
            }

            for (int j = 1; j < max - min + 1; j++)
            {
                D[j] = D[j - 1] + D[j];
            }

            for (int k = list.Length - 1; k >= 0; k--)
            {
                int l = list[k] - min;
                result[D[l] - 1] = list[k];
                D[l] -= 1;
            }

            return result;
        }


    }
}
