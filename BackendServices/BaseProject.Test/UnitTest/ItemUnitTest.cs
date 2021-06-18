using BaseProjectAPI.Service.Items;
using FluentAssertions;
using Xunit;

namespace BaseProject.Test.UnitTest
{
    [Trait("Category", "UnitTest")]
    public class ItemUnitTest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetStockStatusEmptyTest(int quantity)
        { 
            string test = ItemFilters.GetStockStatus(quantity);
            test.Should().Be("Empty", "is bigger than 0");
            Assert.Equal("Empty",test);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetStockStatusLowTest(int quantity)
        {
            string test = ItemFilters.GetStockStatus(quantity);
            test.Should().Be("Low");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        public void GetStockStatusMediumTest(int quantity)
        {
            string test = ItemFilters.GetStockStatus(quantity);
            test.Should().Be("Medium");
        }

        [Theory]
        [InlineData(7)]
        [InlineData(15)]
        public void GetStockStatusTest(int quantity)
        {
            string test = ItemFilters.GetStockStatus(quantity);
            test.Should().Be("Good");
        }
    }
}
