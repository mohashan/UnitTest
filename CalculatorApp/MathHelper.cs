using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    public class MathFormulas : IEnumerable<object[]>
    {
        
        public bool IsEven(int number) => number % 2 == 0;
        public int Diff(int x, int y) => y - x;
        public int Add(int x, int y) => y + x;
        public int Sum(params int[] values)
        {
            int sum = 0;
            foreach (var val in values)
            {
                sum += val;
            }

            return sum;
        }
        public double Average(params int[] values)
        {
            double sum = 0;
            foreach (var item in values)
            {
                sum += item;
            }
            return sum / values.Length;
        }

        public static IEnumerable<object[]> Data_add => new List<object[]>
        {
            new object[]{-4,-6,-10},
            new object[]{-2,2,0},
            new object[]{int.MinValue,-1,int.MaxValue},
        };
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { -4, -6, -10 };
            yield return new object[] { -2, 2, 0 };
            yield return new object[] { int.MinValue, -1, int.MaxValue };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
