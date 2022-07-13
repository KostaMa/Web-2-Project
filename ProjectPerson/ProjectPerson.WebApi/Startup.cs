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
using Project.DataAccess.Repository;
using ProjectPerson.Common.Mapping;
using ProjectPerson.DataAccess;
using ProjectPerson.DataAccess.IRepository;
using ProjectPerson.DataAccess.Repository;
using ProjectPerson.Service.EmailService;
using ProjectPerson.Service.IService;
using ProjectPerson.Service.Service;
using ProjectPerson.WebApi.Authentication;
using ProjectPerson.WebApi.Authorization;

namespace ProjectPerson.WebApi
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectPerson.WebApi", Version = "v1" });
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();


            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();

            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped(typeof(IEmailSender), typeof(EmailSender));

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.WebApi v1"));
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
            app.UseMiddleware<ProjectPerson.WebApi.Authentication.JwtMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
