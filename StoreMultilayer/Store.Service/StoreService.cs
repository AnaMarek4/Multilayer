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

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StoreM> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<StoreM>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> Post(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<int> Put(Product product, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
