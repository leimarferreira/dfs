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
            CreateMap<Product, ProductResource>();
            CreateMap<Purchase, PurchaseResource>();
            CreateMap<User, UserResource>();
        }
    }
}
