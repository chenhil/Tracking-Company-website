using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShippingCo.Services;
using Microsoft.Extensions.Configuration;
using ShippingCo.Models;
using AutoMapper;
using ShippingCo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShippingCo
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            if(_env.IsDevelopment())
            {
                services.AddScoped<IMailService, MailService>();
            }
            else
            {
                //implement a real service
                services.AddScoped<IMailService, DebugMailService>();

            }

            services.AddIdentity<Users, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
            }).AddEntityFrameworkStores<PackageContext>();

            services.AddDbContext<PackageContext>();

            services.AddScoped<IRepository, Repository>();

            services.AddTransient<ContextSeedData>();

            services.AddLogging();

            //Json Result 
            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }

            }).AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ContextSeedData seeder)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentity();

            Mapper.Initialize(config =>
            {
                config.CreateMap<PackageViewModel, Package>().ReverseMap();
                config.CreateMap<StopViewModel, Stop>().ReverseMap();
            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                  name: "Default",
                  template: "{controller}/{action}/{id?}",
                  defaults: new { controller = "App", action = "Index" }
                  );
            });

            //seeder.EnsureSeedData().Wait();
        }
    }
}
