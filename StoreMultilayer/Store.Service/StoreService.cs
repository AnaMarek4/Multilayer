using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Model;
using Store.Repository;
using Store.Service.Common;

namespace Store.Service
{
    public class StoreService : IService<StoreM>
    {
        private StoreRepository storeRepository;

        public StoreService(string connectionString)
        {
            storeRepository = new StoreRepository(connectionString);
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StoreM> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<StoreM>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> PostAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<int> PutAsync(Product product, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
