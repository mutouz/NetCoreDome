using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;

namespace NetCoreDome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //UseServiceProviderFactory ע����������������
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  Console.WriteLine("ConfigureWebHostDefaults");
                  webBuilder.UseStartup<Startup>();
                  //ͨ��ֱ�ӵ���Ҳ����
                  #region ��ʹ��stateup
                  //webBuilder.ConfigureServices(services =>
                  //{
                  //    Console.WriteLine("Startup.ConfigureServices");
                  //    services.AddControllers();
                  //});
                  //webBuilder.Configure(app =>
                  //{
                  //    Console.WriteLine("Startup.Configure");

                  //    app.UseHttpsRedirection();

                  //    app.UseRouting();

                  //    app.UseAuthorization();

                  //    app.UseEndpoints(endpoints =>
                  //    {
                  //        endpoints.MapControllers();
                  //    });
                  //});
                  #endregion

              })
              .ConfigureHostConfiguration(builder =>
              {
                  Console.WriteLine("ConfigureHostConfiguration");
              })
            .ConfigureAppConfiguration(builder =>
            {
                Console.WriteLine("ConfigureAppConfiguration");
            })
            .ConfigureServices(service =>
            {
                Console.WriteLine("ConfigureServices");
            });

    }
}
