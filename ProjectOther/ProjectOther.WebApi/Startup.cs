using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectOther.Common.Mapping;
using ProjectOther.DataAccess;
using ProjectOther.DataAccess.IRepository;
using ProjectOther.DataAccess.Repository;
using ProjectOther.Service.IService;
using ProjectOther.Service.Service;
using ProjectOther.WebApi.Authentication;
using ProjectOther.WebApi.Authorization;

namespace ProjectOther.WebApi
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
            services.AddCors();
            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.WebApi", Version = "v1" });
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddOptions();
            services.AddDbContext<DataAccessContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Web2DBConnection")), ServiceLifetime.Transient);

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrederRepository, OrderRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();


            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddDatabaseDeveloperPageExceptionFilter();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, DataAccessContext dataContext, IWebHostEnvironment env)
        {
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectOther.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .WithOrigins("https://localhost:7015")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<ProjectOther.WebApi.Authentication.JwtMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
