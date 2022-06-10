using Ecom.Apps.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Apps.Core.Specifications
{
    // ProductsWithTypesAndBrandsSpecification is not any generic class but a specific class for products
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        // this class inherited both the property - Criteria and Includes of base class
        public ProductsWithTypesAndBrandsSpecification()
        {
            // called protected method of base class and passed lambda expression as parameter
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}
