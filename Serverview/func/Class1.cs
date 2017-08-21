using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace func
{
    /*
    /命名空间：
    using System.Runtime.InteropServices;
    提供各种各样支持 COM 互操作 及平台调用服务的成员。
    using System.Diagnostics;
    System.Diagnostics 命名空间中，该命名空间提供了用于与事件日志、性能计数器和系统进程进行交互的类 
    */
    /// <summary>
    /// 获取CPU信息
    /// </summary>
    public class CPUInfo
    {
        /// <summary>
        /// 输出CPU信息
        /// </summary>
        /// <returns></returns>
        public string GetCPUInfo()
        {

            StringBuilder sb = new StringBuilder();
            var a = GetCPUCounter();
            int cpuPercent = Convert.ToInt32(GetCPUCounter());
            sb.Append(cpuPercent);
            return sb.ToString();
        }

        /// <summary>
        /// 获取CPU信息
        /// </summary>
        /// <returns></returns>
        private static object GetCPUCounter()
        {
            PerformanceCounter pc = new PerformanceCounter();
            pc.CategoryName = "Processor";
            pc.CounterName = "% Processor Time";
            pc.InstanceName = "_Total";
            dynamic Value_1 = pc.NextValue();
            System.Threading.Thread.Sleep(1000);
            dynamic Value_2 = pc.NextValue();
            return Value_2;
        }
    }

    /// <summary>
    /// 获取内存信息
    /// </summary>
    public class MemoryInfo
    {
        //定义内存的信息结构
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }

        /// <summary>
        /// kernel32.dll是Windows9x/Me中非常重要的32位动态链接库文件，属于内核级文件。
        ///它控制着系统的内存管理、数据的输入输出操作和中断处理，当Windows启动时，kernel32.dll就驻留在内存中特定的写保护区域，使别的程序无法占用这个内存区域。
        /// </summary>

        [DllImport("kernel32")]
        private static extern void GetWindowsDirectory(StringBuilder WinDir, int count);

        [DllImport("kernel32")]
        private static extern void GetSystemDirectory(StringBuilder SysDir, int count);

        [DllImport("kernel32")]
        private static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);



        /// <summary>
        /// 获取内存信息
        /// </summary>
        /// <returns></returns>
        public string GetMemInfo()
        {
            //调用GlobalMemoryStatus函数获取内存的相关信息
            MEMORY_INFO MemInfo = new MEMORY_INFO();
            //System.Threading.Thread.Sleep(1000);
            GlobalMemoryStatus(ref MemInfo);
            //拼接字符串
            StringBuilder sb = new StringBuilder();
            return MemInfo.dwMemoryLoad.ToString();
        }
    }

    /// <summary>
    /// 获取磁盘信息
    /// </summary>
    public class DiskInfo
    {
        /// <summary>
        /// 获取指定驱动器的空间总大小(单位为B) 
        /// 只需输入代表驱动器的字母即可 （大写） 
        /// </summary>
        /// <param name="str_HardDiskName"></param>
        /// <returns></returns>
        public float GetHardDiskSpace(string str_HardDiskName)
        {
            float totalSize = new float();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                }
            }
            return totalSize;
        }

        public string GetHardDiskSpace()
        {
            string str_HardDiskName = "C";
            float totalSize = new float();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                }
            }
            string TotalSize = totalSize.ToString();
            return TotalSize;
        }

        /// <summary>
        /// 获取指定驱动器的剩余空间总大小(单位为B) 
        /// 只需输入代表驱动器的字母即可  
        /// </summary>
        /// <param name="str_HardDiskName"></param>
        /// <returns></returns>
        public string GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            string FreeSpace = "";
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                }
            }
            FreeSpace = freeSpace.ToString();

            return FreeSpace;
        }
    }

    /// <summary>
    /// 获取端口连接数
    /// </summary>
    public class TCPInfo
    {
        public string Get_TCP_Count(ref string Endportname, ref string startportname)
        {
            string TCPCount = "";
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            TCPCount = connections.Count().ToString();
            Endportname = connections[0].LocalEndPoint.ToString();
            startportname = connections[0].RemoteEndPoint.ToString();
            return TCPCount;
        }
    }

    /// <summary>
    /// 转换
    /// </summary>
    public class Trans
    {
        public static decimal ConvertBytes(string b, int iteration)
        {
            long iter = 1;
            for (int i = 0; i < iteration; i++)
                iter *= 1024;
            return Math.Round((Convert.ToDecimal(b)) / Convert.ToDecimal(iter), 2, MidpointRounding.AwayFromZero);
        }
    }

}
