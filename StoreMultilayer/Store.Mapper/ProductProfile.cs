using AutoMapper;
using Store.Model;
using DTO.ProductModel;

namespace Store.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductPost, Product>();
            CreateMap<Product, ProductGet>();
            CreateMap<ProductPut, Product>();
        }
    }
}
