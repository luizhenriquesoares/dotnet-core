using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.src.Infrastructure.Environments
{
    public static class WebHostBuilderExtension
    {
        public static IWebHostBuilder UserPort(this IWebHostBuilder builder)
        {
            var port = Environment.GetEnvironmentVariable("Port");
            if (string.IsNullOrEmpty(port))
            {
                return builder;
            }

            return builder.UseUrls($"http://+:{port}");
        }
    }
}
