using Ecom.Apps.Core.Entities;
using Ecom.Apps.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecom.API.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            return await _basketRepository.GetBasketAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpsertBasketAsync([FromBody]CustomerBasket basket)
        {
            return await _basketRepository.UpsertBasketAsync(basket);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketWithGivenId(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
