using Store.Model;

namespace Store.WebAPI
{
    public static class ProductRepository
    {
        private static ICollection<Product> Products;

        static ProductRepository()
        {
            Products = new List<Product>()
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Milk",
                    Price = 12.75,
                    //ExpirationDate = "2024-12-04",
                }
            };
        }
    }
}
