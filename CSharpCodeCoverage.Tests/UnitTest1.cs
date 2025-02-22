using Xunit;
using CSharpCodeCoverage;

namespace CSharpCodeCoverage.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddition()
        {
            var calc = new Calculator();
            Assert.Equal(5, calc.Add(2, 3));
        }

        [Fact]
        public void TestSubtraction()
        {
            var calc = new Calculator();
            Assert.Equal(1, calc.Subtract(3, 2));
        }
    }
}
