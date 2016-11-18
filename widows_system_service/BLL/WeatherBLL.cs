using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;
using System.Data;
using System.Collections;
using System.IO;

using System.Security.Cryptography;
using System.Web;
using HYD.Business.DAL;
using HYD.Business.Model;
using HYD.EMS.DataBase;

namespace HYD.Business
{
    public class WeatherBLL 
    {
        public WeatherCityDao wcity = new WeatherCityDao();
        public WeatherDataDao wdata = new WeatherDataDao();
        public WeatherDetailDataDao wdatadetail = new WeatherDetailDataDao();

        public List<WeatherCity> getWeatherCities()
        {
             MsSqlHelper msSql = DbUtility.MsSqlHelper;
             try
             {
                 wcity.SqlHelper = msSql;
             }
             finally
             {
                 msSql.Dispose();
             }
            List<WeatherCity> ret = wcity.GetAllCity();
            return ret;
        }

        public void addWeatherData(WeatherData data)
        {
            MsSqlHelper msSql = DbUtility.MsSqlHelper;
            try
            {
                wdata.SqlHelper = msSql;
            }
            finally
            {
                msSql.Dispose();
            }
            wdata.Add(data); 
        }
        
        public void addWeatherDataDetail(WeatherDetailData data)
        {
            MsSqlHelper msSql = DbUtility.MsSqlHelper;
            try
            {
                wdatadetail.SqlHelper = msSql;
            }
            finally
            {
                msSql.Dispose();
            }
            wdatadetail.Add(data); 
        }
    }
}
