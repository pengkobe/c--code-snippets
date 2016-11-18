using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace HYD.EMS.DataBase
{
    /// <summary>
    /// Sql数据访问参数集合
    /// </summary>
    public class SqlParameterCollection : ParameterCollection<SqlParameter>
    {
        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="direction">输入输出类型</param>
        public override void Add(string name, object value, DbType dbType, ParameterDirection direction)
        {
            SqlParameter ps = new SqlParameter();
            ps.ParameterName = name;
            ps.Value = value == null ? DBNull.Value : value;
            ps.DbType = dbType;
            ps.Direction = direction;
            this.Add(ps);
        }

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">长度</param> 
        /// <param name="direction">输入输出类型</param> 
        public override void Add(string name, object value, DbType dbType, int size, ParameterDirection direction)
        {
            SqlParameter ps = new SqlParameter();
            ps.ParameterName = name;
            ps.Value = value == null ? DBNull.Value : value;
            ps.DbType = dbType;
            ps.Size = size;
            ps.Direction = direction;
            this.Add(ps);
        }

        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="name">参数名称</param>
        public void Remove(string name)
        {
            SqlParameter o = this[name] as SqlParameter;
            if (o != null)
            {
                this.Remove(o);
            }
        }
    }
}