using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BamChat.P_Socket
{
    /*数据库连接池*/
    public class mySqlHelper
    {
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