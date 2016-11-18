using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace HYD.EMS.DataBase
{
    /// <summary>
    /// 创建数据库访问对象
    /// </summary>
    public class DbUtility
    {
        /// <summary>
        /// 创建一个新的MsSqlHelper对象
        /// </summary>
        public static MsSqlHelper MsSqlHelper
        {
            get
            {
                return new MsSqlHelper("Data Source=120.24.54.92;timeout=210;Initial Catalog=HEFOS;User ID=xx;Password=xxx");
            }
        }
    }
}
