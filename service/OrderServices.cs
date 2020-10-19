using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDome.service
{
    /// <summary>
    /// IDisposable用于资源释放
    /// </summary>
    public class OrderServices : IOrderService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Disposable:"+this.GetHashCode());
        }
    }
}
