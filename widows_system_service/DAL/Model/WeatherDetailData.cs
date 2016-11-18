/********************************************************************************
** Author: PengYi-PC
** Created On: 2016-01-25
** Description: WeatherDetailData实体类
** Modify by : 
** Modify date : 
** Description: 
*********************************************************************************/
using System;
using System.Data.Linq.Mapping;

namespace HYD.Business.Model
{
    /// <summary>
    /// WeatherDetailData
    /// </summary>
    public class WeatherDetailData
    {
        
        /// <summary>
        /// CollectTime
        /// </summary>
        public DateTime CollectTime { get; set; }
        
        /// <summary>
        /// CityCode
        /// </summary>
        public string CityCode { get; set; }
        
        /// <summary>
        /// Humidity
        /// </summary>
        public decimal ? Humidity { get; set; }
        
        /// <summary>
        /// Temperature
        /// </summary>
        public decimal ? Temperature { get; set; }
        
        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
        
    }
}
