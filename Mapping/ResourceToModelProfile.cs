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
            CreateMap<SaveProductResource, Product>();
            CreateMap<SavePurchaseResource, Purchase>();
            CreateMap<SaveUserResource, User>();
            CreateMap<AuthUserResource, User>();
        }
    }
}
