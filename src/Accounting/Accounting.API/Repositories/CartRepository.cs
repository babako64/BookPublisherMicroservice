using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.API.Entities;
using Accounting.API.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Accounting.API.Repositories
{
    public class CartRepository: ICartRepository
    {

        private readonly IDistributedCache _distributedCache;

        public CartRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            await _distributedCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
            return await GetCartByUserName(cart.UserName);
        }

        public async Task<Cart> GetCartByUserName(string userName)
        {
            var cart = await _distributedCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(cart))
                return null;

            return JsonConvert.DeserializeObject<Cart>(cart);
        }

        public async Task DeleteCart(string userName)
        {
            await _distributedCache.RemoveAsync(userName);
        }
    }
}
