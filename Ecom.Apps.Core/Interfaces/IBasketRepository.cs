using Ecom.Apps.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Apps.Core.Interfaces
{
    // Methods inside Interfaces are Get the basket, upsert the basket, delete the basket
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpsertBasketAsync(string basketId);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
