using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<int> Delete(Guid id)
        {
            return await productRepository.Delete(id);
        }

        public async Task<Product> Get(Guid id)
        {
            return await productRepository.Get(id);
        }

        public async Task<ICollection<Product>> GetAll()
        {
            return await productRepository.GetAll();
        }

        public async Task<int> Post(Product product)
        {
            return await productRepository.Post(product);
        }

        public async Task<int> Put(Product product, Guid id)
        {
            return await productRepository.Put(product, id);
        }
    }
}
