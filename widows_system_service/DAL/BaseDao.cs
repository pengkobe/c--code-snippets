using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YIPENG.DataBase;
using System.Data.SqlClient;
using System.Data;

namespace YIPENG.DAL
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