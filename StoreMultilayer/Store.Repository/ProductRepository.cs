using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Npgsql;
using Store.Model;
using Store.Common;
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

        public async Task<int> DeleteAsync(Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            conn.Open();

            command.CommandText = "UPDATE \"Product\" SET \"IsActive\" = @IsActive WHERE \"Id\" = @productId";
            command.Parameters.AddWithValue("@IsActive", false);
            command.Parameters.AddWithValue("@productId", id);

            int commitNumber = await command.ExecuteNonQueryAsync();
            conn.Close();
            return commitNumber;
            
        }

        public async Task<Product> GetAsync(Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            Product product = new Product();
            conn.Open();

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
                    product.ExpirationDate = reader.IsDBNull(4) ? (DateOnly?)null : reader.GetDateTime(4).ToDateOnly();
                    product.IsActive = reader.GetBoolean(5);
                    product.DateCreated = reader.GetDateTime(6);
                    product.DateUpdated = reader.GetDateTime(7);
                    product.CreatedByUserId = reader.GetInt32(8);
                    product.UpdatedByUserId = reader.GetInt32(9);
                }
            }
            conn.Close();
            return product;
        }

        private static NpgsqlCommand CreateCommand(NpgsqlConnection conn, ProductFilter filter, OrderByFilter order, PageFilter page)
        {
            NpgsqlCommand command = new NpgsqlCommand("", conn);
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM \"Stock\" WHERE \"IsActive\" = @IsActive ");

            command.Parameters.AddWithValue("@IsActive", true);
            if (filter.id != null)
            {
                query.Append(" AND \"Id\" = @Id");
                command.Parameters.AddWithValue("@Id", filter.id);
            }
            if (filter.name != "")
            {
                query.Append(" AND \"Name\" ILIKE @Name");
                command.Parameters.AddWithValue("@Name", $"%{filter.name}%");
            }
            if (filter.minPrice != null)
            {
                query.Append(" AND \"Price\" >= @MinPrice");
                command.Parameters.AddWithValue("@MinPrice", filter.minPrice);
            }
            if (filter.maxPrice != null)
            {
                query.Append(" AND \"Price\" <= @MaxPrice");
                command.Parameters.AddWithValue("@MaxPrice", filter.maxPrice);
            }

            string sortOrder = order.sortOrder.ToUpper() == "DESC" ? "DESC" : "ASC";
            query.Append($" ORDER BY \"{order.orderBy}\" {sortOrder}");
            query.Append(" LIMIT @Limit OFFSET @Offset");
            command.Parameters.AddWithValue("@Limit", page.rpp);
            command.Parameters.AddWithValue("Offset", (page.pageNumber - 1) * page.rpp);
            command.CommandText = query.ToString();
            return command;
        }

        public async Task<ICollection<Product>> GetAsync(ProductFilter filter, OrderByFilter order, PageFilter page)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = CreateCommand(conn, filter, order, page);
            ICollection<Product> products = new List<Product>();
            conn.Open();

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
                        ExpirationDate = reader.IsDBNull(4) ? (DateOnly?)null : reader.GetDateTime(4).ToDateOnly(),
                        IsActive = reader.GetBoolean(5),
                        DateCreated = reader.GetDateTime(6),
                        DateUpdated = reader.GetDateTime(7),
                        CreatedByUserId = reader.GetInt32(8),
                        UpdatedByUserId = reader.GetInt32(9)
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }

        public async Task<ICollection<Product>> GetAllAsync()
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            ICollection<Product> products = new List<Product>();
            conn.Open();

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
                        ExpirationDate = reader.IsDBNull(4) ? (DateOnly?)null : reader.GetDateTime(4).ToDateOnly(),
                        IsActive = reader.GetBoolean(5),
                        DateCreated = reader.GetDateTime(6),
                        DateUpdated = reader.GetDateTime(7),
                        CreatedByUserId = reader.GetInt32(8),
                        UpdatedByUserId = reader.GetInt32(9)
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }

        public async Task<int> PostAsync(Product product)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            conn.Open();

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

            int commitNumber = await command.ExecuteNonQueryAsync();
            conn.Close();
            return commitNumber;

        }

        public async Task<int> PutAsync(Product product, Guid id)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            using NpgsqlCommand command = new NpgsqlCommand("", conn);
            conn.Open();

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
            
            int commitNumber = await command.ExecuteNonQueryAsync();
            conn.Close();
            return commitNumber;
        }
    }
}
