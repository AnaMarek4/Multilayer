using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common
{
    public class ProductFilter
    {
        public Guid? id;
        public string name;
        public double? minPrice;
        public double? maxPrice;

        public ProductFilter(Guid? id, string name, double? minPrice, double? maxPrice)
        {
            this.id = id;
            this.name = name;
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;
        }
    }
}
