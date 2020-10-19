using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreDome.service;

namespace NetCoreDome
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 默认容器注入方法
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Startup.ConfigureServices");
            services.AddControllers();
            //依赖注入 接口实现 抽象分离模式
            //[fromservices]直接获取注入的实例
            //services.AddTransient<IOrderService, OrderServices>();//每次调用都执行
            services.AddScoped<IOrderService, OrderServices>();//有作用域
        }
        /// <summary>
        /// 添加的新容器注入方法 ConfigureContainer Autofac的容器 会替代原有的
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder) 
        {
            //autofac 的注入方式
            builder.RegisterType<MyServerice>().As<IMyService>();
            #region 命名注册
            builder.RegisterType<MyServericeV2>().Named<IMyService>("service2");
            #endregion

            #region 属性注册
            builder.RegisterType<MyServericeV2>().As<IMyService>().PropertiesAutowired();
            #endregion

            #region AOP
            //builder.RegisterType<>
            #endregion
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public ILifetimeScope AutofacContainer;
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //使用autofac容器
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
          //获取实例
            var service = this.AutofacContainer.ResolveNamed<IMyService>("service2");
            Console.WriteLine("Startup.Configure");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
