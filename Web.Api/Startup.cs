using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Web.Api.Infrastructure.AutoMapper;
using Web.Api.Infrastructure.Database;
using Web.Api.Infrastructure.Guards;
using Web.Api.Modules.Auth;
using Web.Api.Infrastructure.Pipes;
using Web.Api.Infrastructure.Environments;
using Web.Api.Infrastructure.Repositories;

namespace Web.Api {
    public class Startup {

        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // add cors
            services.AddCors();

            // Add Context
            services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("web.api"); });

            // Swagger Config
            services.AddSwaggerGen(c => {
               c.SwaggerDoc("v1", new Info { Title = "Web Api 2.2", Version = "V1" });
           });

            // Fluent Validation
            services.AddValidators();

            // Add Identity
            services.AddIdentityConfiguration();

            //Add Redis
            services.AddDistributedRedisCache();

            // appSettings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // JWt Guards
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            var jwt = new Jwt(tokenConfigurations);
            services.AddSingleton(jwt);

            services.AddJwtSecurity(signingConfigurations, tokenConfigurations);

            // Auto Mapper Config
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainProfile());
            });
        
            services.AddSingleton(configMapper.CreateMapper());

            // DI Injection 
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IRepositoryFacade, RepositoryFacade>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }
    
            app.UseMvc ();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}