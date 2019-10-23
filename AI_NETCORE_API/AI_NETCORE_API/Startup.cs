using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Concrete;
using Domain.Creators.Users.Abstract;
using Domain.Creators.Users.Concrete;
using Domain.Infrastructure.AppsettingsConfiguration.Abstract;
using Domain.Infrastructure.AppsettingsConfiguration.Concrete;
using Domain.Infrastructure.EmailAddressValidation.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Concrete;
using Domain.Infrastructure.Logging.Concrete;
using Domain.Infrastructure.PasswordValidation.Abstract;
using Domain.Infrastructure.PasswordValidation.Concrete;
using Domain.Providers.BuyOffers.Abstract;
using Domain.Providers.BuyOffers.Concrete;
using Domain.Providers.Companies.Abstract;
using Domain.Providers.Companies.Concrete;
using Domain.Providers.Resources.Abstract;
using Domain.Providers.Resources.Concrete;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Concrete;
using Domain.Providers.Transactions.Abstract;
using Domain.Providers.Transactions.Concrete;
using Domain.Providers.Users.Abstract;
using Domain.Providers.Users.Concrete;
using Domain.DTOToBOConverting;
using Data.Models;
using Domain.Repositories.UserRepo.Abstract;
using Domain.Repositories.UserRepo.Concrete;
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

            services.AddTransient<RepositoryContext>();
            services.AddTransient<IDTOToBOConverter, DTOToBOConverter>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAppsettingsProvider, AppsettingsProvider>();
            services.AddTransient<Domain.Infrastructure.Logging.Abstract.ILogger, Logger>();
            services.AddTransient<IEmailValidator, EmailValidator>();
            services.AddTransient<IPasswordValidator, PasswordValidator>();
            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<ITransactionsProvider, MockTransactionProvider>();
            services.AddTransient<IResourcesProvider, MockResourcesProvider>();
            services.AddTransient<ICompaniesProvider, MockCompaniesProvider>();
            services.AddTransient<IBusinessObjectToModelsConverter, BusinessObjectToModelsConverter>();
            services.AddTransient<IBuyOffersProvider, BuyOffersProvider>();
            services.AddTransient<ISellOfferProvider, SellOfferProvider>();
            services.AddTransient<IUserCreator, UserCreator>();



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
