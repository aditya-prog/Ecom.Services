using Ecom.Apps.Core.Entities;

namespace Ecom.Apps.Core.Specifications
{
    // ProductsWithTypesAndBrandsSpecification is not any generic class but a specific class for products
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        // this class inherited both the property - Criteria and Includes of base class
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams )
            : base(p => (string.IsNullOrEmpty(productParams.Search) || p.ProductName.ToLower().Contains(productParams.Search.ToLower()))
                     && (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId)
                     && (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId))
        {
            // called protected method of base class and passed lambda expression as parameter
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            
            switch (productParams.SortBy)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    
                    break;

                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    AddOrderBy(p => p.ProductName);
                    break;
            }

            ApplyPaging((int)(productParams.PageSize * (productParams.PageIndex - 1)), (int)productParams.PageSize);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}
