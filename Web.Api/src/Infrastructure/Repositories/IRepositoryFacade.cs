using Microsoft.Extensions.Caching.Distributed;
using System;

namespace Web.Api.Infrastructure.Repositories {


    public interface IRepositoryFacade
    {
        string GetCache(string key);

        void SetCache(string key, string message);
    }

}

