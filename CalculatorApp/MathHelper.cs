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
        // is a number even?
        public bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        // is a number odd? 
        public bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        // is a number prime?
        public bool IsPrime(int number)
        {
            if (number == 1) return false;
            if (number == 2) return true;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 2; i <= boundary; ++i)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        // is a number palindrome?
        public bool IsPalindrome(int number)
        {
            var original = number;
            var reversed = 0;

            while (number > 0)
            {
                reversed = reversed * 10 + number % 10;
                number /= 10;
            }

            return original == reversed;
        }


        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 1, 2 };
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { 2, 2, 4 };
            yield return new object[] { 2, 3, 5 };
            yield return new object[] { 3, 3, 6 };
            yield return new object[] { 3, 4, 7 };
            yield return new object[] { 4, 4, 8 };
            yield return new object[] { 4, 5, 9 };
            yield return new object[] { 5, 5, 10 };
            yield return new object[] { 5, 6, 11 };
            yield return new object[] { 6, 6, 12 };
            yield return new object[] { 6, 7, 13 };
            yield return new object[] { 7, 7, 14 };
            yield return new object[] { 7, 8, 15 };
            yield return new object[] { 8, 8, 16 };
            yield return new object[] { 8, 9, 17 };
            yield return new object[] { 9, 9, 18 };
            yield return new object[] { 9, 10, 19 };
            yield return new object[] { 10, 10, 20 };
            yield return new object[] { 10, 11, 21 };
            yield return new object[] { 11, 11, 22 };
            yield return new object[] { 11, 12, 23 };
            yield return new object[] { 12, 12, 24 };
            yield return new object[] { 12, 13, 25 };
            yield return new object[] { 13, 13, 26 };
            yield return new object[] { 13, 14, 27 };
            yield return new object[] { 14, 14, 28 };
            yield return new object[] { 14, 15, 29 };
            yield return new object[] { 15, 15, 30 };
            yield return new object[] { 15, 16, 31 };
            yield return new object[] { 16, 16, 32 };
            yield return new object[] { 16, 17, 33 };
        }
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


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}