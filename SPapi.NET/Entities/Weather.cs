using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SPapi.NET.Entities
{
    public class Weather
    {
        internal Weather() {}

        [JsonProperty("weather")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public Enums.Weather WorldWeather { get; set; }
    }
}
