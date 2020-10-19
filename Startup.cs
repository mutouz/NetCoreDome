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
        /// Ĭ������ע�뷽��
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Startup.ConfigureServices");
            services.AddControllers();
            //����ע�� �ӿ�ʵ�� �������ģʽ
            //[fromservices]ֱ�ӻ�ȡע���ʵ��
            //services.AddTransient<IOrderService, OrderServices>();//ÿ�ε��ö�ִ��
            services.AddScoped<IOrderService, OrderServices>();//��������
        }
        /// <summary>
        /// ��ӵ�������ע�뷽�� ConfigureContainer Autofac������ �����ԭ�е�
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder) 
        {
            //autofac ��ע�뷽ʽ
            builder.RegisterType<MyServerice>().As<IMyService>();
            #region ����ע��
            builder.RegisterType<MyServericeV2>().Named<IMyService>("service2");
            #endregion

            #region ����ע��
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
            //ʹ��autofac����
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
          //��ȡʵ��
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
