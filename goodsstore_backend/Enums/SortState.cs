namespace goodsstore_backend.Enums
{
    public static class SortState
    {
        public enum Customers
        {
            NameAsc,
            NameDesc,
            CodeAsc,
            CodeDesc,
            AddressAsc,
            AddressDesc,
            DiscountAsc,
            DiscountDesc
        }

        public enum Items
        {
            CodeAsc,
            CodeDesc,
            NameAsc,
            NameDesc,
            PriceAsc,
            PriceDesc,
            CategoryAsc,
            CategoryDesc
        }

        public enum Orders
        {
            CustomerNameAsc,
            CustomerNameDesc,
            CustomerCodeAsc,
            CustomerCodeDesc,
            OrderDateAsc,
            OrderDateDesc,
            ShipmentDateAsc,
            ShipmentDateDesc,
            OrderNumberAsc,
            OrderNumberDesc,
            StatusAsc,
            StatusDesc
        }
    }
}
