using System;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Web.Api.Infrastructure.Database;
using Web.Api.Infrastructure.Guards;

namespace Web.Api.Infrastructure.Repositories
{

    public class RepositoryFacade: IRepositoryFacade
    {
        private IDistributedCache _cache;

        public RepositoryFacade(IDistributedCache cache)
        {
            _cache = cache;
        }

        public string GetCache(string key)
        {
            return _cache.GetString(key);
        }

        public void SetCache(string key, string message)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            try
            {
                _cache.SetString(key, message);
            } catch(Exception e)
            {
                throw e;
            }
        }
    }
}

