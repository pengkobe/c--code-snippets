using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data; 
using System.Web;

namespace HYD.EMS.DataBase
{
    public abstract class DbSqlHelper<T> : ISqlHelper<T>
    {
        /// <summary>
        /// 事务对象
        /// </summary>
        private DbTransaction transaction;

        /// <summary>
        /// 结果集
        /// </summary>
        private DbDataReader reader;

        /// <summary>
        /// 当前连接对象
        /// </summary>
        protected DbConnection connection;

        /// <summary>
        /// 执行对象
        /// </summary>
        protected DbCommand command;

        /// <summary>
        /// 获取DbDataAdapter
        /// </summary>
        protected DbDataAdapter dataAdapter;


        /// <summary>
        /// 创建连接对象
        /// </summary>
        /// <returns>创建的连接</returns>
        protected abstract DbConnection CreateConnection(string connectionString);

        /// <summary>
        /// 创建DataAdapter对象
        /// </summary>
        /// <returns>创建的DataAdapter对象</returns>
        protected abstract DbDataAdapter CreateDataAdapter();

        /// <summary>
        /// 构造对象实例
        /// </summary>
        public DbSqlHelper(string connectionString)
        {
            this.connection = this.CreateConnection(connectionString);
            this.command = this.connection.CreateCommand();
            this.dataAdapter = this.CreateDataAdapter();
        }

        /// <summary>
        /// 获取当前的连接对象
        /// </summary>
        public DbConnection Connection
        {
            get { return connection; }
        }


        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>        
        /// <returns>受影响的行数</returns>
        protected int ExecuteNonQuery(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter)
        {
            int val = 0;
            this.PrepareCommand(this.command, this.connection, this.transaction, cmdType, cmdText, dbParameter);
            val = this.command.ExecuteNonQuery();
            this.command.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行SQL语句返回记录集
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>记录集</returns>
        protected DbDataReader ExecuteReader(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter)
        {
            if (this.reader != null && !this.reader.IsClosed)
            {
                this.reader.Close();
                this.reader.Dispose();
                this.reader = null;
            }
            this.PrepareCommand(this.command, this.connection, this.transaction, cmdType, cmdText, dbParameter);
            reader = this.command.ExecuteReader();
            this.command.Parameters.Clear();
            return reader;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>        
        /// <returns>返回数据表</returns>
        protected DataTable ExecuteTable(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter)
        {
            if (this.reader != null && !this.reader.IsClosed)
            {
                this.reader.Close();
                this.reader.Dispose();
                this.reader = null;
            }
            DataTable dt = new DataTable();
            this.PrepareCommand(this.command, this.connection, this.transaction, cmdType, cmdText, dbParameter);
            this.dataAdapter.SelectCommand = this.command;
            this.dataAdapter.Fill(dt);
            this.command.Parameters.Clear();
            return dt;
        }



        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>        
        /// <returns>返回数据DataSet</returns>
        protected DataSet ExecuteDataSet(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter)
        {
            if (this.reader != null && !this.reader.IsClosed)
            {
                this.reader.Close();
                this.reader.Dispose();
                this.reader = null;
            }
            DataSet ds = new DataSet();
            this.PrepareCommand(this.command, this.connection, this.transaction, cmdType, cmdText, dbParameter);
            this.dataAdapter.SelectCommand = this.command;
            this.dataAdapter.Fill(ds);
            this.command.Parameters.Clear();
            return ds;
        }

        /// <summary>
        /// 返回执行SQL语句第一个值

        /// </summary>
        /// <param name="cmdType">语句类型</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>        
        /// <returns>执行结果</returns>
        protected object ExecuteScalar(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter)
        {
            object val = null;
            this.PrepareCommand(this.command, this.connection, this.transaction, cmdType, cmdText, dbParameter);
            val = this.command.ExecuteScalar();
            this.command.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// 执行SQL语句返回记录集
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>记录集</returns>
        protected DbDataReader ExecuteReader(string cmdText, ParameterCollection<T> dbParameter)
        {
            return this.ExecuteReader(CommandType.Text, cmdText, dbParameter);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>返回数据表</returns>
        protected DataTable ExecuteTable(string cmdText, ParameterCollection<T> dbParameter)
        {
            return this.ExecuteTable(CommandType.Text, cmdText, dbParameter);
        }

        /// <summary>
        /// 返回执行SQL语句第一个值

        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>执行结果</returns>
        protected object ExecuteScalar(string cmdText, ParameterCollection<T> dbParameter)
        {
            return this.ExecuteScalar(CommandType.Text, cmdText, dbParameter);
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="dbParameter">参数集合</param>
        /// <returns>受影响的行数</returns>
        protected int ExecuteNonQuery(string cmdText, ParameterCollection<T> dbParameter)
        {
            return this.ExecuteNonQuery(CommandType.Text, cmdText, dbParameter);
        }
        

        private void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, ParameterCollection<T> cmdParms)
        {
            this.command.Parameters.Clear();

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (T parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        #region 数据库事务处理

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            if (this.connection.State != ConnectionState.Open)
            {
                this.connection.Open();
            }
            transaction = this.connection.BeginTransaction();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
            }
        }
        #endregion

        private bool alreadyDisposed = false;

        /// <summary>
        /// 回收资源
        /// </summary>
        public void Dispose()
        {
            //调用带参数的Dispose方法, 释放托管和非托管资源
            Dispose(true);
            //手动调用了Dispose释放资源，那么析构函数就是不必要的了, 这里阻止GC调用析构函数        
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// protected的Dispose方法, 保证不会被外部调用,传入bool值disposing以确定是否释放托管资源
        /// </summary>
        protected void Dispose(bool disposing)
        {
            //保证不重复释放  
            if (alreadyDisposed)
                return;
            if (disposing)
            {
                //销毁事务
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
                //关闭记录集
                if (reader != null && reader.IsClosed == false)
                {
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }
                //关闭连接
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                    connection = null;
                }
            }
            alreadyDisposed = true;
        }
    }
}