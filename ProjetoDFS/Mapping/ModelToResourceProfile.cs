using AutoMapper;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Resources;

namespace ProjetoDFS.Mapping
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
