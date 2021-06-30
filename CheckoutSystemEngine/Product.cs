using Microsoft.VisualBasic;

using System;

namespace CheckoutSystemEngine
{
    public class Product
    {
        public Guid Id { get; private set; } // This entity simulates in a way the barcode on products...
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; } // Tomorrow this will go in a separate class 'ProductPrice', if needed
        public IPricingStrategy Strategy { get; private set; }
        
        private Product(Guid id, string name, decimal unitPrice, IPricingStrategy strategy)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            Strategy = strategy;
        }

        public static ExecutionResult<Product> Create(Guid id, string name, decimal unitPrice, IPricingStrategy strategy)
        {
            if (id == Guid.Empty)
                return ExecutionResult<Product>.CreateFailure(StringConstants.Product_MissingId);
            if (string.IsNullOrEmpty(name) || name.Length < 3)
                return ExecutionResult<Product>.CreateFailure(StringConstants.Product_MissingName);
            if (unitPrice < 0) // We decide to accept 'free' items, in the shop (unitprice = 0);
                return ExecutionResult<Product>.CreateFailure(StringConstants.Product_NegativeUnitPrice);
            
            return ExecutionResult<Product>.CreateSuccess(new Product(id, name, unitPrice, strategy));
        }
    }
}
