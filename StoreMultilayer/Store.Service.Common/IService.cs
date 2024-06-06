using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Store.Model;

namespace Store.Service.Common
{
    public interface IService<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<int> PostAsync(Product product);
        Task<int> PutAsync(Product product, Guid id);
        Task<int> DeleteAsync(Guid id);
    }
}
