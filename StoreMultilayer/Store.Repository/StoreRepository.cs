using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Store.Common;
using Store.Model;
using Store.Repository.Common;

namespace Store.Repository
{
    public class StoreRepository : IRepository<StoreM>
    {
        private readonly string connectionString;

        public StoreRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StoreM> GetAsync(Guid id)
        {
            throw new NotImplementedException();    
        }

        public async Task<ICollection<StoreM>> GetAllAsync()
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            ICollection<StoreM> stores = new List<StoreM>();
            await conn.OpenAsync();

            command.CommandText = "SELECT s.\"Id\", s.\"Name\", s.\"Address\", s.\"PhoneNumber\", s.\"IsActive\", s.\"DateCreated\", s.\"DateUpdated\", s.\"CreatedByUserId\", s.\"UpdatedByUserId\", " +
                          "p.\"Id\", p.\"Name\", p.\"Price\", p.\"ExpirationDate\", p.\"IsActive\", p.\"DateCreated\", p.\"DateUpdated\", p.\"CreatedByUserId\", p.\"UpdatedByUserId\" " +
                          "FROM \"Store\" s " +
                          "LEFT JOIN \"Product\" p ON s.\"Id\" = p.\"StoreId\" " +
                          "WHERE s.\"IsActive\" = @isActive";
            command.Parameters.AddWithValue("@isActive", true);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var storeId = reader.GetGuid(0);
                    StoreM? existingStore = stores.FirstOrDefault(p => p.Id == storeId);

                    if (existingStore == null)
                    {
                        var store = new StoreM
                        {
                            Id = storeId,
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            IsActive = reader.GetBoolean(4),
                            DateCreated = reader.GetDateTime(5),
                            DateUpdated = reader.GetDateTime(6),
                            CreatedByUserId = reader.GetInt32(7),
                            UpdatedByUserId = reader.GetInt32(8),
                            Products = new List<Product>()
                        };

                        if (!reader.IsDBNull(9))
                        {
                            var product = new Product
                            {
                                Id = reader.GetGuid(9),
                                StoreId = storeId,
                                Name = reader.GetString(11),
                                Price = reader.GetDouble(12),
                                ExpirationDate = reader.IsDBNull(4) ? (DateOnly?)null : reader.GetDateTime(4).ToDateOnly(),
                                IsActive = reader.GetBoolean(14),
                                DateCreated = reader.GetDateTime(15),
                                DateUpdated = reader.GetDateTime(16),
                                CreatedByUserId = reader.GetInt32(17),
                                UpdatedByUserId = reader.GetInt32(18)
                            };

                            store.Products.Add(product);
                        }
                        stores.Add(store);
                    }
                    else if (!reader.IsDBNull(10))
                    {
                        var product = new Product
                        {
                            Id = reader.GetGuid(9),
                            StoreId = reader.GetGuid(10),
                            Name = reader.GetString(11),
                            Price = reader.GetDouble(12),
                            ExpirationDate = reader.IsDBNull(4) ? (DateOnly?)null : reader.GetDateTime(4).ToDateOnly(),
                            IsActive = reader.GetBoolean(14),
                            DateCreated = reader.GetDateTime(15),
                            DateUpdated = reader.GetDateTime(16),
                            CreatedByUserId = reader.GetInt32(17),
                            UpdatedByUserId = reader.GetInt32(18)
                        };

                        existingStore.Products.Add(product);
                    }
                }
            }

            return stores;
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
