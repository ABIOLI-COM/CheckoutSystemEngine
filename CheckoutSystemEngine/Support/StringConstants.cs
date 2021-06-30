namespace CheckoutSystemEngine
{
    public static class StringConstants
    {
        public const string DictionaryExtensions_KeyIsNull = "Guid cannot be empty";

        public const string Product_MissingId = "Id must have a value";
        public const string Product_MissingName = "Name is null or not long enough (at least 3)";
        public const string Product_NegativeUnitPrice = "UnitPrice cannot be negative";

        public const string ProductRepository_IdAlreadyExisting = "Product Id is already existing";
        public const string ProductRepository_NameAlreadyExisting = "Product Name is already existing";
        public const string ProductRepository_ProductNotFound = "Product not found";

        public const string ShoppingCart_NoIProductRepository = "The system cannot work without a proper IProductRepository";
    }
}
