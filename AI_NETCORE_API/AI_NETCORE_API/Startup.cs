using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Concrete;
using Data.Infrastructure.AppsettingsConfiguration.Abstract;
using Data.Infrastructure.AppsettingsConfiguration.Concrete;
using Data.Infrastructure.EmailAddressValidation.Abstract;
using Data.Infrastructure.EmailAddressValidation.Concrete;
using Data.Infrastructure.Logging.Concrete;
using Data.Infrastructure.PasswordValidation.Abstract;
using Data.Infrastructure.PasswordValidation.Concrete;
using Data.Providers.BuyOffers.Abstract;
using Data.Providers.BuyOffers.Concrete;
using Data.Providers.Companies.Abstract;
using Data.Providers.Companies.Concrete;
using Data.Providers.Resources.Abstract;
using Data.Providers.Resources.Concrete;
using Data.Providers.SellOffers.Abstract;
using Data.Providers.SellOffers.Concrete;
using Data.Providers.Transactions.Abstract;
using Data.Providers.Transactions.Concrete;
using Data.Providers.Users.Abstract;
using Data.Providers.Users.Concrete;
using Data.SomethingProviding.Abstract;
using Data.SomethingProviding.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AI_NETCORE_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<ISomethingProvider, SomethingProvider>();
            services.AddTransient<IAppsettingsProvider, AppsettingsProvider>();
            services.AddTransient<Data.Infrastructure.Logging.Abstract.ILogger, Logger>();
            services.AddTransient<IEmailValidator, EmailValidator>();
            services.AddTransient<IPasswordValidator, PasswordValidator>();
            services.AddTransient<IUserProvider, MockedUserProvider>();
            services.AddTransient<ITransactionsProvider, MockTransactionProvider>();
            services.AddTransient<IResourcesProvider, MockResourcesProvider>();
            services.AddTransient<ICompaniesProvider, MockCompaniesProvider>();
            services.AddTransient<IBusinessObjectToModelsConverter, BusinessObjectToModelsConverter>();
            services.AddTransient<IBuyOffersProvider, BuyOffersProvider>();
            services.AddTransient<ISellOfferProvider, SellOfferProvider>();



            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Core Api", Description = "Swagger Core Api" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Core API");
            });
        }
    }
}
