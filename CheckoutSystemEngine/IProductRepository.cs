using System;

namespace CheckoutSystemEngine
{
    public interface IProductRepository
    {
        ExecutionResult<Unit> AddProduct(Product product);

        ExecutionResult<Product> Find(Guid id);
    }
}
