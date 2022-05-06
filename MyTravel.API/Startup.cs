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
            services.AddControllers();
            services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
            //AddTransient ÿ�η�������ʱ�����µ����ݲֿ⣬ ���������ע�������ݶ�������Ӱ��
            //services.AddSingleton ����һ�� �ڴ�ռ���� ȱ�㣺�����������ʱ����������
            //services.AddScoped �����������  ����һ�����ݲֿ� �������������
            services.AddDbContext<AppDbContext>(option => {
                //option.UseSqlServer("server=localhost; Database=FakeXiechengDb; User Id = sa; Password=yourStrong(!)Password;");
                option.UseSqlServer(Configuration["DbContext:ConnectionString"]);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //IApplicationBuilder �����м�� 

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
