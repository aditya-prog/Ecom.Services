using Ecom.Apps.Core.Entities;
using Ecom.Apps.Core.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecom.Apps.Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        private readonly ILogger<BasketRepository> _logger;

        public BasketRepository(IConnectionMultiplexer redis, ILogger<BasketRepository> logger)
        {
            // Get a connection to our redis database, so that we can perform our db opertaions
            // _database is similar to DbContext
            _database = redis.GetDatabase();
            _logger = logger;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            try
            {
                return await _database.KeyDeleteAsync(basketId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in deleteing basket with basketId equals {basketId}");
                throw;
            }
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            try
            {
                // RedisValue / RedisKey are generally a type of string only
                // so we can serialize and deserialize them
                // for Object -> string and String -> Object type conversion respectively
                var data = await _database.StringGetAsync(basketId);
                return data.IsNullOrEmpty ? new CustomerBasket(basketId) : JsonSerializer.Deserialize<CustomerBasket>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while fetching Basket with BasketId equals {basketId}");
                throw;
            }

        }

        public async Task<CustomerBasket> UpsertBasketAsync(CustomerBasket basket)
        {
            try
            {
                // we will keep basket hanging around for 30 days
                var isCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

                if (!isCreated)
                {
                    return null;
                }
                return await GetBasketAsync(basket.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in Upserting basket with basketId equals {basket.Id}");
                throw;
            }
        }
    }
}
