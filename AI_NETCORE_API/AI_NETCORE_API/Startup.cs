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
using Domain.Repositories.CompanyRepo.Abstract;
using Domain.Repositories.CompanyRepo.Concrete;
using Domain.Repositories.ResourceRepo.Abstract;
using Domain.Repositories.ResourceRepo.Concrete;
using Domain.Repositories.BuyOfferRepo.Abstract;
using Domain.Repositories.BuyOfferRepo.Concrete;
using Domain.Repositories.SellOfferRepo.Abstract;
using Domain.Repositories.SellOfferRepo.Concrete;
using Domain.Repositories.TransactionRepo.Abstract;
using Domain.Repositories.TransactionRepo.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AI_NETCORE_API.Infrastructure.GettingUserIdentifierFromRequest.Abstract;
using AI_NETCORE_API.Infrastructure.GettingUserIdentifierFromRequest.Concrete;
using Domain.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Domain.Updaters.Configurations.Concrete;
using Domain.Updaters.Configurations.Abstract;
using Domain.Repositories.ConfigurationRepo.Concrete;
using Domain.Repositories.ConfigurationRepo.Abstract;
using Domain.Creators.BuyOffer.Abstract;
using Domain.Creators.BuyOffer.Concrete;
using Domain.Creators.SellOffer.Concrete;
using Domain.Creators.SellOffer.Abstract;
using Microsoft.EntityFrameworkCore;

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

            services.AddTransient<IUserIdentifierFromHttpRequestProvider, UserIdentifierFromHttpRequestProvider>();
            services.AddTransient<RepositoryContext>();
            services.AddTransient<IDTOToBOConverter, DTOToBOConverter>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IBuyOfferRepository, BuyOfferRepository>();
            services.AddTransient<ISellOfferRepository, SellOfferRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
            services.AddTransient<IAppsettingsProvider, AppsettingsProvider>();
            services.AddTransient<Domain.Infrastructure.Logging.Abstract.ILogger, Logger>();
            services.AddTransient<IEmailValidator, EmailValidator>();
            services.AddTransient<IPasswordValidator, PasswordValidator>();
            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<ITransactionsProvider, TransactionProvider>();
            services.AddTransient<IResourcesProvider, ResourcesProvider>();
            services.AddTransient<ICompaniesProvider, CompaniesProvider>();
            services.AddTransient<IBusinessObjectToModelsConverter, BusinessObjectToModelsConverter>();
            services.AddTransient<IBuyOffersProvider, BuyOffersProvider>();
            services.AddTransient<ISellOfferProvider, SellOfferProvider>();
            services.AddTransient<IBuyOfferCreator, BuyOfferCreator>(); 
            services.AddTransient<IUserCreator, UserCreator>();
            services.AddTransient<ISellOfferCreator, SellOfferCreator>();
            services.AddTransient<IConfigurationUpdater, ConfigurationUpdater>();

            services.AddDbContext<RepositoryContext>(options => options.UseNpgsql(Configuration.GetConnectionString("TestDB")));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Core Api", Description = "Swagger Core Api" });
                x.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                x.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() }, });
            });

            var tokenConfiguration = Configuration.GetSection("tokenManagement");
            services.Configure<TokenManagement>(tokenConfiguration);
            var tokenManagement = tokenConfiguration.Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(tokenManagement.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
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

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
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
