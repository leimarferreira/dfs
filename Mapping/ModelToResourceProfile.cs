using AutoMapper;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Company, CompanyResource>();
            CreateMap<Product, ProductResource>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id));
            CreateMap<Purchase, PurchaseResource>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Buyer.Id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));
            CreateMap<User, UserResource>();
        }
    }
}
