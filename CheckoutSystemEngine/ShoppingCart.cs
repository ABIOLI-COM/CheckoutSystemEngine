using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutSystemEngine
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IProductRepository _products;

        private readonly Dictionary<Guid, decimal> _elements = new();

        public ShoppingCart(IProductRepository products)
        {
            // If we don't have a product repository, we can only fail
            _products = products ?? throw new ApplicationException(StringConstants.ShoppingCart_NoIProductRepository);
        }

        public ExecutionResult<Unit> AddElement(Guid productId, decimal quantity = 1m)
        {
            ExecutionResult<Product> findResult = _products.Find(productId);
            if (!findResult.IsSuccess)
                return ExecutionResult<Unit>.CreateFailure(findResult.ErrorMessage);

            return _elements.AddEntry(productId, quantity);
        }

        public decimal EvaluateShoppingCart()
            => _elements
                .Select(kvp => new { ProductId = kvp.Key, Product = _products.Find(kvp.Key).Result!, Quantity = kvp.Value })
                .Select(v => new { v.ProductId, TotalPricePerProduct = v.Product.Strategy.CalculatePrice(v.Product.UnitPrice, v.Quantity) })
                .Sum(v => v.TotalPricePerProduct);
        // Given the construction of all the other elements, it may be worth to underline that nothing can fail, inside this method, at this point.
    }
}
