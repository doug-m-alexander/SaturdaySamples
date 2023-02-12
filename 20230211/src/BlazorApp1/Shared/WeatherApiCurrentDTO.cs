using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Shared
{
    public class WeatherApiCurrentDTO
    {
        public struct WeatherApiLocation
        {
            public string? name;
            public string? region;
            public string? country;
            public double? lat;
            public double? lon;
            public string? tz_id;
            public int? localtime_epoch;
            public string? localtime;
        }

        public struct WeatherApiCurrentCondition
        {
            public string? text;
            public string? icon;
            public int? code;
        }

        public struct WeatherApiCurrent
        {
            public int? last_updated_epoch;
            public DateTime last_updated;
            public double? temp_c;
            public double? temp_f;
            public bool? is_day;
            public WeatherApiCurrentCondition condition;
            public double wind_mph;
            public double wind_kph;
            public int wind_degree;
            public string? wind_dir;
            public double? pressure_mb;
            public double? pressure_in;
            public double? precip_mm;
            public double? precip_in;
            public int? humidity;
            public int? cloud;
            public double? feelslike_c;
            public double? feelslike_f;
            public double? vis_km;
            public double? vis_miles;
            public double? uv;
            public double? gust_mph;
            public double? gust_kph;
        }

        public WeatherApiCurrentDTO()
        {
            location = new WeatherApiLocation();
            current = new WeatherApiCurrent();
        }

        public WeatherApiLocation location;
        public WeatherApiCurrent current;
    }
}
