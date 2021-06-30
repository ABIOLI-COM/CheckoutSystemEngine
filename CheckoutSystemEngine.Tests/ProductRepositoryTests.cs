using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace CheckoutSystemEngine.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void AddProduct_Works()
        {
            Product p = Product.Create(Guid.NewGuid(), "Orange", 123m).Result!;
            ProductRepository rep = new();

            ExecutionResult<Unit> result = rep.AddProduct(p);

            Assert.True(result.IsSuccess);
            // _products is private, so in this moment we cannot test that the product is really in the container:
            // 'Find_Works' will prove it
        }

        [Fact]
        public void AddProduct_Fails_IfExistingProductId()
        {
            Guid id = Guid.NewGuid();
            Product p = Product.Create(id, "Orange", 123m).Result!;
            ProductRepository rep = new();
            _ = rep.AddProduct(p);
            Product p2 = Product.Create(id, "Apple", 234m).Result!;
            
            ExecutionResult<Unit> result = rep.AddProduct(p2);

            Assert.False(result.IsSuccess);
            Assert.Equal(StringConstants.ProductRepository_IdAlreadyExisting, result.ErrorMessage);
        }

        [Fact]
        public void AddProduct_Fails_IfExistingProductName()
        {
            Product p = Product.Create(Guid.NewGuid(), "Orange", 123m).Result!;
            ProductRepository rep = new();
            _ = rep.AddProduct(p);
            Product p2 = Product.Create(Guid.NewGuid(), "Orange", 234m).Result!;

            ExecutionResult<Unit> result = rep.AddProduct(p2);

            Assert.False(result.IsSuccess);
            Assert.Equal(StringConstants.ProductRepository_NameAlreadyExisting, result.ErrorMessage);
        }

        [Fact]
        public void Find_Works()
        {
            Guid id = Guid.NewGuid();
            Product p = Product.Create(id, "Orange", 123m).Result!;
            ProductRepository rep = new();
            _ = rep.AddProduct(p);

            Product found = rep.Find(id).Result!;

            Assert.Equal(id, found.Id);
            Assert.Equal("Orange", found.Name);
            Assert.Equal(123m, found.UnitPrice);
        }

        [Fact]
        public void Find_Fails_IfNotExisting()
        {
            ProductRepository rep = new();

            ExecutionResult<Product> result = rep.Find(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.Equal(StringConstants.ProductRepository_ProductNotFound, result.ErrorMessage);
        }
    }
}
