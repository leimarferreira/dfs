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
            CreateMap<SavePurchaseResource, Purchase>();
            CreateMap<SaveUserResource, User>();
            CreateMap<AuthUserResource, User>();
        }
    }
}
