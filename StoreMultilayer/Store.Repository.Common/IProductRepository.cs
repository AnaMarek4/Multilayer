using Store.Model;
using Store.Common;
using DTO.ProductModel;

namespace Store.Repository.Common
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<ProductGet> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductGet>> GetFilteredProductsAsync(ProductFiltering filtering, ProductSorting sorting, ProductPaging paging);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}
