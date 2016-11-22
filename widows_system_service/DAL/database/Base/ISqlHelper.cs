using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace YIPENG.DataBase
{
    /// <summary>
    /// ISqlHelper�ӿ�:��SqlHelper��ͳһ����
    /// </summary>
    public interface ISqlHelper<T> : IDisposable
    {

        /// <summary>
        /// ��ȡ��ǰ�����ݿ����Ӷ���
        /// </summary>
        DbConnection Connection
        {
            get;
        }


        /// <summary>
        /// ��������
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// �ύ����
        /// </summary>
        void Commit();

        /// <summary>
        /// �ع�����
        /// </summary>
        void Rollback();

        ///// <summary>
        ///// ִ��SQL���䷵����Ӱ��������
        ///// </summary>
        ///// <param name="cmdType">��������</param>
        ///// <param name="cmdText">SQL����</param>
        ///// <returns>��Ӱ��������</returns>
        //int ExecuteNonQuery(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// ִ��SQL���䷵����Ӱ��������
        ///// </summary>
        ///// <param name="cmdText">SQL����</param>
        ///// <returns>��Ӱ��������</returns>
        //int ExecuteNonQuery(string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// ִ��SQL���䷵�ؼ�¼��
        ///// </summary>
        ///// <param name="cmdType">��������</param>
        ///// <param name="cmdText">SQL����</param>
        ///// <returns>��¼��</returns>
        //DbDataReader ExecuteReader(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// ִ��SQL���䷵�ؼ�¼��
        ///// </summary>
        ///// <param name="cmdText">SQL����</param>
        ///// <returns>��¼��</returns>
        //DbDataReader ExecuteReader(string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// ִ��SQL����
        ///// </summary>
        ///// <param name="cmdType">��������</param>
        ///// <param name="cmdText">SQL����</param>
        ///// <returns>�������ݱ�</returns>
        //DataTable ExecuteTable(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// ִ��SQL����
        ///// </summary>
        ///// <param name="cmdText">SQL����</param>
        ///// <returns>�������ݱ�</returns>
        //DataTable ExecuteTable(string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// ����ִ��SQL������һ��ֵ
        ///// </summary>
        ///// <param name="cmdType">��������</param>
        ///// <param name="cmdText">SQL����</param>
        ///// <returns>ִ�н���</returns>
        //object ExecuteScalar(CommandType cmdType, string cmdText, ParameterCollection<T> dbParameter);

        ///// <summary>
        ///// ����ִ��SQL������һ��ֵ
        ///// </summary>
        ///// <param name="cmdText">SQL����</param>
        ///// <returns>ִ�н���</returns>
        //object ExecuteScalar(string cmdText, ParameterCollection<T> dbParameter);
    }
}