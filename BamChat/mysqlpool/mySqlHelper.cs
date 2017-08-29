using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace mysqlpool
{
    /*数据库连接池*/
    public class mySqlHelper
    {
        private const int MaxPool = 10;//最大连接数
        private const int MinPool = 5;//最小连接数
        private const bool Asyn_Process = true;//设置异步访问数据库
        private const bool Mars = true;
        private const int Conn_Timeout = 15;//设置连接等待时间
        private const int Conn_Lifetime = 15;//设置连接的生命周期

        public void ExecuteScalar()
        {
            string MyConString = "Data Source = 180.76.171.127;" +
                " Database = managecenter;" +
                " User ID = lee; Password = lz2688;" +
                "pooling=true;min pool size=5;max pool size=10;";

        MySqlConnection conn = new MySqlConnection(MyConString);
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    conn.Open();
                    //MySqlCommand command = new MySqlCommand();
                    //command.CommandText = "select firstlistname from functionlistfirst where firstlistid=1";
                    //command.CommandType = System.Data.CommandType.Text;
                    //command.Connection = conn;
                    //ret = command.ExecuteScalar().ToString();
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    string b = ex.ToString();
                }
                finally
                {
                    conn.Close();
                    System.Threading.Thread.Sleep(1000);
                }
            }

        }
    }
}