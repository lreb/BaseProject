using System;
using Xunit;

namespace BaseProject.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var str = "One";
            Assert.Contains("One", str);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            var result = IsPrime(value);

            Assert.False(result, $"{value} should not be prime");
        }

        private bool IsPrime(int candidate)
        {
            if (candidate < 2)
            {
                return false;
            }
            throw new NotImplementedException("Not fully implemented.");
        }
    }
}
