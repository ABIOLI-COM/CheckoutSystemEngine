using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutSystemEngine
{
    public class DefaultStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal unitPrice, decimal quantity)
        {
            if (unitPrice < 0)
            {
                throw new ApplicationException("Unit Price not valid");
            }

            return unitPrice * quantity;
        }
    }

    public class BuyOneGetOneStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal unitPrice, decimal quantity)
        {
            if (unitPrice < 0)
            {
                throw new ApplicationException("Unit Price not valid");
            }

            decimal doubleUnits = Math.Truncate(quantity / 2);
            decimal partAtFullPrice = quantity - doubleUnits * 2;
            return doubleUnits * unitPrice + partAtFullPrice * unitPrice;
        }
    }

    public class ThreeForTwoStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(decimal unitPrice, decimal quantity)
        {
            if (unitPrice < 0)
            {
                throw new ApplicationException("Unit Price not valid");
            }

            decimal tripleUnits = Math.Truncate(quantity / 3);
            decimal partAtFullPrice = quantity - tripleUnits * 3;
            return tripleUnits * unitPrice * 2 + partAtFullPrice * unitPrice;
        }
    }
}
