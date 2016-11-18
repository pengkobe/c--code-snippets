using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
//using WebChatSDK;
using HYD.E3.Business.Model;
//using Newtonsoft.Json;
using System.Web;
using System.Threading;
#endregion

namespace HYD.E3.ServerPush
{
    public class ServerPushHandler
    {
        #region 全局变量
        HttpContext m_Context;
        //推送结果
        ServerPushResult _IAsyncResult;

        string name="";
        static int aaa = 0;

        //声明一个集合
        static Dictionary<string, ServerPushResult> dict = new Dictionary<string, ServerPushResult>();
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public ServerPushHandler(HttpContext context, ServerPushResult _IAsyncResult)
        {
            name = context.Request.Form["name"];//连接名称

            this.m_Context = context;
            this._IAsyncResult = _IAsyncResult;
        }
        #endregion

        #region 执行操作
        /// <summary>
        /// 根据Action判断执行方法
        /// </summary>
        /// <returns></returns>
        public ServerPushResult ExecAction()
        {
            switch (m_Context.Request["Action"])
            {
                case "Keepline":
                    Keepline();
                    break;
                case "getMessage":
                    GetMessage();
                    break;
                default:
                    break;
            }
            return _IAsyncResult;
        }
        #endregion

        #region 保持联接
        private void Keepline()
        {
            aaa++;
            if (!dict.ContainsKey(name))
                dict.Add(name, _IAsyncResult);
            else  
                dict[name] = _IAsyncResult;//每次连接上都要重新设值

            Thread.Sleep(3000);
            _IAsyncResult.Result = aaa + "";
            _IAsyncResult.Send();
        }
        #endregion

        private void GetMessage() {
            aaa++;
            if (dict.ContainsKey(name))
            {
                dict[name].Result = aaa + "";
                dict[name].Send();
            }
            //原请求返回
            _IAsyncResult.Result = "xxx";
            _IAsyncResult.Send();
        }
    }
}
