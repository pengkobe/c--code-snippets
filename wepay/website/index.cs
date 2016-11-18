using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Models;
using System.IO;
using Newtonsoft.Json;
using System.Xml;

namespace PayForWeiXin.Web
{
    /// <summary>
    /// PayForWeiXin 的摘要说明
    /// </summary>
    public class PayForWeiXin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            PayWeiXin model = new PayWeiXin();
            PayForWeiXinHelp PayHelp = new PayForWeiXinHelp();
            string result = string.Empty;
            //传入OpenId
            string openId = context.Request.Form["openId"].ToString();
            //传入红包金额(单位分)
            string amount = context.Request.Form["amount"] == null ? "" : context.Request.Form["amount"].ToString();
            //接叐收红包的用户 用户在wxappid下的openid 
            model.re_openid = openId;//"oFIYdszuDXVqVCtwZ-yIcbIS262k";
            //付款金额，单位分 
            model.total_amount = int.Parse(amount);
            //最小红包金额，单位分 
            model.min_value = int.Parse(amount);
            //最大红包金额，单位分 
            model.max_value = int.Parse(amount);
            //调用方法
            string postData = PayHelp.DoDataForPayWeiXin(model);
            try
            {
                result = PayHelp.PayForWeiXin(postData);
            }
            catch (Exception ex)
            {
                //写日志
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            string jsonResult = JsonConvert.SerializeXmlNode(doc);
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonResult);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}