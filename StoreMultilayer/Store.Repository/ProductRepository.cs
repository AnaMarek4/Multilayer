using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;
using Npgsql;
using Store.Model;
using Store.Repository.Common;

namespace Store.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly string connectionString;
        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<int> Delete(Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            await conn.OpenAsync();

            command.CommandText = "UPDATE \"Product\" SET \"IsActive\" = @IsActive WHERE \"Id\" = @productId";
            command.Parameters.AddWithValue("@IsActive", false);
            command.Parameters.AddWithValue("@productId", id);

            int commitnumber = await command.ExecuteNonQueryAsync();
            return commitnumber;
            
        }

        public async Task<Product> Get(Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            Product product = new Product();
            await conn.OpenAsync();

            command.CommandText = "SELECT * FROM \"Product\" WHERE \"Product\".\"Id\" = @Id and \"IsActive\" = @IsActive";
            command.Parameters.AddWithValue("@IsActive", true);
            command.Parameters.AddWithValue("@Id", id);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    product.Id = reader.GetGuid(0);
                    product.StoreId = reader.GetGuid(1);
                    product.Name = reader.GetString(2);
                    product.Price = reader.GetDouble(3);
                    //product.ExpirationDate = reader.GetDateTime(4);
                    product.IsActive = reader.GetBoolean(5);
                    product.DateCreated = reader.GetDateTime(6);
                    product.DateUpdated = reader.GetDateTime(7);
                    product.CreatedByUserId = reader.GetInt32(8);
                    product.UpdatedByUserId = reader.GetInt32(9);
                }
            }

            return product;
        }
        public async Task<ICollection<Product>> GetAll()
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            ICollection<Product> products = new List<Product>();
            await conn.OpenAsync();

            command.CommandText = "SELECT * FROM \"Product\" WHERE \"IsActive\" = @IsActive";
            command.Parameters.AddWithValue("@IsActive", true);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var product = new Product
                    {
                        Id = reader.GetGuid(0),
                        StoreId = reader.GetGuid(1),
                        Name = reader.GetString(2),
                        Price = reader.GetDouble(3),
                        //ExpirationDate = reader.GetDateTime(4),
                        IsActive = reader.GetBoolean(5),
                        DateCreated = reader.GetDateTime(6),
                        DateUpdated = reader.GetDateTime(7),
                        CreatedByUserId = reader.GetInt32(8),
                        UpdatedByUserId = reader.GetInt32(9)
                    };
                    products.Add(product);
                }
            }
            return products;
        }

        public async Task<int> Post(Product product)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            await conn.OpenAsync();

            command.CommandText = "INSERT INTO \"Product\" (\"Id\", \"StoreId\", \"Name\", \"Price\", \"ExpirationDate\", \"IsActive\", \"DateCreated\", \"DateUpdated\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                                    "VALUES (@Id, @StoreId, @Name, @Price, @ExpirationDate, @IsActive, @DateCreated, @DateUpdated, @CreatedByUserId, @UpdatedByUserId)";
            command.Parameters.AddWithValue("@StoreId", product.StoreId);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@ExpirationDate", product.ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", product.IsActive);
            command.Parameters.AddWithValue("@DateCreated", product.DateCreated);
            command.Parameters.AddWithValue("@DateUpdated", product.DateUpdated);
            command.Parameters.AddWithValue("@CreatedByUserId", product.CreatedByUserId);
            command.Parameters.AddWithValue("@UpdatedByUserId", product.UpdatedByUserId);

            int commitnumber = await command.ExecuteNonQueryAsync();
            return commitnumber;

        }

        public async Task<int> Put(Product product, Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            await conn.OpenAsync();

            command.CommandText = "UPDATE \"Product\" SET \"StoreId\" = @StoreId, \"Name\" = @Name, \"Price\" = @Price, \"ExpirationDate\" = @ExpirationDate, \"IsActive\" = @IsActive, " + 
                                    "\"DateCreated\" = @DateCreated, \"DateUpdated\" = @DateUpdated, \"CreatedByUserId\" = @CreatedByUserId, \"UpdatedByUserId\" = @UpdatedByUserId WHERE \"Id\" = @Id";
            command.Parameters.AddWithValue("@StoreId", product.StoreId);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@ExpirationDate", product.ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", product.IsActive);
            command.Parameters.AddWithValue("@DateCreated", product.DateCreated);
            command.Parameters.AddWithValue("@DateUpdated", product.DateUpdated);
            command.Parameters.AddWithValue("@CreatedByUserId", product.CreatedByUserId);
            command.Parameters.AddWithValue("@UpdatedByUserId", product.UpdatedByUserId);
            command.Parameters.AddWithValue("@prouctId", id);
            
            int commitnumber = await command.ExecuteNonQueryAsync();
            return commitnumber;
        }
    }
}
