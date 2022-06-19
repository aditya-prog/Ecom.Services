using Ecom.Apps.Core.Entities;
using Ecom.Apps.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Apps.Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        public Task<bool> DeleteBasketAsync(string basketId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket> UpsertBasketAsync(string basketId)
        {
            throw new NotImplementedException();
        }
    }
}
