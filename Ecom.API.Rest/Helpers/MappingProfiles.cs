using AutoMapper;
using Ecom.API.Rest.Dtos;
using Ecom.Apps.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.API.Rest.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, options => options.MapFrom(src => src.ProductBrand.BrandName))
                .ForMember(d => d.ProductType, options => options.MapFrom(src => src.ProductType.ProductTypeName))
                .ForMember(d => d.PictureUrl, options => options.MapFrom<ProductUrlResolver>());
        }
    }
}
