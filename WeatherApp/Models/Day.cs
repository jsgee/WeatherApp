using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace WeatherApp.Models
{
    public class Day
    {
        [JsonProperty("DayDate")]
        public DateTime DayDate { get; set; }
        //  public List<Temperature> DailyTempertures { get; set; }
        [JsonProperty("DailyTemp")]
        public Temperature DailyTemp { get; set; }
       
       
  

    }

    public class Temperature
    {
        //  public DateTime StartTime { get; set; }
      [JsonProperty("DailyTemp.avg")]
        public double avg { get; set; }
        [JsonProperty("DailyTemp.min")]
        public double min { get; set; }
        [JsonProperty("DailyTemp.max")]
        public double max { get; set; }
    }





}

