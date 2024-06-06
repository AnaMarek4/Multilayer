using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Common;
using Store.Model;
using Store.Repository;
using Store.Service.Common;

namespace Store.Service
{
    public class ProductService : IService<Product>
    {
        private ProductRepository productRepository;
        public ProductService(string connectionString)
        {
           productRepository = new ProductRepository(connectionString);
        }

        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await productRepository.DeleteAsync(id);
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await productRepository.GetAsync(id);
        }

        public async Task<ICollection<Product>> GetAsync(ProductFilter filter, OrderByFilter order, PageFilter page)
        {
            return await productRepository.GetAsync(filter, order, page);
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task<int> PostAsync(Product product)
        {
            return await productRepository.PostAsync(product);
        }

        public async Task<int> PutAsync(Product product, Guid id)
        {
            return await productRepository.PutAsync(product, id);
        }
    }
}
