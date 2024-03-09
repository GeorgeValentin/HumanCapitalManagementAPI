using HumanCapitalManagement.Utilities.Authorization;
using HumanCapitalManagement.Utilities.AzureKeyVault;
using Microsoft.Extensions.Caching.Memory;

namespace HumanCapitalManagement.Utilities.Cache
{
    public static class CacheHelper
    {
        public static void SetMemoryCache(string cacheKey, KeyVaultValues keyVaultValues, IMemoryCache memoryCache)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(600),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(60)
            };

            memoryCache.Set(cacheKey, keyVaultValues, cacheExpiryOptions);
        }

        public static void SetMemoryCacheDevelopment(string cacheKey, TokenRequest authorizationCreds, IMemoryCache memoryCache)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(600),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(600)
            };

            memoryCache.Set(cacheKey, authorizationCreds, cacheExpiryOptions);
        }
    }
}
