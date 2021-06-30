using System;

using Xunit;

namespace CheckoutSystemEngine.Tests
{
    public class PricingStrategiesTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(2, 1, 2)]
        [InlineData(1.5, 0.5, 3)]
        [InlineData(3.55, 0.5, 7.1)]
        [InlineData(10, 1, 10)]
        [InlineData(11, 1, 11)]
        [InlineData(12, 1, 12)]
        [InlineData(3, 1, 3)]
        [InlineData(3, 0.5, 6)]
        [InlineData(4, 0.5, 8)]
        [InlineData(4.5, 0.5, 9)]
        public void DefaultStrategy_Works(decimal result, decimal unitPrice, decimal quantity)
            => Assert.Equal(result, new DefaultStrategy().CalculatePrice(unitPrice, quantity));

        [Fact]
        public void DefaultStrategy_Fails_IfUnitPriceNegative()
            => Assert.Throws<ApplicationException>(() => new DefaultStrategy().CalculatePrice(-1, 1));

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(1, 0.5, 3)]
        [InlineData(2.05, 0.5, 7.1)]
        [InlineData(5, 1, 10)]
        [InlineData(6, 1, 11)]
        [InlineData(6, 1, 12)]
        public void BuyOneGetOneStrategy_Works(decimal result, decimal unitPrice, decimal quantity)
            => Assert.Equal(result, new BuyOneGetOneStrategy().CalculatePrice(unitPrice, quantity));

        [Fact]
        public void BuyOneGetOneStrategy_Fails_IfUnitPriceNegative()
            => Assert.Throws<ApplicationException>(() => new BuyOneGetOneStrategy().CalculatePrice(-1, 1));


        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(2, 1, 3)]
        [InlineData(2, 0.5, 6)]
        [InlineData(3, 0.5, 8)]
        [InlineData(3, 0.5, 9)]
        [InlineData(8, 1, 11)]
        [InlineData(8, 1, 12)]
        public void ThreeForTwoStrategy_Works(decimal result, decimal unitPrice, decimal quantity)
            => Assert.Equal(result, new ThreeForTwoStrategy().CalculatePrice(unitPrice, quantity));


        [Fact]
        public void ThreeForTwoStrategy_Fails_IfUnitPriceNegative()
            => Assert.Throws<ApplicationException>(() => new ThreeForTwoStrategy().CalculatePrice(-1, 1));
    }
}
