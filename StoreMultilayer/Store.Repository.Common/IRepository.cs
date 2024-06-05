using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Model;

namespace Store.Repository.Common
{
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> Get(Guid id);
        Task<int> Post(Product product);
        Task<int> Put(Product product, Guid id);
        Task<int> Delete(Guid id);
    }
}
