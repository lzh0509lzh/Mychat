using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Bamboo_Socket
{
    /// <summary>
    /// socketHandler 的摘要说明
    /// </summary>
    public class socketHandler : IHttpHandler
    {
        private static Dictionary<string, WebSocket> CONNECT_POOL = new Dictionary<string, WebSocket>();//用户连接池
        private static Dictionary<string, string> GROUP_POOL = new Dictionary<string, string>();//用户桌牌记录池
        private static Dictionary<string, List<MessageInfo>> MESSAGE_POOL = new Dictionary<string, List<MessageInfo>>();//离线消息池

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessChat1);
            }
        }

        private async Task ProcessChat1(AspNetWebSocketContext context)
        {
            WebSocket socket = context.WebSocket;//当前WebSocket实例

            string user = context.QueryString["user"].ToString();
            string table = context.QueryString["table"].ToString();
            GROUP_POOL[user] = table;

            try
            {
                #region 记录用户桌牌
                if (!GROUP_POOL.ContainsKey(user))
                    GROUP_POOL.Add(user, table);
                else
                    
                #endregion

                #region 用户添加连接池
                //第一次open时，添加到连接池中
                if (!CONNECT_POOL.ContainsKey(user))
                    CONNECT_POOL.Add(user, socket);//不存在，添加
                else
                    if (socket != CONNECT_POOL[user])//当前对象不一致，更新
                    CONNECT_POOL[user] = socket;
                #endregion

                string descUser = string.Empty;//目的用户
                while (true)
                {
                    if (socket.State == WebSocketState.Open)
                    {
                        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);//通信数据格式
                        
                        WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);//接收到的数据

                        #region 消息处理（字符截取、消息转发）
                        try
                        {
                            #region 关闭Socket处理，删除连接池，同时删除用户桌牌记录池
                            if (socket.State != WebSocketState.Open)//连接关闭
                            {
                                if (CONNECT_POOL.ContainsKey(user)) CONNECT_POOL.Remove(user);//删除连接池
                                if (GROUP_POOL.ContainsKey(user)) GROUP_POOL.Remove(user);//删除用户桌牌记录池
                                break;
                            }
                            #endregion

                            string userMsg = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);//接收到的消息

                            #region 寻找同桌user（目的用户），并推送消息
                            foreach (var model in GROUP_POOL)
                            {
                                if (model.Value == table)
                                {
                                    WebSocket destSocket = CONNECT_POOL[model.Key];//目的客户端
                                    await destSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                                }
                            }
                            #endregion


                        }
                        catch (Exception exs)
                        {
                            //消息转发异常处理，本次消息忽略 继续监听接下来的消息
                            exs.ToString();
                        }
                        #endregion
                    }
                    else
                    {
                        break;
                    }
                }//while end
            }
            catch (Exception ex)
            {
                ex.ToString();
                //整体异常处理
                if (CONNECT_POOL.ContainsKey(user)) CONNECT_POOL.Remove(user);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 离线消息
    /// </summary>
    public class MessageInfo
    {
        public MessageInfo(DateTime _MsgTime, ArraySegment<byte> _MsgContent)
        {
            MsgTime = _MsgTime;
            MsgContent = _MsgContent;
        }
        public DateTime MsgTime { get; set; }
        public ArraySegment<byte> MsgContent { get; set; }
    }
}