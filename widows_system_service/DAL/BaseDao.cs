using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYD.EMS.DataBase;
using System.Data.SqlClient;
using System.Data;

namespace HYD.EMS.DAL
{
    /// <summary>
    /// 基类数据访问
    /// </summary>
    public class BaseDao
    {
        /// <summary>
        /// MsSqlHelper数据库执行对象
        /// </summary>
        public MsSqlHelper SqlHelper
        {
            get;
            set;
        }
    }
}