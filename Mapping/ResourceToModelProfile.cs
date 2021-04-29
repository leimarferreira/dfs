using ProjetoDFS.Domain.Models;
using ProjetoDFS.Resources;
using AutoMapper;

namespace ProjetoDFS.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCompanyResource, Company>();
            CreateMap<SaveProductResource, Product>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => new Company { Id = src.CompanyId }));
            CreateMap<SavePurchaseResource, Purchase>()
                .ForMember(dest => dest.Buyer, opt => opt.MapFrom(src => new User { Id = src.UserId }))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new Product { Id = src.ProductId }));
            CreateMap<SaveUserResource, User>();
            CreateMap<AuthUserResource, User>();
        }
    }
}
