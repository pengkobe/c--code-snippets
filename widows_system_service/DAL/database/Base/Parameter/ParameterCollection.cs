using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace YIPENG.DataBase
{
    /// <summary>
    /// 数据访问参数集合
    /// </summary>
    public abstract class ParameterCollection<T> : List<T>
    {
        /// <summary>
        /// 根据参数名称返回参数信息
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <returns>值</returns>
        public virtual DbParameter this[string parameterName]
        {
            get
            {
                foreach (T ps in this)
                {
                    DbParameter p = ps as DbParameter;
                    if (parameterName == p.ParameterName)
                    {
                        return p;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="direction">输入输出类型</param>
        public abstract void Add(string name, object value, DbType dbType, ParameterDirection direction);

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">长度</param> 
        /// <param name="direction">输入输出类型</param>
        public abstract void Add(string name, object value, DbType dbType, int size, ParameterDirection direction);

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">长度</param>
        public void Add(string name, object value, DbType dbType, int size)
        {
            this.Add(name, value, dbType, size, ParameterDirection.Input);
        }

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">参数类型</param>
        public void Add(string name, object value, DbType dbType)
        {
            this.Add(name, value, dbType, ParameterDirection.Input);
        }
    }
}