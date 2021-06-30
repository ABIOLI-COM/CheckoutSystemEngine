using System;

using Xunit;

namespace CheckoutSystemEngine.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_Works()
        {
            Guid id = Guid.NewGuid();
            ExecutionResult<Product> result = Product.Create(id, "Orange", 1.23m);

            Assert.True(result.IsSuccess);
            Assert.Equal(id, result.Result!.Id);
            Assert.Equal("Orange", result.Result.Name);
            Assert.Equal(1.23m, result.Result.UnitPrice);
        }

        [Fact]
        public void CreateProduct_Works_WithZeroPrice()
        {
            Guid id = Guid.NewGuid();
            ExecutionResult<Product> result = Product.Create(id, "Orange", 0m);

            Assert.True(result.IsSuccess);
            Assert.Equal(id, result.Result!.Id);
            Assert.Equal("Orange", result.Result.Name);
            Assert.Equal(0m, result.Result.UnitPrice);
        }

        [Fact]
        public void CreateProduct_Fails_IfEmptyGuid()
        {
            ExecutionResult<Product> result = Product.Create(Guid.Empty, "Orange", 1.23m);

            Assert.False(result.IsSuccess);
            Assert.Equal(StringConstants.Product_MissingId, result.ErrorMessage);
        }

        [Fact]
        public void CreateProduct_Fails_IfEmptyName()
        {
            ExecutionResult<Product> result = Product.Create(Guid.NewGuid(), "", 1.23m);

            Assert.False(result.IsSuccess);
            Assert.Equal(StringConstants.Product_MissingName, result.ErrorMessage);
        }

        [Fact]
        public void CreateProduct_Fails_IfNegativeUnitPrice()
        {
            ExecutionResult<Product> result = Product.Create(Guid.NewGuid(), "Orange", -123m);

            Assert.False(result.IsSuccess);
            Assert.Equal(StringConstants.Product_NegativeUnitPrice, result.ErrorMessage);
        }
    }
}
