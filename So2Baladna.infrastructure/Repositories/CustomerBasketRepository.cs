using So2Baladna.Core.Entities;
using So2Baladna.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Repositories
{
    public class CustomerBasketRepository : ICustomerBasketRepository
    {
        private readonly IDatabase database;

        public CustomerBasketRepository(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }

        public Task<bool> DeleteBasketAsync(string id)
        {
            return database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var result = await database.StringGetAsync(id);
            if (!string.IsNullOrEmpty(result))
            {
                return JsonSerializer.Deserialize<CustomerBasket>(result);
            }
            return null;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            // Fix: Convert `customerBasket.Id` (int) to a string before passing it as a RedisKey.
            var basket = await database.StringSetAsync(
                customerBasket.Id,
                JsonSerializer.Serialize(customerBasket),
                TimeSpan.FromDays(3)
            );

            if (basket)
            {
                return await GetBasketAsync(customerBasket.Id);
            }

            return null;
        }
    }
}
