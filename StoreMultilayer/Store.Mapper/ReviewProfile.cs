using AutoMapper;
using Store.Model;
using DTO.ReviewModel;

namespace Store.Mapper
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, GetReviewRest>();
            CreateMap<PostReviewRest, Review>();
            CreateMap<PutReviewRest, Review>();
        }
    }
}
