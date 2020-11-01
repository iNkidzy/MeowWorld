using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MeowWorld.Core.ApplicationService;
using MeowWorld.Core.ApplicationService.Services;
using MeowWorld.Core.DomainService;
using MeowWorld.Infrastructure.data;
using MeowWorld.Infrastructure.data.Repositories;
using Microsoft.AspNetCore.Authentication.Certificate;
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

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;

        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Find more about LoggerFactory
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            }

            );

            if (Environment.IsDevelopment())
            {   //In memory database:
                services.AddDbContext<MEOWcontext>(opt => { opt.UseSqlite("Data Source=MeowWorldApp.db"); }
                );
            }
            //else
            //{
            //    //Azure SQL database:
            //    services.AddDbContext<MEOWcontext>(opt =>
            //        opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection"))
            //        );
            //}

            //AUTHENTICATION

            // Create a byte array with random values. This byte array is used
            // to generate a key for signing JWT tokens.
            //Byte[] secretBytes = new byte[40];
            //Random rand = new Random();
            //rand.NextBytes(secretBytes);

            ////Add JWT based authentication
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //        //ValidAudience = "TodoApiClient",
            //        ValidateIssuer = false,
            //        //ValidIssuer = "TodoApi",
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
            //        ValidateLifetime = true, //validate the expiration and not before values in the token
            //        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
            //    };
            //});


            //services.AddSingleton<IAuthenticationHelper>(new
            //  AuthenticationHelper(secretBytes));


           

            //REPOS implementation in configureServices

            //var serviceCollection = new ServiceCollection();////
            services.AddScoped<ICatRepository, CatRepo>();
            services.AddScoped<ICatService, CatService>();
            services.AddScoped<IOwnerRepository, OwnerRepo>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddTransient<IDBinitializer, DBinitializer>();

            services.AddControllers();

            //NEWtonsoftJson
            services.AddMvc().AddNewtonsoftJson();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers().AddNewtonsoftJson(options =>
            {    // Use the default property (Pascal) casing

                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.MaxDepth = 100;
                //   options.SerializerSettings.MaxDepth = 2;  // 100 pet limit per owner
            });


            //SWAGGER

            // Register the Swagger generator using Swashbuckle.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MeowWorld Swagger",
                    Description = "Welcome to the insides of the MeowWorld.Enjoy!"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });


            //CORS

            //Configure the default CORS policy.
            services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    })
            );

            services.AddAuthentication(
                   CertificateAuthenticationDefaults.AuthenticationScheme)
               .AddCertificate();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                // Initialize the database
                var services = scope.ServiceProvider;
                var ctx = scope.ServiceProvider.GetService<MEOWcontext>();
                //ctx.Database.EnsureDeleted();
                //ctx.Database.EnsureCreated(); <--Reside in the DBinitializer
                var dbInitializer = services.GetService<IDBinitializer>();
                dbInitializer.InitData(ctx);


            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            else
            {
                app.UseHsts();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<MEOWcontext>();
                    ctx.Database.EnsureCreated();
                }
            }

            app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    //c.RoutePrefix = string.Empty;
                });




            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
