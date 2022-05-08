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
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                //setupAction.OutputFormatters.Add(
                //    new XmlDataContractSerializerOutputFormatter());
            }).AddXmlDataContractSerializerFormatters();
            services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
            //AddTransient 每次发起请求时创建新的数据仓库， 请求结束后注销，数据独立互不影响
            //services.AddSingleton 创建一次 内存占用少 缺点：处理独立请求时共享了数据
            //services.AddScoped 引入事务管理  创建一个数据仓库 事务结束后销毁
            services.AddDbContext<AppDbContext>(option => {
                //option.UseSqlServer("server=localhost; Database=FakeXiechengDb; User Id = sa; Password=yourStrong(!)Password;");
                //option.UseSqlServer(Configuration["DbContext:ConnectionString"]);
                option.UseMySql(
                    Configuration["DbContext:ConnectionString"],
                    ServerVersion.AutoDetect(Configuration["DbContext:ConnectionString"]));
        });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //IApplicationBuilder 创建中间件 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

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
