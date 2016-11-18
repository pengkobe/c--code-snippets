using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;

namespace HYD.E3.SystemService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("我的服务启动");//在系统事件查看器里的应用程序事件里来源的描述
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 60000;
            t.Elapsed += new System.Timers.ElapsedEventHandler(ChkSrv);//到达时间的时候执行事件； 
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
        }

        /// <summary>
        /// 定时检查，并执行方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void ChkSrv(object source, System.Timers.ElapsedEventArgs e)
        {
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;

            if (intHour == 13 && intMinute == 30 && intSecond == 00) ///定时设置,判断分时秒
            {
                try
                {
                    System.Timers.Timer tt = (System.Timers.Timer)source;
                    tt.Enabled = false;
                    SetInnPoint();
                    tt.Enabled = true;
                }
                catch (Exception err) 
                {
                    writestr(err.Message);
                }
            }
        }

        //我的方法
        public void SetInnPoint()
        {
            try
            {
                writestr("服务运行");

                string url = "http://apis.baidu.com/heweather/weather/free";
                string param = "city=beijing";
                //这里执行你的东西
                string strURL = url + '?' + param;
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "GET";
                // 添加header
                request.Headers.Add("apikey", "b516f557073749c7291363cd156ec92f");
                System.Net.HttpWebResponse response;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.Stream s;
                s = response.GetResponseStream();
                string StrDate = "";
                string strValue = "";
                StreamReader Reader = new StreamReader(s, Encoding.UTF8);
                while ((StrDate = Reader.ReadLine()) != null)
                {
                    strValue += StrDate + "\r\n";
                }

                // 处理返回值


            }
            catch (Exception err)
            {
                writestr(err.Message);
            }
        }

        ///在指定时间过后执行指定的表达式
        ///
        ///事件之间经过的时间（以毫秒为单位）
        ///要执行的表达式
        public static void SetTimeout(double interval, Action action)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Enabled = false;
                action();
            };
            timer.Enabled = true;
        }



        // 日志
        public void writestr(string readme)
        {
            //debug==================================================
            //StreamWriter dout = new StreamWriter(@"c:\" + System.DateTime.Now.ToString("yyyMMddHHmmss") + ".txt");
            StreamWriter dout = new StreamWriter(@"c:\" + "WServ_InnPointLog.txt", true);
            dout.Write("\r\n事件：" + readme + "\r\n操作时间：" + System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
            //debug==================================================
            dout.Close();
        }

        protected override void OnStop()
        {
            writestr("服务停止");
            EventLog.WriteEntry("我的服务停止");
        }
    }
}
