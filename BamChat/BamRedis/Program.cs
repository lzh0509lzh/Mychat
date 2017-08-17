using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace BamRedis
{
    class Program
    {
        private static RedisClient redisCli = new RedisClient("104.224.167.94", 6379,"Ms2688");

        static void Main(string[] args)
        {
            bool res = redisCli.Set("city", "beijing");
            if (res == true)
            {
                Console.WriteLine("success!");
                Console.WriteLine("输入你要保存的key值：");
                string w1 = Console.ReadLine();
                Console.WriteLine("输入你要保存的value值：");
                string w2 = Console.ReadLine();
                bool res2 = redisCli.Set(w1, w2);
                if (res2)
                {
                    Console.WriteLine("已保存：" + w1 + "," + redisCli.Get<string>(w1));
                }
                else
                {
                    Console.WriteLine("保存失败！");
                }
            }
            Console.Read();
        }
    }
}
