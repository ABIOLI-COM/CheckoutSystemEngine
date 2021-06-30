using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSystemEngine
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(decimal unitPrice, decimal quantity);
    }
}
