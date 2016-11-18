/********************************************************************************
** Author: PengYi-PC
** Created On: 2016-01-25
** Description: WeatherData实体类
** Modify by : 
** Modify date : 
** Description: 
*********************************************************************************/
using System;
using System.Data.Linq.Mapping;

namespace HYD.Business.Model
{

    public class WeatherData
    {
        
        /// <summary>
        /// CollectDate
        /// </summary>
        public DateTime CollectDate { get; set; }
        
        /// <summary>
        /// CityCode
        /// </summary>
        public string CityCode { get; set; }
        
        /// <summary>
        /// HighTemperature
        /// </summary>
        public decimal ? HighTemperature { get; set; }
        
        /// <summary>
        /// LowTemperature
        /// </summary>
        public decimal ? LowTemperature { get; set; }
        
        /// <summary>
        /// HighHumidity
        /// </summary>
        public decimal ? HighHumidity { get; set; }
        
        /// <summary>
        /// LowHumidity
        /// </summary>
        public decimal ? LowHumidity { get; set; }
        
        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
        
    }
}
