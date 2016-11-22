using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration; 
using System.Web;
using System.Data.OleDb;

namespace YIPENG.DataBase
{
    /// <summary>
    /// AccessSqlHelper数据访问
    /// </summary>
    internal class AccessSqlHelper : DbSqlHelper<OleDbParameter>
    {
        /// <summary>
        /// 构造AccessSqlHelper数据访问
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public AccessSqlHelper(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// 创建连接对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>DbConnection</returns>
        protected override DbConnection CreateConnection(string connectionString)
        {
            return new OleDbConnection(connectionString);
        }

        /// <summary>
        /// 创建DbDataAdapter
        /// </summary>
        /// <returns>DbDataAdapter</returns>
        protected override DbDataAdapter CreateDataAdapter()
        {
            return new OleDbDataAdapter();
        }
    }
}