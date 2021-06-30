using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSystemEngine
{
    public interface IShoppingCart
    {
        ExecutionResult<Unit> AddElement(Guid productId, decimal quantity = 1m);
        decimal EvaluateShoppingCart();
    }
}
