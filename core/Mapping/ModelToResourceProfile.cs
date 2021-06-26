using AutoMapper;
using core.Domain.Models;
using core.Resources;

namespace core.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Company, CompanyResource>();
            CreateMap<Product, ProductResource>();
            CreateMap<Purchase, PurchaseResource>();
            CreateMap<User, UserResource>();
        }
    }
}
