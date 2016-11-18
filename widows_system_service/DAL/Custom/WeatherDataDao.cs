/********************************************************************************
** Author: PengYi-PC
** Created On: 2016-01-25
** Description: WeatherData数据访问
** Modify by : 
** Modify date : 
** Description: 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using HYD.Business.Model;
using HYD.EMS.DAL;
using HYD.EMS.DataBase;
using System.Data;

namespace HYD.Business.DAL
{
    /// <summary>
    /// 数据访问(自定义部分类，扩展类的方法，实现与自动生成的代码分离,支持DataTable,DataSet,存储过程等)
    /// </summary>
    public  class WeatherDataDao : BaseDao
    {
        public bool Add(WeatherData model)
        {
            string sql = "INSERT INTO WeatherData Values(@CollectDate,@CityCode,@HighTemperature,@LowTemperature,@HighHumidity,@LowHumidity,@Notes)";
            SqlParameterCollection ps = new SqlParameterCollection();
            ps.Add("CollectDate", model.CollectDate, DbType.DateTime);
            ps.Add("CityCode", model.CityCode, DbType.String);
            ps.Add("HighTemperature", model.HighTemperature, DbType.Decimal);
            ps.Add("LowTemperature", model.LowTemperature, DbType.Decimal);
            ps.Add("HighHumidity", model.HighTemperature, DbType.Decimal);
            ps.Add("LowHumidity", model.LowTemperature, DbType.Decimal);
            ps.Add("Notes", model.Notes, DbType.String);
            this.SqlHelper.ExecuteNonQuery(sql, ps);
            return true;
        }
    }
}
