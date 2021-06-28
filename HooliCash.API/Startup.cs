using HooliCash.API.Extensions;
using HooliCash.API.Filters;
using HooliCash.Core.DbContexts;
using HooliCash.Helpers;
using HooliCash.IHelpers;
using HooliCash.IRepositories;
using HooliCash.IServices;
using HooliCash.Repositories;
using HooliCash.Services;
using HooliCash.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Text;

namespace HooliCash.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(ConfigureCorsOptions);
            services.AddControllers(ConfigureMvcOptions);
            services.AddAuthentication(ConfigureAuthenticationOptions).AddJwtBearer(Jwt.DefaultScheme, ConfigureJwtBearerOptions);
            services.AddSwaggerGen(ConfigureSwaggerGenOptions);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<HooliCashContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<ITokenHelper, TokenHelper>();
            services.AddTransient<IPasswordHelper, PasswordHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(ConfigureSwaggerUIOptions);
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void ConfigureSwaggerGenOptions(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "HooliCash.API", Version = "v1" });
        }

        private void ConfigureSwaggerUIOptions(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "HooliCash.API v1");
        }

        private void ConfigureCorsOptions(CorsOptions options)
        {
            options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }

        private void ConfigureMvcOptions(MvcOptions options)
        {
            options.Filters.Add<ValidatorActionFilter>(int.MinValue); // user min value for order to override default filter
        }

        private void ConfigureAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = Jwt.DefaultScheme;
            options.DefaultChallengeScheme = Jwt.DefaultScheme;
        }

        private void ConfigureJwtBearerOptions(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Jwt.Secret)),
                ValidateIssuer = true,
                ValidIssuer = Jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = Jwt.Audience,
                ValidateLifetime = true,
                ClockSkew = Jwt.TokenLifetime
            };
        }
    }
}
