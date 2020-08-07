using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CustomerManagement.API.Filters;
using CustomerManagement.API.Middlewares;
using CustomerManagement.API.Services;
using CustomerManagement.Data.Implementations;
using CustomerManagement.Data.Interfaces;
using CustomerManagement.Data.MsSqlDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CustomerManagement.API
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
            //adding filter globally...
            services.AddControllers(options => { options.Filters.Add(typeof(ExceptionFilter)); })
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        };
                    }
                )
                .AddXmlDataContractSerializerFormatters();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder => builder.WithOrigins("http://www.apirequest.io", "http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("*").WithMethods("GET", "POST", "GET, POST, PUT, DELETE, OPTIONS")
                        .AllowAnyOrigin());
            });
            
            services.AddHttpContextAccessor();
            
           
            
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                //For MS SQL Data Dependency
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("CustomerManagement.API"));
                
                //For Postgre SQL Data Dependency
                // options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                //     b => b.MigrationsAssembly("Ottobo.Api"));
            });
            
            services.AddScoped<IUnitOfWork>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var applicationDbContext = serviceProvider.GetService<ApplicationDbContext>();

                return new UnitOfWork<ApplicationDbContext>(applicationDbContext);
            });
            
            
            services.AddAutoMapper(typeof(Startup));
            
            services.AddScoped<TitleService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<TitleService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new TitleService(logger, unitOfWork, "");
            });
            
            services.AddScoped<CompanyService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<CompanyService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new CompanyService(logger, unitOfWork, "");
            });
            
            
            services.AddScoped<CustomerService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<CustomerService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new CustomerService(logger, unitOfWork, "Company,Title");
            });
            
            
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",

                    Title = "Customer .Api Web API",
                    Description = "This is a Web API for Customer.Api Clients",
                    License = new OpenApiLicense()
                    {
                        Name = "MIT"
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Serhan URAS",
                        Email = "serhan.uras@ottobo.com",
                        Url = new Uri("http://www.ottobo.com")
                    }
                });
                
                config.SchemaFilter<SnakeCaseSchemaFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
            
            services.AddCors(options =>
            {
               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseSwagger();
            app.UseSwaggerUI(config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Management API"); });
            app.UseMiddleware<OptionsMiddleware>();
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigins");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}