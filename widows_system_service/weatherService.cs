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
//
using System.Web.Script.Serialization;

using YIPENG.Business.DAL;
using YIPENG.Business.Model;
using YIPENG.Business;


namespace YIPENG.E3.SystemService
{
    partial class weatherService : ServiceBase
    {
        const string apiKey = "";
        const string url = "http://apis.baidu.com/heweather/weather/free";

        public weatherService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //在系统事件查看器里的应用程序事件里来源的描述
            EventLog.WriteEntry("我的服务启动");
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval =60 * 60000;
            //到达时间的时候执行事件；
            t.Elapsed += new System.Timers.ElapsedEventHandler(ChkHour);
            //设置是执行一次（false）还是一直执行(true)；
            t.AutoReset = true;
            //是否执行System.Timers.Timer.Elapsed事件；
            t.Enabled = true;

            System.Timers.Timer daily = new System.Timers.Timer();
            daily.Interval = 24 * 60* 60000 ;
            daily.Elapsed += new System.Timers.ElapsedEventHandler(ChkDaily);
            daily.AutoReset = true;
            daily.Enabled = true;

            // 第一次立即执行
            FindCityToRequest();
            FindCityToRequest_day();
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
            writestr("服务停止");
            EventLog.WriteEntry("我的服务停止");
        }

        /// <summary>
        /// 定时检查，并执行方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void ChkHour(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer tt = (System.Timers.Timer)source;
                tt.Enabled = false;
                writestr("服务运行");
                FindCityToRequest();
                tt.Enabled = true;
            }
            catch (Exception err)
            {
                writestr(err.Message);
                writestr(err.StackTrace);
            }
        }

        public void ChkDaily(object source, System.Timers.ElapsedEventArgs e)
        {
             try
             {
                 System.Timers.Timer tt = (System.Timers.Timer)source;
                 tt.Enabled = false;
                 writestr("服务运行");
                 FindCityToRequest_day();
                 tt.Enabled = true;
             }
             catch (Exception err)
             {
                 writestr(err.Message);
                 writestr(err.StackTrace);
             }
        }

        public void FindCityToRequest()
        {
            WeatherBLL bll = new WeatherBLL();
            List<WeatherCity> wcitys = bll.getWeatherCities();
            foreach (WeatherCity c in wcitys)
            {
                SetInnPoint_hour(c.CityName, c.CityCode);
            }
        }

        public void FindCityToRequest_day()
        {
            WeatherBLL bll = new WeatherBLL();
            List<WeatherCity> wcitys = bll.getWeatherCities();
            foreach (WeatherCity c in wcitys)
            {
                SetInnPoint_day(c.CityName, c.CityCode);
            }
        }

        public void SetInnPoint_hour(string cityname, string citycode)
        {
            try
            {
                string param = "city=" + cityname;
                string strURL = url + '?' + param;
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "GET";
                // 添加header
                request.Headers.Add("apikey", apiKey);
                System.Net.HttpWebResponse response;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.Stream s;
                s = response.GetResponseStream();
                string StrDate = "";
                string strValue = "";
                StreamReader Reader = new StreamReader(s, Encoding.UTF8);
                while ((StrDate = Reader.ReadLine()) != null)
                {
                    strValue += StrDate;//+ "\r\n"
                }
                writestr("天气：" + strValue);
                // ======处理返回值======
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> json = serializer.DeserializeObject(strValue) as Dictionary<string, object>;
                List<Object> list_obj = json["HeWeather data service 3.0"] as List<Object>;
                // 7个数据和！
                Dictionary<string, object> sevendata = ((object[])(json["HeWeather data service 3.0"]))[0] as Dictionary<string, object>;
                Dictionary<string, object> boj_d = sevendata["now"] as Dictionary<string, object>;
                ProcessNow(boj_d, citycode);
            }
            catch (Exception err)
            {
                writestr(err.Message);
                writestr(err.StackTrace);
            }
        }

        // 存储实时数据
        public void ProcessNow(Dictionary<string, object> boj_d, string citycode)
        {
            try
            {
                WeatherBLL bll = new WeatherBLL();
                WeatherDetailData wdd = new WeatherDetailData();
                wdd.CityCode = citycode;

                wdd.CollectTime = DateTime.Now;
                // 湿度
                wdd.Humidity = decimal.Parse(boj_d["hum"].ToString());
                // 温度
                wdd.Temperature = decimal.Parse(boj_d["tmp"].ToString());
                // 备注
                wdd.Notes = "";
                bll.addWeatherDataDetail(wdd);
            }
            catch (Exception err)
            {
                writestr(err.Message);
                writestr(err.StackTrace);
            }
        }

        public void SetInnPoint_day(string cityname, string citycode)
        {
            try
            {
                string param = "city=" + cityname;
                string strURL = url + '?' + param;
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "GET";
                // 添加header
                request.Headers.Add("apikey", apiKey);
                System.Net.HttpWebResponse response;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.Stream s;
                s = response.GetResponseStream();
                string StrDate = "";
                string strValue = "";
                StreamReader Reader = new StreamReader(s, Encoding.UTF8);
                while ((StrDate = Reader.ReadLine()) != null)
                {
                    strValue += StrDate;//+ "\r\n"
                }

                // ======处理返回值======
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> json = serializer.DeserializeObject(strValue) as Dictionary<string, object>;
                Dictionary<string, object> sevendata = ((object[])(json["HeWeather data service 3.0"]))[0] as Dictionary<string, object>;
                object boj_d = sevendata["daily_forecast"];
                ProcessDaily(boj_d, citycode);
            }
            catch (Exception err)
            {
                writestr(err.Message);
                writestr(err.StackTrace);
            }
        }

        // 存储日常数据
        public void ProcessDaily(object daily_forecast, string citycode)
        {

            try
            {
                WeatherBLL bll = new WeatherBLL();
                for (int i = 0; i < 7; i++) {
                    Dictionary<string, object> list_obj = ((object[])(daily_forecast))[i] as Dictionary<string, object>;
                    WeatherData wdd = new WeatherData();
                    wdd.CityCode = citycode;
                    wdd.CollectDate = DateTime.Parse(list_obj["date"].ToString());

                    Dictionary<string, object> temp = list_obj["tmp"] as Dictionary<string, object>;
                    wdd.HighHumidity =Decimal.Parse(list_obj["hum"].ToString());
                    wdd.LowHumidity =Decimal.Parse(list_obj["hum"].ToString());
                    wdd.LowTemperature = Decimal.Parse(temp["min"].ToString());
                    wdd.HighTemperature = Decimal.Parse(temp["max"].ToString());
                    wdd.Notes ="";
                    bll.addWeatherData(wdd);
                }
            }
            catch (Exception err)
            {
                writestr(err.Message);
                writestr(err.StackTrace);
            }
        }

        ///在指定时间过后执行指定的表达式
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
            //debug=======================
            StreamWriter dout = new StreamWriter(@"c:\" + "WServ_InnPointLog.txt", true);
            dout.Write("\r\n事件：" + readme + "\r\n操作时间：" + System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
            //debug=======================
            dout.Close();
        }
    }
}
