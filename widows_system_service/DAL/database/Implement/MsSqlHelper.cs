using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;

namespace HYD.EMS.DataBase {
    /// <summary>
    /// SqlServer数据访问,执行完毕请释放资源
    /// MsSqlHelper msSql = DbUtility.MsSqlHelper;
    /// try
    /// {
    ///    ......
    /// }
    /// finally
    /// {
    ///     msSql.Dispose();
    /// }
    /// </summary>
    public class MsSqlHelper : DbSqlHelper<SqlParameter> {
        /// <summary>
        /// 构造SqlServer数据访问
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public MsSqlHelper(string connectionString)
            : base(connectionString) {
            //this.Connection.Open();
        }

        /// <summary>
        /// 创建连接对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>DbConnection</returns>
        protected override DbConnection CreateConnection(string connectionString) {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// 创建DbDataAdapter
        /// </summary>
        /// <returns>DbDataAdapter</returns>
        protected override DbDataAdapter CreateDataAdapter() {
            return new SqlDataAdapter();
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(CommandType cmdType, string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteNonQuery(cmdType, cmdText, dbParameter);
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(CommandType cmdType, string cmdText) {
            return base.ExecuteNonQuery(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句返回记录集
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>记录集</returns>
        public virtual DbDataReader ExecuteReader(CommandType cmdType, string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteReader(cmdType, cmdText, dbParameter);
        }

        /// <summary>
        /// 执行SQL语句返回记录集
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>记录集</returns>
        public virtual DbDataReader ExecuteReader(CommandType cmdType, string cmdText) {
            return base.ExecuteReader(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>        
        /// <returns>返回数据表</returns>
        public virtual DataTable ExecuteTable(CommandType cmdType, string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteTable(cmdType, cmdText, dbParameter);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>返回数据表</returns>
        public virtual DataTable ExecuteTable(CommandType cmdType, string cmdText) {
            return base.ExecuteTable(cmdType, cmdText, null);
        }

        /// <summary>
        /// 返回执行SQL语句第一个值
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>        
        /// <returns>执行结果</returns>
        public virtual object ExecuteScalar(CommandType cmdType, string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteScalar(cmdType, cmdText, dbParameter);
        }

        /// <summary>
        /// 返回执行SQL语句第一个值
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>执行结果</returns>
        public virtual object ExecuteScalar(CommandType cmdType, string cmdText) {
            return base.ExecuteScalar(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句返回记录集
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>记录集</returns>
        public virtual DbDataReader ExecuteReader(string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteReader(cmdText, dbParameter);
        }

        /// <summary>
        /// 执行SQL语句返回记录集
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>记录集</returns>
        public virtual DbDataReader ExecuteReader(string cmdText) {
            return base.ExecuteReader(cmdText, null);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>返回数据表</returns>
        public virtual DataTable ExecuteTable(string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteTable(cmdText, dbParameter);
        }


        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>        
        /// <returns>返回数据DataSet</returns>
        public virtual DataSet ExecuteDataSet(CommandType cmdType, string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteDataSet(cmdType, cmdText, dbParameter);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>返回数据表</returns>
        public virtual DataTable ExecuteTable(string cmdText) {
            return base.ExecuteTable(cmdText, null);
        }
        /// <summary>
        /// 返回执行SQL语句第一个值
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>执行结果</returns>
        public virtual object ExecuteScalar(string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteScalar(cmdText, dbParameter);
        }


        /// <summary>
        /// 返回执行SQL语句第一个值
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>执行结果</returns>
        public virtual object ExecuteScalar(string cmdText) {
            return base.ExecuteScalar(cmdText, null);
        }
        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(string cmdText, SqlParameterCollection dbParameter) {
            return base.ExecuteNonQuery(cmdText, dbParameter);
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(string cmdText) {
            return base.ExecuteNonQuery(cmdText, null);
        }

        /// <summary>
        /// 执行动态分页方法
        /// </summary>
        /// <param name="cmdText">分页的SQL语句</param>
        /// <param name="dbParameter">动态参数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="sortName">排序列</param>
        /// <param name="sortOrder">desc/asc</param>
        /// <returns>数据</returns>
        public virtual DataTable ExecutePaging(string cmdText, SqlParameterCollection dbParameter, int pageIndex, int pageSize, string sortName, string sortOrder, out int total) {
            StringBuilder sb = new StringBuilder();
            if (dbParameter == null) {
                dbParameter = new SqlParameterCollection();
            }

            sb.Append("select count(0) from (" + cmdText + ") t where 1=1 ");
            total = (Int32)this.ExecuteScalar(sb.ToString(), dbParameter);
            sb.Clear();
            sb.Append("select * from ");
            sb.Append("(");
            cmdText = "select ROW_NUMBER() over (order by " + sortName + " " + sortOrder + ") as RowID," + cmdText.Trim().Remove(0, 6);
            sb.Append(cmdText);
            sb.Append(")a where RowID between (@pageIndex-1)*@pagesize+1 AND @pageIndex * @pagesize ");

            if (sortName.IndexOf('.') >= 0) {
                sortName = sortName.Substring(sortName.IndexOf('.') + 1, sortName.Length - sortName.IndexOf('.') - 1);
            }
            sb.Append("order by " + sortName + " " + sortOrder);
            dbParameter.Add("pageIndex", pageIndex, DbType.Int32);
            dbParameter.Add("pageSize", pageSize, DbType.Int32);
            DataTable data = this.ExecuteTable(sb.ToString(), dbParameter);
            return data;
        }
    }
}