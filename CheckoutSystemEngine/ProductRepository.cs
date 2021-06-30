using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSystemEngine
{
    // Simplest implementation (maybe it could go directly on the test side of the solution...)
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public ExecutionResult<Unit> AddProduct(Product product)
        {
            if (_products.SingleOrDefault(p => p.Id == product.Id) != null)
                return ExecutionResult<Unit>.CreateFailure(StringConstants.ProductRepository_IdAlreadyExisting);

            if (_products.SingleOrDefault(p => p.Name.ToUpper() == product.Name.ToUpper()) != null)
                return ExecutionResult<Unit>.CreateFailure(StringConstants.ProductRepository_NameAlreadyExisting);

            _products.Add(product);
            return ExecutionResult<Unit>.CreateSuccess(Unit.Default);
        }

        public ExecutionResult<Product> Find(Guid productId)
        {
            Product? product = _products.SingleOrDefault(p => p.Id == productId);
            return (product != null)
                ? ExecutionResult<Product>.CreateSuccess(product)
                : ExecutionResult<Product>.CreateFailure(StringConstants.ProductRepository_ProductNotFound);
        }
    }
}
