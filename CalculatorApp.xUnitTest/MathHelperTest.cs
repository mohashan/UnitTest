using MSTest;
using Xunit;

namespace CalculatorApp.xUnitTest
{
    public class MathHelperTest
    {
        [Fact]
        public void IsEvenTest()
        {
            var calc = new MathFormulas();

            int x = 1;
            int y = 2;

            var xResult = calc.IsEven(x);
            var yResult = calc.IsEven(y);

            Assert.False(xResult);
            Assert.True(yResult);
        }

        [Theory, InlineData(1, 2, 3)]
        public void AddTest(int x, int y, int expectedValue)
        {
            var calc = new MathFormulas();

            var Result = calc.Add(x, y);

            Assert.Equal(expectedValue, Result);
        }

        [Theory]
        [InlineData(1, 2, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(-1, -2, -1)]
        public void DiffTest(int x, int y, int expectedValue)
        {
            var calc = new MathFormulas();

            var Result = calc.Diff(x, y);

            Assert.Equal(expectedValue, Result);
        }

        [Theory]
        [InlineData(new int[3] { 1, 2, 3 }, 6)]
        [InlineData(new int[1] { 5 }, 5)]
        [InlineData(new int[2] { 1, 2 }, 3)]
        [InlineData(new int[2] { -1, -2 }, -3)]
        public void SumTest(int[] values, int expectedValue)
        {
            var calc = new MathFormulas();

            var Result = calc.Sum(values);

            Assert.Equal(expectedValue, Result);
        }

        [Theory]
        [InlineData(new int[3] { 1, 2, 3 }, 2)]
        [InlineData(new int[1] { 5 }, 5)]
        [InlineData(new int[2] { 1, 2 }, 1.5)]
        [InlineData(new int[2] { -1, -2 }, -1.5)]
        public void AverageTest(int[] values, double expectedValue)
        {
            var calc = new MathFormulas();

            var Result = calc.Average(values);

            Assert.Equal(expectedValue, Result);
        }

        [Theory]
        [MemberData(nameof(MathFormulas.Data_add), MemberType = typeof(MathFormulas))]
        public void Add_MemberData_Test(int x, int y, int expectedValue)
        {
            var calc = new MathFormulas();

            var Result = calc.Add(x, y);

            Assert.Equal(expectedValue, Result);
        }

        [Theory]
        [ClassData(typeof(MathFormulas))]
        public void Add_ClassData_Test(int x, int y, int expectedValue)
        {
            var calc = new MathFormulas();

            var Result = calc.Add(x, y);

            Assert.Equal(expectedValue, Result);
        }

        [Theory(Skip = "For Testing")]
        [ClassData(typeof(MathFormulas))]
        public void Add_ClassData_Test_Skipped(int x, int y, int expectedValue)
        {
            var calc = new MathFormulas();

            var Result = calc.Add(x, y);

            Assert.Equal(expectedValue, Result);
        }
    }
}