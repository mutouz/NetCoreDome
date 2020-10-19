using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDome.service
{
    public interface IMyService
    {
        void ShowCode();
    }
    
    public class MyServerice:IMyService
    {
        public void ShowCode()
        {
            Console.WriteLine("showCode"+GetHashCode());
        }
    }
    public class MyServericeV2 : IMyService
    {
        public MyServerice NameService { get; set; }
        public void ShowCode()
        {
            Console.WriteLine("showCodev2" + GetHashCode(),NameService=null);
        }


    }
    public class MyNameService
    {

    }
}
