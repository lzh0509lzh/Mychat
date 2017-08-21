using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using func;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            #region cpu使用率监控
            //CPUInfo cpu = new CPUInfo();
            //int i = 0;
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    Console.WriteLine("CPU load = " + cpu.GetCPUInfo() + " %.");
            //}
            #endregion

            #region 内存使用率监控
            //MemoryInfo memory = new MemoryInfo();
            //while (true)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    Console.WriteLine("Memory Load:" + memory.GetMemInfo() + "%");
            //}
            #endregion

            #region 获取磁盘信息
            //DiskInfo disk = new DiskInfo();

            #endregion

            #region 获取端口连接数
            TCPInfo tcp = new TCPInfo();
            string Endport = "";
            string startport = "";
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine(tcp.Get_TCP_Count(ref Endport,ref startport));
                Console.WriteLine(Endport);
                Console.WriteLine(startport);
            }
            
            #endregion

            Console.Read();
        }

        //public List<string> Get_TCP_Count()
        //{
        //    List<string> list = new List<string>();
        //    string TCPCount = "";
        //    IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
        //    TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

        //    connections.Select(a => list);

        //    TCPCount = connections.Count().ToString();
        //    return list;
        //}
    }
}
