using Ecom.Apps.Core.Entities;

namespace Ecom.Apps.Core.Specifications
{
    // Created just for Counting total products during pagination
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(p => (string.IsNullOrEmpty(productParams.Search) || p.ProductName.ToLower().Contains(productParams.Search.ToLower()))
                     && (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId)
                     && (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId))
        {
        }
    }
}
