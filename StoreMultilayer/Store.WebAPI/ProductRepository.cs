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
                    ExpirationDate = new DateOnly(2024, 12, 4)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Yogurt",
                    Price = 0.65,
                    ExpirationDate = new DateOnly(2024, 7, 7)
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Bread",
                    Price = 1.60,
                    ExpirationDate = new DateOnly(2024, 15, 5)
                }
            };
        }
    }
}
