using HooliCash.API.Filters;
using HooliCash.Core.DbContexts;
using HooliCash.Helpers;
using HooliCash.IHelpers;
using HooliCash.IRepositories;
using HooliCash.IServices;
using HooliCash.Repositories;
using HooliCash.Services;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

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
            services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddControllers();
            services.AddMvc(MvcOptions);
            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate().AddCertificateCache();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "HooliCash.API", Version = "v1" }));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<HooliCashContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenHelper, TokenHelper>();
            services.AddTransient<IPasswordHelper, PasswordHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HooliCash.API v1"));
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private void MvcOptions(MvcOptions options)
        {
            options.Filters.Add<GlobalExceptionFilter>();
            options.Filters.Add<ValidatorActionFilter>();
        }
    }
}
