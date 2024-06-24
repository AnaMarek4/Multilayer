using Store.Model;
using Store.Common;
using DTO.ProductModel;

namespace Store.Service.Common
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        Task<IEnumerable<ProductGet>> GetFilteredProductsAsync(ProductFiltering filtering, ProductSorting sorting, ProductPaging paging);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}
