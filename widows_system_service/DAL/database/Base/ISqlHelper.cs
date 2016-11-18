using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace HYD.EMS.DataBase
{
    /// <summary>
    /// ISqlHelper接口:对SqlHelper的统一描述
    /// </summary>
    public interface ISqlHelper<T> : IDisposable
    {

        /// <summary>
        /// 获取当前的数据库连接对象
        /// </summary>
        DbConnection Connection
        {
            get;
        }


        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();

        ///// <summary>
        ///// 执行SQL语句返回受影响的行数
        ///// </summary>
        ///// <param name="cmdType">语句类型</param>
        ///// <param name="cmdText">SQL语句</param>
        ///// <returns>受影响的行数</returns>
        //int ExecuteNonQuery(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// 执行SQL语句返回受影响的行数
        ///// </summary>
        ///// <param name="cmdText">SQL语句</param>
        ///// <returns>受影响的行数</returns>
        //int ExecuteNonQuery(string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// 执行SQL语句返回记录集
        ///// </summary>
        ///// <param name="cmdType">语句类型</param>
        ///// <param name="cmdText">SQL语句</param>
        ///// <returns>记录集</returns>
        //DbDataReader ExecuteReader(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// 执行SQL语句返回记录集
        ///// </summary>
        ///// <param name="cmdText">SQL语句</param>
        ///// <returns>记录集</returns>
        //DbDataReader ExecuteReader(string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// 执行SQL语句
        ///// </summary>
        ///// <param name="cmdType">语句类型</param>
        ///// <param name="cmdText">SQL语句</param>
        ///// <returns>返回数据表</returns>
        //DataTable ExecuteTable(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// 执行SQL语句
        ///// </summary>
        ///// <param name="cmdText">SQL语句</param>
        ///// <returns>返回数据表</returns>
        //DataTable ExecuteTable(string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// 返回执行SQL语句第一个值
        ///// </summary>
        ///// <param name="cmdType">语句类型</param>
        ///// <param name="cmdText">SQL语句</param>
        ///// <returns>执行结果</returns>
        //object ExecuteScalar(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// 返回执行SQL语句第一个值
        ///// </summary>
        ///// <param name="cmdText">SQL语句</param>
        ///// <returns>执行结果</returns>
        //object ExecuteScalar(string cmdText, ParameterCollection<T> dbParameter);
    }
}