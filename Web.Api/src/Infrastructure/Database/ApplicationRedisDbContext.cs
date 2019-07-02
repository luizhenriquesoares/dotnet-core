using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System.IO;
using Web.Api.Infrastructure.Environments;

namespace Web.Api.Infrastructure.Database {

    public class RedisConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }

    public static class ApplicationRedisDbContext
    {

        public static IServiceCollection AddDistributedRedisCache(this IServiceCollection services)
        {
            IConfiguration Configuration = OnConfiguring();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["AppSettings:Redis"];
                options.InstanceName  = Configuration["AppSettings:Database"];
            });

            services.AddMemoryCache();

            return services;
        }
     
        public static IConfiguration Configuration { get; private set; }

        public static IConfiguration OnConfiguring()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration;

        }
    }

}