using System;

using Xunit;

namespace CheckoutSystemEngine.Tests
{
    public class ShoppingCartTests
    {
        private static readonly Guid AppleId = Guid.NewGuid();
        private static readonly Guid OrangeId = Guid.NewGuid();

        private static IProductRepository CreateProductRepository()
        {
            ProductRepository rep = new();
            rep.AddProduct(Product.Create(AppleId, "Apple", 0.60m).Result!);
            rep.AddProduct(Product.Create(OrangeId, "Orange", 0.25m).Result!);
            return rep;
        }

        [Fact]
        public void ShoppingCart_Fails_IfNoProductRepository()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = Assert.Throws<ApplicationException>(() => new ShoppingCart(default));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void AddElement_Works()
        {
            IProductRepository rep = CreateProductRepository();
            ShoppingCart cart = new(rep);

            var result = cart.AddElement(AppleId, 2);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void AddElement_Fails_IfProductNotExisting()
        {
            IProductRepository rep = CreateProductRepository();
            ShoppingCart cart = new(rep);

            var result = cart.AddElement(Guid.NewGuid(), 2);

            Assert.False(result.IsSuccess);
            Assert.Equal(StringConstants.ProductRepository_ProductNotFound, result.ErrorMessage);
        }

        [Fact]
        public void EvaluateShoppingCart_Works_WithDefaultQuantity()
        {
            IProductRepository rep = CreateProductRepository();
            ShoppingCart cart = new(rep);
            _ = cart.AddElement(AppleId);
            _ = cart.AddElement(OrangeId);
            _ = cart.AddElement(AppleId);
            _ = cart.AddElement(OrangeId);
            _ = cart.AddElement(OrangeId);
            _ = cart.AddElement(AppleId);

            var result = cart.EvaluateShoppingCart();

            Assert.Equal(2.55m, result);
        }

        [Fact]
        public void EvaluateShoppingCart_Works_WithQuantities()
        {
            IProductRepository rep = CreateProductRepository();
            ShoppingCart cart = new(rep);
            _ = cart.AddElement(AppleId, 4);
            _ = cart.AddElement(OrangeId, 5);

            var result = cart.EvaluateShoppingCart();

            Assert.Equal(3.65m, result);
        }

        [Fact]
        public void EvaluateShoppingCart_Works_WhenEmpty()
        {
            IProductRepository rep = CreateProductRepository();
            ShoppingCart cart = new(rep);

            var result = cart.EvaluateShoppingCart();

            Assert.Equal(0m, result);
        }

        [Fact]
        public void EvaluateShoppingCart_Works_WhenQuantitiesNotInteger()
        {
            IProductRepository rep = CreateProductRepository();
            ShoppingCart cart = new(rep);
            _ = cart.AddElement(AppleId, 0.5m);
            _ = cart.AddElement(OrangeId, 0.5m);

            var result = cart.EvaluateShoppingCart();

            Assert.Equal(0.425m, result);
        }
    }
}
