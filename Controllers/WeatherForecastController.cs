using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreDome.service;

namespace NetCoreDome.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// FromServices 寻找服务中容器注入
        /// IHostApplicationLifetime 管理整个应用的生命周期
        /// </summary>
        /// <param name="observice1"></param>
        /// <param name="orderService2"></param>
        /// <param name="hostApplicationLifetime"></param>
        /// <returns></returns>
        [HttpGet]
        public Object Get([FromServices] IOrderService observice1,
            [FromServices] IOrderService orderService2
            , [FromServices] IHostApplicationLifetime hostApplicationLifetime
            ,bool stop = false
            )
        {
            #region MyRegion
            Console.WriteLine("======1===========");
            //在创建子容器
            using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IOrderService>();
            }
            Console.WriteLine("==============2==================");
            #endregion
            Console.WriteLine("接口请求结束");
            if (stop)
            {
                //关闭应用程序结束整个应用程序生命周期
                hostApplicationLifetime.StopApplication();
            }
            return 1;
        }

    }
}
