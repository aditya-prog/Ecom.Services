using Ecom.Apps.Core.Entities;
using System.Threading.Tasks;

namespace Ecom.Apps.Core.Interfaces
{
    // Methods inside Interfaces are Get the basket, upsert the basket, delete the basket
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpsertBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
