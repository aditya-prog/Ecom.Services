using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Apps.Core.Specifications
{
    public class ProductSpecParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string SortBy { get; set; }
        public int? PageSize { get; set; } = 5;
        public int? PageIndex { get; set; } = 1;
        public string Search { get; set; }
    }
}
