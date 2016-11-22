/********************************************************************************
** Author: PengYi-PC
** Created On: 2016-01-25
** Description: WeatherCity数据访问
** Modify by : 
** Modify date : 
** Description: 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

using System.Data;
using YIPENG.Business.Model;
using YIPENG.DAL;
using System.Data.Common;

namespace YIPENG.Business.DAL
{
    /// <summary>
    /// 数据访问(自定义部分类，扩展类的方法，实现与自动生成的代码分离,支持DataTable,DataSet,存储过程等)
    /// </summary>
    public  class WeatherCityDao : BaseDao
    {
        public List<WeatherCity> GetAllCity()
        {

            List<WeatherCity> models = new List<WeatherCity>();
            using (DbDataReader dr = this.SqlHelper.ExecuteReader("select * from [WeatherCity] ", null))
            {
                while (dr.Read())
                {
                    WeatherCity model = new WeatherCity();
                    model.CityCode = dr["CityCode"].ToString();
                    model.CityName = dr["CityName"].ToString();
                    models.Add(model);
                }
            }
            return models;
        }
    }
}
