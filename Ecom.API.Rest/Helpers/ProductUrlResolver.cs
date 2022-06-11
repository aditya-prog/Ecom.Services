using AutoMapper;
using Ecom.API.Rest.Dtos;
using Ecom.Apps.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.API.Rest.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        // IConfiguration reads our appsettings file
        public ProductUrlResolver(IConfiguration configuration )
        {
           _configuration = configuration;
        }

        // this method is going to return dest member which is a string
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            // _configuration.GetSection("ApiUrl").Value + source.PictureUrl;
            return _configuration["ApiUrl"] + source.PictureUrl;
        }
    }
}
