using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public double? Price { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
    }
}
