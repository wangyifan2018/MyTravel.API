using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTravel.API.Database;
using MyTravel.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace MyTravel.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var secretByte = Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:Audience"],

                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(secretByte)
                };
            });

            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                //setupAction.OutputFormatters.Add(
                //    new XmlDataContractSerializerOutputFormatter());
            })
            .AddXmlDataContractSerializerFormatters()
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            })
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "??????",
                        Title = "????????????",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "????????????",
                        Instance = context.HttpContext.Request.Path
                    };
                    problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetail)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
            services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
            //AddTransient ???????????????????????????????? ????????????????????????????????
            //services.AddSingleton ???????? ?????????? ??????????????????????????????
            //services.AddScoped ????????????  ???????????????? ??????????????
            services.AddDbContext<AppDbContext>(option => {
                //option.UseSqlServer("server=localhost; Database=FakeXiechengDb; User Id = sa; Password=yourStrong(!)Password;");
                //option.UseSqlServer(Configuration["DbContext:ConnectionString"]);
                option.UseMySql(
                    Configuration["DbContext:ConnectionString"],
                    ServerVersion.AutoDetect(Configuration["DbContext:ConnectionString"]));
            });

            // ????profile????
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //IApplicationBuilder ?????????? 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ????????
            app.UseRouting();
            // ????????
            app.UseAuthentication();
            // ??????????????????????????
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/test", async context =>
                //{
                //    throw new Exception("test");
                //    //await context.response.writeasync("hello from test!");
                //});

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                endpoints.MapControllers();
            });
        }
    }
}
